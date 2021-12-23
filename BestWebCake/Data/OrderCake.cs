namespace BestWebCake.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("OrderCake")]
    public class OrderCakeEntity
    {
        [Key, Column(Order = 0)]
        public Guid OrderId {get;set;}

        [Key, Column(Order = 1)]
        public Guid CakeId {get;set;}

        public virtual OrderEntity OrderEntity {get;set;}

        public virtual CakeEntity CakeEntity {get;set;}
    }
}