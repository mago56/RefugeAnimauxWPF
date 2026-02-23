using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using RefugeAnimauxWPF.coucheAccesDB;
using RefugeAnimauxWPF.coucheMetier;

namespace RefugeAnimauxWPF.coucheModeleVue
{
    public class ListerAdoptionsVueModele : INotifyPropertyChanged
    {
        private AccesBD monAccesBD;
        public ObservableCollection<Adoption> collectionAdoptions { get; set; }

        public ListerAdoptionsVueModele()
        {
            try
            {
                this.monAccesBD = new AccesBD();
            }
            catch (ExceptionAccesBD e)
            {
                throw new InvalidOperationException("Erreur BD : " + e.Message, e);
            }

            collectionAdoptions = new ObservableCollection<Adoption>();
            ChargerListe();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ChargerListe()
        {
            List<Adoption> listAdoptions = monAccesBD.ListeAdoptions();
            collectionAdoptions.Clear();
            foreach (Adoption adoption in listAdoptions)
            {
                collectionAdoptions.Add(new Adoption(adoption));
            }
            OnPropertyChanged("collectionAdoptions");
        }
    }
}
