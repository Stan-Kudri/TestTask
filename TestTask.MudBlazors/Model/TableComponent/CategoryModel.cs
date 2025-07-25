using Microsoft.AspNetCore.Components;
using TestTask.Core.Models.Categories;

namespace TestTask.MudBlazors.Model.TableComponent
{
    public class CategoryModel
    {
        private string _name = string.Empty;

        public CategoryModel()
        {
        }

        public CategoryModel(string name) => Name = name;

        [Parameter]
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public Category GetCategory() => new Category(Name);

        public Category GetModifyCategory(int idOldItem) => new Category(Name, idOldItem);

        public void ClearData() => Name = string.Empty;
    }
}
