using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Prj_Datos;

namespace Prj_Trabajo
{
    public class Consultas_Bodas
    {
        Trabajo_BD funciones_db = new Trabajo_BD();

        public void registro_bodas(string fecha_boda, string hora_boda, string nombre_novio, string nombre_novia, string apellido_novio, string apellida_novia , string nombre_padre, string lugar, string observaciones, string email, string telefono, string tipo_tel, string fecha_expediente, string tipo_expediente)
        {
            funciones_db.AbriCerrarBD(true);//Abrir base

            //Carga de fecha que le dan el alta a la boda y tipo de evento
            funciones_db.Consultas_BD("INSERT INTO eventos (fecha, lugar, hora_ingreso, observaciones, fecha_expediente, tipo_expediente, id_tipo_evento) values ('" + fecha_boda + "','" + lugar + "','" + hora_boda + "','" + observaciones + "','" + fecha_expediente + "','" + tipo_expediente + "',3)");

            //Carga de novios 

            //Novio
            funciones_db.Consultas_BD("Insert into personas (nombre, apellido,email , tipo_per) values ('" + nombre_novio + "','"+ apellido_novio + "','" + email + "', 12 )");
            funciones_db.Consultas_BD("Insert into persona_evento (id_evento,id_persona) values (" + funciones_db.Chequeo_BD("select id_evento from eventos where id_evento = (select MAX (id_evento) from eventos)") + "," + funciones_db.Chequeo_BD("select id_persona from personas where id_persona = (select MAX (id_persona) from personas)") + ")");
            funciones_db.Consultas_BD("Insert into telefono (telefono,tipo_tel,id_persona) values ('" + telefono + "','" + tipo_tel + "'," + Convert.ToInt32(funciones_db.Chequeo_BD("select id_persona from personas where id_persona = (select MAX (id_persona) from personas)")) + ")");

            //Novia
            funciones_db.Consultas_BD("Insert into personas (nombre, apellido, email, tipo_per) values ('" + nombre_novia + "','" + apellida_novia + "','" + email +"', 12 )");
            funciones_db.Consultas_BD("Insert into persona_evento (id_evento,id_persona) values (" + funciones_db.Chequeo_BD("select id_evento from eventos where id_evento = (select MAX (id_evento) from eventos)") + "," + funciones_db.Chequeo_BD("select id_persona from personas where id_persona = (select MAX (id_persona) from personas)") + ")");

            //Carga padre
            funciones_db.Consultas_BD("Insert into personas (nombre, tipo_per) values ('" + nombre_padre + "', 3 )");
            funciones_db.Consultas_BD("Insert into persona_evento (id_evento,id_persona) values (" + funciones_db.Chequeo_BD("select id_evento from eventos where id_evento = (select MAX (id_evento) from eventos)") + "," + funciones_db.Chequeo_BD("select id_persona from personas where id_persona = (select MAX (id_persona) from personas)") + ")");

            funciones_db.AbriCerrarBD(false);//Cerrar base
        }

        public DataTable lectura_bodas(string fecha)
        {
            funciones_db.AbriCerrarBD(true);

            DataTable tabla_boda = new DataTable(); //Tabla general, todas las personas del evento
            //Tablas para los datos de las personas
            DataTable tabla_novios = new DataTable();
            DataTable tabla_padre = new DataTable();
            DataTable tabla_datosevento = new DataTable();
            string hora;

            int[] id_evento = receptor_id_evento(fecha);
            //Array que recibira todos los id_evento de los responsos

            //Creamos la estructura de la tabla_bautismos para que quede igual que el dgv de los bautismos
            string[] nombresColumnas = { "Fecha", "Hora", "Novio", "Novia", "Fecha expediente", "Tipo expediente", "Lugar", "E-Mail", "Telefono", "Tipo tel", "Padre", "Observaciones" }; //Array que contiene los nombres de todas los columnas de el DataTable, este se utilizara para cargar las tablas en un bucle
            for (int i = 0; i < nombresColumnas.Length; i++)//Bucle que se utiliza para crear todas las columnas del DataTable con sus respectivos nombres dados por el array nombreColumnas
                tabla_boda.Columns.Add(nombresColumnas[i]);

            for (int i = 0; i < id_evento.Length; i++) //Se hace por la cantidad de responsos que haya en la fecha seleccionada
            {
                tabla_datosevento = funciones_db.Lectura_tabla_BD("SELECT fecha ,hora_ingreso ,observaciones,fecha_expediente,tipo_expediente,lugar from eventos WHERE id_evento IN (SELECT id_evento from persona_evento WHERE id_evento =" + id_evento[i] + ")");
                tabla_novios = funciones_db.Lectura_tabla_BD("SELECT nombre, apellido,email FROM personas WHERE id_persona IN (SELECT id_persona FROM persona_evento WHERE id_evento =" + id_evento[i] + ") AND tipo_per = 12");
                tabla_padre = funciones_db.Lectura_tabla_BD("SELECT nombre FROM personas WHERE id_persona IN (SELECT id_persona FROM persona_evento WHERE id_evento =" + id_evento[i] + ") AND tipo_per = 3");
                tabla_telefono = funciones_db.Lectura_tabla_BD("SELECT telefono,tipo_tel FROM telefono WHERE id_persona IN (SELECT id_persona FROM personas WHERE id_persona IN (SELECT id_persona FROM persona_evento WHERE id_evento =" + id_evento[i] + ") AND tipo_per = 12)");
                hora = funciones_db.Chequeo_BD("SELECT hora_ingreso FROM eventos WHERE id_evento =" + id_evento[i]);

                //En la siguiente seccion de cargara la tabla principal (tabla_responsos) con los datos de todas las personas y las fecha y hora del evento
                tabla_boda.Rows.Add();//Se agrega una fila a tabla_responsos donde se cargara todos los datos del bautismo
                tabla_boda.Rows[i]["Fecha"] = tabla_datosevento.Rows[0]["fecha"].ToString();
                tabla_boda.Rows[i]["Hora"] = tabla_datosevento.Rows[0]["hora_ingreso"].ToString();
                tabla_boda.Rows[i]["Novio"] = tabla_novios.Rows[0]["nombre"] + " " + tabla_novios.Rows[0]["apellido"];
                tabla_boda.Rows[i]["Novia"] = tabla_novios.Rows[1]["nombre"] + " " + tabla_novios.Rows[1]["apellido"];
                tabla_boda.Rows[i]["Fecha expediente"] = tabla_datosevento.Rows[0]["fecha_expediente"];
                tabla_boda.Rows[i]["Tipo expediente"] = tabla_datosevento.Rows[0]["tipo_expediente"];
                tabla_boda.Rows[i]["Lugar"] = tabla_datosevento.Rows[0]["lugar"];
                tabla_boda.Rows[i]["E-mail"] = tabla_novios.Rows[0]["email"];
                tabla_boda.Rows[i]["Telefono"] = tabla_telefono.Rows[0]["telefono"];
                tabla_boda.Rows[i]["Tipo tel"] = tabla_telefono.Rows[0]["tipo_tel"];
                tabla_boda.Rows[i]["Padre"] = tabla_padre.Rows[0]["nombre"];
                tabla_boda.Rows[i]["Observaciones"] = tabla_datosevento.Rows[0]["observaciones"];

            }

            funciones_db.AbriCerrarBD(false);

            return tabla_boda;
        }

        private int[] receptor_id_evento(string fecha) //Metodo que obtiene todos los id_evento de los bautismos para poder obtener todos los datos de esos eventos y luego cargar la dgv de los bautismos
        {

            int[] id_evento;//Array que tendra cada uno de los id_evento que representa a los responsos que hay en la fecha seleccionada por el usuario en el formulario de los bautismos

            DataTable tablaDeID_eventos = funciones_db.Lectura_tabla_BD("select id_evento from eventos where fecha = '" + fecha + "' and id_tipo_evento = 3");//DataTable que recibira todos los id_eventos de los bautismos relacionados con la fecha seleccionada por el usuario en el calendario del formulario de SUM
            id_evento = new Int32[tablaDeID_eventos.Rows.Count];//Instanciomos el array que tendra todos los id_evento con la cantidad de eventos (identificados por su id_evento) que hay en la fecha que selecciono el usuario
            for (int i = 0; i < id_evento.Length; i++)//Bucle que nos sirvira para poder cargar todos los id_evento dentro del array id_evento
                id_evento[i] = Convert.ToInt32(tablaDeID_eventos.Rows[i]["id_evento"].ToString());//Carga cada posicion del array con el el id_evento de cada evento obteniendolo de cada fila dentro de la columna llamada id_evento

            return (id_evento);
        }

        public DataTable tabla_telefono { get; set; }

        public void act_bodas(string fecha_vieja, string hora_vieja, string fecha_nueva, string hora_nueva, string nombre_novio, string nombre_novia, string apellido_novio, string apellida_novia, string nombre_padre, string lugar, string observaciones, string email, string telefono, string tipo_tel, string fecha_expediente, string tipo_expediente)
        {
            funciones_db.AbriCerrarBD(true); //Abrimos la base de datos para poder actualiza los datos

            int id_evento = Convert.ToInt32(funciones_db.Chequeo_BD("SELECT (id_evento) FROM eventos WHERE fecha='" + fecha_vieja + "' AND hora_ingreso='" + hora_vieja + "' AND id_tipo_evento= 3")); // Aca solo busca el id del evento teniendo en cuenta la fecha, la hora y el id_tipo_evento que pertenesca al evento que se desea actualizar.
            funciones_db.Consultas_BD("UPDATE eventos SET fecha='" + fecha_nueva + "', hora_ingreso= '" + hora_nueva + "' WHERE id_evento=" + id_evento); // Aca actualiza la tabla eventos con los datos nuevos ingresados por el usuario, verificando que el evento sea el mismo que el de id_evento lo cual fue obtenido anteriormente.

            int[] id_personas = obtencion_id_personas(id_evento); //Array que contendra todos los id de las personas que estan vinculada con el evento(papa, mama, etc)

            int id_novio = id_personas[0];//Cargamos una variable con el id_persona del responso, sabiendo que en la posicion 0 del array is_personas esta el id del responso
            int id_novia = id_personas[1];
            int id_padre = id_personas[2];

            funciones_db.Consultas_BD("UPDATE personas SET nombre = '" + nombre_novio + "' , apellido = '" + apellido_novio + "', email = '" + email + "' WHERE tipo_per = 12 AND id_persona = " + id_novio);//Actualiza los datos del bautizado con sus respectivos datos. Verificando que el tipo de persona se 7 (osea un bautizado) y que el id_persona sea uno de los id_persona que estan en el array id_personas
            funciones_db.Consultas_BD("UPDATE personas SET nombre = '" + nombre_novia + "' , apellido = '" + apellida_novia + "' ,email = '" + email + "' WHERE tipo_per = 12 AND id_persona = " + id_novia);//
            funciones_db.Consultas_BD("UPDATE telefono SET telefono ='" + telefono + "', tipo_tel = '" + tipo_tel + "' WHERE id_persona = " + id_novio);
            funciones_db.Consultas_BD("UPDATE personas SET nombre = '" + nombre_padre + "' WHERE tipo_per = 3 AND id_persona = " + id_padre);//
            funciones_db.Consultas_BD("UPDATE eventos  SET lugar = '" + lugar + "', observaciones = '" + observaciones + "' ,fecha_expediente = '" + fecha_expediente + "', tipo_expediente = '" + tipo_expediente + "' WHERE id_evento =" + id_evento);
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

        public void borrar_bodas(string fecha, string hora)//Metodo utilizado para borrar un registro del sum, a traves de la fecha y hora del mismo
        {
            funciones_db.AbriCerrarBD(true);

            int id_evento = Convert.ToInt32(funciones_db.Chequeo_BD("SELECT (id_evento) FROM eventos WHERE fecha='" + fecha + "' AND hora_ingreso='" + hora + "' AND id_tipo_evento= 3"));//En la variable id_evento guardamos el id_evento del evento seleccionado para borrar, asi identificar cual borrar
            int[] id_personas = obtencion_id_personas(id_evento); //Array que contendra todos los id de las personas que estan vinculada con el evento(papa, mama, etc)

            int id_novio = id_personas[0];//Cargamos una variable con el id_persona del responso, sabiendo que en la posicion 0 del array is_personas esta el id del responso
            int id_novia = id_personas[1];
            int id_padre = id_personas[2];

            //Borramos el contenido de las tablas buscando el registro a borrar por medio de id_evento o el id_persona, borrando primeramente las tablas dependientes
            funciones_db.Consultas_BD("DELETE FROM persona_evento WHERE id_evento=" + id_evento);
            funciones_db.Consultas_BD("DELETE FROM telefono WHERE id_persona = " + id_novio);
            funciones_db.Consultas_BD("DELETE FROM personas WHERE id_persona=" + id_novio);
            funciones_db.Consultas_BD("DELETE FROM personas WHERE id_persona=" + id_novia);
            funciones_db.Consultas_BD("DELETE FROM personas WHERE id_persona=" + id_padre);
            funciones_db.Consultas_BD("DELETE FROM eventos WHERE id_evento=" + id_evento);


            funciones_db.AbriCerrarBD(false);
        }
    }
}
