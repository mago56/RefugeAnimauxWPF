using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using RefugeAnimauxWPF.coucheAccesDB;
using RefugeAnimauxWPF.coucheMetier;

namespace RefugeAnimauxWPF.coucheModeleVue
{
    public class ListerAnimauxVueModele : INotifyPropertyChanged
    {
        private AccesBD monAccesBD;
        public ObservableCollection<Animal2> collectionAnimaux { get; set; }

        public ListerAnimauxVueModele()
        {
            try
            {
                this.monAccesBD = new AccesBD();
            }
            catch (ExceptionAccesBD e)
            {
                throw new InvalidOperationException("Erreur BD : " + e.Message, e);
            }

            collectionAnimaux = new ObservableCollection<Animal2>();
            ChargerListe();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ChargerListe()
        {
            List<Animal2> listAnimaux = monAccesBD.ListeAnimaux();
            collectionAnimaux.Clear();
            foreach (Animal2 animal in listAnimaux)
            {
                collectionAnimaux.Add(new Animal2(animal));
            }
            OnPropertyChanged("collectionAnimaux");
        }
    }
}
