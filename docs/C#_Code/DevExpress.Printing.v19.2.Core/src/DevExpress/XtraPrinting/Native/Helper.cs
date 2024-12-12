namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.DocumentServices.ServiceModel;
    using DevExpress.DocumentServices.ServiceModel.DataContracts;
    using DevExpress.DocumentServices.ServiceModel.Native;
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Drawing;
    using DevExpress.XtraReports.Parameters;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Compression;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;
    using System.Xml;

    public static class Helper
    {
        public static TimeSpan DefaultStatusUpdateInterval = TimeSpan.FromMilliseconds(500.0);
        private static volatile DataContractSerializer pagesContractSerializer;
        private static readonly object pagesContractSerializerLock = new object();
        private static XtraSerializer serializer;
        private static Dictionary<ExportFormat, Type> exportOptionTypesByFormat;

        public static DocumentExportArgs CreateDocumentExportArgs(ExportFormat format, DevExpress.XtraPrinting.ExportOptions exportOptions, object customArgs)
        {
            DocumentExportArgs args1 = new DocumentExportArgs();
            args1.Format = format;
            args1.SerializedExportOptions = (exportOptions != null) ? SerializeExportOptions(exportOptions.GetByFormat(format)) : null;
            DocumentExportArgs local1 = args1;
            local1.CustomArgs = customArgs;
            return local1;
        }

        public static ReportBuildArgs CreateReportBuildArgs(IParameterContainer parameters, XtraPageSettingsBase pageSettings, Watermark watermark, object customArgs) => 
            CreateReportBuildArgs(parameters.ToParameterStubs(), pageSettings, watermark, customArgs);

        public static ReportBuildArgs CreateReportBuildArgs(IList<Parameter> parameters, XtraPageSettingsBase pageSettings, Watermark watermark, object customArgs)
        {
            Func<Parameter, ReportParameter> selector = <>c.<>9__24_0;
            if (<>c.<>9__24_0 == null)
            {
                Func<Parameter, ReportParameter> local1 = <>c.<>9__24_0;
                selector = <>c.<>9__24_0 = delegate (Parameter x) {
                    ReportParameter parameter1 = new ReportParameter();
                    parameter1.Description = x.Description;
                    parameter1.Name = x.Name;
                    parameter1.Value = x.Value;
                    parameter1.Visible = x.Visible;
                    return parameter1;
                };
            }
            return CreateReportBuildArgs(parameters.Select<Parameter, ReportParameter>(selector).ToArray<ReportParameter>(), pageSettings, watermark, customArgs);
        }

        private static ReportBuildArgs CreateReportBuildArgs(ReportParameter[] parameterDtoArray, XtraPageSettingsBase pageSettings, Watermark watermark, object customArgs)
        {
            ReportBuildArgs args1 = new ReportBuildArgs();
            args1.Parameters = parameterDtoArray;
            args1.SerializedPageData = SerializePageSettings(pageSettings);
            args1.SerializedWatermark = SerializeWatermark(watermark);
            args1.CustomArgs = customArgs;
            return args1;
        }

        public static void DeserializeExportOptions(DevExpress.XtraPrinting.ExportOptions instance, byte[] serializedData)
        {
            using (MemoryStream stream = new MemoryStream(serializedData))
            {
                instance.RestoreFromStream(stream);
            }
        }

        public static string[] DeserializePages(byte[] bytes)
        {
            string[] strArray;
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                using (GZipStream stream2 = new GZipStream(stream, CompressionMode.Decompress))
                {
                    using (XmlDictionaryReader reader = XmlDictionaryReader.CreateBinaryReader(stream2, XmlDictionaryReaderQuotas.Max))
                    {
                        strArray = (string[]) PagesContractSerializer.ReadObject(reader);
                    }
                }
            }
            return strArray;
        }

        public static void DeserializePageSettings(XtraPageSettingsBase instance, byte[] serializedData)
        {
            if (serializedData != null)
            {
                PageData data = new PageData();
                using (MemoryStream stream = new MemoryStream(serializedData))
                {
                    Serializer.DeserializeObject(data, (Stream) stream, typeof(PageData).Name);
                }
                instance.Assign(data);
            }
        }

        public static IEnumerable<ExportOptionKind> FlagsToList(this ExportOptionKind kinds) => 
            from x in GetEnumValues<ExportOptionKind>()
                where kinds.HasFlag(x)
                select x;

        public static ExportOptionsBase GetByFormat(this DevExpress.XtraPrinting.ExportOptions options, ExportFormat format)
        {
            Type type;
            if (!ExportOptionTypesByFormat.TryGetValue(format, out type))
            {
                throw new ArgumentException("format");
            }
            return options.Options[type];
        }

        private static IEnumerable<T> GetEnumValues<T>() where T: struct
        {
            Type type = typeof(T);
            if (!type.IsEnum)
            {
                throw new ArgumentException($"Type '{type.Name}' is not an enum");
            }
            Func<FieldInfo, bool> predicate = <>c__22<T>.<>9__22_0;
            if (<>c__22<T>.<>9__22_0 == null)
            {
                Func<FieldInfo, bool> local1 = <>c__22<T>.<>9__22_0;
                predicate = <>c__22<T>.<>9__22_0 = x => x.IsLiteral;
            }
            return (from x in type.GetFields().Where<FieldInfo>(predicate) select (T) x.GetValue(type));
        }

        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue defaultValue)
        {
            TValue local;
            return (!dict.TryGetValue(key, out local) ? defaultValue : local);
        }

        public static byte[] SerializeExportOptions(ExportOptionsBase exportOptions) => 
            SerializeObject(exportOptions, typeof(DevExpress.XtraPrinting.ExportOptions).Name);

        private static byte[] SerializeObject(object obj, string name)
        {
            if (obj == null)
            {
                return null;
            }
            using (MemoryStream stream = new MemoryStream())
            {
                Serializer.SerializeObject(obj, (Stream) stream, name);
                return stream.ToArray();
            }
        }

        public static byte[] SerializePageSettings(XtraPageSettingsBase instance) => 
            (instance != null) ? SerializeObject(new ReadonlyPageData(instance.Data), typeof(PageData).Name) : null;

        private static byte[] SerializeWatermark(Watermark watermark)
        {
            if (watermark == null)
            {
                return null;
            }
            using (MemoryStream stream = new MemoryStream())
            {
                watermark.SaveToStream(stream);
                return stream.ToArray();
            }
        }

        public static Exception ToException(this ServiceFault fault) => 
            !string.IsNullOrEmpty(fault.FullMessage) ? new Exception(fault.Message, new Exception(fault.FullMessage)) : new Exception(fault.Message);

        public static ReportParameter[] ToParameterStubs(this IParameterContainer parameters)
        {
            ClientParameterContainer container = parameters as ClientParameterContainer;
            if (container == null)
            {
                return new ReportParameter[0];
            }
            Func<ClientParameter, ReportParameter> selector = <>c.<>9__12_0;
            if (<>c.<>9__12_0 == null)
            {
                Func<ClientParameter, ReportParameter> local1 = <>c.<>9__12_0;
                selector = <>c.<>9__12_0 = delegate (ClientParameter p) {
                    ReportParameter parameter1 = new ReportParameter();
                    parameter1.Name = p.Name;
                    parameter1.Path = p.Path;
                    parameter1.Value = p.Value;
                    parameter1.Visible = p.Visible;
                    parameter1.Description = p.Description;
                    parameter1.IsFilteredLookUpSettings = p.IsFilteredLookUpSettings;
                    return parameter1;
                };
            }
            return container.ClientParameters.Select<ClientParameter, ReportParameter>(selector).ToArray<ReportParameter>();
        }

        private static DataContractSerializer PagesContractSerializer
        {
            get
            {
                if (pagesContractSerializer == null)
                {
                    object pagesContractSerializerLock = Helper.pagesContractSerializerLock;
                    lock (pagesContractSerializerLock)
                    {
                        pagesContractSerializer ??= new DataContractSerializer(typeof(string[]));
                    }
                }
                return pagesContractSerializer;
            }
        }

        private static Dictionary<ExportFormat, Type> ExportOptionTypesByFormat
        {
            get
            {
                if (exportOptionTypesByFormat == null)
                {
                    Dictionary<ExportFormat, Type> dictionary = new Dictionary<ExportFormat, Type> {
                        { 
                            ExportFormat.Pdf,
                            typeof(PdfExportOptions)
                        },
                        { 
                            ExportFormat.Htm,
                            typeof(HtmlExportOptions)
                        },
                        { 
                            ExportFormat.Mht,
                            typeof(MhtExportOptions)
                        },
                        { 
                            ExportFormat.Rtf,
                            typeof(RtfExportOptions)
                        },
                        { 
                            ExportFormat.Docx,
                            typeof(DocxExportOptions)
                        },
                        { 
                            ExportFormat.Xls,
                            typeof(XlsExportOptions)
                        },
                        { 
                            ExportFormat.Xlsx,
                            typeof(XlsxExportOptions)
                        },
                        { 
                            ExportFormat.Csv,
                            typeof(CsvExportOptions)
                        },
                        { 
                            ExportFormat.Txt,
                            typeof(TextExportOptions)
                        },
                        { 
                            ExportFormat.Image,
                            typeof(ImageExportOptions)
                        },
                        { 
                            ExportFormat.Xps,
                            typeof(XpsExportOptions)
                        },
                        { 
                            ExportFormat.Prnx,
                            typeof(NativeFormatOptions)
                        }
                    };
                    exportOptionTypesByFormat = dictionary;
                }
                return exportOptionTypesByFormat;
            }
        }

        internal static XtraSerializer Serializer
        {
            get
            {
                serializer ??= new XmlXtraSerializer();
                return serializer;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly Helper.<>c <>9 = new Helper.<>c();
            public static Func<ClientParameter, ReportParameter> <>9__12_0;
            public static Func<Parameter, ReportParameter> <>9__24_0;

            internal ReportParameter <CreateReportBuildArgs>b__24_0(Parameter x)
            {
                ReportParameter parameter1 = new ReportParameter();
                parameter1.Description = x.Description;
                parameter1.Name = x.Name;
                parameter1.Value = x.Value;
                parameter1.Visible = x.Visible;
                return parameter1;
            }

            internal ReportParameter <ToParameterStubs>b__12_0(ClientParameter p)
            {
                ReportParameter parameter1 = new ReportParameter();
                parameter1.Name = p.Name;
                parameter1.Path = p.Path;
                parameter1.Value = p.Value;
                parameter1.Visible = p.Visible;
                parameter1.Description = p.Description;
                parameter1.IsFilteredLookUpSettings = p.IsFilteredLookUpSettings;
                return parameter1;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c__22<T> where T: struct
        {
            public static readonly Helper.<>c__22<T> <>9;
            public static Func<FieldInfo, bool> <>9__22_0;

            static <>c__22()
            {
                Helper.<>c__22<T>.<>9 = new Helper.<>c__22<T>();
            }

            internal bool <GetEnumValues>b__22_0(FieldInfo x) => 
                x.IsLiteral;
        }
    }
}

