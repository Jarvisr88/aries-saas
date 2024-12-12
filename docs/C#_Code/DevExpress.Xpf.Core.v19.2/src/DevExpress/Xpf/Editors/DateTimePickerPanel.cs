namespace DevExpress.Xpf.Editors
{
    public class DateTimePickerPanel : LoopedPanel
    {
        protected override IndexCalculator CreateIndexCalculator() => 
            new DateTimePickerIndexCalculator();
    }
}

