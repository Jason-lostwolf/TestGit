using System;

using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using EVERGRANDE.Common;

namespace EVERGRANDE
{
    /// <summary>
    /// 正则表达式管理类
    /// </summary>
    public static class RuleManager
    {
        private static List<Rule> RuleList = null;

        public static string RuleErrorMessage { get; set; }

        static RuleManager()
        {
            //加载规则
            //RuleList=配置文件里面的List
            RuleList = new List<Rule>();
            //XmlReader xmlReader;
            //如果没有找到嵌入资源,就再找其他目录的
            bool hasPath = false;
            foreach (string path in StaticInfo.asm.GetManifestResourceNames())
            {
                if (path == StaticInfo.RulesConfigResourcePath)
                {
                    hasPath = true;
                }
            }

            if (hasPath)
            {
                RuleList = XMLAccess.GetRuleInfoFromAssembly(StaticInfo.asm, StaticInfo.RulesConfigResourcePath);
            }
            //else
            //{
            //    RuleList = XMLAccess.GetRuleInfo(ProjectConfig.RulesConfigPath);
            //}

            //if (ProjectConfig.asm.GetManifestResourceNames().Any(p=>p == ProjectConfig.RulesConfigResourcePath))
            //{
            //    RuleList = XMLAccess.GetRuleInfoFromAssembly(ProjectConfig.asm, ProjectConfig.RulesConfigResourcePath);
            //}
            //else
            //{
            //    RuleList = XMLAccess.GetRuleInfo(ProjectConfig.RulesConfigPath);
            //}

        }

        #region 正则表达式排序
        private static int CompareRuleBySeq(Rule x, Rule y)
        {
            if (x == null)
            {
                if (y == null)
                {
                    // If x is null and y is null, they're
                    // equal. 
                    return 0;
                }
                else
                {
                    // If x is null and y is not null, y
                    // is greater. 
                    return -1;
                }
            }
            else
            {
                // If x is not null...
                //
                if (y == null)
                // ...and y is null, x is greater.
                {
                    return 1;
                }
                else
                {
                    // ...and y is not null, compare the 
                    // lengths of the two strings.
                    //
                    int retval = x.Seq.CompareTo(y.Seq);

                    return retval;
                    //if (retval != 0)
                    //{
                    //    // If the strings are not of equal length,
                    //    // the longer string is greater.
                    //    //
                    //    return retval;
                    //}
                    //else
                    //{
                    //    // If the strings are of equal length,
                    //    // sort them with ordinary string comparison.
                    //    //
                    //    return x.CompareTo(y);
                    //}
                }
            }
        }
        #endregion

        /// <summary>
        /// 正则表达式判断
        /// </summary>
        /// <param name="key">需要判断的值的类型</param>
        /// <param name="value">需要判断的值</param>
        /// <returns></returns>
        public static string Check(string key, string value)
        {
            //首先提取全部为对应key的Rule
            //初始化出错信息
            RuleManager.RuleErrorMessage = string.Empty;

            //取出对应key的正则表达式
            //var templist = from m in RuleList
            //               where m.Key == key
            //               orderby m.Seq
            //               select m;
            List<Rule> checkList = new List<Rule>();
            foreach (Rule item in RuleList)
            {
                if (item.Key == key)
                {
                    checkList.Add(item);
                }
            }

            checkList.Sort(CompareRuleBySeq);

            //List<Rule> checkList = templist.ToList<Rule>();

            string compare = value;

            //检查每一项正则表达式

            foreach (Rule rule in checkList)
            {
                MatchCollection matches = Regex.Matches(compare, rule.RegexValue);

                Match match = Regex.Match(compare, rule.RegexValue);
                string text = string.Empty;
                foreach (Match m in matches)
                {
                    text = text + m.Value;
                }
                compare = text;

                //规则匹配不成功,将规则的错误信息放到ConfigObject中
                if (compare == string.Empty)
                {
                    RuleManager.RuleErrorMessage = rule.ErrorMessage;
                    
                    return string.Empty;
                }
            }
            return compare;
        }
    }
}
