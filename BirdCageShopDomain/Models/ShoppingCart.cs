using BirdCageShopDbContext.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopDomain.Models
{
	public partial class ShoppingCart
	{
		public int Id { get; set; }

		public int Count { get; set; }
		public DateTime? CreatedAt { get; set; }
		public DateTime? ModifiedAt { get; set; }
		//public DateTime? ExpDate { get; set; }
		public int ProductId { get; set; }
		public string ApplicationUserId { get; set; }
		public virtual ApplicationUser ApplicationUser { get; set; } = null!;
		public virtual Product Product { get; set; } = null!;


        //[NotMapped]
        //public double Price { get; set; }
    }

}
