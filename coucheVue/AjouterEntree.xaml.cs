using System;
using System.Windows;
using RefugeAnimauxWPF.coucheModeleVue;

namespace RefugeAnimauxWPF.coucheVue
{
    public partial class AjouterEntree : Window
    {
        public AjouterEntree()
        {
            InitializeComponent();
            try
            {
                this.DataContext = new AjouterEntreeVueModele();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }

        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            var vm = (AjouterEntreeVueModele)this.DataContext;

            if (vm.Valide())
            {
                try
                {
                    int id = vm.Ajouter();
                    if (id != 0)
                    {
                        MessageBox.Show($"L'entrée (ID = {id}) a bien été ajoutée!", 
                            "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
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
                MessageBox.Show("Veuillez remplir tous les champs obligatoires",
                    "Champs manquants", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Fermer_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            Close();
        }
    }
}
