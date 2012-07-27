using System;
using System.IO;
using System.Threading.Tasks;

namespace WFE.Lib
{
    internal class FileUtils
    {
        internal static string GetFileNameWithExtension(string input)
        {
            string[] parts = input.Split(new [] { '\\' });
            return parts.Length > 0 ? parts[parts.Length - 1] : "unknown";
        }

        internal static string GetFileNameOnly(string input)
        {
            string[] parts = input.Split(new[] { '\\', '.' });
            return parts.Length > 1 ? parts[parts.Length - 2] : "unknown";
        }

        internal static string GetDirectory(string input)
        {
            return Path.GetDirectoryName(input);
        }

        internal static string Copy(string inputFile)
        {
            string outputDirectory = Path.Combine(Environment.CurrentDirectory, "wfe");

            if (Directory.Exists(outputDirectory))
            {
                Directory.Delete(outputDirectory, true);
            }

            Directory.CreateDirectory(outputDirectory);

            Environment.CurrentDirectory = outputDirectory;

            string inputDir = GetDirectory(inputFile);

            string[] files = Directory.GetFiles(inputDir);

            // Copy the files and overwrite destination files if they already exist.
            Parallel.For(0, files.Length, x =>
            {
                string source = Path.GetFileName(files[x]);
                var destn = Path.Combine(outputDirectory, source);
                File.Copy(files[x], destn, true);
            });

            return Path.Combine(outputDirectory, GetFileNameWithExtension(inputFile));
        }
    }
}
