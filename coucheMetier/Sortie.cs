using System;

namespace RefugeAnimauxWPF.coucheMetier
{
    public class Sortie
    {
        private string? raison;
        private DateTime date_sortie;
        private Animal2? animal;
        private Contact? contact;

        public Sortie() { }

        public Sortie(Sortie sortie)
        {
            this.raison = sortie.raison;
            this.date_sortie = sortie.date_sortie;
            this.animal = sortie.animal != null ? new Animal2(sortie.animal) : null;
            this.contact = sortie.contact != null ? new Contact(sortie.contact) : null;
        }

        public Sortie(string? raison, DateTime date_sortie, Animal2? animal, Contact? contact)
        {
            this.raison = raison;
            this.date_sortie = date_sortie;
            this.animal = animal;
            this.contact = contact;
        }

        public string? Raison { get => raison; set => raison = value; }
        public DateTime Date_sortie { get => date_sortie; set => date_sortie = value; }
        public Animal2? Animal { get => animal; set => animal = value; }
        public Contact? Contact { get => contact; set => contact = value; }

        public override string ToString()
        {
            return $"Sortie: {animal?.Nom} - {date_sortie:dd/MM/yyyy}";
        }
    }
}
