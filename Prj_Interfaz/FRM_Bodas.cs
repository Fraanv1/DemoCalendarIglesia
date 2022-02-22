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
    public partial class FRM_Bodas : Form
    {
        Consultas_Bodas receptorDatos = new Consultas_Bodas();

        //Instancia de la clase Missing para representar valores perdidos,
        //por ejemplo, métodos que tienen valores de parámetros predeterminados
        object ObjMiss = System.Reflection.Missing.Value;

        //creamos el objeto Word
        Word.Application ObjWord = new Word.Application();
        string ruta = Application.StartupPath + @"\Impresion(Bodas).docx";

        public FRM_Bodas()
        {
            InitializeComponent();
        }

        private void FRM_Bodas_Load(object sender, EventArgs e)
        {
            crearGrillaBOD(DateTime.Now.ToShortDateString());
        }
        private void mtc_BOD_DateSelected(object sender, DateRangeEventArgs e)
        {
            try
            {
                crearGrillaBOD(e.Start.ToShortDateString());
                btn_BOD_registrar.Enabled = true;
                btn_BOD_eliminar.Enabled = false;
                btn_BOD_modificar.Enabled = false;
                limpiarObjetos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void crearGrillaBOD(string fecha_filtro)
        {
            dgv_BOD.Columns.Clear();
            dgv_BOD.DataSource = receptorDatos.lectura_bodas(fecha_filtro);//Llamamos al metodo lectura_bautismos para que nos devuelva el DataTable con los datos necesarios para cargar el dgv
        }

        private void btn_BOD_registrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmb_BOD_hora.SelectedIndex == -1 || cmb_BOD_minuto.SelectedIndex == -1 || txt_BOD_nomNovio.Text.Trim() == "" || txt_BOD_nomNovia.Text.Trim() == "" || txt_BOD_apellnovio.Text.Trim() == "" || txt_BOD_apellnovia.Text.Trim() == "" || txt_BOD_email.Text.Trim() == "" || txt_BOD_nomPadre.Text.Trim() == "" || txt_BOD_telefono.Text.Trim() == "" || txt_BOD_tipoExpediente.Text.Trim() == "" || dtc_BOD.Text.Trim() == "" || dtc_BOD_fechaexpediente.Text.Trim() == "" || cmb_BOD_lugar.SelectedIndex == -1)//Verifica que cada uno de los objetos tenga un valor en su interior
                    MessageBox.Show("No puede dejar espacios vacios!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    receptorDatos.registro_bodas(dtc_BOD.Text, cmb_BOD_hora.Text + ":" + cmb_BOD_minuto.Text, txt_BOD_nomNovio.Text, txt_BOD_nomNovia.Text, txt_BOD_apellnovio.Text, txt_BOD_apellnovia.Text, txt_BOD_nomPadre.Text, cmb_BOD_lugar.Text, richtxt_BOD_observaciones.Text, txt_BOD_email.Text, txt_BOD_telefono.Text, cmb_BOD_tipotel.Text, dtc_BOD_fechaexpediente.Text, txt_BOD_tipoExpediente.Text);
                    limpiarObjetos();
                    crearGrillaBOD(DateTime.Now.ToShortDateString());
                    MessageBox.Show(("Boda cargada"), ("Exito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        

        private void btn_BOD_modificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmb_BOD_hora.SelectedIndex == -1 || cmb_BOD_minuto.SelectedIndex == -1 || txt_BOD_nomNovio.Text.Trim() == "" || txt_BOD_nomNovia.Text.Trim() == "" || txt_BOD_apellnovio.Text.Trim() == "" || txt_BOD_apellnovia.Text.Trim() == "" || txt_BOD_email.Text.Trim() == "" || txt_BOD_nomPadre.Text.Trim() == "" || txt_BOD_telefono.Text.Trim() == "" || txt_BOD_tipoExpediente.Text.Trim() == "" || dtc_BOD.Text.Trim() == "" || dtc_BOD_fechaexpediente.Text.Trim() == "")//Verifica que cada uno de los objetos tenga un valor en su interior
                    MessageBox.Show("No puede dejar espacios vacios!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    DialogResult pregunta = MessageBox.Show("Esta seguro que desea modificar el evento?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (pregunta == DialogResult.Yes)
                    {
                        receptorDatos.act_bodas(datos_viejos[0], datos_viejos[1], dtc_BOD.Text, cmb_BOD_hora.Text + ":" + cmb_BOD_minuto.Text, txt_BOD_nomNovio.Text, txt_BOD_nomNovia.Text, txt_BOD_apellnovio.Text, txt_BOD_apellnovia.Text, txt_BOD_nomPadre.Text, cmb_BOD_lugar.Text, richtxt_BOD_observaciones.Text, txt_BOD_email.Text, txt_BOD_telefono.Text, cmb_BOD_tipotel.Text, dtc_BOD_fechaexpediente.Text, txt_BOD_tipoExpediente.Text);
                        limpiarObjetos();
                        crearGrillaBOD(DateTime.Now.ToShortDateString());
                        btn_BOD_registrar.Enabled = true;
                        btn_BOD_eliminar.Enabled = false;
                        btn_BOD_modificar.Enabled = false;
                        MessageBox.Show("Boda modificada correctamente!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        string[] datos_viejos;

        private void dgv_BOD_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {       
                DialogResult pregunta = MessageBox.Show("Desea modificar el evento?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (pregunta == DialogResult.Yes)
                {

                    datos_viejos = new string[dgv_BOD.Columns.Count]; //Array que tendra en cada una de sus posiciones los valores de la fila de la grilla que se haya seleccionado
                    for (int i = 0; i < datos_viejos.Length; i++)//Bucle que nos sirve para cargar el array con todos los datos de la fila seleccionada
                        datos_viejos[i] = dgv_BOD.Rows[e.RowIndex].Cells[i].Value.ToString();//Cada posicion del array representara cada columna de la grilla, es decir que tendra el mismo dato que hay en la columna pero obviamente solo de la fila seleccionada

                    //En la siguiente porcion de codigo cargaremos todos los objetos con los datos que estan en la fila seleccionada de la grilla


                    int indiceDelUltimoEspacio = datos_viejos[0].LastIndexOf(" ");

                    //FECHA Y HORA

                    dtc_BOD.Text = datos_viejos[0];//Carga exactamente la fecha. Utiliza un subString ya que lo que esta cargado en el array es exactamente igual a lo que esta en la grilla y como en la grilla tiene la fecha y hora juntas se utiliza el subString para agarrar solo la fecha
                    cmb_BOD_hora.Text = datos_viejos[1].Substring(0, 2);
                    cmb_BOD_minuto.Text = datos_viejos[1].Substring(3, 2);


                    //NOVIO
                    indiceDelUltimoEspacio = datos_viejos[2].LastIndexOf(" ");//Esta variable contendra el indice (posicion) del ultimo espacio dentro de la columna nombre y apellido (que esta en la posicion 1 del array datos_viejos). Este se utilizara para poder agarra el apellido y el nombre por separado (SOLO CUANDO SE ESCRIBE UN NOMBRE Y UN APELLIDO O DOS NOMBRES Y UN APELLIDO, CUALQUIER OTRA COMBINACION LOS SEPARARA DE MALA MANERA)
                    txt_BOD_nomNovio.Text = datos_viejos[2].Substring(0, indiceDelUltimoEspacio);//Se utiliza el indice del ultimo espacio para agarra la longitud del nombre, ya que ambos coiciden
                    txt_BOD_apellnovio.Text = datos_viejos[2].Substring(indiceDelUltimoEspacio + 1);//Agarra todo desde el siguiente caracter del ultimo espacio

                    //NOVIA
                    indiceDelUltimoEspacio = datos_viejos[3].LastIndexOf(" ");//Esta variable contendra el indice (posicion) del ultimo espacio dentro de la columna nombre y apellido (que esta en la posicion 1 del array datos_viejos). Este se utilizara para poder agarra el apellido y el nombre por separado (SOLO CUANDO SE ESCRIBE UN NOMBRE Y UN APELLIDO O DOS NOMBRES Y UN APELLIDO, CUALQUIER OTRA COMBINACION LOS SEPARARA DE MALA MANERA)
                    txt_BOD_nomNovia.Text = datos_viejos[3].Substring(0, indiceDelUltimoEspacio);//Se utiliza el indice del ultimo espacio para agarra la longitud del nombre, ya que ambos coiciden
                    txt_BOD_apellnovia.Text = datos_viejos[3].Substring(indiceDelUltimoEspacio + 1);//Agarra todo desde el siguiente caracter del ultimo espacio

                    //Expediente
                    indiceDelUltimoEspacio = datos_viejos[4].LastIndexOf(" ");
                    dtc_BOD_fechaexpediente.Text = datos_viejos[4];
                    txt_BOD_tipoExpediente.Text = datos_viejos[5];

                    //LUGAR 
                    cmb_BOD_lugar.Text = datos_viejos[6];

                    //MAIL
                    txt_BOD_email.Text = datos_viejos[7];

                    //TELEFONO
                    txt_BOD_telefono.Text = datos_viejos[8];
                    cmb_BOD_tipotel.Text = datos_viejos[9];

                    //PADRE
                    txt_BOD_nomPadre.Text = datos_viejos[10];

                    //Observaciones
                    richtxt_BOD_observaciones.Text = datos_viejos[11];

                    MessageBox.Show(("Datos viejos cargados, ahora puede modificar o eliminar"), ("Aviso"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                    btn_BOD_registrar.Enabled = false;//Deshabilita el boton de agregar un evento
                    btn_BOD_modificar.Enabled = true;//Habilitara los botones de modificacion y borrado solo cuando haga doble click en la grilla del form
                    btn_BOD_eliminar.Enabled = true;
                }
            }
            catch
            {

            }
        }

        private void btn_BOD_eliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult pregunta = MessageBox.Show("Esta seguro que desea borrar el evento?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (pregunta == DialogResult.Yes)
                {
                    //Mandamos todos los parametros al metodo para que borre 
                    receptorDatos.borrar_bodas(datos_viejos[0], datos_viejos[1]);
                    btn_BOD_registrar.Enabled = true;
                    btn_BOD_eliminar.Enabled = false;
                    btn_BOD_modificar.Enabled = false;
                    limpiarObjetos();
                    crearGrillaBOD(DateTime.Now.ToShortDateString());
                    MessageBox.Show("Boda borrada correctamente!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                MessageBox.Show(("No hizo doble click en la grilla"), ("Atencion"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void limpiarObjetos()
        {
            cmb_BOD_hora.SelectedIndex = -1;
            richtxt_BOD_observaciones.Text = "";
            cmb_BOD_minuto.SelectedIndex = -1;
            cmb_BOD_tipotel.SelectedIndex = -1;
            cmb_BOD_lugar.SelectedIndex = -1;
            txt_BOD_nomNovia.Text = "";
            txt_BOD_nomNovio.Text = "";
            txt_BOD_apellnovia.Text = "";
            txt_BOD_apellnovio.Text = "";
            txt_BOD_nomPadre.Text = "";
            txt_BOD_telefono.Text = "";
            txt_BOD_tipoExpediente.Text = "";
            txt_BOD_email.Text = "";
            dtc_BOD.Text = DateTime.Today.ToShortDateString();
            dtc_BOD_fechaexpediente.Text = DateTime.Today.ToShortDateString();
            dgv_BOD.DataSource = receptorDatos.lectura_bodas(mtc_BOD.SelectionStart.ToShortDateString());
        }

        private void txt_BOD_nomNovio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txt_BOD_apellnovio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txt_BOD_nomNovia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txt_BOD_apellnovia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txt_BOD_nomPadre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txt_BOD_telefono_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void btn_BOD_imprimir_Click(object sender, EventArgs e)
        {
            try
            {
                object ObjMiss = System.Reflection.Missing.Value;
                Word.Application ObjWord = new Word.Application();
                object parametro = ruta;
                object fecha = "fecha_boda";
                object horario = "hora_boda";
                object nombre_novio = "nombre_novio";
                object apellido_novio = "apellido_novio";
                object nombre_novia = "nombre_novia";
                object apellido_novia = "apellido_novia";
                object lugar = "lugar";
                object telefono = "telefono";
                object tipo_tel = "tipo_tel";
                object fecha_expediente = "fecha_expediente";
                object tipo_expediente = "tipo_expediente";
                object email = "email";

                Word.Document ObjDoc = ObjWord.Documents.Open(ref parametro, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss);

                Word.Range FECHA = ObjDoc.Bookmarks.get_Item(ref fecha).Range;
                FECHA.Text = datos_viejos[0];
                Word.Range HORARIO = ObjDoc.Bookmarks.get_Item(ref horario).Range;
                HORARIO.Text = datos_viejos[1];

                int indiceDelUltimoEspacio;

                //NOVIO
                indiceDelUltimoEspacio = datos_viejos[2].LastIndexOf(" ");//Esta variable contendra el indice (posicion) del ultimo espacio dentro de la columna nombre y apellido (que esta en la posicion 1 del array datos_viejos). Este se utilizara para poder agarra el apellido y el nombre por separado (SOLO CUANDO SE ESCRIBE UN NOMBRE Y UN APELLIDO O DOS NOMBRES Y UN APELLIDO, CUALQUIER OTRA COMBINACION LOS SEPARARA DE MALA MANERA)
                Word.Range NOMBRE_NOVIO = ObjDoc.Bookmarks.get_Item(ref nombre_novio).Range;
                NOMBRE_NOVIO.Text = datos_viejos[2].Substring(0, indiceDelUltimoEspacio);
                Word.Range APELLIDO_NOVIO = ObjDoc.Bookmarks.get_Item(ref apellido_novio).Range;
                APELLIDO_NOVIO.Text = datos_viejos[2].Substring(indiceDelUltimoEspacio + 1);

                //NOVIA
                indiceDelUltimoEspacio = datos_viejos[3].LastIndexOf(" ");
                Word.Range NOMBRE_NOVIA = ObjDoc.Bookmarks.get_Item(ref nombre_novia).Range;
                NOMBRE_NOVIA.Text = datos_viejos[3].Substring(0, indiceDelUltimoEspacio);
                Word.Range APELLIDO_NOVIA = ObjDoc.Bookmarks.get_Item(ref apellido_novia).Range;
                APELLIDO_NOVIA.Text = datos_viejos[3].Substring(indiceDelUltimoEspacio + 1);


                //LUGAR
                Word.Range LUGAR = ObjDoc.Bookmarks.get_Item(ref lugar).Range;
                LUGAR.Text = datos_viejos[6];

                
                //MAIL
                Word.Range MAIL = ObjDoc.Bookmarks.get_Item(ref email).Range;//AREGLATE ESTO PA
                MAIL.Text = datos_viejos[7];
                //Telefono
                indiceDelUltimoEspacio = datos_viejos[5].LastIndexOf(" ");
                Word.Range TELEFONO = ObjDoc.Bookmarks.get_Item(ref telefono).Range;
                TELEFONO.Text = datos_viejos[8];
                Word.Range TIPO_TELEFONO = ObjDoc.Bookmarks.get_Item(ref tipo_tel).Range;
                TIPO_TELEFONO.Text = datos_viejos[9];

                //EXPEDIENTE
                Word.Range FECHA_EXPEDIENTE = ObjDoc.Bookmarks.get_Item(ref fecha_expediente).Range;
                FECHA_EXPEDIENTE.Text = datos_viejos[4];
                Word.Range TIPO_EXPEDIENTE = ObjDoc.Bookmarks.get_Item(ref tipo_expediente).Range;
                TIPO_EXPEDIENTE.Text = datos_viejos[5];

                object rango1 = FECHA;
                object rango2 = HORARIO;
                object rango3 = NOMBRE_NOVIO;
                object rango4 = APELLIDO_NOVIO;
                object rango5 = NOMBRE_NOVIA;
                object rango6 = APELLIDO_NOVIA;
                object rango7 = LUGAR;
                object rango8 = TELEFONO;
                object rango9 = TIPO_TELEFONO;
                object rango10 = FECHA_EXPEDIENTE;
                object rango11 = TIPO_EXPEDIENTE;


                ObjDoc.Bookmarks.Add("fecha", ref rango1);
                ObjDoc.Bookmarks.Add("hora", ref rango2);
                ObjDoc.Bookmarks.Add("nombre_novio", ref rango3);
                ObjDoc.Bookmarks.Add("apellido_novio", ref rango4);
                ObjDoc.Bookmarks.Add("nombre_novia", ref rango5);
                ObjDoc.Bookmarks.Add("apellido_novia", ref rango6);
                ObjDoc.Bookmarks.Add("lugar", ref rango7);
                ObjDoc.Bookmarks.Add("telefono", ref rango8);
                ObjDoc.Bookmarks.Add("tipo_tel", ref rango9);
                ObjDoc.Bookmarks.Add("fecha_expediente", ref rango10);
                ObjDoc.Bookmarks.Add("tipo_expediente", ref rango11);

                ObjWord.Visible = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}