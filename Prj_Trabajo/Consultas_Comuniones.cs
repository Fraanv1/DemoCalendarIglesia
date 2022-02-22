using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Prj_Datos;

namespace Prj_Trabajo
{
    public class Consultas_Comuniones
    {
        Trabajo_BD funciones_db = new Trabajo_BD();

        public void registro_comunion(string nom_cat, string grupo_cat, string nom_padre, string hora, string fecha, int cant_pibes)
        {
            funciones_db.AbriCerrarBD(true);

            // En esta linea se carga la comunion y sus datos en la tabla EVENTOS (la cantidad de pibes se carga en observaciones
            // para no crear otra columna en la DB y generar asi mas campos vacios).
            funciones_db.Consultas_BD("INSERT INTO eventos (fecha, hora_ingreso, id_tipo_evento) values ('" + fecha + "','" + hora + "', 6)");

            // En esta linea se guardan los datos de la catequista en la DB, y despues se relaciona al ultimo evento cargado
            // en la tabla persona_evento.
            funciones_db.Consultas_BD("INSERT INTO personas (nombre, tipo_per, grupo_cat, cant_grup_cat) VALUES ('" + nom_cat + "', 8, '" + grupo_cat + "'," + cant_pibes + ")");
            funciones_db.Consultas_BD("INSERT INTO persona_evento (id_evento,id_persona) VALUES (" + funciones_db.Chequeo_BD("SELECT id_evento FROM eventos WHERE id_evento = (SELECT MAX (id_evento) FROM eventos)") + "," + funciones_db.Chequeo_BD("SELECT id_persona FROM personas WHERE id_persona = (SELECT MAX (id_persona) FROM personas)") + ")"); 

            // Aca sucede lo mismo con lo anterior, pero ahora carga al padre (hacer despues la verificacion de si ya existe o no en la DB).
            funciones_db.Consultas_BD("INSERT INTO personas (nombre, tipo_per) VALUES ('" + nom_padre + "', 3)");
            funciones_db.Consultas_BD("INSERT INTO persona_evento (id_evento,id_persona) VALUES (" + funciones_db.Chequeo_BD("SELECT id_evento FROM eventos WHERE id_evento = (SELECT MAX (id_evento) FROM eventos)") + "," + funciones_db.Chequeo_BD("SELECT id_persona FROM personas WHERE id_persona = (SELECT MAX (id_persona) FROM personas)") + ")");

            funciones_db.AbriCerrarBD(false);        
        }
        public DataTable lectura_comunion(string fecha)
        {
            funciones_db.AbriCerrarBD(true);

            DataTable tabla_comuniones = new DataTable();//Tabla general, donde se guardaran todos los datos tanto de las personas como del evento
            // Las siguientes tablas tendran los datos de cada una de las personas vinculadas con la comunion. 
            DataTable tabla_catequista = new DataTable();
            DataTable tabla_cura = new DataTable();
            string hora;// Esta variable contendra las horas en las cuales se realizan los bautismos

            int[] id_evento = receptor_id_evento(fecha);//Array que recibira todos los id_evento de los bautismos que se relacionan con los bautismos

            //Creamos la estructura de la tabla_bautismos para que quede igual que el dgv de los bautismos
            string[] nombresColumnas = { "Fecha y hora", "Nombre Catequista", "Grupo", "Cantidad chicos", "Padre"}; //Arrat que contiene los nombres de todas los columnas de el DataTable, este se utilizara para cargar las tablas en un bucle
            for (int i = 0; i < nombresColumnas.Length; i++)//Bucle que se utiliza para crear todas las columnas del DataTable con sus respectivos nombres dados por el array nombreColumnas
                tabla_comuniones.Columns.Add(nombresColumnas[i]);

            for (int i = 0; i < id_evento.Length; i++) //Se hace por la cantidad de bautismos que haya en la fecha seleccionada
            {
                tabla_catequista = funciones_db.Lectura_tabla_BD("SELECT nombre, grupo_cat, cant_grup_cat FROM personas WHERE id_persona IN (SELECT id_persona FROM persona_evento WHERE id_evento =" + id_evento[i] + ") AND tipo_per = 8");
                tabla_cura = funciones_db.Lectura_tabla_BD("SELECT nombre FROM personas WHERE id_persona IN (SELECT id_persona FROM persona_evento WHERE id_evento =" + id_evento[i] + ") AND tipo_per = 3");
                hora = funciones_db.Chequeo_BD("SELECT hora_ingreso FROM eventos WHERE id_evento =" + id_evento[i]);
                
                //En la siguiente seccion de cargara la tabla principal (tabla_bautismos) con los datos de todas las personas y las fecha y hora del evento
                tabla_comuniones.Rows.Add();//Se agrega una fila a tabla_bautismos donde se cargara todos los datos del bautismo
                tabla_comuniones.Rows[i]["Fecha y hora"] = fecha + " " + hora;
                tabla_comuniones.Rows[i]["Nombre Catequista"] = tabla_catequista.Rows[0]["nombre"];
                tabla_comuniones.Rows[i]["Grupo"] = tabla_catequista.Rows[0]["grupo_cat"];
                tabla_comuniones.Rows[i]["Cantidad Chicos"] = tabla_catequista.Rows[0]["cant_grup_cat"];
                tabla_comuniones.Rows[i]["Padre"] = tabla_cura.Rows[0]["nombre"];
            }
            funciones_db.AbriCerrarBD(false);

            return tabla_comuniones;
        }
        private int[] receptor_id_evento(string fecha) //Metodo que obtiene todos los id_evento de los bautismos para poder obtener todos los datos de esos eventos y luego cargar la dgv de los bautismos
        {

            int[] id_evento;//Array que tendra cada uno de los id_evento que representa a los bautismos que hay en la fecha seleccionada por el usuario en el formulario de los bautismos

            DataTable tablaDeID_eventos = funciones_db.Lectura_tabla_BD("select id_evento from eventos where fecha = '" + fecha + "' and id_tipo_evento = 6");//DataTable que recibira todos los id_eventos de los bautismos relacionados con la fecha seleccionada por el usuario en el calendario del formulario de bautismos
            id_evento = new Int32[tablaDeID_eventos.Rows.Count];//Instanciomos el array que tendra todos los id_evento con la cantidad de eventos (identificados por su id_evento) que hay en la fecha que selecciono el usuario
            for (int i = 0; i < id_evento.Length; i++)//Bucle que nos sirvira para poder cargar todos los id_evento dentro del array id_evento
                id_evento[i] = Convert.ToInt32(tablaDeID_eventos.Rows[i]["id_evento"].ToString());//Carga cada posicion del array con el el id_evento de cada evento obteniendolo de cada fila dentro de la columna llamada id_evento

            return (id_evento);
        }
        public void act_comunion(string fecha_vieja, string hora_vieja, string fecha_nueva, string hora_nueva, string nombre_cat, string grupo, int cant_grupo, string nombre_cura)
        {
            // Esta funcion va a actualizar el evento
            funciones_db.AbriCerrarBD(true); // Envia un bool=true para abrir la DB
            int id_evento = Convert.ToInt32(funciones_db.Chequeo_BD("SELECT (id_evento) FROM eventos WHERE fecha='" + fecha_vieja + "' AND hora_ingreso='" + hora_vieja + "' AND id_tipo_evento=6")); // Aca solo busca el id del evento teniendo en cuenta la fecha y la hora.
            int[] id_personas = obtencion_id_personas(id_evento);
            int id_cura = id_personas[1];
            int id_catequista = id_personas[0];
            funciones_db.Consultas_BD("UPDATE eventos SET fecha='" + fecha_nueva + "', hora_ingreso= '" + hora_nueva + "' WHERE id_evento=" + id_evento); // Aca actualiza la tabla eventos con los datos nuevos ingresados por el usuario.
            funciones_db.Consultas_BD("UPDATE personas SET nombre='" + nombre_cat + "', grupo_cat='" + grupo + "', cant_grup_cat=" + cant_grupo + " WHERE id_persona=" + id_catequista); // Aca actualiza la tabla personas (en este caso solo es la catequista) con los datos nuevos ingresador por el usuario.
            funciones_db.Consultas_BD("UPDATE personas SET nombre='" + nombre_cura + "' WHERE id_persona=" + id_cura); // Aca actualiza la tabla personas (en este caso solo es el cura) con los datos nuevos ingresador por el usuario.
            funciones_db.AbriCerrarBD(false);
        }
        private int[] obtencion_id_personas(int id_evento)//Este metodo nos dara todos los id_evento de las personas que pertencen al evento, todo en un array
        {
            int[] id_personas = new int[8];//Array que contendran todos los id_persona de cada persona. Y se instancia segun la cantidad de personas que haya en el evento para que el array tenga espacio para cargar cada uno de los id_persona

            DataTable tabla_id_personas = new DataTable(); //Esta tabla recibira todos los id_persona de la consulta a la base de datos, para que posteriormente sea almacenados en el array id_personas
            tabla_id_personas = funciones_db.Lectura_tabla_BD("SELECT id_persona FROM persona_evento WHERE id_evento = " + id_evento);// recibe todos los id_persona de la tabla persona_evento los cuales tengan el mismo id_evento que el evento que se modifico

            for (int i = 0; i < tabla_id_personas.Rows.Count; i++)//Bucle que cargar el array con cada id_persona, y que se detiene cuando llega al limite de fila que tiene la tabla tabla_id_personas ya que cada fila representa un id_persona de cada persona
                id_personas[i] = Convert.ToInt32(tabla_id_personas.Rows[i]["id_persona"].ToString());//Carga el valor de cada fila de la tabla a cada posicion del array

            return id_personas;
        }
        public void borrar_comunion(string fecha, string hora)
        {
            // Esta funcion va a borrar el evento
            funciones_db.AbriCerrarBD(true);
            int id_evento = Convert.ToInt32(funciones_db.Chequeo_BD("SELECT (id_evento) FROM eventos WHERE fecha='" + fecha + "' AND hora_ingreso='" + hora + "' AND id_tipo_evento=6")); // Aca solo busca el id del evento teniendo en cuenta la fecha y la hora.
            int[] id_personas = obtencion_id_personas(id_evento);
            int id_cura = id_personas[1];
            int id_catequista = id_personas[0];
            funciones_db.Consultas_BD("DELETE FROM persona_evento WHERE id_evento = " + id_evento);//Borramos todos lo registros de la tabla persona_evento que tengan relacion con el evento que se desea borrar
            funciones_db.Consultas_BD("DELETE FROM eventos WHERE id_evento = " + id_evento);//Borramos el evento a traves de su id_evento
            funciones_db.Consultas_BD("DELETE FROM personas WHERE id_persona = " + id_cura);
            funciones_db.Consultas_BD("DELETE FROM personas WHERE id_persona = " + id_catequista);
            funciones_db.AbriCerrarBD(false);
        }
    }
}
