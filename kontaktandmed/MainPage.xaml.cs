using System.Collections.ObjectModel;

namespace kontaktandmed
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Contact> Contacts { get; set; }

        public MainPage()
        {
            InitializeComponent();

            Contacts = new ObservableCollection<Contact>
            {
                new Contact { Name = "Example", Phone = "123456789", Email = "example@example.com", Photo = "default.png" }
            };

            contactPicker.ItemsSource = Contacts;
        }

        private async void OnCallClicked(object sender, EventArgs e)
        {
            var selectedContact = contactPicker.SelectedItem as Contact;
            if (selectedContact != null && !string.IsNullOrEmpty(selectedContact.Phone))
            {
                await Launcher.Default.OpenAsync($"tel:{selectedContact.Phone}");
            }
        }

        private async void OnSmsClicked(object sender, EventArgs e)
        {
            var selectedContact = contactPicker.SelectedItem as Contact;
            if (selectedContact != null && !string.IsNullOrEmpty(selectedContact.Phone))
            {
                Uri smsUri = new Uri($"sms:{selectedContact.Phone}?body=Tere!");
                await Launcher.OpenAsync(smsUri);
            }
            else
            {
                await DisplayAlert("Viga", "Valige kontakt!", "OK");
            }
        }

        private async void OnEmailClicked(object sender, EventArgs e)
        {
            var selectedContact = contactPicker.SelectedItem as Contact;
            if (selectedContact != null && !string.IsNullOrEmpty(selectedContact.Email))
            {
                Uri emailUri = new Uri($"mailto:{selectedContact.Email}?subject=kontakt&body=Tere!");
                await Launcher.OpenAsync(emailUri);
            }
            else
            {
                await DisplayAlert("Viga", "Valige kontakt!", "OK");
            }
        }

        private async void OnViewContactsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ContactsPage(Contacts));
        }
    }
}
