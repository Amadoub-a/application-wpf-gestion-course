using _420_14B_FX_A24_TP2.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Windows.Input;
using System.Net;

namespace _420_14B_FX_A24_TP2.classes
{
    public class GestionCourse
    {
        private List<Course> _courses;

        /// <summary>
        /// Obtien ou modifie la liste des courses
        /// </summary>
		public List<Course> Courses 
		{
			get { 
                return _courses; 
            }
			set { 
                _courses = value; 
            }
		}

        /// <summary>
        /// Constructeur qui charge la course 
        /// </summary>
        /// <param name="cheminFichierCourses">Chemin d'acces du fichier des courses</param>
        /// <param name="cheminFichierCoureurs">Chemin d'acces du fichier des coureurs</param>
        public GestionCourse(string cheminFichierCourses, string cheminFichierCoureurs)
        {
            ChargerCourse(cheminFichierCourses, cheminFichierCoureurs);
        }

        /// <summary>
        /// Permet de charger les données de la course et ses coureurs
        /// </summary>
        /// <param name="cheminFichierCourses">Chemin vers le fichier de la liste des courses</param>
        /// <param name="cheminFichierCoureurs">Chemin vers le fichier de la liste des coureurs</param>
        /// <exception cref="ArgumentException">Lancée lorsque le chemin du fichier est null ou vide</exception>
        private void ChargerCourse(string cheminFichierCourses, string cheminFichierCoureurs)
        {
            if (string.IsNullOrWhiteSpace(cheminFichierCourses))
                throw new ArgumentException("Le nom du fichier ne peut pas être vide ou ne contenir que des espaces.", nameof(cheminFichierCourses));

            if (string.IsNullOrWhiteSpace(cheminFichierCoureurs))
                throw new ArgumentException("Le nom du fichier ne peut pas être vide ou ne contenir que des espaces.", nameof(cheminFichierCoureurs));

            string[] vectLigneCourse = Utilitaire.ChargerDonnees(cheminFichierCourses);
            
            List<Course> CoursesChargees = new List<Course>();

            for (int i = 0; i < vectLigneCourse.Length; i++)
            {
                //sauter la premiere ligne car elle contient les titres
                if (i > 0)
                {
                    //Décomposition de la ligne actuelle en plusieurs champs.
                    string[] vectChamps = vectLigneCourse[i].Split(';');

                    //Extraction des différents champs.
                    Guid id = Guid.Parse(vectChamps[0]);
                    string nom = vectChamps[1].Trim();
                    string ville = vectChamps[2].Trim();
                    Province province = (Province)Enum.Parse(typeof(Province), vectChamps[3]);
                    DateOnly date = DateOnly.Parse(vectChamps[4]);
                    TypeCourse typeCourse = (TypeCourse)Enum.Parse(typeof(TypeCourse), vectChamps[5]);
                    ushort distance = ushort.Parse(vectChamps[6]);

                    Course course = new Course(id,nom,date,ville,province,typeCourse,distance);
                    ChargerCoureurs(course, cheminFichierCoureurs);
                    CoursesChargees.Add(course);
                }
            }

            CoursesChargees.Sort();
            Courses = CoursesChargees;
        }

        /// <summary>
        /// Permet de charger les coureurs dans la course.
        /// </summary>
        /// <param name="course">La course à laquelle sont liés les coureurs</param>
        /// <param name="cheminFichierCoureurs">Chemin d'acces du fichier des coureurs</param>
        /// <exception cref="ArgumentNullException">Lancer une exception lorsque la course est nulle</exception>
        private void ChargerCoureurs(Course course, string cheminFichierCoureurs)
        {
            if (course == null)
                throw new ArgumentNullException(nameof(course), "La couse ne peut pas être null");

            if (string.IsNullOrWhiteSpace(cheminFichierCoureurs))
                throw new ArgumentNullException("Le nom du fichier ne peut pas être vide ou ne contenir que des espaces.", nameof(cheminFichierCoureurs));

            string[] vectLignes = Utilitaire.ChargerDonnees(cheminFichierCoureurs);

            for (int i = 0; i < vectLignes.Length; i++)
            {
                if(i > 0)
                {
                    string[] vectChamps = vectLignes[i].Split(';');

                    Guid idCourse = Guid.Parse(vectChamps[0]);
                    ushort dossard = ushort.Parse(vectChamps[1]);
                    string nom = vectChamps[2].Trim();
                    string prenom = vectChamps[3].Trim();
                    string ville = vectChamps[4].Trim();
                    Province province = (Province)Enum.Parse(typeof(Province), vectChamps[5]);
                    Categorie categorie = (Categorie)Enum.Parse(typeof(Categorie), vectChamps[6]);
                    TimeSpan temps = TimeSpan.Parse(vectChamps[7]);
                    bool abandon = bool.Parse(vectChamps[8]);
                   
                    Coureur coureur = new Coureur(dossard, nom, prenom, categorie, ville, province, temps);
                    coureur.Abandon = abandon;

                    if (course.Id == idCourse)
                    {
                        course.Coureurs.Add(coureur);
                    }
                }

            }
        }

        /// <summary>
        /// Permet l’ajout de la course à la liste
        /// </summary>
        /// <param name="course">La course à ajouter</param>
        /// <exception cref="ArgumentNullException">Lancée lorsque la course est nulle</exception>
        /// <exception cref="InvalidOperationException"> lancée si l'élément existe déja dans la liste</exception>
        public void AjouterCourse(Course course)
        {
            if (course == null)
                throw new ArgumentNullException(nameof(course), "La course ne peut pas être nul.");

            foreach (var UneCourse in Courses) 
            {
                if (course.Equals(UneCourse))
                    throw new InvalidOperationException("Une course avec les mêmes informations existe déja.");
            }

            Courses.Add(course);
            Courses.Sort();
        }

        /// <summary>
        ///  Permet de retirer une course de la liste des courses.
        /// </summary>
        /// <param name="course">La course à ajouter</param>
        /// <returns>Un booléen pour valider la suppression</returns>
        /// <exception cref="ArgumentNullException"> lancée lorsque la course est nulle</exception>
        /// <exception cref="InvalidOperationException"> lancée si l'élément existe déja</exception>
        public bool SupprimerCourse(Course course)
        {
            if (course == null)
                throw new ArgumentNullException(nameof(course), $"La course ne peut pas etre null.");

            bool succes = Courses.Contains(course);
            if (!succes)
                throw new InvalidOperationException("Cette course n'est pas dans la liste.");

            bool supprimer = Courses.Remove(course);

            return supprimer;
        }

        /// <summary>
        ///  Permet l'enregistrement des données de la course et les données des coureurs dans les fichiers correspondants
        /// </summary>
        /// <param name="cheminFichierCourses">Chemin d'acces vers le fichier des courses</param>
        /// <param name="cheminFichierCoureurs">Chemin d'acces vers le fichier des coureurs</param>
        /// <exception cref="ArgumentNullException">Lancée lorsaue le chemin du fichier est null</exception>
        public void EnregistrerCourses(string cheminFichierCourses, string cheminFichierCoureurs)
        {
            if (string.IsNullOrWhiteSpace(cheminFichierCourses))
                throw new ArgumentNullException("Le nom du fichier ne peut pas être vide ou ne contenir que des espaces.", nameof(cheminFichierCourses));

            if (string.IsNullOrWhiteSpace(cheminFichierCoureurs))
                throw new ArgumentNullException("Le nom du fichier ne peut pas être vide ou ne contenir que des espaces.", nameof(cheminFichierCoureurs));

            //Implémentation de l'affichage de la prémière ligne du fichier
            string donneesCourses = "Id;Nom;Ville;Province;Date;Type;Distance\n";
            string donneesCoureurs = "IdCourse;Dossard;Nom;Prenom;Ville;Province;Categorie;Temps;Abandon\n";

            //Boucle permettant d'enregistrer les données dans le fichier 
            foreach (var course in Courses)
            {
                donneesCourses += course.Id + ";" + course.Nom + ";" + course.Ville + ";" + course.Province + ";" + course.Date + ";" + course.TypeCourse + ";" + course.Distance + "\n";

                foreach (var coureur in course.Coureurs)
                {
                    donneesCoureurs += course.Id + ";" + coureur.Dossard + ";" + coureur.Nom + ";" + coureur.Prenom + ";" + coureur.Ville + ";" + coureur.Province + ";" + coureur.Categorie + ";" + coureur.Temps + ";" + coureur.Abandon + "\n";
                }
            }

            Utilitaire.EnregistrerDonnees(cheminFichierCourses, donneesCourses);
            Utilitaire.EnregistrerDonnees(cheminFichierCoureurs, donneesCoureurs);
        }
    }
}
