using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Product
    {
        public int id { get; set; }
        public string nom { get; set; }
        public string categorie { get; set; }
        public string matiere { get; set; }
        public string Nscolaire { get; set; }
        public float prix { get; set; }
        public int stock { get; set; }
        public float valeur { get; set; }

        public Product(int _id,string _nom, string _categorie, string _matiere, string _Nscolaire,float _prix,int _stock,float _valeur)
        {
            id = _id;
            nom = _nom;
            categorie = _categorie;
            matiere = _matiere;
            Nscolaire = _Nscolaire;
            prix = _prix;
            stock = _stock;
            valeur = _valeur;
        }

    }

   

    public class ProductRoot
    {
        public List<Product> productList { get; set; }
        public string lastUpdateDate { get; set; }

      

    }


}
