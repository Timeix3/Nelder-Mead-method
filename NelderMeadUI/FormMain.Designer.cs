namespace NelderMeadUI
{
    partial class FormMain
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
            labelResult = new Label();
            textBoxFunction = new TextBox();
            label2 = new Label();
            label4 = new Label();
            textBoxPoint = new TextBox();
            button1 = new Button();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
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
            buttonStart.Click += ButtonStart_Click;
            // 
            // labelResult
            // 
            labelResult.AutoSize = true;
            labelResult.Location = new Point(560, 54);
            labelResult.Name = "labelResult";
            labelResult.Size = new Size(0, 15);
            labelResult.TabIndex = 1;
            // 
            // textBoxFunction
            // 
            textBoxFunction.Location = new Point(140, 51);
            textBoxFunction.Name = "textBoxFunction";
            textBoxFunction.Size = new Size(222, 23);
            textBoxFunction.TabIndex = 2;
            textBoxFunction.Text = "(1-x1)^2+100*(x2-x1^2)^2";
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
            // textBoxPoint
            // 
            textBoxPoint.Location = new Point(262, 105);
            textBoxPoint.Name = "textBoxPoint";
            textBoxPoint.Size = new Size(100, 23);
            textBoxPoint.TabIndex = 7;
            textBoxPoint.Text = "0, 0";
            // 
            // button1
            // 
            button1.Location = new Point(572, 362);
            button1.Margin = new Padding(3, 2, 3, 2);
            button1.Name = "button1";
            button1.Size = new Size(145, 43);
            button1.TabIndex = 8;
            button1.Text = "Запустить пробную версию";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = SystemColors.ActiveCaption;
            pictureBox1.Location = new Point(164, 142);
            pictureBox1.Margin = new Padding(3, 2, 3, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(504, 196);
            pictureBox1.TabIndex = 9;
            pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pictureBox1);
            Controls.Add(button1);
            Controls.Add(textBoxPoint);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(textBoxFunction);
            Controls.Add(labelResult);
            Controls.Add(buttonStart);
            Name = "Form1";
            Text = "Метод Нелдера-Мида";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonStart;
        private Label labelResult;
        private TextBox textBoxFunction;
        private Label label2;
        private Label label4;
        private TextBox textBoxPoint;
        private Button button1;
        private PictureBox pictureBox1;
    }
}
