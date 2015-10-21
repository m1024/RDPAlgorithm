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
            this.textBoxSourseSize = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxFilePath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxSmoothedSize = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxEpsilon = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxBlockSize = new System.Windows.Forms.TextBox();
            this.buttonConvert = new System.Windows.Forms.Button();
            this.buttonSmoothedGraph = new System.Windows.Forms.Button();
            this.openFile = new System.Windows.Forms.OpenFileDialog();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxAdditions = new System.Windows.Forms.TextBox();
            this.textBoxMultiplications = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxKmax = new System.Windows.Forms.TextBox();
            this.textBoxKmin = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
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
            // textBoxSourseSize
            // 
            this.textBoxSourseSize.Location = new System.Drawing.Point(246, 106);
            this.textBoxSourseSize.Name = "textBoxSourseSize";
            this.textBoxSourseSize.ReadOnly = true;
            this.textBoxSourseSize.Size = new System.Drawing.Size(100, 20);
            this.textBoxSourseSize.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 113);
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
            this.label2.Location = new System.Drawing.Point(12, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(210, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Упрощенная последовательность, байт";
            // 
            // textBoxSmoothedSize
            // 
            this.textBoxSmoothedSize.Location = new System.Drawing.Point(246, 138);
            this.textBoxSmoothedSize.Name = "textBoxSmoothedSize";
            this.textBoxSmoothedSize.ReadOnly = true;
            this.textBoxSmoothedSize.Size = new System.Drawing.Size(100, 20);
            this.textBoxSmoothedSize.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Точность";
            // 
            // textBoxEpsilon
            // 
            this.textBoxEpsilon.Location = new System.Drawing.Point(72, 66);
            this.textBoxEpsilon.Name = "textBoxEpsilon";
            this.textBoxEpsilon.Size = new System.Drawing.Size(52, 20);
            this.textBoxEpsilon.TabIndex = 9;
            this.textBoxEpsilon.Text = "4";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(161, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Длина блока";
            // 
            // textBoxBlockSize
            // 
            this.textBoxBlockSize.Location = new System.Drawing.Point(246, 66);
            this.textBoxBlockSize.Name = "textBoxBlockSize";
            this.textBoxBlockSize.Size = new System.Drawing.Size(100, 20);
            this.textBoxBlockSize.TabIndex = 11;
            this.textBoxBlockSize.Text = "1000";
            // 
            // buttonConvert
            // 
            this.buttonConvert.Location = new System.Drawing.Point(15, 351);
            this.buttonConvert.Name = "buttonConvert";
            this.buttonConvert.Size = new System.Drawing.Size(161, 30);
            this.buttonConvert.TabIndex = 13;
            this.buttonConvert.Text = "Преобразовать";
            this.buttonConvert.UseVisualStyleBackColor = true;
            this.buttonConvert.Click += new System.EventHandler(this.buttonConvert_Click);
            // 
            // buttonSmoothedGraph
            // 
            this.buttonSmoothedGraph.Location = new System.Drawing.Point(185, 351);
            this.buttonSmoothedGraph.Name = "buttonSmoothedGraph";
            this.buttonSmoothedGraph.Size = new System.Drawing.Size(161, 30);
            this.buttonSmoothedGraph.TabIndex = 15;
            this.buttonSmoothedGraph.Text = "График";
            this.buttonSmoothedGraph.UseVisualStyleBackColor = true;
            this.buttonSmoothedGraph.Click += new System.EventHandler(this.buttonSmoothedGraph_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(370, 17);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.Size = new System.Drawing.Size(513, 372);
            this.dataGridView.TabIndex = 16;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(15, 318);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(331, 20);
            this.progressBar.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 237);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Операций сложения";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 270);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(117, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Операций умножения";
            // 
            // textBoxAdditions
            // 
            this.textBoxAdditions.Location = new System.Drawing.Point(246, 234);
            this.textBoxAdditions.Name = "textBoxAdditions";
            this.textBoxAdditions.ReadOnly = true;
            this.textBoxAdditions.Size = new System.Drawing.Size(100, 20);
            this.textBoxAdditions.TabIndex = 20;
            // 
            // textBoxMultiplications
            // 
            this.textBoxMultiplications.Location = new System.Drawing.Point(246, 267);
            this.textBoxMultiplications.Name = "textBoxMultiplications";
            this.textBoxMultiplications.ReadOnly = true;
            this.textBoxMultiplications.Size = new System.Drawing.Size(100, 20);
            this.textBoxMultiplications.TabIndex = 21;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 173);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(139, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Коэффициент сжатия, min";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 205);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(142, 13);
            this.label8.TabIndex = 23;
            this.label8.Text = "Коэффициент сжатия, max";
            // 
            // textBoxKmax
            // 
            this.textBoxKmax.Location = new System.Drawing.Point(246, 202);
            this.textBoxKmax.Name = "textBoxKmax";
            this.textBoxKmax.ReadOnly = true;
            this.textBoxKmax.Size = new System.Drawing.Size(100, 20);
            this.textBoxKmax.TabIndex = 25;
            // 
            // textBoxKmin
            // 
            this.textBoxKmin.Location = new System.Drawing.Point(246, 170);
            this.textBoxKmin.Name = "textBoxKmin";
            this.textBoxKmin.ReadOnly = true;
            this.textBoxKmin.Size = new System.Drawing.Size(100, 20);
            this.textBoxKmin.TabIndex = 24;
            // 
            // RDPForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 398);
            this.Controls.Add(this.textBoxKmax);
            this.Controls.Add(this.textBoxKmin);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxMultiplications);
            this.Controls.Add(this.textBoxAdditions);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.buttonSmoothedGraph);
            this.Controls.Add(this.buttonConvert);
            this.Controls.Add(this.textBoxBlockSize);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxEpsilon);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxSmoothedSize);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxFilePath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxSourseSize);
            this.Controls.Add(this.buttonOpenFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "RDPForm";
            this.Text = "Ramer-Douglas-Peucker algorithm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOpenFile;
        private System.Windows.Forms.TextBox textBoxSourseSize;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxFilePath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxSmoothedSize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxEpsilon;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxBlockSize;
        private System.Windows.Forms.Button buttonConvert;
        private System.Windows.Forms.Button buttonSmoothedGraph;
        private System.Windows.Forms.OpenFileDialog openFile;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxAdditions;
        private System.Windows.Forms.TextBox textBoxMultiplications;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxKmax;
        private System.Windows.Forms.TextBox textBoxKmin;
    }
}

