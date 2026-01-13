using System.Threading;
using System.Threading.Tasks;
using AwesomeAssertions;
using DevEnv;

namespace AptPackageManagerTests.MethodTests.VerifyAsyncMethodTests;

public sealed class TheVerifyAsyncMethod
{
    [Test]
    public async Task ShouldReturnTrueIfAptIsInstalled(CancellationToken cancellationToken)
    {
        // Arrange
        AptPackageManager packageManager = new();

        // Act
        bool result = await packageManager.VerifyAsync(cancellationToken);

        // Assert
        result.Should().BeTrue(because: "Apt is expected to be installed in the test environment.");
    }

    [Test]
    public async Task ShouldReturnFalseIfAptIsNotInstalled(CancellationToken cancellationToken)
    {
        // Arrange
        AptPackageManager packageManager = new()
        {
            ExecutablePath = "nonexistent-apt-get-executable"  // Simulate Apt not being installed
        };

        // Act
        bool result = await packageManager.VerifyAsync(cancellationToken);

        // Assert
        result.Should().BeFalse(because: "The specified apt-get executable does not exist.");
    }
}