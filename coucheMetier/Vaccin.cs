using System;

namespace RefugeAnimauxWPF.coucheMetier
{
    public class Vaccin
    {
        private string? nom;

        public Vaccin() { }

        public Vaccin(Vaccin vaccin)
        {
            this.nom = vaccin.nom;
        }

        public Vaccin(string? nom)
        {
            this.nom = nom;
        }

        public string? Nom { get => nom; set => nom = value; }

        public override string ToString()
        {
            return nom ?? "";
        }
    }
}
