namespace DevExpress.Utils.Serializing
{
    using DevExpress.Utils.Serializing.Helpers;
    using System;
    using System.Collections;
    using System.IO;

    public class IndependentPagesPrintingSystemSerializer : PrintingSystemXmlSerializer
    {
        protected override IXtraPropertyCollection DeserializeCore(Stream stream, string appName, IList objects) => 
            new IndependentPagesDeserializationVirtualXtraPropertyCollection(stream, objects);

        protected override bool SerializeCore(Stream stream, IXtraPropertyCollection props, string appName)
        {
            DeflateStreamsArchiveWriter writer = new DeflateStreamsArchiveWriter(props.Count, stream);
            foreach (XtraPropertyInfo info in props)
            {
                using (Stream stream2 = writer.GetNextStream())
                {
                    new PrintingSystemXmlSerializer().SerializeObject(stream2, info);
                }
            }
            writer.Close();
            return true;
        }
    }
}

