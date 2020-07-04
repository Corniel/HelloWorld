namespace HelloWorld
{
    public static class FizzBuzz
    {
        public static string WithConditionalOperator(int n) =>
            n % 3 == 0
            ? n % 5 != 0 ? "Fizz" : "FizzBuzz"
            : n % 5 == 0 ? "Buzz" : n.ToString();
    }
}
