using System;
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
        List<string> stdError = [];
        HomebrewPackageManager packageManager = new();
        using IDisposable subscription = packageManager.StdErr.Subscribe(stdError.Add);

        // Act
        await packageManager.UpdatePackageListAsync(cancellationToken).ConfigureAwait(false);

        // Assert
        stdError.First().Should().Contain("Updating Homebrew", because: "The package list update should complete successfully.");
    }
}