using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootlegEmagService.Events.Model
{ 

    public enum UserActionType
    {
        Login = 0,
        Logout
    }

    public class UserActionEvent
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public UserActionType Type { get; set; }
    }
}
