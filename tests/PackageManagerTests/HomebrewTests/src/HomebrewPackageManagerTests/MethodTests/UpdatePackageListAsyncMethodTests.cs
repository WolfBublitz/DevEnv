using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AwesomeAssertions;
using DevEnv;
using R3;

namespace HomebrewTests.MethodTests.UpdatePackageListAsyncMethodTests;

public sealed class TheUpdatePackageListAsyncMethod
{
    [Test]
    public async Task ShouldUpdatePackageListSuccessfully(CancellationToken cancellationToken)
    {
        // Arrange
        List<string> stdOut = [];
        HomebrewPackageManager packageManager = new();
        packageManager.StdOut.Subscribe(stdOut.Add);

        // Act
        await packageManager.UpdatePackageListAsync(cancellationToken).ConfigureAwait(false);

        // Assert
        stdOut.Last().Should().Contain("Reading package lists... Done", because: "The package list update should complete successfully.");
    }
}