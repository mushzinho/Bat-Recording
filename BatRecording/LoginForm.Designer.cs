namespace BatRecording
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.LabelLogin = new System.Windows.Forms.Label();
            this.LabelSenha = new System.Windows.Forms.Label();
            this.textBoxLogin = new System.Windows.Forms.TextBox();
            this.textBoxSenha = new System.Windows.Forms.TextBox();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.buttonCriarConta = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LabelLogin
            // 
            this.LabelLogin.AutoSize = true;
            this.LabelLogin.Location = new System.Drawing.Point(56, 49);
            this.LabelLogin.Name = "LabelLogin";
            this.LabelLogin.Size = new System.Drawing.Size(33, 13);
            this.LabelLogin.TabIndex = 0;
            this.LabelLogin.Text = "Login";
            // 
            // LabelSenha
            // 
            this.LabelSenha.AutoSize = true;
            this.LabelSenha.Location = new System.Drawing.Point(51, 90);
            this.LabelSenha.Name = "LabelSenha";
            this.LabelSenha.Size = new System.Drawing.Size(38, 13);
            this.LabelSenha.TabIndex = 1;
            this.LabelSenha.Text = "Senha";
            // 
            // textBoxLogin
            // 
            this.textBoxLogin.Location = new System.Drawing.Point(95, 46);
            this.textBoxLogin.Name = "textBoxLogin";
            this.textBoxLogin.Size = new System.Drawing.Size(202, 20);
            this.textBoxLogin.TabIndex = 2;
            // 
            // textBoxSenha
            // 
            this.textBoxSenha.Location = new System.Drawing.Point(95, 87);
            this.textBoxSenha.Name = "textBoxSenha";
            this.textBoxSenha.PasswordChar = '*';
            this.textBoxSenha.Size = new System.Drawing.Size(202, 20);
            this.textBoxSenha.TabIndex = 3;
            // 
            // buttonLogin
            // 
            this.buttonLogin.Location = new System.Drawing.Point(138, 129);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(103, 36);
            this.buttonLogin.TabIndex = 4;
            this.buttonLogin.Text = "Login";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // buttonCriarConta
            // 
            this.buttonCriarConta.Location = new System.Drawing.Point(283, 161);
            this.buttonCriarConta.Name = "buttonCriarConta";
            this.buttonCriarConta.Size = new System.Drawing.Size(66, 23);
            this.buttonCriarConta.TabIndex = 5;
            this.buttonCriarConta.Text = "?";
            this.buttonCriarConta.UseVisualStyleBackColor = true;
            this.buttonCriarConta.Click += new System.EventHandler(this.buttonCriarConta_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 196);
            this.Controls.Add(this.buttonCriarConta);
            this.Controls.Add(this.buttonLogin);
            this.Controls.Add(this.textBoxSenha);
            this.Controls.Add(this.textBoxLogin);
            this.Controls.Add(this.LabelSenha);
            this.Controls.Add(this.LabelLogin);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fazer Login";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LoginForm_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelLogin;
        private System.Windows.Forms.Label LabelSenha;
        private System.Windows.Forms.TextBox textBoxLogin;
        private System.Windows.Forms.TextBox textBoxSenha;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Button buttonCriarConta;
    }
}