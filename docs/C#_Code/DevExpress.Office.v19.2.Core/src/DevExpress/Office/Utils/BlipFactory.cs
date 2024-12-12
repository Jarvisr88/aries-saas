namespace DevExpress.Office.Utils
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    public static class BlipFactory
    {
        private static Dictionary<int, ConstructorInfo> typeCodeTranslationTable = new Dictionary<int, ConstructorInfo>(9);
        private static Dictionary<OfficeImageFormat, ConstructorInfo> imageFormatTranslationTable;

        static BlipFactory()
        {
            Type[] parameters = new Type[] { typeof(BinaryReader), typeof(OfficeArtRecordHeader) };
            typeCodeTranslationTable.Add(0xf01a, GetConstructor(typeof(BlipEmf), parameters));
            Type[] typeArray2 = new Type[] { typeof(BinaryReader), typeof(OfficeArtRecordHeader) };
            typeCodeTranslationTable.Add(0xf01b, GetConstructor(typeof(BlipWmf), typeArray2));
            Type[] typeArray3 = new Type[] { typeof(BinaryReader), typeof(OfficeArtRecordHeader) };
            typeCodeTranslationTable.Add(0xf01c, GetConstructor(typeof(BlipPict), typeArray3));
            Type[] typeArray4 = new Type[] { typeof(BinaryReader), typeof(OfficeArtRecordHeader) };
            typeCodeTranslationTable.Add(0xf01d, GetConstructor(typeof(BlipJpeg), typeArray4));
            Type[] typeArray5 = new Type[] { typeof(BinaryReader), typeof(OfficeArtRecordHeader) };
            typeCodeTranslationTable.Add(0xf01e, GetConstructor(typeof(BlipPng), typeArray5));
            Type[] typeArray6 = new Type[] { typeof(BinaryReader), typeof(OfficeArtRecordHeader) };
            typeCodeTranslationTable.Add(0xf01f, GetConstructor(typeof(BlipDib), typeArray6));
            Type[] typeArray7 = new Type[] { typeof(BinaryReader), typeof(OfficeArtRecordHeader) };
            typeCodeTranslationTable.Add(0xf029, GetConstructor(typeof(BlipTiff), typeArray7));
            Type[] typeArray8 = new Type[] { typeof(BinaryReader), typeof(OfficeArtRecordHeader) };
            typeCodeTranslationTable.Add(0xf02a, GetConstructor(typeof(BlipJpeg), typeArray8));
            imageFormatTranslationTable = new Dictionary<OfficeImageFormat, ConstructorInfo>();
            Type[] typeArray9 = new Type[] { typeof(OfficeImage) };
            imageFormatTranslationTable.Add(OfficeImageFormat.Bmp, GetConstructor(typeof(BlipDib), typeArray9));
            Type[] typeArray10 = new Type[] { typeof(OfficeImage) };
            imageFormatTranslationTable.Add(OfficeImageFormat.MemoryBmp, GetConstructor(typeof(BlipDib), typeArray10));
            Type[] typeArray11 = new Type[] { typeof(OfficeImage) };
            imageFormatTranslationTable.Add(OfficeImageFormat.Emf, GetConstructor(typeof(BlipEmf), typeArray11));
            Type[] typeArray12 = new Type[] { typeof(OfficeImage) };
            imageFormatTranslationTable.Add(OfficeImageFormat.Jpeg, GetConstructor(typeof(BlipJpeg), typeArray12));
            Type[] typeArray13 = new Type[] { typeof(OfficeImage) };
            imageFormatTranslationTable.Add(OfficeImageFormat.Png, GetConstructor(typeof(BlipPng), typeArray13));
            Type[] typeArray14 = new Type[] { typeof(OfficeImage) };
            imageFormatTranslationTable.Add(OfficeImageFormat.Gif, GetConstructor(typeof(BlipPng), typeArray14));
            Type[] typeArray15 = new Type[] { typeof(OfficeImage) };
            imageFormatTranslationTable.Add(OfficeImageFormat.Tiff, GetConstructor(typeof(BlipTiff), typeArray15));
            Type[] typeArray16 = new Type[] { typeof(OfficeImage) };
            imageFormatTranslationTable.Add(OfficeImageFormat.Wmf, GetConstructor(typeof(BlipWmf), typeArray16));
        }

        private static bool CompareParameterTypes(ParameterInfo[] ciParameters, Type[] parameters)
        {
            if (ciParameters.Length != parameters.Length)
            {
                return false;
            }
            int length = ciParameters.Length;
            for (int i = 0; i < length; i++)
            {
                if (ciParameters[i].ParameterType != parameters[i])
                {
                    return false;
                }
            }
            return true;
        }

        public static BlipBase CreateBlipFromImage(OfficeImage image)
        {
            ConstructorInfo info;
            if (!imageFormatTranslationTable.TryGetValue(image.RawFormat, out info))
            {
                return new BlipDib(image);
            }
            object[] parameters = new object[] { image };
            return (info.Invoke(parameters) as BlipBase);
        }

        public static BlipBase CreateBlipFromStream(BinaryReader reader, OfficeArtRecordHeader header) => 
            CreateBlipFromStreamCore(reader, reader, header);

        public static BlipBase CreateBlipFromStream(BinaryReader reader, BinaryReader embeddedReader, OfficeArtRecordHeader header) => 
            CreateBlipFromStreamCore(reader, embeddedReader, header);

        private static BlipBase CreateBlipFromStreamCore(BinaryReader reader, BinaryReader embeddedReader, OfficeArtRecordHeader header)
        {
            ConstructorInfo info;
            int typeCode = header.TypeCode;
            if (typeCode == 0xf007)
            {
                return new FileBlipStoreEntry(reader, embeddedReader, header);
            }
            if (!typeCodeTranslationTable.TryGetValue(typeCode, out info))
            {
                return null;
            }
            object[] parameters = new object[] { reader, header };
            return (info.Invoke(parameters) as BlipBase);
        }

        public static ConstructorInfo GetConstructor(Type type, Type[] parameters) => 
            type.GetConstructor(parameters);

        public static List<BlipBase> ReadAllBlips(BinaryReader reader, long endPosition) => 
            ReadAllBlips(reader, reader, endPosition);

        public static List<BlipBase> ReadAllBlips(BinaryReader reader, BinaryReader embeddedReader, long endPosition)
        {
            List<BlipBase> list = new List<BlipBase>();
            while (reader.BaseStream.Position < endPosition)
            {
                OfficeArtRecordHeader header = OfficeArtRecordHeader.FromStream(reader);
                long num = reader.BaseStream.Position + header.Length;
                BlipBase item = CreateBlipFromStream(reader, embeddedReader, header);
                if (item != null)
                {
                    list.Add(item);
                }
                reader.BaseStream.Position = Math.Max(num, reader.BaseStream.Position);
            }
            return list;
        }
    }
}

