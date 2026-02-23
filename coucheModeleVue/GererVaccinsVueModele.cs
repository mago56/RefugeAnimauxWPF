using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using RefugeAnimauxWPF.coucheAccesDB;
using RefugeAnimauxWPF.coucheMetier;

namespace RefugeAnimauxWPF.coucheModeleVue
{
    public class GererVaccinsVueModele : INotifyPropertyChanged
    {
        private AccesBD monAccesBD;
        private string? nouveauVaccin;

        public ObservableCollection<Vaccin> collectionVaccins { get; set; }

        public GererVaccinsVueModele()
        {
            try
            {
                monAccesBD = new AccesBD();
                collectionVaccins = new ObservableCollection<Vaccin>();
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

        public string? NouveauVaccin
        {
            get => nouveauVaccin;
            set { nouveauVaccin = value; OnPropertyChanged("NouveauVaccin"); }
        }

        public void ChargerListe()
        {
            List<Vaccin> listVaccins = monAccesBD.ListeVaccins();
            collectionVaccins.Clear();
            foreach (Vaccin vaccin in listVaccins)
            {
                collectionVaccins.Add(new Vaccin(vaccin));
            }
            OnPropertyChanged("collectionVaccins");
        }

        public int AjouterVaccin()
        {
            int retour = monAccesBD.AjouterVaccin(nouveauVaccin ?? "");
            if (retour != 0)
            {
                NouveauVaccin = "";
            }
            return retour;
        }
    }
}
