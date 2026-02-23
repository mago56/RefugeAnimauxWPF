using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using RefugeAnimauxWPF.coucheAccesDB;
using RefugeAnimauxWPF.coucheMetier;

namespace RefugeAnimauxWPF.coucheModeleVue
{
    public class GererAccueilVueModele : INotifyPropertyChanged
    {
        private AccesBD monAccesBD;
        private Animal2? animalSelectionne;
        private Contact? contactSelectionne;
        private DateTime dateDebut;
        private DateTime? dateFin;

        public ObservableCollection<Accueil> collectionAccueils { get; set; }
        public List<Animal2> ListeAnimaux { get; set; }
        public List<Contact> ListeContacts { get; set; }

        public GererAccueilVueModele()
        {
            try
            {
                monAccesBD = new AccesBD();
                ListeAnimaux = monAccesBD.ListeAnimaux();
                ListeContacts = monAccesBD.ListeContacts();
                collectionAccueils = new ObservableCollection<Accueil>();
                dateDebut = DateTime.Now;
                ChargerListe();
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

        public DateTime DateDebut
        {
            get => dateDebut;
            set { dateDebut = value; OnPropertyChanged("DateDebut"); }
        }

        public DateTime? DateFin
        {
            get => dateFin;
            set { dateFin = value; OnPropertyChanged("DateFin"); }
        }

        public void ChargerListe()
        {
            List<Accueil> listAccueils = monAccesBD.ListeAccueils();
            collectionAccueils.Clear();
            foreach (Accueil accueil in listAccueils)
            {
                collectionAccueils.Add(new Accueil(accueil));
            }
            OnPropertyChanged("collectionAccueils");
        }

        public int Ajouter()
        {
            Accueil accueil = new Accueil(dateDebut, dateFin ?? DateTime.MinValue, animalSelectionne, contactSelectionne);
            int retour = monAccesBD.AjouterAccueil(accueil);
            if (retour != 0)
            {
                AnimalSelectionne = null;
                ContactSelectionne = null;
                DateDebut = DateTime.Now;
                DateFin = null;
            }
            return retour;
        }

        public bool Valide()
        {
            return animalSelectionne != null && contactSelectionne != null && dateDebut != DateTime.MinValue;
        }
    }
}
