using VShevchenko;

namespace Lab3.Tests
{
    public class UnitTests
    {
        [Theory]
        [InlineData("Line 1\nLine 2\nLine 3", new[] { "Line 1", "Line 2", "Line 3" })]
        [InlineData("Single Line", new[] { "Single Line" })]
        [InlineData("", new string[] { })]
        public void ProcessInputFile_ReturnsCorrectLines(string fileContent, string[] expectedLines)
        {
            string tempFilePath = Path.GetTempFileName();
            File.WriteAllText(tempFilePath, fileContent);

            var filesHandler = new FilesHandler();
            List<string> result = filesHandler.ProcessInputFile(tempFilePath);

            Assert.Equal(expectedLines, result);

            File.Delete(tempFilePath);
        }

        [Fact]
        public void GetResults_ReturnsCorrectResults_WhenNoRecursion()
        {
            ProcedureChecker checker = new ProcedureChecker();
            List<string> data = new List<string>
            {
                "3",
                "p1",
                "2",
                "p1",
                "p2",
                "*****",
                "p2",
                "1",
                "p1",
                "*****",
                "p3",
                "1",
                "p1",
                "*****"
            };

            checker.ProcessProcedures(data);
            List<string> results = checker.GetResults();
            Assert.Equal(new List<string> { "YES", "YES", "NO" }, results);
        }

        [Theory]
        [InlineData("*****", 0, 1)]
        [InlineData("*****", 5, 6)]
        [InlineData("****", 0, 0)]
        [InlineData("*", 2, 2)]
        public void StarsChecker_ReturnsCorrectIndex(string data, int index, int expectedIndex)
        {
            ProcedureChecker checker = new ProcedureChecker();
            int result = checker.starsChecker(data, index);

            Assert.Equal(expectedIndex, result);
        }

        [Theory]
        [InlineData("5", 5)]
        [InlineData("123", 123)]
        [InlineData("-42", -42)]
        public void DataChecker_ValidatesInput(string data, int expectedResult)
        {
            ProcedureChecker checker = new ProcedureChecker();
            checker.dataChecker(data, out int result, "Invalid number format.");

            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(new string[] { "not a number", "A", "1", "B", "*****" }, "The first line must contain a valid integer for the number of procedures.")]
        [InlineData(new string[] { "1", "A", "not a number", "B", "*****" }, "The line 3 must contain a valid integer for the number of calls for procedure A.")]
        public void ProcessProcedures_InvalidInput_ThrowsFormatException(string[] input, string expectedMessage)
        {
            var checker = new ProcedureChecker();

            var exception = Assert.Throws<FormatException>(() => checker.ProcessProcedures(new List<string>(input)));
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}