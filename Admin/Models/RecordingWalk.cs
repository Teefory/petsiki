using petsk.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace petsk.Models;

public partial class RecordingWalk
{
    [Display(Name = "Питомец")]
    public int IdPet { get; set; }
    //[DataType(DataType.Date)]
    [Display(Name = "Дата")]
    public DateOnly DataR { get; set; }

    [Key]
    [Display(Name = "ID прогулки")]
    public int IdRecordingWalk { get; set; }


   

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
