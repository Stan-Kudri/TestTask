using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using MudBlazor.Services;
using TestTask.Core;
using TestTask.Core.Export.SheetFillers;
using TestTask.Core.Import;
using TestTask.Core.Import.Importers;
using TestTask.Core.Models;
using TestTask.Core.Models.SortModel;
using TestTask.Core.Models.Users;
using TestTask.Migrations;
using TestTask.MudBlazors.Authenticate;
using TestTask.MudBlazors.Messages;
using TestTask.MudBlazors.Pages.Table.Model;

namespace TestTask.MudBlazors
{
    public static class ServiceDI
    {
        const string ConnectionName = "DbConnection";

        public static void AppWebApplicationBuilder(this WebApplicationBuilder builder)
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
            builder.Services.AddScoped<IUserValidator, UserValidator>();
            builder.Services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();

            builder.Services.Scan(scan => scan
                                    .FromAssemblies(typeof(BaseService<>).Assembly)

                                        .AddClasses(service => service.AssignableTo(typeof(BaseService<>)))
                                        .AsSelf()
                                        .WithScopedLifetime()

                                    .FromAssemblies(typeof(IImporter<>).Assembly, typeof(ExcelImporter<>).Assembly)

                                       .AddClasses(importer => importer.AssignableTo(typeof(IImporter<>)))
                                       .AsImplementedInterfaces()
                                       .WithSingletonLifetime()

                                       .AddClasses(importer => importer.AssignableTo(typeof(ExcelImporter<>)))
                                       .AsSelf()
                                       .WithSingletonLifetime()

                                    .FromAssemblies(typeof(ITableDetailProvider<>).Assembly, typeof(ISortEntity<>).Assembly, typeof(ISheetFiller).Assembly)

                                        .AddClasses(table => table.AssignableTo(typeof(ITableDetailProvider<>)))
                                        .AsImplementedInterfaces()
                                        .WithScopedLifetime()

                                        .AddClasses(sort => sort.AssignableTo(typeof(ISortEntity<>)))
                                        .AsImplementedInterfaces()
                                        .WithScopedLifetime()

                                        .AddClasses(sheetFiller => sheetFiller.AssignableTo<ISheetFiller>())
                                        .AsSelf()
                                        .WithScopedLifetime()
                                    );
        }

        public static void AuthenicateDIBuilder(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<BlazorAppLoginService>();
            builder.Services.AddScoped<AuthenticationStateProvider, BlazorAuthStateProvider>();
            builder.Services.AddTransient<IUsersAuthenticate, UsersAuthenticateService>();
        }
    }
}
