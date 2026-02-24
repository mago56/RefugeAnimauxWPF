using System;
using System.Windows;
using RefugeAnimauxWPF.coucheModeleVue;

namespace RefugeAnimauxWPF.coucheVue
{
    public partial class SupprimerAnimal : Window
    {
        public SupprimerAnimal()
        {
            InitializeComponent();
            try
            {
                this.DataContext = new SupprimerAnimalVueModele();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }

        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            var vm = (SupprimerAnimalVueModele)this.DataContext;

            if (vm.Valide())
            {
                MessageBoxResult confirmation = MessageBox.Show(
                    $"Êtes-vous sûr de vouloir supprimer l'animal '{vm.AnimalSelectionne?.Nom}' (ID: {vm.AnimalSelectionne?.Identifiant}) ?\n\nCette action supprimera également ses compatibilités et vaccinations.",
                    "Confirmation de suppression", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (confirmation == MessageBoxResult.Yes)
                {
                    try
                    {
                        int retour = vm.Supprimer();
                        if (retour != 0)
                        {
                            MessageBox.Show("L'animal a bien été supprimé !",
                                "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("La suppression ne s'est pas bien déroulée",
                                "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erreur : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un animal à supprimer",
                    "Champ manquant", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Fermer_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            Close();
        }
    }
}