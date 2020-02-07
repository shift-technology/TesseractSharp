using System;

namespace TesseractSharp.Hocr
{
    public class HOCRException : Exception
    {
        public HOCRException(string message) : base(message) { }

        public HOCRException(string message, Exception innerException) : base(message, innerException) { }
    }
}
