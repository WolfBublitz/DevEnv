using System.Threading;
using System.Threading.Tasks;
using AwesomeAssertions;
using DevEnv;

public sealed class TheVerifyAsyncMethod
{
    [Test]
    public async Task WhenBrewIsInstalled_ShouldReturnTrue(CancellationToken cancellationToken)
    {
        var packageManager = new HomebrewPackageManager();

        bool result = await packageManager.VerifyAsync(cancellationToken);

        result.Should().BeTrue(because: "Homebrew is expected to be installed in the test environment.");
    }
}