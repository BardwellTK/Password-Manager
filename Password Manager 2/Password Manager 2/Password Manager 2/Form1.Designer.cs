namespace Password_Manager_2
{
    partial class HomeWindow
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
            this.GUI = new System.Windows.Forms.PictureBox();
            this.timerGUI = new System.Windows.Forms.Timer(this.components);
            this.timerSecond = new System.Windows.Forms.Timer(this.components);
            this.label_FPS = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.GUI)).BeginInit();
            this.SuspendLayout();
            // 
            // GUI
            // 
            this.GUI.Location = new System.Drawing.Point(34, 21);
            this.GUI.Name = "GUI";
            this.GUI.Size = new System.Drawing.Size(100, 50);
            this.GUI.TabIndex = 0;
            this.GUI.TabStop = false;
            // 
            // timerGUI
            // 
            this.timerGUI.Interval = 1;
            this.timerGUI.Tick += new System.EventHandler(this.timerGUI_Tick);
            // 
            // timerSecond
            // 
            this.timerSecond.Enabled = true;
            this.timerSecond.Interval = 1000;
            this.timerSecond.Tick += new System.EventHandler(this.timerSecond_Tick);
            // 
            // label_FPS
            // 
            this.label_FPS.AutoSize = true;
            this.label_FPS.Location = new System.Drawing.Point(92, 122);
            this.label_FPS.Name = "label_FPS";
            this.label_FPS.Size = new System.Drawing.Size(55, 13);
            this.label_FPS.TabIndex = 1;
            this.label_FPS.Text = "label_FPS";
            // 
            // HomeWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.label_FPS);
            this.Controls.Add(this.GUI);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "HomeWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HomeWindow_KeyDown);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.HomeWindow_MouseClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.HomeWindow_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.GUI)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox GUI;
        private System.Windows.Forms.Timer timerGUI;
        private System.Windows.Forms.Timer timerSecond;
        private System.Windows.Forms.Label label_FPS;
    }
}

