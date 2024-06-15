using NUnit.Framework;
using test1;

namespace TestMatrix.test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestPlayerA()
        {
            // Arrange
            double a11 = 10, a12 = 7;
            double a21 = 8, a22 = 11;
            Calculate calculate = new Calculate();

            // Act
            var resultA = calculate.Function(a11, a12, a21, a22, Player.A);

            // Assert
            Assert.That(resultA, Is.EqualTo((0.5, 9, 0.5, 0.5)).Within(0.001));
        }

        [Test]
        public void TestPlayerB()
        {
            // Arrange
            double a11 = 10, a12 = 7;
            double a21 = 8, a22 = 11;
            Calculate calculate = new Calculate();

            // Act
            var resultB = calculate.Function(a11, a12, a21, a22, Player.B);

            // Assert
            Assert.That(resultB, Is.EqualTo((0.667, 9, 0.333, 0.667)).Within(0.001));
        }

        [Test]
        public void TestEqualXValues()
        {
            // Arrange
            double a11 = 10, a12 = 10;
            double a21 = 8, a22 = 8;
            Calculate calculate = new Calculate();

            // Act
            var resultA = calculate.Function(a11, a12, a21, a22, Player.A);
            var resultB = calculate.Function(a11, a12, a21, a22, Player.B);

            // Assert
            Assert.That(resultA, Is.EqualTo(resultB));
        }

        [Test]
        public void TestEqualYValues()
        {
            // Arrange
            double a11 = 10, a12 = 7;
            double a21 = 8, a22 = 11;
            Calculate calculate = new Calculate();

            // Act
            var resultA = calculate.Function(a11, a12, a21, a22, Player.A);
            var resultB = calculate.Function(a11, a12, a21, a22, Player.B);

            // Assert
            Assert.That(resultA.Item2, Is.EqualTo(resultB.Item2));
        }

    }
}
