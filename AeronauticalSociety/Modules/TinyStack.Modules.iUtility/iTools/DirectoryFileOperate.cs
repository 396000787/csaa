using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyStack.Modules.iUtility.InterFaceUtility;
using System.IO;

namespace TinyStack.Modules.iUtility.iTools
{
    public class DirectoryFileOperate : IDirectoryFileOperate
    {
        #region 根据文件全路径截取文件路径
        /// <summary>
        /// 根据文件全路径截取文件路径
        /// </summary>
        /// <param name="fileFull">文件全路径</param>
        /// <returns>文件路径</returns>
        public string FileSubToPath(string fileFull)
        {
            return fileFull.Substring(0, fileFull.LastIndexOf('\\'));
        }
        #endregion

        #region 根据文件全路径截取文件名
        /// <summary>
        /// 根据文件全路径截取文件名
        /// </summary>
        /// <param name="fileFull">文件全路径</param>
        /// <returns>文件名</returns>
        public string FileSubToName(string fileFull)
        {
            return fileFull.Substring(fileFull.LastIndexOf('\\') + 1);
        }
        #endregion

        #region 根据指定路径创建新的文件夹
        /// <summary>
        /// 根据指定路径创建新的文件夹
        /// </summary>
        /// <param name="Path">目标完全路径</param>
        /// <returns>创建结果（true：创建成功，false：创建失败）</returns>
        public bool CreatFolder(string Path)
        {
            try
            {
                //判断文件是否存在
                if (!Directory.Exists(Path))
                {
                    //不存在创建文件
                    Directory.CreateDirectory(Path);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 根据指定路径删除文件夹
        /// <summary>
        /// 根据指定路径删除文件夹
        /// </summary>
        /// <param name="Path">目标完全路径</param>
        /// <returns>删除结果（true：删除成功，false：删除失败）</returns>
        public bool DeleteFolder(string Path)
        {
            try
            {
                //判断文件是否存在
                if (Directory.Exists(Path))
                {
                    //删除文件夹 
                    Directory.Delete(Path, true);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 根据指定路径获取文件夹子文件列表
        /// <summary>
        /// 根据指定路径获取文件夹子文件列表(当前目录)
        /// </summary>
        /// <param name="Path">目标完全路径</param>
        /// <returns>子文件列表</returns>
        public List<string> GetSubfolderlist(string Path)
        {
            //实例化操作类
            DirectoryInfo directory = new DirectoryInfo(Path);
            List<string> list = new List<string>();
            //判断目录目录是否存在
            if (directory.Exists)
            {
                //获取子文件列表
                FileInfo[] files = directory.GetFiles();
                foreach (FileInfo file in files)
                {
                    list.Add(file.FullName);
                }
            }
            //返回子文件列表
            return list;
        }
        /// <summary>
        /// 根据指定路径获取文件夹子文件列表(所有子目录)
        /// </summary>
        /// <param name="Path">目标完全路径</param>
        /// <returns>子文件列表<string></returns>
        public List<string> GetAllfolderlist(string Path)
        {
            //实例化操作类
            DirectoryInfo directory = new DirectoryInfo(Path);
            List<string> list = new List<string>();
            List<string> l = GetFolderItem(Path);
            foreach (string path in l)
            {
                list.AddRange(GetSubfolderlist(path));
            }
            return list;
        }
        #endregion

        #region 根据指定路径获取文件夹子文件夹列表
        /// <summary>
        /// 根据指定路径获取文件夹子文件夹列表
        /// </summary>
        /// <param name="Path">目标完全路径</param>
        /// <returns>子文件夹列表<string></returns>
        public List<string> GetFolderItem(string Path)
        {
            //实例化操作类
            DirectoryInfo directory = new DirectoryInfo(Path);
            List<string> list = new List<string>();
            //判断路径是否存在
            if (directory.Exists)
            {
                //获取子文件列表
                DirectoryInfo[] dirs = directory.GetDirectories();
                foreach (DirectoryInfo d in dirs)
                {
                    list.Add(d.FullName);
                    list.AddRange(GetFolderItem(d.FullName));
                }
            }
            //返回子文件列表
            return list;
        }
        #endregion

        #region 根据路径获取文件夹信息
        /// <summary>
        /// 根据路径获取文件夹信息
        /// </summary>
        /// <param name="Path">目标完全路径</param>
        /// <returns>文件夹信息</returns>
        public DirectoryInfo GetFolderInfor(string Path)
        {
            //实例化操作类并获取文件夹对象
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Path);           
            // 返回文件夹信息
            return dir;
        }
        #endregion

        #region 判断文件夹是否存在
        /// <summary>
        /// 判断文件夹是否存在
        /// </summary>
        /// <param name="Path">目标完全路径</param>
        /// <returns>判断结果（true：存在，false：不存在）</returns>
        public bool FolderNameExit(string Path)
        {
            //判断文件夹是否存在
            if (Directory.Exists(Path))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region 根据指定路径修改文件夹名称
        /// <summary>
        /// 根据指定路径修改文件夹名称
        /// </summary>
        /// <param name="srcFolderPath">源文件完全夹路径</param>
        /// <param name="destFolderPath">目标文件夹路径</param>
        /// <returns>修改结果（true：修改成功，false：修改失败）</returns>
        public bool EditFolderName(string srcFolderPath, string destFolderPath)
        {
            try
            {
                //判断源文件夹路径是否存在
                if (System.IO.Directory.Exists(srcFolderPath))
                {
                    //修改文件夹名称
                    Directory.Move(srcFolderPath, destFolderPath);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 根据指定路径将文件夹复制到指定的路径下
        /// <summary>
        /// 根据指定路径将文件夹复制到指定的路径下
        /// </summary>
        /// <param name="sourceDir">源文件夹完全路径</param>
        /// <param name="targetDir">目标文件夹完全路径</param>
        /// <param name="isDelete">是否删除</param>
        /// <returns>复制结果（true：复制成功，false：复制失败）</returns>
        public bool CopyToNewFolder(string sourceDir, string targetDir, bool isDelete)
        {
            try
            {
                if (sourceDir.Equals(targetDir)) throw new Exception("目标文件夹与源文件夹相同");
                //创建文件夹
                CreatFolder(targetDir);
                //获取源目录下文件列表
                string[] files = Directory.GetFiles(sourceDir);
                string fileName = string.Empty;
                for (int i = 0; i < files.Length; i++)
                {
                    fileName = files[i].Substring(files[i].LastIndexOf("\\") + 1);
                    File.Copy(files[i], targetDir + "\\" + fileName, true);
                }
                //实例化操作类
                DirectoryInfo source = new DirectoryInfo(sourceDir);
                //获取当前目录的子目录
                DirectoryInfo[] dirs = source.GetDirectories();
                for (int j = 0; j < dirs.Length; j++)
                {
                    //copy文件夹
                    CopyToNewFolder(dirs[j].FullName, string.Concat(targetDir, "\\", dirs[j].Name), isDelete);
                }
                //判断源文件是否删除
                if (isDelete == true)
                {
                    //删除源文件
                    DeleteFolder(sourceDir);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 根据指定路径创建新文件
        /// <summary>
        /// 根据指定路径创建新文件
        /// </summary>
        /// <param name="Path">目标完全路径</param>
        /// <returns>创建结果（true:创建成功，false：创建失败）</returns>
        public bool CreatFile(string Path)
        {
            try
            {
                //判断目录路径是否存在该文件
                if (!File.Exists(Path))
                {
                    //创建文件
                    File.Create(Path);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 根据指定路径删除文件
        /// <summary>
        /// 根据指定路径删除文件
        /// </summary>
        /// <param name="Path">目标完全路径</param>
        /// <returns>删除结果（true：删除成功，false：返回失败）</returns>
        public bool DeleteFile(string Path)
        {
            try
            {
                //判断目录路径是否存在该文件
                if (File.Exists(Path))
                {
                    //删除文件
                    File.Delete(Path);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 根据指定路径获取文件信息
        /// <summary>
        /// 根据指定路径获取文件信息
        /// </summary>
        /// <param name="Path">目标完全路径</param>
        /// <returns>文件信息</returns>
        public FileInfo GetFileInfor(string Path)
        {
            //实例化操作类
            System.IO.FileInfo _FileInfo = new System.IO.FileInfo(Path);           
            // 返回文件信息
            return _FileInfo;
        }
        #endregion

        #region 根据指定路径判断文件是否存在
        /// <summary>
        /// 根据指定路径判断文件是否存在
        /// </summary>
        /// <param name="Path">目标完全路径</param>
        /// <returns>判断结果（true：存在，false：不存在）</returns>
        public bool FileNameExit(string Path)
        {
            //判断指定路径下文件是否存在
            if (File.Exists(Path))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region 根据指定路径修改文件名称
        /// <summary>
        /// 根据指定路径修改文件名称
        /// </summary>
        /// <param name="srcFileName">源文件完全路径</param>
        /// <param name="destFileName">目标文件完全路径</param>
        /// <returns>修改结果（true：修改成功，false：修改失败）</returns>
        public bool EditFileName(string srcFileName, string destFileName)
        {
            try
            {
                //判断源文件路径下是否存在该文件
                if (System.IO.File.Exists(srcFileName))
                {
                    //修改文件
                    File.Move(srcFileName, destFileName);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 根据指定路径将文件复制到指定的路径下
        /// <summary>
        /// 根据指定路径将文件复制到指定的路径下
        /// </summary>
        /// <param name="sourceDir">源文件完全路径</param>
        /// <param name="targetDir">目标完全路径</param>
        /// <param name="isDelete">是否删除</param>
        /// <returns>复制结果（true：复制成功，false：复制失败）</returns>
        public bool CopyToNewFile(string sourceDir, string targetDir, bool isDelete)
        {
            try
            {
                //检查源文件路径是否正确
                if (string.IsNullOrEmpty(sourceDir))
                {
                    return false;
                }
                //copy文件
                File.Copy(sourceDir, targetDir, true);
                //判断是否删除源文件
                if (isDelete == true)
                {
                    //删除源文件
                    DeleteFile(sourceDir);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 根据指定路径写文件
        /// <summary>
        /// 根据指定路径写文件
        /// </summary>
        /// <param name="filePath">文件完全路径</param>
        /// <param name="contentt">文件内容</param>
        /// <param name="overWrite">是否覆盖</param>
        /// <returns>写入结果（true：写入成功，false：写入失败）</returns>
        public bool WriteFile(string filePath, string content, bool overWrite)
        {
            FileStream nFile;
            StreamWriter writer;
            try
            {
                //判断是否覆盖文件还是在源文件最后追加文件内容
                if (!overWrite)
                {
                    //在文件里追加文件内容
                    nFile = new FileStream(filePath, FileMode.Append);
                }
                else
                {
                    //覆盖源文件，重新创建文件
                    nFile = new FileStream(filePath, FileMode.Create);
                }
                writer = new StreamWriter(nFile);
                writer.Write(content);
                writer.Close();
                nFile.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 根据指定路径写文件
        /// <summary>
        /// 根据指定路径写文件
        /// </summary>
        /// <param name="filePath">文件完全路径</param>
        /// <param name="content">文件内容</param>
        /// <param name="overWrite">是否覆盖</param>
        /// <param name="_encoding">编码规范</param>
        /// <returns>写入结果（true：写入成功，false：写入失败）</returns>
        public bool WriteFile(string filePath, string content, bool overWrite, Encoding _encoding)
        {
            FileStream nFile;
            StreamWriter writer;
            try
            {

                //判断是否覆盖文件还是在源文件最后追加文件内容
                if (overWrite == true)
                {
                    //在文件里追加文件内容
                    nFile = new FileStream(filePath, FileMode.Append);
                }
                else
                {
                    //覆盖源文件，重新创建文件
                    nFile = new FileStream(filePath, FileMode.Create);
                }
                writer = new StreamWriter(nFile, _encoding);
                writer.WriteLine(content);
                writer.Close();
                nFile.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 根据指定路径读取文件
        /// <summary>
        /// 根据指定路径读取文件
        /// </summary>
        /// <param name="filePath">文件完全目录</param>
        /// <returns>文件内信息</returns>
        public string ReadFile(string filePath)
        {
            FileStream file;
            file = new FileStream(filePath, FileMode.Open);
            //实例化操作类
            StreamReader read = new StreamReader(file);
            //读取文件信息
            string contents = read.ReadToEnd();
            read.Close();
            file.Close();
            //返回文件信息
            return contents;
        }
        #endregion

        #region 根据指定路径读取文件（共享式）
        /// <summary>
        /// 根据指定路径读取文件（共享式）
        /// </summary>
        /// <param name="filePath">文件完全目录</param>
        /// <returns>文件内信息</returns>
        public string ShareReadFile(string filePath)
        {
            FileInfo file1 = new FileInfo(filePath);
            FileStream fs = file1.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            StreamReader sr = new StreamReader(fs);
            string contents = sr.ReadToEnd();
            sr.Close();
            fs.Close();
            return contents;


            //FileStream fs = null;
            ////实例化操作类
            //StreamReader sr = null;
            //string contents = "";
            //try
            //{
            //    fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            //    //实例化操作类
            //    sr = new StreamReader(fs);
            //    //读取文件信息
            //    contents = sr.ReadToEnd();
            //    fs.Close();
            //    sr.Close();
            //}
            //catch (Exception ex)
            //{
            //    sr.Close();
            //    fs.Close();
                
            //    return null;
            //}
            //finally
            //{
            //    sr.Close();
            //    fs.Close();
                
            //}
            //return contents;
        }
        #endregion

    }
}
