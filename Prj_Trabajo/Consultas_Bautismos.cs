using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Prj_Datos;

namespace Prj_Trabajo
{
    public class Consultas_Bautismos
    {
        Trabajo_BD funciones_db = new Trabajo_BD();
        ConexionDB hola = new ConexionDB();

        public void registro_bautismo(string fecha, string hora, string nombre_pibe, string apellido_pibe, int dni_pibe, string lug_nac_pibe, string fecha_nac_pibe, string nombre_padre, string apellido_padre, string nacionalidad_padre, string nombre_madre, string apellido_madre, string nacionalidad_madre, string padrino1, string padrino2, string madrina1, string madrina2, string padre_bautizador)
        {
            funciones_db.AbriCerrarBD(true);
            // En esta sentencia se guardan los datos del bautismo en la DB.
            funciones_db.Consultas_BD("Insert into eventos (fecha, hora_ingreso,id_tipo_evento) values ('" + fecha + "','" + hora + "',2)");

            // En esta sentencia solo guarda los datos del bautizado en la DB
            funciones_db.Consultas_BD("Insert into personas (nombre,apellido,dni,tipo_per,lug_nac,fecha_nac) values ('" + nombre_pibe + "','" + apellido_pibe + "'," + dni_pibe + ",7,'" + lug_nac_pibe + "','" + fecha_nac_pibe + "')");
            funciones_db.Consultas_BD("Insert into persona_evento (id_evento,id_persona) values (" + funciones_db.Chequeo_BD("select id_evento from eventos where id_evento = (select MAX (id_evento) from eventos)") + "," + funciones_db.Chequeo_BD("select id_persona from personas where id_persona = (select MAX (id_persona) from personas)") + ")");  

            // En estas sentencias se guardan los datos de los padres del bautizado, los padrinos y el padre que bautizo al niño.
            funciones_db.Consultas_BD("Insert into personas (nombre, apellido,nacionalidad,tipo_per) values ('" + nombre_padre + "','"+ apellido_padre+ "','" + nacionalidad_padre + "',1)");
            funciones_db.Consultas_BD("Insert into persona_evento (id_evento,id_persona) values (" + Convert.ToInt32(funciones_db.Chequeo_BD("select id_evento from eventos where id_evento = (select MAX (id_evento) from eventos)")) + "," + Convert.ToInt32(funciones_db.Chequeo_BD("select id_persona from personas where id_persona = (select MAX (id_persona) from personas)")) + ")");  
            funciones_db.Consultas_BD("Insert into personas (nombre, apellido,nacionalidad,tipo_per) values ('" + nombre_madre + "','" + apellido_madre + "','" + nacionalidad_madre + "',2)");
            funciones_db.Consultas_BD("Insert into persona_evento (id_evento,id_persona) values (" + Convert.ToInt32(funciones_db.Chequeo_BD("select id_evento from eventos where id_evento = (select MAX (id_evento) from eventos)")) + "," + Convert.ToInt32(funciones_db.Chequeo_BD("select id_persona from personas where id_persona = (select MAX (id_persona) from personas)")) + ")");  
            funciones_db.Consultas_BD("Insert into personas (nombre,tipo_per) values ('" + padre_bautizador + "',3)");
            funciones_db.Consultas_BD("Insert into persona_evento (id_evento,id_persona) values (" + Convert.ToInt32(funciones_db.Chequeo_BD("select id_evento from eventos where id_evento = (select MAX (id_evento) from eventos)")) + "," + Convert.ToInt32(funciones_db.Chequeo_BD("select id_persona from personas where id_persona = (select MAX (id_persona) from personas)")) + ")");  
            funciones_db.Consultas_BD("Insert into personas (nombre,tipo_per) values ('" + padrino1 + "',4)");
            funciones_db.Consultas_BD("Insert into persona_evento (id_evento,id_persona) values (" + Convert.ToInt32(funciones_db.Chequeo_BD("select id_evento from eventos where id_evento = (select MAX (id_evento) from eventos)")) + "," + Convert.ToInt32(funciones_db.Chequeo_BD("select id_persona from personas where id_persona = (select MAX (id_persona) from personas)")) + ")");  
            funciones_db.Consultas_BD("Insert into personas (nombre,tipo_per) values ('" + padrino2 + "',4)");
            funciones_db.Consultas_BD("Insert into persona_evento (id_evento,id_persona) values (" + Convert.ToInt32(funciones_db.Chequeo_BD("select id_evento from eventos where id_evento = (select MAX (id_evento) from eventos)")) + "," + Convert.ToInt32(funciones_db.Chequeo_BD("select id_persona from personas where id_persona = (select MAX (id_persona) from personas)")) + ")");  
            funciones_db.Consultas_BD("Insert into personas (nombre,tipo_per) values ('" + madrina1 + "',5)");
            funciones_db.Consultas_BD("Insert into persona_evento (id_evento,id_persona) values (" + Convert.ToInt32(funciones_db.Chequeo_BD("select id_evento from eventos where id_evento = (select MAX (id_evento) from eventos)")) + "," + Convert.ToInt32(funciones_db.Chequeo_BD("select id_persona from personas where id_persona = (select MAX (id_persona) from personas)")) + ")");  
            funciones_db.Consultas_BD("Insert into personas (nombre,tipo_per) values ('" + madrina2 + "',5)");
            funciones_db.Consultas_BD("Insert into persona_evento (id_evento,id_persona) values (" + Convert.ToInt32(funciones_db.Chequeo_BD("select id_evento from eventos where id_evento = (select MAX (id_evento) from eventos)")) + "," + Convert.ToInt32(funciones_db.Chequeo_BD("select id_persona from personas where id_persona = (select MAX (id_persona) from personas)")) + ")");

            funciones_db.AbriCerrarBD(false);
            // 7 bautizado; 1 padre; 2 madre, 3 cura; 4 padrino; 5 madrina 
        }
        public DataTable lectura_bautismos(string fecha)//Metodo que generara un DataTable que sera recibido por el dgv de bautismos, por lo mismo el DataTable debera traer los datos necesarios para cargar el dgv
        {
            funciones_db.AbriCerrarBD(true);

            DataTable tabla_bautismos = new DataTable();//Tabla general, donde se guardaran todos los datos tanto de las personas como del evento
            // Las siguientes tablas tendran los datos de cada una de las personas vinculadas con el bautismo 
            DataTable tabla_bautizado = new DataTable();
            DataTable tabla_papa = new DataTable();
            DataTable tabla_mama = new DataTable();
            DataTable tabla_padre = new DataTable();
            DataTable tabla_padrinos = new DataTable();
            DataTable tabla_madrinas = new DataTable();

            string hora;// Esta variable contendra las horas en las cuales se realizan los bautismos

            int[] id_evento = receptor_id_evento(fecha);//Array que recibira todos los id_evento de los bautismos que se relacionan con los bautismos

            //Creamos la estructura de la tabla_bautismos para que quede igual que el dgv de los bautismos
            string[] nombresColumnas = { "Fecha","Hora", "Nombre y apellido", "DNI", "Lugar de nacimiento", "Fecha de nacimiento", "Nombre y apellido papá", "Nacionalidad papá", "Nombre y apellido mamá", "Nacionalidad mamá", "Padrino Nº1", "Padrino Nº2", "Madrina Nº1", "Madrina Nº2", "Padre" }; //Arrat que contiene los nombres de todas los columnas de el DataTable, este se utilizara para cargar las tablas en un bucle
            for (int i = 0; i < nombresColumnas.Length; i++)//Bucle que se utiliza para crear todas las columnas del DataTable con sus respectivos nombres dados por el array nombreColumnas
                tabla_bautismos.Columns.Add(nombresColumnas[i]);

            for (int i = 0; i < id_evento.Length; i++) //Se hace por la cantidad de bautismos que haya en la fecha seleccionada
            {
                //El bucle se encarga de cargar cada tabla de las personas y hora que se relacionan con el bautismo el cual se desean los datos. Pero esto lo hace por cada evento del bautismo que hay en la fecha donde cada iteracion representa un bautismo dentro de la fecha
                tabla_bautizado = funciones_db.Lectura_tabla_BD("SELECT nombre, apellido, dni, lug_nac, fecha_nac FROM personas WHERE id_persona IN (SELECT id_persona FROM persona_evento WHERE id_evento =" + id_evento[i] + ") AND tipo_per = 7");//Carga en la tabla_bautizado los datos del bautizado buscandolo dentro de la tabla personas por medio del id_persona el cual es comparado con los id_persona que se encuentra en la tabla persona_evento  que tengan como id_evento el mismo id_evento que tiene el bautismo. Ademas de lo anterior verifica el tipo de persona para verificar de todos los id_persona que se obtuvo cual el del bautizado
                tabla_papa = funciones_db.Lectura_tabla_BD("SELECT nombre, apellido, nacionalidad FROM personas WHERE id_persona IN (SELECT id_persona FROM persona_evento WHERE id_evento =" + id_evento[i] + ") AND tipo_per = 1");//
                tabla_mama = funciones_db.Lectura_tabla_BD("SELECT nombre, apellido, nacionalidad FROM personas WHERE id_persona IN (SELECT id_persona FROM persona_evento WHERE id_evento =" + id_evento[i] + ") AND tipo_per = 2");//
                tabla_padrinos = funciones_db.Lectura_tabla_BD("SELECT nombre, apellido FROM personas WHERE id_persona IN (SELECT id_persona FROM persona_evento WHERE id_evento =" + id_evento[i] + ") AND tipo_per = 4");//
                tabla_madrinas = funciones_db.Lectura_tabla_BD("SELECT nombre, apellido FROM personas WHERE id_persona IN (SELECT id_persona FROM persona_evento WHERE id_evento =" + id_evento[i] + ") AND tipo_per = 5");//
                tabla_padre = funciones_db.Lectura_tabla_BD("SELECT nombre, apellido FROM personas WHERE id_persona IN (SELECT id_persona FROM persona_evento WHERE id_evento =" + id_evento[i] + ") AND tipo_per = 3");//
                hora = funciones_db.Chequeo_BD("SELECT hora_ingreso FROM eventos WHERE id_evento =" + id_evento[i]); //Obtiene la fecha de la tabla eventos verificando 

                //En la siguiente seccion de cargara la tabla principal (tabla_bautismos) con los datos de todas las personas y las fecha y hora del evento
                tabla_bautismos.Rows.Add();//Se agrega una fila a tabla_bautismos donde se cargara todos los datos del bautismo
                tabla_bautismos.Rows[i]["Fecha"] = fecha;
                tabla_bautismos.Rows[i]["Hora"] = hora;
                tabla_bautismos.Rows[i]["Nombre y apellido"] = tabla_bautizado.Rows[0]["nombre"] + " " + tabla_bautizado.Rows[0]["apellido"];//Carga el nombre y apellido en la fila que este marcando i y el la columna "Nombre y apellido", obteniendo los datos desde la tabla_bautizado exactamente desde la fila 0 (la cual es la unica fila) y la columnas "nombre" y "apellido" los cuales tienen ese nombre ya que es el mismo que fue dado desde la base de datos a las tablas que sirven como fuente a la tabla_bautizado
                tabla_bautismos.Rows[i]["DNI"] = tabla_bautizado.Rows[0]["dni"];
                tabla_bautismos.Rows[i]["Lugar de nacimiento"] = tabla_bautizado.Rows[0]["lug_nac"];//
                tabla_bautismos.Rows[i]["Fecha de nacimiento"] = tabla_bautizado.Rows[0]["fecha_nac"];//
                tabla_bautismos.Rows[i]["Nombre y apellido papá"] = tabla_papa.Rows[0]["nombre"] + " " + tabla_papa.Rows[0]["apellido"];//
               tabla_bautismos.Rows[i]["Nacionalidad papá"] = tabla_papa.Rows[0]["nacionalidad"];//
                tabla_bautismos.Rows[i]["Nombre y apellido mamá"] = tabla_mama.Rows[0]["nombre"] + " " + tabla_mama.Rows[0]["apellido"];//
                tabla_bautismos.Rows[i]["Nacionalidad mamá"] = tabla_mama.Rows[0]["nacionalidad"];//
                //Las tablas de padrinos y madrinas tienen ciertas diferencias, ya que las tablas tiene en cada fila a un padrino o madrina diferente, por lo tanto cuando se desea cargar a un padrino o madrina difetente se cambia de fila eligiendo la fila 0 o la 1
                tabla_bautismos.Rows[i]["Padrino Nº1"] = tabla_padrinos.Rows[0]["nombre"]+" "+tabla_padrinos.Rows[0]["apellido"];
                tabla_bautismos.Rows[i]["Padrino Nº2"] = tabla_padrinos.Rows[1]["nombre"]+" "+tabla_padrinos.Rows[1]["apellido"];
                tabla_bautismos.Rows[i]["Madrina Nº1"] = tabla_madrinas.Rows[0]["nombre"]+ "" + tabla_madrinas.Rows[0]["apellido"];
                tabla_bautismos.Rows[i]["Madrina Nº2"] = tabla_madrinas.Rows[1]["nombre"] + "" + tabla_madrinas.Rows[1]["apellido"];

                tabla_bautismos.Rows[i]["Padre"] = tabla_padre.Rows[0]["nombre"] + " " + tabla_padre.Rows[0]["apellido"];//Se carga en la columna "Padre" de la tabla_bautismos el nombre y apellido del padre

                
            }
            funciones_db.AbriCerrarBD(false);
           
            return tabla_bautismos;
        }
        private int[] receptor_id_evento(string fecha) //Metodo que obtiene todos los id_evento de los bautismos para poder obtener todos los datos de esos eventos y luego cargar la dgv de los bautismos
        {
            int[] id_evento;//Array que tendra cada uno de los id_evento que representa a los bautismos que hay en la fecha seleccionada por el usuario en el formulario de los bautismos

            DataTable tablaDeID_eventos = funciones_db.Lectura_tabla_BD("select id_evento from eventos where fecha = '"+ fecha + "' and id_tipo_evento = 2");//DataTable que recibira todos los id_eventos de los bautismos relacionados con la fecha seleccionada por el usuario en el calendario del formulario de bautismos
            id_evento = new Int32[tablaDeID_eventos.Rows.Count];//Instanciomos el array que tendra todos los id_evento con la cantidad de eventos (identificados por su id_evento) que hay en la fecha que selecciono el usuario
            for (int i = 0; i < id_evento.Length; i++)//Bucle que nos sirvira para poder cargar todos los id_evento dentro del array id_evento
                id_evento[i] = Convert.ToInt32(tablaDeID_eventos.Rows[i]["id_evento"].ToString());//Carga cada posicion del array con el el id_evento de cada evento obteniendolo de cada fila dentro de la columna llamada id_evento
 
            return (id_evento);
        }
        public void act_bautismo(string fecha_vieja, string hora_vieja, string fecha_nueva, string hora_nueva, string nombre_bautizado, string apellido_bautizado, int dni_bautizado, string lug_nac, string fecha_nac, string nombre_papa, string apellido_papa, string nacionalidad_papa, string nombre_mama, string apellido_mama, string nacionalidad_mama, string nombre_padrino1, string nombre_padrino2, string nombre_madrina1, string nombre_madrina2, string nombre_cura)
        {
            ///<Explicacion del metodo>
            ///Este metodo se encargar de actualizar la base de datos con lo datos ingresado por el usuario en el form de la actividad. 
            ///Primeramente busca el id_evento del evento que se desea cambiar, esto se consigue con una consulta en la base de datos
            ///trajendo en id_evento que tenga la misma fecha y hora que fecha_vieja y hora_nueva ademas de tener como id_tipo_evento
            ///el id_tipo_evento correspondiente al evento que se desea actualizar.
            ///Una vez que se lo obtiene se actualiza el evento con lo datos necesarios para ese evento (datos obtenidos por los parametros
            ///que recibe el metodo usado actualmente), luego de eso con el id_evento se verifica la tabla de la base de datos persona_evento
            ///para saber que personas estan vinculadas con el evento modificado relacionando todos los id_persona con el id_evento obtenido
            ///cuando ya sabemos que personas pertenecen a ese evento las actualizamos con los datos que se obtienen con los parametros, pero
            ///para saber a que persona debemos deben recibir ciertos parametros (ya que por ejemplo los datos del bautisado no son los mismo
            ///que los del papa) utilizamos el id_tipo_persona de cada persona ya que este nos dice que persona es cada una, por lo tanto podemos
            ///identificarlas para luego cargarles lo que corresponda.
            ///La diferencia entre fecha vieja y fecha nueva es que la vieja es la fecha que se desea cambiar (no es obligatorio que lo cambie)
            ///y la nueva es la que introduce el usuario para cambiar la anterior, todo esto tambien pasa con la hora
            ///</Explicacion del metodo>

            funciones_db.AbriCerrarBD(true); //Abrimos la base de datos para poder actualiza los datos

            int id_evento = Convert.ToInt32(funciones_db.Chequeo_BD("SELECT (id_evento) FROM eventos WHERE fecha='" + fecha_vieja + "' AND hora_ingreso='" + hora_vieja + "' AND id_tipo_evento=2")); // Aca solo busca el id del evento teniendo en cuenta la fecha, la hora y el id_tipo_evento que pertenesca al evento que se desea actualizar.
            funciones_db.Consultas_BD("UPDATE eventos SET fecha='" + fecha_nueva + "', hora_ingreso= '" + hora_nueva +"' WHERE id_evento=" + id_evento); // Aca actualiza la tabla eventos con los datos nuevos ingresados por el usuario, verificando que el evento sea el mismo que el de id_evento lo cual fue obtenido anteriormente.

            int[] id_personas = obtencion_id_personas(id_evento); //Array que contendra todos los id de las personas que estan vinculada con el evento(papa, mama, etc)

            int id_bautizado = id_personas[0];//Cargamos una variable con el id_persona del bautisado, sabiendo que en la posicion 0 del array is_personas esta el id del bautizado
            int id_papa = id_personas[1];
            int id_mama = id_personas[2];
            int id_cura = id_personas[3];
            int[] id_padrinos = { id_personas[4], id_personas[5] };//Este array contendra los id_persona de cada padrino, y se selecciona la posicion 4 y 5 del array id_personas ya que es esas posiciones se encuentran los id_persona de los padrinos
            int[] id_madrinas = { id_personas[6], id_personas[7] };//


            funciones_db.Consultas_BD("UPDATE personas SET nombre = '" + nombre_bautizado + "' , apellido = '" + apellido_bautizado + "' , lug_nac = '" + lug_nac + "' , fecha_nac = '" + fecha_nac + "' WHERE tipo_per = 7 AND id_persona = "+ id_bautizado);//Actualiza los datos del bautizado con sus respectivos datos. Verificando que el tipo de persona se 7 (osea un bautizado) y que el id_persona sea uno de los id_persona que estan en el array id_personas
            funciones_db.Consultas_BD("UPDATE personas SET nombre = '" + nombre_papa + "' , apellido = '" + apellido_papa +"' WHERE tipo_per = 1 AND id_persona = " + id_papa );//
            funciones_db.Consultas_BD("UPDATE personas SET nombre = '" + nombre_mama + "' , apellido = '" + apellido_mama + "' WHERE tipo_per = 2 AND id_persona = " + id_mama );//
            funciones_db.Consultas_BD("UPDATE personas SET nombre = '" + nombre_padrino1 +"' WHERE tipo_per = 4 AND id_persona = " + id_padrinos[0] );//A diferencia de los anteriores aca es diferente la cargar ya que hay 2 padrinos diferente, por lo mismo se separa los id de cada padrino en el array id_padrinos para poder actualizar los datos del padrino a partir de saber su id
            funciones_db.Consultas_BD("UPDATE personas SET nombre = '" + nombre_padrino2 + "' WHERE tipo_per = 4 AND id_persona = " + id_padrinos[1]);//
            funciones_db.Consultas_BD("UPDATE personas SET nombre = '" + nombre_madrina1 + "' WHERE tipo_per = 5 AND id_persona = " + id_madrinas[0]);//
            funciones_db.Consultas_BD("UPDATE personas SET nombre = '" + nombre_madrina2 + "' WHERE tipo_per = 5 AND id_persona = " + id_madrinas[1]);//
            funciones_db.Consultas_BD("UPDATE personas SET nombre = '" + nombre_cura + "' WHERE tipo_per = 3 AND id_persona = " + id_cura );//Y por ultimo actualizamos los datos del cura

            funciones_db.AbriCerrarBD(false); //Cerramos la coneccion de la base de datos
        }
        private int[] obtencion_id_personas (int id_evento)//Este metodo nos dara todos los id_evento de las personas que pertencen al evento, todo en un array
        {
            int[] id_personas = new int [8];//Array que contendran todos los id_persona de cada persona. Y se instancia segun la cantidad de personas que haya en el evento para que el array tenga espacio para cargar cada uno de los id_persona

            DataTable tabla_id_personas = new DataTable(); //Esta tabla recibira todos los id_persona de la consulta a la base de datos, para que posteriormente sea almacenados en el array id_personas
            tabla_id_personas = funciones_db.Lectura_tabla_BD("SELECT id_persona FROM persona_evento WHERE id_evento = " + id_evento);// recibe todos los id_persona de la tabla persona_evento los cuales tengan el mismo id_evento que el evento que se modifico

            for (int i = 0; i < tabla_id_personas.Rows.Count; i++)//Bucle que cargar el array con cada id_persona, y que se detiene cuando llega al limite de fila que tiene la tabla tabla_id_personas ya que cada fila representa un id_persona de cada persona
                id_personas[i] = Convert.ToInt32(tabla_id_personas.Rows[i]["id_persona"].ToString());//Carga el valor de cada fila de la tabla a cada posicion del array

            return id_personas;
        }

        public void borrar_bautismo (string fecha, string hora)
        {
            /// <Explicacion del metodo>
            /// El metodo es similar al metodo de actualizacion ya que primeramente se debe obtener el id_evento del evento que se desea borra,
            /// y para esto se utiliza la fecha, hora y el tipo_evento (que es el numero que identifica que tipo de evento es). La hora y fecha
            /// son dadas por los parametro del metodo pero el tipo_evento se pone manualmente segun el evento que se desea borrar.
            /// Una vez obtenido el id_evento se busca en la tabla persona_evento todos los id_persona que tengan relacion con el id_evento que
            /// se obtuvo, ya con todos esos datos se puede borrar todo lo que este relacionado con el id_evento y los id_persona, tanto el la
            /// tabla eventos, la tabla personas y en la tabla persona_evento
            /// </>

            funciones_db.AbriCerrarBD(true);//Abrimos la base de datos

            int id_evento = Convert.ToInt32(funciones_db.Chequeo_BD("SELECT (id_evento) FROM eventos WHERE fecha='" + fecha + "' AND hora_ingreso='" + hora + "' AND id_tipo_evento = 2")); // Aca solo busca el id del evento teniendo en cuenta la fecha, la hora y el id_tipo_evento que pertenesca al evento que se desea borrar.
            int[] id_personas = obtencion_id_personas(id_evento);//Array que contendra todos los id de las personas que estan vinculada con el evento(papa, mama, etc)
            int id_bautizado = id_personas[0];//Cargamos una variable con el id_persona del bautisado, sabiendo que en la posicion 0 del array is_personas esta el id del bautizado
            int id_papa = id_personas[1];
            int id_mama = id_personas[2];
            int id_cura = id_personas[3];
            int[] id_padrinos = { id_personas[4], id_personas[5] };//Este array contendra los id_persona de cada padrino, y se selecciona la posicion 4 y 5 del array id_personas ya que es esas posiciones se encuentran los id_persona de los padrinos
            int[] id_madrinas = { id_personas[6], id_personas[7] };//

            funciones_db.Consultas_BD("DELETE FROM persona_evento WHERE id_evento = " + id_evento);//Borramos todos lo registros de la tabla persona_evento que tengan relacion con el evento que se desea borrar
            funciones_db.Consultas_BD("DELETE FROM eventos WHERE id_evento = " + id_evento);//Borramos el evento a traves de su id_evento

            //Borrado de las personas segun su id_persona
            funciones_db.Consultas_BD("DELETE FROM personas WHERE id_persona = " + id_bautizado);
            funciones_db.Consultas_BD("DELETE FROM personas WHERE id_persona = " + id_papa);
            funciones_db.Consultas_BD("DELETE FROM personas WHERE id_persona = " + id_mama);
            funciones_db.Consultas_BD("DELETE FROM personas WHERE id_persona = " + id_cura);
            funciones_db.Consultas_BD("DELETE FROM personas WHERE id_persona = " + id_padrinos[0]);
            funciones_db.Consultas_BD("DELETE FROM personas WHERE id_persona = " + id_padrinos[1]);
            funciones_db.Consultas_BD("DELETE FROM personas WHERE id_persona = " + id_madrinas[0]);
            funciones_db.Consultas_BD("DELETE FROM personas WHERE id_persona = " + id_madrinas[1]);

            funciones_db.AbriCerrarBD(false);//Cerramos la base de datos
        }
    }
}
