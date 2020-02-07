using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace TesseractSharpExtensions.Hocr
{
    // SEE http://kba.cloud/hocr-spec/1.2/
    public static class HOCRParser
    {
        private static IEnumerable<XmlNode> GetChildNodes(this XmlNode node)
        {
            if (!node.HasChildNodes)
                yield break;

            var childNodesList = node.ChildNodes;
            var childNodesCount = childNodesList.Count;

            for (var i = 0; i < childNodesCount; i++)
            {
                yield return childNodesList.Item(i);
            }
        }

        private static bool GetChildNode(this XmlNode node, string name, out XmlNode childNode)
        {
            childNode = null;
            if (!node.HasChildNodes)
                return false;

            var childNodesList = node.ChildNodes;
            var childNodesCount = childNodesList.Count;

            for (var i =0; i < childNodesCount; i++)
            {
                childNode = childNodesList.Item(i);
                if (childNode != null && childNode.Name.Equals(name))
                    return true;
            }

            childNode = null;
            return false;
        }

        private static bool GetAttribute(this XmlNode node, string name, out XmlAttribute attr)
        {
            attr = null;

            var attributesCollection = node.Attributes;
            if (attributesCollection == null)
                return false;

            var attributesCount = attributesCollection.Count;

            for (var i = 0; i < attributesCount; i++)
            {
                attr = attributesCollection[i];
                if (attr != null && attr.Name.Equals(name))
                    return true;
            }

            attr = null;
            return false;
        }

        private static HPage BuildPage(XmlNode node)
        {
            if (!node.Name.Equals("div"))
                throw new HOCRException("HWord should be in a span.");

            if (!node.GetAttribute("class", out var classAttr))
                throw new HOCRException("HObject should contain a class attribute.");

            if (!classAttr.Value.Equals("ocr_page"))
                throw new HOCRException($"Invalid class object {classAttr.Value}.");

            var (id, title) = GetHObjectAttr(node);

            var areas = node.GetChildNodes().Select(BuildCarea).ToList();

            var page = new HPage(id, title, areas);
            return page;
        }

        private static HCarea BuildCarea(XmlNode node)
        {
            if (!node.Name.Equals("div"))
                throw new HOCRException("HWord should be in a span.");

            if (!node.GetAttribute("class", out var classAttr))
                throw new HOCRException("HObject should contain a class attribute.");

            if (!classAttr.Value.Equals("ocr_carea"))
                throw new HOCRException($"Invalid class object {classAttr.Value}.");

            var (id, title) = GetHObjectAttr(node);

            var paragraphs = node.GetChildNodes().Select(BuildPar).ToList();

            var area = new HCarea(id, title, paragraphs );
            return area;
        }

        private static HPar BuildPar(XmlNode node)
        {
            if (!node.Name.Equals("p"))
                throw new HOCRException("HWord should be in a p.");

            if (!node.GetAttribute("class", out var classAttr))
                throw new HOCRException("HObject should contain a class attribute.");

            if (!classAttr.Value.Equals("ocr_par"))
                throw new HOCRException($"Invalid class object {classAttr.Value}.");

            var (id, title) = GetHObjectAttr(node);

            var lines = node.GetChildNodes().Select(BuildLine).ToList();

            var lang = "";
            if (node.GetAttribute("lang", out var langAttr))
                lang = langAttr.Value;

            var dir = "";
            if (node.GetAttribute("dir", out var dirAttr))
                dir = dirAttr.Value;

            var par = new HPar(id, title, lang, dir, lines);

            return par;
        }

        private static HLine BuildLine(XmlNode node)
        {
            if (!node.Name.Equals("span"))
                throw new HOCRException("HWord should be in a span.");

            if (!node.GetAttribute("class", out var classAttr))
                throw new HOCRException("HObject should contain a class attribute.");

            if (!classAttr.Value.Equals("ocr_line") && 
                !classAttr.Value.Equals("ocr_textfloat") && // Hack
                !classAttr.Value.Equals("ocr_header") &&
                !classAttr.Value.Equals("ocr_caption"))

                throw new HOCRException($"Invalid class object {classAttr.Value}.");

            var (id, title) = GetHObjectAttr(node);

            var words = node.GetChildNodes().Select(BuildWorld).ToList();

            var line = new HLine ( id, title, words);
            return line;
        }

        private static HWord BuildWorld(XmlNode node)
        {
            if (!node.Name.Equals("span"))
                throw new HOCRException("HWord should be in a span.");

            if (!node.GetAttribute("class", out var classAttr))
                throw new HOCRException("HObject should contain a class attribute.");

            if (!classAttr.Value.Equals("ocrx_word"))
                throw new HOCRException($"Invalid class object {classAttr.Value}.");

            var (id, title) = GetHObjectAttr(node);

            var lang = "";
            if (node.GetAttribute("lang", out var langAttr))
                lang = langAttr.Value;

            var dir = "";
            if (node.GetAttribute("dir", out var dirAttr))
                dir = dirAttr.Value;

            IEnumerable<HCInfo> cinfos = null;
            var childNodes = node.GetChildNodes().ToList();

            if (childNodes.Any(n => n.Name == "span"
                                    && n.GetAttribute("class", out var attr)
                                    && attr.Value.Equals("ocrx_cinfo")))
            {
                cinfos = childNodes.Select(BuildCInfo).ToList();
            }

            var word = new HWord(id, title, lang, dir, value: node.InnerText, hCInfos: cinfos);

            return word;
        }

        private static HCInfo BuildCInfo(XmlNode node)
        {
            if (!node.Name.Equals("span"))
                throw new HOCRException("HWord should be in a span.");

            if (!node.GetAttribute("class", out var classAttr))
                throw new HOCRException("HObject should contain a class attribute.");

            if (!classAttr.Value.Equals("ocrx_cinfo"))
                throw new HOCRException($"Invalid class object {classAttr.Value}.");

            if (!node.GetAttribute("title", out var title))
                throw new HOCRException("HCInfo should contain a title attribute.");

            var cinfo = new HCInfo(title.Value, value: node.InnerText);

            return cinfo;
        }

        private static (string, string) GetHObjectAttr(XmlNode node)
        {

            if (!node.GetAttribute("id", out var id))
                throw new HOCRException("HObject should contain an id attribute.");

            if (!node.GetAttribute("title", out var title))
                throw new HOCRException("HObject should contain a title attribute.");

            return (id.Value, title.Value);
        }

        public static HDocument Parse(StreamReader reader)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(reader.ReadToEnd());

            var root = doc.DocumentElement;
            if (root == null)
                throw new HOCRException("Fail to find root node.");

            if (!root.GetChildNode("head", out var head))
                throw new HOCRException("Fail to find head node.");

            if (!root.GetChildNode("body", out var body))
                throw new HOCRException("Fail to find body node.");

            var pages = body.GetChildNodes().Select(BuildPage).ToList();

            var hdoc = new HDocument(pages);

            return hdoc;
        }

        public static HDocument Parse(string file)
        {
            if (!File.Exists(file))
                throw new HOCRException($"File {file} not exists.");

            var steam = File.OpenText(file);

            return Parse(steam);
        }
    }
}
