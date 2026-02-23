using System;

namespace RefugeAnimauxWPF.coucheMetier
{
    public class Entree
    {
        private string? raison;
        private DateTime date_entree;
        private Animal2? animal;
        private Contact? contact;

        public Entree() { }

        public Entree(Entree entree)
        {
            this.raison = entree.raison;
            this.date_entree = entree.date_entree;
            this.animal = entree.animal != null ? new Animal2(entree.animal) : null;
            this.contact = entree.contact != null ? new Contact(entree.contact) : null;
        }

        public Entree(string? raison, DateTime date_entree, Animal2? animal, Contact? contact)
        {
            this.raison = raison;
            this.date_entree = date_entree;
            this.animal = animal;
            this.contact = contact;
        }

        public string? Raison { get => raison; set => raison = value; }
        public DateTime Date_entree { get => date_entree; set => date_entree = value; }
        public Animal2? Animal { get => animal; set => animal = value; }
        public Contact? Contact { get => contact; set => contact = value; }

        public override string ToString()
        {
            return $"Entrée: {animal?.Nom} - {date_entree:dd/MM/yyyy}";
        }
    }
}
