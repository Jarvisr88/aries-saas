namespace DevExpress.Utils.IoC
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class TypeRegistration : Registration, IDisposable
    {
        private readonly Dictionary<string, object> arguments = new Dictionary<string, object>();
        private ParameterInfo[] parameterInfoArray;

        public TypeRegistration(Type concreteType, System.Reflection.ConstructorInfo constructorInfo)
        {
            if (concreteType == null)
            {
                throw new ArgumentNullException("concreteType");
            }
            if (constructorInfo == null)
            {
                throw new ArgumentNullException("constructorInfo");
            }
            this.ConcreteType = concreteType;
            this.ConstructorInfo = constructorInfo;
            this.SyncRoot = new object();
        }

        public void AsTransient()
        {
            this.Transient = true;
        }

        public void Dispose()
        {
            IDisposable instance = this.Instance as IDisposable;
            if (instance != null)
            {
                instance.Dispose();
            }
        }

        public bool TryGetParameterValue(string name, out object value) => 
            this.arguments.TryGetValue(name, out value);

        public TypeRegistration WithCtorArgument(string name, object value)
        {
            this.parameterInfoArray ??= this.ConstructorInfo.GetParameters();
            if (this.parameterInfoArray.FirstOrDefault<ParameterInfo>(x => (x.Name == name)) == null)
            {
                throw new RegistrationFailedException("Cannot find constructor parameter " + name);
            }
            this.arguments[name] = value;
            return this;
        }

        public Type ConcreteType { get; private set; }

        public System.Reflection.ConstructorInfo ConstructorInfo { get; private set; }

        public object Instance { get; internal set; }

        public bool Transient { get; private set; }

        public object SyncRoot { get; private set; }
    }
}

