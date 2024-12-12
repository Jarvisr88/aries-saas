namespace DevExpress.Office.Utils
{
    using System;

    public interface IStringBuilder
    {
        IStringBuilder Append(char ch);
        void Clear();
        string ToString();

        int Length { get; }
    }
}

