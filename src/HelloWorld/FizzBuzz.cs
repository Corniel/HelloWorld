namespace HelloWorld
{
    public static class FizzBuzz
    {
        /// <summary>Extending the string result.</summary>
        public static string StringConcat(int n)
        {
            string s = null;
            if (n % 3 == 0) s += "Fizz";
            if (n % 5 == 0) s += "Buzz";
            return s ?? n.ToString();
        }

        /// <summary>Straightforward approach.</summary>
        public static string WithSwitch(int n)
            => (n % 15) switch
            {
                0 => "FizzBuzz",
                3 or 6 or 9 or 12 => "Fizz",
                5 or 10 => "Buzz",
                _ => n.ToString(),
            };

        /// <summary>Straightforward approach.</summary>
        public static string WithConditionalOperator(int n)
            => n % 3 == 0
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
