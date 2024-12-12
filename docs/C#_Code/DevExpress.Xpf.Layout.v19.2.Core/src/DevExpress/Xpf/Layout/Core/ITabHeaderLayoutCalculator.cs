namespace DevExpress.Xpf.Layout.Core
{
    public interface ITabHeaderLayoutCalculator
    {
        ITabHeaderLayoutResult Calc(ITabHeaderInfo[] headers, ITabHeaderLayoutOptions options);
    }
}

