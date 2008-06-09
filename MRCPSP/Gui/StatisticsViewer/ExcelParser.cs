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
        private Excel.Worksheet m_sheet;
        private int m_last_row_used;

        public ExcelParser( String filename, ResultSummary summary)
        {
            object missing = System.Reflection.Missing.Value;
            m_result_summary = summary;
            m_excelApp = new Excel.ApplicationClass();
            m_excelApp.UserControl = true;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            m_objBook = m_excelApp.Workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
     
            m_sheet = (Excel.Worksheet)m_objBook.Worksheets.get_Item(1);
            m_last_row_used = 1;        
            loadGeneralData();
            loadSolutionsRanges();

            /*
            m_objSheet.get_Range("A1", "A1").Font.Bold = true;
            m_objSheet.get_Range("A1", "A6").EntireColumn.AutoFit();
            m_objSheet.get_Range("A1", "A7").BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium,
                            Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
	        */
     
            m_objBook.SaveCopyAs(filename);
           // m_excelApp.SaveWorkspace(filename);
        
        }

        private void loadGeneralData()
        {
            m_sheet.Cells[1, 1] = "Problem Title:";
            m_sheet.Cells[1, 2] = m_result_summary.Title;
            m_sheet.Cells[2, 1] = "First Population:";
            m_sheet.Cells[2, 2] = m_result_summary.GeneratePopulationType;
            m_sheet.Cells[3, 1] = "Crossover: ";
            m_sheet.Cells[3, 2] = m_result_summary.CrossoverType;
            m_sheet.Cells[4, 1] = "Selection: ";
            m_sheet.Cells[4, 2] = m_result_summary.SelectionType;
            m_sheet.Cells[5, 1] = "Iteration: ";
            m_sheet.Cells[5, 2] = m_result_summary.NumberOfIterations.ToString();
            m_sheet.Cells[6, 1] = "Start Time: ";
            m_sheet.Cells[6, 2] = m_result_summary.StartTime;
            m_sheet.Cells[7, 1] = "Finish Time: ";
            m_sheet.Cells[7, 2] = m_result_summary.FinishTime;
            m_sheet.Cells[8, 1] = "Result: ";
            m_sheet.Cells[8, 2] = m_result_summary.BestResult;
            m_sheet.get_Range("A1", "A8").Font.Bold = true;
            m_sheet.get_Range("A1", "B8").EntireColumn.AutoFit();
            m_last_row_used = 10;
            
        }

        private void loadSolutionsRanges()
        {
            m_sheet.Cells[m_last_row_used, 1] = "generation";
            m_sheet.Cells[m_last_row_used, 2] = "Best Solution";
            m_sheet.Cells[m_last_row_used, 3] = "worst Solution";
            
            m_sheet.get_Range("A" + m_last_row_used.ToString(), "C" + m_last_row_used.ToString()).Font.Bold = true;
            m_sheet.get_Range("A" + m_last_row_used.ToString(), "C" + m_last_row_used.ToString()).EntireColumn.AutoFit();
            for (int i = 0; i < m_result_summary.MinMaxPerGeneration.Count; i++)
            {
                m_sheet.Cells[m_last_row_used+ i + 1, 1] = i + 1;
                m_sheet.Cells[m_last_row_used + i + 1, 2] = m_result_summary.MinMaxPerGeneration[i].Key;
                m_sheet.Cells[m_last_row_used + i + 1, 3] = m_result_summary.MinMaxPerGeneration[i].Value;
            }
            m_last_row_used = m_last_row_used + m_result_summary.MinMaxPerGeneration.Count + 2;
        }
    }
}
