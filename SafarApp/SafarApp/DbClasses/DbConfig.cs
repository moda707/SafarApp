using System;
using System.Collections.Generic;
using System.Text;

namespace SafarApp.DbClasses
{
    public class DbConfig
    {
        private const string UserName = "Safar_admin";
        private const string Password = "12345";
        private const string ConnectionName = "";
        private const string ConnectionString = "mongodb://" + UserName + ":" + Password + "@127.0.0.1:27017/SafarDB";
        public static string GetConnectionString()
        {
            return ConnectionString;
        }
    }
}
