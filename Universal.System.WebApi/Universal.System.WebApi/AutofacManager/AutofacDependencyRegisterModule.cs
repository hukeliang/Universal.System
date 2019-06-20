using Autofac;
using Universal.System.Common.CustomAttribute;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Universal.System.WebApi.AutofacManager
{
    /// <summary>
    /// Autofac模块化注入
    /// </summary>
    internal sealed class AutofacDependencyRegisterModule : Autofac.Module
    {
        private readonly IConfiguration _configuration;
        internal AutofacDependencyRegisterModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected sealed override void Load(ContainerBuilder builder)
        {
            #region 注册程序集下所有Controller Filter，由Autofac创建  

            //获取包含当前正在执行的代码的程序集名字
            string projectName = Assembly.GetExecutingAssembly().GetName().Name;

            Assembly projectAssembly = Assembly.Load(projectName);

            //注册 CoreAutofac 程序集里面的所有类 条件：继承ControllerBase的类型且不能是ControllerBase
            builder.RegisterAssemblyTypes(projectAssembly)
                .Where(predicate => typeof(ControllerBase).IsAssignableFrom(predicate) && predicate != typeof(ControllerBase))
                .PropertiesAutowired();

            //注册webapi项目实现了IActionFilter IExceptionFilter IAuthorizationFilter 接口的非抽象过滤器类    
            builder.RegisterAssemblyTypes(projectAssembly)
                .Where(predicate => !predicate.IsAbstract && (typeof(IAuthorizationFilter).IsAssignableFrom(predicate) || typeof(IActionFilter).IsAssignableFrom(predicate) || typeof(IExceptionFilter).IsAssignableFrom(predicate)))
                .PropertiesAutowired();

            #endregion

            #region 批量注册

            //拼接当前程序集和配置文件中的程序集名字
            string[] serviceAssemblyArray = $"{projectName},{_configuration["Autofac:Service"]}".Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string serviceAssemblyName in serviceAssemblyArray)
            {
                Assembly assembly = Assembly.Load(serviceAssemblyName);

                //动态注入泛型类
                IEnumerable<Type> genericTypes = assembly.DefinedTypes.Where(item => item.IsGenericType && item.GetCustomAttribute<DependencyRegisterGenericAttribute>() is DependencyRegisterGenericAttribute);

                foreach (Type item in genericTypes)
                {
                    //注册泛型
                    builder.RegisterGeneric(assembly.GetType($"{item.Namespace}.{item.Name}")).AsImplementedInterfaces().AsSelf().InstancePerDependency();
                }

                //标记为SingleInstanceDependencyRegisterAttribute特性的使用依赖注入
                //指定将扫描程序集中的类型注册为提供其所有实现接口。
                //指定可以使用自己当作服务的提供者
                //注册单例
                builder.RegisterAssemblyTypes(assembly)
                    .Where(predicate => predicate.GetCustomAttribute<DependencyRegisterAttribute>()?.Type == RegisterType.SingleInstance)
                    .AsImplementedInterfaces()
                    .AsSelf()
                    .SingleInstance();

                //每次都创建一个新的对象
                builder.RegisterAssemblyTypes(assembly)
                    .Where(predicate => predicate.GetCustomAttribute<DependencyRegisterAttribute>()?.Type == RegisterType.InstancePerDependency)
                    .AsImplementedInterfaces()
                    .AsSelf()
                    .InstancePerDependency();

                //同一个Lifetime生成的对象是同一个实例 有点像AddScoped
                builder.RegisterAssemblyTypes(assembly)
                    .Where(predicate => predicate.GetCustomAttribute<DependencyRegisterAttribute>()?.Type == RegisterType.InstancePerLifetimeScope)
                    .AsImplementedInterfaces()
                    .AsSelf()
                    .InstancePerLifetimeScope();
            }

            #endregion
        }
    }
}
