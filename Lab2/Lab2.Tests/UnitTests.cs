using Lab2;

namespace Lab2.Tests
{
    public class UnitTests
    {
        [Theory]
        [InlineData(0, false)]
        [InlineData(2, false)]
        [InlineData(888,true)]
        [InlineData(3, true)]
        public void IsValuesValid_ReturnsCorrectValidationResult(int N, bool expectedResult)
        {
            var result = FilesHandler.IsValuesValid(N);

            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(1, false)]
        [InlineData(2, true)]
        [InlineData(3, true)]
        [InlineData(4, false)]
        [InlineData(5, true)]
        [InlineData(10, false)]
        [InlineData(29, true)]
        [InlineData(100, false)]
        public void IsPrimeNumber_ShouldReturnCorrectResults(int number, bool expectedResult)
        {
            bool result = Program.IsPrimeNumber(number);
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void CountEnount_ShouldReturnCorrectResults()
        {
            int[,,] d = new int[4, 10, 10];
            d[3, 1, 1] = 5;
            d[3, 2, 2] = 3;
            int N = 3;

            int expectedResult = 8;

            int result = Program.CountEnount(d, N);
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void FindThreeDigitPrimeNumbers_ShouldUpdateArrayCorrectly()
        {
            bool[] a = new bool[1000];
            int[,,] d = new int[4, 10, 10];
            a[123] = true;  // prime 123
            a[456] = false; // non-prime number 456

            Program.FindThreeDigitPrimeNumbers(a, d);

            Assert.Equal(1, d[3, 3, 2]); // 123 -> 3rd position
            Assert.Equal(0, d[3, 6, 5]); // 456 -> not set
        }

        [Theory]
        [InlineData(100, 10000, 100)]
        [InlineData(10500, 10000, 500)]
        public void MathMod_ShouldReturnCorrectModulus(int expression, int mod, int expected)
        {
            int result = Program.MathMod(expression, mod);

            Assert.Equal(expected, result);
        }
    }
}