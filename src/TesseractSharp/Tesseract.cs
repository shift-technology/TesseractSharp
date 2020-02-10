using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TesseractSharp.Core;

namespace TesseractSharp
{
    public static class Tesseract
    {
        public static Stream FileToPdf(
            string inputFilePath,
            long? dotPerInch = null,
            PageSegMode? psm = null,
            OcrEngineMode? oem = null,
            IEnumerable<Language> languages = null,
            IEnumerable<KeyValuePair<string, string>> configVars = null
        )
        {
            var outputBasenameFilePath = Path.Combine(Path.GetTempPath(), "tess_" + Guid.NewGuid().ToString("N"));
            var configFiles = new List<ConfigFile>{ConfigFile.OutputPdf};

            TesseractEngine engine;
            try
            {
                engine = new TesseractEngine(
                    inputFilePath, outputBasenameFilePath,
                    dotPerInch, psm, oem, languages,
                    configFiles, configVars);

            }
            catch (Exception ex)
            {
                throw new TesseractException("Fail to call tesseract", ex);
            }

            if (engine.Result.ExitCode != 0)
                throw new TesseractException(engine.Result.Error);

            var outputFilePath = outputBasenameFilePath + ".pdf";

            if (!File.Exists(outputFilePath))
                throw new TesseractException("PDF was not generated.");

            return new BurnAfterReadingFileStream(outputFilePath);
        }

        public static Stream FileToTxt(
            string inputFilePath,
            long? dotPerInch = null,
            PageSegMode? psm = null,
            OcrEngineMode? oem = null,
            IEnumerable<Language> languages = null,
            IEnumerable<KeyValuePair<string, string>> configVars = null
        )
        {
            var outputBasenameFilePath = "stdout";
            var configFiles = new List<ConfigFile> { ConfigFile.OutputTxt };

            TesseractEngine engine;
            try
            {
                engine = new TesseractEngine(
                    inputFilePath, outputBasenameFilePath,
                    dotPerInch, psm, oem, languages,
                    configFiles, configVars);

            }
            catch (Exception ex)
            {
                throw new TesseractException("Fail to call tesseract", ex);
            }

            if (engine.Result.ExitCode != 0)
                throw new InvalidOperationException(engine.Result.Error);

            return new MemoryStream(Encoding.UTF8.GetBytes(engine.Result.Output));
        }

        public static Stream FileToTsv(
            string inputFilePath,
            long? dotPerInch = null,
            PageSegMode? psm = null,
            OcrEngineMode? oem = null,
            IEnumerable<Language> languages = null,
            IEnumerable<KeyValuePair<string, string>> configVars = null
        )
        {
            var outputBasenameFilePath = "stdout";
            var configFiles = new List<ConfigFile> { ConfigFile.OutputTsv };

            TesseractEngine engine;
            try
            {
                engine = new TesseractEngine(
                    inputFilePath, outputBasenameFilePath,
                    dotPerInch, psm, oem, languages,
                    configFiles, configVars);

            }
            catch (Exception ex)
            {
                throw new TesseractException("Fail to call tesseract", ex);
            }

            return new MemoryStream(Encoding.UTF8.GetBytes(engine.Result.Output));
        }

        public static Stream FileToHocr(
            string inputFilePath,
            long? dotPerInch = null,
            PageSegMode? psm = null,
            OcrEngineMode? oem = null,
            IEnumerable<Language> languages = null,
            IEnumerable<KeyValuePair<string, string>> configVars = null
        )
        {
            var outputBasenameFilePath = "stdout";
            var configFiles = new List<ConfigFile> { ConfigFile.OutputHocr };

            TesseractEngine engine;
            try
            {
                engine = new TesseractEngine(
                    inputFilePath, outputBasenameFilePath,
                    dotPerInch, psm, oem, languages,
                    configFiles, configVars);

            }
            catch (Exception ex)
            {
                throw new TesseractException("Fail to call tesseract", ex);
            }

            return new MemoryStream(Encoding.UTF8.GetBytes(engine.Result.Output));
        }

        public static Stream FileToAlto(
            string inputFilePath,
            long? dotPerInch = null,
            PageSegMode? psm = null,
            OcrEngineMode? oem = null,
            IEnumerable<Language> languages = null,
            IEnumerable<KeyValuePair<string, string>> configVars = null
        )
        {
            var outputBasenameFilePath = "stdout";
            var configFiles = new List<ConfigFile> { ConfigFile.OutputAlto };

            TesseractEngine engine;
            try
            {
                engine = new TesseractEngine(
                    inputFilePath, outputBasenameFilePath,
                    dotPerInch, psm, oem, languages,
                    configFiles, configVars);

            }
            catch (Exception ex)
            {
                throw new TesseractException("Fail to call tesseract", ex);
            }

            return new MemoryStream(Encoding.UTF8.GetBytes(engine.Result.Output));
        }
    }
}
