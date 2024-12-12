namespace ActiproSoftware.WinUICore
{
    using System;
    using System.ComponentModel;

    public class UIElementComponent : UIElement, IDisposable, IComponent
    {
        private ISite #Rve;

        private ISite System.ComponentModel.IComponent.Site
        {
            get => 
                this.#Rve;
            set => 
                this.#Rve = value;
        }
    }
}

