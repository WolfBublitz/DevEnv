using System.Threading;
using System.Threading.Tasks;
using R3;

namespace DevEnv;

public interface IPackageManager
{
    public string Name { get; }

    public Observable<string> StdOut { get; }

    public Observable<string> StdErr { get; }

    public Task<bool> VerifyAsync(CancellationToken cancellationToken);

    public Task UpdatePackageListAsync(CancellationToken cancellationToken);

    public Task UpdatePackageAsync(string packageName, CancellationToken cancellationToken);

    public Task InstallPackageAsync(string packageName, CancellationToken cancellationToken);

    public Task<bool> CheckIfPackageIsInstalledAsync(string packageName, CancellationToken cancellationToken);
}
