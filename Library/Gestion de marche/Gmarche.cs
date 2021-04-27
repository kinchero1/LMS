using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Gestion_de_marche
{
    public class Gmarche
    {
        public string nomMarche;
        public string Nmarche;
        public string fromTime;
        public string fromDate;
        public string objet;
        public string province;
        public string Client;
        public string Adresse;
        public string sign = "ABDELKADER BAHIJ - GERANT";
        public string Ntel = "06 61 93 83 80";
        public string Nfax = "05 23 47 20 00";
        public string Email = "bahij.alfa16@gmail.com";
        public string nomCompte = "STE BAHIJ ALFA - SARL";
        public string montantCapital = "100 000,00 DH";
        public string adresseSiegeSocial = "N° 228 1ERE ETAGE BLOC 2 HAY HASSANIA SOUK SEBT FKIH BEN SALAH";
        public string adresseDomicile = "N° 228 1ERE ETAGE BLOC 2 HAY HASSANIA SOUK SEBT FKIH BEN SALAH";
        public string nCNSS = "5507556";
        public string registreCommerceLoc = "SOUK SEBT";
        public string registreCommerceN = "3667";
        public string Npattente = "41105497";
        public string RIB = "145 095 212 110 989 665 000 852";
        public string idFiscal = "20772235";
        public float montantHT;
        public float montantTVA;
        public float montantTTC;
        public string doneLocation = "Souk Sebt";
        public string doneDate = "06/11/2020";
        public string pdpath=null;

        public Gmarche()
        {

        }
        public void SetHeader(string nm, string fd,string ft, string ob, string pro,string nomM,string CL,string Ads)
        {
            Nmarche = nm;
            fromDate = fd;
            fromTime = ft;
            objet = ob;
            province = pro;
            nomMarche = nomM;
            Client = CL;
            Adresse = Ads;

        }
        public void setSecondPage(string sn, string nt, string nf,string Em, string nomC, string mC, string AdresseSS, string AdresseD, string Ncn, string registreCL, string registerCN, string Npt, string rib)
        {
            sign = sn;
            Ntel = nt;
            Nfax = nf;
            Email = Em;
            nomCompte = nomC;
            montantCapital = mC;
            adresseSiegeSocial = AdresseSS;
            adresseDomicile = AdresseD;
            nCNSS = Ncn;
            registreCommerceLoc = registreCL;
            registreCommerceN = registerCN;
            Npattente = Npt;
            RIB = rib;
        }
        public void setThirdPage(string idF,float mHT,float mTVA,float mTTC,string doneL,string doneD)
        {
            idFiscal = idF;
            montantHT = mHT;
            montantTVA = mTVA;
            montantTTC = mTTC;
            doneLocation = doneL;
            doneDate = doneD;


        }

    }
}
