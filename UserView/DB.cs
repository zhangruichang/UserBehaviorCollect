using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySQLDriverCS;//引入mysql  .net驱动程序  
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace DataAccess
{
        class DB
        {
            public static string connectionString = "Location=202.121.197.75;Data Source=model; User ID=root; Password=sa;Port=1433;CharSet=gb2312";
            public static DataSet getdatasetbysql(string sql)
            {
                DataSet ds=new DataSet();
                //MySQLConnection conn = new MySQLConnection(new MySQLConnectionString("202.121.197.75", "model", "root", "sa", 1433).AsString);
                MySQLConnection conn = new MySQLConnection(connectionString);
                try
                {
                    conn.Open();
                    MySQLCommand com = new MySQLCommand("set names gb2312", conn);
                    com.ExecuteNonQuery();
                    MySQLDataAdapter myadp = new MySQLDataAdapter(sql, conn);
                    myadp.Fill(ds);
                    return ds;
                }
                catch (MySQLException ex)
                {
                    MessageBox.Show(ex.Message);
                    System.Environment.Exit(0);
                    return null;
                }
            }
            public static bool executesql(string sql)
            {
                MySQLConnection conn = new MySQLConnection(connectionString);
                conn.Open();
                    MySQLCommand commn = new MySQLCommand(sql, conn);
                    if (commn.ExecuteNonQuery() > 0)
                    {
                        conn.Close();
                        return true;
                    }
                    else
                    {
                        conn.Close();
                        return false;
                    }
                
            }
        }  
}
