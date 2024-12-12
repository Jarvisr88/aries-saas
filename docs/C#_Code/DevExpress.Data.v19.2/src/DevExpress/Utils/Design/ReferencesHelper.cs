namespace DevExpress.Utils.Design
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Reflection;

    public class ReferencesHelper
    {
        private static void AddAssemblyReference(object references, string identity)
        {
            if (references != null)
            {
                try
                {
                    object[] args = new object[] { identity };
                    if (references.GetType().InvokeMember("Find", BindingFlags.InvokeMethod | BindingFlags.Public, null, references, args) == null)
                    {
                        object[] objArray2 = new object[] { identity };
                        references.GetType().InvokeMember("Add", BindingFlags.InvokeMethod | BindingFlags.Public, null, references, objArray2);
                    }
                }
                catch
                {
                }
            }
        }

        public static void EnsureReferences(IDesignerHost provider, params string[] assemblies)
        {
            try
            {
                object references = GetReferences(provider);
                if (references != null)
                {
                    foreach (string str in assemblies)
                    {
                        AddAssemblyReference(references, str);
                    }
                }
            }
            catch
            {
            }
        }

        private static Assembly FindEnvDTE()
        {
            try
            {
                Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
                int index = 0;
                while (true)
                {
                    if (index >= assemblies.Length)
                    {
                        break;
                    }
                    Assembly assembly = assemblies[index];
                    if (assembly.GetName().Name.ToLowerInvariant() != "envdte")
                    {
                        index++;
                        continue;
                    }
                    return assembly;
                }
            }
            catch
            {
            }
            return null;
        }

        private static object FindProjectItem(Assembly envDTE, IServiceProvider provider)
        {
            if ((envDTE == null) || (provider == null))
            {
                return null;
            }
            Type serviceType = envDTE.GetType("EnvDTE.ProjectItem", false);
            return ((serviceType != null) ? provider?.GetService(serviceType) : null);
        }

        private static object FindSolutionItemForSharePointApplication(Assembly envDTE, IServiceProvider provider)
        {
            if ((envDTE == null) || (provider == null))
            {
                return null;
            }
            Type serviceType = envDTE.GetType("EnvDTE.DTE", false);
            return ((serviceType == null) ? null : provider.GetService(serviceType));
        }

        public static object GetContainingProject(IServiceProvider provider)
        {
            Assembly envDTE = FindEnvDTE();
            object vsSharePointSolution = FindProjectItem(envDTE, provider);
            if (vsSharePointSolution == null)
            {
                vsSharePointSolution = FindSolutionItemForSharePointApplication(envDTE, provider);
                if (vsSharePointSolution != null)
                {
                    vsSharePointSolution = GetProjectItemForSharePointSolution(vsSharePointSolution);
                }
                if (vsSharePointSolution == null)
                {
                    return null;
                }
            }
            return GetPropertyValue(vsSharePointSolution, "ContainingProject");
        }

        private static object GetProjectItemForSharePointSolution(object vsSharePointSolution) => 
            GetPropertyValue(GetPropertyValue(vsSharePointSolution, "ActiveDocument"), "ProjectItem");

        private static object GetPropertyValue(object obj, string name)
        {
            if (obj == null)
            {
                return null;
            }
            PropertyDescriptor descriptor = TypeDescriptor.GetProperties(obj)[name];
            return descriptor?.GetValue(obj);
        }

        private static object GetReferences(IServiceProvider provider)
        {
            object containingProject = GetContainingProject(provider);
            return ((containingProject == null) ? null : GetPropertyValue(GetPropertyValue(containingProject, "Object"), "References"));
        }

        public static bool IsCommonProject(IServiceProvider provider) => 
            FindProjectItem(FindEnvDTE(), provider) != null;
    }
}

