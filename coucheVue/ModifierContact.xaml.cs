using System;
using System.Windows;
using RefugeAnimauxWPF.coucheModeleVue;

namespace RefugeAnimauxWPF.coucheVue
{
    public partial class ModifierContact : Window
    {
        public ModifierContact()
        {
            InitializeComponent();
            try
            {
                this.DataContext = new ModifierContactVueModele();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }

        private void Modifier_Click(object sender, RoutedEventArgs e)
        {
            var vm = (ModifierContactVueModele)this.DataContext;

            if (vm.Valide())
            {
                try
                {
                    int retour = vm.Modifier();
                    if (retour != 0)
                    {
                        MessageBox.Show("Le contact a bien été modifié !",
                            "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("La modification ne s'est pas bien déroulée",
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
                MessageBox.Show("Veuillez sélectionner un contact et remplir les champs obligatoires (nom, prénom, adresse)",
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