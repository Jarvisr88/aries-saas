namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PdfContentStreamParser : PdfDocumentParser
    {
        private static readonly byte[] endImageToken = new byte[] { 0x45, 0x49 };
        private readonly PdfResources resources;
        private readonly PdfStack operands;

        protected PdfContentStreamParser(PdfResources resources, byte[] data) : this((PdfObjectCollection) objects, 0, 0, new PdfArrayDataStream(data), 0)
        {
            object objects;
            this.operands = new PdfStack();
            if (resources == null)
            {
                objects = null;
            }
            else
            {
                PdfDocumentCatalog documentCatalog = resources.DocumentCatalog;
                if (documentCatalog != null)
                {
                    objects = documentCatalog.Objects;
                }
                else
                {
                    PdfDocumentCatalog local1 = documentCatalog;
                    objects = null;
                }
            }
            this.resources = resources;
        }

        private void CheckCommandTermination()
        {
            if (!base.ReadNext() || !this.IsCommandNameTerminate)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
        }

        public static PdfCommandList GetContent(PdfResources resources, byte[] data) => 
            new PdfContentStreamParser(resources, data).Parse();

        protected PdfCommandList Parse()
        {
            PdfCommandList commands = new PdfCommandList();
            try
            {
                this.Parse(null, commands, false);
            }
            catch
            {
            }
            return commands;
        }

        private void Parse(string expectedName, IList<PdfCommand> commands, bool shouldIgnoreUnknownCommands)
        {
            bool flag = this.IsType3FontParser;
            bool flag2 = !string.IsNullOrEmpty(expectedName);
            while (true)
            {
                if (base.SkipSpaces())
                {
                    object obj2 = base.ReadObject(false, false);
                    if (obj2 == null)
                    {
                        continue;
                    }
                    PdfCommand item = obj2 as PdfCommand;
                    if (item != null)
                    {
                        commands.Add(item);
                        continue;
                    }
                    CommandName name = obj2 as CommandName;
                    if (name == null)
                    {
                        this.operands.Push(obj2);
                        continue;
                    }
                    string str = name.Name;
                    if (str != expectedName)
                    {
                        if (flag2 && str.StartsWith(expectedName))
                        {
                            item = PdfCommand.Create(this.resources, str.Substring(expectedName.Length), new PdfStack(), flag, shouldIgnoreUnknownCommands);
                            if (item != null)
                            {
                                commands.Add(item);
                            }
                            return;
                        }
                        item = PdfCommand.Create(this.resources, str, this.operands, flag, shouldIgnoreUnknownCommands);
                        if (item == null)
                        {
                            return;
                        }
                        commands.Add(item);
                        continue;
                    }
                }
                return;
            }
        }

        private PdfPaintImageCommand ParseInlineImage()
        {
            PdfReaderDictionary dictionary = new PdfReaderDictionary(base.Objects, 0, 0);
            while (base.SkipSpaces())
            {
                object obj2 = base.ReadObject(false, false);
                PdfName name = obj2 as PdfName;
                if (name == null)
                {
                    object obj3;
                    int num3;
                    byte[] buffer;
                    bool valueOrDefault;
                    CommandName name2 = obj2 as CommandName;
                    if ((name2 == null) || (name2.Name != "ID"))
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    IList<PdfFilter> filters = dictionary.GetFilters("F", "DP");
                    IList<PdfFilter> list3 = filters;
                    if (filters == null)
                    {
                        IList<PdfFilter> local1 = filters;
                        IList<PdfFilter> list2 = dictionary.GetFilters("Filter", "DecodeParms");
                        list3 = list2;
                        if (list2 == null)
                        {
                            IList<PdfFilter> local2 = list2;
                            list3 = new PdfFilter[0];
                        }
                    }
                    IList<PdfFilter> list = list3;
                    int width = ReadInteger(dictionary, "W", "Width");
                    int height = ReadInteger(dictionary, "H", "Height");
                    PdfColorSpace colorSpace = null;
                    if (dictionary.TryGetValue("CS", out obj3) || dictionary.TryGetValue("ColorSpace", out obj3))
                    {
                        PdfName name3 = obj3 as PdfName;
                        if (name3 != null)
                        {
                            colorSpace = this.resources.GetColorSpace(name3.Name);
                        }
                        colorSpace ??= PdfColorSpace.Parse(null, obj3);
                    }
                    bool? boolean = dictionary.GetBoolean("IM");
                    if (boolean != null)
                    {
                        valueOrDefault = boolean.GetValueOrDefault();
                    }
                    else
                    {
                        bool? nullable2 = dictionary.GetBoolean("ImageMask");
                        valueOrDefault = (nullable2 != null) ? nullable2.GetValueOrDefault() : false;
                    }
                    bool isMask = valueOrDefault;
                    if (!isMask)
                    {
                        num3 = ReadInteger(dictionary, "BPC", "BitsPerComponent");
                    }
                    else
                    {
                        num3 = 1;
                        if ((dictionary.ContainsKey("BPC") || dictionary.ContainsKey("BitsPerComponent")) && (ReadInteger(dictionary, "BPC", "BitsPerComponent") != num3))
                        {
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                        }
                    }
                    if (list.Count != 0)
                    {
                        if (!base.ReadNext())
                        {
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                        }
                        buffer = base.ReadData(endImageToken, false, list[0].EodToken);
                    }
                    else
                    {
                        int num5 = (width * ((colorSpace == null) ? 1 : colorSpace.ComponentsCount)) * num3;
                        int num6 = num5 / 8;
                        if ((num5 % 8) > 0)
                        {
                            num6++;
                        }
                        buffer = base.ReadData(num6 * height, endImageToken, null, false);
                    }
                    return new PdfPaintImageCommand(new PdfImage(width, height, colorSpace, num3, isMask, new PdfArrayCompressedData(list, buffer), dictionary, this.resources));
                }
                if (!base.SkipSpaces())
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                obj2 = base.ReadObject(false, false);
                if ((name == null) || (obj2 == null))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                dictionary.Add(name.Name, obj2);
            }
            PdfDocumentStructureReader.ThrowIncorrectDataException();
            return null;
        }

        protected override object ReadAlphabeticalObject(bool isHexadecimalStringSeparatedUsingWhiteSpaces, bool isIndirect)
        {
            StringBuilder builder;
            PdfCommandGroup group;
            string str;
            object obj2 = base.ReadDictionaryOrStream(isHexadecimalStringSeparatedUsingWhiteSpaces, isIndirect);
            if (obj2 != null)
            {
                return obj2;
            }
            byte current = base.Current;
            if (current != 0x42)
            {
                goto TR_0008;
            }
            else
            {
                if (!base.ReadNext())
                {
                    return new CommandName(new string((char) current, 1));
                }
                group = null;
                str = null;
                current = base.Current;
                if (current > 0x49)
                {
                    if (current != 0x4d)
                    {
                        if (current == 0x54)
                        {
                            if (!base.ReadNext() || !this.IsCommandNameTerminate)
                            {
                                PdfDocumentStructureReader.ThrowIncorrectDataException();
                            }
                            return new CommandName("BT");
                        }
                        if (current == 0x58)
                        {
                            group = new PdfCompatibilityCommandGroup();
                            str = "EX";
                            goto TR_0009;
                        }
                        goto TR_000B;
                    }
                }
                else if (current != 0x44)
                {
                    if (current == 0x49)
                    {
                        this.CheckCommandTermination();
                        return this.ParseInlineImage();
                    }
                    goto TR_000B;
                }
                str = "EMC";
                if (!base.ReadNext() || (base.Current != 0x43))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                group = (current == 0x44) ? PdfMarkedContentCommand.Parse(this.operands, this.resources) : PdfMarkedContentCommand.Parse(this.operands);
                goto TR_0009;
            }
            goto TR_000B;
        TR_0008:
            builder = new StringBuilder();
            while (true)
            {
                if ((builder.Length == 0) || !this.IsCommandNameTerminate)
                {
                    builder.Append((char) base.Current);
                    if (base.ReadNext())
                    {
                        continue;
                    }
                }
                return new CommandName(builder.ToString());
            }
        TR_0009:
            if (group != null)
            {
                this.CheckCommandTermination();
                this.Parse(str, group.Children, group.ShouldIgnoreUnknownCommands);
                return group;
            }
            goto TR_0008;
        TR_000B:
            if (!base.ReadPrev())
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            goto TR_0009;
        }

        private static int ReadInteger(PdfReaderDictionary dictionary, string key, string alternativeKey)
        {
            object obj2 = null;
            if (!dictionary.TryGetValue(key, out obj2) && !dictionary.TryGetValue(alternativeKey, out obj2))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            if (obj2 is int)
            {
                return (int) obj2;
            }
            if (!(obj2 is double))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return PdfMathUtils.ToInt32((double) obj2);
        }

        protected override bool TryReadKnownObject()
        {
            bool flag;
            long currentPosition = base.CurrentPosition;
            try
            {
                object obj2 = base.ReadObject(false, false);
                CommandName name = obj2 as CommandName;
                if (((obj2 == null) || ((name != null) && PdfCommand.IsKnownCommand(name.Name))) || (obj2 is PdfCommand))
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            }
            finally
            {
                base.CurrentPosition = currentPosition;
            }
            return flag;
        }

        private bool IsCommandNameTerminate
        {
            get
            {
                byte current = base.Current;
                return (base.IsSpace || ((current == 60) || ((current == 0x5b) || ((current == 0x5d) || ((current == 40) || ((current == 0x2f) || ((current == 0x25) || (current == 0x3e))))))));
            }
        }

        protected virtual bool IsType3FontParser =>
            false;

        private class CommandName
        {
            private readonly string name;

            public CommandName(string name)
            {
                this.name = name;
            }

            public string Name =>
                this.name;
        }
    }
}

