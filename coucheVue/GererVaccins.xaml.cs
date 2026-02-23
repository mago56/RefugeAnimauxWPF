using System;
using System.Windows;
using RefugeAnimauxWPF.coucheModeleVue;

namespace RefugeAnimauxWPF.coucheVue
{
    public partial class GererVaccins : Window
    {
        public GererVaccins()
        {
            InitializeComponent();
            try
            {
                this.DataContext = new GererVaccinsVueModele();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }

        private void AjouterVaccin_Click(object sender, RoutedEventArgs e)
        {
            var vm = (GererVaccinsVueModele)this.DataContext;

            if (!string.IsNullOrWhiteSpace(vm.NouveauVaccin))
            {
                try
                {
                    int id = vm.AjouterVaccin();
                    if (id != 0)
                    {
                        MessageBox.Show($"Le vaccin a bien été ajouté!", 
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
                MessageBox.Show("Veuillez entrer le nom du vaccin",
                    "Champ manquant", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Rafraichir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((GererVaccinsVueModele)this.DataContext).ChargerListe();
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
