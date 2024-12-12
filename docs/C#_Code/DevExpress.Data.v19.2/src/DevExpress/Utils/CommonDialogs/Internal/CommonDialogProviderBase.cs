namespace DevExpress.Utils.CommonDialogs.Internal
{
    using DevExpress.Utils;
    using DevExpress.Utils.CommonDialogs;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public abstract class CommonDialogProviderBase
    {
        private Dictionary<object, Dictionary<Type, ISlot<ICommonDialog>>> dialogFactories = new Dictionary<object, Dictionary<Type, ISlot<ICommonDialog>>>();
        private object standardDialogFactoriesKey = new object();
        private object defaultDialogFactoriesKey = new object();

        protected CommonDialogProviderBase()
        {
            this.RegisterStandardDialogsFactories();
            this.RegisterDefaultDialogsFactories();
        }

        public IFolderBrowserDialog CreateCustomFolderBrowserDialog(object key) => 
            this.CreateDialogCore<IFolderBrowserDialog>(key);

        public IOpenFileDialog CreateCustomOpenFileDialog(object key) => 
            this.CreateDialogCore<IOpenFileDialog>(key);

        public ISaveFileDialog CreateCustomSaveFileDialog(object key) => 
            this.CreateDialogCore<ISaveFileDialog>(key);

        public IFolderBrowserDialog CreateDefaultFolderBrowserDialog() => 
            this.CreateDialogCore<IFolderBrowserDialog>(this.defaultDialogFactoriesKey);

        public IOpenFileDialog CreateDefaultOpenFileDialog() => 
            this.CreateDialogCore<IOpenFileDialog>(this.defaultDialogFactoriesKey);

        public ISaveFileDialog CreateDefaultSaveFileDialog() => 
            this.CreateDialogCore<ISaveFileDialog>(this.defaultDialogFactoriesKey);

        private TDialog CreateDialogCore<TDialog>(object key) where TDialog: class, ICommonDialog
        {
            Type type = typeof(TDialog);
            if (this.dialogFactories.ContainsKey(key) && this.dialogFactories[key].ContainsKey(type))
            {
                return (this.dialogFactories[key][type].Creator() as TDialog);
            }
            return default(TDialog);
        }

        public IFolderBrowserDialog CreateStandardFolderBrowserDialog() => 
            this.CreateDialogCore<IFolderBrowserDialog>(this.standardDialogFactoriesKey);

        public IOpenFileDialog CreateStandardOpenFileDialog() => 
            this.CreateDialogCore<IOpenFileDialog>(this.standardDialogFactoriesKey);

        public ISaveFileDialog CreateStandardSaveFileDialog() => 
            this.CreateDialogCore<ISaveFileDialog>(this.standardDialogFactoriesKey);

        public void RegisterCustomFolderBrowserDialogFactory(object key, Func<IFolderBrowserDialog> factory, bool overwriteIfExists = true)
        {
            this.RegisterFactoryCore<IFolderBrowserDialog>(key, factory, overwriteIfExists);
        }

        public void RegisterCustomOpenFileDialogFactory(object key, Func<IOpenFileDialog> factory, bool overwriteIfExists = true)
        {
            this.RegisterFactoryCore<IOpenFileDialog>(key, factory, overwriteIfExists);
        }

        public void RegisterCustomSaveFileDialogFactory(object key, Func<ISaveFileDialog> factory, bool overwriteIfExists = true)
        {
            this.RegisterFactoryCore<ISaveFileDialog>(key, factory, overwriteIfExists);
        }

        private void RegisterDefaultDialogsFactories()
        {
            this.RegisterDefaultOpenFileDialogFactory(new Func<IOpenFileDialog>(this.CreateStandardOpenFileDialog), true);
            this.RegisterDefaultSaveFileDialogFactory(new Func<ISaveFileDialog>(this.CreateStandardSaveFileDialog), true);
            this.RegisterDefaultFolderBrowserDialogFactory(new Func<IFolderBrowserDialog>(this.CreateStandardFolderBrowserDialog), true);
        }

        public void RegisterDefaultFolderBrowserDialogFactory(Func<IFolderBrowserDialog> factory, bool overwriteIfExists = true)
        {
            this.RegisterFactoryCore<IFolderBrowserDialog>(this.defaultDialogFactoriesKey, factory, overwriteIfExists);
        }

        public void RegisterDefaultOpenFileDialogFactory(Func<IOpenFileDialog> factory, bool overwriteIfExists = true)
        {
            this.RegisterFactoryCore<IOpenFileDialog>(this.defaultDialogFactoriesKey, factory, overwriteIfExists);
        }

        public void RegisterDefaultSaveFileDialogFactory(Func<ISaveFileDialog> factory, bool overwriteIfExists = true)
        {
            this.RegisterFactoryCore<ISaveFileDialog>(this.defaultDialogFactoriesKey, factory, overwriteIfExists);
        }

        private void RegisterFactoryCore<TDialog>(object key, Func<TDialog> factory, bool overwriteIfExists) where TDialog: ICommonDialog
        {
            Guard.ArgumentNotNull(key, "controlKey");
            Guard.ArgumentNotNull(factory, "factory");
            Type type = typeof(TDialog);
            bool flag = this.dialogFactories.ContainsKey(key);
            if (!flag || overwriteIfExists)
            {
                if (!flag)
                {
                    Dictionary<Type, ISlot<ICommonDialog>> dictionary = new Dictionary<Type, ISlot<ICommonDialog>>();
                    this.dialogFactories.Add(key, dictionary);
                }
                this.dialogFactories[key][type] = new DevExpress.Utils.CommonDialogs.Internal.Slot<TDialog>(factory) as ISlot<ICommonDialog>;
            }
        }

        private void RegisterStandardDialogsFactories()
        {
            this.RegisterFactoryCore<IOpenFileDialog>(this.standardDialogFactoriesKey, new Func<IOpenFileDialog>(this.StandardOpenFileDialogFactory), true);
            this.RegisterFactoryCore<ISaveFileDialog>(this.standardDialogFactoriesKey, new Func<ISaveFileDialog>(this.StandardSaveFileDialogFactory), true);
            this.RegisterFactoryCore<IFolderBrowserDialog>(this.standardDialogFactoriesKey, new Func<IFolderBrowserDialog>(this.StandardFolderBrowserDialogFactory), true);
        }

        protected abstract IFolderBrowserDialog StandardFolderBrowserDialogFactory();
        protected abstract IOpenFileDialog StandardOpenFileDialogFactory();
        protected abstract ISaveFileDialog StandardSaveFileDialogFactory();
    }
}

