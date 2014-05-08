using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using AndroidToolkit.Infrastructure.Adapters;

namespace AndroidToolkit.Infrastructure.Utilities
{
    public class CommandExecutor : ICommandExecutor
    {

        public CommandExecutor()
        {
            _adapter = new TextBlockAdapter();
        }

        public Task Execute(Command cmd, TextBlock context, bool createNoWindow = true)
        {
            return Task.Run(async () =>
            {
                _processStartInfo = new ProcessStartInfo
                {
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    FileName = "cmd.exe",
                    CreateNoWindow = createNoWindow
                };
                _process = new Process { StartInfo = _processStartInfo };
                _process.OutputDataReceived += (sender, args) => _adapter.Adapt(context, args.Data);
                _process.ErrorDataReceived += (sender, args) => _adapter.Adapt(context, args.Data);
                _process.Start();
                _process.BeginErrorReadLine();
                _process.BeginOutputReadLine();
                await _process.StandardInput.WriteLineAsync(cmd.Text);
                await _process.StandardInput.FlushAsync();
                _process.StandardInput.Dispose();
                _process.Close();
                _process.Kill();
            });
        }

        public Task<string> Execute(Command cmd, bool createNoWindow = true)
        {
            return Task.Run(async () =>
            {
                _processStartInfo = new ProcessStartInfo
                {
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    FileName = "cmd.exe",
                    CreateNoWindow = createNoWindow
                };
                _process = new Process { StartInfo = _processStartInfo };
                _process.Start();
                await _process.StandardInput.WriteLineAsync(cmd.Text);
                await _process.StandardInput.FlushAsync();
                _process.StandardInput.Dispose();
                string output = await _process.StandardOutput.ReadToEndAsync();
                _process.Close();
                _process.Kill();
                return output;
            });
        }

        public Task Execute(IEnumerable<Command> cmds, TextBlock context, bool createNoWindow = true)
        {
            return Task.Run(async () =>
            {
                _processStartInfo = new ProcessStartInfo
                {
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    FileName = "cmd.exe",
                    CreateNoWindow = createNoWindow
                };
                _process = new Process { StartInfo = _processStartInfo };
                _process.OutputDataReceived += (sender, args) => _adapter.Adapt(context, args.Data);
                _process.ErrorDataReceived += (sender, args) => _adapter.Adapt(context, args.Data);
                _process.Start();
                _process.BeginErrorReadLine();
                _process.BeginOutputReadLine();
                foreach (var cmd in cmds.Where(cmd => cmd.Text.Length > 0))
                {
                    await _process.StandardInput.WriteLineAsync(cmd.Text);
                }
                await _process.StandardInput.FlushAsync();
                _process.StandardInput.Dispose();
                _process.Close();
                _process.Kill();
            });
        }

        public Task<string> Execute(IEnumerable<Command> cmds, bool createNoWindow = true)
        {
            return Task.Run(async () =>
            {
                _processStartInfo = new ProcessStartInfo
                {
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    FileName = "cmd.exe",
                    CreateNoWindow = createNoWindow
                };
                _process = new Process { StartInfo = _processStartInfo };

                _process.Start();

                foreach (var cmd in cmds.Where(cmd => cmd.Text.Length > 0))
                {
                    await _process.StandardInput.WriteLineAsync(cmd.Text);
                }
                await _process.StandardInput.FlushAsync();
                _process.StandardInput.Dispose();
                string output = await _process.StandardOutput.ReadToEndAsync();
                _process.Close();
                _process.Kill();
                return output;
            });
        }

        private readonly ITextBlockAdapter _adapter;

        private Process _process;

        private ProcessStartInfo _processStartInfo;


    }
}
