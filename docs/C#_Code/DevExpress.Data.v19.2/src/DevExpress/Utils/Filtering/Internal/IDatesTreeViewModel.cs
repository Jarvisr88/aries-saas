namespace DevExpress.Utils.Filtering.Internal
{
    public interface IDatesTreeViewModel : IRangeValueViewModel<DateTime>, IRangeValueViewModel, IValueViewModel
    {
        IDateIntervalsHashTree HashTree { get; }
    }
}

