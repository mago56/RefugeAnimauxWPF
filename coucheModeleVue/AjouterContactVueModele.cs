using System;
using System.ComponentModel;
using RefugeAnimauxWPF.coucheAccesDB;
using RefugeAnimauxWPF.coucheMetier;

namespace RefugeAnimauxWPF.coucheModeleVue
{
    public class AjouterContactVueModele : INotifyPropertyChanged
    {
        private AccesBD monAccesBD;
        private Contact contact;

        public AjouterContactVueModele()
        {
            contact = new Contact();
            contact.Adresse_contact = new Adresse();

            try
            {
                this.monAccesBD = new AccesBD();
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

        public string? Nom
        {
            get => contact.Nom;
            set { contact.Nom = value; OnPropertyChanged("Nom"); }
        }

        public string? Prenom
        {
            get => contact.Prenom;
            set { contact.Prenom = value; OnPropertyChanged("Prenom"); }
        }

        public string? RegistreNational
        {
            get => contact.Registre_national;
            set { contact.Registre_national = value; OnPropertyChanged("RegistreNational"); }
        }

        public string? Gsm
        {
            get => contact.Gsm;
            set { contact.Gsm = value; OnPropertyChanged("Gsm"); }
        }

        public string? Telephone
        {
            get => contact.Telephone;
            set { contact.Telephone = value; OnPropertyChanged("Telephone"); }
        }

        public string? Email
        {
            get => contact.Email;
            set { contact.Email = value; OnPropertyChanged("Email"); }
        }

        public string? Rue
        {
            get => contact.Adresse_contact?.Rue;
            set
            {
                if (contact.Adresse_contact == null) contact.Adresse_contact = new Adresse();
                contact.Adresse_contact.Rue = value;
                OnPropertyChanged("Rue");
            }
        }

        public string Cp
        {
            get => contact.Adresse_contact?.Cp.ToString() ?? "";
            set
            {
                if (contact.Adresse_contact == null) contact.Adresse_contact = new Adresse();
                if (int.TryParse(value, out int cp)) contact.Adresse_contact.Cp = cp;
                OnPropertyChanged("Cp");
            }
        }

        public string? Localite
        {
            get => contact.Adresse_contact?.Localite;
            set
            {
                if (contact.Adresse_contact == null) contact.Adresse_contact = new Adresse();
                contact.Adresse_contact.Localite = value;
                OnPropertyChanged("Localite");
            }
        }

        public int Ajouter()
        {
            int retour = monAccesBD.AjouterContact(contact);
            if (retour != 0)
            {
                Nom = "";
                Prenom = "";
                RegistreNational = "";
                Gsm = "";
                Telephone = "";
                Email = "";
                Rue = "";
                Cp = "";
                Localite = "";
            }
            return retour;
        }

        public bool Valide()
        {
            return !string.IsNullOrEmpty(Nom) && Nom.Length >= 2 &&
                   !string.IsNullOrEmpty(Prenom) && Prenom.Length >= 2 &&
                   !string.IsNullOrEmpty(Rue) && Rue.Length >= 2 &&
                   !string.IsNullOrEmpty(Localite) && Localite.Length >= 2 &&
                   contact.Adresse_contact?.Cp > 0;
        }
    }
}
