using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace TesseractSharp.Hocr
{
    public struct WConf
    {
        public WConf(float confidence)
        {
            Confidence = confidence;
        }

        public float Confidence { get; }
    } 

    public struct Baseline
    {
        public Baseline(float slope, float constant)
        {
            Slope = slope;
            Constant = constant;
        }

        public float Slope { get; }
        public float Constant { get; }

    }
    
    public struct BBox
    {
        public BBox(int x0, int y0, int x1, int y1)
        {
            X0 = x0;
            Y0 = y0;
            X1 = x1;
            Y1 = y1;
        }

        public int X0 { get; }
        public int Y0 { get; }
        public int X1 { get; }
        public int Y1 { get; }

        public int Width => X1 - X0;
        public int Height => Y1 - Y0;
    }

    public abstract class HTitle
    {
        protected HTitle(string value)
        {
            Title = value;

            var rawFields = new Dictionary<string, string>();

            var fields = value.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var field in fields)
            {
                var subfields = field.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (subfields.Length == 0)
                    throw new HOCRException($"Invalid title format {value}");

                rawFields[subfields[0]] = string.Join(" ", subfields.Where(((s, i) => i > 0)));

                if (subfields[0].Equals("bbox") || subfields[0].Equals("x_bboxes"))
                {
                    if (subfields.Length != 5)
                        throw new HOCRException($"Invalid title format {value}");

                    BBox = new BBox(
                        x0: int.Parse(subfields[1]),
                        y0: int.Parse(subfields[2]), 
                        x1: int.Parse(subfields[3]), 
                        y1: int.Parse(subfields[4]));
                }
                else if (subfields[0].Equals("baseline"))
                {
                    if (subfields.Length != 3)
                        throw new HOCRException($"Invalid title format {value}");

                    Baseline = new Baseline(
                        slope: float.Parse(subfields[1], CultureInfo.InvariantCulture.NumberFormat),
                        constant: float.Parse(subfields[2], CultureInfo.InvariantCulture.NumberFormat));
                }
                else if (subfields[0].Equals("x_wconf"))
                {
                    if (subfields.Length != 2)
                        throw new HOCRException($"Invalid title format {value}");

                    WConf = new WConf(confidence: float.Parse(subfields[1], CultureInfo.InvariantCulture.NumberFormat));
                }
            }

            Fields = rawFields;
        }

        public string Title { get; }
        public IReadOnlyDictionary<string, string> Fields { get; }
        public BBox BBox { get; }
        public Baseline Baseline { get; }
        public WConf WConf { get; }
    }

    public abstract class HObject : HTitle
    {
        public string Id { get; }

        protected HObject(string id, string title) : base(title)
        {
            Id = id;
        }
    }

    public class HCInfo : HTitle
    {
        public HCInfo(string title, string value) : base(title)
        {
            Value = value;
        }
        
        public string Value { get; }
    }

    public class HWord : HObject
    {
        public HWord(string id, string title, string lang, string dir, string value, IEnumerable<HCInfo> hCInfos = null) : base(id, title)
        {
            Lang = lang;
            Dir = dir;
            CInfos = hCInfos?.ToArray() ?? new HCInfo[0];
            Value = hCInfos == null ? value : string.Join("", CInfos.Select(i => i.Value));
        }

        public string Lang { get; }
        public string Dir { get; }
        public string Value { get; }
        public HCInfo[] CInfos { get; }
    }

    public class HLine : HObject
    {
        public HLine(string id, string title, IEnumerable<HWord> words) : base(id, title)
        {
            Words = words.ToArray();  
        }

        public string ConcatenatedWords => string.Join(" ", Words.Select(w => w.Value));

        public HWord[] Words { get; }
    }

    public class HPar : HObject
    {
        public HPar(string id, string title, string lang, string dir, IEnumerable<HLine> lines) : base(id, title)
        {
            Lang = lang;
            Dir = dir;
            Lines = lines.ToArray();  
        }

        public string Lang { get; }
        public string Dir { get; }
        public HLine[] Lines { get; }
    }

    public class HCarea : HObject
    {
        public HCarea(string id, string title, IEnumerable<HPar> paragraphs) : base(id, title)
        {
            Paragraphs = paragraphs.ToArray();    
        }

        public HPar[] Paragraphs { get; }
    }

    public class HPage : HObject
    {
        public HPage(string id, string title, IEnumerable<HCarea> areas) : base(id, title)
        {
            Areas = areas.ToArray();  
        }

        public HCarea[] Areas { get; }
    }

    public class HDocument
    {
        public HDocument(IEnumerable<HPage> pages)
        {
            Pages = pages.ToArray();  
        }

        public HPage[] Pages { get; }
    }
}
