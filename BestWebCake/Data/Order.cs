namespace BestWebCake.Data
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Order")]
    public class OrderEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id {get;set;}

        [Required]
        [Column(TypeName = "string")]
        public int Nuber{get;set;}

        [Required]
        [Column(TypeName = "double")]
        public double TotalPrice{get;set;}
        public virtual ICollection<IdentityUser> ApplicationUser {get;set;}
        public virtual ICollection<OrderCakeEntity> OrderCakes {get;set;}
    }
}
