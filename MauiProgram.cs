using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Boost.Views.Controls;
using Boost.Platforms.Android;
using Boost.ViewModels;
using Fitx_Api;
using Boost.Common;
using CommunityToolkit.Maui.Core;


namespace Boost
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Anton-Regular.ttf", "AntonRegular");
                });
               builder.Services.AddSingleton<IPopupService, PopupService>();

#if DEBUG
            builder.Logging.AddDebug();
#endif
            Microsoft.Maui.Handlers.ElementHandler.ElementMapper.AppendToMapping("Classic", (handler, view) =>
            {
                if (view is CustomEntry)
                {
                    Platforms.Android.CustomMappers.EntryMapper.Map(handler, view);
                }
                if (view is CustomSearchbar)
                {
                    Platforms.Android.CustomMappers.SearchbarMapper.Map(handler, view);
                }
            });

            return builder.Build();
        }
    }
}
