namespace DevExpress.XtraReports.Parameters
{
    using DevExpress.Data;
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraEditors.Filtering;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    [Designer("DevExpress.XtraReports.Design.RangeBoundaryParameterDesigner,DevExpress.XtraReports.v19.2.Extensions, Version=19.2.9.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a")]
    public abstract class RangeBoundaryParameter : Parameter, IRangeBoundaryParameter, IParameter, IFilterParameter, IObject
    {
        protected RangeBoundaryParameter()
        {
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), XtraSerializableProperty(XtraSerializationVisibility.Hidden)]
        public bool AllowNull
        {
            get
            {
                bool? nullable2;
                bool? nullable1;
                RangeParametersSettings ownerValueSource = this.OwnerValueSource;
                if (ownerValueSource == null)
                {
                    RangeParametersSettings local1 = ownerValueSource;
                    nullable2 = null;
                    nullable1 = nullable2;
                }
                else
                {
                    Parameter parameter = ownerValueSource.Parameter;
                    if (parameter != null)
                    {
                        nullable1 = new bool?(parameter.AllowNull);
                    }
                    else
                    {
                        Parameter local2 = parameter;
                        nullable2 = null;
                        nullable1 = nullable2;
                    }
                }
                bool? nullable = nullable1;
                return ((nullable != null) ? nullable.GetValueOrDefault() : false);
            }
            set
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), XtraSerializableProperty(XtraSerializationVisibility.Hidden)]
        public bool Visible
        {
            get => 
                false;
            set
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), XtraSerializableProperty(XtraSerializationVisibility.Hidden)]
        public bool MultiValue
        {
            get => 
                false;
            set
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), XtraSerializableProperty(XtraSerializationVisibility.Hidden)]
        public object Tag
        {
            get => 
                string.Empty;
            set
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), XtraSerializableProperty(XtraSerializationVisibility.Hidden)]
        public string Description
        {
            get => 
                string.Empty;
            set
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), XtraSerializableProperty(XtraSerializationVisibility.Hidden)]
        public override DevExpress.XtraReports.Parameters.ValueSourceSettings ValueSourceSettings
        {
            get => 
                null;
            set
            {
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), XtraSerializableProperty(-1)]
        public string ObjectType
        {
            get
            {
                System.Type type = base.GetType();
                return $"{type.FullName}, {type.Assembly.GetName().Name}";
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), XtraSerializableProperty(XtraSerializationVisibility.Hidden)]
        public override System.Type Type
        {
            get
            {
                object type;
                RangeParametersSettings ownerValueSource = this.OwnerValueSource;
                if (ownerValueSource == null)
                {
                    RangeParametersSettings local1 = ownerValueSource;
                    type = null;
                }
                else
                {
                    Parameter parameter = ownerValueSource.Parameter;
                    if (parameter != null)
                    {
                        type = parameter.Type;
                    }
                    else
                    {
                        Parameter local2 = parameter;
                        type = null;
                    }
                }
                object local3 = type;
                object obj2 = local3;
                if (local3 == null)
                {
                    object local4 = local3;
                    obj2 = typeof(string);
                }
                return (System.Type) obj2;
            }
            set => 
                base.Type = value;
        }

        internal RangeParametersSettings OwnerValueSource { get; set; }

        internal override bool IsLoading
        {
            get
            {
                bool? nullable2;
                bool? nullable1;
                RangeParametersSettings ownerValueSource = this.OwnerValueSource;
                if (ownerValueSource == null)
                {
                    RangeParametersSettings local1 = ownerValueSource;
                    nullable2 = null;
                    nullable1 = nullable2;
                }
                else
                {
                    Parameter parameter = ownerValueSource.Parameter;
                    if (parameter != null)
                    {
                        nullable1 = new bool?(parameter.IsLoading);
                    }
                    else
                    {
                        Parameter local2 = parameter;
                        nullable2 = null;
                        nullable1 = nullable2;
                    }
                }
                bool? nullable = nullable1;
                return ((nullable != null) ? nullable.GetValueOrDefault() : true);
            }
        }
    }
}

