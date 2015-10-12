using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace RDPalgorithm
{
    public partial class RDPForm : Form
    {
        public MyPoint[] pRez;

        public RDPForm()
        {
            InitializeComponent();
        }

        public short[] ReadAndPrepare2()
        {
            string file = File.ReadAllText(openFile.FileName);
            string[] numbers = file.Split(new char[] { ' ', '\n', '\r' });

            short[] numbersInt = new short[numbers.Length / 3];
            double[] numFloats = new double[numbers.Length / 3];

            int j = 0;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            for (int i = 0; i < numbersInt.Length; i++)
            {
                numFloats[i] = Double.Parse(numbers[j], NumberStyles.AllowExponent | NumberStyles.Number);
                j += 3;
            }
            double maxValue = numFloats.Max();
            double minValue = numFloats.Min();
            double k = 8192 / (maxValue - minValue);

            for (int i = 0; i < numbersInt.Length; i++)
                numbersInt[i] = (System.Convert.ToInt16(numFloats[i] * k));

            return numbersInt;
        }

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
                numbersInt[i] = (System.Convert.ToInt16(numDoublesList.ElementAt(i)* k));

            return numbersInt;
        }

        private void buttonOriginalGraph_Click(object sender, EventArgs e)
        {
            if (openFile.FileName != "openFile")
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
            if (openFile.FileName != "openFile")
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
            richTextBoxConvertTime.Text = "";
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

                richTextBoxConvertTime.Text += swatch.Elapsed.Ticks + @"   " +
                                               smoothSequence.ElementAt(i / blockSize).Length +
                                               @"    k = " +
                                               Math.Round(blockSize / (smoothSequence.ElementAt(i / blockSize).Length * 1.5),
                                                   2) + "\n";
            }

            pRez = new MyPoint[smooothSeqLength];
            j = 0;
            foreach (var block in smoothSequence)
            {
                for (int i = 0; i < block.Length; i++)
                    pRez[j++] = block[i];
            }           
        }

        private void buttonConvert_Click(object sender, EventArgs e)
        {
            try
            {
                Convert();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
