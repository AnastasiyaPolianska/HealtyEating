using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Healthy_Eating.Classes
{
    public    enum ENSex { Male, Female};
    public    enum ENCountry { Ukraine, Russia, Albania, Andorra, Armenia, Azerbaijan, Austria, Belarus, Belgium, BosniaAndHerzegovina, Bulgaria, Croatia, Cyprus, CzechRepublic, Denmark, Estonia, Finland, France, Georgia, Germany, Greece, Hungary, Iceland, Ireland, Italy, Latvia, Liechtenstein, Lithuania, Luxembourg, Macedonia, Malta, Moldova, Montenegro, Netherlands, Norway, Poland, Portugal, Romania, Serbia, Slovakia, Slovenia, Spain, Sweden, Switzerland, Turkey, UK };
    public    enum ENDriverLicence { A1, A, B1, B, C1, C, D1, D, BE, CE, C1E, D1E, DE, T};
    public    enum ENChildDiseases { Measles, ChickenPox, WhoopingCough, Diphtheria, ScarletFever, Mumps, Polio, Pneumococcal, Haemophilus};
    public    enum ENChronicDiseases { Atherosclerosis, CardiacIschemia, ChronicMyocarditis, Cardiomyopathy,
        Herpes, Cytomegalovirus, HPV,
        COPD, ChronicLungAbscess, ChronicalBronchitis, BronchialAsthma,
        Pyelonephritis, ChronicCystitis, StonesInTheKidneys,
        ChronicGastritis, ChronicPancreatitis, ChronicColitis,
        Urethritis, Prostatitis, Orchitis, Epididymitis, Adnexitis
    };

    public class User
    {
        [PrimaryKey]
        public string Name { get; set; }
        public DateTime Age { get; set; }
        public ENSex Sex { get; set; }
        public ENCountry Country { get; set; }
        public bool DrivingLicence { get; set; }
        public bool ChildDiseases { get; set; }
        public bool ChronicDiseases { get; set; }

        [TextBlob("ParametersString")]
        public  List<ParametresOfUser> Parameters { get; set; }
        public string ParametersString { get; set; }

        [TextBlob("MenuString")]
        public List<Product> Products { get; set; }
        public string MenuString { get; set; }

        [TextBlob("AlcoholString")]
        public List<Alcohol> Alcohols { get; set; }
        public string AlcoholString { get; set; }

        [TextBlob("CigaretteString")]
        public List<Cigarette> Cigarettes { get; set; }
        public string CigaretteString { get; set; }

        [TextBlob("LicenceString")]
        public List<ENDriverLicence> DriverLicence { get; set; }
        public string LicenceString { get; set; }

        [TextBlob("DiseasesString")]
        public List<ENChildDiseases> ChildrenDiseasesList { get; set; }
        public string DiseasesString { get; set; }

        [TextBlob("ChronicDiseasesString")]
        public List<ENChronicDiseases> ChronicDiseasesList { get; set; }
        public string ChronicDiseasesString { get; set; }

        [Ignore]
        public static int CurrentUser { get; set; }

        /*Constructors*/
        public User()
        {
            Parameters = new List<ParametresOfUser>();
            Products = new List<Product>();
            Alcohols = new List<Alcohol>();
            Cigarettes = new List<Cigarette>();
            DriverLicence = new List<ENDriverLicence>();
            ChildrenDiseasesList = new List<ENChildDiseases>();
            ChronicDiseasesList = new List<ENChronicDiseases>();
        }

        public User(string incomeName, DateTime incomeAge, ENSex incomeSex, ENCountry incomeCountry, bool drivingLicence, List<ENDriverLicence> driverLicences, bool childDiseases, List<ENChildDiseases> childrenDiseasesList, bool chronicDiseases, List<ENChronicDiseases> chronicDiseasesList)
        {
            Name = incomeName;
            Age = incomeAge;
            Sex = incomeSex;
            Country = incomeCountry;
            DrivingLicence = drivingLicence;
            ChildDiseases = childDiseases;
            ChronicDiseases = chronicDiseases;

            Parameters = new List<ParametresOfUser>();
            Products = new List<Product>();
            Alcohols = new List<Alcohol>();
            Cigarettes = new List<Cigarette>();

            DriverLicence = new List<ENDriverLicence>();
            DriverLicence = driverLicences;
            ChildrenDiseasesList = new List<ENChildDiseases>();
            ChildrenDiseasesList = childrenDiseasesList;
            ChronicDiseasesList = new List<ENChronicDiseases> ();
            ChronicDiseasesList = chronicDiseasesList;
        }


        /*Functions*/
        public override string ToString()
        {
            String forOutputLicences = " ";
            foreach (ENDriverLicence Temp in DriverLicence)
            { forOutputLicences += Temp.ToString() + " "; }

            if (forOutputLicences == " ") forOutputLicences = " NONE";

            String forOutputDiseases = " ";
            foreach (ENChildDiseases Temp in ChildrenDiseasesList)
            { forOutputDiseases += Temp.ToString() + " "; }

            if (forOutputDiseases == " ") forOutputDiseases = " NONE";

            String forOutputChronicDiseases = " ";
            foreach (ENChronicDiseases Temp in ChronicDiseasesList)
            { forOutputChronicDiseases += Temp.ToString() + " "; }

            if (forOutputChronicDiseases == " ") forOutputChronicDiseases = " NONE";

            return String.Format("Name: {0} Age: {1} Sex: {2} Country: {3} \nLisence: {4} Types:{5} \nChild diseases: {6} Types: {7} \nChronic diseases: {8} Types: {9}", Name, Age.ToShortDateString(), Sex, Country, DrivingLicence.ToString(), forOutputLicences, ChildDiseases.ToString(), forOutputDiseases, ChronicDiseases.ToString(), forOutputChronicDiseases);
        }
    }
}