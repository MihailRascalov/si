namespace k_NN
{
    partial class kNN
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
            this.btnSelectTestSystem = new System.Windows.Forms.Button();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.btnStart = new System.Windows.Forms.Button();
            this.tbTrainingSystem = new System.Windows.Forms.TextBox();
            this.tbTestSystem = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnSelectTrainingSystem = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnWybierzSystemTestowy
            // 
            this.btnSelectTestSystem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectTestSystem.Location = new System.Drawing.Point(88, 20);
            this.btnSelectTestSystem.Margin = new System.Windows.Forms.Padding(2);
            this.btnSelectTestSystem.Name = "btnWybierzSystemTestowy";
            this.btnSelectTestSystem.Size = new System.Drawing.Size(145, 35);
            this.btnSelectTestSystem.TabIndex = 0;
            this.btnSelectTestSystem.Text = "Select test system";
            this.btnSelectTestSystem.UseVisualStyleBackColor = true;
            this.btnSelectTestSystem.Click += new System.EventHandler(this.btnSelectTestSystem_Click);
            // 
            // btnStart
            // 
            this.btnStart.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnStart.Location = new System.Drawing.Point(244, 396);
            this.btnStart.Margin = new System.Windows.Forms.Padding(2);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(104, 35);
            this.btnStart.TabIndex = 9;
            this.btnStart.Text = "Load";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // tbSystemTreningowy
            // 
            this.tbTrainingSystem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbTrainingSystem.Location = new System.Drawing.Point(0, 0);
            this.tbTrainingSystem.Margin = new System.Windows.Forms.Padding(2);
            this.tbTrainingSystem.Multiline = true;
            this.tbTrainingSystem.Name = "tbSystemTreningowy";
            this.tbTrainingSystem.Size = new System.Drawing.Size(285, 320);
            this.tbTrainingSystem.TabIndex = 7;
            this.tbTrainingSystem.TextChanged += new System.EventHandler(this.tbTrainingSystem_TextChanged);
            // 
            // tbSystemTestowy
            // 
            this.tbTestSystem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbTestSystem.Location = new System.Drawing.Point(0, 0);
            this.tbTestSystem.Margin = new System.Windows.Forms.Padding(2);
            this.tbTestSystem.Multiline = true;
            this.tbTestSystem.Name = "tbSystemTestowy";
            this.tbTestSystem.Size = new System.Drawing.Size(285, 320);
            this.tbTestSystem.TabIndex = 3;
            this.tbTestSystem.TextChanged += new System.EventHandler(this.tbTestSystem_TextChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(11, 72);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tbTestSystem);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tbTrainingSystem);
            this.splitContainer1.Size = new System.Drawing.Size(573, 320);
            this.splitContainer1.SplitterDistance = 285;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 8;
            // 
            // btnWybierzSystemTreningowy
            // 
            this.btnSelectTrainingSystem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectTrainingSystem.Location = new System.Drawing.Point(382, 20);
            this.btnSelectTrainingSystem.Margin = new System.Windows.Forms.Padding(2);
            this.btnSelectTrainingSystem.Name = "btnWybierzSystemTreningowy";
            this.btnSelectTrainingSystem.Size = new System.Drawing.Size(145, 35);
            this.btnSelectTrainingSystem.TabIndex = 6;
            this.btnSelectTrainingSystem.Text = "Select training system";
            this.btnSelectTrainingSystem.UseVisualStyleBackColor = true;
            this.btnSelectTrainingSystem.Click += new System.EventHandler(this.btnSelectTrainingSystem_Click);
            // 
            // kNN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(593, 444);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnSelectTrainingSystem);
            this.Controls.Add(this.btnSelectTestSystem);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(529, 413);
            this.Name = "kNN";
            this.Text = "Load data";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button btnSelectTestSystem;
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox tbTrainingSystem;
        private System.Windows.Forms.TextBox tbTestSystem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnSelectTrainingSystem;
    }
}