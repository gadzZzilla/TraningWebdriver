namespace BestWebCake.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("Cake")]
    public class CakeEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id {get;set;}

        [Column(TypeName = "string")]
        [Display(Name = "Описание торта")]
        public string Description{get;set;}

        [Required]
        [Column(TypeName = "string")]
        [Display(Name = "Название торта")]
        public string Name{get;set;}

        [Required]
        [Column(TypeName = "double")]
        [Display(Name = "Вес торта")]
        public double Weight {get;set;}

        [Required]
        [Column(TypeName = "double")]
        [Display(Name = "Цена торта")]
        public double Price{get;set;}

        [Display(Name = "Предпросмотр")]
        public byte[] Image {get;set;}

        public string ContentType {get;set;}

        [Display(Name = "Картинка")]
        public string FilePath {get;set;}
        public virtual ICollection<CakeCrudeEntity> CakeCrudeEntity {get;set;}
    }
}