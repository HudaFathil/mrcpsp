﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MRCPSP.CommonTypes;
using System.Collections;

namespace MRCPSP.Lindo
{
    public enum LINDO_PARAMETER_TYPE
    {
        START = 1 ,
        FINISH
    }

    class LindoParameter
    {
        private static int idCounter = 1;
        private int m_id;
        private Mode m_mode;
        private Resource m_resource;
        private LindoParameter m_constrain;
        private LINDO_PARAMETER_TYPE m_type;
        private ArrayList m_column;
        private ArrayList m_row;
        private bool m_nextStepWaiting;
        private Step m_step;

        public LindoParameter(LINDO_PARAMETER_TYPE type , 
                              LindoParameter cons ,
                              Mode m , 
                              Resource r , 
                              Step s)
        {
            m_id = idCounter;
            m_type = type;
            m_constrain = cons;
            m_resource = r;
            m_column = new ArrayList();
            m_row = new ArrayList();
            m_mode = m;
            m_nextStepWaiting = false;
            idCounter++;
            m_step = s;
        }

        public static void init()
        {
            idCounter = 1;
        }

        public int Id
        {
            get { return m_id; }
        }

        public Step Step
        {
            get { return m_step; }
        }

        public bool NextStepWaiting
        {
            get { return m_nextStepWaiting ; }
            set
            {
                m_nextStepWaiting = value;
            }
        }


        public ArrayList Columns
        {
            get { return m_column; }
        }

        public ArrayList Rows
        {
            get { return m_row; }
        }


        public LINDO_PARAMETER_TYPE type
        {
            get { return m_type; }
        }

        public Resource resource
        {
            get { return m_resource; }
        }

        public Mode mode
        {
            get { return m_mode; }
            set
            {
                m_mode = value;
            }
        }
           
         

        public double getProcessTime()
        {
            int processTime = 0;
            foreach (Operation op in m_mode.operations)
            {
                if (op.Rseource.Id == m_resource.Id)
                {
                    processTime += op.EndTime - op.StartTime;
                }
            }
            return processTime;
        }

        public double getOperationStartTime() 
        {
            int opStart = -1;
            foreach (Operation op in m_mode.operations)
            {
                if (op.Rseource.Id == m_resource.Id)
                {
                    if (opStart == -1 || op.StartTime < opStart) 
                        opStart = op.StartTime;
                }
            }
            return opStart;
        }

        public LindoParameter Predecessor
        {
            get { return m_constrain; }
            set
            {
                m_constrain = value;
            }
        }
        public override String ToString()
        {
            if (m_type.Equals(LINDO_PARAMETER_TYPE.START))
                return "S" + m_id;
            return "F" + m_id;

        }

        public bool Equals(LindoParameter other)
        {
            if (other == null)
                return false;
            return m_id == other.Id;
        }
    }
}
