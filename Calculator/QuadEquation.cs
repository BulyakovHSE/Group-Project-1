using System.Numerics;
using StringExpressionsLibrary;

namespace Calculator
{
    public class QuadEquation
    {
        private Complex a, b, c, d, x1, x2;

        public Complex A { get { return a; } set { a = value; UpdateProperties(); } }

        public Complex B { get { return b; } set { b = value; UpdateProperties(); } }

        public Complex C { get { return c; } set { c = value; UpdateProperties(); } }

        public Complex D
        {
            get
            {
                return d;
            }
        }

        public Complex X1
        {
            get
            {
                return x1;
            }
        }

        public Complex X2
        {
            get
            {
                return x2;
            }
        }

        public QuadEquation(Complex a, Complex b, Complex c)
        {
            A = a;
            B = b;
            C = c;
        }

        public QuadEquation(string a, string b, string c)
        {
            Complex x1, x2, x3;
            
            if (ExpressionCalculator.TryParseComplex(a, out x1) &&
                ExpressionCalculator.TryParseComplex(b, out x2) &&
                ExpressionCalculator.TryParseComplex(c, out x3))
            {

                A = x1;
                B = x2;
                C = x3;
            }
            else throw new System.Exception("Один из переданных параметров не является комплексным числом!");
        }

        private void UpdateProperties()
        {
            d = B * B - 4 * A * C;
            if (A == 0)
            {
                x1 = -C / B;
                x2 = x1;
            }
            else
            {
                x1 = (-B + Complex.Pow(D, 0.5)) / 2 * A;
                x2 = (-B - Complex.Pow(D, 0.5)) / 2 * A;
            }
        }
    }
}
