using NpgsqlTypes;
using petsiki.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace petsiki.Models
{

    public partial class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUser { get; set; }


        [Required]
        [RegularExpression(@"^[A-ZА-Я]+[a-zA-Zа-яА-Я\s]*$")]
        [StringLength(15)]
        [Display(Name = "Имя")]
        public string Name { get; set; } = null!;

        [Required]
        [RegularExpression(@"^[A-ZА-Я]+[a-zA-Zа-яА-Я\s]*$")]
        [StringLength(15)]
        [Display(Name = "Фамилия")]
        public string Surname { get; set; } = null!;

        [Required]
        [RegularExpression(@"^[A-ZА-Я]+[a-zA-Zа-яА-Я\s]*$")]
        [StringLength(15)]
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; } = null!;


        [Required]
        [RegularExpression(@"^[A-ZА-Я]+[a-zA-Z0-9а-яА-Я""'\s-]*$")]
        [StringLength(20)]
        [Display(Name = "Логин")]
        public string Login { get; set; } = null!;


        [Required]
        [RegularExpression(@"^[A-ZА-Я]+[a-zA-Z0-9а-яА-Я""'\s-]*$")]
        [StringLength(32)]
        [Display(Name = "Пароль")]
        public string Password { get; set; } = null!;

        //public string Role { get; set; } 

        [Display(Name = "Роль")]
        public CompetitionsRole Role { get; set; } 


        public virtual ICollection<Collecting> Collectings { get; set; } = new List<Collecting>();

        public virtual ICollection<Donation> Donations { get; set; } = new List<Donation>();

        public virtual ICollection<RecordingWalk> RecordingWalks { get; set; } = new List<RecordingWalk>();

    }

    // public enum Role { Users, Administrator };
    public enum CompetitionsRole
    {
        [PgName("User")]
        User,
        [PgName("Administrator")]
        Administrator

    }
}
