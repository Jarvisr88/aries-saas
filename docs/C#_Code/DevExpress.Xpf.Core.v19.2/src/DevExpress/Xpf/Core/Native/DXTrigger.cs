namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Collections.ObjectModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class DXTrigger
    {
        private Control templatedParent;

        public DXTrigger(UIElement owner, string visualState, string visualStateNormal, DXConditionCollection conditions, TargetPropertyUpdater resolver);
        internal DXTrigger(UIElement owner, string visualState, string visualStateNormal, DXConditionCollection conditions, TargetPropertyUpdater resolver, DXSetterCollection setters);
        private void ActivateTrigger();
        private SituationTrigger DefineSituationTrigger();
        private void DXTrigger_LayoutUpdated(object sender, EventArgs e);
        private void Initialize(UIElement owner, DXConditionCollection conditions);
        private void InitializeBySituationTrigger(SituationTrigger situationTrigger);
        private void InitializeCore(UIElement owner, DXConditionCollection conditions);
        protected internal bool IsSetValue();
        protected internal virtual void PerformAction();
        private void Resolver_AnimationUpdated(object sender, EventArgs e);
        private void triggerCondition_ActualValueChanged(object sender, EventArgs e);

        public DXSetterCollection Setters { get; set; }

        public string VisualState { get; set; }

        public string VisualStateNormal { get; set; }

        protected internal DXConditionCollection Conditions { get; set; }

        protected internal bool IsActive { get; set; }

        protected internal UIElement Owner { get; set; }

        protected internal TargetPropertyUpdater Resolver { get; set; }

        protected internal Collection<DXTriggerCondition> TriggerConditions { get; set; }

        private SituationTrigger TriggerSituation { get; set; }

        protected internal Control TemplatedParent { get; }
    }
}

