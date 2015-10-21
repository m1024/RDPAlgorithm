using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RDPalgorithm
{
    public partial class SmoothedGraph : Form
    {
        private int _beginGraph;

        private int BeginGraph
        {
            get { return _beginGraph; }
            set
            {
                if (value < 0) return;
                if (_sequence != null)
                {
                    if (value <= _sequence.Length)
                        _beginGraph = value;
                }
                else
                {
                    _beginGraph = value;
                }
            }
        }

        private short[] _sequence;

        public SmoothedGraph()
        {
            InitializeComponent();
            BeginGraph = 0;
        }

        private void DrawLine(Point onePoint, Point twoPoint, PaintEventArgs e, Color color)
        {
            var myPen = new Pen(color, 1);
            e.Graphics.DrawLine(myPen, onePoint, twoPoint);
            myPen.Dispose();
        }

        private void DrawSpectrum(PaintEventArgs e)
        {
            var ownerForm = (RDPForm) this.Owner;
            if (_sequence == null) _sequence = ownerForm.ReadAndPrepare();

            int sMin = _sequence.Min(); //соответствует нулю
            int[] spectrum = new int[_sequence.Max() - sMin + 1];

            for (int i = 0; i < _sequence.Length; i++)
            {
                spectrum[_sequence[i] - sMin]++;
            }

            float k = (float) (panel.Height - 30)/(spectrum.Max() - spectrum.Min());
            float k2 = (float) panel.Width/spectrum.Length;

            for (int i = 1; i < spectrum.Length; i++)
            {
                DrawLine(new Point((int) (k2*(i - 1)), panel.Bottom - (int) (k*spectrum[i - 1]) - panel.Top - 20),
                    new Point((int) (k2*i), panel.Bottom - (int) (k*spectrum[i]) - panel.Top - 20), e,
                    Color.DarkSlateGray);
            }
        }

        private void DrawOriginal(PaintEventArgs e, int height, int topleft)
        {
            var ownerForm = (RDPForm) this.Owner;
            if (_sequence == null) _sequence = ownerForm.ReadAndPrepare();

            float k = (float) height/(_sequence.Max() - _sequence.Min());

            for (int i = BeginGraph + 1; i < BeginGraph + panel.Width; i++)
            {
                if (i < _sequence.Length && i > 0)
                {
                    var onePoint = new Point(i - BeginGraph - 1,
                        panel.Bottom - (int) (_sequence[i - 1]*k) - height/2 + topleft);
                    var twoPoint = new Point(i - BeginGraph, panel.Bottom - (int) (_sequence[i]*k) - height/2 + topleft);
                    DrawLine(onePoint, twoPoint, e, Color.Blue);
                }
            }
        }

        private void DrawSmoothed(PaintEventArgs e, int height, int topleft)
        {
            var ownerForm = (RDPForm) this.Owner;
            short[] sequence2 = new short[ownerForm.PRez[ownerForm.PRez.Length - 1].X + 1];

            sequence2[0] = ownerForm.PRez[0].Y;
            int n = 1;
            for (int i = 1; i < ownerForm.PRez.Length && n < sequence2.Length; i++)
            {
                if (ownerForm.PRez[i].X - ownerForm.PRez[i - 1].X > 1)
                {
                    int deltaX = ownerForm.PRez[i].X - ownerForm.PRez[i - 1].X;
                    int deltaY = ownerForm.PRez[i].Y - ownerForm.PRez[i - 1].Y;

                    for (int j = 1; j < (ownerForm.PRez[i].X - ownerForm.PRez[i - 1].X) && n < sequence2.Length; j++)
                        sequence2[n++] = (short) ((double) deltaY/deltaX*j + ownerForm.PRez[i - 1].Y);
                    sequence2[n++] = ownerForm.PRez[i].Y;
                }
                else
                {
                    sequence2[n++] = ownerForm.PRez[i].Y;
                }
            }

            float k = (float) height/(sequence2.Max() - sequence2.Min());
            for (int i = BeginGraph + 1; i < BeginGraph + panel.Width; i++)
            {
                if (i < sequence2.Length && i > 0)
                {
                    var onePoint = new Point(i - BeginGraph - 1,
                        panel.Bottom - (int) (sequence2[i - 1]*k) - height/2 + topleft);
                    var twoPoint = new Point(i - BeginGraph, panel.Bottom - (int) (sequence2[i]*k) - height/2 + topleft);
                    DrawLine(onePoint, twoPoint, e, Color.Red);
                }
            }
        }

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
                    DrawSpectrum(e);
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
    }

    public sealed class MyPanel : Panel
    {
        public MyPanel()
        {
            DoubleBuffered = true;
        }
    }
}
