using System;
using System.Threading;
using System.Threading.Tasks;

namespace DevEnv;

public sealed class HomebrewPackageManager : IPackageManager
{
    public string Name { get; internal set; } = "Homebrew";

    internal string ExecutablePath { get; set; } = "brew";

    public Task<bool> VerifyAsync(CancellationToken cancellationToken)
        => Process.CheckIfExecutableExistsAsync(ExecutablePath, ["--version"], cancellationToken);

    public Task InstallPackageAsync(string packageName, CancellationToken cancellationToken)
    {
        Process process = new(ExecutablePath, "install", packageName);

        return process.ExecuteAsync(cancellationToken);
    }

    public Task UpdateAsync(CancellationToken cancellationToken)
    {
        Process process = new(ExecutablePath, "update");

        return process.ExecuteAsync(cancellationToken);
    }
    public async Task<bool> CheckIfPackageIsInstalledAsync(string packageName,CancellationToken cancellationToken)
    {
        Process processService = new(ExecutablePath, "list", "--versions", packageName);

        int exitCode = await processService.ExecuteAsync(cancellationToken).ConfigureAwait(false);
    
        return exitCode == 0;
    }

    public Task UpdatePackageAsync(string packageName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}