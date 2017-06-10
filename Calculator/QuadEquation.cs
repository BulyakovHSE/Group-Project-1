using System.Numerics;

namespace Calculator
{
    public class QuadEquation
    {
        public Complex A { get; set; }

        public Complex B { get; set; }

        public Complex C { get; set; }

        public Complex D
        {
            get
            {
                return B * B - 4 * A * C;
            }
        }

        public Complex X1
        {
            get
            {
                return (-B + Complex.Pow(D, 0.5)) / 2;
            }
        }

        public Complex X2
        {
            get
            {
                return (-B - Complex.Pow(D, 0.5)) / 2;
            }
        }

        public QuadEquation(Complex a, Complex b, Complex c)
        {
            A = a;
            B = b;
            C = c;
        }
    }
}
