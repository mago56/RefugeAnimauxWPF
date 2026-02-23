using System;

namespace RefugeAnimauxWPF.coucheMetier
{
    public class Adresse
    {
        private string? rue;
        private int cp;
        private string? localite;

        public Adresse() { }

        public Adresse(Adresse adresse)
        {
            this.rue = adresse.rue;
            this.cp = adresse.cp;
            this.localite = adresse.localite;
        }

        public Adresse(string? rue, int cp, string? localite)
        {
            this.rue = rue;
            this.cp = cp;
            this.localite = localite;
        }

        public string? Rue { get => rue; set => rue = value; }
        public int Cp { get => cp; set => cp = value; }
        public string? Localite { get => localite; set => localite = value; }

        public override string ToString()
        {
            return $"{rue}, {cp} {localite}";
        }
    }
}
