using System;
using System.Drawing;
using System.Windows.Forms;

namespace RDPalgorithm
{
    public partial class SmoothedGraph : Form
    {
        private int beginGraph { get; set; }

        public SmoothedGraph()
        {
            InitializeComponent();
            beginGraph = 0;
        }

        private void DrawLine(Point onePoint, Point twoPoint, PaintEventArgs e)
        {
            var myPen = new Pen(Color.DarkSlateGray, 1);
            e.Graphics.DrawLine(myPen, onePoint, twoPoint);
            myPen.Dispose();
        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            var ownerForm = (RDPForm) this.Owner;
            int max = 0, min = int.MaxValue;
            for (int i = 0; i < ownerForm.pRez.Length; i++)
            {
                if (ownerForm.pRez[i].Y > max) max = ownerForm.pRez[i].Y;
                if (ownerForm.pRez[i].Y < min) min = ownerForm.pRez[i].Y;
            }

            float k = (float) panel.Height/(max - min);

            for (int i = beginGraph + 1;
                (i < ownerForm.pRez.Length) && (ownerForm.pRez[i].X <= ownerForm.pRez[beginGraph].X + panel.Width);
                i++)
            {
                if ((i < ownerForm.pRez.Length) && (i > 0))
                {
                    var onePoint = new Point(ownerForm.pRez[i - 1].X - ownerForm.pRez[beginGraph].X,
                        panel.Bottom - (int) (ownerForm.pRez[i - 1].Y*k) - panel.Height/2);
                    var twoPoint = new Point(ownerForm.pRez[i].X - ownerForm.pRez[beginGraph].X,
                        panel.Bottom - (int) (ownerForm.pRez[i].Y*k) - panel.Height/2);
                    DrawLine(onePoint, twoPoint, e);
                }
            }
        }

        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            var ownerForm = (RDPForm) this.Owner;

            for (int i = beginGraph; i >= 0; i--)
            {
                if (ownerForm.pRez[i].X <= ownerForm.pRez[beginGraph].X - panel.Width)
                {
                    beginGraph = i + 1;
                    break;
                }
            }

            panel.Refresh();
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            var ownerForm = (RDPForm) this.Owner;

            for (int i = 0; i < ownerForm.pRez.Length; i++)
            {
                if (ownerForm.pRez[i].X >= ownerForm.pRez[beginGraph].X + panel.Width)
                {
                    if (i != ownerForm.pRez.Length - 1)
                        beginGraph = i;
                    break;
                }
            }

            panel.Refresh();
        }
    }
}
