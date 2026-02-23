using System;
using System.Collections.Generic;
using System.ComponentModel;
using RefugeAnimauxWPF.coucheAccesDB;
using RefugeAnimauxWPF.coucheMetier;

namespace RefugeAnimauxWPF.coucheModeleVue
{
    public class AjouterSortieVueModele : INotifyPropertyChanged
    {
        private AccesBD monAccesBD;
        private Animal2? animalSelectionne;
        private Contact? contactSelectionne;
        private DateTime dateSortie;
        private string? raison;

        public List<Animal2> ListeAnimaux { get; set; }
        public List<Contact> ListeContacts { get; set; }

        public AjouterSortieVueModele()
        {
            try
            {
                monAccesBD = new AccesBD();
                ListeAnimaux = monAccesBD.ListeAnimaux();
                ListeContacts = monAccesBD.ListeContacts();
                dateSortie = DateTime.Now;
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

        public Animal2? AnimalSelectionne
        {
            get => animalSelectionne;
            set { animalSelectionne = value; OnPropertyChanged("AnimalSelectionne"); }
        }

        public Contact? ContactSelectionne
        {
            get => contactSelectionne;
            set { contactSelectionne = value; OnPropertyChanged("ContactSelectionne"); }
        }

        public DateTime DateSortie
        {
            get => dateSortie;
            set { dateSortie = value; OnPropertyChanged("DateSortie"); }
        }

        public string? Raison
        {
            get => raison;
            set { raison = value; OnPropertyChanged("Raison"); }
        }

        public int Ajouter()
        {
            Sortie sortie = new Sortie(raison, dateSortie, animalSelectionne, contactSelectionne);
            int retour = monAccesBD.AjouterSortie(sortie);
            if (retour != 0)
            {
                AnimalSelectionne = null;
                ContactSelectionne = null;
                Raison = null;
                DateSortie = DateTime.Now;
            }
            return retour;
        }

        public bool Valide()
        {
            return animalSelectionne != null && contactSelectionne != null && 
                   !string.IsNullOrEmpty(raison) && dateSortie != DateTime.MinValue;
        }
    }
}
