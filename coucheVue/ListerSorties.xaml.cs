using System;
using System.Windows;
using RefugeAnimauxWPF.coucheModeleVue;

namespace RefugeAnimauxWPF.coucheVue
{
    public partial class ListerSorties : Window
    {
        public ListerSorties()
        {
            InitializeComponent();
            try
            {
                this.DataContext = new ListerSortiesVueModele();
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
                AjouterSortie fenetre = new AjouterSortie();
                if (fenetre.ShowDialog() == true)
                {
                    ((ListerSortiesVueModele)this.DataContext).ChargerListe();
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
                ((ListerSortiesVueModele)this.DataContext).ChargerListe();
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
