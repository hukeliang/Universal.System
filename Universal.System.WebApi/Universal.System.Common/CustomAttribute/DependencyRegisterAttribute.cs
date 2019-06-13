using System;

namespace Universal.System.Common.CustomAttribute
{
    /// <summary>
    /// 标记类需要依赖注入
    /// 默认 RegisterType.InstancePerDependency 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class DependencyRegisterAttribute : Attribute
    {
        /// <summary>
        /// 注入类型
        /// </summary>
        public RegisterType Type { get; set; }

        public DependencyRegisterAttribute()
        {
            Type = RegisterType.InstancePerDependency;
        }
        public DependencyRegisterAttribute(RegisterType registerType)
        {
            Type = registerType;
        }
    }

    /// <summary>
    /// 注册类型
    /// </summary>
    public enum RegisterType
    {
        /// <summary>
        /// 默认
        /// 每次都创建一个新的对象
        /// </summary>
        InstancePerDependency,

        /// <summary>
        /// 单例
        /// </summary>
        SingleInstance,

        /// <summary>
        /// 每个嵌套作用域将获得一个实例
        /// </summary>
        InstancePerLifetimeScope
    }
}
