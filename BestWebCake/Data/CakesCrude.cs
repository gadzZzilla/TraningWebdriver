namespace BestWebCake.Data
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("CakesCrudes")]
    public class CakeCrudeEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id {get;set;}

        [Column]
        public Guid CakeId {get;set;}

        [Column]
        public string Name { get; set; }

        public virtual CakeEntity CakeEntity {get;set;}

        [Column(Order = 1)]
        public Guid CrudeId {get;set;}

        public virtual CrudeEntity CrudeEntity {get;set;}

        // [Column]
        // public string CrudeName{get;set;}

        [Column]
        public double WeightCrude {get;set;}

        public Guid UnitId {get;set;}

        public UnitEntity UnitEntity {get;set;}
    }
}