﻿using MaterialSkin;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;
using TestTask.ChildForms.Import;
using TestTask.Core;
using TestTask.Core.Import;
using TestTask.Core.Import.Importers;
using TestTask.Core.Models.Categories;
using TestTask.Core.Models.Companies;
using TestTask.Core.Models.Products;
using TestTask.Core.Models.Types;
using TestTask.Core.Models.Users;
using TestTask.Forms;
using TestTask.Forms.Categories;
using TestTask.Forms.Companies;
using TestTask.Forms.Products;
using TestTask.Forms.Type;
using TestTask.Messages;
using TestTask.Migrations;

namespace TestTask
{
    public static class Program
    {
        private const string ConnectionName = "DbConnection";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var collection = new ServiceCollection()
                .AddSingleton(e => new DbContextFactory(ConnectionName))
                .AddScoped(e => e.GetRequiredService<DbContextFactory>().Create())
                .AddScoped<IMessageBox>(e => new MessageBoxShow())
                .AddScoped<UserService>()
                .AddScoped<CompanyRepository>()
                .AddScoped<ProductRepository>()
                .AddScoped<CategoryRepository>()
                .AddScoped<ProductTypeRepository>()
                .AddScoped<UserValidator>()
                .AddSingleton<BaseForm>()
                .AddTransient<LoginForm>()
                .AddTransient<RegistrationForm>()
                .AddTransient<ImportDatabaseForm>()
                .AddTransient<MainForm>()
                .AddTransient<AddItemCompanyForm>()
                .AddTransient<EditItemCompanyForm>()
                .AddTransient<AddItemProductForm>()
                .AddTransient<EditItemProductForm>()
                .AddTransient<AddCategoryForm>()
                .AddTransient<EditCategoryForm>()
                .AddTransient<AddProductTypeForm>()
                .AddTransient<EditProductTypeForm>()
                .AddScoped<MessageByTable<ProductType>>()
                .AddScoped<MessageByTable<Product>>()
                .AddSingleton(e => new OpenFileDialog { Filter = "Excel Files |*.xlsx;*.xls;*.xlsm" })
                .AddSingleton(e => new SaveFileDialog() { Filter = "Excel Files |*.xlsx;*.xls;*.xlsm" })
                .AddSingleton(e => new CompanyImporter())
                .AddSingleton(e => new ProductImporter())
                .AddSingleton(e => new CategoryImporter())
                .AddSingleton(e => new TypeProductImporter())
                .AddSingleton(e => new ExcelImporter<Company>(e.GetRequiredService<CompanyImporter>()))
                .AddSingleton(e => new ExcelImporter<Product>(e.GetRequiredService<ProductImporter>()))
                .AddSingleton(e => new ExcelImporter<Category>(e.GetRequiredService<CategoryImporter>()))
                .AddSingleton(e => new ExcelImporter<ProductType>(e.GetRequiredService<TypeProductImporter>()));

            var container = collection.BuildServiceProvider();

            using (var scope = container.CreateScope())
            {
                using (var loginForm = scope.ServiceProvider.GetRequiredService<LoginForm>())
                {
                    var materialSkinManager = MaterialSkinManager.Instance;
                    materialSkinManager.AddFormToManage(loginForm);
                    materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
                    materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey600, Primary.BlueGrey400, Primary.Grey800, Accent.Pink100, TextShade.WHITE);
                    Application.EnableVisualStyles();
                    Application.Run(loginForm);
                }
            }
        }
    }
}
