namespace DevExpress.Xpf.Layout.Core
{
    public interface IDragServiceStateFactory
    {
        IDragServiceState Create(IDragService service, OperationType operationType);
    }
}

