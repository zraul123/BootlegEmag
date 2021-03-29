using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootlegEmagService.Models
{
    public class UserModel
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public int Counter { get; set; }
    } 
}
