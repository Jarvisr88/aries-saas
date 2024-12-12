namespace DevExpress.Xpf.Editors.DateNavigator
{
    using DevExpress.Xpf.Editors.DateNavigator.Controls;
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;

    public interface INavigationService
    {
        void BringToView();
        void CheckSelectedDates();
        bool Move(DateTime date);
        bool MoveDown();
        bool MoveLeft();
        bool MoveRight();
        bool MoveUp();
        void ProcessKeyDown(KeyEventArgs e);
        void ProcessKeyUp(KeyEventArgs e);
        void ProcessMouseDown(DateTime buttonDate, DateNavigatorCalendarButtonKind buttonKind);
        void ProcessMouseMove(DateTime? buttonDate, DateNavigatorCalendarButtonKind buttonKind);
        void ProcessMouseUp(DateTime? buttonDate, DateNavigatorCalendarButtonKind buttonKind);
        bool ScrollNext();
        bool ScrollNextPage();
        bool ScrollPrevious();
        bool ScrollPreviousPage();
        bool ScrollTo(DateTime dateTime, bool scrollIfValueInactive);
        bool Select(IList<DateTime> range, bool clearSelection);
        bool Select(DateTime dateTime, bool clearSelection);
        bool SelectDown(bool clearSelection);
        bool SelectLeft(bool clearSelection);
        bool SelectRight(bool clearSelection);
        bool SelectUp(bool clearSelection);
        void ToView(DateNavigatorCalendarView navigationState);
        bool Unselect(DateTime dateTime, bool clearSelection);
    }
}

