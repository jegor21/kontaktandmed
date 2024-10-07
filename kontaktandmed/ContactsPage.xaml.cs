using System.Collections.ObjectModel;

namespace kontaktandmed
{
    public partial class ContactsPage : ContentPage
    {
        public ObservableCollection<Contact> Contacts { get; set; }

        public ContactsPage(ObservableCollection<Contact> contacts)
        {
            InitializeComponent();
            Contacts = contacts;
            PopulateContacts();
        }

        private void PopulateContacts()
        {
            contactsTableSection.Clear();

            foreach (var contact in Contacts)
            {
                var viewCell = new ViewCell();
                var grid = new Grid
                {
                    Padding = new Thickness(10),
                    RowDefinitions =
            {
                new RowDefinition { Height = GridLength.Auto }, // Для имени
                new RowDefinition { Height = GridLength.Auto }, // Для телефона
                new RowDefinition { Height = GridLength.Auto }, // Для email
                new RowDefinition { Height = GridLength.Auto }  // Для описания
            },
                    ColumnDefinitions =
            {
                new ColumnDefinition { Width = GridLength.Star } // Один столбец на всю ширину
            }
                };

                // Создаем элементы для отображения данных контакта
                var nameLabel = new Label
                {
                    Text = contact.Name,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 18,
                    LineBreakMode = LineBreakMode.WordWrap
                };

                var phoneLabel = new Label
                {
                    Text = "Telefon: " + contact.Phone,
                    FontSize = 14,
                    LineBreakMode = LineBreakMode.WordWrap
                };

                var emailLabel = new Label
                {
                    Text = "Email: " + contact.Email,
                    FontSize = 14,
                    LineBreakMode = LineBreakMode.WordWrap
                };

                var descriptionLabel = new Label
                {
                    Text = "Kirjeldus: " + contact.Description,
                    FontAttributes = FontAttributes.Italic,
                    FontSize = 12,
                    LineBreakMode = LineBreakMode.WordWrap
                };

                // Добавляем элементы в Grid
                grid.Children.Add(nameLabel);         // Добавляем элемент в Grid
                Grid.SetRow(nameLabel, 0);            // Указываем, что nameLabel в 1-й строке

                grid.Children.Add(phoneLabel);        // Добавляем элемент в Grid
                Grid.SetRow(phoneLabel, 1);           // Указываем, что phoneLabel во 2-й строке

                grid.Children.Add(emailLabel);        // Добавляем элемент в Grid
                Grid.SetRow(emailLabel, 2);           // Указываем, что emailLabel в 3-й строке

                grid.Children.Add(descriptionLabel);  // Добавляем элемент в Grid
                Grid.SetRow(descriptionLabel, 3);     // Указываем, что descriptionLabel в 4-й строке

                viewCell.View = grid;

                // Добавляем возможность редактирования контакта по нажатию
                viewCell.Tapped += async (sender, e) =>
                {
                    await Navigation.PushAsync(new EditContactPage(contact, Contacts));
                };

                // Добавляем ячейку в секцию
                contactsTableSection.Add(viewCell);
            }
        }






        // Обработчик добавления нового контакта
        private async void OnAddContactClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditContactPage(null, Contacts));
        }
    }
}
