using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xrf.Imaging.Filters.Matrix;
using Xrf.Imaging.Filters.Substitution;

namespace XRFGDITest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_open_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            string fname = ofd.FileName;

            pb_orig.Load(fname);
            pb_presub.Image = pb_orig.Image;
            pb_gscale.Image = GrayscaleMatrix.Draw(pb_orig.Image);
            pb_sepia.Image = SepiaMatrix.Draw(pb_orig.Image);
            pb_neg.Image = NegativeMatrix.Draw(pb_orig.Image);
            pb_trans.Image = TransparencyMatrix.Draw(pb_orig.Image, 0.3f);

            var violet = new ColorMatrix(new float[][]
            {
               new float[]{.1f, .63f, .43f, 0, 0},
               new float[]{.59f, 0, .77f, 0, 0},
               new float[]{.56f, .11f, .11f, 0, 0},
               new float[]{0, 0, 0, 1, 0},
               new float[]{0, 0, 0, 0, 1}
             });

            var cm = new ColorMatrix(new float[][]
            {
               new float[]{0, 0, 0, 0, 1},
               new float[]{0, 0, 0, 7f, 0},
               new float[]{0, 0, 1, 0, 0},
               new float[]{0, 1, 0, 0, 0},
               new float[]{1, 0, 0, 0, 0}
             });

            pb_custmat.Image = CustomMatrix.Draw(pb_orig.Image, cm);
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            var filter = new ColorSubstitutionFilter()
            {
                SourceColor = pnlsubclr.BackColor,
                NewColor = pnlrplclr.BackColor,
                ThresholdValue = threshold.Value
            };

            Bitmap bmp = ((Bitmap)pb_presub.Image).Format32bppArgbCopy();
            pb_postsub.Image = SubstitutionIO.SubstituteColour(bmp, filter);
        }

        private void pnlsubclr_Click(object sender, EventArgs e)
        {
            if (cld.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            pnlsubclr.BackColor = cld.Color;
        }

        private void pnlrplclr_Click(object sender, EventArgs e)
        {
            if (cld.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            pnlrplclr.BackColor = cld.Color;
        }
    }
}
