using System;
using System.Windows;
using System.Windows.Input;

namespace RefugeAnimauxWPF.coucheVue
{
    public partial class MainWindow : Window
    {
        // Commandes pour les animaux
        public ICommand ListerAnimaux_Click { get; set; }
        public ICommand AjouterAnimal_Click { get; set; }
        public ICommand SupprimerAnimal_Click { get; set; }
        
        // Commandes pour les contacts
        public ICommand ListerContacts_Click { get; set; }
        public ICommand AjouterContact_Click { get; set; }
        public ICommand ModifierContact_Click { get; set; }
        
        // Commandes pour les entrées/sorties
        public ICommand ListerEntrees_Click { get; set; }
        public ICommand ListerSorties_Click { get; set; }
        public ICommand AjouterEntree_Click { get; set; }
        public ICommand AjouterSortie_Click { get; set; }
        
        // Commandes pour les adoptions
        public ICommand ListerAdoptions_Click { get; set; }
        public ICommand AjouterAdoption_Click { get; set; }
        public ICommand GererAccueil_Click { get; set; }
        
        // Commandes pour les vaccins
        public ICommand GererVaccins_Click { get; set; }
        public ICommand GererVaccinations_Click { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            
            // Initialisation des commandes - Animaux
            ListerAnimaux_Click = new CommandeMenu(ExecuteListerAnimaux, CanExecuteCommandeMenu);
            AjouterAnimal_Click = new CommandeMenu(ExecuteAjouterAnimal, CanExecuteCommandeMenu);
            SupprimerAnimal_Click = new CommandeMenu(ExecuteSupprimerAnimal, CanExecuteCommandeMenu);
            
            // Initialisation des commandes - Contacts
            ListerContacts_Click = new CommandeMenu(ExecuteListerContacts, CanExecuteCommandeMenu);
            AjouterContact_Click = new CommandeMenu(ExecuteAjouterContact, CanExecuteCommandeMenu);
            ModifierContact_Click = new CommandeMenu(ExecuteModifierContact, CanExecuteCommandeMenu);
            
            // Initialisation des commandes - Entrées/Sorties
            ListerEntrees_Click = new CommandeMenu(ExecuteListerEntrees, CanExecuteCommandeMenu);
            ListerSorties_Click = new CommandeMenu(ExecuteListerSorties, CanExecuteCommandeMenu);
            AjouterEntree_Click = new CommandeMenu(ExecuteAjouterEntree, CanExecuteCommandeMenu);
            AjouterSortie_Click = new CommandeMenu(ExecuteAjouterSortie, CanExecuteCommandeMenu);
            
            // Initialisation des commandes - Adoptions
            ListerAdoptions_Click = new CommandeMenu(ExecuteListerAdoptions, CanExecuteCommandeMenu);
            AjouterAdoption_Click = new CommandeMenu(ExecuteAjouterAdoption, CanExecuteCommandeMenu);
            GererAccueil_Click = new CommandeMenu(ExecuteGererAccueil, CanExecuteCommandeMenu);
            
            // Initialisation des commandes - Vaccins
            GererVaccins_Click = new CommandeMenu(ExecuteGererVaccins, CanExecuteCommandeMenu);
            GererVaccinations_Click = new CommandeMenu(ExecuteGererVaccinations, CanExecuteCommandeMenu);
        }

        // ==================== ANIMAUX ====================
        public void ExecuteListerAnimaux(object? parameter)
        {
            try
            {
                ListerAnimaux fenetre = new ListerAnimaux();
                fenetre.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void ExecuteAjouterAnimal(object? parameter)
        {
            try
            {
                AjouterAnimal fenetre = new AjouterAnimal();
                fenetre.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void ExecuteSupprimerAnimal(object? parameter)
        {
            MessageBox.Show("Fonctionnalité à implémenter : Supprimer un animal", "Information");
        }

        // ==================== CONTACTS ====================
        public void ExecuteListerContacts(object? parameter)
        {
            try
            {
                ListerContacts fenetre = new ListerContacts();
                fenetre.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void ExecuteAjouterContact(object? parameter)
        {
            try
            {
                AjouterContact fenetre = new AjouterContact();
                fenetre.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void ExecuteModifierContact(object? parameter)
        {
            MessageBox.Show("Fonctionnalité à implémenter : Modifier un contact", "Information");
        }

        // ==================== ENTREES/SORTIES ====================
        public void ExecuteListerEntrees(object? parameter)
        {
            try
            {
                ListerEntrees fenetre = new ListerEntrees();
                fenetre.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void ExecuteListerSorties(object? parameter)
        {
            try
            {
                ListerSorties fenetre = new ListerSorties();
                fenetre.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void ExecuteAjouterEntree(object? parameter)
        {
            try
            {
                AjouterEntree fenetre = new AjouterEntree();
                fenetre.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void ExecuteAjouterSortie(object? parameter)
        {
            try
            {
                AjouterSortie fenetre = new AjouterSortie();
                fenetre.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // ==================== ADOPTIONS ====================
        public void ExecuteListerAdoptions(object? parameter)
        {
            try
            {
                ListerAdoptions fenetre = new ListerAdoptions();
                fenetre.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void ExecuteAjouterAdoption(object? parameter)
        {
            try
            {
                AjouterAdoption fenetre = new AjouterAdoption();
                fenetre.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void ExecuteGererAccueil(object? parameter)
        {
            try
            {
                GererAccueil fenetre = new GererAccueil();
                fenetre.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // ==================== VACCINS ====================
        public void ExecuteGererVaccins(object? parameter)
        {
            try
            {
                GererVaccins fenetre = new GererVaccins();
                fenetre.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void ExecuteGererVaccinations(object? parameter)
        {
            try
            {
                GererVaccinations fenetre = new GererVaccinations();
                fenetre.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public bool CanExecuteCommandeMenu(object? parameter)
        {
            return true;
        }

        private void BtQuitterProgramme_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
