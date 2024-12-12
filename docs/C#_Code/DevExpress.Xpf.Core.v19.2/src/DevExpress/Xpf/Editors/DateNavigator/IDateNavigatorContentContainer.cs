namespace DevExpress.Xpf.Editors.DateNavigator
{
    using DevExpress.Xpf.Editors.DateNavigator.Controls;

    public interface IDateNavigatorContentContainer
    {
        DateNavigatorContent GetContent(DateNavigatorCalendarView state);
    }
}

