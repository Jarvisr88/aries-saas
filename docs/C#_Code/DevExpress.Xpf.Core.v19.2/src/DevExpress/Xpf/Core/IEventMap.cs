namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.ReflectionExtensions.Attributes;
    using System;
    using System.Collections;
    using System.Reflection;

    [Wrapper, BindingFlags(BindingFlags.NonPublic | BindingFlags.Static)]
    public interface IEventMap
    {
        [FieldAccessor, Name("_eventsTable")]
        Hashtable EventsTable { get; set; }

        [FieldAccessor, Name("_lock")]
        object Lock { get; }
    }
}

