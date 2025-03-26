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
            button1 = new Button();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // buttonStart
            // 
            buttonStart.Location = new Point(97, 496);
            buttonStart.Margin = new Padding(3, 4, 3, 4);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(153, 31);
            buttonStart.TabIndex = 0;
            buttonStart.Text = "Запустить алгоритм";
            buttonStart.UseVisualStyleBackColor = true;
            buttonStart.Click += buttonStart_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(640, 72);
            label1.Name = "label1";
            label1.Size = new Size(0, 20);
            label1.TabIndex = 1;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(160, 68);
            textBox1.Margin = new Padding(3, 4, 3, 4);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(253, 27);
            textBox1.TabIndex = 2;
            textBox1.Text = "(1-x1)^2+100*(x2-x1^2)^2";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(42, 72);
            label2.Name = "label2";
            label2.Size = new Size(72, 20);
            label2.TabIndex = 3;
            label2.Text = "Функция:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(42, 144);
            label4.Name = "label4";
            label4.Size = new Size(130, 20);
            label4.TabIndex = 6;
            label4.Text = "Начальная точка:";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(299, 140);
            textBox2.Margin = new Padding(3, 4, 3, 4);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(114, 27);
            textBox2.TabIndex = 7;
            textBox2.Text = "0, 0";
            // 
            // button1
            // 
            button1.Location = new Point(654, 483);
            button1.Name = "button1";
            button1.Size = new Size(166, 57);
            button1.TabIndex = 8;
            button1.Text = "Запустить пробную версию";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = SystemColors.ActiveCaption;
            pictureBox1.Location = new Point(188, 190);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(576, 262);
            pictureBox1.TabIndex = 9;
            pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 600);
            Controls.Add(pictureBox1);
            Controls.Add(button1);
            Controls.Add(textBox2);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(textBox1);
            Controls.Add(label1);
            Controls.Add(buttonStart);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            Text = "Метод Нелдера-Мида";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
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
        private Button button1;
        private PictureBox pictureBox1;
    }
}
