using System;
using System.Windows;
using RefugeAnimauxWPF.coucheModeleVue;

namespace RefugeAnimauxWPF.coucheVue
{
    public partial class AjouterAnimal : Window
    {
        public AjouterAnimal()
        {
            InitializeComponent();
            try
            {
                this.DataContext = new AjouterAnimalVueModele();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }

        private void AjoutAnimal_Click(object sender, RoutedEventArgs e)
        {
            var vm = (AjouterAnimalVueModele)this.DataContext;

            if (vm.Valide())
            {
                try
                {
                    int idAnimal = vm.Ajouter();
                    if (idAnimal != 0)
                    {
                        MessageBox.Show($"L'animal (ID = {idAnimal}) a bien été ajouté!", 
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
                MessageBox.Show("Veuillez remplir les champs obligatoires (nom, type, date de naissance)",
                    "Champs manquants", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BFermer_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            Close();
        }
    }
}
