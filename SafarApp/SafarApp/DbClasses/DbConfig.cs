﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SafarApp.DbClasses
{
    public class DbConfig
    {
        private const string UserName = "moda_admin";
        private const string Password = "177282523";
        private const string ConnectionName = "";
        private const string ConnectionString = "mongodb://" + UserName + ":" + Password + "@127.0.0.1:27017/Safar";
        public static string GetConnectionString()
        {
            return ConnectionString;
        }
    }
}
