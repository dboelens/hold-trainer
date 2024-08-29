namespace IFRHoldClearanceTrainer;

using CommunityToolkit.Maui.Views;
using IFRHoldClearanceTrainer.services;

public partial class MainPage : ContentPage
{
	private IClearenceGenerator clearenceGenerator;

	public MainPage(IClearenceGenerator generator)
	{
		InitializeComponent();
		clearenceGenerator = generator;
		this.ShowPopup(new Warning());
	}

	private async void OnGenerateClicked(object sender, EventArgs e)
	{
		var clearence = clearenceGenerator.Generate();
		ClearenceBox.Text = clearence.DisplayClearence();

		SemanticScreenReader.Announce(ClearenceBox.Text);		
	}
}

