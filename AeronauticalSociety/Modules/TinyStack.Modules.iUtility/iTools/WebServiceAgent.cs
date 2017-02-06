using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Services.Description;
using System.Xml;

namespace TinyStack.Modules.iUtility.iTools
{
    public class WebServiceAgent
    {
        private object agent;
        private Type agentType;
        private string CODE_NAMESPACE = "";
        private string dirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Temp");
        private byte[] fileByte = null;


        #region 获取文件流
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] GetFile()
        {
            return fileByte;
        }
        #endregion

        #region 获取程序集类型
        /// <summary>
        /// 获取程序集类型
        /// </summary>
        /// <returns></returns>
        public Type GetType()
        {
            return agentType;
        }
        #endregion


        /// <summary<   
        /// 构造函数   
        /// </summary<   
        /// <param name="url"></param>   
        public WebServiceAgent(string url, string NameSpace)
        {
            //判断文件存储路径是否存在
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            if (url.ToLower().IndexOf("?wsdl") == -1)
            {
                url = url + "?wsdl";
            }
            XmlTextReader reader = new XmlTextReader(url);
            CODE_NAMESPACE = NameSpace;
            //创建和格式化 WSDL 文档   
            ServiceDescription sd = ServiceDescription.Read(reader);

            //创建客户端代理代理类   
            ServiceDescriptionImporter sdi = new ServiceDescriptionImporter();
            sdi.AddServiceDescription(sd, null, null);

            //使用 CodeDom 编译客户端代理类   
            CodeNamespace cn = new CodeNamespace(CODE_NAMESPACE);
            CodeCompileUnit ccu = new CodeCompileUnit();

            ccu.Namespaces.Add(cn);
            sdi.Import(cn, ccu);

            Microsoft.CSharp.CSharpCodeProvider icc = new Microsoft.CSharp.CSharpCodeProvider();
            CompilerParameters parameter = new CompilerParameters();
            parameter.GenerateExecutable = false;            
            parameter.OutputAssembly = Path.Combine(dirPath, NameSpace + ".dll");//输出程序集的名称            
            parameter.ReferencedAssemblies.Add("System.dll");
            parameter.ReferencedAssemblies.Add("System.XML.dll");
            parameter.ReferencedAssemblies.Add("System.Web.Services.dll");
            parameter.ReferencedAssemblies.Add("System.Data.dll");

            CompilerResults cr = icc.CompileAssemblyFromDom(parameter, ccu);
            if (true == cr.Errors.HasErrors)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (System.CodeDom.Compiler.CompilerError ce in cr.Errors)
                {
                    sb.Append(ce.ToString());
                    sb.Append(System.Environment.NewLine);
                }
                throw new Exception(sb.ToString());
            }

            agentType = cr.CompiledAssembly.GetTypes()[0];
            agent = Activator.CreateInstance(agentType);
            //读取文件字节            
            FileStream fileStream = agentType.Assembly.GetFiles()[0];
            using (BinaryReader br = new BinaryReader(fileStream))
            {
                fileByte = br.ReadBytes((int)fileStream.Length);
                fileStream.Dispose();
            }

            //TextWriter writer = File.CreateText("F:\\MyTest.cs");
            //icc.GenerateCodeFromCompileUnit(ccu, writer, null);
            //writer.Flush();
            //writer.Close();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public object Invoke(string methodName, params object[] args)
        {
            MethodInfo mi = agentType.GetMethod(methodName);
            return this.Invoke(mi, args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public object Invoke(MethodInfo method, params object[] args)
        {
            return method.Invoke(agent, args);
        }
        public MethodInfo[] Methods
        {
            get
            {
                return agentType.GetMethods();
            }
        }

    }
}
