using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Prj_Trabajo;

namespace Prj_Interfaz
{
    public partial class FRM_Principal : Form
    {
        // Declaracion de variables
        Consultas_Principal obtencionActividades = new Consultas_Principal();//Conexion con clase que nos permite saber las horas ocupadas en determinada fecha
        Consultas_SUM obtencionSum = new Consultas_SUM();
        Consultas_Bautismos obtencionBautismo = new Consultas_Bautismos();//Conexion con clase que nos dara los datos de los bautimos en determinada fecha
        Consultas_Bodas obtencionBodas = new Consultas_Bodas();
        Consultas_Comuniones obtencionComuniones = new Consultas_Comuniones();
        Consultas_Misas obtencionMisas = new Consultas_Misas();
        Consultas_Responsos obtencionResponsos = new Consultas_Responsos();

        public FRM_Principal()
        {
            InitializeComponent();
        }

        private void FRM_Principal_Load(object sender, EventArgs e)
        {
            crearTablaColores(); 
        }
        private void mtc_PRI_DateSelected(object sender, DateRangeEventArgs e)
        {
            crearTablaColores();
        }
        private void crearTablaColores()//Metodo que se encarga de cargar el dgv_PRI_colorActividad
        {
            dgv_PRI_detalleActividad.Columns.Clear();//Borra toda el dgv_PRI_detalleActividad ya que se cambio la fecha en el calendario
            lbl_fechaSeleccionada.Text = mtc_PRI.SelectionStart.ToShortDateString();//Cargamos en el label la fecha seleccionada

            dgv_PRI_colorActividad.Columns.Clear();
            dgv_PRI_colorActividad.DataSource = obtencionActividades.horasOcupadas(lbl_fechaSeleccionada.Text);//Se carga una tabla que tiene todas las hora, incluyendo a las ocupadas

            colorearActividadesOcupadas();
        }
        private void colorearActividadesOcupadas()//Metodo que se ocupara de colorear la dgv_PRI_colorActividad en las horas que se marcan como ocupadas con la palabra "ocupado"
        {
            for (int columna = 1; columna < dgv_PRI_colorActividad.Columns.Count; columna++)//Bucle que pasara por cada una de las columnas de dgv_PRI_colorActividad empezando con la columna del SUM
            {
                for (int fila = 0; fila < dgv_PRI_colorActividad.Rows.Count; fila++)//Bucle que pasara por cada una de las filas de dgv_PRI_colorActividad
                {
                    if (dgv_PRI_colorActividad[columna, fila].Value.ToString() == "ocupado")//Bifurcacion que surge cuando en la celda marcada por la columna y fila encontro la palabra ocupado
                    {
                        dgv_PRI_colorActividad[columna, fila].Value = "";//Quitamos la palabra ocupado por cuestiones de estetica

                        switch (columna)//Verica en que columna esta la celda a pintar para pintarlos de un color en especifico
                        {
                            case 1:
                                dgv_PRI_colorActividad[columna, fila].Style.BackColor = Color.Red;
                                break;
                            case 2:
                                dgv_PRI_colorActividad[columna, fila].Style.BackColor = Color.Orange;
                                break;
                            case 3:
                                dgv_PRI_colorActividad[columna, fila].Style.BackColor = Color.Blue;
                                break;
                            case 4:
                                dgv_PRI_colorActividad[columna, fila].Style.BackColor = Color.Yellow;
                                break;
                            case 5:
                                dgv_PRI_colorActividad[columna, fila].Style.BackColor = Color.DarkBlue;//No se sabe el color
                                break;
                            case 6:
                                dgv_PRI_colorActividad[columna, fila].Style.BackColor = Color.Green;
                                break;
                        }
                    }
                }
            }
        }
        private void dgv_principal_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int ubicacion = e.ColumnIndex;//Variable que contendra el indice de la columna que se selecciono, esto para saber de que actividad se desea tener mas detalle
            CargarDGVDetallado(ubicacion);
        }
        private void CargarDGVDetallado(int ubicacion)//Metodo que da estructura al dgv_PRI_detalleActividad segun el sector que selecciona en el dgv_PRI_colorActividad, haciendo que pueda mostrar datos de las diferentes actividades de la iglesia
        {
            dgv_PRI_detalleActividad.Columns.Clear();
            //dgv_PRI_detalleActividad.Rows.Clear();
            switch (ubicacion)
            {
                case 1://Entra en es bifurcacion cuando se selecciona la columna del SUM el dgv_PRI_colorActividad
                    dgv_PRI_detalleActividad.DataSource = obtencionSum.lectura_sum(lbl_fechaSeleccionada.Text);
                    break;
                case 2:// Tabla de bautismos
                    dgv_PRI_detalleActividad.DataSource = obtencionBautismo.lectura_bautismos(lbl_fechaSeleccionada.Text);//Obtiene un dataTable con todos los datos de los bautimos de la fecha seleccionada
                    break;
                case 3:// Tabla de bodas
                    dgv_PRI_detalleActividad.DataSource = obtencionBodas.lectura_bodas(lbl_fechaSeleccionada.Text);
                    break;
                case 4:// Tabla de comuniones
                    dgv_PRI_detalleActividad.DataSource = obtencionComuniones.lectura_comunion(lbl_fechaSeleccionada.Text);
                    break;
                case 5:// Tabla de misas
                    dgv_PRI_detalleActividad.DataSource = obtencionMisas.lectura_misa(lbl_fechaSeleccionada.Text);
                    break;
                case 6://Tabla de responsos
                    dgv_PRI_detalleActividad.DataSource = obtencionResponsos.lectura_responsos(lbl_fechaSeleccionada.Text);
                    break;
            }
        }      
            
        //Metodos para llamar al resto de formularios

        private void bautismosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FRM_Bautismo bautismos = new FRM_Bautismo();
            bautismos.ShowDialog();
        }

        private void bodasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FRM_Bodas bodas = new FRM_Bodas();
            bodas.ShowDialog();
        }

        private void comunionesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FRM_Comuniones comuniones = new FRM_Comuniones();
            comuniones.ShowDialog();
        }

        private void responsosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FRM_Responsos responsos = new FRM_Responsos();
            responsos.ShowDialog();
        }

        private void misasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FRM_Misas misas = new FRM_Misas();
            misas.ShowDialog();
        }

        private void agregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FRM_SUM sum = new FRM_SUM();
            sum.ShowDialog();
        }

        

    }
}
