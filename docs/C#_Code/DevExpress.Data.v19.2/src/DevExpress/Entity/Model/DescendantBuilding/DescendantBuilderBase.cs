namespace DevExpress.Entity.Model.DescendantBuilding
{
    using DevExpress.Utils.Controls;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public abstract class DescendantBuilderBase : DisposableObject
    {
        private DescendantInstanceActivator instanceActivator;

        protected DescendantBuilderBase()
        {
        }

        protected DescendantInstanceActivator CreateDescendantInstanceActivator(Type resultType, Type[] parametersTypes, Expression connection, string tempFolder)
        {
            List<Expression> arguments = new List<Expression>();
            if (connection.Type != parametersTypes[0])
            {
                arguments.Add(Expression.Convert(connection, parametersTypes[0]));
            }
            else
            {
                arguments.Add(connection);
            }
            if (parametersTypes.Length == 2)
            {
                arguments.Add(Expression.Constant(true));
            }
            return new DescendantInstanceActivator(tempFolder, Expression.New(resultType.GetConstructor(parametersTypes), arguments));
        }

        public object DescendantInstance =>
            this.instanceActivator?.DefaultInstance;

        public DescendantInstanceActivator InstanceActivator
        {
            get => 
                this.instanceActivator;
            protected set => 
                this.instanceActivator = value;
        }
    }
}

