using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace TesseractSharp.Core
{
    public static class ProcessHelper
    {
        /// <summary>
        /// Default timeout in milliseconds.
        /// </summary>
        public static readonly int DefaultTimeOut = Convert.ToInt32(TimeSpan.FromMinutes(5).TotalMilliseconds);

        public static ProcessResult RunProcess(
            string command,
            IEnumerable<string> arguments = null,
            int? timeout = null,
            IEnumerable<KeyValuePair<string, string>> environmentVariables  = null)
        {
            return RunProcessAsync(command, arguments, timeout, environmentVariables).Result;
        }

        public static async Task<ProcessResult> RunProcessAsync(
            string command,
            IEnumerable<string> arguments = null,
            int? timeout = null,
            IEnumerable<KeyValuePair<string, string>> environmentVariables = null)
        {
            var result = new ProcessResult
            {
                ExitCode = -1,
                Output = "",
                Error = ""
            };

            var timeoutMs = timeout ?? DefaultTimeOut;

            using (var process = new Process())
            {
                process.StartInfo.FileName = command;
                process.StartInfo.Arguments = arguments != null ? string.Join(" ", arguments) : "";

                if (environmentVariables != null)
                {
                    foreach (var env in environmentVariables)
                        process.StartInfo.EnvironmentVariables[env.Key] = env.Value;
                }

                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;

                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.StandardOutputEncoding = Encoding.UTF8;

                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.StandardErrorEncoding = Encoding.UTF8;

                var outputBuilder = new StringBuilder();
                var outputCloseEvent = new TaskCompletionSource<bool>();

                process.OutputDataReceived += (s, e) =>
                {
                    // When the redirected stream is closed, a null line is sent to the event handler
                    if (e.Data == null)
                    {
                        outputCloseEvent.SetResult(true);
                    }
                    else
                    {
                        outputBuilder.Append(e.Data + "\n");
                    }
                };

                var errorBuilder = new StringBuilder();
                var errorCloseEvent = new TaskCompletionSource<bool>();

                process.ErrorDataReceived += (s, e) =>
                {
                    // When the redirected stream is closed, a null line is sent to the event handler
                    if (e.Data == null)
                    {
                        errorCloseEvent.SetResult(true);
                    }
                    else
                    {
                        errorBuilder.Append(e.Data + "\n");
                    }
                };


                var isStarted = process.Start();
                if (!isStarted)
                {
                    result.ExitCode = process.ExitCode;
                    return result;
                }

                // Reads the output stream first and then waits because deadlocks are possible
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                // Creates task to wait for process exit using timeout
                var waitForExit = WaitForExitAsync(process, timeoutMs);

                // Create task to wait for process exit and closing all output streams
                var processTask = Task.WhenAll(waitForExit, outputCloseEvent.Task, errorCloseEvent.Task);

                // Waits process completion and then checks it was not completed by timeout
                if (await Task.WhenAny(Task.Delay(timeoutMs), processTask) == processTask && waitForExit.Result)
                {
                    result.ExitCode = process.ExitCode;
                    result.Output = outputBuilder.ToString();
                    result.Error = errorBuilder.ToString();
                }
                else
                {
                    try
                    {
                        // Kill hung process
                        process.Kill();
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }

            return result;
        }


        private static Task<bool> WaitForExitAsync(Process process, int timeout)
        {
            return Task.Run(() => process.WaitForExit(timeout));
        }


        public struct ProcessResult
        {
            public int ExitCode;
            public string Output;
            public string Error;
        }
    }
}