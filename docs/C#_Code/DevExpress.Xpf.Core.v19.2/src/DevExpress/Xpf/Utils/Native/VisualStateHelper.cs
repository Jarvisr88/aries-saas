namespace DevExpress.Xpf.Utils.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Animation;

    public class VisualStateHelper
    {
        private static string GetCorrectPath(PropertyPath propertyPath)
        {
            if (!propertyPath.Path.Contains<char>(':'))
            {
                return string.Empty;
            }
            char[] separator = new char[] { ':' };
            char[] chArray2 = new char[] { ')' };
            return propertyPath.Path.Split(separator)[1].Split(chArray2)[0];
        }

        private static DependencyProperty GetDependencyPropertyByPath(string path, Dictionary<string, DependencyProperty> properties) => 
            properties.ContainsKey(path) ? properties[path] : null;

        public static IList GetVisualStateGroups(FrameworkElement owner)
        {
            IList visualStateGroups = VisualStateManager.GetVisualStateGroups(owner);
            if (visualStateGroups.Count != 0)
            {
                return visualStateGroups;
            }
            if (VisualTreeHelper.GetChildrenCount(owner) == 0)
            {
                return visualStateGroups;
            }
            FrameworkElement child = VisualTreeHelper.GetChild(owner, 0) as FrameworkElement;
            return ((child != null) ? VisualStateManager.GetVisualStateGroups(child) : visualStateGroups);
        }

        public static void UpdateStates(FrameworkElement owner, string groupName, Dictionary<string, DependencyProperty> properties)
        {
            if (owner != null)
            {
                foreach (VisualStateGroup group in GetVisualStateGroups(owner))
                {
                    if (group.Name.Equals(groupName))
                    {
                        foreach (VisualState state in group.States)
                        {
                            if (state.Storyboard != null)
                            {
                                foreach (Timeline timeline in state.Storyboard.Children)
                                {
                                    PropertyPath targetProperty = Storyboard.GetTargetProperty(timeline);
                                    if (!targetProperty.Path.Contains("(0)"))
                                    {
                                        DependencyProperty dependencyPropertyByPath = GetDependencyPropertyByPath(GetCorrectPath(targetProperty), properties);
                                        if (dependencyPropertyByPath != null)
                                        {
                                            Storyboard.SetTargetProperty(timeline, new PropertyPath(dependencyPropertyByPath));
                                        }
                                    }
                                    if (string.IsNullOrEmpty(Storyboard.GetTargetName(timeline)))
                                    {
                                        Storyboard.SetTarget(timeline, owner);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

