using System.Configuration;
using System.Data.SqlClient;

namespace CadFarmacia
{
    public static class Conexion
    { 
        private static readonly string cs = ConfigurationManager.ConnectionStrings["Labsis457Farmacia"].ConnectionString;


    public static SqlConnection Abrir()
        {
            var cn = new SqlConnection(cs);
            cn.Open();
            return cn;
        }
    }
}