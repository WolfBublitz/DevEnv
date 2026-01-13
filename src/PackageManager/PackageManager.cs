using System;
using System.Threading;
using System.Threading.Tasks;
using R3;

namespace DevEnv;

internal abstract class PackageManager : IPackageManager, IDisposable
{
    private readonly Subject<string> stdOut = new();

    private readonly Subject<string> stdErr = new();

    public abstract string Name { get; internal set; }

    public Observable<string> StdOut => stdOut;

    public Observable<string> StdErr => stdErr;

    public abstract Task<bool> VerifyAsync(CancellationToken cancellationToken);

    public abstract Task UpdatePackageListAsync(CancellationToken cancellationToken);

    public abstract Task UpdatePackageAsync(string packageName, CancellationToken cancellationToken);

    public abstract Task InstallPackageAsync(string packageName, CancellationToken cancellationToken);

    public abstract Task<bool> CheckIfPackageIsInstalledAsync(string packageName, CancellationToken cancellationToken);

    public void Dispose()
    {
        Dispose(true);

        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            stdOut.Dispose();
            stdErr.Dispose();
        }
    }

    protected void OnStdOut(string message)
        => stdOut.OnNext(message);

    protected void OnStdErr(string message)
        => stdErr.OnNext(message);
}