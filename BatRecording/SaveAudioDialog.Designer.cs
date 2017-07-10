namespace BatRecording
{
    partial class SaveAudioDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaveAudioDialog));
            this.TextBoxClienteName = new System.Windows.Forms.TextBox();
            this.TextBoxCPF = new System.Windows.Forms.TextBox();
            this.labelClienteName = new System.Windows.Forms.Label();
            this.labelCPF = new System.Windows.Forms.Label();
            this.labelSubtitle = new System.Windows.Forms.Label();
            this.Finalize = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TextBoxClienteName
            // 
            this.TextBoxClienteName.Location = new System.Drawing.Point(67, 56);
            this.TextBoxClienteName.Name = "TextBoxClienteName";
            this.TextBoxClienteName.Size = new System.Drawing.Size(306, 20);
            this.TextBoxClienteName.TabIndex = 0;
            // 
            // TextBoxCPF
            // 
            this.TextBoxCPF.Location = new System.Drawing.Point(146, 82);
            this.TextBoxCPF.Name = "TextBoxCPF";
            this.TextBoxCPF.Size = new System.Drawing.Size(227, 20);
            this.TextBoxCPF.TabIndex = 1;
            // 
            // labelClienteName
            // 
            this.labelClienteName.AutoSize = true;
            this.labelClienteName.Location = new System.Drawing.Point(13, 59);
            this.labelClienteName.Name = "labelClienteName";
            this.labelClienteName.Size = new System.Drawing.Size(38, 13);
            this.labelClienteName.TabIndex = 2;
            this.labelClienteName.Text = "Nome:";
            // 
            // labelCPF
            // 
            this.labelCPF.AutoSize = true;
            this.labelCPF.Location = new System.Drawing.Point(13, 89);
            this.labelCPF.Name = "labelCPF";
            this.labelCPF.Size = new System.Drawing.Size(126, 13);
            this.labelCPF.TabIndex = 3;
            this.labelCPF.Text = "CPF (Somente Números):";
            // 
            // labelSubtitle
            // 
            this.labelSubtitle.AutoSize = true;
            this.labelSubtitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSubtitle.Location = new System.Drawing.Point(80, 20);
            this.labelSubtitle.Name = "labelSubtitle";
            this.labelSubtitle.Size = new System.Drawing.Size(256, 17);
            this.labelSubtitle.TabIndex = 4;
            this.labelSubtitle.Text = "Prençha com as informações do cliente";
            // 
            // Finalize
            // 
            this.Finalize.Location = new System.Drawing.Point(146, 134);
            this.Finalize.Name = "Finalize";
            this.Finalize.Size = new System.Drawing.Size(104, 32);
            this.Finalize.TabIndex = 5;
            this.Finalize.Text = "Finalizar";
            this.Finalize.UseVisualStyleBackColor = true;
            this.Finalize.Click += new System.EventHandler(this.Finalize_Click);
            // 
            // SaveAudioDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 196);
            this.Controls.Add(this.Finalize);
            this.Controls.Add(this.labelSubtitle);
            this.Controls.Add(this.labelCPF);
            this.Controls.Add(this.labelClienteName);
            this.Controls.Add(this.TextBoxCPF);
            this.Controls.Add(this.TextBoxClienteName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SaveAudioDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Salvar Gravação";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TextBoxClienteName;
        private System.Windows.Forms.TextBox TextBoxCPF;
        private System.Windows.Forms.Label labelClienteName;
        private System.Windows.Forms.Label labelCPF;
        private System.Windows.Forms.Label labelSubtitle;
        private System.Windows.Forms.Button Finalize;
    }
}