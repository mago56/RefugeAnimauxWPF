using System;
using System.Collections.Generic;

namespace RefugeAnimauxWPF.coucheMetier
{
    public class Contact
    {
        private int identifiant;
        private string? nom;
        private string? prenom;
        private Adresse? adresse_contact;
        private string? registre_national;
        private string? gsm;
        private string? telephone;
        private string? email;
        private List<Role> roles;

        public Contact()
        {
            roles = new List<Role>();
            adresse_contact = new Adresse();
        }

        public Contact(Contact contact)
        {
            this.identifiant = contact.identifiant;
            this.nom = contact.nom;
            this.prenom = contact.prenom;
            this.adresse_contact = contact.adresse_contact != null ? new Adresse(contact.adresse_contact) : new Adresse();
            this.registre_national = contact.registre_national;
            this.gsm = contact.gsm;
            this.telephone = contact.telephone;
            this.email = contact.email;
            this.roles = new List<Role>(contact.roles ?? new List<Role>());
        }

        public Contact(int identifiant, string? nom, string? prenom, Adresse? adresse_contact,
                      string? registre_national, string? gsm, string? telephone, string? email)
        {
            this.identifiant = identifiant;
            this.nom = nom;
            this.prenom = prenom;
            this.adresse_contact = adresse_contact ?? new Adresse();
            this.registre_national = registre_national;
            this.gsm = gsm;
            this.telephone = telephone;
            this.email = email;
            this.roles = new List<Role>();
        }

        public int Identifiant { get => identifiant; set => identifiant = value; }
        public string? Nom { get => nom; set => nom = value; }
        public string? Prenom { get => prenom; set => prenom = value; }
        public Adresse? Adresse_contact { get => adresse_contact; set => adresse_contact = value; }
        public string? Registre_national { get => registre_national; set => registre_national = value; }
        public string? Gsm { get => gsm; set => gsm = value; }
        public string? Telephone { get => telephone; set => telephone = value; }
        public string? Email { get => email; set => email = value; }
        public List<Role> Roles { get => roles; set => roles = value; }

        public override string ToString()
        {
            return $"{nom} {prenom}";
        }
    }
}
