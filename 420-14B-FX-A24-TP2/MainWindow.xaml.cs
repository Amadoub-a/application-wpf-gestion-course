
using _420_14B_FX_A24_TP2.classes;
using _420_14B_FX_A24_TP2.enums;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Converters;

namespace _420_14B_FX_A24_TP2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Chemin d'accès au fichier CSV contenant les données.
        /// </summary>
        public const string CHEMIN_FICHIER_COURSES = "C:\\data-420-14B-FX\\TP2\\courses.csv";
        public const string CHEMIN_FICHIER_COUREURS = "C:\\data-420-14B-FX\\TP2\\coureurs.csv";

        /// <summary>
        /// Création d'une instance de la class gestionCourse
        /// </summary>
        GestionCourse _gestionCourse = new GestionCourse(CHEMIN_FICHIER_COURSES, CHEMIN_FICHIER_COUREURS);

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AfficherListeCourses();
        }

        private void AfficherListeCourses()
        {
            lstCourses.Items.Clear();

            foreach(var course in _gestionCourse.Courses)
            {
                lstCourses.Items.Add(course);
            }
        }

        private void btnNouveau_Click(object sender, RoutedEventArgs e)
        {
            FormCourse frmCourse = new FormCourse();

            EtatFormulaire Etat = EtatFormulaire.Ajouter;

            if (frmCourse.ShowDialog() == true)
            {
                try
                {
                     Course nouvelleCourse = frmCourse.Course;
                    _gestionCourse.AjouterCourse(nouvelleCourse);

                    AfficherListeCourses();
                    MessageBox.Show("Course ajouté avec succèss.");
                    _gestionCourse.EnregistrerCourses(CHEMIN_FICHIER_COURSES,CHEMIN_FICHIER_COUREURS);
                }
                catch (ArgumentNullException ane)
                {
                    MessageBox.Show(ane.Message, $"{Etat} une course");
                }
                catch (InvalidOperationException ioe)
                {
                    MessageBox.Show(ioe.Message, $"{Etat} une course");
                }
                catch (ArgumentOutOfRangeException aore)
                {
                    MessageBox.Show(aore.Message, $"{Etat} une course");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, $"{Etat} une course", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            if(lstCourses.SelectedItem != null)
            {
                EtatFormulaire Etat = EtatFormulaire.Modifier;

                Course courseSelectionnee = lstCourses.SelectedItem as Course;

                FormCourse frmCourse = new FormCourse(Etat, courseSelectionnee);

                if (frmCourse.ShowDialog() == true)
                {
                    try
                    {
                        AfficherListeCourses();
                        MessageBox.Show("Course modifiée avec success.");
                        _gestionCourse.EnregistrerCourses(CHEMIN_FICHIER_COURSES, CHEMIN_FICHIER_COUREURS);
                    }
                    catch (ArgumentException ae)
                    {
                        MessageBox.Show(ae.Message, $"{Etat} une course");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, $"{Etat} une course", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("veuillez sélectionner une course.");
            }
        }

        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (lstCourses.SelectedItem != null)
            {
                EtatFormulaire Etat = EtatFormulaire.Supprimer;

                Course courseSelectionnee = lstCourses.SelectedItem as Course;

                FormCourse frmCourse = new FormCourse(Etat, courseSelectionnee);

                if (frmCourse.ShowDialog() == true)
                {
                    MessageBoxResult result = MessageBox.Show($"Désirez-vous supprimer la course {frmCourse.Course.Nom} ?", $" {Etat} une course", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    
                    if (result == MessageBoxResult.Yes) { 

                        try
                        {
                             Course courseAsupprimer = frmCourse.Course;
                            _gestionCourse.SupprimerCourse(courseAsupprimer);

                            AfficherListeCourses();
                            MessageBox.Show("Course supprimée avec success.");
                            _gestionCourse.EnregistrerCourses(CHEMIN_FICHIER_COURSES, CHEMIN_FICHIER_COUREURS);
                        }
                        catch (ArgumentNullException ane)
                        {
                            MessageBox.Show(ane.Message, $"{Etat} une course");
                        }
                        catch (InvalidOperationException ioe)
                        {
                            MessageBox.Show(ioe.Message, $"{Etat} une course");
                        }
                        catch (ArgumentException ae)
                        {
                            MessageBox.Show(ae.Message, $"{Etat} une course");
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
                MessageBox.Show("veuillez sélectionner une course.");
            }
        }
        
    }
}