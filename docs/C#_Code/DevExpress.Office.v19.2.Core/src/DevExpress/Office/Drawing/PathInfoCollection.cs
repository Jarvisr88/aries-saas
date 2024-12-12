namespace DevExpress.Office.Drawing
{
    using System;
    using System.Collections.Generic;
    using System.Drawing.Drawing2D;

    public class PathInfoCollection : List<PathInfoBase>
    {
        private static Dictionary<Type, GraphicsPathCollectFlags> excludedTypes = CreateExcludedTypes();

        public GraphicsPath CollectFigure(GraphicsPathCollectFlags flags)
        {
            GraphicsPath path = new GraphicsPath(FillMode.Alternate);
            foreach (PathInfoBase base2 in this)
            {
                if ((base2.GraphicsPath != null) && ((base2.GraphicsPath.PointCount != 0) && !this.ShouldSkipPathInfo(base2, flags)))
                {
                    path.AddPath(base2.GraphicsPath, false);
                }
            }
            return path;
        }

        private static Dictionary<Type, GraphicsPathCollectFlags> CreateExcludedTypes() => 
            new Dictionary<Type, GraphicsPathCollectFlags> { 
                { 
                    typeof(ConnectionShapePathInfo),
                    GraphicsPathCollectFlags.ExcludeConnectors
                },
                { 
                    typeof(InnerShadowPathInfo),
                    GraphicsPathCollectFlags.ExcludeInnerShadow
                },
                { 
                    typeof(GlowPathInfo),
                    GraphicsPathCollectFlags.ExcludeGlow
                },
                { 
                    typeof(OuterShadowPathInfo),
                    GraphicsPathCollectFlags.ExcludeOuterShadow
                },
                { 
                    typeof(ReflectionPathInfo),
                    GraphicsPathCollectFlags.ExcludeReflection
                },
                { 
                    typeof(TextRectanglePathInfo),
                    GraphicsPathCollectFlags.ExcludeTextRectangle
                },
                { 
                    typeof(SoftEdgePathInfo),
                    GraphicsPathCollectFlags.ExcludeSoftEdges
                },
                { 
                    typeof(BlurPathInfo),
                    GraphicsPathCollectFlags.ExcludeBlur
                }
            };

        public T GetPathInfo<T>() where T: PathInfoBase
        {
            T local;
            using (List<PathInfoBase>.Enumerator enumerator = base.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        PathInfoBase current = enumerator.Current;
                        if (!(current.GetType() == typeof(T)))
                        {
                            continue;
                        }
                        local = (T) current;
                    }
                    else
                    {
                        return default(T);
                    }
                    break;
                }
            }
            return local;
        }

        public T[] GetPathInfos<T>() where T: PathInfoBase
        {
            List<T> list = new List<T>();
            foreach (PathInfoBase base2 in this)
            {
                if (base2.GetType() == typeof(T))
                {
                    list.Add((T) base2);
                }
            }
            return list.ToArray();
        }

        public T[] GetPathInfosAndDerived<T>() where T: PathInfoBase
        {
            List<T> list = new List<T>();
            foreach (PathInfoBase base2 in this)
            {
                Type type = base2.GetType();
                Type c = typeof(T);
                if ((type == c) || type.IsSubclassOf(c))
                {
                    list.Add((T) base2);
                }
            }
            return list.ToArray();
        }

        private bool IsExcludedType(GraphicsPathCollectFlags flags, GraphicsPathCollectFlags excludedType) => 
            (flags & excludedType) == excludedType;

        public bool ShouldSkipPathInfo(PathInfoBase pathInfo, GraphicsPathCollectFlags flags)
        {
            if (this.IsExcludedType(flags, GraphicsPathCollectFlags.ExcludeInvisiblePaths) && (!pathInfo.Stroke && !pathInfo.Filled))
            {
                return true;
            }
            Type key = pathInfo.GetType();
            return (excludedTypes.ContainsKey(key) && this.IsExcludedType(flags, excludedTypes[key]));
        }
    }
}

