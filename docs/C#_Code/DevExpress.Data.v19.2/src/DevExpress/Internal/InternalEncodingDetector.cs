namespace DevExpress.Internal
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public class InternalEncodingDetector
    {
        private readonly EncodingDetectorLanguageFilter languageFilter;
        private BomLessUnicodeGroupDetector unicodeDetector;
        private List<EncodingDetectorBase> detectors;
        private EncodingDetectorState detectorState;

        public InternalEncodingDetector() : this(EncodingDetectorLanguageFilter.All)
        {
        }

        internal InternalEncodingDetector(EncodingDetectorLanguageFilter languageFilter)
        {
            this.languageFilter = languageFilter;
        }

        public bool AnalyseData(byte[] buffer, int from, int length) => 
            this.DetectorState.AnalyseData(buffer, from, length);

        public void BeginDetection()
        {
            this.unicodeDetector = new BomLessUnicodeGroupDetector();
            this.detectors = this.CreateDetectors();
            this.DetectorState = new InitialEncodingDetectorState(this);
        }

        protected internal virtual List<EncodingDetectorBase> CreateDetectors() => 
            new List<EncodingDetectorBase> { 
                new MultiByteCharsetGroupDetector(this.LanguageFilter),
                new SingleByteCharsetGroupDetector(),
                new Latin1EncodingDetector(),
                this.unicodeDetector
            };

        public Encoding Detect(byte[] buffer) => 
            this.Detect(buffer, 0, buffer.Length);

        public Encoding Detect(Stream stream) => 
            this.Detect(stream, 0x1000, true);

        public Encoding Detect(Stream stream, int maxByteCount) => 
            this.Detect(stream, maxByteCount, true);

        public Encoding Detect(Stream stream, int maxByteCount, bool keepPosition)
        {
            Encoding encoding;
            long offset = 0L;
            if (keepPosition)
            {
                offset = stream.Position;
            }
            try
            {
                int num2 = Math.Min((int) (stream.Length - stream.Position), maxByteCount);
                encoding = (num2 > 0) ? this.DetectStreamEncodingCore(stream, num2) : null;
            }
            finally
            {
                if (keepPosition)
                {
                    stream.Seek(offset, SeekOrigin.Begin);
                }
            }
            return encoding;
        }

        public Encoding Detect(byte[] buffer, int from, int length)
        {
            this.BeginDetection();
            this.AnalyseData(buffer, from, length);
            return this.EndDetection();
        }

        private Encoding DetectStreamEncodingCore(Stream stream, int maxByteCount)
        {
            byte[] buffer = new byte[Math.Min(0x400, maxByteCount)];
            this.BeginDetection();
            while (true)
            {
                if (maxByteCount > 0)
                {
                    int count = Math.Min(maxByteCount, buffer.Length);
                    stream.Read(buffer, 0, count);
                    if (!this.AnalyseData(buffer, 0, count))
                    {
                        maxByteCount -= count;
                        continue;
                    }
                }
                return this.EndDetection();
            }
        }

        public Encoding EndDetection() => 
            this.DetectorState.CalculateResult();

        public EncodingDetectorLanguageFilter LanguageFilter =>
            this.languageFilter;

        protected internal List<EncodingDetectorBase> Detectors =>
            this.detectors;

        protected internal EncodingDetectorBase UnicodeDetector =>
            this.unicodeDetector;

        protected internal EncodingDetectorState DetectorState
        {
            get => 
                this.detectorState;
            set => 
                this.detectorState = value;
        }
    }
}

