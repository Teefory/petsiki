using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace petsiki.Models;

public partial class Pet
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdPet { get; set; }

    [Required]
    [Display(Name = "Имя")]
    [StringLength(10)]
    [RegularExpression(@"^[A-ZА-Я]+[a-zA-Zа-яА-Я\s]*$")]
    public string Nickname { get; set; } = null!;

    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "День рождения")]
    public DateOnly DateOfBirth { get; set; }

    [Required]
    [Display(Name = "Описание")]

    public string DescriptionP { get; set; } = null!;
    [Required]
    [Display(Name = "Мед. Сведия")]
    [StringLength(100)]
    [RegularExpression(@"^[A-ZА-Я]+[a-zA-Z0-9а-яА-Я""'\s-]*$")]

    public string MedicalInformation { get; set; } = null!;

    [Display(Name = "Приют")]

    public int IdShelter { get; set; }

    public virtual ICollection<Collecting> Collectings { get; set; } = new List<Collecting>();

    [Display(Name = "Приют")]
    public virtual Shelter IdShelterNavigation { get; set; } = null!;

    public virtual ICollection<RecordingWalk> RecordingWalks { get; set; } = new List<RecordingWalk>();
}
