namespace DevExpress.DocumentServices.ServiceModel
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public interface IParameterContainer : IEnumerable<IClientParameter>, IEnumerable
    {
        int Count { get; }

        IClientParameter this[string path] { get; }
    }
}

