namespace DevExpress.Xpf.Layout.Core
{
    public interface IUIElement
    {
        IUIElement Scope { get; }

        UIChildren Children { get; }
    }
}

