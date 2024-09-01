namespace IFRHoldClearanceTrainer;

using System.Collections;
using CommunityToolkit.Maui.Core.Views;
using CommunityToolkit.Maui.Views;
using IFRHoldClearanceTrainer.models;
using IFRHoldClearanceTrainer.services;
using Microsoft.Maui.Controls.Shapes;

public partial class MainPage : ContentPage
{
	private IClearenceGenerator clearenceGenerator;
	private ChartType chartType = ChartType.VFRSectional;
	private Fix? currentFix;
	private string ifrImageSource = "ifrlow.tif";
	private string vfrImageSource = "seattlesectional.tif";

	private Coordinate defaultVFRCoordinate = new() {X = 6942, Y = 3923.5};

	private Stack drawQueue = new Stack();

	public MainPage(IClearenceGenerator generator)
	{
		InitializeComponent();
		clearenceGenerator = generator;
		this.ShowPopup(new Warning());
		VFRChart.Source = vfrImageSource;
		IFRChart.Source = ifrImageSource;
		VFRChart.AnchorX = 0;
		VFRChart.AnchorY = 0;
		IFRChart.AnchorX = 0;
		IFRChart.AnchorY = 0;
		VFRChart.Scale = 1;
		IFRChart.Scale = 1;
		VFRChart.IsVisible = true;
		//MoveChartPosition(VFRContainer, defaultVFRCoordinate);
	}

	private void OnChartTypeChange(object sender, EventArgs e)
	{
		if(chartType == ChartType.VFRSectional){
			chartType = ChartType.IFREnRoute;
			VFRChart.IsVisible = false;
			VFRContainer.IsVisible=false;
			IFRChart.IsVisible = true;
			IFRContainer.IsVisible=true;
			var coordinate = clearenceGenerator.GetFixChartCoordinate(currentFix.FixIdentifier, chartType);
			MoveChartPosition(IFRContainer, coordinate);
		}
		else
		{
			chartType = ChartType.VFRSectional;
			VFRChart.IsVisible = true;
			VFRContainer.IsVisible=true;
			IFRChart.IsVisible = false;
			IFRContainer.IsVisible=false;
			var coordinate = clearenceGenerator.GetFixChartCoordinate(currentFix.FixIdentifier, chartType);
			MoveChartPosition(VFRContainer, coordinate);
		}
	}
	private void OnDrawClicked(object sender, EventArgs e){
		if(DrawingViewControl.IsEnabled)
		{
			DrawingViewControl.WidthRequest = 0;
			DrawingViewControl.HeightRequest = 0;
			DrawingViewControl.BackgroundColor = Color.FromRgba(255,255,255,0);
			DrawButton.Text="Draw";
			DrawingViewControl.IsEnabled = false;
			DrawControls.IsVisible=false;
			drawQueue.Clear();
		}
		else 
		{
			DrawingViewControl.WidthRequest = Math.Max(Window.Width, Window.Height);
			DrawingViewControl.HeightRequest = Math.Max(Window.Width, Window.Height);
			DrawingViewControl.BackgroundColor = Color.FromRgba(255,255,255,0);
			DrawButton.Text="Stop Drawing";
			DrawingViewControl.IsEnabled = true;
			DrawControls.IsVisible=true;
		}	
	}

	private void OnUndoClicked(object sender, EventArgs e)
	{
		if(DrawingViewControl.Lines.Count > 0)
		{
			var lastLine = DrawingViewControl.Lines.Last();
			drawQueue.Push(lastLine);
			DrawingViewControl.Lines.RemoveAt(DrawingViewControl.Lines.Count-1);
		}
	}
	private void OnRedoClicked(object sender, EventArgs e)
	{
		if(drawQueue.Count > 0)
		{
			var line = (DrawingLine) drawQueue.Pop();
			DrawingViewControl.Lines.Add(line);
		}
	}

	private void OnChartIsLoaded(object sender, EventArgs e)
	{
		if(chartType == ChartType.VFRSectional){
			MoveChartPosition(VFRContainer, defaultVFRCoordinate);
		}
		else{
			MoveChartPosition(IFRContainer, defaultVFRCoordinate);
		}
	}

	private void OnGenerateClicked(object sender, EventArgs e)
	{
		var clearence = clearenceGenerator.Generate();
		ClearenceBox.Text = clearence.DisplayClearence();
		currentFix = clearence.Fix;

		var vorCoordinate = clearenceGenerator.GetFixChartCoordinate(clearence.Fix.FixIdentifier, chartType);

		if(chartType == ChartType.VFRSectional){
			VFRChart.IsVisible = true;
			IFRChart.IsVisible = false;
			MoveChartPosition(VFRContainer, vorCoordinate);
		}
		else{
			VFRChart.IsVisible = false;
			IFRChart.IsVisible = true;
			MoveChartPosition(IFRContainer, vorCoordinate);
		}

		DrawingViewControl.Clear();
	}
	

	private void MoveChartPosition(InteractionContainer container, Coordinate coordinates)
	{
		var source = container.Content;
		double boundsX = source.Width;
		double boundsY = source.Height;
		
		try{
			var x = Math.Clamp(-coordinates.X, -boundsX, boundsX);
			var y = Math.Clamp(-coordinates.Y, -boundsY, boundsY);
			source.TranslationX = x;
			container.panX = x;
			source.TranslationY = y; 
			container.panY = y;
		}
		catch{
			
		}
	}

}

