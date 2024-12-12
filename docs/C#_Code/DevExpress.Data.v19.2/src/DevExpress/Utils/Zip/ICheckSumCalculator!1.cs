namespace DevExpress.Utils.Zip
{
    using System;

    public interface ICheckSumCalculator<T>
    {
        T GetFinalCheckSum(T value);
        T UpdateCheckSum(T value, byte[] buffer, int offset, int count);

        T InitialCheckSumValue { get; }
    }
}

