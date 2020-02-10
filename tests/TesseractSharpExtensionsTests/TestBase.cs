using System;

namespace TesseractSharpExtensionsTests
{
    public class TestBase
    {
        public static string RemoveFileNameFromHocr(string text)
        {
            const string pattern = "title='image \"";

            var startTitleIdx = text.IndexOf(pattern, StringComparison.InvariantCulture);
            var endTitleIdx = (startTitleIdx > 0) ? text.IndexOf('"', startTitleIdx + pattern.Length + 1) : 0;

            if (startTitleIdx > 0 && endTitleIdx > 0)
            {
                text = text.Remove(startTitleIdx + pattern.Length, (endTitleIdx - startTitleIdx - pattern.Length));
            }

            return text;
        }
    }
}
