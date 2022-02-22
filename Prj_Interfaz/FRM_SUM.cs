using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;
using Prj_Trabajo;

namespace Prj_Interfaz
{
    public partial class FRM_SUM : Form
    {
        Consultas_SUM Sum = new Consultas_SUM();
        //Instancia de la clase Missing para representar valores perdidos,
        //por ejemplo, métodos que tienen valores de parámetros predeterminados
        object ObjMiss = System.Reflection.Missing.Value;
        //creamos el objeto Word
        Word.Application ObjWord = new Word.Application();
        string ruta = Application.StartupPath + @"\Impresion(SUM).docx";
        public FRM_SUM()
        {
            InitializeComponent();
        }

        private void FRM_SUM_Load(object sender, EventArgs e)
        {
            crearGrillaSUM(DateTime.Now.ToShortDateString());//Creamos la grilla en la fecha actual
        }
        private void mtc_SUM_calendario_DateSelected(object sender, DateRangeEventArgs e)
        {
            btn_SUM_ingresar.Enabled = true; //Habilita el boton de agregar un evento
            btn_SUM_modificar.Enabled = false;//Deshabilita los botones de borrado y modificacion cuando se cambia la fecha del calendario
            btn_SUM_borrar.Enabled = false;
            btn_SUM_imprimir.Enabled = false;
            limpiarObjetos();//Limpiamos todos lo objetos cuando se cambia la seleccion del calendario

            crearGrillaSUM(e.Start.ToShortDateString());//Creamos la grilla en la fecha seleccionada
        }
        private void crearGrillaSUM(string fecha_filtro)//Metodo que crea la grilla del form pero requiere de un parametro que le de la fecha que va a ser utilizada como filtro para cargar la grilla
        {
            dgv_SUM.Columns.Clear();
            dgv_SUM.DataSource = Sum.lectura_sum(fecha_filtro);//Llamamos al metodo lectura_bautismos para que nos devuelva el DataTable con los datos necesarios para cargar el dgv
        }

        private void btn_SUM_ingresar_Click(object sender, EventArgs e)
        {
            int verificadorDNI = 0;//Variable que servira para verificar si lo ingresado en el dni sea un numero
            int verificadorCoste = 0;//Variable que servira para verificar si lo ingresado en el coste sea un numero

            if (txt_SUM_nombre.Text.Trim() == "" || txt_SUM_apellido.Text.Trim() == "" || txt_SUM_coste.Text.Trim() == "" || txt_SUM_dni.Text.Trim() == "" || txt_SUM_tel.Text.Trim() == "" || cmb_SUM_horaegreso.SelectedIndex == -1 || cmb_SUM_miningreso.SelectedIndex == -1 || cmb_SUM_horaingreso.SelectedIndex == -1 || cmb_SUM_miningreso.SelectedIndex == -1 || cmb_SUM_tipotel.SelectedIndex == -1 || richtxt_SUM_observaciones.Text.Trim() == "")//Verifica que cada uno de los objetos tenga un valor en su interior
                MessageBox.Show("No puede dejar espacios vacios!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                try
                {
                    verificadorDNI = Convert.ToInt32(txt_SUM_dni.Text);//Verifica que los objetos tengan el formato correcto para poder registrar
                    verificadorCoste = Convert.ToInt32(txt_SUM_coste.Text);

                    Sum.registro_sum(txt_SUM_nombre.Text, txt_SUM_apellido.Text, int.Parse(txt_SUM_dni.Text), txt_SUM_tel.Text, cmb_SUM_tipotel.Text, int.Parse(txt_SUM_coste.Text), dtp_SUM_fechauso.Text, cmb_SUM_horaingreso.Text + ":" + cmb_SUM_miningreso.Text, cmb_SUM_horaegreso.Text + ":" + cmb_SUM_minegreso.Text, richtxt_SUM_observaciones.Text);
                    crearGrillaSUM(DateTime.Now.ToShortDateString());
                    MessageBox.Show("Alquiler de sum agregada correctamente!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch//Ocurre solo si ingresa numero en el dni o en el coste
                {
                    if (verificadorDNI == 0) MessageBox.Show("Debe ingresar un numero entero como dni", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else MessageBox.Show("Debe ingresar un numero entero como coste del alquiler", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                }
            }
        }
        private void btn_SUM_modificar_Click(object sender, EventArgs e)
        {
            if (txt_SUM_nombre.Text.Trim() == "" || txt_SUM_apellido.Text.Trim() == "" || txt_SUM_coste.Text.Trim() == "" || txt_SUM_dni.Text.Trim() == "" || txt_SUM_tel.Text.Trim() == "" || cmb_SUM_horaegreso.SelectedIndex == -1 || cmb_SUM_miningreso.SelectedIndex == -1 || cmb_SUM_horaingreso.SelectedIndex == -1 || cmb_SUM_miningreso.SelectedIndex == -1 || cmb_SUM_tipotel.SelectedIndex == -1 || richtxt_SUM_observaciones.Text.Trim() == "")//Verifica que cada uno de los objetos tenga un valor en su interior
                MessageBox.Show("No puede dejar espacios vacios!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                DialogResult pregunta = MessageBox.Show("Esta seguro que desea modificar el evento?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (pregunta == DialogResult.Yes)
                {
                    //Mandamos todos los parametros al metodo para que actualice
                    Sum.act_sum(datos_viejos[4], datos_viejos[5].Substring(0, 2) + ":" + datos_viejos[5].Substring(3, 2), txt_SUM_nombre.Text, txt_SUM_apellido.Text, Convert.ToInt32(txt_SUM_dni.Text), txt_SUM_tel.Text, cmb_SUM_tipotel.Text, Convert.ToInt32(txt_SUM_coste.Text), dtp_SUM_fechauso.Text, cmb_SUM_horaingreso.Text + ":" + cmb_SUM_miningreso.Text, cmb_SUM_horaegreso.Text + ":" + cmb_SUM_miningreso.Text, richtxt_SUM_observaciones.Text);
                    crearGrillaSUM(DateTime.Now.ToShortDateString());//Cargamos nuevamente la grilla del form para que de sensacion de refresco
                    limpiarObjetos();
                    crearGrillaSUM(DateTime.Now.ToShortDateString());
                    MessageBox.Show("Alquiler de sum modificada correctamente!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btn_SUM_ingresar.Enabled = true; //Habilita el boton de agregar un evento
                    btn_SUM_modificar.Enabled = false;//Deshabilita los botones de borrado y modificacion cuando se cambia la fecha del calendario
                    btn_SUM_borrar.Enabled = false;
                }
            }
        }
        private void btn_SUM_borrar_Click(object sender, EventArgs e)
        {
            DialogResult pregunta = MessageBox.Show("Esta seguro que desea borrar el evento?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (pregunta == DialogResult.Yes)
            {
                //Mandamos todos los parametros al metodo para que borre 
                Sum.borrar_sum(datos_viejos[4], datos_viejos[5].Substring(0, 2) + ":" + datos_viejos[5].Substring(3, 2));
                crearGrillaSUM(DateTime.Now.ToShortDateString());//Cargamos nuevamente la grilla del form para que de sensacion de refresco
                limpiarObjetos();
                MessageBox.Show("Alquiler de sum borrado correctamente!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btn_SUM_ingresar.Enabled = true; //Habilita el boton de agregar un evento
                btn_SUM_modificar.Enabled = false;//Deshabilita los botones de borrado y modificacion cuando se cambia la fecha del calendario
                btn_SUM_borrar.Enabled = false;
            }
        }

        private void limpiarObjetos()//Metodo que volvera a todos los objetos a su estado inicial
        {
            //Fecha y hora
            dtp_SUM_fechauso.Text = DateTime.Now.ToShortDateString();
            cmb_SUM_horaingreso.SelectedItem = null;
            cmb_SUM_miningreso.SelectedItem = null;
            cmb_SUM_horaegreso.SelectedItem = null;
            cmb_SUM_minegreso.SelectedItem = null;
            //Locador
            txt_SUM_nombre.Text = "";
            txt_SUM_apellido.Text = "";
            txt_SUM_dni.Text = "";
            txt_SUM_tel.Text = "";
            cmb_SUM_tipotel.SelectedItem = null;
            txt_SUM_coste.Text = "";
            richtxt_SUM_observaciones.Text = "";
            btn_SUM_imprimir.Enabled = false;
        }

        string[] datos_viejos;
        private void dgv_SUM_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                datos_viejos = new string[dgv_SUM.Columns.Count]; //Array que tendra en cada una de sus posiciones los valores de la fila de la grilla que se haya seleccionado
                for (int i = 0; i < datos_viejos.Length; i++)//Bucle que nos sirve para cargar el array con todos los datos de la fila seleccionada
                    datos_viejos[i] = dgv_SUM.Rows[e.RowIndex].Cells[i].Value.ToString();//Cada posicion del array representara cada columna de la grilla, es decir que tendra el mismo dato que hay en la columna pero obviamente solo de la fila seleccionada

                //En la siguiente porcion de codigo cargaremos todos los objetos con los datos que estan en la fila seleccionada de la grilla (esta manera de hacerlo depende del formato que tenga la grilla del form ya que no en todas tienen el mismos datos)

                //LOCADOR
                int indiceDelUltimoEspacio = datos_viejos[0].LastIndexOf(" ");//Esta variable contendra el indice (posicion) del ultimo espacio dentro de la columna nombre y apellido (que esta en la posicion 1 del array datos_viejos). Este se utilizara para poder agarra el apellido y el nombre por separado (SOLO CUANDO SE ESCRIBE UN NOMBRE Y UN APELLIDO O DOS NOMBRES Y UN APELLIDO, CUALQUIER OTRA COMBINACION LOS SEPARARA DE MALA MANERA)
                txt_SUM_nombre.Text = datos_viejos[0].Substring(0, indiceDelUltimoEspacio);//Se utiliza el indice del ultimo espacio para agarra la longitud del nombre, ya que ambos coiciden
                txt_SUM_apellido.Text = datos_viejos[0].Substring(indiceDelUltimoEspacio + 1);//Agarra todo desde el siguiente caracter del ultimo espacio
                txt_SUM_dni.Text = datos_viejos[1];
                //TELEFONO
                indiceDelUltimoEspacio = datos_viejos[2].LastIndexOf(" ");//En este caso la variable tendra el indice en donde se encuentra el espacio que separa el numero de telefono con el tipo de telefono
                txt_SUM_tel.Text = datos_viejos[2].Substring(0, indiceDelUltimoEspacio);
                cmb_SUM_tipotel.Text = datos_viejos[2].Substring(indiceDelUltimoEspacio + 1);
                //OTROS
                txt_SUM_coste.Text = datos_viejos[3];
                richtxt_SUM_observaciones.Text = datos_viejos[7];
                //FECHA Y HORA
                dtp_SUM_fechauso.Text = datos_viejos[4];
                cmb_SUM_horaingreso.Text = datos_viejos[5].Substring(0, 2);
                cmb_SUM_miningreso.Text = datos_viejos[5].Substring(3, 2);
                cmb_SUM_horaegreso.Text = datos_viejos[6].Substring(0, 2);
                cmb_SUM_minegreso.Text = datos_viejos[6].Substring(3, 2);

                btn_SUM_ingresar.Enabled = false; //Deshabilita el boton de agregar un evento
                btn_SUM_borrar.Enabled = true;//Habilitara los botones de modificacion y borrado solo cuando haga doble click en la grilla del form
                btn_SUM_modificar.Enabled = true;
                btn_SUM_imprimir.Enabled = true;
            }
            catch
            {
            }
            
        }

        private void txt_SUM_nombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_SUM_apellido_TextChanged(object sender, EventArgs e)
        {

        }

        private void richtxt_SUM_observaciones_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_SUM_coste_TextChanged(object sender, EventArgs e)
        {

        }

        private void richtxt_SUM_observaciones_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txt_SUM_tel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void btn_SUM_imprimir_Click(object sender, EventArgs e)
        {
            object ObjMiss = System.Reflection.Missing.Value;
            Word.Application ObjWord = new Word.Application();
            object parametro = ruta;
            object fecha = "fecha_alquilada";
            object nombreLocador = "nombre_locador";
            object apellidoLocador = "apellido_locador";
            object dniLocador = "dni_locador";
            object telefonoLocador = "telefono_locador";
            object tipoTel = "tipo_telefono";
            object costeAlquiler = "coste_alquiler";
            object observaciones = "observaciones";
            object horario_ingreso = "hora_ingreso";
            object horario_egreso = "hora_egreso";

            Word.Document ObjDoc = ObjWord.Documents.Open(ref parametro, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss);

            Word.Range FECHA = ObjDoc.Bookmarks.get_Item(ref fecha).Range;
            FECHA.Text = datos_viejos[4];
            //LOCADOR
            int indiceDelUltimoEspacio = datos_viejos[0].LastIndexOf(" ");//Esta variable contendra el indice (posicion) del ultimo espacio dentro de la columna nombre y apellido (que esta en la posicion 1 del array datos_viejos). Este se utilizara para poder agarra el apellido y el nombre por separado (SOLO CUANDO SE ESCRIBE UN NOMBRE Y UN APELLIDO O DOS NOMBRES Y UN APELLIDO, CUALQUIER OTRA COMBINACION LOS SEPARARA DE MALA MANERA)
            Word.Range NOMBRELOCADOR = ObjDoc.Bookmarks.get_Item(ref nombreLocador).Range;
            NOMBRELOCADOR.Text = datos_viejos[0].Substring(0, indiceDelUltimoEspacio);
            Word.Range APELLIDOLOCADOR = ObjDoc.Bookmarks.get_Item(ref apellidoLocador).Range;
            APELLIDOLOCADOR.Text = datos_viejos[0].Substring(indiceDelUltimoEspacio + 1);
            Word.Range DNILOCADOR = ObjDoc.Bookmarks.get_Item(ref dniLocador).Range;
            DNILOCADOR.Text = datos_viejos[1];
            //TELEFONO
            indiceDelUltimoEspacio = datos_viejos[2].LastIndexOf(" ");//En este caso la variable tendra el indice en donde se encuentra el espacio que separa el numero de telefono con el tipo de telefono
            Word.Range TELEFONOLOCADOR = ObjDoc.Bookmarks.get_Item(ref telefonoLocador).Range;
            TELEFONOLOCADOR.Text = datos_viejos[2].Substring(0, indiceDelUltimoEspacio);
            Word.Range TIPOTELEFONO = ObjDoc.Bookmarks.get_Item(ref tipoTel).Range;
            TIPOTELEFONO.Text = datos_viejos[2].Substring(indiceDelUltimoEspacio + 1);
            Word.Range COSTEALQUILER = ObjDoc.Bookmarks.get_Item(ref costeAlquiler).Range;
            COSTEALQUILER.Text = datos_viejos[3];
            Word.Range OBSERVACIONES = ObjDoc.Bookmarks.get_Item(ref observaciones).Range;
            OBSERVACIONES.Text = datos_viejos[7];
            Word.Range HORARIOINGRESO = ObjDoc.Bookmarks.get_Item(ref horario_ingreso).Range;
            HORARIOINGRESO.Text = datos_viejos[5].Substring(0, 2) + ":" + datos_viejos[5].Substring(3, 2);
            Word.Range HORAEGRESO = ObjDoc.Bookmarks.get_Item(ref horario_egreso).Range;
            HORAEGRESO.Text = datos_viejos[6].Substring(0, 2) + ":" + datos_viejos[6].Substring(3, 2);

            object rango1 = FECHA;
            object rango2 = NOMBRELOCADOR;
            object rango3 = APELLIDOLOCADOR;
            object rango4 = DNILOCADOR;
            object rango5 = TELEFONOLOCADOR;
            object rango6 = TIPOTELEFONO;
            object rango7 = COSTEALQUILER;
            object rango8 = OBSERVACIONES;
            object rango9 = HORARIOINGRESO;
            object rango10 = HORAEGRESO;

            ObjDoc.Bookmarks.Add("fecha_alquilada", ref rango1);
            ObjDoc.Bookmarks.Add("nombre_locador", ref rango2);
            ObjDoc.Bookmarks.Add("apellido_locador", ref rango3);
            ObjDoc.Bookmarks.Add("dni_locador", ref rango4);
            ObjDoc.Bookmarks.Add("telefono_locador", ref rango5);
            ObjDoc.Bookmarks.Add("tipo_telefono", ref rango6);
            ObjDoc.Bookmarks.Add("coste_alquiler", ref rango7);
            ObjDoc.Bookmarks.Add("hora_ingreso", ref rango8);
            ObjDoc.Bookmarks.Add("hora_egreso", ref rango9);
            ObjDoc.Bookmarks.Add("observaciones", ref rango10);

            ObjWord.Visible = true;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        

        

        
        
    }
}
