namespace NelderMeadUI
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
            buttonStart = new Button();
            label1 = new Label();
            textBox1 = new TextBox();
            label2 = new Label();
            label4 = new Label();
            textBox2 = new TextBox();
            SuspendLayout();
            // 
            // buttonStart
            // 
            buttonStart.Location = new Point(85, 372);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(134, 23);
            buttonStart.TabIndex = 0;
            buttonStart.Text = "Запустить алгоритм";
            buttonStart.UseVisualStyleBackColor = true;
            buttonStart.Click += buttonStart_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(560, 54);
            label1.Name = "label1";
            label1.Size = new Size(0, 15);
            label1.TabIndex = 1;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(140, 51);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(222, 23);
            textBox1.TabIndex = 2;
            textBox1.Text = "(1-x1)^2+100*(x2-x1^2)^2";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(37, 54);
            label2.Name = "label2";
            label2.Size = new Size(58, 15);
            label2.TabIndex = 3;
            label2.Text = "Функция:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(37, 108);
            label4.Name = "label4";
            label4.Size = new Size(104, 15);
            label4.TabIndex = 6;
            label4.Text = "Начальная точка:";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(262, 105);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(100, 23);
            textBox2.TabIndex = 7;
            textBox2.Text = "0, 0";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(textBox2);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Controls.Add(buttonStart);
            Name = "Form1";
            Text = "Метод Нелдера-Мида";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonStart;
        private Label label1;
        private TextBox textBox1;
        private Label label2;
        private Label label4;
        private TextBox textBox2;
    }
}
