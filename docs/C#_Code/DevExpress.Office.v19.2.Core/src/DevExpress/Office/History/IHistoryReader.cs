namespace DevExpress.Office.History
{
    using System;

    public interface IHistoryReader
    {
        HistoryItem CreateHistoryItem();
        object GetObject(int id);
        bool ReadBoolean();
        byte ReadByte();
        byte[] ReadBytes();
        double ReadDouble();
        short ReadInt16();
        int ReadInt32();
        long ReadInt64();
        void ReadObject(object obj);
        float ReadSingle();
        string ReadString();
    }
}

