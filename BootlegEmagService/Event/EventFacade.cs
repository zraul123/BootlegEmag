using BootlegEmagService.Events.Model;
using BootlegEmagService.Events.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootlegEmagService.Events
{
    public class EventFacade
    {
        private EventRepository Repository { get; set; }

        public EventFacade()
        {
            Repository = new EventRepository();
        }

        public bool RegisterUserAction(UserActionEvent userAction)
        {
            Repository.RegisterUserAction(userAction);
            return CheckForDiscount(userAction.UserId);
        }

        private bool CheckForDiscount(string UserId)
        {
            if (Repository.GetLoginActionCount(UserId) == 3)
            {
                Repository.RegisterDiscountForUser(UserId);
                return true;
            }
            return false;
        }

        public void RegisterProductAction(ProductActionEvent productAction)
        {
            Repository.RegisterProductAction(productAction);
        }
    }
}