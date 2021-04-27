using Aspose.Cells;
using Aspose.Cells.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Library
{
    class ExcelHelperImage
    {
        Workbook wb;
        Worksheet ws;
        public ExcelHelperImage(string path)
        {
             wb = new Workbook(path);
            ws = wb.Worksheets[0];

        }

        public void addImageWithText(string cellrangeText,string cellRangeImage,string value,string imagepath,int width=140,int height =60,int left =5,int top=15)
        {
            ws.Cells[cellrangeText].Value = value;
            Cell cell = ws.Cells[cellRangeImage];
            int idx = ws.Pictures.Add(cell.Row, cell.Column,imagepath);

            Picture pic = ws.Pictures[idx];
            double w = ws.Cells.GetColumnWidthInch(cell.Column);
            double h = ws.Cells.GetRowHeight(cell.Row);
            pic.Width = width;
            pic.Height = height;
            pic.Left = left;
            pic.Top = top;


        }

        public void addimage( string cellRangeImage, string imagepath, int width = 140, int height = 60, int left = 5, int top = 15)
        {
           
            Cell cell = ws.Cells[cellRangeImage];
            int idx = ws.Pictures.Add(cell.Row, cell.Column, imagepath);

            Picture pic = ws.Pictures[idx];
            double w = ws.Cells.GetColumnWidthInch(cell.Column);
            double h = ws.Cells.GetRowHeight(cell.Row);
            pic.Width = width;
            pic.Height = height;
            pic.Left = left;
            pic.Top = top;


        }
        public void Save(string Savepath)
        {
            wb.Save(Savepath,SaveFormat.Xlsx);
            
        }
    }
}
