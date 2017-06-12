using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            Complex c;
            ExpressionCalculator.TryParseComplex("1", out c);
            //MessageBox.Show(c.ToString());

        }
    }
}
