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
        private ResultSummary m_result_summary;
        private Excel.ApplicationClass m_excelApp;
        private Excel.Workbook m_objBook;

        public ExcelParser( String filename, ResultSummary summary)
        {
            object missing = System.Reflection.Missing.Value;
            m_result_summary = summary;
            m_excelApp = new Excel.ApplicationClass();
            m_excelApp.UserControl = true;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            m_objBook = m_excelApp.Workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
     
            Excel.Worksheet general_obj_sheet = (Excel.Worksheet)m_objBook.Worksheets.get_Item(1);           
            Excel.Worksheet sol_range_in_gen = (Excel.Worksheet)m_objBook.Worksheets.get_Item(2);
            
         
            loadGeneralData(general_obj_sheet);
            loadSolutionsRanges(sol_range_in_gen);

            /*
            m_objSheet.get_Range("A1", "A1").Font.Bold = true;
            m_objSheet.get_Range("A1", "A6").EntireColumn.AutoFit();
            m_objSheet.get_Range("A1", "A7").BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium,
                            Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
	        */
     
            m_objBook.SaveCopyAs(filename);
           // m_excelApp.SaveWorkspace(filename);
        
        }

        private void loadGeneralData(Excel.Worksheet sheet)
        {
            sheet.Cells[1, 1] = "Problem Title:";
            sheet.Cells[1, 2] = m_result_summary.Title;
            sheet.Cells[2, 1] = "First Population:";
            sheet.Cells[2, 2] = m_result_summary.GeneratePopulationType;
            sheet.Cells[3, 1] = "Crossover: ";
            sheet.Cells[3, 2] = m_result_summary.CrossoverType;
            sheet.Cells[4, 1] = "Selection: ";
            sheet.Cells[4, 2] = m_result_summary.SelectionType;
            sheet.Cells[5, 1] = "Iteration: ";
            sheet.Cells[5, 2] = m_result_summary.NumberOfIterations.ToString();
            sheet.Cells[6, 1] = "Start Time: ";
            sheet.Cells[6, 2] = m_result_summary.StartTime;
            sheet.Cells[7, 1] = "Finish Time: ";
            sheet.Cells[7, 2] = m_result_summary.FinishTime;
            sheet.Cells[8, 1] = "Result: ";
            sheet.Cells[8, 2] = m_result_summary.BestResult;
        }

        private void loadSolutionsRanges(Excel.Worksheet sheet)
        {
            sheet.Cells[1, 1] = "generation";
            sheet.Cells[1, 2] = "Best Solution";
            sheet.Cells[1, 3] = "worst Solution";
            for (int i = 0; i < m_result_summary.MinMaxPerGeneration.Count; i++)
            {
                sheet.Cells[i + 2, 1] = i + 1;
                sheet.Cells[i + 2, 2] = m_result_summary.MinMaxPerGeneration[i].Key;
                sheet.Cells[i + 2, 3] = m_result_summary.MinMaxPerGeneration[i].Key;
            }
        }
    }
}
