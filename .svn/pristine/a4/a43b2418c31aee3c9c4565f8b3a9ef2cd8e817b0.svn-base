//===============================================================================
// This file is based on the Microsoft Data Access Application Block for .NET
// For more information please go to 
// http://msdn.microsoft.com/library/en-us/dnbda/html/daab-rm.asp
//===============================================================================

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.IO;

namespace OneCardSystem.DAL.DBUtility
{

    /// <summary>
    /// The SqlHelper class is intended to encapsulate high performance, 
    /// scalable best practices for common uses of SqlClient.
    /// </summary>
    public abstract class SqlHelper
    {

        //Database connection strings
        public static string ConnectionStringLocalTransaction = "";
        //      public static readonly string ConnectionStringInventoryDistributedTransaction = ConfigurationManager.ConnectionStrings["SQLConnString2"].ConnectionString;
        //      public static readonly string ConnectionStringOrderDistributedTransaction = ConfigurationManager.ConnectionStrings["SQLConnString3"].ConnectionString;
        //public static readonly string ConnectionStringProfile = ConfigurationManager.ConnectionStrings["SQLProfileConnString"].ConnectionString;		

        // Hashtable to store cached parameters
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(ConnectionStringLocalTransaction))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {

            SqlCommand cmd = new SqlCommand();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) against an existing database connection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="conn">an existing database connection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {

            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) using an existing SQL Transaction 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="trans">an existing sql transaction</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// Execute a SqlCommand that returns a resultset against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  SqlDataReader r = ExecuteReader(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>A SqlDataReader containing the results</returns>
        public static SqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connectionString);

            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }
        /// <summary>
        /// 获取数据集
        /// </summary>
        /// <param name="connection">连接</param>
        /// <param name="cmdType">SQL类型</param>
        /// <param name="cmdText">指令</param>
        /// <param name="commandParameters">参数</param>
        /// <returns></returns>
        public static DataSet ExecuteTable(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {

            SqlCommand cmd = new SqlCommand();
            DataSet dt = new DataSet();

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = cmd;

            adapter.Fill(dt);

            return dt;
        }
        /// <summary>
        /// 获取数据集
        /// </summary>
        /// <param name="cmdType">SQL类型</param>
        /// <param name="cmdText">指令</param>
        /// <param name="commandParameters">参数</param>
        /// <returns>数据集</returns>
        public static DataTable ExecuteTable(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionStringLocalTransaction))
                {
                    PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = cmd;

                    adapter.Fill(dt);
                }
            }
            catch (SqlException e)
            {
                throw e;
            }

            return dt;
        }

        /// <summary>
        /// 分页存储过程     
        /// </summary>
        /// <param name="tblName">TableName</param>
        /// <param name="strGetFields">Select field</param>
        /// <param name="fldName">Primary field name</param>
        /// <param name="PageSize">Page size</param>
        /// <param name="PageIndex">Page index</param>
        /// <param name="orderType">SortType, not 0 is DESC</param>
        /// <param name="strWhere">QueryCodition(Note: Don't include where) </param>
        /// <param name="totalCount">Total Count</param>
        /// <returns>DataSet</returns>
        public static DataSet ExecutePage(string tblName, string strGetFields, string fldName, int PageSize, int PageIndex, bool orderType, string strWhere, out int totalCount)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionStringLocalTransaction))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    SetPageParams(cmd);

                    cmd.Parameters[0].Value = tblName;
                    cmd.Parameters[1].Value = strGetFields;
                    cmd.Parameters[2].Value = fldName;
                    cmd.Parameters[3].Value = PageSize;
                    cmd.Parameters[4].Value = PageIndex;
                    cmd.Parameters[5].Value = orderType ? 1 : 0;
                    cmd.Parameters[6].Value = strWhere;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_ReacordPage";
                    cmd.CommandTimeout = 180;

                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = cmd;

                    DataSet source = new DataSet();
                    adapter.Fill(ds);

                    object o = cmd.Parameters["@total"].Value;
                    totalCount = (o == null || o == DBNull.Value) ? 0 : System.Convert.ToInt32(o);
                }
            }
            catch (SqlException e)
            {
                throw e;
            }

            return ds;
        }
        /// <summary>
        /// Execute a SqlCommand that returns the first column of the first record against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>        
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        public static object ExecuteScalar(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection connection = new SqlConnection(ConnectionStringLocalTransaction))
            {
                connection.Open();
                SqlTransaction sqlTran = connection.BeginTransaction();
                try
                {
                    PrepareCommand(cmd, connection, sqlTran, cmdType, cmdText, commandParameters);
                    object val = cmd.ExecuteScalar();
                    //cmd.Parameters.Clear();
                    sqlTran.Commit();
                    return val;
                }
                catch (Exception e)
                {
                    sqlTran.Rollback();
                    throw e;
                }
            }
        }
        /// <summary>
        /// Execute a SqlCommand that returns the first column of the first record against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// Execute a SqlCommand that returns the first column of the first record against an existing database connection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="conn">an existing database connection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        public static object ExecuteScalar(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {

            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// add parameter array to the cache
        /// </summary>
        /// <param name="cacheKey">Key to the parameter cache</param>
        /// <param name="cmdParms">an array of SqlParamters to be cached</param>
        public static void CacheParameters(string cacheKey, params SqlParameter[] commandParameters)
        {
            parmCache[cacheKey] = commandParameters;
        }

        /// <summary>
        /// Retrieve cached parameters
        /// </summary>
        /// <param name="cacheKey">key used to lookup parameters</param>
        /// <returns>Cached SqlParamters array</returns>
        public static SqlParameter[] GetCachedParameters(string cacheKey)
        {
            SqlParameter[] cachedParms = (SqlParameter[])parmCache[cacheKey];

            if (cachedParms == null)
                return null;

            SqlParameter[] clonedParms = new SqlParameter[cachedParms.Length];

            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms[i] = (SqlParameter)((ICloneable)cachedParms[i]).Clone();

            return clonedParms;
        }

        /// <summary>
        /// Prepare a command for execution
        /// </summary>
        /// <param name="cmd">SqlCommand object</param>
        /// <param name="conn">SqlConnection object</param>
        /// <param name="trans">SqlTransaction object</param>
        /// <param name="cmdType">Cmd type e.g. stored procedure or text</param>
        /// <param name="cmdText">Command text, e.g. Select * from Products</param>
        /// <param name="cmdParms">SqlParameters to use in the command</param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }
        /// <summary>
        /// 设置分页存储过程
        /// </summary>
        /// <param name="cmd">SQL Command</param>
        private static void SetPageParams(SqlCommand cmd)
        {
            cmd.Parameters.Add(new SqlParameter("@tblName", SqlDbType.VarChar, 255));
            cmd.Parameters.Add(new SqlParameter("@strGetFields", SqlDbType.VarChar, 1000));
            cmd.Parameters.Add(new SqlParameter("@fldName", SqlDbType.VarChar, 255));
            cmd.Parameters.Add(new SqlParameter("@PageSize", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("@PageIndex", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("@OrderType", SqlDbType.Bit));
            cmd.Parameters.Add(new SqlParameter("@strWhere", SqlDbType.VarChar, 1500));

            SqlParameter param = new SqlParameter("@total", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(param);
        }
        /// <summary>
        /// 数据库备份脚本
        /// 要求当前连接为master
        /// </summary>
        public static readonly string BackupDatabaseSQL = "BACKUP DATABASE {0} TO DISK =  '{1}.bak' ";
        public static readonly string RestoreDatabaseSQL = "RESTORE DATABASE {0} FROM DISK = '{1}'";
        public static readonly string SPIDSQL = "SELECT spid FROM sysprocesses ,sysdatabases WHERE sysprocesses.dbid=sysdatabases.dbid AND sysdatabases.Name='{0}'";
        public static readonly string KILLSPID = "KILL {0}";
    }
}