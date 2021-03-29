using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootlegEmagService.Events.Model
{

    public enum ProductActionType
    {
        Added = 0,
        Removed,
        Purchased
    }

    public class ProductActionEvent
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string ProductId { get; set; }

        public ProductActionType Type { get; set; }
    }
}
