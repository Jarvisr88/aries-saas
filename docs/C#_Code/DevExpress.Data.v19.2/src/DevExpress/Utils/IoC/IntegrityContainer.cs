namespace DevExpress.Utils.IoC
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class IntegrityContainer : IServiceProvider, IDisposable
    {
        private readonly Dictionary<Type, Registration> registry = new Dictionary<Type, Registration>();
        private readonly List<IDisposable> objectsToDispose = new List<IDisposable>();

        private object CreateInstance(TypeRegistration typeRegistration)
        {
            ParameterInfo[] parameters = typeRegistration.ConstructorInfo.GetParameters();
            object[] args = new object[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                ParameterInfo info = parameters[i];
                if (!typeRegistration.TryGetParameterValue(info.Name, out args[i]))
                {
                    try
                    {
                        args[i] = this.Resolve(info.ParameterType);
                    }
                    catch (ResolutionFailedException exception)
                    {
                        throw new ResolutionFailedException($"Missing parameter '{info.Name}' for the requested type '{typeRegistration.ConcreteType}'", exception);
                    }
                }
            }
            object instance = Activator.CreateInstance(typeRegistration.ConcreteType, args);
            this.InitializeProperties(instance, typeRegistration);
            return instance;
        }

        public void Dispose()
        {
            foreach (IDisposable disposable in this.objectsToDispose)
            {
                disposable.Dispose();
            }
            this.registry.Clear();
            this.objectsToDispose.Clear();
        }

        protected void InitializeProperties(object instance, TypeRegistration typeRegistration)
        {
            foreach (PropertyInfo info in typeRegistration.ConcreteType.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
            {
                DevExpress.Utils.IoC.DependencyAttribute customAttribute = info.GetCustomAttribute<DevExpress.Utils.IoC.DependencyAttribute>();
                if (customAttribute != null)
                {
                    try
                    {
                        object obj2 = this.Resolve(info.PropertyType);
                        info.SetValue(instance, obj2);
                    }
                    catch (ResolutionFailedException exception)
                    {
                        if (customAttribute.IsMandatory)
                        {
                            throw new ResolutionFailedException($"Missing property value '{info.Name}' for the requested type '{typeRegistration.ConcreteType}'", exception);
                        }
                    }
                }
            }
        }

        protected TRegistration RegisterCore<TRegistration>(Type serviceType, TRegistration registration) where TRegistration: Registration
        {
            IDisposable item = registration as IDisposable;
            if (item != null)
            {
                this.objectsToDispose.Add(item);
            }
            this.registry[serviceType] = registration;
            return registration;
        }

        public FactoryRegistration RegisterFactory<TServiceType>(Func<TServiceType> factory)
        {
            Guard.ArgumentNotNull(factory, "factory");
            return this.RegisterCore<FactoryRegistration>(typeof(TServiceType), new FactoryRegistration(() => factory()));
        }

        public FactoryRegistration RegisterFactory<TServiceType>(Func<IServiceProvider, TServiceType> factory)
        {
            Guard.ArgumentNotNull(factory, "factory");
            return this.RegisterCore<FactoryRegistration>(typeof(TServiceType), new FactoryRegistration(() => factory(this)));
        }

        public InstanceRegistration RegisterInstance<TServiceType>(TServiceType instance) => 
            this.RegisterCore<InstanceRegistration>(typeof(TServiceType), new InstanceRegistration(instance));

        public InstanceRegistration RegisterInstance(Type serviceType, object instance)
        {
            Guard.ArgumentNotNull(serviceType, "serviceType");
            Guard.ArgumentNotNull(instance, "instance");
            return this.RegisterCore<InstanceRegistration>(serviceType, new InstanceRegistration(instance));
        }

        public TypeRegistration RegisterType<TConcreteType>() => 
            this.RegisterType<TConcreteType, TConcreteType>();

        public TypeRegistration RegisterType<TServiceType, TConcreteType>() where TConcreteType: TServiceType => 
            this.RegisterType(typeof(TServiceType), typeof(TConcreteType));

        public TypeRegistration RegisterType(Type serviceType, Type concreteType)
        {
            Guard.ArgumentNotNull(serviceType, "serviceType");
            Guard.ArgumentNotNull(concreteType, "concreteType");
            if (concreteType.IsAbstract)
            {
                throw new RegistrationFailedException("Concrete type can not be abstract.");
            }
            ConstructorInfo constructorInfo = SelectMostGreedyConstructor(concreteType);
            return this.RegisterCore<TypeRegistration>(serviceType, new TypeRegistration(concreteType, constructorInfo));
        }

        public TServiceType Resolve<TServiceType>() => 
            (TServiceType) this.Resolve(typeof(TServiceType));

        public object Resolve(Type serviceType)
        {
            Registration registration;
            object instance;
            if (!this.registry.TryGetValue(serviceType, out registration) && !this.TryResolveUnregistered(serviceType, out registration))
            {
                throw new ResolutionFailedException($"Can not resolve type '{serviceType}'");
            }
            InstanceRegistration registration2 = registration as InstanceRegistration;
            if (registration2 != null)
            {
                return registration2.Instance;
            }
            FactoryRegistration registration3 = registration as FactoryRegistration;
            if (registration3 != null)
            {
                return registration3.Instance;
            }
            TypeRegistration typeRegistration = (TypeRegistration) registration;
            if (typeRegistration.Transient)
            {
                return this.CreateInstance(typeRegistration);
            }
            if (typeRegistration.Instance != null)
            {
                return typeRegistration.Instance;
            }
            object syncRoot = typeRegistration.SyncRoot;
            lock (syncRoot)
            {
                if (typeRegistration.Instance != null)
                {
                    instance = typeRegistration.Instance;
                }
                else
                {
                    object obj3 = this.CreateInstance(typeRegistration);
                    typeRegistration.Instance = obj3;
                    instance = obj3;
                }
            }
            return instance;
        }

        protected static ConstructorInfo SelectMostGreedyConstructor(Type type)
        {
            ConstructorInfo[] infoArray = SelectMostGreedyConstructors(type);
            if (infoArray.Length == 0)
            {
                throw new RegistrationFailedException("Concrete type does not have public instance constructor.");
            }
            if (infoArray.Length > 1)
            {
                throw new RegistrationFailedException("Can not resolve ambiguity and choose most greedy constructor.");
            }
            return infoArray[0];
        }

        protected internal static ConstructorInfo[] SelectMostGreedyConstructors(Type type)
        {
            ConstructorInfo[] constructors = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance);
            if (constructors.Length == 0)
            {
                return constructors;
            }
            Func<ConstructorInfo, int> selector = <>c.<>9__18_0;
            if (<>c.<>9__18_0 == null)
            {
                Func<ConstructorInfo, int> local1 = <>c.<>9__18_0;
                selector = <>c.<>9__18_0 = arg => arg.GetParameters().Length;
            }
            int maximumNumberOfParameters = constructors.Max<ConstructorInfo>(selector);
            return (from arg in constructors
                where arg.GetParameters().Length == maximumNumberOfParameters
                select arg).ToArray<ConstructorInfo>();
        }

        object IServiceProvider.GetService(Type serviceType) => 
            this.Resolve(serviceType);

        protected virtual bool TryResolveUnregistered(Type serviceType, out Registration registration)
        {
            if (serviceType == typeof(IServiceProvider))
            {
                registration = new InstanceRegistration(this);
                return true;
            }
            registration = null;
            return false;
        }

        protected internal IEnumerable<Type> RegisteredTypes =>
            this.registry.Keys;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly IntegrityContainer.<>c <>9 = new IntegrityContainer.<>c();
            public static Func<ConstructorInfo, int> <>9__18_0;

            internal int <SelectMostGreedyConstructors>b__18_0(ConstructorInfo arg) => 
                arg.GetParameters().Length;
        }
    }
}

