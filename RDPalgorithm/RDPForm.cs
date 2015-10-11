using System;
using System.Globalization;
using System.IO;
using System.Linq;
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

        public short[] ReadAndPrepare()
        {
            string file = File.ReadAllText(openFile.FileName);
            string[] numbers = file.Split(new char[] {' ', '\n', '\r'});

            short[] numbersInt = new short[numbers.Length/3];
            double[] numFloats = new double[numbers.Length/3];

            int j = 0;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            for (int i = 0; i < numbersInt.Length; i++)
            {
                numFloats[i] = Double.Parse(numbers[j], NumberStyles.AllowExponent | NumberStyles.Number);
                j += 3;
            }
            double maxValue = numFloats.Max();
            double minValue = numFloats.Min();
            double k = 8192/(maxValue - minValue);

            for (int i = 0; i < numbersInt.Length; i++)
                numbersInt[i] = (Convert.ToInt16(numFloats[i]*k));

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
            Form SGForm = new SmoothedGraph();
            SGForm.Show(this);
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
            short[] originalSequence = ReadAndPrepare();
            MyPoint[] points = new MyPoint[originalSequence.Length];

            for (int i = 0; i < originalSequence.Length; i++)
            {
                points[i].X = i;
                points[i].Y = originalSequence[i];
            }

            System.Diagnostics.Stopwatch swatch = new System.Diagnostics.Stopwatch(); // создаем объект
            swatch.Start(); // старт
            pRez = RDPAlgorithm.DouglasPeucker(points, textBoxEpsilon.Text == "" ? 1 : float.Parse(textBoxEpsilon.Text));
            swatch.Stop(); // стоп
            richTextBoxConvertTime.Text += swatch.Elapsed + "\n";
        }
    }
}
