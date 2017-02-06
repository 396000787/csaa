using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyStack.Modules.iUtility.InterFaceUtility
{
    /// <summary>
    /// 压缩解压缩接口
    /// </summary>
    interface ISizeOperate
    {
        bool Compress(string filepath, string rarpath, string rarname, bool del, bool way);
        bool SecCompressRAR(string filepath, string rarpath, string rarname, bool del, int datano);
        bool UnCompressRAR(string filepath, string rarpath, string rarname, bool del);
        string CompressStr(string str);
        string UnCompressStr(string str);
    }
}
