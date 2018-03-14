using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnifyPermission.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method,AllowMultiple =false,Inherited =true)]
    public class ModuleNoAttribute:Attribute
    {
        private string moduleNo;
        public string ModuleNo => moduleNo;
        public string ModuleName { get; set; }
        public ModuleNoAttribute(string moduleNo)
        {
            this.moduleNo = moduleNo;
        }
    }
}
