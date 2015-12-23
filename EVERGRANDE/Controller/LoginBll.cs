using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using EVERGRANDE.Common;
using System.IO;
using EVERGRANDE.Model;

namespace EVERGRANDE.BLL
{
    public class LoginBLL
    {
        /// <summary>
        /// 获取登录用户列表
        /// </summary>
        public List<User> GetUserList()
        {
            List<User> list = new List<User>();
            try
            {
                string fileName = StaticInfo.UserFile;
                //假如文件存在就读取文件
                if (System.IO.File.Exists(fileName) == true)
                {
                    using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                    {
                        using (StreamReader sr = new StreamReader(fileStream, Encoding.UTF8))
                        {
                            string temp = sr.ReadToEnd().Replace("\r\n", "\n").Trim();
                            var tempList = temp.Split(new char[] { '\n' });

                            if (tempList != null)
                            {
                                int i = 0;
                                foreach (string item in tempList)
                                {
                                    if (i == 0)
                                    {
                                        i++;
                                        continue;
                                    }
                                    //赋值
                                    User record = User.Parse(item);

                                    list.Add(record);
                                }
                            }
                        }
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 登录
        /// </summary>
        public User Login(string userName, string password)
        {
            StaticInfo.UserList = this.GetUserList();

            return StaticInfo.UserList.FirstOrDefault(p => p.UserName == userName && p.Password == password);
        }

        /// <summary>
        /// 用户名是否存在
        /// </summary>
        public bool IsUserExist(string userName)
        {
            StaticInfo.UserList = this.GetUserList();

            return StaticInfo.UserList != null && StaticInfo.UserList.FirstOrDefault(p => p.UserName == userName) != null;
        }

        public void SaveUser(List<User> recordList)
        {
            try
            {
                string fileName = StaticInfo.UserFile;
                //假如文件存在就读取文件
                //bool isFileExist = System.IO.File.Exists(fileName);

                using (FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fileStream, Encoding.UTF8))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("User,Password");
                        foreach (User item in recordList)
                        {
                            sb.AppendFormat("{0},{1}\r\n", item.UserName,item.Password);
                        }
                        sw.Write(sb.ToString());
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
