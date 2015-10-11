using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RDPalgorithm
{
    public partial class OriginalGraph : Form
    {
        private int beginGraph { get; set; }

        private int endGraph { get; set; }

        public OriginalGraph()
        {
            InitializeComponent();
            beginGraph = 0;
            endGraph = beginGraph + panel.Width;
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
            short[] sequence = ownerForm.ReadAndPrepare();

            float k = (float) panel.Height/(sequence.Max() - sequence.Min());

            for (int i = beginGraph + 1; i < endGraph; i++)
            {
                if (i < sequence.Length && i > 0)
                {
                    var onePoint = new Point(i - beginGraph - 1,
                        panel.Bottom - (int) (sequence[i - 1]*k) - panel.Height/2);
                    var twoPoint = new Point(i - beginGraph, panel.Bottom - (int) (sequence[i]*k) - panel.Height/2);
                    DrawLine(onePoint, twoPoint, e);
                }
            }
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            beginGraph += panel.Width;
            endGraph += panel.Width;
            panel.Refresh();
        }

        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            beginGraph -= panel.Width;
            endGraph -= panel.Width;
            panel.Refresh();
        }
    }
}
