using System.IO;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using TesseractSharpExtensions.Hocr;

namespace TesseractSharpExtensionsTests
{
    [TestFixture]
    internal class test_hocr : TestBase
    {
        [Test]
        public void TestParser()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyDirectory = Path.GetDirectoryName(assembly.Location);

            var file = Path.Combine(assemblyDirectory, @"samples\sample.hocr");

            var sample = File.OpenText(file).ReadToEnd();
            sample = RemoveFileNameFromHocr(sample);

            HDocument computed;
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(sample)))
            using (var reader = new StreamReader(stream))
            {
                computed = HOCRParser.Parse(reader);
            }

            var hDocumentComparer = HDocumentComparer.Instance;

            var expected = new HDocument(
                new []
                {
                    new HPage(
                        "page_1",
                        "image \"\"; bbox 0 0 600 350; ppageno 0",
                        new []
                        {
                            new HCarea(
                                "block_1_1",
                                "bbox 0 0 600 350",
                                new []
                                {
                                    new HPar(
                                        "par_1_1",
                                        "bbox 0 0 600 350",
                                        "eng",
                                        "",
                                        new []
                                        {
                                            new HLine(
                                                "line_1_1",
                                                "bbox 0 0 600 57; baseline 0 293; x_size 49.5; x_descenders 11.5; x_ascenders 15",
                                                new []
                                                {
                                                    new HWord(
                                                        "word_1_1",
                                                        "bbox 0 0 600 57; x_wconf 95",
                                                        "",
                                                        "",
                                                        ""), 
                                                }),
                                            new HLine(
                                                "line_1_2",
                                                "bbox 0 57 600 91; baseline 0 -1; x_size 42.986839; x_descenders 9.9868412; x_ascenders 13.026317",
                                                new []
                                                {
                                                    new HWord(
                                                        "word_1_2",
                                                        "bbox 0 57 600 91; x_wconf 95",
                                                        "",
                                                        "",
                                                        ""),

                                                }),
                                            new HLine(
                                                "line_1_3",
                                                "bbox 0 91 600 166; baseline 0 0; x_size 49.5; x_descenders 11.5; x_ascenders 15",
                                                new []
                                                {
                                                    new HWord(
                                                        "word_1_3",
                                                        "bbox 0 91 352 166; x_wconf 95",
                                                        "",
                                                        "",
                                                        ""),
                                                    new HWord(
                                                        "word_1_4",
                                                        "bbox 545 91 600 166; x_wconf 95",
                                                        "",
                                                        "",
                                                        ""),
                                                }),
                                            new HLine(
                                                "line_1_4",
                                                "bbox 0 276 600 350; baseline 0 -11; x_size 42.986839; x_descenders 9.9868412; x_ascenders 13.026317",
                                                new []
                                                {
                                                    new HWord(
                                                        "word_1_5",
                                                        "bbox 0 276 600 350; x_wconf 95",
                                                        "",
                                                        "",
                                                        ""),
                                                    new HWord(
                                                        "word_1_6",
                                                        "bbox 596 276 600 339; x_wconf 95",
                                                        "",
                                                        "",
                                                        ""),
                                                }),
                                        }),
                                }),
                            new HCarea(
                                "block_1_2",
                                "bbox 11 33 584 59",
                                new []
                                {
                                    new HPar(
                                        "par_1_2",
                                        "bbox 11 33 584 59",
                                        "fra",
                                        "",
                                        new []
                                        {
                                            new HLine(
                                                "line_1_5",
                                                "bbox 11 33 584 59; baseline 0 -6; x_size 23; x_descenders 3; x_ascenders 7",
                                                new []
                                                {
                                                    new HWord(
                                                        "word_1_7",
                                                        "bbox 11 33 73 53; x_wconf 63",
                                                        "",
                                                        "",
                                                        "CARTE"),
                                                    new HWord(
                                                        "word_1_8",
                                                        "bbox 72 29 155 63; x_wconf 90",
                                                        "",
                                                        "",
                                                        "NATIONALE"),
                                                    new HWord(
                                                        "word_1_9",
                                                        "bbox 155 29 239 63; x_wconf 81",
                                                        "",
                                                        "",
                                                        "D'IDENTITÉ"),
                                                    new HWord(
                                                        "word_1_10",
                                                        "bbox 240 35 262 53; x_wconf 68",
                                                        "",
                                                        "",
                                                        "N°:"),
                                                    new HWord(
                                                        "word_1_11",
                                                        "bbox 275 33 416 56; x_wconf 90",
                                                        "",
                                                        "",
                                                        "67492827482"),
                                                    new HWord(
                                                        "word_1_12",
                                                        "bbox 448 38 517 59; x_wconf 11",
                                                        "eng",
                                                        "",
                                                        "Nationals"),
                                                    new HWord(
                                                        "word_1_13",
                                                        "bbox 522 44 584 56; x_wconf 44",
                                                        "eng",
                                                        "",
                                                        "Franaise"),
                                                }),
                                        }),
                                }),
                            new HCarea(
                                "block_1_3",
                                "bbox 196 65 545 158",
                                new []
                                {
                                    new HPar(
                                        "par_1_3",
                                        "bbox 196 65 545 158",
                                        "eng",
                                        "",
                                        new []
                                        {
                                            new HLine(
                                                "line_1_6",
                                                "bbox 196 65 277 81; baseline 0.049 -3; x_size 22.862068; x_descenders 5.8620687; x_ascenders 5.666667",
                                                new []
                                                {
                                                    new HWord(
                                                        "word_1_14",
                                                        "bbox 196 69 232 79; x_wconf 76",
                                                        "",
                                                        "",
                                                        "Nom:"),
                                                    new HWord(
                                                        "word_1_15",
                                                        "bbox 239 65 277 81; x_wconf 96",
                                                        "",
                                                        "",
                                                        "JOE"),
                                                }),
                                            new HLine(
                                                "line_1_7",
                                                "bbox 196 104 346 130; baseline 0.007 -13; x_size 29.25; x_descenders 7.5; x_ascenders 7.25",
                                                new []
                                                {
                                                    new HWord(
                                                        "word_1_16",
                                                        "bbox 188 92 265 134; x_wconf 46",
                                                        "fra",
                                                        "",
                                                        "Prénomte)"),
                                                    new HWord(
                                                        "word_1_17",
                                                        "bbox 268 92 343 134; x_wconf 96",
                                                        "fra",
                                                        "",
                                                        "SAMPLE"),
                                                }),
                                            new HLine(
                                                "line_1_8",
                                                "bbox 352 142 545 158; baseline 0.01 -3; x_size 29.25; x_descenders 7.5; x_ascenders 7.25",
                                                new []
                                                {
                                                    new HWord(
                                                        "word_1_18",
                                                        "bbox 352 146 409 157; x_wconf 0",
                                                        "fra",
                                                        "",
                                                        "Note):"),
                                                    new HWord(
                                                        "word_1_19",
                                                        "bbox 417 142 545 158; x_wconf 74",
                                                        "fra",
                                                        "",
                                                        "12.02.1990"),
                                                }),
                                        }),
                                }),
                            new HCarea(
                                "block_1_4",
                                "bbox 40 282 559 331",
                                new []
                                {
                                    new HPar(
                                        "par_1_4",
                                        "bbox 40 282 559 331",
                                        "fra",
                                        "",
                                        new []
                                        {
                                            new HLine(
                                                "line_1_9",
                                                "bbox 40 282 558 295; baseline 0 0; x_size 17.333332; x_descenders 4.333333; x_ascenders 4.3333335",
                                                new []
                                                {
                                                    new HWord(
                                                        "word_1_20",
                                                        "bbox 40 282 558 295; x_wconf 91",
                                                        "",
                                                        "",
                                                        "FAKE<<ID<<GENERATOR<<FOR<<ANDROID<<<<"),
                                                }),
                                            new HLine(
                                                "line_1_10",
                                                "bbox 40 314 559 331; baseline 0 -3; x_size 17.333332; x_descenders 4.333333; x_ascenders 4.3333335",
                                                new []
                                                {
                                                    new HWord(
                                                        "word_1_21",
                                                        "bbox 40 314 559 331; x_wconf 84",
                                                        "eng",
                                                        "",
                                                        "2348M35810451QW58240<<245801<<1239801"),
                                                }),

                                        }),
                                }),
                            new HCarea(
                                "block_1_5",
                                "bbox 11 349 585 350",
                                new []
                                {
                                    new HPar(
                                        "par_1_5",
                                        "bbox 11 349 585 350",
                                        "eng",
                                        "",
                                        new []
                                        {
                                            new HLine(
                                                "line_1_11",
                                                "bbox 11 349 585 350; baseline 0 0; x_size 0.5; x_descenders -0.25; x_ascenders 0.25",
                                                new []
                                                {
                                                    new HWord(
                                                        "word_1_22",
                                                        "bbox 11 349 585 350; x_wconf 95",
                                                        "",
                                                        "",
                                                        ""),
                                                }),

                                        }),
                                }),
                        }
                    ),
                });

            Assert.IsTrue(hDocumentComparer.Equals(expected, computed));
        }

        [Test]
        public void TestParser2()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyDirectory = Path.GetDirectoryName(assembly.Location);

            var file = Path.Combine(assemblyDirectory, @"samples\sample2.hocr");

            var sample = File.OpenText(file).ReadToEnd();
            sample = RemoveFileNameFromHocr(sample);

            HDocument computed;
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(sample)))
            using (var reader = new StreamReader(stream))
            {
                computed = HOCRParser.Parse(reader);
            }

            var hDocumentComparer = HDocumentComparer.Instance;

            var expected = new HDocument(
                new[]
                {
                    new HPage(
                        "page_1",
                        "image \"\"; bbox 0 0 600 350; ppageno 0",
                        new []
                        {
                            new HCarea(
                                "block_1_1",
                                "bbox 0 0 600 72",
                                new []
                                {
                                    new HPar(
                                        "par_1_1",
                                        "bbox 0 0 600 72",
                                        "eng",
                                        "",
                                        new []
                                        {
                                            new HLine(
                                                "line_1_1",
                                                "bbox 0 0 600 72; baseline 0 0; x_size 97.333336; x_descenders 24.333334; x_ascenders 24.333334",
                                                new []
                                                {
                                                    new HWord(
                                                        "word_1_1",
                                                        "bbox 0 0 600 72; x_wconf 95",
                                                        "",
                                                        "",
                                                        value: null,
                                                        hCInfos: new []
                                                        {
                                                            new HCInfo(
                                                                title:"x_bboxes 0 57 196 72; x_conf 95",
                                                                value: "A"),
                                                            new HCInfo(
                                                                title:"x_bboxes 0 0 600 57; x_conf 95",
                                                                value: "B"),
                                                            new HCInfo(
                                                                title:"x_bboxes 277 57 600 72; x_conf 95",
                                                                value: "C"),
                                                        }),
                                                }),
                                        }),
                                }),
                        }
                    ),
                });

            Assert.IsTrue(hDocumentComparer.Equals(expected, computed));
        }
    }

}