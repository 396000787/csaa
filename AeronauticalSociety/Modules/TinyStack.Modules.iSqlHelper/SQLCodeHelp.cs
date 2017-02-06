using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using TinyStack.Modules.iUtility.iTools;
using System.Threading;
using System.Configuration;

namespace TinyStack.Modules.iSqlHelper
{
    /// <summary>
    /// 获取SQL语句帮助类
    /// </summary>
    public class SQLCodeHelp
    {

        #region 属性
        ///// <summary>
        ///// 工作目录
        ///// </summary>
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
        /// SQL语句缓存
        /// </summary>
        public static Dictionary<String, String> SQLCode;
        /// <summary>
        /// 文件系统监视器。
        /// </summary>
        //private static FileSystemWatcher _SqlXmlFileWatcher;
        private static MyFileSystemWatcher _MyFileSystemWatcher;
        #endregion

        #region 方法

        #region 静态构造函数
        /// <summary>
        /// 静态构造函数
        /// </summary>
        static SQLCodeHelp()
        {
            //SQL语句所在xml文件路径
            string xmlPath = SQLPath;
            //检查SQLXML路径是否存在
            SQLCodeManager.CheckPath();

            //if (_SqlXmlFileWatcher == null)
            //{
            //    //实例化监视器
            //    _SqlXmlFileWatcher = new FileSystemWatcher(xmlPath);
            //    //启动监控
            //    _SqlXmlFileWatcher.EnableRaisingEvents = true;
            //    //设置监控文件的类型
            //    _SqlXmlFileWatcher.Filter = "*.xml";
            //    //设置文件的文件名、目录名及文件的大小改动会触发Changed事件
            //    _SqlXmlFileWatcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.Size;
            //    //_SqlXmlFileWatcher.NotifyFilter = NotifyFilters.LastWrite;
            //    //当目录内SQLXML文件发生变化时被触发
            //    _SqlXmlFileWatcher.Changed += new FileSystemEventHandler(_SqlXmlFileWatcher_Changed);
            //    //当目录内SQLXML文件被删除时被触发
            //    _SqlXmlFileWatcher.Deleted += new FileSystemEventHandler(_SqlXmlFileWatcher_Deleted);
            //    //当目录内SQLXML文件重命名时被触发
            //    _SqlXmlFileWatcher.Renamed += new RenamedEventHandler(_SqlXmlFileWatcher_Renamed);
            //}
            if (_MyFileSystemWatcher == null)
            {
                _MyFileSystemWatcher = new MyFileSystemWatcher(xmlPath);
                //设置监控文件的类型
                _MyFileSystemWatcher.Filter = "*.xml";
                //设置文件的文件名、目录名及文件的大小改动会触发Changed事件
                _MyFileSystemWatcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.Size;
                //当目录内SQLXML文件发生变化时被触发
                _MyFileSystemWatcher.Change += new FileSystemEventChange(_MyFileSystemWatcher_Change);
                //当目录内SQLXML文件被删除时被触发
                _MyFileSystemWatcher.Deleted += new FileSystemEventDeleted(_MyFileSystemWatcher_Deleted);
                //当目录内SQLXML文件重命名时被触发
                _MyFileSystemWatcher.Renamed += new FileSystemEventRenamed(_MyFileSystemWatcher_Renamed);
                //启动监控
                _MyFileSystemWatcher.Start();
            }
        }

        #endregion

        #region 当目录内SQLXML文件发生变化时被触发
        /// <summary>
        /// 当目录内SQLXML文件发生变化时被触发。
        /// </summary>
        /// <param name="path">文件路径</param>
        static void _MyFileSystemWatcher_Change(string path)
        {
            try
            {
                //获得发生变化文件的文件名
                string _SqlNum = Path.GetFileNameWithoutExtension(path);
                if (SQLCode != null && SQLCode.ContainsKey(_SqlNum))//如果缓存不为null，并且缓存中存在被修改的文件
                {
                    //从缓存移除修改文件
                    SQLCode.Remove(_SqlNum);
                }
                if (SQLCodeManager._SQLCodeEntityList == null)//如果SQLXML列表缓存为空
                {
                    return;
                }
                //根据SQLXML文件路径获取SQL实体
                SQLCodeEntity _SQLCodeEntity = GetSQLCodeEntityByPath(path);
                //从缓存中获得SQL实体
                var _SQLCodeEntityCache = SQLCodeManager._SQLCodeEntityList.FirstOrDefault(S => S.SqlNum == _SqlNum);
                if (_SQLCodeEntity != null && _SQLCodeEntityCache != null && _SQLCodeEntity.SqlNum == _SQLCodeEntityCache.SqlNum && _SQLCodeEntity.SqlDescription == _SQLCodeEntityCache.SqlDescription && _SQLCodeEntity.SQLString == _SQLCodeEntityCache.SQLString)//如果没有改变
                {
                    return;
                }
                if (_SQLCodeEntity == null || _SQLCodeEntity.SqlNum != _SqlNum)//如果不存在本地SQLXML文件，或者序号和文件名不统一
                {
                    if (_SQLCodeEntityCache != null)//如果缓存中存在发生变化的SLQXML
                    {
                        //移除旧的缓存
                        SQLCodeManager._SQLCodeEntityList.Remove(_SQLCodeEntityCache);
                    }
                    if (!File.Exists(path) || Path.GetExtension(path) != ".xml")//如果文件不存在或者新的扩展名不是XML
                    {
                        return;
                    }
                    FileInfo fi = new FileInfo(path);
                    //如果只读则修改为普通
                    fi.Attributes = FileAttributes.Normal;
                    //重命名为源文件名+.bak扩展名
                    fi.MoveTo(path + ".bak");
                }
                else
                {
                    if (_SQLCodeEntityCache != null)//如果缓存中存在发生变化的SLQXML
                    {
                        //移除旧的缓存
                        SQLCodeManager._SQLCodeEntityList.Remove(_SQLCodeEntityCache);
                    }
                    //添加新的缓存
                    SQLCodeManager._SQLCodeEntityList.Add(_SQLCodeEntity);
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region 当目录内SQLXML文件被删除时被触发
        /// <summary>
        /// 当目录内SQLXML文件被删除时被触发。
        /// </summary>
        /// <param name="e">事件信息。</param>
        static void _MyFileSystemWatcher_Deleted(FileSystemEventArgs e)
        {
            try
            {
                //获得发生变化文件的文件名
                string _SqlNum = Path.GetFileNameWithoutExtension(e.FullPath);
                if (SQLCode != null && SQLCode.ContainsKey(_SqlNum))//如果缓存不为null，并且缓存中存在被修改的文件
                {
                    //从缓存移除修改文件
                    SQLCode.Remove(_SqlNum);
                }
                if (SQLCodeManager._SQLCodeEntityList == null)//如果SQLXML列表缓存为空
                {
                    return;
                }
                //从缓存中获得SQL实体
                var _SQLCodeEntityCache = SQLCodeManager._SQLCodeEntityList.FirstOrDefault(S => S.SqlNum == _SqlNum);
                if (_SQLCodeEntityCache != null)//如果缓存中存在发生变化的SLQXML
                {
                    //移除旧的缓存
                    SQLCodeManager._SQLCodeEntityList.Remove(_SQLCodeEntityCache);
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region 当目录内SQLXML文件重命名时被触发
        /// <summary>
        /// 当目录内SQLXML文件重命名时被触发。
        /// </summary>
        /// <param name="e">事件信息。</param>
        static void _MyFileSystemWatcher_Renamed(RenamedEventArgs e)
        {
            try
            {
                //获得发生变化文件的文件名
                string _SqlNum = Path.GetFileNameWithoutExtension(e.OldFullPath);
                string _SqlNumNew = Path.GetFileNameWithoutExtension(e.FullPath);
                if (SQLCode != null && SQLCode.ContainsKey(_SqlNum))//如果缓存不为null，并且缓存中存在被修改的文件
                {
                    //从缓存移除修改文件
                    SQLCode.Remove(_SqlNum);
                }
                if (SQLCodeManager._SQLCodeEntityList == null)//如果SQLXML列表缓存为空
                {
                    return;
                }
                //根据SQLXML文件路径获取SQL实体
                SQLCodeEntity _SQLCodeEntity = GetSQLCodeEntityByPath(e.FullPath);
                //从缓存中获得SQL实体
                var _SQLCodeEntityCache = SQLCodeManager._SQLCodeEntityList.FirstOrDefault(S => S.SqlNum == _SqlNum);
                if (_SQLCodeEntity == null || _SQLCodeEntity.SqlNum != _SqlNumNew)//如果不存在本地SQLXML文件，或者序号和文件名不统一
                {
                    if (_SQLCodeEntityCache != null)//如果缓存中存在发生变化的SLQXML
                    {
                        //移除旧的缓存
                        SQLCodeManager._SQLCodeEntityList.Remove(_SQLCodeEntityCache);
                    }
                    if (!File.Exists(e.FullPath) || Path.GetExtension(e.FullPath) != ".xml")//如果文件不存在或者新的扩展名不是XML
                    {
                        return;
                    }
                    FileInfo fi = new FileInfo(e.FullPath);
                    //如果只读则修改为普通
                    fi.Attributes = FileAttributes.Normal;

                    //重命名为源文件名+.bak扩展名
                    fi.MoveTo(e.FullPath + ".bak");
                }
                else
                {
                    if (_SQLCodeEntityCache != null)//如果缓存中存在发生变化的SLQXML
                    {
                        //移除旧的缓存
                        SQLCodeManager._SQLCodeEntityList.Remove(_SQLCodeEntityCache);
                    }
                    //添加新的缓存
                    SQLCodeManager._SQLCodeEntityList.Add(_SQLCodeEntity);
                }
            }
            catch (Exception ex)
            {

            }
        }

        #endregion

        #region Old

        #region 当目录内SQLXML文件发生变化时被触发
        /// <summary>
        /// 当目录内SQLXML文件发生变化时被触发。
        /// </summary>
        /// <param name="sender">事件触发源。</param>
        /// <param name="e">事件信息。</param>
        //static void _SqlXmlFileWatcher_Changed(object sender, FileSystemEventArgs e)
        //{
        //    try
        //    {
        //        //获得发生变化文件的文件名
        //        string _SqlNum = Path.GetFileNameWithoutExtension(e.FullPath);
        //        if (SQLCode != null && SQLCode.ContainsKey(_SqlNum))//如果缓存不为null，并且缓存中存在被修改的文件
        //        {
        //            //从缓存移除修改文件
        //            SQLCode.Remove(_SqlNum);
        //        }
        //        if (SQLCodeManager._SQLCodeEntityList == null)//如果SQLXML列表缓存为空
        //        {
        //            return;
        //        }
        //        //根据SQLXML文件路径获取SQL实体
        //        SQLCodeEntity _SQLCodeEntity = GetSQLCodeEntityByPath(e.FullPath);
        //        //从缓存中获得SQL实体
        //        var _SQLCodeEntityCache = SQLCodeManager._SQLCodeEntityList.FirstOrDefault(S => S.SqlNum == _SqlNum);
        //        if (_SQLCodeEntity != null && _SQLCodeEntityCache != null && _SQLCodeEntity.SqlNum == _SQLCodeEntityCache.SqlNum && _SQLCodeEntity.SqlDescription == _SQLCodeEntityCache.SqlDescription && _SQLCodeEntity.SQLString == _SQLCodeEntityCache.SQLString)//如果没有改变
        //        {
        //            return;
        //        }
        //        if (_SQLCodeEntity == null || _SQLCodeEntity.SqlNum != _SqlNum)//如果不存在本地SQLXML文件，或者序号和文件名不统一
        //        {
        //            if (_SQLCodeEntityCache != null)//如果缓存中存在发生变化的SLQXML
        //            {
        //                //移除旧的缓存
        //                SQLCodeManager._SQLCodeEntityList.Remove(_SQLCodeEntityCache);
        //            }
        //            if (!File.Exists(e.FullPath) || Path.GetExtension(e.FullPath) != ".xml")//如果文件不存在或者新的扩展名不是XML
        //            {
        //                return;
        //            }
        //            FileInfo fi = new FileInfo(e.FullPath);
        //            //如果只读则修改为普通
        //            fi.Attributes = FileAttributes.Normal;
        //            //重命名为源文件名+.bak扩展名
        //            fi.MoveTo(e.FullPath + ".bak");
        //        }
        //        else
        //        {
        //            if (_SQLCodeEntityCache != null)//如果缓存中存在发生变化的SLQXML
        //            {
        //                //移除旧的缓存
        //                SQLCodeManager._SQLCodeEntityList.Remove(_SQLCodeEntityCache);
        //            }
        //            //添加新的缓存
        //            SQLCodeManager._SQLCodeEntityList.Add(_SQLCodeEntity);
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        #endregion

        #region 当目录内SQLXML文件被删除时被触发
        /// <summary>
        /// 当目录内SQLXML文件被删除时被触发。
        /// </summary>
        /// <param name="sender">事件触发源。</param>
        /// <param name="e">事件信息。</param>
        //static void _SqlXmlFileWatcher_Deleted(object sender, FileSystemEventArgs e)
        //{
        //    try
        //    {
        //        //获得发生变化文件的文件名
        //        string _SqlNum = Path.GetFileNameWithoutExtension(e.FullPath);
        //        if (SQLCode != null && SQLCode.ContainsKey(_SqlNum))//如果缓存不为null，并且缓存中存在被修改的文件
        //        {
        //            //从缓存移除修改文件
        //            SQLCode.Remove(_SqlNum);
        //        }
        //        if (SQLCodeManager._SQLCodeEntityList == null)//如果SQLXML列表缓存为空
        //        {
        //            return;
        //        }
        //        //从缓存中获得SQL实体
        //        var _SQLCodeEntityCache = SQLCodeManager._SQLCodeEntityList.FirstOrDefault(S => S.SqlNum == _SqlNum);
        //        if (_SQLCodeEntityCache != null)//如果缓存中存在发生变化的SLQXML
        //        {
        //            //移除旧的缓存
        //            SQLCodeManager._SQLCodeEntityList.Remove(_SQLCodeEntityCache);
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        #endregion

        #region 当目录内SQLXML文件重命名时被触发
        /// <summary>
        /// 当目录内SQLXML文件重命名时被触发。
        /// </summary>
        /// <param name="sender">事件触发源。</param>
        /// <param name="e">事件信息。</param>
        //static void _SqlXmlFileWatcher_Renamed(object sender, RenamedEventArgs e)
        //{
        //    try
        //    {
        //        //获得发生变化文件的文件名
        //        string _SqlNum = Path.GetFileNameWithoutExtension(e.OldFullPath);
        //        string _SqlNumNew = Path.GetFileNameWithoutExtension(e.FullPath);
        //        if (SQLCode != null && SQLCode.ContainsKey(_SqlNum))//如果缓存不为null，并且缓存中存在被修改的文件
        //        {
        //            //从缓存移除修改文件
        //            SQLCode.Remove(_SqlNum);
        //        }
        //        if (SQLCodeManager._SQLCodeEntityList == null)//如果SQLXML列表缓存为空
        //        {
        //            return;
        //        }
        //        //根据SQLXML文件路径获取SQL实体
        //        SQLCodeEntity _SQLCodeEntity = GetSQLCodeEntityByPath(e.FullPath);
        //        //从缓存中获得SQL实体
        //        var _SQLCodeEntityCache = SQLCodeManager._SQLCodeEntityList.FirstOrDefault(S => S.SqlNum == _SqlNum);
        //        if (_SQLCodeEntity == null || _SQLCodeEntity.SqlNum != _SqlNumNew)//如果不存在本地SQLXML文件，或者序号和文件名不统一
        //        {
        //            if (_SQLCodeEntityCache != null)//如果缓存中存在发生变化的SLQXML
        //            {
        //                //移除旧的缓存
        //                SQLCodeManager._SQLCodeEntityList.Remove(_SQLCodeEntityCache);
        //            }
        //            if (!File.Exists(e.FullPath) || Path.GetExtension(e.FullPath) != ".xml")//如果文件不存在或者新的扩展名不是XML
        //            {
        //                return;
        //            }
        //            FileInfo fi = new FileInfo(e.FullPath);
        //            //如果只读则修改为普通
        //            fi.Attributes = FileAttributes.Normal;

        //            //重命名为源文件名+.bak扩展名
        //            fi.MoveTo(e.FullPath + ".bak");
        //        }
        //        else
        //        {
        //            if (_SQLCodeEntityCache != null)//如果缓存中存在发生变化的SLQXML
        //            {
        //                //移除旧的缓存
        //                SQLCodeManager._SQLCodeEntityList.Remove(_SQLCodeEntityCache);
        //            }
        //            //添加新的缓存
        //            SQLCodeManager._SQLCodeEntityList.Add(_SQLCodeEntity);
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        #endregion

        #endregion

        #region 根据SQL语句序号获取SQL语句
        /// <summary>
        /// 根据SQL语句序号获取SQL语句
        /// </summary>
        /// <param name="SqlNum">SQL语句序号</param>
        /// <returns>SQL语句</returns>
        public String GetSQLCode(String SqlNum)
        {
            //SQL语句
            string SQLString = "";
            try
            {
                if (SQLCode == null)//如果缓存为null
                {
                    SQLCode = new Dictionary<string, string>();
                }

                if (!SQLCode.ContainsKey(SqlNum))//如果缓存中不存在SQL语句
                {
                    //SQL语句所在xml文件路径
                    string xmlPath = Path.Combine( SQLPath, SqlNum + ".xml");
                    //if (!File.Exists(xmlPath))
                    //{
                    //    return null;
                    //}
                    //实例化XML文件操作类
                    //XmlOperate _XmlOperate = new XmlOperate();
                    //判断配置文件是否只读
                    //FileInfo fi = new FileInfo(xmlPath);
                    //如果只读则修改为普通
                    //fi.Attributes = FileAttributes.Normal;
                    //实例化文件操作类
                    //DirectoryFileOperate _DirectoryFileOperate = new DirectoryFileOperate();
                    ////根据指定路径读取文件
                    //string _XmlContent = _DirectoryFileOperate.ShareReadFile(xmlPath);
                    ////把xml转换成对象
                    //SQLCodeEntity _SQLCodeEntity = _XmlOperate.SimpleXmlToObject<SQLCodeEntity>(_XmlContent);
                    SQLCodeEntity _SQLCodeEntity = SQLCodeHelp.GetSQLCodeEntityByPath(xmlPath);
                    SQLString = _SQLCodeEntity.SQLString;
                    //放入缓存
                    SQLCode.Add(SqlNum, SQLString);
                }
                else//如果缓存中存在SQL语句
                {
                    //从缓存中获取SQL语句
                    SQLString = SQLCode[SqlNum];
                }
            }
            catch (Exception ex)
            {
                return "";
            }
            return SQLString;
        }
        #endregion

        #region 根据SQLXML文件路径获取SQL实体
        /// <summary>
        /// 根据SQLXML文件路径获取SQL实体
        /// </summary>
        /// <param name="_Path">SQLXML路径</param>
        /// <returns>SQLXML实体</returns>
        public static SQLCodeEntity GetSQLCodeEntityByPath(String _Path)
        {
            SQLCodeEntity _SQLCodeEntity = null;
            try
            {
                if (!File.Exists(_Path))
                {
                    return null;
                }
                //实例化XML文件操作类
                XmlOperate _XmlOperate = new XmlOperate();
                //判断配置文件是否只读
                FileInfo fi = new FileInfo(_Path);
                //如果只读则修改为普通
                fi.Attributes = FileAttributes.Normal;
                //实例化文件操作类
                DirectoryFileOperate _DirectoryFileOperate = new DirectoryFileOperate();
                //根据指定路径读取文件
                string _XmlContent = _DirectoryFileOperate.ShareReadFile(_Path);
                //把xml转换成对象
                _SQLCodeEntity = _XmlOperate.SimpleXmlToObject<SQLCodeEntity>(_XmlContent);
            }
            catch (Exception ex)
            {
                return null;
            }
            return _SQLCodeEntity;
        }
        #endregion

        #endregion
    }
}
