using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Prj_Trabajo;
//using Word = Microsoft.Office.Interop.Word;

namespace Prj_Interfaz
{
    public partial class FRM_Comuniones : Form
    {
        Consultas_Comuniones Comuniones = new Consultas_Comuniones();
        public FRM_Comuniones()
        {
            InitializeComponent();
        }
        public string[] datos;
        bool check;
        //Instancia de la clase Missing para representar valores perdidos,
        //por ejemplo, métodos que tienen valores de parámetros predeterminados
        object ObjMiss = System.Reflection.Missing.Value;

        //creamos el objeto Word
        Word.Application ObjWord = new Word.Application();
        string ruta = Application.StartupPath + @"\Impresion(Comuniones).docx";
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult pregunta = MessageBox.Show("Esta seguro que desea modificar el evento?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (pregunta == DialogResult.Yes)
            {
                try
                {
                    // Abajo se carga el bool check con el valor de datos en posicion 0 y solo 2 lugares, para comprobar si es numerico (ya que de windows 7 en adelante los dias menores al 9 se guardan sin el 0 al principio). Si lo es, modifica con una posicion, si no, con otra posicion (lo mismo ocurre con los demas llamados iguales).
                    check = IsNumeric(datos[0].Substring(0, 2));
                    if (check == true)
                    {
                        Comuniones.act_comunion(datos[0].Substring(0, 10), datos[0].Substring(11, 5), dtp_COM_fecha.Text, (cmb_COM_hora.Text + ":" + cmb_COM_minuto.Text), txt_COM_nomCatesista.Text, txt_COM_grupo.Text, Convert.ToInt32(txt_COM_cantChicos.Text), txt_COM_nomPadre.Text);
                        
                    }
                    else
                    {
                        Comuniones.act_comunion(datos[0].Substring(0, 9), datos[0].Substring(10, 5), dtp_COM_fecha.Text, (cmb_COM_hora.Text + ":" + cmb_COM_minuto.Text), txt_COM_nomCatesista.Text, txt_COM_grupo.Text, Convert.ToInt32(txt_COM_cantChicos.Text), txt_COM_nomPadre.Text);
                    }
                    MessageBox.Show("Comunion modificada correctamente!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarTodo();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dgv_COM.DataSource = Comuniones.lectura_comunion(DateTime.Today.ToShortDateString());
            btn_COM_borrar.Enabled = false;
            btn_COM_modificar.Enabled = false;
            btn_COM_imprimir.Enabled = false;
        }

        public bool IsNumeric(string value) // Booleano que recive un string para ver si su contenido solo tiene numeros o no.
        {
            return value.All(char.IsNumber);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((cmb_COM_hora.Text == "") || (cmb_COM_minuto.Text == "") || (txt_COM_cantChicos.Text == "") || (txt_COM_grupo.Text == "") || (txt_COM_nomCatesista.Text == "") || (txt_COM_nomPadre.Text == ""))
            {
                MessageBox.Show("No puede dejar espacios vacios!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string aux = txt_COM_cantChicos.Text;
                try
                {
                    Comuniones.registro_comunion(txt_COM_nomCatesista.Text, txt_COM_grupo.Text, txt_COM_nomPadre.Text, (cmb_COM_hora.Text + ":" + cmb_COM_minuto.Text), dtp_COM_fecha.Text, Convert.ToInt32(txt_COM_cantChicos.Text));
                    MessageBox.Show("Comunion agregada correctamente!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarTodo();
                }
                catch (Exception ex)
                {
                    if (IsNumeric(aux) == false) // Evalua si el string aux contiene solo numeros, llamando a la funcion IsNumeric. Si no los tiene, devuelve este error.
                    {
                        MessageBox.Show("Error al agregar la comunion: El campo 'Cant.Chicos' solo puede contener numeros", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else MessageBox.Show("Error al agregar la comunion: " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void mtc_COM_DateChanged(object sender, DateRangeEventArgs e)
        {
            LimpiarTodo();
        }

        private void dgv_COM_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgv_COM_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string f_h_m; // Este string se utiliza para guardar la fecha, la hora y los minutos para despues cargarlos en sus respectivos objetos para su posterior modificacion.
                datos = new string[dgv_COM.Columns.Count];
                for (int i = 0; i <= datos.Length - 1; i++)
                {
                    datos[i] = dgv_COM.Rows[e.RowIndex].Cells[i].Value.ToString();
                }
                check = IsNumeric(datos[0].Substring(0, 2));
                if (check == false)
                {
                    f_h_m = datos[0].Substring(0, 9); // Este substring agarra la fecha de la celda.
                    dtp_COM_fecha.Text = f_h_m;
                    f_h_m = datos[0].Substring(10, 2); // Este substring agarra la hora de la celda.
                    cmb_COM_hora.Text = f_h_m;
                    f_h_m = datos[0].Substring(13, 2); // Este substring agarra los minutos de la celda.
                    cmb_COM_minuto.Text = f_h_m;
                }
                else
                {
                    f_h_m = datos[0].Substring(0, 10); // Este substring agarra la fecha de la celda.
                    dtp_COM_fecha.Text = f_h_m;
                    f_h_m = datos[0].Substring(11, 2); // Este substring agarra la hora de la celda.
                    cmb_COM_hora.Text = f_h_m;
                    f_h_m = datos[0].Substring(14, 2); // Este substring agarra los minutos de la celda.
                    cmb_COM_minuto.Text = f_h_m;
                }

                txt_COM_nomCatesista.Text = datos[1];
                txt_COM_grupo.Text = datos[2];
                txt_COM_cantChicos.Text = datos[3];
                txt_COM_nomPadre.Text = datos[4];
                btn_COM_modificar.Enabled = true;
                btn_COM_borrar.Enabled = true;
                btn_COM_aceptar.Enabled = false;
                btn_COM_imprimir.Enabled = true;
            }
            catch
            {

            }

        }
        private void LimpiarTodo()
        {
            txt_COM_cantChicos.Text = "";
            txt_COM_grupo.Text = "";
            txt_COM_nomCatesista.Text = "";
            txt_COM_nomPadre.Text = "";
            cmb_COM_hora.SelectedItem = null;
            cmb_COM_minuto.SelectedItem = null;
            btn_COM_aceptar.Enabled = true;
            btn_COM_borrar.Enabled = false;
            btn_COM_modificar.Enabled = false;
            btn_COM_imprimir.Enabled = false;
            dtp_COM_fecha.Text = DateTime.Today.ToShortDateString();
            txt_COM_nomPadre.Focus();
            dgv_COM.Columns.Clear();
            dgv_COM.DataSource = Comuniones.lectura_comunion(mtc_COM.SelectionStart.ToShortDateString());
        }

        private void btn_COM_borrar_Click(object sender, EventArgs e)
        {

        }

        private void btn_COM_borrar_Click_1(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DialogResult pregunta = MessageBox.Show("Esta seguro que desea eliminar el evento?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (pregunta == DialogResult.Yes)
            {
                try
                {
                    check = IsNumeric(datos[0].Substring(0, 2));
                    if (check == true)
                    {
                        Comuniones.borrar_comunion(datos[0].Substring(0, 10), datos[0].Substring(11, 5));
                    }
                    else
                    {
                        Comuniones.borrar_comunion(datos[0].Substring(0, 9), datos[0].Substring(10, 5));
                    }
                    MessageBox.Show("Comunion eliminada correctamente!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarTodo();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex);
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btn_COM_imprimir_Click(object sender, EventArgs e)
        {
            object ObjMiss = System.Reflection.Missing.Value;
            Word.Application ObjWord = new Word.Application();
            string ruta = Application.StartupPath + @"\Impresion(Comuniones).docx";
            object parametro = ruta;

            //creamos objetos con el nombre de los marcadores
            object fecha = "fecha";
            object hora = "hora";
            object catequista = "catequista";
            object grupo = "grupo";
            object cant_chicos = "cant_chicos";
            object padre = "padre";

            Word.Document ObjDoc = ObjWord.Documents.Open(ref parametro, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss);

            //por cada objeto marcador se crea un rango que almacenará y modificará
            //el bookmark (o marcador especificado) según el nombre asignado
            Word.Range FECHA = ObjDoc.Bookmarks.get_Item(ref fecha).Range;
            FECHA.Text = dtp_COM_fecha.Text;
            Word.Range HORA = ObjDoc.Bookmarks.get_Item(ref hora).Range;
            HORA.Text = cmb_COM_hora.Text + ":" + cmb_COM_minuto.Text;
            Word.Range CATEQUISTA = ObjDoc.Bookmarks.get_Item(ref catequista).Range;
            CATEQUISTA.Text = txt_COM_nomCatesista.Text;
            Word.Range GRUPO = ObjDoc.Bookmarks.get_Item(ref grupo).Range;
            GRUPO.Text = txt_COM_grupo.Text;
            Word.Range CANT_CHICOS = ObjDoc.Bookmarks.get_Item(ref cant_chicos).Range;
            CANT_CHICOS.Text = txt_COM_cantChicos.Text;
            Word.Range PADRE = ObjDoc.Bookmarks.get_Item(ref padre).Range;
            PADRE.Text = txt_COM_nomPadre.Text;

            //Creamos objetos que almacenen los rangos creados
            object rango1 = FECHA;
            object rango2 = HORA;
            object rango3 = CATEQUISTA;
            object rango4 = GRUPO;
            object rango5 = CANT_CHICOS;
            object rango6 = PADRE;

            //Enviamos los parámetros a cada marcador
            ObjDoc.Bookmarks.Add("fecha", ref rango1);
            ObjDoc.Bookmarks.Add("hora", ref rango2);
            ObjDoc.Bookmarks.Add("catequista", ref rango3);
            ObjDoc.Bookmarks.Add("grupo", ref rango4);
            ObjDoc.Bookmarks.Add("cant_chicos", ref rango5);
            ObjDoc.Bookmarks.Add("padre", ref rango6);
            ObjWord.Visible = true;
        }
    }
}