using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void result_Click(object sender, EventArgs e)
        {
            QuadEquation qe;
            try
            {
                qe = new QuadEquation(firstK.Text, secondK.Text, thirdK.Text);
                Complex d = qe.D;
                if (d.Imaginary==0) resultOfD.Text = "" + d.Real; else
                resultOfD.Text = "" + d.Real + " + " + d.Imaginary + "i";
                d = qe.X1;
                if (d.Imaginary == 0) resultX1.Text = "" + d.Real;
                else
                    resultX1.Text = "" + d.Real + " + " + d.Imaginary + "i";
                d = qe.X2;
                if (d.Imaginary == 0) resultX2.Text = "" + d.Real;
                else
                    resultX2.Text = "" + d.Real + " + " + d.Imaginary + "i";

            }
            catch (Exception ept)
            {
                MessageBox.Show("Некорректные коэффициенты", "Error");
            }
        }

        private void firstK_TextChanged(object sender, EventArgs e)
        {
            firstKD.Text = firstK.Text;
            firstInX1.Text = firstK.Text;
            firstInX2.Text = firstK.Text;
        }

        private void secondKD_TextChanged(object sender, EventArgs e)
        {

        }

        private void secondK_TextChanged(object sender, EventArgs e)
        {
            secondKD.Text = secondK.Text;
            secondInX1.Text = secondK.Text;
            secondInX2.Text = secondK.Text;
        }

        private void thirdK_TextChanged(object sender, EventArgs e)
        {
            thirdKD.Text = thirdK.Text;
        }

        private void resultOfD_TextChanged(object sender, EventArgs e)
        {
            dInX1.Text = resultOfD.Text;
            dInX2.Text = resultOfD.Text;
        }
    }
}
