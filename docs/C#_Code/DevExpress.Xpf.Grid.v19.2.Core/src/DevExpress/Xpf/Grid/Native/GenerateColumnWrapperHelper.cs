namespace DevExpress.Xpf.Grid.Native
{
    using System;
    using System.Collections.Generic;

    public static class GenerateColumnWrapperHelper
    {
        public static GenerateColumnWrapper FindColumnWrapper(GenerateBandWrapper bandRootWrapper, string fieldName)
        {
            GenerateColumnWrapper wrapper2;
            using (List<GenerateColumnWrapper>.Enumerator enumerator = bandRootWrapper.ColumnWrappers.GetEnumerator())
            {
                while (true)
                {
                    if (!enumerator.MoveNext())
                    {
                        break;
                    }
                    GenerateColumnWrapper current = enumerator.Current;
                    if (current.FieldName == fieldName)
                    {
                        return current;
                    }
                }
            }
            using (List<GenerateBandWrapper>.Enumerator enumerator2 = bandRootWrapper.BandWrappers.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator2.MoveNext())
                    {
                        GenerateBandWrapper current = enumerator2.Current;
                        GenerateColumnWrapper wrapper4 = FindColumnWrapper(current, fieldName);
                        if (wrapper4 == null)
                        {
                            continue;
                        }
                        wrapper2 = wrapper4;
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return wrapper2;
        }
    }
}

