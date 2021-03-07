using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Recipes.UI.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Recipes.UI.Helpers.ImageAddDel
{
    public class ImageHelper
    {
        private readonly IWebHostEnvironment _env;
        private readonly string _wwwroot;
        private const string imgFolder = "Pictures";
        private const string foodCategoryImageFolder = "foodCategoryImgs";
        private const string recipeImageFolder = "recipesImgs";
        private const string userImageFolder = "userImgs";

        public ImageHelper(IWebHostEnvironment env)
        {
            _env = env;
            _wwwroot = _env.WebRootPath;
        }

        public async Task<string> UploadPhoto(string name, IFormFile pictureFile, PictureType type, string folderName = null)
        {
            if (folderName == null)
            {
                switch (type)
                {
                    case PictureType.FoodCategoryPic:
                        folderName = foodCategoryImageFolder;
                        break;
                    case PictureType.RecipePic:
                        folderName = recipeImageFolder;
                        break;
                    case PictureType.UserPic:
                        folderName = userImageFolder;
                        break;
                }
            }

            if (!Directory.Exists($"{_wwwroot}/{imgFolder}/{folderName}"))
            {
                Directory.CreateDirectory($"{_wwwroot}/{imgFolder}/{folderName}");
            }

            Regex regex = new Regex("[*'\",._&#^@ ]");
            name = regex.Replace(name, string.Empty);

            string fileExtension = Path.GetExtension(pictureFile.FileName);
            DateTime date = DateTime.Now;

            string newFile = $"{name}_{date.DateUnderscore()}{fileExtension}";
            var path = Path.Combine($"{_wwwroot}/{imgFolder}/{folderName}", newFile);

            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await pictureFile.CopyToAsync(stream);
            }

            return newFile;
        }
    }
}
