using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace TesseractSharp.Core
{
    public class BurnAfterReadingFileStream : Stream
    {
        private readonly FileStream _fs;
        public BurnAfterReadingFileStream(string path) { _fs = File.OpenRead(path); }
        public override bool CanRead => true;
        public override bool CanSeek => false;
        public override bool CanWrite => false;
        public override long Length => _fs.Length;

        public override long Position
        {
            get => _fs.Position;
            set => _fs.Position = value;
        }

        public override void Flush() { _fs.Flush(); }

        public override int Read(byte[] buffer, int offset, int count) => _fs.Read(buffer, offset, count);

        public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken) => _fs.ReadAsync(buffer, offset, count, cancellationToken);

        public override long Seek(long offset, SeekOrigin origin) => _fs.Seek(offset, origin);

        public override void SetLength(long value) => throw new NotSupportedException();

        public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _fs.Close();

            if (!File.Exists(_fs.Name))
                return;

            try
            {
                File.Delete(_fs.Name);
            }
            catch
            {
                // ignored
            }
        }
    }
}
