using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace RDPalgorithm
{
    public struct Statistic
    {
        public TimeSpan Time { get; set; }
        public int BlockRezultSize { get; set; }
        public float СompressionRatio { get; set; }
    }

    public struct CompressMyPoint
    {
        public byte DeltaX;
        public short Y;
    }

    public partial class RDPForm : Form
    {
        public MyPoint[] PRez;

        public RDPForm()
        {
            InitializeComponent();
        }

        #region Логика

        /// <summary>
        /// Конечная последовательность, координата по Х закодирована разностным кодированием,
        /// из-за чего ее можно хранить в 1 байте
        /// </summary>
        private void CompressRezult()
        {
            CompressMyPoint[] newPRrez = new CompressMyPoint[PRez.Length];

            newPRrez[0].DeltaX = 0;
            newPRrez[0].Y = PRez[0].Y;

            for (int i = 1; i < PRez.Length; i++)
            {
                newPRrez[i].DeltaX = (byte) (PRez[i].X - PRez[i - 1].X);
                newPRrez[i].Y = PRez[i].Y;
            }
            textBoxSmoothedSize.Text = (newPRrez.Length*3).ToString();
        }

        /// <summary>
        /// Считывание из файла и преобразование значений из чисел с плавающей точкой в целые
        /// </summary>
        /// <returns>Массив двухбайтовых целочисленных значений</returns>
        public short[] ReadAndPrepare()
        {
            string file = File.ReadAllText(openFile.FileName);
            var numbers = file.Split(new char[] {'\n', '\r'}).ToList();
            List<double> numDoublesList = new List<double>();

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");

            foreach (var number in numbers)
            {
                double d;
                if (Double.TryParse(number, out d))
                    numDoublesList.Add(d);
            }

            double maxValue = numDoublesList.Max();
            double minValue = numDoublesList.Min();
            double k = 8192/(maxValue - minValue);

            short[] numbersInt = new short[numDoublesList.Count];
            for (int i = 0; i < numbersInt.Length; i++)
                numbersInt[i] = (System.Convert.ToInt16(numDoublesList.ElementAt(i)*k));

            textBoxSourseSize.Text = (numbersInt.Length*2).ToString();
            return numbersInt;
        }

        /// <summary>
        /// Разбиение последовательности на части и ее преобразование с сохранением и отображением данных статистики
        /// </summary>
        private void Convert()
        {
            short[] originalSequence = ReadAndPrepare();
            MyPoint[] points = new MyPoint[originalSequence.Length];

            for (int i = 0; i < originalSequence.Length; i++)
            {
                points[i].X = i;
                points[i].Y = originalSequence[i];
            }

            int blockSize = int.Parse(textBoxBlockSize.Text);
            List<MyPoint[]> smoothSequence = new List<MyPoint[]>();
            List<Statistic> stat = new List<Statistic>();
            progressBar.Maximum = points.Length/blockSize + 1;
            int smooothSeqLength = 0;
            int j = 0;

            for (int i = 0; i < points.Length; i += blockSize)
            {
                System.Diagnostics.Stopwatch swatch = new System.Diagnostics.Stopwatch(); // создаем объект
                swatch.Start(); // старт

                smoothSequence.Add(RDPAlgorithm.DouglasPeucker(points, i,
                    (i + blockSize < points.Length) ? i + blockSize : points.Length - 1,
                    (textBoxEpsilon.Text == "") ? 1 : float.Parse(textBoxEpsilon.Text)));

                smooothSeqLength += smoothSequence.ElementAt(j++).Length;

                swatch.Stop(); // стоп

                Statistic blockStat = new Statistic();
                blockStat.Time = swatch.Elapsed;
                blockStat.BlockRezultSize = smoothSequence.ElementAt(i/blockSize).Length;
                blockStat.СompressionRatio =
                    (float) Math.Round(blockSize/(smoothSequence.ElementAt(i/blockSize).Length*1.5), 3);

                stat.Add(blockStat);
                progressBar.Value++;
            }

            progressBar.Value = 0;
            dataGridView.DataSource = stat;
            dataGridView.Columns[0].HeaderText = @"Время преобразования";
            dataGridView.Columns[1].HeaderText = @"Длина блока";
            dataGridView.Columns[2].HeaderText = @"Коэф. сжатия";
            dataGridView.Columns[0].Width = 100;
            dataGridView.Columns[1].Width = 50;
            dataGridView.Columns[2].Width = 50;

            PRez = new MyPoint[smooothSeqLength];
            j = 0;
            foreach (var block in smoothSequence)
            {
                for (int i = 0; i < block.Length; i++)
                    PRez[j++] = block[i];
            }
        }

        #endregion

        #region Кнопки

        private void buttonOriginalGraph_Click(object sender, EventArgs e)
        {
            if (openFile.FileName != String.Empty)
            {
                Form OGForm = new OriginalGraph();
                OGForm.Show(this);
            }
            else
            {
                MessageBox.Show("Выберите файл");
            }
        }

        private void buttonSmoothedGraph_Click(object sender, EventArgs e)
        {
            if (openFile.FileName != String.Empty)
            {
                Form SGForm = new SmoothedGraph();
                SGForm.Show(this);
            }
            else
            {
                MessageBox.Show("Выберите файл");
            }
        }

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            if (openFile.ShowDialog(this) == DialogResult.OK)
            {
                textBoxFilePath.Text = openFile.FileName;
            }
        }

        private void buttonConvert_Click(object sender, EventArgs e)
        {
            try
            {
                Convert();
                CompressRezult();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion
    }
}
