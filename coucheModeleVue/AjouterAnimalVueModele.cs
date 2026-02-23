using System;
using System.ComponentModel;
using RefugeAnimauxWPF.coucheAccesDB;
using RefugeAnimauxWPF.coucheMetier;

namespace RefugeAnimauxWPF.coucheModeleVue
{
    public class AjouterAnimalVueModele : INotifyPropertyChanged
    {
        private AccesBD monAccesBD;
        private Animal2 animal;

        public AjouterAnimalVueModele()
        {
            animal = new Animal2();
            animal.Date_naissance = DateTime.Now;

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
            get => animal.Nom;
            set { animal.Nom = value; OnPropertyChanged("Nom"); }
        }

        public string? Type
        {
            get => animal.Type;
            set { animal.Type = value; OnPropertyChanged("Type"); }
        }

        public DateTime DateNaissance
        {
            get => animal.Date_naissance;
            set { animal.Date_naissance = value; OnPropertyChanged("DateNaissance"); }
        }

        public string? Couleurs
        {
            get => animal.Couleurs;
            set { animal.Couleurs = value; OnPropertyChanged("Couleurs"); }
        }

        public bool Sterilise
        {
            get => animal.Sterilise;
            set { animal.Sterilise = value; OnPropertyChanged("Sterilise"); }
        }

        public DateTime? DateSterilisation
        {
            get => animal.Date_sterilisation == DateTime.MinValue ? null : animal.Date_sterilisation;
            set { animal.Date_sterilisation = value ?? DateTime.MinValue; OnPropertyChanged("DateSterilisation"); }
        }

        public string? Particularite
        {
            get => animal.Particularite;
            set { animal.Particularite = value; OnPropertyChanged("Particularite"); }
        }

        public string? Description
        {
            get => animal.Description;
            set { animal.Description = value; OnPropertyChanged("Description"); }
        }

        public int Ajouter()
        {
            int retour = monAccesBD.AjouterAnimal(animal);
            if (retour != 0)
            {
                Nom = "";
                Type = "";
                Couleurs = "";
                Particularite = "";
                Description = "";
                Sterilise = false;
                DateNaissance = DateTime.Now;
                DateSterilisation = null;
            }
            return retour;
        }

        public bool Valide()
        {
            return !string.IsNullOrEmpty(Nom) && Nom.Length >= 2 &&
                   !string.IsNullOrEmpty(Type) && Type.Length >= 2 &&
                   DateNaissance != DateTime.MinValue;
        }
    }
}
