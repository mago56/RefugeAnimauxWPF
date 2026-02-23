using System;
using System.Collections.Generic;
using RefugeAnimauxWPF.coucheMetier;
using Npgsql;
using NpgsqlTypes;

namespace RefugeAnimauxWPF.coucheAccesDB
{
    public class AccesBD
    {
        private NpgsqlConnection sqlConn;

        public AccesBD()
        {
            try
            {
                sqlConn = new NpgsqlConnection("Host=localhost;Port=5432;Database=refuge_animaux;Username=postgres;Password=p@ssword");
                sqlConn.Open();
            }
            catch (Exception e)
            {
                throw new ExceptionAccesBD("Connexion à la BD", e.Message);
            }
        }

        // Méthode helper pour convertir DateOnly en DateTime (.NET 8+)
        private DateTime DateOnlyToDateTime(object value)
        {
            if (value is DateOnly dateOnly)
            {
                return dateOnly.ToDateTime(TimeOnly.MinValue);
            }
            return Convert.ToDateTime(value);
        }

        // ==================== ANIMAL ====================

        public int AjouterAnimal(Animal2 animal)
        {
            try
            {
                string requete = @"SELECT ajouter_animal(
                    @nom, @type, @sterilise, @date_naissance, @couleurs, 
                    @particularite, @description, @date_sterilisation, @date_deces)";

                NpgsqlCommand sqlCmd = new NpgsqlCommand(requete, sqlConn);

                sqlCmd.Parameters.Add(new NpgsqlParameter("nom", NpgsqlDbType.Text) { Value = animal.Nom ?? (object)DBNull.Value });
                sqlCmd.Parameters.Add(new NpgsqlParameter("type", NpgsqlDbType.Text) { Value = animal.Type ?? (object)DBNull.Value });
                sqlCmd.Parameters.Add(new NpgsqlParameter("sterilise", NpgsqlDbType.Boolean) { Value = animal.Sterilise });
                sqlCmd.Parameters.Add(new NpgsqlParameter("date_naissance", NpgsqlDbType.Date) { Value = animal.Date_naissance });
                sqlCmd.Parameters.Add(new NpgsqlParameter("couleurs", NpgsqlDbType.Text) { Value = animal.Couleurs ?? (object)DBNull.Value });
                sqlCmd.Parameters.Add(new NpgsqlParameter("particularite", NpgsqlDbType.Text) { Value = animal.Particularite ?? (object)DBNull.Value });
                sqlCmd.Parameters.Add(new NpgsqlParameter("description", NpgsqlDbType.Text) { Value = animal.Description ?? (object)DBNull.Value });
                sqlCmd.Parameters.Add(new NpgsqlParameter("date_sterilisation", NpgsqlDbType.Date) { Value = animal.Date_sterilisation == DateTime.MinValue ? DBNull.Value : animal.Date_sterilisation });
                sqlCmd.Parameters.Add(new NpgsqlParameter("date_deces", NpgsqlDbType.Date) { Value = animal.Date_deces == DateTime.MinValue ? DBNull.Value : animal.Date_deces });

                int identifiant = Convert.ToInt32(sqlCmd.ExecuteScalar());
                return identifiant;
            }
            catch (Exception ex)
            {
                throw new ExceptionAccesBD("Erreur lors de l'ajout de l'animal", ex.Message);
            }
        }

        public List<Animal2> ListeAnimaux()
        {
            try
            {
                string requete = "SELECT * FROM lister_animaux()";

                NpgsqlCommand sqlCmd = new NpgsqlCommand(requete, sqlConn);
                NpgsqlDataReader sqlReader = sqlCmd.ExecuteReader();

                List<Animal2> listeAnimaux = new List<Animal2>();

                while (sqlReader.Read())
                {
                    Animal2 animal = new Animal2(
                        Convert.ToInt32(sqlReader["identifiant"]),
                        Convert.ToBoolean(sqlReader["sterilise"]),
                        sqlReader.IsDBNull(sqlReader.GetOrdinal("nom")) ? null : Convert.ToString(sqlReader["nom"]),
                        sqlReader.IsDBNull(sqlReader.GetOrdinal("type")) ? null : Convert.ToString(sqlReader["type"]),
                        sqlReader.IsDBNull(sqlReader.GetOrdinal("particularite")) ? null : Convert.ToString(sqlReader["particularite"]),
                        sqlReader.IsDBNull(sqlReader.GetOrdinal("description")) ? null : Convert.ToString(sqlReader["description"]),
                        sqlReader.IsDBNull(sqlReader.GetOrdinal("date_deces")) ? DateTime.MinValue : DateOnlyToDateTime(sqlReader["date_deces"]),
                        sqlReader.IsDBNull(sqlReader.GetOrdinal("date_sterilisation")) ? DateTime.MinValue : DateOnlyToDateTime(sqlReader["date_sterilisation"]),
                        sqlReader.IsDBNull(sqlReader.GetOrdinal("date_naissance")) ? DateTime.MinValue : DateOnlyToDateTime(sqlReader["date_naissance"]),
                        sqlReader.IsDBNull(sqlReader.GetOrdinal("couleurs")) ? null : Convert.ToString(sqlReader["couleurs"])
                    );
                    listeAnimaux.Add(animal);
                }

                sqlReader.Close();
                return listeAnimaux;
            }
            catch (Exception ex)
            {
                throw new ExceptionAccesBD("Erreur lors de la récupération des animaux", ex.Message);
            }
        }

        public int SupprimerAnimal(int identifiant)
        {
            try
            {
                string requete = "SELECT supprimer_animal(@identifiant)";

                NpgsqlCommand sqlCmd = new NpgsqlCommand(requete, sqlConn);
                sqlCmd.Parameters.Add(new NpgsqlParameter("identifiant", NpgsqlDbType.Integer) { Value = identifiant });

                int nbLignes = Convert.ToInt32(sqlCmd.ExecuteScalar());
                return nbLignes;
            }
            catch (Exception ex)
            {
                throw new ExceptionAccesBD("Erreur lors de la suppression de l'animal", ex.Message);
            }
        }

        // ==================== CONTACT ====================

        public int AjouterContact(Contact contact)
        {
            try
            {
                string requete = @"SELECT ajouter_contact(
                    @nom, @prenom, @rue, @cp, @localite, 
                    @registre_national, @gsm, @telephone, @email)";

                NpgsqlCommand sqlCmd = new NpgsqlCommand(requete, sqlConn);
                sqlCmd.Parameters.Add(new NpgsqlParameter("nom", NpgsqlDbType.Text) { Value = contact.Nom ?? (object)DBNull.Value });
                sqlCmd.Parameters.Add(new NpgsqlParameter("prenom", NpgsqlDbType.Text) { Value = contact.Prenom ?? (object)DBNull.Value });
                sqlCmd.Parameters.Add(new NpgsqlParameter("rue", NpgsqlDbType.Text) { Value = contact.Adresse_contact?.Rue ?? (object)DBNull.Value });
                sqlCmd.Parameters.Add(new NpgsqlParameter("cp", NpgsqlDbType.Integer) { Value = contact.Adresse_contact?.Cp ?? 0 });
                sqlCmd.Parameters.Add(new NpgsqlParameter("localite", NpgsqlDbType.Text) { Value = contact.Adresse_contact?.Localite ?? (object)DBNull.Value });
                sqlCmd.Parameters.Add(new NpgsqlParameter("registre_national", NpgsqlDbType.Text) { Value = contact.Registre_national ?? (object)DBNull.Value });
                sqlCmd.Parameters.Add(new NpgsqlParameter("gsm", NpgsqlDbType.Text) { Value = contact.Gsm ?? (object)DBNull.Value });
                sqlCmd.Parameters.Add(new NpgsqlParameter("telephone", NpgsqlDbType.Text) { Value = contact.Telephone ?? (object)DBNull.Value });
                sqlCmd.Parameters.Add(new NpgsqlParameter("email", NpgsqlDbType.Text) { Value = contact.Email ?? (object)DBNull.Value });

                int identifiant = Convert.ToInt32(sqlCmd.ExecuteScalar());
                return identifiant;
            }
            catch (Exception ex)
            {
                throw new ExceptionAccesBD("Erreur lors de l'ajout du contact", ex.Message);
            }
        }

        public List<Contact> ListeContacts()
        {
            try
            {
                string requete = "SELECT * FROM lister_contacts()";

                NpgsqlCommand sqlCmd = new NpgsqlCommand(requete, sqlConn);
                NpgsqlDataReader sqlReader = sqlCmd.ExecuteReader();

                List<Contact> listeContacts = new List<Contact>();

                while (sqlReader.Read())
                {
                    Adresse adresse = new Adresse(
                        sqlReader.IsDBNull(sqlReader.GetOrdinal("rue")) ? null : Convert.ToString(sqlReader["rue"]),
                        Convert.ToInt32(sqlReader["cp"]),
                        sqlReader.IsDBNull(sqlReader.GetOrdinal("localite")) ? null : Convert.ToString(sqlReader["localite"])
                    );

                    Contact contact = new Contact(
                        Convert.ToInt32(sqlReader["identifiant"]),
                        sqlReader.IsDBNull(sqlReader.GetOrdinal("nom")) ? null : Convert.ToString(sqlReader["nom"]),
                        sqlReader.IsDBNull(sqlReader.GetOrdinal("prenom")) ? null : Convert.ToString(sqlReader["prenom"]),
                        adresse,
                        sqlReader.IsDBNull(sqlReader.GetOrdinal("registre_national")) ? null : Convert.ToString(sqlReader["registre_national"]),
                        sqlReader.IsDBNull(sqlReader.GetOrdinal("gsm")) ? null : Convert.ToString(sqlReader["gsm"]),
                        sqlReader.IsDBNull(sqlReader.GetOrdinal("telephone")) ? null : Convert.ToString(sqlReader["telephone"]),
                        sqlReader.IsDBNull(sqlReader.GetOrdinal("email")) ? null : Convert.ToString(sqlReader["email"])
                    );
                    listeContacts.Add(contact);
                }

                sqlReader.Close();
                return listeContacts;
            }
            catch (Exception ex)
            {
                throw new ExceptionAccesBD("Erreur lors de la récupération des contacts", ex.Message);
            }
        }

        // ==================== ENTREE ====================

        public int AjouterEntree(Entree entree)
        {
            try
            {
                string requete = "SELECT ajouter_entree(@raison, @date_entree, @identifiant_animal, @identifiant_contact)";

                NpgsqlCommand sqlCmd = new NpgsqlCommand(requete, sqlConn);
                sqlCmd.Parameters.Add(new NpgsqlParameter("raison", NpgsqlDbType.Text) { Value = entree.Raison ?? (object)DBNull.Value });
                sqlCmd.Parameters.Add(new NpgsqlParameter("date_entree", NpgsqlDbType.Date) { Value = entree.Date_entree });
                sqlCmd.Parameters.Add(new NpgsqlParameter("identifiant_animal", NpgsqlDbType.Integer) { Value = entree.Animal?.Identifiant ?? 0 });
                sqlCmd.Parameters.Add(new NpgsqlParameter("identifiant_contact", NpgsqlDbType.Integer) { Value = entree.Contact?.Identifiant ?? 0 });

                int id = Convert.ToInt32(sqlCmd.ExecuteScalar());
                return id;
            }
            catch (Exception ex)
            {
                throw new ExceptionAccesBD("Erreur lors de l'ajout de l'entrée", ex.Message);
            }
        }

        public List<Entree> ListeEntrees()
        {
            try
            {
                string requete = "SELECT * FROM lister_entrees()";

                NpgsqlCommand sqlCmd = new NpgsqlCommand(requete, sqlConn);
                NpgsqlDataReader sqlReader = sqlCmd.ExecuteReader();

                List<Entree> listeEntrees = new List<Entree>();

                while (sqlReader.Read())
                {
                    Animal2 animal = new Animal2();
                    animal.Identifiant = Convert.ToInt32(sqlReader["id_animal"]);
                    animal.Nom = sqlReader.IsDBNull(sqlReader.GetOrdinal("nom_animal")) ? null : Convert.ToString(sqlReader["nom_animal"]);
                    animal.Type = sqlReader.IsDBNull(sqlReader.GetOrdinal("type_animal")) ? null : Convert.ToString(sqlReader["type_animal"]);

                    Contact contact = new Contact();
                    contact.Identifiant = Convert.ToInt32(sqlReader["id_contact"]);
                    contact.Nom = sqlReader.IsDBNull(sqlReader.GetOrdinal("nom_contact")) ? null : Convert.ToString(sqlReader["nom_contact"]);
                    contact.Prenom = sqlReader.IsDBNull(sqlReader.GetOrdinal("prenom_contact")) ? null : Convert.ToString(sqlReader["prenom_contact"]);

                    Entree entree = new Entree(
                        sqlReader.IsDBNull(sqlReader.GetOrdinal("raison")) ? null : Convert.ToString(sqlReader["raison"]),
                        DateOnlyToDateTime(sqlReader["date_entree"]),
                        animal,
                        contact
                    );
                    listeEntrees.Add(entree);
                }

                sqlReader.Close();
                return listeEntrees;
            }
            catch (Exception ex)
            {
                throw new ExceptionAccesBD("Erreur lors de la récupération des entrées", ex.Message);
            }
        }

        // ==================== SORTIE ====================

        public int AjouterSortie(Sortie sortie)
        {
            try
            {
                string requete = "SELECT ajouter_sortie(@raison, @date_sortie, @identifiant_animal, @identifiant_contact)";

                NpgsqlCommand sqlCmd = new NpgsqlCommand(requete, sqlConn);
                sqlCmd.Parameters.Add(new NpgsqlParameter("raison", NpgsqlDbType.Text) { Value = sortie.Raison ?? (object)DBNull.Value });
                sqlCmd.Parameters.Add(new NpgsqlParameter("date_sortie", NpgsqlDbType.Date) { Value = sortie.Date_sortie });
                sqlCmd.Parameters.Add(new NpgsqlParameter("identifiant_animal", NpgsqlDbType.Integer) { Value = sortie.Animal?.Identifiant ?? 0 });
                sqlCmd.Parameters.Add(new NpgsqlParameter("identifiant_contact", NpgsqlDbType.Integer) { Value = sortie.Contact?.Identifiant ?? 0 });

                int id = Convert.ToInt32(sqlCmd.ExecuteScalar());
                return id;
            }
            catch (Exception ex)
            {
                throw new ExceptionAccesBD("Erreur lors de l'ajout de la sortie", ex.Message);
            }
        }

        public List<Sortie> ListeSorties()
        {
            try
            {
                string requete = "SELECT * FROM lister_sorties()";

                NpgsqlCommand sqlCmd = new NpgsqlCommand(requete, sqlConn);
                NpgsqlDataReader sqlReader = sqlCmd.ExecuteReader();

                List<Sortie> listeSorties = new List<Sortie>();

                while (sqlReader.Read())
                {
                    Animal2 animal = new Animal2();
                    animal.Identifiant = Convert.ToInt32(sqlReader["id_animal"]);
                    animal.Nom = sqlReader.IsDBNull(sqlReader.GetOrdinal("nom_animal")) ? null : Convert.ToString(sqlReader["nom_animal"]);
                    animal.Type = sqlReader.IsDBNull(sqlReader.GetOrdinal("type_animal")) ? null : Convert.ToString(sqlReader["type_animal"]);

                    Contact contact = new Contact();
                    contact.Identifiant = Convert.ToInt32(sqlReader["id_contact"]);
                    contact.Nom = sqlReader.IsDBNull(sqlReader.GetOrdinal("nom_contact")) ? null : Convert.ToString(sqlReader["nom_contact"]);
                    contact.Prenom = sqlReader.IsDBNull(sqlReader.GetOrdinal("prenom_contact")) ? null : Convert.ToString(sqlReader["prenom_contact"]);

                    Sortie sortie = new Sortie(
                        sqlReader.IsDBNull(sqlReader.GetOrdinal("raison")) ? null : Convert.ToString(sqlReader["raison"]),
                        DateOnlyToDateTime(sqlReader["date_sortie"]),
                        animal,
                        contact
                    );
                    listeSorties.Add(sortie);
                }

                sqlReader.Close();
                return listeSorties;
            }
            catch (Exception ex)
            {
                throw new ExceptionAccesBD("Erreur lors de la récupération des sorties", ex.Message);
            }
        }

        // ==================== ADOPTION ====================

        public int AjouterAdoption(Adoption adoption)
        {
            try
            {
                string requete = "SELECT ajouter_adoption(@statut, @date_demande, @identifiant_animal, @identifiant_contact)";

                NpgsqlCommand sqlCmd = new NpgsqlCommand(requete, sqlConn);
                sqlCmd.Parameters.Add(new NpgsqlParameter("statut", NpgsqlDbType.Text) { Value = adoption.Statut ?? (object)DBNull.Value });
                sqlCmd.Parameters.Add(new NpgsqlParameter("date_demande", NpgsqlDbType.Date) { Value = adoption.Date_demande });
                sqlCmd.Parameters.Add(new NpgsqlParameter("identifiant_animal", NpgsqlDbType.Integer) { Value = adoption.Animal_adopte?.Identifiant ?? 0 });
                sqlCmd.Parameters.Add(new NpgsqlParameter("identifiant_contact", NpgsqlDbType.Integer) { Value = adoption.Adoptant?.Identifiant ?? 0 });

                int id = Convert.ToInt32(sqlCmd.ExecuteScalar());
                return id;
            }
            catch (Exception ex)
            {
                throw new ExceptionAccesBD("Erreur lors de l'ajout de l'adoption", ex.Message);
            }
        }

        public List<Adoption> ListeAdoptions()
        {
            try
            {
                string requete = "SELECT * FROM lister_adoptions()";

                NpgsqlCommand sqlCmd = new NpgsqlCommand(requete, sqlConn);
                NpgsqlDataReader sqlReader = sqlCmd.ExecuteReader();

                List<Adoption> listeAdoptions = new List<Adoption>();

                while (sqlReader.Read())
                {
                    Animal2 animal = new Animal2();
                    animal.Identifiant = Convert.ToInt32(sqlReader["id_animal"]);
                    animal.Nom = sqlReader.IsDBNull(sqlReader.GetOrdinal("nom_animal")) ? null : Convert.ToString(sqlReader["nom_animal"]);
                    animal.Type = sqlReader.IsDBNull(sqlReader.GetOrdinal("type_animal")) ? null : Convert.ToString(sqlReader["type_animal"]);

                    Contact contact = new Contact();
                    contact.Identifiant = Convert.ToInt32(sqlReader["id_contact"]);
                    contact.Nom = sqlReader.IsDBNull(sqlReader.GetOrdinal("nom_contact")) ? null : Convert.ToString(sqlReader["nom_contact"]);
                    contact.Prenom = sqlReader.IsDBNull(sqlReader.GetOrdinal("prenom_contact")) ? null : Convert.ToString(sqlReader["prenom_contact"]);

                    Adoption adoption = new Adoption(
                        sqlReader.IsDBNull(sqlReader.GetOrdinal("statut")) ? null : Convert.ToString(sqlReader["statut"]),
                        DateOnlyToDateTime(sqlReader["date_demande"]),
                        animal,
                        contact
                    );
                    listeAdoptions.Add(adoption);
                }

                sqlReader.Close();
                return listeAdoptions;
            }
            catch (Exception ex)
            {
                throw new ExceptionAccesBD("Erreur lors de la récupération des adoptions", ex.Message);
            }
        }

        // ==================== ACCUEIL ====================

        public int AjouterAccueil(Accueil accueil)
        {
            try
            {
                string requete = "SELECT ajouter_accueil(@date_debut, @date_fin, @identifiant_animal, @identifiant_contact)";

                NpgsqlCommand sqlCmd = new NpgsqlCommand(requete, sqlConn);
                sqlCmd.Parameters.Add(new NpgsqlParameter("date_debut", NpgsqlDbType.Date) { Value = accueil.Date_debut });
                sqlCmd.Parameters.Add(new NpgsqlParameter("date_fin", NpgsqlDbType.Date) { Value = accueil.Date_fin == DateTime.MinValue ? DBNull.Value : accueil.Date_fin });
                sqlCmd.Parameters.Add(new NpgsqlParameter("identifiant_animal", NpgsqlDbType.Integer) { Value = accueil.Animal_accueilli?.Identifiant ?? 0 });
                sqlCmd.Parameters.Add(new NpgsqlParameter("identifiant_contact", NpgsqlDbType.Integer) { Value = accueil.Famille_accueil?.Identifiant ?? 0 });

                int id = Convert.ToInt32(sqlCmd.ExecuteScalar());
                return id;
            }
            catch (Exception ex)
            {
                throw new ExceptionAccesBD("Erreur lors de l'ajout de l'accueil", ex.Message);
            }
        }

        public List<Accueil> ListeAccueils()
        {
            try
            {
                string requete = "SELECT * FROM lister_accueils()";

                NpgsqlCommand sqlCmd = new NpgsqlCommand(requete, sqlConn);
                NpgsqlDataReader sqlReader = sqlCmd.ExecuteReader();

                List<Accueil> listeAccueils = new List<Accueil>();

                while (sqlReader.Read())
                {
                    Animal2 animal = new Animal2();
                    animal.Identifiant = Convert.ToInt32(sqlReader["id_animal"]);
                    animal.Nom = sqlReader.IsDBNull(sqlReader.GetOrdinal("nom_animal")) ? null : Convert.ToString(sqlReader["nom_animal"]);
                    animal.Type = sqlReader.IsDBNull(sqlReader.GetOrdinal("type_animal")) ? null : Convert.ToString(sqlReader["type_animal"]);

                    Contact contact = new Contact();
                    contact.Identifiant = Convert.ToInt32(sqlReader["id_contact"]);
                    contact.Nom = sqlReader.IsDBNull(sqlReader.GetOrdinal("nom_contact")) ? null : Convert.ToString(sqlReader["nom_contact"]);
                    contact.Prenom = sqlReader.IsDBNull(sqlReader.GetOrdinal("prenom_contact")) ? null : Convert.ToString(sqlReader["prenom_contact"]);

                    Accueil accueil = new Accueil(
                        DateOnlyToDateTime(sqlReader["date_debut"]),
                        sqlReader.IsDBNull(sqlReader.GetOrdinal("date_fin")) ? DateTime.MinValue : DateOnlyToDateTime(sqlReader["date_fin"]),
                        animal,
                        contact
                    );
                    listeAccueils.Add(accueil);
                }

                sqlReader.Close();
                return listeAccueils;
            }
            catch (Exception ex)
            {
                throw new ExceptionAccesBD("Erreur lors de la récupération des accueils", ex.Message);
            }
        }

        // ==================== VACCIN ====================

        public int AjouterVaccin(string nom)
        {
            try
            {
                string requete = "SELECT ajouter_vaccin(@nom)";

                NpgsqlCommand sqlCmd = new NpgsqlCommand(requete, sqlConn);
                sqlCmd.Parameters.Add(new NpgsqlParameter("nom", NpgsqlDbType.Text) { Value = nom });

                int id = Convert.ToInt32(sqlCmd.ExecuteScalar());
                return id;
            }
            catch (Exception ex)
            {
                throw new ExceptionAccesBD("Erreur lors de l'ajout du vaccin", ex.Message);
            }
        }

        public List<Vaccin> ListeVaccins()
        {
            try
            {
                string requete = "SELECT * FROM lister_vaccins()";

                NpgsqlCommand sqlCmd = new NpgsqlCommand(requete, sqlConn);
                NpgsqlDataReader sqlReader = sqlCmd.ExecuteReader();

                List<Vaccin> listeVaccins = new List<Vaccin>();

                while (sqlReader.Read())
                {
                    Vaccin vaccin = new Vaccin(Convert.ToString(sqlReader["nom"]));
                    listeVaccins.Add(vaccin);
                }

                sqlReader.Close();
                return listeVaccins;
            }
            catch (Exception ex)
            {
                throw new ExceptionAccesBD("Erreur lors de la récupération des vaccins", ex.Message);
            }
        }

        // ==================== VACCINATION ====================

        public int AjouterVaccination(int idAnimal, string nomVaccin, DateTime dateVaccination)
        {
            try
            {
                string requete = "SELECT ajouter_vaccination(@identifiant_animal, @nom_vaccin, @date_vaccination)";

                NpgsqlCommand sqlCmd = new NpgsqlCommand(requete, sqlConn);
                sqlCmd.Parameters.Add(new NpgsqlParameter("identifiant_animal", NpgsqlDbType.Integer) { Value = idAnimal });
                sqlCmd.Parameters.Add(new NpgsqlParameter("nom_vaccin", NpgsqlDbType.Text) { Value = nomVaccin });
                sqlCmd.Parameters.Add(new NpgsqlParameter("date_vaccination", NpgsqlDbType.Date) { Value = dateVaccination });

                int id = Convert.ToInt32(sqlCmd.ExecuteScalar());
                return id;
            }
            catch (Exception ex)
            {
                throw new ExceptionAccesBD("Erreur lors de l'ajout de la vaccination", ex.Message);
            }
        }

        public List<Vaccination> ListeVaccinations()
        {
            try
            {
                List<Vaccination> listeVaccinations = new List<Vaccination>();

                
                List<Animal2> animaux = ListeAnimaux();

               
                foreach (Animal2 animal in animaux)
                {
                    string requete = "SELECT * FROM lister_vaccinations_animal(@p_identifiant_animal)";

                    NpgsqlCommand sqlCmd = new NpgsqlCommand(requete, sqlConn);
                    sqlCmd.Parameters.Add(new NpgsqlParameter("p_identifiant_animal", NpgsqlDbType.Integer) { Value = animal.Identifiant });

                    NpgsqlDataReader sqlReader = sqlCmd.ExecuteReader();

                    while (sqlReader.Read())
                    {
                        Animal2 animalVaccine = new Animal2();
                        animalVaccine.Identifiant = Convert.ToInt32(sqlReader["identifiant_animal"]);
                        animalVaccine.Nom = sqlReader.IsDBNull(sqlReader.GetOrdinal("nom_animal")) ? null : Convert.ToString(sqlReader["nom_animal"]);
                        animalVaccine.Type = sqlReader.IsDBNull(sqlReader.GetOrdinal("type_animal")) ? null : Convert.ToString(sqlReader["type_animal"]);

                        Vaccin vaccin = new Vaccin(Convert.ToString(sqlReader["nom_vaccin"]));

                        Vaccination vaccination = new Vaccination(
                            DateOnlyToDateTime(sqlReader["date_vaccination"]),
                            animalVaccine,
                            vaccin
                        );
                        listeVaccinations.Add(vaccination);
                    }

                    sqlReader.Close();
                }

                return listeVaccinations;
            }
            catch (Exception ex)
            {
                throw new ExceptionAccesBD("Erreur lors de la récupération des vaccinations", ex.Message);
            }
        }

        // ==================== FERMETURE CONNEXION ====================

        public void FermerConnexion()
        {
            if (sqlConn != null && sqlConn.State == System.Data.ConnectionState.Open)
            {
                sqlConn.Close();
            }
        }
    }
}