using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoMarket.Shared.Messages
{
    public class ProductNameChangedEvent
    {
        public string ProductId { get; set; }
        public string UpdatedName { get; set; }



    }
}
