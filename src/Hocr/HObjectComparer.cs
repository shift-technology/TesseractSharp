using System.Collections.Generic;

namespace TesseractSharp.Hocr
{
    public class HObjectComparer : EqualityComparer<HObject>
    {
        public static HObjectComparer Instance { get; } = new HObjectComparer();

        public override bool Equals(HObject x, HObject y)
        {
            if (x == null && y == null)
                return true;
            if (x != null && y == null)
                return false;
            if (x == null)
                return false;

            if (!x.Id.Equals(y.Id))
                return false;

            if (!x.Title.Equals(y.Title))
                return false;

            return true;
        }

        public override int GetHashCode(HObject obj)
        {
            return obj.Id.GetHashCode() ^ obj.Title.GetHashCode();
        }
    }

    public class HWordComparer : EqualityComparer<HWord>
    {
        public static HWordComparer Instance { get; } = new HWordComparer();

        public override bool Equals(HWord x, HWord y)
        {
            if (x == null && y == null)
                return true;
            if (x != null && y == null)
                return false;
            if (x == null)
                return false;

            var hObjectComparer = HObjectComparer.Instance;

            if (!hObjectComparer.Equals(x, y))
                return false;

            if (x.Lang != null || y.Lang != null)
            {
                if (x.Lang != null && y.Lang != null)
                {
                    if (!x.Lang.Equals(y.Lang))
                        return false;
                }
                else if ((x.Lang != null && y.Lang == null) || (x.Lang == null && y.Lang != null))
                    return false;
            }

            if (x.Dir != null || y.Dir != null)
            {
                if (x.Dir != null && y.Dir != null)
                {
                    if (!x.Dir.Equals(y.Dir))
                        return false;
                }
                else if ((x.Dir != null && y.Dir == null) || (x.Dir == null && y.Dir != null))
                    return false;
            }

            return true;
        }

        public override int GetHashCode(HWord obj)
        {
            return obj.Id.GetHashCode() ^ obj.Title.GetHashCode() ^ obj.Lang.GetHashCode() ^ obj.Dir.GetHashCode();
        }
    }

    public class HLineComparer : EqualityComparer<HLine>
    {
        public static HLineComparer Instance { get; } = new HLineComparer();

        public override bool Equals(HLine x, HLine y)
        {
            if (x == null && y == null)
                return true;
            if (x != null && y == null)
                return false;
            if (x == null)
                return false;

            if (x.Words.Length != y.Words.Length)
                return false;

            var hObjectComparer = HObjectComparer.Instance;

            if (!hObjectComparer.Equals(x, y))
                return false;

            var hWordComparer = HWordComparer.Instance;

            for (var wordNum = 0; wordNum < x.Words.Length; wordNum++)
            {
                var xWord = x.Words[wordNum];
                var yWord = y.Words[wordNum];

                if (!hWordComparer.Equals(xWord, yWord))
                    return false;
            }

            return true;
        }

        public override int GetHashCode(HLine obj)
        {
            return obj.Id.GetHashCode() ^ obj.Title.GetHashCode() ^ obj.Words.GetHashCode();
        }
    }

    public class HParComparer : EqualityComparer<HPar>
    {
        public static HParComparer Instance { get; } = new HParComparer();

        public override bool Equals(HPar x, HPar y)
        {
            if (x == null && y == null)
                return true;
            if (x != null && y == null)
                return false;
            if (x == null)
                return false;

            var hObjectComparer = HObjectComparer.Instance;

            if (!hObjectComparer.Equals(x, y))
                return false;

            if (x.Lang != null || y.Lang != null)
            {
                if (x.Lang != null && y.Lang != null)
                {
                    if (!x.Lang.Equals(y.Lang))
                        return false;
                }
                else if ((x.Lang != null && y.Lang == null) || (x.Lang == null && y.Lang != null))
                    return false;
            }

            if (x.Dir != null || y.Dir != null)
            {
                if (x.Dir != null && y.Dir != null)
                {
                    if (!x.Dir.Equals(y.Dir))
                        return false;
                }
                else if ((x.Dir != null && y.Dir == null) || (x.Dir == null && y.Dir != null))
                    return false;
            }

            var hLineComparer = HLineComparer.Instance;

            if (x.Lines.Length != y.Lines.Length)
                return false;

            for (var parNum = 0; parNum < x.Lines.Length; parNum++)
            {
                var xPar = x.Lines[parNum];
                var yPar = y.Lines[parNum];

                if (!hLineComparer.Equals(xPar, yPar))
                    return false;
            }

            return true;
        }

        public override int GetHashCode(HPar obj)
        {
            return obj.Id.GetHashCode() ^ obj.Title.GetHashCode() ^ obj.Lang.GetHashCode() ^ obj.Dir.GetHashCode() ^ obj.Lines.GetHashCode();
        }
    }

    public class HCareaComparer : EqualityComparer<HCarea>
    {
        public static HCareaComparer Instance { get; } = new HCareaComparer();

        public override bool Equals(HCarea x, HCarea y)
        {
            if (x == null && y == null)
                return true;
            if (x != null && y == null)
                return false;
            if (x == null)
                return false;

            var hObjectComparer = HObjectComparer.Instance;

            if (!hObjectComparer.Equals(x, y))
                return false;

            var hParComparer = HParComparer.Instance;

            if (x.Paragraphs.Length != y.Paragraphs.Length)
                return false;

            for (var parNum = 0; parNum < x.Paragraphs.Length; parNum++)
            {
                var xPar = x.Paragraphs[parNum];
                var yPar = y.Paragraphs[parNum];

                if (!hParComparer.Equals(xPar, yPar))
                    return false;
            }

            return true;
        }

        public override int GetHashCode(HCarea obj)
        {
            return obj.Id.GetHashCode() ^ obj.Title.GetHashCode() ^ obj.Paragraphs.GetHashCode();
        }
    }

    public class HPageComparer : EqualityComparer<HPage>
    {
        public static HPageComparer Instance { get; } = new HPageComparer();

        public override bool Equals(HPage x, HPage y)
        {
            if (x == null && y == null)
                return true;
            if (x != null && y == null)
                return false;
            if (x == null)
                return false;

            var hObjectComparer = HObjectComparer.Instance;

            if (!hObjectComparer.Equals(x, y))
                return false;

            var hCareaComparer = HCareaComparer.Instance;

            if (x.Areas.Length != y.Areas.Length)
                return false;

            for (var areaNum = 0; areaNum < x.Areas.Length; areaNum++)
            {
                var xArea = x.Areas[areaNum];
                var yArea = y.Areas[areaNum];

                if (!hCareaComparer.Equals(xArea, yArea))
                    return false;
            }

            return true;
        }

        public override int GetHashCode(HPage obj)
        {
            return obj.Id.GetHashCode() ^ obj.Title.GetHashCode() ^ obj.Areas.GetHashCode();
        }
    }

    public class HDocumentComparer : EqualityComparer<HDocument>
    {
        public static HDocumentComparer Instance { get; } = new HDocumentComparer();

        public override bool Equals(HDocument x, HDocument y)
        {
            if (x == null && y == null)
                return true;
            if (x != null && y == null)
                return false;
            if (x == null)
                return false;

            var hPageComparer = HPageComparer.Instance;

            if (x.Pages.Length != y.Pages.Length)
                return false;

            for (var pageNum = 0; pageNum < x.Pages.Length; pageNum++)
            {
                var xPage = x.Pages[pageNum];
                var yPage = y.Pages[pageNum];

                if (!hPageComparer.Equals(xPage, yPage))
                    return false;
            }

            return true;
        }

        public override int GetHashCode(HDocument obj)
        {
            return obj.Pages.GetHashCode();
        }
    }
}
