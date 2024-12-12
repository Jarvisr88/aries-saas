namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;

    public class DropTargetValidatorFactory
    {
        private readonly Func<DragInfo, IDropTargetValidator> createInternalValidator;
        private readonly Func<IDropTargetValidator> createExternalValidator;

        public DropTargetValidatorFactory() : this(func1, <>c.<>9__3_1 ??= () => new DefaultDropTargetValidator())
        {
            Func<DragInfo, IDropTargetValidator> func1 = <>c.<>9__3_0;
            if (<>c.<>9__3_0 == null)
            {
                Func<DragInfo, IDropTargetValidator> local1 = <>c.<>9__3_0;
                func1 = <>c.<>9__3_0 = _ => new DefaultDropTargetValidator();
            }
        }

        public DropTargetValidatorFactory(Func<DragInfo, IDropTargetValidator> createInternalValidator, Func<IDropTargetValidator> createExternalValidator)
        {
            Guard.ArgumentNotNull(createInternalValidator, "createInternalValidator");
            Guard.ArgumentNotNull(createExternalValidator, "createExternalValidator");
            this.createInternalValidator = createInternalValidator;
            this.createExternalValidator = createExternalValidator;
        }

        public IDropTargetValidator CreateExternalValidator() => 
            this.createExternalValidator();

        public IDropTargetValidator CreateInternalValidator(DragInfo activeDragInfo) => 
            this.createInternalValidator(activeDragInfo);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DropTargetValidatorFactory.<>c <>9 = new DropTargetValidatorFactory.<>c();
            public static Func<DragInfo, IDropTargetValidator> <>9__3_0;
            public static Func<IDropTargetValidator> <>9__3_1;

            internal IDropTargetValidator <.ctor>b__3_0(DragInfo _) => 
                new DefaultDropTargetValidator();

            internal IDropTargetValidator <.ctor>b__3_1() => 
                new DefaultDropTargetValidator();
        }
    }
}

