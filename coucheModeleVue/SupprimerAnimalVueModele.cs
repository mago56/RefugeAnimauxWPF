using System;
using System.Collections.Generic;
using System.ComponentModel;
using RefugeAnimauxWPF.coucheAccesDB;
using RefugeAnimauxWPF.coucheMetier;

namespace RefugeAnimauxWPF.coucheModeleVue
{
	public class SupprimerAnimalVueModele : INotifyPropertyChanged
	{
		private AccesBD monAccesBD;
		private Animal2? animalSelectionne;

		public List<Animal2> ListeAnimaux { get; set; }

		public SupprimerAnimalVueModele()
		{
			try
			{
				monAccesBD = new AccesBD();
				ListeAnimaux = monAccesBD.ListeAnimaux();
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

		public int Supprimer()
		{
			if (animalSelectionne == null) return 0;
			int retour = monAccesBD.SupprimerAnimal(animalSelectionne.Identifiant);
			if (retour != 0)
			{
				ListeAnimaux = monAccesBD.ListeAnimaux();
				OnPropertyChanged("ListeAnimaux");
				AnimalSelectionne = null;
			}
			return retour;
		}

		public bool Valide()
		{
			return animalSelectionne != null;
		}
	}
}