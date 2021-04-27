using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace Library.Gestion_de_marche
{
    public partial class GM_Start : UserControl
    {
        public Gmarche marcheBahij;
        public Gmarche marchePerle;
        public GM_Start()
        {
            InitializeComponent();
        }
       
        public  void copyFileToDocument(string path,string name)
        {
            
            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Dossier GM\\"+path)))
            {
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Dossier GM\\" + path));
            }

            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)+("/Dossier GM/" +path+ name));
            if (File.Exists(fileName))
            {
                return;
            }

            File.Copy("Gestion marche models/" + path + name, fileName);
        }
        private void gunaAdvenceButton2_Click(object sender, EventArgs e)
        {   string toPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Dossier GM";
            string fromPath = "Gestion marche models";
            Program.Copy(fromPath, toPath);
            //copyFileToDocument("Dossier Administratif et technique/Dossier Administratif/", "Caution.docx");
            //copyFileToDocument("Dossier Administratif et technique/Dossier Administratif/", "DECLARATION SUR L'HONNEUR.docx");
            //copyFileToDocument("Dossier Administratif et technique/Dossier Administratif/", "Papier sur envolope Dossier Administratif.docx");
            //copyFileToDocument("Dossier Administratif et technique/Dossier technique/", "Moyens Humaine et Technique.docx");
            //copyFileToDocument("Dossier Administratif et technique/Dossier technique/", "Papier sur envolope Dossier Technique.docx");
            //copyFileToDocument("Dossier Administratif et technique/Dossier technique/", "touts Attestations de référence.docx");
            //copyFileToDocument("Dossier Administratif et technique/", "Papier sur envolope Dossier administratif et Technique.docx");
            //copyFileToDocument("Dossier Financiére/", "ACTE D'ENGAGEMENT.docx");
            //copyFileToDocument("Dossier Financiére/", "Papier sur cps.docx");
            //copyFileToDocument("Dossier Financiére/", "Papier sur envolope Dossier Financiére.docx");
            
            marcheBahij = new Gmarche();
            marchePerle = new Gmarche();
            Program.main.gM_front_page1.loadData();
            Program.main.gM_front_page1.BringToFront();
            Program.main.gM_secondpage_Bahij.loadData();
            Program.main.gM_secondpage_perle.loadData();
            Program.main.gM_third_page1.loadData();
            Program.main.gM_fourth_page1.loadData();
            Program.main.gM_fourth_page1.GMbahij = marcheBahij;
            Program.main.gM_fourth_page1.GMperle = marchePerle;
        }

        private void GM_Start_Load(object sender, EventArgs e)
        {
            Program.centercontrolH(label1);
            Program.centercontrolH(gunaAdvenceButton2);
        }
    }
}
