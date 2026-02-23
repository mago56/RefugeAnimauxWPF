using System;
using System.Collections.Generic;
using System.ComponentModel;
using RefugeAnimauxWPF.coucheAccesDB;
using RefugeAnimauxWPF.coucheMetier;

namespace RefugeAnimauxWPF.coucheModeleVue
{
    public class AjouterAdoptionVueModele : INotifyPropertyChanged
    {
        private AccesBD monAccesBD;
        private Animal2? animalSelectionne;
        private Contact? contactSelectionne;
        private DateTime dateDemande;
        private string? statut;

        public List<Animal2> ListeAnimaux { get; set; }
        public List<Contact> ListeContacts { get; set; }

        public AjouterAdoptionVueModele()
        {
            try
            {
                monAccesBD = new AccesBD();
                ListeAnimaux = monAccesBD.ListeAnimaux();
                ListeContacts = monAccesBD.ListeContacts();
                dateDemande = DateTime.Now;
                statut = "demande";
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

        public DateTime DateDemande
        {
            get => dateDemande;
            set { dateDemande = value; OnPropertyChanged("DateDemande"); }
        }

        public string? Statut
        {
            get => statut;
            set { statut = value; OnPropertyChanged("Statut"); }
        }

        public int Ajouter()
        {
            Adoption adoption = new Adoption(statut, dateDemande, animalSelectionne, contactSelectionne);
            int retour = monAccesBD.AjouterAdoption(adoption);
            if (retour != 0)
            {
                AnimalSelectionne = null;
                ContactSelectionne = null;
                Statut = "demande";
                DateDemande = DateTime.Now;
            }
            return retour;
        }

        public bool Valide()
        {
            return animalSelectionne != null && contactSelectionne != null && 
                   !string.IsNullOrEmpty(statut) && dateDemande != DateTime.MinValue;
        }
    }
}
