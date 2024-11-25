
using _420_14B_FX_A24_TP2.enums;
using System.Globalization;
using System.Threading.Channels;
using System.Windows.Controls;
using System.Windows.Input;

namespace _420_14B_FX_A24_TP2.classes
{
    /// <summary>
    /// Classe représentant une course à pied
    /// </summary>
    public class Course : IComparable<Course>
    {
        /// <summary>
        /// Déclaration des constantes
        /// </summary>
        public const int NOM_NB_CAR_MIN = 3;
        public const int VILLE_NB_CAR_MIN = 4;
        public const ushort DISTANCE_VAL_MIN = 1;

        /// <summary>
        /// Identifiant unique de la course
        /// </summary>
        private Guid _id;


        /// <summary>
        /// Nom de la course
        /// </summary>
        private string _nom;

        /// <summary>
        /// Date de la course
        /// </summary>
        private DateOnly _date;

        /// <summary>
        /// Ville où a lieu la course
        /// </summary>
        private string _ville;

        /// <summary>
        /// Province où a lieu la course
        /// </summary>
        private Province _province;

        /// <summary>
        /// Type de course
        /// </summary>
        private TypeCourse _typeCourse;


        /// <summary>
        /// Distance de la course
        /// </summary>
        private ushort _distance;


        /// <summary>
        /// Liste des coureurs 
        /// </summary>
        private List<Coureur> _coureurs;


        /// <summary>
        /// Obtient ou définit l'identifiant unique d'une course
        /// </summary>
        public Guid Id
        {
            get { return _id; }
            set {
                if (value == Guid.Empty)
                    throw new ArgumentException($"L'id ne doit pas etre vide.", nameof(Nom));

                _id = value; 
            }
        }


        /// <summary>
        ///Obtien ou modifie le nom de la course.
        /// </summary>
        /// <value>Obtien ou modifie la valeur de l'attribut :  _nom.</value>
        /// <exception cref="System.ArgumentNullException">Lancée lorsque que le nom est nul ou n'a aucune valeur.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Lancé lors que le nom a moins de NOM_NB_CAR_MIN caractères.</exception>
        public string Nom
        {
            get { return _nom; }

            set 
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException(nameof(Nom), "Le nom de la course ne peut pas être vide.");
                
                if (value.Trim().Length < NOM_NB_CAR_MIN)
                    throw new ArgumentOutOfRangeException($"Le nom de la course doit contenir au moins {NOM_NB_CAR_MIN} caractères.", nameof(Nom));
               
                _nom = value.Trim().ToUpper(); 
            }
        }


        /// <summary>
        ///Obtien ou modifie la date de la course
        /// </summary>
        /// <value>Obtien ou modifie la valeur de l'attribut :  _date.</value>
        public DateOnly Date
        {
            get { return _date; }
            set { _date = value; }
        }

        /// <summary>
        ///Obtien ou modifie la ville où a lieu la course
        /// </summary>
        /// <value>Obtien ou modifie la valeur de l'attribut :  _ville.</value>
        /// <exception cref="System.ArgumentNullException">Lancée lorsque que la ville est nulle ou n'a aucune valeur.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Lancé lors que la ville a moins de VILLE_NB_CAR_MIN caractères.</exception>
        public string Ville
        {
            get { return _ville; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException(nameof(Ville), "Le nom de la ville ne peut pas être vide.");

                if (value.Trim().Length < VILLE_NB_CAR_MIN)
                    throw new ArgumentOutOfRangeException($"Le nom de la ville doit contenir au moins {VILLE_NB_CAR_MIN} caractères.", nameof(Ville));

                _ville = value.Trim();
            }
        }


        /// <summary>
        ///Obtien ou modifie la province où a lieu la course
        /// </summary>
        /// <value>Obtien ou modifie la valeur de l'attribut :  _province.</value>
        /// <exception cref="System.ArgumentOutOfRangeException">Lancée lorsque la valeur de la province n'est pas valide.</exception>
        public Province Province
        {
            get { return _province; }
            set 
            {
                if(!Enum.IsDefined(typeof(Province), value))
                  throw new ArgumentOutOfRangeException(nameof(Province), $"Cette province ne se trouve pas dans la liste des provinces.");

                _province = value; 
            }
        }


        /// <summary>
        ///Obtien ou modifie le type de course.
        /// </summary>
        /// <value>Obtien ou modifie la valeur de l'attribut :  _type.</value>
        /// <exception cref="System.ArgumentOutOfRangeException">Lancée lorsque que le type de course n'est pas valide.</exception>
        public TypeCourse TypeCourse
        {
            get { return _typeCourse; }
            set
            {
                if (!Enum.IsDefined(typeof(TypeCourse), value))
                    throw new ArgumentOutOfRangeException(nameof(TypeCourse), $"Ce type de course ne se trouve pas dans la liste.");

                _typeCourse = value;
            }
        }

        /// <summary>
        ///Obtien ou modifie la distance de la course en km
        /// </summary>
        /// <value>Obtien ou modifie la valeur de l'attribut :  _distance.</value>
        /// <exception cref="System.ArgumentOutOfRangeException">Lancée lorsque que la distance est inférieure à DISTANCE_VAL_MIN.</exception>
        public ushort Distance
        {
            get { return _distance; }
            set 
            {
                if (value < DISTANCE_VAL_MIN)
                    throw new ArgumentOutOfRangeException(nameof(Distance), $"La distance de la course doit être supérieure à {DISTANCE_VAL_MIN}.");

                _distance = value; 
            }
        }

        /// <summary>
        ///Obtien ou modifie la liste des coureurs
        /// </summary>
        /// <value>Obtien ou modifie la valeur de l'attribut :  _coureurs.</value>
        public List<Coureur> Coureurs
        {
            get { return _coureurs; }
            set { _coureurs = value; }
        }

        /// <summary>
        /// Obtien le nombre de coureurs participants à la course
        /// </summary>
        /// <value>Obtien la valeur de l'attribut :  _coureurs.Count.</value>
        public int NbParticipants
        {
            get {
                return Coureurs.Count;
            }
        }

        /// <summary>
        ///Obtien le temps de course moyen
        /// </summary>
        /// <value>Obtien la valeur retourné par la méthode : CalculerTempsCourseMoyen() </value>
        public TimeSpan TempCourseMoyen
        {
            get {
                return CalculerTempsCourseMoyen();
            }
        }

        /// <summary>
        /// Permet de constuire un objet de type Course
        /// </summary>
        /// <param name="nom">Nom de la course</param>
        /// <param name="date">Date à laquelle a lieu la course</param>
        /// <param name="ville">Ville où a lieu la course</param>
        /// <param name="province">Province ou a lieu la course</param>
        /// <param name="typeCourse">Type de course</param>
        /// <param name="distance">Distance de la course</param>
        /// <remarks>Initialise une liste de coureurs vide</remarks>
        public Course(Guid id, string nom, DateOnly date, string ville, Province province, TypeCourse typeCourse, ushort distance)
        {
            Id = id;
            Nom = nom;
            Date = date;
            Ville = ville;
            Province = province;
            TypeCourse = typeCourse;
            Distance = distance;
            Coureurs = new List<Coureur>();
        }

        /// <summary>
        ///  Permet l'ajout d'un coureur à la liste des coureurs.
        /// </summary>
        /// <param name="coureur"></param>
        /// <exception cref="ArgumentNullException"> lancer des exceptions si une valeur ne peut pas etre null</exception>
        /// <exception cref="InvalidOperationException"> lancer des exceptions si l'élément existe déja ou que le numero du dossard est deja utilisé</exception>
        public void AjouterCoureur(Coureur coureur)
        {
            if(coureur == null)
                throw new ArgumentNullException(nameof(coureur), "Le coureur ne peut pas être nul.");
            
             if(ObtenirCoureurParNoDossard(coureur.Dossard) != null)
                throw new InvalidOperationException($"Le numéro de dossard {coureur.Dossard} est déja utilisé par un autre coureur.");

            foreach (Coureur participant in Coureurs)
            {
                if (coureur.Equals(participant))
                    throw new InvalidOperationException("Un coureur avec les memes informations existe déja.");
            }

            Coureurs.Add(coureur);
            Coureurs.Sort();
        }

        /// <summary>
        /// Permet d'obtenir un coureur à partir de son numéro de dossard.
        /// </summary>
        /// <param name="noDossard">Numéro dossard du coureur</param>
        /// <returns> la valeur nulle est retournée si le coureur n'est pas trouvé dans le cas contraire le coureur trouvé est retourné. </returns>
        /// <exception cref="ArgumentOutOfRangeException">Lancée lorsque le numéro du dossard ne respecte pas la valeur demandée</exception>
        public Coureur ObtenirCoureurParNoDossard(ushort noDossard)
        {
            if (noDossard < Coureur.DOSSARD_VAL_MIN)
                throw new ArgumentOutOfRangeException(nameof(noDossard), $"Le numéro du dossard doit etre supérieur ou égal à {Coureur.DOSSARD_VAL_MIN}.");

            foreach(Coureur participant in Coureurs)
            {
                if(participant.Dossard == noDossard)
                {
                    return participant;
                }
            }
            return null;
        }

        /// <summary>
        ///  Permet de retirer un coureur de la liste de coureurs.
        /// </summary>
        /// <param name="coureur">Information du coureur</param>
        /// <exception cref="ArgumentNullException">Lancée si le coureur est null</exception>
        /// <exception cref="InvalidOperationException">Lancée si le coureur n'est pas dans la liste</exception>
        public void SupprimerCoureur(Coureur coureur)
        {
            if (coureur == null)
                throw new ArgumentNullException(nameof(coureur), $"Le coureur ne peut pas etre null.");

            bool succes = Coureurs.Contains(coureur);

            if (!succes)
                throw new InvalidOperationException("Ce coureur n'est pas dans la liste.");

            Coureurs.Remove(coureur);
        }

        /// <summary>
        /// Permet de calculer le temps moyen de la course.
        /// </summary>
        /// <returns>Le tems moyen de la course</returns>
        private TimeSpan CalculerTempsCourseMoyen()
        {
            double tempsTotalEnMilliseconde = 0;
            uint nbParticipantsReels = 0;

            foreach (var coureur in Coureurs)
            {
                if (!coureur.Abandon && coureur.Temps>TimeSpan.Zero)
                {
                    tempsTotalEnMilliseconde += (double)coureur.Temps.TotalMilliseconds;
                    nbParticipantsReels++;
                }
            }

            TimeSpan tempsMoyen = TimeSpan.Zero;

            if (nbParticipantsReels > 0)
            {
                double tempsMoyenEnMilliseconde = tempsTotalEnMilliseconde / (double)nbParticipantsReels;

                tempsMoyen = TimeSpan.FromMilliseconds(tempsMoyenEnMilliseconde);
            }

            return tempsMoyen;
        }

        /// <summary>
        /// Permet de trier la liste des coureurs selon le temps de course en ajustant le rang des coureurs
        /// </summary>
        public void TrierCoureurs()
        {
            Coureurs.Sort();
            ushort rang = 1;
            foreach (var coureur in Coureurs)
            {
                if (!coureur.Abandon && coureur.Temps > TimeSpan.Zero)
                    coureur.Rang = rang++;
            }
           
        }

        /// <summary>
        ///  Retourne la représentation d'une course sous forme de chaîne de caractère de la manière
        /// </summary>
        /// <returns>Les infrmations de la course</returns>
        public override string ToString()
        {
            return $"{Nom.PadRight(38)} {Ville.PadRight(25)} {Province.GetDescription().ToString().PadRight(22)} {Date}";
        }

        /// <summary>
        ///  Permet de comparer deux courses.
        /// </summary>
        /// <param name="obj">La course avec laquelle on compare la course actuelle</param>
        /// <returns>Un boolean si les deux courses sont egales ou non</returns>
        public override bool Equals(object? obj)
        {
            if (obj is null || obj is not Course)
                return false;

            Course course = obj as Course;

            if (this.Nom.ToUpper() == course.Nom.ToUpper() && this.Date == course.Date && this.Ville.ToUpper() == course.Ville.ToUpper() && this.Province == course.Province && this.TypeCourse == course.TypeCourse && this.Distance == course.Distance)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        ///  Permet de comparer deux courses.
        /// </summary>
        /// <param name="courseGauche">Course de gauche</param>
        /// <param name="courseDroite">Course de droite</param>
        /// <returns>Un boolean si les deux courses sont egales ou non</returns>
        public static bool operator ==(Course courseGauche, Course courseDroite)
        {
            if (Object.ReferenceEquals(courseGauche, courseDroite))
                return true;

            if ((Object)courseGauche == null || (Object)courseDroite == null)
                return false;

            if (courseGauche.Nom.ToUpper() == courseDroite.Nom.ToUpper() && courseGauche.Date == courseDroite.Date && courseGauche.Ville.ToUpper() == courseDroite.Ville.ToUpper() && courseGauche.Province == courseDroite.Province && courseGauche.TypeCourse == courseDroite.TypeCourse && courseGauche.Distance == courseDroite.Distance)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        ///  Permet de comparer deux courses.
        /// </summary>
        /// <param name="courseGauche">Course de gauche</param>
        /// <param name="courseDroite">Course de droite</param>
        /// <returns>Un boolean si les deux courses sont egales ou non</returns>
        public static bool operator !=(Course courseGauche, Course courseDroite)
        {
            return !(courseGauche == courseDroite);
        }
       
        /// <summary>
        /// Permet de trier les courses selon la date 
        /// </summary>
        /// <param name="other">La course avec laquelle on compare la course actuelle</param>
        /// <returns>Les noms par odre de comparaison de grandauer</returns>
        public int CompareTo(Course? other)
        {
            if (other is null)
                return 1;

            int dateComparer = Date.CompareTo(other.Date);
            if (dateComparer != 0)
            { 
                return dateComparer * -1;
            }

            return string.Compare(Nom, other.Nom, CultureInfo.InvariantCulture, CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace);
        }
    }
}
