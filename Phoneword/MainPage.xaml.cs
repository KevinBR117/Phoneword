namespace Phoneword
{
    public partial class MainPage : ContentPage
    {
        // int count = 0;
        
        public MainPage()
        {
            InitializeComponent();
        }

        /*private void OnCounterClicked(object sender, EventArgs e)
        {
            count += 1;

            if (count == 1)
                CounterBtn.Text = $"Pulsaste {count} vez";
            else
                CounterBtn.Text = $"Pulsaste {count} veces";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }*/

        string translatedNumber;

        private void OnTranslate(object sender, EventArgs e)
        {
            string enteredNumber = PhoneNumberText.Text;
            translatedNumber = Core.PhonewordTranslator.ToNumber(enteredNumber);

            if(!string.IsNullOrEmpty(translatedNumber))
            {
                CallButton.IsEnabled = true;
                CallButton.Text = "call " + translatedNumber;
            }
            else
            {
                CallButton.IsEnabled = false;
                CallButton.Text = "call";
            }
        }

        async void OnCall(object sender, EventArgs e)
        {
            if (await this.DisplayAlert(
                "Dial a number",
                "Would you like to call " + translatedNumber + "?",
                "Yes",
                "No"
                ))
            {
                // TODO: dial the phone
                try
                {
                    if (PhoneDialer.Default.IsSupported)
                        PhoneDialer.Default.Open(translatedNumber);
                }
                catch (ArgumentNullException)
                {
                    await DisplayAlert("Unable to dial", "Phone number was not valid.", "OK");
                }
                catch (Exception)
                {
                    // Other error has occurred.
                    await DisplayAlert("Unable to dial", "Phone dialing failed.", "OK");
                }
            }
        }
    }
}