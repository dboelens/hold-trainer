namespace IFRHoldClearanceTrainer;
using CommunityToolkit.Maui.Views;


public partial class Warning : Popup
{

	public Warning()
	{
		InitializeComponent();
	}

	private async void OnAcknowledge(object sender, EventArgs e)
	{
		var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

         await CloseAsync(token: cts.Token);
	}
}

