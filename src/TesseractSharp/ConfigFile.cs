using System.ComponentModel;

namespace TesseractSharp
{
    public enum ConfigFile
    {
        [Description("alto")]
        OutputAlto = 0,

        [Description("hocr")]
        OutputHocr = 1,

        [Description("pdf")]
        OutputPdf = 2,

        [Description("tsv")]
        OutputTsv = 3,

        [Description("txt")]
        OutputTxt = 4
    }
}
