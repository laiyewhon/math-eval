class ProgramTest
{
    static void Main(string[] args)
    {
        // display the number of command line arguments.
        MathEvalNS.MathEval mathEval = new MathEvalNS.MathEval();

        // test samples from pdf doc
        foreach (var testCase in testCases)
        {
            Demarcate(testCase.Statement);
            Assert(mathEval.Calculate(testCase.Statement), testCase.ExpectedValue);
            Console.WriteLine();
        }
    }

    static void Assert(decimal result, decimal expectedResult)
    {
        Console.WriteLine();

        if (result == expectedResult)
        {
            Console.WriteLine("👍 SUCCESS result is " + expectedResult);
        }
        else
        {
            Console.WriteLine("🙅‍♂️ FAILURE: Expected result " + expectedResult + " does not match result " + result);
        }
    }

    static void Demarcate(string section)
    {
        Console.WriteLine("===== Testcase " + section + " =====");
    }

    class TestCase
    {
        public TestCase(string statement, decimal expectedValue)
        {
            this.statement = statement;
            this.expectedValue = expectedValue;
        }

        private string statement;
        private decimal expectedValue;

        public string Statement
        {
            get { return statement; }
        }

        public decimal ExpectedValue
        {
            get { return expectedValue; }
        }
    }

    static TestCase[] testCases = new TestCase[] {
        new TestCase("1 + 1", 2),
        new TestCase("2 * 2", 4),
        new TestCase("1 + 2 + 3", 6),
        new TestCase("6 / 2", 3),
        new TestCase("11 + 23", 34),
        new TestCase("11.1 + 23", 34.1m),
        new TestCase("1 + 1 * 3", 4),

        new TestCase("( 11.5 + 15.4 ) + 10.1", 37),
        new TestCase("23 - ( 29.3 - 12.5 )", 6.2m),
        new TestCase("( 1 / 2 ) - 1 + 1", 0.5m),

        new TestCase("10 - ( 2 + 3 * ( 7 - 5 ) )", 2),
    };
}
