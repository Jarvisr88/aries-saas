namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.DrawingML;
    using System;

    public class CommonDrawingLocks : DrawingUndoableIndexBasedObject<CommonDrawingLocksInfo>, ISupportsNoChangeAspect
    {
        public CommonDrawingLocks(IDocumentModelPart part) : base(part)
        {
        }

        public override bool Equals(object obj)
        {
            CommonDrawingLocks locks = obj as CommonDrawingLocks;
            return ((locks != null) ? base.Info.Equals(locks.Info) : false);
        }

        public override DocumentModelChangeActions GetBatchUpdateChangeActions() => 
            DocumentModelChangeActions.None;

        protected internal override UniqueItemsCache<CommonDrawingLocksInfo> GetCache(IDocumentModel documentModel) => 
            base.DocumentModel.DrawingCache.CommonDrawingLocksInfoCache;

        public override int GetHashCode() => 
            base.Info.GetHashCode();

        public bool IsEmpty() => 
            base.Info.IsEmpty();

        private DocumentModelChangeActions SetNoAdjustHandlesCore(CommonDrawingLocksInfo info, bool value)
        {
            info.NoAdjustHandles = value;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetNoChangeArrowheadsCore(CommonDrawingLocksInfo info, bool value)
        {
            info.NoChangeArrowheads = value;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetNoChangeAspectCore(CommonDrawingLocksInfo info, bool value)
        {
            info.NoChangeAspect = value;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetNoChangeShapeTypeCore(CommonDrawingLocksInfo info, bool value)
        {
            info.NoChangeShapeType = value;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetNoCropCore(CommonDrawingLocksInfo info, bool value)
        {
            info.NoCrop = value;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetNoDrillDownCore(CommonDrawingLocksInfo info, bool value)
        {
            info.NoDrillDown = value;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetNoEditPointsCore(CommonDrawingLocksInfo info, bool value)
        {
            info.NoEditPoints = value;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetNoGroupCore(CommonDrawingLocksInfo info, bool value)
        {
            info.NoGroup = value;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetNoMoveCore(CommonDrawingLocksInfo info, bool value)
        {
            info.NoMove = value;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetNoResizeCore(CommonDrawingLocksInfo info, bool value)
        {
            info.NoResize = value;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetNoRotateCore(CommonDrawingLocksInfo info, bool value)
        {
            info.NoRotate = value;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetNoSelectCore(CommonDrawingLocksInfo info, bool value)
        {
            info.NoSelect = value;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetNoTextEditCore(CommonDrawingLocksInfo info, bool value)
        {
            info.NoTextEdit = value;
            return DocumentModelChangeActions.None;
        }

        private DocumentModelChangeActions SetNoUngroupCore(CommonDrawingLocksInfo info, bool value)
        {
            info.NoUngroup = value;
            return DocumentModelChangeActions.None;
        }

        public bool NoGroup
        {
            get => 
                base.Info.NoGroup;
            set
            {
                if (value != this.NoGroup)
                {
                    this.SetPropertyValue<bool>(new UndoableIndexBasedObject<CommonDrawingLocksInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<bool>(this.SetNoGroupCore), value);
                }
            }
        }

        public bool NoSelect
        {
            get => 
                base.Info.NoSelect;
            set
            {
                if (value != this.NoSelect)
                {
                    this.SetPropertyValue<bool>(new UndoableIndexBasedObject<CommonDrawingLocksInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<bool>(this.SetNoSelectCore), value);
                }
            }
        }

        public bool NoRotate
        {
            get => 
                base.Info.NoRotate;
            set
            {
                if (value != this.NoRotate)
                {
                    this.SetPropertyValue<bool>(new UndoableIndexBasedObject<CommonDrawingLocksInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<bool>(this.SetNoRotateCore), value);
                }
            }
        }

        public bool NoChangeAspect
        {
            get => 
                base.Info.NoChangeAspect;
            set
            {
                if (value != this.NoChangeAspect)
                {
                    this.SetPropertyValue<bool>(new UndoableIndexBasedObject<CommonDrawingLocksInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<bool>(this.SetNoChangeAspectCore), value);
                }
            }
        }

        public bool NoMove
        {
            get => 
                base.Info.NoMove;
            set
            {
                if (value != this.NoMove)
                {
                    this.SetPropertyValue<bool>(new UndoableIndexBasedObject<CommonDrawingLocksInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<bool>(this.SetNoMoveCore), value);
                }
            }
        }

        public bool NoResize
        {
            get => 
                base.Info.NoResize;
            set
            {
                if (value != this.NoResize)
                {
                    this.SetPropertyValue<bool>(new UndoableIndexBasedObject<CommonDrawingLocksInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<bool>(this.SetNoResizeCore), value);
                }
            }
        }

        public bool NoEditPoints
        {
            get => 
                base.Info.NoEditPoints;
            set
            {
                if (value != this.NoEditPoints)
                {
                    this.SetPropertyValue<bool>(new UndoableIndexBasedObject<CommonDrawingLocksInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<bool>(this.SetNoEditPointsCore), value);
                }
            }
        }

        public bool NoAdjustHandles
        {
            get => 
                base.Info.NoAdjustHandles;
            set
            {
                if (value != this.NoAdjustHandles)
                {
                    this.SetPropertyValue<bool>(new UndoableIndexBasedObject<CommonDrawingLocksInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<bool>(this.SetNoAdjustHandlesCore), value);
                }
            }
        }

        public bool NoChangeArrowheads
        {
            get => 
                base.Info.NoChangeArrowheads;
            set
            {
                if (value != this.NoChangeArrowheads)
                {
                    this.SetPropertyValue<bool>(new UndoableIndexBasedObject<CommonDrawingLocksInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<bool>(this.SetNoChangeArrowheadsCore), value);
                }
            }
        }

        public bool NoChangeShapeType
        {
            get => 
                base.Info.NoChangeShapeType;
            set
            {
                if (value != this.NoChangeShapeType)
                {
                    this.SetPropertyValue<bool>(new UndoableIndexBasedObject<CommonDrawingLocksInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<bool>(this.SetNoChangeShapeTypeCore), value);
                }
            }
        }

        public bool NoTextEdit
        {
            get => 
                base.Info.NoTextEdit;
            set
            {
                if (value != this.NoTextEdit)
                {
                    this.SetPropertyValue<bool>(new UndoableIndexBasedObject<CommonDrawingLocksInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<bool>(this.SetNoTextEditCore), value);
                }
            }
        }

        public bool NoCrop
        {
            get => 
                base.Info.NoCrop;
            set
            {
                if (value != this.NoCrop)
                {
                    this.SetPropertyValue<bool>(new UndoableIndexBasedObject<CommonDrawingLocksInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<bool>(this.SetNoCropCore), value);
                }
            }
        }

        public bool NoDrillDown
        {
            get => 
                base.Info.NoDrillDown;
            set
            {
                if (value != this.NoDrillDown)
                {
                    this.SetPropertyValue<bool>(new UndoableIndexBasedObject<CommonDrawingLocksInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<bool>(this.SetNoDrillDownCore), value);
                }
            }
        }

        public bool NoUngroup
        {
            get => 
                base.Info.NoUngroup;
            set
            {
                if (value != this.NoUngroup)
                {
                    this.SetPropertyValue<bool>(new UndoableIndexBasedObject<CommonDrawingLocksInfo, DocumentModelChangeActions>.SetPropertyValueDelegate<bool>(this.SetNoUngroupCore), value);
                }
            }
        }
    }
}

