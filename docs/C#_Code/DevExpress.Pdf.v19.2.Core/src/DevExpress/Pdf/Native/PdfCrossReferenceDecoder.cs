namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;

    public class PdfCrossReferenceDecoder
    {
        private readonly byte[] data;
        private readonly int length;
        private int position;

        private PdfCrossReferenceDecoder(byte[] data)
        {
            this.data = data;
            this.length = data.Length;
        }

        private void Decode(IEnumerable<PdfIndexDescription> indices, PdfObjectCollection objects, int typeWeight, int offsetWeight, int generationWeight)
        {
            foreach (PdfIndexDescription description in indices)
            {
                int startValue = description.StartValue;
                int count = description.Count;
                for (int i = 0; i < count; i++)
                {
                    int objectStreamNumber = this.ReadValue(offsetWeight);
                    int generation = this.ReadValue(generationWeight);
                    switch (((typeWeight == 0) ? 1 : this.ReadValue(typeWeight)))
                    {
                        case 0:
                            objects.AddFreeObject(startValue, generation);
                            break;

                        case 1:
                            objects.AddItem(new PdfObjectSlot(startValue, generation, (long) objectStreamNumber), false);
                            break;

                        case 2:
                            objects.AddItem(new PdfObjectStreamElement(startValue, objectStreamNumber, generation), false);
                            break;

                        default:
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                            break;
                    }
                    startValue++;
                }
            }
        }

        public static void Decode(byte[] data, IEnumerable<PdfIndexDescription> indices, PdfObjectCollection objects, int typeWeight, int offsetWeight, int generationWeight)
        {
            new PdfCrossReferenceDecoder(data).Decode(indices, objects, typeWeight, offsetWeight, generationWeight);
        }

        private int ReadValue(int weight)
        {
            if (weight == 0)
            {
                return 0;
            }
            if ((this.position + weight) > this.length)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            int position = this.position;
            this.position = position + 1;
            int num = this.data[position];
            for (int i = 1; i < weight; i++)
            {
                position = this.position;
                this.position = position + 1;
                num = (num << 8) + this.data[position];
            }
            return num;
        }
    }
}

