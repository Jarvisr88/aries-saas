namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using DevExpress.Utils;
    using System;
    using System.Windows;

    public class NativeDragManager
    {
        private readonly DragSubscriptionService subscriptionService;
        private readonly UIElement owner;
        private bool isActive;

        internal NativeDragManager(UIElement owner, DragSubscriptionService subscriptionService)
        {
            Guard.ArgumentNotNull(owner, "owner");
            Guard.ArgumentNotNull(subscriptionService, "subscriptionService");
            this.owner = owner;
            this.subscriptionService = subscriptionService;
        }

        private void OnIsActiveChanged()
        {
            if (this.IsActive)
            {
                this.owner.AllowDrop = true;
                this.subscriptionService.IsActive = true;
            }
            else
            {
                this.subscriptionService.IsActive = false;
                this.owner.ClearValue(UIElement.AllowDropProperty);
            }
        }

        public DragSubscriptionService SubscriptionService =>
            this.subscriptionService;

        internal UIElement Owner =>
            this.owner;

        public bool IsActive
        {
            get => 
                this.isActive;
            set
            {
                if (this.isActive != value)
                {
                    this.isActive = value;
                    this.OnIsActiveChanged();
                }
            }
        }
    }
}

