using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using TinyStack.Modules.iUtility.InterFaceUtility;
using System.IO;
using System.Xml.Serialization;
using Polenter.Serialization;

namespace TinyStack.Modules.iUtility.iTools
{
    /// <summary>
    /// xml操作
    /// </summary>
    public class XmlOperate : IXmlOperate
    {
        #region 把XML转换成对象
        /// <summary>
        /// 把XML转换成对象
        /// </summary>
        /// <param name="xml">Xml格式文本</param>
        /// <returns>实例对象</returns>
        public object XmlToObject(string xml)
        {
            SharpSerializer sXML = new SharpSerializer();
            //转换对象
            return sXML.Deserialize(TextToStream(xml)); ;
        }

        /// <summary>
        /// 把XML转换成对象(无xml头)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlString"></param>
        /// <returns></returns>
        public T SimpleXmlToObject<T>(string xmlString)
        {
            T cloneObject = default(T);
            try
            {
                StringBuilder buffer = new StringBuilder();
                buffer.Append(xmlString);

                XmlSerializer serializer = new XmlSerializer(typeof(T));

                using (TextReader reader = new StringReader(buffer.ToString()))
                {
                    Object obj = serializer.Deserialize(reader);
                    cloneObject = (T)obj;
                }
                return cloneObject;
            }
            catch (Exception Err)
            {
                return default(T);
            }
        }
        #endregion

        #region 把对象转换成XML
        /// <summary>
        /// 把对象转换成XML
        /// </summary>
        /// <param name="obj">实例对象</param>
        /// <returns>Xml格式文本</returns>
        public string ObjectToXml(object obj)
        {
            try
            {
                SharpSerializer sXML = new SharpSerializer();
                //实例内存流
                MemoryStream ms = new MemoryStream();
                //把对象转换成流
                sXML.Serialize(obj, ms);
                ms.Position = 0;
                //转换成文本
                String sf = StreamToText(ms).Replace("<?xml version=\"1.0\"?>\r\n", ""); ;
                ms.Close();
                ms.Dispose();
                return sf;

                #region old
                //XmlSerializer serializer = new XmlSerializer(obj.GetType());
                ////实例内存流
                //MemoryStream stream = new MemoryStream();
                ////将对象读到流中
                //serializer.Serialize(stream, obj);
                ////讲流指针设为开始
                //stream.Position = 0;
                ////读取流
                //StreamReader sr = new StreamReader(stream);
                //string str = sr.ReadToEnd();
                ////关闭流
                //sr.Dispose();
                //sr.Close();
                //stream.Dispose();
                //stream.Close();
                ////返回
                //return str;
                #endregion
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// 把对象转换成XML(无xml头)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string ObjectToSimpleXml<T>(T entity)
        {
            try
            {
                StringBuilder buffer = new StringBuilder();

                XmlSerializer serializer = new XmlSerializer(typeof(T));
                XmlSerializerNamespaces xsn = new XmlSerializerNamespaces();
                xsn.Add("", "");
                using (TextWriter writer = new StringWriter(buffer))
                {
                    serializer.Serialize(writer, entity, xsn);
                }

                //去掉xml格式的头部信息
                buffer = buffer.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");

                return buffer.ToString();
            }
            catch (Exception Err)
            {
                return null;
            }
        }
        #endregion

        #region 数据流转文本
        /// <summary>
        /// 数据流转文本
        /// </summary>
        /// <param name="stream"></param>
        public static String StreamToText(Stream stream)
        {
            string text = "";
            try
            {
                //实例编码
                Encoding encode = System.Text.Encoding.UTF8;
                stream.Position = 0;
                //实例读取流
                StreamReader SR = new StreamReader(stream, encode);
                //转换成文本
                text = SR.ReadToEnd();
                SR.Close();
                SR.Dispose();
            }
            catch
            {

            }
            return text;
        }
        #endregion

        #region 文本转数据流
        /// <summary>
        /// 文本转数据流
        /// </summary>
        /// <param name="stream"></param>
        public static Stream TextToStream(String text)
        {
            Stream stream = null;
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(text);
                //转换成数据量
                stream = new MemoryStream(buffer);
            }
            catch
            {
                stream = null;
            }
            return stream;
        }
        #endregion

        #region 通过路径加载XML
        /// <summary>
        /// 通过路径加载XML
        /// </summary>
        /// <param name="xmlPath">Xml物理路径</param>
        /// <returns>Xml文本</returns>
        public XmlDocument Load(string xmlPath)
        {
            XmlDocument xml = new XmlDocument();
            try
            {
                xml.Load(xmlPath);
            }
            catch (Exception e)
            {
                xml = null;
            }
            return xml;
        }
        #endregion

        #region 通过xml文本加载XML
        /// <summary>
        /// 通过xml文本加载XML
        /// </summary>
        /// <param name="xmlString">Xml格式文本</param>
        /// <returns>Xml文档</returns>
        public XmlDocument LoadXml(string xmlString)
        {
            XmlDocument xml = new XmlDocument();
            try
            {
                xml.LoadXml(xmlString);
            }
            catch (Exception e)
            {
                xml = null;
            }
            return xml;
        }
        #endregion

        #region 新建节点对象
        /// <summary>
        /// 新建节点对象
        /// </summary>
        /// <param name="xml">xml文档</param>
        /// <param name="nodeName">节点名</param>
        /// <param name="nodeItem">节点的属性</param>
        /// <param name="innerText">节点串联值</param>
        /// <param name="innerXml">节点标记</param>
        /// <returns>节点对象</returns>
        public XmlElement CreateNode(XmlDocument xml, string nodeName, Dictionary<string, string> nodeItem, string innerText, string innerXml)
        {
            if (xml == null || nodeName == null || nodeItem == null || innerText == null || innerXml == null)
            {
                return null;
            }
            try
            {
                //创建节点
                XmlElement newElement = xml.CreateElement(nodeName);
                //循环添加节点的属性
                foreach (var item in nodeItem)
                {
                    newElement.SetAttribute(item.Key, item.Value);
                }
                //添加节点的串联值
                newElement.InnerText = innerText;
                //添加节点标记
                newElement.InnerXml = innerXml;
                return newElement;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region 根据节点路径查找节点
        /// <summary>
        /// 根据节点路径查找节点
        /// </summary>
        /// <param name="xml">xml文档</param>
        /// <param name="nodePath">查找节点的路径</param>
        /// <returns>节点</returns>
        public XmlNode SelectSingleNode(XmlDocument xml, string nodePath)
        {
            if (xml == null || nodePath == null || nodePath == "")
            {
                return null;
            }
            //根据路径查找节点
            XmlNode newNode = xml.SelectSingleNode(nodePath);
            return newNode;
        }
        #endregion

        #region 根据节点名查找xml节点,返回第一个
        /// <summary>
        /// 根据节点名查找xml节点,返回第一个
        /// </summary>
        /// <param name="xml">xml文档</param>
        /// <param name="nodeName">查找节点的名称</param>
        /// <returns>节点</returns>
        public XmlNode SelectFirstNodeByName(XmlDocument xml, string nodeName)
        {
            if (xml == null || nodeName == null || nodeName == "")
            {
                return null;
            }
            //根据名称查找xml节点
            XmlNodeList nodeList = xml.GetElementsByTagName(nodeName);
            //判断查找的个数,如果为空返回null
            if (nodeList == null || nodeList.Count <= 0)
            {
                return null;
            }
            //获取并返回第一个节点
            XmlNode newNode = nodeList.Item(0);
            return newNode;
        }
        #endregion

        #region 根据节点路径查找节点集合
        /// <summary>
        /// 根据节点路径查找节点集合
        /// </summary>
        /// <param name="xml">xml文档</param>
        /// <param name="nodePath">查找节点的路径</param>
        /// <returns>节点集合</returns>
        public XmlNodeList SelectNodes(XmlDocument xml, string nodePath)
        {
            if (xml == null || nodePath == null || nodePath == "")
            {
                return null;
            }
            //根据名称查找xml节点
            XmlNodeList nodeList = xml.SelectNodes(nodePath);
            return nodeList;
        }
        #endregion

        #region 根据节点名查找xml节点,返回同名节点集合
        /// <summary>
        /// 根据节点名查找xml节点,返回同名节点集合
        /// </summary>
        /// <param name="xml">xml文档</param>
        /// <param name="nodeName">查找节点的名称</param>
        /// <returns>同名节点集合</returns>
        public XmlNodeList SelectNodeByName(XmlDocument xml, string nodeName)
        {
            if (xml == null || nodeName == null || nodeName == "")
            {
                return null;
            }
            //根据名称查找xml节点
            XmlNodeList nodeList = xml.GetElementsByTagName(nodeName);
            return nodeList;
        }
        #endregion

        #region 获取节点下第一个子节点
        /// <summary>
        /// 获取节点下第一个子节点
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <returns>第一个子节点</returns>
        public XmlNode GetFirstChild(XmlNode node)
        {
            if (node == null)
            {
                return null;
            }
            return node.FirstChild;
        }
        #endregion

        #region 获取节点下最后一个子节点
        /// <summary>
        /// 获取节点下最后一个子节点
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <returns>最后一个子节点</returns>
        public XmlNode GetLastChild(XmlNode node)
        {
            if (node == null)
            {
                return null;
            }
            return node.LastChild;
        }
        #endregion

        #region 通过子节点名获取节点下子节点，满足多个返回第一个
        /// <summary>
        /// 通过子节点名获取节点下子节点，满足多个返回第一个
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="nodeName">子节点名</param>
        /// <returns>子节点</returns>
        public XmlNode GetFirstChildByName(XmlNode node, string nodeName)
        {
            if (node == null || node.ChildNodes.Count <= 0 || nodeName == null || nodeName == "")
            {
                return null;
            }
            try
            {
                //根据路径查找子节点集合
                XmlNode newnode = node.SelectSingleNode(nodeName);
                return newnode;
            }
            catch (Exception e)
            {
                return null;
            }

        }
        #endregion

        #region 获取指定位置的子节点
        /// <summary>
        /// 获取指定位置的子节点
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="index">索引位置</param>
        /// <returns>查找的节点</returns>
        public XmlNode GetChildByIndex(XmlNode node, int index)
        {
            if (node == null || node.ChildNodes.Count <= 0)
            {
                return null;
            }
            try
            {
                //防止越界
                XmlNode newnode = node.ChildNodes.Item(index);
                return newnode;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region 查询节点下所有子节点
        /// <summary>
        /// 查询节点下所有子节点
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <returns>子节点集合</returns>
        public XmlNodeList ChildNodes(XmlNode node)
        {
            if (node == null)
            {
                return null;
            }
            return node.ChildNodes;
        }
        #endregion

        #region 通过子节点名获取节点下子节点集合
        /// <summary>
        /// 通过子节点名获取节点下子节点集合
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="nodeName">子节点名</param>
        /// <returns>子节点集合</returns>
        public XmlNodeList ChildNodesByName(XmlNode node, string nodeName)
        {
            if (node == null || node.ChildNodes.Count <= 0 || nodeName == null || nodeName == "")
            {
                return null;
            }
            try
            {
                //根据路径查找子节点集合
                XmlNodeList nodeList = node.SelectNodes(nodeName);
                return nodeList;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region 判断改节点下是否有子节点
        /// <summary>
        /// 判断改节点下是否有子节点
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <returns>判断结果true有;false没有</returns>
        public bool IsChildNodes(XmlNode node)
        {
            if (node == null)
            {
                return false;
            }
            return node.HasChildNodes;
        }
        #endregion

        #region 获取同名同级节点集合
        /// <summary>
        /// 获取同名同级节点集合
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <returns>节点下同名的节点</returns>
        public XmlNodeList NodesByName(XmlNode node)
        {
            if (node == null)
            {
                return null;
            }
            return node.ParentNode.SelectNodes(node.Name);
        }
        #endregion

        #region 将节点添加到指定节点下某个子节点前
        /// <summary>
        /// 将节点添加到指定节点下某个子节点前
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="childNode">指定子节点</param>
        /// <param name="addNode">要添加的节点</param>
        /// <returns>添加结果</returns>
        public bool AddPrepend(XmlNode node, XmlNode childNode, XmlNode addNode)
        {
            if (node == null || childNode == null || addNode == null)
            {
                return false;
            }
            try
            {
                node.InsertBefore(addNode, childNode);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
        #endregion

        #region 将节点添加到指定节点下某个子节点后
        /// <summary>
        /// 将节点添加到指定节点下某个子节点后
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="childNode">指定子节点</param>
        /// <param name="addNode">要添加的节点</param>
        /// <returns>添加结果</returns>
        public bool AddAppend(XmlNode node, XmlNode childNode, XmlNode addNode)
        {
            if (node == null || childNode == null || addNode == null)
            {
                return false;
            }
            try
            {
                node.InsertAfter(addNode, childNode);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
        #endregion

        #region 将节点作为第一个节点添加到指定节点下，作为其子节点
        /// <summary>
        /// 将节点作为第一个节点添加到指定节点下，作为其子节点
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="addNode">要添加的节点</param>
        /// <returns>添加结果</returns>
        public bool AddPrependChild(XmlNode node, XmlNode addNode)
        {
            if (node == null || addNode == null)
            {
                return false;
            }
            try
            {
                node.PrependChild(addNode);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
        #endregion

        #region 将节点作为最后一个节点添加到指定节点下，作为其子节点
        /// <summary>
        /// 将节点作为最后一个节点添加到指定节点下，作为其子节点
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="addNode">要添加的节点</param>
        /// <returns>添加结果</returns>
        public bool AddAppendChild(XmlNode node, XmlNode addNode)
        {
            if (node == null || addNode == null)
            {
                return false;
            }
            try
            {
                node.AppendChild(addNode);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
        #endregion

        #region 将节点添加到指定节点的指定索引位置，作为其子节点
        /// <summary>
        /// 将节点添加到指定节点的指定索引位置，作为其子节点
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="index">添加的索引位置</param>
        /// <param name="addNode">要添加的节点</param>
        /// <returns>添加结果</returns>
        public bool AddIndexChild(XmlNode node, int index, XmlNode addNode)
        {
            if (node == null || addNode == null)
            {
                return false;
            }
            try
            {
                node.InsertBefore(addNode, node.ChildNodes.Item(index));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region 为节点添加属性
        /// <summary>
        /// 为节点添加属性
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="nodeItem">节点的属性</param>
        /// <returns>添加结果</returns>
        public bool SetAttribute(XmlNode node, Dictionary<string, string> nodeItem)
        {
            if (node == null || nodeItem == null)
            {
                return false;
            }
            try
            {
                XmlElement xmle = node as XmlElement;
                //循环添加属性
                foreach (var item in nodeItem)
                {
                    xmle.SetAttribute(item.Key, item.Value);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
        #endregion

        #region 获取指定节点的指定属性值
        /// <summary>
        /// 获取指定节点的指定属性值
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="nodeKey">节点属性key</param>
        /// <returns>属性值</returns>
        public string GetAttributesValueByKey(XmlNode node, string nodeKey)
        {
            //参数和判断查找属性是否为空
            if (node == null || nodeKey == null || nodeKey == "" || node.Attributes[nodeKey] == null)
            {
                return null;
            }
            return node.Attributes[nodeKey].Value;
        }
        #endregion

        #region 修改节点指定属性的值
        /// <summary>
        /// 修改节点指定属性的值
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="nodeKey">节点属性key</param>
        /// <param name="nodeValue">要修改的值</param>
        /// <returns>修改结果</returns>
        public bool UpdateNodeValue(XmlNode node, string nodeKey, string nodeValue)
        {
            //参数和判断查找属性是否为空
            if (node == null || nodeKey == null || nodeKey == "" || node.Attributes[nodeKey] == null)
            {
                return false;
            }
            try
            {
                node.Attributes[nodeKey].Value = nodeValue;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
        #endregion

        #region 删除指定节点的指定属性
        /// <summary>
        /// 删除指定节点的指定属性
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="nodeKey">节点属性key(为空删除全部属性)</param>
        /// <returns>删除结果</returns>
        public bool RemoveAttributesByKey(XmlNode node, string nodeKey = null)
        {
            //参数和判断查找属性是否为空
            if (node == null || node.Attributes[nodeKey] == null)
            {
                return false;
            }
            try
            {
                //key为空删除全部属性
                if (nodeKey == null || nodeKey == "")
                {
                    node.Attributes.RemoveAll();
                }
                else
                {
                    //删除指定属性
                    node.Attributes.Remove(node.Attributes[nodeKey]);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region 获取节点的属性列表
        /// <summary>
        /// 获取节点的属性列表
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <returns>属性列表</returns>
        public XmlAttributeCollection GetAttributes(XmlNode node)
        {
            if (node == null)
            {
                return null;
            }
            return node.Attributes;
        }
        #endregion

        #region 为指定节点添加子节点
        /// <summary>
        /// 为指定节点添加子节点
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="nodeName">节点名</param>
        /// <param name="nodeItem">节点的属性</param>
        /// <param name="innerText">节点串联值</param>
        /// <param name="innerXml">节点标记</param>
        /// <returns>添加结果</returns>
        public bool AddNode(XmlNode node, string nodeName, Dictionary<string, string> nodeItem, string innerText, string innerXml)
        {
            if (node == null || nodeName == null || nodeName == "" || nodeItem == null)
            {
                return false;
            }
            try
            {
                //创建节点
                XmlElement newEle = CreateNode(node.OwnerDocument, nodeName, nodeItem, innerText, innerXml);
                //添加为子节点
                node.AppendChild(newEle);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region 删除指定节点
        /// <summary>
        /// 删除指定节点
        /// </summary>
        /// <param name="xml">xml文档</param>
        /// <param name="path">节点路径</param>
        /// <returns>删除结果</returns>
        public bool RemoveNode(XmlDocument xml, string path)
        {
            if (xml == null)
            {
                return false;
            }
            try
            {
                XmlNode node = xml.SelectSingleNode(path);
                if (node == null)
                {
                    return false;
                }
                //删除指定节点
                node.ParentNode.RemoveChild(node);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region 根据子节点索引删除节点的子节点
        /// <summary>
        /// 根据子节点索引删除节点的子节点
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="nodeIndex">子节点索引</param>
        /// <returns>删除结果</returns>
        public bool RemoveNodeChildByIndex(XmlNode node, int nodeIndex)
        {
            if (node == null || node.ChildNodes[nodeIndex] == null)
            {
                return false;
            }
            try
            {
                //根据索引删除子节点
                node.RemoveChild(node.ChildNodes[nodeIndex]);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
        #endregion

        #region 根据子节点名称删除节点的子节点
        /// <summary>
        /// 根据子节点名称删除节点的子节点
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="nodeName">节点名</param>
        /// <returns>删除结果</returns>
        public bool RemoveNodeChildByName(XmlNode node, string nodeName)
        {
            if (node == null || nodeName == null || nodeName == "")
            {
                return false;
            }
            try
            {
                //获取子节点
                XmlNode newNode = GetFirstChildByName(node, nodeName);
                //判断子节点是否为空
                if (newNode == null)
                {
                    return false;
                }
                //删除子节点
                node.RemoveChild(newNode);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
        #endregion

        #region 删除第一个子节点
        /// <summary>
        /// 删除第一个子节点
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <returns>删除结果</returns>
        public bool RemoveFirstNodeChild(XmlNode node)
        {
            if (node == null || node.FirstChild == null)
            {
                return false;
            }
            //删除第一个子节点
            node.RemoveChild(node.FirstChild);
            return true;
        }
        #endregion

        #region 删除最后一个子节点
        /// <summary>
        /// 删除最后一个子节点
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <returns>删除结果</returns>
        public bool RemoveLastNodeChild(XmlNode node)
        {
            if (node == null || node.LastChild == null)
            {
                return false;
            }
            //删除最后一个子节点
            node.RemoveChild(node.LastChild);
            return true;
        }
        #endregion

        #region 删除所有子节点
        /// <summary>
        /// 删除所有子节点
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <returns>删除结果</returns>
        public bool RemoveNodeChilds(XmlNode node)
        {
            if (node == null || !node.HasChildNodes)
            {
                return false;
            }
            try
            {
                node.RemoveAll();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        #endregion

        #region 根据路径获取节点文本
        /// <summary>
        /// 根据路径获取节点文本
        /// </summary>
        /// <param name="xml">xml文档</param>
        /// <param name="nodePath">查找节点的路径</param>
        /// <returns>节点文本</returns>
        public string SelectNodeTextByPath(XmlDocument xml, string nodePath)
        {
            if (xml == null || nodePath == null || nodePath == "")
            {
                return null;
            }
            //获取节点
            XmlNode newNode = xml.SelectSingleNode(nodePath);
            if (newNode == null)
            {
                return null;
            }
            return newNode.InnerText;
        }
        #endregion

        #region 根据路径获取节点文本列表
        /// <summary>
        /// 根据路径获取节点文本列表
        /// </summary>
        /// <param name="xml">xml文档</param>
        /// <param name="nodePath">查找节点的路径</param>
        /// <returns>节点文本列表</returns>
        public string[] SelectNodeTextsByPath(XmlDocument xml, string nodePath)
        {
            if (xml == null || nodePath == null || nodePath == "")
            {
                return null;
            }
            try
            {
                //获取节点列表
                XmlNodeList nodeList = xml.SelectNodes(nodePath);
                if (nodeList == null || nodeList.Count <= 0)
                {
                    return null;
                }
                //声明文本列表
                string[] strList = new string[nodeList.Count];
                int count = 0;
                foreach (XmlNode item in nodeList)
                {
                    //循环添加文本到列表
                    strList[count] = item.InnerText;
                    count++;
                }
                return strList;
            }
            catch (Exception e)
            {
                return null;
            }

        }
        #endregion

        #region 根据节点名称获取节点文本，只返回满足条件的第一个
        /// <summary>
        /// 根据节点名称获取节点文本，只返回满足条件的第一个
        /// </summary>
        /// <param name="xml">xml文档</param>
        /// <param name="nodeName">查找节点名</param>
        /// <returns>节点文本</returns>
        public string SelectFirstNodeTextByName(XmlDocument xml, string nodeName)
        {
            if (xml == null || nodeName == null || nodeName == "")
            {
                return null;
            }
            try
            {
                //获取第一个节点
                XmlNode newNode = SelectFirstNodeByName(xml, nodeName);
                if (newNode == null)
                {
                    return null;
                }
                return newNode.InnerText;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region 根据名称获取节点文本列表
        /// <summary>
        /// 根据名称获取节点文本列表
        /// </summary>
        /// <param name="xml">xml文档</param>
        /// <param name="nodeName">查找节点名</param>
        /// <returns>节点文本列表</returns>
        public string[] SelectNodeTextsByName(XmlDocument xml, string nodeName)
        {
            if (xml == null || nodeName == null || nodeName == "")
            {
                return null;
            }
            try
            {
                //获取节点列表
                XmlNodeList nodeList = SelectNodeByName(xml, nodeName);
                if (nodeList == null || nodeList.Count <= 0)
                {
                    return null;
                }
                //声明文本列表
                string[] strList = new string[nodeList.Count];
                int count = 0;
                foreach (XmlNode item in nodeList)
                {
                    //循环添加文本到列表
                    strList[count] = item.InnerText;
                    count++;
                }
                return strList;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region 根据索引获取子节点文本
        /// <summary>
        /// 根据索引获取子节点文本
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="nodeIndex">子节点索引</param>
        /// <returns>子节点文本</returns>
        public string GetNodeChildTextByIndex(XmlNode node, int nodeIndex)
        {
            if (node == null)
            {
                return null;
            }
            try
            {
                return node.ChildNodes[nodeIndex].InnerText;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region 根据子节点名称获取节点的子节点文本
        /// <summary>
        /// 根据子节点名称获取节点的子节点文本
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="nodeName">子节点名称</param>
        /// <returns>子节点文本</returns>
        public string GetFirstNodeChildTextByName(XmlNode node, string nodeName)
        {
            if (node == null || nodeName == null || nodeName == "")
            {
                return null;
            }
            //获取子节点
            XmlNode newNode = GetFirstChildByName(node, nodeName);
            if (newNode == null)
            {
                return null;
            }
            return newNode.InnerText;
        }
        #endregion

        #region 根据子节点名称获取节点的子节点文本列表
        /// <summary>
        /// 根据子节点名称获取节点的子节点文本列表
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="nodeName">子节点名称</param>
        /// <returns>子节点文本列表</returns>
        public string[] GetNodeChildTextsByName(XmlNode node, string nodeName)
        {
            if (node == null)
            {
                return null;
            }
            try
            {
                //获取子节点列表
                XmlNodeList nodeList = ChildNodesByName(node, nodeName);
                if (nodeList == null || nodeList.Count <= 0)
                {
                    return null;
                }
                //声明文本列表
                string[] strList = new string[nodeList.Count];
                int count = 0;
                foreach (XmlNode item in nodeList)
                {
                    //循环添加文本到列表
                    strList[count] = item.InnerText;
                    count++;
                }
                return strList;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region 获取节点下第一个子节点文本
        /// <summary>
        /// 获取节点下第一个子节点文本
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <returns>子节点文本</returns>
        public string GetFirstNodeChildText(XmlNode node)
        {
            if (node == null)
            {
                return null;
            }
            try
            {
                return node.FirstChild.InnerText;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region 获取节点下最后一个子节点文本
        /// <summary>
        /// 获取节点下最后一个子节点文本
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <returns>子节点文本</returns>
        public string GetLastNodeChildText(XmlNode node)
        {
            if (node == null)
            {
                return null;
            }
            try
            {
                return node.LastChild.InnerText;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region 获取子节点文本列表
        /// <summary>
        /// 获取子节点文本列表
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <returns>子节点文本列表</returns>
        public string[] GetNodeChildTexts(XmlNode node)
        {
            if (node == null)
            {
                return null;
            }
            try
            {
                //获取子节点列表
                XmlNodeList nodeList = node.ChildNodes;
                if (nodeList == null || nodeList.Count <= 0)
                {
                    return null;
                }
                //声明文本列表
                string[] strList = new string[nodeList.Count];
                int count = 0;
                foreach (XmlNode item in nodeList)
                {
                    //循环添加文本到列表
                    strList[count] = item.InnerText;
                    count++;
                }
                return strList;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        #region 根据路径删除节点文本
        /// <summary>
        /// 根据路径删除节点文本
        /// </summary>
        /// <param name="xml">xml文档</param>
        /// <param name="nodePath">删除节点的路径</param>
        /// <returns>删除结果</returns>
        public bool DeleteNodeTextByPath(XmlDocument xml, string nodePath)
        {
            if (xml == null || nodePath == null || nodePath == "")
            {
                return false;
            }
            //获取节点
            XmlNode newNode = xml.SelectSingleNode(nodePath);
            if (newNode == null)
            {
                return false;
            }
            newNode.InnerText = null;
            return true;
        }
        #endregion

        #region 根据路径删除节点文本列表
        /// <summary>
        /// 根据路径删除节点文本列表
        /// </summary>
        /// <param name="xml">xml文档</param>
        /// <param name="nodePath">删除节点的路径</param>
        /// <returns>删除结果</returns>
        public bool DeleteNodeTextsByPath(XmlDocument xml, string nodePath)
        {
            if (xml == null || nodePath == null || nodePath == "")
            {
                return false;
            }
            try
            {
                //获取节点列表
                XmlNodeList nodeList = xml.SelectNodes(nodePath);
                if (nodeList == null || nodeList.Count <= 0)
                {
                    return false;
                }
                foreach (XmlNode item in nodeList)
                {
                    //循环删除文本
                    item.InnerText = null;
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region 根据节点名称删除节点文本，只返回满足条件的第一个
        /// <summary>
        /// 根据节点名称删除节点文本，只返回满足条件的第一个
        /// </summary>
        /// <param name="xml">xml文档</param>
        /// <param name="nodeName">删除节点名</param>
        /// <returns>删除结果</returns>
        public bool DeleteFirstNodeTextByName(XmlDocument xml, string nodeName)
        {
            if (xml == null || nodeName == null || nodeName == "")
            {
                return false;
            }
            try
            {
                //获取第一个节点
                XmlNode newNode = SelectFirstNodeByName(xml, nodeName);
                if (newNode == null)
                {
                    return false;
                }
                newNode.InnerText = null;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region 根据名称删除节点文本列表
        /// <summary>
        /// 根据名称删除节点文本列表
        /// </summary>
        /// <param name="xml">xml文档</param>
        /// <param name="nodeName">删除节点名</param>
        /// <returns>删除结果</returns>
        public bool DeleteNodeTextsByName(XmlDocument xml, string nodeName)
        {
            if (xml == null || nodeName == null || nodeName == "")
            {
                return false;
            }
            try
            {
                //获取节点列表
                XmlNodeList nodeList = SelectNodeByName(xml, nodeName);
                if (nodeList == null || nodeList.Count <= 0)
                {
                    return false;
                }
                foreach (XmlNode item in nodeList)
                {
                    //循环删除文本
                    item.InnerText = null;
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region 根据索引删除子节点文本
        /// <summary>
        /// 根据索引删除子节点文本
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="nodeIndex">删除节点索引</param>
        /// <returns>删除结果</returns>
        public bool DeleteNodeChildTextByIndex(XmlNode node, int nodeIndex)
        {
            try
            {
                if (node == null || node.ChildNodes[nodeIndex] == null)
                {
                    return false;
                }
                node.ChildNodes[nodeIndex].InnerText = null;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region 根据子节点名称删除节点的满足第一个子节点文本
        /// <summary>
        /// 根据子节点名称删除节点的满足第一个子节点文本
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="nodeName">删除节点名</param>
        /// <returns>删除结果</returns>
        public bool DeleteFirstNodeChildTextByName(XmlNode node, string nodeName)
        {
            if (node == null || nodeName == null || nodeName == "")
            {
                return false;
            }
            try
            {
                //获取子节点
                XmlNode newNode = GetFirstChildByName(node, nodeName);
                if (newNode == null)
                {
                    return false;
                }
                //删除文本
                newNode.InnerText = null;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region 根据子节点名称删除文本列表
        /// <summary>
        /// 根据子节点名称删除文本列表
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="nodeName">删除节点名</param>
        /// <returns>删除结果</returns>
        public bool DeleteNodeChildTextsByName(XmlNode node, string nodeName)
        {
            if (node == null)
            {
                return false;
            }
            try
            {
                //获取子节点列表
                XmlNodeList nodeList = ChildNodesByName(node, nodeName);
                if (nodeList == null || nodeList.Count <= 0)
                {
                    return false;
                }
                foreach (XmlNode item in nodeList)
                {
                    //循环删除文本
                    item.InnerText = null;
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region 删除节点下第一个子节点文本
        /// <summary>
        /// 删除节点下第一个子节点文本
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <returns>删除结果</returns>
        public bool DeleteFirstNodeChildText(XmlNode node)
        {
            if (node == null)
            {
                return false;
            }
            try
            {
                node.FirstChild.InnerText = null;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region 删除节点下最后一个子节点文本
        /// <summary>
        /// 删除节点下最后一个子节点文本
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <returns>删除结果</returns>
        public bool DeleteLastNodeChildText(XmlNode node)
        {
            if (node == null)
            {
                return false;
            }
            try
            {
                node.LastChild.InnerText = null;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region 删除子节点文本列表
        /// <summary>
        /// 删除子节点文本列表
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <returns>删除结果</returns>
        public bool DeleteNodeChildTexts(XmlNode node)
        {
            if (node == null)
            {
                return false;
            }
            try
            {
                //获取子节点列表
                XmlNodeList nodeList = node.ChildNodes;
                if (nodeList == null || nodeList.Count <= 0)
                {
                    return false;
                }
                foreach (XmlNode item in nodeList)
                {
                    //循环删除文本
                    item.InnerText = null;
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region 根据路径修改节点文本
        /// <summary>
        /// 根据路径修改节点文本
        /// </summary>
        /// <param name="xml">xml文档</param>
        /// <param name="nodePath">节点的路径</param>
        /// <param name="textValue">修改的文本</param>
        /// <returns>修改结果</returns>
        public bool UpdateNodeTextByPath(XmlDocument xml, string nodePath, string textValue)
        {
            if (xml == null || nodePath == null || nodePath == "")
            {
                return false;
            }
            //获取节点
            XmlNode newNode = xml.SelectSingleNode(nodePath);
            if (newNode == null)
            {
                return false;
            }
            newNode.InnerText = textValue;
            return true;
        }
        #endregion

        #region 根据路径修改节点文本列表
        /// <summary>
        /// 根据路径修改节点文本列表
        /// </summary>
        /// <param name="xml">xml文档</param>
        /// <param name="nodePath">节点的路径</param>
        /// <param name="textValue">修改的文本</param>
        /// <returns>修改结果</returns>
        public bool UpdateNodeTextsByPath(XmlDocument xml, string nodePath, string textValue)
        {
            if (xml == null || nodePath == null || nodePath == "")
            {
                return false;
            }
            try
            {
                //获取节点列表
                XmlNodeList nodeList = xml.SelectNodes(nodePath);
                if (nodeList == null || nodeList.Count <= 0)
                {
                    return false;
                }
                foreach (XmlNode item in nodeList)
                {
                    //循环修改文本
                    item.InnerText = textValue;
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region 根据节点名称修改节点文本，只返回满足条件的第一个
        /// <summary>
        /// 根据节点名称修改节点文本，只返回满足条件的第一个
        /// </summary>
        /// <param name="xml">xml文档</param>
        /// <param name="nodeName">文本名</param>
        /// <param name="textValue">修改的文本</param>
        /// <returns>修改结果</returns>
        public bool UpdateFirstNodeTextByName(XmlDocument xml, string nodeName, string textValue)
        {
            if (xml == null || nodeName == null || nodeName == "")
            {
                return false;
            }
            try
            {
                //获取第一个节点
                XmlNode newNode = SelectFirstNodeByName(xml, nodeName);
                if (newNode == null)
                {
                    return false;
                }
                newNode.InnerText = textValue;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region 根据名称修改节点文本列表
        /// <summary>
        /// 根据名称修改节点文本列表
        /// </summary>
        /// <param name="xml">xml文档</param>
        /// <param name="nodeName">节点名</param>
        /// <param name="textValue">修改的文本</param>
        /// <returns>修改结果</returns>
        public bool UpdateNodeTextsByName(XmlDocument xml, string nodeName, string textValue)
        {
            if (xml == null || nodeName == null || nodeName == "")
            {
                return false;
            }
            try
            {
                //获取节点列表
                XmlNodeList nodeList = SelectNodeByName(xml, nodeName);
                if (nodeList == null || nodeList.Count <= 0)
                {
                    return false;
                }
                foreach (XmlNode item in nodeList)
                {
                    //循环修改文本
                    item.InnerText = textValue;
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region 根据索引修改子节点文本
        /// <summary>
        /// 根据索引修改子节点文本
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="nodeIndex">子节点索引</param>
        /// <param name="textValue">修改的文本</param>
        /// <returns>修改结果</returns>
        public bool UpdateNodeChildTextByIndex(XmlNode node, int nodeIndex, string textValue)
        {
            if (node == null)
            {
                return false;
            }
            try
            {
                //修改节点文本
                node.ChildNodes[nodeIndex].InnerText = textValue;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region 根据子节点名称修改节点的满足第一个子节点文本
        /// <summary>
        /// 根据子节点名称修改节点的满足第一个子节点文本
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="nodeName">子节点名</param>
        /// <param name="textValue">修改的文本</param>
        /// <returns>修改结果</returns>
        public bool UpdateFirstNodeChildTextByName(XmlNode node, string nodeName, string textValue)
        {
            if (node == null || nodeName == null || nodeName == "")
            {
                return false;
            }
            try
            {
                //获取子节点
                XmlNode newNode = GetFirstChildByName(node, nodeName);
                if (newNode == null)
                {
                    return false;
                }
                //修改文本
                newNode.InnerText = textValue;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region 根据子节点名称修改文本列表
        /// <summary>
        /// 根据子节点名称修改文本列表
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="nodeName">子节点名</param>
        /// <param name="textValue">修改的文本</param>
        /// <returns>修改结果</returns>
        public bool UpdateNodeChildTextsByName(XmlNode node, string nodeName, string textValue)
        {
            if (node == null)
            {
                return false;
            }
            try
            {
                //获取子节点列表
                XmlNodeList nodeList = ChildNodesByName(node, nodeName);
                if (nodeList == null || nodeList.Count <= 0)
                {
                    return false;
                }
                foreach (XmlNode item in nodeList)
                {
                    //循环修改文本
                    item.InnerText = textValue;
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region 修改节点下第一个子节点文本
        /// <summary>
        /// 修改节点下第一个子节点文本
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="textValue">修改的文本</param>
        /// <returns>修改结果</returns>
        public bool UpdateFirstNodeChildText(XmlNode node, string textValue)
        {
            if (node == null)
            {
                return false;
            }
            try
            {
                node.FirstChild.InnerText = textValue;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region 修改节点下最后一个子节点文本
        /// <summary>
        /// 修改节点下最后一个子节点文本
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="textValue">修改的文本</param>
        /// <returns>修改结果</returns>
        public bool UpdateLastNodeChildText(XmlNode node, string textValue)
        {
            if (node == null)
            {
                return false;
            }
            try
            {
                node.LastChild.InnerText = textValue;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region 修改子节点文本列表
        /// <summary>
        /// 修改子节点文本列表
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="textValue">修改的文本</param>
        /// <returns>修改结果</returns>
        public bool UpdateNodeChildTexts(XmlNode node, string textValue)
        {
            if (node == null)
            {
                return false;
            }
            try
            {
                //获取子节点列表
                XmlNodeList nodeList = node.ChildNodes;
                if (nodeList == null || nodeList.Count <= 0)
                {
                    return false;
                }
                foreach (XmlNode item in nodeList)
                {
                    //循环修改文本
                    item.InnerText = textValue;
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        #region 保存xml
        /// <summary>
        /// 保存xml
        /// </summary>
        /// <param name="xml">xml文档</param>
        /// <param name="fileName">保存位置(物理路径)</param>
        /// <returns>保存结果</returns>
        public bool SaveXml(XmlDocument xml, string fileName)
        {
            if (xml == null || fileName == null || fileName == "")
            {
                return false;
            }
            try
            {
                //123\456.xml
                string[] strList = fileName.Split('\\');
                string path = "";
                for (int i = 0; i < strList.Count()-1; i++)
                {
                    path = path + strList[i] + "\\";
                }
                //判断目录是否存在
                if (!Directory.Exists(path))
                {
                    //目录不存在创建目录
                    Directory.CreateDirectory(path);
                }
                //保存xml
                xml.Save(fileName);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion
    }
}
