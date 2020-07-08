using System.ComponentModel;

namespace TesseractSharp
{
    public enum Language
    {
        [Description("eng")]
        English = 0,

        [Description("fra")]
        French = 1,

        [Description("deu")]
        German = 2,

        [Description("ita")]
        Italian = 3,

        [Description("jpn")]
        Japanese = 4,

        [Description("por")]
        Portuguese = 5,

        [Description("spa")]
        SpanishCastilian = 6,

        [Description("chi_sim")]
        ChineseSimplified = 7,

        [Description("chi_tra")]
        ChineseTraditional = 8,

        [Description("equ")]
        MathAndEquationDetectionModule = 9,

        [Description("osd")]
        OrientationAndScriptDetectionModule = 10,

        [Description("rus")]
        Russian = 11
    }
}
