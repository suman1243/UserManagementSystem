using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementSystem.General
{
    public class StaticData
    {
        public static string EncryptData(string value)
        {
            MD5 mD5 = new MD5CryptoServiceProvider();
            byte[] passwordHash = Encoding.UTF8.GetBytes(value);
            return Encoding.UTF8.GetString(mD5.ComputeHash(passwordHash));
        }
    }
}
