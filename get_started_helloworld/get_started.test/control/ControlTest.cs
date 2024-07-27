using Xunit;

namespace get_started.control
{
    public class ControlTest
    {
        [Fact]
        public void Start_ShouldDoSomething()
        {
            // Arrange
            IControl control = new Control();

            // Act
            control.Start();

            // Assert
            Assert.True(true);
        }

        [Fact]
        public void Stop_ShouldDoSomething()
        {
            // Arrange
            IControl control = new Control();

            // Act
            control.Stop();

            // Assert
            Assert.True(true);
            }
    }
}