using System.ComponentModel.DataAnnotations;
// ReSharper disable StringLiteralTypo

namespace WebAppMVC.Models
{
    public class Person : IModelForEditorForm
    {
        [Display(Name = "Имя"), StringLength(10)]
        public string FirstName { get; set; }
        [Display(Name = "Фамилия"), StringLength(10)]
        public string Surname { get; set; }
        [Display(Name = "Возраст"), Range(0, 100, ErrorMessage = "Age has to be between 0 and 100")]
        public int? Age { get; set; }
        [Display(Name = "Пол")]
        public Sex Sex { get; set; }
    }

    public enum Sex
    {
        Male,
        Female,
        Helicopter
    }
}
