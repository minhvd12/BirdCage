using System;
using System.Collections.Generic;

namespace BirdCageShopDbContext.Models
{
    public partial class BankAccount
    {
        public int Id { get; set; }
        public string Bank { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string AccountNo { get; set; } = null!;
        public string ApplicationUserId { get; set; }

        //
        public ApplicationUser ApplicationUser { get; set; }
    }
}
