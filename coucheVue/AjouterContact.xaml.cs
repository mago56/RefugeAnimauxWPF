using System;
using System.Windows;
using RefugeAnimauxWPF.coucheModeleVue;

namespace RefugeAnimauxWPF.coucheVue
{
    public partial class AjouterContact : Window
    {
        public AjouterContact()
        {
            InitializeComponent();
            try
            {
                this.DataContext = new AjouterContactVueModele();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }

        private void AjoutContact_Click(object sender, RoutedEventArgs e)
        {
            var vm = (AjouterContactVueModele)this.DataContext;

            if (vm.Valide())
            {
                try
                {
                    int idContact = vm.Ajouter();
                    if (idContact != 0)
                    {
                        MessageBox.Show($"Le contact (ID = {idContact}) a bien été ajouté!", 
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
                MessageBox.Show("Veuillez remplir les champs obligatoires (nom, prénom, adresse)",
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
