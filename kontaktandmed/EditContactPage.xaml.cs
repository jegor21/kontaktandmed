using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace kontaktandmed
{
    public partial class EditContactPage : ContentPage
    {
        private Contact _contact;
        private ObservableCollection<Contact> _contacts;
        private bool _isNewContact;

        public EditContactPage(Contact contact, ObservableCollection<Contact> contacts)
        {
            InitializeComponent();

            _contacts = contacts;

            if (contact == null)
            {
                _contact = new Contact();
                _isNewContact = true;  
            }
            else
            {
                _contact = contact;
                _isNewContact = false; 
            }

            BindingContext = _contact;
        }

        
        private async void OnSaveClicked(object sender, EventArgs e)
        {
            if (_isNewContact)
            {
                
                _contacts.Add(_contact);
            }
            else
            {
                
                var index = _contacts.IndexOf(_contact);
                if (index >= 0)
                {
                    _contacts[index] = _contact;
                }
            }

            
            await Navigation.PopAsync();
        }


       
        private async void OnAddPhotoClicked(object sender, EventArgs e)
        {
            try
            {
                
                var result = await MediaPicker.PickPhotoAsync();

                if (result != null)
                {
                    _contact.Photo = result.FullPath;
                    contactPhoto.Source = _contact.Photo; 
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Viga", "Pole foto: " + ex.Message, "OK");
            }
        }
    }
}
