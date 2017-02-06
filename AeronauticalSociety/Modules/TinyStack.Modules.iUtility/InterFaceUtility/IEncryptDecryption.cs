using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyStack.Modules.iUtility.InterFaceUtility
{
    /// <summary>
    /// 加密解密接口
    /// </summary>
    interface IEncryptDecryption
    {
        string EncryptStr(string str, bool negative, string key);
        string DESDecryptStr(string str, string key);
        bool EncryptFile(string inFile, string outFile, string key);
        bool DecryptFile(string inFile, string outFile, string key);
    }
}
