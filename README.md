## TesseractSharp

[![GitHub license](https://img.shields.io/badge/license-Apache--2.0-blue.svg)](LICENSE)

A C# wrapper for Tesseract 5


## Use TesseractSharp !

Example usage:

```C#
using System;
using System.IO;
using TesseractSharp;
using TesseractSharp.Hocr;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = @"C:\Users\nobody\sample.jpg";

            var ouput1 = input.Replace(".jpg", ".pdf");
            using (var stream = Tesseract.ImageToPdf(input, languages: new[] { Language.English, Language.French }))
            using (var writer = File.OpenWrite(ouput1))
            {
                stream.CopyTo(writer);
            }

            var ouput2 = input.Replace(".jpg", ".txt");
            using (var stream = Tesseract.ImageToTxt(input, languages: new[] { Language.English, Language.French }))
            using (var writer = File.OpenWrite(ouput2))
            {
                stream.CopyTo(writer);
            }

            var ouput3 = input.Replace(".jpg", ".tsv");
            using (var stream = Tesseract.ImageToTsv(input, languages: new[] { Language.English, Language.French }))
            using (var writer = File.OpenWrite(ouput3))
            {
                stream.CopyTo(writer);
            }

            var ouput4 = input.Replace(".jpg", ".hocr");
            using (var stream = Tesseract.ImageToHocr(input, languages: new[] { Language.English, Language.French }))
            using (var writer = File.OpenWrite(ouput4))
            {
                stream.CopyTo(writer);
            }

            var hocr = HOCRParser.Parse(File.OpenText(ouput4));
            foreach (var page in hocr.Pages)
            {
                Console.WriteLine($"page={page.Title}");
                foreach (var area in page.Areas)
                {
                    Console.WriteLine($"area={area.Title}");
                    foreach (var par in area.Paragraphs)
                    {
                        Console.WriteLine($"par={par.Title}");
                        foreach (var line in par.Lines)
                        {
                            Console.WriteLine($"line={line.Title}");
                            foreach (var word in line.Words)
                            {
                                Console.WriteLine($"word={word.Title}");
                            }
                        }
                    }
                }
            }

            Console.ReadKey();
        }
    }
}
```

## For developers

Developers can rebuild tesseract from [build it from source](tools/Build.md).


