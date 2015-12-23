using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace EVERGRANDE.Controller
{
    public class FileHelper
    {
        /// <summary>
        /// 加载文件内容
        /// </summary>
        public string LoadFile(string fileName)
        {
            try
            {
                string record = string.Empty;
                //假如文件存在就读取文件
                //if (System.IO.File.Exists(fileName) == true)
                //{
                    using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                    {
                        using (StreamReader sr = new StreamReader(fileStream, Encoding.UTF8))
                        {
                            record = sr.ReadToEnd();
                        }
                    }
                //}
                //else
                //{
                //    throw new Exception("文件不存在。");
                //}

                return record;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 保存文件内容
        /// </summary>
        /// <param name="record"></param>
        public void SaveFile(string record,string fileName)
        {
            try
            {
                using (FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fileStream, Encoding.UTF8))
                    {
                        sw.Write(record);
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
