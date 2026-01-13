using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AwesomeAssertions;
using DevEnv;
using R3;

namespace AptPackageManagerTests.MethodTests.UpdatePackageListAsyncMethodTests;

public sealed class TheUpdatePackageListAsyncMethod
{
    [Test]
    public async Task ShouldUpdatePackageListSuccessfully(CancellationToken cancellationToken)
    {
        // Arrange
        List<string> stdOut = [];
        AptPackageManager packageManager = new();
        packageManager.StdOut.Subscribe(line => stdOut.Add(line));

        // Act
        await packageManager.UpdatePackageListAsync(cancellationToken).ConfigureAwait(false);

        // Assert
        stdOut.Last().Should().Contain("Reading package lists... Done", because: "The package list update should complete successfully.");
    }
}