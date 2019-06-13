using Universal.System.Common.CustomAttribute;
using System;

namespace Universal.System.Service
{
    [DependencyRegister(Type = RegisterType.SingleInstance)]
    public class HomeService
    {
        public HomeService()
        {
            Console.Out.Write("对象被构造");
        }
    }
}
