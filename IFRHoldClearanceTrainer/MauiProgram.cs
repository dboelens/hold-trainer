using IFRHoldClearanceTrainer.models;
using IFRHoldClearanceTrainer.services;
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
				Identifier = "PAE",
				IFRChartImage = "paeifr.png",
				SectionalImage = "paevfr.png"
			},
			new()
			{
				Identifier = "SEA",
				IFRChartImage = "seaifr.png",
				SectionalImage = "seavfr.png"
			},
			new()
			{
				Identifier = "OLM",
				IFRChartImage = "olmifr.png",
				SectionalImage = "olmvfr.png"
			},
			new()
			{
				Identifier = "BTG",
				IFRChartImage = "btgifr.png",
				SectionalImage = "btfvfr.png"
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
				.AddSingleton<IClearenceGenerator, ClearenceGenerater>()
				.AddSingleton<MainPage>()
				.AddSingleton<IList<VOR>>(vorList);
			

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
