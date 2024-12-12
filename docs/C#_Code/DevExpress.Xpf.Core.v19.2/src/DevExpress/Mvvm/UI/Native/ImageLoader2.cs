namespace DevExpress.Mvvm.UI.Native
{
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Packaging;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    public static class ImageLoader2
    {
        private static Dictionary<Uri, WeakReference> cache = new Dictionary<Uri, WeakReference>();
        private const int MaxCacheSize = 320;
        private static Type wpfWebRequestHelper;
        private static MethodInfo createRequestAndGetResponseStreamMethod;
        private static Type securityHelper;
        private static MethodInfo mapUrlToZoneWrapperMethod;

        private static void AddToCache(Uri uri, byte[] data)
        {
            Dictionary<Uri, WeakReference> cache = ImageLoader2.cache;
            lock (cache)
            {
                if (!ImageLoader2.cache.ContainsKey(uri))
                {
                    if (ImageLoader2.cache.Count == 320)
                    {
                        Func<KeyValuePair<Uri, WeakReference>, bool> predicate = <>c.<>9__30_0;
                        if (<>c.<>9__30_0 == null)
                        {
                            Func<KeyValuePair<Uri, WeakReference>, bool> local1 = <>c.<>9__30_0;
                            predicate = <>c.<>9__30_0 = r => (r.Value != null) && (r.Value.Target == null);
                        }
                        Func<KeyValuePair<Uri, WeakReference>, Uri> selector = <>c.<>9__30_1;
                        if (<>c.<>9__30_1 == null)
                        {
                            Func<KeyValuePair<Uri, WeakReference>, Uri> local2 = <>c.<>9__30_1;
                            selector = <>c.<>9__30_1 = r => r.Key;
                        }
                        foreach (Uri uri2 in ImageLoader2.cache.Where<KeyValuePair<Uri, WeakReference>>(predicate).Select<KeyValuePair<Uri, WeakReference>, Uri>(selector).ToList<Uri>())
                        {
                            ImageLoader2.cache.Remove(uri2);
                        }
                    }
                    if (ImageLoader2.cache.Count != 320)
                    {
                        ImageLoader2.cache[uri] = new WeakReference(data);
                    }
                }
            }
        }

        private static byte[] CheckCache(Uri uri)
        {
            Dictionary<Uri, WeakReference> cache = ImageLoader2.cache;
            lock (cache)
            {
                WeakReference reference;
                return (ImageLoader2.cache.TryGetValue(uri, out reference) ? ((byte[]) reference.Target) : null);
            }
        }

        private static Stream CreateRequestAndGetResponseStream(Uri uri)
        {
            Stream stream;
            try
            {
                object[] parameters = new object[] { uri };
                stream = (Stream) CreateRequestAndGetResponseStreamMethod.Invoke(null, parameters);
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException("", exception);
            }
            return stream;
        }

        private static FieldInfo GetDecoderField(BitmapDecoder decoder, string fieldName)
        {
            FieldInfo field = decoder.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            if (field == null)
            {
                throw new Exception($"{decoder.GetType().FullName}.{fieldName} ({decoder.GetType().AssemblyQualifiedName})");
            }
            return field;
        }

        private static byte[] ImageToByteArray(Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException("uri");
            }
            byte[] data = CheckCache(uri);
            if (data == null)
            {
                Stream stream = null;
                if (uri.IsAbsoluteUri && string.Equals(uri.Scheme, PackUriHelper.UriSchemePack, StringComparison.OrdinalIgnoreCase))
                {
                    stream = CreateRequestAndGetResponseStream(uri);
                }
                stream ??= (uri.IsAbsoluteUri ? ((!uri.IsFile || (!uri.IsUnc && (MapUrlToZoneWrapper(uri) != 0))) ? CreateRequestAndGetResponseStream(uri) : new FileStream(uri.LocalPath, FileMode.Open, FileAccess.Read, FileShare.Read)) : new FileStream(uri.OriginalString, FileMode.Open, FileAccess.Read, FileShare.Read));
                if (stream == null)
                {
                    return null;
                }
                data = stream.CopyAllBytes();
                AddToCache(uri, data);
            }
            return data;
        }

        public static byte[] ImageToByteArray(ImageSource source, Func<Uri> baseUriProvider = null, Size? drawingImageSize = new Size?())
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            byte[] buffer = TryGetStreamData(source, drawingImageSize);
            if (buffer != null)
            {
                if (buffer.Length == 0)
                {
                    throw new ArgumentException("EndOfStream", "source");
                }
                return buffer;
            }
            Uri relativeUri = TryGetUri(source);
            if (relativeUri == null)
            {
                throw new ArgumentException("ImageSource", "source");
            }
            Uri baseUri = baseUriProvider?.Invoke();
            byte[] buffer2 = ImageToByteArray((baseUri == null) ? relativeUri : new Uri(baseUri, relativeUri));
            if (buffer2 == null)
            {
                throw new ArgumentException("Uri:Stream.CanRead", "source");
            }
            if (buffer2.Length == 0)
            {
                throw new ArgumentException("Uri:EndOfStream", "source");
            }
            return buffer2;
        }

        private static int MapUrlToZoneWrapper(Uri url)
        {
            int num;
            try
            {
                object[] parameters = new object[] { url };
                num = (int) MapUrlToZoneWrapperMethod.Invoke(null, parameters);
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException("", exception);
            }
            return num;
        }

        private static void RemoveFromCache(Uri uri)
        {
            Dictionary<Uri, WeakReference> cache = ImageLoader2.cache;
            lock (cache)
            {
                if (ImageLoader2.cache.ContainsKey(uri))
                {
                    ImageLoader2.cache.Remove(uri);
                }
            }
        }

        private static byte[] SaveAsPng(BitmapSource bitmapSource)
        {
            PngBitmapEncoder encoder = new PngBitmapEncoder {
                Frames = { BitmapFrame.Create(bitmapSource) }
            };
            using (MemoryStream stream = new MemoryStream())
            {
                encoder.Save(stream);
                return stream.ToArray();
            }
        }

        private static byte[] TryCopyAllBytes(Stream stream)
        {
            try
            {
                return stream.CopyAllBytes();
            }
            catch (ObjectDisposedException)
            {
                return null;
            }
        }

        private static T TryGetImageData<T>(ImageSource imageSource, Func<BitmapImage, T> fromBitmapImage, Func<BitmapFrame, T> fromBitmapFrame, Func<DrawingImage, T> fromDrawingImage, Func<BitmapSource, T> fromGenericBitmapSource, Func<T> fallback)
        {
            BitmapImage arg = imageSource as BitmapImage;
            if (arg != null)
            {
                return fromBitmapImage(arg);
            }
            BitmapFrame frame = imageSource as BitmapFrame;
            if (frame != null)
            {
                return fromBitmapFrame(frame);
            }
            DrawingImage image2 = imageSource as DrawingImage;
            if (image2 != null)
            {
                return fromDrawingImage(image2);
            }
            BitmapSource source = imageSource as BitmapSource;
            return ((source == null) ? fallback() : fromGenericBitmapSource(source));
        }

        public static string TryGetImageUriOriginalString(ImageSource source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            Uri uri = TryGetUri(source);
            return uri?.OriginalString;
        }

        private static byte[] TryGetStreamData(BitmapFrame bitmapFrame)
        {
            BitmapDecoder decoder = bitmapFrame.Decoder;
            FieldInfo decoderField = GetDecoderField(decoder, "_uriStream");
            FieldInfo info3 = GetDecoderField(decoder, "_uri");
            Stream stream = (Stream) GetDecoderField(decoder, "_stream").GetValue(decoder);
            if ((stream == null) || !stream.CanRead)
            {
                stream = (Stream) decoderField.GetValue(decoder);
            }
            return (((stream == null) || !stream.CanRead) ? null : TryCopyAllBytes(stream));
        }

        private static byte[] TryGetStreamData(BitmapImage bitmapImage)
        {
            Stream streamSource = bitmapImage.StreamSource;
            return (((streamSource == null) || !streamSource.CanRead) ? null : TryCopyAllBytes(streamSource));
        }

        private static byte[] TryGetStreamData(DrawingImage drawingImage, Size? drawingImageSize)
        {
            double a = (drawingImageSize == null) ? drawingImage.Width : drawingImageSize.Value.Width;
            double num2 = (drawingImageSize == null) ? drawingImage.Height : drawingImageSize.Value.Height;
            RenderTargetBitmap bitmapSource = new RenderTargetBitmap((int) Math.Ceiling(a), (int) Math.Ceiling(num2), 96.0, 96.0, PixelFormats.Pbgra32);
            DrawingVisual visual = new DrawingVisual();
            using (DrawingContext context = visual.RenderOpen())
            {
                Point location = new Point();
                context.DrawImage(drawingImage, new Rect(location, new Size(a, num2)));
            }
            bitmapSource.Render(visual);
            return SaveAsPng(bitmapSource);
        }

        private static byte[] TryGetStreamData(ImageSource imageSource, Size? drawingImageSize)
        {
            Func<byte[]> fallback = <>c.<>9__11_1;
            if (<>c.<>9__11_1 == null)
            {
                Func<byte[]> local1 = <>c.<>9__11_1;
                fallback = <>c.<>9__11_1 = (Func<byte[]>) (() => null);
            }
            return TryGetImageData<byte[]>(imageSource, new Func<BitmapImage, byte[]>(ImageLoader2.TryGetStreamData), new Func<BitmapFrame, byte[]>(ImageLoader2.TryGetStreamData), x => TryGetStreamData(x, drawingImageSize), new Func<BitmapSource, byte[]>(ImageLoader2.SaveAsPng), fallback);
        }

        private static Uri TryGetUri(ImageSource imageSource)
        {
            Func<DrawingImage, Uri> fromDrawingImage = <>c.<>9__10_0;
            if (<>c.<>9__10_0 == null)
            {
                Func<DrawingImage, Uri> local1 = <>c.<>9__10_0;
                fromDrawingImage = <>c.<>9__10_0 = (Func<DrawingImage, Uri>) (_ => null);
            }
            return TryGetImageData<Uri>(imageSource, new Func<BitmapImage, Uri>(ImageLoader2.TryGetUri), new Func<BitmapFrame, Uri>(ImageLoader2.TryGetUri), fromDrawingImage, <>c.<>9__10_1 ??= ((Func<BitmapSource, Uri>) (_ => null)), <>c.<>9__10_2 ??= ((Func<Uri>) (() => null)));
        }

        private static Uri TryGetUri(BitmapFrame bitmapFrame)
        {
            BitmapDecoder decoder = bitmapFrame.Decoder;
            Uri relativeUri = (Uri) GetDecoderField(decoder, "_uri").GetValue(decoder);
            if ((relativeUri != null) && (bitmapFrame.BaseUri != null))
            {
                relativeUri = new Uri(bitmapFrame.BaseUri, relativeUri);
            }
            return relativeUri;
        }

        private static Uri TryGetUri(BitmapImage bitmapImage) => 
            bitmapImage.UriSource;

        private static MethodInfo CreateRequestAndGetResponseStreamMethod
        {
            get
            {
                if (createRequestAndGetResponseStreamMethod == null)
                {
                    Type[] types = new Type[] { typeof(Uri) };
                    createRequestAndGetResponseStreamMethod = WpfWebRequestHelper.GetMethod("CreateRequestAndGetResponseStream", BindingFlags.NonPublic | BindingFlags.Static, null, types, null);
                }
                return createRequestAndGetResponseStreamMethod;
            }
        }

        private static Type WpfWebRequestHelper
        {
            get
            {
                if (wpfWebRequestHelper == null)
                {
                    wpfWebRequestHelper = typeof(ImageSource).Assembly.GetType("MS.Internal.WpfWebRequestHelper");
                }
                return wpfWebRequestHelper;
            }
        }

        private static MethodInfo MapUrlToZoneWrapperMethod
        {
            get
            {
                if (mapUrlToZoneWrapperMethod == null)
                {
                    Type[] types = new Type[] { typeof(Uri) };
                    mapUrlToZoneWrapperMethod = SecurityHelper.GetMethod("MapUrlToZoneWrapper", BindingFlags.NonPublic | BindingFlags.Static, null, types, null);
                }
                return mapUrlToZoneWrapperMethod;
            }
        }

        private static Type SecurityHelper
        {
            get
            {
                if (securityHelper == null)
                {
                    securityHelper = typeof(ImageSource).Assembly.GetType("MS.Internal.SecurityHelper");
                }
                return securityHelper;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ImageLoader2.<>c <>9 = new ImageLoader2.<>c();
            public static Func<DrawingImage, Uri> <>9__10_0;
            public static Func<BitmapSource, Uri> <>9__10_1;
            public static Func<Uri> <>9__10_2;
            public static Func<byte[]> <>9__11_1;
            public static Func<KeyValuePair<Uri, WeakReference>, bool> <>9__30_0;
            public static Func<KeyValuePair<Uri, WeakReference>, Uri> <>9__30_1;

            internal bool <AddToCache>b__30_0(KeyValuePair<Uri, WeakReference> r) => 
                (r.Value != null) && (r.Value.Target == null);

            internal Uri <AddToCache>b__30_1(KeyValuePair<Uri, WeakReference> r) => 
                r.Key;

            internal byte[] <TryGetStreamData>b__11_1() => 
                null;

            internal Uri <TryGetUri>b__10_0(DrawingImage _) => 
                null;

            internal Uri <TryGetUri>b__10_1(BitmapSource _) => 
                null;

            internal Uri <TryGetUri>b__10_2() => 
                null;
        }
    }
}

