namespace HelloWorld
{
    public static class FizzBuzz
    {
        /// <summary>Straightforward approach.</summary>
        public static string WithConditionalOperator(int n) =>
            n % 3 == 0
            ? n % 5 != 0 ? "Fizz" : "FizzBuzz"
            : n % 5 == 0 ? "Buzz" : n.ToString();

        /// <remarks>
        /// Uses the binary pattern of 19142723 (0x1241843) 
        /// that contains the 4 flavors to switch on.
        /// </remarks>
        public static string N19142723(int n) =>
            ((19142723 >> ((n % 15) * 2)) & 3) switch
            {
                1 => "Fizz",
                2 => "Buzz",
                3 => "FizzBuzz",
                _ => n.ToString(),
            };
    }
}
