using System.Drawing;
using System.IO;
using System.Reflection;
using NLog;
using NUnit.Framework;

using TesseractSharp;
using TesseractSharpExtensions;

namespace TesseractSharpExtensionsTests
{
    [TestFixture]
    public class test_call : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            var config = new NLog.Config.LoggingConfiguration();

            // Targets where to log to: File and Console
            var logconsole = new NLog.Targets.ConsoleTarget("logconsole");

            // Rules for mapping loggers to targets            
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole);

            // Apply config           
            LogManager.Configuration = config;

        }

        [Test]
        public void TestTxt()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyDirectory = Path.GetDirectoryName(assembly.Location);
            var input  = Path.Combine(assemblyDirectory, @"samples\fakeidcard.bmp");
            var output = Path.Combine(assemblyDirectory, @"samples\fakeidcard.txt");

            var bitmap = (Bitmap)Image.FromFile(input);
            using (var stream = TesseractExtension.ImageToTxt(bitmap, languages: new[] { Language.English, Language.French }))
            using (var read = new StreamReader(stream))
            {
                var computed = read.ReadToEnd().Replace("\r\n", "\n").Trim('\f', '\n');
                var expected = File.OpenText(output).ReadToEnd().Replace("\r\n", "\n").Trim('\f', '\n');
                Assert.AreEqual(expected, computed);
            }
        }

        [Test]
        public void TestTsv()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyDirectory = Path.GetDirectoryName(assembly.Location);
            var input = Path.Combine(assemblyDirectory, @"samples\fakeidcard.bmp");
            var output = Path.Combine(assemblyDirectory, @"samples\fakeidcard.tsv");

            var bitmap = (Bitmap)Image.FromFile(input);
            using (var stream = TesseractExtension.ImageToTsv(bitmap, languages: new[] { Language.English, Language.French }))
            using (var read = new StreamReader(stream))
            {
                var computed = read.ReadToEnd().Replace("\r\n", "\n");
                var expected = File.OpenText(output).ReadToEnd().Replace("\r\n", "\n");
                Assert.AreEqual(expected, computed);
            }
        }

        [Test]
        public void TestHocr()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyDirectory = Path.GetDirectoryName(assembly.Location);
            var input = Path.Combine(assemblyDirectory, @"samples\fakeidcard.bmp");
            var output = Path.Combine(assemblyDirectory, @"samples\fakeidcard.hocr");

            var bitmap = (Bitmap)Image.FromFile(input);
            using (var stream = TesseractExtension.ImageToHocr(bitmap, languages: new[] { Language.English, Language.French }))
            using (var read = new StreamReader(stream))
            {
                var computed = read.ReadToEnd().Replace("\r\n", "\n");
                var expected = File.OpenText(output).ReadToEnd().Replace("\r\n", "\n");
                computed = RemoveFileNameFromHocr(computed);
                expected = RemoveFileNameFromHocr(expected);
                Assert.AreEqual(expected, computed);
            }
        }

        [Test]
        public void TestAlto()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyDirectory = Path.GetDirectoryName(assembly.Location);
            var input = Path.Combine(assemblyDirectory, @"samples\fakeidcard.bmp");
            var output = Path.Combine(assemblyDirectory, @"samples\fakeidcard.alto");
            var bitmap = (Bitmap)Image.FromFile(input);
            using (var stream = TesseractExtension.ImageToAlto(bitmap, languages: new[] { Language.English, Language.French }))
            using (var read = new StreamReader(stream))
            {
                var computed = read.ReadToEnd().Replace("\r\n", "\n");
                var expected = File.OpenText(output).ReadToEnd().Replace("\r\n", "\n");
                Assert.AreEqual(expected, computed);
            }
        }
    }
}
