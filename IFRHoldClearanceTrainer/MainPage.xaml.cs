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
		SemanticScreenReader.Announce(ClearenceBox.Text);		
	}

	private void UpdateImage(){
		try{
			var imagePath = clearenceGenerator.GetFixResource(currentFix.FixIdentifier, chartType);
			if(string.IsNullOrEmpty(imagePath) && Chart.IsVisible){
				imagePath = "uhoh.jpg";
			}
			Chart.Source = imagePath;
			Chart.IsVisible = true;
		}
		catch(Exception exp){
			Chart.Source = "uhoh.jpg";
			Chart.IsVisible = true;
		}
	}
}

