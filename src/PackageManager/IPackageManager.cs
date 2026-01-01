using System.Threading;
using System.Threading.Tasks;

namespace DevEnv;

public interface IPackageManager
{
    public string Name { get; }

    public Task UpdateAsync(CancellationToken cancellationToken);

    public Task UpdatePackageAsync(string packageName, CancellationToken cancellationToken);

    public Task InstallPackageAsync(string packageName, CancellationToken cancellationToken);

    public Task<bool> CheckIfPackageIsInstalledAsync(string packageName, CancellationToken cancellationToken);
}
