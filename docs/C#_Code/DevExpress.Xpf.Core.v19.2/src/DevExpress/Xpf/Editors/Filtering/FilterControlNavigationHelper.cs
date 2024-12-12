namespace DevExpress.Xpf.Editors.Filtering
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    public static class FilterControlNavigationHelper
    {
        private static void FindChildLeft(ref IFilterControlNavigationNode currentNode, ref UIElement currentChild)
        {
            IList<UIElement> children = currentNode.Children;
            int index = children.IndexOf(currentChild);
            if (index > 0)
            {
                currentChild = children[index - 1];
            }
            else
            {
                currentNode = GetNodeUp(currentNode);
                children = currentNode.Children;
                currentChild = (children.Count > 0) ? children[children.Count - 1] : null;
            }
        }

        private static void FindChildRight(ref IFilterControlNavigationNode currentNode, ref UIElement currentChild)
        {
            IList<UIElement> children = currentNode.Children;
            int index = children.IndexOf(currentChild);
            if (index < (children.Count - 1))
            {
                currentChild = children[index + 1];
            }
            else
            {
                currentNode = GetNodeDown(currentNode, true);
                children = currentNode.Children;
                currentChild = (children.Count > 0) ? children[0] : null;
            }
        }

        private static IFilterControlNavigationNode GetBottomMostNode(IFilterControlNavigationNode currentNode)
        {
            IList<IFilterControlNavigationNode> subNodes = currentNode.SubNodes;
            return ((subNodes.Count <= 0) ? currentNode : GetBottomMostNode(subNodes[subNodes.Count - 1]));
        }

        public static UIElement GetChildToFocus(IFilterControlNavigationNode rootNode, IFilterControlNavigationNode currentNode, UIElement currentChild, FilterControlNavigationDirection direction)
        {
            if (currentNode == null)
            {
                if (rootNode.Children.Count > 0)
                {
                    return rootNode.Children[0];
                }
                currentNode = rootNode;
                direction = FilterControlNavigationDirection.Down;
            }
            IFilterControlNavigationNode nodeUp = currentNode;
            UIElement element = currentChild;
            switch (direction)
            {
                case FilterControlNavigationDirection.Left:
                    while (true)
                    {
                        FindChildLeft(ref nodeUp, ref element);
                        if (ReferenceEquals(element, currentChild))
                        {
                            break;
                        }
                        if (element != null)
                        {
                            return element;
                        }
                    }
                    break;

                case FilterControlNavigationDirection.Up:
                    while (true)
                    {
                        nodeUp = GetNodeUp(nodeUp);
                        if (ReferenceEquals(nodeUp, currentNode))
                        {
                            break;
                        }
                        IList<UIElement> children = nodeUp.Children;
                        if (children.Count > 0)
                        {
                            return children[0];
                        }
                    }
                    break;

                case FilterControlNavigationDirection.Right:
                    while (true)
                    {
                        FindChildRight(ref nodeUp, ref element);
                        if (ReferenceEquals(element, currentChild))
                        {
                            break;
                        }
                        if (element != null)
                        {
                            return element;
                        }
                    }
                    break;

                case FilterControlNavigationDirection.Down:
                    while (true)
                    {
                        nodeUp = GetNodeDown(nodeUp, true);
                        if (ReferenceEquals(nodeUp, currentNode))
                        {
                            break;
                        }
                        IList<UIElement> children = nodeUp.Children;
                        if (children.Count > 0)
                        {
                            return children[0];
                        }
                    }
                    break;

                default:
                    break;
            }
            return null;
        }

        private static IFilterControlNavigationNode GetNodeDown(IFilterControlNavigationNode currentNode, bool canReturnChild)
        {
            if (canReturnChild && (currentNode.SubNodes.Count > 0))
            {
                return currentNode.SubNodes[0];
            }
            if (currentNode.ParentNode == null)
            {
                return currentNode;
            }
            IList<IFilterControlNavigationNode> subNodes = currentNode.ParentNode.SubNodes;
            int index = subNodes.IndexOf(currentNode);
            return ((index >= (subNodes.Count - 1)) ? GetNodeDown(currentNode.ParentNode, false) : subNodes[index + 1]);
        }

        private static IFilterControlNavigationNode GetNodeUp(IFilterControlNavigationNode currentNode)
        {
            if (currentNode.ParentNode == null)
            {
                return GetBottomMostNode(currentNode);
            }
            IList<IFilterControlNavigationNode> subNodes = currentNode.ParentNode.SubNodes;
            int index = subNodes.IndexOf(currentNode);
            return ((index <= 0) ? currentNode.ParentNode : GetBottomMostNode(subNodes[index - 1]));
        }

        public enum FilterControlNavigationDirection
        {
            Left,
            Up,
            Right,
            Down
        }
    }
}

