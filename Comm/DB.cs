using MySql.Data.MySqlClient;
using System;
using System.Data;
using works.Data;

namespace works.Comm
{
    public class DB
    {
        #region single
        public static DataTable SelectSingle(string query)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(Info.Conf["DB"]))
            {
                con.Open();

                using (MySqlCommand cmd = new MySqlCommand(query.ToString(), con))
                {
                    using (MySqlDataAdapter sda = new MySqlDataAdapter())
                    {
                        sda.SelectCommand = cmd;
                        using (DataSet ds = new DataSet())
                        {
                            sda.Fill(ds);
                            dt = ds.Tables[0];
                        }
                    }
                }
            }
            return dt;
        }
        #endregion

        #region multi
        public static DataTable[] SelectMulti(string[] query)
        {
            DataTable[] dt = new DataTable[query.Length];
            using (MySqlConnection con = new MySqlConnection(Info.Conf["DB"]))
            {
                con.Open();

                using (MySqlCommand cmd = new MySqlCommand(string.Concat(query), con))
                {
                    using (MySqlDataAdapter sda = new MySqlDataAdapter())
                    {
                        sda.SelectCommand = cmd;
                        using (DataSet ds = new DataSet())
                        {
                            sda.Fill(ds);
                            for (int i = 0; i < ds.Tables.Count; i++)
                            {
                                dt[i] = ds.Tables[i];
                            }
                        }
                    }
                }
            }
            return dt;
        }
        #endregion

        #region execute
        public static int Execute(string query)
        {
            int rst = 0;
            using (MySqlConnection conn = new MySqlConnection(Info.Conf["DB"]))
            {
                try
                {
                    conn.Open();
                    rst = new MySqlCommand(query, conn).ExecuteNonQuery();
                }
                catch (Exception)
                {
                    return -1;
                }
            }
            return rst;
        }
        #endregion

        #region single for holiday
        public static DataTable SelectComm(string query)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection con = new MySqlConnection(Info.Conf["COMM"]))
            {
                con.Open();

                using (MySqlCommand cmd = new MySqlCommand(query.ToString(), con))
                {
                    using (MySqlDataAdapter sda = new MySqlDataAdapter())
                    {
                        sda.SelectCommand = cmd;
                        using (DataSet ds = new DataSet())
                        {
                            sda.Fill(ds);
                            dt = ds.Tables[0];
                        }
                    }
                }
            }
            return dt;
        }
        #endregion
    }
}