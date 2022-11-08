using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace DieuHanhCongTruong.Common
{
    internal class Logincmd
    {
        private static List<string> GetDataSources2()
        {
            List<string> retVal = new List<string>();

            string ServerName = Environment.MachineName;
            RegistryView registryView = Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32;
            using (RegistryKey hklm = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, registryView))
            {
                RegistryKey instanceKey = hklm.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL", false);
                if (instanceKey != null)
                {
                    foreach (var instanceName in instanceKey.GetValueNames())
                    {
                        if (instanceName == "MSSQLSERVER")
                            continue;

                        retVal.Add(ServerName + "\\" + instanceName);
                    }
                }
            }

            return retVal;
        }

        public static SqlConnection connectSever(string ipAddr, string databaseName, string userName, string userPasswords)
        {
            SqlConnection cn = null;

            try
            {
                //cn = new SqlConnection("server=MrLam-PC;" + "Initial Catalog=LOGIN;" + "User id=sa;" + "Password=Vuthelam1608;");
                //cn = new SqlConnection("server=192.168.0.114;" + "Initial Catalog=LOGIN;" + "User id=sa;" + "Password=Vuthelam1608;");
                //cn = new SqlConnection(String.Format("server={0};", ipAddr) + "Initial Catalog=LOGIN;" + "User id=sa;" + "Password=Vuthelam1608;");

                if (string.IsNullOrEmpty(ipAddr) || ipAddr == "...")
                    ipAddr = Environment.MachineName;

                cn = new SqlConnection(String.Format("server={0};", ipAddr) + String.Format("Initial Catalog={0};", databaseName) + String.Format("User id={0};", userName) + String.Format("Password={0};", userPasswords) + "MultipleActiveResultSets=true;");
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
                cn.Open();
                return cn;
            }
            catch (Exception ex)
            {
                var mess = ex.Message;

                var lstSeverName = GetDataSources2();
                foreach (var severName in lstSeverName)
                {
                    try
                    {
                        cn = new SqlConnection(String.Format("server={0};", severName) + String.Format("Initial Catalog={0};", databaseName) + String.Format("User id={0};", userName) + String.Format("Password={0};", userPasswords) + "MultipleActiveResultSets=true;");
                        if (cn.State == ConnectionState.Open)
                        {
                            cn.Close();
                        }
                        cn.Open();

                        return cn;
                    }
                    catch (Exception ex1)
                    {
                        var mess1 = ex1.Message;
                    }
                }

                return null;
            }
        }
    }
}