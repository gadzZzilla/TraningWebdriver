namespace BestWebCake.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("Unit")]
    public class UnitEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Ед. измерения")]
        public string ShortName { get; set; }

        [Required]
        [Display(Name = "Ед.и.")]
        public string LongName { get; set; }
        
        [Required]
        [Display(Name = "Вес")]
        public double Weight { get; internal set; }

        public List<CrudeEntity> CrudeEntity { get; set; }
        public List<CakeCrudeEntity> CakeCrudeEntity { get; internal set; }
    }
}