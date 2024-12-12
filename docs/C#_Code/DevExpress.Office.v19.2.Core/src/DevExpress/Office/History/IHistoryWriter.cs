namespace DevExpress.Office.History
{
    using System;

    public interface IHistoryWriter
    {
        int GetObjectId(object obj);
        int GetTypeCode(HistoryItem historyItem);
        void RegisterObject(object obj);
        void Write(bool value);
        void Write(byte value);
        void Write(double value);
        void Write(short value);
        void Write(int value);
        void Write(long value);
        void Write(float value);
        void Write(string value);
        void Write(byte[] buffer);
        void WriteObject(object obj);
    }
}

