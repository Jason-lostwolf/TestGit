using System;

using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Reflection;
using System.IO;

namespace EVERGRANDE
{
    /// <summary>
    /// XML加载
    /// </summary>
    public class XMLAccess
    {

        ///// <summary>
        ///// 读取XML文件信息
        ///// </summary>
        ///// <returns>配置信息</returns>
        //public Dictionary<string, string> GetXMLInfo(string filePath)
        //{
        //    XmlDocument document = new XmlDocument();
        //    document.Load(filePath); //加载XML文件
        //    XmlNode xmlNode = document.DocumentElement;
        //    Dictionary<string, string> nodeList = new Dictionary<string, string>();
        //    foreach (XmlNode node in xmlNode.ChildNodes)
        //    {
        //        if (node.ChildNodes.Count >= 2)
        //        {
        //            if (!nodeList.ContainsKey(node.ChildNodes[0].InnerText))
        //            {
        //                nodeList.Add(node.ChildNodes[0].InnerText, node.ChildNodes[1].InnerText);
        //            }
        //        }
        //    }
        //    return nodeList;
        //}

        /// <summary>
        /// 读取正则表达式表
        /// </summary>
        /// <returns></returns>
        public static List<Rule> GetRuleInfo(string filePath)
        {
            List<Rule> ruleList = new List<Rule>();
            Rule rule = null;
            XmlDocument document = new XmlDocument();
            
            document.Load(filePath);
            XmlNode xmlNode = document.DocumentElement;
            //XmlNodeList nodes = document.SelectNodes("//Rules");
            foreach (XmlNode node in xmlNode.ChildNodes)
            {
                if (node.ChildNodes.Count == 5)
                {
                    rule = new Rule();
                    rule.Key = node.SelectSingleNode("Key").InnerText;
                    rule.RegexValue = node.SelectSingleNode("RegexValue").InnerText;
                    rule.ErrorMessage = node.SelectSingleNode("ErrorMessage").InnerText;
                    rule.Description = node.SelectSingleNode("Description").InnerText;
                    rule.Seq = Convert.ToInt32(node.SelectSingleNode("Seq").InnerText);
                    ruleList.Add(rule);
                }
            }
            return ruleList;
        }

        /// <summary>
        /// 读取正则表达式表
        /// </summary>
        /// <param name="asm">执行文件的程序集</param>
        /// <param name="assemblyName">资源里的文件名称</param>
        /// <returns>正则表达式列表</returns>
        public static List<Rule> GetRuleInfoFromAssembly(Assembly asm, string assemblyName)
        {
            List<Rule> ruleList = new List<Rule>();
            Rule rule = null;

            XmlDocument document = new XmlDocument();

            document.Load(asm.GetManifestResourceStream(assemblyName));
            XmlNode xmlNode = document.DocumentElement;
            //XmlNodeList nodes = document.SelectNodes("//Rules");
            foreach (XmlNode node in xmlNode.ChildNodes)
            {
                if (node.ChildNodes.Count == 5)
                {
                    rule = new Rule();
                    rule.Key = node.SelectSingleNode("Key").InnerText;
                    rule.RegexValue = node.SelectSingleNode("RegexValue").InnerText;
                    rule.ErrorMessage = node.SelectSingleNode("ErrorMessage").InnerText;
                    rule.Description = node.SelectSingleNode("Description").InnerText;
                    rule.Seq = Convert.ToInt32(node.SelectSingleNode("Seq").InnerText);
                    ruleList.Add(rule);
                }
            }
            return ruleList;
        }

        //public Dictionary<string, string> GetXMLInfoFromAssembly(Assembly asm,string assemblyName)
        //{
        //    ////Assembly asm = Assembly.GetExecutingAssembly();
        //    //XmlTextReader reader;
        //    //Dictionary<string, string> list = new Dictionary<string, string>();
        //    ////string[] name = asm.GetManifestResourceNames();
        //    //Stream sm = asm.GetManifestResourceStream(assemblyName);
        //    //reader = new XmlTextReader(sm);
        //    //string key = string.Empty;
        //    //string value = string.Empty;
        //    //while (reader.Read())
        //    //{

        //    //    if (reader.NodeType == XmlNodeType.Element)
        //    //    {
        //    //        if (reader.Name == "Key")
        //    //        {
        //    //            key = reader.ReadElementString();
                        
        //    //        }
        //    //        if (reader.Name == "Value")
        //    //        {
        //    //            value = reader.ReadElementString();
        //    //        }
        //    //        if (key != string.Empty && value != string.Empty)
        //    //        {
        //    //            list.Add(key, value);
        //    //            key = string.Empty;
        //    //            value = string.Empty;
        //    //        }
        //    //    }
        //    //}


        //    //return list;

        //    XmlDocument document = new XmlDocument();
        //    Stream s = asm.GetManifestResourceStream(assemblyName);
        //    //Stream s1 = asm.GetManifestResourceStream("enpot.exe");
        //    document.Load(s); //加载XML文件
        //    XmlNode xmlNode = document.DocumentElement;
        //    Dictionary<string, string> nodeList = new Dictionary<string, string>();
        //    foreach (XmlNode node in xmlNode.ChildNodes)
        //    {
        //        if (node.ChildNodes.Count >= 2)
        //        {
        //            if (!nodeList.ContainsKey(node.ChildNodes[0].InnerText))
        //            {
        //                nodeList.Add(node.ChildNodes[0].InnerText, node.ChildNodes[1].InnerText);
        //            }
        //        }
        //    }
        //    return nodeList;
        //}
    }
}
