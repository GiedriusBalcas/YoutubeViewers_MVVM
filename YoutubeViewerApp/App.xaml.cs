using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System.Windows;
using YoutubeViewerApp_CodeAlong.Stores;
using YoutubeViewerApp_CodeAlong.ViewModels;
using YoutubeViewers.Domain.Commands;
using YoutubeViewers.Domain.Queries;
using YoutubeViewers.EntityFramework;
using YoutubeViewers.EntityFramework.Commands;
using YoutubeViewers.EntityFramework.Queries;
using YoutubeViewerApp_CodeAlong.HostBuilders;

namespace YoutubeViewerApp_CodeAlong
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .AddDbContext()
                .ConfigureServices((context, services) => 
                {
                    services.AddSingleton<IGetAllYoutubeViewersQuery, GetAllYoutubeViewersQuery>();
                    services.AddSingleton<ICreateYoutubeViewerCommand, CreateYoutubeViewerCommand>();
                    services.AddSingleton<IUpdateYoutubeViewerCommand, UpdateYoutubeViewerCommand>();
                    services.AddSingleton<IDeleteYoutubeViewerCommand, DeleteYoutubeViewerCommand>();

                    services.AddSingleton<ModalNavigationStore>();
                    services.AddSingleton<YoutubeViewersStore>();
                    services.AddSingleton<SelectedYoutubeViewerStore>();

                    services.AddTransient<YoutubeViewerViewModel>(CreateYoutubeViewersViewModel);
                    services.AddSingleton<MainViewModel>();

                    services.AddSingleton<MainWindow>((services) => new MainWindow()
                    {
                        DataContext = services.GetRequiredService<MainViewModel>()
                    });
                })
                .Build();


        }

        private YoutubeViewerViewModel CreateYoutubeViewersViewModel(IServiceProvider services)
        {
            return YoutubeViewerViewModel.LoadViewModel(
                services.GetRequiredService<YoutubeViewersStore>(),
                services.GetRequiredService<SelectedYoutubeViewerStore>(),
                services.GetRequiredService<ModalNavigationStore>()
                );
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            YoutubeViewersDbContextFactory youtubeViewersDbContextFactory = _host.Services.GetRequiredService<YoutubeViewersDbContextFactory>();

            using (YoutubeViewersDbContext context = youtubeViewersDbContextFactory.Create())
            {
                context.Database.Migrate();
            }

            MainWindow = _host.Services.GetRequiredService<MainWindow>();

            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.StopAsync();
            _host.Dispose();

            base.OnExit(e);
        }

    }

}
