namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using DevExpress.Office.History;
    using DevExpress.Utils;
    using System;

    public class ContainerEffect : IDrawingEffect, ISupportsCopyFrom<ContainerEffect>, IOfficeNotifyPropertyChanged
    {
        public static readonly PropertyKey EffectsPropertyKey = new PropertyKey(0);
        public static readonly PropertyKey NamePropertyKey = new PropertyKey(1);
        public static readonly PropertyKey TypePropertyKey = new PropertyKey(2);
        public static readonly PropertyKey HasEffectsListPropertyKey = new PropertyKey(3);
        public static readonly PropertyKey ApplyEffectsListPropertyKey = new PropertyKey(4);
        private readonly DrawingEffectCollection effects;
        private readonly PropertyChangedNotifier notifier;
        private string name = string.Empty;
        private DrawingEffectContainerType type;
        private bool hasEffectsList = true;
        private bool applyEffectList;

        public event EventHandler<OfficePropertyChangedEventArgs> PropertyChanged
        {
            add
            {
                this.notifier.Handler += value;
            }
            remove
            {
                this.notifier.Handler -= value;
            }
        }

        public ContainerEffect(IDocumentModel documentModel)
        {
            Guard.ArgumentNotNull(documentModel, "documentModel");
            this.notifier = new PropertyChangedNotifier(this);
            this.effects = new DrawingEffectCollection(documentModel);
            this.effects.Modified += new EventHandler(this.OnEffectsModified);
        }

        internal void ApplyEffects(IDrawingEffectVisitor visitor)
        {
            this.Effects.ApplyEffects(visitor);
        }

        private void ApplyHistoryItem(HistoryItem item)
        {
            this.DocumentModel.History.Add(item);
            item.Execute();
        }

        public IDrawingEffect CloneTo(IDocumentModel documentModel)
        {
            ContainerEffect effect = new ContainerEffect(documentModel);
            effect.CopyFrom(this);
            return effect;
        }

        public void CopyFrom(ContainerEffect value)
        {
            this.Effects.Clear();
            foreach (IDrawingEffect effect in value.Effects)
            {
                this.Effects.Add(effect.CloneTo(this.DocumentModel));
            }
            this.Name = value.Name;
            this.Type = value.Type;
            this.HasEffectsList = value.HasEffectsList;
            this.ApplyEffectList = value.ApplyEffectList;
        }

        public override bool Equals(object obj)
        {
            ContainerEffect effect = obj as ContainerEffect;
            return ((effect != null) ? ((StringExtensions.CompareInvariantCultureIgnoreCase(this.name, effect.name) == 0) && ((this.type == effect.type) && ((this.hasEffectsList == effect.hasEffectsList) && ((this.applyEffectList == effect.applyEffectList) && this.effects.Equals(effect.effects))))) : false);
        }

        public override int GetHashCode() => 
            ((((base.GetType().GetHashCode() ^ this.name.GetHashCode()) ^ this.type.GetHashCode()) ^ this.effects.GetHashCode()) ^ this.hasEffectsList.GetHashCode()) ^ this.applyEffectList.GetHashCode();

        private void OnEffectsModified(object sender, EventArgs e)
        {
            this.notifier.OnPropertyChanged(EffectsPropertyKey);
        }

        protected internal void SetApplyEffectListCore(bool value)
        {
            this.applyEffectList = value;
            this.notifier.OnPropertyChanged(ApplyEffectsListPropertyKey);
        }

        private void SetHasEffectsList(bool value)
        {
            if (this.hasEffectsList != value)
            {
                this.ApplyHistoryItem(new DrawingContainerEffectHasEffectsListChangedHistoryItem(this.DocumentModel.MainPart, this, this.hasEffectsList, value));
            }
        }

        internal void SetHasEffectsListCore(bool value)
        {
            this.hasEffectsList = value;
        }

        private void SetName(string value)
        {
            if (this.name != value)
            {
                this.ApplyHistoryItem(new DrawingContainerEffectNameChangedHistoryItem(this.DocumentModel.MainPart, this, this.name, value));
            }
        }

        internal void SetNameCore(string value)
        {
            this.name = value;
            this.notifier.OnPropertyChanged(NamePropertyKey);
        }

        private void SetType(DrawingEffectContainerType value)
        {
            if (this.type != value)
            {
                this.ApplyHistoryItem(new DrawingContainerEffectTypeChangedHistoryItem(this.DocumentModel.MainPart, this, this.type, value));
            }
        }

        internal void SetTypeCore(DrawingEffectContainerType value)
        {
            this.type = value;
            this.notifier.OnPropertyChanged(TypePropertyKey);
        }

        public void Visit(IDrawingEffectVisitor visitor)
        {
            visitor.Visit(this);
        }

        public DrawingEffectCollection Effects =>
            this.effects;

        public string Name
        {
            get => 
                this.name;
            set => 
                this.SetName(value);
        }

        public DrawingEffectContainerType Type
        {
            get => 
                this.type;
            set => 
                this.SetType(value);
        }

        public bool HasEffectsList
        {
            get => 
                this.hasEffectsList;
            set => 
                this.SetHasEffectsList(value);
        }

        public bool ApplyEffectList
        {
            get => 
                this.applyEffectList;
            set
            {
                if (this.applyEffectList != value)
                {
                    this.ApplyHistoryItem(new ActionBooleanHistoryItem(this.DocumentModel.MainPart, this.applyEffectList, value, new Action<bool>(this.SetApplyEffectListCore)));
                }
            }
        }

        public bool IsEmpty =>
            !this.ApplyEffectList && (this.effects.Count == 0);

        private IDocumentModel DocumentModel =>
            this.effects.DocumentModel;
    }
}

