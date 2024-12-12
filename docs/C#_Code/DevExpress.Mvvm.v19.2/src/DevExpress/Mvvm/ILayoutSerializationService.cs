namespace DevExpress.Mvvm
{
    using System;

    public interface ILayoutSerializationService
    {
        void Deserialize(string state);
        string Serialize();
    }
}

