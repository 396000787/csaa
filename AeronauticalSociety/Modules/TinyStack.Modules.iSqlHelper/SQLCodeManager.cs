using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyStack.Modules.iUtility;
using TinyStack.Modules.iUtility.iTools;
using System.IO;
using System.Runtime.Serialization;
using System.Configuration;

namespace TinyStack.Modules.iSqlHelper
{
    public class SQLCodeManager
    {
        #region 属性
        /// <summary>
        /// 工作目录
        /// </summary>
        //string WorkDir = iConfig.iConfig.WorkDir;
        /// <summary>
        /// SQL语句文件存储目录
        /// </summary>
        static string SQLPath
        {
            get
            {
                string SqlXml = ConfigurationManager.AppSettings["SQLXmlPath"];
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SqlXml);
            }
        }
        /// <summary>
        /// SQLXML列表缓存
        /// </summary>
        public static List<SQLCodeEntity> _SQLCodeEntityList;
        #endregion

        #region 根据SQL序号、SQL说明和SQL语句获得SQL语句列表
        /// <summary>
        /// 根据SQL序号、SQL说明和SQL语句获得SQL语句列表
        /// </summary>
        /// <param name="parameters">页面传递的参数</param>
        /// <returns>SQL对象列表</returns>
        public object GetSqlList(Dictionary<String, String> parameters)
        {
            try
            {
                //检查SQLXML路径是否存在
                CheckPath();
                //SQL语句所在xml文件路径
                string xmlPath = SQLPath;
                //if (!Directory.Exists(xmlPath))//如果路径不存在
                //{
                //    return null;
                //}

                if (_SQLCodeEntityList == null || _SQLCodeEntityList.Count == 0)
                {
                    //将指定路径下所有SQLXML转换成SQLCodeEntity对象列表
                    _SQLCodeEntityList = ConvertXmlToEntity(xmlPath);
                }
                List<SQLCodeEntity> _TempSQLCodeEntityList = _SQLCodeEntityList.ToList();
                //将指定路径下所有SQLXML转换成SQLCodeEntity对象列表
                //List<SQLCodeEntity> _SQLCodeEntityList = ConvertXmlToEntity(xmlPath);

                JsonOperate _JsonOperate = new JsonOperate();
                //获取SQL序号
                string _SqlNum = parameters.ContainsKey("SqlNum") ? _JsonOperate.FromJsonString<string>(parameters["SqlNum"]) : "";
                //获取SQL说明
                string _SqlDescription = parameters.ContainsKey("SqlDescription") ? _JsonOperate.FromJsonString<string>(parameters["SqlDescription"]) : "";
                //获取SQL语句
                string _SQLString = parameters.ContainsKey("SQLString") ? _JsonOperate.FromJsonString<string>(parameters["SQLString"]) : "";
                //获取每页显示条数
                int _pageSize = parameters.ContainsKey("pageSize") ? Convert.ToInt32(parameters["pageSize"]) : 0;
                //获取当前页码
                int _pageIndex = parameters.ContainsKey("pageIndex") ? Convert.ToInt32(parameters["pageIndex"]) - 1 : 0;

                //判断是否根据SQL序号查询
                if (!string.IsNullOrEmpty(_SqlNum))
                {
                    _TempSQLCodeEntityList = _TempSQLCodeEntityList.Where(S => S.SqlNum.Contains(_SqlNum)).ToList();
                }
                //判断是否根据SQL说明查询
                if (!string.IsNullOrEmpty(_SqlDescription))
                {
                    _TempSQLCodeEntityList = _TempSQLCodeEntityList.Where(S => S.SqlDescription.ToLower().Contains(_SqlDescription.ToLower())).ToList();
                }
                //判断是否根据SQL语句查询
                if (!string.IsNullOrEmpty(_SQLString))
                {
                    _TempSQLCodeEntityList = _TempSQLCodeEntityList.Where(S => S.SQLString.ToLower().Contains(_SQLString.ToLower())).ToList();
                }
                //按序号排序
                var _SQLList = from S in _TempSQLCodeEntityList
                               orderby S.SqlNum
                               select
                                   new
                                   {
                                       SqlNum = S.SqlNum,
                                       SqlDescription = S.SqlDescription
                                   };

                IEnumerable<object> result = _SQLList;
                //定义总页数
                int _pageCount;
                //分页
                result = result.TakePage(_pageSize, _pageIndex, out _pageCount);

                return new { PageSize = _pageSize, PageIndex = _pageIndex + 1, PageCount = _pageCount, Methods = result };
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region 根据SQL序号修改SQL文件
        /// <summary>
        /// 根据SQL序号修改SQL文件
        /// </summary>
        /// <param name="parameters">页面传递的参数</param>
        /// <returns>true：成功，false：失败</returns>
        public object EditSql(Dictionary<String, String> parameters)
        {
            try
            {
                JsonOperate _JsonOperate = new JsonOperate();
                //获取SQL序号
                string _SqlNum = parameters.ContainsKey("SqlNum") ? _JsonOperate.FromJsonString<string>(parameters["SqlNum"]) : "";
                //获取SQL说明
                string _SqlDescription = parameters.ContainsKey("SqlDescription") ? _JsonOperate.FromJsonString<string>(parameters["SqlDescription"]) : "";
                //获取SQL语句
                string _SQLString = parameters.ContainsKey("SQLString") ? _JsonOperate.FromJsonString<string>(parameters["SQLString"]) : "";

                //SQL语句所在xml文件路径
                string xmlPath = Path.Combine(SQLPath, _SqlNum + ".xml");
                if (!File.Exists(xmlPath))
                {
                    return new { Methods = false };
                }
                //实例化XML文件操作类
                XmlOperate _XmlOperate = new XmlOperate();
                //实例化SQL实体类
                SQLCodeEntity _SQLCodeEntity = new SQLCodeEntity();
                _SQLCodeEntity.SqlNum = _SqlNum;
                _SQLCodeEntity.SqlDescription = _SqlDescription;
                _SQLCodeEntity.SQLString = _SQLString;

                //转换成xml字符串
                string _Xmlstring = _XmlOperate.ObjectToSimpleXml(_SQLCodeEntity);
                bool _IsSuccess = SaveXml(_Xmlstring, xmlPath);
                return new { Methods = _IsSuccess };
            }
            catch
            {
                return new { Methods = false };
            }
        }
        #endregion

        #region 根据SQL序号删除SQLXML文件
        /// <summary>
        /// 根据SQL序号删除SQLXML文件
        /// </summary>
        /// <param name="parameters">页面传递的参数</param>
        /// <returns>true：成功，false：失败</returns>
        public object DeleteSql(Dictionary<String, String> parameters)
        {
            try
            {
                JsonOperate _JsonOperate = new JsonOperate();
                //获取SQL序号
                string _SqlNum = parameters.ContainsKey("SqlNum") ? _JsonOperate.FromJsonString<string>(parameters["SqlNum"]) : "";

                //SQL语句所在xml文件路径
                string xmlPath = Path.Combine(SQLPath, _SqlNum + ".xml");
                //判断是否存在文件
                if (!File.Exists(xmlPath))
                {
                    return new { Methods = false };
                }
                //判断配置文件是否只读
                FileInfo fi = new FileInfo(xmlPath);
                //如果只读则修改为普通
                //if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
                //{
                fi.Attributes = FileAttributes.Normal;
                //}

                //删除SQLXML文件
                File.Delete(xmlPath);

                return new { Methods = true };
            }
            catch
            {
                return new { Methods = false };
            }
        }
        #endregion

        #region 添加SQLXML文件
        /// <summary>
        /// 添加SQLXML文件
        /// </summary>
        /// <param name="parameters">页面传递的参数</param>
        /// <returns>true：成功，false：失败</returns>
        public object AddSql(Dictionary<String, String> parameters)
        {
            try
            {
                ////XML存储路径
                //string _XmlOutPath = Path.Combine(WorkDir, SQLPath);
                //if (!Directory.Exists(_XmlOutPath))//如果路径不存在
                //{
                //    //创建路径
                //    Directory.CreateDirectory(_XmlOutPath);
                //}

                //检查SQLXML路径是否存在
                CheckPath();
                JsonOperate _JsonOperate = new JsonOperate();
                //获取SQL序号
                string _SqlNum = parameters.ContainsKey("SqlNum") ? _JsonOperate.FromJsonString<string>(parameters["SqlNum"]) : "";
                //获取SQL说明
                string _SqlDescription = parameters.ContainsKey("SqlDescription") ? _JsonOperate.FromJsonString<string>(parameters["SqlDescription"]) : "";
                //获取SQL语句
                string _SQLString = parameters.ContainsKey("SQLString") ? _JsonOperate.FromJsonString<string>(parameters["SQLString"]) : "";

                //SQL语句所在xml文件路径
                string xmlPath = Path.Combine(SQLPath, _SqlNum + ".xml");
                if (File.Exists(xmlPath))//如果存在文件，返回false
                {
                    return new { Methods = false };
                }
                if (string.IsNullOrEmpty(_SqlNum) || string.IsNullOrEmpty(_SqlDescription) || string.IsNullOrEmpty(_SQLString))
                {
                    return new { Methods = false };
                }
                //实例化XML文件操作类
                XmlOperate _XmlOperate = new XmlOperate();
                //实例化SQL实体类
                SQLCodeEntity _SQLCodeEntity = new SQLCodeEntity();
                _SQLCodeEntity.SqlNum = _SqlNum;
                _SQLCodeEntity.SqlDescription = _SqlDescription;
                _SQLCodeEntity.SQLString = _SQLString;

                //转换成xml字符串
                string _XmlString = _XmlOperate.ObjectToSimpleXml(_SQLCodeEntity);
                //生成SQLXML文件
                File.WriteAllText(xmlPath, _XmlString);
                return new { Methods = true };
            }
            catch
            {
                return new { Methods = false };
            }
        }
        #endregion

        #region 根据SQL序号获得SQL信息
        /// <summary>
        /// 根据SQL序号获得SQL信息
        /// </summary>
        /// <param name="parameters">页面传递的参数</param>
        /// <returns>SQL信息</returns>
        public object GetSqlByNum(Dictionary<String, String> parameters)
        {
            try
            {
                JsonOperate _JsonOperate = new JsonOperate();
                //获取SQL序号
                string _SqlNum = parameters.ContainsKey("SqlNum") ? _JsonOperate.FromJsonString<string>(parameters["SqlNum"]) : "";

                //SQLCodeEntity _SQLCodeEntity;
                if (_SQLCodeEntityList != null && SQLCodeManager._SQLCodeEntityList.FirstOrDefault(S => S.SqlNum == _SqlNum) != null)
                {
                    //从缓存中获得SQL实体
                    SQLCodeEntity _SQLCodeEntity = SQLCodeManager._SQLCodeEntityList.FirstOrDefault(S => S.SqlNum == _SqlNum);

                    return new { Methods = _SQLCodeEntity };
                }
                else
                {
                    //SQL语句所在xml文件路径
                    string xmlPath = Path.Combine(SQLPath, _SqlNum + ".xml");
                    //判断是否存在文件
                    if (!File.Exists(xmlPath))
                    {
                        return new { Methods = "" };
                    }
                    //实例化XML文件操作类
                    //XmlOperate _XmlOperate = new XmlOperate();
                    //判断配置文件是否只读
                    //FileInfo fi = new FileInfo(xmlPath);
                    ////如果只读则修改为普通
                    //if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
                    //{
                    //    fi.Attributes = FileAttributes.Normal;
                    //}
                    ////实例化文件操作类
                    //DirectoryFileOperate _DirectoryFileOperate = new DirectoryFileOperate();
                    ////根据指定路径读取文件
                    //string _XmlContent = _DirectoryFileOperate.ShareReadFile(xmlPath);
                    ////把xml转换成对象
                    //SQLCodeEntity _SQLCodeEntity = _XmlOperate.SimpleXmlToObject<SQLCodeEntity>(_XmlContent);
                    SQLCodeEntity _SQLCodeEntity = SQLCodeHelp.GetSQLCodeEntityByPath(xmlPath);

                    return new { Methods = _SQLCodeEntity };
                }
            }
            catch
            {
                return new { Methods = "" };
            }
        }
        #endregion

        #region 获取指定路径下所有xml文件列表

        /// <summary>
        /// 获取指定路径下所有xml文件列表
        /// </summary>
        /// <param name="_Path">目标完全路径</param>
        /// <returns>xml文件路径列表</returns>
        private List<string> GetAllXmlFilelist(string _Path)
        {
            DirectoryFileOperate df = new DirectoryFileOperate();
            List<string> _XmlPathlist = new List<string>();
            _XmlPathlist = df.GetSubfolderlist(_Path);
            for (int i = 0; i < _XmlPathlist.Count; i++)
            {
                //移除目录中非xml文件
                if (!_XmlPathlist[i].ToString().EndsWith(".xml"))
                {
                    _XmlPathlist.RemoveAt(i);
                }
            }
            return _XmlPathlist;
        }
        #endregion

        #region 读取指定目录下的所有xml文件,将其转换为SQL语句实体对象列表
        /// <summary>
        /// 读取指定目录下的所有xml文件,将其转换为SQL语句实体对象列表
        /// </summary>
        /// <param name="path">xml文件的路径</param>
        /// <returns>SQL语句实体对象列表</returns>
        private List<SQLCodeEntity> ConvertXmlToEntity(String _Path)
        {
            try
            {
                List<SQLCodeEntity> _SQLCodeEntityLsit = new List<SQLCodeEntity>();
                DirectoryInfo directory = new DirectoryInfo(_Path);
                //DirectoryFileOperate df = new DirectoryFileOperate();
                XmlOperate _XmlOperate = new XmlOperate();

                if (directory.Exists)//判断目录是否存在
                {
                    List<string> _XmlPathlist = GetAllXmlFilelist(_Path);
                    //将xml文件转为iLogObject对象
                    foreach (string _path in _XmlPathlist)
                    {
                        //判断配置文件是否只读
                        FileInfo fi = new FileInfo(_path);
                        //如果只读则修改为普通
                        //if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
                        //{
                        fi.Attributes = FileAttributes.Normal;
                        //}
                        //string _XmlContent = df.ShareReadFile(_path);
                        ////把xml转换成对象
                        //SQLCodeEntity _SQLCodeEntity = _XmlOperate.SimpleXmlToObject<SQLCodeEntity>(_XmlContent);
                        SQLCodeEntity _SQLCodeEntity = SQLCodeHelp.GetSQLCodeEntityByPath(_path);
                        if (_SQLCodeEntity == null || _SQLCodeEntity.SqlNum != Path.GetFileNameWithoutExtension(_path))
                        {
                            //重命名为源文件名+.bak扩展名
                            fi.MoveTo(_path + ".bak");
                            continue;
                        }
                        _SQLCodeEntityLsit.Add(_SQLCodeEntity);
                    }
                }
                return _SQLCodeEntityLsit;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 保存SQLXML文件
        /// <summary>
        /// 保存SQLXML文件
        /// </summary>
        /// <param name="_XmlString">保存xml字符串</param>
        /// <param name="_configPath">SQLXML文件地址</param>
        /// <returns>执行结果</returns>
        private bool SaveXml(string _XmlString, string _configPath)
        {
            try
            {
                //判断路径是否为空
                if (string.IsNullOrEmpty(_XmlString) || string.IsNullOrEmpty(_configPath))
                {
                    return false;
                }
                //获取文件存储文件夹路径 
                iUtility.iTools.DirectoryFileOperate _DirectoryFileOperate = new DirectoryFileOperate();
                //string DirPath = _DirectoryFileOperate.FileSubToPath(_configPath);
                //string FileName = Guid.NewGuid().ToString();
                //string temppath = Path.Combine(DirPath, FileName + ".xml.bak");

                ////生成临时文件
                //File.WriteAllText(temppath, _XmlString);
                ////判断配置文件是否只读
                //FileInfo fi = new FileInfo(_configPath);
                ////如果只读则修改为普通
                //if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
                //{
                //    fi.Attributes = FileAttributes.Normal;
                //}

                ////重命名                                
                //File.Copy(temppath, _configPath, true);
                ////删除临时文件
                //File.Delete(temppath);
                File.WriteAllText(_configPath, _XmlString);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region 检查工作路径是否存在
        /// <summary>
        /// 检查SQLXML路径是否存在
        /// </summary>
        public static void CheckPath()
        {
            //如果路径不存在，返回
            if (string.IsNullOrEmpty(SQLPath))
            {
                return;
            }
            //SQL语句所在xml文件路径
            // string xmlPath = Path.Combine(iConfig.iConfig.WorkDir, iConfig.iConfig.SQLXmlPath);
            if (!Directory.Exists(SQLPath))//如果路径不存在
            {
                //创建路径
                Directory.CreateDirectory(SQLPath);
            }
        }
        #endregion
    }
}
