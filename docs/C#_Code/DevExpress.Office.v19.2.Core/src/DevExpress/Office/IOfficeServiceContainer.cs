namespace DevExpress.Office
{
    public interface IOfficeServiceContainer
    {
        T GetService<T>() where T: class;
        T ReplaceService<T>(T newService) where T: class;
    }
}

