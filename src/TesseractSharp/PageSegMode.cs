namespace TesseractSharp
{
    public enum PageSegMode
    {
        /// <summary>
        /// Orientation and script detection (OSD) only.
        /// </summary>
        PsmOsdOnly = 0,

        /// <summary>
        /// Automatic page segmentation with OSD.
        /// </summary>
        PsmAutoOsd = 1,

        /// <summary>
        /// Automatic page segmentation, but no OSD, or OCR. (not implemented)
        /// </summary>
        PsmAutoOnly = 2,

        /// <summary>
        /// Fully automatic page segmentation, but no OSD. (Default)
        /// </summary>
        PsmAuto = 3,

        /// <summary>
        /// Assume a single column of text of variable sizes.
        /// </summary>
        PsmSingleColumn = 4,

        /// <summary>
        /// Assume a single uniform block of vertically aligned text.
        /// </summary>
        PsmSingleBlockVertText = 5,

        /// <summary>
        /// Assume a single uniform block of text.
        /// </summary>
        PsmSingleBlock = 6,

        /// <summary>
        /// Treat the image as a single text line.
        /// </summary>
        PsmSingleLine = 7,

        /// <summary>
        /// Treat the image as a single word.
        /// </summary>
        PsmSingleWord = 8,

        /// <summary>
        /// Treat the image as a single word in a circle.
        /// </summary>
        PsmCircleWord = 9,

        /// <summary>
        /// Treat the image as a single character.
        /// </summary>
        PsmSingleChar = 10,

        /// <summary>
        /// Sparse text. Find as much text as possible in no particular order.
        /// </summary>
        PsmSparseText = 11,

        /// <summary>
        /// Sparse text with OSD.
        /// </summary>
        PsmSparseTextOsd = 12,

        /// <summary>
        /// Raw line. Treat the image as a single text line
        /// </summary>
        PsmRawLine = 13
    }
}
