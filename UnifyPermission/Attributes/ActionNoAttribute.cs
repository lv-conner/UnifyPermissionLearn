using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnifyPermission.Attributes
{
    [AttributeUsage(AttributeTargets.Method,AllowMultiple = false,Inherited =true)]
    public class ActionNoAttribute:Attribute
    {
        private string actionNo;
        public string ActionNo => actionNo;
        public string ActionName { get; set; }
        public ActionNoAttribute(string actionNo)
        {
            this.actionNo = actionNo;
        }
    }
}
