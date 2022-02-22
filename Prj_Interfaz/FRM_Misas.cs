using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Prj_Trabajo;
using Word = Microsoft.Office.Interop.Word;

namespace Prj_Interfaz
{
    public partial class FRM_Misas : Form
    {
        //Instancia de la clase Missing para representar valores perdidos,
        //por ejemplo, métodos que tienen valores de parámetros predeterminados
        object ObjMiss = System.Reflection.Missing.Value;

        //creamos el objeto Word
        Word.Application ObjWord = new Word.Application();
        string ruta = Application.StartupPath + @"\Impresion(Misas).docx";

        public FRM_Misas()
        {
            InitializeComponent();
        }
        string[] datos;
        string[] intencion_split;
        string intencion = "";
        Consultas_Misas Misas = new Consultas_Misas();
        private void Misas_Load(object sender, EventArgs e)
        {
            dgv_MIS.DataSource = Misas.lectura_misa(DateTime.Today.ToShortDateString());
            btn_MIS_modificar.Enabled = false;
            btn_MIS_borrar.Enabled = false;
            btn_MIS_imprimir.Enabled = false;
            dtp_MIS_fecha.Text = DateTime.Today.ToShortDateString();
        }
        private void numericUpDown1_KeyPress(object sender, KeyPressEventArgs e)
        {
      
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((cmb_MIS_hora.Text == "") || (cmb_MIS_minuto.Text == "") || (txt_MIS_nomPadre.Text == "") || (cmb_MIS_intencion.Text == ""))
            {
                MessageBox.Show("No puede dejar espacios vacios!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    if (cmb_MIS_intencion.Text == "Otros...") // Si el combobox de intencion es 'Otros', agrega una '|' para despues poder separarlo y para poder modificarlo mas adelante.
                    {
                        Misas.registro_misa(dtp_MIS_fecha.Text, (cmb_MIS_hora.Text + ":" + cmb_MIS_minuto.Text), (cmb_MIS_intencion.Text + " | " + txt_MIS_intencion.Text), txt_MIS_nomPadre.Text);
                        MessageBox.Show("Misa agregada correctamente!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarTodo();
                    }
                    else
                    {
                        Misas.registro_misa(dtp_MIS_fecha.Text, (cmb_MIS_hora.Text + ":" + cmb_MIS_minuto.Text), cmb_MIS_intencion.Text, txt_MIS_nomPadre.Text);
                        MessageBox.Show("Misa agregada correctamente!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarTodo();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al agregar la misa: " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cmb_intenciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            // NO USAR ESTE, NO SE POR QUE ESTA ESTO
        }
        private void LimpiarTodo()
        {
            cmb_MIS_hora.SelectedItem = null;
            cmb_MIS_minuto.SelectedItem = null;
            cmb_MIS_intencion.SelectedItem = null;
            txt_MIS_intencion.Text = "";
            txt_MIS_nomPadre.Text = "";
            btn_MIS_modificar.Enabled = false;
            btn_MIS_borrar.Enabled = false;
            btn_MIS_agregar.Enabled = true;
            btn_MIS_imprimir.Enabled = false;
            txt_MIS_intencion.Visible = false;
            dtp_MIS_fecha.Text = DateTime.Today.ToShortDateString();
            txt_MIS_nomPadre.Focus();
            dgv_MIS.Columns.Clear();
            dgv_MIS.DataSource = Misas.lectura_misa(mtc_MIS.SelectionStart.ToShortDateString());//Llamamos al metodo lectura_bautismos para que nos devuelva el DataTable con los datos necesarios para cargar el dgv
        }

        private void mtc_MIS_DateChanged(object sender, DateRangeEventArgs e)
        {
            LimpiarTodo();
        }
        private void dgv_MIS_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                datos = new string[dgv_MIS.Columns.Count];
                string f_h_m; // Este string se utiliza para guardar la fecha, la hora y los minutos para despues cargarlos en sus respectivos objetos para su posterior modificacion.
                for (int i = 0; i <= datos.Length - 1; i++)
                {
                    datos[i] = dgv_MIS.Rows[e.RowIndex].Cells[i].Value.ToString();
                }
                check = IsNumeric(datos[0].Substring(0, 2));
                if (check == true)
                {
                    f_h_m = datos[0].Substring(0, 10); // Este substring agarra la fecha de la celda.
                    dtp_MIS_fecha.Text = f_h_m;
                    f_h_m = datos[0].Substring(11, 2); // Este substring agarra la hora de la celda.
                    cmb_MIS_hora.Text = f_h_m;
                    f_h_m = datos[0].Substring(14, 2); // Este substring agarra los minutos de la celda.
                    cmb_MIS_minuto.Text = f_h_m;
                }
                else
                {
                    f_h_m = datos[0].Substring(0, 9); // Este substring agarra la fecha de la celda.
                    dtp_MIS_fecha.Text = f_h_m;
                    f_h_m = datos[0].Substring(10, 2); // Este substring agarra la hora de la celda.
                    cmb_MIS_hora.Text = f_h_m;
                    f_h_m = datos[0].Substring(13, 2); // Este substring agarra los minutos de la celda.
                    cmb_MIS_minuto.Text = f_h_m;
                }
                intencion = datos[1];
                if (intencion.Contains("Otros..."))
                {
                    cmb_MIS_intencion.Text = "Otros..."; // Lo que hace esta linea es usar el metodo Contains, osea que busca en el string 'intencion' el texto 'Otros...', y si lo encuentra, lo pone en el cmb.
                    intencion_split = new string[2]; // Se instancia el tamaño del string[] para poder hacer un split.
                    intencion_split = intencion.Split('|'); // Se hace un split del string intencion para poder separar 'Otros...' de lo que puso el usuario en el textbox para asi cargarlo en el mismo para mostrarlo al usuario y que lo pueda modificar.
                    txt_MIS_intencion.Text = intencion_split[1]; // Se carga el txt antes mencionado con el valor guardado del split.
                }
                else cmb_MIS_intencion.Text = datos[1].Trim(); // Si 'intencion' no contiene 'Otros...', carga el combobox de intencion con alguno de los otros 2 valores (se usa el metodo Trim para quitarle los espacios que posee para asi evitar posibles errores).

                txt_MIS_nomPadre.Text = datos[2];
                btn_MIS_modificar.Enabled = true;
                btn_MIS_borrar.Enabled = true;
                btn_MIS_agregar.Enabled = false;
                btn_MIS_imprimir.Enabled = true;
            }
            catch
            {
            }
        }
        public bool IsNumeric(string value) // Booleano que recive un string para ver si su contenido solo tiene numeros o no.
        {
            return value.All(char.IsNumber);
        }
        bool check;
        private void btn_MIS_modificar_Click(object sender, EventArgs e)
        {
            DialogResult pregunta = MessageBox.Show("Esta seguro que desea modificar el evento?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (pregunta == DialogResult.Yes)
            {
                if ((cmb_MIS_hora.Text == "") || (cmb_MIS_minuto.Text == "") || (txt_MIS_nomPadre.Text == "") || (cmb_MIS_intencion.Text == ""))
                {
                    MessageBox.Show("No puede dejar espacios vacios!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    check = IsNumeric(datos[0].Substring(0, 2));
                    try
                    {
                        if (cmb_MIS_intencion.Text == "Otros...")
                        {

                            if (check == true)
                            {
                                Misas.act_misa(datos[0].Substring(0, 10), datos[0].Substring(11, 5), dtp_MIS_fecha.Text, (cmb_MIS_hora.Text + ":" + cmb_MIS_minuto.Text), (cmb_MIS_intencion.Text + " | " + txt_MIS_intencion.Text), txt_MIS_nomPadre.Text);
                                MessageBox.Show("Misa modificada correctamente!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LimpiarTodo();
                            }
                            else
                            {
                                Misas.act_misa(datos[0].Substring(0, 9), datos[0].Substring(10, 5), dtp_MIS_fecha.Text, (cmb_MIS_hora.Text + ":" + cmb_MIS_minuto.Text), (cmb_MIS_intencion.Text + " | " + txt_MIS_intencion.Text), txt_MIS_nomPadre.Text);
                                MessageBox.Show("Misa modificada correctamente!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LimpiarTodo();
                            }
                        }
                        else
                        {
                            if (check == true)
                            {
                                Misas.act_misa(datos[0].Substring(0, 10), datos[0].Substring(11, 5), dtp_MIS_fecha.Text, (cmb_MIS_hora.Text + ":" + cmb_MIS_minuto.Text), cmb_MIS_intencion.Text, txt_MIS_nomPadre.Text);
                                MessageBox.Show("Misa modificada correctamente!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LimpiarTodo();
                            }
                            else
                            {
                                Misas.act_misa(datos[0].Substring(0, 9), datos[0].Substring(10, 5), dtp_MIS_fecha.Text, (cmb_MIS_hora.Text + ":" + cmb_MIS_minuto.Text), cmb_MIS_intencion.Text, txt_MIS_nomPadre.Text);
                                MessageBox.Show("Misa modificada correctamente!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LimpiarTodo();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al modificar la misa: " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btn_MIS_borrar_Click(object sender, EventArgs e)
        {
            DialogResult pregunta = MessageBox.Show("Esta seguro que desea eliminar el evento?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (pregunta == DialogResult.Yes)
            {

                try
                {
                    check = IsNumeric(datos[0].Substring(0, 2));
                    if (check == true)
                    {
                        Misas.borrar_misa(datos[0].Substring(0, 10), datos[0].Substring(11, 5));
                        MessageBox.Show("Misa eliminada correctamente!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarTodo();
                    }
                    else
                    {
                        Misas.borrar_misa(datos[0].Substring(0, 9), datos[0].Substring(10, 5));
                        MessageBox.Show("Misa eliminada correctamente!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarTodo();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar la misa: " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void cmb_MIS_intencion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_MIS_intencion.Text == "Otros...")
            {
                txt_MIS_intencion.Visible = true;
            }
            else txt_MIS_intencion.Visible = false;
        }

        private void btn_MIS_imprimir_Click(object sender, EventArgs e)
        {
            object ObjMiss = System.Reflection.Missing.Value;
            Word.Application ObjWord = new Word.Application();
            object parametro = ruta;
            object fecha = "fecha";
            object horario = "horario";
            object intencion = "intencion";
            Word.Document ObjDoc = ObjWord.Documents.Open(ref parametro, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss);
            Word.Range FECHA = ObjDoc.Bookmarks.get_Item(ref fecha).Range;
            FECHA.Text = dtp_MIS_fecha.Text;
            Word.Range HORARIO = ObjDoc.Bookmarks.get_Item(ref horario).Range;
            HORARIO.Text = cmb_MIS_hora.Text + ":" + cmb_MIS_minuto.Text;
            Word.Range INTENCION = ObjDoc.Bookmarks.get_Item(ref intencion).Range;
            if (cmb_MIS_intencion.Text.Contains("Otros..."))
            {
                INTENCION.Text = txt_MIS_intencion.Text;
            }
            else
            {
                INTENCION.Text = cmb_MIS_intencion.Text;
            }
            object rango1 = FECHA;
            object rango2 = HORARIO;
            object rango3 = INTENCION;
            ObjDoc.Bookmarks.Add("fecha", ref rango1);
            ObjDoc.Bookmarks.Add("horario", ref rango2);
            ObjDoc.Bookmarks.Add("Intencion", ref rango3);
            ObjWord.Visible = true;
        }

        private void txt_MIS_nomPadre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }
    }
}
