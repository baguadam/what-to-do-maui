using Microsoft.Extensions.Logging;
using WhatToDo.Services;
using WhatToDo.ViewModel;

namespace WhatToDo;

using CommunityToolkit.Maui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            // Initialize the .NET MAUI Community Toolkit by adding the below line of code
            .UseMauiCommunityToolkit()
            // After initializing the .NET MAUI Community Toolkit, optionally add additional fonts
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Continue initializing your .NET MAUI App here

        builder.Services.AddSingleton<AddTaskPage>();
        builder.Services.AddSingleton<AddTaskViewModel>();

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<MainPageViewModel>();

        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "tasks.db3");

        builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<TaskService>(s, dbPath));

        return builder.Build();
    }
}


//public static class MauiProgram
//{
//	public static MauiApp CreateMauiApp()
//	{
//		var builder = MauiApp.CreateBuilder();
//		builder
//			.UseMauiApp<App>()
//			.ConfigureFonts(fonts =>
//			{
//				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
//				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
//			});

//#if DEBUG
//		builder.Logging.AddDebug();
//#endif

//		builder.Services.AddSingleton<AddTaskPage>();
//		builder.Services.AddSingleton<AddTaskViewModel>();

//		builder.Services.AddSingleton<MainPage>();
//		builder.Services.AddSingleton<MainPageViewModel>();

//        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "tasks.db3");

//        builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<TaskService>(s, dbPath));

//		return builder.Build();
//	}
//}
