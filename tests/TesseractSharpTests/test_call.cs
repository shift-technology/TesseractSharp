using System.IO;
using System.Reflection;
using NLog;
using NUnit.Framework;

// DO NOT USE IN PRODUCTION
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
// DO NOT USE IN PRODUCTION

using TesseractSharp;

namespace TesseractSharpTests
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
            var input  = Path.Combine(assemblyDirectory, @"samples\fakeidcard.jpg");
            var output = Path.Combine(assemblyDirectory, @"samples\fakeidcard.txt");

            using (var stream = Tesseract.ImageToTxt(input, languages: new [] {Language.English, Language.French}))
            using (var read = new StreamReader(stream))
            {
                var computed = read.ReadToEnd().Replace("\r\n", "\n").Trim('\f', '\n');
                var expected = File.OpenText(output).ReadToEnd().Replace("\r\n", "\n");
                Assert.AreEqual(expected, computed);
            }
        }

        [Test]
        public void TestTsv()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyDirectory = Path.GetDirectoryName(assembly.Location);
            var input = Path.Combine(assemblyDirectory, @"samples\fakeidcard.jpg");
            var output = Path.Combine(assemblyDirectory, @"samples\fakeidcard.tsv");

            using (var stream = Tesseract.ImageToTsv(input, languages: new[] { Language.English, Language.French }))
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
            var input = Path.Combine(assemblyDirectory, @"samples\fakeidcard.jpg");
            var output = Path.Combine(assemblyDirectory, @"samples\fakeidcard.hocr");

            using (var stream = Tesseract.ImageToHocr(input, languages: new[] { Language.English, Language.French }))
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
            var input = Path.Combine(assemblyDirectory, @"samples\fakeidcard.jpg");
            var output = Path.Combine(assemblyDirectory, @"samples\fakeidcard.alto");

            using (var stream = Tesseract.ImageToAlto(input, languages: new[] { Language.English, Language.French }))
            using (var read = new StreamReader(stream))
            {
                var computed = read.ReadToEnd().Replace("\r\n", "\n");
                var expected = File.OpenText(output).ReadToEnd().Replace("\r\n", "\n");
                Assert.AreEqual(expected, computed);
            }
        }

        [Test]
        public void TestPdf()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyDirectory = Path.GetDirectoryName(assembly.Location);
            var input = Path.Combine(assemblyDirectory, @"samples\fakeidcard.jpg");
            var output = Path.Combine(assemblyDirectory, @"samples\fakeidcard.pdf");

            using (var reader1 = new PdfReader(output))
            using (var stream = Tesseract.ImageToPdf(input, languages: new[] {Language.English, Language.French}))
            using (var reader2 = new PdfReader(stream))
            {
                var pdf1 = new PdfDocument(reader1);
                var pdf2 = new PdfDocument(reader2);

                var compareTool = new CompareTool();
                var result = compareTool.CompareByCatalog(pdf1, pdf2);
                Assert.IsTrue(result.IsOk());
            }
        }

    }
}
