namespace DevExpress.Xpf.Editors.Filtering
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class FieldInOperationButtonControl : XPFContentControl
    {
        public static readonly DependencyProperty NodeProperty;
        public static readonly DependencyProperty OperatorPropertyNameProperty;
        public static readonly DependencyProperty ColumnCaptionProperty;

        static FieldInOperationButtonControl()
        {
            NodeProperty = DependencyPropertyManager.Register("Node", typeof(ClauseNode), typeof(FieldInOperationButtonControl), new PropertyMetadata(null, (d, e) => ((FieldInOperationButtonControl) d).UpdateColumnCaption()));
            OperatorPropertyNameProperty = DependencyPropertyManager.Register("OperatorPropertyName", typeof(string), typeof(FieldInOperationButtonControl), new PropertyMetadata(null, (d, e) => ((FieldInOperationButtonControl) d).UpdateColumnCaption()));
            ColumnCaptionProperty = DependencyPropertyManager.Register("ColumnCaption", typeof(object), typeof(FieldInOperationButtonControl), new PropertyMetadata(null));
        }

        private void UpdateColumnCaption()
        {
            if ((this.OperatorPropertyName == null) || (this.Node == null))
            {
                this.ColumnCaption = string.Empty;
            }
            else
            {
                FilterColumn columnByFieldName = this.Node.Owner.GetColumnByFieldName(this.OperatorPropertyName);
                if (columnByFieldName != null)
                {
                    this.ColumnCaption = columnByFieldName.ColumnCaption;
                }
            }
        }

        public ClauseNode Node
        {
            get => 
                (ClauseNode) base.GetValue(NodeProperty);
            set => 
                base.SetValue(NodeProperty, value);
        }

        public string OperatorPropertyName
        {
            get => 
                (string) base.GetValue(OperatorPropertyNameProperty);
            set => 
                base.SetValue(OperatorPropertyNameProperty, value);
        }

        public object ColumnCaption
        {
            get => 
                base.GetValue(ColumnCaptionProperty);
            set => 
                base.SetValue(ColumnCaptionProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FieldInOperationButtonControl.<>c <>9 = new FieldInOperationButtonControl.<>c();

            internal void <.cctor>b__14_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FieldInOperationButtonControl) d).UpdateColumnCaption();
            }

            internal void <.cctor>b__14_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FieldInOperationButtonControl) d).UpdateColumnCaption();
            }
        }
    }
}

