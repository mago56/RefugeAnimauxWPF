using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using RefugeAnimauxWPF.coucheAccesDB;
using RefugeAnimauxWPF.coucheMetier;

namespace RefugeAnimauxWPF.coucheModeleVue
{
    public class ListerEntreesVueModele : INotifyPropertyChanged
    {
        private AccesBD monAccesBD;
        public ObservableCollection<Entree> collectionEntrees { get; set; }

        public ListerEntreesVueModele()
        {
            try
            {
                this.monAccesBD = new AccesBD();
            }
            catch (ExceptionAccesBD e)
            {
                throw new InvalidOperationException("Erreur BD : " + e.Message, e);
            }

            collectionEntrees = new ObservableCollection<Entree>();
            ChargerListe();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ChargerListe()
        {
            List<Entree> listEntrees = monAccesBD.ListeEntrees();
            collectionEntrees.Clear();
            foreach (Entree entree in listEntrees)
            {
                collectionEntrees.Add(new Entree(entree));
            }
            OnPropertyChanged("collectionEntrees");
        }
    }
}
