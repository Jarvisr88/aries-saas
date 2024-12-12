namespace DevExpress.Export.Xl
{
    using DevExpress.Office;
    using System;

    public class XlDifferentialFormatting : XlFormatting, ISupportsCopyFrom<XlDifferentialFormatting>
    {
        public void CopyFrom(XlDifferentialFormatting other)
        {
            if (other == null)
            {
                base.Font = null;
                base.Fill = null;
                base.Alignment = null;
                base.NetFormatString = null;
                base.IsDateTimeFormatString = false;
                base.Border = null;
                base.NumberFormat = null;
            }
            else
            {
                base.Font = CopyObject<XlFont>(other.Font);
                base.Fill = CopyObject<XlFill>(other.Fill);
                base.Alignment = CopyObject<XlCellAlignment>(other.Alignment);
                base.Border = CopyObject<XlBorder>(other.Border);
                base.NetFormatString = other.NetFormatString;
                base.IsDateTimeFormatString = other.IsDateTimeFormatString;
                base.NumberFormat = other.NumberFormat;
            }
        }

        internal static XlDifferentialFormatting ExcludeBorderFormatting(XlDifferentialFormatting formatting)
        {
            if (formatting == null)
            {
                return null;
            }
            if (formatting.Border == null)
            {
                return formatting;
            }
            XlDifferentialFormatting formatting2 = CopyObject<XlDifferentialFormatting>(formatting);
            formatting2.Border = null;
            return formatting2;
        }

        internal static XlDifferentialFormatting ExtractBorderFormatting(XlDifferentialFormatting formatting) => 
            formatting?.Border;

        public static XlDifferentialFormatting Merge(XlDifferentialFormatting target, XlDifferentialFormatting source)
        {
            if (target == null)
            {
                return CopyObject<XlDifferentialFormatting>(source);
            }
            target.MergeWith(source);
            return target;
        }

        private void MergeWith(XlDifferentialFormatting other)
        {
            if (other != null)
            {
                if (other.Font != null)
                {
                    base.Font = CopyObject<XlFont>(other.Font);
                }
                if (other.Fill != null)
                {
                    base.Fill = CopyObject<XlFill>(other.Fill);
                }
                if (other.Alignment != null)
                {
                    base.Alignment = CopyObject<XlCellAlignment>(other.Alignment);
                }
                if (other.Border != null)
                {
                    base.Border = CopyObject<XlBorder>(other.Border);
                }
                if (other.NetFormatString != null)
                {
                    base.NetFormatString = other.NetFormatString;
                    base.IsDateTimeFormatString = other.IsDateTimeFormatString;
                }
                if (other.NumberFormat != null)
                {
                    base.NumberFormat = other.NumberFormat;
                }
            }
        }

        public static implicit operator XlDifferentialFormatting(XlBorder border)
        {
            XlCellFormatting formatting = new XlCellFormatting {
                Border = border
            };
            return formatting.ToDifferentialFormatting();
        }

        public static implicit operator XlDifferentialFormatting(XlCellAlignment alignment)
        {
            XlCellFormatting formatting = new XlCellFormatting {
                Alignment = alignment
            };
            return formatting.ToDifferentialFormatting();
        }

        public static implicit operator XlDifferentialFormatting(XlCellFormatting formatting) => 
            formatting?.ToDifferentialFormatting();

        public static implicit operator XlDifferentialFormatting(XlFill fill)
        {
            XlCellFormatting formatting = new XlCellFormatting {
                Fill = fill
            };
            return formatting.ToDifferentialFormatting();
        }

        public static implicit operator XlDifferentialFormatting(XlFont font)
        {
            XlCellFormatting formatting = new XlCellFormatting {
                Font = font
            };
            return formatting.ToDifferentialFormatting();
        }

        public static implicit operator XlDifferentialFormatting(XlNumberFormat numberFormat)
        {
            XlCellFormatting formatting = new XlCellFormatting {
                NumberFormat = numberFormat
            };
            return formatting.ToDifferentialFormatting();
        }
    }
}

