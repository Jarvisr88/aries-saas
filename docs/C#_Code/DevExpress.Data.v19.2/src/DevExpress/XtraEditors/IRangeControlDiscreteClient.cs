namespace DevExpress.XtraEditors
{
    using System;

    public interface IRangeControlDiscreteClient : IRangeControlClient
    {
        object GetMaxValue(double normalizedMaxValue);
        object GetMinValue(double normalizedMinValue);
        double GetNormalizedMaxValue(object maxValue);
        double GetNormalizedMinValue(object minValue);
        string MaxValueToString(double normalizedMaxValue);
        string MinValueToString(double normalizedMinValue);
    }
}

