using System;
using System.Threading;
using System.Threading.Tasks;

using R3;

namespace DevEnv;

internal sealed class HomebrewPackageManager : PackageManager
{
    public sealed override string Name { get; internal set; } = "Homebrew";

    internal string ExecutablePath { get; set; } = "brew";

    public sealed override Task<bool> VerifyAsync(CancellationToken cancellationToken)
        => Process.CheckIfExecutableExistsAsync(ExecutablePath, ["--version"], cancellationToken);

    public sealed async override Task InstallPackageAsync(string packageName, CancellationToken cancellationToken)
    {
        Process process = new(ExecutablePath, "install", packageName);

        using IDisposable stdOutSubscription = process.StdOut.Subscribe(OnStdOut);
        using IDisposable stdErrSubscription = process.StdErr.Subscribe(OnStdErr);

        await process.ExecuteAsync(cancellationToken).ConfigureAwait(false);
    }

    public sealed async override Task UpdatePackageListAsync(CancellationToken cancellationToken)
    {
        Process process = new(ExecutablePath, "update");

        using IDisposable stdOutSubscription = process.StdOut.Subscribe(OnStdOut);
        using IDisposable stdErrSubscription = process.StdErr.Subscribe(OnStdErr);

        await process.ExecuteAsync(cancellationToken).ConfigureAwait(false);
    }
    public sealed override async Task<bool> CheckIfPackageIsInstalledAsync(string packageName, CancellationToken cancellationToken)
    {
        Process processService = new(ExecutablePath, "list", "--versions", packageName);

        using IDisposable stdOutSubscription = processService.StdOut.Subscribe(OnStdOut);
        using IDisposable stdErrSubscription = processService.StdErr.Subscribe(OnStdErr);

        int exitCode = await processService.ExecuteAsync(cancellationToken).ConfigureAwait(false);

        return exitCode == 0;
    }

    public sealed override Task UpdatePackageAsync(string packageName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}