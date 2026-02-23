using System;
using System.Collections.Generic;

namespace RefugeAnimauxWPF.coucheMetier
{
    public class Animal2
    {
        private int identifiant;
        private bool sterilise;
        private string? nom;
        private string? type;
        private string? particularite;
        private string? description;
        private DateTime date_deces;
        private DateTime date_sterilisation;
        private DateTime date_naissance;
        private string? couleurs;
        private List<Compatibilite> compatibilites;

        public Animal2()
        {
            compatibilites = new List<Compatibilite>();
        }

        public Animal2(Animal2 animal)
        {
            this.identifiant = animal.identifiant;
            this.sterilise = animal.sterilise;
            this.nom = animal.nom;
            this.type = animal.type;
            this.particularite = animal.particularite;
            this.description = animal.description;
            this.date_deces = animal.date_deces;
            this.date_sterilisation = animal.date_sterilisation;
            this.date_naissance = animal.date_naissance;
            this.couleurs = animal.couleurs;
            this.compatibilites = new List<Compatibilite>(animal.compatibilites ?? new List<Compatibilite>());
        }

        public Animal2(int identifiant, bool sterilise, string? nom, string? type,
                     string? particularite, string? description, DateTime date_deces,
                     DateTime date_sterilisation, DateTime date_naissance, string? couleurs)
        {
            this.identifiant = identifiant;
            this.sterilise = sterilise;
            this.nom = nom;
            this.type = type;
            this.particularite = particularite;
            this.description = description;
            this.date_deces = date_deces;
            this.date_sterilisation = date_sterilisation;
            this.date_naissance = date_naissance;
            this.couleurs = couleurs;
            this.compatibilites = new List<Compatibilite>();
        }

        public int Identifiant { get => identifiant; set => identifiant = value; }
        public bool Sterilise { get => sterilise; set => sterilise = value; }
        public string? Nom { get => nom; set => nom = value; }
        public string? Type { get => type; set => type = value; }
        public string? Particularite { get => particularite; set => particularite = value; }
        public string? Description { get => description; set => description = value; }
        public DateTime Date_deces { get => date_deces; set => date_deces = value; }
        public DateTime Date_sterilisation { get => date_sterilisation; set => date_sterilisation = value; }
        public DateTime Date_naissance { get => date_naissance; set => date_naissance = value; }
        public string? Couleurs { get => couleurs; set => couleurs = value; }
        public List<Compatibilite> Compatibilites { get => compatibilites; set => compatibilites = value; }

        public override string ToString()
        {
            return $"ID: {identifiant}, Nom: {nom}, Type: {type}";
        }
    }
}
