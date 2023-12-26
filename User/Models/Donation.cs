using petsiki.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace petsiki.Models;

public partial class Donation
{
    [Key]
    public int IdDonation { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Дата")]
    public DateOnly Data { get; set; } = DateOnly.FromDateTime(DateTime.Today);


    [Display(Name = "Сумма")]
    [Column(TypeName = "decimal(10, 2)")]
    public decimal Amount { get; set; }

    [Required]

    [Display(Name = "Пользователь")]
    public int IdUser { get; set; }

    [Required]
    [Display(Name = "Сбор")]
    public int IdCollecting { get; set; }

    [Display(Name = "Сбор")]
    public virtual Collecting IdCollectingNavigation { get; set; } = null!;
    [Display(Name = "Пользователь")]
    public virtual Users IdUserNavigation { get; set; } = null!;
}
