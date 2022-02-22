namespace Prj_Interfaz
{
    partial class FRM_Comuniones
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
            this.btn_COM_aceptar = new System.Windows.Forms.Button();
            this.mtc_COM = new System.Windows.Forms.MonthCalendar();
            this.dgv_COM = new System.Windows.Forms.DataGridView();
            this.txt_COM_nomCatesista = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_COM_grupo = new System.Windows.Forms.TextBox();
            this.btn_COM_modificar = new System.Windows.Forms.Button();
            this.cmb_COM_minuto = new System.Windows.Forms.ComboBox();
            this.cmb_COM_hora = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_COM_nomPadre = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_COM_cantChicos = new System.Windows.Forms.TextBox();
            this.dtp_COM_fecha = new System.Windows.Forms.DateTimePicker();
            this.btn_COM_borrar = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_COM_imprimir = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_COM)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_COM_aceptar
            // 
            this.btn_COM_aceptar.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_COM_aceptar.Location = new System.Drawing.Point(739, 192);
            this.btn_COM_aceptar.Name = "btn_COM_aceptar";
            this.btn_COM_aceptar.Size = new System.Drawing.Size(106, 49);
            this.btn_COM_aceptar.TabIndex = 0;
            this.btn_COM_aceptar.Text = "Registrar";
            this.btn_COM_aceptar.UseVisualStyleBackColor = true;
            this.btn_COM_aceptar.Click += new System.EventHandler(this.button1_Click);
            // 
            // mtc_COM
            // 
            this.mtc_COM.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtc_COM.Location = new System.Drawing.Point(18, 16);
            this.mtc_COM.MaxSelectionCount = 1;
            this.mtc_COM.Name = "mtc_COM";
            this.mtc_COM.ShowToday = false;
            this.mtc_COM.ShowTodayCircle = false;
            this.mtc_COM.TabIndex = 1;
            this.mtc_COM.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.mtc_COM_DateChanged);
            // 
            // dgv_COM
            // 
            this.dgv_COM.AllowUserToAddRows = false;
            this.dgv_COM.AllowUserToDeleteRows = false;
            this.dgv_COM.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_COM.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_COM.Location = new System.Drawing.Point(278, 9);
            this.dgv_COM.Name = "dgv_COM";
            this.dgv_COM.ReadOnly = true;
            this.dgv_COM.RowHeadersVisible = false;
            this.dgv_COM.Size = new System.Drawing.Size(677, 175);
            this.dgv_COM.TabIndex = 2;
            this.dgv_COM.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_COM_CellContentClick);
            this.dgv_COM.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_COM_CellDoubleClick);
            // 
            // txt_COM_nomCatesista
            // 
            this.txt_COM_nomCatesista.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_COM_nomCatesista.Location = new System.Drawing.Point(242, 44);
            this.txt_COM_nomCatesista.MaxLength = 20;
            this.txt_COM_nomCatesista.Name = "txt_COM_nomCatesista";
            this.txt_COM_nomCatesista.Size = new System.Drawing.Size(91, 27);
            this.txt_COM_nomCatesista.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(238, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 19);
            this.label1.TabIndex = 5;
            this.label1.Text = "Nom. Catequista";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(371, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 19);
            this.label4.TabIndex = 10;
            this.label4.Text = "Grupo:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // txt_COM_grupo
            // 
            this.txt_COM_grupo.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_COM_grupo.Location = new System.Drawing.Point(375, 44);
            this.txt_COM_grupo.MaxLength = 10;
            this.txt_COM_grupo.Name = "txt_COM_grupo";
            this.txt_COM_grupo.Size = new System.Drawing.Size(71, 27);
            this.txt_COM_grupo.TabIndex = 9;
            // 
            // btn_COM_modificar
            // 
            this.btn_COM_modificar.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_COM_modificar.Location = new System.Drawing.Point(739, 247);
            this.btn_COM_modificar.Name = "btn_COM_modificar";
            this.btn_COM_modificar.Size = new System.Drawing.Size(106, 30);
            this.btn_COM_modificar.TabIndex = 14;
            this.btn_COM_modificar.Text = "Modificar";
            this.btn_COM_modificar.UseVisualStyleBackColor = true;
            this.btn_COM_modificar.Click += new System.EventHandler(this.button2_Click);
            // 
            // cmb_COM_minuto
            // 
            this.cmb_COM_minuto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_COM_minuto.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_COM_minuto.FormattingEnabled = true;
            this.cmb_COM_minuto.Items.AddRange(new object[] {
            "00",
            "30"});
            this.cmb_COM_minuto.Location = new System.Drawing.Point(184, 43);
            this.cmb_COM_minuto.Name = "cmb_COM_minuto";
            this.cmb_COM_minuto.Size = new System.Drawing.Size(39, 27);
            this.cmb_COM_minuto.TabIndex = 20;
            // 
            // cmb_COM_hora
            // 
            this.cmb_COM_hora.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_COM_hora.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_COM_hora.FormattingEnabled = true;
            this.cmb_COM_hora.Items.AddRange(new object[] {
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
            "07",
            "08"});
            this.cmb_COM_hora.Location = new System.Drawing.Point(133, 43);
            this.cmb_COM_hora.Name = "cmb_COM_hora";
            this.cmb_COM_hora.Size = new System.Drawing.Size(39, 27);
            this.cmb_COM_hora.TabIndex = 19;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(171, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(13, 19);
            this.label5.TabIndex = 18;
            this.label5.Text = ":";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(129, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 19);
            this.label6.TabIndex = 17;
            this.label6.Text = "Hora:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(565, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 19);
            this.label3.TabIndex = 24;
            this.label3.Text = "Nombre Cura";
            // 
            // txt_COM_nomPadre
            // 
            this.txt_COM_nomPadre.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_COM_nomPadre.Location = new System.Drawing.Point(568, 44);
            this.txt_COM_nomPadre.MaxLength = 20;
            this.txt_COM_nomPadre.Name = "txt_COM_nomPadre";
            this.txt_COM_nomPadre.Size = new System.Drawing.Size(91, 27);
            this.txt_COM_nomPadre.TabIndex = 23;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(5, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 19);
            this.label7.TabIndex = 26;
            this.label7.Text = "Fecha";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(467, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 19);
            this.label8.TabIndex = 28;
            this.label8.Text = "Cant. chicos";
            // 
            // txt_COM_cantChicos
            // 
            this.txt_COM_cantChicos.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_COM_cantChicos.Location = new System.Drawing.Point(471, 44);
            this.txt_COM_cantChicos.MaxLength = 2;
            this.txt_COM_cantChicos.Name = "txt_COM_cantChicos";
            this.txt_COM_cantChicos.Size = new System.Drawing.Size(70, 27);
            this.txt_COM_cantChicos.TabIndex = 27;
            // 
            // dtp_COM_fecha
            // 
            this.dtp_COM_fecha.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_COM_fecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_COM_fecha.Location = new System.Drawing.Point(9, 42);
            this.dtp_COM_fecha.Name = "dtp_COM_fecha";
            this.dtp_COM_fecha.Size = new System.Drawing.Size(108, 27);
            this.dtp_COM_fecha.TabIndex = 29;
            // 
            // btn_COM_borrar
            // 
            this.btn_COM_borrar.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_COM_borrar.Location = new System.Drawing.Point(851, 247);
            this.btn_COM_borrar.Name = "btn_COM_borrar";
            this.btn_COM_borrar.Size = new System.Drawing.Size(104, 30);
            this.btn_COM_borrar.TabIndex = 30;
            this.btn_COM_borrar.Text = "Eliminar";
            this.btn_COM_borrar.UseVisualStyleBackColor = true;
            this.btn_COM_borrar.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.dtp_COM_fecha);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txt_COM_nomPadre);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txt_COM_cantChicos);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cmb_COM_hora);
            this.groupBox1.Controls.Add(this.cmb_COM_minuto);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txt_COM_grupo);
            this.groupBox1.Controls.Add(this.txt_COM_nomCatesista);
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 12F);
            this.groupBox1.Location = new System.Drawing.Point(18, 192);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(715, 85);
            this.groupBox1.TabIndex = 31;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos";
            // 
            // btn_COM_imprimir
            // 
            this.btn_COM_imprimir.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_COM_imprimir.Location = new System.Drawing.Point(849, 192);
            this.btn_COM_imprimir.Name = "btn_COM_imprimir";
            this.btn_COM_imprimir.Size = new System.Drawing.Size(106, 49);
            this.btn_COM_imprimir.TabIndex = 32;
            this.btn_COM_imprimir.Text = "Imprimir";
            this.btn_COM_imprimir.UseVisualStyleBackColor = true;
            this.btn_COM_imprimir.Click += new System.EventHandler(this.btn_COM_imprimir_Click);
            // 
            // FRM_Comuniones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Peru;
            this.ClientSize = new System.Drawing.Size(967, 282);
            this.Controls.Add(this.btn_COM_imprimir);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_COM_borrar);
            this.Controls.Add(this.btn_COM_modificar);
            this.Controls.Add(this.dgv_COM);
            this.Controls.Add(this.mtc_COM);
            this.Controls.Add(this.btn_COM_aceptar);
            this.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FRM_Comuniones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Comuniones";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_COM)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_COM_aceptar;
        private System.Windows.Forms.MonthCalendar mtc_COM;
        private System.Windows.Forms.DataGridView dgv_COM;
        private System.Windows.Forms.TextBox txt_COM_nomCatesista;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_COM_grupo;
        private System.Windows.Forms.Button btn_COM_modificar;
        private System.Windows.Forms.ComboBox cmb_COM_minuto;
        private System.Windows.Forms.ComboBox cmb_COM_hora;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_COM_nomPadre;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_COM_cantChicos;
        private System.Windows.Forms.DateTimePicker dtp_COM_fecha;
        private System.Windows.Forms.Button btn_COM_borrar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_COM_imprimir;
    }
}

