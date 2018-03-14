using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnifyPermission.Models
{
    public class PermissionModel : ModuleModel
    {


        public string ActionName { get; set; }
        public string ActionNo { get; set; }
        public PermissionModel()
        {

        }
        public PermissionModel(string systemNo, string systemName, string moduleNo, string moduleName, string actionName, string actionNo)
        {
            SystemName = systemName;
            SystemNo = systemNo;
            ModuleName = moduleName;
            ModuleNo = moduleNo;
            ActionNo = actionNo;
            ActionName = actionName;
        }

        public PermissionModel(SystemOptions model, string moduleNo, string moduleName, string actionName, string actionNo)
            : this(model.SystemNo, model.SystemName, moduleNo, moduleName, actionName, actionNo)
        {

        }
        public override string ToString()
        {
            return $"{"SystemNo:" + SystemNo + "\t" + "SystemName:" + SystemName + "\t" + "ModuleNo:" + ModuleNo + "\t" + "ModuleName:" + ModuleName + "\t" + "ActionNo" + ActionNo + "\t" + "ActionName:" + ActionName}";
        }
    }

    public class ModuleModel : SystemOptions
    {
        public string ModuleName { get; set; }
        public string ModuleNo { get; set; }
    }

    public class SystemOptions
    {
        public string SystemName { get; set; }
        public string SystemNo { get; set; }
    }
}
