using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UnifyPermission.Models
{
    public class UserModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
