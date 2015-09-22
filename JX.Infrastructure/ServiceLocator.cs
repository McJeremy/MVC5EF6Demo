using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JX.Infrastructure
{
    public class ServiceLocator :IServiceProvider
    {
        private readonly IUnityContainer container;

        private static readonly Lazy<ServiceLocator> _instance = new Lazy<ServiceLocator>();

        public static ServiceLocator Instance
        {
            get { return _instance.Value; }
        }

        private ServiceLocator()
        {            
            container = new UnityContainer();
            //UnityConfigurationSection section = ConfigurationManager.GetSection("unity") as UnityConfigurationSection;

            UnityConfigurationSection section = ConfigurationManager.GetSection(UnityConfigurationSection.SectionName) as UnityConfigurationSection;
            section.Configure(container);
        }

        #region IServiceProvider Members

        public object GetService(Type serviceType)
        {
            return container.Resolve(serviceType);
        }

        #endregion

        private IEnumerable<ParameterOverride> GetParameterOverrides(object overridedArguments)
        {
            List<ParameterOverride> overrides = new List<ParameterOverride>();
            Type argumentsType = overridedArguments.GetType();
            argumentsType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .ToList()
                .ForEach(property =>
                {
                    var propertyValue = property.GetValue(overridedArguments, null);
                    var propertyName = property.Name;
                    overrides.Add(new ParameterOverride(propertyName, propertyValue));
                });
            return overrides;
        }

        public T GetService<T>()
        {
            return container.Resolve<T>();
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            return container.ResolveAll<T>();
        }

        /// <summary>
        /// Gets the service instance with the given type by using the overrided arguments.
        /// </summary>
        /// <typeparam name="T">The type of the service.</typeparam>
        /// <param name="overridedArguments">The overrided arguments.</param>
        /// <returns>The service instance.</returns>
        public T GetService<T>(object overridedArguments)
        {
            var overrides = GetParameterOverrides(overridedArguments);
            return container.Resolve<T>(overrides.ToArray());
        }
        /// <summary>
        /// Gets the service instance with the given type by using the overrided arguments.
        /// </summary>
        /// <param name="serviceType">The type of the service.</param>
        /// <param name="overridedArguments">The overrided arguments.</param>
        /// <returns>The service instance.</returns>
        public object GetService(Type serviceType, object overridedArguments)
        {
            var overrides = GetParameterOverrides(overridedArguments);
            return container.Resolve(serviceType, overrides.ToArray());
        }
    }
}
