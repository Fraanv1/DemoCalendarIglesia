namespace Prj_Interfaz
{
    partial class FRM_Principal
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
            this.components = new System.ComponentModel.Container();
            this.mtc_PRI = new System.Windows.Forms.MonthCalendar();
            this.dgv_PRI_colorActividad = new System.Windows.Forms.DataGridView();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.agregarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lbl_fechaSeleccionada = new System.Windows.Forms.Label();
            this.dgv_PRI_detalleActividad = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.sacramentosToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.bautismosToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.bodasToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.comunionesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.responsosToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.misasToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_PRI_colorActividad)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_PRI_detalleActividad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // mtc_PRI
            // 
            this.mtc_PRI.Font = new System.Drawing.Font("Calibri", 11F);
            this.mtc_PRI.Location = new System.Drawing.Point(12, 88);
            this.mtc_PRI.MaxSelectionCount = 1;
            this.mtc_PRI.Name = "mtc_PRI";
            this.mtc_PRI.ShowTodayCircle = false;
            this.mtc_PRI.TabIndex = 0;
            this.mtc_PRI.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.mtc_PRI_DateSelected);
            // 
            // dgv_PRI_colorActividad
            // 
            this.dgv_PRI_colorActividad.AllowUserToAddRows = false;
            this.dgv_PRI_colorActividad.AllowUserToDeleteRows = false;
            this.dgv_PRI_colorActividad.AllowUserToResizeColumns = false;
            this.dgv_PRI_colorActividad.AllowUserToResizeRows = false;
            this.dgv_PRI_colorActividad.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_PRI_colorActividad.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_PRI_colorActividad.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column6,
            this.Column2,
            this.Column1,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column7});
            this.dgv_PRI_colorActividad.Location = new System.Drawing.Point(272, 33);
            this.dgv_PRI_colorActividad.Name = "dgv_PRI_colorActividad";
            this.dgv_PRI_colorActividad.ReadOnly = true;
            this.dgv_PRI_colorActividad.RowHeadersVisible = false;
            this.dgv_PRI_colorActividad.Size = new System.Drawing.Size(732, 381);
            this.dgv_PRI_colorActividad.TabIndex = 1;
            this.dgv_PRI_colorActividad.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_principal_CellDoubleClick);
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Hora";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "SUM";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Bautismo";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Bodas";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Comuniones";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Misas";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Responsos";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.LightGray;
            this.menuStrip1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.agregarToolStripMenuItem,
            this.sacramentosToolStripMenuItem1,
            this.misasToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1025, 27);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // agregarToolStripMenuItem
            // 
            this.agregarToolStripMenuItem.Name = "agregarToolStripMenuItem";
            this.agregarToolStripMenuItem.Size = new System.Drawing.Size(51, 23);
            this.agregarToolStripMenuItem.Text = "SUM";
            this.agregarToolStripMenuItem.Click += new System.EventHandler(this.agregarToolStripMenuItem_Click);
            // 
            // lbl_fechaSeleccionada
            // 
            this.lbl_fechaSeleccionada.AutoSize = true;
            this.lbl_fechaSeleccionada.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_fechaSeleccionada.Location = new System.Drawing.Point(161, 60);
            this.lbl_fechaSeleccionada.Name = "lbl_fechaSeleccionada";
            this.lbl_fechaSeleccionada.Size = new System.Drawing.Size(48, 19);
            this.lbl_fechaSeleccionada.TabIndex = 4;
            this.lbl_fechaSeleccionada.Text = "Fecha";
            // 
            // dgv_PRI_detalleActividad
            // 
            this.dgv_PRI_detalleActividad.AllowUserToAddRows = false;
            this.dgv_PRI_detalleActividad.AllowUserToDeleteRows = false;
            this.dgv_PRI_detalleActividad.AllowUserToResizeColumns = false;
            this.dgv_PRI_detalleActividad.AllowUserToResizeRows = false;
            this.dgv_PRI_detalleActividad.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_PRI_detalleActividad.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_PRI_detalleActividad.Location = new System.Drawing.Point(12, 420);
            this.dgv_PRI_detalleActividad.Name = "dgv_PRI_detalleActividad";
            this.dgv_PRI_detalleActividad.ReadOnly = true;
            this.dgv_PRI_detalleActividad.RowHeadersVisible = false;
            this.dgv_PRI_detalleActividad.Size = new System.Drawing.Size(992, 153);
            this.dgv_PRI_detalleActividad.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(24, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 19);
            this.label2.TabIndex = 6;
            this.label2.Text = "Fecha seleccionada:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackgroundImage = global::Programa_Iglesia.Properties.Resources._5a018f9d7ca233f48ba6271a;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(28, 279);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(220, 135);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // sacramentosToolStripMenuItem1
            // 
            this.sacramentosToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bautismosToolStripMenuItem1,
            this.bodasToolStripMenuItem1,
            this.comunionesToolStripMenuItem1,
            this.responsosToolStripMenuItem1});
            this.sacramentosToolStripMenuItem1.Name = "sacramentosToolStripMenuItem1";
            this.sacramentosToolStripMenuItem1.Size = new System.Drawing.Size(104, 23);
            this.sacramentosToolStripMenuItem1.Text = "Sacramentos";
            // 
            // bautismosToolStripMenuItem1
            // 
            this.bautismosToolStripMenuItem1.Name = "bautismosToolStripMenuItem1";
            this.bautismosToolStripMenuItem1.Size = new System.Drawing.Size(158, 24);
            this.bautismosToolStripMenuItem1.Text = "Bautismos";
            this.bautismosToolStripMenuItem1.Click += new System.EventHandler(this.bautismosToolStripMenuItem1_Click);
            // 
            // bodasToolStripMenuItem1
            // 
            this.bodasToolStripMenuItem1.Name = "bodasToolStripMenuItem1";
            this.bodasToolStripMenuItem1.Size = new System.Drawing.Size(158, 24);
            this.bodasToolStripMenuItem1.Text = "Bodas";
            this.bodasToolStripMenuItem1.Click += new System.EventHandler(this.bodasToolStripMenuItem1_Click);
            // 
            // comunionesToolStripMenuItem1
            // 
            this.comunionesToolStripMenuItem1.Name = "comunionesToolStripMenuItem1";
            this.comunionesToolStripMenuItem1.Size = new System.Drawing.Size(158, 24);
            this.comunionesToolStripMenuItem1.Text = "Comuniones";
            this.comunionesToolStripMenuItem1.Click += new System.EventHandler(this.comunionesToolStripMenuItem1_Click);
            // 
            // responsosToolStripMenuItem1
            // 
            this.responsosToolStripMenuItem1.Name = "responsosToolStripMenuItem1";
            this.responsosToolStripMenuItem1.Size = new System.Drawing.Size(158, 24);
            this.responsosToolStripMenuItem1.Text = "Responsos";
            this.responsosToolStripMenuItem1.Click += new System.EventHandler(this.responsosToolStripMenuItem1_Click);
            // 
            // misasToolStripMenuItem1
            // 
            this.misasToolStripMenuItem1.Name = "misasToolStripMenuItem1";
            this.misasToolStripMenuItem1.Size = new System.Drawing.Size(60, 23);
            this.misasToolStripMenuItem1.Text = "Misas";
            this.misasToolStripMenuItem1.Click += new System.EventHandler(this.misasToolStripMenuItem1_Click);
            // 
            // FRM_Principal
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1025, 585);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgv_PRI_detalleActividad);
            this.Controls.Add(this.lbl_fechaSeleccionada);
            this.Controls.Add(this.dgv_PRI_colorActividad);
            this.Controls.Add(this.mtc_PRI);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "FRM_Principal";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Iglesia";
            this.Load += new System.EventHandler(this.FRM_Principal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_PRI_colorActividad)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_PRI_detalleActividad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MonthCalendar mtc_PRI;
        private System.Windows.Forms.DataGridView dgv_PRI_colorActividad;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem agregarToolStripMenuItem;
        private System.Windows.Forms.Label lbl_fechaSeleccionada;
        private System.Windows.Forms.DataGridView dgv_PRI_detalleActividad;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem sacramentosToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem bautismosToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem bodasToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem comunionesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem responsosToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem misasToolStripMenuItem1;
    }
}

