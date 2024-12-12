namespace DevExpress.Xpf.Editors.DateNavigator
{
    using System.Collections.Generic;

    public interface IValueValidatingService
    {
        IList<DateTime> Validate(IList<DateTime> selectedDates);
    }
}

