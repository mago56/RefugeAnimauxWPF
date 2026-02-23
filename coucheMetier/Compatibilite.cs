using System;

namespace RefugeAnimauxWPF.coucheMetier
{
    public class Compatibilite
    {
        private string? type;
        private bool valeur;
        private string? description;

        public Compatibilite() { }

        public Compatibilite(string? type, bool valeur, string? description)
        {
            this.type = type;
            this.valeur = valeur;
            this.description = description;
        }

        public string? Type { get => type; set => type = value; }
        public bool Valeur { get => valeur; set => valeur = value; }
        public string? Description { get => description; set => description = value; }

        public override string ToString()
        {
            return $"{type}: {(valeur ? "Oui" : "Non")}";
        }
    }
}
