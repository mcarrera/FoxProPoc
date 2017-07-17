﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxProPOC
{
    class Program
    {
        static void Main(string[] args)
        {

            var conn = Connection;
            var comm = conn.CreateCommand();
            comm.CommandText = @"SELECT * FROM crop";

            IDataReader reader = comm.ExecuteReader();

            var arrayList = new ArrayList();
            while (reader != null && reader.Read())
            {
                var values = new object[reader.FieldCount];
                reader.GetValues(values);
                arrayList.Add(values);
            }

            reader?.Close();
            conn.Close();

            foreach (object[] row in arrayList)
            {
                foreach (var column in row)
                {
                    Console.WriteLine(column.ToString());
                }

            }
        }

        private static OleDbConnection Connection
        {
            get
            {
                var connectionString = ConfigurationManager.ConnectionStrings["FoxProPOC.Properties.Settings.ConnectionString"].ConnectionString;
                return new OleDbConnection(connectionString);
            }
        }
    }


}
