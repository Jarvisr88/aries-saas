namespace DevExpress.Mvvm
{
    using System;

    public interface IStateSerializer
    {
        object DeserializeState(string state, Type stateType);
        string SerializeState(object state, Type stateType);
    }
}

