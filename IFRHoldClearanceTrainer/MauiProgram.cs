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
				VFRCoordinate = new(){X = 6842, Y = 2797.5},
				IFRCoordinate = new(){X = 4754,Y = 3929.5}
			},
			new()
			{
				Identifier = "SEA",
				VFRCoordinate = new(){X = 6805, Y = 3938},
				IFRCoordinate = new(){X = 4687.5,Y = 5422}
			},
			new()
			{
				Identifier = "OLM",
				VFRCoordinate = new(){X = 5693.5, Y = 5150},
				IFRCoordinate = new(){X = 3446.5,Y = 6819}
			},
			new()
			{
				Identifier = "UBG",
				VFRCoordinate = new(){X = 5427.5, Y = 9420.5},
				IFRCoordinate = new(){X = 3314.5,Y = 11692.5}
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
				.AddSingleton<IDirectionRules,BasicDirectionRulesEngine>()
				.AddSingleton<MainPage>()
				.AddSingleton<IList<VOR>>(vorList);
			

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
