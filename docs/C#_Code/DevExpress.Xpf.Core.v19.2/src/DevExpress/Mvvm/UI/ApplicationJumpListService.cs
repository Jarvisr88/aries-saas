namespace DevExpress.Mvvm.UI
{
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Mvvm.UI.Interactivity.Internal;
    using DevExpress.Mvvm.UI.Native;
    using DevExpress.Utils;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;
    using System.Windows.Shell;

    [ContentProperty("Items"), TargetType(typeof(Window)), TargetType(typeof(UserControl))]
    public class ApplicationJumpListService : ServiceBase, IApplicationJumpListService, IApplicationJumpListImplementation
    {
        public const string CommandLineArgumentDefaultPrefix = "/APPLICATION_JUMP_TASK=";
        public static readonly DependencyProperty CommandLineArgumentPrefixProperty = DependencyProperty.Register("CommandLineArgumentPrefix", typeof(string), typeof(ApplicationJumpListService), new PropertyMetadata("/APPLICATION_JUMP_TASK="));
        public static readonly DependencyProperty DefaultLauncherStorageFolderProperty = DependencyProperty.Register("DefaultLauncherStorageFolder", typeof(string), typeof(ApplicationJumpListService), new PropertyMetadata(NativeResourceManager.ResourcesFolder));
        public static readonly DependencyProperty CustomLauncherPathProperty = DependencyProperty.Register("CustomLauncherPath", typeof(string), typeof(ApplicationJumpListService), new PropertyMetadata(null));
        public static readonly DependencyProperty IconStorageFolderProperty = DependencyProperty.Register("IconStorageFolder", typeof(string), typeof(ApplicationJumpListService), new PropertyMetadata(NativeResourceManager.ResourcesFolder));
        public static readonly DependencyProperty IconStorageProperty = DependencyProperty.Register("IconStorage", typeof(IIconStorage), typeof(ApplicationJumpListService), new PropertyMetadata(null, null, (CoerceValueCallback) ((d, v) => (v ?? ((ApplicationJumpListService) d).CreateDefaultIconStorage()))));
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty InternalItemsProperty = DependencyProperty.RegisterAttached("InternalItems", typeof(FreezableCollection<ApplicationJumpItem>), typeof(ApplicationJumpListService), new PropertyMetadata(null));
        private static NativeJumpList currentNativeJumpList = new NativeJumpList(new Func<JumpItem, string>(ApplicationJumpItemWrapper.GetJumpItemCommandId));
        private INativeJumpList nativeJumpList;
        private IJumpActionsManager jumpActionsManager;
        private bool designModeShowFrequentCategory;
        private bool designModeShowRecentCategory;
        private List<ApplicationJumpItemInfo> designModeItems;
        private ApplicationJumpList jumpList;

        public ApplicationJumpListService() : this(null, null)
        {
        }

        protected ApplicationJumpListService(INativeJumpList nativeJumpList, IJumpActionsManager jumpActionsManager)
        {
            this.designModeItems = new List<ApplicationJumpItemInfo>();
            this.IconStorage = this.CreateDefaultIconStorage();
            this.jumpList = new ApplicationJumpList(this);
            this.Items = new ApplicationJumpItemCollectionInternal(this);
            if (!InteractionHelper.IsInDesignMode(this))
            {
                INativeJumpList currentNativeJumpList = nativeJumpList;
                if (nativeJumpList == null)
                {
                    INativeJumpList local1 = nativeJumpList;
                    currentNativeJumpList = ApplicationJumpListService.currentNativeJumpList;
                }
                this.nativeJumpList = currentNativeJumpList;
                IJumpActionsManager current = jumpActionsManager;
                if (jumpActionsManager == null)
                {
                    IJumpActionsManager local2 = jumpActionsManager;
                    current = JumpActionsManager.Current;
                }
                this.jumpActionsManager = current;
            }
        }

        protected virtual void AddItem(ApplicationJumpItemInfo item)
        {
            if (InteractionHelper.IsInDesignMode(this))
            {
                this.designModeItems.Add(item);
            }
            else
            {
                this.nativeJumpList.Add(this.PrepareItem(item, null));
            }
        }

        public virtual bool AddOrReplace(ApplicationJumpTaskInfo jumpTask)
        {
            if (InteractionHelper.IsInDesignMode(this))
            {
                this.designModeItems.Add(jumpTask);
                return true;
            }
            ApplicationJumpTaskWrap task = new ApplicationJumpTaskWrap(jumpTask);
            this.PrepareTask(task);
            ((ISupportInitialize) jumpTask).EndInit();
            JumpItem item = this.nativeJumpList.Find(jumpTask.CommandId);
            if (item != null)
            {
                this.nativeJumpList[this.nativeJumpList.IndexOf(item)] = task;
                return false;
            }
            this.nativeJumpList.Add(task);
            return true;
        }

        public virtual void AddToRecentCategory(string jumpPath)
        {
            if (!InteractionHelper.IsInDesignMode(this))
            {
                this.nativeJumpList.AddToRecentCategory(jumpPath);
            }
        }

        public virtual IEnumerable<RejectedApplicationJumpItem> Apply()
        {
            IEnumerable<RejectedApplicationJumpItem> enumerable2;
            if (InteractionHelper.IsInDesignMode(this))
            {
                return new RejectedApplicationJumpItem[0];
            }
            this.jumpActionsManager.BeginUpdate();
            try
            {
                foreach (JumpItem item in this.nativeJumpList)
                {
                    ApplicationJumpItemWrapper.FillWrapProperties(item);
                    ApplicationJumpTaskWrap jumpAction = item as ApplicationJumpTaskWrap;
                    if (jumpAction != null)
                    {
                        this.jumpActionsManager.RegisterAction(jumpAction, this.CommandLineArgumentPrefix, new Func<string>(this.GetLauncherPath));
                    }
                }
                enumerable2 = (this.nativeJumpList.Apply() ?? new Tuple<JumpItem, JumpItemRejectionReason>[0]).Select<Tuple<JumpItem, JumpItemRejectionReason>, RejectedApplicationJumpItem>((<>c.<>9__44_0 ??= i => new RejectedApplicationJumpItem(ApplicationJumpItemWrapper.Unwrap(i.Item1), i.Item2))).ToArray<RejectedApplicationJumpItem>();
            }
            finally
            {
                this.jumpActionsManager.EndUpdate();
            }
            return enumerable2;
        }

        protected virtual void ClearItems()
        {
            if (InteractionHelper.IsInDesignMode(this))
            {
                this.designModeItems.Clear();
            }
            else
            {
                this.nativeJumpList.Clear();
            }
        }

        protected virtual bool ContainsItem(ApplicationJumpItemInfo item) => 
            !InteractionHelper.IsInDesignMode(this) ? this.nativeJumpList.Contains(ApplicationJumpItemWrapper.Wrap(item)) : this.designModeItems.Contains(item);

        private IIconStorage CreateDefaultIconStorage() => 
            new DevExpress.Mvvm.UI.IconStorage(new Func<Uri>(this.GetBaseUri));

        void IApplicationJumpListImplementation.AddItem(ApplicationJumpItemInfo item)
        {
            this.AddItem(item);
        }

        bool IApplicationJumpListImplementation.AddOrReplace(ApplicationJumpTaskInfo jumpTask) => 
            this.AddOrReplace(jumpTask);

        void IApplicationJumpListImplementation.ClearItems()
        {
            this.ClearItems();
        }

        bool IApplicationJumpListImplementation.ContainsItem(ApplicationJumpItemInfo item) => 
            this.ContainsItem(item);

        ApplicationJumpTaskInfo IApplicationJumpListImplementation.Find(string commandId) => 
            this.Find(commandId);

        ApplicationJumpItemInfo IApplicationJumpListImplementation.GetItem(int index) => 
            this.GetItem(index);

        IEnumerable<ApplicationJumpItemInfo> IApplicationJumpListImplementation.GetItems() => 
            this.GetItems();

        int IApplicationJumpListImplementation.IndexOfItem(ApplicationJumpItemInfo item) => 
            this.IndexOfItem(item);

        void IApplicationJumpListImplementation.InsertItem(int index, ApplicationJumpItemInfo item)
        {
            this.InsertItem(index, item);
        }

        int IApplicationJumpListImplementation.ItemsCount() => 
            this.ItemsCount();

        bool IApplicationJumpListImplementation.RemoveItem(ApplicationJumpItemInfo item) => 
            this.RemoveItem(item);

        void IApplicationJumpListImplementation.RemoveItemAt(int index)
        {
            this.RemoveItemAt(index);
        }

        void IApplicationJumpListImplementation.SetItem(int index, ApplicationJumpItemInfo item)
        {
            this.SetItem(index, item);
        }

        protected virtual ApplicationJumpTaskInfo Find(string commandId)
        {
            if (InteractionHelper.IsInDesignMode(this))
            {
                return null;
            }
            ApplicationJumpTaskWrap wrap = (ApplicationJumpTaskWrap) this.nativeJumpList.Find(commandId);
            return wrap?.ApplicationJumpTask;
        }

        private static FreezableCollection<ApplicationJumpItem> GetInternalItems(DependencyObject obj) => 
            (FreezableCollection<ApplicationJumpItem>) obj.GetValue(InternalItemsProperty);

        protected virtual ApplicationJumpItemInfo GetItem(int index) => 
            !InteractionHelper.IsInDesignMode(this) ? ApplicationJumpItemWrapper.Unwrap(this.nativeJumpList[index]) : this.designModeItems[index];

        protected virtual IEnumerable<ApplicationJumpItemInfo> GetItems()
        {
            if (InteractionHelper.IsInDesignMode(this))
            {
                return this.designModeItems;
            }
            Func<JumpItem, ApplicationJumpItemInfo> selector = <>c.<>9__58_0;
            if (<>c.<>9__58_0 == null)
            {
                Func<JumpItem, ApplicationJumpItemInfo> local1 = <>c.<>9__58_0;
                selector = <>c.<>9__58_0 = i => ApplicationJumpItemWrapper.Unwrap(i);
            }
            return this.nativeJumpList.Select<JumpItem, ApplicationJumpItemInfo>(selector);
        }

        protected virtual string GetLauncherPath()
        {
            if (!string.IsNullOrEmpty(this.CustomLauncherPath))
            {
                return NativeResourceManager.ExpandVariables(this.CustomLauncherPath);
            }
            string path = Path.Combine(NativeResourceManager.ExpandVariables(this.DefaultLauncherStorageFolder), "DevExpress.Mvvm.UI.ApplicationJumpTaskLauncher.v19.2.exe");
            if (!File.Exists(path) || (NativeResourceManager.GetFileTime(path) <= NativeResourceManager.GetApplicationCreateTime()))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                Stream stream = AssemblyHelper.GetEmbeddedResourceStream(typeof(JumpActionsManager).Assembly, "DevExpress.Mvvm.UI.ApplicationJumpTaskLauncher.exe", true);
                try
                {
                    File.WriteAllBytes(path, stream.CopyAllBytes());
                }
                catch (IOException)
                {
                }
                catch (UnauthorizedAccessException)
                {
                }
            }
            return path;
        }

        protected virtual int IndexOfItem(ApplicationJumpItemInfo item) => 
            !InteractionHelper.IsInDesignMode(this) ? this.nativeJumpList.IndexOf(ApplicationJumpItemWrapper.Wrap(item)) : this.designModeItems.IndexOf(item);

        protected virtual void InsertItem(int index, ApplicationJumpItemInfo item)
        {
            if (InteractionHelper.IsInDesignMode(this))
            {
                this.designModeItems.Insert(index, item);
            }
            else
            {
                this.nativeJumpList.Insert(index, this.PrepareItem(item, null));
            }
        }

        protected virtual int ItemsCount() => 
            !InteractionHelper.IsInDesignMode(this) ? this.nativeJumpList.Count : this.designModeItems.Count;

        protected override void OnAttached()
        {
            if (this.Items.SourceItems.Any<ApplicationJumpItem>())
            {
                this.Apply();
            }
            base.OnAttached();
            base.AssociatedObject.SetValue(InternalItemsProperty, this.Items.SourceItems);
        }

        protected override void OnDetaching()
        {
            base.AssociatedObject.SetValue(InternalItemsProperty, null);
            base.OnDetaching();
        }

        private JumpItem PrepareItem(ApplicationJumpItemInfo item, JumpItem itemToReplace)
        {
            JumpItem item2 = ApplicationJumpItemWrapper.Wrap(item);
            ApplicationJumpTaskWrap task = item2 as ApplicationJumpTaskWrap;
            if (task != null)
            {
                this.PrepareTask(task);
                JumpItem objA = this.nativeJumpList.Find(task.ApplicationJumpTask.CommandId);
                if ((objA != null) && !ReferenceEquals(objA, itemToReplace))
                {
                    throw new ApplicationJumpTaskDuplicateCommandIdException();
                }
            }
            ((ISupportInitialize) item).EndInit();
            return item2;
        }

        private void PrepareTask(ApplicationJumpTaskWrap task)
        {
            IApplicationJumpTaskInfoInternal applicationJumpTask = task.ApplicationJumpTask;
            if (!applicationJumpTask.IsInitialized)
            {
                string str;
                if (task.ApplicationJumpTask.Icon == null)
                {
                    task.IconResourcePath = task.ApplicationJumpTask.IconResourcePath;
                    task.IconResourceIndex = task.ApplicationJumpTask.IconResourceIndex;
                    str = $"{task.IconResourcePath}_{task.IconResourceIndex}";
                }
                else
                {
                    string str2;
                    if (task.ApplicationJumpTask.IconResourcePath != null)
                    {
                        throw new ApplicationJumpTaskBothIconAndIconResourcePathSpecifiedException();
                    }
                    if (!this.IconStorage.TryStoreIconToFile(task.ApplicationJumpTask.Icon, NativeResourceManager.ExpandVariables(this.IconStorageFolder), out str, out str2))
                    {
                        throw new ApplicationJumpTaskInvalidIconException();
                    }
                    task.IconResourcePath = str2;
                    task.IconResourceIndex = 0;
                }
                if (task.ApplicationJumpTask.CommandId == null)
                {
                    applicationJumpTask.SetAutoGeneratedCommandId($"{task.ApplicationJumpTask.CustomCategory}${task.ApplicationJumpTask.Title}${str}");
                }
            }
        }

        protected virtual bool RemoveItem(ApplicationJumpItemInfo item) => 
            !InteractionHelper.IsInDesignMode(this) ? this.nativeJumpList.Remove(ApplicationJumpItemWrapper.Wrap(item)) : this.designModeItems.Remove(item);

        protected virtual void RemoveItemAt(int index)
        {
            if (InteractionHelper.IsInDesignMode(this))
            {
                this.designModeItems.RemoveAt(index);
            }
            else
            {
                this.nativeJumpList.RemoveAt(index);
            }
        }

        private static void SetInternalItems(DependencyObject obj, FreezableCollection<ApplicationJumpItem> value)
        {
            obj.SetValue(InternalItemsProperty, value);
        }

        protected virtual void SetItem(int index, ApplicationJumpItemInfo item)
        {
            if (InteractionHelper.IsInDesignMode(this))
            {
                this.designModeItems[index] = item;
            }
            else
            {
                this.nativeJumpList[index] = this.PrepareItem(item, this.nativeJumpList[index]);
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ApplicationJumpItemCollection Items { get; private set; }

        public string CommandLineArgumentPrefix
        {
            get => 
                (string) base.GetValue(CommandLineArgumentPrefixProperty);
            set => 
                base.SetValue(CommandLineArgumentPrefixProperty, value);
        }

        public string DefaultLauncherStorageFolder
        {
            get => 
                (string) base.GetValue(DefaultLauncherStorageFolderProperty);
            set => 
                base.SetValue(DefaultLauncherStorageFolderProperty, value);
        }

        public string CustomLauncherPath
        {
            get => 
                (string) base.GetValue(CustomLauncherPathProperty);
            set => 
                base.SetValue(CustomLauncherPathProperty, value);
        }

        public IIconStorage IconStorage
        {
            get => 
                (IIconStorage) base.GetValue(IconStorageProperty);
            set => 
                base.SetValue(IconStorageProperty, value);
        }

        public string IconStorageFolder
        {
            get => 
                (string) base.GetValue(IconStorageFolderProperty);
            set => 
                base.SetValue(IconStorageFolderProperty, value);
        }

        public virtual bool ShowFrequentCategory
        {
            get => 
                InteractionHelper.IsInDesignMode(this) ? this.designModeShowFrequentCategory : this.nativeJumpList.ShowFrequentCategory;
            set
            {
                if (InteractionHelper.IsInDesignMode(this))
                {
                    this.designModeShowFrequentCategory = value;
                }
                else
                {
                    this.nativeJumpList.ShowFrequentCategory = value;
                }
            }
        }

        public virtual bool ShowRecentCategory
        {
            get => 
                InteractionHelper.IsInDesignMode(this) ? this.designModeShowRecentCategory : this.nativeJumpList.ShowRecentCategory;
            set
            {
                if (InteractionHelper.IsInDesignMode(this))
                {
                    this.designModeShowRecentCategory = value;
                }
                else
                {
                    this.nativeJumpList.ShowRecentCategory = value;
                }
            }
        }

        IApplicationJumpList IApplicationJumpListService.Items =>
            this.jumpList;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ApplicationJumpListService.<>c <>9 = new ApplicationJumpListService.<>c();
            public static Func<Tuple<JumpItem, JumpItemRejectionReason>, RejectedApplicationJumpItem> <>9__44_0;
            public static Func<JumpItem, ApplicationJumpItemInfo> <>9__58_0;

            internal object <.cctor>b__79_0(DependencyObject d, object v) => 
                v ?? ((ApplicationJumpListService) d).CreateDefaultIconStorage();

            internal RejectedApplicationJumpItem <Apply>b__44_0(Tuple<JumpItem, JumpItemRejectionReason> i) => 
                new RejectedApplicationJumpItem(ApplicationJumpItemWrapper.Unwrap(i.Item1), i.Item2);

            internal ApplicationJumpItemInfo <GetItems>b__58_0(JumpItem i) => 
                ApplicationJumpItemWrapper.Unwrap(i);
        }
    }
}

