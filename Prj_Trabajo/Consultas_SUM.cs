using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Prj_Datos;

namespace Prj_Trabajo
{
    public class Consultas_SUM
    {
        Trabajo_BD funciones_db = new Trabajo_BD();

        public void registro_sum(string nombre_locador, string apellido_locador, int dni, string telefono, string tipo_tel, int coste, string fecha, string hora_ingreso, string hora_egreso, string observaciones) 
        {
            funciones_db.AbriCerrarBD(true);//Abrir base

            //Carga de fecha que le dan el alta al sum y tipo de evento
            funciones_db.Consultas_BD("INSERT INTO eventos (fecha, hora_ingreso,hora_egreso, id_tipo_evento, observaciones, coste) values ('" + fecha + "','" + hora_ingreso + "','" + hora_egreso + "',' 1' ,'" + observaciones + "','" + coste + "')");

            //Carga de locador 
            funciones_db.Consultas_BD("Insert into personas (nombre, apellido, dni, tipo_per) values ('" + nombre_locador + "','" + apellido_locador + "'," + dni + ", 6)");
            funciones_db.Consultas_BD("Insert into persona_evento (id_evento,id_persona) values (" + funciones_db.Chequeo_BD("select id_evento from eventos where id_evento = (select MAX (id_evento) from eventos)") + "," + funciones_db.Chequeo_BD("select id_persona from personas where id_persona = (select MAX (id_persona) from personas)") + ")");
            funciones_db.Consultas_BD("Insert into telefono (telefono,tipo_tel,id_persona) values ('" + telefono + "','" + tipo_tel + "'," + Convert.ToInt32(funciones_db.Chequeo_BD("select id_persona from personas where id_persona = (select MAX (id_persona) from personas)")) + ")");

            funciones_db.AbriCerrarBD(false);//Cerrar base
        }

        public DataTable lectura_sum(string fecha)
        {
            funciones_db.AbriCerrarBD(true);

            DataTable tabla_sum = new DataTable(); //Tabla general, todas las personas del evento
            //Tablas para los datos de las personas
            DataTable tabla_locador = new DataTable();
            DataTable tabla_tellocador = new DataTable();
            DataTable tabla_datosevento = new DataTable();
            string hora;

            int[] id_evento = receptor_id_evento(fecha);
            //Array que recibira todos los id_evento de los responsos

            //Creamos la estructura de la tabla_bautismos para que quede igual que el dgv de los bautismos
            string[] nombresColumnas = { "Nombre y apellido locador", "DNI", "Tel & tipo", "Coste" ,"Fecha uso", "Ingreso", "Egreso", "Observaciones"}; //Array que contiene los nombres de todas los columnas de el DataTable, este se utilizara para cargar las tablas en un bucle
            for (int i = 0; i < nombresColumnas.Length; i++)//Bucle que se utiliza para crear todas las columnas del DataTable con sus respectivos nombres dados por el array nombreColumnas
                tabla_sum.Columns.Add(nombresColumnas[i]);

            for (int i = 0; i < id_evento.Length; i++) //Se hace por la cantidad de responsos que haya en la fecha seleccionada
            {
                tabla_datosevento = funciones_db.Lectura_tabla_BD("SELECT fecha, hora_ingreso, hora_egreso,coste, observaciones from eventos WHERE id_evento IN (SELECT id_evento from persona_evento WHERE id_evento =" + id_evento[i] + ")");
                tabla_locador = funciones_db.Lectura_tabla_BD("SELECT nombre, apellido, dni FROM personas WHERE id_persona IN (SELECT id_persona FROM persona_evento WHERE id_evento =" + id_evento[i] + ") AND tipo_per = 6");
                tabla_tellocador = funciones_db.Lectura_tabla_BD("SELECT telefono,tipo_tel FROM telefono WHERE id_persona IN (SELECT id_persona FROM personas WHERE id_persona IN (SELECT id_persona FROM persona_evento WHERE id_evento =" + id_evento[i] + ") AND tipo_per = 6)");
                hora = funciones_db.Chequeo_BD("SELECT hora_ingreso FROM eventos WHERE id_evento =" + id_evento[i]);

                //En la siguiente seccion de cargara la tabla principal (tabla_responsos) con los datos de todas las personas y las fecha y hora del evento (esta manera de hacerlo depende del formato que tenga la grilla del form ya que no en todas tienen el mismos datos)
                tabla_sum.Rows.Add();//Se agrega una fila a tabla_responsos donde se cargara todos los datos del bautismo
                tabla_sum.Rows[i]["Nombre y apellido locador"] = tabla_locador.Rows[0]["nombre"].ToString() + " " + tabla_locador.Rows[0]["apellido"].ToString();
                tabla_sum.Rows[i]["DNI"] = tabla_locador.Rows[0]["dni"];
                tabla_sum.Rows[i]["Tel & tipo"] = tabla_tellocador.Rows[0]["telefono"].ToString() + " " + tabla_tellocador.Rows[0]["tipo_tel"].ToString();
                tabla_sum.Rows[i]["Fecha uso"] = tabla_datosevento.Rows[0]["fecha"];             
                tabla_sum.Rows[i]["Ingreso"] = tabla_datosevento.Rows[0]["hora_ingreso"];
                tabla_sum.Rows[i]["Egreso"] = tabla_datosevento.Rows[0]["hora_egreso"];
                tabla_sum.Rows[i]["Coste"] = tabla_datosevento.Rows[0]["coste"];
                tabla_sum.Rows[i]["Observaciones"] = tabla_datosevento.Rows[0]["observaciones"];
            }

            funciones_db.AbriCerrarBD(false);

            return tabla_sum;
        }

        private int[] receptor_id_evento(string fecha) //Metodo que obtiene todos los id_evento de los alquileres del sum para poder obtener todos los datos de esos eventos y luego cargar la dgv de los bautismos (es una de las utilizaciones del id_evento)
        {

            int[] id_evento;//Array que tendra cada uno de los id_evento que representa a los responsos que hay en la fecha seleccionada por el usuario en el formulario de los bautismos

            DataTable tablaDeID_eventos = funciones_db.Lectura_tabla_BD("select id_evento from eventos where fecha = '" + fecha + "' and id_tipo_evento = 1");//DataTable que recibira todos los id_eventos de los bautismos relacionados con la fecha seleccionada por el usuario en el calendario del formulario de SUM
            id_evento = new Int32[tablaDeID_eventos.Rows.Count];//Instanciomos el array que tendra todos los id_evento con la cantidad de eventos (identificados por su id_evento) que hay en la fecha que selecciono el usuario
            for (int i = 0; i < id_evento.Length; i++)//Bucle que nos sirvira para poder cargar todos los id_evento dentro del array id_evento
                id_evento[i] = Convert.ToInt32(tablaDeID_eventos.Rows[i]["id_evento"].ToString());//Carga cada posicion del array con el el id_evento de cada evento obteniendolo de cada fila dentro de la columna llamada id_evento

            return (id_evento);
        }

        public void act_sum(string fecha_vieja, string hora_vieja , string nombre_locador, string apellido_locador, int dni_locador, string telefono_locador, string tipo_tel, int coste_alquiler, string fecha_uso, string hora_ingreso, string hora_egreso, string observaciones)//Metodo para actualizar un registro del sum
        {
            funciones_db.AbriCerrarBD(true);

            int id_evento = Convert.ToInt32(funciones_db.Chequeo_BD("SELECT (id_evento) FROM eventos WHERE fecha='" + fecha_vieja + "' AND hora_ingreso='" + hora_vieja + "' AND id_tipo_evento= 1"));//En la variable id_evento guardamos el id_evento del evento seleccionado para modificar
            int id_persona = Convert.ToInt32(funciones_db.Chequeo_BD("SELECT id_persona FROM personas WHERE id_persona = (SELECT id_persona FROM persona_evento WHERE id_evento = "+id_evento+")"));//Conseguimos el id_persona de la persona a actualizar por medio del metodo, esto se hace para ademas de saber que persona actualizar para saber que telefono actualizar ya que estos dependen de la persona (La obtencion del id_persona de este modo solo funciona para una sola persona y no para varias, para el segundo caso se hace de la manera que esta establecida en el bautismo)

            funciones_db.Consultas_BD("UPDATE eventos SET fecha='" + fecha_uso + "', hora_ingreso= '" + hora_ingreso + "', hora_egreso ='" + hora_egreso + "', coste = "+ coste_alquiler + ", observaciones = '" + observaciones + "' WHERE id_evento=" + id_evento); // Ya con el id_evento podemos mandar el update a la tabla de evento actualizando los campos deseados al poder identificar el evento a actualizar con el id_evento
            funciones_db.Consultas_BD("UPDATE personas SET nombre='" + nombre_locador + "', apellido = '" + apellido_locador + "', dni = " + dni_locador + " WHERE id_persona = " + id_persona);//Actualizamos la persona sabiendo su id_persona para poder identificarla
            funciones_db.Consultas_BD("UPDATE telefono SET telefono ='" + telefono_locador + "', tipo_tel = '" + tipo_tel + "' WHERE id_persona = " + id_persona);//Actualizamos el telefono de la persona reconociendo a traves del id_persona

            funciones_db.AbriCerrarBD(false);
        }

        public void borrar_sum(string fecha, string hora)//Metodo utilizado para borrar un registro del sum, a traves de la fecha y hora del mismo
        {
            funciones_db.AbriCerrarBD(true);

            int id_evento = Convert.ToInt32(funciones_db.Chequeo_BD("SELECT (id_evento) FROM eventos WHERE fecha='" + fecha + "' AND hora_ingreso='" + hora + "' AND id_tipo_evento= 1"));//En la variable id_evento guardamos el id_evento del evento seleccionado para borrar, asi identificar cual borrar
            int id_persona = Convert.ToInt32(funciones_db.Chequeo_BD("SELECT id_persona FROM personas WHERE id_persona = (SELECT id_persona FROM persona_evento WHERE id_evento = " + id_evento+")"));//Conseguimos el id_persona de la persona a borrar por medio del metodo, esto se hace para ademas de saber que persona borrar para saber que telefono borrar ya que estos dependen de la persona (La obtencion del id_persona de este modo solo funciona para una sola persona y no para varias, para el segundo caso se hace de la manera que esta establecida en el bautismo)

            //Borramos el contenido de las tablas buscando el registro a borrar por medio de id_evento o el id_persona, borrando primeramente las tablas dependientes
            funciones_db.Consultas_BD("DELETE FROM persona_evento WHERE id_evento=" + id_evento + " AND id_persona=" + id_persona);
            funciones_db.Consultas_BD("DELETE FROM telefono WHERE id_persona = "+ id_persona);
            funciones_db.Consultas_BD("DELETE FROM personas WHERE id_persona=" + id_persona);
            funciones_db.Consultas_BD("DELETE FROM eventos WHERE id_evento=" + id_evento);

            funciones_db.AbriCerrarBD(false);
        }
    }
}
