namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.UI;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors;
    using System;
    using System.IO;
    using System.Windows;
    using System.Windows.Interop;
    using System.Windows.Media;
    using System.Windows.Media.Effects;
    using System.Windows.Media.Imaging;

    public class ImageLoader
    {
        private static BitmapDecoder CreateDecoder(ImageSource imageSource)
        {
            if (imageSource == null)
            {
                return null;
            }
            BitmapImage bitmapImage = imageSource as BitmapImage;
            if (bitmapImage != null)
            {
                return CreateDecoder(bitmapImage);
            }
            BitmapFrame frame = imageSource as BitmapFrame;
            return frame?.Decoder;
        }

        private static BitmapDecoder CreateDecoder(BitmapImage bitmapImage)
        {
            if (bitmapImage == null)
            {
                return null;
            }
            if ((bitmapImage.StreamSource == null) || !bitmapImage.StreamSource.CanRead)
            {
                return ((bitmapImage.UriSource == null) ? null : BitmapDecoder.Create(bitmapImage.UriSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default));
            }
            bitmapImage.StreamSource.Seek(0L, SeekOrigin.Begin);
            return BitmapDecoder.Create(bitmapImage.StreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
        }

        private static BitmapEncoder CreateEncoder(BitmapDecoder decoder, ImageSource source) => 
            ((decoder == null) || (decoder.CodecInfo == null)) ? (!(source is BitmapSource) ? null : new BmpBitmapEncoder()) : BitmapEncoder.Create(decoder.CodecInfo.ContainerFormat);

        private static BitmapEncoder GetEncoderByFilterIndex(int index)
        {
            switch (index)
            {
                case 2:
                    return new BmpBitmapEncoder();

                case 3:
                    return new JpegBitmapEncoder();

                case 4:
                    return new GifBitmapEncoder();
            }
            return new PngBitmapEncoder();
        }

        public static BitmapSource GetSafeBitmapSource(BitmapSource source, Effect effect)
        {
            if (effect == null)
            {
                return source;
            }
            DrawingVisual visual1 = new DrawingVisual();
            visual1.Effect = effect;
            DrawingVisual visual = visual1;
            DrawingContext context = visual.RenderOpen();
            context.DrawImage(source, new Rect(0.0, 0.0, source.Width, source.Height));
            context.Close();
            RenderTargetBitmap bitmap = new RenderTargetBitmap(source.PixelWidth, source.PixelHeight, 96.0, 96.0, PixelFormats.Pbgra32);
            bitmap.Render(visual);
            return bitmap;
        }

        private static byte[] ImageToByArrayCore(ImageSource source)
        {
            BitmapDecoder decoder = CreateDecoder(source);
            BitmapEncoder encoder = CreateEncoder(decoder, source);
            if (!Initialize(encoder, decoder, source))
            {
                return null;
            }
            using (MemoryStream stream = new MemoryStream())
            {
                encoder.Save(stream);
                return stream.ToArray();
            }
        }

        public static byte[] ImageToByteArray(ImageSource source)
        {
            try
            {
                return ImageToByArrayCore(source);
            }
            catch
            {
                return null;
            }
        }

        private static bool Initialize(BitmapEncoder encoder, BitmapDecoder decoder, ImageSource source)
        {
            if (encoder == null)
            {
                return false;
            }
            if (decoder == null)
            {
                BitmapSource source2 = source as BitmapSource;
                if (source2 == null)
                {
                    return false;
                }
                encoder.Frames.Add(BitmapFrame.Create(source2));
                return true;
            }
            foreach (BitmapFrame frame in decoder.Frames)
            {
                encoder.Frames.Add(BitmapFrame.Create(frame));
            }
            return true;
        }

        public static ImageSource LoadImage()
        {
            if (BrowserInteropHelper.IsBrowserHosted)
            {
                return null;
            }
            try
            {
                return LoadImageCore();
            }
            catch (Exception exception1)
            {
                if (exception1 is NotSupportedException)
                {
                    MessageBoxHelper.ShowError(EditorLocalizer.GetString(EditorStringId.ImageEdit_InvalidFormatMessage), EditorLocalizer.GetString(EditorStringId.CaptionError), MessageBoxButton.OK);
                }
                return null;
            }
        }

        private static ImageSource LoadImageCore()
        {
            IOpenFileDialogService service = new OpenFileDialogService {
                Filter = EditorLocalizer.GetString(EditorStringId.ImageEdit_OpenFileFilter)
            };
            if (!service.ShowDialog())
            {
                return null;
            }
            using (Stream stream = service.File.OpenRead())
            {
                return ImageHelper.CreateImageFromStream(new MemoryStream(stream.GetDataFromStream()));
            }
        }

        public static void SaveImage(BitmapSource image)
        {
            if ((image != null) && !BrowserInteropHelper.IsBrowserHosted)
            {
                SaveImageCore(image);
            }
        }

        private static void SaveImageCore(BitmapSource image)
        {
            ISaveFileDialogService service = new SaveFileDialogService {
                Filter = EditorLocalizer.GetString(EditorStringId.ImageEdit_SaveFileFilter)
            };
            if (service.ShowDialog(null, null))
            {
                using (Stream stream = service.OpenFile())
                {
                    BitmapEncoder encoderByFilterIndex = GetEncoderByFilterIndex(service.FilterIndex);
                    encoderByFilterIndex.Frames.Add(BitmapFrame.Create(image));
                    encoderByFilterIndex.Save(stream);
                }
            }
        }
    }
}

