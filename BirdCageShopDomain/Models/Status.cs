using BirdCageShopDbContext.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopDomain.Models
{
    public class Status
    {
        public int Id { get; set; }
        public string StatusState { get; set; } = null!;
        //public virtual ICollection<Order> Orders { get; set; }
    }
}
