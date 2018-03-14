using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Options
{
    public class WeChatOption
    {
        public WeChatOption()
        {

        }
        private string weChatName;
        private string weChatID;
        public string WeChatName { get => weChatName; set => weChatName = value; }
        public string WeChatID { get => weChatID; set => weChatID = value; }
    }
}
