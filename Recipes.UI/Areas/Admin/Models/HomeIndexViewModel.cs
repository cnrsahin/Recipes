namespace Recipes.UI.Areas.Admin.Models
{
    public class HomeIndexViewModel
    {
        public int WaitConfirmRecipeCount { get; set; }
        public int WaitConfirmFoodCategoryCount { get; set; }
        public int WaitConfirmCommentCount { get; set; }

        public int NotConfirmedAndDeletedRecipeCount { get; set; }
        public int NotConfirmedAndDeletedFoodCategoryCount { get; set; }
        public int NotConfirmedAndDeletedCommentCount { get; set; }
    }
}
