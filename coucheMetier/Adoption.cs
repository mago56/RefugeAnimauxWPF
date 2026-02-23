using System;

namespace RefugeAnimauxWPF.coucheMetier
{
    public class Adoption
    {
        private string? statut;
        private DateTime date_demande;
        private Animal2? animal_adopte;
        private Contact? adoptant;

        public Adoption() { }

        public Adoption(Adoption adoption)
        {
            this.statut = adoption.statut;
            this.date_demande = adoption.date_demande;
            this.animal_adopte = adoption.animal_adopte != null ? new Animal2(adoption.animal_adopte) : null;
            this.adoptant = adoption.adoptant != null ? new Contact(adoption.adoptant) : null;
        }

        public Adoption(string? statut, DateTime date_demande, Animal2? animal_adopte, Contact? adoptant)
        {
            this.statut = statut;
            this.date_demande = date_demande;
            this.animal_adopte = animal_adopte;
            this.adoptant = adoptant;
        }

        public string? Statut { get => statut; set => statut = value; }
        public DateTime Date_demande { get => date_demande; set => date_demande = value; }
        public Animal2? Animal_adopte { get => animal_adopte; set => animal_adopte = value; }
        public Contact? Adoptant { get => adoptant; set => adoptant = value; }

        public override string ToString()
        {
            return $"Adoption: {animal_adopte?.Nom} - {statut}";
        }
    }
}
