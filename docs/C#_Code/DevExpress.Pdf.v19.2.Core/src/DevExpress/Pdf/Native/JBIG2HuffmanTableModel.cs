namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class JBIG2HuffmanTableModel
    {
        public void AddLine(int preflenOOB)
        {
            this.Preflen.Add(preflenOOB);
        }

        public void AddLine(int preflen, int rangelen, int rangelow)
        {
            this.Preflen.Add(preflen);
            this.Rangelen.Add(rangelen);
            this.Rangelow.Add(rangelow);
        }

        public static IHuffmanTreeNode StandardHuffmanTableA()
        {
            JBIG2HuffmanTableModel tableModel = new JBIG2HuffmanTableModel();
            tableModel.AddLine(1, 4, 0);
            tableModel.AddLine(2, 8, 0x10);
            tableModel.AddLine(3, 0x10, 0x110);
            tableModel.AddLine(0, 0, 0);
            tableModel.AddLine(3, 0x20, 0x10110);
            tableModel.PrefixSize = 2;
            tableModel.HTOOB = false;
            return JBIG2HuffmanTableBuilder.BuildHuffmanTree(tableModel);
        }

        public static IHuffmanTreeNode StandardHuffmanTableB()
        {
            JBIG2HuffmanTableModel tableModel = new JBIG2HuffmanTableModel();
            tableModel.AddLine(1, 0, 0);
            tableModel.AddLine(2, 0, 1);
            tableModel.AddLine(3, 0, 2);
            tableModel.AddLine(4, 3, 3);
            tableModel.AddLine(5, 6, 11);
            tableModel.AddLine(0, 0, 0);
            tableModel.AddLine(6, 0x20, 0x4b);
            tableModel.AddLine(6);
            tableModel.PrefixSize = 3;
            tableModel.HTOOB = true;
            return JBIG2HuffmanTableBuilder.BuildHuffmanTree(tableModel);
        }

        public static IHuffmanTreeNode StandardHuffmanTableC()
        {
            JBIG2HuffmanTableModel tableModel = new JBIG2HuffmanTableModel();
            tableModel.AddLine(8, 8, -256);
            tableModel.AddLine(1, 0, 0);
            tableModel.AddLine(2, 0, 1);
            tableModel.AddLine(3, 0, 2);
            tableModel.AddLine(4, 3, 3);
            tableModel.AddLine(5, 6, 11);
            tableModel.AddLine(8, 0x20, -257);
            tableModel.AddLine(7, 0x20, 0x4b);
            tableModel.AddLine(6);
            tableModel.PrefixSize = 4;
            tableModel.HTOOB = true;
            return JBIG2HuffmanTableBuilder.BuildHuffmanTree(tableModel);
        }

        public static IHuffmanTreeNode StandardHuffmanTableD()
        {
            JBIG2HuffmanTableModel tableModel = new JBIG2HuffmanTableModel();
            tableModel.AddLine(1, 0, 1);
            tableModel.AddLine(2, 0, 2);
            tableModel.AddLine(3, 0, 3);
            tableModel.AddLine(4, 3, 4);
            tableModel.AddLine(5, 6, 12);
            tableModel.AddLine(0, 0, 0);
            tableModel.AddLine(5, 0x20, 0x4c);
            tableModel.PrefixSize = 3;
            tableModel.HTOOB = false;
            return JBIG2HuffmanTableBuilder.BuildHuffmanTree(tableModel);
        }

        public static IHuffmanTreeNode StandardHuffmanTableE()
        {
            JBIG2HuffmanTableModel tableModel = new JBIG2HuffmanTableModel();
            tableModel.AddLine(7, 8, -255);
            tableModel.AddLine(1, 0, 1);
            tableModel.AddLine(2, 0, 2);
            tableModel.AddLine(3, 0, 3);
            tableModel.AddLine(4, 3, 4);
            tableModel.AddLine(5, 6, 12);
            tableModel.AddLine(7, 0x20, -256);
            tableModel.AddLine(6, 0x20, 0x4c);
            tableModel.PrefixSize = 3;
            tableModel.HTOOB = false;
            return JBIG2HuffmanTableBuilder.BuildHuffmanTree(tableModel);
        }

        public static IHuffmanTreeNode StandardHuffmanTableF()
        {
            JBIG2HuffmanTableModel tableModel = new JBIG2HuffmanTableModel();
            tableModel.AddLine(5, 10, -2048);
            tableModel.AddLine(4, 9, -1024);
            tableModel.AddLine(4, 8, -512);
            tableModel.AddLine(4, 7, -256);
            tableModel.AddLine(5, 6, -128);
            tableModel.AddLine(5, 5, -64);
            tableModel.AddLine(4, 5, -32);
            tableModel.AddLine(2, 7, 0);
            tableModel.AddLine(3, 7, 0x80);
            tableModel.AddLine(3, 8, 0x100);
            tableModel.AddLine(4, 9, 0x200);
            tableModel.AddLine(4, 10, 0x400);
            tableModel.AddLine(6, 0x20, -2049);
            tableModel.AddLine(6, 0x20, 0x800);
            tableModel.PrefixSize = 3;
            tableModel.HTOOB = false;
            return JBIG2HuffmanTableBuilder.BuildHuffmanTree(tableModel);
        }

        public static IHuffmanTreeNode StandardHuffmanTableG()
        {
            JBIG2HuffmanTableModel tableModel = new JBIG2HuffmanTableModel();
            tableModel.AddLine(4, 9, -1024);
            tableModel.AddLine(3, 8, -512);
            tableModel.AddLine(4, 7, -256);
            tableModel.AddLine(5, 6, -128);
            tableModel.AddLine(5, 5, -64);
            tableModel.AddLine(4, 5, -32);
            tableModel.AddLine(4, 5, 0);
            tableModel.AddLine(5, 5, 0x20);
            tableModel.AddLine(5, 6, 0x40);
            tableModel.AddLine(4, 7, 0x80);
            tableModel.AddLine(3, 8, 0x100);
            tableModel.AddLine(3, 9, 0x200);
            tableModel.AddLine(3, 10, 0x400);
            tableModel.AddLine(5, 0x20, -1025);
            tableModel.AddLine(5, 0x20, 0x800);
            tableModel.PrefixSize = 3;
            tableModel.HTOOB = false;
            return JBIG2HuffmanTableBuilder.BuildHuffmanTree(tableModel);
        }

        public static IHuffmanTreeNode StandardHuffmanTableH()
        {
            JBIG2HuffmanTableModel tableModel = new JBIG2HuffmanTableModel();
            tableModel.AddLine(8, 3, -15);
            tableModel.AddLine(9, 1, -7);
            tableModel.AddLine(8, 1, -5);
            tableModel.AddLine(9, 0, -3);
            tableModel.AddLine(7, 0, -2);
            tableModel.AddLine(4, 0, -1);
            tableModel.AddLine(2, 1, 0);
            tableModel.AddLine(5, 0, 2);
            tableModel.AddLine(6, 0, 3);
            tableModel.AddLine(3, 4, 4);
            tableModel.AddLine(6, 1, 20);
            tableModel.AddLine(4, 4, 0x16);
            tableModel.AddLine(4, 5, 0x26);
            tableModel.AddLine(5, 6, 70);
            tableModel.AddLine(5, 7, 0x86);
            tableModel.AddLine(6, 7, 0x106);
            tableModel.AddLine(7, 8, 390);
            tableModel.AddLine(6, 10, 0x286);
            tableModel.AddLine(9, 0x20, -16);
            tableModel.AddLine(9, 0x20, 0x686);
            tableModel.AddLine(2);
            tableModel.PrefixSize = 4;
            tableModel.HTOOB = true;
            return JBIG2HuffmanTableBuilder.BuildHuffmanTree(tableModel);
        }

        public static IHuffmanTreeNode StandardHuffmanTableI()
        {
            JBIG2HuffmanTableModel tableModel = new JBIG2HuffmanTableModel();
            tableModel.AddLine(8, 4, -31);
            tableModel.AddLine(9, 2, -15);
            tableModel.AddLine(8, 2, -11);
            tableModel.AddLine(9, 1, -7);
            tableModel.AddLine(7, 1, -5);
            tableModel.AddLine(4, 1, -3);
            tableModel.AddLine(3, 1, -1);
            tableModel.AddLine(3, 1, 1);
            tableModel.AddLine(5, 1, 3);
            tableModel.AddLine(6, 1, 5);
            tableModel.AddLine(3, 5, 7);
            tableModel.AddLine(6, 2, 0x27);
            tableModel.AddLine(4, 5, 0x2b);
            tableModel.AddLine(4, 6, 0x4b);
            tableModel.AddLine(5, 7, 0x8b);
            tableModel.AddLine(5, 8, 0x10b);
            tableModel.AddLine(6, 8, 0x20b);
            tableModel.AddLine(7, 9, 0x30b);
            tableModel.AddLine(6, 11, 0x50b);
            tableModel.AddLine(9, 0x20, -32);
            tableModel.AddLine(9, 0x20, 0xd0b);
            tableModel.AddLine(2);
            tableModel.PrefixSize = 4;
            tableModel.HTOOB = true;
            return JBIG2HuffmanTableBuilder.BuildHuffmanTree(tableModel);
        }

        public static IHuffmanTreeNode StandardHuffmanTableJ()
        {
            JBIG2HuffmanTableModel tableModel = new JBIG2HuffmanTableModel();
            tableModel.AddLine(7, 4, -21);
            tableModel.AddLine(8, 0, -5);
            tableModel.AddLine(7, 0, -4);
            tableModel.AddLine(5, 0, -3);
            tableModel.AddLine(2, 2, -2);
            tableModel.AddLine(5, 0, 2);
            tableModel.AddLine(6, 0, 3);
            tableModel.AddLine(7, 0, 4);
            tableModel.AddLine(8, 0, 5);
            tableModel.AddLine(2, 6, 6);
            tableModel.AddLine(5, 5, 70);
            tableModel.AddLine(6, 5, 0x66);
            tableModel.AddLine(6, 6, 0x86);
            tableModel.AddLine(6, 7, 0xc6);
            tableModel.AddLine(6, 8, 0x146);
            tableModel.AddLine(6, 9, 0x246);
            tableModel.AddLine(6, 10, 0x446);
            tableModel.AddLine(7, 11, 0x846);
            tableModel.AddLine(8, 0x20, -22);
            tableModel.AddLine(8, 0x20, 0x1046);
            tableModel.AddLine(2);
            tableModel.PrefixSize = 4;
            tableModel.HTOOB = true;
            return JBIG2HuffmanTableBuilder.BuildHuffmanTree(tableModel);
        }

        public static IHuffmanTreeNode StandardHuffmanTableK()
        {
            JBIG2HuffmanTableModel tableModel = new JBIG2HuffmanTableModel();
            tableModel.AddLine(1, 0, 1);
            tableModel.AddLine(2, 1, 2);
            tableModel.AddLine(4, 0, 4);
            tableModel.AddLine(4, 1, 5);
            tableModel.AddLine(5, 1, 7);
            tableModel.AddLine(5, 2, 9);
            tableModel.AddLine(6, 2, 13);
            tableModel.AddLine(7, 2, 0x11);
            tableModel.AddLine(7, 3, 0x15);
            tableModel.AddLine(7, 4, 0x1d);
            tableModel.AddLine(7, 5, 0x2d);
            tableModel.AddLine(7, 6, 0x4d);
            tableModel.AddLine(0, 0, 0);
            tableModel.AddLine(7, 0x20, 0x8d);
            tableModel.PrefixSize = 3;
            tableModel.HTOOB = false;
            return JBIG2HuffmanTableBuilder.BuildHuffmanTree(tableModel);
        }

        public static IHuffmanTreeNode StandardHuffmanTableL()
        {
            JBIG2HuffmanTableModel tableModel = new JBIG2HuffmanTableModel();
            tableModel.AddLine(1, 0, 1);
            tableModel.AddLine(2, 0, 2);
            tableModel.AddLine(3, 1, 3);
            tableModel.AddLine(5, 0, 5);
            tableModel.AddLine(5, 1, 6);
            tableModel.AddLine(6, 1, 8);
            tableModel.AddLine(7, 0, 10);
            tableModel.AddLine(7, 1, 11);
            tableModel.AddLine(7, 2, 13);
            tableModel.AddLine(7, 3, 0x11);
            tableModel.AddLine(7, 4, 0x19);
            tableModel.AddLine(8, 5, 0x29);
            tableModel.AddLine(0, 0, 0);
            tableModel.AddLine(8, 0x20, 0x49);
            tableModel.PrefixSize = 4;
            tableModel.HTOOB = false;
            return JBIG2HuffmanTableBuilder.BuildHuffmanTree(tableModel);
        }

        public static IHuffmanTreeNode StandardHuffmanTableM()
        {
            JBIG2HuffmanTableModel tableModel = new JBIG2HuffmanTableModel();
            tableModel.AddLine(1, 0, 1);
            tableModel.AddLine(3, 0, 2);
            tableModel.AddLine(4, 0, 3);
            tableModel.AddLine(5, 0, 4);
            tableModel.AddLine(4, 1, 5);
            tableModel.AddLine(3, 3, 7);
            tableModel.AddLine(6, 1, 15);
            tableModel.AddLine(6, 2, 0x11);
            tableModel.AddLine(6, 3, 0x15);
            tableModel.AddLine(6, 4, 0x1d);
            tableModel.AddLine(6, 5, 0x2d);
            tableModel.AddLine(7, 6, 0x4d);
            tableModel.AddLine(0, 0, 0);
            tableModel.AddLine(7, 0x20, 0x8d);
            tableModel.PrefixSize = 3;
            tableModel.HTOOB = false;
            return JBIG2HuffmanTableBuilder.BuildHuffmanTree(tableModel);
        }

        public static IHuffmanTreeNode StandardHuffmanTableN()
        {
            JBIG2HuffmanTableModel tableModel = new JBIG2HuffmanTableModel();
            tableModel.AddLine(3, 0, -2);
            tableModel.AddLine(3, 0, -1);
            tableModel.AddLine(1, 0, 0);
            tableModel.AddLine(3, 0, 1);
            tableModel.AddLine(3, 0, 2);
            tableModel.AddLine(0, 0, 0);
            tableModel.AddLine(0, 0, 0);
            tableModel.PrefixSize = 2;
            tableModel.HTOOB = false;
            return JBIG2HuffmanTableBuilder.BuildHuffmanTree(tableModel);
        }

        public static IHuffmanTreeNode StandardHuffmanTableO()
        {
            JBIG2HuffmanTableModel tableModel = new JBIG2HuffmanTableModel();
            tableModel.AddLine(7, 4, -24);
            tableModel.AddLine(6, 2, -8);
            tableModel.AddLine(5, 1, -4);
            tableModel.AddLine(4, 0, -2);
            tableModel.AddLine(3, 0, -1);
            tableModel.AddLine(1, 0, 0);
            tableModel.AddLine(3, 0, 1);
            tableModel.AddLine(4, 0, 2);
            tableModel.AddLine(5, 1, 3);
            tableModel.AddLine(6, 2, 5);
            tableModel.AddLine(7, 4, 9);
            tableModel.AddLine(7, 0x20, -25);
            tableModel.AddLine(7, 0x20, 0x19);
            tableModel.PrefixSize = 3;
            tableModel.HTOOB = false;
            return JBIG2HuffmanTableBuilder.BuildHuffmanTree(tableModel);
        }

        public List<int> Preflen { get; } = new List<int>()

        public List<int> Rangelen { get; } = new List<int>()

        public List<int> Rangelow { get; } = new List<int>()

        public int PrefixSize { get; set; }

        public bool HTOOB { get; set; }

        public int ntemp =>
            this.Preflen.Count;
    }
}

