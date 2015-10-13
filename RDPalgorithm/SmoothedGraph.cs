using System;
using System.Drawing;
using System.Windows.Forms;

namespace RDPalgorithm
{
    public partial class SmoothedGraph : Form
    {
        private int BeginGraph { get; set; }

        public SmoothedGraph()
        {
            InitializeComponent();
            BeginGraph = 0;
        }

        private void DrawLine(Point onePoint, Point twoPoint, PaintEventArgs e)
        {
            var myPen = new Pen(Color.DarkSlateGray, 1);
            e.Graphics.DrawLine(myPen, onePoint, twoPoint);
            myPen.Dispose();
        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                var ownerForm = (RDPForm) this.Owner;
                int max = 0, min = int.MaxValue;
                for (int i = 0; i < ownerForm.PRez.Length; i++)
                {
                    if (ownerForm.PRez[i].Y > max) max = ownerForm.PRez[i].Y;
                    if (ownerForm.PRez[i].Y < min) min = ownerForm.PRez[i].Y;
                }

                float k = (float) panel.Height/(max - min);

                for (int i = BeginGraph + 1;
                    (i < ownerForm.PRez.Length) && (ownerForm.PRez[i].X <= ownerForm.PRez[BeginGraph].X + panel.Width);
                    i++)
                {
                    if ((i < ownerForm.PRez.Length) && (i > 0))
                    {
                        var onePoint = new Point(ownerForm.PRez[i - 1].X - ownerForm.PRez[BeginGraph].X,
                            panel.Bottom - (int) (ownerForm.PRez[i - 1].Y*k) - panel.Height/2);
                        var twoPoint = new Point(ownerForm.PRez[i].X - ownerForm.PRez[BeginGraph].X,
                            panel.Bottom - (int) (ownerForm.PRez[i].Y*k) - panel.Height/2);
                        DrawLine(onePoint, twoPoint, e);
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
            var ownerForm = (RDPForm) this.Owner;

            for (int i = BeginGraph; i >= 0; i--)
            {
                if (ownerForm.PRez[i].X <= ownerForm.PRez[BeginGraph].X - panel.Width)
                {
                    BeginGraph = i + 1;
                    break;
                }
            }

            panel.Refresh();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            var ownerForm = (RDPForm) this.Owner;

            for (int i = 0; i < ownerForm.PRez.Length; i++)
            {
                if (ownerForm.PRez[i].X >= ownerForm.PRez[BeginGraph].X + panel.Width)
                {
                    if (i != ownerForm.PRez.Length - 1)
                        BeginGraph = i;
                    break;
                }
            }

            panel.Refresh();
        }
    }
}
