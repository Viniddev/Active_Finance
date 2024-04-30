using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace RPA_Teste.DataBase
{
    public class AppSettings
    {
        public string ReadConnectionString(string conectionString)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath($@"{AppDomain.CurrentDomain.BaseDirectory}")
                .AddJsonFile(@"AppSettings.json")
                .Build();
            var conn = configuration.GetConnectionString(conectionString);

            return conn;
        }
    }
}
