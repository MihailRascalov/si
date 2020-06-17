namespace k_NN
{
    partial class Form2
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
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Nup = new System.Windows.Forms.NumericUpDown();
            this.Combobax = new System.Windows.Forms.ComboBox();
            this.Grid = new System.Windows.Forms.DataGridView();
            this.bt1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Nup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "k";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(160, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Metrics";
            // 
            // Nup
            // 
            this.Nup.Location = new System.Drawing.Point(16, 42);
            this.Nup.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.Nup.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Nup.Name = "Nup";
            this.Nup.Size = new System.Drawing.Size(120, 20);
            this.Nup.TabIndex = 2;
            this.Nup.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Nup.ValueChanged += new System.EventHandler(this.Nup_ValueChanged);
            // 
            // Combobax
            // 
            this.Combobax.FormattingEnabled = true;
            this.Combobax.Location = new System.Drawing.Point(163, 41);
            this.Combobax.Name = "Combobax";
            this.Combobax.Size = new System.Drawing.Size(121, 21);
            this.Combobax.TabIndex = 3;
            this.Combobax.SelectedIndexChanged += new System.EventHandler(this.Combobax_SelectedIndexChanged);
            // 
            // Grid
            // 
            this.Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Grid.Location = new System.Drawing.Point(12, 104);
            this.Grid.Name = "Grid";
            this.Grid.Size = new System.Drawing.Size(636, 167);
            this.Grid.TabIndex = 4;
            this.Grid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellContentClick);
            // 
            // bt1
            // 
            this.bt1.AccessibleDescription = "";
            this.bt1.Location = new System.Drawing.Point(320, 33);
            this.bt1.Name = "bt1";
            this.bt1.Size = new System.Drawing.Size(145, 35);
            this.bt1.TabIndex = 5;
            this.bt1.Text = "Generate";
            this.bt1.UseVisualStyleBackColor = true;
            this.bt1.Click += new System.EventHandler(this.bt1_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(660, 360);
            this.Controls.Add(this.bt1);
            this.Controls.Add(this.Grid);
            this.Controls.Add(this.Combobax);
            this.Controls.Add(this.Nup);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form2";
            this.Text = "Generate result";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Nup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown Nup;
        private System.Windows.Forms.ComboBox Combobax;
        private System.Windows.Forms.DataGridView Grid;
        private System.Windows.Forms.Button bt1;
    }
}