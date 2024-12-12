namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfICCBasedColorSpace : PdfCustomColorSpace
    {
        internal const string TypeName = "ICCBased";
        private const string componentsCountDictionaryKey = "N";
        private const string alternateDictionaryKey = "Alternate";
        private const string rangeDictionaryKey = "Range";
        private readonly int componentsCount;
        private readonly PdfColorSpace alternate;
        private readonly IList<PdfRange> range;
        private readonly PdfMetadata metadata;
        private readonly byte[] data;

        internal PdfICCBasedColorSpace()
        {
            this.componentsCount = 3;
            this.alternate = this.CreateAlternateColorSpace();
            this.range = this.CreateRange();
            this.data = Convert.FromBase64String("\r\n                AAAMSExpbm8CEAAAbW50clJHQiBYWVogB84AAgAJAAYAMQAAYWNzcE1TRlQAAAAASUVDIHNSR0IAAAAAAAAAAAAAAAAAAPb\r\n                WAAEAAAAA0y1IUCAgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAARY3BydAAAAVAAAA\r\n                AzZGVzYwAAAYQAAABsd3RwdAAAAfAAAAAUYmtwdAAAAgQAAAAUclhZWgAAAhgAAAAUZ1hZWgAAAiwAAAAUYlhZWgAAAkAAA\r\n                AAUZG1uZAAAAlQAAABwZG1kZAAAAsQAAACIdnVlZAAAA0wAAACGdmlldwAAA9QAAAAkbHVtaQAAA/gAAAAUbWVhcwAABAwA\r\n                AAAkdGVjaAAABDAAAAAMclRSQwAABDwAAAgMZ1RSQwAABDwAAAgMYlRSQwAABDwAAAgMdGV4dAAAAABDb3B5cmlnaHQgKGM\r\n                pIDE5OTggSGV3bGV0dC1QYWNrYXJkIENvbXBhbnkAAGRlc2MAAAAAAAAAEnNSR0IgSUVDNjE5NjYtMi4xAAAAAAAAAAAAAA\r\n                ASc1JHQiBJRUM2MTk2Ni0yLjEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAFhZW\r\n                iAAAAAAAADzUQABAAAAARbMWFlaIAAAAAAAAAAAAAAAAAAAAABYWVogAAAAAAAAb6IAADj1AAADkFhZWiAAAAAAAABimQAA\r\n                t4UAABjaWFlaIAAAAAAAACSgAAAPhAAAts9kZXNjAAAAAAAAABZJRUMgaHR0cDovL3d3dy5pZWMuY2gAAAAAAAAAAAAAABZ\r\n                JRUMgaHR0cDovL3d3dy5pZWMuY2gAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAZGVzYw\r\n                AAAAAAAAAuSUVDIDYxOTY2LTIuMSBEZWZhdWx0IFJHQiBjb2xvdXIgc3BhY2UgLSBzUkdCAAAAAAAAAAAAAAAuSUVDIDYxO\r\n                TY2LTIuMSBEZWZhdWx0IFJHQiBjb2xvdXIgc3BhY2UgLSBzUkdCAAAAAAAAAAAAAAAAAAAAAAAAAAAAAGRlc2MAAAAAAAAA\r\n                LFJlZmVyZW5jZSBWaWV3aW5nIENvbmRpdGlvbiBpbiBJRUM2MTk2Ni0yLjEAAAAAAAAAAAAAACxSZWZlcmVuY2UgVmlld2l\r\n                uZyBDb25kaXRpb24gaW4gSUVDNjE5NjYtMi4xAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAB2aWV3AAAAAAATpP4AFF8uAB\r\n                DPFAAD7cwABBMLAANcngAAAAFYWVogAAAAAABMCVYAUAAAAFcf521lYXMAAAAAAAAAAQAAAAAAAAAAAAAAAAAAAAAAAAKPA\r\n                AAAAnNpZyAAAAAAQ1JUIGN1cnYAAAAAAAAEAAAAAAUACgAPABQAGQAeACMAKAAtADIANwA7AEAARQBKAE8AVABZAF4AYwBo\r\n                AG0AcgB3AHwAgQCGAIsAkACVAJoAnwCkAKkArgCyALcAvADBAMYAywDQANUA2wDgAOUA6wDwAPYA+wEBAQcBDQETARkBHwE\r\n                lASsBMgE4AT4BRQFMAVIBWQFgAWcBbgF1AXwBgwGLAZIBmgGhAakBsQG5AcEByQHRAdkB4QHpAfIB+gIDAgwCFAIdAiYCLw\r\n                I4AkECSwJUAl0CZwJxAnoChAKOApgCogKsArYCwQLLAtUC4ALrAvUDAAMLAxYDIQMtAzgDQwNPA1oDZgNyA34DigOWA6IDr\r\n                gO6A8cD0wPgA+wD+QQGBBMEIAQtBDsESARVBGMEcQR+BIwEmgSoBLYExATTBOEE8AT+BQ0FHAUrBToFSQVYBWcFdwWGBZYF\r\n                pgW1BcUF1QXlBfYGBgYWBicGNwZIBlkGagZ7BowGnQavBsAG0QbjBvUHBwcZBysHPQdPB2EHdAeGB5kHrAe/B9IH5Qf4CAs\r\n                IHwgyCEYIWghuCIIIlgiqCL4I0gjnCPsJEAklCToJTwlkCXkJjwmkCboJzwnlCfsKEQonCj0KVApqCoEKmAquCsUK3ArzCw\r\n                sLIgs5C1ELaQuAC5gLsAvIC+EL+QwSDCoMQwxcDHUMjgynDMAM2QzzDQ0NJg1ADVoNdA2ODakNww3eDfgOEw4uDkkOZA5/D\r\n                psOtg7SDu4PCQ8lD0EPXg96D5YPsw/PD+wQCRAmEEMQYRB+EJsQuRDXEPURExExEU8RbRGMEaoRyRHoEgcSJhJFEmQShBKj\r\n                EsMS4xMDEyMTQxNjE4MTpBPFE+UUBhQnFEkUahSLFK0UzhTwFRIVNBVWFXgVmxW9FeAWAxYmFkkWbBaPFrIW1hb6Fx0XQRd\r\n                lF4kXrhfSF/cYGxhAGGUYihivGNUY+hkgGUUZaxmRGbcZ3RoEGioaURp3Gp4axRrsGxQbOxtjG4obshvaHAIcKhxSHHscox\r\n                zMHPUdHh1HHXAdmR3DHeweFh5AHmoelB6+HukfEx8+H2kflB+/H+ogFSBBIGwgmCDEIPAhHCFIIXUhoSHOIfsiJyJVIoIir\r\n                yLdIwojOCNmI5QjwiPwJB8kTSR8JKsk2iUJJTglaCWXJccl9yYnJlcmhya3JugnGCdJJ3onqyfcKA0oPyhxKKIo1CkGKTgp\r\n                aymdKdAqAio1KmgqmyrPKwIrNitpK50r0SwFLDksbiyiLNctDC1BLXYtqy3hLhYuTC6CLrcu7i8kL1ovkS/HL/4wNTBsMKQ\r\n                w2zESMUoxgjG6MfIyKjJjMpsy1DMNM0YzfzO4M/E0KzRlNJ402DUTNU01hzXCNf02NzZyNq426TckN2A3nDfXOBQ4UDiMOM\r\n                g5BTlCOX85vDn5OjY6dDqyOu87LTtrO6o76DwnPGU8pDzjPSI9YT2hPeA+ID5gPqA+4D8hP2E/oj/iQCNAZECmQOdBKUFqQ\r\n                axB7kIwQnJCtUL3QzpDfUPARANER0SKRM5FEkVVRZpF3kYiRmdGq0bwRzVHe0fASAVIS0iRSNdJHUljSalJ8Eo3Sn1KxEsM\r\n                S1NLmkviTCpMcky6TQJNSk2TTdxOJU5uTrdPAE9JT5NP3VAnUHFQu1EGUVBRm1HmUjFSfFLHUxNTX1OqU/ZUQlSPVNtVKFV\r\n                1VcJWD1ZcVqlW91dEV5JX4FgvWH1Yy1kaWWlZuFoHWlZaplr1W0VblVvlXDVchlzWXSddeF3JXhpebF69Xw9fYV+zYAVgV2\r\n                CqYPxhT2GiYfViSWKcYvBjQ2OXY+tkQGSUZOllPWWSZedmPWaSZuhnPWeTZ+loP2iWaOxpQ2maafFqSGqfavdrT2una/9sV\r\n                2yvbQhtYG25bhJua27Ebx5veG/RcCtwhnDgcTpxlXHwcktypnMBc11zuHQUdHB0zHUodYV14XY+dpt2+HdWd7N4EXhueMx5\r\n                KnmJeed6RnqlewR7Y3vCfCF8gXzhfUF9oX4BfmJ+wn8jf4R/5YBHgKiBCoFrgc2CMIKSgvSDV4O6hB2EgITjhUeFq4YOhnK\r\n                G14c7h5+IBIhpiM6JM4mZif6KZIrKizCLlov8jGOMyo0xjZiN/45mjs6PNo+ekAaQbpDWkT+RqJIRknqS45NNk7aUIJSKlP\r\n                SVX5XJljSWn5cKl3WX4JhMmLiZJJmQmfyaaJrVm0Kbr5wcnImc951kndKeQJ6unx2fi5/6oGmg2KFHobaiJqKWowajdqPmp\r\n                Fakx6U4pammGqaLpv2nbqfgqFKoxKk3qamqHKqPqwKrdavprFys0K1ErbiuLa6hrxavi7AAsHWw6rFgsdayS7LCszizrrQl\r\n                tJy1E7WKtgG2ebbwt2i34LhZuNG5SrnCuju6tbsuu6e8IbybvRW9j74KvoS+/796v/XAcMDswWfB48JfwtvDWMPUxFHEzsV\r\n                LxcjGRsbDx0HHv8g9yLzJOsm5yjjKt8s2y7bMNcy1zTXNtc42zrbPN8+40DnQutE80b7SP9LB00TTxtRJ1MvVTtXR1lXW2N\r\n                dc1+DYZNjo2WzZ8dp22vvbgNwF3IrdEN2W3hzeot8p36/gNuC94UThzOJT4tvjY+Pr5HPk/OWE5g3mlucf56noMui86Ubp0\r\n                Opb6uXrcOv77IbtEe2c7ijutO9A78zwWPDl8XLx//KM8xnzp/Q09ML1UPXe9m32+/eK+Bn4qPk4+cf6V/rn+3f8B/yY/Sn9\r\n                uv5L/tz/bf//");
        }

        internal PdfICCBasedColorSpace(PdfObjectCollection collection, PdfStream stream) : this(collection, objArray1)
        {
            object[] objArray1 = new object[] { 0, stream };
        }

        internal PdfICCBasedColorSpace(PdfObjectCollection collection, IList<object> array)
        {
            if (array.Count != 2)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            object obj2 = array[1];
            PdfReaderStream stream = collection.TryResolve(obj2, null) as PdfReaderStream;
            if (stream == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            PdfReaderDictionary dictionary = stream.Dictionary;
            int? integer = dictionary.GetInteger("N");
            if (integer == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.componentsCount = integer.Value;
            if (!this.IsValidComponentsCount)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            if (!dictionary.TryGetValue("Alternate", out obj2))
            {
                this.alternate = this.CreateAlternateColorSpace();
            }
            else
            {
                this.alternate = collection.GetColorSpace(obj2);
                if (this.alternate.ComponentsCount != this.componentsCount)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
            }
            IList<object> list = dictionary.GetArray("Range");
            if (list == null)
            {
                this.range = this.CreateRange();
            }
            else
            {
                if (list.Count != (this.componentsCount * 2))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                this.range = PdfDocumentReader.CreateDomain(list);
            }
            this.metadata = dictionary.GetMetadata();
            try
            {
                this.data = stream.UncompressedData;
            }
            catch
            {
                try
                {
                    this.data = stream.GetUncompressedData(false);
                }
                catch
                {
                    this.data = new byte[0];
                }
            }
        }

        private PdfColorSpace CreateAlternateColorSpace()
        {
            int componentsCount = this.componentsCount;
            return ((componentsCount == 1) ? new PdfDeviceColorSpace(PdfDeviceColorSpaceKind.Gray) : ((componentsCount == 4) ? new PdfDeviceColorSpace(PdfDeviceColorSpaceKind.CMYK) : new PdfDeviceColorSpace(PdfDeviceColorSpaceKind.RGB)));
        }

        protected internal override PdfRange[] CreateDefaultDecodeArray(int bitsPerComponent)
        {
            PdfRange[] rangeArray = new PdfRange[this.range.Count];
            int num = 0;
            foreach (PdfRange range in this.range)
            {
                rangeArray[num++] = new PdfRange(range.Min, range.Max);
            }
            return rangeArray;
        }

        private IList<PdfRange> CreateRange()
        {
            List<PdfRange> list = new List<PdfRange>(this.componentsCount);
            for (int i = 0; i < this.componentsCount; i++)
            {
                list.Add(new PdfRange(0.0, 1.0));
            }
            return list;
        }

        internal PdfObjectReference CreateStream(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(collection);
            dictionary.Add("N", this.componentsCount);
            dictionary.Add("Alternate", this.alternate.Write(collection));
            bool flag = false;
            List<object> list = new List<object>(this.range.Count * 2);
            foreach (PdfRange range in this.range)
            {
                double min = range.Min;
                double max = range.Max;
                list.Add(min);
                list.Add(max);
                if ((min != 0.0) || (max != 1.0))
                {
                    flag = true;
                }
            }
            if (flag)
            {
                dictionary.Add("Range", list);
            }
            dictionary.Add("Metadata", this.metadata);
            return collection.AddStream(dictionary, this.data);
        }

        protected internal override object ToWritableObject(PdfObjectCollection collection)
        {
            List<object> list1 = new List<object>();
            list1.Add(new PdfName("ICCBased"));
            list1.Add(this.CreateStream(collection));
            return list1;
        }

        protected internal override PdfColor Transform(PdfColor color) => 
            this.alternate.AlternateTransform(color);

        protected internal override PdfScanlineTransformationResult Transform(IPdfImageScanlineSource data, int width) => 
            this.alternate.Transform(data, width);

        private bool IsValidComponentsCount =>
            (this.componentsCount == 1) || ((this.componentsCount == 3) || (this.componentsCount == 4));

        public PdfColorSpace Alternate =>
            this.alternate;

        public IEnumerable<PdfRange> Range =>
            this.range;

        public PdfMetadata Metadata =>
            this.metadata;

        public byte[] Data =>
            this.data;

        public override int ComponentsCount =>
            this.componentsCount;
    }
}

