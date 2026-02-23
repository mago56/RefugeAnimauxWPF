using System;
using System.Windows;
using RefugeAnimauxWPF.coucheModeleVue;

namespace RefugeAnimauxWPF.coucheVue
{
    public partial class ListerEntrees : Window
    {
        public ListerEntrees()
        {
            InitializeComponent();
            try
            {
                this.DataContext = new ListerEntreesVueModele();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }

        private void Ajouter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AjouterEntree fenetre = new AjouterEntree();
                if (fenetre.ShowDialog() == true)
                {
                    ((ListerEntreesVueModele)this.DataContext).ChargerListe();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Rafraichir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ((ListerEntreesVueModele)this.DataContext).ChargerListe();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BFermer_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
