using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
   public class Calculator
    {
        public static Form1 Calc(Form1 t1)
        {
            Form1 t = t1;
            QuadEquation qe;
            try
            {
                qe = new QuadEquation(t.firstK.Text, t.secondK.Text, t.thirdK.Text);
                Complex d = qe.D;
                if (d.Imaginary == 0) t.resultOfD.Text = "" + Math.Round(d.Real, 2);
                else
                    t.resultOfD.Text = "" + Math.Round(d.Real, 2) + " + " + Math.Round(d.Imaginary, 2) + "i";
                d = qe.X1;
                if (d.Imaginary == 0) t.resultX1.Text = "" + Math.Round(d.Real, 2);
                else
                    t.resultX1.Text = "" + Math.Round(d.Real, 2) + " + " + Math.Round(d.Imaginary, 2) + "i";
                d = qe.X2;
                if (d.Imaginary == 0) t.resultX2.Text = "" + Math.Round(d.Real, 2);
                else
                    t.resultX2.Text = "" + Math.Round(d.Real, 2) + " + " + Math.Round(d.Imaginary, 2) + "i";
                t1 = t;

            }
            catch
            {
                MessageBox.Show("Некорректные коэффициенты", "Error");
            }
            return t1;
        }
    }
}
