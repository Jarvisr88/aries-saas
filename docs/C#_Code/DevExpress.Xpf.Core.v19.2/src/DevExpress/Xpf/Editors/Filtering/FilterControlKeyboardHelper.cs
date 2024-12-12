namespace DevExpress.Xpf.Editors.Filtering
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Input;

    internal static class FilterControlKeyboardHelper
    {
        public static FilterControlEditor GetParentEditor(UIElement child) => 
            LayoutHelper.FindLayoutOrVisualParentObject<FilterControlEditor>(child, false, null);

        public static bool IsAddKey(KeyEventArgs e)
        {
            ModifierKeys keyboardModifiers = ModifierKeysHelper.GetKeyboardModifiers(e);
            if (((e.Key != Key.Insert) && (e.Key != Key.Add)) || (keyboardModifiers != ModifierKeys.None))
            {
                return ((e.Key == Key.Insert) && (keyboardModifiers == ModifierKeys.Shift));
            }
            return true;
        }

        public static bool IsDeleteKey(KeyEventArgs e)
        {
            ModifierKeys keyboardModifiers = ModifierKeysHelper.GetKeyboardModifiers(e);
            if (((e.Key != Key.Delete) && (e.Key != Key.Subtract)) || (keyboardModifiers != ModifierKeys.None))
            {
                return ((e.Key == Key.Delete) && (keyboardModifiers == ModifierKeys.Shift));
            }
            return true;
        }

        public static bool IsNavigationKey(KeyEventArgs e, bool isRTL, out FilterControlNavigationHelper.FilterControlNavigationDirection navigationDirection)
        {
            if (ModifierKeysHelper.NoModifiers(ModifierKeysHelper.GetKeyboardModifiers(e)) || ModifierKeysHelper.IsCtrlPressed(ModifierKeysHelper.GetKeyboardModifiers(e)))
            {
                Key key = e.Key;
                if (key == Key.Tab)
                {
                    navigationDirection = FilterControlNavigationHelper.FilterControlNavigationDirection.Right;
                    return true;
                }
                switch (key)
                {
                    case Key.Left:
                        navigationDirection = isRTL ? FilterControlNavigationHelper.FilterControlNavigationDirection.Right : FilterControlNavigationHelper.FilterControlNavigationDirection.Left;
                        return true;

                    case Key.Up:
                        navigationDirection = FilterControlNavigationHelper.FilterControlNavigationDirection.Up;
                        return true;

                    case Key.Right:
                        navigationDirection = isRTL ? FilterControlNavigationHelper.FilterControlNavigationDirection.Left : FilterControlNavigationHelper.FilterControlNavigationDirection.Right;
                        return true;

                    case Key.Down:
                        navigationDirection = FilterControlNavigationHelper.FilterControlNavigationDirection.Down;
                        return true;

                    default:
                        break;
                }
            }
            if ((e.Key == Key.Tab) && ModifierKeysHelper.IsShiftPressed(ModifierKeysHelper.GetKeyboardModifiers(e)))
            {
                navigationDirection = FilterControlNavigationHelper.FilterControlNavigationDirection.Left;
                return true;
            }
            navigationDirection = FilterControlNavigationHelper.FilterControlNavigationDirection.Left;
            return false;
        }

        public static bool IsShowMenuKey(KeyEventArgs e)
        {
            ModifierKeys keyboardModifiers = ModifierKeysHelper.GetKeyboardModifiers(e);
            return (((e.Key != Key.Space) || ModifierKeysHelper.IsAltPressed(keyboardModifiers)) ? (((e.Key == Key.Apps) && (keyboardModifiers == ModifierKeys.None)) || ((e.Key == Key.Return) && ((keyboardModifiers == ModifierKeys.None) || (keyboardModifiers == ModifierKeys.Shift)))) : true);
        }

        public static bool IsTabKey(KeyEventArgs e, out bool isShiftPressed)
        {
            ModifierKeys keyboardModifiers = ModifierKeysHelper.GetKeyboardModifiers(e);
            isShiftPressed = keyboardModifiers == ModifierKeys.Shift;
            return ((e.Key == Key.Tab) && ((keyboardModifiers == ModifierKeys.None) | isShiftPressed));
        }
    }
}

