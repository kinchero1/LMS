using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;
using System.IO;


namespace Library
{
    class wordHelper
    {
        Word.Application wordApp = new Word.Application();
        Word.Document myWordDoc = null;
        public void FindAndReplace(Word.Application wordApp, object ToFindText, object replaceWithText)
        {
            

            object matchCase = true;
            object matchWholeWord = true;
            object matchWildCards = false;
            object matchSoundLike = false;
            object nmatchAllforms = false;
            object forward = true;
            object format = false;
            object matchKashida = false;
            object matchDiactitics = false;
            object matchAlefHamza = false;
            object matchControl = false;
            object read_only = false;
            object visible = true;
            object replace = 2;
            object wrap = 1;

            wordApp.Selection.Find.Execute(ref ToFindText,
                ref matchCase, ref matchWholeWord,
                ref matchWildCards, ref matchSoundLike,
                ref nmatchAllforms, ref forward,
                ref wrap, ref format, ref replaceWithText,
                ref replace, ref matchKashida,
                ref matchDiactitics, ref matchAlefHamza,
                ref matchControl);
        }

        
        //Creeate the Doc Method
        public void CreateWordDocument(object filename, string[] tofind,string[] toreplace,bool print,int numberofcpy/*,IProgress<int> progress*/)
        {
            string letters = "A.B.C.D.E.F.G.H.I.J.K.L.M.N.OP.U.V.W.Q.R.S.T.Y.X.Z";
            string[] letterArray = letters.Split('.');
            
            /*string file=  Path.GetFileName(filename.ToString());
            var myfile = File.Create(SaveAs.ToString());
             myfile.Close();*/
            
            object missing = Missing.Value;

            if (File.Exists((string)filename))
            {
                object readOnly = false;
                object isVisible = false;
                wordApp.Visible = false;

                myWordDoc = wordApp.Documents.Open(ref filename, ref missing, ref readOnly,
                                        ref missing, ref missing, ref missing,
                                        ref missing, ref missing, ref missing,
                                        ref missing, ref missing, ref missing,
                                        ref missing, ref missing, ref missing, ref missing);
                myWordDoc.Activate();
                var Shapes = myWordDoc.Shapes;
                if (tofind != null && toreplace !=null)
                {
                    
                    //find and replace
                    for (int i = 0; i < tofind.Length; i++)
                    {
                        if (toreplace[i].Length >= 250)
                        {
                            int x = toreplace[i].Length / 9;

                            this.FindAndReplace(wordApp, tofind[i], "<A><B><C><D><E><F><G><H><I>");
                            this.FindAndReplace(wordApp, "<A>", toreplace[i].Substring(0,x));
                            this.FindAndReplace(wordApp, "<B>", toreplace[i].Substring(x, x));
                            this.FindAndReplace(wordApp, "<C>", toreplace[i].Substring(x*2, x));
                            this.FindAndReplace(wordApp, "<D>", toreplace[i].Substring(x*3, x));
                            this.FindAndReplace(wordApp, "<E>", toreplace[i].Substring(0*4, x));
                            this.FindAndReplace(wordApp, "<G>", toreplace[i].Substring(x*5, x));
                            this.FindAndReplace(wordApp, "<H>", toreplace[i].Substring(x * 6, x));
                            this.FindAndReplace(wordApp, "<I>", toreplace[i].Substring(x * 7, 0));

                        }
                        else
                        {
                            this.FindAndReplace(wordApp, tofind[i], toreplace[i]);
                        }
                        
                        foreach (Shape shape in Shapes)
                        {
                            var initialText = shape.TextFrame.TextRange.Text;
                            var resultingText = initialText.Replace(tofind[i], toreplace[i]);
                            
                            shape.TextFrame.TextRange.Text = resultingText;
                        }
                        // progress.Report(i * 100 / x);

                    }
                }

                

               


            }

            else
            {

            }

            //Save as
           /* myWordDoc.SaveAs2(ref SaveAs, ref missing, ref missing, ref missing,
                            ref missing, ref missing, ref missing,
                            ref missing, ref missing, ref missing,
                            ref missing, ref missing, ref missing,
                            ref missing, ref missing, ref missing);*/
            if (print)
            {
                for (int i = 0; i < numberofcpy; i++)
                {
                    myWordDoc.PrintOut();
                }
               
            }
            myWordDoc.Save();
            myWordDoc.Close();
            wordApp.Quit();
            
          
        }
        public void save()
        {
            myWordDoc.Save();
            myWordDoc.Close();
            wordApp.Quit();

        }



    }
}
