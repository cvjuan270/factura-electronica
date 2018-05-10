using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace MiLibreria
{
    public class Utilidades
    {
        public static DataSet ejecutar(string cmd)
        {
            SqlConnection con = new SqlConnection("data source = WIN-N0KOJNSL9KV; initial catalog = DVINVERSIONES; user id = sa; password = Dv123456789");
            con.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd, con);
            da.Fill(ds);
            con.Close();
            return ds;
        }
    }
}
