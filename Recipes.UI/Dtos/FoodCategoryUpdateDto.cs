using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Recipes.UI.Dtos
{
    public class FoodCategoryUpdateDto
    {
        [DisplayName("Kategori Adı")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir.")]
        [MaxLength(100, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır.")]
        [DataType(DataType.Text)]
        public string CategoryName { get; set; }

        [DisplayName("Resim")]
        [Required(ErrorMessage = "Lütfen, bir {0} seçiniz.")]
        [DataType(DataType.Upload)]
        public IFormFile PictureFile { get; set; }
        public string CategoryPicture { get; set; } = "default.jpg";

        [DisplayName("Kategori Açıklaması")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir.")]
        [MaxLength(100, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır.")]
        [DataType(DataType.Text)]
        public string CategoryDescription { get; set; }
    }
}
