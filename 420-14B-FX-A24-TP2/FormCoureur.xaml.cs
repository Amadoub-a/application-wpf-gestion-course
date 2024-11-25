using _420_14B_FX_A24_TP2.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using _420_14B_FX_A24_TP2.enums;

namespace _420_14B_FX_A24_TP2
{
    /// <summary>
    /// Logique d'interaction pour FormCoureur.xaml
    /// </summary>
    public partial class FormCoureur : Window
    {

        private Coureur _coureur;
        private EtatFormulaire _etat;

        public Coureur Coureur
        {
            get { return _coureur; }
            set { _coureur = value; }
        }

        public EtatFormulaire Etat
        {
            get { return _etat; }
            set { _etat = value; }
        }

        public FormCoureur(Coureur coureur = null, EtatFormulaire etat = EtatFormulaire.Ajouter)
        {
            Coureur = coureur;
            Etat = etat;
            InitializeComponent();
            RemplirCombos();
        }


        /// <summary>
        /// Méthode qui permet de remplir les comboBox à l'ouverture du formulaire
        /// </summary>
        private void RemplirCombos()
        {
            cBoxProvince.Items.Clear();
            cBoxCategorie.Items.Clear();

            string[] vectProvince = UtilEnum.GetAllDescriptions<Province>();
            string[] vetcCategorie = UtilEnum.GetAllDescriptions<Categorie>(); 

            for (int i = 0; i < vectProvince.Length; i++)
            {
                cBoxProvince.Items.Add(vectProvince[i]);
            }

            for (int i = 0; i < vetcCategorie.Length; i++)
            {
                cBoxCategorie.Items.Add(vetcCategorie[i]);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnAjouModifSupCoureur.Content = Etat;
            tbTitreCoureur.Text = $"{Etat} un coureur";

            switch (Etat)
            {
                case EtatFormulaire.Ajouter:
                    break;
                case EtatFormulaire.Modifier:
                case EtatFormulaire.Supprimer:
                    txtNumero.Text = Coureur.Dossard.ToString();
                    txtNumero.IsEnabled = false;

                    txtNom.Text = Coureur.Nom;
                    txtPrenom.Text = Coureur.Prenom;
                    txtVille.Text = Coureur.Ville;
                    cBoxProvince.SelectedIndex = (int)Coureur.Province;
                    cBoxCategorie.SelectedIndex = (int)Coureur.Categorie;
                    tsudTemps.Text = Coureur.Temps.ToString();
                    chkAbandon.IsChecked = Coureur.Abandon;

                    if (Etat == EtatFormulaire.Supprimer)
                    {
                        // Désactiver le contrôle de saisie 
                        txtNom.IsEnabled = false;
                        txtPrenom.IsEnabled = false;
                        txtVille.IsEnabled = false;
                        cBoxProvince.IsEnabled = false;
                        cBoxCategorie.IsEnabled = false;
                        tsudTemps.IsEnabled = false;
                        chkAbandon.IsEnabled = false;
                    }
                    
                    break;
            }
        }

        private void btnAjouModifSupCoureur_Click(object sender, RoutedEventArgs e)
        {
            if (ValiderFormulaire())
            {
                try
                {
                    ushort nodossard = ushort.Parse(txtNumero.Text);
                    string nom = txtNom.Text;
                    string prenom = txtPrenom.Text;
                    string ville = txtVille.Text;
                    Province province = (Province)cBoxProvince.SelectedIndex;
                    Categorie categorie = (Categorie)cBoxCategorie.SelectedIndex;
                    TimeSpan temps = TimeSpan.Zero;
                    bool abandon = chkAbandon.IsChecked == true ? true : false;

                    if (!string.IsNullOrWhiteSpace(tsudTemps.Text))
                        temps = TimeSpan.Parse(tsudTemps.Text);
                    
                    
                    switch (Etat)
                    {
                        case EtatFormulaire.Ajouter:

                            Coureur = new Coureur(nodossard, nom, prenom, categorie, ville, province, temps);
                            Coureur.Abandon = abandon;

                            DialogResult = true;

                            break;
                        case EtatFormulaire.Modifier:
                            Coureur.Dossard = nodossard;
                            Coureur.Nom = nom;
                            Coureur.Prenom = prenom;
                            Coureur.Ville = ville;
                            Coureur.Province = province;
                            Coureur.Categorie = categorie;
                            Coureur.Temps = temps;
                            Coureur.Abandon = abandon;

                            DialogResult = true;

                            break;
                        case EtatFormulaire.Supprimer:
                             DialogResult = true;

                            break;
                    }
                }
                catch (ArgumentNullException ane)
                {
                    MessageBox.Show(ane.Message, $"{Etat} un coureur");
                }
                catch (InvalidOperationException ioe)
                {
                    MessageBox.Show(ioe.Message, $"{Etat} un coureur");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, $"{Etat} un coureur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }


        }
   
        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void chkAbandon_Checked(object sender, RoutedEventArgs e)
        {

            if (chkAbandon.IsChecked != true)
            {
                // Réactiver le contrôle de saisie du temps
                tsudTemps.IsEnabled = true;
            }
            else
            {
                // Réinitialiser le temps
                tsudTemps.Value = TimeSpan.Zero;

                // Désactiver le contrôle de saisie du temps
                tsudTemps.IsEnabled = false;
               
            }
        }

        /// <summary>
        /// Méthode permettant la validation du formulaire course 
        /// </summary>
        /// <returns>True si tous les champs on été rempli correctement, false si non</returns>
        private bool ValiderFormulaire()
        {
            StringBuilder sb = new StringBuilder();

            ushort dossard;
            if (!ushort.TryParse(txtNumero.Text, out dossard) || dossard < Coureur.DOSSARD_VAL_MIN)
                sb.AppendLine($"-Le No dossard doit être une valeur numérique supérieure ou égale à {Coureur.DOSSARD_VAL_MIN}.");

            if (string.IsNullOrWhiteSpace(txtNom.Text) || txtNom.Text.Length < Coureur.NOM_NB_CARC_MIN)
                sb.AppendLine($"-Le nom doit contenir au moins {Coureur.NOM_NB_CARC_MIN} caractères.");

            if (string.IsNullOrWhiteSpace(txtPrenom.Text) || txtPrenom.Text.Length < Coureur.PRENOM_NB_CARC_MIN)
                sb.AppendLine($"-Le prénom doit contenir au moins {Coureur.PRENOM_NB_CARC_MIN} caractères");

            if (string.IsNullOrWhiteSpace(txtVille.Text) || txtVille.Text.Length < Coureur.VILLE_NB_CARC_MIN)
                sb.AppendLine($"-Le nom de la ville doit contenir au moins {Coureur.VILLE_NB_CARC_MIN} caractères.");

            if (cBoxProvince.SelectedIndex == -1)
                sb.AppendLine("-Veuillez sélectionner une province.");

            if (cBoxCategorie.SelectedIndex == -1)
                sb.AppendLine("-Veuillez sélectionner une catégorie.");

            if (sb.Length > 0)
            {
                MessageBox.Show(sb.ToString(), "Validation du formulaire");
                return false;
            }
            return true;
        }

    }
}
