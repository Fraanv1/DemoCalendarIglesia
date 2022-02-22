using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Prj_Datos;

namespace Prj_Trabajo
{
    public class Consultas_Misas
    {
        Trabajo_BD funciones_db = new Trabajo_BD();

        public void registro_misa(string fecha, string hora, string intencion, string padre)
        {
            funciones_db.AbriCerrarBD(true);

            // En esta sentencia se guardan los datos de la misa en la DB.
            funciones_db.Consultas_BD("INSERT INTO eventos (fecha, hora_ingreso, id_tipo_evento, observaciones) VALUES ('" + fecha + "','" + hora + "', 4,'" + intencion + "')");
            
            // En esta sentencia solo guarda los datos del padre en la DB y luego se hace la relacion en la tabla persona_evento
            funciones_db.Consultas_BD("INSERT INTO personas (nombre, tipo_per) VALUES ('" + padre + "', 3)");
            funciones_db.Consultas_BD("INSERT INTO persona_evento (id_evento,id_persona) VALUES (" + funciones_db.Chequeo_BD("SELECT id_evento FROM eventos WHERE id_evento = (SELECT MAX (id_evento) FROM eventos)") + "," + funciones_db.Chequeo_BD("SELECT id_persona FROM personas WHERE id_persona = (SELECT MAX (id_persona) FROM personas)") + ")");

            funciones_db.AbriCerrarBD(false);
        }
        public DataTable lectura_misa(string fecha)
        {
            funciones_db.AbriCerrarBD(true);

            // No carga las misas en el dgv, pero SI carga los datos de eventos que involucren a un cura.

            DataTable tabla_misas = new DataTable();//Tabla general, donde se guardaran todos los datos tanto de las personas como del evento
            // Las siguientes tablas tendran los datos de cada una de las personas vinculadas con la misa 
            DataTable tabla_cura = new DataTable();

            string hora, intencion;// Estas variables contendra las horas en las cuales se realizan las misas y su intencion

            int[] id_evento = receptor_id_evento(fecha);//Array que recibira todos los id_evento de los bautismos que se relacionan con las misas

            //Creamos la estructura de la tabla_bautismos para que quede igual que el dgv de los bautismos
            string[] nombresColumnas = { "Fecha y hora", "Intencion", "Padre" }; //Arrat que contiene los nombres de todas los columnas de el DataTable, este se utilizara para cargar las tablas en un bucle
            for (int i = 0; i < nombresColumnas.Length; i++)//Bucle que se utiliza para crear todas las columnas del DataTable con sus respectivos nombres dados por el array nombreColumnas
                tabla_misas.Columns.Add(nombresColumnas[i]);
            
            for (int i = 0; i < id_evento.Length; i++) //Se hace por la cantidad de bautismos que haya en la fecha seleccionada
            {
                tabla_cura = funciones_db.Lectura_tabla_BD("SELECT nombre FROM personas WHERE id_persona IN (SELECT id_persona FROM persona_evento WHERE id_evento =" + id_evento[i] + ") AND tipo_per = 3");
                hora = funciones_db.Chequeo_BD("SELECT hora_ingreso FROM eventos WHERE id_evento =" + id_evento[i]);
                intencion = funciones_db.Chequeo_BD("SELECT observaciones FROM eventos WHERE id_evento =" + id_evento[i]);

                //En la siguiente seccion de cargara la tabla principal (tabla_bautismos) con los datos de todas las personas y las fecha y hora del evento
                tabla_misas.Rows.Add();//Se agrega una fila a tabla_bautismos donde se cargara todos los datos del bautismo
                tabla_misas.Rows[i]["Fecha y hora"] = fecha + " " + hora;
                tabla_misas.Rows[i]["Intencion"] = intencion;
                tabla_misas.Rows[i]["Padre"] = tabla_cura.Rows[0]["nombre"];
            }
            funciones_db.AbriCerrarBD(false);

            return tabla_misas;
        }
        private int[] receptor_id_evento(string fecha) //Metodo que obtiene todos los id_evento de los bautismos para poder obtener todos los datos de esos eventos y luego cargar la dgv de los bautismos
        {
            int[] id_evento;//Array que tendra cada uno de los id_evento que representa a los bautismos que hay en la fecha seleccionada por el usuario en el formulario de los bautismos

            DataTable tablaDeID_eventos = funciones_db.Lectura_tabla_BD("select id_evento from eventos where fecha = '" + fecha + "' and id_tipo_evento = 4");//DataTable que recibira todos los id_eventos de los bautismos relacionados con la fecha seleccionada por el usuario en el calendario del formulario de bautismos
            id_evento = new Int32[tablaDeID_eventos.Rows.Count];//Instanciomos el array que tendra todos los id_evento con la cantidad de eventos (identificados por su id_evento) que hay en la fecha que selecciono el usuario
            for (int i = 0; i < id_evento.Length; i++)//Bucle que nos sirvira para poder cargar todos los id_evento dentro del array id_evento
                id_evento[i] = Convert.ToInt32(tablaDeID_eventos.Rows[i]["id_evento"].ToString());//Carga cada posicion del array con el el id_evento de cada evento obteniendolo de cada fila dentro de la columna llamada id_evento

            return (id_evento);
        }
        public void act_misa(string fecha_vieja, string hora_vieja, string fecha_nueva, string hora_nueva, string intencion, string padre)
        { 
            // Esta funcion va a actualizar el evento
            funciones_db.AbriCerrarBD(true); // Envia un bool=true para abrir la DB
            int id_evento = Convert.ToInt32(funciones_db.Chequeo_BD("SELECT (id_evento) FROM eventos WHERE fecha='" + fecha_vieja + "' AND hora_ingreso='" + hora_vieja + "' AND id_tipo_evento=4")); // Aca solo busca el id del evento teniendo en cuenta la fecha y la hora.
            funciones_db.Consultas_BD("UPDATE eventos SET fecha='" + fecha_nueva + "', hora_ingreso= '" + hora_nueva + "', observaciones='" + intencion + "' WHERE id_evento=" + id_evento); // Aca actualiza la tabla eventos con los datos nuevos ingresados por el usuario.
            funciones_db.Consultas_BD("UPDATE personas SET nombre='" + padre + "' WHERE id_persona=(SELECT id_persona FROM persona_evento WHERE id_evento=" + id_evento + ")"); // Aca actualiza la tabla personas (en este caso solo es el padre) con los datos nuevos ingresador por el usuario.
            funciones_db.AbriCerrarBD(false);
        }
        public void borrar_misa(string fecha, string hora)
        {
            // Esta funcion va a borrar el evento
            funciones_db.AbriCerrarBD(true);
            int id_evento = Convert.ToInt32(funciones_db.Chequeo_BD("SELECT (id_evento) FROM eventos WHERE fecha='" + fecha + "' AND hora_ingreso='" + hora + "' AND id_tipo_evento=4")); // Aca solo busca el id del evento teniendo en cuenta la fecha y la hora.
            int id_persona = Convert.ToInt32(funciones_db.Chequeo_BD("SELECT (id_persona) FROM persona_evento WHERE id_evento=" + id_evento));
            funciones_db.Consultas_BD("DELETE FROM persona_evento WHERE id_evento=" + id_evento + " AND id_persona=" + id_persona);
            funciones_db.Consultas_BD("DELETE FROM personas WHERE id_persona=" + id_persona);
            funciones_db.Consultas_BD("DELETE FROM eventos WHERE id_evento=" + id_evento);
            funciones_db.AbriCerrarBD(false);
        }
    }
}
