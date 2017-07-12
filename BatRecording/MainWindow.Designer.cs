namespace BatRecording
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.RecordButton = new System.Windows.Forms.Button();
            this.statusTextBox = new System.Windows.Forms.TextBox();
            this.StatusOfRecording = new System.Windows.Forms.Label();
            this.NotifycationIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.SuspendLayout();
            // 
            // RecordButton
            // 
            this.RecordButton.Font = new System.Drawing.Font("Trebuchet MS", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RecordButton.Location = new System.Drawing.Point(120, 12);
            this.RecordButton.Name = "RecordButton";
            this.RecordButton.Size = new System.Drawing.Size(163, 49);
            this.RecordButton.TabIndex = 0;
            this.RecordButton.Text = "Gravar";
            this.RecordButton.UseVisualStyleBackColor = true;
            this.RecordButton.Click += new System.EventHandler(this.Record_Click);
            // 
            // statusTextBox
            // 
            this.statusTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusTextBox.Location = new System.Drawing.Point(181, 87);
            this.statusTextBox.Name = "statusTextBox";
            this.statusTextBox.Size = new System.Drawing.Size(154, 23);
            this.statusTextBox.TabIndex = 1;
            this.statusTextBox.Text = "Não está gravando.";
            this.statusTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // StatusOfRecording
            // 
            this.StatusOfRecording.AutoSize = true;
            this.StatusOfRecording.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusOfRecording.Location = new System.Drawing.Point(24, 90);
            this.StatusOfRecording.Name = "StatusOfRecording";
            this.StatusOfRecording.Size = new System.Drawing.Size(142, 17);
            this.StatusOfRecording.TabIndex = 2;
            this.StatusOfRecording.Text = "Status da Gravação: ";
            // 
            // NotifycationIcon
            // 
            this.NotifycationIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("NotifycationIcon.Icon")));
            this.NotifycationIcon.Text = "NotifycationIcon";
            this.NotifycationIcon.Visible = true;
            this.NotifycationIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotificationIcon_DoubleClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 141);
            this.Controls.Add(this.StatusOfRecording);
            this.Controls.Add(this.statusTextBox);
            this.Controls.Add(this.RecordButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bat Recording";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainWindow_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.SkyCallCenter_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button RecordButton;
        private System.Windows.Forms.TextBox statusTextBox;
        private System.Windows.Forms.Label StatusOfRecording;
        private System.Windows.Forms.NotifyIcon NotifycationIcon;
    }
}

