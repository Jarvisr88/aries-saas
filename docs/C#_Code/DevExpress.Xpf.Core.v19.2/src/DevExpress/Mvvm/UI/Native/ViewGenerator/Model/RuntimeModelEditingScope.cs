namespace DevExpress.Mvvm.UI.Native.ViewGenerator.Model
{
    using System;

    public class RuntimeModelEditingScope : IModelEditingScope, IDisposable
    {
        private readonly EditingContextBase context;
        private readonly string description;

        public RuntimeModelEditingScope(EditingContextBase context, string description)
        {
            context.Trace.CreateEditingScope(description);
            this.context = context;
            this.description = description;
        }

        void IModelEditingScope.Complete()
        {
            this.context.Trace.CompleteEditingScope(this.description);
        }

        void IModelEditingScope.Revert()
        {
            this.context.Trace.RevertEditingScope(this.description);
        }

        void IModelEditingScope.Update()
        {
            this.context.Trace.UpdateEditingScope(this.description);
        }

        void IDisposable.Dispose()
        {
            this.context.Trace.DisposeEditingScope(this.description);
        }

        string IModelEditingScope.Description
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}

