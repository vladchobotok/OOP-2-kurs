using Xunit;
using Lab1.Table;

namespace UnitTests
{
    public class UnitTest1
    {
        TableGrid tableGrid = new TableGrid();

        [Fact]
        public void TestMethod1()
        {
            string expected = "Division by zero error";
            string result = tableGrid.Calculate("2/0");
            Assert.True(expected.Equals(result));
        }
        [Fact]
        public void TestMethod2()
        {
            string expected = "Error";
            string result = tableGrid.Calculate("////");
            Assert.True(expected.Equals(result));
        }
        [Fact]
        public void TestMethod3()
        {
            string expected = "2";
            string result = tableGrid.Calculate("max(2, -1)");
            Assert.True(expected.Equals(result));
        }
        [Fact]
        public void TestMethod4()
        {
            string expected = "-1";
            string result = tableGrid.Calculate("min(2, -1)");
            Assert.True(expected.Equals(result));
        }
        [Fact]
        public void TestMethod5()
        {
            string expected = "-1";
            string result = tableGrid.Calculate("dec(0, 1)");
            Assert.True(expected.Equals(result));
        }
        [Fact]
        public void TestMethod6()
        {
            string expected = "1";
            string result = tableGrid.Calculate("inc(0, 1)");
            Assert.True(expected.Equals(result));
        }
        [Fact]
        public void TestMethod7()
        {
            string expected = "4";
            string result = tableGrid.Calculate("(1 + 1) * (1 + 1)");
            Assert.True(expected.Equals(result));
        }
        [Fact]
        public void TestMethod8()
        {
            string expected = "2";
            string result = tableGrid.Calculate("max(max(1,2),0)");
            Assert.True(expected.Equals(result));
        }
        [Fact]
        public void TestMethod9()
        {
            string expected = "5";
            string result = tableGrid.Calculate("inc(0, min(9, 5))");
            Assert.True(expected.Equals(result));
        }
    }
}
