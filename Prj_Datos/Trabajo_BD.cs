using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
namespace Prj_Datos
{
    public class Trabajo_BD
    {
        ConexionDB ConexionDB= new ConexionDB();
        OleDbCommand Orden;
        OleDbDataReader Lector;

        public void AbriCerrarBD(bool apertura)
        {
            if (apertura == true) ConexionDB.abrirDB();
            else ConexionDB.cerrarDB();
        }

        public void Consultas_BD (string consulta) //Metodo que recibe una consulta para la base de datos para poder realizar una accion como un insert,delete o update en la base de datos
        {
            Orden = new OleDbCommand(consulta, ConexionDB.conexion);
            Orden.ExecuteNonQuery();
        }

        public string Chequeo_BD(string consulta)//Recibe una consulta a la base de datos para poder enviar un string con el unico resultado que da la consulta
        {
            string dato_recibido; //Variable donde se guardara el dato recolectado de la base de datos

            Orden = new OleDbCommand(consulta, ConexionDB.conexion);
            dato_recibido = Orden.ExecuteScalar().ToString();

            return dato_recibido;
        }
        public DataTable Lectura_tabla_BD(string consulta)//Recibe una consulta a la base de datos para poder enviar un DataTable con los resultados de la consulta
        {
            DataTable tablaConsulta = new DataTable();

            Orden = new OleDbCommand(consulta, ConexionDB.conexion);
            Lector = Orden.ExecuteReader();
            tablaConsulta.Load(Lector);
            Lector.Close();

            return tablaConsulta;
        }
    }
}
