using System;

namespace Calculator
{
    class ExpressionCalculator
    {
        public partial class TreeNode
        {
            public TreeNode()
            {
            }

            public TreeNode(string value)
            {
                Info = value;
            }

            public TreeNode[] Branchs { get; set; }

            public string Info { get; set; }

            public TreeNode Left { get; set; }

            public TreeNode Right { get; set; }
        }

        private static int SN, Count;

        private static char Scansymbol;

        private static string ExprStr;

        private static bool WithError = false;

        public String LastResult { get; set; }

        private static Char[] Letters = {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
            'A', 'B', 'C', 'D', 'E', 'F', 'F', 'G', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        private static double[] Vars = new double[Letters.Length];
        private static bool[] VarExist = new bool[Letters.Length];

        public ExpressionCalculator()
        {
            for (int i = 0; i < Vars.Length; i++)
                Vars[i] = 0;
            for (int i = 0; i < VarExist.Length; i++)
                VarExist[i] = false;
            ExprStr = "";
        }

        public string Calculate(string expr)
        {
            for (int i = 0; i < Vars.Length; i++)
                Vars[i] = 0;
            for (int i = 0; i < VarExist.Length; i++)
                VarExist[i] = false;
            ExprStr = "";
            foreach (Char ch in expr) ExprStr += ch;
            if (ExprStr.Length != 0)
            {
                SN = 0; Count = 0;
                double result;
                WithError = false;
                Scansymbol = ExprStr[SN];
                TreeNode node = EXPRESSION();
                result = ExpandedCalculate(node);
                if (!WithError)
                {
                    LastResult = result.ToString();
                    return LastResult;
                }
                else
                {
                    LastResult = expr;
                    return LastResult;
                }
            }
            else LastResult = expr;
            return LastResult;
        }

        //static private void Main()
        //{
        //    for (int i = 0; i < Vars.Length; i++)
        //        Vars[i] = 0;
        //    for (int i = 0; i < VarExist.Length; i++)
        //        VarExist[i] = false;
        //    Console.Write("Введите выражение (либо ничего для выхода): ");
        //    ExprStr = Console.ReadLine();
        //    if (ExprStr.Length != 0)
        //    {
        //        SN = 0; Count = 0;
        //        double result;
        //        WithError = false;
        //        Scansymbol = ExprStr[SN];
        //        TreeNode node = EXPRESSION();
        //        result = ExpandedCalculate(node);
        //        if (!WithError)
        //            Console.WriteLine("Ответ: " + result);
        //        else
        //            Console.WriteLine("Выражение синтаксически неверно!");
        //        Main();
        //    }
        //}

        private static TreeNode EXPRESSION()
        {
            // Разбор выражения по слагаемым
            int j = 0;
            TreeNode node = new TreeNode();
            node.Branchs = new TreeNode[1];
            node.Branchs[j] = new TreeNode("+");
            node.Branchs[j].Right = ADDEND();
            if (Scansymbol == '+' || Scansymbol == '-')
            {
                while (Scansymbol == '+' || Scansymbol == '-')
                {
                    node = ExpandTree(node, 1);
                    j++;
                    node.Branchs[j] = new TreeNode(Scansymbol.ToString());
                    NEXTSYMBOL();
                    node.Branchs[j].Right = ADDEND();
                }
            }// В условном операторе взята с отрицание дизъюнкция двух единственных возможных варантов завершения выражения.
            // Если сейчас не конец выражения и текущий символ - закрывающая скобка, и количество открывающих скобок больше количества закрывающих
            // Или если сейчас конец выражения и количество открывающих скобок равно количеству закрывающих и текущий символ - знак конца строки
            if (!(SN != -1 && Scansymbol == ')' && Count > 0 || SN == -1 && Count == 0 && Scansymbol == '|'))
            {
                //Console.WriteLine("Слагаемое не распознается!");
                WithError = true;
            }
            return node;
        }

        private static TreeNode ADDEND()
        {
            // Разбор слагаемого по множителям
            int j = 0;
            TreeNode node = new TreeNode();
            node.Branchs = new TreeNode[1];
            node.Branchs[j] = new TreeNode("+");
            node.Branchs[j].Right = FACTOR();
            if (Scansymbol == '*' || Scansymbol == '/')
            {
                while (Scansymbol == '*' || Scansymbol == '/')
                {
                    node = ExpandTree(node, 1); j++;
                    node.Branchs[j] = new TreeNode();
                    if (GETNEXTSYMBOL() == '*')
                        node.Branchs[j].Info = "**";
                    else
                        node.Branchs[j].Info = Scansymbol.ToString();
                    NEXTSYMBOL();
                    node.Branchs[j].Right = FACTOR();
                }
            }
            return node;
        }

        private static void NEXTSYMBOL(bool Space = false)
        {
            // Заменяет текущий сканируемый символ на следующий, если существует
            // Параметр Space отвечает за возможность пропускания пробелов.
            if (SN < ExprStr.Length - 1)
            {
                SN++;
                Scansymbol = ExprStr[SN];
                if (Scansymbol == ' ' && !Space)
                    NEXTSYMBOL();
            }
            else
            {
                SN = -1;
                Scansymbol = '|';
            }
        }

        private static char GETNEXTSYMBOL()
        {
            // Возвращает следующий за текущим символ, если он существует
            if (SN + 1 < ExprStr.Length)
            {
                return ExprStr[SN + 1];
            }
            return '|';
        }

        private static TreeNode FACTOR()
        {
            // Разбор множителя, это либо переменная, либо число, либо выражение, либо возведение в степень.
            TreeNode node = new TreeNode("+");
            if (LETTER())
            {
                node.Info = Scansymbol.ToString();
                NEXTSYMBOL();
            }
            else if (DIGIT())
                node.Info = NUMBER().Info;
            else if (Scansymbol == '(')
            {
                Count++;
                NEXTSYMBOL();
                node.Right = EXPRESSION();
                if (Scansymbol == ')')
                {
                    NEXTSYMBOL();
                    Count--;
                }
                else
                {
                    WithError = true;
                    //Console.WriteLine("Нет закрывающей скобки!");
                }
            }
            else if (Scansymbol == '-')
            {
                NEXTSYMBOL();
                node.Info = "-";
                node.Right = FACTOR();
            }
            else if (Scansymbol == '+')
            {
                NEXTSYMBOL();
                node.Right = FACTOR();
            }
            else if (Scansymbol == '*')
            {
                if (GETNEXTSYMBOL() == '*')
                    WithError = true;
                NEXTSYMBOL();
                node.Right = INVOLUTION();
                node.Info = node.Right.Info;
            }
            else
            {
                //Console.WriteLine("Множитель не распозанется!");
                WithError = true;
            }
            return node;
        }

        private static TreeNode INVOLUTION()
        {
            // Разбор возведения в степень
            int i = 0;
            TreeNode node = new TreeNode();
            node.Branchs = new TreeNode[1];
            node.Branchs[0] = new TreeNode();
            node.Branchs[0] = FACTOR();
            while (Scansymbol == '*' && GETNEXTSYMBOL() == '*')
            {
                NEXTSYMBOL();
                NEXTSYMBOL();
                node = ExpandTree(node, 1); i++;
                node.Branchs[i] = new TreeNode();
                node.Branchs[i] = FACTOR();
            }
            node.Info = i.ToString();
            return node;
        }

        private static bool LETTER()
        {
            // Проверка на то, является ли текущий символ буквой англ. алфавита
            if (SN != -1)
            {
                for (int i = 0, j = Letters.Length - 1; i <= j; i++, j--)
                {
                    if (ExprStr[SN] == Letters[i] || ExprStr[SN] == Letters[j])
                        return true;
                }
            }
            return false;
        }

        private static bool DIGIT()
        {
            // Проверка на то, является ли текущий символ числом
            byte i;
            return Byte.TryParse(Scansymbol.ToString(), out i);
        }

        private static TreeNode NUMBER()
        {
            // Разбор действительного числа, с проверкой на присутствие пробелов в нем
            String Num = "";
            int Count = 0;
            while (DIGIT() || Scansymbol == ',' || Scansymbol == '.')
            {
                if (Scansymbol == ',' || Scansymbol == '.')
                {
                    if (++Count >= 2)
                    {
                        //Console.WriteLine("Ошибка при вводе действительного числа!");
                        WithError = true;
                    }
                    Scansymbol = ',';
                }
                Num += Scansymbol;
                NEXTSYMBOL(true);
            }
            if (Scansymbol == ' ')
                NEXTSYMBOL();
            if (DIGIT())
            {
                //Console.WriteLine("Пробелы в числе недопустимы!");
                WithError = true;
            }
            TreeNode Node = new TreeNode(Num);
            return Node;
        }

        private static double Calculate(TreeNode R)
        {
            // Возвращает значение переменной, либо вводит ее значение, возвращат значение ветви дерева, если она концевая.
            if (WithError)
                return 0;
            double value = 0;
            char CH;
            if (Char.TryParse(R.Info, out CH))
            {
                for (int i = 0; i < Letters.Length; i++)
                    if (CH == Letters[i] && !VarExist[i])
                    {
                        WithError = true;
                        return 0;
                        if (CH == 'e' || CH == 'E')
                        {
                            Vars[i] = Math.E;
                            VarExist[i] = true;
                        }
                        else if (CH == 'p' || CH == 'P')
                        {
                            Vars[i] = Math.PI;
                            VarExist[i] = true;
                        }
                        else
                        {
                            bool input;
                            //Console.Write("Введите значение " + CH + ": ");
                            do
                            {
                                input = Double.TryParse(Console.ReadLine(), out Vars[i]);
                                if (!input) { }
                                //Console.Write("Введите число!\nВведите значение " + CH + ": ");
                            } while (!input);
                            VarExist[i] = true;
                        }
                        return Vars[i];
                    }
                    else if (CH == Letters[i] && VarExist[i])
                        return Vars[i];
            }
            if (Double.TryParse(R.Info, out value))
                return value;
            else return ExpandedCalculate(R);
        }

        private static double CalculateInvolution(TreeNode R)
        {
            // Вычисляет значение возведения в степень.
            int i = int.Parse(R.Info);
            for (int j = i; j <= i && j > 0; j--)
            {
                double b = Calculate(R.Right.Branchs[j]);
                double a = Calculate(R.Right.Branchs[j - 1]);
                R.Right.Branchs[j - 1].Info = Math.Pow(a, b).ToString();
            }
            if (i == 0)
                return double.Parse(R.Right.Branchs[0].Info);
            return Calculate(R.Right.Branchs[0]);
        }

        private static double ExpandedCalculate(TreeNode R)
        {
            // Вычисляет значение выражения.
            double result = 0;
            if (R.Info == "+")
                return Calculate(R.Right);
            else if (R.Info == "-")
                return -1 * Calculate(R.Right);
            else if (R.Info == "*")
                return Calculate(R.Right);
            else if (R.Info == "/")
                return Calculate(R.Right);
            int len = R.Branchs.Length;
            if (len > 0)
            {
                for (int i = 0; i < len; i++)
                {
                    if (R.Branchs[i].Info == "+")
                        result += Calculate(R.Branchs[i].Right);
                    else if (R.Branchs[i].Info == "-")
                        result -= Calculate(R.Branchs[i].Right);
                    else if (R.Branchs[i].Info == "*")
                        result *= Calculate(R.Branchs[i].Right);
                    else if (R.Branchs[i].Info == "/")
                        result /= Calculate(R.Branchs[i].Right);
                    else if (R.Branchs[i].Info == "**")
                        result *= Math.Pow(Calculate(R.Branchs[i - 1].Right), CalculateInvolution(R.Branchs[i].Right) - 1);
                    else
                        result = 0;
                }
            }
            return result;
        }

        private static TreeNode ExpandTree(TreeNode R, int k)
        {
            // Увеличивает количество ветвей дерева
            int len = R.Branchs.Length;
            TreeNode NewR = new TreeNode();
            NewR.Branchs = new TreeNode[len + k];
            for (int i = 0; i < len; i++)
                NewR.Branchs[i] = R.Branchs[i];
            return NewR;
        }
    }
}
