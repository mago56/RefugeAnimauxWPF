using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using RefugeAnimauxWPF.coucheAccesDB;
using RefugeAnimauxWPF.coucheMetier;

namespace RefugeAnimauxWPF.coucheModeleVue
{
    public class ListerSortiesVueModele : INotifyPropertyChanged
    {
        private AccesBD monAccesBD;
        public ObservableCollection<Sortie> collectionSorties { get; set; }

        public ListerSortiesVueModele()
        {
            try
            {
                this.monAccesBD = new AccesBD();
            }
            catch (ExceptionAccesBD e)
            {
                throw new InvalidOperationException("Erreur BD : " + e.Message, e);
            }

            collectionSorties = new ObservableCollection<Sortie>();
            ChargerListe();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ChargerListe()
        {
            List<Sortie> listSorties = monAccesBD.ListeSorties();
            collectionSorties.Clear();
            foreach (Sortie sortie in listSorties)
            {
                collectionSorties.Add(new Sortie(sortie));
            }
            OnPropertyChanged("collectionSorties");
        }
    }
}
