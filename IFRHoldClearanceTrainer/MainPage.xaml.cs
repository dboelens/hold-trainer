namespace IFRHoldClearanceTrainer;

using IFRHoldClearanceTrainer.services;
using CommunityToolkit.Maui.Views;

public partial class MainPage : ContentPage
{
	private IClearenceGenerator clearenceGenerator;

	public MainPage(IClearenceGenerator generator)
	{
		InitializeComponent();
		clearenceGenerator = generator;
	}

	private void OnGenerateClicked(object sender, EventArgs e)
	{
		var clearence = clearenceGenerator.Generate();
		ClearenceBox.Text = clearence.DisplayClearence();

		SemanticScreenReader.Announce(ClearenceBox.Text);
	}
}

