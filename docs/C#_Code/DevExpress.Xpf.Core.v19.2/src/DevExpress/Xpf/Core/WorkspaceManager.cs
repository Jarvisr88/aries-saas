namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Core.Serialization;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Documents;
    using System.Windows.Interop;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Media.Effects;
    using System.Windows.Media.Imaging;

    public class WorkspaceManager : IWorkspaceManager
    {
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty IsEnabledProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        public static readonly DependencyProperty WorkspaceManagerProperty;
        private readonly FrameworkElement targetControl;
        private readonly List<IWorkspace> workspaces = new List<IWorkspace>();
        private DevExpress.Xpf.Core.TransitionEffect transitionEffect;
        private Effect saveEffect;
        private Effect saveAdornerEffect;
        private string serializationName;

        public event EventHandler AfterApplyWorkspace;

        public event EventHandler BeforeApplyWorkspace;

        static WorkspaceManager()
        {
            IsEnabledProperty = DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(WorkspaceManager), new PropertyMetadata(false, (d, e) => OnIsEnabledPropertyChanged((FrameworkElement) d, (bool) e.NewValue)));
            WorkspaceManagerProperty = DependencyProperty.RegisterAttached("WorkspaceManager", typeof(IWorkspaceManager), typeof(WorkspaceManager), new PropertyMetadata(null));
        }

        protected WorkspaceManager(FrameworkElement targetControl)
        {
            this.targetControl = targetControl;
            if (EnableLegacySerializationIdAssignment)
            {
                DXSerializer.SetSerializationID(targetControl, this.SerializationName);
            }
        }

        protected virtual void AddWorkspaceCore(string name, object serializationData)
        {
            if (!string.IsNullOrEmpty(name) && (serializationData != null))
            {
                IWorkspace item = new Workspace(name, serializationData);
                int workspaceIndex = this.GetWorkspaceIndex(name);
                if (workspaceIndex != -1)
                {
                    this.Workspaces[workspaceIndex] = item;
                }
                else
                {
                    this.Workspaces.Add(item);
                }
            }
        }

        public void ApplyWorkspace(string name)
        {
            Brush screenshot = null;
            if (this.TransitionEffect != DevExpress.Xpf.Core.TransitionEffect.None)
            {
                screenshot = this.GetScreenshot();
            }
            this.OnBeforeApplyWorkspace();
            this.OnBeforeTransitionAnimation(screenshot);
            this.ApplyWorkspaceCore(this.GetWorkspace(name));
            this.TransitionAnimation(screenshot);
        }

        protected virtual void ApplyWorkspaceCore(IWorkspace workspace)
        {
            if (workspace != null)
            {
                Stream serializationStream = this.GetSerializationStream(workspace);
                if (serializationStream != null)
                {
                    using (serializationStream)
                    {
                        DXSerializer.Deserialize(this.TargetControl, serializationStream, this.SerializationName, null);
                    }
                }
            }
        }

        public void CaptureWorkspace(string name)
        {
            if ((this.TargetControl != null) && !string.IsNullOrEmpty(name))
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    DXSerializer.Serialize((DependencyObject) this.TargetControl, (Stream) stream, this.SerializationName, null);
                    this.AddWorkspaceCore(name, stream.ToArray());
                }
            }
        }

        protected virtual TransitionEffectBase GetAdornerTransitionEffect(DevExpress.Xpf.Core.TransitionEffect effect) => 
            (effect != DevExpress.Xpf.Core.TransitionEffect.Wave) ? this.GetTransitionEffect(this.TransitionEffect) : this.GetTransitionEffect(DevExpress.Xpf.Core.TransitionEffect.Fade);

        protected virtual Stream GetFileStream(string filePath, bool save)
        {
            try
            {
                FileMode mode = save ? FileMode.Create : FileMode.Open;
                return File.Open(filePath, mode);
            }
            catch
            {
                return null;
            }
        }

        public static bool GetIsEnabled(FrameworkElement targetControl) => 
            (targetControl != null) && ((bool) targetControl.GetValue(IsEnabledProperty));

        protected virtual Stream GetLoadStream(object path)
        {
            bool flag;
            return this.GetLoadStream(path, out flag);
        }

        protected virtual Stream GetLoadStream(object path, out bool keepOpened) => 
            this.GetStreamCore(path, false, out keepOpened);

        protected virtual Stream GetSaveStream(object path)
        {
            bool flag;
            return this.GetSaveStream(path, out flag);
        }

        protected virtual Stream GetSaveStream(object path, out bool keepOpened) => 
            this.GetStreamCore(path, true, out keepOpened);

        protected virtual Brush GetScreenshot()
        {
            if (!BrowserInteropHelper.IsBrowserHosted)
            {
                this.RemoveTransitionEffectAnimation();
            }
            ImageBrush brush1 = new ImageBrush();
            brush1.ImageSource = this.RenderFrameworkElement(this.TargetControl);
            brush1.Stretch = Stretch.None;
            brush1.AlignmentX = AlignmentX.Left;
            brush1.AlignmentY = AlignmentY.Top;
            return brush1;
        }

        protected virtual Stream GetSerializationStream(IWorkspace workspace) => 
            this.ValidateWorkspace(workspace) ? new MemoryStream((byte[]) workspace.SerializationData) : null;

        protected virtual Stream GetStreamCore(object path, bool save, out bool keepOpened)
        {
            keepOpened = false;
            if (!string.IsNullOrEmpty(path as string))
            {
                return this.GetFileStream((string) path, save);
            }
            if (!(path is Stream))
            {
                return null;
            }
            keepOpened = true;
            return (Stream) path;
        }

        protected virtual TransitionEffectBase GetTransitionEffect(DevExpress.Xpf.Core.TransitionEffect effect)
        {
            switch (effect)
            {
                case DevExpress.Xpf.Core.TransitionEffect.Dissolve:
                    return new DissolveTransitionEffect();

                case DevExpress.Xpf.Core.TransitionEffect.Fade:
                    return new FadeTransitionEffect();

                case DevExpress.Xpf.Core.TransitionEffect.LineReveal:
                    return new LineRevealTransitionEffect();

                case DevExpress.Xpf.Core.TransitionEffect.RadialBlur:
                    return new RadialBlurTransitionEffect();

                case DevExpress.Xpf.Core.TransitionEffect.Ripple:
                    return new RippleTransitionEffect();

                case DevExpress.Xpf.Core.TransitionEffect.Wave:
                    return new WaveTransitionEffect();
            }
            return null;
        }

        protected internal virtual IWorkspace GetWorkspace(int index) => 
            ((this.TargetControl == null) || ((index < 0) || (index >= this.Workspaces.Count))) ? null : this.Workspaces[index];

        protected internal virtual IWorkspace GetWorkspace(string name) => 
            ((this.TargetControl == null) || string.IsNullOrEmpty(name)) ? null : this.Workspaces.Find(workspace => this.WorkspaceNameEquals(workspace, name));

        protected internal virtual int GetWorkspaceIndex(string name)
        {
            for (int i = 0; i < this.Workspaces.Count; i++)
            {
                if (this.WorkspaceNameEquals(this.Workspaces[i], name))
                {
                    return i;
                }
            }
            return -1;
        }

        public static IWorkspaceManager GetWorkspaceManager(FrameworkElement targetControl) => 
            (targetControl == null) ? null : ((IWorkspaceManager) targetControl.GetValue(WorkspaceManagerProperty));

        public bool LoadWorkspace(string name, object path)
        {
            bool flag;
            return this.LoadWorkspaceCore(name, this.GetLoadStream(path, out flag), flag);
        }

        protected virtual bool LoadWorkspaceCore(string name, Stream stream) => 
            this.LoadWorkspaceCore(name, stream, true);

        protected virtual bool LoadWorkspaceCore(string name, Stream stream, bool keepOpened)
        {
            bool flag;
            if (string.IsNullOrEmpty(name) || (stream == null))
            {
                return false;
            }
            try
            {
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                this.AddWorkspaceCore(name, buffer);
                flag = true;
            }
            catch
            {
                flag = false;
            }
            finally
            {
                if (this.CloseStreamOnWorkspaceLoading.GetValueOrDefault(!keepOpened))
                {
                    stream.Dispose();
                }
            }
            return flag;
        }

        protected virtual void OnAdornerTransitionAnimationCompleted(object sender, EventArgs e)
        {
            this.RestoreTargetControlAdornerEffect();
        }

        protected virtual void OnAfterApplyWorkspace()
        {
            if (this.AfterApplyWorkspace != null)
            {
                this.AfterApplyWorkspace(this, EventArgs.Empty);
            }
        }

        protected virtual void OnBeforeApplyWorkspace()
        {
            if (this.BeforeApplyWorkspace != null)
            {
                this.BeforeApplyWorkspace(this, EventArgs.Empty);
            }
        }

        private void OnBeforeTransitionAnimation(Brush screenshot)
        {
            if (!BrowserInteropHelper.IsBrowserHosted)
            {
                TransitionEffectBase transitionEffect = this.GetTransitionEffect(this.TransitionEffect);
                TransitionEffectBase adornerTransitionEffect = this.GetAdornerTransitionEffect(this.TransitionEffect);
                if ((transitionEffect != null) && (adornerTransitionEffect != null))
                {
                    transitionEffect.OldInput = screenshot;
                    this.SaveTargetControlEffect();
                    this.TargetControl.Effect = transitionEffect;
                    adornerTransitionEffect.OldInput = new ImageBrush();
                    AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this.TargetControl);
                    if (adornerLayer != null)
                    {
                        this.SaveTargetControlAdornerEffect();
                        adornerLayer.Effect = adornerTransitionEffect;
                    }
                }
            }
        }

        private static void OnIsEnabledPropertyChanged(FrameworkElement targetControl, bool enabled)
        {
            if (targetControl != null)
            {
                IWorkspaceManager workspaceManager = enabled ? new WorkspaceManager(targetControl) : null;
                SetWorkspaceManager(targetControl, workspaceManager);
            }
        }

        protected virtual void OnTransitionAnimationCompleted(object sender, EventArgs e)
        {
            this.RestoreTargetControlEffect();
            this.OnAfterApplyWorkspace();
        }

        protected virtual void RemoveTransitionEffectAnimation()
        {
            if (this.TargetControl.Effect is TransitionEffectBase)
            {
                this.TargetControl.Effect.BeginAnimation(TransitionEffectBase.ProgressProperty, null, HandoffBehavior.SnapshotAndReplace);
                this.RestoreTargetControlEffect();
            }
            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this.TargetControl);
            if ((adornerLayer != null) && (adornerLayer.Effect is TransitionEffectBase))
            {
                adornerLayer.Effect.BeginAnimation(TransitionEffectBase.ProgressProperty, null, HandoffBehavior.SnapshotAndReplace);
                this.RestoreTargetControlAdornerEffect();
            }
        }

        public void RemoveWorkspace(string name)
        {
            IWorkspace item = this.GetWorkspace(name);
            if (item != null)
            {
                this.Workspaces.Remove(item);
            }
        }

        public void RenameWorkspace(string oldName, string newName)
        {
            IWorkspace workspace = this.GetWorkspace(oldName);
            if ((workspace != null) && (!string.IsNullOrEmpty(newName) && (oldName != newName)))
            {
                int workspaceIndex = this.GetWorkspaceIndex(oldName);
                this.Workspaces[workspaceIndex] = new Workspace(newName, workspace.SerializationData);
            }
        }

        protected virtual ImageSource RenderFrameworkElement(FrameworkElement source)
        {
            if (source == null)
            {
                return null;
            }
            Rect descendantBounds = VisualTreeHelper.GetDescendantBounds(source);
            descendantBounds = new Rect(descendantBounds.Location, new Point(descendantBounds.Width * DpiScaleX, descendantBounds.Height * DpiScaleY));
            if (descendantBounds.IsEmpty)
            {
                return null;
            }
            RenderTargetBitmap bitmap = new RenderTargetBitmap((int) Math.Round(descendantBounds.Width), (int) Math.Round(descendantBounds.Height), 96.0, 96.0, PixelFormats.Pbgra32);
            VisualBrush brush = new VisualBrush(source);
            DrawingVisual visual = new DrawingVisual();
            using (DrawingContext context = visual.RenderOpen())
            {
                context.DrawRectangle(brush, null, descendantBounds);
                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(source);
                if (adornerLayer != null)
                {
                    Adorner[] adorners = adornerLayer.GetAdorners(source);
                    if (adorners != null)
                    {
                        foreach (Adorner adorner in adorners)
                        {
                            descendantBounds = VisualTreeHelper.GetDescendantBounds(adorner);
                            if (!descendantBounds.IsEmpty)
                            {
                                descendantBounds = new Rect(descendantBounds.Location, new Point(descendantBounds.Width * DpiScaleX, descendantBounds.Height * DpiScaleY));
                                context.DrawRectangle(new VisualBrush(adorner), null, descendantBounds);
                            }
                        }
                    }
                }
            }
            bitmap.Render(visual);
            return bitmap;
        }

        protected virtual void RestoreTargetControlAdornerEffect()
        {
            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this.TargetControl);
            if (adornerLayer != null)
            {
                adornerLayer.Effect = this.saveAdornerEffect;
            }
        }

        protected virtual void RestoreTargetControlEffect()
        {
            this.TargetControl.Effect = this.saveEffect;
        }

        protected virtual void SaveTargetControlAdornerEffect()
        {
            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this.TargetControl);
            if (adornerLayer != null)
            {
                this.saveAdornerEffect = adornerLayer.Effect;
            }
        }

        protected virtual void SaveTargetControlEffect()
        {
            this.saveEffect = this.TargetControl.Effect;
        }

        public bool SaveWorkspace(string name, object path)
        {
            bool flag;
            return this.SaveWorkspaceCore(name, this.GetSaveStream(path, out flag), flag);
        }

        protected virtual bool SaveWorkspaceCore(string name, Stream stream) => 
            this.SaveWorkspaceCore(name, stream, true);

        protected virtual bool SaveWorkspaceCore(string name, Stream stream, bool keepOpened)
        {
            bool flag;
            IWorkspace workspace = this.GetWorkspace(name);
            if (!this.ValidateWorkspace(workspace) || (stream == null))
            {
                return false;
            }
            try
            {
                byte[] serializationData = (byte[]) workspace.SerializationData;
                stream.Write(serializationData, 0, serializationData.Length);
                stream.Flush();
                flag = true;
            }
            catch
            {
                flag = false;
            }
            finally
            {
                if (this.CloseStreamOnWorkspaceSaving.GetValueOrDefault(!keepOpened))
                {
                    stream.Dispose();
                }
            }
            return flag;
        }

        public static void SetIsEnabled(FrameworkElement targetControl, bool enabled)
        {
            if (targetControl != null)
            {
                targetControl.SetValue(IsEnabledProperty, enabled);
            }
        }

        private static void SetWorkspaceManager(FrameworkElement targetControl, IWorkspaceManager workspaceManager)
        {
            if (targetControl != null)
            {
                targetControl.SetValue(WorkspaceManagerProperty, workspaceManager);
            }
        }

        protected virtual void TransitionAnimation(Brush screenshot)
        {
            if (BrowserInteropHelper.IsBrowserHosted)
            {
                this.OnAfterApplyWorkspace();
            }
            else
            {
                TransitionEffectBase effect = this.TargetControl.Effect as TransitionEffectBase;
                Func<AdornerLayer, Effect> evaluator = <>c.<>9__76_0;
                if (<>c.<>9__76_0 == null)
                {
                    Func<AdornerLayer, Effect> local1 = <>c.<>9__76_0;
                    evaluator = <>c.<>9__76_0 = x => x.Effect;
                }
                TransitionEffectBase base3 = AdornerLayer.GetAdornerLayer(this.TargetControl).Return<AdornerLayer, Effect>(evaluator, null) as TransitionEffectBase;
                if ((effect == null) || (base3 == null))
                {
                    this.OnAfterApplyWorkspace();
                }
                else
                {
                    DoubleAnimation animation = new DoubleAnimation(0.0, 1.0, new Duration(TimeSpan.FromSeconds(1.0)));
                    DoubleAnimation animation2 = new DoubleAnimation(0.0, 1.0, new Duration(TimeSpan.FromSeconds(1.0)));
                    animation.Completed += new EventHandler(this.OnTransitionAnimationCompleted);
                    this.TargetControl.Effect.BeginAnimation(TransitionEffectBase.ProgressProperty, animation);
                    if (AdornerLayer.GetAdornerLayer(this.TargetControl) != null)
                    {
                        animation2.Completed += new EventHandler(this.OnAdornerTransitionAnimationCompleted);
                        base3.BeginAnimation(TransitionEffectBase.ProgressProperty, animation2);
                    }
                }
            }
        }

        protected internal virtual bool ValidateWorkspace(IWorkspace workspace) => 
            (workspace != null) && (!string.IsNullOrEmpty(workspace.Name) && (workspace.SerializationData is byte[]));

        protected virtual bool WorkspaceNameEquals(IWorkspace workspace, string name) => 
            workspace.Name == name;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
        public static bool EnableLegacySerializationIdAssignment { get; set; }

        private static double DpiScaleX =>
            ScreenHelper.ScaleX;

        private static double DpiScaleY =>
            DpiScaleX;

        [Description("Gets the target control.")]
        public FrameworkElement TargetControl =>
            this.targetControl;

        [Description("Gets the collection of workspaces.")]
        public List<IWorkspace> Workspaces =>
            this.workspaces;

        [Description("Gets or sets the shader effect for the animation played when switching between workspaces.")]
        public DevExpress.Xpf.Core.TransitionEffect TransitionEffect
        {
            get => 
                this.transitionEffect;
            set => 
                this.transitionEffect = value;
        }

        protected internal virtual string SerializationName
        {
            get
            {
                string str2;
                if (!string.IsNullOrEmpty(this.serializationName))
                {
                    return this.serializationName;
                }
                if (this.TargetControl == null)
                {
                    return string.Empty;
                }
                string serializationID = DXSerializer.GetSerializationID(this.TargetControl);
                if (string.IsNullOrEmpty(serializationID) || EnableLegacySerializationIdAssignment)
                {
                    serializationID = this.TargetControl.GetType().Name;
                }
                this.serializationName = str2 = serializationID;
                DXSerializer.SetSerializationID(this.TargetControl, str2);
                return serializationID;
            }
        }

        public bool? CloseStreamOnWorkspaceSaving { get; set; }

        public bool? CloseStreamOnWorkspaceLoading { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly WorkspaceManager.<>c <>9 = new WorkspaceManager.<>c();
            public static Func<AdornerLayer, Effect> <>9__76_0;

            internal void <.cctor>b__7_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                WorkspaceManager.OnIsEnabledPropertyChanged((FrameworkElement) d, (bool) e.NewValue);
            }

            internal Effect <TransitionAnimation>b__76_0(AdornerLayer x) => 
                x.Effect;
        }
    }
}

