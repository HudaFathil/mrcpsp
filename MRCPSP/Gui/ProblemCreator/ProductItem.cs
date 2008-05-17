using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace MRCPSP.Gui.ProblemCreator
{
    public class ProductItem
    {
        private static int m_product_id_counter = 1;
        private int m_product_id;
        private string m_product_name;
        private int m_product_size;
        private Color m_constraints_color;

        private DataGridView m_requested_job_start_end_time;

        public ProductItem(String name, int size) {
            this.m_product_id = m_product_id_counter;
            m_product_id_counter++;
            m_product_name = name;
            m_product_size = size;           
            m_requested_job_start_end_time = new DataGridView();
            m_requested_job_start_end_time.Columns.Add("job_id", "Job Id");
            m_requested_job_start_end_time.Columns.Add("earliest_start_time", "Earliest Start Time");
            m_requested_job_start_end_time.Columns.Add("latest_termination_time", "Latest Temination Time");
            m_requested_job_start_end_time.Columns[1].Width +=30;
            m_requested_job_start_end_time.Columns[2].Width += 30;
            m_requested_job_start_end_time.RowCount = m_product_size;
            for (int i=0; i < m_product_size; i++) {
                m_requested_job_start_end_time[0, i].Value = i + 1;
                m_requested_job_start_end_time[1, i].Value = 0;
                m_requested_job_start_end_time[2, i].Value = Double.PositiveInfinity;
            }
         // m_requested_job_start_end_time.Columns[0]           
        }

        public void deleteMe() {
            m_product_id_counter--;
        }

        public override string ToString()
        {
            return m_product_name + " (" + m_product_size.ToString() + ")";
        }

        public int Size
        {
            get { return m_product_size; }
            set
            {
               m_product_size = value;
            }
        }

        public int Id
        {
            get { return m_product_id; }
            set
            {
                m_product_id = value;
            }
        }
        public string Name
        {
            get { return m_product_name; }
            set
            {
                m_product_name = value;
            }
        }
        public Color ConstraintsColor
        {
            get { return m_constraints_color; }
            set
            {
                m_constraints_color = value;
            }
        }

        public DataGridView JobsData 
        {
            get { return m_requested_job_start_end_time; }
            set
            {
                m_requested_job_start_end_time = value;
            }
        }
    }
}
