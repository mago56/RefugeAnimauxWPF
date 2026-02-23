using System;
using System.Windows;
using RefugeAnimauxWPF.coucheModeleVue;

namespace RefugeAnimauxWPF.coucheVue
{
    public partial class GererAccueil : Window
    {
        public GererAccueil()
        {
            InitializeComponent();
            try
            {
                this.DataContext = new GererAccueilVueModele();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }

        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            var vm = (GererAccueilVueModele)this.DataContext;

            if (vm.Valide())
            {
                try
                {
                    int id = vm.Ajouter();
                    if (id != 0)
                    {
                        MessageBox.Show($"L'accueil (ID = {id}) a bien été enregistré!", 
                            "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                        vm.ChargerListe();
                    }
                    else
                    {
                        MessageBox.Show("L'ajout ne s'est pas bien déroulé", 
                            "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un animal, une famille et une date de début",
                    "Champs manquants", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Rafraichir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((GererAccueilVueModele)this.DataContext).ChargerListe();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Fermer_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
