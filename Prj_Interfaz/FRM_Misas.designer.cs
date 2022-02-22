namespace Prj_Interfaz
{
    partial class FRM_Misas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_MIS_agregar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.mtc_MIS = new System.Windows.Forms.MonthCalendar();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_MIS_intencion = new System.Windows.Forms.TextBox();
            this.cmb_MIS_hora = new System.Windows.Forms.ComboBox();
            this.cmb_MIS_minuto = new System.Windows.Forms.ComboBox();
            this.txt_MIS_nomPadre = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtp_MIS_fecha = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.cmb_MIS_intencion = new System.Windows.Forms.ComboBox();
            this.dgv_MIS = new System.Windows.Forms.DataGridView();
            this.btn_MIS_modificar = new System.Windows.Forms.Button();
            this.btn_MIS_borrar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_MIS_imprimir = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_MIS)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_MIS_agregar
            // 
            this.btn_MIS_agregar.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_MIS_agregar.Location = new System.Drawing.Point(461, 187);
            this.btn_MIS_agregar.Name = "btn_MIS_agregar";
            this.btn_MIS_agregar.Size = new System.Drawing.Size(150, 39);
            this.btn_MIS_agregar.TabIndex = 0;
            this.btn_MIS_agregar.Text = "Registrar";
            this.btn_MIS_agregar.UseVisualStyleBackColor = true;
            this.btn_MIS_agregar.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(243, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "Hora:";
            // 
            // mtc_MIS
            // 
            this.mtc_MIS.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.mtc_MIS.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtc_MIS.Location = new System.Drawing.Point(12, 6);
            this.mtc_MIS.MaxSelectionCount = 1;
            this.mtc_MIS.Name = "mtc_MIS";
            this.mtc_MIS.ShowToday = false;
            this.mtc_MIS.ShowTodayCircle = false;
            this.mtc_MIS.TabIndex = 2;
            this.mtc_MIS.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.mtc_MIS_DateChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(338, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 19);
            this.label2.TabIndex = 7;
            this.label2.Text = ":";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 19);
            this.label3.TabIndex = 12;
            this.label3.Text = "Intención:";
            // 
            // txt_MIS_intencion
            // 
            this.txt_MIS_intencion.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_MIS_intencion.Location = new System.Drawing.Point(172, 52);
            this.txt_MIS_intencion.MaxLength = 100;
            this.txt_MIS_intencion.Multiline = true;
            this.txt_MIS_intencion.Name = "txt_MIS_intencion";
            this.txt_MIS_intencion.Size = new System.Drawing.Size(71, 24);
            this.txt_MIS_intencion.TabIndex = 13;
            this.txt_MIS_intencion.Visible = false;
            // 
            // cmb_MIS_hora
            // 
            this.cmb_MIS_hora.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_MIS_hora.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_MIS_hora.FormattingEnabled = true;
            this.cmb_MIS_hora.Items.AddRange(new object[] {
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "00",
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07"});
            this.cmb_MIS_hora.Location = new System.Drawing.Point(299, 22);
            this.cmb_MIS_hora.Name = "cmb_MIS_hora";
            this.cmb_MIS_hora.Size = new System.Drawing.Size(39, 27);
            this.cmb_MIS_hora.TabIndex = 15;
            // 
            // cmb_MIS_minuto
            // 
            this.cmb_MIS_minuto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_MIS_minuto.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_MIS_minuto.FormattingEnabled = true;
            this.cmb_MIS_minuto.Items.AddRange(new object[] {
            "00",
            "30"});
            this.cmb_MIS_minuto.Location = new System.Drawing.Point(353, 22);
            this.cmb_MIS_minuto.Name = "cmb_MIS_minuto";
            this.cmb_MIS_minuto.Size = new System.Drawing.Size(39, 27);
            this.cmb_MIS_minuto.TabIndex = 16;
            // 
            // txt_MIS_nomPadre
            // 
            this.txt_MIS_nomPadre.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_MIS_nomPadre.Location = new System.Drawing.Point(299, 52);
            this.txt_MIS_nomPadre.MaxLength = 20;
            this.txt_MIS_nomPadre.Name = "txt_MIS_nomPadre";
            this.txt_MIS_nomPadre.Size = new System.Drawing.Size(117, 27);
            this.txt_MIS_nomPadre.TabIndex = 18;
            this.txt_MIS_nomPadre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_MIS_nomPadre_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(243, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 19);
            this.label4.TabIndex = 19;
            this.label4.Text = "Padre:";
            // 
            // dtp_MIS_fecha
            // 
            this.dtp_MIS_fecha.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_MIS_fecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_MIS_fecha.Location = new System.Drawing.Point(60, 21);
            this.dtp_MIS_fecha.Name = "dtp_MIS_fecha";
            this.dtp_MIS_fecha.Size = new System.Drawing.Size(183, 27);
            this.dtp_MIS_fecha.TabIndex = 20;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 19);
            this.label5.TabIndex = 21;
            this.label5.Text = "Fecha:";
            // 
            // cmb_MIS_intencion
            // 
            this.cmb_MIS_intencion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_MIS_intencion.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_MIS_intencion.FormattingEnabled = true;
            this.cmb_MIS_intencion.Items.AddRange(new object[] {
            "Difunto",
            "Salud",
            "Agradecimiento",
            "Otros..."});
            this.cmb_MIS_intencion.Location = new System.Drawing.Point(80, 52);
            this.cmb_MIS_intencion.Name = "cmb_MIS_intencion";
            this.cmb_MIS_intencion.Size = new System.Drawing.Size(89, 23);
            this.cmb_MIS_intencion.TabIndex = 22;
            this.cmb_MIS_intencion.SelectedIndexChanged += new System.EventHandler(this.cmb_MIS_intencion_SelectedIndexChanged);
            // 
            // dgv_MIS
            // 
            this.dgv_MIS.AllowUserToAddRows = false;
            this.dgv_MIS.AllowUserToDeleteRows = false;
            this.dgv_MIS.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_MIS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_MIS.Location = new System.Drawing.Point(272, 5);
            this.dgv_MIS.Name = "dgv_MIS";
            this.dgv_MIS.ReadOnly = true;
            this.dgv_MIS.RowHeadersVisible = false;
            this.dgv_MIS.Size = new System.Drawing.Size(496, 175);
            this.dgv_MIS.TabIndex = 3;
            this.dgv_MIS.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_MIS_CellDoubleClick);
            // 
            // btn_MIS_modificar
            // 
            this.btn_MIS_modificar.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_MIS_modificar.Location = new System.Drawing.Point(461, 234);
            this.btn_MIS_modificar.Name = "btn_MIS_modificar";
            this.btn_MIS_modificar.Size = new System.Drawing.Size(150, 29);
            this.btn_MIS_modificar.TabIndex = 23;
            this.btn_MIS_modificar.Text = "Modificar";
            this.btn_MIS_modificar.UseVisualStyleBackColor = true;
            this.btn_MIS_modificar.Click += new System.EventHandler(this.btn_MIS_modificar_Click);
            // 
            // btn_MIS_borrar
            // 
            this.btn_MIS_borrar.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_MIS_borrar.Location = new System.Drawing.Point(617, 234);
            this.btn_MIS_borrar.Name = "btn_MIS_borrar";
            this.btn_MIS_borrar.Size = new System.Drawing.Size(151, 29);
            this.btn_MIS_borrar.TabIndex = 24;
            this.btn_MIS_borrar.Text = "Borrar";
            this.btn_MIS_borrar.UseVisualStyleBackColor = true;
            this.btn_MIS_borrar.Click += new System.EventHandler(this.btn_MIS_borrar_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmb_MIS_intencion);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txt_MIS_intencion);
            this.groupBox1.Controls.Add(this.dtp_MIS_fecha);
            this.groupBox1.Controls.Add(this.cmb_MIS_hora);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cmb_MIS_minuto);
            this.groupBox1.Controls.Add(this.txt_MIS_nomPadre);
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 182);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(443, 82);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos:";
            // 
            // btn_MIS_imprimir
            // 
            this.btn_MIS_imprimir.Font = new System.Drawing.Font("Calibri", 12F);
            this.btn_MIS_imprimir.Location = new System.Drawing.Point(617, 187);
            this.btn_MIS_imprimir.Name = "btn_MIS_imprimir";
            this.btn_MIS_imprimir.Size = new System.Drawing.Size(150, 40);
            this.btn_MIS_imprimir.TabIndex = 26;
            this.btn_MIS_imprimir.Text = "Imprimir";
            this.btn_MIS_imprimir.UseVisualStyleBackColor = true;
            this.btn_MIS_imprimir.Click += new System.EventHandler(this.btn_MIS_imprimir_Click);
            // 
            // FRM_Misas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSkyBlue;
            this.ClientSize = new System.Drawing.Size(780, 267);
            this.Controls.Add(this.btn_MIS_imprimir);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_MIS_borrar);
            this.Controls.Add(this.btn_MIS_modificar);
            this.Controls.Add(this.dgv_MIS);
            this.Controls.Add(this.mtc_MIS);
            this.Controls.Add(this.btn_MIS_agregar);
            this.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FRM_Misas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Misas";
            this.Load += new System.EventHandler(this.Misas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_MIS)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_MIS_agregar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MonthCalendar mtc_MIS;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_MIS_intencion;
        private System.Windows.Forms.ComboBox cmb_MIS_hora;
        private System.Windows.Forms.ComboBox cmb_MIS_minuto;
        private System.Windows.Forms.TextBox txt_MIS_nomPadre;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtp_MIS_fecha;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmb_MIS_intencion;
        private System.Windows.Forms.DataGridView dgv_MIS;
        private System.Windows.Forms.Button btn_MIS_modificar;
        private System.Windows.Forms.Button btn_MIS_borrar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_MIS_imprimir;
    }
}