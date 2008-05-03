/*********************************************************************
 **
 **    LINDO API Version 5.0
 **    Copyright (c) 2000-2007
 **
 **    LINDO Systems, Inc.            312.988.7422
 **    1415 North Dayton St.          info@lindo.com
 **    Chicago, IL 60622              http://www.lindo.com
 **
 **    @file     lindo.h  (C/C++ header)
 **    @modified on 08-11-2007
 **
 *********************************************************************/

using System;
using System.Text;
using System.Runtime.InteropServices;

public class lindo
{

 /*     Define LSWIN64 to enable x64 compatibility 
  */
 
#if LSWIN64
  public const string   LINDO_DLL                         = "lindo64_5_0.dll";
#else  
  public const string   LINDO_DLL                         = "lindo5_0.dll";
#endif  

/*********************************************************************
 *                        Constant Definitions                       *
 *********************************************************************/

   public const int    LS_MIN                            = +1;
   public const int    LS_MAX                            = -1;

   public const double LS_INFINITY                       = 1.0E+30;

   public const int    LS_BASTYPE_BAS                    = 0;
   public const int    LS_BASTYPE_ATLO                   = -1;
   public const int    LS_BASTYPE_ATUP                   = -2;
   public const int    LS_BASTYPE_FNUL                   = -3;
   public const int    LS_BASTYPE_SBAS                   = -4;

   public const int    LS_UNFORMATTED_MPS                = 0;
   public const int    LS_FORMATTED_MPS                  = 1;
   public const int    LS_UNFORMATTED_MPS_COMP           = 2;
   public const int    LS_FORMATTED_MPS_COMP             = 3;

   public const int    LS_SOLUTION_OPT                   = 0;
   public const int    LS_SOLUTION_MIP                   = 1;
   public const int    LS_SOLUTION_OPT_IPM               = 2;
   public const int    LS_SOLUTION_OPT_OLD               = 3;
   public const int    LS_SOLUTION_MIP_OLD               = 4;

   public const int    LS_BASFILE_BIN                    = 1;
   public const int    LS_BASFILE_MPS                    = 2;
   public const int    LS_BASFILE_TXT                    = 3;

   public const int    LS_INT_PARAMETER_TYPE             = 4;
   public const int    LS_DOUBLE_PARAMETER_TYPE          = 8;

   public const int    LS_MAX_ERROR_MESSAGE_LENGTH       = 256;

/*********************************************************************
 *                      Macro Type Definitions                       *
 *********************************************************************/

/* Solution or model status (1-20) */
   public const int    LS_STATUS_OPTIMAL                 = 1;
   public const int    LS_STATUS_BASIC_OPTIMAL           = 2;
   public const int    LS_STATUS_INFEASIBLE              = 3;
   public const int    LS_STATUS_UNBOUNDED               = 4;
   public const int    LS_STATUS_FEASIBLE                = 5;
   public const int    LS_STATUS_INFORUNB                = 6;
   public const int    LS_STATUS_NEAR_OPTIMAL            = 7;
   public const int    LS_STATUS_LOCAL_OPTIMAL           = 8;
   public const int    LS_STATUS_LOCAL_INFEASIBLE        = 9;
   public const int    LS_STATUS_CUTOFF                  = 10;
   public const int    LS_STATUS_NUMERICAL_ERROR         = 11;
   public const int    LS_STATUS_UNKNOWN                 = 12;
   public const int    LS_STATUS_UNLOADED                = 13;
   public const int    LS_STATUS_LOADED                  = 14;


/* Parameter codes (21-999) */

/* General solver parameters (21-199) */
   public const int    LS_IPARAM_OBJSENSE                = 22;
   public const int    LS_DPARAM_CALLBACKFREQ            = 23;
   public const int    LS_DPARAM_OBJPRINTMUL             = 24;
   public const int    LS_IPARAM_CHECK_FOR_ERRORS        = 25;
   public const int    LS_IPARAM_ALLOW_CNTRLBREAK        = 26;
   public const int    LS_IPARAM_DECOMPOSITION_TYPE      = 27;
   public const int    LS_IPARAM_LP_SCALE                = 29;
   public const int    LS_IPARAM_LP_ITRLMT               = 30;
   public const int    LS_IPARAM_SPLEX_PPRICING          = 31;
   public const int    LS_IPARAM_SPLEX_REFACFRQ          = 32;
   public const int    LS_IPARAM_BARRIER_SOLVER          = 33;
   public const int    LS_IPARAM_PROB_TO_SOLVE           = 34;
   public const int    LS_IPARAM_LP_PRINTLEVEL           = 35;
   public const int    LS_IPARAM_MPS_OBJ_WRITESTYLE      = 36;
   public const int    LS_IPARAM_SPLEX_DPRICING          = 37;
   public const int    LS_IPARAM_SOL_REPORT_STYLE        = 38;
   public const int    LS_IPARAM_INSTRUCT_LOADTYPE       = 39;
   public const int    LS_IPARAM_SPLEX_DUAL_PHASE        = 40;
   public const int    LS_IPARAM_LP_PRELEVEL             = 41;
   public const int    LS_IPARAM_STRING_LENLMT           = 42;
   public const int    LS_IPARAM_USE_NAMEDATA            = 43;

   public const int    LS_IPARAM_SOLVER_IUSOL            = 51;
   public const int    LS_IPARAM_SOLVER_TIMLMT           = 52;
   public const int    LS_DPARAM_SOLVER_CUTOFFVAL        = 53;
   public const int    LS_DPARAM_SOLVER_FEASTOL          = 54;
   public const int    LS_IPARAM_SOLVER_RESTART          = 55;
   public const int    LS_IPARAM_SOLVER_IPMSOL           = 56;
   public const int    LS_DPARAM_SOLVER_OPTTOL           = 57;
   public const int    LS_IPARAM_SOLVER_USECUTOFFVAL     = 58;
   public const int    LS_IPARAM_SOLVER_PRE_ELIM_FILL    = 59;
   public const int    LS_DPARAM_SOLVER_TIMLMT           = 60;
   public const int    LS_IPARAM_SOLVER_USE_CONCURRENT_OPT = 61;

/* Parameters for the IPM method*/

   public const int    LS_DPARAM_IPM_TOL_INFEAS          = 150;
   public const int    LS_DPARAM_IPM_TOL_PATH            = 151;
   public const int    LS_DPARAM_IPM_TOL_PFEAS           = 152;
   public const int    LS_DPARAM_IPM_TOL_REL_STEP        = 153;
   public const int    LS_DPARAM_IPM_TOL_PSAFE           = 154;
   public const int    LS_DPARAM_IPM_TOL_DFEAS           = 155;
   public const int    LS_DPARAM_IPM_TOL_DSAFE           = 156;
   public const int    LS_DPARAM_IPM_TOL_MU_RED          = 157;
   public const int    LS_DPARAM_IPM_BASIS_REL_TOL_S     = 158;
   public const int    LS_DPARAM_IPM_BASIS_TOL_S         = 159;
   public const int    LS_DPARAM_IPM_BASIS_TOL_X         = 160;
   public const int    LS_DPARAM_IPM_BI_LU_TOL_REL_PIV   = 161;
   public const int    LS_IPARAM_IPM_MAX_ITERATIONS      = 162;
   public const int    LS_IPARAM_IPM_OFF_COL_TRH         = 163;
   public const int    LS_IPARAM_IPM_NUM_THREADS         = 164;

/* Nonlinear programming (NLP) parameters (200-299) */
   public const int    LS_IPARAM_NLP_SOLVE_AS_LP         = 200;
   public const int    LS_IPARAM_NLP_SOLVER              = 201;
   public const int    LS_IPARAM_NLP_SUBSOLVER           = 202;
   public const int    LS_IPARAM_NLP_PRINTLEVEL          = 203;
   public const int    LS_DPARAM_NLP_PSTEP_FINITEDIFF    = 204;
   public const int    LS_IPARAM_NLP_DERIV_DIFFTYPE      = 205;
   public const int    LS_DPARAM_NLP_FEASTOL             = 206;
   public const int    LS_DPARAM_NLP_REDGTOL             = 207;
   public const int    LS_IPARAM_NLP_USE_CRASH           = 208;
   public const int    LS_IPARAM_NLP_USE_STEEPEDGE       = 209;
   public const int    LS_IPARAM_NLP_USE_SLP             = 210;
   public const int    LS_IPARAM_NLP_USE_SELCONEVAL      = 211;
   public const int    LS_IPARAM_NLP_PRELEVEL            = 212;
   public const int    LS_IPARAM_NLP_ITRLMT              = 213;
   public const int    LS_IPARAM_NLP_LINEARZ             = 214;
   public const int    LS_IPARAM_NLP_LINEARITY           = 215;
   public const int    LS_IPARAM_NLP_STARTPOINT          = 216;
   public const int    LS_IPARAM_NLP_CONVEXRELAX         = 217;
   public const int    LS_IPARAM_NLP_CR_ALG_REFORM       = 218;
   public const int    LS_IPARAM_NLP_QUADCHK             = 219;
   public const int    LS_IPARAM_NLP_AUTODERIV           = 220;
   public const int    LS_IPARAM_NLP_MAXLOCALSEARCH      = 221;
   public const int    LS_IPARAM_NLP_CONVEX              = 222;
   public const int    LS_IPARAM_NLP_CONOPT_VER          = 223;
   public const int    LS_IPARAM_NLP_USE_LINDO_CRASH     = 224;
   public const int    LS_IPARAM_NLP_STALL_ITRLMT        = 225;
   public const int    LS_IPARAM_NLP_AUTOHESS            = 226;
   public const int    LS_IPARAM_NLP_FEASCHK             = 227;   

/* Mixed integer programming (MIP) parameters (300-399) */
   public const int    LS_IPARAM_MIP_TIMLIM              = 300;
   public const int    LS_IPARAM_MIP_AOPTTIMLIM          = 301;
   public const int    LS_IPARAM_MIP_LSOLTIMLIM          = 302;
   public const int    LS_IPARAM_MIP_PRELEVEL            = 303;
   public const int    LS_IPARAM_MIP_NODESELRULE         = 304;
   public const int    LS_DPARAM_MIP_INTTOL              = 305;
   public const int    LS_DPARAM_MIP_RELINTTOL           = 306;
   public const int    LS_DPARAM_MIP_RELOPTTOL           = 307;
   public const int    LS_DPARAM_MIP_PEROPTTOL           = 308;
   public const int    LS_IPARAM_MIP_MAXCUTPASS_TOP      = 309;
   public const int    LS_IPARAM_MIP_MAXCUTPASS_TREE     = 310;
   public const int    LS_DPARAM_MIP_ADDCUTPER           = 311;
   public const int    LS_DPARAM_MIP_ADDCUTPER_TREE      = 312;
   public const int    LS_IPARAM_MIP_MAXNONIMP_CUTPASS   = 313;
   public const int    LS_IPARAM_MIP_CUTLEVEL_TOP        = 314;
   public const int    LS_IPARAM_MIP_CUTLEVEL_TREE       = 315;
   public const int    LS_IPARAM_MIP_CUTTIMLIM           = 316;
   public const int    LS_IPARAM_MIP_CUTDEPTH            = 317;
   public const int    LS_IPARAM_MIP_CUTFREQ             = 318;
   public const int    LS_IPARAM_MIP_HEULEVEL            = 319;
   public const int    LS_IPARAM_MIP_PRINTLEVEL          = 320;
   public const int    LS_IPARAM_MIP_PREPRINTLEVEL       = 321;
   public const int    LS_DPARAM_MIP_CUTOFFOBJ           = 322;
   public const int    LS_IPARAM_MIP_USECUTOFFOBJ        = 323;
   public const int    LS_IPARAM_MIP_STRONGBRANCHLEVEL   = 324;
   public const int    LS_IPARAM_MIP_TREEREORDERLEVEL    = 325;
   public const int    LS_IPARAM_MIP_BRANCHDIR           = 326;
   public const int    LS_IPARAM_MIP_TOPOPT              = 327;
   public const int    LS_IPARAM_MIP_REOPT               = 328;
   public const int    LS_IPARAM_MIP_SOLVERTYPE          = 329;
   public const int    LS_IPARAM_MIP_KEEPINMEM           = 330;
   public const int    LS_IPARAM_MIP_BRANCHRULE          = 331;
   public const int    LS_DPARAM_MIP_REDCOSTFIX_CUTOFF   = 332;
   public const int    LS_DPARAM_MIP_ADDCUTOBJTOL        = 333;
   public const int    LS_IPARAM_MIP_HEUMINTIMLIM        = 334;
   public const int    LS_IPARAM_MIP_BRANCH_PRIO         = 335;
   public const int    LS_IPARAM_MIP_SCALING_BOUND       = 336;
   public const int    LS_DPARAM_MIP_PSEUDOCOST_WEIGT    = 337;
   public const int    LS_DPARAM_MIP_LBIGM               = 338;
   public const int    LS_DPARAM_MIP_DELTA               = 339;
   public const int    LS_IPARAM_MIP_DUAL_SOLUTION       = 340;
   public const int    LS_IPARAM_MIP_BRANCH_LIMIT        = 341;
   public const int    LS_IPARAM_MIP_ITRLIM              = 342;
   public const int    LS_IPARAM_MIP_AGGCUTLIM_TOP       = 343;
   public const int    LS_IPARAM_MIP_AGGCUTLIM_TREE      = 344;
   public const int    LS_IPARAM_MIP_SWITCHFAC_SIM_IPM   = 345;
   public const int    LS_IPARAM_MIP_ANODES_SWITCH_DF    = 346;
   public const int    LS_DPARAM_MIP_ABSOPTTOL           = 347;
   public const int    LS_DPARAM_MIP_MINABSOBJSTEP       = 348;
   public const int    LS_IPARAM_MIP_PSEUDOCOST_RULE     = 349;
   public const int    LS_IPARAM_MIP_USE_ENUM_HEU        = 350;
   public const int    LS_IPARAM_MIP_PRELEVEL_TREE       = 351;
   public const int    LS_DPARAM_MIP_REDCOSTFIX_CUTOFF_TREE = 352;
   public const int    LS_IPARAM_MIP_USE_INT_ZERO_TOL    = 353;
   public const int    LS_IPARAM_MIP_USE_CUTS_HEU        = 354;
   public const int    LS_DPARAM_MIP_BIGM_FOR_INTTOL     = 355;
   public const int    LS_IPARAM_MIP_STRONGBRANCHDONUM   = 366;
   public const int    LS_IPARAM_MIP_MAKECUT_INACTIVE_COUNT = 367;
   public const int    LS_IPARAM_MIP_PRE_ELIM_FILL       = 368;
   public const int    LS_IPARAM_MIP_HEU_MODE            = 369;
   public const int    LS_DPARAM_MIP_TIMLIM              = 370;
   public const int    LS_DPARAM_MIP_AOPTTIMLIM          = 371;
   public const int    LS_DPARAM_MIP_LSOLTIMLIM          = 372;
   public const int    LS_DPARAM_MIP_CUTTIMLIM           = 373;
   public const int    LS_DPARAM_MIP_HEUMINTIMLIM        = 374;
   public const int    LS_IPARAM_MIP_FP_MODE             = 375;
   public const int    LS_DPARAM_MIP_FP_WEIGTH           = 376;
   public const int    LS_IPARAM_MIP_FP_OPT_METHOD       = 377;
   public const int    LS_DPARAM_MIP_FP_TIMLIM           = 378;
   public const int    LS_IPARAM_MIP_FP_ITRLIM           = 379;
   public const int    LS_DPARAM_MIP_OBJ_THRESHOLD       = 380;
   public const int    LS_IPARAM_MIP_LOCALBRANCHNUM      = 381;

/* Global optimization (GOP) parameters (400-499) */
   public const int    LS_DPARAM_GOP_OPTTOL              = 400;
   public const int    LS_DPARAM_GOP_FLTTOL              = 401;
   public const int    LS_DPARAM_GOP_BOXTOL              = 402;
   public const int    LS_DPARAM_GOP_WIDTOL              = 403;
   public const int    LS_DPARAM_GOP_DELTATOL            = 404;
   public const int    LS_DPARAM_GOP_BNDLIM              = 405;
   public const int    LS_IPARAM_GOP_TIMLIM              = 406;
   public const int    LS_IPARAM_GOP_OPTCHKMD            = 407;
   public const int    LS_IPARAM_GOP_BRANCHMD            = 408;
   public const int    LS_IPARAM_GOP_MAXWIDMD            = 409;
   public const int    LS_IPARAM_GOP_PRELEVEL            = 410;
   public const int    LS_IPARAM_GOP_POSTLEVEL           = 411;
   public const int    LS_IPARAM_GOP_BBSRCHMD            = 412;
   public const int    LS_IPARAM_GOP_DECOMPPTMD          = 413;
   public const int    LS_IPARAM_GOP_ALGREFORMMD         = 414;
   public const int    LS_IPARAM_GOP_RELBRNDMD           = 415;
   public const int    LS_IPARAM_GOP_PRINTLEVEL          = 416;
   public const int    LS_IPARAM_GOP_USEBNDLIM           = 417;
   public const int    LS_IPARAM_GOP_BRANCH_LIMIT        = 418;
   public const int    LS_IPARAM_GOP_CORELEVEL           = 419;
   public const int    LS_IPARAM_GOP_OPT_MODE            = 420;
   public const int    LS_IPARAM_GOP_HEU_MODE            = 421;
   public const int    LS_IPARAM_GOP_SUBOUT_MODE         = 422;
   public const int    LS_IPARAM_GOP_USE_NLPSOLVE        = 423;
   public const int    LS_IPARAM_GOP_LSOLBRANLIM         = 424;
   public const int    LS_IPARAM_GOP_LPSOPT              = 425;

/* License information parameters (500-549) */
   public const int    LS_IPARAM_LIC_CONSTRAINTS         = 500;
   public const int    LS_IPARAM_LIC_VARIABLES           = 501;
   public const int    LS_IPARAM_LIC_INTEGERS            = 502;
   public const int    LS_IPARAM_LIC_NONLINEARVARS       = 503;
   public const int    LS_IPARAM_LIC_GOP_INTEGERS        = 504;
   public const int    LS_IPARAM_LIC_GOP_NONLINEARVARS   = 505;
   public const int    LS_IPARAM_LIC_DAYSTOEXP           = 506;
   public const int    LS_IPARAM_LIC_DAYSTOTRIALEXP      = 507;
   public const int    LS_IPARAM_LIC_NONLINEAR           = 508;
   public const int    LS_IPARAM_LIC_EDUCATIONAL         = 509;
   public const int    LS_IPARAM_LIC_RUNTIME             = 510;
   public const int    LS_IPARAM_LIC_NUMUSERS            = 511;
   public const int    LS_IPARAM_LIC_BARRIER             = 512;
   public const int    LS_IPARAM_LIC_GLOBAL              = 513;
   public const int    LS_IPARAM_LIC_PLATFORM            = 514;
   public const int    LS_IPARAM_LIC_MIP                 = 515;

/* Model analysis parameters (550-600) */
   public const int    LS_IPARAM_IIS_ANALYZE_LEVEL       = 550;
   public const int    LS_IPARAM_IUS_ANALYZE_LEVEL       = 551;
   public const int    LS_IPARAM_IIS_TOPOPT              = 552;
   public const int    LS_IPARAM_IIS_REOPT               = 553;
   public const int    LS_IPARAM_IIS_USE_SFILTER         = 554;
   public const int    LS_IPARAM_IIS_PRINT_LEVEL         = 555;
   public const int    LS_IPARAM_IIS_INFEAS_NORM         = 556;
   public const int    LS_IPARAM_IIS_ITER_LIMIT          = 557;
   public const int    LS_IPARAM_IIS_TIME_LIMIT          = 558;
   public const int    LS_IPARAM_IIS_METHOD              = 559;
   public const int    LS_IPARAM_IIS_USE_EFILTER         = 560;
   public const int    LS_IPARAM_IIS_USE_GOP             = 561;

/* Version info */
   public const int    LS_IPARAM_VER_MAJOR               = 990;
   public const int    LS_IPARAM_VER_MINOR               = 991;
   public const int    LS_IPARAM_VER_BUILD               = 992;
   public const int    LS_IPARAM_VER_REVISION            = 993;

/* Last card for parameters */
   public const int    LS_IPARAM_VER_NUMBER              = 999;


/* Math operator codes (1000-1500) */
   public const int    EP_NO_OP                          = 0000;
   public const int    EP_PLUS                           = 1001;
   public const int    EP_MINUS                          = 1002;
   public const int    EP_MULTIPLY                       = 1003;
   public const int    EP_DIVIDE                         = 1004;
   public const int    EP_POWER                          = 1005;
   public const int    EP_EQUAL                          = 1006;
   public const int    EP_NOT_EQUAL                      = 1007;
   public const int    EP_LTOREQ                         = 1008;
   public const int    EP_GTOREQ                         = 1009;
   public const int    EP_LTHAN                          = 1010;
   public const int    EP_GTHAN                          = 1011;
   public const int    EP_AND                            = 1012;
   public const int    EP_OR                             = 1013;
   public const int    EP_NOT                            = 1014;
   public const int    EP_PERCENT                        = 1015;
   public const int    EP_POSATE                         = 1016;
   public const int    EP_NEGATE                         = 1017;
   public const int    EP_ABS                            = 1018;
   public const int    EP_SQRT                           = 1019;
   public const int    EP_LOG                            = 1020;
   public const int    EP_LN                             = 1021;
   public const int    EP_PI                             = 1022;
   public const int    EP_SIN                            = 1023;
   public const int    EP_COS                            = 1024;
   public const int    EP_TAN                            = 1025;
   public const int    EP_ATAN2                          = 1026;
   public const int    EP_ATAN                           = 1027;
   public const int    EP_ASIN                           = 1028;
   public const int    EP_ACOS                           = 1029;
   public const int    EP_EXP                            = 1030;
   public const int    EP_MOD                            = 1031;
   public const int    EP_FALSE                          = 1032;
   public const int    EP_TRUE                           = 1033;
   public const int    EP_IF                             = 1034;
   public const int    EP_PSN                            = 1035;
   public const int    EP_PSL                            = 1036;
   public const int    EP_LGM                            = 1037;
   public const int    EP_SIGN                           = 1038;
   public const int    EP_FLOOR                          = 1039;
   public const int    EP_FPA                            = 1040;
   public const int    EP_FPL                            = 1041;
   public const int    EP_PEL                            = 1042;
   public const int    EP_PEB                            = 1043;
   public const int    EP_PPS                            = 1044;
   public const int    EP_PPL                            = 1045;
   public const int    EP_PTD                            = 1046;
   public const int    EP_PCX                            = 1047;
   public const int    EP_WRAP                           = 1048;
   public const int    EP_PBN                            = 1049;
   public const int    EP_PFS                            = 1050;
   public const int    EP_PFD                            = 1051;
   public const int    EP_PHG                            = 1052;
   public const int    EP_RAND                           = 1053;
   public const int    EP_USER                           = 1054;
   public const int    EP_SUM                            = 1055;
   public const int    EP_AVG                            = 1056;
   public const int    EP_MIN                            = 1057;
   public const int    EP_MAX                            = 1058;
   public const int    EP_NPV                            = 1059;
   public const int    EP_VAND                           = 1060;
   public const int    EP_VOR                            = 1061;
   public const int    EP_PUSH_NUM                       = 1062;
   public const int    EP_PUSH_VAR                       = 1063;
   public const int    EP_NORMDENS                       = 1064;
   public const int    EP_NORMINV                        = 1065;
   public const int    EP_TRIAINV                        = 1066;
   public const int    EP_EXPOINV                        = 1067;
   public const int    EP_UNIFINV                        = 1068;
   public const int    EP_MULTINV                        = 1069;
   public const int    EP_USRCOD                         = 1070;
   public const int    EP_SUMPROD                        = 1071;
   public const int    EP_SUMIF                          = 1072;
   public const int    EP_VLOOKUP                        = 1073;
   public const int    EP_VPUSH_NUM                      = 1074;
   public const int    EP_VPUSH_VAR                      = 1075;
   public const int    EP_VMULT                          = 1076;
   public const int    EP_SQR                            = 1077;
   public const int    EP_SINH                           = 1078;
   public const int    EP_COSH                           = 1079;
   public const int    EP_TANH                           = 1080;
   public const int    EP_ASINH                          = 1081;
   public const int    EP_ACOSH                          = 1082;
   public const int    EP_ATANH                          = 1083;
   public const int    EP_LOGB                           = 1084;
   public const int    EP_LOGX                           = 1085;
   public const int    EP_LNX                            = 1086;
   public const int    EP_TRUNC                          = 1087;
   public const int    EP_NORMSINV                       = 1088;
   public const int    EP_INT                            = 1089;
   public const int    EP_PUSH_STR                       = 1090;
   public const int    EP_VPUSH_STR                      = 1091;
   public const int    EP_PUSH_SVAR                      = 1092;


/* Model and solution information codes ( 1200-1399) */
/* Model statistics (1200-1249)*/
   public const int    LS_IINFO_NUM_QCP_CONS             = 1195;
   public const int    LS_IINFO_NUM_CONT_CONS            = 1196;
   public const int    LS_IINFO_NUM_INT_CONS             = 1197;
   public const int    LS_IINFO_NUM_BIN_CONS             = 1198;
   public const int    LS_IINFO_NUM_QCP_VARS             = 1199;
   public const int    LS_IINFO_NUM_CONS                 = 1200;
   public const int    LS_IINFO_NUM_VARS                 = 1201;
   public const int    LS_IINFO_NUM_NONZ                 = 1202;
   public const int    LS_IINFO_NUM_BIN                  = 1203;
   public const int    LS_IINFO_NUM_INT                  = 1204;
   public const int    LS_IINFO_NUM_CONT                 = 1205;
   public const int    LS_IINFO_NUM_QC_NONZ              = 1206;
   public const int    LS_IINFO_NUM_NLP_NONZ             = 1207;
   public const int    LS_IINFO_NUM_NLPOBJ_NONZ          = 1208;
   public const int    LS_IINFO_NUM_RDCONS               = 1209;
   public const int    LS_IINFO_NUM_RDVARS               = 1210;
   public const int    LS_IINFO_NUM_RDNONZ               = 1211;
   public const int    LS_IINFO_NUM_RDINT                = 1212;
   public const int    LS_IINFO_LEN_VARNAMES             = 1213;
   public const int    LS_IINFO_LEN_CONNAMES             = 1214;
   public const int    LS_IINFO_NUM_NLP_CONS             = 1215;
   public const int    LS_IINFO_NUM_NLP_VARS             = 1216;
   public const int    LS_IINFO_NUM_SUF_ROWS             = 1217;
   public const int    LS_IINFO_NUM_IIS_ROWS             = 1218;
   public const int    LS_IINFO_NUM_SUF_BNDS             = 1219;
   public const int    LS_IINFO_NUM_IIS_BNDS             = 1220;
   public const int    LS_IINFO_NUM_SUF_COLS             = 1221;
   public const int    LS_IINFO_NUM_IUS_COLS             = 1222;
   public const int    LS_IINFO_NUM_CONES                = 1223;
   public const int    LS_IINFO_NUM_CONE_NONZ            = 1224;
   public const int    LS_IINFO_LEN_CONENAMES            = 1225;
   public const int    LS_DINFO_INST_VAL_MIN_COEF        = 1226;
   public const int    LS_IINFO_INST_VARNDX_MIN_COEF     = 1227;
   public const int    LS_IINFO_INST_CONNDX_MIN_COEF     = 1228;
   public const int    LS_DINFO_INST_VAL_MAX_COEF        = 1229;
   public const int    LS_IINFO_INST_VARNDX_MAX_COEF     = 1230;
   public const int    LS_IINFO_INST_CONNDX_MAX_COEF     = 1231;
   public const int    LS_IINFO_NUM_VARS_CARD            = 1232;
   public const int    LS_IINFO_NUM_VARS_SOS1            = 1233;
   public const int    LS_IINFO_NUM_VARS_SOS2            = 1234;
   public const int    LS_IINFO_NUM_VARS_SOS3            = 1235;
   public const int    LS_IINFO_NUM_VARS_SCONT           = 1236;
   public const int    LS_IINFO_NUM_CONS_L               = 1237;
   public const int    LS_IINFO_NUM_CONS_E               = 1238;
   public const int    LS_IINFO_NUM_CONS_G               = 1239;
   public const int    LS_IINFO_NUM_CONS_R               = 1240;
   public const int    LS_IINFO_NUM_CONS_N               = 1241;
   public const int    LS_IINFO_NUM_VARS_LB              = 1242;
   public const int    LS_IINFO_NUM_VARS_UB              = 1243;
   public const int    LS_IINFO_NUM_VARS_LUB             = 1244;
   public const int    LS_IINFO_NUM_VARS_FR              = 1245;
   public const int    LS_IINFO_NUM_VARS_FX              = 1246;
   public const int    LS_IINFO_NUM_INST_CODES           = 1247;
   public const int    LS_IINFO_NUM_INST_REAL_NUM        = 1248;
   public const int    LS_IINFO_NUM_NUM_SVAR             = 1249;

/* LP and NLP related info (1250-1299)*/
   public const int    LS_IINFO_METHOD                   = 1250;
   public const int    LS_DINFO_POBJ                     = 1251;
   public const int    LS_DINFO_DOBJ                     = 1252;
   public const int    LS_DINFO_PINFEAS                  = 1253;
   public const int    LS_DINFO_DINFEAS                  = 1254;
   public const int    LS_IINFO_MODEL_STATUS             = 1255;
   public const int    LS_IINFO_PRIMAL_STATUS            = 1256;
   public const int    LS_IINFO_DUAL_STATUS              = 1257;
   public const int    LS_IINFO_BASIC_STATUS             = 1258;
   public const int    LS_IINFO_BAR_ITER                 = 1259;
   public const int    LS_IINFO_SIM_ITER                 = 1260;
   public const int    LS_IINFO_NLP_ITER                 = 1261;
   public const int    LS_IINFO_ELAPSED_TIME             = 1262;
   public const int    LS_DINFO_MSW_POBJ                 = 1263;
   public const int    LS_IINFO_MSW_PASS                 = 1264;
   public const int    LS_IINFO_MSW_NSOL                 = 1265;
   public const int    LS_IINFO_IPM_STATUS               = 1266;
   public const int    LS_DINFO_IPM_POBJ                 = 1267;
   public const int    LS_DINFO_IPM_DOBJ                 = 1268;
   public const int    LS_DINFO_IPM_PINFEAS              = 1269;
   public const int    LS_DINFO_IPM_DINFEAS              = 1270;
   public const int    LS_IINFO_NLP_CALL_FUN             = 1271;
   public const int    LS_IINFO_NLP_CALL_DEV             = 1272;
   public const int    LS_IINFO_NLP_CALL_HES             = 1273;
   public const int    LS_IINFO_CONCURRENT_OPTIMIZER     = 1274;
   

/* MIP and MINLP related info (1300-1349) */
   public const int    LS_IINFO_MIP_STATUS               = 1300;
   public const int    LS_DINFO_MIP_OBJ                  = 1301;
   public const int    LS_DINFO_MIP_BESTBOUND            = 1302;
   public const int    LS_IINFO_MIP_SIM_ITER             = 1303;
   public const int    LS_IINFO_MIP_BAR_ITER             = 1304;
   public const int    LS_IINFO_MIP_NLP_ITER             = 1305;
   public const int    LS_IINFO_MIP_BRANCHCOUNT          = 1306;
   public const int    LS_IINFO_MIP_NEWIPSOL             = 1307;
   public const int    LS_IINFO_MIP_LPCOUNT              = 1308;
   public const int    LS_IINFO_MIP_ACTIVENODES          = 1309;
   public const int    LS_IINFO_MIP_LTYPE                = 1310;
   public const int    LS_IINFO_MIP_AOPTTIMETOSTOP       = 1311;
   public const int    LS_IINFO_MIP_NUM_TOTAL_CUTS       = 1312;
   public const int    LS_IINFO_MIP_GUB_COVER_CUTS       = 1313;
   public const int    LS_IINFO_MIP_FLOW_COVER_CUTS      = 1314;
   public const int    LS_IINFO_MIP_LIFT_CUTS            = 1315;
   public const int    LS_IINFO_MIP_PLAN_LOC_CUTS        = 1316;
   public const int    LS_IINFO_MIP_DISAGG_CUTS          = 1317;
   public const int    LS_IINFO_MIP_KNAPSUR_COVER_CUTS   = 1318;
   public const int    LS_IINFO_MIP_LATTICE_CUTS         = 1319;
   public const int    LS_IINFO_MIP_GOMORY_CUTS          = 1320;
   public const int    LS_IINFO_MIP_COEF_REDC_CUTS       = 1321;
   public const int    LS_IINFO_MIP_GCD_CUTS             = 1322;
   public const int    LS_IINFO_MIP_OBJ_CUT              = 1323;
   public const int    LS_IINFO_MIP_BASIS_CUTS           = 1324;
   public const int    LS_IINFO_MIP_CARDGUB_CUTS         = 1325;
   public const int    LS_IINFO_MIP_CLIQUE_CUTS          = 1326;
   public const int    LS_IINFO_MIP_CONTRA_CUTS          = 1327;
   public const int    LS_IINFO_MIP_GUB_CONS             = 1328;
   public const int    LS_IINFO_MIP_GLB_CONS             = 1329;
   public const int    LS_IINFO_MIP_PLANTLOC_CONS        = 1330;
   public const int    LS_IINFO_MIP_DISAGG_CONS          = 1331;
   public const int    LS_IINFO_MIP_SB_CONS              = 1332;
   public const int    LS_IINFO_MIP_IKNAP_CONS           = 1333;
   public const int    LS_IINFO_MIP_KNAP_CONS            = 1334;
   public const int    LS_IINFO_MIP_NLP_CONS             = 1335;
   public const int    LS_IINFO_MIP_CONT_CONS            = 1336;
   public const int    LS_DINFO_MIP_TOT_TIME             = 1347;
   public const int    LS_DINFO_MIP_OPT_TIME             = 1348;
   public const int    LS_DINFO_MIP_HEU_TIME             = 1349;
   public const int    LS_IINFO_MIP_SOLSTATUS_LAST_BRANCH = 1350;
   public const int    LS_DINFO_MIP_SOLOBJVAL_LAST_BRANCH = 1351;
   public const int    LS_IINFO_MIP_HEU_LEVEL            = 1352;
   public const int    LS_DINFO_MIP_PFEAS                = 1353;
   public const int    LS_DINFO_MIP_INTPFEAS             = 1354;
   public const int    LS_IINFO_MIP_WHERE_IN_CODE        = 1355;
   public const int    LS_IINFO_MIP_FP_ITER              = 1356;
   public const int    LS_DINFO_MIP_FP_SUMFEAS           = 1357;
   public const int    LS_DINFO_MIP_RELMIPGAP            = 1358;


/* GOP related info (1650-1799) */
   public const int    LS_DINFO_GOP_OBJ                  = 1650;
   public const int    LS_IINFO_GOP_SIM_ITER             = 1651;
   public const int    LS_IINFO_GOP_BAR_ITER             = 1652;
   public const int    LS_IINFO_GOP_NLP_ITER             = 1653;
   public const int    LS_DINFO_GOP_BESTBOUND            = 1654;
   public const int    LS_IINFO_GOP_STATUS               = 1655;
   public const int    LS_IINFO_GOP_LPCOUNT              = 1656;
   public const int    LS_IINFO_GOP_NLPCOUNT             = 1657;
   public const int    LS_IINFO_GOP_MIPCOUNT             = 1658;
   public const int    LS_IINFO_GOP_NEWSOL               = 1659;
   public const int    LS_IINFO_GOP_BOX                  = 1660;
   public const int    LS_IINFO_GOP_BBITER               = 1661;
   public const int    LS_IINFO_GOP_SUBITER              = 1662;
   public const int    LS_IINFO_GOP_MIPBRANCH            = 1663;
   public const int    LS_IINFO_GOP_ACTIVEBOXES          = 1664;
   public const int    LS_IINFO_GOP_TOT_TIME             = 1665;
   public const int    LS_IINFO_GOP_MAXDEPTH             = 1666;
   public const int    LS_DINFO_GOP_PFEAS                = 1667;
   public const int    LS_DINFO_GOP_INTPFEAS             = 1668;

/* Progress info during callbacks */
   public const int    LS_DINFO_SUB_OBJ                  = 1700;
   public const int    LS_DINFO_SUB_PINF                 = 1701;
   public const int    LS_DINFO_CUR_OBJ                  = 1702;
   public const int    LS_IINFO_CUR_ITER                 = 1703;
   public const int    LS_DINFO_CUR_BEST_BOUND           = 1704;
   public const int    LS_IINFO_CUR_STATUS               = 1705;
   public const int    LS_IINFO_CUR_LP_COUNT             = 1706;
   public const int    LS_IINFO_CUR_BRANCH_COUNT         = 1707;
   public const int    LS_IINFO_CUR_ACTIVE_COUNT         = 1708;
   public const int    LS_IINFO_CUR_NLP_COUNT            = 1709;
   public const int    LS_IINFO_CUR_MIP_COUNT            = 1710;
   public const int    LS_IINFO_CUR_CUT_COUNT            = 1711;

/* Model generation progress info (1800+)*/
   public const int    LS_DINFO_GEN_PERCENT              = 1800;
   public const int    LS_IINFO_GEN_NONZ_TTL             = 1801;
   public const int    LS_IINFO_GEN_NONZ_NL              = 1802;
   public const int    LS_IINFO_GEN_ROW_NL               = 1803;
   public const int    LS_IINFO_GEN_VAR_NL               = 1804;

/* IIS-IUS info */
   public const int    LS_IINFO_IIS_BAR_ITER             = 1850;
   public const int    LS_IINFO_IIS_SIM_ITER             = 1851;
   public const int    LS_IINFO_IIS_NLP_ITER             = 1852;
   public const int    LS_IINFO_IIS_TOT_TIME             = 1853;
   public const int    LS_IINFO_IIS_ACT_NODE             = 1854;
   public const int    LS_IINFO_IIS_LPCOUNT              = 1855;
   public const int    LS_IINFO_IIS_NLPCOUNT             = 1856;
   public const int    LS_IINFO_IIS_MIPCOUNT             = 1857;

   public const int    LS_IINFO_IUS_BAR_ITER             = 1860;
   public const int    LS_IINFO_IUS_SIM_ITER             = 1861;
   public const int    LS_IINFO_IUS_NLP_ITER             = 1862;
   public const int    LS_IINFO_IUS_TOT_TIME             = 1863;
   public const int    LS_IINFO_IUS_ACT_NODE             = 1864;
   public const int    LS_IINFO_IUS_LPCOUNT              = 1865;
   public const int    LS_IINFO_IUS_NLPCOUNT             = 1866;
   public const int    LS_IINFO_IUS_MIPCOUNT             = 1867;

/* Presolve info    */
   public const int    LS_IINFO_PRE_NUM_RED              = 1900;
   public const int    LS_IINFO_PRE_TYPE_RED             = 1901;
   public const int    LS_IINFO_PRE_NUM_RDCONS           = 1902;
   public const int    LS_IINFO_PRE_NUM_RDVARS           = 1903;
   public const int    LS_IINFO_PRE_NUM_RDNONZ           = 1904;
   public const int    LS_IINFO_PRE_NUM_RDINT            = 1905;

/* Error info */
   public const int    LS_IINFO_ERR_OPTIM                = 1999;

/* Misc info */
   public const int    LS_SINFO_MODEL_FILENAME           = 2000;
   public const int    LS_SINFO_MODEL_SOURCE             = 2001;
   public const int    LS_IINFO_MODEL_TYPE               = 2002;
   public const int    LS_SINFO_CORE_FILENAME            = 2003;
   public const int    LS_SINFO_STOC_FILENAME            = 2004;
   public const int    LS_SINFO_TIME_FILENAME            = 2005;


/* Error codes (2001-2299) */
   public const int    LSERR_NO_ERROR                    = 0000;
   public const int    LSERR_OUT_OF_MEMORY               = 2001;
   public const int    LSERR_CANNOT_OPEN_FILE            = 2002;
   public const int    LSERR_BAD_MPS_FILE                = 2003;
   public const int    LSERR_BAD_CONSTRAINT_TYPE         = 2004;
   public const int    LSERR_BAD_MODEL                   = 2005;
   public const int    LSERR_BAD_SOLVER_TYPE             = 2006;
   public const int    LSERR_BAD_OBJECTIVE_SENSE         = 2007;
   public const int    LSERR_BAD_MPI_FILE                = 2008;
   public const int    LSERR_INFO_NOT_AVAILABLE          = 2009;
   public const int    LSERR_ILLEGAL_NULL_POINTER        = 2010;
   public const int    LSERR_UNABLE_TO_SET_PARAM         = 2011;
   public const int    LSERR_INDEX_OUT_OF_RANGE          = 2012;
   public const int    LSERR_ERRMSG_FILE_NOT_FOUND       = 2013;
   public const int    LSERR_VARIABLE_NOT_FOUND          = 2014;
   public const int    LSERR_INTERNAL_ERROR              = 2015;
   public const int    LSERR_ITER_LIMIT                  = 2016;
   public const int    LSERR_TIME_LIMIT                  = 2017;
   public const int    LSERR_NOT_CONVEX                  = 2018;
   public const int    LSERR_NUMERIC_INSTABILITY         = 2019;
   public const int    LSERR_STEP_TOO_SMALL              = 2021;
   public const int    LSERR_USER_INTERRUPT              = 2023;
   public const int    LSERR_PARAMETER_OUT_OF_RANGE      = 2024;
   public const int    LSERR_ERROR_IN_INPUT              = 2025;
   public const int    LSERR_TOO_SMALL_LICENSE           = 2026;
   public const int    LSERR_NO_VALID_LICENSE            = 2027;
   public const int    LSERR_NO_METHOD_LICENSE           = 2028;
   public const int    LSERR_NOT_SUPPORTED               = 2029;
   public const int    LSERR_MODEL_ALREADY_LOADED        = 2030;
   public const int    LSERR_MODEL_NOT_LOADED            = 2031;
   public const int    LSERR_INDEX_DUPLICATE             = 2032;
   public const int    LSERR_INSTRUCT_NOT_LOADED         = 2033;
   public const int    LSERR_OLD_LICENSE                 = 2034;
   public const int    LSERR_NO_LICENSE_FILE             = 2035;
   public const int    LSERR_BAD_LICENSE_FILE            = 2036;
   public const int    LSERR_MIP_BRANCH_LIMIT            = 2037;
   public const int    LSERR_GOP_FUNC_NOT_SUPPORTED      = 2038;
   public const int    LSERR_GOP_BRANCH_LIMIT            = 2039;
   public const int    LSERR_BAD_DECOMPOSITION_TYPE      = 2040;
   public const int    LSERR_BAD_VARIABLE_TYPE           = 2041;
   public const int    LSERR_BASIS_BOUND_MISMATCH        = 2042;
   public const int    LSERR_BASIS_COL_STATUS            = 2043;
   public const int    LSERR_BASIS_INVALID               = 2044;
   public const int    LSERR_BASIS_ROW_STATUS            = 2045;
   public const int    LSERR_BLOCK_OF_BLOCK              = 2046;
   public const int    LSERR_BOUND_OUT_OF_RANGE          = 2047;
   public const int    LSERR_COL_BEGIN_INDEX             = 2048;
   public const int    LSERR_COL_INDEX_OUT_OF_RANGE      = 2049;
   public const int    LSERR_COL_NONZCOUNT               = 2050;
   public const int    LSERR_INVALID_ERRORCODE           = 2051;
   public const int    LSERR_ROW_INDEX_OUT_OF_RANGE      = 2052;
   public const int    LSERR_TOTAL_NONZCOUNT             = 2053;
   public const int    LSERR_MODEL_NOT_LINEAR            = 2054;
   public const int    LSERR_CHECKSUM                    = 2055;
   public const int    LSERR_USER_FUNCTION_NOT_FOUND     = 2056;
   public const int    LSERR_TRUNCATED_NAME_DATA         = 2057;
   public const int    LSERR_ILLEGAL_STRING_OPERATION    = 2058;
   public const int    LSERR_STRING_ALREADY_LOADED       = 2059;
   public const int    LSERR_STRING_NOT_LOADED           = 2060;
   public const int    LSERR_STRING_LENGTH_LIMIT         = 2061;
   public const int    LSERR_DATA_TERM_EXIST             = 2062;
   public const int    LSERR_NOT_SORTED_ORDER            = 2063;
   public const int    LSERR_INST_MISS_ELEMENTS          = 2064;
   public const int    LSERR_INST_TOO_SHORT              = 2065;
   public const int    LSERR_INST_INVALID_BOUND          = 2066;
   public const int    LSERR_INST_SYNTAX_ERROR           = 2067;
   public const int    LSERR_COL_TOKEN_NOT_FOUND         = 2068;
   public const int    LSERR_ROW_TOKEN_NOT_FOUND         = 2069;
   public const int    LSERR_NAME_TOKEN_NOT_FOUND        = 2070;
   public const int    LSERR_LAST_ERROR                  = 2071;


/* Callback locations */
   public const int    LSLOC_PRIMAL                      = 0;
   public const int    LSLOC_DUAL                        = 1;
   public const int    LSLOC_BARRIER                     = 2;
   public const int    LSLOC_CROSSOVER                   = 3;
   public const int    LSLOC_CONOPT                      = 4;
   public const int    LSLOC_MIP                         = 5;
   public const int    LSLOC_LOCAL_OPT                   = 6;
   public const int    LSLOC_GEN_START                   = 7;
   public const int    LSLOC_GEN_PROCESSING              = 8;
   public const int    LSLOC_GEN_END                     = 9;
   public const int    LSLOC_GOP                         = 10;
   public const int    LSLOC_EXIT_SOLVER                 = 11;
   public const int    LSLOC_PRESOLVE                    = 12;
   public const int    LSLOC_MSW                         = 13;
   public const int    LSLOC_FUNC_CALC                   = 14;
   public const int    LSLOC_IISIUS                      = 15;


   public const int    LS_METHOD_FREE                    = 0;
   public const int    LS_METHOD_PSIMPLEX                = 1;
   public const int    LS_METHOD_DSIMPLEX                = 2;
   public const int    LS_METHOD_BARRIER                 = 3;
   public const int    LS_METHOD_NLP                     = 4;
   public const int    LS_METHOD_MIP                     = 5;
   public const int    LS_METHOD_MULTIS                  = 6;
   public const int    LS_METHOD_GOP                     = 7;
   public const int    LS_METHOD_IIS                     = 8;
   public const int    LS_METHOD_IUS                     = 9;


   public const int    LS_NMETHOD_FREE                   = 4;
   public const int    LS_NMETHOD_CONOPT                 = 7;
   public const int    LS_NMETHOD_MSW_GRG                = 9;


   public const int    LS_PROB_SOLVE_FREE                = 0;
   public const int    LS_PROB_SOLVE_PRIMAL              = 1;
   public const int    LS_PROB_SOLVE_DUAL                = 2;
   public const int    LS_BAR_METHOD_FREE                = 4;
   public const int    LS_BAR_METHOD_INTPNT              = 5;
   public const int    LS_BAR_METHOD_CONIC               = 6;
   public const int    LS_BAR_METHOD_QCONE               = 7;

   public const int    LSSOL_BASIC_PRIMAL                = 11;
   public const int    LSSOL_BASIC_DUAL                  = 12;
   public const int    LSSOL_BASIC_SLACK                 = 13;
   public const int    LSSOL_BASIC_REDCOST               = 14;
   public const int    LSSOL_INTERIOR_PRIMAL             = 15;
   public const int    LSSOL_INTERIOR_DUAL               = 16;
   public const int    LSSOL_INTERIOR_SLACK              = 17;
   public const int    LSSOL_INTERIOR_REDCOST            = 18;


/* Model types */
   /* linear programs                          */
   public const int    LS_LP                             = 10;

   /* quadratic programs                       */
   public const int    LS_QP                             = 11;

   /* second-order-cone programs               */
   public const int    LS_SOCP                           = 12;

   /* semidefinite programs                    */
   public const int    LS_SDP                            = 13;

   /* nonlinear programs                       */
   public const int    LS_NLP                            = 14;

   /* mixed-integer linear programs            */
   public const int    LS_MILP                           = 15;

   /* mixed-integer quadratic programs         */
   public const int    LS_MIQP                           = 16;

   /* mixed-integer second-order-cone programs */
   public const int    LS_MISOCP                         = 17;

   /* mixed-integer semidefinite programs      */
   public const int    LS_MISDP                          = 18;

   /* mixed-integer nonlinear programs         */
   public const int    LS_MINLP                          = 19;


   public const int    LS_LINK_BLOCKS_FREE               = 0;
   public const int    LS_LINK_BLOCKS_SELF               = 1;
   public const int    LS_LINK_BLOCKS_NONE               = 2;
   public const int    LS_LINK_BLOCKS_COLS               = 3;
   public const int    LS_LINK_BLOCKS_ROWS               = 4;
   public const int    LS_LINK_BLOCKS_BOTH               = 5;


/* Controls the way objective function and
 * objective sense are printed when writing
 * LS_MAX type problems in MPS format.
 */
   public const int    LS_MPS_USE_MAX_NOTE               = 0;
   public const int    LS_MPS_USE_MAX_CARD               = 1;
   public const int    LS_MPS_USE_MAX_FLIP               = 2;


/* Finite differences methods */
   public const int    LS_DERIV_FREE                     = 0;
   public const int    LS_DERIV_FORWARD_DIFFERENCE       = 1;
   public const int    LS_DERIV_BACKWARD_DIFFERENCE      = 2;
   public const int    LS_DERIV_CENTER_DIFFERENCE        = 3;


/* MIP Sets
 *  SOS1: S={x_1,...,x_p}  only one x_j can be different from zero
 *  SOS2: S={x_1,...,x_p}  at most two x_j can be different from zero
 *                         and  when they are they have to be adjacent
 *  SOS3: S={x_1,...,x_p}  @sum(j: x_j      )  = 1;  x_j >=0,
 *  CARD: S={x_1,...,x_p}  @sum(j: sign(x_j)) <= k;  x_j >=0
 */
   public const int    LS_MIP_SET_CARD                   = 4;
   public const int    LS_MIP_SET_SOS1                   = 1;
   public const int    LS_MIP_SET_SOS2                   = 2;
   public const int    LS_MIP_SET_SOS3                   = 3;


/* Bit mask for cut generation levels. Use sums to
 * enable a collection of available cuts.
 */
   public const int    LS_MIP_GUB_COVER_CUTS             = 2;
   public const int    LS_MIP_FLOW_COVER_CUTS            = 4;
   public const int    LS_MIP_LIFT_CUTS                  = 8;
   public const int    LS_MIP_PLAN_LOC_CUTS              = 16;
   public const int    LS_MIP_DISAGG_CUTS                = 32;
   public const int    LS_MIP_KNAPSUR_COVER_CUTS         = 64;
   public const int    LS_MIP_LATTICE_CUTS               = 128;
   public const int    LS_MIP_GOMORY_CUTS                = 256;
   public const int    LS_MIP_COEF_REDC_CUTS             = 512;
   public const int    LS_MIP_GCD_CUTS                   = 1024;
   public const int    LS_MIP_OBJ_CUT                    = 2048;
   public const int    LS_MIP_BASIS_CUTS                 = 4096;
   public const int    LS_MIP_CARDGUB_CUTS               = 8192;
   public const int    LS_MIP_DISJUN_CUTS                = 16384;


/* Bit masks for MIP preprocessing levels. Use sums
 * to enable a collection of available levels.
 */
   public const int    LS_MIP_PREP_SPRE                  = 2;
   public const int    LS_MIP_PREP_PROB                  = 4;
   public const int    LS_MIP_PREP_COEF                  = 8;
   public const int    LS_MIP_PREP_ELIM                  = 16;
   public const int    LS_MIP_PREP_DUAL                  = 32;
   public const int    LS_MIP_PREP_DBACK                 = 64;
   public const int    LS_MIP_PREP_BINROWS               = 128;
   public const int    LS_MIP_PREP_AGGROWS               = 256;
   public const int    LS_MIP_PREP_COEF_LIFTING          = 512;
   public const int    LS_MIP_PREP_MAXPASS               = 1024;


/* Bit masks for solver preprocessing levels. Use sums
 * to enable a collection of available levels.
 */
   public const int    LS_SOLVER_PREP_SPRE               = 2;
   public const int    LS_SOLVER_PREP_PFOR               = 4;
   public const int    LS_SOLVER_PREP_DFOR               = 8;
   public const int    LS_SOLVER_PREP_ELIM               = 16;
   public const int    LS_SOLVER_PREP_DCOL               = 32;
   public const int    LS_SOLVER_PREP_DROW               = 64;
   public const int    LS_SOLVER_PREP_MAXPASS            = 1024;


/* Bit masks for IIS & IUS analysis levels. Use sums to
 * enable a collection of available levels.
 */
   public const int    LS_NECESSARY_ROWS                 = 1;
   public const int    LS_NECESSARY_COLS                 = 2;
   public const int    LS_SUFFICIENT_ROWS                = 4;
   public const int    LS_SUFFICIENT_COLS                = 8;


/* Infeasibility norms for IIS finder */
   public const int    LS_IIS_NORM_FREE                  = 0;
   public const int    LS_IIS_NORM_ONE                   = 1;
   public const int    LS_IIS_NORM_INFINITY              = 2;


/* IIS methods */
   public const int    LS_IIS_DEFAULT                    = 0;
   public const int    LS_IIS_DEL_FILTER                 = 1;
   public const int    LS_IIS_ADD_FILTER                 = 2;
   public const int    LS_IIS_GBS_FILTER                 = 3;
   public const int    LS_IIS_DFBS_FILTER                = 4;
   public const int    LS_IIS_FSC_FILTER                 = 5;
   public const int    LS_IIS_ELS_FILTER                 = 6;


/*codes for IINFO_MIP_WHERE_IN_CODE*/
   public const int    LS_MIP_IN_PRESOLVE                = 0 ;
   public const int    LS_MIP_IN_FP_MODE                 = 1 ;
   public const int    LS_MIP_IN_HEU_MODE                = 2 ;
   public const int    LS_MIP_IN_ENUM                    = 3 ;
   public const int    LS_MIP_IN_CUT_ADD_TOP             = 4 ;
   public const int    LS_MIP_IN_CUT_ADD_TREE            = 5 ;
   public const int    LS_MIP_IN_BANDB                   = 6 ;

   
/* Equivalences */
   public const int    LS_IINFO_OBJSENSE                 = LS_IPARAM_OBJSENSE;
   public const int    LS_IINFO_VER_MAJOR                = LS_IPARAM_VER_MAJOR;
   public const int    LS_IINFO_VER_MINOR                = LS_IPARAM_VER_MINOR;
   public const int    LS_IINFO_VER_BUILD                = LS_IPARAM_VER_BUILD;
   public const int    LS_IINFO_VER_REVISION             = LS_IPARAM_VER_REVISION;

/*********************************************************************
 *                   Conversion to version 1.x                       *
 *********************************************************************/

/* old parameter names */
    /* shay to avoid build error
   public const int    LS_IPARAM_LP_SCALE                = LS_IPARAM_SPLEX_SCALE;
   public const int    LS_IPARAM_LP_ITRLMT               = LS_IPARAM_SPLEX_ITRLMT;
   public const int    LSLOC_BANDB                       = LSLOC_MIP;
   public const int    LS_IPARAM_ITRLMT                  = LS_IPARAM_SPLEX_ITRLMT;
   public const int    LS_IPARAM_PRICING                 = LS_IPARAM_SPLEX_PPRICING;
   public const int    LS_IPARAM_SCALE                   = LS_IPARAM_SPLEX_SCALE;
   public const int    LS_IPARAM_TIMLMT                  = LS_IPARAM_SOLVER_TIMLMT;
   public const int    LS_DPARAM_CUTOFFVAL               = LS_DPARAM_SOLVER_CUTOFFVAL;
   public const int    LS_IPARAM_RESTART                 = LS_IPARAM_SOLVER_RESTART;
   public const int    LS_DPARAM_FEASTOL                 = LS_DPARAM_SOLVER_FEASTOL;
   public const int    LS_IPARAM_IUSOL                   = LS_IPARAM_SOLVER_IUSOL;
   public const int    LS_IPARAM_MIPTIMLIM               = LS_IPARAM_MIP_TIMLIM;
   public const int    LS_IPARAM_MIPAOPTTIMLIM           = LS_IPARAM_MIP_AOPTTIMLIM;
   public const int    LS_IPARAM_MIPPRELEVEL             = LS_IPARAM_MIP_PRELEVEL;
   public const int    LS_IPARAM_MIPNODESELRULE          = LS_IPARAM_MIP_NODESELRULE;
   public const int    LS_DPARAM_MIPINTTOL               = LS_DPARAM_MIP_INTTOL;
   public const int    LS_DPARAM_MIPRELINTTOL            = LS_DPARAM_MIP_RELINTTOL;
   public const int    LS_DPARAM_MIP_OPTTOL              = LS_DPARAM_MIP_RELOPTTOL;
   public const int    LS_DPARAM_MIPOPTTOL               = LS_DPARAM_MIP_OPTTOL;
   public const int    LS_DPARAM_MIPPEROPTTOL            = LS_DPARAM_MIP_PEROPTTOL;
   public const int    LS_IPARAM_MIPMAXCUTPASS           = LS_IPARAM_MIP_MAXCUTPASS_TOP;
   public const int    LS_DPARAM_MIPADDCUTPER            = LS_DPARAM_MIP_ADDCUTPER;
   public const int    LS_IPARAM_MIPCUTLEVEL             = LS_IPARAM_MIP_CUTLEVEL_TOP;
   public const int    LS_IPARAM_MIPHEULEVEL             = LS_IPARAM_MIP_HEULEVEL;
   public const int    LS_IPARAM_MIPPRINTLEVEL           = LS_IPARAM_MIP_PRINTLEVEL;
   public const int    LS_IPARAM_MIPPREPRINTLEVEL        = LS_IPARAM_MIP_PREPRINTLEVEL;
   public const int    LS_DPARAM_MIPCUTOFFOBJ            = LS_DPARAM_MIP_CUTOFFOBJ;
   public const int    LS_IPARAM_MIPSTRONGBRANCHLEVEL    = LS_IPARAM_MIP_STRONGBRANCHLEVEL;
   public const int    LS_IPARAM_MIPBRANCHDIR            = LS_IPARAM_MIP_BRANCHDIR;
   public const int    LS_IPARAM_MIPTOPOPT               = LS_IPARAM_MIP_TOPOPT;
   public const int    LS_IPARAM_MIPREOPT                = LS_IPARAM_MIP_REOPT;
   public const int    LS_IPARAM_MIPSOLVERTYPE           = LS_IPARAM_MIP_SOLVERTYPE;
   public const int    LS_IPARAM_MIPKEEPINMEM            = LS_IPARAM_MIP_KEEPINMEM;
   public const int    LS_DPARAM_MIP_REDCOSTFIXING_CUTOFF = LS_DPARAM_MIP_REDCOSTFIX_CUTOFF;
   public const int    LS_IPARAM_NLPPRINTLEVEL           = LS_IPARAM_NLP_PRINTLEVEL;
   public const int    LS_IPARAM_LPPRINTLEVEL            = LS_IPARAM_LP_PRINTLEVEL;
   public const int    LS_IPARAM_NLPSOLVER               = LS_IPARAM_NLP_SOLVER;
   public const int    LS_IPARAM_MODEL_CONVEX_FLAG       = LS_IPARAM_NLP_CONVEX;
   public const int    LS_IPARAM_NLP_SOLVEASLP           = LS_IPARAM_NLP_SOLVE_AS_LP;
   public const int    LS_DINFO_MIPBESTBOUND             = LS_DINFO_MIP_BESTBOUND;
   public const int    LS_IINFO_MIPBRANCHCOUNT           = LS_IINFO_MIP_BRANCHCOUNT;
   public const int    LS_IINFO_MIPSTATUS                = LS_IINFO_MIP_STATUS;
   public const int    LS_IINFO_MIPNEWIPSOL              = LS_IINFO_MIP_NEWIPSOL;
   public const int    LS_IINFO_MIPLPCOUNT               = LS_IINFO_MIP_LPCOUNT;
   public const int    LS_IINFO_MIPACTIVENODES           = LS_IINFO_MIP_ACTIVENODES;
   public const int    LS_IINFO_MIPLTYPE                 = LS_IINFO_MIP_LTYPE;
   public const int    LS_IINFO_MIPAOPTTIMETOSTOP        = LS_IINFO_MIP_AOPTTIMETOSTOP;
   public const int    LS_DINFO_MIPOBJ                   = LS_DINFO_MIP_OBJ;
   public const int    LS_IPARAM_BARRIER_PROB_TO_SOLVE   = LS_IPARAM_PROB_TO_SOLVE;
   public const int    LS_IINFO_STATUS                   = LS_IINFO_PRIMAL_STATUS;
   public const int    LS_GOPSOLSTAT_GLOBAL_OPTIMAL      = LS_STATUS_OPTIMAL;
   public const int    LS_GOPSOLSTAT_LOCAL_OPTIMAL       = LS_STATUS_LOCAL_OPTIMAL;
   public const int    LS_GOPSOLSTAT_INFEASIBLE          = LS_STATUS_INFEASIBLE;
   public const int    LS_GOPSOLSTAT_TOPUNBOUNDED        = LS_STATUS_UNBOUNDED;
   public const int    LS_GOPSOLSTAT_FEASIBLE            = LS_STATUS_FEASIBLE;
   public const int    LS_GOPSOLSTAT_UNKNOWN             = LS_STATUS_UNKNOWN;
   public const int    LS_GOPSOLSTAT_NUMERICAL_ERROR     = LS_STATUS_NUMERICAL_ERROR;
   public const int    LS_IIS_NORM_NONE                  = LS_IIS_NORM_FREE;

/* old operator names */
   public const int    EP_EXT_AND                        = EP_VAND;
   public const int    EP_EXT_OR                         = EP_VOR;
   public const int    EP_MULTMULT                       = EP_VMULT;
   
/*********************************************************************
 * Structure Creation and Deletion Routines (4)                      *
 *********************************************************************/


      [DllImport(LINDO_DLL,
      EntryPoint="LScreateEnv")]
      public static extern IntPtr LScreateEnv 
      (                                     ref          int        pnErrorcode         ,
                                                      string        pszPassword         );


      [DllImport(LINDO_DLL,
      EntryPoint="LScreateModel")]
      public static extern IntPtr LScreateModel 
      (                                                 IntPtr             pEnv           ,
                                            ref          int        pnErrorcode         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSdeleteEnv")]
      public static extern int LSdeleteEnv 
      (                                     ref         IntPtr             pEnv           );


      [DllImport(LINDO_DLL,
      EntryPoint="LSdeleteModel")]
      public static extern int LSdeleteModel 
      (                                     ref         IntPtr             pModel         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSloadLicenseString")]
      public static extern int LSloadLicenseString 
      (                                               string           pszFname         ,
                                                StringBuilder        pachLicense         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetVersionInfo")]
      public static extern int LSgetVersionInfo 
      (                                         StringBuilder         pachVernum         ,
                                                StringBuilder       pachBuildDate         );

/**********************************************************************
 * Model I-O Routines (13)                                            *
 **********************************************************************/


      [DllImport(LINDO_DLL,
      EntryPoint="LSreadMPSFile")]
      public static extern int LSreadMPSFile 
      (                                               IntPtr             pModel         ,
                                                      string           pszFname         ,
                                                         int            nFormat         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSwriteMPSFile")]
      public static extern int LSwriteMPSFile 
      (                                               IntPtr             pModel         ,
                                                      string           pszFname         ,
                                                         int            nFormat         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSreadLINDOFile")]
      public static extern int LSreadLINDOFile 
      (                                               IntPtr             pModel         ,
                                                      string           pszFname         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSwriteLINDOFile")]
      public static extern int LSwriteLINDOFile 
      (                                               IntPtr             pModel         ,
                                                      string           pszFname         );


      [DllImport(LINDO_DLL,
     EntryPoint="LSreadLINDOStream")]
      public static extern int LSreadLINDOStream 
      (                                               IntPtr             pModel         ,
                                                      string          pszStream         ,
                                                         int         nStreamLen         );
                                                         
      [DllImport(LINDO_DLL,
      EntryPoint="LSwriteLINGOFile")]                                                         
      public static extern int LSwriteLINGOFile 
      (                                               IntPtr             pModel         ,
                                                      string           pszFname         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSwriteDualMPSFile")]
      public static extern int LSwriteDualMPSFile 
      (                                               IntPtr             pModel         ,
                                                      string           pszFname         ,
                                                         int            nFormat         ,
                                                         int          nObjSense         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSwriteDualLINDOFile")]
      public static extern int LSwriteDualLINDOFile 
      (                                               IntPtr             pModel         ,
                                                      string           pszFname         ,
                                                         int          nObjSense         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSwriteSolution")]
      public static extern int LSwriteSolution 
      (                                               IntPtr             pModel         ,
                                                      string           pszFname         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSwriteSolutionOfType")]
      public static extern int LSwriteSolutionOfType 
      (                                               IntPtr             pModel         ,
                                                      string           pszFname         ,
                                                         int            nFormat         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSwriteIIS")]
      public static extern int LSwriteIIS 
      (                                               IntPtr             pModel         ,
                                                      string           pszFname         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSwriteIUS")]
      public static extern int LSwriteIUS 
      (                                               IntPtr             pModel         ,
                                                      string           pszFname         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSreadMPIFile")]
      public static extern int LSreadMPIFile 
      (                                               IntPtr             pModel         ,
                                                      string           pszFname         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSwriteMPIFile")]
      public static extern int LSwriteMPIFile 
      (                                               IntPtr             pModel         ,
                                                      string           pszFname         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSreadBasis")]
      public static extern int LSreadBasis 
      (                                               IntPtr             pModel         ,
                                                      string           pszFname         ,
                                                         int            nFormat         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSwriteBasis")]
      public static extern int LSwriteBasis 
      (                                               IntPtr             pModel         ,
                                                      string           pszFname         ,
                                                         int            nFormat         );

      [DllImport("lindo5_0.dll",
     EntryPoint="LSreadLPFile")]
      public static extern int LSreadLPFile 
      (                                               IntPtr             pModel         ,
                                                      string           pszFname         );


      [DllImport("lindo5_0.dll",
     EntryPoint="LSreadLPStream")]
      public static extern int LSreadLPStream 
      (                                               IntPtr             pModel         ,
                                                      string          pszStream         ,
                                                         int         nStreamLen         );

/**********************************************************************
 * Error Handling Routines (3)                                        *
 **********************************************************************/


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetErrorMessage")]
      public static extern int LSgetErrorMessage 
      (                                                 IntPtr             pEnv           ,
                                                         int         nErrorcode         ,
                                                StringBuilder        pachMessage         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetFileError")]
      public static extern int LSgetFileError 
      (                                               IntPtr             pModel         ,
                                            ref          int          pnLinenum         ,
                                                StringBuilder        pachLinetxt         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetErrorRowIndex")]
      public static extern int LSgetErrorRowIndex 
      (                                               IntPtr             pModel         ,
                                            ref          int              piRow         );

/**********************************************************************
 * Routines for Setting and Retrieving Parameter Values (14)          *
 **********************************************************************/


    [DllImport(LINDO_DLL,
    EntryPoint="LSsetModelParameter")]
    public static extern int LSsetModelParameter
    (                                   IntPtr         nModel,
                                        int            nParameter,
                                    ref int            nvValue );


    [DllImport(LINDO_DLL,
    EntryPoint="LSgetModelParameter")]
    public static extern int LSgetModelParameter
    (                                   IntPtr         nModel,
                                        int            nParameter,
                                    ref int            nvValue );


    [DllImport(LINDO_DLL,
    EntryPoint="LSsetEnvParameter")]
    public static extern int LSsetEnvParameter
    (                                   IntPtr         nEnv,
                                        int            nParameter,
                                    ref int            nvValue );


    [DllImport(LINDO_DLL,
    EntryPoint="LSgetEnvParameter")]
    public static extern int LSgetEnvParameter
    (                                   IntPtr         nEnv,
                                        int            nParameter,
                                    ref int            nvValue );


    [DllImport(LINDO_DLL,
    EntryPoint="LSsetModelParameter")]
    public static extern int LSsetModelParameter
    (                                   IntPtr         nModel,
                                        int            nParameter,
                                    ref double         nvValue );


    [DllImport(LINDO_DLL,
    EntryPoint="LSgetModelParameter")]
    public static extern int LSgetModelParameter
    (                                   IntPtr         nModel,
                                        int            nParameter,
                                    ref double         nvValue );


    [DllImport(LINDO_DLL,
    EntryPoint="LSsetEnvParameter")]
    public static extern int LSsetEnvParameter
    (                                   IntPtr         nEnv,
                                        int            nParameter,
                                    ref double         nvValue );


    [DllImport(LINDO_DLL,
    EntryPoint="LSgetEnvParameter")]
    public static extern int LSgetEnvParameter
    (                                   IntPtr         nEnv,
                                        int            nParameter,
                                    ref double         nvValue );


    [DllImport(LINDO_DLL,
    EntryPoint="LSsetModelDouParameter")]
    public static extern int LSsetModelDouParameter
    (                                   IntPtr         nModel,
                                        int            nParameter,
                                        double         dVal);


    [DllImport(LINDO_DLL,
    EntryPoint="LSgetModelDouParameter")]
    public static extern int LSgetModelDouParameter
    (                                   IntPtr         nModel,
                                        int            nParameter,
                                    ref double         dVal );


    [DllImport(LINDO_DLL,
    EntryPoint="LSsetModelIntParameter")]
    public static extern int LSsetModelIntParameter
    (                                   IntPtr         nModel,
                                        int            nParameter,
                                        int            nVal);


    [DllImport(LINDO_DLL,
    EntryPoint="LSgetModelIntParameter")]
    public static extern int LSgetModelIntParameter
    (                                   IntPtr         nModel,
                                        int            nParameter,
                                    ref int            nVal );


    [DllImport(LINDO_DLL,
    EntryPoint="LSsetEnvDouParameter")]
    public static extern int LSsetEnvDouParameter
    (                                   IntPtr         nEnv,
                                        int            nParameter,
                                        double         dVal);


    [DllImport(LINDO_DLL,
    EntryPoint="LSgetEnvDouParameter")]
    public static extern int LSgetEnvDouParameter
    (                                   IntPtr         nEnv,
                                        int            nParameter,
                                    ref double         dVal );


    [DllImport(LINDO_DLL,
    EntryPoint="LSsetEnvIntParameter")]
    public static extern int LSsetEnvIntParameter
    (                                   IntPtr         nEnv,
                                        int            nParameter,
                                        int            nVal);


    [DllImport(LINDO_DLL,
    EntryPoint="LSgetEnvIntParameter")]
    public static extern int LSgetEnvIntParameter
    (                                   IntPtr         nEnv,
                                        int            nParameter,
                                    ref int            nVal );


    [DllImport(LINDO_DLL,
    EntryPoint="LSreadModelParameter")]
    public static extern int LSreadModelParameter
    (                                   IntPtr         nModel,
                                        string         szFname);

    [DllImport(LINDO_DLL,
    EntryPoint="LSreadEnvParameter")]
    public static extern int LSreadEnvParameter
    (                                   IntPtr         nEnv,
                                        string         szFname);


    [DllImport(LINDO_DLL,
    EntryPoint="LSgetFormattedInfo")]
    public static extern int LSgetFormattedInfo   
    (                                   IntPtr         nModel,    
                                        string         pszFname,
                                        StringBuilder  achStrHead,
                                        StringBuilder  achStrInfo,
                                        int            nFormatId);

      [DllImport(LINDO_DLL,
      EntryPoint="LSwriteModelParameter")]
      public static extern int LSwriteModelParameter 
      (                                               IntPtr             pModel         ,
                                                      string           pszFname         );

 /*********************************************************************
  * Model Loading Routines (9)                                        *
  *********************************************************************/


      [DllImport(LINDO_DLL,
      EntryPoint="LSloadLPData")]
      public static extern int LSloadLPData 
      (                                               IntPtr             pModel         ,
                                                         int              nCons         ,
                                                         int              nVars         ,
                                                         int          dObjSense         ,
                                                      double          dObjConst         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []         padC         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []         padB         ,
                                                      string        pszConTypes         ,
                                                         int              nAnnz         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []     paiAcols         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []     panAcols         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []     padAcoef         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []     paiArows         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []         padL         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []         padU         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSloadQCData")]
      public static extern int LSloadQCData 
      (                                               IntPtr             pModel         ,
                                                         int             nQCnnz         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []    paiQCrows         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []   paiQCcols1         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []   paiQCcols2         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []    padQCcoef         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSloadConeData")]
      public static extern int LSloadConeData 
      (                                               IntPtr             pModel         ,
                                                         int              nCone         ,
                                                      string       pszConeTypes         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    [] paiConebegcone         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []  paiConecols         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSloadSETSData")]
      public static extern int LSloadSETSData 
      (                                               IntPtr             pModel         ,
                                                         int              nSETS         ,
                                                      string        pszSETStype         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []   paiCARDnum         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    [] paiSETSbegcol         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []  paiSETScols         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSloadSemiContData")]
      public static extern int LSloadSemiContData 
      (                                               IntPtr             pModel         ,
                                                         int            nSCVars         ,
                                            ref          int           piVarndx         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []         padl         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []         padu         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSloadVarType")]
      public static extern int LSloadVarType 
      (                                               IntPtr             pModel         ,
                                                      string        pszVarTypes         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSloadNameData")]
      public static extern int LSloadNameData 
      (                                               IntPtr             pModel         ,
                                                      string           pszTitle         ,
                                                      string         pszObjName         ,
                                                      string         pszRhsName         ,
                                                      string         pszRngName         ,
                                                      string         pszBndname         ,
             [MarshalAs(UnmanagedType.LPArray)]       string    [] paszConNames         ,
             [MarshalAs(UnmanagedType.LPArray)]       string    [] paszVarNames         ,
             [MarshalAs(UnmanagedType.LPArray)]       string    [] paszConeNames         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSloadStocVarNames")]
      public static extern int LSloadStocVarNames 
      (                                               IntPtr             pModel         ,
                                                         int             nSvars         ,
             [MarshalAs(UnmanagedType.LPArray)]       string    [] paszSVarNames         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSloadNLPData")]
      public static extern int LSloadNLPData 
      (                                               IntPtr             pModel         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []   paiNLPcols         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []   panNLPcols         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []   padNLPcoef         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []   paiNLProws         ,
                                                         int            nNLPobj         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []    paiNLPobj         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []    padNLPobj         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSloadInstruct")]
      public static extern int LSloadInstruct 
      (                                               IntPtr             pModel         ,
                                                         int              nCons         ,
                                                         int              nObjs         ,
                                                         int              nVars         ,
                                                         int           nNumbers         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []  panObjSense         ,
                                                      string         pszConType         ,
                                                      string         pszVarType         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []  panInstruct         ,
                                                         int          nInstruct         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []      paiVars         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []    padNumVal         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []    padVarVal         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []    paiObjBeg         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []    panObjLen         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []    paiConBeg         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []    panConLen         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []        padLB         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []        padUB         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSaddInstruct")]
      public static extern int LSaddInstruct 
      (                                               IntPtr             pModel         ,
                                                         int              nCons         ,
                                                         int              nObjs         ,
                                                         int              nVars         ,
                                                         int           nNumbers         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []  panObjSense         ,
                                                      string         pszConType         ,
                                                      string         pszVarType         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []  panInstruct         ,
                                                         int          nInstruct         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []      paiCons         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []    padNumVal         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []    padVarVal         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []    paiObjBeg         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []    panObjLen         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []    paiConBeg         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []    panConLen         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []        padLB         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []        padUB         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSloadStringData")]
      public static extern int LSloadStringData 
      (                                               IntPtr             pModel         ,
                                                         int           nStrings         ,
             [MarshalAs(UnmanagedType.LPArray)]       string[]  paszStringData          );
             
      [DllImport(LINDO_DLL,
      EntryPoint="LSloadString")]
      public static extern int LSloadString 
      (                                               IntPtr             pModel         ,
                                                      string           szString         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSbuildStringData")]
      public static extern int LSbuildStringData 
      (                                                 IntPtr             pModel         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSdeleteStringData")]
      public static extern int LSdeleteStringData 
      (                                                 IntPtr             pModel         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSdeleteString")]
      public static extern int LSdeleteString 
      (                                                 IntPtr             pModel         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetStringValue")]
      public static extern int LSgetStringValue 
      (                                               IntPtr             pModel         ,
                                                         int            iString         ,
                                            ref       double            pdValue         );

/**********************************************************************
 * Solver Initialization Routines (6)                                 *
 **********************************************************************/


      [DllImport(LINDO_DLL,
      EntryPoint="LSloadBasis")]
      public static extern int LSloadBasis 
      (                                               IntPtr             pModel         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []   panCstatus         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []   panRstatus         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSloadVarPriorities")]
      public static extern int LSloadVarPriorities 
      (                                               IntPtr             pModel         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []    panCprior         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSreadVarPriorities")]
      public static extern int LSreadVarPriorities 
      (                                               IntPtr             pModel         ,
                                                      string           pszFname         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSloadVarStartPoint")]
      public static extern int LSloadVarStartPoint 
      (                                               IntPtr             pModel         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []    padPrimal         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSreadVarStartPoint")]
      public static extern int LSreadVarStartPoint 
      (                                               IntPtr             pModel         ,
                                                      string           pszFname         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSloadBlockStructure")]
      public static extern int LSloadBlockStructure 
      (                                               IntPtr             pModel         ,
                                                         int             nBlock         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []    panRblock         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []    panCblock         ,
                                                         int              nType         );

/**********************************************************************
 * Optimization Routines (3)                                          *
 **********************************************************************/


      [DllImport(LINDO_DLL,
      EntryPoint="LSoptimize")]
      public static extern int LSoptimize 
      (                                               IntPtr             pModel         ,
                                                         int            nMethod         ,
                                            ref          int        pnSolStatus         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSsolveMIP")]
      public static extern int LSsolveMIP 
      (                                               IntPtr             pModel         ,
                                            ref          int       pnMIPSolStatus         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSsolveGOP")]
      public static extern int LSsolveGOP 
      (                                               IntPtr             pModel         ,
                                            ref          int       pnGOPSolStatus         );

       /* query general model and solver information */


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetInfo")]
      public static extern int  LSgetInfo 
      (                                               IntPtr             pModel         ,
                                                         int             nQuery         ,
                                            ref          int           pvResult         );

      [DllImport(LINDO_DLL,
      EntryPoint="LSgetInfo")]
      public static extern int  LSgetInfo 
      (                                               IntPtr             pModel         ,
                                                         int             nQuery         ,
                                            ref       double           pvResult         );

       /* query continous models */


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetPrimalSolution")]
      public static extern int LSgetPrimalSolution 
      (                                               IntPtr             pModel         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []    padPrimal         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetDualSolution")]
      public static extern int LSgetDualSolution 
      (                                               IntPtr             pModel         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []      padDual         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetReducedCosts")]
      public static extern int LSgetReducedCosts 
      (                                               IntPtr             pModel         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []  padRedcosts         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetReducedCostsCone")]
      public static extern int LSgetReducedCostsCone 
      (                                               IntPtr             pModel         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []  padRedcosts         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetSlacks")]
      public static extern int LSgetSlacks 
      (                                               IntPtr             pModel         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []    padSlacks         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetBasis")]
      public static extern int LSgetBasis 
      (                                               IntPtr             pModel         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []   panCstatus         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []   panRstatus         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetSolution")]
      public static extern int LSgetSolution 
      (                                               IntPtr             pModel         ,
                                                         int             nWhich         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []       padVal         );

       /* query integer models */


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetMIPPrimalSolution")]
      public static extern int LSgetMIPPrimalSolution 
      (                                               IntPtr             pModel         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []    padPrimal         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetMIPDualSolution")]
      public static extern int LSgetMIPDualSolution 
      (                                               IntPtr             pModel         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []      padDual         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetMIPReducedCosts")]
      public static extern int LSgetMIPReducedCosts 
      (                                               IntPtr             pModel         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []  padRedcosts         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetMIPSlacks")]
      public static extern int LSgetMIPSlacks 
      (                                               IntPtr             pModel         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []    padSlacks         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetMIPBasis")]
      public static extern int LSgetMIPBasis 
      (                                               IntPtr             pModel         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []   panCstatus         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []   panRstatus         );

 /*********************************************************************
  * Model Query Routines (13)                                         *
  *********************************************************************/


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetLPData")]
      public static extern int LSgetLPData 
      (                                               IntPtr             pModel         ,
                                            ref          int         pdObjSense         ,
                                            ref       double         pdObjConst         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []         padC         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []         padB         ,
                                                StringBuilder       pachConTypes         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []     paiAcols         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []     panAcols         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []     padAcoef         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []     paiArows         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []         padL         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []         padU         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetQCData")]
      public static extern int LSgetQCData 
      (                                               IntPtr             pModel         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []    paiQCrows         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []   paiQCcols1         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []   paiQCcols2         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []    padQCcoef         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetQCDatai")]
      public static extern int LSgetQCDatai 
      (                                               IntPtr             pModel         ,
                                                         int               iCon         ,
                                            ref          int            pnQCnnz         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []   paiQCcols1         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []   paiQCcols2         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []    padQCcoef         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetVarType")]
      public static extern int LSgetVarType 
      (                                               IntPtr             pModel         ,
                                                StringBuilder       pachVarTypes         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetVarStartPoint")]
      public static extern int LSgetVarStartPoint 
      (                                               IntPtr             pModel         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []    padPrimal         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetNameData")]
      public static extern int LSgetNameData 
      (                                               IntPtr             pModel         ,
                                                StringBuilder          pachTitle         ,
                                                StringBuilder        pachObjName         ,
                                                StringBuilder        pachRhsName         ,
                                                StringBuilder        pachRngName         ,
                                                StringBuilder        pachBndname         ,
                                                StringBuilder       pachConNames         ,
                                                StringBuilder       pachConNameData      ,
                                                StringBuilder       pachVarNames         ,
                                                StringBuilder       pachVarNameData         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetLPVariableDataj")]
      public static extern int LSgetLPVariableDataj 
      (                                               IntPtr             pModel         ,
                                                         int               iVar         ,
                                                StringBuilder        pachVartype         ,
                                            ref       double                pdC         ,
                                            ref       double                pdL         ,
                                            ref       double                pdU         ,
                                            ref          int             pnAnnz         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []     paiArows         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []     padAcoef         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetVariableNamej")]
      public static extern int LSgetVariableNamej 
      (                                               IntPtr             pModel         ,
                                                         int               iVar         ,
                                                StringBuilder        pachVarName         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetVariableIndex")]
      public static extern int LSgetVariableIndex 
      (                                               IntPtr             pModel         ,
                                                      string         pszVarName         ,
                                            ref          int              piVar         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetConstraintNamei")]
      public static extern int LSgetConstraintNamei 
      (                                               IntPtr             pModel         ,
                                                         int               iCon         ,
                                                StringBuilder        pachConName         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetConstraintIndex")]
      public static extern int LSgetConstraintIndex 
      (                                               IntPtr             pModel         ,
                                                      string         pszConName         ,
                                            ref          int              piCon         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetConstraintDatai")]
      public static extern int LSgetConstraintDatai 
      (                                               IntPtr             pModel         ,
                                                         int               iCon         ,
                                                StringBuilder        pachConType         ,
                                                StringBuilder          pachIsNlp         ,
                                            ref       double                pdB         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetLPConstraintDatai")]
      public static extern int LSgetLPConstraintDatai 
      (                                                  int              Model         ,
                                                         int               iCon         ,
                                                StringBuilder        pachConType         ,
                                            ref       double                pdB         ,
                                            ref          int              pnNnz         ,
                                            ref          int              piVar         ,
                                            ref       double            pdAcoef         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetConeNamei")]
      public static extern int LSgetConeNamei 
      (                                               IntPtr             pModel         ,
                                                         int              iCone         ,
                                                StringBuilder       pachConeName         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetConeIndex")]
      public static extern int LSgetConeIndex 
      (                                               IntPtr             pModel         ,
                                                      string        pszConeName         ,
                                            ref          int             piCone         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetConeDatai")]
      public static extern int LSgetConeDatai 
      (                                               IntPtr             pModel         ,
                                                         int              iCone         ,
                                                StringBuilder       pachConeType        ,
                                            ref          int              piNnz         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []   paiCols           );


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetNLPData")]
      public static extern int LSgetNLPData 
      (                                               IntPtr             pModel         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []   paiNLPcols         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []   panNLPcols         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []   padNLPcoef         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []   paiNLProws         ,
                                            ref          int           pnNLPobj         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []    paiNLPobj         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []    padNLPobj         ,
                                                StringBuilder       pachNLPConTypes         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetNLPConstraintDatai")]
      public static extern int LSgetNLPConstraintDatai 
      (                                               IntPtr             pModel         ,
                                                         int               iCon         ,
                                            ref          int              pnNnz         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []   paiNLPcols         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []   padNLPcoef         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetNLPVariableDataj")]
      public static extern int LSgetNLPVariableDataj 
      (                                               IntPtr             pModel         ,
                                                         int               iVar         ,
                                            ref          int              pnNnz         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []   panNLProws         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []   padNLPcoef         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetNLPObjectiveData")]
      public static extern int LSgetNLPObjectiveData 
      (                                               IntPtr             pModel         ,
                                            ref          int        pnNLPobjnnz         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []    paiNLPobj         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []    padNLPobj         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetDualModel")]
      public static extern int LSgetDualModel 
      (                                               IntPtr             pModel         ,
                                                         int         pDualModel         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetInstruct")]
      public static extern int LSgetInstruct 
      (                                               IntPtr             pModel         ,
                                            ref          int         pnObjSense         ,
                                                StringBuilder        pachConType         ,
                                                StringBuilder        pachVarType         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []      panCode         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []    padNumVal         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []    padVarVal         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []    panObjBeg         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    [] panObjLength         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []    panConBeg         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    [] panConLength         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []    padLwrBnd         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []    padUprBnd         );


      [DllImport(LINDO_DLL,
      EntryPoint="LScalinfeasMIPsolution")]
      public static extern int LScalinfeasMIPsolution 
      (                                               IntPtr             pModel         ,
                                            ref       double         pdIntPfeas         ,
                                            ref       double        pbConsPfeas         ,
                                            ref       double       pdPrimalMipsol         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetRoundMIPsolution")]
      public static extern int LSgetRoundMIPsolution 
      (                                               IntPtr             pModel         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []    padPrimal         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    [] padPrimalRound         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []  padObjRound         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    [] padPfeasRound         ,
                                            ref          int           pnstatus         ,
                                                         int           iUseOpti         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetDuplicateColumns")]
      public static extern int LSgetDuplicateColumns 
      (                                               IntPtr             pModel         ,
                                                         int         nCheckVals         ,
                                            ref          int             pnSets         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []   paiSetsBeg         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []      paiCols         );

/**********************************************************************
 *  Model Modification Routines (22)                                  *
 **********************************************************************/


      [DllImport(LINDO_DLL,
      EntryPoint="LSaddConstraints")]
      public static extern int LSaddConstraints 
      (                                               IntPtr             pModel         ,
                                                         int        nNumaddcons         ,
                                                      string        pszConTypes         ,
             [MarshalAs(UnmanagedType.LPArray)]       string    [] paszConNames         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []     paiArows         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []     padAcoef         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []     paiAcols         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []         padB         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSaddVariables")]
      public static extern int LSaddVariables 
      (                                               IntPtr             pModel         ,
                                                         int        nNumaddvars         ,
                                                      string        pszVarTypes         ,
             [MarshalAs(UnmanagedType.LPArray)]       string    [] paszVarNames         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []     paiAcols         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []     panAcols         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []     padAcoef         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []     paiArows         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []         padC         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []         padL         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []         padU         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSaddCones")]
      public static extern int LSaddCones 
      (                                               IntPtr             pModel         ,
                                                         int              nCone         ,
                                                      string       pszConeTypes         ,
             [MarshalAs(UnmanagedType.LPArray)]       string    [] paszConenames        ,
             [MarshalAs(UnmanagedType.LPArray)]          int    [] paiConebegcol         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []  paiConecols         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSaddSETS")]
      public static extern int  LSaddSETS 
      (                                               IntPtr             pModel         ,
                                                         int              nSETS         ,
                                                      string        pszSETStype         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []   paiCARDnum         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    [] paiSETSbegcol         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []  paiSETScols         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSaddQCterms")]
      public static extern int LSaddQCterms 
      (                                               IntPtr             pModel         ,
                                                         int        nQCnonzeros         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []  paiQCconndx         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    [] paiQCvarndx1         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    [] paiQCvarndx2         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []    padQCcoef         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSdeleteConstraints")]
      public static extern int LSdeleteConstraints 
      (                                               IntPtr             pModel         ,
                                                         int              nCons         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []      paiCons         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSdeleteCones")]
      public static extern int LSdeleteCones 
      (                                               IntPtr             pModel         ,
                                                         int             nCones         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []     paiCones         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSdeleteSETS")]
      public static extern int LSdeleteSETS 
      (                                               IntPtr             pModel         ,
                                                         int              nSETS         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []     paiSETS         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSdeleteSemiContVars")]
      public static extern int LSdeleteSemiContVars 
      (                                               IntPtr             pModel         ,
                                                         int            nSCVars         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []     paiSCVars        );


      [DllImport(LINDO_DLL,
      EntryPoint="LSdeleteVariables")]
      public static extern int LSdeleteVariables 
      (                                               IntPtr             pModel         ,
                                                         int              nVars         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []      paiVars         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSdeleteQCterms")]
      public static extern int LSdeleteQCterms 
      (                                               IntPtr             pModel         ,
                                                         int              nCons         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []      paiCons         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSdeleteAj")]
      public static extern int LSdeleteAj 
      (                                               IntPtr             pModel         ,
                                                         int              iVar1         ,
                                                         int              nRows         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []      paiRows         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSmodifyLowerBounds")]
      public static extern int LSmodifyLowerBounds 
      (                                               IntPtr             pModel         ,
                                                         int              nVars         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []      paiVars         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []         padL         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSmodifyUpperBounds")]
      public static extern int LSmodifyUpperBounds 
      (                                               IntPtr             pModel         ,
                                                         int              nVars         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []      paiVars         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []         padU         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSmodifyRHS")]
      public static extern int LSmodifyRHS 
      (                                               IntPtr             pModel         ,
                                                         int              nCons         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []      paiCons         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []         padB         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSmodifyObjective")]
      public static extern int LSmodifyObjective 
      (                                               IntPtr             pModel         ,
                                                         int              nVars         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []      paiVars         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []         padC         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSmodifyObjConstant")]
      public static extern int LSmodifyObjConstant 
      (                                               IntPtr             pModel         ,
                                                      double          dObjConst         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSmodifyAj")]
      public static extern int LSmodifyAj 
      (                                               IntPtr             pModel         ,
                                                         int              iVar1         ,
                                                         int              nRows         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []      paiRows         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []        padAj         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSmodifyCone")]
      public static extern int LSmodifyCone 
      (                                               IntPtr             pModel         ,
                                                      string          cConeType         ,
                                                         int           iConeNum         ,
                                                         int           iConeNnz         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []  paiConeCols         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSmodifySET")]
      public static extern int LSmodifySET 
      (                                               IntPtr             pModel         ,
                                                      string           cSETtype         ,
                                                         int            iSETnum         ,
                                                         int            iSETnnz         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []   paiSETcols         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSmodifySemiContVars")]
      public static extern int LSmodifySemiContVars 
      (                                               IntPtr             pModel         ,
                                                         int            nSCVars         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []    paiSCVars         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []         padL         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []         padU         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSmodifyConstraintType")]
      public static extern int LSmodifyConstraintType 
      (                                               IntPtr             pModel         ,
                                                         int              nCons         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []      paiCons         ,
                                                      string        pszConTypes         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSmodifyVariableType")]
      public static extern int LSmodifyVariableType 
      (                                               IntPtr             pModel         ,
                                                         int              nVars         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []      paiVars         ,
                                                      string        pszVarTypes         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSaddNLPAj")]
      public static extern int LSaddNLPAj 
      (                                               IntPtr             pModel         ,
                                                         int              iVar1         ,
                                                         int              nRows         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []      paiRows         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []        padAj         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSaddNLPobj")]
      public static extern int LSaddNLPobj 
      (                                               IntPtr             pModel         ,
                                                         int              nCols         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []      paiCols         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []      padColj         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSdeleteNLPobj")]
      public static extern int LSdeleteNLPobj 
      (                                               IntPtr             pModel         ,
                                                         int              nCols         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []      paiCols         );

/*********************************************************************
 *   Model & Solution Analysis Routines (10)                         *
 *********************************************************************/


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetConstraintRanges")]
      public static extern int LSgetConstraintRanges 
      (                                               IntPtr             pModel         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []       padDec         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []       padInc         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetObjectiveRanges")]
      public static extern int LSgetObjectiveRanges 
      (                                               IntPtr             pModel         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []       padDec         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []       padInc         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetBoundRanges")]
      public static extern int LSgetBoundRanges 
      (                                               IntPtr             pModel         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []       padDec         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []       padInc         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetBestBounds")]
      public static extern int LSgetBestBounds 
      (                                               IntPtr             pModel         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []     padBestL         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []     padBestU         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSfindIIS")]
      public static extern int  LSfindIIS 
      (                                               IntPtr             pModel         ,
                                                         int             nLevel         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSfindIUS")]
      public static extern int  LSfindIUS 
      (                                               IntPtr             pModel         ,
                                                         int             nLevel         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSfindBlockStructure")]
      public static extern int LSfindBlockStructure 
      (                                               IntPtr             pModel         ,
                                                         int             nBlock         ,
                                                         int              nType         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetIIS")]
      public static extern int   LSgetIIS 
      (                                               IntPtr             pModel         ,
                                            ref          int            pnSuf_r         ,
                                            ref          int            pnIIS_r         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []      paiCons         ,
                                            ref          int            pnSuf_c         ,
                                            ref          int            pnIIS_c         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []      paiVars         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []      panBnds         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetIUS")]
      public static extern int   LSgetIUS 
      (                                               IntPtr             pModel         ,
                                            ref          int              pnSuf         ,
                                            ref          int              pnIUS         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []      paiVars         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetBlockStructure")]
      public static extern int LSgetBlockStructure 
      (                                               IntPtr             pModel         ,
                                            ref          int            pnBlock         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []    panRblock         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []    panCblock         ,
                                            ref          int             pnType         );

/**********************************************************************
 * Advanced Routines (6)                                              *
 **********************************************************************/


      [DllImport(LINDO_DLL,
      EntryPoint="LSdoBTRAN")]
      public static extern int  LSdoBTRAN 
      (                                               IntPtr             pModel         ,
                                            ref          int              pcYnz         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []         paiY         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []         padY         ,
                                            ref          int              pcXnz         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []         paiX         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []         padX         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSdoFTRAN")]
      public static extern int  LSdoFTRAN 
      (                                               IntPtr             pModel         ,
                                            ref          int              pcYnz         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []         paiY         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []         padY         ,
                                            ref          int              pcXnz         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []         paiX         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []         padX         );

       /* function and gradient evaluations */


      [DllImport(LINDO_DLL,
      EntryPoint="LScalcObjFunc")]
      public static extern int LScalcObjFunc 
      (                                               IntPtr             pModel         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []    padPrimal         ,
                                            ref       double           pdObjval         );


      [DllImport(LINDO_DLL,
      EntryPoint="LScalcConFunc")]
      public static extern int LScalcConFunc 
      (                                               IntPtr             pModel         ,
                                                         int               iRow         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []    padPrimal         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []    padSlacks         );


      [DllImport(LINDO_DLL,
      EntryPoint="LScalcObjGrad")]
      public static extern int LScalcObjGrad 
      (                                               IntPtr             pModel         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []    padPrimal         ,
                                                         int           nParList         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []   paiParList         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []   padParGrad         );


      [DllImport(LINDO_DLL,
      EntryPoint="LScalcConGrad")]
      public static extern int LScalcConGrad 
      (                                               IntPtr             pModel         ,
                                                         int               irow         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []    padPrimal         ,
                                                         int           nParList         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []   paiParList         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []   padParGrad         );

/**********************************************************************
 * Callback Management Routines (9)                                   *
 **********************************************************************/


    /* Delegate declarations */
    
    public delegate int typCallback
    (                                   IntPtr            nModel,
                                        int            nLoc,
                                        IntPtr         nvCbData );
                                        
    public delegate int typMIPCallback
    (                                   IntPtr            nModel,
                                        IntPtr         nvCbData,
                                        double         dObj,
                                        IntPtr         adPrimal);   
                                        
    public delegate int typFuncalc 
    (
                                        IntPtr            nModel, 
                                        IntPtr         nvCbData,
                                        int            nRow,
                                        IntPtr         adX,
                                        int            nJDiff,
                                        double         dXJBase,
                                   ref  double         dFuncVal,
                                        IntPtr         pReserved);
                                        
    public delegate int typUsercalc 
    (
                                        IntPtr            nModel, 
                                        int            nArgs,
                                        IntPtr         pdValues,                                        
                                        IntPtr         nvCbData,
                                   ref  double         dFuncVal);                                        
                                        
    public delegate int typGradcalc 
    (                                   IntPtr            nModel,
                                        IntPtr         nvCbData,
                                        int            nRow,
                                        IntPtr         adX,
                                        IntPtr         adLB,
                                        IntPtr         adUB,
                                        int            nNewPnt,
                                        int            nNPar,
                                        IntPtr         aiPartial,
                                        IntPtr         adPartial);
                                        
    public delegate int typModelLOG
    (                                   IntPtr            nModel,
                                       string        achStr,
                                       IntPtr        nvCbData);

    public delegate int typEnvLOG
    (
                                        IntPtr            nEnv,
                                       string       achStr,
                                       IntPtr       nvCbData);                                        
                                        
    // General Callback Declaration 
    
    [DllImport(LINDO_DLL,
    EntryPoint="LSsetCallback")]
    public static extern int LSsetCallback
    (                                   IntPtr            nModel,
                                        typCallback    nfCallback,
                                        IntPtr         nvCbData );

    [DllImport(LINDO_DLL,
    EntryPoint="LSsetCallback")]
    public static extern int LSsetCallback
    (                                   IntPtr            nModel,
                                        typCallback    nfCallback,
     [MarshalAs(UnmanagedType.AsAny)]   object         nvCbData );


 
 
    // MIP Callback Declaration 

    [DllImport(LINDO_DLL,
    EntryPoint="LSsetMIPCallback")]
    public static extern int LSsetMIPCallback
    (                                   IntPtr            nModel,
                                        typMIPCallback nfMIPCallback,
                                        IntPtr         nvCbData );

    [DllImport(LINDO_DLL,
    EntryPoint="LSsetMIPCallback")]
    public static extern int LSsetMIPCallback
    (                                   IntPtr            nModel,
                                        typMIPCallback nfMIPCallback,
     [MarshalAs(UnmanagedType.AsAny)]   object         nvCbData );


    [DllImport(LINDO_DLL,
    EntryPoint = "LSsetModelLogfunc")]
    public static extern int LSsetModelLogfunc   
    (                                   IntPtr            nModel,
                                        typModelLOG nfCallback,
     [MarshalAs(UnmanagedType.AsAny)]   object nvCbData);


    [DllImport(LINDO_DLL,
    EntryPoint = "LSsetEnvLogfunc")]
    public static extern int LSsetEnvLogfunc
    (                                   IntPtr            nEnv,
                                        typEnvLOG nfCallback,
     [MarshalAs(UnmanagedType.AsAny)]   object nvCbData);

    [DllImport(LINDO_DLL,
    EntryPoint="LSgetCallbackInfo")]
    public static extern int LSgetCallbackInfo
    (                                   IntPtr            nModel,
                                        int            nLocation,
                                        int            nQuery,
                                    ref int            nvValue );

    [DllImport(LINDO_DLL,
    EntryPoint="LSgetCallbackInfo")]
    public static extern int LSgetCallbackInfo
    (                                   IntPtr            nModel,
                                        int            nLocation,
                                        int            nQuery,
                                    ref double         nvValue );

    [DllImport(LINDO_DLL,
    EntryPoint="LSgetQuickbackInfo")]
    public static extern int LSgetQuickbackInfo
    (                                   IntPtr            nModel,
                                        int            nLocation,
                                        int            nQuery,
                                    ref int            nvValue );

    [DllImport(LINDO_DLL,
    EntryPoint="LSgetQuickbackInfo")]
    public static extern int LSgetQuickbackInfo
    (                                   IntPtr            nModel,
                                        int            nLocation,
                                        int            nQuery,
                                    ref double         nvValue );


    [DllImport(LINDO_DLL,
    EntryPoint="LSgetMIPCallbackInfo")]
    public static extern int LSgetMIPCallbackInfo
    (                                   IntPtr            nModel,
                                        int            nQuery,
                                    ref int            nvValue );

    [DllImport(LINDO_DLL,
    EntryPoint="LSgetMIPCallbackInfo")]
    public static extern int LSgetMIPCallbackInfo
    (                                   IntPtr            nModel,
                                        int            nQuery,
                                    ref double         nvValue );


 /* function evaluation routines for NLP solvers */

    [DllImport(LINDO_DLL,
    EntryPoint="LSsetUsercalc")]
    public static extern int LSsetUsercalc
    (                                   IntPtr            nModel,
                                        typUsercalc    nfFunc,
                                        IntPtr         nvCbData );
                                        
    [DllImport(LINDO_DLL,
    EntryPoint="LSsetUsercalc")]
    public static extern int LSsetUsercalc
    (                                   IntPtr            nModel,
                                        typUsercalc    nfFunc,
     [MarshalAs(UnmanagedType.AsAny)]   object         nvCbData );
 

    [DllImport(LINDO_DLL,
    EntryPoint="LSsetFuncalc")]
    public static extern int LSsetFuncalc
    (                                   IntPtr            nModel,
                                        typFuncalc     nfFunc,
                                        IntPtr         nvCbData );
                                        
    [DllImport(LINDO_DLL,
    EntryPoint="LSsetFuncalc")]
    public static extern int LSsetFuncalc
    (                                   IntPtr            nModel,
                                        typFuncalc     nfFunc,
     [MarshalAs(UnmanagedType.AsAny)]   object         nvCbData );
 


    [DllImport(LINDO_DLL,
    EntryPoint="LSsetGradcalc")]
    public static extern int LSsetGradcalc
    (                                   IntPtr            nModel,
                                        typGradcalc    nfGrad_func,
                                        IntPtr         nvCbData ,
                                        int            nLenUseGrad,
                                    ref int            nUseGrad );                                    
                                    
    [DllImport(LINDO_DLL,
    EntryPoint="LSsetGradcalc")]
    public static extern int LSsetGradcalc
    (                                   IntPtr            nModel,
                                        typGradcalc    nfGrad_func,
     [MarshalAs(UnmanagedType.AsAny)]   object         nvCbData,
                                        int            nLenUseGrad,
                                    ref int            nUseGrad );                                    

/**********************************************************************
 *  Memory Related Routines (7)                                       *
 **********************************************************************/


      [DllImport(LINDO_DLL,
      EntryPoint="LSfreeSolverMemory")]
      public static extern int LSfreeSolverMemory 
      (                                                 IntPtr             pModel         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSfreeHashMemory")]
      public static extern int LSfreeHashMemory 
      (                                                 IntPtr             pModel         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSfreeSolutionMemory")]
      public static extern int LSfreeSolutionMemory 
      (                                                 IntPtr             pModel         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSfreeMIPSolutionMemory")]
      public static extern int LSfreeMIPSolutionMemory 
      (                                                 IntPtr             pModel         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSfreeGOPSolutionMemory")]
      public static extern int LSfreeGOPSolutionMemory 
      (                                                 IntPtr             pModel         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSsetProbAllocSizes")]
      public static extern int LSsetProbAllocSizes 
      (                                               IntPtr             pModel         ,
                                                         int       n_vars_alloc         ,
                                                         int       n_cons_alloc         ,
                                                         int         n_QC_alloc         ,
                                                         int       n_Annz_alloc         ,
                                                         int       n_Qnnz_alloc         ,
                                                         int       n_NLPnnz_alloc         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSaddEmptySpacesAcolumns")]
      public static extern int LSaddEmptySpacesAcolumns 
      (                                               IntPtr             pModel         ,
                                            ref          int             colnnz         );


      [DllImport(LINDO_DLL,
      EntryPoint="LSaddEmptySpacesNLPAcolumns")]
      public static extern int LSaddEmptySpacesNLPAcolumns 
      (                                               IntPtr             pModel         ,
                                            ref          int             colnnz         );

       /* Deprecated,  use LSgetInfo() */


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetLicenseInfo")]
      public static extern int LSgetLicenseInfo 
      (                                               IntPtr             pModel         ,
                                            ref          int          pnMaxcons         ,
                                            ref          int          pnMaxvars         ,
                                            ref          int       pnMaxintvars         ,
                                            ref          int        pnReserved1         ,
                                            ref          int        pnDaystoexp         ,
                                            ref          int       pnDaystotrialexp         ,
                                            ref          int       pnNlpAllowed         ,
                                            ref          int            pnUsers         ,
                                            ref          int       pnBarAllowed         ,
                                            ref          int          pnRuntime         ,
                                            ref          int       pnEdulicense         ,
                                                StringBuilder           pachText         );

       /* Deprecated,  use LSgetInfo() */


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetDimensions")]
      public static extern int LSgetDimensions 
      (                                               IntPtr             pModel         ,
                                            ref          int             pnVars         ,
                                            ref          int             pnCons         ,
                                            ref          int            pnCones         ,
                                            ref          int             pnAnnz         ,
                                            ref          int            pnQCnnz         ,
                                            ref          int          pnConennz         ,
                                            ref          int           pnNLPnnz         ,
                                            ref          int        pnNLPobjnnz         ,
                                            ref          int       pnVarNamelen         ,
                                            ref          int       pnConNamelen         ,
                                            ref          int       pnConeNamelen         );

       /* Deprecated, use LSsolveMIP() */


      [DllImport(LINDO_DLL,
      EntryPoint="LSbnbSolve")]
      public static extern int LSbnbSolve 
      (                                               IntPtr             pModel         ,
                                                      string           pszFname         );

       /* Deprecated,  use LSgetInfo() */


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetDualMIPsolution")]
      public static extern int LSgetDualMIPsolution 
      (                                               IntPtr             pModel         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []    padPrimal         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []      padDual         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []  padRedcosts         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []   panCstatus         ,
             [MarshalAs(UnmanagedType.LPArray)]          int    []   panRstatus         );

      /* Deprecated,  use LSgetInfo() */


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetMIPSolutionStatus")]
      public static extern int LSgetMIPSolutionStatus 
      (                                               IntPtr             pModel         ,
                                            ref          int           pnStatus         );

      /* Deprecated,  use LSgetInfo() */


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetSolutionStatus")]
      public static extern int LSgetSolutionStatus 
      (                                               IntPtr             pModel         ,
                                            ref          int            nStatus         );

      /* Deprecated,  use LSgetInfo() */


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetObjective")]
      public static extern int LSgetObjective 
      (                                               IntPtr             pModel         ,
                                            ref       double           pdObjval         );

      /* Deprecated,  use LSgetInfo() */


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetSolutionInfo")]
      public static extern int LSgetSolutionInfo 
      (                                               IntPtr             pModel         ,
                                            ref          int           pnMethod         ,
                                            ref          int          pnElapsed         ,
                                            ref          int          pnSpxiter         ,
                                            ref          int          pnBariter         ,
                                            ref          int          pnNlpiter         ,
                                            ref          int       pnPrimStatus         ,
                                            ref          int       pnDualStatus         ,
                                            ref          int        pnBasStatus         ,
                                            ref       double          pdPobjval         ,
                                            ref       double          pdDobjval         ,
                                            ref       double          pdPinfeas         ,
                                            ref       double          pdDinfeas         );

      /* Deprecated,  use LSgetInfo() */


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetMIPSolution")]
      public static extern int LSgetMIPSolution 
      (                                               IntPtr             pModel         ,
                                            ref       double          pdPobjval         ,
             [MarshalAs(UnmanagedType.LPArray)]       double    []    padPrimal         );

      /* Deprecated,  use LSgetInfo() */


      [DllImport(LINDO_DLL,
      EntryPoint="LSgetCurrentMIPSolutionInfo")]
      public static extern int LSgetCurrentMIPSolutionInfo 
      (                                               IntPtr             pModel         ,
                                            ref          int        pnMIPstatus         ,
                                            ref       double        pdMIPobjval         ,
                                            ref       double        pdBestbound         ,
                                            ref          int          pnSpxiter         ,
                                            ref          int          pnBariter         ,
                                            ref          int          pnNlpiter         ,
                                            ref          int            pnLPcnt         ,
                                            ref          int        pnBranchcnt         ,
                                            ref          int        pnActivecnt         ,
                                            ref          int        pnCons_prep         ,
                                            ref          int        pnVars_prep         ,
                                            ref          int        pnAnnz_prep         ,
                                            ref          int         pnInt_prep         ,
                                            ref          int       pnCut_contra         ,
                                            ref          int          pnCut_obj         ,
                                            ref          int          pnCut_gub         ,
                                            ref          int         pnCut_lift         ,
                                            ref          int         pnCut_flow         ,
                                            ref          int       pnCut_gomory         ,
                                            ref          int          pnCut_gcd         ,
                                            ref          int       pnCut_clique         ,
                                            ref          int       pnCut_disagg         ,
                                            ref          int       pnCut_planloc         ,
                                            ref          int       pnCut_latice         ,
                                            ref          int         pnCut_coef         );

}
