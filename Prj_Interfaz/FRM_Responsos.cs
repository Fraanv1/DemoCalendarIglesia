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
    public partial class FRM_Responsos : Form
    {
        Consultas_Responsos Responsos = new Consultas_Responsos();

        public FRM_Responsos()
        {
            InitializeComponent();
        }
        //Instancia de la clase Missing para representar valores perdidos,
        //por ejemplo, métodos que tienen valores de parámetros predeterminados
        object ObjMiss = System.Reflection.Missing.Value;

        //creamos el objeto Word
        Word.Application ObjWord = new Word.Application();
        string ruta = Application.StartupPath + @"\Impresion(Responsos).docx";
        private void FRM_Responsos_Load(object sender, EventArgs e)
        {
            crearGrillaRES(DateTime.Now.ToShortDateString());
            btn_RES_imprimir.Enabled = false;
        }
        private void mtc_RES_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                dgv_RES.ClearSelection();
                crearGrillaRES(e.Start.ToShortDateString());//Llamamos al metodo lectura_bautismos para que nos devuelva el DataTable con los datos necesarios para cargar el dgv
                btn_RES_registrar.Enabled = true;
                btn_RES_eliminar.Enabled = false;
                btn_RES_modificar.Enabled = false;
                limpiarObjetos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void crearGrillaRES(string fecha_filtro)
        {
            dgv_RES.Columns.Clear();
            dgv_RES.DataSource = Responsos.lectura_responsos(fecha_filtro);//Llamamos al metodo lectura_bautismos para que nos devuelva el DataTable con los datos necesarios para cargar el dgv
        }

        private void btn_RES_registrar_Click(object sender, EventArgs e)
        {
            int verificadorDNI = 0;
            if (cmb_RES_hora.SelectedIndex == -1 || cmb_RES_minuto.SelectedIndex == -1 || txt_RES_apeDifunto.Text.Trim() == "" || txt_RES_nomDifunto.Text.Trim() == "" || txt_RES_dniDifunto.Text.Trim() == "" || txt_RES_nominscriptor.Text.Trim() == "" || txt_RES_apellinscriptor.Text.Trim() == "" || txt_RES_nomTestigo1.Text.Trim() == "" || txt_RES_nomTestigo2.Text.Trim() == "" || txt_RES_telefono.Text.Trim() == "" || dtp_RES_fecha.Text.Trim() == "" || cmb_RES_tipotel.SelectedIndex == -1)//Verifica que cada uno de los objetos tenga un valor en su interior
                MessageBox.Show("No puede dejar espacios vacios!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                try
                {
                    verificadorDNI = Convert.ToInt32(txt_RES_dniDifunto.Text);
                    Responsos.registro_responsos(txt_RES_nomDifunto.Text, txt_RES_apeDifunto.Text, int.Parse(txt_RES_dniDifunto.Text), txt_RES_nominscriptor.Text, txt_RES_apellinscriptor.Text, txt_RES_telefono.Text, cmb_RES_tipotel.Text, txt_RES_nomTestigo1.Text, txt_RES_nomTestigo2.Text, cmb_RES_hora.Text + ":" + cmb_RES_minuto.Text, dtp_RES_fecha.Text);
                    limpiarObjetos();
                    crearGrillaRES(DateTime.Now.ToShortDateString());
                    MessageBox.Show(("Responso cargado"), ("Exito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("Ingrese solo numeros en el dni", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
        private void btn_RES_modificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmb_RES_hora.SelectedIndex == -1 || cmb_RES_minuto.SelectedIndex == -1 || txt_RES_apeDifunto.Text.Trim() == "" || txt_RES_nomDifunto.Text.Trim() == "" || txt_RES_dniDifunto.Text.Trim() == "" || txt_RES_nominscriptor.Text.Trim() == "" || txt_RES_apellinscriptor.Text.Trim() == "" || txt_RES_nomTestigo1.Text.Trim() == "" || txt_RES_nomTestigo2.Text.Trim() == "" || txt_RES_telefono.Text.Trim() == "" || dtp_RES_fecha.Text.Trim() == "" || cmb_RES_tipotel.SelectedIndex == -1)//Verifica que cada uno de los objetos tenga un valor en su interior
                    MessageBox.Show("No puede dejar espacios vacios!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    DialogResult pregunta = MessageBox.Show("Esta seguro que desea modificar el evento?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (pregunta == DialogResult.Yes)
                    {
                        try
                        {
                            Responsos.act_responsos(datos_viejos[1], datos_viejos[0], dtp_RES_fecha.Text, cmb_RES_hora.Text + ":" + cmb_RES_minuto.Text, txt_RES_nomDifunto.Text, txt_RES_apeDifunto.Text, int.Parse(txt_RES_dniDifunto.Text), txt_RES_nominscriptor.Text, txt_RES_apellinscriptor.Text, txt_RES_telefono.Text, cmb_RES_tipotel.Text, txt_RES_nomTestigo1.Text, txt_RES_nomTestigo2.Text);
                            limpiarObjetos();
                            crearGrillaRES(DateTime.Now.ToShortDateString());
                            btn_RES_registrar.Enabled = true;
                            btn_RES_eliminar.Enabled = false;
                            btn_RES_modificar.Enabled = false;
                            MessageBox.Show("Responso modificado correctamente!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch
                        {
                            MessageBox.Show("Error en la base de datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void limpiarObjetos()
        {
            cmb_RES_hora.SelectedIndex = -1;
            cmb_RES_minuto.SelectedIndex = -1;
            cmb_RES_tipotel.Text = "";
            txt_RES_apeDifunto.Text = "";
            txt_RES_dniDifunto.Text = "";
            txt_RES_nomDifunto.Text = "";
            txt_RES_nominscriptor.Text = "";
            txt_RES_nomTestigo1.Text = "";
            txt_RES_nomTestigo2.Text = "";
            txt_RES_telefono.Text = "";
            txt_RES_apellinscriptor.Text = "";
            dtp_RES_fecha.Text = DateTime.Today.ToShortDateString();

            txt_RES_nomDifunto.Focus();
            dgv_RES.DataSource = Responsos.lectura_responsos(mtc_RES.SelectionStart.ToShortDateString());
        }

        string [] datos_viejos;

        private void dgv_RES_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DialogResult pregunta = MessageBox.Show("Desea modificar el evento?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (pregunta == DialogResult.Yes)
                {
                    btn_RES_registrar.Enabled = false;//Deshabilita el boton de agregar un evento
                    btn_RES_modificar.Enabled = true;//Habilitara los botones de modificacion y borrado solo cuando haga doble click en la grilla del form
                    btn_RES_eliminar.Enabled = true;

                    datos_viejos = new string[dgv_RES.Columns.Count]; //Array que tendra en cada una de sus posiciones los valores de la fila de la grilla que se haya seleccionado
                    for (int i = 0; i < datos_viejos.Length; i++)//Bucle que nos sirve para cargar el array con todos los datos de la fila seleccionada
                        datos_viejos[i] = dgv_RES.Rows[e.RowIndex].Cells[i].Value.ToString();//Cada posicion del array representara cada columna de la grilla, es decir que tendra el mismo dato que hay en la columna pero obviamente solo de la fila seleccionada

                    //En la siguiente porcion de codigo cargaremos todos los objetos con los datos que estan en la fila seleccionada de la grilla

                    //FECHA Y HORA
                    int indiceDelUltimoEspacio = datos_viejos[0].LastIndexOf(" ");
                    dtp_RES_fecha.Text = datos_viejos[0];//Carga exactamente la fecha. Utiliza un subString ya que lo que esta cargado en el array es exactamente igual a lo que esta en la grilla y como en la grilla tiene la fecha y hora juntas se utiliza el subString para agarrar solo la fecha
                    cmb_RES_hora.Text = datos_viejos[1].Substring(0, 2);
                    cmb_RES_minuto.Text = datos_viejos[1].Substring(3, 2);

                    //RESPONSO
                    indiceDelUltimoEspacio = datos_viejos[2].LastIndexOf(" ");//Esta variable contendra el indice (posicion) del ultimo espacio dentro de la columna nombre y apellido (que esta en la posicion 1 del array datos_viejos). Este se utilizara para poder agarra el apellido y el nombre por separado (SOLO CUANDO SE ESCRIBE UN NOMBRE Y UN APELLIDO O DOS NOMBRES Y UN APELLIDO, CUALQUIER OTRA COMBINACION LOS SEPARARA DE MALA MANERA)
                    txt_RES_nomDifunto.Text = datos_viejos[2].Substring(0, indiceDelUltimoEspacio);//Se utiliza el indice del ultimo espacio para agarra la longitud del nombre, ya que ambos coiciden
                    txt_RES_apeDifunto.Text = datos_viejos[2].Substring(indiceDelUltimoEspacio + 1);//Agarra todo desde el siguiente caracter del ultimo espacio
                    txt_RES_dniDifunto.Text = datos_viejos[3];

                    //INSCRIPTOR Y TESTIGOS
                    indiceDelUltimoEspacio = datos_viejos[4].LastIndexOf(" ");
                    txt_RES_nominscriptor.Text = datos_viejos[4].Substring(0, indiceDelUltimoEspacio);
                    txt_RES_apellinscriptor.Text = datos_viejos[4].Substring(indiceDelUltimoEspacio + 1);

                    //Telefono
                    indiceDelUltimoEspacio = datos_viejos[5].LastIndexOf(" ");
                    txt_RES_telefono.Text = datos_viejos[5].Substring(0, indiceDelUltimoEspacio);
                    cmb_RES_tipotel.Text = datos_viejos[5].Substring(indiceDelUltimoEspacio + 1);

                    //Testigos
                    txt_RES_nomTestigo1.Text = datos_viejos[6];
                    txt_RES_nomTestigo2.Text = datos_viejos[7];

                    MessageBox.Show(("Datos viejos cargados, ahora puede modificar o eliminar"), ("Aviso"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                    btn_RES_registrar.Enabled = false;//Deshabilita el boton de agregar un evento
                    btn_RES_modificar.Enabled = true;//Habilitara los botones de modificacion y borrado solo cuando haga doble click en la grilla del form
                    btn_RES_eliminar.Enabled = true;
                    btn_RES_imprimir.Enabled = true;
                }
                else
                {

                }
            }
            catch
            {
            }
        }

        private void btn_RES_eliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult pregunta = MessageBox.Show("Esta seguro que desea borrar el evento?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (pregunta == DialogResult.Yes)
                {
                    //Mandamos todos los parametros al metodo para que borre 
                    Responsos.borrar_responsos(datos_viejos[0], datos_viejos[1]);
                    crearGrillaRES(DateTime.Now.ToShortDateString());//Cargamos nuevamente la grilla del form para que de sensacion de refresco
                    btn_RES_registrar.Enabled = true;
                    btn_RES_eliminar.Enabled = false;
                    btn_RES_modificar.Enabled = false;
                    limpiarObjetos();
                    crearGrillaRES(DateTime.Now.ToShortDateString());
                    MessageBox.Show("Responso borrado correctamente!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txt_RES_nomTestigo1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txt_RES_nomTestigo2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txt_RES_nomDifunto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txt_RES_apeDifunto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txt_RES_nominscriptor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txt_RES_apellinscriptor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void btn_RES_imprimir_Click(object sender, EventArgs e)
        {
            try
            {
                object ObjMiss = System.Reflection.Missing.Value;
                Word.Application ObjWord = new Word.Application();
                object parametro = ruta;
                object fecha = "fecha_responso";
                object horario = "hora_responso";
                object nombreResponso = "nombre_responso";
                object apellidoResponso = "apellido_responso";
                object DNI_responso = "dni_difunto";
                object nombre_inscriptor = "nombre_inscriptor";
                object apellido_inscriptor = "apellido_inscriptor";
                object telefono_inscriptor = "telefono_inscriptor";
                object tipo_tel = "tipo_tel";
                object testigo1 = "testigo_1";
                object testigo2 = "testigo_2";

                Word.Document ObjDoc = ObjWord.Documents.Open(ref parametro, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss);

                Word.Range FECHA = ObjDoc.Bookmarks.get_Item(ref fecha).Range;
                FECHA.Text = datos_viejos[0];
                Word.Range HORARIO = ObjDoc.Bookmarks.get_Item(ref horario).Range;
                HORARIO.Text = datos_viejos[1];

                int indiceDelUltimoEspacio;

                //RESPONSO
                indiceDelUltimoEspacio = datos_viejos[2].LastIndexOf(" ");//Esta variable contendra el indice (posicion) del ultimo espacio dentro de la columna nombre y apellido (que esta en la posicion 1 del array datos_viejos). Este se utilizara para poder agarra el apellido y el nombre por separado (SOLO CUANDO SE ESCRIBE UN NOMBRE Y UN APELLIDO O DOS NOMBRES Y UN APELLIDO, CUALQUIER OTRA COMBINACION LOS SEPARARA DE MALA MANERA)
                Word.Range NOMBRE_RES = ObjDoc.Bookmarks.get_Item(ref nombreResponso).Range;
                NOMBRE_RES.Text = datos_viejos[2].Substring(0, indiceDelUltimoEspacio);
                Word.Range APE_RES = ObjDoc.Bookmarks.get_Item(ref apellidoResponso).Range;
                APE_RES.Text = datos_viejos[2].Substring(indiceDelUltimoEspacio + 1);
                Word.Range DNI_RES = ObjDoc.Bookmarks.get_Item(ref DNI_responso).Range;
                DNI_RES.Text = datos_viejos[3];


                //INSCRIPTOR
                indiceDelUltimoEspacio = datos_viejos[4].LastIndexOf(" ");
                Word.Range NOMBRE_INS = ObjDoc.Bookmarks.get_Item(ref nombre_inscriptor).Range;
                NOMBRE_INS.Text = datos_viejos[4].Substring(0, indiceDelUltimoEspacio);
                Word.Range APE_INS = ObjDoc.Bookmarks.get_Item(ref apellido_inscriptor).Range;
                APE_INS.Text = datos_viejos[4].Substring(indiceDelUltimoEspacio + 1);


                //Telefono
                indiceDelUltimoEspacio = datos_viejos[5].LastIndexOf(" ");
                Word.Range TELEFONO = ObjDoc.Bookmarks.get_Item(ref telefono_inscriptor).Range;
                TELEFONO.Text = datos_viejos[5].Substring(0, indiceDelUltimoEspacio);
                Word.Range TIPO_TELEFONO = ObjDoc.Bookmarks.get_Item(ref tipo_tel).Range;
                TIPO_TELEFONO.Text = datos_viejos[5].Substring(indiceDelUltimoEspacio + 1);

                //Testigos
                Word.Range TESTIGO1 = ObjDoc.Bookmarks.get_Item(ref testigo1).Range;
                TESTIGO1.Text = datos_viejos[6];
                Word.Range TESTIGO2 = ObjDoc.Bookmarks.get_Item(ref testigo2).Range;
                TESTIGO2.Text = datos_viejos[7];

                object rango1 = FECHA;
                object rango2 = HORARIO;
                object rango3 = NOMBRE_RES;
                object rango4 = APE_RES;
                object rango5 = DNI_RES;
                object rango6 = NOMBRE_INS;
                object rango7 = APE_INS;
                object rango8 = TELEFONO;
                object rango9 = TIPO_TELEFONO;
                object rango10 = TESTIGO1;
                object rango11 = TESTIGO2;


                ObjDoc.Bookmarks.Add("fecha_responso", ref rango1);
                ObjDoc.Bookmarks.Add("hora_responso", ref rango2);
                ObjDoc.Bookmarks.Add("nombre_responso", ref rango3);
                ObjDoc.Bookmarks.Add("apellido_responso", ref rango4);
                ObjDoc.Bookmarks.Add("dni_difunto", ref rango5);
                ObjDoc.Bookmarks.Add("nombre_inscriptor", ref rango6);
                ObjDoc.Bookmarks.Add("apellido_inscriptor", ref rango7);
                ObjDoc.Bookmarks.Add("telefono_inscriptor", ref rango8);
                ObjDoc.Bookmarks.Add("tipo_tel", ref rango9);
                ObjDoc.Bookmarks.Add("testigo1", ref rango10);
                ObjDoc.Bookmarks.Add("testigo2", ref rango11);

                ObjWord.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cargue los campos" + ex.ToString());
            }
        }

      
    }
}
