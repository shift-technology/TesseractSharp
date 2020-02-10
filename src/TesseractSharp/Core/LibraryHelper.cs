using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using NLog;

namespace TesseractSharp.Core
{
    public class LibraryHelper
    {
        private const string AssemblyVersionFile = "TesseractSharp.version";
        private const string ResourceFile = "TesseractSharpResources.zip";

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static LibraryHelper Instance { get; } = new LibraryHelper();


        /// <summary>
        /// Map processor 
        /// </summary>
        private readonly Dictionary<Architecture, string> _processorArchitecture =
            new Dictionary<Architecture, string>
            {
                {Architecture.X86, "x86"},
                {Architecture.X64, "x64"},
                {Architecture.Arm, "Arm"},
                {Architecture.Arm64, "Arm64"}
            };

        /// <summary>
        /// Mao os
        /// </summary>
        private readonly Dictionary<OSPlatform, string> _osPlatforms =
            new Dictionary<OSPlatform, string>
            {
                {OSPlatform.Linux, "linux"},
                {OSPlatform.OSX, "osx"},
                {OSPlatform.Windows, "win"},
            };

        private readonly string _assemblyDirectory;
        private readonly string _osProc;

        public LibraryHelper()
        {
            var procArchitecture = _processorArchitecture[RuntimeInformation.ProcessArchitecture];
            var osPlatform = _osPlatforms.Single(kv => RuntimeInformation.IsOSPlatform(kv.Key)).Value;
            _osProc = $"{osPlatform}-{procArchitecture}";
            Logger.Info($"Idenfity processor architecture '{procArchitecture}' and os platform '{osPlatform}'");

            var assembly = Assembly.GetAssembly(typeof(LibraryHelper));
            _assemblyDirectory = Path.GetDirectoryName(assembly.Location);
            var assemblyVersionFile = Path.Combine(_assemblyDirectory, AssemblyVersionFile);
            var assemblyVersion = assembly.GetName().Version.ToString();

            var assemblyVersionMatch = false;
            if (File.Exists(assemblyVersionFile))
            {
                try
                {
                    using (var stream = File.OpenText(assemblyVersionFile))
                    {
                        var storedVersion = stream.ReadToEnd();
                        if (storedVersion.Equals(assemblyVersion))
                            assemblyVersionMatch = true;
                    }
                }
                catch
                {
                    // Ignore exception
                }
            }

            if (assemblyVersionMatch)
                return;

            try
            {
                using (var stream = assembly.GetManifestResourceStream(ResourceFile))
                    Extract(stream, _assemblyDirectory);
            }
            catch (FileNotFoundException ex)
            {
                throw new TesseractException( "Resources not found...", ex);
            }

            // Delete the previous version before create the new one.
            if (File.Exists(assemblyVersionFile))
                File.Delete(assemblyVersionFile);

            using (var stream = File.OpenWrite(assemblyVersionFile))
            using (var writer = new BinaryWriter(stream))
            {
                writer.Write(Encoding.UTF8.GetBytes(assemblyVersion));
            }
        }

        private void Extract(Stream stream, string extractPath)
        {
            using (var archive = new ZipArchive(stream))
            {
                foreach (var entry in archive.Entries)
                {
                    var destinationPath = Path.GetFullPath(Path.Combine(extractPath, entry.FullName));
                    var destinationDir = Path.GetDirectoryName(destinationPath);
                    if (!Directory.Exists(destinationDir))
                        Directory.CreateDirectory(destinationDir);

                    entry.ExtractToFile(destinationPath, overwrite: true);
                }
            }
        }

        public bool TryGetBinary(string name, out string path)
        {
            var binDirectory = Path.Combine(_assemblyDirectory, _osProc);
            path = Path.Combine(binDirectory, name);
            if (!File.Exists(path))
            {
                Logger.Error($"Fail to find '{name}' in '{binDirectory}'");
                path = null;
                return false;
            }

            return true;
        }

        public bool TryGetDirectory(string name, out string path)
        {
            path = Path.Combine(_assemblyDirectory, name);
            if (!Directory.Exists(path))
            {
                Logger.Error($"Fail to find '{name}' in '{_assemblyDirectory}'");
                path = null;
                return false;
            }

            return true;
        }
    }
}
