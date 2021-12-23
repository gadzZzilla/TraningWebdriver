namespace BestWebCake.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Crude")]
    public class CrudeEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id {get;set;}

        [Required]
        [Display(Name = "Название товара")]
        [Column(TypeName = "string")]
        public string Name{get;set;}

        [Required]
        [Display(Name = "Цена товара")]
        [Column(TypeName = "double")]
        public double Price{get;set;}

        [Required]
        [Display(Name = "Цена за")]
        [Column(TypeName = "double")]
        public double PriceOfOnce {get;set;}

        public Guid UnitId {get; set ;}

        public UnitEntity UnitEntity {get;set;}

        public virtual ICollection<CakeCrudeEntity> CakeCrudeEntity { get; set; }
    }
}