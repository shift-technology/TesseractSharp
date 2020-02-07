using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using TesseractSharp;

namespace TesseractSharpExtensions
{
    public static class TesseractSharpExtensions
    {
        public static Stream ImageToPdf(
            Bitmap bitmap,
            long? dotPerInch = null,
            PageSegMode? psm = null,
            OcrEngineMode? oem = null,
            IEnumerable<Language> languages = null,
            IEnumerable<KeyValuePair<string, string>> configVars = null
        )
        {
            var inputBasenameFilePath = Path.Combine(Path.GetTempPath(), "tess_" + Guid.NewGuid().ToString("N") + ".png");
            try
            {
                bitmap.Save(inputBasenameFilePath, ImageFormat.Png);
                return Tesseract.FileToPdf(inputBasenameFilePath, dotPerInch, psm, oem, languages, configVars);
            }
            finally
            {
                if (File.Exists(inputBasenameFilePath))
                    File.Delete(inputBasenameFilePath);
            }
        }

        public static Stream ImageToTxt(
            Bitmap bitmap,
            long? dotPerInch = null,
            PageSegMode? psm = null,
            OcrEngineMode? oem = null,
            IEnumerable<Language> languages = null,
            IEnumerable<KeyValuePair<string, string>> configVars = null
        )
        {
            var inputBasenameFilePath = Path.Combine(Path.GetTempPath(), "tess_" + Guid.NewGuid().ToString("N") + ".png");
            try
            {
                bitmap.Save(inputBasenameFilePath, ImageFormat.Png);
                return Tesseract.FileToTxt(inputBasenameFilePath, dotPerInch, psm, oem, languages, configVars);
            }
            finally
            {
                if (File.Exists(inputBasenameFilePath))
                    File.Delete(inputBasenameFilePath);
            }
        }

        public static Stream ImageToTsv(
            Bitmap bitmap,
            long? dotPerInch = null,
            PageSegMode? psm = null,
            OcrEngineMode? oem = null,
            IEnumerable<Language> languages = null,
            IEnumerable<KeyValuePair<string, string>> configVars = null
        )
        {
            var inputBasenameFilePath = Path.Combine(Path.GetTempPath(), "tess_" + Guid.NewGuid().ToString("N") + ".png");
            try
            {
                bitmap.Save(inputBasenameFilePath, ImageFormat.Png);
                return Tesseract.FileToTsv(inputBasenameFilePath, dotPerInch, psm, oem, languages, configVars);
            }
            finally
            {
                if (File.Exists(inputBasenameFilePath))
                    File.Delete(inputBasenameFilePath);
            }
        }

        public static Stream ImageToHocr(
            Bitmap bitmap,
            long? dotPerInch = null,
            PageSegMode? psm = null,
            OcrEngineMode? oem = null,
            IEnumerable<Language> languages = null,
            IEnumerable<KeyValuePair<string, string>> configVars = null
        )
        {
            var inputBasenameFilePath = Path.Combine(Path.GetTempPath(), "tess_" + Guid.NewGuid().ToString("N") + ".png");
            try
            {
                bitmap.Save(inputBasenameFilePath, ImageFormat.Png);
                return Tesseract.FileToHocr(inputBasenameFilePath, dotPerInch, psm, oem, languages, configVars);
            }
            finally
            {
                if (File.Exists(inputBasenameFilePath))
                    File.Delete(inputBasenameFilePath);
            }
        }

        public static Stream ImageToAlto(
            Bitmap bitmap,
            long? dotPerInch = null,
            PageSegMode? psm = null,
            OcrEngineMode? oem = null,
            IEnumerable<Language> languages = null,
            IEnumerable<KeyValuePair<string, string>> configVars = null
        )
        {
            var inputBasenameFilePath = Path.Combine(Path.GetTempPath(), "tess_" + Guid.NewGuid().ToString("N") + ".png");
            try
            {
                bitmap.Save(inputBasenameFilePath, ImageFormat.Png);
                return Tesseract.FileToAlto(inputBasenameFilePath, dotPerInch, psm, oem, languages, configVars);
            }
            finally
            {
                if (File.Exists(inputBasenameFilePath))
                    File.Delete(inputBasenameFilePath);
            }
        }
    }
}