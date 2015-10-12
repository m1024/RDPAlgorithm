namespace RDPalgorithm
{
    partial class RDPForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonOpenFile = new System.Windows.Forms.Button();
            this.textBoxSourseSequence = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxFilePath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxSmoothedSequence = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxEpsilon = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxBlockSize = new System.Windows.Forms.TextBox();
            this.richTextBoxConvertTime = new System.Windows.Forms.RichTextBox();
            this.buttonConvert = new System.Windows.Forms.Button();
            this.buttonOriginalGraph = new System.Windows.Forms.Button();
            this.buttonSmoothedGraph = new System.Windows.Forms.Button();
            this.openFile = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // buttonOpenFile
            // 
            this.buttonOpenFile.Location = new System.Drawing.Point(246, 17);
            this.buttonOpenFile.Name = "buttonOpenFile";
            this.buttonOpenFile.Size = new System.Drawing.Size(100, 25);
            this.buttonOpenFile.TabIndex = 0;
            this.buttonOpenFile.Text = "Обзор";
            this.buttonOpenFile.UseVisualStyleBackColor = true;
            this.buttonOpenFile.Click += new System.EventHandler(this.buttonOpenFile_Click);
            // 
            // textBoxSourseSequence
            // 
            this.textBoxSourseSequence.Location = new System.Drawing.Point(246, 101);
            this.textBoxSourseSequence.Name = "textBoxSourseSequence";
            this.textBoxSourseSequence.ReadOnly = true;
            this.textBoxSourseSequence.Size = new System.Drawing.Size(100, 20);
            this.textBoxSourseSequence.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Исходная последовательность, байт";
            // 
            // textBoxFilePath
            // 
            this.textBoxFilePath.Location = new System.Drawing.Point(15, 20);
            this.textBoxFilePath.Name = "textBoxFilePath";
            this.textBoxFilePath.ReadOnly = true;
            this.textBoxFilePath.Size = new System.Drawing.Size(207, 20);
            this.textBoxFilePath.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(210, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Упрощенная последовательность, байт";
            // 
            // textBoxSmoothedSequence
            // 
            this.textBoxSmoothedSequence.Location = new System.Drawing.Point(246, 141);
            this.textBoxSmoothedSequence.Name = "textBoxSmoothedSequence";
            this.textBoxSmoothedSequence.ReadOnly = true;
            this.textBoxSmoothedSequence.Size = new System.Drawing.Size(100, 20);
            this.textBoxSmoothedSequence.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 180);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Время преобразования";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Точность";
            // 
            // textBoxEpsilon
            // 
            this.textBoxEpsilon.Location = new System.Drawing.Point(72, 61);
            this.textBoxEpsilon.Name = "textBoxEpsilon";
            this.textBoxEpsilon.Size = new System.Drawing.Size(52, 20);
            this.textBoxEpsilon.TabIndex = 9;
            this.textBoxEpsilon.Text = "1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(161, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Размер блока";
            // 
            // textBoxBlockSize
            // 
            this.textBoxBlockSize.Location = new System.Drawing.Point(246, 61);
            this.textBoxBlockSize.Name = "textBoxBlockSize";
            this.textBoxBlockSize.Size = new System.Drawing.Size(100, 20);
            this.textBoxBlockSize.TabIndex = 11;
            this.textBoxBlockSize.Text = "1000";
            // 
            // richTextBoxConvertTime
            // 
            this.richTextBoxConvertTime.Location = new System.Drawing.Point(185, 177);
            this.richTextBoxConvertTime.Name = "richTextBoxConvertTime";
            this.richTextBoxConvertTime.ReadOnly = true;
            this.richTextBoxConvertTime.Size = new System.Drawing.Size(161, 93);
            this.richTextBoxConvertTime.TabIndex = 12;
            this.richTextBoxConvertTime.Text = "";
            // 
            // buttonConvert
            // 
            this.buttonConvert.Location = new System.Drawing.Point(35, 220);
            this.buttonConvert.Name = "buttonConvert";
            this.buttonConvert.Size = new System.Drawing.Size(124, 35);
            this.buttonConvert.TabIndex = 13;
            this.buttonConvert.Text = "Преобразовать";
            this.buttonConvert.UseVisualStyleBackColor = true;
            this.buttonConvert.Click += new System.EventHandler(this.buttonConvert_Click);
            // 
            // buttonOriginalGraph
            // 
            this.buttonOriginalGraph.Location = new System.Drawing.Point(15, 276);
            this.buttonOriginalGraph.Name = "buttonOriginalGraph";
            this.buttonOriginalGraph.Size = new System.Drawing.Size(161, 25);
            this.buttonOriginalGraph.TabIndex = 14;
            this.buttonOriginalGraph.Text = "Исходный график";
            this.buttonOriginalGraph.UseVisualStyleBackColor = true;
            this.buttonOriginalGraph.Click += new System.EventHandler(this.buttonOriginalGraph_Click);
            // 
            // buttonSmoothedGraph
            // 
            this.buttonSmoothedGraph.Location = new System.Drawing.Point(185, 276);
            this.buttonSmoothedGraph.Name = "buttonSmoothedGraph";
            this.buttonSmoothedGraph.Size = new System.Drawing.Size(161, 25);
            this.buttonSmoothedGraph.TabIndex = 15;
            this.buttonSmoothedGraph.Text = "Упрощенный график";
            this.buttonSmoothedGraph.UseVisualStyleBackColor = true;
            this.buttonSmoothedGraph.Click += new System.EventHandler(this.buttonSmoothedGraph_Click);
            // 
            // openFile
            // 
            this.openFile.FileName = "openFile";
            // 
            // RDPForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 316);
            this.Controls.Add(this.buttonSmoothedGraph);
            this.Controls.Add(this.buttonOriginalGraph);
            this.Controls.Add(this.buttonConvert);
            this.Controls.Add(this.richTextBoxConvertTime);
            this.Controls.Add(this.textBoxBlockSize);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxEpsilon);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxSmoothedSequence);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxFilePath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxSourseSequence);
            this.Controls.Add(this.buttonOpenFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "RDPForm";
            this.Text = "Ramer-Douglas-Peucker algorithm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOpenFile;
        private System.Windows.Forms.TextBox textBoxSourseSequence;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxFilePath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxSmoothedSequence;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxEpsilon;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxBlockSize;
        private System.Windows.Forms.RichTextBox richTextBoxConvertTime;
        private System.Windows.Forms.Button buttonConvert;
        private System.Windows.Forms.Button buttonOriginalGraph;
        private System.Windows.Forms.Button buttonSmoothedGraph;
        private System.Windows.Forms.OpenFileDialog openFile;
    }
}

