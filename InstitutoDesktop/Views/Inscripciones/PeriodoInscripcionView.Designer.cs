namespace InstitutoDesktop.Views.Inscripciones
{
    partial class PeriodoInscripcionView
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
            label1 = new Label();
            chInscripcionActiva = new CheckBox();
            cboNombres = new ComboBox();
            cboCiclosLectivos = new ComboBox();
            label2 = new Label();
            chMaterias2doCuatrimestre = new CheckBox();
            LabelTitulo = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(27, 125);
            label1.Name = "label1";
            label1.Size = new Size(67, 20);
            label1.TabIndex = 0;
            label1.Text = "Nombre:";
            // 
            // chInscripcionActiva
            // 
            chInscripcionActiva.AutoSize = true;
            chInscripcionActiva.Location = new Point(100, 239);
            chInscripcionActiva.Name = "chInscripcionActiva";
            chInscripcionActiva.Size = new Size(147, 24);
            chInscripcionActiva.TabIndex = 1;
            chInscripcionActiva.Text = "Inscripcion Activa";
            chInscripcionActiva.UseVisualStyleBackColor = true;
            // 
            // cboNombres
            // 
            cboNombres.FormattingEnabled = true;
            cboNombres.Location = new Point(100, 117);
            cboNombres.Name = "cboNombres";
            cboNombres.Size = new Size(231, 28);
            cboNombres.TabIndex = 2;
            // 
            // cboCiclosLectivos
            // 
            cboCiclosLectivos.FormattingEnabled = true;
            cboCiclosLectivos.Location = new Point(515, 117);
            cboCiclosLectivos.Name = "cboCiclosLectivos";
            cboCiclosLectivos.Size = new Size(231, 28);
            cboCiclosLectivos.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(413, 125);
            label2.Name = "label2";
            label2.Size = new Size(96, 20);
            label2.TabIndex = 3;
            label2.Text = "Ciclo Lectivo:";
            // 
            // chMaterias2doCuatrimestre
            // 
            chMaterias2doCuatrimestre.AutoSize = true;
            chMaterias2doCuatrimestre.Location = new Point(515, 239);
            chMaterias2doCuatrimestre.Name = "chMaterias2doCuatrimestre";
            chMaterias2doCuatrimestre.Size = new Size(208, 24);
            chMaterias2doCuatrimestre.TabIndex = 5;
            chMaterias2doCuatrimestre.Text = "Materias 2do Cuatrimentre";
            chMaterias2doCuatrimestre.UseVisualStyleBackColor = true;
            // 
            // LabelTitulo
            // 
            LabelTitulo.AutoSize = true;
            LabelTitulo.Font = new Font("Bahnschrift SemiCondensed", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            LabelTitulo.Location = new Point(91, 26);
            LabelTitulo.Name = "LabelTitulo";
            LabelTitulo.Size = new Size(240, 36);
            LabelTitulo.TabIndex = 6;
            LabelTitulo.Text = "Período Inscripción";
            // 
            // PeriodoInscripcionView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(LabelTitulo);
            Controls.Add(chMaterias2doCuatrimestre);
            Controls.Add(cboCiclosLectivos);
            Controls.Add(label2);
            Controls.Add(cboNombres);
            Controls.Add(chInscripcionActiva);
            Controls.Add(label1);
            Name = "PeriodoInscripcionView";
            Text = "Periodo Inscripción";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private CheckBox chInscripcionActiva;
        private ComboBox cboNombres;
        private ComboBox cboCiclosLectivos;
        private Label label2;
        private CheckBox chMaterias2doCuatrimestre;
        private Label LabelTitulo;
    }
}