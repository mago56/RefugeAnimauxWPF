using System;
using System.Collections.Generic;
using System.ComponentModel;
using RefugeAnimauxWPF.coucheAccesDB;
using RefugeAnimauxWPF.coucheMetier;

namespace RefugeAnimauxWPF.coucheModeleVue
{
    public class ModifierContactVueModele : INotifyPropertyChanged
    {
        private AccesBD monAccesBD;
        private Contact? contactSelectionne;

        public List<Contact> ListeContacts { get; set; }

        public ModifierContactVueModele()
        {
            try
            {
                monAccesBD = new AccesBD();
                ListeContacts = monAccesBD.ListeContacts();
            }
            catch (ExceptionAccesBD e)
            {
                throw new InvalidOperationException("Erreur BD : " + e.Message, e);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Contact? ContactSelectionne
        {
            get => contactSelectionne;
            set
            {
                contactSelectionne = value;
                OnPropertyChanged("ContactSelectionne");
                // Notifier tous les champs pour rafraîchir le formulaire
                OnPropertyChanged("Nom");
                OnPropertyChanged("Prenom");
                OnPropertyChanged("RegistreNational");
                OnPropertyChanged("Gsm");
                OnPropertyChanged("Telephone");
                OnPropertyChanged("Email");
                OnPropertyChanged("Rue");
                OnPropertyChanged("Cp");
                OnPropertyChanged("Localite");
            }
        }

        public string? Nom
        {
            get => contactSelectionne?.Nom;
            set { if (contactSelectionne != null) { contactSelectionne.Nom = value; OnPropertyChanged("Nom"); } }
        }

        public string? Prenom
        {
            get => contactSelectionne?.Prenom;
            set { if (contactSelectionne != null) { contactSelectionne.Prenom = value; OnPropertyChanged("Prenom"); } }
        }

        public string? RegistreNational
        {
            get => contactSelectionne?.Registre_national;
            set { if (contactSelectionne != null) { contactSelectionne.Registre_national = value; OnPropertyChanged("RegistreNational"); } }
        }

        public string? Gsm
        {
            get => contactSelectionne?.Gsm;
            set { if (contactSelectionne != null) { contactSelectionne.Gsm = value; OnPropertyChanged("Gsm"); } }
        }

        public string? Telephone
        {
            get => contactSelectionne?.Telephone;
            set { if (contactSelectionne != null) { contactSelectionne.Telephone = value; OnPropertyChanged("Telephone"); } }
        }

        public string? Email
        {
            get => contactSelectionne?.Email;
            set { if (contactSelectionne != null) { contactSelectionne.Email = value; OnPropertyChanged("Email"); } }
        }

        public string? Rue
        {
            get => contactSelectionne?.Adresse_contact?.Rue;
            set
            {
                if (contactSelectionne != null)
                {
                    if (contactSelectionne.Adresse_contact == null) contactSelectionne.Adresse_contact = new Adresse();
                    contactSelectionne.Adresse_contact.Rue = value;
                    OnPropertyChanged("Rue");
                }
            }
        }

        public string Cp
        {
            get => contactSelectionne?.Adresse_contact?.Cp.ToString() ?? "";
            set
            {
                if (contactSelectionne != null)
                {
                    if (contactSelectionne.Adresse_contact == null) contactSelectionne.Adresse_contact = new Adresse();
                    if (int.TryParse(value, out int cp)) contactSelectionne.Adresse_contact.Cp = cp;
                    OnPropertyChanged("Cp");
                }
            }
        }

        public string? Localite
        {
            get => contactSelectionne?.Adresse_contact?.Localite;
            set
            {
                if (contactSelectionne != null)
                {
                    if (contactSelectionne.Adresse_contact == null) contactSelectionne.Adresse_contact = new Adresse();
                    contactSelectionne.Adresse_contact.Localite = value;
                    OnPropertyChanged("Localite");
                }
            }
        }

        public int Modifier()
        {
            if (contactSelectionne == null) return 0;
            int retour = monAccesBD.ModifierContact(contactSelectionne);
            if (retour != 0)
            {
                ListeContacts = monAccesBD.ListeContacts();
                OnPropertyChanged("ListeContacts");
            }
            return retour;
        }

        public bool Valide()
        {
            return contactSelectionne != null &&
                   !string.IsNullOrEmpty(Nom) && Nom.Length >= 2 &&
                   !string.IsNullOrEmpty(Prenom) && Prenom.Length >= 2 &&
                   !string.IsNullOrEmpty(Rue) && Rue.Length >= 2 &&
                   !string.IsNullOrEmpty(Localite) && Localite.Length >= 2 &&
                   contactSelectionne.Adresse_contact?.Cp > 0;
        }
    }
}