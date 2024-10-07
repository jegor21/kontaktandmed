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
                _isNewContact = true;  // Это новый контакт
            }
            else
            {
                _contact = contact;
                _isNewContact = false; // Мы редактируем существующий контакт
            }

            BindingContext = _contact;
        }

        // Сохранение контакта
        private async void OnSaveClicked(object sender, EventArgs e)
        {
            if (_isNewContact)
            {
                // Добавляем новый контакт в коллекцию
                _contacts.Add(_contact);
            }
            else
            {
                // Обновляем данные в Picker, уведомляя об изменении имени контакта
                var index = _contacts.IndexOf(_contact);
                if (index >= 0)
                {
                    _contacts[index] = _contact;
                }
            }

            // Возвращаемся на предыдущую страницу
            await Navigation.PopAsync();
        }


        // Добавление фотографии контакта
        private async void OnAddPhotoClicked(object sender, EventArgs e)
        {
            try
            {
                // Логика для выбора фото (используй MediaPicker или другой механизм)
                var result = await MediaPicker.PickPhotoAsync();

                if (result != null)
                {
                    _contact.Photo = result.FullPath;
                    contactPhoto.Source = _contact.Photo; // Обновляем изображение
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", "Не удалось выбрать фото: " + ex.Message, "OK");
            }
        }
    }
}
