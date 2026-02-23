using System;
using System.Windows;
using RefugeAnimauxWPF.coucheModeleVue;

namespace RefugeAnimauxWPF.coucheVue
{
    public partial class ListerContacts : Window
    {
        public ListerContacts()
        {
            InitializeComponent();
            try
            {
                this.DataContext = new ListerContactsVueModele();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }

        private void AjoutContact_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AjouterContact fenetre = new AjouterContact();
                if (fenetre.ShowDialog() == true)
                {
                    ((ListerContactsVueModele)this.DataContext).ChargerListe();
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
                ((ListerContactsVueModele)this.DataContext).ChargerListe();
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
