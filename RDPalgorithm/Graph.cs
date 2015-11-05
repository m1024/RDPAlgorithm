using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace RDPalgorithm
{
    public partial class Graph : Form
    {
        private int _beginGraph;
        private int[] spectrumSourse;
        private int[] spectrumSmoothed;

        private int BeginGraph
        {
            get { return _beginGraph; }
            set
            {
                var ownerForm = (RDPForm) this.Owner;

                if (value < 0) return;
                if (ownerForm != null && ownerForm.SequenceSourse != null)
                {
                    if (value <= ownerForm.SequenceSourse.Length)
                        _beginGraph = value;
                }
                else
                {
                    _beginGraph = value;
                }
            }
        }

        public Graph()
        {
            InitializeComponent();
            BeginGraph = 0;
        }

        private static void DrawLine(Point onePoint, Point twoPoint, PaintEventArgs e, Color color)
        {
            var myPen = new Pen(color, 1);
            e.Graphics.DrawLine(myPen, onePoint, twoPoint);
            myPen.Dispose();
        }

        /// <summary>
        /// Вычисление спектра
        /// </summary>
        /// <param name="sequence">Дискретный сигнал</param>
        /// <returns>Амплитудный спектр сигнала</returns>
        private static int[] CalculateSpectrum(IReadOnlyList<short> sequence)
        {
            var n = (int) Math.Log(sequence.Count, 2) + 1;
            var len = (int) Math.Pow(2, n);
            var sLen = sequence.Count;

            var complexSequence = new Complex[len];

            //длина должна быть кратна 2, лишнее надо заполнять нулями
            for (int i = 0; i < len; i++)
                complexSequence[i] = (i < sLen)
                    ? new Complex(sequence[i], 0)
                    : complexSequence[i] = new Complex(0, 0);

            var spectrumComplex = FFT.fft(complexSequence);

            var spectrum = new int[spectrumComplex.Length/2];
            for (int i = 0; i < spectrumComplex.Length/2; i++)
                spectrum[i] = (int) Math.Sqrt(spectrumComplex[i].Real*spectrumComplex[i].Real +
                                              spectrumComplex[i].Imaginary*spectrumComplex[i].Imaginary);
            return spectrum;
        }

        /// <summary>
        /// Отрисовка графика спектра
        /// </summary>
        private void DrawSpectrum(PaintEventArgs e, IReadOnlyList<int> spectrum, Color color)
        {
            var k = (float) (panel.Height - 30)/(spectrum.Max() - spectrum.Min());
            var k2 = panel.Width/((float) spectrum.Count/5);
            var k3 = (float) 25000/spectrum.Count;

            //отрисуем координаты
            DrawLine(new Point(panel.Left, panel.Bottom - 15), new Point(panel.Right, panel.Bottom - 15), e, Color.Black);
            for (int i = 0; i < 5000; i += 500)
            {
                DrawLine(new Point((int) ((i/k3)*k2), panel.Bottom - 13),
                    new Point((int) ((i/k3)*k2), panel.Bottom - 17), e, Color.Black);
                e.Graphics.DrawString(i.ToString(), new Font("Arial", 7), new SolidBrush(Color.Black),
                    panel.Left + (int) ((i/k3)*k2), panel.Bottom - 15);
            }

            for (int i = 1; i < spectrum.Count; i++)
                DrawLine(new Point((int) (k2*(i - 1)), panel.Bottom - (int) (k*spectrum[i - 1]) - panel.Top - 20),
                    new Point((int) (k2*i), panel.Bottom - (int) (k*spectrum[i]) - panel.Top - 20), e,
                    color);
        }

        /// <summary>
        /// Отрисовка исходного графика
        /// </summary>
        private void DrawOriginal(PaintEventArgs e, int height, int topleft)
        {
            var ownerForm = (RDPForm) this.Owner;

            var k = (float) height/(ownerForm.SequenceSourse.Max() - ownerForm.SequenceSourse.Min());

            for (int i = BeginGraph + 1; i < BeginGraph + panel.Width; i++)
            {
                if (i >= ownerForm.SequenceSourse.Length || i <= 0) continue;

                var onePoint = new Point(i - BeginGraph - 1,
                    panel.Bottom - (int) (ownerForm.SequenceSourse[i - 1]*k) - height/2 + topleft);
                var twoPoint = new Point(i - BeginGraph,
                    panel.Bottom - (int) (ownerForm.SequenceSourse[i]*k) - height/2 + topleft);
                DrawLine(onePoint, twoPoint, e, Color.Blue);
            }
        }

        /// <summary>
        /// Отображение переобразованной последовательности в виде графика
        /// </summary>
        private void DrawSmoothed(PaintEventArgs e, int height, int topleft)
        {
            var ownerForm = (RDPForm) this.Owner;

            var k = (float) height/(ownerForm.SequenceSmoothed.Max() - ownerForm.SequenceSmoothed.Min());
            for (int i = BeginGraph + 1; i < BeginGraph + panel.Width; i++)
            {
                if (i >= ownerForm.SequenceSmoothed.Length || i <= 0) continue;

                var onePoint = new Point(i - BeginGraph - 1,
                    panel.Bottom - (int) (ownerForm.SequenceSmoothed[i - 1]*k) - height/2 + topleft);
                var twoPoint = new Point(i - BeginGraph,
                    panel.Bottom - (int) (ownerForm.SequenceSmoothed[i]*k) - height/2 + topleft);
                DrawLine(onePoint, twoPoint, e, Color.Red);
            }
        }

        /// <summary>
        /// Отрисовка графиков в зависимости от указанного режима отображения
        /// </summary>
        private void panel_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (radioButton1.Checked)
                {
                    DrawOriginal(e, panel.Height, 0);
                    DrawSmoothed(e, panel.Height, 0);
                }
                else if (radioButton2.Checked)
                {
                    DrawOriginal(e, panel.Height, -panel.Height/4);
                    DrawSmoothed(e, panel.Height, panel.Height/4);
                }
                else if (radioButton3.Checked)
                {
                    DrawOriginal(e, panel.Height/2, -(panel.Height/4)*2);
                    DrawSmoothed(e, panel.Height/2, 0);
                }
                else if (radioButton4.Checked)
                {
                    DrawOriginal(e, panel.Height, 0);
                }
                else if (radioButton5.Checked)
                {
                    DrawSmoothed(e, panel.Height, 0);
                }
                else if (radioButton6.Checked)
                {
                    if (spectrumSourse != null && spectrumSmoothed != null)
                    {
                        DrawSpectrum(e, spectrumSourse, Color.Blue);
                        DrawSpectrum(e, spectrumSmoothed, Color.Red);
                    }
                    else
                    {
                        var ownerForm = (RDPForm) this.Owner;
                        DrawSpectrum(e, spectrumSourse = CalculateSpectrum(ownerForm.SequenceSourse), Color.Blue);
                        DrawSpectrum(e, spectrumSmoothed = CalculateSpectrum(ownerForm.SequenceSmoothed), Color.Red);
                    }
                }
                else if (radioButton7.Checked)
                {
                    if (spectrumSourse != null && spectrumSmoothed != null)
                    {
                        DrawSpectrum(e, spectrumSmoothed, Color.Red);
                        DrawSpectrum(e, spectrumSourse, Color.Blue);
                    }
                    else
                    {
                        var ownerForm = (RDPForm) this.Owner;
                        DrawSpectrum(e, spectrumSmoothed = CalculateSpectrum(ownerForm.SequenceSmoothed), Color.Red);
                        DrawSpectrum(e, spectrumSourse = CalculateSpectrum(ownerForm.SequenceSourse), Color.Blue);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            BeginGraph -= panel.Width;
            panel.Refresh();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            BeginGraph += panel.Width;
            panel.Refresh();
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            panel.Refresh();
        }

        private void Graph_Shown(object sender, EventArgs e)
        {
            var ownerForm = (RDPForm) this.Owner;
            new Thread(() => spectrumSourse = CalculateSpectrum(ownerForm.SequenceSourse)).Start();
            new Thread(() => spectrumSmoothed = CalculateSpectrum(ownerForm.SequenceSmoothed)).Start();
        }
    }

    public sealed class MyPanel : Panel
    {
        public MyPanel()
        {
            DoubleBuffered = true;
        }
    }
}
