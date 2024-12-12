namespace DevExpress.Internal
{
    using DevExpress.Utils;
    using System;
    using System.Text;

    public class InitialEncodingDetectorState : EncodingDetectorState
    {
        public InitialEncodingDetectorState(InternalEncodingDetector detector) : base(detector)
        {
        }

        public override bool AnalyseData(byte[] buffer, int from, int length)
        {
            if (length <= 0)
            {
                return false;
            }
            Encoding result = this.TryAnalyseUnicodeSignature(buffer, from, length);
            if (result != null)
            {
                base.Detector.DetectorState = new FinalEncodingDetectorState(base.Detector, result);
                return true;
            }
            base.Detector.DetectorState = new PureAsciiEncodingDetectorState(base.Detector);
            return base.Detector.AnalyseData(buffer, from, length);
        }

        public override Encoding CalculateResult() => 
            null;

        private Encoding TryAnalyseUnicodeSignature(byte[] buffer, int from, int length)
        {
            if (length > 3)
            {
                byte num = buffer[from];
                if (num > 0xef)
                {
                    if (num == 0xfe)
                    {
                        if ((0xff == buffer[from + 1]) && ((buffer[from + 2] == 0) && (buffer[from + 3] == 0)))
                        {
                            return null;
                        }
                        if (0xff == buffer[from + 1])
                        {
                            return DXEncoding.GetEncoding(0x4b1);
                        }
                    }
                    else if (num == 0xff)
                    {
                        if ((0xfe == buffer[from + 1]) && ((buffer[from + 2] == 0) && (buffer[from + 3] == 0)))
                        {
                            return Encoding.UTF32;
                        }
                        if (0xfe == buffer[from + 1])
                        {
                            return Encoding.Unicode;
                        }
                    }
                    else
                    {
                        goto TR_0001;
                    }
                }
                else if (num == 0)
                {
                    if ((buffer[from + 1] == 0) && ((0xfe == buffer[from + 2]) && (0xff == buffer[from + 3])))
                    {
                        return DXEncoding.GetEncoding(0x2ee1);
                    }
                    if ((buffer[from + 1] == 0) && (0xff == buffer[from + 2]))
                    {
                        byte num1 = buffer[from + 3];
                        return null;
                    }
                }
                else if (num == 0xef)
                {
                    if ((0xbb == buffer[from + 1]) && (0xbf == buffer[from + 2]))
                    {
                        return Encoding.UTF8;
                    }
                }
                else
                {
                    goto TR_0001;
                }
            }
            return null;
        TR_0001:
            return null;
        }
    }
}

