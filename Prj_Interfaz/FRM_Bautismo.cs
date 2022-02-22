using System;
using System.Windows.Forms;
using Prj_Trabajo;

namespace Prj_Interfaz
{
    public partial class FRM_Bautismo : Form
    {
        public FRM_Bautismo()
        {
            InitializeComponent();
        }
        //Instancia de la clase Missing para representar valores perdidos,
        //por ejemplo, métodos que tienen valores de parámetros predeterminados
        object ObjMiss = System.Reflection.Missing.Value;

        //creamos el objeto Word
        Word.Application ObjWord = new Word.Application();
        string ruta = Application.StartupPath + @"\Impresion(Bautismos).docx";
        Consultas_Bautismos Bautismos = new Consultas_Bautismos();

        private void FRM_Bautismo_Load(object sender, EventArgs e)
        {
            crearGrillaBAU(DateTime.Now.ToShortDateString());//Creamos la grilla en la fecha actual
        }
        private void mtc_BAU_DateSelected(object sender, DateRangeEventArgs e)
        {
            btn_BAU_registrar.Enabled = true;//Habilita el boton de agregar un evento
            btn_BAU_modificar.Enabled = false;//Deshabilita los botones de borrado y modificacion cuando se cambia la fecha del calendario
            btn_BAU_borrar.Enabled = false;
            btn_BAU_imprimir.Enabled = false;
            limpiarObjetos();//Limpiamos todos lo objetos cuando se cambia la seleccion del calendario

            crearGrillaBAU(e.Start.ToShortDateString());//Creamos la grilla en la fecha seleccionada
        }
        private void crearGrillaBAU(string fecha_filtro)//Metodo que crea la grilla del form pero requiere de un parametro que le de la fecha que va a ser utilizada como filtro para cargar la grilla
        {
            dgv_BAU.Columns.Clear();
            dgv_BAU.DataSource = Bautismos.lectura_bautismos(fecha_filtro);//Llamamos al metodo lectura_bautismos para que nos devuelva el DataTable con los datos necesarios para cargar el dgv
        }
        private void btn_BAU_registrar_Click(object sender, EventArgs e)
        {
            int verificadorDNI = 0;//Variable que servira para verificar si lo ingresado en el dni sea un numero

            if (cmb_BAU_hora.SelectedIndex == -1 || cmb_BAU_minuto.SelectedIndex == -1 || txt_BAU_nomBautizado.Text.Trim() == "" || txt_BAU_apeBautizado.Text.Trim() == "" || txt_BAU_dniBautizado.Text.Trim() == "" || txt_BAU_lugarNacimiento.Text.Trim() == "" ||  txt_BAU_nomPapa.Text.Trim() == "" || txt_BAU_apePapa.Text.Trim() == "" || txt_BAU_nacPapa.Text.Trim() == "" || txt_BAU_nomMama.Text.Trim() == "" || txt_BAU_apeMama.Text.Trim() == "" || txt_BAU_nacMama.Text.Trim() == "" || txt_BAU_nomPadrino1.Text.Trim() == "" || txt_BAU_nomPadrino2.Text.Trim() == "" || txt_BAU_nomMadrina1.Text.Trim() == "" || txt_BAU_nomMadrina2.Text.Trim() == "" || txt_BAU_nomPadre.Text.Trim() == "")//Verifica que cada uno de los objetos tenga un valor en su interior
                MessageBox.Show("No puede dejar espacios vacios!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                try
                {
                    verificadorDNI = Convert.ToInt32(txt_BAU_dniBautizado.Text);
                    //Mandamos los paramentro para que el metodo registre todo en la base de datos
                    Bautismos.registro_bautismo(dtp_BAU_fecha.Text, cmb_BAU_hora.Text + ":" + cmb_BAU_minuto.Text, txt_BAU_nomBautizado.Text, txt_BAU_apeBautizado.Text, Convert.ToInt32(txt_BAU_dniBautizado.Text), txt_BAU_lugarNacimiento.Text, dtp_BAU_fechaNacimiento.Text, txt_BAU_nomPapa.Text, txt_BAU_apePapa.Text, txt_BAU_nacPapa.Text, txt_BAU_nomMama.Text, txt_BAU_apeMama.Text, txt_BAU_nacMama.Text, txt_BAU_nomPadrino1.Text, txt_BAU_nomPadrino2.Text, txt_BAU_nomMadrina1.Text, txt_BAU_nomMadrina2.Text, txt_BAU_nomPadre.Text);
                    MessageBox.Show("Bautismo agregada correctamente!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    crearGrillaBAU(DateTime.Now.ToShortDateString());
                
                }
                catch //Ocurre cuando no se ingreso un numero en el dni
                {
                    MessageBox.Show("Debe ingresar un numero entero como dni", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btn_BAU_modificar_Click(object sender, EventArgs e)
        {
            if (cmb_BAU_hora.SelectedIndex == -1 || cmb_BAU_minuto.SelectedIndex == -1 || txt_BAU_nomBautizado.Text.Trim() == "" || txt_BAU_apeBautizado.Text.Trim() == "" || txt_BAU_dniBautizado.Text.Trim() == "" || txt_BAU_lugarNacimiento.Text.Trim() == "" || txt_BAU_nomPapa.Text.Trim() == "" || txt_BAU_apePapa.Text.Trim() == "" || txt_BAU_nacPapa.Text.Trim() == "" || txt_BAU_nomMama.Text.Trim() == "" || txt_BAU_apeMama.Text.Trim() == "" || txt_BAU_nacMama.Text.Trim() == "" || txt_BAU_nomPadrino1.Text.Trim() == "" || txt_BAU_nomPadrino2.Text.Trim() == "" || txt_BAU_nomMadrina1.Text.Trim() == "" || txt_BAU_nomMadrina2.Text.Trim() == "" || txt_BAU_nomPadre.Text.Trim() == "")//Verifica que cada uno de los objetos tenga un valor en su interior
                MessageBox.Show("No puede dejar espacios vacios!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                DialogResult pregunta = MessageBox.Show("Esta seguro que desea modificar el evento?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (pregunta == DialogResult.Yes)
                {
                    //Mandamos todos los parametros al metodo para que actualice
                    Bautismos.act_bautismo(datos_viejos[0], datos_viejos[1], dtp_BAU_fecha.Text, cmb_BAU_hora.Text + ":" + cmb_BAU_minuto.Text, txt_BAU_nomBautizado.Text, txt_BAU_apeBautizado.Text, Convert.ToInt32(txt_BAU_dniBautizado.Text), txt_BAU_lugarNacimiento.Text, dtp_BAU_fechaNacimiento.Text, txt_BAU_nomPapa.Text, txt_BAU_apePapa.Text, txt_BAU_nacPapa.Text, txt_BAU_nomMama.Text, txt_BAU_apeMama.Text, txt_BAU_nacMama.Text, txt_BAU_nomPadrino1.Text, txt_BAU_nomPadrino2.Text, txt_BAU_nomMadrina1.Text, txt_BAU_nomMadrina2.Text, txt_BAU_nomPadre.Text);
                    crearGrillaBAU(DateTime.Now.ToShortDateString());//Cargamos nuevamente la grilla del form para que de sensacion de refresco
                    limpiarObjetos();
                    MessageBox.Show("Bautismo modificada correctamente!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    btn_BAU_registrar.Enabled = true;//Habilita el boton de agregar un evento
                    btn_BAU_modificar.Enabled = false;//Deshabilita los botones de borrado y modificacion cuando se cambia la fecha del calendario
                    btn_BAU_borrar.Enabled = false;
                    btn_BAU_imprimir.Enabled = false;
                }
            }
        }
        private void btn_BAU_borrar_Click(object sender, EventArgs e)
        {
            DialogResult pregunta = MessageBox.Show("Esta seguro que desea borrar el evento?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (pregunta == DialogResult.Yes)
            {
                //Mandamos todos los parametros al metodo para que borre 
                Bautismos.borrar_bautismo(datos_viejos[0], datos_viejos[1]);
                crearGrillaBAU(DateTime.Now.ToShortDateString());//Cargamos nuevamente la grilla del form para que de sensacion de refresco
                limpiarObjetos();
                MessageBox.Show("Bautismo borrado correctamente!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                btn_BAU_registrar.Enabled = true;//Habilita el boton de agregar un evento
                btn_BAU_modificar.Enabled = false;//Deshabilita los botones de borrado y modificacion cuando se cambia la fecha del calendario
                btn_BAU_borrar.Enabled = false;
                btn_BAU_imprimir.Enabled = false;
            }
        }
        private void limpiarObjetos()//Metodo que volvera a todos los objetos a su estado inicial
        {
            //Fecha y hora
            dtp_BAU_fecha.Text = DateTime.Now.ToShortDateString();
            cmb_BAU_hora.SelectedItem = null;
            cmb_BAU_minuto.SelectedItem = null;
            //Bautisado
            txt_BAU_nomBautizado.Text = "";
            txt_BAU_apeBautizado.Text = "";
            txt_BAU_dniBautizado.Text = "";
            txt_BAU_lugarNacimiento.Text = "";
            //Papa
            txt_BAU_nomPapa.Text = "";
            txt_BAU_apePapa.Text = "";
            txt_BAU_nacPapa.Text = "";
            //Mama
            txt_BAU_nomMama.Text = "";
            txt_BAU_apeMama.Text = "";
            txt_BAU_nacMama.Text = "";
            //Cura
            txt_BAU_nomPadre.Text = "";
            //Padrinos
            txt_BAU_nomPadrino1.Text = "";
            txt_BAU_nomPadrino2.Text = "";
            //Madrinas
            txt_BAU_nomMadrina1.Text = "";
            txt_BAU_nomMadrina2.Text = "";
        }
        string[] datos_viejos;

        private void dgv_BAU_CellDoubleClick(object sender, DataGridViewCellEventArgs e)//Evento que utilizaremos para cargar los objetos como los txt y el resto, con lo seleccionado en la fila de la grilla
        {
            try
            {    
                datos_viejos = new string[dgv_BAU.Columns.Count]; //Array que tendra en cada una de sus posiciones los valores de la fila de la grilla que se haya seleccionado
                for (int i = 0; i < datos_viejos.Length; i++)//Bucle que nos sirve para cargar el array con todos los datos de la fila seleccionada
                    datos_viejos[i] = dgv_BAU.Rows[e.RowIndex].Cells[i].Value.ToString();//Cada posicion del array representara cada columna de la grilla, es decir que tendra el mismo dato que hay en la columna pero obviamente solo de la fila seleccionada

                //En la siguiente porcion de codigo cargaremos todos los objetos con los datos que estan en la fila seleccionada de la grilla

                //FECHA Y HORA
                dtp_BAU_fecha.Text = datos_viejos[0];//Carga exactamente la fecha. Utiliza un subString ya que lo que esta cargado en el array es exactamente igual a lo que esta en la grilla y como en la grilla tiene la fecha y hora juntas se utiliza el subString para agarrar solo la fecha
                cmb_BAU_hora.Text = datos_viejos[1].Substring(0, 2);
                cmb_BAU_minuto.Text = datos_viejos[1].Substring(3, 2);
                //BAUTIZADO
                int indiceDelUltimoEspacio = datos_viejos[2].LastIndexOf(" ");//Esta variable contendra el indice (posicion) del ultimo espacio dentro de la columna nombre y apellido (que esta en la posicion 1 del array datos_viejos). Este se utilizara para poder agarra el apellido y el nombre por separado (SOLO CUANDO SE ESCRIBE UN NOMBRE Y UN APELLIDO O DOS NOMBRES Y UN APELLIDO, CUALQUIER OTRA COMBINACION LOS SEPARARA DE MALA MANERA)
                txt_BAU_nomBautizado.Text = datos_viejos[2].Substring(0, indiceDelUltimoEspacio);//Se utiliza el indice del ultimo espacio para agarra la longitud del nombre, ya que ambos coiciden
                txt_BAU_apeBautizado.Text = datos_viejos[2].Substring(indiceDelUltimoEspacio + 1);//Agarra todo desde el siguiente caracter del ultimo espacio
                txt_BAU_dniBautizado.Text = datos_viejos[3];
                txt_BAU_lugarNacimiento.Text = datos_viejos[4];
                dtp_BAU_fechaNacimiento.Text = datos_viejos[5];
                //PAPA
                indiceDelUltimoEspacio = datos_viejos[6].LastIndexOf(" ");//Mismo que antes pero con el papa
                txt_BAU_nomPapa.Text = datos_viejos[6].Substring(0, indiceDelUltimoEspacio);
                txt_BAU_apePapa.Text = datos_viejos[6].Substring(indiceDelUltimoEspacio + 1);
                txt_BAU_nacPapa.Text = datos_viejos[7];
                //MAMA
                indiceDelUltimoEspacio = datos_viejos[8].LastIndexOf(" ");
                txt_BAU_nomMama.Text = datos_viejos[8].Substring(0, indiceDelUltimoEspacio);
                txt_BAU_apeMama.Text = datos_viejos[8].Substring(indiceDelUltimoEspacio + 1);
                txt_BAU_nacMama.Text = datos_viejos[9];
                //PADRINOS
                txt_BAU_nomPadrino1.Text = datos_viejos[10];
                txt_BAU_nomPadrino2.Text = datos_viejos[11];
                //MADRINAS
                txt_BAU_nomMadrina1.Text = datos_viejos[12];
                txt_BAU_nomMadrina2.Text = datos_viejos[13];
                //CURA
                txt_BAU_nomPadre.Text = datos_viejos[14];

                btn_BAU_registrar.Enabled = false;//Deshabilita el boton de agregar un evento
                btn_BAU_borrar.Enabled = true;//Habilitara los botones de modificacion y borrado solo cuando haga doble click en la grilla del form
                btn_BAU_modificar.Enabled = true;
                btn_BAU_imprimir.Enabled = true;
            }
            catch
            {
            }
        }

        private void btn_BAU_imprimir_Click(object sender, EventArgs e)
        {


            object ObjMiss = System.Reflection.Missing.Value;
            Word.Application ObjWord = new Word.Application();
            object parametro = ruta;
            object fecha = "fecha_bautismo";
            object horario = "hora_bautismo";
            object nombreBautizado = "nombre_niño";
            object apellidoBautizado = "apellido_niño";
            object dniBautizado = "dni_niño";
            object lugNacimiento = "lug_nac";
            object fechaNacimiento = "fecha_nac";
            object nombrePapa = "nombre_papa";
            object nacionalidadPapa = "nacionalidad_papa";
            object nombreMama = "nombre_mama";
            object nacionalidadMama = "nacionalidad_mama";
            object nombrePadrino1 = "nombre_padrino1";
            object nombrePadrino2 = "nombre_padrino2";
            object nombreMadrina1 = "nombre_madrina1";
            object nombreMadrina2 = "nombre_madrina2";
            object nombrePadre = "nombre_padre";
            
            Word.Document ObjDoc = ObjWord.Documents.Open(ref parametro, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss, ref ObjMiss);
            Word.Range FECHA = ObjDoc.Bookmarks.get_Item(ref fecha).Range;
            FECHA.Text = datos_viejos[0];
            Word.Range HORARIO = ObjDoc.Bookmarks.get_Item(ref horario).Range;
            HORARIO.Text = datos_viejos[1];
            //BAUTIZADO
            int indiceDelUltimoEspacio = datos_viejos[2].LastIndexOf(" ");//Esta variable contendra el indice (posicion) del ultimo espacio dentro de la columna nombre y apellido (que esta en la posicion 1 del array datos_viejos). Este se utilizara para poder agarra el apellido y el nombre por separado (SOLO CUANDO SE ESCRIBE UN NOMBRE Y UN APELLIDO O DOS NOMBRES Y UN APELLIDO, CUALQUIER OTRA COMBINACION LOS SEPARARA DE MALA MANERA)
            Word.Range NOMBREBAUTIZADO = ObjDoc.Bookmarks.get_Item(ref nombreBautizado).Range;
            NOMBREBAUTIZADO.Text = datos_viejos[2].Substring(0, indiceDelUltimoEspacio);
            Word.Range APEBAUTIZADO = ObjDoc.Bookmarks.get_Item(ref apellidoBautizado).Range;
            APEBAUTIZADO.Text = datos_viejos[2].Substring(indiceDelUltimoEspacio + 1);
            Word.Range DNIBAUTIZADO = ObjDoc.Bookmarks.get_Item(ref dniBautizado).Range;
            DNIBAUTIZADO.Text = datos_viejos[3];
            Word.Range LUGNACIMIENTO = ObjDoc.Bookmarks.get_Item(ref lugNacimiento).Range;
            LUGNACIMIENTO.Text = datos_viejos[4];
            Word.Range FECHANACIMIENTO = ObjDoc.Bookmarks.get_Item(ref fechaNacimiento).Range;
            FECHANACIMIENTO.Text = datos_viejos[5];
            //PAPA
            Word.Range NOMPAPA= ObjDoc.Bookmarks.get_Item(ref nombrePapa).Range;
            NOMPAPA.Text = datos_viejos[6];
            Word.Range NACPAPA= ObjDoc.Bookmarks.get_Item(ref nacionalidadPapa).Range;
            NACPAPA.Text = datos_viejos[7];
            //MAMA
            Word.Range NOMMAMA = ObjDoc.Bookmarks.get_Item(ref nombreMama).Range;
            NOMMAMA.Text = datos_viejos[8];;
            Word.Range NACMAMA = ObjDoc.Bookmarks.get_Item(ref nacionalidadMama).Range;
            NACMAMA.Text = datos_viejos[9];
            Word.Range NOMPADRINO1 = ObjDoc.Bookmarks.get_Item(ref nombrePadrino1).Range;
            NOMPADRINO1.Text = datos_viejos[10];
            Word.Range NOMPADRINO2 = ObjDoc.Bookmarks.get_Item(ref nombrePadrino2).Range;
            NOMPADRINO2.Text = datos_viejos[11];
            Word.Range NOMMADRINA1 = ObjDoc.Bookmarks.get_Item(ref nombreMadrina1).Range;
            NOMMADRINA1.Text = datos_viejos[12];
            Word.Range NOMMADRINA2 = ObjDoc.Bookmarks.get_Item(ref nombreMadrina2).Range;
            NOMMADRINA2.Text = datos_viejos[13];
            Word.Range NOMPADRE = ObjDoc.Bookmarks.get_Item(ref nombrePadre).Range;
            NOMPADRE.Text = datos_viejos[14];

            object rango1 = FECHA;
            object rango2 = HORARIO;
            object rango3 = NOMBREBAUTIZADO;
            object rango4 = APEBAUTIZADO;
            object rango5 = DNIBAUTIZADO;
            object rango6 = LUGNACIMIENTO;
            object rango7 = FECHANACIMIENTO;
            object rango8 = NOMPAPA;
            object rango9 = NACPAPA;
            object rango10 = NOMMAMA;
            object rango11 = NACMAMA;
            object rango12 = NOMPADRINO1;
            object rango13 = NOMPADRINO2;
            object rango14 = NOMMADRINA1;
            object rango15 = NOMMADRINA2;
            object rango16 = NOMPADRE;

            ObjDoc.Bookmarks.Add("fecha_bautismo", ref rango1);
            ObjDoc.Bookmarks.Add("hora_bautismo", ref rango2);
            ObjDoc.Bookmarks.Add("nombre_niño", ref rango3);
            ObjDoc.Bookmarks.Add("apellido_niño", ref rango4);
            ObjDoc.Bookmarks.Add("dni_niño", ref rango5);
            ObjDoc.Bookmarks.Add("lug_nac", ref rango6);
            ObjDoc.Bookmarks.Add("fecha_nac", ref rango7);
            ObjDoc.Bookmarks.Add("nombre_papa", ref rango8);
            ObjDoc.Bookmarks.Add("nacionalidad_papa", ref rango9);
            ObjDoc.Bookmarks.Add("nombre_mama", ref rango10);
            ObjDoc.Bookmarks.Add("nacionalidad_mama", ref rango11);
            ObjDoc.Bookmarks.Add("nombre_padrino1", ref rango12);
            ObjDoc.Bookmarks.Add("nombre_padrino2", ref rango13);
            ObjDoc.Bookmarks.Add("nombre_madrina1", ref rango14);
            ObjDoc.Bookmarks.Add("nombre_madrina2", ref rango15);
            ObjDoc.Bookmarks.Add("nombre_padre", ref rango16);

            ObjWord.Visible = true;
        }

        private void txt_BAU_nomPapa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void dgv_BAU_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        

        

        
    }
}
