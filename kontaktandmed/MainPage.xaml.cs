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

        // Обработка нажатия на кнопку "Helista"
        private async void OnCallClicked(object sender, EventArgs e)
        {
            var selectedContact = contactPicker.SelectedItem as Contact;
            if (selectedContact != null && !string.IsNullOrEmpty(selectedContact.Phone))
            {
                await Launcher.Default.OpenAsync($"tel:{selectedContact.Phone}");
            }
        }

        // Обработка нажатия на кнопку "Saada SMS"
        private async void OnSmsClicked(object sender, EventArgs e)
        {
            var selectedContact = contactPicker.SelectedItem as Contact;
            if (selectedContact != null && !string.IsNullOrEmpty(selectedContact.Phone))
            {
                if (Sms.Default.IsComposeSupported)
                {
                    SmsMessage sms = new SmsMessage("Hello!", selectedContact.Phone);
                    await Sms.Default.ComposeAsync(sms);
                }
                else
                {
                    await DisplayAlert("Ошибка", "Отправка SMS не поддерживается на этом устройстве", "OK");
                }
            }
            else
            {
                await DisplayAlert("Ошибка", "Выберите контакт для отправки SMS", "OK");
            }
        }

        private async void OnEmailClicked(object sender, EventArgs e)
        {
            var selectedContact = contactPicker.SelectedItem as Contact;
            if (selectedContact != null && !string.IsNullOrEmpty(selectedContact.Email))
            {
                if (Email.Default.IsComposeSupported)
                {
                    EmailMessage emailMessage = new EmailMessage
                    {
                        Subject = "Контактная информация",
                        Body = "Привет!",
                        To = new List<string> { selectedContact.Email }
                    };
                    await Email.Default.ComposeAsync(emailMessage);
                }
                else
                {
                    await DisplayAlert("Ошибка", "Отправка Email не поддерживается на этом устройстве", "OK");
                }
            }
            else
            {
                await DisplayAlert("Ошибка", "Выберите контакт для отправки Email", "OK");
            }
        }

        // Переход на страницу списка контактов
        private async void OnViewContactsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ContactsPage(Contacts));
        }
    }
}
