using System;
using System.Collections.Generic;
using System.Text;

namespace Universal.System.Common.CustomAttribute
{
    /// <summary>
    /// 标记泛型类需要依赖注入
    /// 默认 RegisterType.InstancePerDependency 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class DependencyRegisterGenericAttribute : Attribute
    {
        /// <summary>
        /// 注入类型
        /// </summary>
        public RegisterType Type { get; set; }

        public DependencyRegisterGenericAttribute()
        {
            Type = RegisterType.InstancePerDependency;
        }
        public DependencyRegisterGenericAttribute(RegisterType registerType)
        {
            Type = registerType;
        }
    }
}
