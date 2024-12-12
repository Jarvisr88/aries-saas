namespace DevExpress.Utils
{
    using System;

    public interface IConvertToInt<T> where T: struct
    {
        T FromInt(int value);
        int ToInt();
    }
}

