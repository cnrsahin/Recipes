using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Recipes.UI.Dtos
{
    public class FoodCategoryAddDto
    {
        [DisplayName("Kategori Adı")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir.")]
        [MaxLength(100, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır.")]
        [DataType(DataType.Text)]
        public string CategoryName { get; set; }

        [DisplayName("Kategori Fotoğrafı")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir.")]
        public string CategoryPicture { get; set; } = "default.jpg";

        [DisplayName("Kategori Açıklaması")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir.")]
        [MaxLength(100, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır.")]
        [DataType(DataType.Text)]
        public string CategoryDescription { get; set; }
    }
}
