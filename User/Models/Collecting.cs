using petsiki.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace petsiki.Models;

public partial class Collecting
{
    [Key]
    public int IdCollecting { get; set; }
    [Required]
    [RegularExpression(@"^[A-ZА-Я]+[a-zA-Z0-9а-яА-Я""'\s-]*$")]//первая буква прописная, цифры и буквы можно и спец символы
    [Display(Name = "Описание")]
    public string DescriptionC { get; set; } = null!;


    [Required]
    [Display(Name = "Требуемая сумма")]
    [Column(TypeName = "decimal(10, 2)")]
    public decimal RequiredAmount { get; set; }


    [Required]
    [Display(Name = "Уже собрано")]
    [Column(TypeName = "decimal(10, 2)")]
    public decimal AlreadyAssembled { get; set; }


    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Дата открытия сбора")]
    public DateOnly OpeningDate { get; set; }


    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Дата закрытия сбора")]
    public DateOnly ClosingDate { get; set; }


    [Display(Name = "Питомец")]
    public int? IdPet { get; set; }

    [Required]
    [Display(Name = "Приют")]
    public int IdShelter { get; set; }

    [Required]
    [Display(Name = "Пользователь")]
    public int IdUser { get; set; }

    public virtual ICollection<Donation> Donations { get; set; } = new List<Donation>();

    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();

    [Display(Name = "Питомец")]
    public virtual Pet? IdPetNavigation { get; set; }

    [Display(Name = "Приют")]
    public virtual Shelter IdShelterNavigation { get; set; } = null!;
    [Display(Name = "Пользователь")]
    public virtual Users IdUserNavigation { get; set; } = null!;
}

