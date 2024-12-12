namespace DevExpress.Mvvm
{
    using System;
    using System.Collections.Generic;

    public interface IApplicationJumpListService
    {
        void AddToRecentCategory(string jumpPath);
        IEnumerable<RejectedApplicationJumpItem> Apply();

        bool ShowFrequentCategory { get; set; }

        bool ShowRecentCategory { get; set; }

        IApplicationJumpList Items { get; }
    }
}

