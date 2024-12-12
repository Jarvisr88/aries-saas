namespace DevExpress.Pdf.Native
{
    using System;

    public abstract class JBIG2AdaptiveTemplateRegionDecoder : JBIG2GenericRegionDecoder
    {
        private readonly int[] adaptiveTemplate;

        protected JBIG2AdaptiveTemplateRegionDecoder(JBIG2Image image, JBIG2Decoder decoder, int[] gbat) : base(image, decoder)
        {
            this.adaptiveTemplate = this.CreateAdaptiveTemplate(gbat);
        }

        protected abstract int[] CreateAdaptiveTemplate(int[] gbat);
        public override void Decode()
        {
            JBIG2Image image = base.Image;
            int[] adaptiveTemplate = this.AdaptiveTemplate;
            int y = 0;
            while (y < image.Height)
            {
                int x = 0;
                while (true)
                {
                    if (x >= image.Width)
                    {
                        y++;
                        break;
                    }
                    int context = 0;
                    int num4 = 0;
                    int num5 = 0;
                    while (true)
                    {
                        if (num4 >= adaptiveTemplate.Length)
                        {
                            image.SetPixel(x, y, base.Decoder.DecodeGB(context));
                            x++;
                            break;
                        }
                        context |= image.GetPixel(x + adaptiveTemplate[num4++], y + adaptiveTemplate[num4++]) << (num5++ & 0x1f);
                    }
                }
            }
        }

        protected int[] AdaptiveTemplate =>
            this.adaptiveTemplate;
    }
}

