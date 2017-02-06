using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace TinyStack.Modules.iUtility.InterFaceUtility
{
    /// <summary>
    /// xml操作接口
    /// </summary>
    interface IXmlOperate
    {
        /// <summary>
        /// 把XML转换成对象
        /// </summary>
        /// <param name="xml">Xml格式文本</param>
        /// <returns>实例对象</returns>
        object XmlToObject(string xml);

        /// <summary>
        /// 把对象转换成XML
        /// </summary>
        /// <param name="obj">实例对象</param>
        /// <returns>Xml格式文本</returns>
        string ObjectToXml(object obj);

        /// <summary>
        /// 通过路径加载XML
        /// </summary>
        /// <param name="xmlPath">Xml物理路径</param>
        /// <returns>Xml文档</returns>
        XmlDocument Load(string xmlPath);

        /// <summary>
        /// 通过xml文本加载XML
        /// </summary>
        /// <param name="xmlString">Xml格式文本</param>
        /// <returns>Xml文档</returns>
        XmlDocument LoadXml(string xmlString);

        /// <summary>
        /// 新建节点对象
        /// </summary>
        /// <param name="xml">xml文档</param>
        /// <param name="nodeName">节点名</param>
        /// <param name="nodeItem">节点的属性</param>
        /// <param name="innerText">节点串联值</param>
        /// <param name="innerXml">节点标记</param>
        /// <returns>节点对象</returns>
        XmlElement CreateNode(XmlDocument xml, string nodeName, Dictionary<string, string> nodeItem, string innerText, string innerXml);

        /// <summary>
        /// 根据节点路径查找节点
        /// </summary>
        /// <param name="xml">xml文档</param>
        /// <param name="nodePath">查找节点的路径</param>
        /// <returns>节点</returns>
        XmlNode SelectSingleNode(XmlDocument xml, string nodePath);

        /// <summary>
        /// 根据节点名查找xml节点,返回第一个
        /// </summary>
        /// <param name="xml">xml文档</param>
        /// <param name="nodeName">查找节点的名称</param>
        /// <returns>节点</returns>
        XmlNode SelectFirstNodeByName(XmlDocument xml, string nodeName);

        /// <summary>
        /// 根据节点路径查找节点集合
        /// </summary>
        /// <param name="xml">xml文档</param>
        /// <param name="nodePath">查找节点的路径</param>
        /// <returns>节点集合</returns>
        XmlNodeList SelectNodes(XmlDocument xml, string nodePath);

        /// <summary>
        /// 根据节点名查找xml节点,返回同名节点集合
        /// </summary>
        /// <param name="xml">xml文档</param>
        /// <param name="nodeName">查找节点的名称</param>
        /// <returns>同名节点集合</returns>
        XmlNodeList SelectNodeByName(XmlDocument xml, string nodeName);

        /// <summary>
        /// 获取节点下第一个子节点
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <returns>第一个子节点</returns>
        XmlNode GetFirstChild(XmlNode node);

        /// <summary>
        /// 获取节点下最后一个子节点
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <returns>最后一个子节点</returns>
        XmlNode GetLastChild(XmlNode node);

        /// <summary>
        /// 通过子节点名获取节点下子节点的方法，满足多个返回第一个
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="nodeName">子节点名</param>
        /// <returns>子节点</returns>
        XmlNode GetFirstChildByName(XmlNode node, string nodeName);

        /// <summary>
        /// 获取指定位置的子节点
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="index">索引位置</param>
        /// <returns>查找的节点</returns>
        XmlNode GetChildByIndex(XmlNode node, int index);

        /// <summary>
        /// 查询节点下所有子节点
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <returns>子节点集合</returns>
        XmlNodeList ChildNodes(XmlNode node);

        /// <summary>
        /// 通过子节点名获取节点下子节点集合
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="nodeName">子节点名</param>
        /// <returns>子节点集合</returns>
        XmlNodeList ChildNodesByName(XmlNode node, string nodeName);

        /// <summary>
        /// 判断改节点下是否有子节点
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <returns>判断结果(bool)</returns>
        bool IsChildNodes(XmlNode node);

        /// <summary>
        /// 获取同名同级节点集合
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <returns>节点下同名的节点</returns>
        XmlNodeList NodesByName(XmlNode node);

        /// <summary>
        /// 将节点添加到指定节点下某个子节点前
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="childNode">指定子节点</param>
        /// <param name="addNode">要添加的节点</param>
        /// <returns>添加结果</returns>
        bool AddPrepend(XmlNode node, XmlNode childNode, XmlNode addNode);

        /// <summary>
        /// 将节点添加到指定节点下某个子节点后
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="childNode">指定子节点</param>
        /// <param name="addNode">要添加的节点</param>
        /// <returns>添加结果</returns>
        bool AddAppend(XmlNode node, XmlNode childNode, XmlNode addNode);

        /// <summary>
        /// 将节点作为第一个节点添加到指定节点下，作为其子节点
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="addNode">要添加的节点</param>
        /// <returns>添加结果</returns>
        bool AddPrependChild(XmlNode node, XmlNode addNode);

        /// <summary>
        /// 将节点作为最后一个基点添加到指定节点下，作为其子节点
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="addNode">要添加的节点</param>
        /// <returns>添加结果</returns>
        bool AddAppendChild(XmlNode node, XmlNode addNode);

        /// <summary>
        /// 将节点添加到指定节点的指定索引位置，作为其子节点
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="index">添加的索引位置</param>
        /// <param name="addNode">要添加的节点</param>
        /// <returns>添加结果</returns>
        bool AddIndexChild(XmlNode node, int index, XmlNode addNode);

        /// <summary>
        /// 为节点添加属性
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="nodeItem">节点的属性</param>
        /// <returns>添加结果</returns>
        bool SetAttribute(XmlNode node, Dictionary<string, string> nodeItem);
        
        /// <summary>
        /// 获取指定节点的指定属性值
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="nodeKey">节点属性key</param>
        /// <returns>属性值</returns>
        string GetAttributesValueByKey(XmlNode node, string nodeKey);

        /// <summary>
        /// 修改节点指定属性的值
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="nodeKey">节点属性key</param>
        /// <param name="nodeValue">要修改的值</param>
        /// <returns>修改结果</returns>
        bool UpdateNodeValue(XmlNode node, string nodeKey, string nodeValue);

        /// <summary>
        /// 删除指定节点的指定属性
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="nodeKey">节点属性key(为空删除全部属性)</param>
        /// <returns>删除结果</returns>
        bool RemoveAttributesByKey(XmlNode node, string nodeKey = null);

        /// <summary>
        /// 获取节点的属性列表
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <returns>属性列表</returns>
        XmlAttributeCollection GetAttributes(XmlNode node);

        /// <summary>
        /// 为指定节点添加子节点
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="nodeName">节点名</param>
        /// <param name="nodeItem">节点的属性</param>
        /// <param name="innerText">节点串联值</param>
        /// <param name="innerXml">节点标记</param>
        /// <returns>添加结果</returns>
        bool AddNode(XmlNode node, string nodeName, Dictionary<string, string> nodeItem, string innerText, string innerXml);

        /// <summary>
        /// 删除指定节点
        /// </summary>
        /// <param name="xml">xml文本</param>
        /// <param name="path">节点路径</param>
        /// <returns>删除结果</returns>
        bool RemoveNode(XmlDocument xml,string path);

        /// <summary>
        /// 根据子节点索引删除节点的子节点
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="nodeIndex">子节点索引</param>
        /// <returns>删除结果</returns>
        bool RemoveNodeChildByIndex(XmlNode node, int nodeIndex);

        /// <summary>
        /// 根据子节点名称删除节点的子节点
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="nodeName">节点名</param>
        /// <returns>删除结果</returns>
        bool RemoveNodeChildByName(XmlNode node, string nodeName);

        /// <summary>
        /// 删除第一个子节点
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <returns>删除结果</returns>
        bool RemoveFirstNodeChild(XmlNode node);

        /// <summary>
        /// 删除最后一个子节点
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <returns>删除结果</returns>
        bool RemoveLastNodeChild(XmlNode node);

        /// <summary>
        /// 删除所有子节点
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <returns>删除结果</returns>
        bool RemoveNodeChilds(XmlNode node);

        /// <summary>
        /// 根据路径获取节点文本
        /// </summary>
        /// <param name="xml">xml文档</param>
        /// <param name="nodePath">查找节点的路径</param>
        /// <returns>节点文本</returns>
        string SelectNodeTextByPath(XmlDocument xml, string nodePath);

        /// <summary>
        /// 根据路径获取节点文本列表
        /// </summary>
        /// <param name="xml">xml文档</param>
        /// <param name="nodePath">查找节点的路径</param>
        /// <returns>节点文本列表</returns>
        string[] SelectNodeTextsByPath(XmlDocument xml, string nodePath);

        /// <summary>
        /// 根据节点名称获取节点文本，只返回满足条件的第一个
        /// </summary>
        /// <param name="xml">xml文档</param>
        /// <param name="nodeName">查找节点名</param>
        /// <returns>节点文本</returns>
        string SelectFirstNodeTextByName(XmlDocument xml, string nodeName);

        /// <summary>
        /// 根据名称获取节点文本列表
        /// </summary>
        /// <param name="xml">xml文档</param>
        /// <param name="nodeName">查找节点名</param>
        /// <returns>节点文本列表</returns>
        string[] SelectNodeTextsByName(XmlDocument xml, string nodeName);

        /// <summary>
        /// 根据索引获取子节点文本
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="nodeIndex">子节点索引</param>
        /// <returns>子节点文本</returns>
        string GetNodeChildTextByIndex(XmlNode node, int nodeIndex);

        /// <summary>
        /// 根据子节点名称获取节点的子节点文本
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="nodeName">子节点名称</param>
        /// <returns>子节点文本</returns>
        string GetFirstNodeChildTextByName(XmlNode node, string nodeName);

        /// <summary>
        /// 根据子节点名称获取节点的子节点文本列表
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="nodeName">子节点名称</param>
        /// <returns>子节点文本列表</returns>
        string[] GetNodeChildTextsByName(XmlNode node, string nodeName);

        /// <summary>
        /// 获取节点下第一个子节点文本
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <returns>子节点文本</returns>
        string GetFirstNodeChildText(XmlNode node);

        /// <summary>
        /// 获取节点下最后一个子节点文本
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <returns>子节点文本</returns>
        string GetLastNodeChildText(XmlNode node);

        /// <summary>
        /// 获取子节点文本列表
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <returns>子节点文本列表</returns>
        string[] GetNodeChildTexts(XmlNode node);

        /// <summary>
        /// 根据路径删除节点文本
        /// </summary>
        /// <param name="xml">xml文档</param>
        /// <param name="nodePath">删除节点的路径</param>
        /// <returns>删除结果</returns>
        bool DeleteNodeTextByPath(XmlDocument xml, string nodePath);

        /// <summary>
        /// 根据路径删除节点文本列表
        /// </summary>
        /// <param name="xml">xml文档</param>
        /// <param name="nodePath">删除节点的路径</param>
        /// <returns>删除结果</returns>
        bool DeleteNodeTextsByPath(XmlDocument xml, string nodePath);

        /// <summary>
        /// 根据节点名称删除节点文本，只返回满足条件的第一个
        /// </summary>
        /// <param name="xml">xml文档</param>
        /// <param name="nodeName">删除节点名</param>
        /// <returns>删除结果</returns>
        bool DeleteFirstNodeTextByName(XmlDocument xml, string nodeName);

        /// <summary>
        /// 根据名称删除节点文本列表
        /// </summary>
        /// <param name="xml">xml文档</param>
        /// <param name="nodeName">删除节点名</param>
        /// <returns>删除结果</returns>
        bool DeleteNodeTextsByName(XmlDocument xml, string nodeName);

        /// <summary>
        /// 根据索引删除子节点文本
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="nodeIndex">删除节点索引</param>
        /// <returns>删除结果</returns>
        bool DeleteNodeChildTextByIndex(XmlNode node, int nodeIndex);

        /// <summary>
        /// 根据子节点名称删除节点的满足第一个子节点文本
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="nodeName">删除节点名</param>
        /// <returns>删除结果</returns>
        bool DeleteFirstNodeChildTextByName(XmlNode node, string nodeName);

        /// <summary>
        /// 根据子节点名称删除文本列表
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="nodeName">删除节点名</param>
        /// <returns>删除结果</returns>
        bool DeleteNodeChildTextsByName(XmlNode node, string nodeName);

        /// <summary>
        /// 删除节点下第一个子节点文本
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <returns>删除结果</returns>
        bool DeleteFirstNodeChildText(XmlNode node);

        /// <summary>
        /// 删除节点下最后一个子节点文本
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <returns>删除结果</returns>
        bool DeleteLastNodeChildText(XmlNode node);
        
        /// <summary>
        /// 删除子节点文本列表
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <returns>删除结果</returns>
        bool DeleteNodeChildTexts(XmlNode node);

        /// <summary>
        /// 根据路径修改节点文本
        /// </summary>
        /// <param name="xml">xml文档</param>
        /// <param name="nodePath">节点的路径</param>
        /// <param name="textValue">修改的文本</param>
        /// <returns>修改结果</returns>
        bool UpdateNodeTextByPath(XmlDocument xml, string nodePath, string textValue);

        /// <summary>
        /// 根据路径修改节点文本列表
        /// </summary>
        /// <param name="xml">xml文档</param>
        /// <param name="nodePath">节点的路径</param>
        /// <param name="textValue">修改的文本</param>
        /// <returns>修改结果</returns>
        bool UpdateNodeTextsByPath(XmlDocument xml, string nodePath, string textValue);

        /// <summary>
        /// 根据节点名称修改节点文本，只返回满足条件的第一个
        /// </summary>
        /// <param name="xml">xml文档</param>
        /// <param name="nodeName">文本名</param>
        /// <param name="textValue">修改的文本</param>
        /// <returns>修改结果</returns>
        bool UpdateFirstNodeTextByName(XmlDocument xml, string nodeName, string textValue);

        /// <summary>
        /// 根据名称修改节点文本列表
        /// </summary>
        /// <param name="xml">xml文档</param>
        /// <param name="nodeName">节点名</param>
        /// <param name="textValue">修改的文本</param>
        /// <returns>修改结果</returns>
        bool UpdateNodeTextsByName(XmlDocument xml, string nodeName, string textValue);

        /// <summary>
        /// 根据索引修改子节点文本
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="nodeIndex">子节点索引</param>
        /// <param name="textValue">修改的文本</param>
        /// <returns>修改结果</returns>
        bool UpdateNodeChildTextByIndex(XmlNode node, int nodeIndex, string textValue);

        /// <summary>
        /// 根据子节点名称修改节点的满足第一个子节点文本
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="nodeName">子节点名</param>
        /// <param name="textValue">修改的文本</param>
        /// <returns>修改结果</returns>
        bool UpdateFirstNodeChildTextByName(XmlNode node, string nodeName, string textValue);

        /// <summary>
        /// 根据子节点名称修改文本列表
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="nodeName">子节点名</param>
        /// <param name="textValue">修改的文本</param>
        /// <returns>修改结果</returns>
        bool UpdateNodeChildTextsByName(XmlNode node, string nodeName, string textValue);

        /// <summary>
        /// 修改节点下第一个子节点文本
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="textValue">修改的文本</param>
        /// <returns>修改结果</returns>
        bool UpdateFirstNodeChildText(XmlNode node, string textValue);

        /// <summary>
        /// 修改节点下最后一个子节点文本
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="textValue">修改的文本</param>
        /// <returns>修改结果</returns>
        bool UpdateLastNodeChildText(XmlNode node, string textValue);

        /// <summary>
        /// 修改子节点文本列表
        /// </summary>
        /// <param name="node">xml节点</param>
        /// <param name="textValue">修改的文本</param>
        /// <returns>修改结果</returns>
        bool UpdateNodeChildTexts(XmlNode node, string textValue);

        /// <summary>
        /// 保存xml
        /// </summary>
        /// <param name="xml">xml文档</param>
        /// <param name="fileName">保存位置(物理路径)</param>
        /// <returns>保存结果</returns>
        bool SaveXml(XmlDocument xml, string fileName);
    }
}
