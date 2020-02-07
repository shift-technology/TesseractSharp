namespace TesseractSharp
{
    public enum OcrEngineMode
    {
        /// <summary>
        /// Original Tesseract only.
        /// </summary>
        OemTesseractOnly = 0,

        /// <summary>
        /// Neural nets LSTM only.
        /// </summary>
        OemLstmOnly = 1,

        /// <summary>
        /// Tesseract + LSTM.
        /// </summary>
        OemTesseractLstmCombined = 2,

        /// <summary>
        /// Default, based on what is available.
        /// </summary>
        OemDefault = 3
    }
}
