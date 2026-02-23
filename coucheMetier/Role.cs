using System;

namespace RefugeAnimauxWPF.coucheMetier
{
    public class Role
    {
        private int id_role;
        private string? nom_role;

        public Role() { }

        public Role(Role role)
        {
            this.id_role = role.id_role;
            this.nom_role = role.nom_role;
        }

        public Role(int id_role, string? nom_role)
        {
            this.id_role = id_role;
            this.nom_role = nom_role;
        }

        public int Id_role { get => id_role; set => id_role = value; }
        public string? Nom_role { get => nom_role; set => nom_role = value; }

        public override string ToString()
        {
            return nom_role ?? "";
        }
    }
}
