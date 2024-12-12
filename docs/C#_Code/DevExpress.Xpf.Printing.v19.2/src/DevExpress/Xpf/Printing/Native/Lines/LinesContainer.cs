namespace DevExpress.Xpf.Printing.Native.Lines
{
    using DevExpress.Mvvm.UI.Interactivity;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class LinesContainer : DXContentPresenter
    {
        private const int headerColumnIndex = 0;
        private const int editorColumnIndex = 2;
        private const int separatorColumnWidth = 4;
        private const int editorColumnMinWidth = 60;
        private const int rowSpacerHeight = 4;
        public static readonly DependencyProperty LinesProperty;

        static LinesContainer()
        {
            LinesProperty = DependencyPropertyManager.Register("Lines", typeof(IEnumerable<LineBase>), typeof(LinesContainer), new FrameworkPropertyMetadata(null, (d, e) => ((LinesContainer) d).OnLinesChanged((LineBase[]) e.NewValue)));
        }

        private static void AddInvisibleBorder(Grid grid, LineBase line, int row)
        {
            ComboBoxPropertyLine editorLine = line as ComboBoxPropertyLine;
            if ((editorLine != null) && !editorLine.IsDropDownMode)
            {
                Border element = new Border();
                element.SizeChanged += (o, e) => SyncWidth(editorLine.Editor, e.NewSize.Width);
                element.SetValue(Grid.RowProperty, row);
                element.SetValue(Grid.ColumnProperty, 2);
                grid.Children.Add(element);
            }
        }

        private static void AddRow(Grid grid, LineBase line, int row)
        {
            grid.RowDefinitions.Add(new RowDefinition());
            if (line.Header == null)
            {
                line.Content.SetValue(Grid.ColumnProperty, 0);
                line.Content.SetValue(Grid.ColumnSpanProperty, grid.ColumnDefinitions.Count);
            }
            else
            {
                line.Header.SetValue(Grid.RowProperty, row);
                line.Header.SetValue(Grid.ColumnProperty, 0);
                grid.Children.Add(line.Header);
                line.Content.SetValue(Grid.ColumnProperty, 2);
            }
            line.Content.SetValue(Grid.RowProperty, row);
            grid.Children.Add(line.Content);
            AddInvisibleBorder(grid, line, row);
        }

        private void AddRowSpacer(Grid grid)
        {
            RowDefinition definition1 = new RowDefinition();
            definition1.Height = new GridLength(4.0);
            grid.RowDefinitions.Add(definition1);
        }

        private void OnLinesChanged(LineBase[] lines)
        {
            Window owner = Window.GetWindow(this);
            this.SetLines(lines, owner);
        }

        private void OnLineValueChanged(object sender, EventArgs e)
        {
            RefreshPropertiesAttribute attribute = (RefreshPropertiesAttribute) ((PropertyLine) sender).Property.Attributes[typeof(RefreshPropertiesAttribute)];
            if ((attribute != null) && (attribute.RefreshProperties == RefreshProperties.All))
            {
                foreach (LineBase base2 in this.Lines)
                {
                    base2.RefreshContent();
                }
            }
        }

        private void SetLines(LineBase[] lines, Window owner)
        {
            if (lines == null)
            {
                throw new ArgumentNullException("lines");
            }
            LinesGrid d = new LinesGrid();
            AutoWidthColumnBehavior behavior1 = new AutoWidthColumnBehavior();
            behavior1.AutoWidthColumnIndex = 0;
            Interaction.GetBehaviors(d).Add(behavior1);
            ColumnDefinition definition1 = new ColumnDefinition();
            definition1.Width = new GridLength(0.0);
            d.ColumnDefinitions.Add(definition1);
            ColumnDefinition definition2 = new ColumnDefinition();
            definition2.Width = new GridLength(4.0);
            d.ColumnDefinitions.Add(definition2);
            ColumnDefinition definition3 = new ColumnDefinition();
            definition3.Width = new GridLength(1.0, GridUnitType.Star);
            definition3.MinWidth = 60.0;
            d.ColumnDefinitions.Add(definition3);
            foreach (LineBase base2 in lines)
            {
                if (base2 is EmptyLine)
                {
                    this.AddRowSpacer(d);
                    this.AddRowSpacer(d);
                }
                else
                {
                    AddRow(d, base2, d.RowDefinitions.Count);
                }
                this.AddRowSpacer(d);
                base2.RefreshContent();
                PropertyLine line = base2 as PropertyLine;
                if (line != null)
                {
                    line.ValueChanged += new EventHandler(this.OnLineValueChanged);
                }
                ButtonEditPropertyLine line2 = base2 as ButtonEditPropertyLine;
                if (line2 != null)
                {
                    line2.OwnerWindow = owner;
                }
            }
            base.Content = d;
        }

        private static void SyncWidth(FrameworkElement element, double width)
        {
            element.Width = Math.Max((double) 0.0, (double) ((width - element.Margin.Left) - element.Margin.Right));
        }

        public IEnumerable<LineBase> Lines
        {
            get => 
                (IEnumerable<LineBase>) base.GetValue(LinesProperty);
            set => 
                base.SetValue(LinesProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LinesContainer.<>c <>9 = new LinesContainer.<>c();

            internal void <.cctor>b__9_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((LinesContainer) d).OnLinesChanged((LineBase[]) e.NewValue);
            }
        }
    }
}

