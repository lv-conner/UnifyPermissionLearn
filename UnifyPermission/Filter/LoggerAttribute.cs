using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
namespace UnifyPermission.Filter
{
    /// <summary>
    /// 非全局特性过滤器的DI实现
    /// </summary>
    public class LoggerAttribute : Attribute, IFilterFactory
    {
        public bool IsReusable => true;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            return serviceProvider.GetService<LoggerFilter>();
        }
    }


    //特性过滤器，由于特性过滤器的特点。因此在使用特性过滤器的时候，如果需要依赖某些服务。则应该通过此种方式实现特性过滤。


    //通用特性过滤器
    public class FilterAttribute : Attribute, IFilterFactory
    {
        private readonly Type type;
        public FilterAttribute(Type T)
        {
            //父类.IsAssignableFrom(子类) true
            //From 从。
            if (!typeof(IFilterMetadata).IsAssignableFrom(T))
            {
                throw new InvalidOperationException("T must inherit from IFilterMetadata");
            }
            type = T;
        }
        public bool IsReusable => true;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            return serviceProvider.GetService(type) as IFilterMetadata;
        }
    }
}
