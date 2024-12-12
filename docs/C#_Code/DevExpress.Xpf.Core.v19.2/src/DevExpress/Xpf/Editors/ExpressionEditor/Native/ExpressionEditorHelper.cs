namespace DevExpress.Xpf.Editors.ExpressionEditor.Native
{
    using DevExpress.Data;
    using DevExpress.Data.Controls.ExpressionEditor;
    using DevExpress.Data.ExpressionEditor;
    using DevExpress.Data.Utils;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.ExpressionEditor;
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Imaging;

    public static class ExpressionEditorHelper
    {
        private static string expressionEditorControlTypeName = "DevExpress.Xpf.ExpressionEditor.ExpressionEditorControl";
        private static string expressionEditorContextHelperTypeName = "DevExpress.Xpf.ExpressionEditor.ExpressionEditorContextHelper";

        static ExpressionEditorHelper()
        {
            PreferStandardExpressionEditorControl = false;
        }

        public static IAutoCompleteExpressionEditor GetAutoCompleteExpressionEditorControl(IDataColumnInfo columnInfo, bool useAggregateFunctions = false)
        {
            if (PreferStandardExpressionEditorControl)
            {
                return null;
            }
            Assembly assembly = Helpers.LoadWithPartialName("DevExpress.Xpf.ExpressionEditor.v19.2, Version=19.2.9.0");
            if (assembly == null)
            {
                return null;
            }
            Type[] types = new Type[] { typeof(bool), typeof(bool), typeof(bool), typeof(IExpressionEditor) };
            MethodInfo method = assembly.GetType(expressionEditorContextHelperTypeName).GetMethod("GetContext", types);
            object[] parameters = new object[4];
            parameters[0] = useAggregateFunctions;
            parameters[1] = true;
            parameters[2] = false;
            ExpressionEditorContext context = (ExpressionEditorContext) method.Invoke(method, parameters);
            if (context == null)
            {
                return null;
            }
            if (columnInfo != null)
            {
                Func<IDataColumnInfo, ColumnInfo> selector = <>c.<>9__9_0;
                if (<>c.<>9__9_0 == null)
                {
                    Func<IDataColumnInfo, ColumnInfo> local1 = <>c.<>9__9_0;
                    selector = <>c.<>9__9_0 = delegate (IDataColumnInfo c) {
                        ColumnInfo info1 = new ColumnInfo();
                        info1.Name = c.Caption;
                        info1.Type = c.FieldType;
                        return info1;
                    };
                }
                IEnumerable<ColumnInfo> collection = columnInfo.Columns.Select<IDataColumnInfo, ColumnInfo>(selector);
                context.Columns.AddRange(collection);
            }
            Control control = (Control) Activator.CreateInstance(assembly.GetType(expressionEditorControlTypeName));
            control.GetType().GetProperty("Context").SetValue(control, context, null);
            ((ISupportExpressionString) control).SetExpressionString(columnInfo, columnInfo.UnboundExpression);
            return (control as IAutoCompleteExpressionEditor);
        }

        public static void ShowExpressionEditor(ExpressionEditorParams p)
        {
            MessageBoxResult? nullable;
            if (p.DialogWindow == null)
            {
                ThemedWindow window1 = new ThemedWindow();
                ThemedWindow window2 = new ThemedWindow();
                window2.Owner = (p.RootElement == null) ? null : Window.GetWindow(p.RootElement);
                p.DialogWindow = window2;
            }
            else if (p.RootElement != null)
            {
                p.DialogWindow.Owner ??= Window.GetWindow(p.RootElement);
            }
            p.DialogWindow.Title = EditorLocalizer.GetString(EditorStringId.ExpressionEditor_Title);
            p.DialogWindow.ShowInTaskbar = false;
            p.DialogWindow.Width = 600.0;
            p.DialogWindow.Height = 450.0;
            p.DialogWindow.MinHeight = 230.0;
            p.DialogWindow.MinWidth = 400.0;
            p.DialogWindow.WindowStyle = WindowStyle.ToolWindow;
            p.DialogWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            if ((p.RootElement != null) && ThemeHelper.IsTouchTheme(p.RootElement))
            {
                p.DialogWindow.Width = 950.0;
                p.DialogWindow.Height = 750.0;
            }
            ISupportExpressionString editorToShow = null;
            if (p.Mode != ExpressionEditorMode.Standard)
            {
                ISupportExpressionString autoCompleteEditor = p.AutoCompleteEditor;
                ISupportExpressionString autoCompleteExpressionEditorControl = autoCompleteEditor;
                if (autoCompleteEditor == null)
                {
                    ISupportExpressionString local1 = autoCompleteEditor;
                    autoCompleteExpressionEditorControl = GetAutoCompleteExpressionEditorControl(p.Column, false);
                }
                editorToShow = autoCompleteExpressionEditorControl;
            }
            if (editorToShow == null)
            {
                ISupportExpressionString standardEditor = p.StandardEditor;
                ISupportExpressionString text4 = standardEditor;
                if (standardEditor == null)
                {
                    ISupportExpressionString local2 = standardEditor;
                    text4 = new ExpressionEditorControl(p.Column);
                }
                editorToShow = text4;
            }
            ((FrameworkElement) editorToShow).Loaded += delegate (object s, RoutedEventArgs e) {
                if (p.CustomExpression != null)
                {
                    editorToShow.SetExpressionString(p.Column, p.CustomExpression);
                }
            };
            p.DialogWindow.Content = editorToShow;
            if (p.Theme != null)
            {
                ThemeManager.SetTheme(p.DialogWindow, p.Theme);
            }
            else if (p.DialogWindow.Owner != null)
            {
                Theme theme = ThemeManager.GetTheme(p.DialogWindow.Owner);
                if (theme == null)
                {
                    ThemeManager.SetThemeName(p.DialogWindow, ThemeManager.GetThemeName(p.DialogWindow.Owner));
                }
                else
                {
                    ThemeManager.SetTheme(p.DialogWindow, theme);
                }
            }
            if (p.DialogWindow is ThemedWindow)
            {
                nullable = null;
                (p.DialogWindow as ThemedWindow).ShowDialog(MessageBoxButton.OKCancel, nullable);
            }
            else if (p.DialogWindow is DXDialogWindow)
            {
                (p.DialogWindow as DXDialogWindow).SmallIcon = new BitmapImage(DevExpress.Xpf.Core.UriHelper.GetUri("DevExpress.Xpf.Core", "/Editors/Images/ExpressionEditor/expression.png", null));
                nullable = null;
                nullable = null;
                (p.DialogWindow as DXDialogWindow).ShowDialogWindow(MessageBoxButton.OKCancel, nullable, nullable);
            }
            p.ClosedHandler(((p.DialogWindow.DialogResult == null) || !p.DialogWindow.DialogResult.Value) ? null : editorToShow.GetExpressionString(p.Column));
        }

        public static void ShowExpressionEditor(ExpressionEditorCreatedEventArgsBase args, ExpressionEditorDialogClosedDelegate closedHandler, FrameworkElement rootElement = null)
        {
            ExpressionEditorParams p = new ExpressionEditorParams(args, closedHandler) {
                RootElement = rootElement
            };
            ShowExpressionEditor(p);
        }

        public static FloatingContainer ShowExpressionEditor(Control expressionEditorControl, FrameworkElement rootElement, DialogClosedDelegate closedHandler)
        {
            Size size = new Size(600.0, 450.0);
            if (ThemeHelper.IsTouchTheme(rootElement))
            {
                size = new Size(950.0, 750.0);
            }
            FloatingContainerParameters parameters = new FloatingContainerParameters();
            parameters.ClosedDelegate = closedHandler;
            parameters.Title = EditorLocalizer.GetString(EditorStringId.ExpressionEditor_Title);
            parameters.AllowSizing = true;
            parameters.CloseOnEscape = true;
            return FloatingContainer.ShowDialogContent(expressionEditorControl, rootElement, size, parameters);
        }

        public static void ShowExpressionEditor(Window dialogWindow, ExpressionEditorCreatedEventArgsBase args, ExpressionEditorDialogClosedDelegate closedHandler, FrameworkElement rootElement = null)
        {
            ExpressionEditorParams p = new ExpressionEditorParams(args, closedHandler) {
                DialogWindow = dialogWindow,
                RootElement = rootElement
            };
            ShowExpressionEditor(p);
        }

        public static bool PreferStandardExpressionEditorControl { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ExpressionEditorHelper.<>c <>9 = new ExpressionEditorHelper.<>c();
            public static Func<IDataColumnInfo, ColumnInfo> <>9__9_0;

            internal ColumnInfo <GetAutoCompleteExpressionEditorControl>b__9_0(IDataColumnInfo c)
            {
                ColumnInfo info1 = new ColumnInfo();
                info1.Name = c.Caption;
                info1.Type = c.FieldType;
                return info1;
            }
        }
    }
}

