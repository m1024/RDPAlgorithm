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
        public int BlockSourseSize { get; set; }
        public int BlockRezultLength { get; set; }
        public int BlockRezultSize { get; set; }
        public ulong Additions { get; set; }
        public ulong Multiplications { get; set; }
        public float СompressionRatio { get; set; }
        public int RecursiveCalls { get; set; }
    }

    public struct CompressMyPoint
    {
        public byte DeltaX;
        public short Y;
    }

    public partial class RDPForm : Form
    {
        private MyPoint[] PRez;
        public short[] SequenceSmoothed;
        public short[] SequenceSourse;

        public RDPForm()
        {
            InitializeComponent();
        }

        #region Логика

        /// <summary>
        /// Преобразование сжатой последовательности обратно в обычную
        /// </summary>
        private void ConvertSmootedSequence()
        {
            SequenceSmoothed = new short[PRez[PRez.Length - 1].X + 1];

            SequenceSmoothed[0] = PRez[0].Y;
            int n = 1;
            for (int i = 1; i < PRez.Length && n < SequenceSmoothed.Length; i++)
            {
                if (PRez[i].X - PRez[i - 1].X > 1)
                {
                    int deltaX = PRez[i].X - PRez[i - 1].X;
                    int deltaY = PRez[i].Y - PRez[i - 1].Y;

                    for (int j = 1; j < (PRez[i].X - PRez[i - 1].X) && n < SequenceSmoothed.Length; j++)
                        SequenceSmoothed[n++] = (short) ((double) deltaY/deltaX*j + PRez[i - 1].Y);
                    SequenceSmoothed[n++] = PRez[i].Y;
                }
                else
                {
                    SequenceSmoothed[n++] = PRez[i].Y;
                }
            }
        }

        /// <summary>
        /// Расчет погрешности
        /// </summary>
        /// <param name="maxDeviation">Максимальная погрешность</param>
        /// <param name="averageDeviation">Средняя погрешность</param>
        public void CalculateDeviation(out int maxDeviation, out float averageDeviation)
        {
            maxDeviation = 0;
            averageDeviation = 0;
            for (int i = 0; i < SequenceSourse.Length; i++)
            {
                if (maxDeviation < Math.Abs(SequenceSourse[i] - SequenceSmoothed[i]))
                    maxDeviation = Math.Abs(SequenceSourse[i] - SequenceSmoothed[i]);
                averageDeviation += Math.Abs(SequenceSourse[i] - SequenceSmoothed[i]);
            }
            averageDeviation /= SequenceSourse.Length;
        }

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
                RDPAlgorithm.Additions += 3;
                newPRrez[i].DeltaX = (byte) (PRez[i].X - PRez[i - 1].X);
                newPRrez[i].Y = PRez[i].Y;
            }
            textBoxSmoothedSize.Text = String.Format("{0:0,0}", (newPRrez.Length*3));
            textBoxAdditions.Text = String.Format("{0:0,0}", RDPAlgorithm.Additions);
            textBoxMultiplications.Text = String.Format("{0:0,0}", RDPAlgorithm.Multiplications);
            int maxDeviation;
            float averageDeviation;
            CalculateDeviation(out maxDeviation, out averageDeviation);
            textBoxAverageDeviation.Text = averageDeviation.ToString();
            textBoxMaxDeviation.Text = String.Format("{0:0,0}", maxDeviation);
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

            textBoxSourseSize.Text = String.Format("{0:0,0}", numbersInt.Length*2);
            return numbersInt;
        }

        /// <summary>
        /// Разбиение последовательности на части и ее преобразование с сохранением и отображением данных статистики
        /// </summary>
        private void Convert()
        {
            RDPAlgorithm.Additions = 0;
            RDPAlgorithm.Multiplications = 0;

            MyPoint[] points = new MyPoint[SequenceSourse.Length];

            for (int i = 0; i < SequenceSourse.Length; i++)
            {
                points[i].X = i;
                points[i].Y = SequenceSourse[i];
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
                Statistic blockStat = new Statistic
                {
                    Additions = RDPAlgorithm.Additions,
                    Multiplications = RDPAlgorithm.Multiplications
                };
                RDPAlgorithm.MaxCalls = 0;
                RDPAlgorithm.Calls = 0;
                swatch.Start(); // старт

                smoothSequence.Add(RDPAlgorithm.DouglasPeucker(points, i,
                    (i + blockSize < points.Length) ? i + blockSize : points.Length - 1,
                    (textBoxEpsilon.Text == "") ? 1 : (float) Math.Pow(float.Parse(textBoxEpsilon.Text), 2)));

                smooothSeqLength += smoothSequence.ElementAt(j++).Length - 1;
                //-1 чтобы убрать последний элемент, т.к. он будет началом следующего блока

                swatch.Stop(); // стоп

                //сохраняем статситку
                blockStat.Time = swatch.Elapsed;
                blockStat.BlockRezultLength = smoothSequence.ElementAt(i/blockSize).Length;
                blockStat.BlockRezultSize = blockStat.BlockRezultLength*3;
                blockStat.BlockSourseSize = blockSize*2;
                blockStat.Additions = RDPAlgorithm.Additions - blockStat.Additions;
                blockStat.Multiplications = RDPAlgorithm.Multiplications - blockStat.Multiplications;
                blockStat.СompressionRatio =
                    (float) Math.Round(blockSize/(smoothSequence.ElementAt(i/blockSize).Length*1.5), 3);
                blockStat.RecursiveCalls = RDPAlgorithm.MaxCalls;

                stat.Add(blockStat);
                progressBar.Value++;
            }

            progressBar.Value = 0;
            dataGridView.DataSource = stat;
            dataGridView.Columns[0].HeaderText = @"Время преобразования";
            dataGridView.Columns[1].HeaderText = @"Размер исходного блока, байт";
            dataGridView.Columns[2].HeaderText = @"Длина сжатого блока";
            dataGridView.Columns[3].HeaderText = @"Размер сжатого блока, байт";
            dataGridView.Columns[4].HeaderText = @"Слож ений";
            dataGridView.Columns[5].HeaderText = @"Умнож ений";
            dataGridView.Columns[6].HeaderText = @"Коэф. сжатия";
            dataGridView.Columns[7].HeaderText = @"Рекурсия";
            dataGridView.Columns[0].Width = 100;
            dataGridView.RowHeadersWidth = 60;
            for (int i = 1; i < 8; i++)
                dataGridView.Columns[i].Width = 55;
            for (int i = 0; i < stat.Count; i++)
                dataGridView.Rows[i].HeaderCell.Value = (i + 1).ToString();

            PRez = new MyPoint[smooothSeqLength + 1];
            j = 0;
            foreach (var block in smoothSequence)
            {
                for (int i = 0; i < block.Length - 1; i++)
                    PRez[j++] = block[i];
            }
            PRez[j] = smoothSequence.Last().Last();

            textBoxKmin.Text = stat.OrderBy(u => u.СompressionRatio).First().СompressionRatio.ToString();
            textBoxKmax.Text = stat.OrderByDescending(u => u.СompressionRatio).First().СompressionRatio.ToString();
            textBoxRecursiveCalls.Text = stat.OrderByDescending(u => u.RecursiveCalls).First().RecursiveCalls.ToString();
        }

        #endregion

        #region Кнопки

        private void buttonSmoothedGraph_Click(object sender, EventArgs e)
        {
            if (SequenceSourse != null || SequenceSmoothed != null)
            {
                Form gForm = new Graph();
                gForm.Show(this);
            }
            else
            {
                MessageBox.Show(@"Последовательности не сформированы. Нажмите кнопку Преобразовать.");
            }
        }

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            openFile.Filter = @"текстовые файлы | *.txt";
            if (openFile.ShowDialog(this) == DialogResult.OK)
            {
                textBoxFilePath.Text = openFile.FileName;
            }
        }

        private void buttonConvert_Click(object sender, EventArgs e)
        {
            try
            {
                SequenceSourse = ReadAndPrepare();
                Convert();
                ConvertSmootedSequence();
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
