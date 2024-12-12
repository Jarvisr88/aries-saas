namespace DevExpress.Internal
{
    using System;
    using System.Collections.Generic;

    public class SingleByteCharsetGroupDetector : GroupDetector
    {
        public SingleByteCharsetGroupDetector()
        {
            this.CreateDetectors();
        }

        protected internal byte[] FilterBuffer(byte[] buffer, int from, int length)
        {
            List<byte> target = new List<byte>();
            bool flag = false;
            int to = from + length;
            int num2 = from;
            for (int i = from; i < to; i++)
            {
                byte currentByte = buffer[i];
                if (base.IsUpperAsciiByte(currentByte))
                {
                    flag = true;
                }
                else if (base.IsNonEnglishLetterLowerAsciiByte(currentByte))
                {
                    if (!flag || (i <= num2))
                    {
                        num2 = i + 1;
                    }
                    else
                    {
                        num2 = 1 + base.AppendBytes(buffer, num2, i, target);
                        target.Add(0x20);
                        flag = false;
                    }
                }
            }
            if (flag)
            {
                base.AppendBytes(buffer, num2, to, target);
            }
            return target.ToArray();
        }

        protected internal override DetectionResult ForceAnalyseData(byte[] buffer, int from, int length)
        {
            byte[] buffer2 = this.FilterBuffer(buffer, from, length);
            return this.AnalyseDataCore(buffer2, 0, buffer2.Length);
        }

        protected internal override void PopulateDetectors()
        {
            base.Detectors.Add(new Win1251EncodingDetector());
            base.Detectors.Add(new Koi8rEncodingDetector());
            base.Detectors.Add(new Latin5EncodingDetector());
            base.Detectors.Add(new MacCyrillicDetector());
            base.Detectors.Add(new Ibm866EncodingDetector());
            base.Detectors.Add(new Ibm855EncodingDetector());
            base.Detectors.Add(new Latin7EncodingDetector());
            base.Detectors.Add(new Win1253EncodingDetector());
            base.Detectors.Add(new Latin5BulgarianEncodingDetector());
            base.Detectors.Add(new Win1251BulgarianEncodingDetector());
        }
    }
}

