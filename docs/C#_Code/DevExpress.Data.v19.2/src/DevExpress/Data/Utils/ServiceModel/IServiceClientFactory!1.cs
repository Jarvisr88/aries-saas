namespace DevExpress.Data.Utils.ServiceModel
{
    public interface IServiceClientFactory<TClient>
    {
        TClient Create();
    }
}

