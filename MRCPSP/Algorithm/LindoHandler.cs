using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;

using MRCPSP.Domain;
using MRCPSP.Controllers;
using MRCPSP.CommonTypes;

namespace MRCPSP.Algorithm
{
	[ StructLayout( LayoutKind.Sequential )]
	public class CallbackData
	{
		public int count;

		// Constructor:    
		public CallbackData() 
		{
			count=0;
		}
	}
	
	public class LindoHandler
	{
		public static void APIErrorCheck(StreamWriter sr3,int pEnv, int nErr)
		{
			if (nErr > 0)
			{
				StringBuilder cMessage = new StringBuilder(lindo.LS_MAX_ERROR_MESSAGE_LENGTH);
				lindo.LSgetErrorMessage((System.IntPtr)pEnv, nErr, cMessage);
				//Console.WriteLine(cMessage.ToString());
				sr3.WriteLine(cMessage.ToString());
				// MessageBox.Show(cMessage.ToString());
			}
		}

		public static  int    TOTAL_NUMBER_OF_CONSTRAINTS             = 100;
		public static  int    TOTAL_NUMBER_OF_VARIBLES                = 1000;
		
		public class SpSeqSolution
		{
			public int[] seq;
			public ArrayList varListToDel;
			public int res_ID;

			public SpSeqSolution(int jobsNum, int resID)
			{
				seq = new int[jobsNum];
				varListToDel.Clear();
				res_ID = resID;
			}
		}


		#region Class Precedence_Lf_Matrix
	
		public class Precedence_Lf_Matrix
		{
			public int familyID;
			public int[] arrPre;
			public int[] arrSec;
			public int[] arr_L_First;
			public int[] arr_L_Last;
			private int counterOperations;
			private int[,] matrix_L_f_r;
			public int[] operationsOfFamily;
			public int[] resourcesOfFamily;
			public int jobsNumber;
			public Operations_Positions_On_Resource[] arr_L_F_R;

			public Precedence_Lf_Matrix(StreamWriter sr1, int famNum, int[] arrP, int[] arrS, int[] operations, int operationsCounter, int famlyJobsNumber, int[] arrFamilyResources)
			{
				familyID = famNum;
				arrPre = new int[arrP.Length];
				arrSec = new int[arrP.Length];
				operationsOfFamily = new int[operationsCounter];
				resourcesOfFamily = new int[arrFamilyResources.Length];

				this.counterOperations = operationsCounter;
		//120707		Console.WriteLine("THE OPERATION COUNTER + {0}",operationsCounter);
				jobsNumber = famlyJobsNumber;
				matrix_L_f_r = new int[ operationsCounter, operationsCounter];
				int[] arr_New_Relation = new int[arrSec.Length+1];
			
				arrPre = arrP;
				arrSec = arrS;
				operationsOfFamily = operations;
				resourcesOfFamily = arrFamilyResources;
				for (int i=0;i<arrPre.Length;i++)
				{		
		//120707			Console.WriteLine("Before Write_In_Matrix");
					sr1.Flush();
					Write_In_Matrix(ref  matrix_L_f_r, FindRelation(arrPre, arrSec, i+1,arrSec[i]),arrPre[i]);
				//	Write_In_Matrix(ref  matrix_L_f_r, FindRelation(arrPre, arrSec, i+1,arrSec[i]),arrPre, i);
				}
				Print_Matrix(sr1);
				Creat_L_First();
				Creat_L_Last();
		//120707		Console.WriteLine("arr_L_Last:");
		//120707		for(int i = 0;i<arr_L_Last.Length;i++)
		//120707		{
		//120707			Console.WriteLine(" {0}",arr_L_Last[i]);
		//120707		}
			}		
			

			public void Print_Matrix(StreamWriter sr1)
			{
				//Console.WriteLine(" PRINTING: {0} X {1}",this.matrix_L_f_r.GetLength(0),this.matrix_L_f_r.GetLength(1));			
				sr1.WriteLine(" PRINTING: {0} X {1}",this.matrix_L_f_r.GetLength(0),this.matrix_L_f_r.GetLength(1));			
				for (int i=0;i<this.matrix_L_f_r.GetLength(0);i++)
				{
					for (int j=0;j<this.matrix_L_f_r.GetLength(1);j++)
					{
						//Console.Write(" {0}",this.matrix_L_f_r[i,j]);
						sr1.Write(" {0}",this.matrix_L_f_r[i,j]);
					}
					//Console.WriteLine(" ");	
					sr1.WriteLine(" ");	
				}
			}
			
			
			public int[,] Remov_Column(int columnToDelete)
			{
				int[,] new_Remov_Column = new int[this.matrix_L_f_r.GetLength(0),this.matrix_L_f_r.GetLength(0)-1];
				int col;
				for (int i=0;i<this.matrix_L_f_r.GetLength(1);i++)
				{
					col=0;							
					for (int j=0;j<this.matrix_L_f_r.GetLength(1);j++)
					{
						if (j!=columnToDelete)
						{
							new_Remov_Column[i,col]= this.matrix_L_f_r[i,j];
							//Console.Write(" {0}",RelMat[i,j]);
							col++;
						}
					}				
				}
				Console.WriteLine(" ");	
				return(new_Remov_Column);
			}

			
			public int[,] Remov_Line(int lineToDelete)
			{
				int[,] new_Remov_Line = new int[this.matrix_L_f_r.GetLength(1)-1,this.matrix_L_f_r.GetLength(1)];
				int line=0;
				for (int i=0;i<this.matrix_L_f_r.GetLength(1);i++)
				{										
					if (i!=lineToDelete)
					{
						for (int j=0;j<this.matrix_L_f_r.GetLength(1);j++)
						{					
							new_Remov_Line[line,j]= this.matrix_L_f_r[i,j];
							//Console.Write(" {0}",RelMat[i,j]);
						}
						line++;
					}				
				}
				Console.WriteLine(" ");	
				return(new_Remov_Line);
			}

		
			public void Creat_L_First()
			{		
				arr_L_First = new int[this.counterOperations];
				int counterIfOne;
				int matSize=this.matrix_L_f_r.GetLength(1);
				for (int i=0;i<matSize;i++)
				{
					counterIfOne=0;							
					for (int j=0;j<matSize;j++)
					{
						if (this.matrix_L_f_r[j,i]==1)
						{						
							//Console.Write(" {0}",RelMat[i,j]);
							counterIfOne++;
						}					
					}
					//	Console.WriteLine("i={0} counterIfOne+1={1} ",i,counterIfOne+1);	
					arr_L_First[i]=counterIfOne+1;			
				}				
				//return(arrL_first);			
			}
	
		
			public void Creat_L_Last()
			{
				arr_L_Last = new int[this.counterOperations];			
				int matSize=this.matrix_L_f_r.GetLength(1);
				int[] countingLine;
				countingLine = Counting_Line();
				for(int i=0;i<countingLine.Length;i++)
				{
					arr_L_Last[i] = (this.jobsNumber*this.counterOperations - countingLine[i]);
				}
			}


			public int[] Counting_Line()
			{		
				int[] arrLine_Count = new int[this.counterOperations];
				int counterIfOne;
				int matSize=this.matrix_L_f_r.GetLength(1);
				for (int i=0;i<matSize;i++)
				{
					counterIfOne=0;							
					for (int j=0;j<matSize;j++)
					{
						if (this.matrix_L_f_r[i,j]==1)
						{						
							//Console.Write(" {0}",RelMat[i,j]);
							counterIfOne++;
						}					
					}
					//Console.WriteLine("i={0} counterIfOne+1={1} ",i,counterIfOne+1);	
					arrLine_Count[i]=counterIfOne;			
				}
				//Console.WriteLine(" ");	
				return(arrLine_Count);			
			}


			public int[,] Matrix_L_f_r
			{
				get 
				{
					return matrix_L_f_r; 
				}				
			}


			public int Convert_OperationID_To_Index(int operationID)
			{  // Input  = operationID Output = operation index in the family Operation list
				bool found = false;
				int index = 0;
				while ((found==false) & (index<operationsOfFamily.Length))
				{
					if (operationID==operationsOfFamily[index])
					{
						found=true;
					}
					index++;
				}
				return(index-1);
			}			
		}

		#endregion
	
		#region class Operations_Positions_On_Resource
		public class Operations_Positions_On_Resource
		{
			public byte Resource_ID;
			public int[] Operations_Of_Resource;
			public int[] operation_Real_Index;
			public int[] Operation_That_Must_Be_On_Resource;
			public int[] arr_L_First;
			public int[] arr_L_Last;			

			public Operations_Positions_On_Resource(byte resourceID, int[,] RelMat,int sizeOfMatrix, int[] operations_Of_RelMat, int[] operations_Vector,int[] operationsRealIndex, int[] operationsMustBeOnResource, int jobsNumber)
			{   
				Resource_ID = resourceID;
				Operations_Of_Resource = new int[operations_Vector.Length];
				operation_Real_Index = new int[operations_Vector.Length];
				Operation_That_Must_Be_On_Resource = new int[operationsMustBeOnResource.Length];

				Operations_Of_Resource = operations_Vector;
				operation_Real_Index = operationsRealIndex;
				Operation_That_Must_Be_On_Resource = operationsMustBeOnResource;
				int[,] matrix_L_F_R = Creat_L_F_R(RelMat, sizeOfMatrix, operations_Of_RelMat, operations_Vector);
	//120707			Print_Matrix(matrix_L_F_R);
				Creat_L_First(matrix_L_F_R);
				Creat_L_Last(matrix_L_F_R, jobsNumber);
	//120707			PrintValues(Operations_Of_Resource);
	//120707			PrintValues(arr_L_First);
	//120707			PrintValues(arr_L_Last);
			}
		
	
			public void Print_Matrix(int[,] matrix_L_f_r)
			{
			 	Console.WriteLine(" PRINTING: {0} X {1}",matrix_L_f_r.GetLength(0),matrix_L_f_r.GetLength(1));			
	 			for (int i=0;i<matrix_L_f_r.GetLength(0);i++)
				{
					for (int j=0;j<matrix_L_f_r.GetLength(1);j++)
					{
						Console.Write(" {0}",matrix_L_f_r[i,j]);
					}
					Console.WriteLine(" ");				
 				} 
			}
			
			
			public int Convert_Operation_Index(int realIndex)
			{
				bool found = false;
				int index = 0;
				while ((found==false) & (index<operation_Real_Index.Length))
				{
					if (realIndex==operation_Real_Index[index])
					{
						found=true;
					}

					index++;
				}
				return(index-1);
			}

			
			public void Print_L_First_And_Last()
			{
				for (int i=0;i<arr_L_First.Length;i++)
				{
					Console.WriteLine("{0}) 1st={1} Last={2}",i,arr_L_First[i], arr_L_Last[i]);
				}
			}

			
			public void Creat_L_First(int[,] matrix_L_f_r)
			{		
				arr_L_First = new int[Operations_Of_Resource.Length];
				int counterIfOne;
				int matSize=matrix_L_f_r.GetLength(1);
				for (int i=0;i<matSize;i++)
				{
					counterIfOne=0;	
					int myIndex=Array.BinarySearch(Operation_That_Must_Be_On_Resource,Operations_Of_Resource[i]);
	
					if (myIndex>=0)
					{
						Console.WriteLine("BinarySearch - {0}",Operations_Of_Resource[i]);
						for (int j=0;j<matSize;j++)
						{
							if (matrix_L_f_r[j,i]==1)
							{						
								//Console.Write(" {0}",RelMat[i,j]);
								counterIfOne++;
							}					
						}
					}
					Console.WriteLine("i={0} counterIfOne+1={1} ",i,counterIfOne+1);	
					arr_L_First[i]=(counterIfOne+1);			
				}				
				//return(arrL_first);			
			}
			
		
			public void Creat_L_Last(int[,] matrix_L_f_r, int jobsNumber)
			{
				arr_L_Last = new int[Operations_Of_Resource.Length];			
				int matSize=matrix_L_f_r.GetLength(1);
				int[] countingLine;
				countingLine = Counting_Line(matrix_L_f_r);
				for(int i=0;i<countingLine.Length;i++)
				{
					arr_L_Last[i] = (jobsNumber*Operations_Of_Resource.Length - countingLine[i]);
				}
			}


			public int[] Counting_Line(int[,] matrix_L_f_r)
			{		
				int[] arrLine_Count = new int[Operations_Of_Resource.Length];
				int counterIfOne;
				int matSize=matrix_L_f_r.GetLength(1);
				for (int i=0;i<matSize;i++)
				{
					counterIfOne=0;							
					for (int j=0;j<matSize;j++)
					{
						if (matrix_L_f_r[i,j]==1)
						{						
							//Console.Write(" {0}",RelMat[i,j]);
							counterIfOne++;
						}					
					}
					//Console.WriteLine("i={0} counterIfOne+1={1} ",i,counterIfOne+1);	
					arrLine_Count[i]=counterIfOne;			
				}
				//Console.WriteLine(" ");	
				return(arrLine_Count);			
			}
		
			
			public int[,] Creat_L_F_R(int[,] RelMat,int sizeOfMatrix,int[] operations_Of_RelMat, int[] operations_Of_R)
			{							
				int[,] matrix_L_F_R = new int[operations_Of_R.Length, operations_Of_R.Length];
				int line=0;
				int col=0;
				int sizeRelMat = RelMat.GetLength(0);
				//************  Remove The Line  ***************
				for (int i=0;i<sizeRelMat;i++)
				{		
					col=0;				
					if (line<operations_Of_R.Length)
					{
						//Console.Write("operations_Of_RelMat[i]={0}",operations_Of_RelMat[i]);
						if (operations_Of_RelMat[i] == operations_Of_R[line])
						{
							for (int j=0;j<sizeRelMat;j++)
							{	
								if (col<operations_Of_R.Length)
								{
									//Console.WriteLine("j {0} - col={1} operation={2}",j,col,operations_Of_R[col]);
									if (operations_Of_RelMat[j] == operations_Of_R[col])
									{	//**********************************************
										//************  Remove The Rows  ***************
										//**********************************************
										matrix_L_F_R[line,col]= RelMat[i,j];
										col++;
									}
								}
							}
							line++;
						}
					}
				}
			//	Console.WriteLine(" ");	
				//Print_Matrix(matrix_L_F_R);
				return(matrix_L_F_R);
			}
		}
		
		#endregion
	
		#region Class Yjfim_With_One_Mode
		public class Yjfim_With_One_Mode
		{
			public Yjfim_Class[] Y_With_One_ModeArray;
			public int arrayCounter=0;

			//  *********  Constructor  ********
			public Yjfim_With_One_Mode(int Array_Size)
			{
				Y_With_One_ModeArray = new Yjfim_Class[Array_Size];
			}
			
			public void AddVar(ulong YjfimV, int operationNumber, int familyV, int jobV)  // Add link to the Net
			{
				Y_With_One_ModeArray[this.arrayCounter] = new Yjfim_Class(YjfimV, operationNumber, familyV, jobV);
				arrayCounter++;
			}
		}

		#endregion
	
	 	#region Class Yjfim_Class
		public class Yjfim_Class
		{
			public ulong yjfim;
			public int operation;
			public int family;
			public int job;

			//  ***  **  *  Constructor *  **  ***
			public Yjfim_Class(ulong YjfimV, int operationNumber, int familyV, int jobV)
			{
				yjfim = YjfimV;
				operation = operationNumber;
				family = familyV;
				job = jobV;
			}
		}
		#endregion
		
		#region Class Batch_Resource_Yimrl_Links
		public class Batch_Resource_Yimrl_Links
		{
			public ResourceYimrlLinks[] arr_Batch_Resource;

			//  *********  Constructor **********
			//  *********************************
			public Batch_Resource_Yimrl_Links(int number)
			{
				arr_Batch_Resource = new ResourceYimrlLinks[number];
			}
			public void Add_Yimrl_Links(int r_index, ResourceYimrlLinks Res_Yimrl_Link, int resLr, int resID)
			{
				ResourceYimrlLinks resYLinks = new ResourceYimrlLinks(resLr, resID);
				resYLinks = Res_Yimrl_Link;

				arr_Batch_Resource[r_index] = resYLinks;
			}
			public void Print(StreamWriter sr2)
			{
				for(int i=0;i<arr_Batch_Resource.Length;i++)
				{
					arr_Batch_Resource[i].Print(sr2);
				}
			}
			//********************
			//**  Destructor: ***
			//********************
			~Batch_Resource_Yimrl_Links()
			{			
				 
	 			arr_Batch_Resource.Initialize();
			}		
		}
		#endregion

		#region Class ResourceYimrlLinks
		public class ResourceYimrlLinks
		{
			public int resourceID;
			public YimrlLinkToDelete[] arrYimrl_Tasks;

			//  *********  Constructor **********
			//  *********************************
			public ResourceYimrlLinks(int Lr, int r_ID)
			{			
				resourceID = r_ID;
				arrYimrl_Tasks = new YimrlLinkToDelete[Lr];//-1 because we save only the information about the Batches>=2  1Batch isn't relevant 
			}
			public void Add_Task_Link(int task, IEnumerable XList, int constraintNum)
			{
				YimrlLinkToDelete yimrl_Link =new YimrlLinkToDelete(task, XList,constraintNum);
				arrYimrl_Tasks[task] = yimrl_Link;
			}
			public void Print(StreamWriter sr2)
			{
				sr2.WriteLine("");
				sr2.WriteLine("The tasks of Resource {0}:",resourceID);
				sr2.WriteLine("--------------------------");
				for(int j=0;j<arrYimrl_Tasks.Length;j++)
				{						
					arrYimrl_Tasks[j].Print(sr2);
				}
			}
			//********************
			//**  Destructor: ***
			//********************
			~ResourceYimrlLinks()
			{
				arrYimrl_Tasks.Initialize();
			}
		}
		#endregion

		#region Class YimrlLinkToDelete
		public class YimrlLinkToDelete
		{
			public int task_Number;
			public ArrayList YimrlVarList = new ArrayList();
			public int ConstraintsNumberToDelete;

			//  *********  Constructor **********
			//  *********************************
			public YimrlLinkToDelete(int taskNumber, IEnumerable XList, int ConstraintNum)
			{
				this.task_Number = taskNumber;
				this.ConstraintsNumberToDelete = ConstraintNum;
				System.Collections.IEnumerator myEnumerator1 = XList.GetEnumerator();
				while ( myEnumerator1.MoveNext() )
				{
					this.YimrlVarList.Add(myEnumerator1.Current);									
				}				
			}
			public void Print(StreamWriter sr2)
			{
				sr2.WriteLine("The Variables of Task = {0}",this.task_Number);
				System.Collections.IEnumerator myEnumerator1 = this.YimrlVarList.GetEnumerator();
				while ( myEnumerator1.MoveNext() )
				{
					sr2.Write("{0}, ",myEnumerator1.Current);
				}
				sr2.WriteLine(" ");

			}
			//********************
			//**  Destructor: ***
			//********************
			~YimrlLinkToDelete()
			{
				YimrlVarList.Clear();
			}
		}
		#endregion

		#region Class YxNetLinks
		public class YxNetLinks
		{
			private YxLinkToDelete[] YxArray;
			private int arrayCounter=0;

			//  *********  Constructor **********
			//  *********************************
			public YxNetLinks(ulong Array_Size)
			{
				this.YxArray = new YxLinkToDelete[Array_Size];
			}
			public void IndexOfYjfim(int YjfimToLook, ref int index)
			{
				for (int i=0;i<this.arrayCounter;i++)
				{
					if (this.YxArray[i].GetYjfimVar==YjfimToLook)
					{
						index=i;
					}
					else
					{
						index=-1;
					}
				}
			}
			
			public void AddLink(int YjfimV, ArrayList XjfimrlList,ArrayList XjfimrlListResource, int ConstraintNum)  // Add link to the Net
			{
				this.YxArray[this.arrayCounter] = new YxLinkToDelete(YjfimV, XjfimrlList,XjfimrlListResource, ConstraintNum);
				arrayCounter++;
			}
			
			public ArrayList GetVectorToDelete(int YjfimV)
			{
				int i = 0 ;
				while (!(this.YxArray[i].GetYjfimVar==YjfimV)&(i<this.YxArray.Length))
				{
					i++;
				}
				return this.YxArray[i].GetYxLinkList;				
			}
	
			public ArrayList GetResourceVectorToDelete(int YjfimV)
			{
				int i = 0 ;
				while (!(this.YxArray[i].GetYjfimVar==YjfimV)&(i<this.YxArray.Length))
				{
					i++;
				}
				return this.YxArray[i].GetYxLinkListResource;				
			}

			
			//  ***  **  *  Printing *  **  ***
			public void PrintYxNetLinks(StreamWriter sr2)
			{
				//Console.WriteLine("PRINTING THE NET LINKS :");
				sr2.WriteLine("PRINTING THE NET LINKS :");
				for(int i=0;i<this.YxArray.Length;i++)
				{
					this.YxArray[i].PrintYxLinkToDelete(sr2);
				}
			}			
		}
		#endregion
	
		#region class YxLinkToDelete
		public class YxLinkToDelete
		{
			private int YjfimVar;
			private ArrayList XjfimrlVarList = new ArrayList();	
            private ArrayList XjfimrlResourceList = new ArrayList();
			private int ConstraintsNumberToDelete;	
			
			
			//  *********  Constructor **********
			//  *********************************
			public YxLinkToDelete(int YjfimV, IEnumerable XList,IEnumerable XListResource, int ConstraintNum)
			{
				this.YjfimVar = YjfimV;
				this.ConstraintsNumberToDelete = ConstraintNum;
				System.Collections.IEnumerator myEnumerator1 = XList.GetEnumerator();
				while ( myEnumerator1.MoveNext() )
				{
					this.XjfimrlVarList.Add(myEnumerator1.Current);									
				}
				System.Collections.IEnumerator myEnumerator2 = XListResource.GetEnumerator();
				while ( myEnumerator2.MoveNext() )
				{
					this.XjfimrlResourceList.Add(myEnumerator2.Current);									
				}
			}

			public ArrayList GetYxLinkList
			{
				get
				{			
					return this.XjfimrlVarList;
				}
			}
			public ArrayList GetYxLinkListResource
			{
				get
				{			
					return this.XjfimrlResourceList;;
				}
			}
			//  ***  **  *  Printing *  **  ***
			public void PrintYxLinkToDelete(StreamWriter sr2)
			{
				sr2.WriteLine("Printing the Yjfim={0}",this.GetYjfimVar);
			//	Console.WriteLine("Printing the Yjfim={0}",this.GetYjfimVar);
		//120707		PrintValues(sr2, this.GetYxLinkList);
			}

			public int GetYjfimVar
			{
				get
				{			
					return this.YjfimVar;
				}
			}
		}
#endregion
		
		#region Class YxResourcelinkToDelete
		public class YxResourcelinkToDelete
		{
			public int resourceVar;
			public ArrayList XjfimrlVarList = new ArrayList();
			public YxResourcelinkToDelete()
			{
				resourceVar=0;
				XjfimrlVarList.Clear();
				
			}
			 
		}
		#endregion
	

		#region Class VariableVector
		public class VariableVector
		{
			private ArrayList CoefficientList = new ArrayList();
			private ArrayList EquationLevel   = new ArrayList();
			private int listCounter = new int();
			public VariableVector()
			{}
			public void AddCoefficient(int equationLevel, double coefficient)
			{
				CoefficientList.Add(coefficient);
				EquationLevel.Add(equationLevel);
			}
			public void PrintVariableVectorLists()
			{
				//Console.WriteLine("Printing the Coefficients of {0}",
				Print2Values(EquationLevel,CoefficientList);
			}
			public void TransferListsToVectors(ref ArrayList lindoadA, ref ArrayList lindoanRoxList, ref ArrayList lindopnLenColList)
			{
				lindopnLenColList.Add(this.CoefficientList.Count);
			//	Console.WriteLine("the counter={0}",this.CoefficientList.Count);
				System.Collections.IEnumerator myEnumerator1 = this.EquationLevel.GetEnumerator();
				System.Collections.IEnumerator myEnumerator2 = this.CoefficientList.GetEnumerator();
				//ulong variableNumber = new ulong();
				//variableNumber = 0;
				while ( myEnumerator1.MoveNext() )
				{
					myEnumerator2.MoveNext();					
					lindoadA.Add( myEnumerator2.Current );
					lindoanRoxList.Add( myEnumerator1.Current );							
				}
			}			        
		}
#endregion

   
		public LindoHandler()		{}

		public void run()
		{	
		
			#region Varaibles Defenitions
            Problem problem = ApplicManager.Instance.CurrentProblem;

			//  *** MrscspVariable Preparation ***
			ArrayList VariblesVectorJFIMRL = new ArrayList();
			StringBuilder LindoVarType = new StringBuilder();
			sbyte startIndexXjfimrlInVariableVector = 0;
			byte[] resourceImCount = new byte[problem.getNumberOfResources()];
			ulong counterXjfimrlInVariablesVector = new ulong();
			ulong startIndexYjfimInVariableVector = 0;
			ulong counterYjfimInVariablesVector = 0;
			ulong startIndexYimrlInVariableVector = 0;
			ulong counterYimrlInVariablesVector = 0;
			ulong startIndexYfimrlVariablesVector = 0;
			ulong counterYfimrlInVariablesVector = 0;
			ulong startIndexTjfiInVariableVector = 0;
			ulong counterTjfiInVariablesVector = 0;
			ulong startIndexTrlInVariableVector = 0;
			ulong counterTrlInVariablesVector = 0;
			ulong startIndexVrlInVariableVector = 0;
			ulong counterVrlInVariablesVector = 0;
			ulong startIndexZrlInVariableVector = 0;
			ulong counterZrlInVariablesVector = 0;
			ulong startIndexYniInVariableVector = 0;
			ulong counterYniInVariablesVector = 0;
			ulong mrscspVariable = new ulong();
			ulong[] yjfimOneMode; // Array which encompass the Yjfim with OneMode
			int yjfimOneModeCounter = 0;
			//ulong[] mrscspYimrlVariableArray=new ulong[4];
			
			float[,] ConstraintsMatrix = new float[9500, 9500];
			ConstraintsMatrix.Initialize();
			double[] RightHandSideValues = new double[9500];
			char[] ConstraintsSenses = new char [9500];
			int constraintsCounter = new int();		
			long rightSideVector = new long(); // The values of in the right side (like 0in const. 1 ...)
			ArrayList imList = new ArrayList();
			ArrayList OperationFamilyList = new ArrayList();
			ArrayList IFinishfList = new ArrayList();
			ArrayList IStartfList = new ArrayList();
			ArrayList OperationMaxMode = new ArrayList();
			SpSeqSolution[] spSeqSol;
			DateTime d1 = DateTime.Now;

			/*OperationMode om1 = new OperationMode(6,9);
			OperationMaxMode.Add(new OperationMode(1,5));
			OperationMaxMode.Add(om1);*/

			
			StreamWriter sr = File.CreateText("MyFile.txt");
			sr.WriteLine("-- something need to be here ---------------------");
			
			#endregion			
			
			#region CALCULATING THE PRECEDENCE VARAIABLE
			// ********************************************
			// *** Creating for each family the Lf Matrix **
			// ********************************************
			Precedence_Lf_Matrix[] family_Lf_Matrix = new Precedence_Lf_Matrix[problem.Products.Length];
            for (int f = 0; f < problem.Products.Length; f++)
			{
				// ****  Creating a vector of the Family Operations  *********				
				Product p = problem.Products[f];
				ArrayList listFamilyResource = new ArrayList();
				int[] familyOperations = new int[((System.Collections.ArrayList)problem.StepsInProduct[p]).Count];
                for (int i = 0; i < ((System.Collections.ArrayList)problem.StepsInProduct[p]).Count; i++)
				{
                    Step s = (Step)(((System.Collections.ArrayList)problem.StepsInProduct[p])[i]);
                    familyOperations[i] = s.Id; 
					foreach (Resource r in  problem.getAllResourcesInStep(s))
					{
                        if (!listFamilyResource.Contains(r.Id))
                            listFamilyResource.Add(r.Id);
					}
				}
							
				int[] arrFamilyResources = new int[listFamilyResource.Count];
				arrFamilyResources = Translate_List_To_Array(listFamilyResource,listFamilyResource.Count);
				Array.Sort(arrFamilyResources);
				PrintValues(arrFamilyResources);
				listFamilyResource.Clear();
		
				// **** input the Previous operations to the Pre array
				// **** input the Subsequent operations to the Sec array
                System.Collections.ArrayList const_in_product = problem.getAllConstraintForProduct(p);
                int counterPrec = const_in_product.Count;
				int counterMatrix = familyOperations.Length;
				int[] arrPre = new int[counterPrec];
				int[] arrSec = new int[counterPrec];
				int[,] matrix_L_f_r = new int[counterMatrix,counterMatrix];
				int[] arr_New_Relation = new int[arrSec.Length+1];		

				for (int i=0;i<counterPrec;i++)
				{
                    arrPre[i] = ((Constraint)const_in_product[i]).StepFrom.Id;
                    arrSec[i] = ((Constraint)const_in_product[i]).StepTo.Id;			
				}
				//*****************************
				//***   SORTING THE ARRAY    **
				//*****************************
				Console.WriteLine("Before Sorting");
				//PrintValues(arrPre);
				Array.Sort(arrPre,arrSec,0,arrPre.Length);
				//PrintValues(arrPre);
				//*****************************
					
				int jobsNumber = p.Size;
				Precedence_Lf_Matrix matrix_Lf = new Precedence_Lf_Matrix(sr, p.Id, arrPre, arrSec, familyOperations, counterMatrix, jobsNumber, arrFamilyResources );
				family_Lf_Matrix[f] = matrix_Lf;						
			}		
	/*
			Operations_Positions_On_Resource[] arr_L_F_R = new Operations_Positions_On_Resource[dataSet.Resources.Count];
			//DataView operationsToFamiliesView = new DataView(dataSet.OperationsToFamilies);
			
			// **********************************************************************************************
			// ***  Creating the Vectors of Operations for each resource and for the OBJECT - "Oper_Posit_On_Res"
			// **********************************************************************************************
			for (int f=0;f<dataSet.Families.Count;f++)
			{
				for (int r=0;r<dataSet.Resources.Count;r++)
				{				
					ArrayList operationsInResource = new ArrayList();
					ArrayList operationsRealIndex = new ArrayList();
					ArrayList operationsMustBeOnResource = new ArrayList();
					for (int op=0;op<dataSet.OperationsToFamilies.Count;op++)
					{
						DataView resourceUsageView = new DataView(dataSet.ResourceUsage);
						resourceUsageView.RowFilter = "Operation_ID ="+ dataSet.OperationsToFamilies[op].Operation_ID  + "AND Resource_ID=" + dataSet.Resources[r].Resource_ID ;
						//Console.WriteLine("resourceUsageView.Count={0}",resourceUsageView.Count);
						if (resourceUsageView.Count>0)
						{
							operationsInResource.Add(dataSet.OperationsToFamilies[op].Operation_ID);						
							operationsRealIndex.Add(op);
						}
					}
					//Console.WriteLine("Operations That Must Be:");
					System.Collections.IEnumerator myEnumerator = operationsInResource.GetEnumerator();
					while ( myEnumerator.MoveNext() )
					{
						DataView resourceUsageView = new DataView(dataSet.ResourceUsage);
						resourceUsageView.RowFilter = "Operation_ID ="+ (int)myEnumerator.Current  + "AND Resource_ID<>" + dataSet.Resources[r].Resource_ID ;
						if (resourceUsageView.Count==0)
						{
							operationsMustBeOnResource.Add((int)myEnumerator.Current);
							//Console.Write("{0}, ",(int)myEnumerator.Current);
						}
					}

					//Console.WriteLine("Length={0}",operationsInResource.Count);
					//PrintValues(family_Lf_Matrix[f].operationsOfFamily);
					Operations_Positions_On_Resource Oper_Posit_On_Res = new Operations_Positions_On_Resource((byte)dataSet.Resources[r].Resource_ID,family_Lf_Matrix[f].Matrix_L_f_r,family_Lf_Matrix[f].Matrix_L_f_r.GetLength(0), family_Lf_Matrix[f].operationsOfFamily ,Translate_List_To_Array(operationsInResource,operationsInResource.Count),Translate_List_To_Array(operationsRealIndex, operationsRealIndex.Count), Translate_List_To_Array(operationsMustBeOnResource,operationsMustBeOnResource.Count),family_Lf_Matrix[f].jobsNumber);
					arr_L_F_R[r] = Oper_Posit_On_Res;
					operationsInResource.Clear();
				}
			}
				
			for (int i=0;i<arr_L_F_R.Length;i++)
			{
				Console.WriteLine("Resource - {0}",arr_L_F_R[i].Resource_ID);
				PrintValues(arr_L_F_R[i].Operations_Of_Resource);
				PrintValues(arr_L_F_R[i].operation_Real_Index);
				arr_L_F_R[i].Print_L_First_And_Last();
			}

			strId=System.Console.ReadLine();
			*/	

	
			#endregion

			#region CALCULATING THE Lr VARAIABLE

			// *** Creating 2 arrays which represent the Resources of the problem and the
			// *** Lr of each Resource by using a shared index 
			int[] resourceID = new int[problem.Resources.Length];
            int[] resourceLr = new int[problem.Resources.Length];
			int[] resourcetr1 = new int[problem.Resources.Length];
            int[] resourceKr = new int[problem.Resources.Length];
			int Lr = new int();
			int resourceCounterInOperations = new int();
			bool found = new bool();
			int operInResourceUsage=0;
			int familyLr=new int();

            for (int r = 0; r < problem.Resources.Length; r++)
			{
                sr.WriteLine("RESOURCE+{0}", problem.Resources[r].Id);
				familyLr=0;
				for (int f=0;f< problem.Products.Length;f++)
				{
                    int c =problem.getNumberOfResourceShowsInProduct(problem.Resources[r], problem.Products[f]);
                    familyLr = familyLr + c * problem.Products[f].Size;				
				}

                resourceID[r] = problem.Resources[r].Id;
                resourceKr[r] = 1; // shay need to change for batch
                resourcetr1[r] = 0;  // shay need to know what is it dataSet.Resources[r].Release_Date_tr1;
				resourceLr[r] = familyLr;
			}
			// *** PRINTING THE Lr OF EACE RESOURCE
			sr.WriteLine("THE Lr OF EACH RESOURCE:");
			for (int t=0;t<resourceID.Length;t++)
				sr.WriteLine("\tRESOURCE -{0} ===> Lr- {1}",resourceID[t],resourceLr[t]);
			#endregion
		
			#region Calculating the Number of Yjfim with One Mode
			for (int i=0;i< problem.Products.Length;i++) 
            {
                foreach (Step s in (System.Collections.ArrayList)problem.StepsInProduct[problem.Products[i]]) 
                {
                    if (((System.Collections.ArrayList)problem.ModesInStep[s]).Count == 1)
                        yjfimOneModeCounter++;
                }
            }
							
			#endregion	

			#region Creating the Xjfimrl Variables + Adding them to VariblesVectorJFIMRL
			// ************************************************************************
			// *** Creating the Xjfimrl Variables + Adding them to VariblesVectorJFIMRL
			// ************************************************************************
			ulong Mrscsp = new ulong();
			sr.WriteLine("Xjfimrl VARIABLES :");
			int vParameterIdentifier = 0;// characterize the Yjfim Varaible from the Yimrl.Yjfim become Y1jfim & Yfimr becom Y2imrl
			int vJob = 0;
			int vFamily = 0;
			int vOperation = 0;
			int vMode = 0;
			int vResource = 0;			 
			for (int i = 0;i< problem.Products.Length;i++)
			{				
				for (int k=0;k< problem.Products[i].Size;k++)
				{
					foreach (Step s in (System.Collections.ArrayList)problem.StepsInProduct[problem.Products[i]])
					{
						foreach (Mode m in (System.Collections.ArrayList)problem.ModesInStep[s])
						{
						    for (int o =0; o < m.operations.Count; o++) {                        
								int index1=0;
								while ( resourceID[index1]!= ((Operation)m.operations[o]).Rseource.Id)
										index1++;
								for (int lr=1;lr<=resourceLr[index1];lr++)
            					{
                                    vJob = k; // need to check
									vFamily = problem.Products[i].Id;
                                    vOperation = s.Id;
									vMode = m.Id;
									vResource = ((Operation)m.operations[o]).Rseource.Id;									
									ulong[] mrscspVariableArray=new ulong[6] {(ulong)vJob,(ulong)vFamily,(ulong)vOperation,(ulong)vMode,(ulong)vResource,(ulong)lr };
									creatMrscspVariable(ref Mrscsp, mrscspVariableArray);
									if (VariblesVectorJFIMRL.Contains(Mrscsp))
									{
										sr.WriteLine("BUUUG  Xjfimrl={0} Contains={1}",Mrscsp,VariblesVectorJFIMRL.Contains(Mrscsp));
									}
									else
									{
										VariblesVectorJFIMRL.Add(Mrscsp);
										LindoVarType.Append("B");
										counterXjfimrlInVariablesVector++;
									}
								}

							}
						
						}
					}
				}
			}
						
		    #endregion
			
			#region Creating the Yjfim Variables + Adding them to VariblesVectorJFIMRL
			// ************************************************************************
			// *** Creating the Yjfim Variables + Adding them to VariblesVectorJFIMRL
			// ************************************************************************
			startIndexYjfimInVariableVector = counterXjfimrlInVariablesVector;
			sr.WriteLine("Yjfim VARIABLES :  ( Starting counter ={0} )",startIndexYjfimInVariableVector );			
			Yjfim_With_One_Mode yjfim_One_Mode = new Yjfim_With_One_Mode(yjfimOneModeCounter);
			for (int i = 0;i< problem.Products.Length;i++)
			{				
				for (int k=0;k< problem.Products[i].Size;k++)
				{
					foreach (Step s in (System.Collections.ArrayList)problem.StepsInProduct[problem.Products[i]])
					{
						foreach (Mode m in (System.Collections.ArrayList)problem.ModesInStep[s])
						{
							vParameterIdentifier = 999;
                            vJob = k;
                            vFamily = problem.Products[i].Id;
                            vOperation = s.Id;
                            vMode = m.Id;
							ulong[] mrscspVariableArray=new ulong[5] {(ulong)vParameterIdentifier,(ulong)vJob,(ulong)vFamily,(ulong)vOperation,(ulong)vMode};
							creatMrscspVariable(ref Mrscsp, mrscspVariableArray);
							if (VariblesVectorJFIMRL.Contains(Mrscsp))
							{
								sr.WriteLine("BUUUG  Yjfim={0} Contains={1}",Mrscsp,VariblesVectorJFIMRL.Contains(Mrscsp));
							}
						    else
							{
								VariblesVectorJFIMRL.Add(Mrscsp);
								LindoVarType.Append("B");
								counterYjfimInVariablesVector++;
								if (((System.Collections.ArrayList)problem.ModesInStep[s]).Count == 1)
								{
								    yjfim_One_Mode.AddVar(Mrscsp,vOperation, i, vJob);										
								}				
							}						
						}						
					}			
				}						 
			}
			
			#endregion
		
			#region Creating the Yimrl Variables + Adding them to VariblesVectorJFIMRL

			//Creating the Yimrl Variables + Adding them to VariblesVectorJFIMRL	

			startIndexYimrlInVariableVector = startIndexYjfimInVariableVector + counterYjfimInVariablesVector;
			for (int i =0; i < problem.Steps.Length; i++) 
            {
                foreach (Mode m in ((System.Collections.ArrayList)problem.ModesInStep[problem.Steps[i]])) {
                
                    for (int o=0; o < m.operations.Count; o++) {

                        int index1 = ((Operation)m.operations[o]).Rseource.Id;
                       
                        for (int lr = 1; lr <= resourceLr[index1]; lr++)
                        {
                            //vJob = (int)jobs[k].ItemArray[2];
                            //vFamily = (int)dataSet.Families[i].Family_ID;
                            vParameterIdentifier = 998;
                            vOperation = problem.Steps[i].Id;
                            vMode = m.Id;
                            vResource = ((Operation)m.operations[o]).Rseource.Id;
                            ulong[] mrscspVariableArray = new ulong[5] { (ulong)vParameterIdentifier, (ulong)vOperation, (ulong)vMode, (ulong)vResource, (ulong)lr };
                            //sr.WriteLine("COUNTERXjfimrlInVariablesVector={0} The j={1},f={2},i={3},m={4},r={5},Lr={6}",counterXjfimrlInVariablesVector,dataSet.Jobs[k].Job_ID,dataSet.Families[i].Family_ID,dataSet.OperationsToFamilies[g].Operation_ID,dataSet.Modes[j].Mode_ID,dataSet.ResourceUsage[r].Resource_ID,lr );
                            creatMrscspVariable(ref Mrscsp, mrscspVariableArray);
                            if (VariblesVectorJFIMRL.Contains(Mrscsp))
                            {
                                sr.WriteLine("BUUUG  Yimrl={0} Contains={1}", Mrscsp, VariblesVectorJFIMRL.Contains(Mrscsp));
                            }
                            else
                            {
                                VariblesVectorJFIMRL.Add(Mrscsp);
                                LindoVarType.Append("B");
                                counterYimrlInVariablesVector++;
                            }
                        }
                    }
                }
            }									
			#endregion

	        // deleted exclusive family region // shay !!!!!!!!!!

			#region Creating the Tjfi Variables + Adding them to VariblesVectorJFIMRL

			// *** Creating the Tjfi Variables + Adding them to VariblesVectorJFIMRL
			startIndexTjfiInVariableVector = startIndexYfimrlVariablesVector + counterYfimrlInVariablesVector;
			sr.WriteLine("Tjfi VARIABLES :      ( Starting counter ={0})", startIndexTjfiInVariableVector);			
			for (int i = 0;i< problem.Products.Length;i++)
			{				
				for (int k=0;k< problem.Products[i].Size;k++)
				{
					foreach (Step s in (System.Collections.ArrayList)problem.StepsInProduct[problem.Products[i]])
					{
                        vJob = k;
                        vOperation = s.Id;
                        vFamily = problem.Products[i].Id;
						sr.WriteLine("{0} - The j={1},f={2},i={3}",VariblesVectorJFIMRL.Count,vJob,vFamily,vOperation);
						ulong[] mrscspVariableArray=new ulong[3] {(ulong)vJob,(ulong)vFamily,(ulong)vOperation};//.Operation_ID};
						//ulong[] mrscspVariableArray=new ulong[3] {(ulong)jobs[k].ItemArray[2],(ulong)dataSet.Families[i].Family_ID,(ulong)operationsToFamilies[g].ItemArray[2]};//.Operation_ID};														
						creatMrscspVariable(ref Mrscsp, mrscspVariableArray);
					    VariblesVectorJFIMRL.Add(Mrscsp);
						LindoVarType.Append("C");
						counterTjfiInVariablesVector++;
					}
				}
			}
			
			#endregion
	
			#region Creating the Trl Variables + Adding them to VariblesVectorJFIMRL

			// *** Creating the Trl Variables + Adding them to VariblesVectorJFIMRL
			startIndexTrlInVariableVector = startIndexTjfiInVariableVector + counterTjfiInVariablesVector;
			sr.WriteLine("Trl VARIABLES :");
			for (int r=0;r<resourceID.Length;r++)
			{
				for (int lr=0;lr<resourceLr[r];lr++)
				{

					ulong[] mrscspVariableArray=new ulong[2] {(ulong)resourceID[r],(ulong)lr+1};
					sr.WriteLine("{0} - The Tr={1},l={2}",VariblesVectorJFIMRL.Count,resourceID[r],lr+1);
					creatMrscspVariable(ref Mrscsp, mrscspVariableArray);
					VariblesVectorJFIMRL.Add(Mrscsp);
					LindoVarType.Append("C");
					counterTrlInVariablesVector++;
				}
			}
			#endregion
              // shay support loading time
            /*
			#region Creating the Vrl Variables + Adding them to VariblesVectorJFIMRL

			// *** Creating the Vrl Variables + Adding them to VariblesVectorJFIMRL
			startIndexVrlInVariableVector = startIndexTrlInVariableVector + counterTrlInVariablesVector;
			sr.WriteLine("Vrl VARIABLES :");
			sr.WriteLine("*********************************");
			sr.WriteLine("VariblesVectorJFIMRL.count={0},VrlIndex={1}",VariblesVectorJFIMRL.Count,startIndexVrlInVariableVector );
			sr.WriteLine("*********************************");
		
           
            DataView LoadingTimesView = new DataView(dataSet.LoadingTimes);			
			
			if (LoadingTimesView.Count>0)
			{
				//23.01.07 - for (int r=0;r<LoadingTimesView.Count;r++)
				//23.01.07 -{
				int r=0;//23.01.07

					int rcounter = 0;
					while ((resourceID[rcounter])!=(int)(LoadingTimesView[r]["Resource_ID"]))
					{			
						rcounter++;
					}
					for (int lr=0;lr<resourceLr[rcounter];lr++)
					{
						ulong[] mrscspVariableArray=new ulong[2] {(ulong)resourceID[rcounter],(ulong)lr+1};
						sr.WriteLine("{0} - The Vr={1},l={2}",VariblesVectorJFIMRL.Count,resourceID[rcounter],lr+1);
						creatMrscspVariable(ref Mrscsp, mrscspVariableArray);
						VariblesVectorJFIMRL.Add(Mrscsp);
						LindoVarType.Append("C");
						sr.WriteLine("Now={0} In={1}",startIndexVrlInVariableVector+counterVrlInVariablesVector,VariblesVectorJFIMRL.IndexOf(Mrscsp));
						sr.WriteLine("Now={0} In={1}",startIndexVrlInVariableVector+counterVrlInVariablesVector,VariblesVectorJFIMRL.IndexOf(Mrscsp,(int)startIndexVrlInVariableVector));
						counterVrlInVariablesVector++;
					}
				//23.01.07 -}
			}
			#endregion
            */

            // shay constraint delay view
            /*
			#region Creating the Zrl Variables + Adding them to VariblesVectorJFIMRL

			// *** Creating the Zrl Variables + Adding them to VariblesVectorJFIMRL
			startIndexZrlInVariableVector = startIndexVrlInVariableVector + counterVrlInVariablesVector;
			sr.WriteLine("Zrl VARIABLES :");
			DataView ConstantDelaysView = new DataView(dataSet.ConstantDelays);			
			
			if (ConstantDelaysView.Count>0)
			{					
				for (int r=0;r<ConstantDelaysView.Count;r++)
				{
					int rcounter = 0;
					while ((resourceID[rcounter])!=(int)(ConstantDelaysView[r]["Resource_ID"]))
					{			
						rcounter++;
					}
					bool zFirst = false;
					for (int lr=0;lr<resourceLr[rcounter];lr++)
					{
						if (zFirst==true)
						{
							ulong[] mrscspVariableArray=new ulong[2] {(ulong)resourceID[rcounter],(ulong)lr+1};
							sr.WriteLine("{0} - The Zr={1},l={2}",VariblesVectorJFIMRL.Count,resourceID[rcounter],lr+1);
							creatMrscspVariable(ref Mrscsp, mrscspVariableArray);
							VariblesVectorJFIMRL.Add(Mrscsp);
							LindoVarType.Append("C");
							sr.WriteLine("Counter is{0}",VariblesVectorJFIMRL.Count);
							sr.WriteLine("Now={0} In={1}",startIndexZrlInVariableVector+counterZrlInVariablesVector,VariblesVectorJFIMRL.IndexOf(Mrscsp));
							sr.WriteLine("Now={0} In={1}",startIndexZrlInVariableVector+counterZrlInVariablesVector,VariblesVectorJFIMRL.IndexOf(Mrscsp,(int)startIndexZrlInVariableVector));
							counterZrlInVariablesVector++;
						}
						zFirst=true;
					}
				}
			}
			#endregion
            */

            // no complement shay dont know
            /*
			#region Creating the Yni Variables + Adding them to VariblesVectorJFIMRL

			// *** Creating the Yni Variables + Adding them to VariblesVectorJFIMRL
			startIndexYniInVariableVector = startIndexZrlInVariableVector + counterZrlInVariablesVector;
			sr.WriteLine("Yni VARIABLES :");
			// *** Calculationg the modes of each operation
			DataView complementView = new DataView(dataSet.Complementaries);
			if (complementView.Count!=0)
			{
				for (int comp=0;comp<(int)complementView[complementView.Count-1]["Complementary_ID"];comp++)
				{
					ulong[] mrscspVariableArray=new ulong[1] {(ulong)(comp+1)};
					sr.WriteLine("The Yni={0} Var#={1} inVector#={2}",comp+1,startIndexYniInVariableVector+counterYniInVariablesVector,VariblesVectorJFIMRL.Count);
					sr.WriteLine("The Yni={0}",comp+1);
					creatMrscspVariable(ref Mrscsp, mrscspVariableArray);
					VariblesVectorJFIMRL.Add(Mrscsp);
					LindoVarType.Append("B");
					counterYniInVariablesVector++;				
				}
			}
			#endregion

             *
			#region Creating the F Variable  + Adding it to VariblesVectorJFIMRL

			// *** Creating the F Variable + Adding it to VariblesVectorJFIMRL
			sr.WriteLine("The F Variable Created :");
			VariblesVectorJFIMRL.Add("F");
			LindoVarType.Append("C");			
			#endregion

			#region SP_Environment_Creation
			DataView FamilyView = new DataView(dataSet.Families);
			DataView FamilyOperationView = new DataView(dataSet.OperationsToFamilies);
			DataView JobsView = new DataView(dataSet.Jobs);
			DataView ModesView = new DataView(dataSet.Modes);
			DataView PrecView = new DataView(dataSet.Precedence);
			DataView ResUsageView = new DataView(dataSet.ResourceUsage);
            
            
			SP_Creator.SP_Environment spEnvi = new SP_Creator.SP_Environment( sr,yjfim_One_Mode, myResourceDataTable,myResourceJobDataTable, myResourceFamilyDataTable, ref FamilyView,ref FamilyOperationView, ref JobsView, ref ModesView,
				PrecView, ref VariblesVectorJFIMRL, ref ResUsageView, ref resourceID, ref resourceKr, ref resourceLr);
			
          */
			

			#region Testing Ideas

			/*Console.WriteLine("Creating The Arrays of the Max Modes Of Operations");
			SortedList mySL = new SortedList();

			int[] operationID = new int[dataSet.Operations.Count];
			int[] operationMaxMode = new int[dataSet.Operations.Count];
			DataView MaxModesView = new DataView(dataSet.Modes);
			for (int oper=0;oper<dataSet.Operations.Count;oper++)
			{				
				MaxModesView.RowFilter = "Operation_ID ="+ dataSet.Operations[oper].Operation_ID;
				//Console.WriteLine("oper={0}",oper);
				//mySL.Add(MaxModesView.Count,dataSet.Operations[oper].Operation_ID);
				
				operationID[oper] = dataSet.Operations[oper].Operation_ID;
				operationMaxMode[oper] = MaxModesView.Count;
				Console.WriteLine("The operation {0} Has {1}",operationID[oper],operationMaxMode[oper]);
			}
			Array.Sort(operationMaxMode,operationID);
			PrintKeysAndValues(operationMaxMode,operationID);
			Console.WriteLine("***");
			Array.Sort(operationID,operationMaxMode);
			PrintKeysAndValues(operationID,operationMaxMode);
			Console.WriteLine("---");
			PrintKeysAndValues(operationMaxMode,operationID);			 

			//Lindos_Vectors lindos_Vector = new Lindos_Vectors(VariblesVectorJFIMRL.Count);			
			/*ShortStringDictionary s = new ShortStringDictionary();
			Classroom MyClass = new Classroom();
			MyClass.Add_Student("yariv",027487040);
			MyClass.Add_Student("Uri",0274434440);
			Student[] Mystudent = new Student[10];
			Mystudent[0] = new Student("lolo",54545);
			Mystudent[0].AddStudent_Flowers("Savion");
			Mystudent[0].AddStudent_Flowers("Rakefet");*/
			sr.WriteLine("Creating The Three Vectors for Lindo");						
			VariableVector[] lindoVariableVector = new VariableVector[VariblesVectorJFIMRL.Count];
			for (int i=0;i<VariblesVectorJFIMRL.Count;i++)
			{
				lindoVariableVector[i] = new VariableVector();
			}
			
			
			#endregion

			
			#region Creating Constraints No. 1	
 
			///<summary>
			/// Creating Constraints No. 1	
			///</summary>
			///<param name = "constraintsCounter">Counter of the the number of constraints in the Matrix</param>
			 
			sr.WriteLine("\n\t\tThe constraints 1 creation:\n");		
			sr.WriteLine("\t\t=============================");			
		    for (int i = 0;i< problem.Products.Length;i++)
		   	{				
				for (int k=0;k< problem.Products[i].Size;k++)
				{
					foreach (Step s in (System.Collections.ArrayList)problem.StepsInProduct[problem.Products[i]])
					{
						foreach (Mode m in (System.Collections.ArrayList)problem.ModesInStep[s])
						{
				    		vParameterIdentifier = 999;
                            vJob = k;
                            vFamily = problem.Products[i].Id;
                            vOperation = s.Id;
                            vMode = m.Id;
							ulong[] mrscspVariableArray=new ulong[5] {(ulong)vParameterIdentifier,(ulong)vJob,(ulong)vFamily,(ulong)vOperation,(ulong)vMode};
							creatMrscspVariable(ref Mrscsp, mrscspVariableArray);
							if (!VariblesVectorJFIMRL.Contains(Mrscsp))
							{
								sr.WriteLine("BBBUUUGGGG  1");
							} else
							{	
								ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp)]=1;
								lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp)].AddCoefficient(constraintsCounter,1);
								RightHandSideValues[constraintsCounter] = 1;
								ConstraintsSenses[constraintsCounter] = 'E';			
							}
						}
						constraintsCounter++;
					}
							
				}			
			}				
			#endregion

			#region Creating Constraints No. 2
				
			sr.WriteLine("\n\t\tThe constraints 2 creation:\n");		
			sr.WriteLine("\t\t=============================");
			YxNetLinks YxNetLinksMRCCSP = new YxNetLinks(counterYjfimInVariablesVector);
			ArrayList YxNetList = new ArrayList();
			ArrayList YxNetListResource = new ArrayList();
			int YjfimVarIndex = 0;            

			 for (int i = 0;i< problem.Products.Length;i++)
		   	{				
				for (int k=0;k< problem.Products[i].Size;k++)
				{
					foreach (Step s in (System.Collections.ArrayList)problem.StepsInProduct[problem.Products[i]])
					{
                        
						foreach (Mode m in (System.Collections.ArrayList)problem.ModesInStep[s])
						{
                            foreach (Operation o in m.operations) {
								vParameterIdentifier = 999;
							    vJob = k;
								vFamily = problem.Products[i].Id;
								vOperation = s.Id;
								vMode = m.Id;
                                vResource = o.Rseource.Id;
								sr.WriteLine("The {0} Constraint has the following Varibles:",constraintsCounter);								
								//************************
								// *** Creating the Yjfim
								//************************
								ulong[] mrscspYjfimVariableArray=new ulong[5] {(ulong)vParameterIdentifier,(ulong)vJob,(ulong)vFamily,(ulong)vOperation,(ulong)vMode };
								creatMrscspVariable(ref Mrscsp, mrscspYjfimVariableArray);
								if (!VariblesVectorJFIMRL.Contains(Mrscsp))
								{
									sr.WriteLine("BBBUUUGGGG  2");
								}
								else
								{
									ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp)]=1;
									lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp)].AddCoefficient(constraintsCounter,1);
									YjfimVarIndex = VariblesVectorJFIMRL.IndexOf(Mrscsp);
									RightHandSideValues[constraintsCounter] = 0;
									ConstraintsSenses[constraintsCounter] = 'E';
									sr.WriteLine("{0} - Yj={1},f={2},i={3},m={4}",VariblesVectorJFIMRL.IndexOf(Mrscsp),vJob,vFamily,vOperation,vMode  );
									//System.Console.WriteLine("The {0} Constraint has the following Varibles:{1}",constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp));							
								}
								// *** find the resources that mode r usesto know what is its Lr according to 
								// *** the resourceID & resourceLr Arrays
								int index1=0;
								while ( resourceID[index1]!= o.Rseource.Id)
									index1++;
					            for (int lr=1;lr<=resourceLr[index1];lr++)
								{
									//*************************
									// *** Creating the Xjfimrl
									//*************************
									ulong[] mrscspXjfimrlVariableArray=new ulong[6] {(ulong)vJob,(ulong)vFamily,(ulong)vOperation,(ulong)vMode,(ulong)vResource,(ulong)lr };
									creatMrscspVariable(ref Mrscsp, mrscspXjfimrlVariableArray);
									if (!VariblesVectorJFIMRL.Contains(Mrscsp))
									{
										sr.WriteLine("BUUUG  2");
									}
									else
									{
										ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp)]=-1;
										lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp)].AddCoefficient(constraintsCounter,-1);
										YxNetList.Add(VariblesVectorJFIMRL.IndexOf(Mrscsp));
										YxNetListResource.Add(vResource);											
										sr.WriteLine("{0} - Xj={1},f={2},i={3},m={4},r={5},Lr={6}",VariblesVectorJFIMRL.IndexOf(Mrscsp),vJob,vFamily,vOperation,vMode,vResource,lr );							
									}										 										 
								}
								sr.WriteLine("Constraint {0} IS {1} TO {2}",constraintsCounter,ConstraintsSenses[constraintsCounter],RightHandSideValues[constraintsCounter]);
								constraintsCounter++;									
							}
							YxNetLinksMRCCSP.AddLink(YjfimVarIndex,YxNetList,YxNetListResource,(constraintsCounter-1));
							//Console.WriteLine("YjfimVarIndex={0}",YjfimVarIndex);
							YxNetList.Clear();
							YxNetListResource.Clear();
						}
					}
				}
			}
								
			#endregion
		 
			#region Creating Constraints No. 3
             /*
			// *** Creating Constraints No. 3		
				//Console.WriteLine("\n\t\tThe Constraints 3 Creation:\n");				
				//Console.WriteLine("\t\t=============================");
			sr.WriteLine("\n\t\tThe Constraints 3 Creation:\n");				
			sr.WriteLine("\t\t=============================");
			sbyte equation3CoefficientPlus = new sbyte(); // the coefiicient of the mrscsp variable -1 OR 1
			sbyte equation3CoefficientMinus = new sbyte();
			equation3CoefficientPlus=1;
			equation3CoefficientMinus=-1;
			vParameterIdentifier = 998;	
			int countBatchResource = 0;
			bool ifResourceIsBatch = false;
			Batch_Resource_Yimrl_Links batch_Res_Yimrl_Links;
			ResourceYimrlLinks res_Yimrl_links;
			//if (countBatchResource>0)
			//{}
				batch_Res_Yimrl_Links = new Batch_Resource_Yimrl_Links(problem.Resources.Length);//030707countBatchResource);				
				//arr_Batch_Resource = new ResourceYimrlLinks[countBatchResource];
			sr.WriteLine("");
			sr.WriteLine("There are {0} Batch Resources",countBatchResource);
			sr.WriteLine("");
			int batchResCounter = -1;
			int YimrlVarIndex;
			ArrayList YimrlList = new ArrayList();
			for (byte r=0;r< problem.Resources.Length;r++)
			{	
				res_Yimrl_links = new ResourceYimrlLinks(resourceLr[r], resourceID[r]);
		//030707		if (dataSet.Resources[r].Capacity_Kr>1)
		//030707		{
		//030707			ifResourceIsBatch = true;
					batchResCounter++;					
		//030707		}
		//030707		else
		//030707		{
		//030707			ifResourceIsBatch = false;
		//030707		}  
                
		    //  DataRow[]resourceResourceUsage =
			//		dataSet.Resources[r].GetChildRows(resourceResourcesUsageRelation);    									
				for (int task=0;task<resourceLr[r];task++)
				{
					sr.WriteLine("The {0} Constraint has the following Varibles:",constraintsCounter);
						
					for (int imCount=0;imCount<resourceResourceUsage.Length;imCount++)
					{			
                        
						int i_operation=(int)resourceResourceUsage[imCount].ItemArray[1];
						int m_mode=(int)resourceResourceUsage[imCount].ItemArray[2];
						//Console.WriteLine("The {0} Constraint has the following Varibles:",constraintsCounter);
						//Console.WriteLine("i={0},m={1},r={2},l={3}\n", resourceResourceUsage[imCount].ItemArray[1], resourceResourceUsage[imCount].ItemArray[2],dataSet.Resources[r].Resource_ID,task+1);
						//21.01.07//sr.WriteLine("i={0},m={1},r={2},l={3}\n", resourceResourceUsage[imCount].ItemArray[1], resourceResourceUsage[imCount].ItemArray[2],dataSet.Resources[r].Resource_ID,task+1);
						
						// **************************************
						// ********** Creating the Yimrl  *******
						// **************************************
						if (task!=0)  // if its not the first equation of the resource r
						{							
							ulong[] mrscspYimrlVariableArray=new ulong[5] {(ulong)vParameterIdentifier,(ulong)i_operation,(ulong)m_mode,(ulong)dataSet.Resources[r].Resource_ID,(ulong)(task+1) };														
							creatMrscspVariable(ref Mrscsp, mrscspYimrlVariableArray);
							if (!VariblesVectorJFIMRL.Contains(Mrscsp))
							{
								Console.WriteLine("BBBUUUGGGG   3");
								sr.WriteLine("BBBUUUGGGG   3");
							}
							else
							{
								ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp)]=equation3CoefficientPlus;
								lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp)].AddCoefficient(constraintsCounter,equation3CoefficientPlus);
								YimrlVarIndex = VariblesVectorJFIMRL.IndexOf(Mrscsp);
								YimrlList.Add(YimrlVarIndex);								
	
								RightHandSideValues[constraintsCounter] = 0;
								ConstraintsSenses[constraintsCounter] = 'L';
								//Console.WriteLine("constraintsCounter={0}   Mrscsp={1}  equation3CoefficientPlus={2}",constraintsCounter,Mrscsp,equation3CoefficientPlus);
								if (task+1<resourceLr[r])
								{
									//System.Console.WriteLine("task{0}<resourceLr{1}/t",task,resourceLr[r]);
									constraintsCounter++;
									ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp)]=equation3CoefficientMinus;
									//???18.01.07
									lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp)].AddCoefficient(constraintsCounter,equation3CoefficientMinus);
									//Console.WriteLine("constraintsCounter={0}  Mrscsp={1}  equation3CoefficientMinus={2}",constraintsCounter,Mrscsp,equation3CoefficientMinus);
									constraintsCounter--;
								}
							}
						}											
						else
						{
							equation3CoefficientPlus=1;						
							ulong[] mrscspYimrlVariableArray=new ulong[5] {(ulong)vParameterIdentifier,(ulong)i_operation,(ulong)m_mode,(ulong)dataSet.Resources[r].Resource_ID,(ulong)(task+1) };
							creatMrscspVariable(ref Mrscsp, mrscspYimrlVariableArray);
							if (!VariblesVectorJFIMRL.Contains(Mrscsp))
							{
								sr.WriteLine("BBBUUUGGGG   3");
							}
							else
							{
								ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp)]=equation3CoefficientPlus;
								lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp)].AddCoefficient(constraintsCounter,equation3CoefficientPlus);
								YimrlVarIndex = VariblesVectorJFIMRL.IndexOf(Mrscsp);
								YimrlList.Add(YimrlVarIndex);	
								RightHandSideValues[constraintsCounter] = 1;
								ConstraintsSenses[constraintsCounter] = 'L';
								//Console.WriteLine("constraintsCounter={0} Mrscsp={1}",constraintsCounter,Mrscsp);
								constraintsCounter++;	
								ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp)]=equation3CoefficientMinus;
								lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp)].AddCoefficient(constraintsCounter,equation3CoefficientMinus);
								
								//Console.WriteLine("constraintsCounter={0} Mrscsp={1}",constraintsCounter,Mrscsp);
								constraintsCounter--;
							}
						}				
					}
	//030707				if (ifResourceIsBatch==true)
	//030707				{
					res_Yimrl_links.Add_Task_Link(task,YimrlList,constraintsCounter);
	//030707				}
					//Console.WriteLine("Constraint {0} IS {1} TO {2}",constraintsCounter,ConstraintsSenses[constraintsCounter],RightHandSideValues[constraintsCounter]);
					YimrlList.Clear();
					constraintsCounter++;
				}
	//030707			if (ifResourceIsBatch==true)
	//030707			{										
				batch_Res_Yimrl_Links.Add_Yimrl_Links(batchResCounter, res_Yimrl_links, resourceLr[r], resourceID[r]);
	//030707			}
				
			}
			sr.WriteLine("PRINTING THE -  Y i m r l");
			batch_Res_Yimrl_Links.Print(sr);

		     */
            #endregion

             #region Creating Constraints No. 4

         /*
			sr.WriteLine("\n\n\t\tThe Constraints 4 Creation:\n");	
			sr.WriteLine("\t\t=============================");
			int Kfr = new int();
			int Ujf = new int();
			int MultipleKfrUjf = new int();
			int Kr  = new int();
			vParameterIdentifier = 998;  //{ Remember That vParameterIdentifier = 998 }
			for (int r=0; r < problem.Resources.Length; r++)
			{
                Kr = resourceKr[r];
								
				for (int task=0;task<resourceLr[r];task++)
				{
                    
					for (int imCount=0;imCount<resourceResourceUsage.Length;imCount++)
					{			
						// **** Creating the Yimrl variables { Remember That vParameterIdentifier = 998 }
						int i_operation=(int)resourceResourceUsage[imCount].ItemArray[1];
						int m_mode=(int)resourceResourceUsage[imCount].ItemArray[2];
						//Console.WriteLine("The {0} Constraint has the following Varibles:",constraintsCounter);
						//Console.WriteLine("                    i={0},m={1},r={2},l={3}\n", resourceResourceUsage[imCount].ItemArray[1], resourceResourceUsage[imCount].ItemArray[2],dataSet.Resources[r].Resource_ID,task+1);
						sr.WriteLine("The {0} Constraint has the following Varibles:",constraintsCounter);
						sr.WriteLine("                    i={0},m={1},r={2},l={3}\n", resourceResourceUsage[imCount].ItemArray[1], resourceResourceUsage[imCount].ItemArray[2],dataSet.Resources[r].Resource_ID,task+1);
						
						ulong[] mrscspYimrlVariableArray=new ulong[5] {(ulong)vParameterIdentifier,(ulong)i_operation,(ulong)m_mode,(ulong)dataSet.Resources[r].Resource_ID,(ulong)(task+1) };														
						creatMrscspVariable(ref Mrscsp, mrscspYimrlVariableArray);
						if (!VariblesVectorJFIMRL.Contains(Mrscsp))
						{
							sr.WriteLine("BBBUUUGGGG   4");
							Console.WriteLine("BBBUUUGGGG   4");
						}
						else
						{
							ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp)]=1;
							lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp)].AddCoefficient(constraintsCounter,1);
								
							RightHandSideValues[constraintsCounter] = 0;
							ConstraintsSenses[constraintsCounter] = 'L';
							//		System.Console.Write("Index=======>>>{0}",VariblesVectorJFIMRL.IndexOf(Mrscsp));
							constraintsCounter++;
							ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp)]=(-1*Kr);
							lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp)].AddCoefficient(constraintsCounter,(-1*Kr));
							RightHandSideValues[constraintsCounter] = 0;
							ConstraintsSenses[constraintsCounter] = 'L';
							//		System.Console.WriteLine("(-1*Kr)=======>>>{0}====Counter={1}",-1*Kr,constraintsCounter);
							constraintsCounter--;
						}
						// **** Creating the Xjfimrl variables
						for (int i=0;i<dataSet.Families.Rows.Count;i++)
						{
							DataRow[]operationsToFamilies =
								dataSet.Families[i].GetChildRows(familiesOperationsToFamiliesRelation);
							OperationFamilyList.Clear();
							for (int oper=0;oper<operationsToFamilies.Length;oper++)
							{
								OperationFamilyList.Add(operationsToFamilies[oper].ItemArray[2]);
								//IStartfList.Add(dataSet.OperationsToFamilies[oper].Operation_ID);					
							}
							
							if (!OperationFamilyList.Contains(i_operation))
							{
								//Console.WriteLine("BUUUG {0} F={1}",i_operation,dataSet.Families[i].Family_ID);
							}
							else
							{
								//Console.WriteLine("YESSSSSSSSS {0} F={1}",i_operation,dataSet.Families[i].Family_ID);
								DataRow[]jobs =
									dataSet.Families[i].GetChildRows(familiesJobesRelation);
								DataRow[]familyCapacityOnResource = 
									dataSet.Families[i].GetChildRows(familiesFamilyCapacityOnResourceRelation);
								for (int k=0;k<jobs.Length;k++)
								{
									Ujf=(int)dataSet.Jobs[k].Units;
									
									sr.WriteLine("j={0}, f={1}, i={2}, m={3}, r={4}, l={5}, Kr={6}, Ujf={7}\n",dataSet.Jobs[k].Job_ID,dataSet.Families[i].Family_ID ,resourceResourceUsage[imCount].ItemArray[1], resourceResourceUsage[imCount].ItemArray[2],dataSet.Resources[r].Resource_ID,task+1,Kr,Ujf);
									
									for (int ResCapacityIndex=0;ResCapacityIndex<familyCapacityOnResource.Length;ResCapacityIndex++)
									{
										if (dataSet.Resources[r].Resource_ID==dataSet.FamilyCapacityOnResource[ResCapacityIndex].Resource_ID)
										{
											//				Console.WriteLine("Kfr={0}  r={1}",dataSet.FamilyCapacityOnResource[ResCapacityIndex].Family_Capacity_kfr,dataSet.Resources[r].Resource_ID);                
											Kfr=(int)dataSet.FamilyCapacityOnResource[ResCapacityIndex].Family_Capacity_kfr;
										}
									}
									// *** Creating The Xjfimrl Variable 
									
									ulong[] mrscspXjfimrlVariableArray=new ulong[6] {(ulong)dataSet.Jobs[k].Job_ID,(ulong)dataSet.Families[i].Family_ID ,(ulong)i_operation,(ulong)m_mode,(ulong)dataSet.Resources[r].Resource_ID,(ulong)(task+1)};
									creatMrscspVariable(ref Mrscsp, mrscspXjfimrlVariableArray);
									if (!VariblesVectorJFIMRL.Contains(Mrscsp))
									{
										Console.WriteLine("BUUUG  4");
										sr.WriteLine("BUUUG  4");
									}
									else
									{
										MultipleKfrUjf= -1*(Kfr*Ujf);
										//System.Console.Write("MultipleKfrUjf={0}---",MultipleKfrUjf);
										ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp)]=MultipleKfrUjf;
										//sr.WriteLine("Mrscsp={0}, MultipleKfrUjf={1}",VariblesVectorJFIMRL.IndexOf(Mrscsp),MultipleKfrUjf);
										lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp)].AddCoefficient(constraintsCounter,MultipleKfrUjf);
								
										//System.Console.WriteLine("Index={0}--->Counter={1}",VariblesVectorJFIMRL.IndexOf(Mrscsp),constraintsCounter);
										constraintsCounter++;
										ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp)]=(Kfr*Ujf);
										lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp)].AddCoefficient(constraintsCounter,Kfr*Ujf);
										//System.Console.WriteLine("Index={0}--->Counter={1}",VariblesVectorJFIMRL.IndexOf(Mrscsp),constraintsCounter);
										constraintsCounter--;						
									}								
								}
							}
						}
						constraintsCounter++;
						constraintsCounter++;
						//Console.WriteLine("*** CounterConstraints={0}",constraintsCounter);
					}					
				}
			}			
           */
			#endregion

			#region Creating Constraints No. 5 THE COMPLEMENTARY + COMPLEMENTARY CHECK
			/*
			// *** Creating Constraints No. 5		THE COMPLEMENTARY 
			sr.WriteLine("\n\t\tThe Constraints 5 Creation:\n");	
			sr.WriteLine("\t\t=============================");
			ArrayList compList = new ArrayList();			
			int mode = new int();
			int operation = new int();
			int family = new int();
			int job = new int();
			for (int f=0;f<dataSet.Families.Count;f++)
			{
				sr.WriteLine("f={0} - {1}",f,(int)dataSet.Families[f].Family_ID);
				compList = new ArrayList();
				DataView complement_1View = new DataView(dataSet.Complementaries);
				DataView complement_2View = new DataView(dataSet.Complementaries);	
				complement_1View.RowFilter = "Family_ID =" + (int)dataSet.Families[f].Family_ID;
				DataView jobs1View = new DataView(dataSet.Jobs);
				if (complement_1View.Count!=0)
				{
					int comp1End = (int)complement_1View[complement_1View.Count-1]["Complementary_ID"];					
					for (int comp1=0;comp1<comp1End;comp1++)
					{						
						complement_2View.RowFilter = "Complementary_ID =" + (comp1+1)+ "AND Family_ID =" + (int)dataSet.Families[f].Family_ID;
						if (complement_2View.Count!=0)
						{
							for (int comp2=0;comp2<complement_2View.Count;comp2++)
							{
								// *** Creating the Yjfim
								//	sr.Write("\tYj={0},f={1},i={2},m={3}",complement_2View[comp2]["Job_ID"],complement_2View[comp2]["Family_ID"],complement_2View[comp2]["Operation_ID"],complement_2View[comp2]["Mode_ID"]);
								vParameterIdentifier = 999;
								mode = (int)complement_2View[comp2]["Mode_ID"];
								operation = (int)complement_2View[comp2]["Operation_ID"];
								family = (int)complement_2View[comp2]["Family_ID"];
								jobs1View.RowFilter = "family_ID =" + family;
								for (int j=0;j<jobs1View.Count;j++)
								{
									job = (int)jobs1View[j]["Job_ID"];//omplement_2View[comp2]["Job_ID"];
									sr.Write("\tYj={0},f={1},i={2},m={3}",job,family,operation,mode);
									ulong[] mrscspYjfimVariableArray=new ulong[5] {(ulong)vParameterIdentifier,(ulong)job,(ulong)family,(ulong)operation,(ulong)mode };
									creatMrscspVariable(ref Mrscsp, mrscspYjfimVariableArray);							
									if (!VariblesVectorJFIMRL.Contains(Mrscsp))
									{
										sr.WriteLine("BBBUUUGGGG  5 Yjfim");
										Console.WriteLine("BBBUUUGGGG  5 Yjfim");
									}
									else
									{
										compList.Add(VariblesVectorJFIMRL.IndexOf(Mrscsp));
										ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp)]=1;
										lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp)].AddCoefficient(constraintsCounter,1);								
									}
								}
							}							
							// *** Creating the Yni
							ulong[] mrscspVariableArray=new ulong[1] {(ulong)(comp1+1)};
							creatMrscspVariable(ref Mrscsp, mrscspVariableArray);
							if (!VariblesVectorJFIMRL.Contains(Mrscsp))
							{
								sr.WriteLine("BBBUUUGGGG  5 ni");
							}
							else
							{
								ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp)]=((-1)*complement_2View.Count);
								lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp)].AddCoefficient(constraintsCounter,((-1)*complement_2View.Count));
							}
							RightHandSideValues[constraintsCounter] = 0;
							ConstraintsSenses[constraintsCounter] = 'E';
							//Console.WriteLine("{0} - the Yjfim ; j={1},f={2},i={3},m={4}",VariblesVectorJFIMRL.IndexOf(Mrscsp),dataSet.Jobs[k].Job_ID,dataSet.Families[i].Family_ID,dataSet.OperationsToFamilies[g].Operation_ID,j+1 );
							sr.WriteLine("The {0} Constraint has the following Varibles:{1}",constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp));							
							sr.WriteLine("\nConstraint {0} IS {1} TO {2}",constraintsCounter,ConstraintsSenses[constraintsCounter],RightHandSideValues[constraintsCounter]);
							constraintsCounter++;	
						}
					}
				}
				spEnvi.ComplementaryCheck(sr, compList, f);				 
			}		
			compList.RemoveRange(0,compList.Count);
            */
			#endregion
	 
			#region Creating Constraints No. 6	THE EXCLUSIVE    	+ EXCLUSIVE CHECK	
            /*
			// *** Creating Constraints No. 6		
			sr.WriteLine("\n\t\tThe Constraints 6 Creation:\n");	
			sr.WriteLine("\t\t=============================");
			ArrayList exclusList = new ArrayList();
			for (int f=0;f<dataSet.Families.Count;f++)
			{
				sr.WriteLine("f={0} - {1}",f,(int)dataSet.Families[f].Family_ID);
				exclusList = new ArrayList();
				DataView exclusive_1View = new DataView(dataSet.Exclusives);
				DataView exclusive_2View = new DataView(dataSet.Exclusives);
				exclusive_1View.RowFilter = "Family_ID =" + (int)dataSet.Families[f].Family_ID;
				DataView jobs1View = new DataView(dataSet.Jobs);
				if (exclusive_1View.Count!=0)
				{
					int exclusive_1Counter = (int)exclusive_1View[exclusive_1View.Count-1]["Exclusive_ID"];
					for (int exclu1=0;exclu1<exclusive_1Counter;exclu1++)
					{						
						exclusive_2View.RowFilter = "Exclusive_ID =" + (exclu1+1)+ "AND Family_ID =" + (int)dataSet.Families[f].Family_ID;						
						if (exclusive_2View.Count!=0)
						{
							for (int exclu2=0;exclu2<exclusive_2View.Count;exclu2++)
							{
								// *** Creating the Yjfim
								//sr.Write("\tj={0},f={1},i={2},m={3}",exclusive_2View[exclu2]["Job_ID"],exclusive_2View[exclu2]["Family_ID"],exclusive_2View[exclu2]["Operation_ID"],exclusive_2View[exclu2]["Mode_ID"]);
								vParameterIdentifier = 999;
								mode = (int)exclusive_2View[exclu2]["Mode_ID"];
								operation = (int)exclusive_2View[exclu2]["Operation_ID"];
								family = (int)exclusive_2View[exclu2]["Family_ID"];
								jobs1View.RowFilter = "family_ID =" + family;
								//job = (int)exclusive_2View[exclu2]["Job_ID"];
								for (int j=0;j<jobs1View.Count;j++)
								{
									job = (int)jobs1View[j]["Job_ID"]; 
									ulong[] mrscspYjfimVariableArray=new ulong[5] {(ulong)vParameterIdentifier,(ulong)job,(ulong)family,(ulong)operation,(ulong)mode };
									creatMrscspVariable(ref Mrscsp, mrscspYjfimVariableArray);
									if (!VariblesVectorJFIMRL.Contains(Mrscsp))
									{
										sr.WriteLine("BBBUUUGGGG  6 Yjfim");
									}
									else
									{
										sr.Write("\tYj={0},f={1},i={2},m={3}={4}",job,family,operation,mode,VariblesVectorJFIMRL.IndexOf(Mrscsp));									
										exclusList.Add(VariblesVectorJFIMRL.IndexOf(Mrscsp));
										ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp)]=1;
										lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp)].AddCoefficient(constraintsCounter,1);				
									} 
								}
							}
							sr.WriteLine("");                    					 
							RightHandSideValues[constraintsCounter] = 1;
							ConstraintsSenses[constraintsCounter] = 'L';
							//Console.WriteLine("{0} - the Yjfim ; j={1},f={2},i={3},m={4}",VariblesVectorJFIMRL.IndexOf(Mrscsp),dataSet.Jobs[k].Job_ID,dataSet.Families[i].Family_ID,dataSet.OperationsToFamilies[g].Operation_ID,j+1 );
							//System.Console.WriteLine("The {0} Constraint has the following Varibles:{1}",constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp));							
							sr.WriteLine("\nConstraint {0} IS {1} TO {2}",constraintsCounter,ConstraintsSenses[constraintsCounter],RightHandSideValues[constraintsCounter]);
							constraintsCounter++;	
						}
					}
				}
				if (exclusList.Count>0)
				{
					spEnvi.ExclusiveCheck(sr, exclusList);
				}
			}
            */
			#endregion			

			#region Creating Constraints No. 7 THE EXCLUSIVE FAMILIES 1		
            /*
			// *** Creating Constraints No. 7****	The Variables In This Section Are Based On The Section of Yfimrl Variables  
			sr.WriteLine("\n\t\tThe Constraints 7 Creation:\n");	
			sr.WriteLine("\t\t=============================");
			exclusiveFamilyMainView = new DataView(dataSet.Exclusive_Families);
			if (exclusiveFamilyMainView.Count!=0)
			{
				int exclusiveFamilyCounter = (int)exclusiveFamilyMainView[exclusiveFamilyMainView.Count-1]["Excusive_Families_ID"];
				for (int excluFam1=0;excluFam1<exclusiveFamilyCounter;excluFam1++)
				{
					DataView exclusiveFamilySecondView = new DataView(dataSet.Exclusive_Families);			
					exclusiveFamilySecondView.RowFilter = "Excusive_Families_ID =" + (excluFam1+1);
					int index1=0;
					while ( resourceID[index1]!=((int)exclusiveFamilySecondView[0]["Resource_ID"]))
						index1++;
					for (int lr=1;lr<=resourceLr[index1];lr++)
					{	
						// *** Creating the Constraints Variables of Yfimrl	
						vOperation = (int)exclusiveFamilySecondView[0]["Operation_ID"];
						vMode = (int)exclusiveFamilySecondView[0]["Mode_ID"];
						vResource = (int)exclusiveFamilySecondView[0]["Resource_ID"];						
						for (int excluFam2=0;excluFam2<exclusiveFamilySecondView.Count;excluFam2++)
						{																									
							vFamily = (int)exclusiveFamilySecondView[excluFam2]["Family_ID"];
							sr.WriteLine("f={0},i={1},m={2},r={3},l={4}",vFamily,vOperation,vMode,vResource,lr);
							ulong[] mrscspVariableArray=new ulong[5] {(ulong)vFamily,(ulong)vOperation,(ulong)vMode,(ulong)vResource,(ulong)lr };
							creatMrscspVariable(ref Mrscsp, mrscspVariableArray);							
							if (!VariblesVectorJFIMRL.Contains(Mrscsp))
							{
								sr.WriteLine("BBBUUUGGGG  7 Yjfim");
							}
							else
							{
								ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp)]=1;
								lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp)].AddCoefficient(constraintsCounter,1);				
							} 
						}							                    					 
							RightHandSideValues[constraintsCounter] = 1;
							ConstraintsSenses[constraintsCounter] = 'L';
							sr.WriteLine("Constraint {0} IS {1} TO {2}\n",constraintsCounter,ConstraintsSenses[constraintsCounter],RightHandSideValues[constraintsCounter]);
							constraintsCounter++;
					}
				}				
			}	
              */
			#endregion

			#region Creating Constraints No. 8 THE EXCLUSIVE FAMILIES 2	
		    /*
			// *** Creating Constraints No. 8 ****	The Variables In This Section Are Based On The Section of Yfimrl Variables  
			sr.WriteLine("\n\t\tThe Constraints 8 Creation:\n");	
			sr.WriteLine("\t\t=============================");
			exclusiveFamilyMainView = new DataView(dataSet.Exclusive_Families);
			if (exclusiveFamilyMainView.Count!=0)
			{
				int exclusiveFamilyCounter = (int)exclusiveFamilyMainView[exclusiveFamilyMainView.Count-1]["Excusive_Families_ID"];
				for (int excluFam1=0;excluFam1<exclusiveFamilyCounter;excluFam1++)
				{
					DataView exclusiveFamilySecondView = new DataView(dataSet.Exclusive_Families);			
					exclusiveFamilySecondView.RowFilter = "Excusive_Families_ID =" + (excluFam1+1);
					int index1=0;
					while ( resourceID[index1]!=((int)exclusiveFamilySecondView[0]["Resource_ID"]))
						index1++;
					for (int lr=1;lr<=resourceLr[index1];lr++)
					{	
						// *** Creating the Constraints Variables of Yfimrl	
						vOperation = (int)exclusiveFamilySecondView[0]["Operation_ID"];
						vMode = (int)exclusiveFamilySecondView[0]["Mode_ID"];
						vResource = (int)exclusiveFamilySecondView[0]["Resource_ID"];						
						for (int excluFam2=0;excluFam2<exclusiveFamilySecondView.Count;excluFam2++)
						{																									
							vFamily = (int)exclusiveFamilySecondView[excluFam2]["Family_ID"];
							DataView familyJobs = new DataView(dataSet.Jobs);// To creat in the next Section the j of Xjfimrl
							familyJobs.RowFilter = "Family_ID =" + vFamily;
							sr.WriteLine("f={0},i={1},m={2},r={3},l={4}",vFamily,vOperation,vMode,vResource,lr);
							ulong[] mrscspVariableArray=new ulong[5] {(ulong)vFamily,(ulong)vOperation,(ulong)vMode,(ulong)vResource,(ulong)lr };
							creatMrscspVariable(ref Mrscsp, mrscspVariableArray);							
							if (!VariblesVectorJFIMRL.Contains(Mrscsp))
							{
								sr.WriteLine("BBBUUUGGGG  8 Yjfim");
							}
							else
							{
								ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp)]=((-1)*familyJobs.Count);
								lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp)].AddCoefficient(constraintsCounter,((-1)*familyJobs.Count));				
							} 
							//DataView familyJobs = new DataView(dataSet.Jobs);
							//familyJobs.RowFilter = "Family_ID =" + vFamily;

							// *** Creating the Constraints Variables of Xjfimrl	
							for (int jobCounter=0;jobCounter<familyJobs.Count;jobCounter++)
							{
								vJob=(int)familyJobs[jobCounter]["Job_ID"];
								sr.WriteLine("j={0}",vJob);
								ulong[] mrscspXjfimrlVariableArray=new ulong[6] {(ulong)vJob,(ulong)vFamily ,(ulong)vOperation,(ulong)vMode,(ulong)vResource,(ulong)lr};
								creatMrscspVariable(ref Mrscsp, mrscspXjfimrlVariableArray);
								if (!VariblesVectorJFIMRL.Contains(Mrscsp))
								{
									sr.WriteLine("BUUUG  8");
								}
								else
								{									
									//System.Console.Write("MultipleKfrUjf={0}---",MultipleKfrUjf);
									ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp)]=1;
									lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp)].AddCoefficient(constraintsCounter,1);								
								}
							}							                    					 
						RightHandSideValues[constraintsCounter] = 0;
						ConstraintsSenses[constraintsCounter] = 'L';
						sr.WriteLine("Constraint {0} IS {1} TO {2}\n",constraintsCounter,ConstraintsSenses[constraintsCounter],RightHandSideValues[constraintsCounter]);
						constraintsCounter++;
						}
					}
				}
				
			}	
              */
			#endregion 

			#region Creating Constraints No. 9	THE EXCLUSIVE JOBS		
            /*
			// *** Creating Constraints No. 9  
			sr.WriteLine("\n\t\tThe Constraints 9 Creation:\n");	
			sr.WriteLine("\t\t=============================");
			DataView exclusiveJobsMainView = new DataView(dataSet.Exclusive_Jobs);
			if (exclusiveJobsMainView.Count!=0)
			{
				int exclusiveJobsCounter = (int)exclusiveJobsMainView[exclusiveJobsMainView.Count-1]["Exclusive_Jobs_ID"];
				for (int excluJob1=0;excluJob1<exclusiveJobsCounter;excluJob1++)
				{
					DataView exclusiveJobsSecondView = new DataView(dataSet.Exclusive_Jobs);			
					exclusiveJobsSecondView.RowFilter = "Exclusive_Jobs_ID =" + (excluJob1+1);
					int index1=0;
					while ( resourceID[index1]!=((int)exclusiveJobsSecondView[0]["Resource_ID"]))
						index1++;				
									
					for (int lr=1;lr<=resourceLr[index1];lr++)
					{	
						// *** Creating the Constraints Variables of Xjfimrl	
						vOperation = (int)exclusiveJobsSecondView[0]["Operation_ID"];
						vMode = (int)exclusiveJobsSecondView[0]["Mode_ID"];
						vResource = (int)exclusiveJobsSecondView[0]["Resource_ID"];		
						vFamily = (int)exclusiveJobsSecondView[0]["Family_ID"];
						for (int excluJob2=0;excluJob2<exclusiveJobsSecondView.Count;excluJob2++)
						{							
																								
							vJob = (int)exclusiveJobsSecondView[excluJob2]["Job_ID"];
							sr.WriteLine("j={0},f={1},i={2},m={3},r={4},l={5}",vJob,vFamily,vOperation,vMode,vResource,lr);
							ulong[] mrscspVariableArray=new ulong[6] {(ulong)vJob,(ulong)vFamily,(ulong)vOperation,(ulong)vMode,(ulong)vResource,(ulong)lr };
							creatMrscspVariable(ref Mrscsp, mrscspVariableArray);							
							if (!VariblesVectorJFIMRL.Contains(Mrscsp))
							{
								sr.WriteLine("BBBUUUGGGG  9 Yjfim");
								Console.WriteLine("BBBUUUGGGG  9 Yjfim");
							}
							else
							{
								ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp)]=1;
								lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp)].AddCoefficient(constraintsCounter,1);				
							} 
						}							                    					 
						RightHandSideValues[constraintsCounter] = 1;
						ConstraintsSenses[constraintsCounter] = 'L';
						sr.WriteLine("Constraint {0} IS {1} TO {2}\n",constraintsCounter,ConstraintsSenses[constraintsCounter],RightHandSideValues[constraintsCounter]);
						constraintsCounter++;
					}
				}				
			}	
             */
			#endregion
/*
			#region 	
			spEnvi.EditSpInfo(sr);
			#endregion
*/

			#region Creating Constraints No. 10	
 /*
			// *** Creating Constraints No. 10	
			sr.WriteLine("\n\t\tThe constraints 10 creation:\n");		
			sr.WriteLine("\t\t=============================");
			// ***  Finding The Jobs from each family with Due Date<> from 0
			// ***  { 0, Zero means that there is no due-date to the job }
			for (int i=0;i<dataSet.Families.Rows.Count;i++)
			{
				DataRow[]operationsToFamilies =
					dataSet.Families[i].GetChildRows(familiesOperationsToFamiliesRelation);
				for (int oper=0;oper<operationsToFamilies.Length;oper++)
				{
					//System.Console.WriteLine("Count={0}",dataSet.OperationsToFamilies.Rows.Count);
					//System.Console.WriteLine("Operations={0}",dataSet.OperationsToFamilies[oper].Operation_ID);
					IFinishfList.Add(dataSet.OperationsToFamilies[oper].Operation_ID);
					//IStartfList.Add(dataSet.OperationsToFamilies[oper].Operation_ID);					
				}					
				DataRow[]Precedence =
					dataSet.Families[i].GetChildRows(familiesPrecedenceRelation);
				for (int prec_oper=0;prec_oper< Precedence.Length ;prec_oper++)
				{					
					IFinishfList.Remove(dataSet.Precedence[prec_oper].Previous_Operation_ID);
					//IStartfList.Remove(dataSet.Precedence[prec_oper].Subsequent_Operation_ID);
				}			
				DataRow[]jobs =
					dataSet.Families[i].GetChildRows(familiesJobesRelation);
				for (int k=0;k<jobs.Length;k++)
				{
					if (dataSet.Jobs[k].Due_Date!=0) // *** The Criteria that defines jobs with due date
					{
						sr.WriteLine("A Job With A DUE-DATE : f={0}, j={1}", dataSet.Families[i].Family_ID,dataSet.Jobs[k].Job_ID);
						//System.Console.WriteLine("ArraylistCounter={0}",IFinishfList.Count);
						foreach (int operation_F in IFinishfList) 
						{
							// *** Creating The Constraint
							// ***			Tjfi
							sr.WriteLine("operation_F={0} In Array={1}",operation_F,IFinishfList[0]);					
							sr.Write("Tj={0},f={1},i={2}",dataSet.Jobs[k].Job_ID,dataSet.Families[i].Family_ID,operation_F);
							int sa = (int)operation_F;
							ulong[] mrscspVariableArray=new ulong[3] {(ulong)dataSet.Jobs[k].Job_ID,(ulong)dataSet.Families[i].Family_ID,(ulong)sa};
							creatMrscspVariable(ref Mrscsp, mrscspVariableArray);
							if (!VariblesVectorJFIMRL.Contains(Mrscsp))
							{
								sr.WriteLine("BUUUG   10");
							}
							else
							{
								ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp)]=1;
								lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp)].AddCoefficient(constraintsCounter,1);				
						
								RightHandSideValues[constraintsCounter] = dataSet.Jobs[k].Due_Date;
								ConstraintsSenses[constraintsCounter] = 'L';
								sr.WriteLine("(MRSCSPVAR={0})",VariblesVectorJFIMRL.IndexOf(Mrscsp));
							}						 														
							int op_finding=0;
							while (op_finding<dataSet.Modes.Rows.Count)//(dataSet.Precedence[prec_oper].Previous_Operation_ID!=(int)dataSet.Modes[op_finding].Operation_ID)// .Operations[op_finding].Operation_ID)
							{		
								//	Console.WriteLine("----Constraint Number={0},Op_finding={1}",constraintsCounter,op_finding);
								//Console.WriteLine("P_Operation={0} Modes_operation={1}",operation_F,dataSet.Modes[op_finding].Operation_ID);
								if ((int)operation_F==(int)dataSet.Modes[op_finding].Operation_ID)// .Operations[op_finding].Operation_ID)
								{
									//  *** CREATING THR ti,m
									DataRow[]resourceUsage =
										dataSet.Modes[op_finding].GetChildRows(modesResourceUsageRelation);									
									sr.WriteLine("resourceUasgeop_finding].ItemArray[2]==Mode_ID={0}",resourceUsage[0].ItemArray[2]);
									int Max_tfimr=0;
									int Min_tsimr=100000;
									for (int j=0;j<resourceUsage.Length;j++)
									{
										if (Max_tfimr<(int)resourceUsage[j].ItemArray[5])//Tf
											Max_tfimr=(int)resourceUsage[j].ItemArray[5];//Tf
										if (Min_tsimr>(int)resourceUsage[j].ItemArray[4])//Ts
											Min_tsimr=(int)resourceUsage[j].ItemArray[4];
										//System.Console.WriteLine("resourceUsage Length={0},dataSet.ResourceUsage[j].Resource_ID={1}",resourceUsage.Length, resourceUsage[j].ItemArray[3]);
									}
									sr.Write("TS={0}, Tf={1}", Min_tsimr,Max_tfimr);
									int t_im = Max_tfimr- Min_tsimr;
									sr.WriteLine("\tt_im={0}",t_im);									
									vParameterIdentifier = 999;
									sa = (int)operation_F;
									ulong[] mrscspYjfimVariableArray=new ulong[5] {(ulong)vParameterIdentifier,(ulong)dataSet.Jobs[k].Job_ID,(ulong)dataSet.Families[i].Family_ID,(ulong)sa,(ulong)dataSet.Modes[op_finding].Mode_ID};
									//							mrscspYjfimVariableArray.SetValue((ulong)dataSet.Jobs[k].Job_ID,(ulong)dataSet.Families[i].Family_ID,(ulong)sa,(ulong)dataSet.ResourceUsage[op_finding].Mode_ID,0,1,2,3);	
									creatMrscspVariable(ref Mrscsp, mrscspYjfimVariableArray);
									if (!VariblesVectorJFIMRL.Contains(Mrscsp))
									{
										sr.WriteLine("BBBUUUGGGG 10");
									}
									else
									{
										ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp)]=t_im;
										lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp)].AddCoefficient(constraintsCounter,t_im);				
						
										//System.Console.WriteLine("{0} - the Xjfimrl Variable Parameters: j={1},f={2},i={3},m={4}",VariblesVectorJFIMRL.IndexOf(Mrscsp),dataSet.Jobs[k].Job_ID,dataSet.Families[i].Family_ID,dataSet.OperationsToFamilies[g].Operation_ID,j );						
										sr.WriteLine("The Yjfim (MRSCSPVAR={0})",VariblesVectorJFIMRL.IndexOf(Mrscsp));
									}										
								}
								op_finding++;		
							}								
							sr.WriteLine("Constraint {0} IS {1} TO {2}",constraintsCounter,ConstraintsSenses[constraintsCounter],RightHandSideValues[constraintsCounter]);						
							constraintsCounter++;
						}
					}				
				}
			}			  
   */
			#endregion			

			#region Creating Constraints No. 11A	

            /*
			// *** Creating Constraints No. 11A	
			sr.WriteLine("\n\t\tThe constraints 11A creation:\n");
			sr.WriteLine("\t\t=============================");
			int job_p = new int();
			int family_p = new int();
			int operation1_p = new int();
			int operation2_p = new int();
			int mode_p = new int();
			// *** Creating The Precedece Operations Loop
			for (int prec=0;prec<dataSet.Precedence.Rows.Count;prec++)
			{
				// *** Creating The Jobs Loop
				DataView jobsView = new DataView(dataSet.Jobs);
				jobsView.RowFilter = "Family_ID =" + dataSet.Precedence[prec].ItemArray[1];
				
				for(int q=0;q<jobsView.Count;q++)
				{
					// Creating The modes for the Yjfi1m's
					DataView modes_Prec_View = new DataView(dataSet.Modes);
					modes_Prec_View.RowFilter = "Operation_ID =" + dataSet.Precedence[prec].ItemArray[2];
							
					for(int v=0;v<modes_Prec_View.Count;v++)
					{
						// *** Creating The Yjfi1m
						sr.Write("Yj={0},f={1},i1={2},m={3}",jobsView[q]["Job_ID"],dataSet.Precedence[prec].ItemArray[1],dataSet.Precedence[prec].ItemArray[2],modes_Prec_View[v]["Mode_ID"]);
						// *** Calculationg the ti1,m according i1,m
						DataView ResourceUsageView = new DataView(dataSet.ResourceUsage);
						ResourceUsageView.RowFilter = "Operation_ID ="+ dataSet.Precedence[prec].ItemArray[2] + "AND Mode_ID =" + modes_Prec_View[v]["Mode_ID"];
						int Max_tfi1mr=0;
						int Min_tsi1mr=100000;
						for(int s=0;s<ResourceUsageView.Count;s++)
						{
							sr.Write("Tf={0} Ts={1}",(int)ResourceUsageView[s]["Tf"],(int)ResourceUsageView[s]["Ts"]);
							if (Max_tfi1mr<(int)ResourceUsageView[s]["Tf"])
								Max_tfi1mr=(int)ResourceUsageView[s]["Tf"];
							if (Min_tsi1mr>(int)ResourceUsageView[s]["Ts"])
								Min_tsi1mr=(int)ResourceUsageView[s]["Ts"];
							//	System.Console.WriteLine("Operation {0} Length is{1}",ob,Max_tfimr-Min_tsimr);							
						}							
						sr.WriteLine(" ");
						sr.Write("\tTS={0}, Tf={1}", Min_tsi1mr,Max_tfi1mr);
						int t_i1m = Max_tfi1mr- Min_tsi1mr;
						sr.Write("\tt_i1m={0}",t_i1m);

						vParameterIdentifier = 999;
						job_p = (int)jobsView[q]["Job_ID"];
						family_p = (int)dataSet.Precedence[prec].ItemArray[1];
						operation1_p = (int)dataSet.Precedence[prec].ItemArray[2];
						operation2_p = (int)dataSet.Precedence[prec].ItemArray[3];
						mode_p = (int)modes_Prec_View[v]["Mode_ID"];
						ulong[] mrscspYjfi1mVariableArray=new ulong[5] {(ulong)vParameterIdentifier,(ulong)job_p,(ulong)family_p,(ulong)operation1_p,(ulong)mode_p};
						creatMrscspVariable(ref Mrscsp, mrscspYjfi1mVariableArray);
						if (!VariblesVectorJFIMRL.Contains(Mrscsp))
						{
							sr.WriteLine("BUUUG  11A");
						}
						else
						{
							ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp)]=t_i1m;							
							lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp)].AddCoefficient(constraintsCounter,t_i1m);				
						
							sr.WriteLine("\tThe MRSCSPVAR={0}",VariblesVectorJFIMRL.IndexOf(Mrscsp));
						}
					}
					// *** Tjfi1
					sr.Write("Tj={0},f={1},i1={2}",job_p,family_p,operation1_p);
					ulong[] mrscspTjfi1VariableArray=new ulong[3] {(ulong)job_p,(ulong)family_p,(ulong)operation1_p};
					creatMrscspVariable(ref Mrscsp, mrscspTjfi1VariableArray);
					if (!VariblesVectorJFIMRL.Contains(Mrscsp))
					{
						sr.WriteLine("BUUUG  11A");
					}
					else
					{
						ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp)]=1;
						lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp)].AddCoefficient(constraintsCounter,1);				
						RightHandSideValues[constraintsCounter] = (int)dataSet.Precedence[prec].ItemArray[4];
						ConstraintsSenses[constraintsCounter] = 'L';
						sr.WriteLine("\t\tThe Tjfi1 MRSCSPVAR={0}",VariblesVectorJFIMRL.IndexOf(Mrscsp));
					}

					// *** Tjfi2
					sr.Write("Tj={0},f={1},i2={2}",job_p,family_p,operation2_p);
					ulong[] mrscspTjfi2VariableArray=new ulong[3] {(ulong)job_p,(ulong)family_p,(ulong)operation2_p};
					creatMrscspVariable(ref Mrscsp, mrscspTjfi2VariableArray);
					if (!VariblesVectorJFIMRL.Contains(Mrscsp))
					{
						sr.WriteLine("BUUUG 11A ");
					}
					else
					{
						ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp)]=-1;
						lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp)].AddCoefficient(constraintsCounter,-1);				
						sr.WriteLine("\t\tThe Tjfi2 MRSCSPVAR={0}",VariblesVectorJFIMRL.IndexOf(Mrscsp));
					}
				sr.WriteLine("\nConstraint {0} IS {1} TO {2}",constraintsCounter,ConstraintsSenses[constraintsCounter],RightHandSideValues[constraintsCounter]);
				sr.WriteLine("-----------------------------");
				constraintsCounter++;
				}
			}	
              */
			#endregion 

			#region Creating Constraints No. 11B  
            /*
			// *** Creating Constraints No. 11B
			sr.WriteLine("\n\t\tThe constraints 11B creation:\n");
			sr.WriteLine("\t\t=============================");
			//int job_p = new int();
			//int family_p = new int();
			//int operation1_p = new int();
			//int operation2_p = new int();
			//int mode_p = new int();

			DataView precedenceSMaxView = new DataView(dataSet.Precedence);
			precedenceSMaxView.RowFilter = "MaxLfi1i2 > 0";
			 
			for (int prec2=0;prec2<precedenceSMaxView.Count;prec2++)
			{
				// *** Creating The Jobs Loop
				DataView jobsView = new DataView(dataSet.Jobs);
				jobsView.RowFilter = "Family_ID =" + precedenceSMaxView[prec2]["Family_ID"];				
				for(int q=0;q<jobsView.Count;q++)
				{
					// creating The modes for the Yjfi1m's
					DataView modes_Prec_View = new DataView(dataSet.Modes);
					modes_Prec_View.RowFilter = "Operation_ID =" + precedenceSMaxView[prec2]["Previous_Operation_ID"];
							
					for(int v=0;v<modes_Prec_View.Count;v++)
					{
						// *** Creating The Yjfi1m
						sr.Write("Tj={0},f={1},i1={2},m={3}",jobsView[q]["Job_ID"],precedenceSMaxView[prec2]["Family_ID"],precedenceSMaxView[prec2]["Previous_Operation_ID"],modes_Prec_View[v]["Mode_ID"]);
						// *** Calculationg the ti1,m according i1,m
						DataView ResourceUsageView = new DataView(dataSet.ResourceUsage);
						ResourceUsageView.RowFilter = "Operation_ID ="+ precedenceSMaxView[prec2]["Previous_Operation_ID"] + "AND Mode_ID =" + modes_Prec_View[v]["Mode_ID"];
						int Max_tfi1mr=0;
						int Min_tsi1mr=100000;
						for(int s=0;s<ResourceUsageView.Count;s++)
						{
							if (Max_tfi1mr<(int)ResourceUsageView[s]["Tf"])
								Max_tfi1mr=(int)ResourceUsageView[s]["Tf"];
							if (Min_tsi1mr>(int)ResourceUsageView[s]["Ts"])
								Min_tsi1mr=(int)ResourceUsageView[s]["Ts"];
						}							
						sr.Write("\tTS={0}, Tf={1}", Min_tsi1mr,Max_tfi1mr);
						int t_i1m = Max_tfi1mr- Min_tsi1mr;
						sr.Write("\tt_i1m={0}",t_i1m);
						
						vParameterIdentifier = 999;
						job_p = (int)jobsView[q]["Job_ID"];
						family_p = (int)precedenceSMaxView[prec2]["Family_ID"];
						operation1_p = (int)precedenceSMaxView[prec2]["Previous_Operation_ID"];
						operation2_p = (int)precedenceSMaxView[prec2]["Subsequent_Operation_ID"];;
						mode_p = (int)modes_Prec_View[v]["Mode_ID"];
						ulong[] mrscspYjfi1mVariableArray=new ulong[5] {(ulong)vParameterIdentifier,(ulong)job_p,(ulong)family_p,(ulong)operation1_p,(ulong)mode_p};
						creatMrscspVariable(ref Mrscsp, mrscspYjfi1mVariableArray);
						if (!VariblesVectorJFIMRL.Contains(Mrscsp))
						{
							sr.WriteLine("BUUUG  11B ");
						}
						else
						{
							ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp)]=(-1*(t_i1m));							
							lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp)].AddCoefficient(constraintsCounter,(-1*(t_i1m)));				
							sr.WriteLine("\tThe MRSCSPVAR={0}",VariblesVectorJFIMRL.IndexOf(Mrscsp));
						}
					}
					// *** Tjfi1
					sr.Write("Tj={0},f={1},i1={2}",job_p,family_p,operation1_p);
					ulong[] mrscspTjfi1VariableArray=new ulong[3] {(ulong)job_p,(ulong)family_p,(ulong)operation1_p};
					creatMrscspVariable(ref Mrscsp, mrscspTjfi1VariableArray);
					if (!VariblesVectorJFIMRL.Contains(Mrscsp))
					{
						sr.WriteLine("BUUUG");
					}
					else
					{
						ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp)]=-1;
						lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp)].AddCoefficient(constraintsCounter,-1);				
							
						RightHandSideValues[constraintsCounter] = (int)precedenceSMaxView[prec2]["MaxLfi1i2"];
						ConstraintsSenses[constraintsCounter] = 'L';
						sr.WriteLine("\t\tThe Tjfi1 MRSCSPVAR={0}",VariblesVectorJFIMRL.IndexOf(Mrscsp));
					}

					// *** Tjfi2
					sr.Write("Tj={0},f={1},i2={2}",job_p,family_p,operation2_p);
					ulong[] mrscspTjfi2VariableArray=new ulong[3] {(ulong)job_p,(ulong)family_p,(ulong)operation2_p};
					creatMrscspVariable(ref Mrscsp, mrscspTjfi2VariableArray);
					if (!VariblesVectorJFIMRL.Contains(Mrscsp))
					{
						sr.WriteLine("BUUUG  11b");
					}
					else
					{
						ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp)]=1;
						lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp)].AddCoefficient(constraintsCounter,1);				
							
						sr.WriteLine("\t\tThe Tjfi2 MRSCSPVAR={0}",VariblesVectorJFIMRL.IndexOf(Mrscsp));
					}
					sr.WriteLine("\nConstraint {0} IS {1} TO {2}",constraintsCounter,ConstraintsSenses[constraintsCounter],RightHandSideValues[constraintsCounter]);
					sr.WriteLine("-----------------------------");
					constraintsCounter++;
				}
			}
			*/
			#endregion

			#region Creating Constraints No. 12	 
            /*
			// *** Creating Constraints No. 12
			sr.WriteLine("\n\t\tThe constraints 12 creation:\n");		
			sr.WriteLine("\t\t=============================");
			int i_operation1 = new int();
			int m_mode1 = new int();
			int i_operation2 = new int();
			int m_mode2 = new int();
			vParameterIdentifier = 998; //{ Remember That vParameterIdentifier = 998 }
			for (int constD=0;constD<dataSet.ConstantDelays.Rows.Count;constD++)
			{
				int r=0;
				while ((resourceID[r])!=(dataSet.ConstantDelays[constD].Resource_ID))
					r++;				
				sr.WriteLine("R={0},r={1}",dataSet.ConstantDelays[constD].Resource_ID,resourceID[r]);
				for (int task=1;task<resourceLr[r];task++)//task=1 because of the constraints defenitions
				{
					// *** Creating the Zr,l

					ulong[] mrscspVariableArray=new ulong[2] {(ulong)resourceID[r],(ulong)task+1};
					sr.Write("The Zr={0},l={1}",resourceID[r],task+1);
					creatMrscspVariable(ref Mrscsp, mrscspVariableArray);
					if (!VariblesVectorJFIMRL.Contains(Mrscsp))
					{
						sr.WriteLine("** BUG 12 ** BUG 12*** BUG 12  ***      Creating the Zr,l");
						Console.WriteLine("** BUG 12 ** BUG 12*** BUG 12  ***      Creating the Zr,l");
					}
					else
					{						
						ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp,(int)startIndexZrlInVariableVector)]=(-1);//*(dataSet.ConstantDelays[constD].di1m1i2m2r));
						lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp,(int)startIndexZrlInVariableVector)].AddCoefficient(constraintsCounter,(-1));//*(dataSet.ConstantDelays[constD].di1m1i2m2r)));				
							
						RightHandSideValues[constraintsCounter] = dataSet.ConstantDelays[constD].di1m1i2m2r;
						ConstraintsSenses[constraintsCounter] = 'L';
						sr.WriteLine("      The MRSCSPVAR={0}",VariblesVectorJFIMRL.IndexOf(Mrscsp,(int)startIndexZrlInVariableVector));
					}

					// *** Creating the Yi1,m1,r,l-1

					sr.WriteLine("Yi1={0},m1={1},r={2},l-1={3}", dataSet.ConstantDelays[constD].ItemArray[1],dataSet.ConstantDelays[constD].ItemArray[2],resourceID[r],task);
					i_operation1=(int)dataSet.ConstantDelays[constD].ItemArray[1];
					m_mode1=(int)dataSet.ConstantDelays[constD].ItemArray[2];
					ulong[] mrscspYi1m1rlVariableArray=new ulong[5] {(ulong)vParameterIdentifier,(ulong)i_operation1,(ulong)m_mode1,(ulong)dataSet.Resources[r].Resource_ID,(ulong)(task) };
					creatMrscspVariable(ref Mrscsp, mrscspYi1m1rlVariableArray);
					if (!VariblesVectorJFIMRL.Contains(Mrscsp))
					{
						sr.WriteLine("** BUG 12 ** BUG 12*** BUG 12  ***      Creating the Yi1,m1,r,l-1");
						Console.WriteLine("** BUG 12 ** BUG 12*** BUG 12  ***      Creating the Yi1,m1,r,l-1");
					}
					else
					{
						ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp)]=dataSet.ConstantDelays[constD].di1m1i2m2r;
						lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp)].AddCoefficient(constraintsCounter,dataSet.ConstantDelays[constD].di1m1i2m2r);				
						
					}
					
					// *** Creating the Yi2,m2,r,l
					sr.WriteLine("Yi2={0},m2={1},r={2},l={3}", dataSet.ConstantDelays[constD].ItemArray[3],dataSet.ConstantDelays[constD].ItemArray[4],resourceID[r],task+1);
					i_operation2=(int)dataSet.ConstantDelays[constD].ItemArray[3];
					m_mode2=(int)dataSet.ConstantDelays[constD].ItemArray[4];
					ulong[] mrscspYi2m2rlVariableArray=new ulong[5] {(ulong)vParameterIdentifier,(ulong)i_operation2,(ulong)m_mode2,(ulong)dataSet.Resources[r].Resource_ID,(ulong)(task+1) };														
					creatMrscspVariable(ref Mrscsp, mrscspYi2m2rlVariableArray);
					if (!VariblesVectorJFIMRL.Contains(Mrscsp))
					{
						sr.WriteLine("** BUG 12 ** BUG 12*** BUG 12  ***      Creating the Yi2,m2,r,l");
						Console.WriteLine("** BUG 12 ** BUG 12*** BUG 12  ***      Creating the Yi2,m2,r,l");
					}
					else
					{
						ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp)]=dataSet.ConstantDelays[constD].di1m1i2m2r;
						lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp)].AddCoefficient(constraintsCounter,dataSet.ConstantDelays[constD].di1m1i2m2r);				
					}
						
					sr.WriteLine("Constraint {0} IS {1} TO {2}",constraintsCounter,ConstraintsSenses[constraintsCounter],RightHandSideValues[constraintsCounter]);
					sr.WriteLine("-----------------------------");
					constraintsCounter++;
				}					
			}		
             * */
			#endregion

			#region Creating Constraints No. 13			
			// *** Creating Constraints No. 13		
            /*
			int MultipleUjfDl = new int();
			int Dl            = new int();
			sr.WriteLine("\n\t\tThe Constraints 13 Creation:\n");
			sr.WriteLine("\t\t=============================");
			for (byte loading=0;loading<dataSet.LoadingTimes.Count;loading++)
			{				
				int r=0;
				while ((resourceID[r])==(dataSet.LoadingTimes[loading].Resource_ID))
				{
					for (int task=0;task<resourceLr[r];task++)
					{	
						// **** Creating the Vrl variables
						
						ulong[] mrscspVariableArray=new ulong[2] {(ulong)resourceID[r],(ulong)task+1};
						creatMrscspVariable(ref Mrscsp, mrscspVariableArray);
						if (!VariblesVectorJFIMRL.Contains(Mrscsp))
						{
							sr.WriteLine("BUUUG  13 ");
						}
						else
						{							
							ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp,(int)startIndexVrlInVariableVector)]=-1;
							lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp,(int)startIndexVrlInVariableVector)].AddCoefficient(constraintsCounter,-1);				
							sr.WriteLine("The Vr={0},l={1} Mrscsp={2}",resourceID[r],task+1,VariblesVectorJFIMRL.IndexOf(Mrscsp,(int)startIndexVrlInVariableVector));
						}										
						for (int i=0;i<dataSet.Families.Rows.Count;i++)
						{	
							DataRow[]operationsToFamilies =
								dataSet.Families[i].GetChildRows(familiesOperationsToFamiliesRelation);
							OperationFamilyList.Clear();
							for (int oper=0;oper<operationsToFamilies.Length;oper++)
							{
								OperationFamilyList.Add(operationsToFamilies[oper].ItemArray[2]);
							}						

							// **** Creating the Xjfimrl variables
							if (!OperationFamilyList.Contains(dataSet.LoadingTimes[loading].Operation_ID))
							{
								//Console.WriteLine("BUUUG {0} F={1}",i_operation,dataSet.Families[i].Family_ID);
							}
							else
							{
								//Console.WriteLine("YESSSSSSSSS {0} F={1}",dataSet.LoadingTimes[loading].Operation_ID,dataSet.Families[i].Family_ID);
								DataRow[]jobs =
									dataSet.Families[i].GetChildRows(familiesJobesRelation);
								for (int k=0;k<jobs.Length;k++)
								{						
									//	for (int task=0;task<resourceLr[r];task++)
									//	{
									 Ujf=(int)dataSet.Jobs[k].Units;
									
									// *** Creating The Xjfimrl Variable 
								
									ulong[] mrscspXjfimrlVariableArray=new ulong[6] {(ulong)dataSet.Jobs[k].Job_ID,(ulong)dataSet.Families[i].Family_ID ,(ulong)dataSet.LoadingTimes[loading].Operation_ID,(ulong)dataSet.LoadingTimes[loading].Mode_ID,(ulong)dataSet.LoadingTimes[loading].Resource_ID,(ulong)(task+1)};
									creatMrscspVariable(ref Mrscsp, mrscspXjfimrlVariableArray);
									sr.WriteLine("j={0}, f={1}, i={2}, m={3}, r={4}, l={5}, Ujf={6},Mrscsp={7}\n",dataSet.Jobs[k].Job_ID,dataSet.Families[i].Family_ID ,dataSet.LoadingTimes[loading].Operation_ID, dataSet.LoadingTimes[loading].Mode_ID,dataSet.LoadingTimes[loading].Resource_ID,task+1,dataSet.Jobs[k].Units,VariblesVectorJFIMRL.IndexOf(Mrscsp));
									if (!VariblesVectorJFIMRL.Contains(Mrscsp))
									{
										sr.WriteLine("BUUUG  13");
									}
									else
									{
										Dl = dataSet.LoadingTimes[loading].DLimr;									
										MultipleUjfDl = (Dl*Ujf);
										sr.Write("MultipleUjfDl={0}---",MultipleUjfDl);
										ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp)]=MultipleUjfDl;
										lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp)].AddCoefficient(constraintsCounter,MultipleUjfDl);				
										RightHandSideValues[constraintsCounter] = 0;
										ConstraintsSenses[constraintsCounter] = 'L';
										//System.Console.WriteLine("Index={0}--->Counter={1}",VariblesVectorJFIMRL.IndexOf(Mrscsp),constraintsCounter);								
									}
									sr.WriteLine("Constraint {0} IS {1} TO {2}",constraintsCounter,ConstraintsSenses[constraintsCounter],RightHandSideValues[constraintsCounter]);
								}
							}
						}					
						constraintsCounter++;
					}					
					sr.WriteLine("r={0} R={1},r={2}",resourceID[r],dataSet.LoadingTimes[loading].Resource_ID,r);
					r++;					
				}
			}
				*/
			#endregion

			#region Creating Constraints No. 14	
            /*
			// *** Creating Constraints No. 14	
			sr.WriteLine("\n\t\tThe constraints 14 creation:\n");	
			sr.WriteLine("\n\t\t============================\n");	
			// *** Preparations - r's with delays ( in order to calculate the Vrl & Zr'l)
			ArrayList rInConstantDelays = new ArrayList();
			ArrayList rInLoadingTimes = new ArrayList();
			vParameterIdentifier = 998; //{ Remember That vParameterIdentifier = 998 }
			//sr.WriteLine("rInConstantDelays.count={0}",dataSet.ConstantDelays.Rows.Count);
			for (int r1=0;r1<dataSet.ConstantDelays.Rows.Count;r1++)
			{
				rInConstantDelays.Add(dataSet.ConstantDelays[r1].Resource_ID);				
			}			
			for (int r2=0;r2<dataSet.LoadingTimes.Rows.Count;r2++)
			{
				rInLoadingTimes.Add(dataSet.LoadingTimes[r2].Resource_ID);
			}
			// *** Creating the constraint equations
			for (byte r=0;r<dataSet.Resources.Count;r++)
			{		
				DataRow[]resourceResourceUsage =
					dataSet.Resources[r].GetChildRows(resourceResourcesUsageRelation);    									
				for (int task=1;task<resourceLr[r];task++)
				{			
					// **** Creating the Tr,l-1 
					ulong[] mrscspTrl_1VariableArray=new ulong[2] {(ulong)resourceID[r],(ulong)task};
					sr.Write("The Tr={0},l-1={1}",resourceID[r],task);
					creatMrscspVariable(ref Mrscsp, mrscspTrl_1VariableArray);
					if (!VariblesVectorJFIMRL.Contains(Mrscsp))
					{
						sr.WriteLine("BUUUG  14");
					}
					else
					{ 					
						ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp)]=1;
						lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp)].AddCoefficient(constraintsCounter,1);						
						sr.WriteLine(" (MRSCSPVAR={0})",VariblesVectorJFIMRL.IndexOf(Mrscsp));
					}
					// **** Creating the Trl 
					ulong[] mrscspTrlVariableArray=new ulong[2] {(ulong)resourceID[r],(ulong)task+1};
					sr.Write("The Tr={0},l={1}",resourceID[r],task+1);
					creatMrscspVariable(ref Mrscsp, mrscspTrlVariableArray);
					if (!VariblesVectorJFIMRL.Contains(Mrscsp))
					{
						sr.WriteLine("BUUUG  14");
					}
					else
					{ 					
						ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp)]=-1;
						lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp)].AddCoefficient(constraintsCounter,-1);						
						sr.WriteLine(" (MRSCSPVAR={0})",VariblesVectorJFIMRL.IndexOf(Mrscsp));
					}
					// **** Creating the Vrl variables
					if (rInLoadingTimes.Contains(resourceID[r]))
					{                        							
						ulong[] mrscspVariableArray=new ulong[2] {(ulong)resourceID[r],(ulong)task+1};
						creatMrscspVariable(ref Mrscsp, mrscspVariableArray);
						if (!VariblesVectorJFIMRL.Contains(Mrscsp))
						{
							sr.WriteLine("BUUUG  14");
						}
						else
						{							
							ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp,(int)startIndexVrlInVariableVector)]=1;
							lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp,(int)startIndexVrlInVariableVector)].AddCoefficient(constraintsCounter,1);	
							sr.WriteLine(" The Vr={0},l={1} Mrscsp={2}",resourceID[r],task+1,VariblesVectorJFIMRL.IndexOf(Mrscsp,(int)startIndexVrlInVariableVector));
						}
					}					
					// **** Creating the Zrl variables
					if (rInConstantDelays.Contains(resourceID[r]))
					{							
						ulong[] mrscspVariableArray=new ulong[2] {(ulong)resourceID[r],(ulong)task+1};
						creatMrscspVariable(ref Mrscsp, mrscspVariableArray);
						if (!VariblesVectorJFIMRL.Contains(Mrscsp))
						{
							sr.WriteLine("BUUUG  14");
						}
						else
						{							
							ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp,(int)startIndexZrlInVariableVector)]=1;
							lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp,(int)startIndexZrlInVariableVector)].AddCoefficient(constraintsCounter,1);	
							sr.WriteLine("The Zr={0},l={1} Mrscsp={2}",resourceID[r],task+1,VariblesVectorJFIMRL.IndexOf(Mrscsp,(int)startIndexZrlInVariableVector));
						}
					}
					for (int imCount=0;imCount<resourceResourceUsage.Length;imCount++)
					{			
						int i_operation=(int)resourceResourceUsage[imCount].ItemArray[1];
						int m_mode=(int)resourceResourceUsage[imCount].ItemArray[2];
						//sr.WriteLine("The {0} Constraint has the following Varibles:",constraintsCounter);
						sr.WriteLine("i={0},m={1},r={2},l-1={3}\n", resourceResourceUsage[imCount].ItemArray[1], resourceResourceUsage[imCount].ItemArray[2],dataSet.Resources[r].Resource_ID,task+1-1);
						// ***		// *** Creating the Yimrl-1												
					//	sr.WriteLine("(int)resourceResourceUsage[imCount].ItemArray[5]={0},[4]={1}",(int)resourceResourceUsage[imCount].ItemArray[5],(int)resourceResourceUsage[imCount].ItemArray[4]);
						ulong[] mrscspYimrlVariableArray=new ulong[5] {(ulong)vParameterIdentifier,(ulong)i_operation,(ulong)m_mode,(ulong)dataSet.Resources[r].Resource_ID,(ulong)(task+1-1) };														
						creatMrscspVariable(ref Mrscsp, mrscspYimrlVariableArray);
						if (!VariblesVectorJFIMRL.Contains(Mrscsp))
						{
							sr.WriteLine("BBBUUUGGGG  14");
						}
						else
						{
							ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp)]=((int)resourceResourceUsage[imCount].ItemArray[5]-(int)resourceResourceUsage[imCount].ItemArray[4]);//dataSet.ResourceUsage[imCount].ItemArray[5]-dataSet.ResourceUsage[imCount].ItemArray[4]);
							//25.01.07 - lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp)].AddCoefficient(constraintsCounter,(dataSet.ResourceUsage[imCount].Tf-dataSet.ResourceUsage[imCount].Ts));	
							lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp)].AddCoefficient(constraintsCounter,((int)resourceResourceUsage[imCount].ItemArray[5]-(int)resourceResourceUsage[imCount].ItemArray[4]));	
							RightHandSideValues[constraintsCounter] = 0;
							ConstraintsSenses[constraintsCounter] = 'L';
							//Console.WriteLine("constraintsCounter={0}   Mrscsp={1}  equation3CoefficientPlus={2}",constraintsCounter,Mrscsp,equation3CoefficientPlus);
						}
					}
					sr.WriteLine("Constraint {0} IS {1} TO {2}",constraintsCounter,ConstraintsSenses[constraintsCounter],RightHandSideValues[constraintsCounter]);
					constraintsCounter++;
				}
			}
             * */
			#endregion

		

			#region Creating Constraints No. 15	
            /*
			// *** Creating Constraints No. 15	
			sr.WriteLine("\n\t\tThe constraints 15 creation:\n");	
			sr.WriteLine("\t\t=============================");
			for (int r=0;r<dataSet.Resources.Rows.Count;r++)
			{
				int task = 1;
				ulong[] mrscspVariableArray=new ulong[2] {(ulong)dataSet.Resources[r].Resource_ID,(ulong)task};
				//System.Console.WriteLine("The -Tr={0},1 + Vr={1},l",dataSet.Resources[r].Resource_ID,resourceID[r]);
				// *** Tr1 
				
				ulong[] mrscspTrlVariableArray=new ulong[2] {(ulong)dataSet.Resources[r].Resource_ID,(ulong)task};
				sr.Write("The Tr={0},l=1",dataSet.Resources[r].Resource_ID);
				creatMrscspVariable(ref Mrscsp, mrscspTrlVariableArray);
				if (!VariblesVectorJFIMRL.Contains(Mrscsp))
				{
					sr.WriteLine("BUUUG  15");
				}
				else
				{ 					
					ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp)]=-1;
					lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp)].AddCoefficient(constraintsCounter,-1);						
					RightHandSideValues[constraintsCounter] = (-1)*dataSet.Resources[r].Release_Date_tr1;
					ConstraintsSenses[constraintsCounter] = 'L';
					sr.WriteLine("(MRSCSPVAR={0})",VariblesVectorJFIMRL.IndexOf(Mrscsp));
				}
			 
				// *** Vr1 
				DataRow[]resources =
					dataSet.Resources[r].GetChildRows(resourcesLoadingTimes);
				if (resources.Length>0)
				{
					ulong[] mrscspVrlVariableArray=new ulong[2] {(ulong)resourceID[r],(ulong)task};
					sr.Write("The Vr={0},l=1",resourceID[r]);
					creatMrscspVariable(ref Mrscsp, mrscspVrlVariableArray);
					if (!VariblesVectorJFIMRL.Contains(Mrscsp))
					{
						sr.WriteLine("BUUUG  15");
					}
					else
					{
						//System.Console.WriteLine("Now={0} In={1}",startIndexVrlInVariableVector+counterVrlInVariablesVector,VariblesVectorJFIMRL.IndexOf(Mrscsp,(int)startIndexVrlInVariableVector));
						ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp,(int)startIndexVrlInVariableVector)]=1;
						lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp,(int)startIndexVrlInVariableVector)].AddCoefficient(constraintsCounter,1);			
						sr.WriteLine("(MRSCSPVAR={0})",VariblesVectorJFIMRL.IndexOf(Mrscsp,(int)startIndexVrlInVariableVector));
					}
				}
				sr.WriteLine("Constraint {0} IS {1} TO ({2})",constraintsCounter,ConstraintsSenses[constraintsCounter],RightHandSideValues[constraintsCounter]);						
				constraintsCounter++;
			}			
			#endregion

			#region Creating Constraints No. 16	

			// *** Creating Constraints No. 16	
			sr.WriteLine("\n\t\tThe constraints 16 creation:\n");		
			sr.WriteLine("=============================");
			// ***  Finding The Jobs from each family with Due Date<> from 0
			// ***  { 0, Zero means that there is no due-date to the job }
			for (int i=0;i<dataSet.Families.Rows.Count;i++)
			{
				DataRow[]operationsToFamilies =
					dataSet.Families[i].GetChildRows(familiesOperationsToFamiliesRelation);
				for (int oper=0;oper<operationsToFamilies.Length;oper++)
				{ 
					IStartfList.Add(dataSet.OperationsToFamilies[oper].Operation_ID);					
				}					
				DataRow[]Precedence =
					dataSet.Families[i].GetChildRows(familiesPrecedenceRelation);
				for (int prec_oper=0;prec_oper< Precedence.Length ;prec_oper++)
				{					
					//IFinishfList.Remove(dataSet.Precedence[prec_oper].Previous_Operation_ID);
					IStartfList.Remove(dataSet.Precedence[prec_oper].Subsequent_Operation_ID);
				}			
				DataRow[]jobs =
					dataSet.Families[i].GetChildRows(familiesJobesRelation);
				for (int k=0;k<jobs.Length;k++)
				{
					if (dataSet.Jobs[k].Release_Date!=0) // *** The Criteria that defines jobs with due date
					{
						sr.WriteLine("THERE IS A Job With A RELEASE-DATE : f={0}, j={1}", dataSet.Families[i].Family_ID,dataSet.Jobs[k].Job_ID);
						//System.Console.WriteLine("ArraylistCounter={0}",IFinishfList.Count);
						foreach (int operation_S in IStartfList) 
						{
							// *** Creating The Constraint
							// ***			Tjfi			
							sr.Write("Tj={0},f={1},i={2}",dataSet.Jobs[k].Job_ID,dataSet.Families[i].Family_ID,operation_S);
							int sa = (int)operation_S;
							ulong[] mrscspVariableArray=new ulong[3] {(ulong)dataSet.Jobs[k].Job_ID,(ulong)dataSet.Families[i].Family_ID,(ulong)sa};
							creatMrscspVariable(ref Mrscsp, mrscspVariableArray);
							if (!VariblesVectorJFIMRL.Contains(Mrscsp))
							{
								sr.WriteLine("BUUUG  16");
							}
							else
							{
								ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp)]=-1;
								lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp)].AddCoefficient(constraintsCounter,-1);	
								RightHandSideValues[constraintsCounter] =(-1)* dataSet.Jobs[k].Release_Date;
								ConstraintsSenses[constraintsCounter] = 'L';
								sr.WriteLine(" MRSCSPVAR={0}",Mrscsp);
							}		
							sr.WriteLine("Constraint {0} IS {1} TO ({2})",constraintsCounter,ConstraintsSenses[constraintsCounter],RightHandSideValues[constraintsCounter]);						
							constraintsCounter++;
						}
					}
				}
			}
             * */
			#endregion

			#region Creating Constraints No. 17	
            /*
			// *** Creating Constraints No. 17	
			sr.WriteLine("\n\t\tThe constraints 17 creation:\n");		
			sr.WriteLine("\t\t=============================");
			mode = new int();
			operation = new int();
			family = new int();
			job = new int();
			for (int i=0;i<dataSet.Families.Rows.Count;i++)
			{	
				IFinishfList.Clear();		
				DataRow[]jobs =
					dataSet.Families[i].GetChildRows(familiesJobesRelation);
				DataRow[]operationsToFamilies =
					dataSet.Families[i].GetChildRows(familiesOperationsToFamiliesRelation);
															
				for (int k=0;k<jobs.Length;k++)
				{
					for (int oper=0;oper<operationsToFamilies.Length;oper++)
					{
						// *** Creating the Resource Loop
						DataView ResourceUsageView = new DataView(dataSet.ResourceUsage);
						ResourceUsageView.RowFilter = "Operation_ID ="+ operationsToFamilies[oper].ItemArray[2];
						for(int v=0;v<ResourceUsageView.Count;v++)
						{
							sr.WriteLine("Operation {0} has Resource {1} Ts={2}",operationsToFamilies[oper].ItemArray[2],ResourceUsageView[v]["Resource_ID"],ResourceUsageView[v]["Ts"]);
							// *** Creating The Lr Loop
							int res=(int)ResourceUsageView[v]["Resource_ID"];
							int index1=0;								
							while ( resourceID[index1]!=((int)ResourceUsageView[v]["Resource_ID"]))
								index1++;
		//					int position_Lower = arr_L_F_R[index1].arr_L_First[arr_L_F_R[index1].Convert_Operation_Index(oper)];
		//					int position_Upper = arr_L_F_R[index1].arr_L_Last[arr_L_F_R[index1].Convert_Operation_Index(oper)];
							//for (int lr=position_Lower;lr<=position_Upper;lr++)
							for (int lr=1;lr<=resourceLr[index1];lr++)
							{
								// *** Creating The Modes Loop
								DataView modesInResourceUsageView = new DataView(dataSet.ResourceUsage);
								modesInResourceUsageView.RowFilter = "Operation_ID ="+ operationsToFamilies[oper].ItemArray[2]+ "AND Resource_ID =" + res;	
								for (int numModes=0;numModes<modesInResourceUsageView.Count;numModes++)
								{								
									// *** Creating The Xjfimrl's According to 
									sr.Write("Xj={0},f={1},i={2},m={3},r={4},l={5}",jobs[k].ItemArray[2],dataSet.Families.Rows[i].ItemArray[1],operationsToFamilies[oper].ItemArray[2],modesInResourceUsageView[numModes]["Mode_ID"],resourceID[index1],lr);
									sr.WriteLine("     Tsimr={0} ==>{1}",modesInResourceUsageView[numModes]["Ts"],100000+(int)modesInResourceUsageView[numModes]["Ts"]);
									mode = (int)modesInResourceUsageView[numModes]["Mode_ID"];
									operation =(int)operationsToFamilies[oper].ItemArray[2];
									family =(int)dataSet.Families.Rows[i].ItemArray[1];
									job = (int)jobs[k].ItemArray[2];
									ulong[] mrscspXjfimrlVariableArray=new ulong[6] {(ulong)job,(ulong)family,(ulong)operation,(ulong)mode,(ulong)resourceID[index1],(ulong)lr};
									creatMrscspVariable(ref Mrscsp, mrscspXjfimrlVariableArray);
									if (!VariblesVectorJFIMRL.Contains(Mrscsp))
									{
										sr.WriteLine("BUUUG  17");
									}
									else
									{
										ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp)]=(100000+(int)modesInResourceUsageView[numModes]["Ts"]);
										lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp)].AddCoefficient(constraintsCounter,(100000+(int)modesInResourceUsageView[numModes]["Ts"]));	
										constraintsCounter++;
										ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp)]=(100000-(int)modesInResourceUsageView[numModes]["Ts"]);
										lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp)].AddCoefficient(constraintsCounter,(100000-(int)modesInResourceUsageView[numModes]["Ts"]));	
										constraintsCounter--;						
									}									
									// *** Creating The Tjfi
									sr.Write("Tj={0},f={1},i={2}",jobs[k].ItemArray[2],dataSet.Families.Rows[i].ItemArray[1],operationsToFamilies[oper].ItemArray[2]);
									operation=(int)operationsToFamilies[oper].ItemArray[2];
									family = (int)dataSet.Families.Rows[i].ItemArray[1];
									job = (int)jobs[k].ItemArray[2];
									ulong[] mrscspTjfiVariableArray=new ulong[3] {(ulong)job,(ulong)family,(ulong)operation};
									creatMrscspVariable(ref Mrscsp, mrscspTjfiVariableArray);
									if (!VariblesVectorJFIMRL.Contains(Mrscsp))
									{
										sr.WriteLine("BUUUG   17");
									}
									else
									{
										ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp)]=1;
										lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp)].AddCoefficient(constraintsCounter,1);	
										constraintsCounter++;
										ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp)]=-1;
										lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp)].AddCoefficient(constraintsCounter,-1);	
										constraintsCounter--;
										sr.WriteLine("(MRSCSPVAR={0})",VariblesVectorJFIMRL.IndexOf(Mrscsp));
									}
									// *** Trl
									ulong[] mrscspTrlVariableArray=new ulong[2] {(ulong)resourceID[index1],(ulong)lr};
									sr.Write("The Tr={0},lr={1}",resourceID[index1],lr);
									creatMrscspVariable(ref Mrscsp, mrscspTrlVariableArray);
									if (!VariblesVectorJFIMRL.Contains(Mrscsp))
									{
										sr.WriteLine("BUUUG   17");
									}
									else
									{ 										
										ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp)]=-1;
										lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp)].AddCoefficient(constraintsCounter,-1);	
										RightHandSideValues[constraintsCounter] = 100000;
										ConstraintsSenses[constraintsCounter] = 'L';
										constraintsCounter++;
										ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp)]=1;
										lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp)].AddCoefficient(constraintsCounter,1);	
										RightHandSideValues[constraintsCounter] = 100000;
										ConstraintsSenses[constraintsCounter] = 'L';
										sr.WriteLine("(MRSCSPVAR={0})",VariblesVectorJFIMRL.IndexOf(Mrscsp));
										constraintsCounter--;
									}								
								}
								sr.WriteLine("\nConstraint {0} IS {1} TO {2}",constraintsCounter,ConstraintsSenses[constraintsCounter],RightHandSideValues[constraintsCounter]);
								sr.WriteLine("-----------------------------");
								constraintsCounter++;
								constraintsCounter++;
							}
						}						
					}
				}
			}	
             * */
			#endregion
		
			#region Creating Constraints No. 18	
            /*
			// *** Creating Constraints No. 18	
			sr.WriteLine("\n\t\tThe constraints 18 creation:\n");
			sr.WriteLine("\t\t=============================");
				mode = new int();
				operation = new int();
				family = new int();
				job = new int();
			
			for (int i=0;i<dataSet.Families.Rows.Count;i++)
			{	
				IFinishfList.Clear();		
				DataRow[]jobs =
					dataSet.Families[i].GetChildRows(familiesJobesRelation);
				DataRow[]operationsToFamilies =
					dataSet.Families[i].GetChildRows(familiesOperationsToFamiliesRelation);
			//	sr.WriteLine("Adding:");
				for (int oper=0;oper<operationsToFamilies.Length;oper++)
				{
					IFinishfList.Add(operationsToFamilies[oper].ItemArray[2]);//dataSet.OperationsToFamilies[oper].Operation_ID);
				//	sr.Write("{0}, ", operationsToFamilies[oper].ItemArray[2]);//dataSet.OperationsToFamilies[oper].Operation_ID);
				}
				DataRow[]Precedence =
					dataSet.Families[i].GetChildRows(familiesPrecedenceRelation);
			//	sr.WriteLine("Removing:");
				for (int prec_oper=0;prec_oper< Precedence.Length ;prec_oper++)
				{									
					IFinishfList.Remove(Precedence[prec_oper].ItemArray[2]);//.Previous_Operation_ID);
				//	sr.WriteLine("{0}, ",Precedence[prec_oper].ItemArray[2]);//dataSet.Precedence[prec_oper].Previous_Operation_ID);
				}											
				for (int k=0;k<jobs.Length;k++)
				{
					foreach (int ob in IFinishfList) 
					{
						// *** Calculationg the modes of each operation
						DataView modesView = new DataView(dataSet.Modes);
						modesView.RowFilter = "Operation_ID ="+ ob;
						
						// *** Creating The Tjfi
						sr.Write("Tj={0},f={1},i1={2}",jobs[k].ItemArray[2],dataSet.Families.Rows[i].ItemArray[1],ob);
						operation=ob;
						family = (int)dataSet.Families.Rows[i].ItemArray[1];
						job = (int)jobs[k].ItemArray[2];
						ulong[] mrscspTjfiVariableArray=new ulong[3] {(ulong)job,(ulong)family,(ulong)operation};
						creatMrscspVariable(ref Mrscsp, mrscspTjfiVariableArray);
						if (!VariblesVectorJFIMRL.Contains(Mrscsp))
						{
							sr.WriteLine("BUUUG  18");
						}
						else
						{
							ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp)]=1;
							lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp)].AddCoefficient(constraintsCounter,1);	
							sr.WriteLine("(MRSCSPVAR={0}",VariblesVectorJFIMRL.IndexOf(Mrscsp));
						}
						// *** Adding The F Variable
						if (!VariblesVectorJFIMRL.Contains("F"))
						{
							sr.WriteLine("BBBUUUGGGG   18");
						}
						else
						{
							ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf("F")]=-1;
							lindoVariableVector[VariblesVectorJFIMRL.IndexOf("F")].AddCoefficient(constraintsCounter,-1);	
						}
						// *** Creating The Yjfim's
						for (int numModes=0;numModes<modesView.Count;numModes++)
						{
							// *** Calculationg the ti,m according i,m
							DataView ResourceUsageView = new DataView(dataSet.ResourceUsage);
							ResourceUsageView.RowFilter = "Operation_ID ="+ ob + "AND Mode_ID =" + modesView[numModes]["Mode_ID"];
							int Max_tfimr=0;
							int Min_tsimr=100000;
							for(int v=0;v<ResourceUsageView.Count;v++)
							{
								if (Max_tfimr<(int)ResourceUsageView[v]["Tf"])
									Max_tfimr=(int)ResourceUsageView[v]["Tf"];
								if (Min_tsimr>(int)ResourceUsageView[v]["Ts"])
									Min_tsimr=(int)ResourceUsageView[v]["Ts"];
								//	System.Console.WriteLine("Operation {0} Length is{1}",ob,Max_tfimr-Min_tsimr);							
							}							
							sr.Write("TS={0}, Tf={1}", Min_tsimr,Max_tfimr);
							int t_im = Max_tfimr- Min_tsimr;
							sr.WriteLine("\tt_im={0}",t_im);

							sr.WriteLine("Yj={0},f={1},i={2},m={3}",jobs[k].ItemArray[2],dataSet.Families.Rows[i].ItemArray[1],ob,modesView[numModes]["Mode_ID"]);
							mode = (int)modesView[numModes]["Mode_ID"];
							vParameterIdentifier = 999;
							//Console.WriteLine("mode={0}  operation={1}  family={2} job={3}",mode,operation,family,job);
							ulong[] mrscspVariableArray=new ulong[5] {(ulong)vParameterIdentifier,(ulong)job,(ulong)family,(ulong)operation,(ulong)mode };
							creatMrscspVariable(ref Mrscsp, mrscspVariableArray);
							if (!VariblesVectorJFIMRL.Contains(Mrscsp))
							{
								sr.WriteLine("BBBUUUGGGG   18");
							}
							else
							{
								ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp)]=t_im;
								lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp)].AddCoefficient(constraintsCounter,t_im);	
								RightHandSideValues[constraintsCounter] = 0;
								ConstraintsSenses[constraintsCounter] = 'L';
							}
						}
						sr.WriteLine("\nConstraint {0} IS {1} TO {2}",constraintsCounter,ConstraintsSenses[constraintsCounter],RightHandSideValues[constraintsCounter]);
						sr.WriteLine("-----------------------------");
						constraintsCounter++;
					}
				}
			}	
			//constraintsCounter--; // taking this variable to the real amount of constraints it should represent and not the next one as we did in the previous sections where we build the problems constraints		
			*/
              #endregion

			#region Bound B2.1 Creating Constraints No. 19.2
/*
			sr.WriteLine("\n\t\tThe constraints Bound B2.1 19.2 creation:\n");	
			sr.WriteLine("\n\t\t============================\n");	
			// *** Creating the Constraint Equations
			for (byte r=0;r<dataSet.Resources.Count;r++)
			{		
				DataRow[]resourceResourceUsage =
					dataSet.Resources[r].GetChildRows(resourceResourcesUsageRelation);    									
				//for (int task=1;task<resourceLr[r];task++)
				// **** Creating the Trl 
				ulong[] mrscspTrlVariableArray=new ulong[2] {(ulong)resourceID[r],(ulong)resourceLr[r]};
				sr.Write("The Tr={0},l={1}",resourceID[r],resourceLr[r]);
				creatMrscspVariable(ref Mrscsp, mrscspTrlVariableArray);
				if (!VariblesVectorJFIMRL.Contains(Mrscsp))
				{
					sr.WriteLine("BUG BUG BUG  19.2");
				}
				else
				{ 					
					ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf(Mrscsp)]=1;
					lindoVariableVector[VariblesVectorJFIMRL.IndexOf(Mrscsp)].AddCoefficient(constraintsCounter,1);						
					sr.WriteLine(" (MRSCSPVAR={0})",VariblesVectorJFIMRL.IndexOf(Mrscsp));
				}
				// *** Adding The F Variable
				if (!VariblesVectorJFIMRL.Contains("F"))
				{
					sr.WriteLine("BUG BUG BUG  19.2");
					Console.WriteLine("BUG BUG BUG  19.2");
				}
				else
				{
					ConstraintsMatrix[constraintsCounter,VariblesVectorJFIMRL.IndexOf("F")]=-1;
					lindoVariableVector[VariblesVectorJFIMRL.IndexOf("F")].AddCoefficient(constraintsCounter,-1);	
					RightHandSideValues[constraintsCounter] = 0;
					ConstraintsSenses[constraintsCounter] = 'L';
				}
				constraintsCounter++;
			}
*/
			#endregion	
			
			#region Lindos Vectors		

			//int [] anRoX = new int();
			ArrayList anRoxList = new ArrayList();
			//double[] adA = new double();
			ArrayList adAList = new ArrayList();
			ArrayList pnLenColList = new ArrayList();
			ArrayList anBegCol_1 = new ArrayList();

			for (int i=0;i<VariblesVectorJFIMRL.Count;i++)
			{					
				lindoVariableVector[i].TransferListsToVectors(ref adAList, ref anRoxList, ref pnLenColList);
			}			

			// Creating the anBegCol Vector
			System.Collections.IEnumerator myEnumeratorPn = pnLenColList.GetEnumerator();
			//System.Collections.IEnumerator myEnumeratorAnRoxList = anRoxList.GetEnumerator();
			//myEnumeratorAnRoxList.MoveNext();
			int accumulator = 0;//(int)myEnumeratorAnRoxList.Current;
			anBegCol_1.Add(accumulator); 
		//	Console.Write("The anBegCol_1={0}",accumulator);
			//======================================
			//myEnumeratorPn.MoveNext();
			
			for (int i=0;i<(pnLenColList.Count-1);i++)
			{

				//			while ( myEnumeratorPn.MoveNext() )
				//			{
				myEnumeratorPn.MoveNext();
				accumulator=accumulator + (int)myEnumeratorPn.Current;
				//Console.WriteLine("The myEnumeratorPn.Current={0} accumaltor={1}\n",myEnumeratorPn.Current,accumulator);
				anBegCol_1.Add(accumulator);
			}
			anBegCol_1.Add(adAList.Count);
		//	Console.WriteLine("anBegCol_1.Count={0}",anBegCol_1.Count);

			//***  PRINTING THE LINDO'S VECTORS  ***
			
			sr.WriteLine("\nPrinting 2 Lists: adA, anRoxList");
			System.Collections.IEnumerator myEnumerator1 = adAList.GetEnumerator();
			System.Collections.IEnumerator myEnumerator2 = anRoxList.GetEnumerator();
			System.Collections.IEnumerator myEnumerator3 = pnLenColList.GetEnumerator();
			System.Collections.IEnumerator myEnumerator4 = anBegCol_1.GetEnumerator();
					
			ulong variableNumber = new ulong();
			variableNumber = 0;
			sr.WriteLine("pnLenColList");
			int zeroCounter = 0;
			while ( myEnumerator3.MoveNext() )
			{				
				sr.Write( "{0}-{1},",myEnumerator3.Current,	variableNumber);
				variableNumber++;
				if ((int)myEnumerator3.Current==0)
				{
					zeroCounter++;
				}
			}
			sr.WriteLine("");
			sr.WriteLine("number of ZEROES={0}",zeroCounter);
			variableNumber = 0;
			sr.WriteLine("adA");			
			while ( myEnumerator1.MoveNext() )
			{				
				sr.Write( "{0}-{1},",myEnumerator1.Current,	variableNumber);				
				variableNumber++;
			}
			
			sr.WriteLine("\nanRoxList");
			variableNumber = 0;
			while ( myEnumerator2.MoveNext() )
			{							
				sr.Write( "{0}-{1},",myEnumerator2.Current,	variableNumber);	
				variableNumber++;
			}
			sr.WriteLine("\nanBegCol");
			variableNumber = 0;
			while ( myEnumerator4.MoveNext() )
			{							
				sr.Write( "{0}-{1},",myEnumerator4.Current,	variableNumber);	
				variableNumber++;
			}
						
			
			//***  END - PRINTING THE LINDO'S VECTORS - END ***
	
			
			
			//PrintValues(VariblesVectorJFIMRL);
			
			sr.WriteLine("TOTAL NUMBER VARAIABLES IN VariblesVectorJFIMRL:\nXjfimrl - Starting index=0  | Counter={0},\nYjfim   - Starting index={1} | Counter={2}\nYimrl   - Starting index={3} | Counter={4}",counterXjfimrlInVariablesVector,startIndexYjfimInVariableVector,counterYjfimInVariablesVector,startIndexYimrlInVariableVector,counterYimrlInVariablesVector);
			sr.WriteLine("Timrl   - Starting index={0} | Counter={1}",startIndexTjfiInVariableVector,counterTjfiInVariablesVector);
			sr.WriteLine("Total Variables={0}",VariblesVectorJFIMRL.Count);
			
			for (int row=0;row<constraintsCounter;row++)
			{
				sr.Write("{0}|",row);
				for (int col=0;col<(VariblesVectorJFIMRL.Count);col++)
					sr.Write("{0}",ConstraintsMatrix[row,col]);
				sr.Write("{0}{1}",ConstraintsSenses[row],RightHandSideValues[row]);
				sr.WriteLine("");;// .Write("\n");
			}
			ConstraintsMatrix.Initialize();
			 
			#endregion
						
			//*****************************************************************************
	
			#region LINDO "MACHINE 1"
			
	//public class BIP
	//{		
		//public static void Main (string[] args)
	//	{

		/*	int nErrorCode = lindo.LSERR_NO_ERROR;

			// Number of constraints 
			int nM = 4;

			// Number of variables 
			int nN = 4;

			// declare an instance of the LINDO environment object 
			int pEnv = 0;

			// declare an instance of the LINDO model object 
			int pModel = 0;        
                
			// initialize the counter that counts the number of times 
			//the callback function is called //
			CallbackData cbd = new CallbackData();         
		
			int nSolStatus = lindo.LS_STATUS_UNKNOWN;

			StringBuilder LicenseKey = new StringBuilder(lindo.LS_MAX_ERROR_MESSAGE_LENGTH);

			// >>> Step 1 <<< Create a LINDO environment. Note:
			//MY_LICENSE_KEY must be defined to be the license key
			//shipped with your software. //

			nErrorCode = lindo.LSloadLicenseString("\\lindoapi\\license\\lndapi40.lic", LicenseKey);
			APIErrorCheck(pEnv,nErrorCode);
		
		
			pEnv = lindo.LScreateEnv(ref nErrorCode, LicenseKey.ToString()); 
			if ( nErrorCode == lindo.LSERR_NO_VALID_LICENSE)  
			{
				Console.WriteLine("Invalid License Key!\n");
				return;
			}
			APIErrorCheck(pEnv,nErrorCode);

			// >>> Step 2 <<< Create a model in the environment. //
			pModel = lindo.LScreateModel ( pEnv, ref nErrorCode);
			APIErrorCheck(pEnv,nErrorCode);

			// >>> Step 3 <<< Specify the model.

			// The direction of optimization //
			int nDir = lindo.LS_MAX;

			// The objective's constant term //
			double dObjConst = 0.0;

			// The coefficients of the objective function //
			double [] adC = new double[] { 1.0, 1.0, 1.0, 1.0};

			// The right-hand sides of the constraints //
			double [] adB = new double[] { 1.0, 1.0, 1.0, 1.0};

			// The constraint types //
			string acConTypes = "EEEE";

			// The number of nonzeros in the constraint matrix //
			int nNZ = 4;

			// The indices of the first nonzero in each column //
			int [] anBegCol = new int[]{ 0, 1, 2, 3, nNZ};

			// The length of each column.  Since we aren't leaving
			//any blanks in our matrix, we can set this to NULL //
			int [] pnLenCol = new int[]{1, 1, 1, 1};

			// The nonzero coefficients //
			double [] adA = new double[] { 1.0, 1.0, 1.0, 1.0};

			// The row indices of the nonzero coefficients //
			int [] anRowX = new int[]{ 0, 1, 2, 3};

			// Simple upper and lower bounds on the variables.
			//By default, all variables have a lower bound of zero
			//and an upper bound of infinity.  Therefore pass NULL
			//pointers in order to use these default values. //
			//new double[] {0.0, 0.0}
			//new double[] {lindo.LS_INFINITY,lindo.LS_INFINITY}
			double [] pdLower = null;
			double [] pdUpper = null;

			//	string [] varnames = new string[] {"Variable1","Variable2","Variable3","Variable4"};
			//		string [] connames = new string[] {"Constraint1","Constraint2","Constraint3","Constraint4"};
	        	        

			// We have now assembled a full description of the model.
			//We pass this information to LSloadLPData with the
			//following call. 
			nErrorCode = lindo.LSloadLPData( pModel, nM, nN, nDir,
				dObjConst, adC, adB, acConTypes, nNZ, anBegCol,
				pnLenCol, adA, anRowX, pdLower, pdUpper);
			APIErrorCheck(pEnv,nErrorCode);

			// Mark all Variables as being Binary Integer
			nErrorCode = lindo.LSloadVarType( pModel, "BBBB");
			APIErrorCheck(pEnv,nErrorCode);

			// >>> Step 4 <<< Perform the optimization //
			int Null1=0;
			nErrorCode = lindo.LSsolveMIP( pModel, ref Null1);
			APIErrorCheck(pEnv,nErrorCode);

			// >>> Step 5 <<< Retrieve the Solution //
			
			double [] dStart = new double[4];
			double [] dSlack = new double[4];
			double dObj=0.0;

			//double dObjVal, dStart [4], dSlacks [4];
			nErrorCode = lindo.LSgetInfo( pModel, lindo.LS_DINFO_MIP_OBJ, ref dObj);
			APIErrorCheck(pEnv,nErrorCode);
			nErrorCode = lindo.LSgetMIPPrimalSolution( pModel, dStart);
			APIErrorCheck(pEnv,nErrorCode);
			nErrorCode = lindo.LSgetMIPSlacks( pModel, dSlack);
			APIErrorCheck(pEnv,nErrorCode);
			
			//Display Solution in dialog box
			Console.WriteLine("\tYeS{0}",dObj);

			// >>> Step 6 <<< Delete the LINDO environment //
			nErrorCode = lindo.LSdeleteModel( ref pModel);
			nErrorCode = lindo.LSdeleteEnv( ref pEnv);*/
			#endregion LINDO "MACHINE 1"

			#region LINDO "MACHINE 2 "
			/************************************************************
			
			/************************************************************/
			d1 = DateTime.Now;
			sr.WriteLine("STARTING LINDO={0}", d1);
			int nErrorCode = lindo.LSERR_NO_ERROR;

			// Number of constraints 
			int nM = (int)constraintsCounter;
			sr.WriteLine("Number of constraints {0},={1}",nM ,constraintsCounter);

			// Number of variables 
			int nN = VariblesVectorJFIMRL.Count;
			sr.WriteLine("Number of variables {0},={1}",nN ,VariblesVectorJFIMRL.Count);

			 //declare an instance of the LINDO environment object 
			System.IntPtr pEnv = (System.IntPtr)0;

			// declare an instance of the LINDO model object 
			System.IntPtr pModel = (System.IntPtr)0;        
                
			//initialize the counter that counts the number of times 
			//the callback function is called 
			CallbackData cbd = new CallbackData();         
		
			int nSolStatus = lindo.LS_STATUS_UNKNOWN;

			StringBuilder LicenseKey = new StringBuilder(lindo.LS_MAX_ERROR_MESSAGE_LENGTH);

			// >>> Step 1 <<< Create a LINDO environment. Note:
			//MY_LICENSE_KEY must be defined to be the license key
			//shipped with your software. 

			nErrorCode = lindo.LSloadLicenseString("C:\\Lindoapi\\license\\lndapi50.lic", LicenseKey);
			APIErrorCheck(sr,(int)pEnv,nErrorCode);		
		
			pEnv = lindo.LScreateEnv(ref nErrorCode, LicenseKey.ToString()); 
			if ( nErrorCode == lindo.LSERR_NO_VALID_LICENSE)  
			{
				Console.WriteLine("Invalid License Key!\n");
				sr.WriteLine("Invalid License Key!\n");
				return;
			}
			APIErrorCheck(sr,(int)pEnv,nErrorCode);

			// >>> Step 2 <<< Create a model in the environment. 
			pModel = lindo.LScreateModel (pEnv, ref nErrorCode);
			APIErrorCheck(sr,(int)pEnv,nErrorCode);

			// >>> Step 3 <<< Specify the model.

			// The direction of optimization 
			int nDir = lindo.LS_MIN;

			// The objective's constant term 
			double dObjConst = 0.0;

			// The coefficients of the objective function 
			double [] adC = new double[VariblesVectorJFIMRL.Count] ;
			for (int i = 0; i < VariblesVectorJFIMRL.Count-1; i++)
				adC[i] = 0.0;
			adC[VariblesVectorJFIMRL.Count-1] = 1.0;
	//***>>> PaY ATTENTION	//		adC[45] = 1.0;

			// The right-hand sides of the constraints 
			double [] adB = new double[nM];// double[] { 1.0, 1.0, 1.0, 1.0};
			//int[] arr2 = (int[])arr1.Clone();
			//double [] adB = (double[])RightHandSideValues.Clone();
			int index = 0;			
			//Console.WriteLine("***************************************");
			//Console.WriteLine("The number of Right={0} ",RightHandSideValues.Length);
			//Console.WriteLine("***************************************");
			while ( index<constraintsCounter )
			{					
				adB[index]=RightHandSideValues[index];				
			//	Console.Write("..{0}={1}",RightHandSideValues[index],adB[index]);
				index++;
			}
			//Console.WriteLine("***************************************");
			
			/*foreach (int element in adB)
			{
				System.Console.WriteLine(element);
			}*/
			//Console.WriteLine("***************************************");
						
			// The constraint types 
			System.Collections.IEnumerator myEnumeratorConstraintsSenses = ConstraintsSenses.GetEnumerator();
			StringBuilder sb = new StringBuilder();
			while ( myEnumeratorConstraintsSenses.MoveNext() )
			{				
				sb.Append((char)myEnumeratorConstraintsSenses.Current);
			}
			
			string acConTypes = sb.ToString();// "EEEE";
			//Console.WriteLine("***************************************");
			//Console.WriteLine("acConTypes={0}",acConTypes);
			// The number of nonzeros in the constraint matrix 
			int nNZ = (int)adAList.Count;
			//Console.WriteLine("adAList.Count={0}..nNZ={1}",adAList.Count,nNZ);
			 
			// The indices of the first nonzero in each column 
			int [] anBegCol = new int[(int)anBegCol_1.Count];//{ 0, 1, 2, 3, nNZ};
			//System.Console.WriteLine("The number of anBegCol={0}",anBegCol_1.Count);
			System.Collections.IEnumerator myEnumeratoranBegCol_1 = anBegCol_1.GetEnumerator();
			index=0;
			while ( myEnumeratoranBegCol_1.MoveNext() )
			{				
				anBegCol[index]=(int)myEnumeratoranBegCol_1.Current;
				//Console.Write("Index={0}anBeg={1}",index,(int)myEnumeratoranBegCol_1.Current);
				index++;
			}

			// The length of each column.  Since we aren't leaving
			//any blanks in our matrix, we can set this to NULL 
			int [] pnLenCol = new int[(int)pnLenColList.Count];//{1, 1, 1, 1};
			System.Collections.IEnumerator myEnumeratorpnLenColList = pnLenColList.GetEnumerator();
			index=0;
			while ( myEnumeratorpnLenColList.MoveNext() )
			{				
				pnLenCol[index]=(int)myEnumeratorpnLenColList.Current;
				index++;
			}
			//Console.WriteLine("Number={0} =={1}",pnLenColList.Count,pnLenCol.Length);
			//Console.WriteLine("pnLenCol[4]={0},pnLenCol[71]={1}",pnLenCol[4],pnLenCol[71]);

			// The nonzero coefficients 
			double [] adA = new double[(int)adAList.Count];//{ 1.0, 1.0, 1.0, 1.0};
			System.Collections.IEnumerator myEnumeratoradA = adAList.GetEnumerator();
			index=0;
			int even=1;
			//while ( myEnumeratoradA.MoveNext() )
			while ( index<adAList.Count )
			{				
				myEnumeratoradA.MoveNext();
				adA[index]=(double)myEnumeratoradA.Current;
				if (even==3)
				{
					//Console.Write("i={0},adA={1},EnumeradA.Current={2} | ",index,adA[index],myEnumeratoradA.Current);
				//	Console.WriteLine(" ");
					even=1;
				}				
				//Console.Write("i={0},adA={1},EnumeradA.Current={2} | ",index,adA[index],myEnumeratoradA.Current);
				index++;
				even++;
			}

			// The row indices of the nonzero coefficients 
			int [] anRowX = new int[(int)anRoxList.Count];//{ 0, 1, 2, 3};
			//Console.WriteLine("anRowx.count={0}",anRoxList.Count);
			System.Collections.IEnumerator myEnumeratoranRoxList = anRoxList.GetEnumerator();
			index=0;
			while ( myEnumeratoranRoxList.MoveNext() )
			{				
				anRowX[index]=(int)myEnumeratoranRoxList.Current;
				//Console.WriteLine("i={0},anRowX[index]={1}..{2}",index,anRowX[index],myEnumeratoranRoxList.Current);
				index++;
			}
			// Simple upper and lower bounds on the variables.
			//By default, all variables have a lower bound of zero
			//and an upper bound of infinity.  Therefore pass NULL
			//pointers in order to use these default values.

			//new double[] {0.0, 0.0}
			//new double[] {lindo.LS_INFINITY,lindo.LS_INFINITY}
			double [] pdLower = new double[VariblesVectorJFIMRL.Count];//null;//
            double [] pdUpper = new double[VariblesVectorJFIMRL.Count];//null;//
			string var_Type = LindoVarType.ToString();
			char[] cArr = var_Type.ToCharArray(0,var_Type.Length);
			//int[] upBound = new int[var_Type.Length];
			for (int i=0;i< pdUpper.Length;i++)
			{
				if (cArr[i]=='B')
				{
					pdUpper[i]=1.0;
					pdLower[i]=0.0;
				}
				else
				{
					pdUpper[i]=lindo.LS_INFINITY;
					pdLower[i]=0.0;
				}
			}
			
//020807			for (int i = 0; i < VariblesVectorJFIMRL.Count; i++)
		//	{
		//		pdLower[i]=0.0;
		//		pdUpper[i]=lindo.LS_INFINITY;
		//		//Console.WriteLine(">>{0}",i);
		//	}
			//Console.WriteLine("VariblesVectorJFIMRL.Count={0}",VariblesVectorJFIMRL.Count);
			
			/*index=0;
			foreach (int element in pdLower)
			{
				System.Console.WriteLine("index={0},element={1}",index,element);
				index++;
			}*/

		//	string [] varnames = new string[] {"Variable1","Variable2","Variable3","Variable4"};
		//	string [] connames = new string[] {"Constraint1","Constraint2","Constraint3","Constraint4"};
	        	        

			// We have now assembled a full description of the model.
			//We pass this information to LSloadLPData with the
			//following call. 

			// nErrorCode = LSloadLPData(pModel, BackUPnM, BackUPnN, nDir, dObjConst, adC, RightSideConstraints, 
            //      SignsConstraints, BackUPnNZ,BackUPanBegCol, pnLenCol, BackUPadA, BackUPanRowX, pdLower, pdUpper);
  
		//	Console.WriteLine("Perform the Step 3");
			sr.WriteLine("Perform the Step 3");
			nErrorCode = lindo.LSloadLPData( (System.IntPtr)pModel, nM, nN, nDir,
				dObjConst, adC, adB, acConTypes, nNZ, anBegCol,
				pnLenCol, adA, anRowX, pdLower, pdUpper);
			APIErrorCheck(sr,(int)pEnv,nErrorCode);

			// Mark all Variables as being Binary Integer		
			sr.WriteLine("Mark All Variables");
			string VarType = LindoVarType.ToString();
			//Console.WriteLine("LindoVarType.ToString()={0}\t\nVarType={1}",LindoVarType,VarType);
			nErrorCode = lindo.LSloadVarType( (System.IntPtr)pModel, VarType);
			APIErrorCheck(sr,(int)pEnv,nErrorCode);


			// >>> Step 4 <<< Perform the optimization //
	//		Console.WriteLine("Perform the optimization");
			sr.WriteLine("Perform the optimization");
			int Null1=0;
			//int nStatus = lindo.LS_STATUS_UNKNOWN;	
		//sr.Close();					
			//17.06.07nErrorCode = lindo.LSsolveMIP( pModel,ref Null1);//ref nStatus);//Object reference not set to an instance of an object
			//18.02.07
			
			nErrorCode = lindo.LSoptimize((System.IntPtr)pModel,0, ref Null1);
			APIErrorCheck(sr,(int)pEnv,nErrorCode);
	
			sr.WriteLine("Parameter - LS_STATUS_INFEASIBLE={0}",lindo.LS_STATUS_INFEASIBLE);
			sr.WriteLine("Parameter - LS_STATUS_OPTIMAL={0}",lindo.LS_STATUS_OPTIMAL);
			sr.WriteLine("Parameter - Null1={0}",Null1);
			// >>> Step 5 <<< Retrieve the Solution //
			
			double [] dStart = new double[VariblesVectorJFIMRL.Count];
			double [] dSlack = new double[VariblesVectorJFIMRL.Count];
			double dObj=0.0;
			double dObjCont=0.0;
			double lTime1=0.0;
			double lTime2=0.0;
			double lTimeRelaxation = 0.0;

			//double dObjVal, dStart [4], dSlacks [4];
			nErrorCode = lindo.LSgetInfo((System.IntPtr)pModel, lindo.LS_DINFO_POBJ, ref dObjCont);
			APIErrorCheck(sr,(int)pEnv,nErrorCode);
//150707			nErrorCode = lindo.LSgetInfo( pModel, lindo.LS_DINFO_MIP_OBJ, ref dObj);
//150707			APIErrorCheck(sr,pEnv,nErrorCode);
//150707			nErrorCode = lindo.LSgetMIPPrimalSolution( pModel, dStart);
//150707			APIErrorCheck(sr,pEnv,nErrorCode);
//150707			nErrorCode = lindo.LSgetMIPSlacks( pModel, dSlack);
//150707			APIErrorCheck(sr,pEnv,nErrorCode);
//150707			nErrorCode = lindo.LSgetInfo( pModel,lindo.LS_DINFO_MIP_OPT_TIME, ref lTime1);
//150707			APIErrorCheck(sr,pEnv,nErrorCode);
//150707			nErrorCode = lindo.LSgetInfo( pModel,lindo.LS_DINFO_MIP_TOT_TIME, ref lTime2);
//150707			APIErrorCheck(sr,pEnv,nErrorCode);
			nErrorCode = lindo.LSgetInfo((System.IntPtr)pModel,lindo.LS_IINFO_ELAPSED_TIME, ref lTimeRelaxation);
			APIErrorCheck(sr,(int)pEnv,nErrorCode);
			nErrorCode = lindo.LSgetPrimalSolution((System.IntPtr)pModel, dStart);
			APIErrorCheck(sr,(int)pEnv,nErrorCode);
			
			nErrorCode = lindo.LSgetSlacks((System.IntPtr)pModel, dSlack);
			APIErrorCheck(sr,(int)pEnv,nErrorCode);
			index=0;
			foreach (double element in dStart)
			{
				//120707		System.Console.WriteLine("[index]={0},dStart={1} dSlack={2}",index,element,dSlack[index]);
				sr.WriteLine("[index]={0},dStart={1} dSlack={2}",index,element,dSlack[index]);
				index++;
			}
			sr.Flush();
			//Display Solution in dialog box
	//		Console.WriteLine("\tSOLUTION = {0}",dObj);
	//		Console.WriteLine("\tSOLUTION = {0}",dObjCont);
	//		Console.WriteLine("\tOpt Time = {0}",lTime1);
	//		Console.WriteLine("\tTotal Time = {0}",lTime2);
			sr.WriteLine("\tSOLUTION = {0}",dObj);
			sr.WriteLine("\tSOLUTION = {0}",dObjCont);
			sr.WriteLine("\tOpt Time = {0}",lTime1);
			sr.WriteLine("\tTotal Time = {0}",lTime2);
			sr.WriteLine("\tTotal Time = {0}",lTimeRelaxation);

			
			
		#region Deletion Example
			// * * * Deletion Example * * *
		/*	Console.WriteLine(" ---------------   Perform the Deletion   -------------");
			
			int[] DelVars = new int[27] {36,35,34,31,30,29,28,27,26,23,22,21,19,18,17,15,14,13,12,11,10,7,6,5,4,3,2};//{2,3,4,5,6,7,10,11,12,13,14,15,17,18,19,21,22,23};
			
			int NumDelVar = 27;
			
			nErrorCode = lindo.LSdeleteVariables(pModel, NumDelVar, DelVars); 
			APIErrorCheck(pEnv,nErrorCode);
			//int[] DelVars = {61};
			// >>> Step 444 <<< Perform the optimization //
			Console.WriteLine("Perform the 2nd optimization");
			Null1=0;
			//int nStatus = lindo.LS_STATUS_UNKNOWN;
			nErrorCode = lindo.LSsolveMIP( pModel,ref Null1);//ref nStatus);//Object reference not set to an instance of an object
			APIErrorCheck(pEnv,nErrorCode);
			Console.WriteLine("Parameter - LS_STATUS_INFEASIBLE={0}",lindo.LS_STATUS_INFEASIBLE);
			Console.WriteLine("Parameter - LS_STATUS_OPTIMAL={0}",lindo.LS_STATUS_OPTIMAL);
			Console.WriteLine("Parameter - Null1={0}",Null1);
			// >>> Step 555 <<< Retrieve the Solution //
			
			dStart.Initialize();// = new double[VariblesVectorJFIMRL.Count];
			dSlack.Initialize();// = new double[VariblesVectorJFIMRL.Count];
			dObj=0.0;

			//double dObjVal, dStart [4], dSlacks [4];
			nErrorCode = lindo.LSgetInfo( pModel, lindo.LS_DINFO_MIP_OBJ, ref dObj);
			APIErrorCheck(pEnv,nErrorCode);
			nErrorCode = lindo.LSgetMIPPrimalSolution( pModel, dStart);
			APIErrorCheck(pEnv,nErrorCode);
			nErrorCode = lindo.LSgetMIPSlacks( pModel, dSlack);
			APIErrorCheck(pEnv,nErrorCode);
			
			//Display Solution in dialog box
			Console.WriteLine("\tSOLUTION = {0}",dObj);
			index=0;Console.WriteLine("The SP Creator {0}",intId);
			foreach (int element in (dStart))
			{
				System.Console.WriteLine("[index]={0},dStart={1} dSlack={2}",index,element,dSlack[index]);
				index++;
			}*/
			#endregion*/

		
		//  ***  **  *   CREATING  THE  SP   ***  **  *

			/*DataView FamilyView = new DataView(dataSet.Families);
			DataView FamilyOperationView = new DataView(dataSet.OperationsToFamilies);
			DataView JobsView = new DataView(dataSet.Jobs);
			DataView ModesView = new DataView(dataSet.Modes);

			SP_Creator.SP_Environment spEnvi = new SP_Creator.SP_Environment( sr, ref FamilyView,ref FamilyOperationView, ref JobsView, ref ModesView,
																			ref VariblesVectorJFIMRL, ref pModel, ref Null1, ref pEnv,ref nM, ref nN, ref nDir,
																			ref dObjConst, ref adC, ref adB, ref acConTypes, ref nNZ, ref anBegCol,
																			ref pnLenCol, ref adA, ref anRowX, ref pdLower, ref pdUpper);
			*/
			


			// >>> Step 6 <<< Delete the LINDO environment 
			nErrorCode = lindo.LSdeleteModel( ref pModel);
			nErrorCode = lindo.LSdeleteEnv( ref pEnv);
			
			
	//150707		YxNetLinksMRCCSP.PrintYxNetLinks(sr);
			sr.Close();	
			StreamWriter sr2 = File.CreateText("MyFile1.txt");
			sr2.WriteLine("STARTING Time={0}", d1);
			sr2.WriteLine("------------------------------------");
			DateTime d2 = DateTime.Now;
            /*
			spEnvi.SpLindoResults(sr2, ref nM,ref nN, ref nDir, ref dObjConst,
								  ref adC, ref adB,	ref acConTypes, ref VarType, ref nNZ, ref anBegCol, ref pnLenCol,
								  ref adA, ref anRowX, ref pdLower, ref pdUpper, ref YxNetLinksMRCCSP,ref VariblesVectorJFIMRL);
            */
            //			spEnvi.SpBestSolution(sr);
			//**********************************************************
			//* Sending the Best SP to Creat the Resources Environment *
			//**********************************************************
			sr2.Flush();
			int[] arraySpSample;
            /*
			DataView resourceUsageView1 = new DataView(dataSet.ResourceUsage);
			DataView resourcesView = new DataView(dataSet.Resources);
              */
			// ********************************************************
			// *** printing all the tasks of the resources of each SP
			// ********************************************************
			
			/*for (int i=0;i<spEnvi.solIndex.Length;i++)//.sortedFeasibleSolutions.Count;i++)
			{
				spEnvi.CalculateSP_Characteristics(sr, )
			}*/
			DateTime dStartBB, dFinishBB, dNow;
			dStartBB = DateTime.Now;
			long counter_Iter = 0;
			double[] dStartFinal = new double[209];
			int[] deletedVarVector = new int[3000];
			int lengthDelVec = 0;
			double objFunct = 1000000;
			double UB = 0;
			/*
			for (int i=0;i<spEnvi.solIndex.Length;i++)//.sortedFeasibleSolutions.Count;i++)
			{
				if ((spEnvi.solVal[i]<objFunct)|(objFunct==1000000))
				{
					sr2.WriteLine("sp={0} sp_source= {1} solVal={2} - objFunct={3}",spEnvi.solIndex[i],spEnvi.SpinfoAfterSetConst[spEnvi.solIndex[i]].sourceNumSP,spEnvi.solVal[i],objFunct);
					Console.WriteLine("sp={0} sp_source= {1} solVal={2} - objFunct={3}",spEnvi.solIndex[i],spEnvi.SpinfoAfterSetConst[spEnvi.solIndex[i]].sourceNumSP,spEnvi.solVal[i],objFunct);
					dNow = DateTime.Now;					
					sr2.WriteLine("STARTING spEnvi.CreatResourceInfrastructure={0},  {1}", dNow, dNow-dStartBB );//(d1-dStartBB));
					Console.WriteLine("STARTING spEnvi.CreatResourceInfrastructure={0},  {1}",  dNow, dNow-dStartBB );//(d1-dStartBB));

					spEnvi.CreatResourceInfrastructure(sr2, ref resourceID, ref resourceLr, resourcesInfoDataTableToOrder, myResource_Lfirst_Llast_Table, myJobFamilyDataTable, OperationModeTable,
						family_Lf_Matrix, i, ref resourceUsageView1, ref YxNetLinksMRCCSP, ref batch_Res_Yimrl_Links
						, yjfim_One_Mode, ref resourcesView, ref VariblesVectorJFIMRL);
					sr2.Flush();
					double current_LB = spEnvi.solIndex[i];//solVal[i];
					//UB = spEnvi.SearchDeep
					//Console.WriteLine("NOOOOO");
					UB = spEnvi.SearchDeep(sr2, ref counter_Iter, ref dStartFinal, ref deletedVarVector, ref lengthDelVec,i ,0, ref objFunct, ref current_LB, ref nM,ref nN, ref nDir, ref dObjConst, ref adC, ref adB, ref acConTypes,
						ref VarType, ref nNZ, ref anBegCol, ref pnLenCol, ref adA, ref anRowX, ref pdLower,
						ref pdUpper, ref YxNetLinksMRCCSP,ref VariblesVectorJFIMRL, myResource_Lfirst_Llast_Table,
						myJobFamilyDataTable,OperationModeTable,family_Lf_Matrix,ref batch_Res_Yimrl_Links);
					if (UB==spEnvi.solVal[i])
					{
						d1 = DateTime.Now;					
						sr2.WriteLine("UB={0} total Time={1}",UB ,(d1-dStartBB));
						Console.WriteLine("UB={0} total Time={1}",UB ,(d1-dStartBB));
						break;
					}
					GC.Collect();
					spEnvi.NewSP(sr2);
				}
				else
				{
					break;
				}
			}
			sr2.WriteLine("OBJECT FUNC={0} # TOTAL ITERATIONS={1}",objFunct, counter_Iter);
			dFinishBB = DateTime.Now;
			sr2.WriteLine("TOTAL B&B TIME = {0} # TOTAL ITERATIONS={1}",dFinishBB-dStartBB, counter_Iter);
			
			for (int y=0;y<lengthDelVec;y++)
			{
				sr2.Write("{0},",deletedVarVector[y]);
			}
			sr2.WriteLine(" ");
			sr2.WriteLine("total deleted={0}",lengthDelVec);
			index=0;
			int indexDel=0;
			for (int t=0;t<VariblesVectorJFIMRL.Count;t++)
			{
				if ((Array.IndexOf(deletedVarVector,t)!=-1)&(indexDel<lengthDelVec)&(Array.IndexOf(deletedVarVector,t)!=lengthDelVec+1))
				{
					sr2.WriteLine("[t]={0} indexOf={1}, dStartFinal=Deleted ",t,Array.IndexOf(deletedVarVector,t));	
					indexDel++;
				}	
				else
				{
					sr2.WriteLine("[index]={0},dStartFinal={1} ",t, dStartFinal[index]);
					index++;
				}
				 
			}
			sr2.WriteLine("total dStartFinal={0}  indexDel={1}",dStartFinal.Length,indexDel);
		 */
			 
			/**/

//030807			for (int i=0;i<spEnvi.solIndex.Length;i++)//.sortedFeasibleSolutions.Count;i++)
//030807			{
//150707	 	 		if (spEnvi.Spinfo[i].Solution!=0)//.Feasible==1)
//150707	 	 		{
					//arraySpSample =((int[])spEnvi.sortedFeasibleSolutions.GetByIndex(i));		  
			//120707		Console.WriteLine("Here is the Current of BEST SP={0}",i);//arraySpSample[0]);
//030807					sr2.WriteLine("Here is the Current of BEST SP={0}",spEnvi.solIndex[i]);//arraySpSample[0]);
					// 17-05-07 ResourcesOfSP MRCCSPResources = new ResourcesOfSP( sr, arraySpSample[0], ref spEnvi, ref resourceUsageView1, ref YxNetLinksMRCCSP);
//030807					d1 = DateTime.Now;
//030807					sr2.WriteLine("STARTING spEnvi.CreatResourceInfrastructure={0}", d1);
//030807					spEnvi.CreatResourceInfrastructure(sr2, ref resourceID, ref resourceLr, resourcesInfoDataTableToOrder, myResource_Lfirst_Llast_Table, myJobFamilyDataTable, OperationModeTable,
//030807						                               family_Lf_Matrix, i/*arraySpSample[0]*/, ref resourceUsageView1, ref YxNetLinksMRCCSP, ref batch_Res_Yimrl_Links
//030807													   , yjfim_One_Mode, ref resourcesView, ref VariblesVectorJFIMRL);
//030807					sr2.Flush();	
			///		spEnvi.FindTheBestBatchSize(sr,i /*arraySpSample[0]*/,/*ref pModel, ref Null1,*/ /*ref pEnv,*/ ref nM,ref nN, ref nDir, ref dObjConst,
			///			ref adC, ref adB,	ref acConTypes, ref VarType, ref nNZ, ref anBegCol, ref pnLenCol,
			///			ref adA, ref anRowX, ref pdLower, ref pdUpper, ref YxNetLinksMRCCSP,ref VariblesVectorJFIMRL);
			///	
//030807					d1 = DateTime.Now;
//030807					sr2.WriteLine("STARTING spEnvi.ResourceLindoResults={0}", d1);
//030807					spEnvi.ResourceLindoResults(sr2,spEnvi.solIndex[i] /*arraySpSample[0]*/,/*ref pModel, ref Null1,*/ /*ref pEnv,*/ ref nM,ref nN, ref nDir, ref dObjConst,
//030807						ref adC, ref adB, ref acConTypes, ref VarType, ref nNZ, ref anBegCol, ref pnLenCol,
//030807						ref adA, ref anRowX, ref pdLower, ref pdUpper, ref YxNetLinksMRCCSP,ref VariblesVectorJFIMRL);
								
//030807					GC.Collect();
//030807					spEnvi.NewSP(sr2);
					//MRCCSPResources.
//150707	 	 		}				
//030807			}
 
			//Sequence seq1 = new Sequence(1);
		//}
	//}
//}			
			#endregion
				
			/*ArrayList a = new ArrayList();
			a.Add(1);
			a.Add(2);
			a.Add(3);*/									
			sr2.Close();				
			
		}
		#region STATIC Functions
		public static void creatMrscspVariable(ref ulong MrscspVariable, ulong[] arrayMrscsp)
		{
			MrscspVariable=arrayMrscsp[0];
			//System.Console.WriteLine("MRSCSP={0}",MrscspVariable);
			for (int i1=1;i1<arrayMrscsp.Length;i1++)
			{
				MoveBits(ref MrscspVariable,10,true);
				MrscspVariable=MrscspVariable|arrayMrscsp[i1];
			}
		}
		public static void MoveBits(ref ulong MrscspVariable,int BitTOMove, bool Direction)
		{
			ulong e_Number=MrscspVariable;
			if (Direction==true)
				MrscspVariable=e_Number << BitTOMove;
			else
				MrscspVariable=e_Number >> BitTOMove;
			//System.Console.WriteLine("\nHey MrscspVariable ={0}",MrscspVariable);
		}
		public static void PrintValues(StreamWriter sr2,IEnumerable VariblesVectorJFIMRL )  
		{
			sr2.WriteLine("\nVariblesVectorJFIMRL Printing");
		//120707	Console.WriteLine("\nVariblesVectorJFIMRL Printing");
			System.Collections.IEnumerator myEnumerator = VariblesVectorJFIMRL.GetEnumerator();
			ulong variableNumber = new ulong();
			variableNumber = 0;
			while ( myEnumerator.MoveNext() )
			{
				sr2.WriteLine( "{0}  -   {1}",variableNumber, myEnumerator.Current );
	//120707			Console.WriteLine( "{0}  -   {1}",variableNumber, myEnumerator.Current );
				variableNumber++;				
			}
			sr2.WriteLine();
	//120707		Console.WriteLine();
		}
		
		public static void Print2Values( IEnumerable arrayList1,IEnumerable arrayList2 )  
		{
			Console.WriteLine("\nPrinting 2 Lists");
			System.Collections.IEnumerator myEnumerator1 = arrayList1.GetEnumerator();
			System.Collections.IEnumerator myEnumerator2 = arrayList2.GetEnumerator();
			
			//ulong variableNumber = new ulong();
			//variableNumber = 0;
			while ( myEnumerator1.MoveNext() )
			{
				myEnumerator2.MoveNext();
				Console.WriteLine( "{0}  -   {1}",myEnumerator1.Current, myEnumerator2.Current);
				//variableNumber++;				
			}
			//Console.WriteLine();
		}
		public static void PrintKeysAndValues( SortedList myList )  
		{
			Console.WriteLine( "\t-KEY-\t-VALUE-" );
			for ( int i = 0; i < myList.Count; i++ )  
			{
				Console.WriteLine( "\t{0}:\t{1}", myList.GetKey(i), myList.GetByIndex(i) );
			}
		}
		public static void PrintKeysAndValues( int[] myKeys, int[] myValues )  
		{
			for ( int i = 0; i < myKeys.Length; i++ )  
			{
				Console.WriteLine( "   {0,-10}: {1}", myKeys[i], myValues[i] );
			}
		}
		public class Factorial 
		{
			public static long Fac(long i) 
			{
				return ((i <= 1) ? 1 : (i * Fac(i-1))); 
			} 
		}
		public static int[] FindRelation(int[] arrP, int[] arrS, int index, int operation)
		{		
			int[] arrRelation = new int[arrP.Length+1 ];
			arrRelation[index-1] = operation;
			int indexAddArrRelation = index;
			int indexCheckArrRelation = 1;

			for (int j=0;j<indexAddArrRelation;j++)
			{		
				operation = arrRelation[j];
				for (int i=index;i<arrP.Length;i++)
				{
					//	Console.Write("i={0}",i);
					if (arrP[i]==operation)
					{
						if (Check_If_Uniqu(arrRelation,arrS[i]))
						{
							arrRelation[indexAddArrRelation] = arrS[i];				
							indexAddArrRelation++;
							//	Console.WriteLine("New Relation {0} {1}, ",operation, arrS[i]);
						}
					}
				}
			}
		//120707	Console.WriteLine("The Find Relations Arr:");
		//120707	PrintValues(arrRelation);
			return(arrRelation);
		}
		public static bool Check_If_Uniqu(int[] arr, int var)
		{
			
			bool check = true;			
			for (int i=0;i<arr.Length;i++)
			{
				//Console.WriteLine("var={0} arr[i]={1} ",var,arr[i]);
				if (arr[i]==var)
				{
					check = false;
				}
			}
			//Console.WriteLine("{0}",check);
			return(check);
										 
		}
		public static int[] Translate_List_To_Array(IEnumerable myCollection, int count)
		{
			///  ***  Translating the List to Array 
			int[] jobsArr = new int[count];
			System.Collections.IEnumerator myEnumerator = myCollection.GetEnumerator();
			int index=0;
			while ( myEnumerator.MoveNext() )
			{
				jobsArr[index]=(int)(myEnumerator.Current);
				index++;
			}
			return (jobsArr);			
		}
		public static void Write_In_Matrix(ref int[,] RelMat, int[] relations, int operation)//int[] operationsArr, int operationindex)
		{
			Console.WriteLine(" RelMat.GetLength(1)={0} ",RelMat.GetLength(0));

			for (int i=0;i<RelMat.GetLength(1);i++)
			{Console.WriteLine(" RelMat.GetLength(1)={0} i={1} relations[i]={2}",RelMat.GetLength(1),i,relations[i]);
				//Console.WriteLine("In For : operation={0}  operationindex={1}",operationsArr[operationindex], operationindex);
				if (relations[i]!=0)
				{
					Console.Write("relations[i]={0} i={1}",relations[i],i);
					RelMat[operation-1,relations[i]-1] = 1;
					//RelMat[operationindex,i] = 1;
				}
			}			
		}
		public static void PrintValues( int[] myArr )  
		{
			foreach ( int i in myArr )  
			{
				Console.Write( " {0}", i );
			}
			Console.WriteLine();
		}
	
		#endregion
	}
}
 
