namespace IFRHoldClearanceTrainer;

using CommunityToolkit.Maui.Views;
using IFRHoldClearanceTrainer.models;
using IFRHoldClearanceTrainer.services;

public partial class MainPage : ContentPage
{
	private IClearenceGenerator clearenceGenerator;
	private ChartType chartType = ChartType.VFRSectional;
	private Fix currentFix;

	public MainPage(IClearenceGenerator generator)
	{
		InitializeComponent();
		clearenceGenerator = generator;
		this.ShowPopup(new Warning());
		Routing.RegisterRoute("DrawingTest",typeof(DrawingViewTest));
	}

	private async void OnDrawingModeClicked(object sender, EventArgs e){
		await AppShell.Current.GoToAsync("DrawingTest");
	}
	private async void OnChartTypeChange(object sender, EventArgs e)
	{
		chartType = chartType == ChartType.VFRSectional? ChartType.IFREnRoute: ChartType.VFRSectional;
		UpdateImage();
		
	}

	private async void OnGenerateClicked(object sender, EventArgs e)
	{
		var clearence = clearenceGenerator.Generate();
		ClearenceBox.Text = clearence.DisplayClearence();
		currentFix = clearence.Fix;
		UpdateImage();	
	}
	private void UpdateImage(){
		try{
			var imagePath = clearenceGenerator.GetFixResource(currentFix.FixIdentifier, chartType);
			if(string.IsNullOrEmpty(imagePath) && Chart.IsVisible){
				imagePath = "uhoh.jpg";
			}
			DrawingViewControl.Lines = new System.Collections.ObjectModel.ObservableCollection<CommunityToolkit.Maui.Core.IDrawingLine>();
			Chart.Source = imagePath;
			Chart.WidthRequest = Window.Width;
			DrawingViewControl.WidthRequest = Chart.Width;
			DrawingViewControl.HeightRequest = Chart.Height;
			DrawingViewControl.BackgroundColor = Color.FromRgba(255,255,255,0);
			Chart.IsVisible = true;
			DrawingViewControl.IsVisible = true;
		}
		catch(Exception exp){
			Chart.Source = "uhoh.jpg";
			Chart.IsVisible = true;
		}
	}

}

