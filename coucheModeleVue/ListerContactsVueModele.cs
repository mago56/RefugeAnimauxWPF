using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using RefugeAnimauxWPF.coucheAccesDB;
using RefugeAnimauxWPF.coucheMetier;

namespace RefugeAnimauxWPF.coucheModeleVue
{
    public class ListerContactsVueModele : INotifyPropertyChanged
    {
        private AccesBD monAccesBD;
        public ObservableCollection<Contact> collectionContacts { get; set; }

        public ListerContactsVueModele()
        {
            try
            {
                this.monAccesBD = new AccesBD();
            }
            catch (ExceptionAccesBD e)
            {
                throw new InvalidOperationException("Erreur BD : " + e.Message, e);
            }

            collectionContacts = new ObservableCollection<Contact>();
            ChargerListe();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ChargerListe()
        {
            List<Contact> listContacts = monAccesBD.ListeContacts();
            collectionContacts.Clear();
            foreach (Contact contact in listContacts)
            {
                collectionContacts.Add(new Contact(contact));
            }
            OnPropertyChanged("collectionContacts");
        }
    }
}
