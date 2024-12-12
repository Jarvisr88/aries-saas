namespace DevExpress.XtraReports.Native
{
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.Native.Lines;
    using DevExpress.XtraReports.Native.Parameters;
    using DevExpress.XtraReports.Parameters;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public abstract class ParameterLineControllerBase : BaseLineController
    {
        private readonly ParameterPropertyDescriptor propertyDescriptor;

        public ParameterLineControllerBase(DevExpress.XtraReports.Parameters.Parameter parameter, object obj)
        {
            this.<Parameter>k__BackingField = parameter;
            this.propertyDescriptor = new ParameterPropertyDescriptor(parameter, PrintingSettings.ParameterPanelResetMode == ParameterPanelResetMode.RestoreOriginalValue);
            this.<Obj>k__BackingField = obj;
        }

        public static bool CanReset(IList<ParameterLineControllerBase> lineControllers)
        {
            if (lineControllers != null)
            {
                using (IEnumerator<ParameterLineControllerBase> enumerator = lineControllers.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        ParameterLineControllerBase current = enumerator.Current;
                        if (current.propertyDescriptor.CanReset())
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static void Commit(IList<ParameterLineControllerBase> lineControllers)
        {
            foreach (ParameterLineControllerBase base2 in lineControllers)
            {
                base2.propertyDescriptor.Commit();
            }
        }

        protected ITypeDescriptorContext CreateContext() => 
            new RuntimeTypeDescriptorContext(this.propertyDescriptor, this.Obj);

        protected TypeStringConverter CreateStringConverter() => 
            new TypeStringConverter(this.propertyDescriptor.Converter, this.CreateContext());

        protected override void InitLine()
        {
            base.InitLine();
            base.fLine.SetText(string.IsNullOrEmpty(this.Parameter.Description) ? this.Parameter.Name : this.Parameter.Description);
        }

        public static void Reset(IList<ParameterLineControllerBase> lineControllers)
        {
            foreach (ParameterLineControllerBase base2 in lineControllers)
            {
                base2.propertyDescriptor.Reset();
            }
        }

        public DevExpress.XtraReports.Parameters.Parameter Parameter { get; }

        protected System.ComponentModel.PropertyDescriptor PropertyDescriptor =>
            this.propertyDescriptor;

        protected object Obj { get; }
    }
}

