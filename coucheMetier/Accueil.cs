using System;

namespace RefugeAnimauxWPF.coucheMetier
{
    public class Accueil
    {
        private DateTime date_debut;
        private DateTime date_fin;
        private Animal2? animal_accueilli;
        private Contact? famille_accueil;

        public Accueil() { }

        public Accueil(Accueil accueil)
        {
            this.date_debut = accueil.date_debut;
            this.date_fin = accueil.date_fin;
            this.animal_accueilli = accueil.animal_accueilli != null ? new Animal2(accueil.animal_accueilli) : null;
            this.famille_accueil = accueil.famille_accueil != null ? new Contact(accueil.famille_accueil) : null;
        }

        public Accueil(DateTime date_debut, DateTime date_fin, Animal2? animal_accueilli, Contact? famille_accueil)
        {
            this.date_debut = date_debut;
            this.date_fin = date_fin;
            this.animal_accueilli = animal_accueilli;
            this.famille_accueil = famille_accueil;
        }

        public DateTime Date_debut { get => date_debut; set => date_debut = value; }
        public DateTime Date_fin { get => date_fin; set => date_fin = value; }
        public Animal2? Animal_accueilli { get => animal_accueilli; set => animal_accueilli = value; }
        public Contact? Famille_accueil { get => famille_accueil; set => famille_accueil = value; }

        public override string ToString()
        {
            return $"Accueil: {animal_accueilli?.Nom} chez {famille_accueil?.Nom}";
        }
    }
}
