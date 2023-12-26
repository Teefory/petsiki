using petsiki.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace petsiki.Models;

public partial class Expense
{
    [Key]
    public int IdExpenses { get; set; }

    [Required]
    [RegularExpression(@"^[A-ZА-Я]+[a-zA-Zа-яА-Я\s]*$")]
    [StringLength(20)]
    [Display(Name = "Вид расхода")]
    public string TypeExpense { get; set; } = null!;

    [Required]
    [Display(Name = "Количество")]
    public int Quantity { get; set; }


    [Required]
    [Display(Name = "Затраченная сумма")]
    [Column(TypeName = "decimal(10, 2)")]
    public decimal AmountSpent { get; set; }


    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Дата")]
    public DateOnly Data { get; set; }

    [Required]
    [Display(Name = "Фото")]
    public string Photo { get; set; } = null!;

    [Display(Name = "Сбор")]
    public int? IdCollecting { get; set; }
    [Display(Name = "Сбор")]
    public virtual Collecting? IdCollectingNavigation { get; set; }
}
