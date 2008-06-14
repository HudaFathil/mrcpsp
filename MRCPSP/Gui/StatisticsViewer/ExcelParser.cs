using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Runtime.InteropServices;

using MRCPSP.CommonTypes;
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

        public ExcelParser(String filename, ResultSummary summary)
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
            loadResourceUsage();
            loadResourceDistribute();

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
            m_sheet.Cells[4, 1] = "Mutation Percent: ";
            m_sheet.Cells[4, 2] = m_result_summary.MutationPercent;
            m_sheet.Cells[6, 1] = "Iteration: ";
            m_sheet.Cells[6, 2] = m_result_summary.NumberOfIterations.ToString();
            m_sheet.Cells[7, 1] = "Start Time: ";
            m_sheet.Cells[7, 2] = m_result_summary.StartTime;
            m_sheet.Cells[8, 1] = "Finish Time: ";
            m_sheet.Cells[8, 2] = m_result_summary.FinishTime;
            m_sheet.Cells[9, 1] = "Result: ";
            m_sheet.Cells[9, 2] = m_result_summary.BestResult;
            m_sheet.get_Range("A1", "A9").Font.Bold = true;
            m_sheet.get_Range("A1", "B9").EntireColumn.AutoFit();
            m_last_row_used = 11;

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
                m_sheet.Cells[m_last_row_used + i + 1, 1] = i + 1;
                m_sheet.Cells[m_last_row_used + i + 1, 2] = m_result_summary.MinMaxPerGeneration[i].Key;
                m_sheet.Cells[m_last_row_used + i + 1, 3] = m_result_summary.MinMaxPerGeneration[i].Value;
            }
            m_last_row_used = m_last_row_used + m_result_summary.MinMaxPerGeneration.Count + 2;
        }

        private void loadResourceUsage()
        {
            m_sheet.Cells[m_last_row_used, 1] = "Resource";
            m_sheet.Cells[m_last_row_used, 2] = "Totla Busy Time";
            m_sheet.get_Range("A" + m_last_row_used.ToString(), "B" + m_last_row_used.ToString()).Font.Bold = true;
            m_sheet.get_Range("A" + m_last_row_used.ToString(), "B" + m_last_row_used.ToString()).EntireColumn.AutoFit();
            
            String[] names = new String[m_result_summary.getBestSolution().resultFromLindo.Keys.Count];
            double[] values = new double[m_result_summary.getBestSolution().resultFromLindo.Keys.Count];
            int resource_counter = 0;
            foreach (Resource r in m_result_summary.getBestSolution().resultFromLindo.Keys)
            {
                names[resource_counter] = r.Name;
                List<ResultParameter> tasks_for_resource = m_result_summary.getBestSolution().resultFromLindo[r];
                for (int i = 0; i < tasks_for_resource.Count; i++)
                {
                    values[resource_counter] += (tasks_for_resource[i].finishTime - tasks_for_resource[i].startTime);
                }
                resource_counter++;
            }         
            for (int i = 0; i < resource_counter; i++)
            {
                m_sheet.Cells[m_last_row_used + i + 1, 1] = names[i];
                m_sheet.Cells[m_last_row_used + i + 1, 2] = values[i];
             
            }
            m_last_row_used = m_last_row_used + resource_counter + 2;
        }

        private void loadResourceDistribute()
        {
            m_sheet.Cells[m_last_row_used, 1] = "Resource";
            m_sheet.Cells[m_last_row_used, 2] = "Product";
            m_sheet.Cells[m_last_row_used, 3] = "Job";
            m_sheet.Cells[m_last_row_used, 4] = "Step";
            m_sheet.Cells[m_last_row_used, 5] = "Start Time";
            m_sheet.Cells[m_last_row_used, 6] = "Finish Time";
            m_sheet.get_Range("A" + m_last_row_used.ToString(), "F" + m_last_row_used.ToString()).Font.Bold = true;
            m_sheet.get_Range("A" + m_last_row_used.ToString(), "F" + m_last_row_used.ToString()).EntireColumn.AutoFit();
            m_last_row_used++;
            foreach (Resource r in m_result_summary.getBestSolution().resultFromLindo.Keys)
            {
                List<ResultParameter> resource_operations_done = m_result_summary.getBestSolution().resultFromLindo[r];

                for (int i = 0; i < resource_operations_done.Count; i++)
                {
                    m_sheet.Cells[m_last_row_used, 1] = r.Name;
                    m_sheet.Cells[m_last_row_used, 2] = resource_operations_done[i].product.Name;
                    m_sheet.Cells[m_last_row_used, 3] = resource_operations_done[i].jobID.ToString();
                    m_sheet.Cells[m_last_row_used, 4] = resource_operations_done[i].step.Name;
                    m_sheet.Cells[m_last_row_used, 5] = resource_operations_done[i].startTime.ToString();
                    m_sheet.Cells[m_last_row_used, 6] = resource_operations_done[i].finishTime.ToString();
                    m_last_row_used++;
                }              
            }
            m_last_row_used++;
        }

    }
}
