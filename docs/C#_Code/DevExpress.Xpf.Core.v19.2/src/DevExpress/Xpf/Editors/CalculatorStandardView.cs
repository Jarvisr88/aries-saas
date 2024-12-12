namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;
    using System.Windows.Input;

    public class CalculatorStandardView : CalculatorViewBase
    {
        public static readonly DependencyProperty ButtonTypeProperty;
        public static readonly string DisplayFormat;

        static CalculatorStandardView()
        {
            string str = new string('#', 0x1d) + "0." + new string('#', 30);
            DisplayFormat = str + ";-" + str;
            ButtonTypeProperty = DependencyProperty.RegisterAttached("ButtonType", typeof(ButtonType), typeof(CalculatorStandardView), new PropertyMetadata(ButtonType.None, new PropertyChangedCallback(CalculatorStandardView.PropertyChangedButtonType)));
        }

        public CalculatorStandardView(ICalculatorViewOwner owner) : base(owner)
        {
        }

        protected override CalculatorStrategyBase CreateStrategy() => 
            new CalculatorStandardStrategy(this);

        protected override object GetButtonIDByKey(KeyEventArgs e)
        {
            Key key = e.Key;
            if (key > Key.Escape)
            {
                if ((key == Key.Decimal) || ((key == Key.OemComma) || (key == Key.OemPeriod)))
                {
                    return ButtonType.Decimal;
                }
            }
            else
            {
                if (key == Key.Back)
                {
                    return ButtonType.Back;
                }
                if (key != Key.Return)
                {
                    if (key == Key.Escape)
                    {
                        return ButtonType.Clear;
                    }
                }
                else if (ModifierKeysHelper.GetKeyboardModifiers(e) == ModifierKeys.None)
                {
                    return ButtonType.Equal;
                }
            }
            return null;
        }

        protected override object GetButtonIDByTextInput(TextCompositionEventArgs e)
        {
            string text = e.Text;
            uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
            if (num > 0x320ca3f6)
            {
                if (num > 0x360caa42)
                {
                    if (num > 0x380cad68)
                    {
                        if (num == 0x3c0cb3b4)
                        {
                            if (text == "9")
                            {
                                return ButtonType.Nine;
                            }
                        }
                        else if (num == 0x3d0cb547)
                        {
                            if (text == "8")
                            {
                                return ButtonType.Eight;
                            }
                        }
                        else if ((num == 0x3f0cb86d) && (text == ":"))
                        {
                            goto TR_0006;
                        }
                    }
                    else if (num == 0x370cabd5)
                    {
                        if (text == "2")
                        {
                            return ButtonType.Two;
                        }
                    }
                    else if ((num == 0x380cad68) && (text == "="))
                    {
                        goto TR_0000;
                    }
                }
                else if (num <= 0x340ca71c)
                {
                    if (num != 0x330ca589)
                    {
                        if ((num == 0x340ca71c) && (text == "1"))
                        {
                            return ButtonType.One;
                        }
                    }
                    else if (text == "6")
                    {
                        return ButtonType.Six;
                    }
                }
                else if (num != 0x350ca8af)
                {
                    if ((num == 0x360caa42) && (text == "3"))
                    {
                        return ButtonType.Three;
                    }
                }
                else if (text == "0")
                {
                    return ButtonType.Zero;
                }
                goto TR_0001;
            }
            else
            {
                if (num > 0x2a0c975e)
                {
                    if (num <= 0x2f0c9f3d)
                    {
                        if (num != 0x2e0c9daa)
                        {
                            if ((num == 0x2f0c9f3d) && (text == "*"))
                            {
                                return ButtonType.Mul;
                            }
                        }
                        else if (text == "+")
                        {
                            return ButtonType.Add;
                        }
                    }
                    else if (num == 0x300ca0d0)
                    {
                        if (text == "5")
                        {
                            return ButtonType.Five;
                        }
                    }
                    else if (num != 0x310ca263)
                    {
                        if ((num == 0x320ca3f6) && (text == "7"))
                        {
                            return ButtonType.Seven;
                        }
                    }
                    else if (text == "4")
                    {
                        return ButtonType.Four;
                    }
                }
                else if (num > 0x250c8f7f)
                {
                    if (num == 0x280c9438)
                    {
                        if (text == "-")
                        {
                            return ButtonType.Sub;
                        }
                    }
                    else if ((num == 0x2a0c975e) && (text == "/"))
                    {
                        goto TR_0006;
                    }
                }
                else if (num == 0x200c87a0)
                {
                    if (text == "%")
                    {
                        return ButtonType.Percent;
                    }
                }
                else if ((num == 0x250c8f7f) && (text == " "))
                {
                    goto TR_0000;
                }
                goto TR_0001;
            }
            goto TR_0006;
        TR_0000:
            return ButtonType.Equal;
        TR_0001:
            return null;
        TR_0006:
            return ButtonType.Div;
        }

        public static ButtonType GetButtonType(DependencyObject obj) => 
            (ButtonType) obj.GetValue(ButtonTypeProperty);

        private static void PropertyChangedButtonType(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ButtonBase dependencyObject = d as ButtonBase;
            ButtonType newValue = (ButtonType) e.NewValue;
            if ((dependencyObject != null) && (newValue != ButtonType.None))
            {
                dependencyObject.CommandParameter = newValue;
                dependencyObject.SetBinding(ButtonBase.CommandProperty, new Binding("ButtonClickCommand"));
                if (DependencyPropertyHelper.GetValueSource(dependencyObject, ContentControl.ContentProperty).BaseValueSource == BaseValueSource.Default)
                {
                    if (newValue != ButtonType.Decimal)
                    {
                        dependencyObject.Content = EditorLocalizer.GetString((EditorStringId) Enum.Parse(typeof(EditorStringId), "CalculatorButton" + newValue.ToString(), false));
                    }
                    else
                    {
                        dependencyObject.Content = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
                    }
                }
            }
        }

        public static void SetButtonType(DependencyObject obj, ButtonType value)
        {
            obj.SetValue(ButtonTypeProperty, value);
        }

        public object LastOperationButtonID { get; set; }

        public enum ButtonType
        {
            MC,
            MR,
            MS,
            MAdd,
            MSub,
            Back,
            Cancel,
            Clear,
            Zero,
            One,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Decimal,
            Sign,
            Add,
            Sub,
            Mul,
            Div,
            Fract,
            Percent,
            Sqrt,
            Equal,
            None
        }
    }
}

