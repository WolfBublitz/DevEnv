using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using R3;

namespace DevEnv;

public sealed class Process(string command, params string[] args)
{
    private readonly ReplaySubject<string> stdOut = new();

    private readonly ReplaySubject<string> stdErr = new();

    private readonly string command = command;

    private readonly string[] args = args;

    public Observable<string> StdOut => stdOut;

    public Observable<string> StdErr => stdErr;

    public async Task<int> ExecuteAsync(CancellationToken cancellationToken)
    {
        ProcessStartInfo processStartInfo = new()
        {
            FileName = command,
            Arguments = string.Join(' ', args),
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        };

        using System.Diagnostics.Process process = new()
        {
            StartInfo = processStartInfo,
        };

        process.OutputDataReceived += (sender, e) =>
        {
            if (e.Data != null)
            {
                stdOut.OnNext(e.Data);
            }
        };

        process.ErrorDataReceived += (sender, e) =>
        {
            if (e.Data != null)
            {
                stdErr.OnNext(e.Data);
            }
        };

        process.Start();

        process.BeginOutputReadLine();
        process.BeginErrorReadLine();

        await process.WaitForExitAsync(cancellationToken).ConfigureAwait(false);

        return process.ExitCode;
    }
}