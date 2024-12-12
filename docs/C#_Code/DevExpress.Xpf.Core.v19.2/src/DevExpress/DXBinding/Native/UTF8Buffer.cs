namespace DevExpress.DXBinding.Native
{
    using System;

    internal class UTF8Buffer : DevExpress.DXBinding.Native.Buffer
    {
        public UTF8Buffer(DevExpress.DXBinding.Native.Buffer b) : base(b)
        {
        }

        public override int Read()
        {
            while (true)
            {
                int num = base.Read();
                if ((num < 0x80) || (((num & 0xc0) == 0xc0) || (num == 0x10000)))
                {
                    if ((num >= 0x80) && (num != 0x10000))
                    {
                        if ((num & 240) == 240)
                        {
                            int num3 = base.Read() & 0x3f;
                            int num4 = base.Read() & 0x3f;
                            int num5 = base.Read() & 0x3f;
                            num = ((((((num & 7) << 6) | num3) << 6) | num4) << 6) | num5;
                        }
                        else if ((num & 0xe0) == 0xe0)
                        {
                            int num7 = base.Read() & 0x3f;
                            int num8 = base.Read() & 0x3f;
                            num = ((((num & 15) << 6) | num7) << 6) | num8;
                        }
                        else if ((num & 0xc0) == 0xc0)
                        {
                            int num10 = base.Read() & 0x3f;
                            num = ((num & 0x1f) << 6) | num10;
                        }
                    }
                    return num;
                }
            }
        }
    }
}

