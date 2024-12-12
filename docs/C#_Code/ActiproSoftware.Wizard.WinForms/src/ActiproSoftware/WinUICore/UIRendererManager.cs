namespace ActiproSoftware.WinUICore
{
    using ActiproSoftware.ComponentModel;
    using ActiproSoftware.Drawing;
    using Microsoft.Win32;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Threading;

    [DefaultProperty("ColorSchemeType"), ToolboxBitmap(typeof(UIRendererManager)), ToolboxItem(true)]
    public class UIRendererManager : Component
    {
        private static WindowsColorScheme #w2d;
        private static HybridDictionary #Sve;

        private static void #4ae(object #xhb, UserPreferenceChangedEventArgs #yhb)
        {
            if (#yhb.Category == UserPreferenceCategory.Color)
            {
                ColorScheme.Initialize();
                #Eye();
            }
        }

        private static void #Eye()
        {
            if (#Sve != null)
            {
                foreach (Type type in #Sve.Keys)
                {
                    ((RendererData) #Sve[type]).#Gye(((RendererData) #Sve[type]).#Uve.CreateRenderer());
                    ((RendererData) #Sve[type]).#Fye(EventArgs.Empty);
                }
            }
        }

        static UIRendererManager()
        {
            SystemEvents.UserPreferenceChanged += new UserPreferenceChangedEventHandler(UIRendererManager.#4ae);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void DecrementUsageCount(Type rendererType)
        {
            if (#Sve != null)
            {
                if (#Sve.Contains(rendererType))
                {
                    RendererData data = (RendererData) #Sve[rendererType];
                    data.#Wve = Math.Max(0, data.#Wve - 1);
                    if (!data.#Vve && (data.#Wve == 0))
                    {
                        data.#Gye(null);
                        #Sve.Remove(rendererType);
                    }
                }
                if (#Sve.Count == 0)
                {
                    #Sve = null;
                }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static RendererData GetRendererData(Type rendererType) => 
            (#Sve != null) ? ((RendererData) #Sve[rendererType]) : null;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void IncrementUsageCount(Type rendererType, IUIRendererFactory rendererFactory)
        {
            if (0 == 0)
            {
                RegisterRendererFactory(0, rendererFactory, (bool) rendererType);
            }
            else
            {
                Type local1 = rendererType;
                IUIRendererFactory local2 = rendererFactory;
            }
            RendererData data1 = (RendererData) #Sve[rendererType];
            data1.#Wve++;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void RegisterRendererFactory(Type rendererType, IUIRendererFactory rendererFactory, bool overwrite)
        {
            #Sve ??= new HybridDictionary();
            if (!#Sve.Contains(rendererType))
            {
                RendererData data = new RendererData {
                    #Vve = overwrite,
                    #Uve = rendererFactory
                };
                data.#Gye(rendererFactory.CreateRenderer());
                #Sve[rendererType] = data;
            }
            else if (overwrite)
            {
                RendererData data1 = (RendererData) #Sve[rendererType];
                data1.#Vve = overwrite;
                data1.#Uve = rendererFactory;
                data1.#Gye(rendererFactory.CreateRenderer());
            }
        }

        public static WindowsColorScheme ColorScheme
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                #w2d ??= WindowsColorScheme.WindowsDefault;
                return #w2d;
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            set
            {
                if (!ReferenceEquals(#w2d, value))
                {
                    #w2d = value;
                    #Eye();
                }
            }
        }

        [Category("Data"), Description("The type of color scheme to use for the renderers."), DefaultValue(0)]
        public WindowsColorSchemeType ColorSchemeType
        {
            get => 
                ColorScheme.BaseColorSchemeType;
            set => 
                ColorScheme = WindowsColorScheme.GetColorScheme(value);
        }

        internal class #0qe : WeakReference
        {
            public #0qe(object target) : base(target)
            {
            }

            public override bool Equals(object #QOd)
            {
                WeakReference reference = #QOd as UIRendererManager.#0qe;
                return ((reference != null) ? ((this.IsAlive == reference.IsAlive) && (this.Target == reference.Target)) : false);
            }

            public override int GetHashCode() => 
                this.GetHashCode();
        }

        public class RendererData
        {
            private List<UIRendererManager.#0qe> #Tve = new List<UIRendererManager.#0qe>();
            private IUIRenderer #IZd;
            internal IUIRendererFactory #Uve;
            internal bool #Vve;
            internal int #Wve;

            public event EventHandler RendererPropertyChanged;

            internal void #Fye(EventArgs #yhb)
            {
                if (this.#Xve != null)
                {
                    this.#Xve(this, #yhb);
                }
                if (this.#Tve != null)
                {
                    List<UIRendererManager.#0qe> list = this.#Tve;
                    lock (list)
                    {
                        int index = this.#Tve.Count - 1;
                        while (true)
                        {
                            while (true)
                            {
                                if (index >= 0)
                                {
                                    UIRendererManager.#0qe #qe = this.#Tve[index];
                                    if (#qe.IsAlive)
                                    {
                                        IWeakEventListener target = #qe.Target as IWeakEventListener;
                                        if (target != null)
                                        {
                                            target.ReceiveWeakEvent(typeof(UIRendererManager.RendererData), this, #yhb);
                                            break;
                                        }
                                    }
                                    this.#Tve.RemoveAt(index);
                                }
                                else
                                {
                                    return;
                                }
                                break;
                            }
                            index--;
                        }
                    }
                }
            }

            internal void #Gye(IUIRenderer #Ld)
            {
                if (!ReferenceEquals(this.#IZd, #Ld))
                {
                    if (this.#IZd != null)
                    {
                        this.#IZd.PropertyChanged -= new EventHandler(this.#J6d);
                        if (this.#IZd is IDisposable)
                        {
                            ((IDisposable) this.#IZd).Dispose();
                        }
                    }
                    this.#IZd = #Ld;
                    if (this.#IZd != null)
                    {
                        this.#IZd.PropertyChanged += new EventHandler(this.#J6d);
                    }
                }
            }

            private void #J6d(object #xhb, EventArgs #yhb)
            {
                this.#Fye(#yhb);
            }

            public void AddRendererPropertyChangedListener(IWeakEventListener listener)
            {
                if (listener != null)
                {
                    List<UIRendererManager.#0qe> list = this.#Tve;
                    lock (list)
                    {
                        UIRendererManager.#0qe item = new UIRendererManager.#0qe(listener);
                        if (!this.#Tve.Contains(item))
                        {
                            this.#Tve.Insert(0, item);
                        }
                    }
                }
            }

            public void RemoveRendererPropertyChangedListener(IWeakEventListener listener)
            {
                if (listener != null)
                {
                    List<UIRendererManager.#0qe> list = this.#Tve;
                    lock (list)
                    {
                        UIRendererManager.#0qe item = new UIRendererManager.#0qe(listener);
                        this.#Tve.Remove(item);
                    }
                }
            }

            public IUIRenderer Renderer =>
                this.#IZd;
        }
    }
}

