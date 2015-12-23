using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using EVERGRANDE.Common;

namespace EVERGRANDE.Model
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public static User Parse(string record)
        {
            User result = new User();

            string[] arr = record.Split(StaticInfo.SplitChat);

            result.UserName = arr[0];
            result.Password = arr[1];

            return result;
        }
    }
}
