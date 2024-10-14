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
                new RowDefinition { Height = GridLength.Auto }, 
                new RowDefinition { Height = GridLength.Auto }, 
                new RowDefinition { Height = GridLength.Auto }, 
                new RowDefinition { Height = GridLength.Auto }  
            },
                    ColumnDefinitions =
            {
                new ColumnDefinition { Width = GridLength.Star } 
            }
                };

                
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

                
                grid.Children.Add(nameLabel);         
                Grid.SetRow(nameLabel, 0);            

                grid.Children.Add(phoneLabel);        
                Grid.SetRow(phoneLabel, 1);           

                grid.Children.Add(emailLabel);        
                Grid.SetRow(emailLabel, 2);           

                grid.Children.Add(descriptionLabel);  
                Grid.SetRow(descriptionLabel, 3);     

                viewCell.View = grid;

                viewCell.Tapped += async (sender, e) =>
                {
                    await Navigation.PushAsync(new EditContactPage(contact, Contacts));
                };
                
                contactsTableSection.Add(viewCell);
            }
        }
        
        private async void OnAddContactClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditContactPage(null, Contacts));
        }
    }
}
