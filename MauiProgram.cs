using Microsoft.Extensions.Logging;
using PollingSystem.MAUI.Services;
using Syncfusion.Maui.Core.Hosting;

namespace PollingSystem.MAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureSyncfusionCore()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<IHttpClientService, HttpClientService>();
            builder.Services.AddSingleton<IPollService, PollService>();
            builder.Services.AddSingleton<IUserService, UserService>();
            builder.Services.AddSingleton<IVoteService, VoteService>();

            

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
