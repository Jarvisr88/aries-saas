namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Runtime.InteropServices;

    internal sealed class DefaultDisplayTextServiceFactory : IDisplayTextServiceFactory
    {
        internal static readonly IDisplayTextServiceFactory Instance = new DefaultDisplayTextServiceFactory();

        private DefaultDisplayTextServiceFactory()
        {
        }

        IDisplayTextService IDisplayTextServiceFactory.Create(string path) => 
            DefaultDisplayTextService.Instance;

        bool IDisplayTextServiceFactory.TryCreate(string path, out IDisplayTextService service)
        {
            service = DefaultDisplayTextService.Instance;
            return true;
        }

        internal static bool IsNullOrDefault(IDisplayTextService service) => 
            (service == null) || ReferenceEquals(service, DefaultDisplayTextService.Instance);

        private sealed class DefaultDisplayTextService : IDisplayTextService
        {
            internal static readonly IDisplayTextService Instance = new DefaultDisplayTextServiceFactory.DefaultDisplayTextService();

            private DefaultDisplayTextService()
            {
            }

            string IDisplayTextService.GetCaption() => 
                null;

            string IDisplayTextService.GetDescription() => 
                null;

            string IDisplayTextService.GetDisplayText(object value) => 
                (value != null) ? value.ToString() : string.Empty;

            string IDisplayTextService.GetEditMask(out int maskType)
            {
                maskType = 0;
                return string.Empty;
            }

            object IDisplayTextService.GetHtmlImages() => 
                null;

            DisplayFormat IDisplayTextService.DisplayFormat =>
                DisplayFormat.None;

            AutoHeight IDisplayTextService.AutoHeight =>
                AutoHeight.None;
        }
    }
}

