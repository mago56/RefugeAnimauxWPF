using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using RefugeAnimauxWPF.coucheAccesDB;
using RefugeAnimauxWPF.coucheMetier;

namespace RefugeAnimauxWPF.coucheModeleVue
{
    public class GererVaccinationsVueModele : INotifyPropertyChanged
    {
        private AccesBD monAccesBD;
        private Animal2? animalSelectionne;
        private Vaccin? vaccinSelectionne;
        private DateTime dateVaccination;

        public ObservableCollection<Vaccination> collectionVaccinations { get; set; }
        public List<Animal2> ListeAnimaux { get; set; }
        public List<Vaccin> ListeVaccins { get; set; }

        public GererVaccinationsVueModele()
        {
            try
            {
                monAccesBD = new AccesBD();
                ListeAnimaux = monAccesBD.ListeAnimaux();
                ListeVaccins = monAccesBD.ListeVaccins();
                collectionVaccinations = new ObservableCollection<Vaccination>();
                dateVaccination = DateTime.Now;
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

        public Vaccin? VaccinSelectionne
        {
            get => vaccinSelectionne;
            set { vaccinSelectionne = value; OnPropertyChanged("VaccinSelectionne"); }
        }

        public DateTime DateVaccination
        {
            get => dateVaccination;
            set { dateVaccination = value; OnPropertyChanged("DateVaccination"); }
        }

        public void ChargerListe()
        {
            List<Vaccination> listVaccinations = monAccesBD.ListeVaccinations();
            collectionVaccinations.Clear();
            foreach (Vaccination vaccination in listVaccinations)
            {
                collectionVaccinations.Add(new Vaccination(vaccination));
            }
            OnPropertyChanged("collectionVaccinations");
        }

        public int Ajouter()
        {
            int retour = monAccesBD.AjouterVaccination(animalSelectionne?.Identifiant ?? 0, 
                                                        vaccinSelectionne?.Nom ?? "", 
                                                        dateVaccination);
            if (retour != 0)
            {
                AnimalSelectionne = null;
                VaccinSelectionne = null;
                DateVaccination = DateTime.Now;
            }
            return retour;
        }

        public bool Valide()
        {
            return animalSelectionne != null && vaccinSelectionne != null && dateVaccination != DateTime.MinValue;
        }
    }
}
