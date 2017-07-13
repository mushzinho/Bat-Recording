namespace BatRecording
{
    partial class AdminPass
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminPass));
            this.textBoxSenhaAdmin = new System.Windows.Forms.TextBox();
            this.buttonLogarAdmin = new System.Windows.Forms.Button();
            this.labelTextAdmin = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxSenhaAdmin
            // 
            this.textBoxSenhaAdmin.Location = new System.Drawing.Point(56, 35);
            this.textBoxSenhaAdmin.Name = "textBoxSenhaAdmin";
            this.textBoxSenhaAdmin.PasswordChar = '*';
            this.textBoxSenhaAdmin.Size = new System.Drawing.Size(214, 20);
            this.textBoxSenhaAdmin.TabIndex = 0;
            // 
            // buttonLogarAdmin
            // 
            this.buttonLogarAdmin.Location = new System.Drawing.Point(129, 61);
            this.buttonLogarAdmin.Name = "buttonLogarAdmin";
            this.buttonLogarAdmin.Size = new System.Drawing.Size(85, 31);
            this.buttonLogarAdmin.TabIndex = 1;
            this.buttonLogarAdmin.Text = "Logar";
            this.buttonLogarAdmin.UseVisualStyleBackColor = true;
            this.buttonLogarAdmin.Click += new System.EventHandler(this.buttonLogarAdmin_Click);
            // 
            // labelTextAdmin
            // 
            this.labelTextAdmin.AutoSize = true;
            this.labelTextAdmin.Location = new System.Drawing.Point(129, 13);
            this.labelTextAdmin.Name = "labelTextAdmin";
            this.labelTextAdmin.Size = new System.Drawing.Size(70, 13);
            this.labelTextAdmin.TabIndex = 2;
            this.labelTextAdmin.Text = "Senha Admin";
            // 
            // AdminPass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(333, 107);
            this.Controls.Add(this.labelTextAdmin);
            this.Controls.Add(this.buttonLogarAdmin);
            this.Controls.Add(this.textBoxSenhaAdmin);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AdminPass";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Logar";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxSenhaAdmin;
        private System.Windows.Forms.Button buttonLogarAdmin;
        private System.Windows.Forms.Label labelTextAdmin;
    }
}