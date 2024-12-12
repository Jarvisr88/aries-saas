namespace DevExpress.Utils.Zip
{
    using System;
    using System.IO;

    public class CheckSumStream<T> : System.IO.Stream
    {
        private readonly System.IO.Stream stream;
        private readonly ICheckSumCalculator<T> checkSumCalculator;
        private T readCheckSum;
        private T writeCheckSum;

        public CheckSumStream(System.IO.Stream stream, ICheckSumCalculator<T> checkSumCalculator)
        {
            this.stream = stream;
            this.checkSumCalculator = checkSumCalculator;
            this.ResetCheckSum();
        }

        public override void Flush()
        {
            this.Stream.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            count = this.Stream.Read(buffer, offset, count);
            this.readCheckSum = this.checkSumCalculator.UpdateCheckSum(this.readCheckSum, buffer, offset, count);
            return count;
        }

        public void ResetCheckSum()
        {
            this.readCheckSum = this.checkSumCalculator.InitialCheckSumValue;
            this.writeCheckSum = this.checkSumCalculator.InitialCheckSumValue;
        }

        public override long Seek(long offset, SeekOrigin origin) => 
            this.Stream.Seek(offset, origin);

        public override void SetLength(long value)
        {
            this.Stream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            this.Stream.Write(buffer, offset, count);
            this.writeCheckSum = this.checkSumCalculator.UpdateCheckSum(this.writeCheckSum, buffer, offset, count);
        }

        public System.IO.Stream Stream =>
            this.stream;

        public override bool CanRead =>
            this.Stream.CanRead;

        public override bool CanSeek =>
            this.Stream.CanSeek;

        public override bool CanWrite =>
            this.Stream.CanWrite;

        public override long Length =>
            this.Stream.Length;

        public override long Position
        {
            get => 
                this.Stream.Position;
            set => 
                this.Stream.Position = value;
        }

        public T ReadCheckSum =>
            this.checkSumCalculator.GetFinalCheckSum(this.readCheckSum);

        public T WriteCheckSum =>
            this.checkSumCalculator.GetFinalCheckSum(this.writeCheckSum);
    }
}

