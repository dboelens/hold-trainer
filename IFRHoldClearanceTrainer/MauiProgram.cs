using IFRHoldClearanceTrainer.models;
using IFRHoldClearanceTrainer.services;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;

namespace IFRHoldClearanceTrainer;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var vorList = new List<VOR>{
			new() 
			{
				Identifier = "PAE"
			},
			new()
			{
				Identifier = "SEA"
			}
		};

		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
			.Services
				.AddSingleton<IRandom,RandomWrapper>()
				.AddSingleton<IClearenceGenerator, ClearnceGenerater>()
				.AddSingleton<MainPage>()
				.AddSingleton<IList<VOR>>(vorList);
			

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
