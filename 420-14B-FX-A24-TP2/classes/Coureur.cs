
using _420_14B_FX_A24_TP2.enums;
using System.CodeDom;
using System.Globalization;

namespace _420_14B_FX_A24_TP2.classes
{
    /// <summary>
    /// Classe représentant un coureur
    /// </summary>
    public class Coureur: IComparable<Coureur>
    {
        /// <summary>
        /// Déclaration des constantes
        /// </summary>
        public const int DOSSARD_VAL_MIN = 1;
        public const int NOM_NB_CARC_MIN = 3;
        public const int PRENOM_NB_CARC_MIN = 3;
        public const int VILLE_NB_CARC_MIN = 4;


        /// <summary>
        /// Numéro du dossard
        /// </summary>
        private ushort _dossard;

        /// <summary>
        /// Nom du coureur
        /// </summary>
        private string _nom;

        /// <summary>
        /// Prénom du coureur
        /// </summary>
        private string _prenom;

     
        /// <summary>
        /// Catégorie d'âge du coureur
        /// </summary>
        private Categorie _categorie;

        /// <summary>
        /// Ville d'origine du courreur
        /// </summary>
        private string _ville;

        /// <summary>
        /// Province d'origine du coureur.
        /// </summary>
        private Province _province;


        /// <summary>
        /// Temps de course
        /// </summary>
        private TimeSpan _temps;

        /// <summary>
        /// Rang du coureur
        /// </summary>
        private ushort _rang;

       
        /// <summary>
        /// Indicateur d'abandon de la course
        /// </summary>
        private bool _abandon;


       

        /// <summary>
        ///Obtien ou modifie le numéro du dossard.
        /// </summary>
        /// <value>Obtien ou modifie la valeur de l'attribut </value>
        /// <exception cref="System.ArgumentOutOfRangeException">Lancée lorsque que le numéro du dossard est inférieur à 1.</exception>
        public ushort Dossard
        {
            get { return _dossard; }
            set 
            {   if (value < DOSSARD_VAL_MIN)
                    throw new ArgumentOutOfRangeException(nameof(Dossard),$"Le dossard doit être une valeur numérique supérieure ou égale à {DOSSARD_VAL_MIN}");
                _dossard = value; 
            }
        }

        /// <summary>
        ///Obtien ou modifie le nom du coureur.
        /// </summary>
        /// <value>Obtien ou modifie la valeur de l'attribut </value>
        /// <exception cref="System.ArgumentNullException">Lancée lorsque que le nom est nul ou n'a aucune valeur.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Lancée lors que le nom a moins de caractères que NOM_NB_CARC_MIN .</exception>
        public string Nom
        {
            get { return _nom; }
            set 
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException(nameof(Nom), "Le nom ne peut pas être vide.");

                if (value.Trim().Length < NOM_NB_CARC_MIN)
                    throw new ArgumentOutOfRangeException($"Le nom doit contenir au moins {NOM_NB_CARC_MIN} caractères.", nameof(Nom));

                _nom = value.Trim(); 
            }
        }


        /// <summary>
        ///Obtien ou modifie le prénom du coureur.
        /// </summary>
        /// <value>Obtien ou modifie la valeur de l'attribut </value>
        /// <exception cref="System.ArgumentNullException">Lancée lorsque que le prénom est nul ou n'a aucune valeur.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Lancée lors que le prénom a moins de caractères que PRENON_NB_CARC_MIN .</exception>

        public string Prenom
        {
            get { return _prenom; }
            set 
            {
                if (string.IsNullOrWhiteSpace(value))
                        throw new ArgumentNullException(nameof(Nom), "Le prénom ne peut pas être vide.");

                if (value.Trim().Length < PRENOM_NB_CARC_MIN)
                    throw new ArgumentOutOfRangeException($"Le prénom doit contenir au moins {PRENOM_NB_CARC_MIN} caractères.", nameof(Prenom));

                _prenom = value.Trim(); 
            }
        }

        /// <summary>
        /// Obtien ou modifie la catégorie du coureur.
        /// </summary>
        /// <value>Obtien ou modifie la valeur de l'attribut </value>
        /// <exception cref="System.ArgumentOutOfRangeException">Lancée lorsque la valeur de la catégorie n'existe pas dans les plages de valeurs possibles.</exception>
        public Categorie Categorie
        {
            get { return _categorie; }
            set 
            {
                if(!Enum.IsDefined(typeof(Categorie), value))
                    throw new ArgumentOutOfRangeException(nameof(Categorie), $"Cette catégorie ne se trouve pas dans la liste des catégories.");
                _categorie = value; 
            }
        }

        /// <summary>
        ///Méthode permettant d'obtient ou modifie la ville d'origine du coureur.
        /// </summary>
        /// <value>Obtient ou modifie la valeur de l'attribut</value>
        /// <exception cref="System.ArgumentNullException">Lancée lorsque que la ville est nul ou n'a aucune valeur.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Lancée lors que la ville a moins de caractères que VILLE_NB_CARC_MIN .</exception>
        public string Ville
        {
            get { return _ville; }
            set 
            {
                if (string.IsNullOrWhiteSpace(value))
                        throw new ArgumentNullException(nameof(Ville), "Le nom de la ville ne peut pas être vide.");

                if (value.Trim().Length < VILLE_NB_CARC_MIN)
                    throw new ArgumentOutOfRangeException($"Le nom de la ville doit contenir au moins {VILLE_NB_CARC_MIN} caractères.", nameof(Ville));
               
                _ville = value.Trim(); 
            }
        }

        /// <summary>
        ///Obtient ou modifie la province d'origine du coureur
        /// </summary>
        /// <value>Obtient ou modifie la valeur de l'attribut </value>
        /// <exception cref="System.ArgumentOutOfRangeException">Lancée lorsque la valeur de la province n'est pas entre PROVINCE_MIN_VAL et PROVINCE_MAX_VAL.</exception>
        public Province Province
        {
            get { return _province; }
            set 
            {
                if (!Enum.IsDefined(typeof(Province), value))
                    throw new ArgumentOutOfRangeException(nameof(Province), "Cette province ne se trouve pas dans la liste des provinces.");
                _province = value; 
            }
        }


        /// <summary>
        ///Obtient ou modifie la temps de course du coureur
        /// </summary>
        /// <value>Obtient ou modifie la valeur de l'attribut :  _temps.</value>
        public TimeSpan Temps
        {
            get { return _temps; }
            set  { _temps = value; }
        }

        /// <summary>
        /// Obtient ou défini le rang du coureur
        /// </summary>
        /// <value>Obtien ou modifie la valeur de l'attribut :  _rang.</value>
        public ushort Rang
        {
            get { return _rang; }
            set { _rang = value; }
        }

        /// <summary>
        ///Obtient ou modifie l'indicateur d'abandon du coureur.
        /// </summary>
        /// <value>Obtient ou modifie la valeur de l'attribut :  _abandon.</value>
        public bool Abandon
        {
            get { return _abandon; }
            set { _abandon = value; }
        }

 

        /// <summary>
        /// Permet de construire un objet Coureur
        /// </summary>
        /// <param name="dossard">No. de dossard du coureur</param>
        /// <param name="nom">Nom du coureur</param>
        /// <param name="prenom">Prénom du coureur</param>
        /// <param name="categorie">Catégorie à laquelle appartient le coureur</param>
        /// <param name="ville">Ville du coureur</param>
        /// <param name="province">Province du coureur</param>
        /// <param name="temps">Temps de course du coureur</param>
        public Coureur(ushort dossard, string nom, string prenom, Categorie categorie, string ville, Province province, TimeSpan temps)
        {
            Dossard = dossard;
            Nom = nom;
            Prenom = prenom;
            Categorie = categorie;
            Ville = ville;
            Province = province;
            Temps = temps;
        }

        /// <summary>
        /// Permet de retourner les paramétres d'un coureur sous forme de chaîne de caractère.
        /// </summary>
        /// <returns>les informations du coureur</returns>
        public override string ToString()                   
        {
            string nomComplet = $"{Nom.ToUpper()}, {Prenom.ToUpper()}";
            nomComplet = string.Format("{0,-30}", nomComplet);

            string temps = Temps > TimeSpan.Zero ? Temps.ToString() : "";
            string rang = (Rang > 0 && Temps > TimeSpan.Zero) ? Rang.ToString() : "";

            return $"{Dossard.ToString().PadRight(10)} {nomComplet} {Categorie.GetDescription().ToString().PadLeft(15)} {temps.PadLeft(15)} {rang.PadLeft(17)}";
        }

        /// <summary>
        /// Permet de trier les coureurs selon leur temps de course
        /// </summary>
        /// <param name="other">Celui à qui on compare le coureur actuel</param>
        public int CompareTo(Coureur? other)
        {
            if(other is null)
                return 1 ;

            // Comparaison par temps si les deux coureurs ont un temps valide et n'ont pas abandonné
            if (Temps.TotalMicroseconds > 0 && !this.Abandon && other.Temps.TotalMicroseconds > 0 && !other.Abandon)
            {
                int tempsComparer = Temps.CompareTo(other.Temps);
                if (tempsComparer != 0)
                {
                    return tempsComparer;
                }
            }
            //Coureur avec un temps valide et n'ayant pas abandonné
            else if (Temps.TotalMicroseconds > 0 && !Abandon)
            {
                return -1;
            }
            else if (other.Temps.TotalMicroseconds > 0 && !other.Abandon)
            {
                 return 1;
            }

            return string.Compare(Nom, other.Nom, CultureInfo.InvariantCulture, CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace);
        }

        /// <summary>
        ///  Permet de comparer deux coureurs.
        /// </summary>
        /// <param name="obj">Celui à qui on compare le coureur actuel</param>
        /// <returns>Un boolean si les deux coureurs sont egaux ou non</returns>
        public override bool Equals(object? obj)
        {
         
            if (obj is null || obj is not Coureur)
                return false;

            Coureur coureur = obj as Coureur;

            if(this.Nom.ToUpper() == coureur.Nom.ToUpper() && this.Prenom.ToUpper() == coureur.Prenom.ToUpper() && this.Ville.ToUpper() == coureur.Ville.ToUpper() && this.Province == coureur.Province)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Permet de comparer deux coureurs.
        /// </summary>
        /// <param name="coureurGauche">Coureur gauche</param>
        /// <param name="coureurDroit">Coureur droit</param>
        /// <returns>Un boolean si les deux coureurs sont egaux ou non</returns>
        public static bool operator ==(Coureur coureurGauche, Coureur coureurDroit)
        {
            if (Object.ReferenceEquals(coureurGauche, coureurDroit))
                return true;

            if ((Object)coureurGauche == null || (Object)coureurDroit == null)
                return false;

            if (coureurGauche.Nom.ToUpper() == coureurDroit.Nom.ToUpper() && coureurGauche.Prenom.ToUpper() == coureurDroit.Prenom.ToUpper() && coureurGauche.Ville.ToUpper() == coureurDroit.Ville.ToUpper() && coureurGauche.Province == coureurDroit.Province)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Permet de comparer deux coureurs.
        /// </summary>
        /// <param name="coureurGauche">Coureur gauche</param>
        /// <param name="coureurDroit">Coureur droit</param>
        /// <returns>Un boolean si les deux coureurs sont differents ou non</returns>
        public static bool operator !=(Coureur coureurGauche, Coureur coureurDroit)
        {
            return !(coureurGauche == coureurDroit);
        }
    }
}
