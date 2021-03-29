using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BootlegEmagService.User.Repository;

namespace BootlegEmagService.User
{
    public class UserFacade
    {
        private UserRepository UserRepository { get; set; }

        public UserFacade(UserRepository userRepository)
        {
            UserRepository = userRepository;
        }
        
        public Models.UserModel login(string name, string pass)
        {   
                return UserRepository.find(name,pass);
        }

        public Models.UserModel register(string name, string pass, string role)
        {
            return UserRepository.register(name, pass, role);
        }
    }
}
