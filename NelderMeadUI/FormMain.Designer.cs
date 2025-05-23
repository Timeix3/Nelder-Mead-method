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
            panel = new Panel();
            SuspendLayout();
            // 
            // buttonStart
            // 
            buttonStart.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonStart.Location = new Point(33, 542);
            buttonStart.Margin = new Padding(3, 4, 3, 4);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(210, 45);
            buttonStart.TabIndex = 0;
            buttonStart.Text = "Запустить алгоритм";
            buttonStart.UseVisualStyleBackColor = true;
            buttonStart.Click += ButtonStart_Click;
            // 
            // labelResult
            // 
            labelResult.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelResult.AutoSize = true;
            labelResult.Location = new Point(635, 30);
            labelResult.Name = "labelResult";
            labelResult.Size = new Size(0, 20);
            labelResult.TabIndex = 1;
            // 
            // textBoxFunction
            // 
            textBoxFunction.Location = new Point(209, 30);
            textBoxFunction.Margin = new Padding(3, 4, 3, 4);
            textBoxFunction.Name = "textBoxFunction";
            textBoxFunction.Size = new Size(253, 27);
            textBoxFunction.TabIndex = 2;
            textBoxFunction.Text = "x1^2+x2^2";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(33, 30);
            label2.Name = "label2";
            label2.Size = new Size(72, 20);
            label2.TabIndex = 3;
            label2.Text = "Функция:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(33, 79);
            label4.Name = "label4";
            label4.Size = new Size(130, 20);
            label4.TabIndex = 6;
            label4.Text = "Начальная точка:";
            // 
            // textBoxPoint
            // 
            textBoxPoint.Location = new Point(209, 72);
            textBoxPoint.Margin = new Padding(3, 4, 3, 4);
            textBoxPoint.Name = "textBoxPoint";
            textBoxPoint.Size = new Size(114, 27);
            textBoxPoint.TabIndex = 7;
            textBoxPoint.Text = "10, 10";
            // 
            // panel
            // 
            panel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel.BackColor = SystemColors.ActiveCaption;
            panel.Location = new Point(33, 126);
            panel.Name = "panel";
            panel.Size = new Size(842, 394);
            panel.TabIndex = 10;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 600);
            Controls.Add(panel);
            Controls.Add(textBoxPoint);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(textBoxFunction);
            Controls.Add(labelResult);
            Controls.Add(buttonStart);
            Margin = new Padding(3, 4, 3, 4);
            Name = "FormMain";
            Text = "Метод Нелдера-Мида";
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
        private Panel panel;
    }
}
