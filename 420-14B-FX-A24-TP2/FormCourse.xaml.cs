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
using _420_14B_FX_A24_TP2.classes;
using _420_14B_FX_A24_TP2.enums;

namespace _420_14B_FX_A24_TP2
{
    /// <summary>
    /// Logique d'interaction pour FormCourse.xaml
    /// </summary>
    public partial class FormCourse : Window
    {
    
        private Course _course;

        private EtatFormulaire _etat;


        public Course Course
        {
            get { return _course; }
            private set { _course = value; }
        }

        public EtatFormulaire Etat
        {
            get { return _etat; }
            private set { _etat = value; }
        }

        public FormCourse(EtatFormulaire etat = EtatFormulaire.Ajouter, Course course = null)
        {
            Course = course;
            Etat = etat;
            InitializeComponent();
            RemplirCombos();
        }

        /// <summary>
        /// Permet d'afficher les coureurs d'une course
        /// </summary>
        /// <param name="Course">La course concernée</param>
        private void AfficherCoureurs(Course Course)
        {
            lstCoureurs.Items.Clear();
            Course.TrierCoureurs();

            foreach(Coureur coureur in Course.Coureurs)
            {
                lstCoureurs.Items.Add(coureur);
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnAjouModifSupprCourse.Content = Etat;
            tbTitreCourse.Text = $"{Etat} une course";

            switch (Etat)
            {
                case EtatFormulaire.Ajouter:
                    //Aucune information des coureurs n'est disponible si la course n'existe pas. 
                    tbItemCoureurs.IsEnabled = false;
                     break;
                case EtatFormulaire.Modifier:
                case EtatFormulaire.Supprimer:

                    txtNom.Text = Course.Nom;
                    txtVille.Text = Course.Ville;
                    txtDistance.Text = Course.Distance.ToString();
                    cBoxProvince.SelectedIndex = (int)Course.Province;
                    cBoxType.SelectedIndex = (int)Course.TypeCourse;
                    dtpDateCourse.Text = Course.Date.ToString();
                    txtNbParticipant.Text = Course.NbParticipants.ToString();
                    this.tsudTempsMoyens.Value = Course.TempCourseMoyen;

                    if (Etat == EtatFormulaire.Supprimer)
                    {
                        txtNom.IsEnabled = false;
                        txtVille.IsEnabled = false;
                        cBoxProvince.IsEnabled = false;
                        cBoxType.IsEnabled = false;
                        dtpDateCourse.IsEnabled = false;
                        txtDistance.IsEnabled = false;
                    }
                   
                    AfficherCoureurs(Course);

                    break;
            }
        }

        private void btnAjouModifSupprCourse_Click(object sender, RoutedEventArgs e)
        {

            if (ValiderFormulaire())
            {
                try
                {
                    string nom = txtNom.Text;
                    string ville = txtVille.Text;
                    Province province = (Province)cBoxProvince.SelectedIndex;
                    DateOnly date = DateOnly.Parse(dtpDateCourse.Text);
                    TypeCourse typeCourse = (TypeCourse)cBoxType.SelectedIndex;
                    ushort distance = ushort.Parse(txtDistance.Text);

                    switch (Etat)
                    {
                        case EtatFormulaire.Ajouter:

                            Guid id = Guid.NewGuid();
                            Course = new Course(id,nom,date,ville,province,typeCourse,distance);

                            DialogResult = true;

                            break;
                        case EtatFormulaire.Modifier:
                            Course.Nom = nom;
                            Course.Ville = ville;
                            Course.Distance = distance;
                            Course.Province = province;
                            Course.TypeCourse = typeCourse;
                            Course.Date = date;

                            DialogResult = true;

                            break;
                        case EtatFormulaire.Supprimer:
                             DialogResult = true;

                            break;
                    }
                }
                catch (ArgumentNullException ane)
                {
                    MessageBox.Show(ane.Message, $"{Etat} une course");
                }
                catch (InvalidOperationException ioe)
                {
                    MessageBox.Show(ioe.Message, $"{Etat} une course");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, $"{Etat} une course", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }            
          
        }

        /// <summary>
        /// Méthode qui permet de remplir les comboBox à l'ouverture du formulaire
        /// </summary>
        private void RemplirCombos()
        {
            cBoxProvince.Items.Clear();
            cBoxType.Items.Clear();

            string[] vectProvince = UtilEnum.GetAllDescriptions<Province>();
            string[] vetcType = Enum.GetNames(typeof(TypeCourse));

            for (int i = 0; i < vectProvince.Length; i++)
            {
                cBoxProvince.Items.Add(vectProvince[i]);
            }

            for (int i = 0; i < vetcType.Length; i++)
            {
                cBoxType.Items.Add(vetcType[i]);
            }
        }

        /// <summary>
        /// Méthode permettant la validation du formulaire course 
        /// </summary>
        /// <returns>True si tous les champs on été rempli correctement, false si non</returns>
        private bool ValiderFormulaire()
        {
            StringBuilder sb = new StringBuilder();

            if (string.IsNullOrWhiteSpace(txtNom.Text) || txtNom.Text.Length < Course.NOM_NB_CAR_MIN)
                sb.AppendLine($"- Le nom de la course doit contenir au moins {Course.NOM_NB_CAR_MIN} caractères.");

            if (string.IsNullOrWhiteSpace(txtVille.Text) || txtVille.Text.Length < Course.VILLE_NB_CAR_MIN)
                sb.AppendLine($"- Le nom de la ville doit contenir au moins {Course.VILLE_NB_CAR_MIN} caractères.");

            if (cBoxProvince.SelectedIndex == -1)
                sb.AppendLine($"- Vous devez sélectionner une province.");
            
            if (dtpDateCourse.SelectedDate == null)
                sb.AppendLine($"- Vous devez sélectionner une date pour la course.");

            if (cBoxType.SelectedIndex == -1)
                sb.AppendLine($"- Vous devez sélectionner un type de course.");

            ushort distance;
            if (!ushort.TryParse(txtDistance.Text, out distance) || distance < Course.DISTANCE_VAL_MIN)
                sb.AppendLine($"- La distance doit être une valeur numérique supérieure ou égale à {Course.DISTANCE_VAL_MIN}.");

            if (sb.Length > 0)
            {
                MessageBox.Show(sb.ToString(), "Validation du formulaire");
                return false;
            }

            return true;
        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void btnAjouCoureur_Click(object sender, RoutedEventArgs e)
        {
            FormCoureur frmCoureur = new FormCoureur();
            EtatFormulaire EtatFormCoureur = frmCoureur.Etat;

            if (frmCoureur.ShowDialog() == true)
            {
                try
                {
                    Coureur nouveauCoureur = frmCoureur.Coureur;
                    Course.AjouterCoureur(nouveauCoureur);

                    AfficherCoureurs(Course);
                    MessageBox.Show("Coureur ajouté avec succèss.");

                }
                catch (ArgumentNullException ane)
                {
                    MessageBox.Show(ane.Message, $"{EtatFormCoureur} un coureur");
                }
                catch (InvalidOperationException ioe)
                {
                    MessageBox.Show(ioe.Message, $"{EtatFormCoureur} un coureur");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, $"{EtatFormCoureur} une course", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
               
        private void btnModifCoureur_Click(object sender, RoutedEventArgs e)
        {

            if (lstCoureurs.SelectedItem != null)
            {        
                Coureur courseSelectionnee = lstCoureurs.SelectedItem as Coureur;
                EtatFormulaire EtatFormCoureur = EtatFormulaire.Modifier;

                FormCoureur frmCoureur = new FormCoureur(courseSelectionnee, EtatFormCoureur);

                if (frmCoureur.ShowDialog() == true)
                {
                    try
                    {
                        AfficherCoureurs(Course);
                        MessageBox.Show("Coureur modifié avec succèss.");
                    }
                    catch (ArgumentNullException ane)
                    {
                        MessageBox.Show(ane.Message, $"{EtatFormCoureur} un coureur");
                    }
                    catch (InvalidOperationException ioe)
                    {
                        MessageBox.Show(ioe.Message, $"{EtatFormCoureur} un coureur");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, $"{EtatFormCoureur} une course", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("veuillez sélectionner un coureur.");
            }
        }

        private void btnSupprCoureur_Click(object sender, RoutedEventArgs e)
        {
            if (lstCoureurs.SelectedItem != null)
            {
                EtatFormulaire Etat = EtatFormulaire.Supprimer;

                Coureur courseSelectionnee = lstCoureurs.SelectedItem as Coureur;

                FormCoureur frmCoureur = new FormCoureur(courseSelectionnee, Etat);

                if (frmCoureur.ShowDialog() == true)
                {
                    MessageBoxResult result = MessageBox.Show($"Désirez-vous supprimer le coureur {frmCoureur.Coureur.Dossard} - {frmCoureur.Coureur.Nom} ?", $" {Etat} un coureur", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    
                    if (result == MessageBoxResult.Yes)
                    {
                        try
                        {
                            Coureur coureurAsupprimer = frmCoureur.Coureur;
                            Course.SupprimerCoureur(coureurAsupprimer);

                            AfficherCoureurs(Course);
                            MessageBox.Show("Coureur supprimé avec success.");

                        }
                        catch (ArgumentNullException ane)
                        {
                            MessageBox.Show(ane.Message, $"{Etat} un coureur");
                        }
                        catch (InvalidOperationException ioe)
                        {
                            MessageBox.Show(ioe.Message, $"{Etat} un coureur");
                        }
                        catch (ArgumentException ae)
                        {
                            MessageBox.Show(ae.Message, $"{Etat} un coureur");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, $"{Etat} une course", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("veuillez sélectionner un coureur.");
            }
        }
    }
}
