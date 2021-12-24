using Xunit;
using MINAMESPACE;

namespace MINAMESPACE
{
    public class MISISTEMATests
    {
        [Fact]
        public void FakeFact_Equal()
        {
            // Arrange
            int a = 19, b = 23, expected = 42;
            // Act
            int result = a + b;
            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(3, 4, 7)]
        public void Suma_Equal(int left, int right, int result)
        {
            Assert.Equal(result, left + right);
        }
    }
}
