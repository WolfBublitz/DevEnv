using System.Threading;
using System.Threading.Tasks;
using AwesomeAssertions;
using DevEnv;

public sealed class TheVerifyAsyncMethod
{
    [Test]
    public async Task ShouldReturnTrueIfHomebrewIsInstalled(CancellationToken cancellationToken)
    {
        // Arrange
        HomebrewPackageManager packageManager = new();

        // Act
        bool result = await packageManager.VerifyAsync(cancellationToken);

        // Assert
        result.Should().BeTrue(because: "Homebrew is expected to be installed in the test environment.");
    }

    [Test]
    public async Task ShouldReturnFalseIfHomebrewIsNotInstalled(CancellationToken cancellationToken)
    {
        // Arrange
        HomebrewPackageManager packageManager = new()
        {
            ExecutablePath = "nonexistent-brew-executable"  // Simulate Homebrew not being installed
        };

        // Act
        bool result = await packageManager.VerifyAsync(cancellationToken);

        // Assert
        result.Should().BeFalse(because: "The specified Homebrew executable does not exist.");
    }
}