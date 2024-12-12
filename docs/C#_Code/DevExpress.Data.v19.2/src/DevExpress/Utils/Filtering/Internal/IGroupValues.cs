namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public interface IGroupValues : IEnumerable<object>, IEnumerable, IEnumerable<IGrouping<int, object>>
    {
        event EventHandler<GroupValuesLoadedEventArgs> Loaded;

        event EventHandler VisualUpdateRequired;

        int GetIndex(int group);
        int? GetParent(int group);
        object GetValue(int index, out int group);
        void Invert();
        bool? IsChecked(object value, int? group = new int?());
        bool IsLoaded(object[] path);
        bool Load(object[] level, int? group = new int?(), int? depth = new int?(), string[] texts = null);
        void QueryLoad(int group);
        void Reset();
        void Toggle(object value, int? group = new int?());
        void ToggleAll();

        int Depth { get; }

        int Count { get; }

        object this[int index] { get; }

        bool HasValue { get; }
    }
}

