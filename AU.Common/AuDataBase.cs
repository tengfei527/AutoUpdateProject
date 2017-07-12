using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AU.Common
{
    public class AuDataBase
    {
        /// <summary>
        /// 执行升级脚本
        /// </summary>
        /// <param name="scriptPath"></param>
        /// <param name="constr"></param>
        /// <returns></returns>

        public static bool RunScript(string scriptPath, string constr)
        {
            if (!File.Exists(scriptPath))
                return false;

            string sql = "";
            string temp = "";
            //Trans tr = dbHelper.InnerCreatTrans();
            using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(constr))
            {
                FileStream fs = new FileStream(scriptPath, FileMode.Open);
                StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default);
                conn.Open();
                //事务级别
                System.Data.SqlClient.SqlTransaction tran = conn.BeginTransaction();
                bool isCommit = false;
                while (sr.Peek() > -1)
                {
                    temp = sr.ReadLine();
                    if (temp.ToLower() != "go")
                        sql += temp + "\r\n";
                    else if (!string.IsNullOrEmpty(sql))
                    {
                        OneCardSystem.DAL.DBUtility.SqlHelper.ExecuteNonQuery(tran, System.Data.CommandType.Text, sql);
                        isCommit = true;
                        sql = "";
                    }
                }
                sr.Close();
                fs.Close();
                if (isCommit)
                    tran.Commit();
                conn.Close();
            }

            return true;
        }
    }
}
