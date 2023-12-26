using petsiki.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace petsiki.Models;

public partial class Shelter
{
    [Key]
    public int IdShelter { get; set; }

    [Required]
    //[RegularExpression(@"^[A-ZА-Я]+[a-zA-Z0-9а-яА-Я""'\s-]*$")]
    [StringLength(50)]
    [Display(Name = "Адрес")]
    public string Address { get; set; } = null!;

    [Required]
    //[RegularExpression(@"^[A-ZА-Я]+[a-zA-Z0-9а-яА-Я""'\s-]*$")]
    [Display(Name = "Описание")]
    public string Description { get; set; } = null!;

    [Required]
    [Display(Name = "Кол-во собранных денег")]
    [Column(TypeName = "decimal(10, 2)")]
    public decimal AmountOfMoneyCollected { get; set; }

    public virtual ICollection<Collecting> Collectings { get; set; } = new List<Collecting>();

    public virtual ICollection<Pet> Pets { get; set; } = new List<Pet>();
}
