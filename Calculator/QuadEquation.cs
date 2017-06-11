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
                return (-B + Complex.Pow(D, 0.5)) / 2 * A;
            }
        }

        public Complex X2
        {
            get
            {
                return (-B - Complex.Pow(D, 0.5)) / 2 * A;
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
    }
}
