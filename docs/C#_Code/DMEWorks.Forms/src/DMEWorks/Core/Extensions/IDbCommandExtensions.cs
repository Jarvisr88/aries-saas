namespace DMEWorks.Core.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public static class IDbCommandExtensions
    {
        public static List<TEntity> ExecuteList<TEntity>(this IDbCommand command, Func<IDataRecord, TEntity> selector)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if (selector == null)
            {
                throw new ArgumentNullException("selector");
            }
            using (IDataReader reader = command.ExecuteReader(CommandBehavior.SingleResult))
            {
                return reader.ToEnumerable().Select<IDataRecord, TEntity>(selector).ToList<TEntity>();
            }
        }

        public static List<TEntity> ExecuteList<TWrapper, TEntity>(this IDbCommand command, Func<IDataRecord, TWrapper> constructor, Func<TWrapper, TEntity> selector)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if (constructor == null)
            {
                throw new ArgumentNullException("constructor");
            }
            if (selector == null)
            {
                throw new ArgumentNullException("selector");
            }
            using (IDataReader reader = command.ExecuteReader(CommandBehavior.SingleResult))
            {
                return reader.ToEnumerable<TWrapper>(constructor).Select<TWrapper, TEntity>(selector).ToList<TEntity>();
            }
        }

        public static Tuple<List<T1>> ExecuteTuple<T1>(this IDbCommand command, Func<IDataRecord, T1> selector1)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if (selector1 == null)
            {
                throw new ArgumentNullException("selector1");
            }
            using (IDataReader reader = command.ExecuteReader())
            {
                return Tuple.Create<List<T1>>(reader.ToEnumerable().Select<IDataRecord, T1>(selector1).ToList<T1>());
            }
        }

        public static Tuple<List<T1>, List<T2>> ExecuteTuple<T1, T2>(this IDbCommand command, Func<IDataRecord, T1> selector1, Func<IDataRecord, T2> selector2)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if (selector1 == null)
            {
                throw new ArgumentNullException("selector1");
            }
            if (selector2 == null)
            {
                throw new ArgumentNullException("selector2");
            }
            using (IDataReader reader = command.ExecuteReader())
            {
                List<T2> list = null;
                if (reader.NextResult())
                {
                    list = reader.ToEnumerable().Select<IDataRecord, T2>(selector2).ToList<T2>();
                }
                return Tuple.Create<List<T1>, List<T2>>(reader.ToEnumerable().Select<IDataRecord, T1>(selector1).ToList<T1>(), list);
            }
        }

        public static Tuple<List<T1>, List<T2>, List<T3>> ExecuteTuple<T1, T2, T3>(this IDbCommand command, Func<IDataRecord, T1> selector1, Func<IDataRecord, T2> selector2, Func<IDataRecord, T3> selector3)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if (selector1 == null)
            {
                throw new ArgumentNullException("selector1");
            }
            if (selector2 == null)
            {
                throw new ArgumentNullException("selector2");
            }
            if (selector3 == null)
            {
                throw new ArgumentNullException("selector3");
            }
            using (IDataReader reader = command.ExecuteReader())
            {
                List<T2> list = null;
                List<T3> list2 = null;
                if (reader.NextResult())
                {
                    list = reader.ToEnumerable().Select<IDataRecord, T2>(selector2).ToList<T2>();
                    if (reader.NextResult())
                    {
                        list2 = reader.ToEnumerable().Select<IDataRecord, T3>(selector3).ToList<T3>();
                    }
                }
                return Tuple.Create<List<T1>, List<T2>, List<T3>>(reader.ToEnumerable().Select<IDataRecord, T1>(selector1).ToList<T1>(), list, list2);
            }
        }

        public static Tuple<List<T1>, List<T2>, List<T3>, List<T4>> ExecuteTuple<T1, T2, T3, T4>(this IDbCommand command, Func<IDataRecord, T1> selector1, Func<IDataRecord, T2> selector2, Func<IDataRecord, T3> selector3, Func<IDataRecord, T4> selector4)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if (selector1 == null)
            {
                throw new ArgumentNullException("selector1");
            }
            if (selector2 == null)
            {
                throw new ArgumentNullException("selector2");
            }
            if (selector3 == null)
            {
                throw new ArgumentNullException("selector3");
            }
            if (selector4 == null)
            {
                throw new ArgumentNullException("selector4");
            }
            using (IDataReader reader = command.ExecuteReader())
            {
                List<T2> list = null;
                List<T3> list2 = null;
                List<T4> list3 = null;
                if (reader.NextResult())
                {
                    list = reader.ToEnumerable().Select<IDataRecord, T2>(selector2).ToList<T2>();
                    if (reader.NextResult())
                    {
                        list2 = reader.ToEnumerable().Select<IDataRecord, T3>(selector3).ToList<T3>();
                        if (reader.NextResult())
                        {
                            list3 = reader.ToEnumerable().Select<IDataRecord, T4>(selector4).ToList<T4>();
                        }
                    }
                }
                return Tuple.Create<List<T1>, List<T2>, List<T3>, List<T4>>(reader.ToEnumerable().Select<IDataRecord, T1>(selector1).ToList<T1>(), list, list2, list3);
            }
        }
    }
}

