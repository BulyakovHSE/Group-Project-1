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
            Calculator.Calc(this);
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }
    }
}
