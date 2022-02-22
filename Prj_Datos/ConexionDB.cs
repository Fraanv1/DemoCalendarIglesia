using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;

namespace Prj_Datos
{
    public class ConexionDB
    {
        public OleDbConnection conexion = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=BD_iglesia2.0.mdb");

        public void abrirDB()
        {
            conexion.Open();
        }
        public void cerrarDB()
        {
            conexion.Close();
        }
    }
}
