using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using _excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using System.Drawing.Printing;
using GemBox.Spreadsheet;

namespace Library
{
    class ExcelHelper
    {
        string path = "";
        public string savePath;
        _Application Excel = new _excel.Application();
        Workbook wb;
        Worksheet ws;
        bool Temporary = false;
        public ExcelHelper(string path, int sheet)
        {

            this.path = path;
            savePath = path;
            wb = Excel.Workbooks.Open(path);

            ws = wb.Worksheets[sheet];
            ws.Activate();

        }
        public ExcelHelper(string path)
        {
            this.path = path;
            wb = Excel.Workbooks.Open(path);
        }

        public void deleteWorkSheet(int x)
        {
            Worksheet ws2 = wb.Worksheets[x];
            ws2.Cells.ClearContents();
        }
        public string readcell(int i, int j)
        {

            i++;
            j++;
            return ws.Cells[i, j].Value2;
        }
        public void writecell(int i, int j, string value)
        {

            j++;
            ws.Cells[i, j].Value2 = value;
        }
        public void writecell(string range, string value, bool append = false, bool number = false)
        {
            if (append)
            {
                string x = ws.Range[range].Value;
                ws.Range[range].Value = x + " " + value;
            }
            else
            {
                ws.Range[range].Value = value;
            }
            if (number)
            {
                ws.Range[range].NumberFormat = "#.##";
            }



        }

        public void insertLine(int position, bool merge = false, string cell1 = null, string cell2 = null, bool clearFormat = true)
        {

            Range line = (Range)ws.Rows[position];
            line.Insert();
            if (clearFormat == true)
            {
                line.Cells.ClearFormats();
            }




        }
        public void merge(string cell1, string cell2)
        {
            ws.get_Range(cell1, cell2).Merge();
        }


        public void writecell(int i, int j, string value, int x, int y, bool date, bool border = false, bool number = false, bool border2 = true)
        {

            i += x;
            j += y;
            CultureInfo ci = CultureInfo.InstalledUICulture;
            if (date)
            {
                ws.Cells[i, j].Value2 = value;

                if (border2)
                {
                    ws.Cells[i, j].Borders.LineStyle = _excel.XlLineStyle.xlContinuous;

                }

                if (border)
                {
                    ws.Cells[i, j + 1].Borders.LineStyle = _excel.XlLineStyle.xlContinuous;
                    ws.Cells[i, j + 2].Borders.LineStyle = _excel.XlLineStyle.xlContinuous;
                }


                //if (ci.Name.ToString().Contains("fr"))
                //{
                //    ws.Cells[i, j].NumberFormat = "MM/jj/aaaa";

                //}
                //else
                //{
                //    ws.Cells[i, j].NumberFormat = "MM/DD/YYYY";
                //}
            }
            else
            {
                ws.Cells[i, j].Value = value;
                if (border2)
                {
                    ws.Cells[i, j].Borders.LineStyle = _excel.XlLineStyle.xlContinuous;

                }


                if (border)
                {
                    ws.Cells[i, j + 1].Borders.LineStyle = _excel.XlLineStyle.xlContinuous;
                    ws.Cells[i, j + 2].Borders.LineStyle = _excel.XlLineStyle.xlContinuous;
                }

                if (number)
                {
                    ws.Cells[i, j].NumberFormat = "#,##0.00";
                }


            }


        }
        public void writecell2(int i, int j, string value, int x, int y, bool number)
        {

            i += x;
            j += y;

            ws.Cells[i, j].Value2 = value;



            if (number)
            {
                ws.Cells[i, j].NumberFormat = "#,##0.00";
            }





        }

        
        


        public void SaveAndClose(bool print = false, bool dialogue = false)
        {
            if (print)
            {
                if (dialogue)
                {
                  

                    PrintDialog dialog = new PrintDialog();
                    dialog.UseEXDialog = true;

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        ws.PrintOut(Type.Missing, Type.Missing, Type.Missing, Type.Missing, dialog.PrinterSettings.PrinterName, Type.Missing, Type.Missing, Type.Missing);
                    }


                }
                else
                {
                    
                    Print();
                   
                }


            }
            wb.Save();
            Close();

            if(!print && !Temporary)
            {
                if (Properties.Settings.Default.open_Files)
                {
                    Program.openProcess(savePath);
                }
            }



        }
        void Close()
        {
            wb.Close();
        }
        void Print()
        {
            ws.PrintOut();
        }
        public static ExcelHelper Export(string ModelPath, string name, bool temp = false)
        {
            string From = ModelPath;
            string to = "";

            if (temp)
            {
                to = Path.GetTempPath() + name + ".xlsx";
                
            }
            else
            {
                SaveFileDialog sv = new SaveFileDialog();
                sv.Filter = " Excel File|.xlsx";
                sv.FileName = name + " " + DateTime.Now.Year.ToString();
                if (sv.ShowDialog() == DialogResult.OK)
                {
                    to = sv.FileName;
                }

            }



            if (to != "")
            {
                string x = Path.GetFileNameWithoutExtension(to);
                /* if (Program.checkifprocessalreadyopned(x))
                 {

                     MessageBox.Show("Fichier Deja ouvert \n \n Fermer le fichier S'il vous plait");
                     return null;
                 }*/
                Thread.Sleep(300);

                bool isrunning = Program.IsFileLocked(to);
                if (isrunning)
                {
                    MessageBox.Show("Fermer le fichier S.V.P");
                    return null;
                }
           
               Thread.Sleep(500);
                if (File.Exists(to))
                {
                    File.Delete(to);
                }
                File.Copy(From, to);
                ExcelHelper Ex = new ExcelHelper(to, 1);
                Ex.savePath = to;
                Ex.Temporary = temp;
                return Ex;
            }
            else
            {
                return null;
            }


        }

    }
}
