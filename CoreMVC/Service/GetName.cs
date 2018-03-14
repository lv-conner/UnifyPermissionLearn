using CoreMVC.Options;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Service
{
    public class GetName:IGetName
    {
        public GetName(IOptions<WeChatOption> option)
        {
            var type = option.GetType().ToString();
            _name = option.Value.WeChatName;
        }
        private string _name;

        public string Name => _name;
    }
}
