using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVC5EF6Demo
{
    public class UnityControllerFactory :DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            //使用Unity来返回对象。
            //如果相应的控制器里有使用依赖注入（比如构造注入、SETTER注入，那么就会自动生成）
            //比如下面这个代码片段：
            /*
                public class HomeController : Controller
                {
                    public IContactRepository Repository { get; private set; }
                    public HomeController(IContactRepository repository)
                    {
                        this.Repository = repository;
                    }
                    public ActionResult Index()
                    {
                        return View(this.Repository.GetAllContacts());
                    }
                }

                而在配置中，是这样的片段：
                <unity>
                  <alias alias="IContactRepository" type="JX.Domain.IRepositories.IContactRepository, UnityIntegration" />
                  <alias alias="DefaultContactRepository" type="JX.Domain.IRepositories.DefaultContactRepository, UnityIntegration" />
                  <container>
                    <register type="IContactRepository" mapTo="DefaultContactRepository"/>
                  </container>
                </unity>

            */
            /*
                在上面的代码片段中，并没有显式的生成IContactRepository的实现实例，那在运行时是怎么用到相应的实例的。
                就是在生成控制器对象的时候，由Unity自动映射并创建的。

                由于在配置里是使用的Unity来做注入容器，而默认的生成控制器的工厂并没有使用Unity,所以要基于Unity重新自定义控制器工厂。

                下面的ServiceLocator实际上是对Unity的封装。
            */
            return null == controllerType ? null : JX.Infrastructure.ServiceLocator.Instance.GetService(controllerType) as IController;
            //return base.GetControllerInstance(requestContext, controllerType);
        }

        #region 如果没有ServiceLocator,则可以如下实现

        /*:
            public class UnityControllerFactory: DefaultControllerFactory
            {
                public IUnityContainer Container { get; private set; }
                public UnityControllerFactory(string containerName = "")
                {
                    IUnityContainer container = new UnityContainer();
                    UnityConfigurationSection configSection = ConfigurationManager.GetSection(UnityConfigurationSection.SectionName) as UnityConfigurationSection;
                    if (null == configSection && !string.IsNullOrEmpty(containerName))
                    {
                        throw new ConfigurationErrorsException("Cannot find <unity> configuration section");
                    }
     
                    if (string.IsNullOrEmpty(containerName))
                    {
                        configSection.Configure(container);
                    }
                    else
                    {
                        configSection.Configure(container, containerName);
                    }
                    this.Container = container;
                }
                protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
                {
                    Guard.ArgumentNotNull(controllerType, "controllerType");
                    return (IController)this.Container.Resolve(controllerType);
                }
            }

        */

        #endregion 

    }
}