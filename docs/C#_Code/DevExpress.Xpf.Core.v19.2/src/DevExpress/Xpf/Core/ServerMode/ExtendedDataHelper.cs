namespace DevExpress.Xpf.Core.ServerMode
{
    using DevExpress.Data;
    using DevExpress.Data.Helpers;
    using DevExpress.Data.Linq;
    using System;
    using System.IO;
    using System.IO.Compression;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    public static class ExtendedDataHelper
    {
        public const string ExtendedQuerySuffix = "ExtendedData";
        public const string ExtendedParameterName = "extendedDataInfo";

        public static T Deserialize<T>(string sourceXml)
        {
            T local;
            if (string.IsNullOrEmpty(sourceXml))
            {
                return default(T);
            }
            byte[] buffer = Convert.FromBase64String(sourceXml.Replace('-', '+').Replace('_', '/'));
            using (MemoryStream stream = new MemoryStream(buffer))
            {
                using (MemoryStream stream2 = new MemoryStream(buffer.Length * 10))
                {
                    using (DeflateStream stream3 = new DeflateStream(stream, CompressionMode.Decompress, true))
                    {
                        stream3.CopyTo(stream2);
                    }
                    byte[] bytes = stream2.ToArray();
                    using (StringReader reader = new StringReader(Encoding.UTF8.GetString(bytes, 0, bytes.Length)))
                    {
                        local = (T) new XmlSerializer(typeof(T)).Deserialize(reader);
                    }
                }
            }
            return local;
        }

        public static string GetExtendedData(IQueryable queryable, string extendedDataInfo) => 
            GetExtendedData(queryable, new CriteriaToExpressionConverter(), extendedDataInfo);

        public static string GetExtendedData(IQueryable queryable, ICriteriaToExpressionConverter converter, string extendedDataInfo)
        {
            ExtendedDataParametersContainer container = Deserialize<ExtendedDataParametersContainer>(extendedDataInfo);
            ExtendedDataResultContainer container2 = null;
            switch (container.OperationType)
            {
                case ExtendedOperationType.FetchKeys:
                    container2 = new ExtendedDataResultContainer(ExtendedOperationType.FetchKeys, ServerModeCoreExtender.FetchKeys(queryable, converter, container.KeysCriteria, container.Where, UnwrapOrder(container.Order), container.Skip, container.Take));
                    break;

                case ExtendedOperationType.GetCount:
                    container2 = new ExtendedDataResultContainer(ServerModeCoreExtender.GetCount(queryable, converter, container.Where));
                    break;

                case ExtendedOperationType.GetUniqueValues:
                    container2 = new ExtendedDataResultContainer(ExtendedOperationType.GetUniqueValues, ServerModeCoreExtender.GetUniqueValues(queryable, converter, container.Expression, container.MaxCount, container.Where));
                    break;

                case ExtendedOperationType.PrepareChildren:
                    container2 = new ExtendedDataResultContainer(ServerModeCoreExtender.PrepareChildren(queryable, converter, container.GroupWhere, container.GroupByDescriptor.ToServerModeOrderDescriptor(), UnwrapSummaries(container.Summaries)));
                    break;

                case ExtendedOperationType.PrepareTopGroupInfo:
                    container2 = new ExtendedDataResultContainer(ServerModeCoreExtender.PrepareTopGroupInfo(queryable, converter, container.Where, UnwrapSummaries(container.Summaries)));
                    break;

                default:
                    throw new NotImplementedException(container.OperationType.ToString() + " is not implemented");
            }
            return Serialize<ExtendedDataResultContainer>(container2);
        }

        public static string Serialize<T>(object obj)
        {
            string str;
            if (obj == null)
            {
                return string.Empty;
            }
            using (StringWriter writer = new StringWriter())
            {
                new XmlSerializer(typeof(T)).Serialize((TextWriter) writer, obj);
                byte[] bytes = Encoding.UTF8.GetBytes(writer.ToString());
                using (MemoryStream stream = new MemoryStream(bytes))
                {
                    using (MemoryStream stream2 = new MemoryStream(bytes.Length * 10))
                    {
                        using (DeflateStream stream3 = new DeflateStream(stream2, CompressionMode.Compress, true))
                        {
                            stream.CopyTo(stream3);
                        }
                        str = Convert.ToBase64String(stream2.ToArray()).Replace('+', '-').Replace('/', '_');
                    }
                }
            }
            return str;
        }

        internal static ServerModeGroupInfoData[] UnwrapGroupInfoData(ServerModeGroupInfoDataSerializationWrapper[] wrappers)
        {
            ServerModeGroupInfoData[] dataArray = new ServerModeGroupInfoData[wrappers.Length];
            for (int i = 0; i < wrappers.Length; i++)
            {
                dataArray[i] = wrappers[i].ToServerModeGroupInfoData();
            }
            return dataArray;
        }

        internal static ServerModeOrderDescriptor[] UnwrapOrder(ServerModeOrderDescriptorSerializationWrapper[] wrappers)
        {
            ServerModeOrderDescriptor[] descriptorArray = new ServerModeOrderDescriptor[wrappers.Length];
            for (int i = 0; i < wrappers.Length; i++)
            {
                descriptorArray[i] = wrappers[i].ToServerModeOrderDescriptor();
            }
            return descriptorArray;
        }

        internal static ServerModeSummaryDescriptor[] UnwrapSummaries(ServerModeSummaryDescriptorSerializationWrapper[] wrappers)
        {
            ServerModeSummaryDescriptor[] descriptorArray = new ServerModeSummaryDescriptor[wrappers.Length];
            for (int i = 0; i < wrappers.Length; i++)
            {
                descriptorArray[i] = wrappers[i].ToServerModeSummaryDescriptor();
            }
            return descriptorArray;
        }

        internal static ServerModeGroupInfoDataSerializationWrapper[] WrapGroupInfoData(ServerModeGroupInfoData[] groupInfoData)
        {
            ServerModeGroupInfoDataSerializationWrapper[] wrapperArray = new ServerModeGroupInfoDataSerializationWrapper[groupInfoData.Length];
            for (int i = 0; i < groupInfoData.Length; i++)
            {
                wrapperArray[i] = new ServerModeGroupInfoDataSerializationWrapper(groupInfoData[i]);
            }
            return wrapperArray;
        }

        internal static ServerModeOrderDescriptorSerializationWrapper[] WrapOrder(ServerModeOrderDescriptor[] orderDescriptors)
        {
            ServerModeOrderDescriptorSerializationWrapper[] wrapperArray = new ServerModeOrderDescriptorSerializationWrapper[orderDescriptors.Length];
            for (int i = 0; i < orderDescriptors.Length; i++)
            {
                wrapperArray[i] = new ServerModeOrderDescriptorSerializationWrapper(orderDescriptors[i]);
            }
            return wrapperArray;
        }

        internal static ServerModeSummaryDescriptorSerializationWrapper[] WrapSummaries(ServerModeSummaryDescriptor[] summaryDescriptors)
        {
            ServerModeSummaryDescriptorSerializationWrapper[] wrapperArray = new ServerModeSummaryDescriptorSerializationWrapper[summaryDescriptors.Length];
            for (int i = 0; i < summaryDescriptors.Length; i++)
            {
                wrapperArray[i] = new ServerModeSummaryDescriptorSerializationWrapper(summaryDescriptors[i]);
            }
            return wrapperArray;
        }
    }
}

