namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class JBIG2TextRegionHuffmanTables
    {
        public JBIG2TextRegionHuffmanTables(int huffFlags, IList<JBIG2HuffmanTableSegment> userDefinedTables)
        {
            JBIG2UserHuffmanTablesEnumerator userDefinedTablesEnumerator = new JBIG2UserHuffmanTablesEnumerator(userDefinedTables);
            switch ((huffFlags & 3))
            {
                case 0:
                    this.<Sbhufffs>k__BackingField = JBIG2HuffmanTableModel.StandardHuffmanTableF();
                    break;

                case 1:
                    this.<Sbhufffs>k__BackingField = JBIG2HuffmanTableModel.StandardHuffmanTableG();
                    break;

                case 3:
                    this.<Sbhufffs>k__BackingField = userDefinedTablesEnumerator.GetNext();
                    break;

                default:
                    break;
            }
            switch (((huffFlags >> 2) & 3))
            {
                case 0:
                    this.<Sbhuffds>k__BackingField = JBIG2HuffmanTableModel.StandardHuffmanTableH();
                    break;

                case 1:
                    this.<Sbhuffds>k__BackingField = JBIG2HuffmanTableModel.StandardHuffmanTableI();
                    break;

                case 2:
                    this.<Sbhuffds>k__BackingField = JBIG2HuffmanTableModel.StandardHuffmanTableJ();
                    break;

                case 3:
                    this.<Sbhuffds>k__BackingField = userDefinedTablesEnumerator.GetNext();
                    break;

                default:
                    break;
            }
            switch (((huffFlags >> 4) & 3))
            {
                case 0:
                    this.<Sbhuffdt>k__BackingField = JBIG2HuffmanTableModel.StandardHuffmanTableK();
                    break;

                case 1:
                    this.<Sbhuffdt>k__BackingField = JBIG2HuffmanTableModel.StandardHuffmanTableL();
                    break;

                case 2:
                    this.<Sbhuffdt>k__BackingField = JBIG2HuffmanTableModel.StandardHuffmanTableM();
                    break;

                case 3:
                    this.<Sbhuffdt>k__BackingField = userDefinedTablesEnumerator.GetNext();
                    break;

                default:
                    break;
            }
            this.<Sbhuffrdw>k__BackingField = SelectHuffmanTable((huffFlags >> 6) & 3, userDefinedTablesEnumerator);
            this.<Sbhuffrdh>k__BackingField = SelectHuffmanTable((huffFlags >> 8) & 3, userDefinedTablesEnumerator);
            this.<Sbhuffrdx>k__BackingField = SelectHuffmanTable((huffFlags >> 10) & 3, userDefinedTablesEnumerator);
            this.<Sbhuffrdy>k__BackingField = SelectHuffmanTable((huffFlags >> 12) & 3, userDefinedTablesEnumerator);
            if (((huffFlags >> 14) & 3) == 0)
            {
                this.<Sbhuffrsize>k__BackingField = JBIG2HuffmanTableModel.StandardHuffmanTableA();
            }
            else
            {
                this.<Sbhuffrsize>k__BackingField = userDefinedTablesEnumerator.GetNext();
            }
        }

        public JBIG2TextRegionHuffmanTables(IHuffmanTreeNode sbhufffs, IHuffmanTreeNode sbhuffds, IHuffmanTreeNode sbhuffdt, IHuffmanTreeNode sbhuffrdw, IHuffmanTreeNode sbhuffrdh, IHuffmanTreeNode sbhuffrdx, IHuffmanTreeNode sbhuffrdy, IHuffmanTreeNode sbhuffrsize)
        {
            this.<Sbhufffs>k__BackingField = sbhufffs;
            this.<Sbhuffds>k__BackingField = sbhuffds;
            this.<Sbhuffdt>k__BackingField = sbhuffdt;
            this.<Sbhuffrdw>k__BackingField = sbhuffrdw;
            this.<Sbhuffrdh>k__BackingField = sbhuffrdh;
            this.<Sbhuffrdx>k__BackingField = sbhuffrdx;
            this.<Sbhuffrdy>k__BackingField = sbhuffrdy;
            this.<Sbhuffrsize>k__BackingField = sbhuffrsize;
        }

        private static IHuffmanTreeNode SelectHuffmanTable(int flag, JBIG2UserHuffmanTablesEnumerator userDefinedTablesEnumerator)
        {
            switch (flag)
            {
                case 0:
                    return JBIG2HuffmanTableModel.StandardHuffmanTableN();

                case 1:
                    return JBIG2HuffmanTableModel.StandardHuffmanTableO();

                case 3:
                    return userDefinedTablesEnumerator.GetNext();
            }
            return null;
        }

        public IHuffmanTreeNode Sbhufffs { get; }

        public IHuffmanTreeNode Sbhuffds { get; }

        public IHuffmanTreeNode Sbhuffdt { get; }

        public IHuffmanTreeNode Sbhuffrdw { get; }

        public IHuffmanTreeNode Sbhuffrdh { get; }

        public IHuffmanTreeNode Sbhuffrdx { get; }

        public IHuffmanTreeNode Sbhuffrdy { get; }

        public IHuffmanTreeNode Sbhuffrsize { get; }
    }
}

