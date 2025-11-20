namespace AvailableOnTeams
{
    partial class AvailableOnTeamsTray
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AvailableOnTeamsTray));
            label1 = new Label();
            label2 = new Label();
            button1 = new Button();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(89, 9);
            label1.Name = "label1";
            label1.Size = new Size(166, 25);
            label1.TabIndex = 0;
            label1.Text = "AvailableOnTeams";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(90, 153);
            label2.Name = "label2";
            label2.Size = new Size(165, 15);
            label2.TabIndex = 1;
            label2.Text = "Made with love by Atlas Team";
            // 
            // button1
            // 
            button1.BackColor = SystemColors.Control;
            button1.BackgroundImage = (Image)resources.GetObject("button1.BackgroundImage");
            button1.BackgroundImageLayout = ImageLayout.Stretch;
            button1.Cursor = Cursors.Hand;
            button1.ForeColor = Color.Transparent;
            button1.Location = new Point(89, 171);
            button1.Name = "button1";
            button1.Size = new Size(166, 57);
            button1.TabIndex = 2;
            button1.UseVisualStyleBackColor = false;
            button1.Click += BuyMeACoffeeOnClick;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label3.Location = new Point(96, 44);
            label3.Name = "label3";
            label3.Size = new Size(151, 15);
            label3.TabIndex = 3;
            label3.Text = "\"Work Smarter, not harder!\"";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(65, 89);
            label4.Name = "label4";
            label4.Size = new Size(248, 15);
            label4.TabIndex = 4;
            label4.Text = "Start: Ctrl + Alt + a (\"a\" stands for \"Available\")";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(65, 104);
            label5.Name = "label5";
            label5.Size = new Size(222, 15);
            label5.TabIndex = 5;
            label5.Text = "Stop: Ctrl + Alt + s (\"s\" stands for \"Stop\")";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(131, 236);
            label6.Name = "label6";
            label6.Size = new Size(45, 15);
            label6.TabIndex = 6;
            label6.Text = "Version";
            // 
            // AvailableOnTeamsTray
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(352, 260);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "AvailableOnTeamsTray";
            Text = "About";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Button button1;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
    }
}