using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Runtime.InteropServices;

using MRCPSP.Domain;

namespace MRCPSP.Gui.StatisticsViewer
{
    class ExcelParser
    {

        private string m_filename;
        private ResultSummary m_result_summary;
        private Excel.ApplicationClass m_excelApp;
        private Excel.Workbook m_objBook;
        private Excel.Worksheet m_objSheet;

        public ExcelParser(string filename, ResultSummary summary)
        {
            object missing = System.Reflection.Missing.Value;
            m_filename = filename;
            m_result_summary = summary;
            m_excelApp = new Excel.ApplicationClass();
            m_excelApp.UserControl = true;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            m_objBook = m_excelApp.Workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            m_objSheet = (Excel.Worksheet)m_objBook.Sheets["Sheet1"];
            m_objSheet.Name = "General";
            
            m_objSheet.Cells[1, 1] = "Details";
            m_objSheet.Cells[2,2] = global::MRCPSP.Properties.Resources.excel_icon;
            /*
            m_objSheet.get_Range("A1", "A1").Font.Bold = true;
            m_objSheet.get_Range("A1", "A6").EntireColumn.AutoFit();
            m_objSheet.get_Range("A1", "A7").BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium,
                            Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
	        */
            
            m_excelApp.SaveWorkspace(m_filename);
        
        }
    }
}
