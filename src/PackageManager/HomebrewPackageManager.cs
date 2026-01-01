using System;
using System.Threading;
using System.Threading.Tasks;

namespace DevEnv;

public sealed class HomebrewPackageManager : IPackageManager
{
    public string Name { get; } = "Homebrew";

    public Task InstallPackageAsync(string packageName, CancellationToken cancellationToken)
    {
        Process process = new("brew", "install", packageName);

        return process.ExecuteAsync(cancellationToken);
    }

    public Task UpdateAsync(CancellationToken cancellationToken)
    {
        Process process = new("brew", "update");

        return process.ExecuteAsync(cancellationToken);
    }
    public async Task<bool> CheckIfPackageIsInstalledAsync(string packageName,CancellationToken cancellationToken)
    {
        Process processService = new("brew", "list", "--versions", packageName);

        int exitCode = await processService.ExecuteAsync(cancellationToken).ConfigureAwait(false);
    
        return exitCode == 0;
    }

    public Task UpdatePackageAsync(string packageName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}