namespace WinForms
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Button1 = new Button();
            button2 = new Button();
            TManifest = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // Button1
            // 
            Button1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            Button1.Location = new Point(454, 78);
            Button1.Name = "Button1";
            Button1.Size = new Size(102, 31);
            Button1.TabIndex = 0;
            Button1.Text = "Instalovat";
            Button1.UseVisualStyleBackColor = true;
            Button1.Click += Button1_Click;
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            button2.Location = new Point(12, 118);
            button2.Name = "button2";
            button2.Size = new Size(102, 32);
            button2.TabIndex = 1;
            button2.Text = "Manifest";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // TManifest
            // 
            TManifest.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            TManifest.Location = new Point(114, 39);
            TManifest.Name = "TManifest";
            TManifest.Size = new Size(102, 29);
            TManifest.TabIndex = 2;
            TManifest.Text = "0.0.1";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            label1.Location = new Point(114, 81);
            label1.Name = "label1";
            label1.Size = new Size(52, 21);
            label1.TabIndex = 3;
            label1.Text = "label1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            label2.Location = new Point(12, 47);
            label2.Name = "label2";
            label2.Size = new Size(58, 21);
            label2.TabIndex = 4;
            label2.Text = "Nastav";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            label3.Location = new Point(12, 81);
            label3.Name = "label3";
            label3.Size = new Size(84, 21);
            label3.TabIndex = 5;
            label3.Text = "Nastaveno";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(568, 162);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(TManifest);
            Controls.Add(button2);
            Controls.Add(Button1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Button1;
        private Button button2;
        private TextBox TManifest;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}
