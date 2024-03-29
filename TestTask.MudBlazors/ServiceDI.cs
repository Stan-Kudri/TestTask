﻿using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using MudBlazor.Services;
using TestTask.Core;
using TestTask.Core.Export.SheetFillers;
using TestTask.Core.Import;
using TestTask.Core.Import.Importers;
using TestTask.Core.Models;
using TestTask.Core.Models.Categories;
using TestTask.Core.Models.Companies;
using TestTask.Core.Models.Products;
using TestTask.Core.Models.Types;
using TestTask.Core.Models.Users;
using TestTask.Migrations;
using TestTask.MudBlazors.Authenticate;
using TestTask.MudBlazors.Messages;
using TestTask.MudBlazors.Pages.Table.Model;
using TestTask.MudBlazors.Pages.Table.PageTableProvider;
using TestTask.MudBlazors.Pages.Table.PageTableView;

namespace TestTask.MudBlazors
{
    public static class ServiceDI
    {
        const string ConnectionName = "DbConnection";

        public static void AppWebApplicationBuilder(this WebApplicationBuilder? builder)
        {
            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddMudServices(configuration =>
            {
                configuration.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
                configuration.SnackbarConfiguration.HideTransitionDuration = 100;
                configuration.SnackbarConfiguration.ShowTransitionDuration = 100;
                configuration.SnackbarConfiguration.VisibleStateDuration = 3000;
                configuration.SnackbarConfiguration.ShowCloseIcon = false;
                configuration.SnackbarConfiguration.ClearAfterNavigation = false;
            });
            builder.Services.AddSingleton(e => new DbContextFactory(ConnectionName));
            builder.Services.AddScoped(e => e.GetRequiredService<DbContextFactory>().Create());
            builder.Services.AddScoped<IMessageBox, MessageDialog>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<CompanyRepository>();
            builder.Services.AddScoped<ProductRepository>();
            builder.Services.AddScoped<CategoryRepository>();
            builder.Services.AddScoped<ProductTypeRepository>();
            builder.Services.AddScoped<UserValidator>();
            builder.Services.AddSingleton(e => new CompanyImporter());
            builder.Services.AddSingleton(e => new ProductImporter());
            builder.Services.AddSingleton(e => new ProductImporter());
            builder.Services.AddSingleton(e => new CategoryImporter());
            builder.Services.AddSingleton(e => new TypeProductImporter());
            builder.Services.AddSingleton(e => new ExcelImporter<Company>(e.GetRequiredService<CompanyImporter>()));
            builder.Services.AddSingleton(e => new ExcelImporter<Product>(e.GetRequiredService<ProductImporter>()));
            builder.Services.AddSingleton(e => new ExcelImporter<Category>(e.GetRequiredService<CategoryImporter>()));
            builder.Services.AddSingleton(e => new ExcelImporter<ProductType>(e.GetRequiredService<TypeProductImporter>()));
            builder.Services.AddScoped<CompanySheetFiller>();
            builder.Services.AddScoped<CategorySheetFiller>();
            builder.Services.AddScoped<TypeSheetFiller>();
            builder.Services.AddScoped<ProductSheetFiller>();

            builder.Services.AddScoped<CompanyDetailProvider>();
            builder.Services.AddScoped<ITableDetailProvider<Company>>(e => e.GetRequiredService<CompanyDetailProvider>());
            builder.Services.AddScoped<ISortEntity<Company>>(e => new SortCompany());

            builder.Services.AddScoped<CategoryDetailProvider>();
            builder.Services.AddScoped<ITableDetailProvider<Category>>(e => e.GetRequiredService<CategoryDetailProvider>());
            builder.Services.AddScoped<ISortEntity<Category>>(e => new SortCategories());

            builder.Services.AddScoped<TypeDetailProvider>();
            builder.Services.AddScoped<ITableDetailProvider<ProductType>>(e => e.GetRequiredService<TypeDetailProvider>());
            builder.Services.AddScoped<ISortEntity<ProductType>>(e => new SortProductType());

            builder.Services.AddScoped<ProductDetailProvider>();
            builder.Services.AddScoped<ITableDetailProvider<Product>>(e => e.GetRequiredService<ProductDetailProvider>());
            builder.Services.AddScoped<ISortEntity<Product>>(e => new SortProduct());
        }

        public static void AuthenicateDIBuilder(this WebApplicationBuilder? builder)
        {
            builder.Services.AddTransient<BlazorAppLoginService>();
            builder.Services.AddScoped<AuthenticationStateProvider, BlazorAuthStateProvider>();
            builder.Services.AddTransient<IUsersAuthenticate, UsersAuthenticateService>();
        }
    }
}
