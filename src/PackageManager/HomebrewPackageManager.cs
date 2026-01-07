using System;
using System.Threading;
using System.Threading.Tasks;

namespace DevEnv;

public sealed class HomebrewPackageManager : IPackageManager
{
    public string Name { get; internal set; } = "Homebrew";

    internal string ExecutablePath { get; set; } = "brew";

    public async Task<bool> VerifyAsync(CancellationToken cancellationToken)
    {
        try
        {
            Process process = new(ExecutablePath, "--version");

            // Attempt to start the process to check if the executable exists
            await process.ExecuteAsync(cancellationToken).ConfigureAwait(false);

            return true;
        }
        catch (Exception)
        {
            // If starting the process fails, Homebrew is not installed
            return false;
        }
    }

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