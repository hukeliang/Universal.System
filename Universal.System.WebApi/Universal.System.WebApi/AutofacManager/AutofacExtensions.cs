using Autofac;
using Autofac.Extensions.DependencyInjection;
using Universal.System.WebApi.AutofacManager;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Universal.System.WebApi
{
    internal static class AutofacExtensions
    {

        private static readonly object _look = new object();

        /// <summary>
        /// Autofac IOC容器
        /// </summary>
        internal static Autofac.IContainer Container { get; private set; }

        /// <summary>
        /// Autofac IOC容器初始化
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        internal static IServiceProvider AutofacInitialize(this IServiceCollection services, IConfiguration configuration)
        {
            if (Container == null)
            {
                lock (_look)
                {
                    if (Container == null)
                    {
                        //实例化 AutoFac  容器      
                        ContainerBuilder builder = new ContainerBuilder();

                        AutofacDependencyRegisterModule module = new AutofacDependencyRegisterModule(configuration);

                        //用一组注册的服务描述符填充Autofac容器构建器并接管IServiceProvider和IServiceScopeFactory在AutoFac容器中可用。
                        builder.Populate(services);

                        //模块化注入
                        builder.RegisterModule(module);

                        Container = builder.Build();

                        return new AutofacServiceProvider(Container);
                    }
                }
            }

            return new AutofacServiceProvider(Container);
        }

    }
}
