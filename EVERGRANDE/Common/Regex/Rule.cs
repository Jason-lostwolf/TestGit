using System;

using System.Collections.Generic;
using System.Text;

namespace EVERGRANDE
{
    /// <summary>
    /// 正则表达式实体类
    /// </summary>
    public class Rule
    {
        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 正则表达式
        /// </summary>
        public string RegexValue { get; set; }

        /// <summary>
        /// 错误提示
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 验证顺序
        /// </summary>
        public int Seq { get; set; }

    }
}
