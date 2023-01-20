namespace MathEvalNS
{
    public class MathEval
    {
        public decimal Calculate(string sum)
        {
            //Your code starts here

            return Eval(sum);
        }

        // Evals expressions without nested functions
        public List<StackEntry> EvalNonNested(string expression, List<StackEntry> stackFrame)
        {
            int count = 0;
            Operator ops = Operator.PUSH;
            foreach (string part in expression.Split(" "))
            {
                decimal value;
                if (decimal.TryParse(part, out value))
                {
                    stackFrame.Add(new StackEntry(value, ops));
                }

                ops = Utils.IsOperator(part) ? Utils.ParseOperator(part) : Operator.PUSH;

                count++;
            }
            return stackFrame;
        }

        // Eval expressions with nested functions
        public decimal Eval(String expression)
        {
            Console.WriteLine("expression: " + expression);

            // eval nested functions
            if (expression.Contains("("))
            {
                string nestedFunctionExpression = expression.Substring(expression.IndexOf("(") + 2, expression.LastIndexOf(")") - expression.IndexOf("(") - 3);

                decimal funcResult = Eval(nestedFunctionExpression);

                // replace nested function with evaluated value
                string unestedExpression = expression.Replace(expression.Substring(expression.IndexOf("("), expression.LastIndexOf(")") - expression.IndexOf("(") + 1), funcResult.ToString());

                return Eval(unestedExpression);
            }
            else
            {
                return Execute(EvalNonNested(expression, new List<StackEntry>()));
            }
        }

        public decimal Execute(List<StackEntry> stackFrame)
        {
            while (stackFrame.Count() > 1)
            {
                Execute(stackFrame, Operator.DIVIDE);
                Execute(stackFrame, Operator.MULTIPLY);
                Execute(stackFrame, Operator.SUBTRACT);
                Execute(stackFrame, Operator.ADD);
            }

            return stackFrame[0].Operand;
        }

        public void Execute(List<StackEntry> stackFrame, Operator oper)
        {
            for (int i = 0; i < stackFrame.Count(); i++)
            {
                if (stackFrame[i].Oper == oper)
                {
                    StackEntry prevEntry = stackFrame[i - 1];
                    StackEntry currentEntry = stackFrame[i];

                    prevEntry.Operand = Apply(prevEntry.Operand, currentEntry.Operand, oper);

                    stackFrame.Remove(currentEntry);
                }
            }
        }

        public decimal Apply(decimal x, decimal y, Operator? oper)
        {
            // Console.WriteLine(" x: " + x + " y: " + y + " oper: " + oper);
            switch (oper)
            {
                case Operator.ADD:
                    x += y;
                    break;

                case Operator.SUBTRACT:
                    x -= y;
                    break;

                case Operator.DIVIDE:
                    x /= y;
                    break;

                case Operator.MULTIPLY:
                    x *= y;
                    break;

                default:
                    x = y;
                    break;
            }
            return x;
        }
    }

    public class Utils
    {
        public static bool IsNumber(string value)
        {
            return double.TryParse(value, out _);
        }

        public static bool IsOperator(string value)
        {
            string[] validOperators = new string[] { "+", "-", "/", "*" };
            return validOperators.Any(value.Contains);
        }

        public static Operator ParseOperator(string value)
        {
            Operator result;
            switch (value)
            {
                case "+":
                    result = Operator.ADD;
                    break;

                case "-":
                    result = Operator.SUBTRACT;
                    break;

                case "/":
                    result = Operator.DIVIDE;
                    break;

                case "*":
                    result = Operator.MULTIPLY;
                    break;

                default:
                    result = Operator.PUSH;
                    break;
            }
            return result;
        }
    }

    public class StackEntry
    {
        public StackEntry(decimal operand, Operator oper)
        {
            this.operand = operand;
            this.oper = oper;
        }

        private decimal operand;
        private Operator oper;

        public decimal Operand
        {
            get => operand;
            set => operand = value;
        }

        public Operator Oper
        {
            get { return oper; }
        }
    }

    public enum Operator
    {
        ADD,
        SUBTRACT,
        MULTIPLY,
        DIVIDE,
        PUSH
    }
}