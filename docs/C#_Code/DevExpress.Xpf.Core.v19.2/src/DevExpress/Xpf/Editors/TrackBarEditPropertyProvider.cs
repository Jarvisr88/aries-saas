namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class TrackBarEditPropertyProvider : ActualPropertyProvider
    {
        public event CustomStepEventHandler CustomStep;

        public TrackBarEditPropertyProvider(TrackBarEdit editor) : base(editor)
        {
        }

        public void RaiseCustomStep(CustomStepEventArgs args)
        {
            CustomStepEventHandler customStep = this.CustomStep;
            if (customStep != null)
            {
                customStep(this.Editor, args);
            }
        }

        private TrackBarEdit Editor =>
            base.Editor as TrackBarEdit;

        private TrackBarStyleSettings StyleSettings =>
            base.StyleSettings as TrackBarStyleSettings;

        public bool IsMoveToPointEnabled
        {
            get
            {
                bool? isMoveToPointEnabled = this.Editor.IsMoveToPointEnabled;
                return ((isMoveToPointEnabled != null) ? isMoveToPointEnabled.GetValueOrDefault() : this.StyleSettings.IsMoveToPointEnabled);
            }
        }
    }
}

