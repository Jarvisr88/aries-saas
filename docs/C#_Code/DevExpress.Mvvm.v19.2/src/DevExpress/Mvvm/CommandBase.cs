namespace DevExpress.Mvvm
{
    using System;

    public abstract class CommandBase
    {
        private static bool defaultUseCommandManager = true;

        protected CommandBase()
        {
        }

        public static bool DefaultUseCommandManager
        {
            get => 
                defaultUseCommandManager;
            set => 
                defaultUseCommandManager = value;
        }
    }
}

