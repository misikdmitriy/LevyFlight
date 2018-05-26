using System;
using System.Linq;
using LevyFlight.Entities;
using Shouldly;
using Xunit;

namespace LevyFlight.Tests
{
    public class PollinatorsTests
    {
        [Fact]
        public void EqualsShouldReturnTrueIfPollinatorsAreEqual()
        {
            // Arrange
            var p1 = new Pollinator(new[] {0.0, 1.0, 2.0, 3.0});
            var p2 = new Pollinator(new[] {0.0, 1.0, 2.0, 3.0});

            // Act
            // Assert
            p1.Equals(p2).ShouldBeTrue();
            p2.Equals(p1).ShouldBeTrue();
        }

        [Fact]
        public void EqualsShouldReturnFalseIfPollinatorsAreNotEqualAtLeastInOneElement()
        {
            // Arrange
            var p1 = new Pollinator(new[] { 0.0, 1.0, 2.0, 3.0 });
            var p2 = new Pollinator(new[] { 0.0, 1.0, 2.0, 3.01 });

            // Act
            // Assert
            p1.Equals(p2).ShouldBeFalse();
            p2.Equals(p1).ShouldBeFalse();
        }

        [Fact]
        public void EqualsShouldReturnFalseIfPollinatorsAreHaveDifferentSizes()
        {
            // Arrange
            var p1 = new Pollinator(new[] { 0.0, 1.0, 2.0, 3.0 });
            var p2 = new Pollinator(new[] { 0.0, 1.0, 2.0, 3.0, 4.0 });

            // Act
            // Assert
            p1.Equals(p2).ShouldBeFalse();
            p2.Equals(p1).ShouldBeFalse();
        }

        [Fact]
        public void EqualsShouldReturnFalseIfAnotherTypeIsObject()
        {
            // Arrange
            var p1 = new Pollinator(new[] { 0.0, 1.0, 2.0, 3.0 });
            var p2 = new object();

            // Act
            // Assert
            p1.Equals(p2).ShouldBeFalse();
            p2.Equals(p1).ShouldBeFalse();
        }

        [Fact]
        public void EqualShouldReturnTrueIfEllementIsSame()
        {
            // Arrange
            var p1 = new Pollinator(new[] { 0.0, 1.0, 2.0, 3.0 });

            // Act
            // Assert
            p1.Equals(p1).ShouldBeTrue();
        }

        [Fact]
        public void AddShouldDoIt()
        {
            // Arrange
            var d1 = new[] { 0.0, 1.0, 2.0, 3.0 };
            var d2 = new[] { -1.0, 2.0, 4.5, 7.8 };

            var p1 = new Pollinator(d1);
            var p2 = new Pollinator(d2);

            // Act
            var p = p1 + p2;

            var d = new double[d1.Length];
            for (var i = 0; i < d1.Length; i++)
            {
                d[i] = d1[i] + d2[i];
            }

            // Assert
            p.SequenceEqual(d).ShouldBeTrue();
        }

        [Fact]
        public void AddShouldFailIfSizesAreNotEqual()
        {
            // Arrange
            var d1 = new[] { 0.0, 1.0, 2.0, 3.0 };
            var d2 = new[] { -1.0, 2.0, 4.5, 7.8, 9.1 };

            var p1 = new Pollinator(d1);
            var p2 = new Pollinator(d2);

            // Act
            Action action = () =>
            {
                var p = p1 + p2;
            };

            action.ShouldThrow<Exception>();
        }

        [Fact]
        public void MinusShouldDoIt()
        {
            // Arrange
            var d1 = new[] { 0.0, 1.0, 2.0, 3.0 };
            var d2 = new[] { -1.0, 2.0, 4.5, 7.8 };

            var p1 = new Pollinator(d1);
            var p2 = new Pollinator(d2);

            // Act
            var p = p1 - p2;

            var d = new double[d1.Length];
            for (var i = 0; i < d1.Length; i++)
            {
                d[i] = d1[i] - d2[i];
            }

            // Assert
            p.SequenceEqual(d).ShouldBeTrue();
        }

        [Fact]
        public void MinusShouldFailIfSizesAreNotEqual()
        {
            // Arrange
            var d1 = new[] { 0.0, 1.0, 2.0, 3.0 };
            var d2 = new[] { -1.0, 2.0, 4.5, 7.8, 9.1 };

            var p1 = new Pollinator(d1);
            var p2 = new Pollinator(d2);

            // Act
            Action action = () =>
            {
                var p = p1 - p2;
            };

            action.ShouldThrow<Exception>();
        }

        [Fact]
        public void MultiplyShouldDoIt()
        {
            // Arrange
            var d1 = new[] { 0.0, 1.0, 2.0, 3.0 };
            var d2 = new[] { -1.0, 2.0, 4.5, 7.8 };

            var p1 = new Pollinator(d1);
            var p2 = new Pollinator(d2);

            // Act
            var p = p1 * p2;

            var d = new double[d1.Length];
            for (var i = 0; i < d1.Length; i++)
            {
                d[i] = d1[i] * d2[i];
            }

            // Assert
            p.SequenceEqual(d).ShouldBeTrue();
        }

        [Fact]
        public void MultiplyShouldFailIfSizesAreNotEqual()
        {
            // Arrange
            var d1 = new[] { 0.0, 1.0, 2.0, 3.0 };
            var d2 = new[] { -1.0, 2.0, 4.5, 7.8, 9.1 };

            var p1 = new Pollinator(d1);
            var p2 = new Pollinator(d2);

            // Act
            Action action = () =>
            {
                var p = p1 * p2;
            };

            action.ShouldThrow<Exception>();
        }

        [Fact]
        public void DivideShouldDoIt()
        {
            // Arrange
            var d1 = new[] { 0.0, 1.0, 2.0, 3.0 };
            var d2 = new[] { -1.0, 2.0, 4.5, 7.8 };

            var p1 = new Pollinator(d1);
            var p2 = new Pollinator(d2);

            // Act
            var p = p1 / p2;

            var d = new double[d1.Length];
            for (var i = 0; i < d1.Length; i++)
            {
                d[i] = d1[i] / d2[i];
            }

            // Assert
            p.SequenceEqual(d).ShouldBeTrue();
        }

        [Fact]
        public void DivideShouldFailIfSizesAreNotEqual()
        {
            // Arrange
            var d1 = new[] { 0.0, 1.0, 2.0, 3.0 };
            var d2 = new[] { -1.0, 2.0, 4.5, 7.8, 9.1 };

            var p1 = new Pollinator(d1);
            var p2 = new Pollinator(d2);

            // Act
            Action action = () =>
            {
                var p = p1 / p2;
            };

            action.ShouldThrow<Exception>();
        }
    }
}
