using System;
using System.Windows;
using RefugeAnimauxWPF.coucheModeleVue;

namespace RefugeAnimauxWPF.coucheVue
{
    public partial class ListerAnimaux : Window
    {
        public ListerAnimaux()
        {
            InitializeComponent();
            try
            {
                this.DataContext = new ListerAnimauxVueModele();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }

        private void AjoutAnimal_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AjouterAnimal fenetre = new AjouterAnimal();
                if (fenetre.ShowDialog() == true)
                {
                    ((ListerAnimauxVueModele)this.DataContext).ChargerListe();
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
                ((ListerAnimauxVueModele)this.DataContext).ChargerListe();
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
