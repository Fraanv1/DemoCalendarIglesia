using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Prj_Datos;

namespace Prj_Trabajo
{
    public class Consultas_Principal
    {
        Trabajo_BD funciones_BD = new Trabajo_BD();

        public DataTable horasOcupadas(string fecha)//Metodo que devolvera una tabla que tendra las horas ocupadas por las actividades (marcando las celdas que señalan la hora y actividad con la palabra "ocupado"), para luego cargar el dgv que muestra las actividades que se realizaran en una fecha en especifica (dgv_PRI_colorActividad)
        {
            funciones_BD.AbriCerrarBD(true);
            DataTable tablaHorasOcupadas = new DataTable(); //Tabla que contendra todas las actividades marcando en que horarios estas se encuentran ocupadas
            int filaCargar = 0; //Entero que se utilizara para cargar todas la horas utilizadas de modo que se cargue desde la hora de inicio hasta la de finalizacion de cada uno de las actividades

            //Creamos la estrutura de la tablaHorasOcupada
            string[] nombreColumnas = {"Hora", "SUM", "Bautismo","Bodas","Comuniones","Misas","Responsos"};//Array que contiene todos los nombres de las columnas para luego poder crear las columnas dentro de un bucle
            for (int i = 0; i < nombreColumnas.Length; i++)//Bucle para poder agregar todas las columnas necesarias
                tablaHorasOcupadas.Columns.Add(nombreColumnas[i]);
            string[] horas = { "09:00", "09:30", "10:00", "10:30", "11:00", "11:30", "12:00", "12:30", "13:00", "13:30", "14:00", "14:30", "15:00", "15:30", "16:00", "16:30", "17:00", "17:30", "18:00", "18:30", "19:00", "19:30", "20:00", "20:30", "21:00", "21:30", "22:00", "22:30", "23:00", "23:30", "00:00", "00:30", "01:00", "01:30", "02:00", "02:30", "03:00", "03:30", "04:00", "04:30", "05:00", "05:30", "06:00", "06:30", "07:00","07:30","08:00","08:30" };//Array que tiene todas las horas. Si, soy re vago
            for (int i = 0; i < 48; i++)//Bucle para crear cada fila que corresponde a una hora en especifica
            {
                tablaHorasOcupadas.Rows.Add();
                tablaHorasOcupadas.Rows[i]["Hora"] = horas[i];
            }

            string haber, haber1, haber2, haber3;

            haber1 = tablaHorasOcupadas.Rows.Count.ToString();
            

            DataTable tabla_SUM = funciones_BD.Lectura_tabla_BD("SELECT hora_ingreso, hora_egreso FROM eventos WHERE fecha = '" + fecha + "' AND id_tipo_evento = 1");//Tabla donde se guardara todas las horas de ingreso y egreso del SUM de la fecha seleccionada por el usuario
            DataTable tabla_Bautismo = funciones_BD.Lectura_tabla_BD("SELECT hora_ingreso FROM eventos WHERE fecha = '" + fecha + "' AND id_tipo_evento = 2");//
            DataTable tabla_Boda = funciones_BD.Lectura_tabla_BD("SELECT hora_ingreso FROM eventos WHERE fecha = '" + fecha + "' AND id_tipo_evento = 3");//
            DataTable tabla_Misa = funciones_BD.Lectura_tabla_BD("SELECT hora_ingreso FROM eventos WHERE fecha = '" + fecha + "' AND id_tipo_evento = 4");//
            DataTable tabla_Responsos = funciones_BD.Lectura_tabla_BD("SELECT hora_ingreso FROM eventos WHERE fecha = '" + fecha + "' AND id_tipo_evento = 5");//
            DataTable tabla_Comuniones = funciones_BD.Lectura_tabla_BD("SELECT hora_ingreso FROM eventos WHERE fecha = '" + fecha + "' AND id_tipo_evento = 6");//

            haber3 = tabla_SUM.Rows.Count.ToString();

            for (int filasHoras = 0; filasHoras < tablaHorasOcupadas.Rows.Count; filasHoras++)//Bucle que servira para verificar las horas qeu son ocupadas por todas las actividades
            {
                for (int filasSUM = 0; filasSUM < tabla_SUM.Rows.Count; filasSUM++)//Bucle que recorre cada una de las horas de ingreso que fue reservado el SUM
                {
                    haber = Convert.ToString(tablaHorasOcupadas.Rows[filasHoras]["Hora"]);
                    haber2 = Convert.ToString(tabla_SUM.Rows[filasSUM]["hora_ingreso"]);
                    if (tablaHorasOcupadas.Rows[filasHoras]["Hora"].ToString() == tabla_SUM.Rows[filasSUM]["hora_ingreso"].ToString())//Bifurcacion que ocurre si se encontro una hora que sea una hora de ingreso del SUM dentro de la tabla con todas las horas 
                    {
                        filaCargar = filasHoras; //Se carga la variable con la posicion en donde se encuentra las hora de inicio de la actividad
                        for (; ; )//Este bucle infinito servira para poder cargar las horas ocupadas del SUM (que se encontrarian en la tablaHorasOcupadas) con la palabra ocupado
                        {
                            try
                            {
                                tablaHorasOcupadas.Rows[filaCargar]["SUM"] = "ocupado";//Carga la hora que se encuentra ocupada dentro de las columna donde se encuentran todas las horas del SUM
                                if (tablaHorasOcupadas.Rows[filaCargar]["Hora"].ToString() == tabla_SUM.Rows[filasSUM]["hora_egreso"].ToString())//Verificia que la hora que fue cargada como ocupada sea la misma que la hora de egreso, en caso positivo vuelve a la variable filaCargar a 0 para poder utilizarla despues y termina el bucle infinito
                                {
                                    filaCargar = 0;
                                    break;
                                }
                                filaCargar = filaCargar + 1;//Salta a las siguiente posicion de horas

                                haber = tablaHorasOcupadas.Rows[filaCargar]["Hora"].ToString();
                                haber2 = tabla_SUM.Rows[filasSUM]["hora_egreso"].ToString();  
                            }
                            catch //Falla cuando se intenta cargar una hora despues de las 8:30
                            {
                                filaCargar = 0;//Para que siga pintando lo que le falta se pono nuevamente que empiece a cargar nuevamente en la posicion 0 osea las 9:00
                            }
                        }
                    }
                }

                    haber = tabla_Bautismo.Rows.Count.ToString();
                    for (int filasBAU = 0; filasBAU < tabla_Bautismo.Rows.Count; filasBAU++)//Bucle para verificar cada una de las hora de ingreso en los bautismos
                    {
                        if (tablaHorasOcupadas.Rows[filasHoras]["Hora"].ToString() == tabla_Bautismo.Rows[filasBAU]["hora_ingreso"].ToString()) // Encontro una hora de ingreso
                        {

                            //Carga una hora como ocupada desde la hora de inicio del evento
                            try
                            {
                                tablaHorasOcupadas.Rows[filasHoras]["Bautismo"] = "ocupado";
                                tablaHorasOcupadas.Rows[filasHoras + 1]["Bautismo"] = "ocupado";
                                tablaHorasOcupadas.Rows[filasHoras + 2]["Bautismo"] = "ocupado";
                            }
                            catch // Si falla es porque se intento cargar una fila de la tablaHorasOcupadas que esta mas alla de su rango de filas
                            {
                            }
                        }
                    }
                    for (int filasBOD = 0; filasBOD < tabla_Boda.Rows.Count; filasBOD++)//Bucle para verificar cada una de las hora de ingreso en las bodas
                    {
                        if (tablaHorasOcupadas.Rows[filasHoras]["Hora"].ToString() == tabla_Boda.Rows[filasBOD]["hora_ingreso"].ToString())// Encontro una hora de ingreso
                        {
                            //Carga una hora como ocupada desde la hora de inicio del evento
                            try
                            {
                                tablaHorasOcupadas.Rows[filasHoras]["Bodas"] = "ocupado";
                                tablaHorasOcupadas.Rows[filasHoras + 1]["Bodas"] = "ocupado";
                                tablaHorasOcupadas.Rows[filasHoras + 2]["Bodas"] = "ocupado";
                            }
                            catch // Si falla es porque se intento cargar una fila de la tablaHorasOcupadas que esta mas alla de su rango de filas
                            {
                            }
                        }
                    }
                    for (int filasMIS = 0; filasMIS < tabla_Misa.Rows.Count; filasMIS++)
                    {
                        if (tablaHorasOcupadas.Rows[filasHoras]["Hora"].ToString() == tabla_Misa.Rows[filasMIS]["hora_ingreso"].ToString())// Encontro una hora de ingreso
                        {
                            //Carga una hora como ocupada desde la hora de inicio del evento
                            try
                            {
                                tablaHorasOcupadas.Rows[filasHoras]["Misas"] = "ocupado";
                                tablaHorasOcupadas.Rows[filasHoras + 1]["Misas"] = "ocupado";
                                tablaHorasOcupadas.Rows[filasHoras + 2]["Misas"] = "ocupado";
                            }
                            catch // Si falla es porque se intento cargar una fila de la tablaHorasOcupadas que esta mas alla de su rango de filas
                            {
                            }
                        }
                    }
                    for (int filasCOM = 0; filasCOM < tabla_Comuniones.Rows.Count; filasCOM++)
                    {
                        if (tablaHorasOcupadas.Rows[filasHoras]["Hora"].ToString() == tabla_Comuniones.Rows[filasCOM]["hora_ingreso"].ToString())// Encontro una hora de ingreso
                        {
                            //Carga una hora como ocupada desde la hora de inicio del evento
                            try
                            {
                                tablaHorasOcupadas.Rows[filasHoras]["Comuniones"] = "ocupado";
                                tablaHorasOcupadas.Rows[filasHoras + 1]["Comuniones"] = "ocupado";
                                tablaHorasOcupadas.Rows[filasHoras + 2]["Comuniones"] = "ocupado";
                            }
                            catch // Si falla es porque se intento cargar una fila de la tablaHorasOcupadas que esta mas alla de su rango de filas
                            {
                            }
                        }
                    }

                    for (int filasRES = 0; filasRES < tabla_Responsos.Rows.Count; filasRES++)
                    {
                        if (tablaHorasOcupadas.Rows[filasHoras]["Hora"].ToString() == tabla_Responsos.Rows[filasRES]["hora_ingreso"].ToString())// Encontro una hora de ingreso
                        {
                            //Carga una hora como ocupada desde la hora de inicio del evento
                            try
                            {
                                tablaHorasOcupadas.Rows[filasHoras]["Responsos"] = "ocupado";
                                tablaHorasOcupadas.Rows[filasHoras + 1]["Responsos"] = "ocupado";
                                tablaHorasOcupadas.Rows[filasHoras + 2]["Responsos"] = "ocupado";
                            }
                            catch // Si falla es porque se intento cargar una fila de la tablaHorasOcupadas que esta mas alla de su rango de filas
                            {
                            }
                        }
                    }
                }  

            funciones_BD.AbriCerrarBD(false);

            return tablaHorasOcupadas;
        }
    }
}
