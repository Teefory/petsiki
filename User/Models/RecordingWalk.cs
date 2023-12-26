using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace petsiki.Models;

public partial class RecordingWalk
{
    [Key]
    [Display(Name = "Питомец")]
    public int IdPet { get; set; }

    [Key]
    [DataType(DataType.Date)]
    [Display(Name = "Дата")]
    public DateOnly DataR { get; set; }

    [Display(Name = "Пользователь")]
    public int IdUser { get; set; }

    [Display(Name = "Время начала")]
    [Required]
    [DataType(DataType.Time)]
    public TimeOnly BeginWalk { get; set; }

    [Display(Name = "Время конца")]
    [Required]
    [DataType(DataType.Time)]

    public TimeOnly EndWalk { get; set; }

    [Display(Name = "Питомец")]

    public virtual Pet IdPetNavigation { get; set; } = null!;

    [Display(Name = "Пользователь")]
    public virtual Users IdUserNavigation { get; set; } = null!;
}
