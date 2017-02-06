using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TinyStack.Modules.iUtility.InterFaceUtility
{
    /// <summary>
    /// 目录文件操作接口
    /// </summary>
    interface IDirectoryFileOperate
    {
        /// <summary>
        /// 根据文件全路径截取文件路径
        /// </summary>
        /// <param name="fileFull">文件全路径</param>
        /// <returns>文件路径</returns>
        string FileSubToPath(string fileFull);

        /// <summary>
        /// 根据文件全路径截取文件名
        /// </summary>
        /// <param name="fileFull">文件全路径</param>
        /// <returns>文件名</returns>
        string FileSubToName(string fileFull);

        /// <summary>
        /// 根据指定路径创建新的文件夹
        /// </summary>
        /// <param name="Path">目标完全路径</param>
        /// <returns>创建结果（true：创建成功，false：创建失败）</returns>
        bool CreatFolder(string Path);

        /// <summary>
        /// 根据指定路径删除文件夹
        /// </summary>
        /// <param name="Path">目标完全路径</param>
        /// <returns>删除结果（true：删除成功，false：删除失败）</returns>
        bool DeleteFolder(string Path);

        /// <summary>
        /// 根据指定路径获取文件夹子文件列表
        /// </summary>
        /// <param name="Path">目标完全路径</param>
        /// <returns>子文件列表<string></returns>
        List<string> GetSubfolderlist(string Path);

        /// <summary>
        /// 根据指定路径获取文件夹子文件列表(所有子目录)
        /// </summary>
        /// <param name="Path">目标完全路径</param>
        /// <returns>子文件列表<string></returns>
        List<string> GetAllfolderlist(string Path);

        /// <summary>
        /// 根据指定路径获取文件夹子文件夹列表
        /// </summary>
        /// <param name="Path">目标完全路径</param>
        /// <returns>子文件夹列表<string></returns>
        List<string> GetFolderItem(string Path);

        /// <summary>
        /// 根据路径获取文件夹信息
        /// </summary>
        /// <param name="Path">目标完全路径</param>
        /// <returns>文件夹信息</returns>
        DirectoryInfo GetFolderInfor(string Path);

        /// <summary>
        /// 判断文件夹是否存在
        /// </summary>
        /// <param name="Path">目标完全路径</param>
        /// <returns>判断结果（true：存在，false：不存在）</returns>
        bool FolderNameExit(string Path);

        /// <summary>
        /// 根据指定路径修改文件夹名称
        /// </summary>
        /// <param name="srcFolderPath">源文件夹完全路径</param>
        /// <param name="destFolderPath">目标文件夹完全路径</param>
        /// <returns>修改结果（true：修改成功，false：修改失败）</returns>
        bool EditFolderName(string srcFolderPath, string destFolderPath);

        /// <summary>
        /// 根据指定路径将文件夹复制到指定的路径下
        /// </summary>
        /// <param name="sourceDir">源文件夹完全路径</param>
        /// <param name="targetDir">目标完全路径</param>
        /// <param name="isDelete">是否删除</param>
        /// <returns>复制结果（true：复制成功，false：复制失败）</returns>
        bool CopyToNewFolder(string sourceDir, string targetDir, bool isDelete);

        /// <summary>
        /// 根据指定路径创建新文件
        /// </summary>
        /// <param name="Path">目标完全路径</param>
        /// <returns>创建结果（true：创建成功，false：创建失败）</returns>
        bool CreatFile(string Path);

        /// <summary>
        /// 根据指定路径删除文件
        /// </summary>
        /// <param name="Path">目标完全路径</param>
        /// <returns>删除结果（true：删除成功，false：删除失败）</returns>
        bool DeleteFile(string Path);

        /// <summary>
        /// 根据指定路径获取文件信息
        /// </summary>
        /// <param name="Path">目标完全路径</param>
        /// <returns>文件信息</returns>
        FileInfo GetFileInfor(string Path);

        /// <summary>
        /// 根据指定路径判断文件是否存在
        /// </summary>
        /// <param name="Path">目标完全路径</param>
        /// <returns>判断结果（true：存在，false：不存在）</returns>
        bool FileNameExit(string Path);

        /// <summary>
        /// 根据指定路径修改文件名称
        /// </summary>
        /// <param name="srcFileName">源文件完全路径</param>
        /// <param name="destFileName">目标文件完全路径</param>
        /// <returns>修改结果（true：修改成功，false：修改失败）</returns>
        bool EditFileName(string srcFileName, string destFileName);

        /// <summary>
        /// 根据指定路径将文件复制到指定的路径下
        /// </summary>
        /// <param name="sourceDir">源文件完全路径</param>
        /// <param name="targetDir">目标文件完全路径</param>
        /// <param name="isDelete">是否删除</param>
        /// <returns>复制结果（true：复制成功，false：复制失败）</returns>
        bool CopyToNewFile(string sourceDir, string targetDir, bool isDelete);

        /// <summary>
        /// 根据指定路径写文件
        /// </summary>
        /// <param name="filePath">文件完全路径</param>
        /// <param name="contentt">文件内容</param>
        /// <param name="overWrite">是否覆盖</param>
        /// <returns>写入结果（true：写入成功，false：写入失败）</returns>
        bool WriteFile(string filePath, string content, bool overWrite);

        /// <summary>
        /// 根据指定路径读取文件内容
        /// </summary>
        /// <param name="filePath">文件完全目录</param>
        /// <returns>文件信息</returns>
        string ReadFile(string filePath);
    }
}
