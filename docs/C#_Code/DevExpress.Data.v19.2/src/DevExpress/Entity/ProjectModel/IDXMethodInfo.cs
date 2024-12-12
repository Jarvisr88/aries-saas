namespace DevExpress.Entity.ProjectModel
{
    public interface IDXMethodInfo : IDXMemberInfo
    {
        IDXTypeInfo ReturnType { get; }
    }
}

