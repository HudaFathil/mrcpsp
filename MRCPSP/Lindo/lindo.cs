using System;

namespace MRCPSP.Lindo
{
    /*********************************************************************
     **
     **    LINDO API Version 4.1
     **    Copyright (c) 2000-2006
     **
     **    LINDO Systems, Inc.            312.988.7422
     **    1415 North Dayton St.          info@lindo.com
     **    Chicago, IL 60622              http://www.lindo.com
     **
     **    @file     lindo.cs  (C#.NET header)
     **    @modified on 03-15-2006
     **
     *********************************************************************/



    using System;
    using System.Text;
    using System.Runtime.InteropServices;

    public class lindo
    {

        /*********************************************************************
         *                        Constant Definitions                       *
         *********************************************************************/

        public static int LS_MIN = +1;
        public static int LS_MAX = -1;

        public static double LS_INFINITY = 1.0E+30;

        public static int LS_BASTYPE_BAS = 0;
        public static int LS_BASTYPE_ATLO = -1;
        public static int LS_BASTYPE_ATUP = -2;
        public static int LS_BASTYPE_FNUL = -3;
        public static int LS_BASTYPE_SBAS = -4;

        public static int LS_UNFORMATTED_MPS = 0;
        public static int LS_FORMATTED_MPS = 1;
        public static int LS_UNFORMATTED_MPS_COMP = 2;
        public static int LS_FORMATTED_MPS_COMP = 3;

        public static int LS_SOLUTION_OPT = 0;
        public static int LS_SOLUTION_MIP = 1;
        public static int LS_SOLUTION_OPT_IPM = 2;
        public static int LS_SOLUTION_OPT_OLD = 3;
        public static int LS_SOLUTION_MIP_OLD = 4;

        public static int LS_INT_PARAMETER_TYPE = 4;
        public static int LS_DOUBLE_PARAMETER_TYPE = 8;

        public static int LS_MAX_ERROR_MESSAGE_LENGTH = 256;

        /*********************************************************************
         *                      Macro Type Definitions                       *
         *********************************************************************/

        /* Solution or model status (1-20) */
        public static int LS_STATUS_OPTIMAL = 1;
        public static int LS_STATUS_BASIC_OPTIMAL = 2;
        public static int LS_STATUS_INFEASIBLE = 3;
        public static int LS_STATUS_UNBOUNDED = 4;
        public static int LS_STATUS_FEASIBLE = 5;
        public static int LS_STATUS_INFORUNB = 6;
        public static int LS_STATUS_NEAR_OPTIMAL = 7;
        public static int LS_STATUS_LOCAL_OPTIMAL = 8;
        public static int LS_STATUS_LOCAL_INFEASIBLE = 9;
        public static int LS_STATUS_CUTOFF = 10;
        public static int LS_STATUS_NUMERICAL_ERROR = 11;
        public static int LS_STATUS_UNKNOWN = 12;
        public static int LS_STATUS_UNLOADED = 13;
        public static int LS_STATUS_LOADED = 14;


        /* Parameter codes (21-999) */

        /* General solver parameters (21-199) */
        public static int LS_IPARAM_OBJSENSE = 22;
        public static int LS_DPARAM_CALLBACKFREQ = 23;
        public static int LS_DPARAM_OBJPRINTMUL = 24;
        public static int LS_IPARAM_CHECK_FOR_ERRORS = 25;
        public static int LS_IPARAM_ALLOW_CNTRLBREAK = 26;
        public static int LS_IPARAM_DECOMPOSITION_TYPE = 27;
        public static int LS_IPARAM_SPLEX_PREP = 28;
        public static int LS_IPARAM_SPLEX_SCALE = 29;
        public static int LS_IPARAM_SPLEX_ITRLMT = 30;
        public static int LS_IPARAM_SPLEX_PPRICING = 31;
        public static int LS_IPARAM_SPLEX_REFACFRQ = 32;
        public static int LS_IPARAM_BARRIER_SOLVER = 33;
        public static int LS_IPARAM_PROB_TO_SOLVE = 34;
        public static int LS_IPARAM_LP_PRINTLEVEL = 35;
        public static int LS_IPARAM_MPS_OBJ_WRITESTYLE = 36;
        public static int LS_IPARAM_SPLEX_DPRICING = 37;
        public static int LS_IPARAM_SOL_REPORT_STYLE = 38;
        public static int LS_IPARAM_INSTRUCT_LOADTYPE = 39;
        public static int LS_IPARAM_SPLEX_DUAL_PHASE = 40;
        public static int LS_IPARAM_LP_PRELEVEL = 41;

        public static int LS_IPARAM_SOLVER_ITER = 50;
        public static int LS_IPARAM_SOLVER_IUSOL = 51;
        public static int LS_IPARAM_SOLVER_TIMLMT = 52;
        public static int LS_DPARAM_SOLVER_CUTOFFVAL = 53;
        public static int LS_DPARAM_SOLVER_FEASTOL = 54;
        public static int LS_IPARAM_SOLVER_RESTART = 55;
        public static int LS_IPARAM_SOLVER_IPMSOL = 56;
        public static int LS_DPARAM_SOLVER_OPTTOL = 57;
        public static int LS_IPARAM_SOLVER_USECUTOFFVAL = 58;
        public static int LS_IPARAM_SOLVER_PRE_ELIM_FILL = 59;

        /* Nonlinear programming (NLP) parameters (200-299) */
        public static int LS_IPARAM_NLP_SOLVE_AS_LP = 200;
        public static int LS_IPARAM_NLP_SOLVER = 201;
        public static int LS_IPARAM_NLP_SUBSOLVER = 202;
        public static int LS_IPARAM_NLP_PRINTLEVEL = 203;
        public static int LS_DPARAM_NLP_PSTEP_FINITEDIFF = 204;
        public static int LS_IPARAM_NLP_DERIV_DIFFTYPE = 205;
        public static int LS_DPARAM_NLP_FEASTOL = 206;
        public static int LS_DPARAM_NLP_REDGTOL = 207;
        public static int LS_IPARAM_NLP_USE_CRASH = 208;
        public static int LS_IPARAM_NLP_USE_STEEPEDGE = 209;
        public static int LS_IPARAM_NLP_USE_SLP = 210;
        public static int LS_IPARAM_NLP_USE_SELCONEVAL = 211;
        public static int LS_IPARAM_NLP_PRELEVEL = 212;
        public static int LS_IPARAM_NLP_ITRLMT = 213;
        public static int LS_IPARAM_NLP_LINEARZ = 214;
        public static int LS_IPARAM_NLP_LINEARITY = 215;
        public static int LS_IPARAM_NLP_STARTPOINT = 216;
        public static int LS_IPARAM_NLP_CONVEXRELAX = 217;
        public static int LS_IPARAM_NLP_CR_ALG_REFORM = 218;
        public static int LS_IPARAM_NLP_QUADCHK = 219;
        public static int LS_IPARAM_NLP_AUTODERIV = 220;
        public static int LS_IPARAM_NLP_MAXLOCALSEARCH = 221;
        public static int LS_IPARAM_NLP_CONVEX = 222;
        public static int LS_IPARAM_NLP_CONOPT_VER = 223;
        public static int LS_IPARAM_NLP_USE_LINDO_CRASH = 224;
        public static int LS_IPARAM_NLP_STALL_ITRLMT = 225;
        public static int LS_IPARAM_NLP_AUTOHESS = 226;

        /* Mixed integer programming (MIP) parameters (300-399) */
        public static int LS_IPARAM_MIP_TIMLIM = 300;
        public static int LS_IPARAM_MIP_AOPTTIMLIM = 301;
        public static int LS_IPARAM_MIP_LSOLTIMLIM = 302;
        public static int LS_IPARAM_MIP_PRELEVEL = 303;
        public static int LS_IPARAM_MIP_NODESELRULE = 304;
        public static int LS_DPARAM_MIP_INTTOL = 305;
        public static int LS_DPARAM_MIP_RELINTTOL = 306;
        public static int LS_DPARAM_MIP_RELOPTTOL = 307;
        public static int LS_DPARAM_MIP_PEROPTTOL = 308;
        public static int LS_IPARAM_MIP_MAXCUTPASS_TOP = 309;
        public static int LS_IPARAM_MIP_MAXCUTPASS_TREE = 310;
        public static int LS_DPARAM_MIP_ADDCUTPER = 311;
        public static int LS_DPARAM_MIP_ADDCUTPER_TREE = 312;
        public static int LS_IPARAM_MIP_MAXNONIMP_CUTPASS = 313;
        public static int LS_IPARAM_MIP_CUTLEVEL_TOP = 314;
        public static int LS_IPARAM_MIP_CUTLEVEL_TREE = 315;
        public static int LS_IPARAM_MIP_CUTTIMLIM = 316;
        public static int LS_IPARAM_MIP_CUTDEPTH = 317;
        public static int LS_IPARAM_MIP_CUTFREQ = 318;
        public static int LS_IPARAM_MIP_HEULEVEL = 319;
        public static int LS_IPARAM_MIP_PRINTLEVEL = 320;
        public static int LS_IPARAM_MIP_PREPRINTLEVEL = 321;
        public static int LS_DPARAM_MIP_CUTOFFOBJ = 322;
        public static int LS_IPARAM_MIP_USECUTOFFOBJ = 323;
        public static int LS_IPARAM_MIP_STRONGBRANCHLEVEL = 324;
        public static int LS_IPARAM_MIP_TREEREORDERLEVEL = 325;
        public static int LS_IPARAM_MIP_BRANCHDIR = 326;
        public static int LS_IPARAM_MIP_TOPOPT = 327;
        public static int LS_IPARAM_MIP_REOPT = 328;
        public static int LS_IPARAM_MIP_SOLVERTYPE = 329;
        public static int LS_IPARAM_MIP_KEEPINMEM = 330;
        public static int LS_IPARAM_MIP_BRANCHRULE = 331;
        public static int LS_DPARAM_MIP_REDCOSTFIX_CUTOFF = 332;
        public static int LS_DPARAM_MIP_ADDCUTOBJTOL = 333;
        public static int LS_IPARAM_MIP_HEUMINTIMLIM = 334;
        public static int LS_IPARAM_MIP_BRANCH_PRIO = 335;
        public static int LS_IPARAM_MIP_SCALING_BOUND = 336;
        public static int LS_DPARAM_MIP_PSEUDOCOST_WEIGT = 337;
        public static int LS_DPARAM_MIP_LBIGM = 338;
        public static int LS_DPARAM_MIP_DELTA = 339;
        public static int LS_IPARAM_MIP_DUAL_SOLUTION = 340;
        public static int LS_IPARAM_MIP_BRANCH_LIMIT = 341;
        public static int LS_IPARAM_MIP_ITRLIM = 342;
        public static int LS_IPARAM_MIP_AGGCUTLIM_TOP = 343;
        public static int LS_IPARAM_MIP_AGGCUTLIM_TREE = 344;
        public static int LS_IPARAM_MIP_SWITCHFAC_SIM_IPM = 345;
        public static int LS_IPARAM_MIP_ANODES_SWITCH_DF = 346;
        public static int LS_DPARAM_MIP_ABSOPTTOL = 347;
        public static int LS_DPARAM_MIP_MINABSOBJSTEP = 348;
        public static int LS_IPARAM_MIP_PSEUDOCOST_RULE = 349;
        public static int LS_IPARAM_MIP_USE_ENUM_HEU = 350;
        public static int LS_IPARAM_MIP_PRELEVEL_TREE = 351;
        public static int LS_DPARAM_MIP_REDCOSTFIX_CUTOFF_TREE = 352;
        public static int LS_IPARAM_MIP_USE_INT_ZERO_TOL = 353;
        public static int LS_IPARAM_MIP_USE_CUTS_HEU = 354;
        public static int LS_DPARAM_MIP_BIGM_FOR_INTTOL = 355;
        public static int LS_IPARAM_MIP_STRONGBRANCHDONUM = 366;
        public static int LS_IPARAM_MIP_MAKECUT_INACTIVE_COUNT = 367;
        public static int LS_IPARAM_MIP_PRE_ELIM_FILL = 368;

        /* Global optimization (GOP) parameters (400-499) */
        public static int LS_DPARAM_GOP_OPTTOL = 400;
        public static int LS_DPARAM_GOP_FLTTOL = 401;
        public static int LS_DPARAM_GOP_BOXTOL = 402;
        public static int LS_DPARAM_GOP_WIDTOL = 403;
        public static int LS_DPARAM_GOP_DELTATOL = 404;
        public static int LS_DPARAM_GOP_BNDLIM = 405;
        public static int LS_IPARAM_GOP_TIMLIM = 406;
        public static int LS_IPARAM_GOP_OPTCHKMD = 407;
        public static int LS_IPARAM_GOP_BRANCHMD = 408;
        public static int LS_IPARAM_GOP_MAXWIDMD = 409;
        public static int LS_IPARAM_GOP_PRELEVEL = 410;
        public static int LS_IPARAM_GOP_POSTLEVEL = 411;
        public static int LS_IPARAM_GOP_BBSRCHMD = 412;
        public static int LS_IPARAM_GOP_DECOMPPTMD = 413;
        public static int LS_IPARAM_GOP_ALGREFORMMD = 414;
        public static int LS_IPARAM_GOP_RELBRNDMD = 415;
        public static int LS_IPARAM_GOP_PRINTLEVEL = 416;
        public static int LS_IPARAM_GOP_USEBNDLIM = 417;
        public static int LS_IPARAM_GOP_BRANCH_LIMIT = 418;
        public static int LS_IPARAM_GOP_CORELEVEL = 419;
        public static int LS_IPARAM_GOP_OPT_MODE = 420;
        public static int LS_IPARAM_GOP_HEU_MODE = 421;
        public static int LS_IPARAM_GOP_SUBOUT_MODE = 422;
        public static int LS_IPARAM_GOP_USE_NLPSOLVE = 423;
        public static int LS_IPARAM_GOP_LSOLBRANLIM = 424;
        public static int LS_IPARAM_GOP_LPSOPT = 425;

        /* License information parameters (500-549) */
        public static int LS_IPARAM_LIC_CONSTRAINTS = 500;
        public static int LS_IPARAM_LIC_VARIABLES = 501;
        public static int LS_IPARAM_LIC_INTEGERS = 502;
        public static int LS_IPARAM_LIC_NONLINEARVARS = 503;
        public static int LS_IPARAM_LIC_GOP_INTEGERS = 504;
        public static int LS_IPARAM_LIC_GOP_NONLINEARVARS = 505;
        public static int LS_IPARAM_LIC_DAYSTOEXP = 506;
        public static int LS_IPARAM_LIC_DAYSTOTRIALEXP = 507;
        public static int LS_IPARAM_LIC_NONLINEAR = 508;
        public static int LS_IPARAM_LIC_EDUCATIONAL = 509;
        public static int LS_IPARAM_LIC_RUNTIME = 510;
        public static int LS_IPARAM_LIC_NUMUSERS = 511;
        public static int LS_IPARAM_LIC_BARRIER = 512;
        public static int LS_IPARAM_LIC_GLOBAL = 513;
        public static int LS_IPARAM_LIC_PLATFORM = 514;

        /* Model analysis parameters (550-600) */
        public static int LS_IPARAM_IIS_ANALYZE_LEVEL = 550;
        public static int LS_IPARAM_IUS_ANALYZE_LEVEL = 551;
        public static int LS_IPARAM_IIS_TOPOPT = 552;
        public static int LS_IPARAM_IIS_REOPT = 553;
        public static int LS_IPARAM_IIS_USE_SFILTER = 554;
        public static int LS_IPARAM_IIS_PRINT_LEVEL = 555;
        public static int LS_IPARAM_IIS_INFEAS_NORM = 556;
        public static int LS_IPARAM_IIS_ITER_LIMIT = 557;
        public static int LS_IPARAM_IIS_TIME_LIMIT = 558;
        public static int LS_IPARAM_IIS_METHOD = 559;
        public static int LS_IPARAM_IIS_USE_EFILTER = 560;
        public static int LS_IPARAM_IIS_USE_GOP = 561;

        /* Last card for parameters */
        public static int LS_IPARAM_VER_NUMBER = 999;


        /* Math operator codes (1000-1500) */
        public static int EP_NO_OP = 0000;
        public static int EP_PLUS = 1001;
        public static int EP_MINUS = 1002;
        public static int EP_MULTIPLY = 1003;
        public static int EP_DIVIDE = 1004;
        public static int EP_POWER = 1005;
        public static int EP_EQUAL = 1006;
        public static int EP_NOT_EQUAL = 1007;
        public static int EP_LTOREQ = 1008;
        public static int EP_GTOREQ = 1009;
        public static int EP_LTHAN = 1010;
        public static int EP_GTHAN = 1011;
        public static int EP_AND = 1012;
        public static int EP_OR = 1013;
        public static int EP_NOT = 1014;
        public static int EP_PERCENT = 1015;
        public static int EP_POSATE = 1016;
        public static int EP_NEGATE = 1017;
        public static int EP_ABS = 1018;
        public static int EP_SQRT = 1019;
        public static int EP_LOG = 1020;
        public static int EP_LN = 1021;
        public static int EP_PI = 1022;
        public static int EP_SIN = 1023;
        public static int EP_COS = 1024;
        public static int EP_TAN = 1025;
        public static int EP_ATAN2 = 1026;
        public static int EP_ATAN = 1027;
        public static int EP_ASIN = 1028;
        public static int EP_ACOS = 1029;
        public static int EP_EXP = 1030;
        public static int EP_MOD = 1031;
        public static int LSE = 1032;
        public static int EP_TRUE = 1033;
        public static int EP_IF = 1034;
        public static int EP_PSN = 1035;
        public static int EP_PSL = 1036;
        public static int EP_LGM = 1037;
        public static int EP_SIGN = 1038;
        public static int EP_FLOOR = 1039;
        public static int EP_FPA = 1040;
        public static int EP_FPL = 1041;
        public static int EP_PEL = 1042;
        public static int EP_PEB = 1043;
        public static int EP_PPS = 1044;
        public static int EP_PPL = 1045;
        public static int EP_PTD = 1046;
        public static int EP_PCX = 1047;
        public static int EP_WRAP = 1048;
        public static int EP_PBN = 1049;
        public static int EP_PFS = 1050;
        public static int EP_PFD = 1051;
        public static int EP_PHG = 1052;
        public static int EP_RAND = 1053;
        public static int EP_USER = 1054;
        public static int EP_SUM = 1055;
        public static int EP_AVG = 1056;
        public static int EP_MIN = 1057;
        public static int EP_MAX = 1058;
        public static int EP_NPV = 1059;
        public static int EP_VAND = 1060;
        public static int EP_VOR = 1061;
        public static int EP_PUSH_NUM = 1062;
        public static int EP_PUSH_VAR = 1063;
        public static int EP_NORMDENS = 1064;
        public static int EP_NORMINV = 1065;
        public static int EP_TRIAINV = 1066;
        public static int EP_EXPOINV = 1067;
        public static int EP_UNIFINV = 1068;
        public static int EP_MULTINV = 1069;
        public static int EP_USRCOD = 1070;
        public static int EP_SUMPROD = 1071;
        public static int EP_SUMIF = 1072;
        public static int EP_VLOOKUP = 1073;
        public static int EP_VPUSH_NUM = 1074;
        public static int EP_VPUSH_VAR = 1075;
        public static int EP_VMULT = 1076;
        public static int EP_SQR = 1077;
        public static int EP_SINH = 1078;
        public static int EP_COSH = 1079;
        public static int EP_TANH = 1080;
        public static int EP_ASINH = 1081;
        public static int EP_ACOSH = 1082;
        public static int EP_ATANH = 1083;
        public static int EP_LOGB = 1084;
        public static int EP_LOGX = 1085;
        public static int EP_LNX = 1086;
        public static int EP_TRUNC = 1087;
        public static int EP_NORMSINV = 1088;


        /* Model and solution information codes ( 1200-1399) */
        /* Model statistics (1200-1249)*/
        public static int LS_IINFO_NUM_CONS = 1200;
        public static int LS_IINFO_NUM_VARS = 1201;
        public static int LS_IINFO_NUM_NONZ = 1202;
        public static int LS_IINFO_NUM_BIN = 1203;
        public static int LS_IINFO_NUM_INT = 1204;
        public static int LS_IINFO_NUM_CONT = 1205;
        public static int LS_IINFO_NUM_QC_NONZ = 1206;
        public static int LS_IINFO_NUM_NLP_NONZ = 1207;
        public static int LS_IINFO_NUM_NLPOBJ_NONZ = 1208;
        public static int LS_IINFO_NUM_RDCONS = 1209;
        public static int LS_IINFO_NUM_RDVARS = 1210;
        public static int LS_IINFO_NUM_RDNONZ = 1211;
        public static int LS_IINFO_NUM_RDINT = 1212;
        public static int LS_IINFO_LEN_VARNAMES = 1213;
        public static int LS_IINFO_LEN_CONNAMES = 1214;
        public static int LS_IINFO_NUM_NLP_CONS = 1215;
        public static int LS_IINFO_NUM_NLP_VARS = 1216;
        public static int LS_IINFO_NUM_SUF_ROWS = 1217;
        public static int LS_IINFO_NUM_IIS_ROWS = 1218;
        public static int LS_IINFO_NUM_SUF_BNDS = 1219;
        public static int LS_IINFO_NUM_IIS_BNDS = 1220;
        public static int LS_IINFO_NUM_SUF_COLS = 1221;
        public static int LS_IINFO_NUM_IUS_COLS = 1222;
        public static int LS_IINFO_NUM_CONES = 1223;
        public static int LS_IINFO_NUM_CONE_NONZ = 1224;
        public static int LS_IINFO_LEN_CONENAMES = 1225;
        public static int LS_DINFO_INST_VAL_MIN_COEF = 1226;
        public static int LS_IINFO_INST_VARNDX_MIN_COEF = 1227;
        public static int LS_IINFO_INST_CONNDX_MIN_COEF = 1228;
        public static int LS_DINFO_INST_VAL_MAX_COEF = 1229;
        public static int LS_IINFO_INST_VARNDX_MAX_COEF = 1230;
        public static int LS_IINFO_INST_CONNDX_MAX_COEF = 1231;
        public static int LS_IINFO_NUM_CALL_FUN = 1232;
        public static int LS_IINFO_NUM_CALL_DEV = 1233;
        public static int LS_IINFO_NUM_CALL_HES = 1234;

        /* LP and NLP related info (1250-1299)*/
        public static int LS_IINFO_METHOD = 1250;
        public static int LS_DINFO_POBJ = 1251;
        public static int LS_DINFO_DOBJ = 1252;
        public static int LS_DINFO_PINFEAS = 1253;
        public static int LS_DINFO_DINFEAS = 1254;
        public static int LS_IINFO_MODEL_STATUS = 1255;
        public static int LS_IINFO_PRIMAL_STATUS = 1256;
        public static int LS_IINFO_DUAL_STATUS = 1257;
        public static int LS_IINFO_BASIC_STATUS = 1258;
        public static int LS_IINFO_BAR_ITER = 1259;
        public static int LS_IINFO_SIM_ITER = 1260;
        public static int LS_IINFO_NLP_ITER = 1261;
        public static int LS_IINFO_ELAPSED_TIME = 1262;
        public static int LS_DINFO_MSW_POBJ = 1263;
        public static int LS_IINFO_MSW_PASS = 1264;
        public static int LS_IINFO_MSW_NSOL = 1265;
        public static int LS_IINFO_IPM_STATUS = 1266;
        public static int LS_DINFO_IPM_POBJ = 1267;
        public static int LS_DINFO_IPM_DOBJ = 1268;
        public static int LS_DINFO_IPM_PINFEAS = 1269;
        public static int LS_DINFO_IPM_DINFEAS = 1270;

        /* MIP and MINLP related info (1300-1349) */
        public static int LS_IINFO_MIP_STATUS = 1300;
        public static int LS_DINFO_MIP_OBJ = 1301;
        public static int LS_DINFO_MIP_BESTBOUND = 1302;
        public static int LS_IINFO_MIP_SIM_ITER = 1303;
        public static int LS_IINFO_MIP_BAR_ITER = 1304;
        public static int LS_IINFO_MIP_NLP_ITER = 1305;
        public static int LS_IINFO_MIP_BRANCHCOUNT = 1306;
        public static int LS_IINFO_MIP_NEWIPSOL = 1307;
        public static int LS_IINFO_MIP_LPCOUNT = 1308;
        public static int LS_IINFO_MIP_ACTIVENODES = 1309;
        public static int LS_IINFO_MIP_LTYPE = 1310;
        public static int LS_IINFO_MIP_AOPTTIMETOSTOP = 1311;
        public static int LS_IINFO_MIP_NUM_TOTAL_CUTS = 1312;
        public static int LS_IINFO_MIP_GUB_COVER_CUTS = 1313;
        public static int LS_IINFO_MIP_FLOW_COVER_CUTS = 1314;
        public static int LS_IINFO_MIP_LIFT_CUTS = 1315;
        public static int LS_IINFO_MIP_PLAN_LOC_CUTS = 1316;
        public static int LS_IINFO_MIP_DISAGG_CUTS = 1317;
        public static int LS_IINFO_MIP_KNAPSUR_COVER_CUTS = 1318;
        public static int LS_IINFO_MIP_LATTICE_CUTS = 1319;
        public static int LS_IINFO_MIP_GOMORY_CUTS = 1320;
        public static int LS_IINFO_MIP_COEF_REDC_CUTS = 1321;
        public static int LS_IINFO_MIP_GCD_CUTS = 1322;
        public static int LS_IINFO_MIP_OBJ_CUT = 1323;
        public static int LS_IINFO_MIP_BASIS_CUTS = 1324;
        public static int LS_IINFO_MIP_CARDGUB_CUTS = 1325;
        public static int LS_IINFO_MIP_CLIQUE_CUTS = 1326;
        public static int LS_IINFO_MIP_CONTRA_CUTS = 1327;
        public static int LS_IINFO_MIP_GUB_CONS = 1328;
        public static int LS_IINFO_MIP_GLB_CONS = 1329;
        public static int LS_IINFO_MIP_PLANTLOC_CONS = 1330;
        public static int LS_IINFO_MIP_DISAGG_CONS = 1331;
        public static int LS_IINFO_MIP_SB_CONS = 1332;
        public static int LS_IINFO_MIP_IKNAP_CONS = 1333;
        public static int LS_IINFO_MIP_KNAP_CONS = 1334;
        public static int LS_IINFO_MIP_NLP_CONS = 1335;
        public static int LS_IINFO_MIP_CONT_CONS = 1336;
        public static int LS_DINFO_MIP_TOT_TIME = 1347;
        public static int LS_DINFO_MIP_OPT_TIME = 1348;
        public static int LS_DINFO_MIP_HEU_TIME = 1349;
        public static int LS_IINFO_MIP_SOLSTATUS_LAST_BRANCH = 1350;
        public static int LS_DINFO_MIP_SOLOBJVAL_LAST_BRANCH = 1351;
        public static int LS_IINFO_MIP_HEU_LEVEL = 1352;
        public static int LS_DINFO_MIP_PFEAS = 1353;
        public static int LS_DINFO_MIP_INTPFEAS = 1354;

        /* GOP related info (1650-1799) */
        public static int LS_DINFO_GOP_OBJ = 1650;
        public static int LS_IINFO_GOP_SIM_ITER = 1651;
        public static int LS_IINFO_GOP_BAR_ITER = 1652;
        public static int LS_IINFO_GOP_NLP_ITER = 1653;
        public static int LS_DINFO_GOP_BESTBOUND = 1654;
        public static int LS_IINFO_GOP_STATUS = 1655;
        public static int LS_IINFO_GOP_LPCOUNT = 1656;
        public static int LS_IINFO_GOP_NLPCOUNT = 1657;
        public static int LS_IINFO_GOP_MIPCOUNT = 1658;
        public static int LS_IINFO_GOP_NEWSOL = 1659;
        public static int LS_IINFO_GOP_BOX = 1660;
        public static int LS_IINFO_GOP_BBITER = 1661;
        public static int LS_IINFO_GOP_SUBITER = 1662;
        public static int LS_IINFO_GOP_MIPBRANCH = 1663;
        public static int LS_IINFO_GOP_ACTIVEBOXES = 1664;
        public static int LS_IINFO_GOP_TOT_TIME = 1665;
        public static int LS_IINFO_GOP_MAXDEPTH = 1666;

        /* Progress info during callbacks */
        public static int LS_DINFO_SUB_OBJ = 1700;
        public static int LS_DINFO_SUB_PINF = 1701;
        public static int LS_DINFO_CUR_OBJ = 1702;
        public static int LS_IINFO_CUR_ITER = 1703;
        public static int LS_DINFO_CUR_BEST_BOUND = 1704;
        public static int LS_IINFO_CUR_STATUS = 1705;
        public static int LS_IINFO_CUR_LP_COUNT = 1706;
        public static int LS_IINFO_CUR_BRANCH_COUNT = 1707;
        public static int LS_IINFO_CUR_ACTIVE_COUNT = 1708;
        public static int LS_IINFO_CUR_NLP_COUNT = 1709;
        public static int LS_IINFO_CUR_MIP_COUNT = 1710;
        public static int LS_IINFO_CUR_CUT_COUNT = 1711;

        /* Model generation progress info (1800+)*/
        public static int LS_DINFO_GEN_PERCENT = 1800;
        public static int LS_IINFO_GEN_NONZ_TTL = 1801;
        public static int LS_IINFO_GEN_NONZ_NL = 1802;
        public static int LS_IINFO_GEN_ROW_NL = 1803;
        public static int LS_IINFO_GEN_VAR_NL = 1804;

        /* IIS-IUS info */
        public static int LS_IINFO_IIS_BAR_ITER = 1850;
        public static int LS_IINFO_IIS_SIM_ITER = 1851;
        public static int LS_IINFO_IIS_TOT_TIME = 1852;
        public static int LS_IINFO_IIS_ACT_NODE = 1853;
        public static int LS_IINFO_IIS_LPCOUNT = 1854;
        public static int LS_IINFO_IIS_NLPCOUNT = 1855;
        public static int LS_IINFO_IIS_MIPCOUNT = 1856;

        public static int LS_IINFO_IUS_BAR_ITER = 1860;
        public static int LS_IINFO_IUS_SIM_ITER = 1861;
        public static int LS_IINFO_IUS_TOT_TIME = 1862;
        public static int LS_IINFO_IUS_ACT_NODE = 1863;
        public static int LS_IINFO_IUS_LPCOUNT = 1864;
        public static int LS_IINFO_IUS_NLPCOUNT = 1865;
        public static int LS_IINFO_IUS_MIPCOUNT = 1866;

        /* Presolve info    */
        public static int LS_IINFO_PRE_NUM_RED = 1900;
        public static int LS_IINFO_PRE_TYPE_RED = 1901;
        public static int LS_IINFO_PRE_NUM_RDCONS = 1902;
        public static int LS_IINFO_PRE_NUM_RDVARS = 1903;
        public static int LS_IINFO_PRE_NUM_RDNONZ = 1904;
        public static int LS_IINFO_PRE_NUM_RDINT = 1905;

        /* Error info */
        public static int LS_IINFO_ERR_OPTIM = 1999;


        /* Error codes (2001-2299) */
        public static int LSERR_NO_ERROR = 0000;
        public static int LSERR_OUT_OF_MEMORY = 2001;
        public static int LSERR_CANNOT_OPEN_FILE = 2002;
        public static int LSERR_BAD_MPS_FILE = 2003;
        public static int LSERR_BAD_CONSTRAINT_TYPE = 2004;
        public static int LSERR_BAD_MODEL = 2005;
        public static int LSERR_BAD_SOLVER_TYPE = 2006;
        public static int LSERR_BAD_OBJECTIVE_SENSE = 2007;
        public static int LSERR_BAD_MPI_FILE = 2008;
        public static int LSERR_INFO_NOT_AVAILABLE = 2009;
        public static int LSERR_ILLEGAL_NULL_POINTER = 2010;
        public static int LSERR_UNABLE_TO_SET_PARAM = 2011;
        public static int LSERR_INDEX_OUT_OF_RANGE = 2012;
        public static int LSERR_ERRMSG_FILE_NOT_FOUND = 2013;
        public static int LSERR_VARIABLE_NOT_FOUND = 2014;
        public static int LSERR_INTERNAL_ERROR = 2015;
        public static int LSERR_ITER_LIMIT = 2016;
        public static int LSERR_TIME_LIMIT = 2017;
        public static int LSERR_NOT_CONVEX = 2018;
        public static int LSERR_NUMERIC_INSTABILITY = 2019;
        public static int LSERR_STEP_TOO_SMALL = 2021;
        public static int LSERR_USER_INTERRUPT = 2023;
        public static int LSERR_PARAMETER_OUT_OF_RANGE = 2024;
        public static int LSERR_ERROR_IN_INPUT = 2025;
        public static int LSERR_TOO_SMALL_LICENSE = 2026;
        public static int LSERR_NO_VALID_LICENSE = 2027;
        public static int LSERR_NO_METHOD_LICENSE = 2028;
        public static int LSERR_NOT_SUPPORTED = 2029;
        public static int LSERR_MODEL_ALREADY_LOADED = 2030;
        public static int LSERR_MODEL_NOT_LOADED = 2031;
        public static int LSERR_INDEX_DUPLICATE = 2032;
        public static int LSERR_INSTRUCT_NOT_LOADED = 2033;
        public static int LSERR_OLD_LICENSE = 2034;
        public static int LSERR_NO_LICENSE_FILE = 2035;
        public static int LSERR_BAD_LICENSE_FILE = 2036;
        public static int LSERR_MIP_BRANCH_LIMIT = 2037;
        public static int LSERR_GOP_FUNC_NOT_SUPPORTED = 2038;
        public static int LSERR_GOP_BRANCH_LIMIT = 2039;
        public static int LSERR_BAD_DECOMPOSITION_TYPE = 2040;
        public static int LSERR_BAD_VARIABLE_TYPE = 2041;
        public static int LSERR_BASIS_BOUND_MISMATCH = 2042;
        public static int LSERR_BASIS_COL_STATUS = 2043;
        public static int LSERR_BASIS_INVALID = 2044;
        public static int LSERR_BASIS_ROW_STATUS = 2045;
        public static int LSERR_BLOCK_OF_BLOCK = 2046;
        public static int LSERR_BOUND_OUT_OF_RANGE = 2047;
        public static int LSERR_COL_BEGIN_INDEX = 2048;
        public static int LSERR_COL_INDEX_OUT_OF_RANGE = 2049;
        public static int LSERR_COL_NONZCOUNT = 2050;
        public static int LSERR_INVALID_ERRORCODE = 2051;
        public static int LSERR_ROW_INDEX_OUT_OF_RANGE = 2052;
        public static int LSERR_TOTAL_NONZCOUNT = 2053;
        public static int LSERR_MODEL_NOT_LINEAR = 2054;
        public static int LSERR_CHECKSUM = 2055;
        public static int LSERR_USER_FUNCTION_NOT_FOUND = 2056;
        public static int LSERR_TRUNCATED_NAME_DATA = 2057;
        public static int LSERR_LAST_ERROR = 2058;


        /* Callback locations */
        public static int LSLOC_PRIMAL = 0;
        public static int LSLOC_DUAL = 1;
        public static int LSLOC_BARRIER = 2;
        public static int LSLOC_CROSSOVER = 3;
        public static int LSLOC_CONOPT = 4;
        public static int LSLOC_MIP = 5;
        public static int LSLOC_LOCAL_OPT = 6;
        public static int LSLOC_GEN_START = 7;
        public static int LSLOC_GEN_PROCESSING = 8;
        public static int LSLOC_GEN_END = 9;
        public static int LSLOC_GOP = 10;
        public static int LSLOC_EXIT_SOLVER = 11;
        public static int LSLOC_PRESOLVE = 12;
        public static int LSLOC_MSW = 13;


        public static int LS_METHOD_FREE = 0;
        public static int LS_METHOD_PSIMPLEX = 1;
        public static int LS_METHOD_DSIMPLEX = 2;
        public static int LS_METHOD_BARRIER = 3;
        public static int LS_METHOD_NLP = 4;


        public static int LS_NMETHOD_FREE = 4;
        public static int LS_NMETHOD_CONOPT = 7;
        public static int LS_NMETHOD_MSW_GRG = 9;


        public static int LS_PROB_SOLVE_FREE = 0;
        public static int LS_PROB_SOLVE_PRIMAL = 1;
        public static int LS_PROB_SOLVE_DUAL = 2;
        public static int LS_BAR_METHOD_FREE = 4;
        public static int LS_BAR_METHOD_INTPNT = 5;
        public static int LS_BAR_METHOD_CONIC = 6;
        public static int LS_BAR_METHOD_QCONE = 7;

        public static int LSSOL_BASIC_PRIMAL = 11;
        public static int LSSOL_BASIC_DUAL = 12;
        public static int LSSOL_BASIC_SLACK = 13;
        public static int LSSOL_BASIC_REDCOST = 14;
        public static int LSSOL_INTERIOR_PRIMAL = 15;
        public static int LSSOL_INTERIOR_DUAL = 16;
        public static int LSSOL_INTERIOR_SLACK = 17;
        public static int LSSOL_INTERIOR_REDCOST = 18;


        public static int LS_LINK_BLOCKS_FREE = 0;
        public static int LS_LINK_BLOCKS_SELF = 1;
        public static int LS_LINK_BLOCKS_NONE = 2;
        public static int LS_LINK_BLOCKS_COLS = 3;
        public static int LS_LINK_BLOCKS_ROWS = 4;
        public static int LS_LINK_BLOCKS_BOTH = 5;


        /* Controls the way objective function and
         * objective sense are printed when writing
         * LS_MAX type problems in MPS format.
         */
        public static int LS_MPS_USE_MAX_NOTE = 0;
        public static int LS_MPS_USE_MAX_CARD = 1;
        public static int LS_MPS_USE_MAX_FLIP = 2;


        /* Finite differences methods */
        public static int LS_DERIV_FREE = 0;
        public static int LS_DERIV_FORWARD_DIFFERENCE = 1;
        public static int LS_DERIV_BACKWARD_DIFFERENCE = 2;
        public static int LS_DERIV_CENTER_DIFFERENCE = 3;


        /* MIP Sets
         *  SOS1: S={x_1,...,x_p}  only one x_j can be different from zero
         *  SOS2: S={x_1,...,x_p}  at most two x_j can be different from zero
         *                         and  when they are they have to be adjacent
         *  SOS3: S={x_1,...,x_p}  @sum(j: x_j      )  = 1;  x_j >=0,
         *  CARD: S={x_1,...,x_p}  @sum(j: sign(x_j)) <= k;  x_j >=0
         */
        public static int LS_MIP_SET_CARD = 4;
        public static int LS_MIP_SET_SOS1 = 1;
        public static int LS_MIP_SET_SOS2 = 2;
        public static int LS_MIP_SET_SOS3 = 3;


        /* Bit mask for cut generation levels. Use sums to
         * enable a collection of available cuts.
         */
        public static int LS_MIP_GUB_COVER_CUTS = 2;
        public static int LS_MIP_FLOW_COVER_CUTS = 4;
        public static int LS_MIP_LIFT_CUTS = 8;
        public static int LS_MIP_PLAN_LOC_CUTS = 16;
        public static int LS_MIP_DISAGG_CUTS = 32;
        public static int LS_MIP_KNAPSUR_COVER_CUTS = 64;
        public static int LS_MIP_LATTICE_CUTS = 128;
        public static int LS_MIP_GOMORY_CUTS = 256;
        public static int LS_MIP_COEF_REDC_CUTS = 512;
        public static int LS_MIP_GCD_CUTS = 1024;
        public static int LS_MIP_OBJ_CUT = 2048;
        public static int LS_MIP_BASIS_CUTS = 4096;
        public static int LS_MIP_CARDGUB_CUTS = 8192;
        public static int LS_MIP_DISJUN_CUTS = 16384;


        /* Bit masks for MIP preprocessing levels. Use sums
         * to enable a collection of available levels.
         */
        public static int LS_MIP_PREP_SPRE = 2;
        public static int LS_MIP_PREP_PROB = 4;
        public static int LS_MIP_PREP_COEF = 8;
        public static int LS_MIP_PREP_ELIM = 16;
        public static int LS_MIP_PREP_DUAL = 32;
        public static int LS_MIP_PREP_DBACK = 64;
        public static int LS_MIP_PREP_BINROWS = 128;
        public static int LS_MIP_PREP_AGGROWS = 256;
        public static int LS_MIP_PREP_COEF_LIFTING = 512;
        public static int LS_MIP_PREP_MAXPASS = 1024;


        /* Bit masks for solver preprocessing levels. Use sums
         * to enable a collection of available levels.
         */
        public static int LS_SOLVER_PREP_SPRE = 2;
        public static int LS_SOLVER_PREP_PFOR = 4;
        public static int LS_SOLVER_PREP_DFOR = 8;
        public static int LS_SOLVER_PREP_ELIM = 16;
        public static int LS_SOLVER_PREP_DCOL = 32;
        public static int LS_SOLVER_PREP_DROW = 64;
        public static int LS_SOLVER_PREP_MAXPASS = 1024;


        /* Bit masks for IIS & IUS analysis levels. Use sums to
         * enable a collection of available levels.
         */
        public static int LS_NECESSARY_ROWS = 1;
        public static int LS_NECESSARY_COLS = 2;
        public static int LS_SUFFICIENT_ROWS = 4;
        public static int LS_SUFFICIENT_COLS = 8;


        /* Infeasibility norms for IIS finder */
        public static int LS_IIS_NORM_FREE = 0;
        public static int LS_IIS_NORM_ONE = 1;
        public static int LS_IIS_NORM_INFINITY = 2;


        /* IIS methods */
        public static int LS_IIS_DEFAULT = 0;
        public static int LS_IIS_DEL_FILTER = 1;
        public static int LS_IIS_ADD_FILTER = 2;
        public static int LS_IIS_GBS_FILTER = 3;
        public static int LS_IIS_DFBS_FILTER = 4;
        public static int LS_IIS_FSC_FILTER = 5;
        public static int LS_IIS_ELS_FILTER = 6;


        /*********************************************************************
         *                   Conversion to version 1.x                       *
         *********************************************************************/

        /* old parameter names */
        public static int LSLOC_BANDB = LSLOC_MIP;
        public static int LS_IPARAM_PREP = LS_IPARAM_SPLEX_PREP;
        public static int LS_IPARAM_ITRLMT = LS_IPARAM_SPLEX_ITRLMT;
        public static int LS_IPARAM_PRICING = LS_IPARAM_SPLEX_PPRICING;
        public static int LS_IPARAM_SCALE = LS_IPARAM_SPLEX_SCALE;
        public static int LS_IPARAM_TIMLMT = LS_IPARAM_SOLVER_TIMLMT;
        public static int LS_DPARAM_CUTOFFVAL = LS_DPARAM_SOLVER_CUTOFFVAL;
        public static int LS_IPARAM_RESTART = LS_IPARAM_SOLVER_RESTART;
        public static int LS_DPARAM_FEASTOL = LS_DPARAM_SOLVER_FEASTOL;
        public static int LS_IPARAM_IUSOL = LS_IPARAM_SOLVER_IUSOL;
        public static int LS_IPARAM_ITER = LS_IPARAM_SOLVER_ITER;
        public static int LS_IPARAM_MIPTIMLIM = LS_IPARAM_MIP_TIMLIM;
        public static int LS_IPARAM_MIPAOPTTIMLIM = LS_IPARAM_MIP_AOPTTIMLIM;
        public static int LS_IPARAM_MIPPRELEVEL = LS_IPARAM_MIP_PRELEVEL;
        public static int LS_IPARAM_MIPNODESELRULE = LS_IPARAM_MIP_NODESELRULE;
        public static int LS_DPARAM_MIPINTTOL = LS_DPARAM_MIP_INTTOL;
        public static int LS_DPARAM_MIPRELINTTOL = LS_DPARAM_MIP_RELINTTOL;
        public static int LS_DPARAM_MIP_OPTTOL = LS_DPARAM_MIP_RELOPTTOL;
        public static int LS_DPARAM_MIPOPTTOL = LS_DPARAM_MIP_OPTTOL;
        public static int LS_DPARAM_MIPPEROPTTOL = LS_DPARAM_MIP_PEROPTTOL;
        public static int LS_IPARAM_MIPMAXCUTPASS = LS_IPARAM_MIP_MAXCUTPASS_TOP;
        public static int LS_DPARAM_MIPADDCUTPER = LS_DPARAM_MIP_ADDCUTPER;
        public static int LS_IPARAM_MIPCUTLEVEL = LS_IPARAM_MIP_CUTLEVEL_TOP;
        public static int LS_IPARAM_MIPHEULEVEL = LS_IPARAM_MIP_HEULEVEL;
        public static int LS_IPARAM_MIPPRINTLEVEL = LS_IPARAM_MIP_PRINTLEVEL;
        public static int LS_IPARAM_MIPPREPRINTLEVEL = LS_IPARAM_MIP_PREPRINTLEVEL;
        public static int LS_DPARAM_MIPCUTOFFOBJ = LS_DPARAM_MIP_CUTOFFOBJ;
        public static int LS_IPARAM_MIPSTRONGBRANCHLEVEL = LS_IPARAM_MIP_STRONGBRANCHLEVEL;
        public static int LS_IPARAM_MIPBRANCHDIR = LS_IPARAM_MIP_BRANCHDIR;
        public static int LS_IPARAM_MIPTOPOPT = LS_IPARAM_MIP_TOPOPT;
        public static int LS_IPARAM_MIPREOPT = LS_IPARAM_MIP_REOPT;
        public static int LS_IPARAM_MIPSOLVERTYPE = LS_IPARAM_MIP_SOLVERTYPE;
        public static int LS_IPARAM_MIPKEEPINMEM = LS_IPARAM_MIP_KEEPINMEM;
        public static int LS_DPARAM_MIP_REDCOSTFIXING_CUTOFF = LS_DPARAM_MIP_REDCOSTFIX_CUTOFF;
        public static int LS_IPARAM_NLPPRINTLEVEL = LS_IPARAM_NLP_PRINTLEVEL;
        public static int LS_IPARAM_LPPRINTLEVEL = LS_IPARAM_LP_PRINTLEVEL;
        public static int LS_IPARAM_NLPSOLVER = LS_IPARAM_NLP_SOLVER;
        public static int LS_IPARAM_MODEL_CONVEX_FLAG = LS_IPARAM_NLP_CONVEX;
        public static int LS_IPARAM_NLP_SOLVEASLP = LS_IPARAM_NLP_SOLVE_AS_LP;
        public static int LS_DINFO_MIPBESTBOUND = LS_DINFO_MIP_BESTBOUND;
        public static int LS_IINFO_MIPBRANCHCOUNT = LS_IINFO_MIP_BRANCHCOUNT;
        public static int LS_IINFO_MIPSTATUS = LS_IINFO_MIP_STATUS;
        public static int LS_IINFO_MIPNEWIPSOL = LS_IINFO_MIP_NEWIPSOL;
        public static int LS_IINFO_MIPLPCOUNT = LS_IINFO_MIP_LPCOUNT;
        public static int LS_IINFO_MIPACTIVENODES = LS_IINFO_MIP_ACTIVENODES;
        public static int LS_IINFO_MIPLTYPE = LS_IINFO_MIP_LTYPE;
        public static int LS_IINFO_MIPAOPTTIMETOSTOP = LS_IINFO_MIP_AOPTTIMETOSTOP;
        public static int LS_DINFO_MIPOBJ = LS_DINFO_MIP_OBJ;
        public static int LS_IPARAM_BARRIER_PROB_TO_SOLVE = LS_IPARAM_PROB_TO_SOLVE;
        public static int LS_IINFO_STATUS = LS_IINFO_PRIMAL_STATUS;
        public static int LS_GOPSOLSTAT_GLOBAL_OPTIMAL = LS_STATUS_OPTIMAL;
        public static int LS_GOPSOLSTAT_LOCAL_OPTIMAL = LS_STATUS_LOCAL_OPTIMAL;
        public static int LS_GOPSOLSTAT_INFEASIBLE = LS_STATUS_INFEASIBLE;
        public static int LS_GOPSOLSTAT_TOPUNBOUNDED = LS_STATUS_UNBOUNDED;
        public static int LS_GOPSOLSTAT_FEASIBLE = LS_STATUS_FEASIBLE;
        public static int LS_GOPSOLSTAT_UNKNOWN = LS_STATUS_UNKNOWN;
        public static int LS_GOPSOLSTAT_NUMERICAL_ERROR = LS_STATUS_NUMERICAL_ERROR;

        /* old operator names */
        public static int EP_EXT_AND = EP_VAND;
        public static int EP_EXT_OR = EP_VOR;
        public static int EP_MULTMULT = EP_VMULT;

        /*********************************************************************

        /*********************************************************************
         *                                                                   *
         *                        Function Prototypes                        *
         *                                                                   *
         *********************************************************************/

        /*********************************************************************
         * Structure Creation and Deletion Routines (4)                      *
         *********************************************************************/

        [DllImport("lindo4_1.dll",
             EntryPoint = "LScreateEnv")]
        public static extern int LScreateEnv
            (ref int nErrorcode,
            string szPassword);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LScreateModel")]
        public static extern int LScreateModel
            (int nEnv,
            ref int nErrorcode);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSdeleteEnv")]
        public static extern int LSdeleteEnv
            (ref int nEnv);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSdeleteModel")]
        public static extern int LSdeleteModel
            (ref int nModel);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSloadLicenseString")]
        public static extern int LSloadLicenseString
            (string szFname,
            StringBuilder achLicense);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetVersionInfo")]
        public static extern int LSgetVersionInfo
            (StringBuilder achVernum,
            StringBuilder achBuildDate);


        /**********************************************************************
         * Model I-O Routines (13)                                            *
         **********************************************************************/

        [DllImport("lindo4_1.dll",
             EntryPoint = "LSreadMPSFile")]
        public static extern int LSreadMPSFile
            (int nModel,
            string szFname,
            int nFormat);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSwriteMPSFile")]
        public static extern int LSwriteMPSFile
            (int nModel,
            string szFname,
            int nFormat);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSreadLINDOFile")]
        public static extern int LSreadLINDOFile
            (int nModel,
            string szFname);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSwriteLINDOFile")]
        public static extern int LSwriteLINDOFile
            (int nModel,
            string szFname);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSwriteLINGOFile")]
        public static extern int LSwriteLINGOFile
            (int nModel,
            string szFname);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSwriteDualMPSFile")]
        public static extern int LSwriteDualMPSFile
            (int nModel,
            string szFname,
            int nFormat,
            int nObjSense);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSwriteDualLINDOFile")]
        public static extern int LSwriteDualLINDOFile
            (int nModel,
            string szFname,
            int nObjSense);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSwriteSolution")]
        public static extern int LSwriteSolution
            (int nModel,
            string szFname);

        [DllImport("lindo4_1.dll",
             EntryPoint = "LSwriteSolutionOfType")]
        public static extern int LSwriteSolutionOfType
            (int nModel,
            string szFname,
            int nFormat);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSwriteIIS")]
        public static extern int LSwriteIIS
            (int nModel,
            string szFname);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSwriteIUS")]
        public static extern int LSwriteIUS
            (int nModel,
            string szFname);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSreadMPIFile")]
        public static extern int LSreadMPIFile
            (int nModel,
            string szFname);

        [DllImport("lindo4_1.dll",
             EntryPoint = "LSwriteWithSetsAndSC")]
        public static extern int LSwriteWithSetsAndSC
            (int nModel,
            string szFname,
            int nFormat);

        /**********************************************************************
         * Error Handling Routines (3)                                        *
         **********************************************************************/

        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetErrorMessage")]
        public static extern int LSgetErrorMessage
            (int nEnv,
            int nErrorcode,
            StringBuilder achMessage);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetFileError")]
        public static extern int LSgetFileError
            (int nModel,
            ref int nLinenum,
            StringBuilder achLinetxt);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetErrorRowIndex")]
        public static extern int LSgetErrorRowIndex
            (int nModel,
            ref int iRow);


        /**********************************************************************
         * Routines for Setting and Retrieving Parameter Values (14)          *
         **********************************************************************/

        [DllImport("lindo4_1.dll",
             EntryPoint = "LSsetModelParameter")]
        public static extern int LSsetModelParameter
            (int nModel,
            int nParameter,
            ref int nvValue);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetModelParameter")]
        public static extern int LSgetModelParameter
            (int nModel,
            int nParameter,
            ref int nvValue);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSsetEnvParameter")]
        public static extern int LSsetEnvParameter
            (int nEnv,
            int nParameter,
            ref int nvValue);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetEnvParameter")]
        public static extern int LSgetEnvParameter
            (int nEnv,
            int nParameter,
            ref int nvValue);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSsetModelParameter")]
        public static extern int LSsetModelParameter
            (int nModel,
            int nParameter,
            ref double nvValue);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetModelParameter")]
        public static extern int LSgetModelParameter
            (int nModel,
            int nParameter,
            ref double nvValue);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSsetEnvParameter")]
        public static extern int LSsetEnvParameter
            (int nEnv,
            int nParameter,
            ref double nvValue);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetEnvParameter")]
        public static extern int LSgetEnvParameter
            (int nEnv,
            int nParameter,
            ref double nvValue);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSsetModelDouParameter")]
        public static extern int LSsetModelDouParameter
            (int nModel,
            int nParameter,
            double dVal);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetModelDouParameter")]
        public static extern int LSgetModelDouParameter
            (int nModel,
            int nParameter,
            ref double dVal);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSsetModelIntParameter")]
        public static extern int LSsetModelIntParameter
            (int nModel,
            int nParameter,
            int nVal);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetModelIntParameter")]
        public static extern int LSgetModelIntParameter
            (int nModel,
            int nParameter,
            ref int nVal);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSsetEnvDouParameter")]
        public static extern int LSsetEnvDouParameter
            (int nEnv,
            int nParameter,
            double dVal);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetEnvDouParameter")]
        public static extern int LSgetEnvDouParameter
            (int nEnv,
            int nParameter,
            ref double dVal);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSsetEnvIntParameter")]
        public static extern int LSsetEnvIntParameter
            (int nEnv,
            int nParameter,
            int nVal);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetEnvIntParameter")]
        public static extern int LSgetEnvIntParameter
            (int nEnv,
            int nParameter,
            ref int nVal);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSreadModelParameter")]
        public static extern int LSreadModelParameter
            (int nModel,
            string szFname);

        [DllImport("lindo4_1.dll",
             EntryPoint = "LSreadEnvParameter")]
        public static extern int LSreadEnvParameter
            (int nEnv,
            string szFname);


        /*********************************************************************
         * Model Loading Routines (4)                                        *
         *********************************************************************/

        [DllImport("lindo4_1.dll",
             EntryPoint = "LSloadLPData")]
        public static extern int LSloadLPData
            (int nModel,
            int nCons,
            int nVars,
            int dObjSense,
            double dObjConst,
            [MarshalAs(UnmanagedType.LPArray)] double[] adC,
            [MarshalAs(UnmanagedType.LPArray)] double[] adB,
            string szConTypes,
            int nAnnz,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiAcols,
            [MarshalAs(UnmanagedType.LPArray)] int[] acAcols,
            [MarshalAs(UnmanagedType.LPArray)] double[] adAcoef,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiArows,
            [MarshalAs(UnmanagedType.LPArray)] double[] adL,
            [MarshalAs(UnmanagedType.LPArray)] double[] adU);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSloadQCData")]
        public static extern int LSloadQCData
            (int nModel,
            int nQCnnz,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiQCrows,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiQCcols1,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiQCcols2,
            [MarshalAs(UnmanagedType.LPArray)] double[] adQCcoef);

        [DllImport("lindo4_1.dll",
             EntryPoint = "LSloadConeData")]
        public static extern int LSloadConeData
            (int nModel,
            int nCone,
            string szConeTypes,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiConebegcone,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiConecols);



        [DllImport("lindo4_1.dll",
             EntryPoint = "LSloadSETSData")]
        public static extern int LSloadSETSData
            (int nModel,
            int nSETS,
            string szSETStype,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiCARDnum,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiSETSbegcol,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiSETScols);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSloadSemiContData")]
        public static extern int LSloadSemiContData
            (int nModel,
            int nSC,
            [MarshalAs(UnmanagedType.LPArray)] int[] iVarndx,
            [MarshalAs(UnmanagedType.LPArray)] double[] adl,
            [MarshalAs(UnmanagedType.LPArray)] double[] adu);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSloadVarType")]
        public static extern int LSloadVarType
            (int nModel,
            string szVarTypes);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSloadNameData")]
        public static extern int LSloadNameData
            (int nModel,
            string szTitle,
            string szObjName,
            string szRhsName,
            string szRngName,
            string szBndname,
            [MarshalAs(UnmanagedType.LPArray)] string[] aszConNames,
            [MarshalAs(UnmanagedType.LPArray)] string[] aszVarNames,
            [MarshalAs(UnmanagedType.LPArray)] string[] aszConeNames);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSloadNLPData")]
        public static extern int LSloadNLPData
            (int nModel,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiNLPcols,
            [MarshalAs(UnmanagedType.LPArray)] int[] acNLPcols,
            [MarshalAs(UnmanagedType.LPArray)] double[] adNLPcoef,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiNLProws,
            int nNLPobj,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiNLPobj,
            [MarshalAs(UnmanagedType.LPArray)] double[] adNLPobj);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSloadInstruct")]
        public static extern int LSloadInstruct
            (int nModel,
            int nCons,
            int nObjs,
            int nVars,
            int nNumbers,
            [MarshalAs(UnmanagedType.LPArray)] int[] anObjSense,
            string szConType,
            string szVarType,
            [MarshalAs(UnmanagedType.LPArray)] int[] anInstruct,
            int nInstruct,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiVars,
            [MarshalAs(UnmanagedType.LPArray)] double[] adNumVal,
            [MarshalAs(UnmanagedType.LPArray)] double[] adVarVal,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiObjBeg,
            [MarshalAs(UnmanagedType.LPArray)] int[] anObjLen,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiConBeg,
            [MarshalAs(UnmanagedType.LPArray)] int[] anConLen,
            [MarshalAs(UnmanagedType.LPArray)] double[] adLB,
            [MarshalAs(UnmanagedType.LPArray)] double[] adUB);


        /**********************************************************************
         * Solver Initialization Routines (5)                                 *
         **********************************************************************/

        [DllImport("lindo4_1.dll",
             EntryPoint = "LSloadBasis")]
        public static extern int LSloadBasis
            (int nModel,
            [MarshalAs(UnmanagedType.LPArray)] int[] anCstatus,
            [MarshalAs(UnmanagedType.LPArray)] int[] anRstatus);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSloadVarPriorities")]
        public static extern int LSloadVarPriorities
            (int nModel,
            [MarshalAs(UnmanagedType.LPArray)] int[] anCprior);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSreadVarPriorities")]
        public static extern int LSreadVarPriorities
            (int nModel,
            string szFname);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSloadVarStartPoint")]
        public static extern int LSloadVarStartPoint
            (int nModel,
            [MarshalAs(UnmanagedType.LPArray)] double[] adPrimal);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSreadVarStartPoint")]
        public static extern int LSreadVarStartPoint
            (int nModel,
            string szFname);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSloadBlockStructure")]
        public static extern int LSloadBlockStructure
            (int nModel,
            int nBlock,
            int anRblock,
            int anCblock,
            int nType);


        /**********************************************************************
         * Optimization Routines (2)                                          *
         **********************************************************************/

        [DllImport("lindo4_1.dll",
             EntryPoint = "LSoptimize")]
        public static extern int LSoptimize
            (int nModel,
            int nMethod,
            ref int nSolStatus);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSsolveMIP")]
        public static extern int LSsolveMIP
            (int nModel,
            ref int nMIPSolStatus);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSsolveGOP")]
        public static extern int LSsolveGOP
            (int nModel,
            ref int nGOPSolStatus);


        /**********************************************************************
         * Solution Query Routines (12)                                       *
         **********************************************************************/

        /* query general model and solver information */
        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetInfo")]
        public static extern int LSgetInfo
            (int nModel,
            int nQuery,
            ref int nvResult);

        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetInfo")]
        public static extern int LSgetInfo
            (int nModel,
            int nQuery,
            ref double nvResult);

        /* query continous models */
        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetPrimalSolution")]
        public static extern int LSgetPrimalSolution
            (int nModel,
            [MarshalAs(UnmanagedType.LPArray)] double[] adPrimal);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetDualSolution")]
        public static extern int LSgetDualSolution
            (int nModel,
            [MarshalAs(UnmanagedType.LPArray)] double[] adDual);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetReducedCosts")]
        public static extern int LSgetReducedCosts
            (int nModel,
            [MarshalAs(UnmanagedType.LPArray)] double[] adRedcosts);

        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetReducedCostsCone")]
        public static extern int LSgetReducedCostsCone
            (int nModel,
            [MarshalAs(UnmanagedType.LPArray)] double[] adRedcosts);

        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetSlacks")]
        public static extern int LSgetSlacks
            (int nModel,
            [MarshalAs(UnmanagedType.LPArray)] double[] adSlacks);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetBasis")]
        public static extern int LSgetBasis
            (int nModel,
            [MarshalAs(UnmanagedType.LPArray)] int[] anCstatus,
            [MarshalAs(UnmanagedType.LPArray)] int[] anRstatus);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetSolution")]
        public static extern int LSgetSolution
            (int nModel,
            int[] nWhich,
            [MarshalAs(UnmanagedType.LPArray)] double[] adValue);



        /* query integer models */
        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetMIPPrimalSolution")]
        public static extern int LSgetMIPPrimalSolution
            (int nModel,
            [MarshalAs(UnmanagedType.LPArray)] double[] adPrimal);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetMIPDualSolution")]
        public static extern int LSgetMIPDualSolution
            (int nModel,
            [MarshalAs(UnmanagedType.LPArray)] double[] adDual);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetMIPReducedCosts")]
        public static extern int LSgetMIPReducedCosts
            (int nModel,
            [MarshalAs(UnmanagedType.LPArray)] double[] adRedcosts);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetMIPSlacks")]
        public static extern int LSgetMIPSlacks
            (int nModel,
            [MarshalAs(UnmanagedType.LPArray)] double[] adSlacks);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetMIPBasis")]
        public static extern int LSgetMIPBasis
            (int nModel,
            [MarshalAs(UnmanagedType.LPArray)] int[] anCstatus,
            [MarshalAs(UnmanagedType.LPArray)] int[] anRstatus);


        /*********************************************************************
         * Model Query Routines (13)                                         *
         *********************************************************************/

        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetLPData")]
        public static extern int LSgetLPData
            (int nModel,
            ref int dObjSense,
            ref double dObjConst,
            [MarshalAs(UnmanagedType.LPArray)] double[] adC,
            [MarshalAs(UnmanagedType.LPArray)] double[] adB,
            StringBuilder achConTypes,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiAcols,
            [MarshalAs(UnmanagedType.LPArray)] int[] acAcols,
            [MarshalAs(UnmanagedType.LPArray)] double[] adAcoef,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiArows,
            [MarshalAs(UnmanagedType.LPArray)] double[] adL,
            [MarshalAs(UnmanagedType.LPArray)] double[] adU);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetQCData")]
        public static extern int LSgetQCData
            (int nModel,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiQCrows,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiQCcols1,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiQCcols2,
            [MarshalAs(UnmanagedType.LPArray)] double[] adQCcoef);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetQCDatai")]
        public static extern int LSgetQCDatai
            (int nModel,
            int iCon,
            ref int nQCnnz,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiQCcols1,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiQCcols2,
            [MarshalAs(UnmanagedType.LPArray)] double[] adQCcoef);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetVarType")]
        public static extern int LSgetVarType
            (int nModel,
            StringBuilder achVarTypes);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetVarStartPoint")]
        public static extern int LSgetVarStartPoint
            (int nModel,
            [MarshalAs(UnmanagedType.LPArray)] double[] adPrimal);




        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetSETSData")]
        public static extern int LSgetSETSData
            (int nModel,
            ref  int iNsets,
            ref  int iNtnz,
            StringBuilder achSETtype,
            ref  int iCardnum,
            [MarshalAs(UnmanagedType.LPArray)]  int[] aiNnz,
            [MarshalAs(UnmanagedType.LPArray)]  int[] aiBegset,
            [MarshalAs(UnmanagedType.LPArray)]  int[] aiVarndx);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetSETSDatai")]
        public static extern int LSgetSETSDatai
            (int nModel,
            int iSet,
            StringBuilder achSETType,
            [MarshalAs(UnmanagedType.LPArray)]  int[] aiCardnum,
            [MarshalAs(UnmanagedType.LPArray)]  int[] aiNnz,
            [MarshalAs(UnmanagedType.LPArray)]  int[] aiVarndx);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetSemiContData")]
        public static extern int LSgetSemiContData
            (int nModel,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiNvars,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiVarndx,
            [MarshalAs(UnmanagedType.LPArray)] double[] adl,
            [MarshalAs(UnmanagedType.LPArray)] double[] adu);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetNameData")]
        public static extern int LSgetNameData
            (int nModel,
            StringBuilder achTitle,
            StringBuilder achObjName,
            StringBuilder achRhsName,
            StringBuilder achRngName,
            StringBuilder achBndname,
            StringBuilder achConNames,
            StringBuilder achConNameData,
            StringBuilder achVarNames,
            StringBuilder achVarNamesData);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetLPVariableDataj")]
        public static extern int LSgetLPVariableDataj
            (int nModel,
            int iVar,
            StringBuilder achVartype,
            ref double dC,
            ref double dL,
            ref double dU,
            ref int nAnnz,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiArows,
            [MarshalAs(UnmanagedType.LPArray)] double[] adAcoef);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetVariableNamej")]
        public static extern int LSgetVariableNamej
            (int nModel,
            int iVar,
            StringBuilder achVarName);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetVariableIndex")]
        public static extern int LSgetVariableIndex
            (int nModel,
            StringBuilder szVarName,
            ref int iVar);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetConstraintNamei")]
        public static extern int LSgetConstraintNamei
            (int nModel,
            int iCon,
            StringBuilder achConName);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetConstraintIndex")]
        public static extern int LSgetConstraintIndex
            (int nModel,
            StringBuilder szConName,
            ref int iCon);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetConstraintDatai")]
        public static extern int LSgetConstraintDatai
            (int nModel,
            int iCon,
            StringBuilder achConType,
            StringBuilder achIsNlp,
            ref double dB);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetLPConstraintDatai")]
        public static extern int LSgetLPConstraintDatai
            (int Model,
            int iCon,
            StringBuilder achConType,
            ref double dB,
            ref int nNnz,
            ref int iVar,
            ref double dAcoef);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetConeNamei")]
        public static extern int LSgetConeNamei
            (int nModel,
            int iCone,
            StringBuilder achConeName);

        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetConeIndex")]
        public static extern int LSgetConeIndex
            (int nModel,
            string szConeName,
            ref  int iCone);

        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetConeDatai")]
        public static extern int LSgetConeDatai
            (int nModel,
            int iCone,
            StringBuilder achConeType,
            ref  int iNnz,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiCols);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetNLPData")]
        public static extern int LSgetNLPData
            (int nModel,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiNLPcols,
            [MarshalAs(UnmanagedType.LPArray)] int[] acNLPcols,
            [MarshalAs(UnmanagedType.LPArray)] double[] adNLPcoef,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiNLProws,
            ref int nNLPobj,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiNLPobj,
            [MarshalAs(UnmanagedType.LPArray)] double[] adNLPobj,
            StringBuilder achNLPConTypes);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetNLPConstraintDatai")]
        public static extern int LSgetNLPConstraintDatai
            (int nModel,
            int iCon,
            ref int nNnz,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiNLPcols,
            [MarshalAs(UnmanagedType.LPArray)] double[] adNLPcoef);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetNLPVariableDataj")]
        public static extern int LSgetNLPVariableDataj
            (int nModel,
            int iVar,
            ref int nNnz,
            [MarshalAs(UnmanagedType.LPArray)] int[] anNLProws,
            [MarshalAs(UnmanagedType.LPArray)] double[] adNLPcoef);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetNLPObjectiveData")]
        public static extern int LSgetNLPObjectiveData
            (int nModel,
            ref int nNLPobjnnz,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiNLPobj,
            [MarshalAs(UnmanagedType.LPArray)] double[] adNLPobj);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetDualModel")]
        public static extern int LSgetDualModel
            (int nModel,
            int nDualModel);

        /**********************************************************************
         *  Model Modification Routines (13)                                  *
         **********************************************************************/

        [DllImport("lindo4_1.dll",
             EntryPoint = "LSaddConstraints")]
        public static extern int LSaddConstraints
            (int nModel,
            int nNumaddcons,
            string szConTypes,
            [MarshalAs(UnmanagedType.LPArray)] string[] aszConNames,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiArows,
            [MarshalAs(UnmanagedType.LPArray)] double[] adAcoef,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiAcols,
            [MarshalAs(UnmanagedType.LPArray)] double[] adB);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSaddVariables")]
        public static extern int LSaddVariables
            (int nModel,
            int nNumaddvars,
            string szVarTypes,
            [MarshalAs(UnmanagedType.LPArray)] string[] aszVarNames,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiAcols,
            [MarshalAs(UnmanagedType.LPArray)] int[] acAcols,
            [MarshalAs(UnmanagedType.LPArray)] double[] adAcoef,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiArows,
            [MarshalAs(UnmanagedType.LPArray)] double[] adC,
            [MarshalAs(UnmanagedType.LPArray)] double[] adL,
            [MarshalAs(UnmanagedType.LPArray)] double[] adU);

        [DllImport("lindo4_1.dll",
             EntryPoint = "LSaddCones")]
        public static extern int LSaddCones
            (int nModel,
            int nCone,
            string szConeTypes,
            [MarshalAs(UnmanagedType.LPArray)] string[] aszConeNames,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiConebegcone,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiConecols);



        [DllImport("lindo4_1.dll",
             EntryPoint = "LSaddSETS")]
        public static extern int LSaddSETS
            (int nModel,
            int nSETS,
            string szSETStype,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiCARDnum,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiSETSbegcol,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiSETScols);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSdeleteConstraints")]
        public static extern int LSdeleteConstraints
            (int nModel,
            int nCons,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiCons);

        [DllImport("lindo4_1.dll",
             EntryPoint = "LSdeleteCones")]
        public static extern int LSdeleteCones
            (int nModel,
            int nCones,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiCones);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSdeleteSETS")]
        public static extern int LSdeleteSETS
            (int nModel,
            int nSETS,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiSETidx);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSdeleteSemiContVars")]
        public static extern int LSdeleteSemiContVars
            (int nModel,
            int nSC,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiSCndx);

        [DllImport("lindo4_1.dll",
             EntryPoint = "LSdeleteVariables")]
        public static extern int LSdeleteVariables
            (int nModel,
            int nVars,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiVars);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSdeleteQCterms")]
        public static extern int LSdeleteQCterms
            (int nModel,
            int nCons,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiCons);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSmodifyLowerBounds")]
        public static extern int LSmodifyLowerBounds
            (int nModel,
            int nVars,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiVars,
            [MarshalAs(UnmanagedType.LPArray)] double[] adL);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSmodifyUpperBounds")]
        public static extern int LSmodifyUpperBounds
            (int nModel,
            int nVars,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiVars,
            [MarshalAs(UnmanagedType.LPArray)] double[] adU);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSmodifyRHS")]
        public static extern int LSmodifyRHS
            (int nModel,
            int nCons,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiCons,
            [MarshalAs(UnmanagedType.LPArray)] double[] adB);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSmodifyObjective")]
        public static extern int LSmodifyObjective
            (int nModel,
            int nVars,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiVars,
            [MarshalAs(UnmanagedType.LPArray)] double[] adC);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSmodifyObjConstant")]
        public static extern int LSmodifyObjConstant
            (int nModel,
            double dObjConst);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSmodifyAij")]
        public static extern int LSmodifyAij
            (int nModel,
            int iCon1,
            int iVar1,
            double dAij);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSmodifyCone")]
        public static extern int LSmodifyCone
            (int nModel,
            string cConeType,
            int iConeNum,
            int iConeNnz,
            [MarshalAs(UnmanagedType.LPArray)]  int[] aiConeCols);

        [DllImport("lindo4_1.dll",
             EntryPoint = "LSmodifySET")]
        public static extern int LSmodifySET
            (int nModel,
            string cSETtype,
            int iSETnum,
            int iSETnnz,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiSETcols);

        [DllImport("lindo4_1.dll",
             EntryPoint = "LSmodifySemiContVars")]
        public static extern int LSmodifySemiContVars
            (int nModel,
            int nSC,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiVarndx,
            [MarshalAs(UnmanagedType.LPArray)] double[] adl,
            [MarshalAs(UnmanagedType.LPArray)] double[] adu);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSmodifyAj")]
        public static extern int LSmodifyAj
            (int nModel,
            int iVar1,
            int nRows,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiRows,
            [MarshalAs(UnmanagedType.LPArray)] double[] adAj);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSmodifyConstraintType")]
        public static extern int LSmodifyConstraintType
            (int nModel,
            int nCons,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiCons,
            string szConTypes);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSmodifyVariableType")]
        public static extern int LSmodifyVariableType
            (int nModel,
            int nVars,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiVars,
            string szVarTypes);


        /*********************************************************************
         *   Model & Solution Analysis Routines (6)                          *
         *********************************************************************/

        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetConstraintRanges")]
        public static extern int LSgetConstraintRanges
            (int nModel,
            [MarshalAs(UnmanagedType.LPArray)] double[] adDec,
            [MarshalAs(UnmanagedType.LPArray)] double[] adInc);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetObjectiveRanges")]
        public static extern int LSgetObjectiveRanges
            (int nModel,
            [MarshalAs(UnmanagedType.LPArray)] double[] adDec,
            [MarshalAs(UnmanagedType.LPArray)] double[] adInc);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetBoundRanges")]
        public static extern int LSgetBoundRanges
            (int nModel,
            [MarshalAs(UnmanagedType.LPArray)] double[] adDec,
            [MarshalAs(UnmanagedType.LPArray)] double[] adInc);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetBestBounds")]
        public static extern int LSgetBestBounds
            (int nModel,
            [MarshalAs(UnmanagedType.LPArray)] double[] adBestL,
            [MarshalAs(UnmanagedType.LPArray)] double[] adBestU);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSfindIIS")]
        public static extern int LSfindIIS
            (int nModel,
            int nLevel);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSfindIUS")]
        public static extern int LSfindIUS
            (int nModel,
            int nLevel);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSfindBlockStructure")]
        public static extern int LSfindBlockStructure
            (int nModel,
            int nBlock,
            int nType);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetIIS")]
        public static extern int LSgetIIS
            (int nModel,
            ref int nSuf_r,
            ref int nIIS_r,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiCons,
            ref int nSuf_c,
            ref int nIIS_c,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiVars,
            [MarshalAs(UnmanagedType.LPArray)] int[] anBnds);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetIUS")]
        public static extern int LSgetIUS
            (int nModel,
            ref int nSuf,
            ref int nIUS,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiVars);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetBlockStructure")]
        public static extern int LSgetBlockStructure
            (int nModel,
            ref int nBlock,
            [MarshalAs(UnmanagedType.LPArray)] int[] anRblock,
            [MarshalAs(UnmanagedType.LPArray)] int[] anCblock,
            ref int nType);


        /**********************************************************************
         * Advanced Routines (6)                                              *
         **********************************************************************/

        [DllImport("lindo4_1.dll",
             EntryPoint = "LSdoBTRAN")]
        public static extern int LSdoBTRAN
            (int nModel,
            ref int cYnz,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiY,
            [MarshalAs(UnmanagedType.LPArray)] double[] adY,
            ref int cXnz,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiX,
            [MarshalAs(UnmanagedType.LPArray)] double[] adX);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSdoFTRAN")]
        public static extern int LSdoFTRAN
            (int nModel,
            ref int cYnz,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiY,
            [MarshalAs(UnmanagedType.LPArray)] double[] adY,
            ref int cXnz,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiX,
            [MarshalAs(UnmanagedType.LPArray)] double[] adX);


        /* function and gradient evaluations */
        [DllImport("lindo4_1.dll",
             EntryPoint = "LScalcObjFunc")]
        public static extern int LScalcObjFunc
            (int nModel,
            [MarshalAs(UnmanagedType.LPArray)] double[] adPrimal,
            ref double dObjval);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LScalcConFunc")]
        public static extern int LScalcConFunc
            (int nModel,
            int iRow,
            [MarshalAs(UnmanagedType.LPArray)] double[] adPrimal,
            [MarshalAs(UnmanagedType.LPArray)] double[] adSlacks);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LScalcObjGrad")]
        public static extern int LScalcObjGrad
            (int nModel,
            [MarshalAs(UnmanagedType.LPArray)] double[] adPrimal,
            int nParList,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiParList,
            [MarshalAs(UnmanagedType.LPArray)] double[] adParGrad);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LScalcConGrad")]
        public static extern int LScalcConGrad
            (int nModel,
            int irow,
            [MarshalAs(UnmanagedType.LPArray)] double[] adPrimal,
            int nParList,
            [MarshalAs(UnmanagedType.LPArray)] int[] aiParList,
            [MarshalAs(UnmanagedType.LPArray)] double[] adParGrad);



        /**********************************************************************
         * Callback Management Routines (5)                                   *
         **********************************************************************/

        /* Delegate declarations */

        public delegate int typCallback
            (int nModel,
            int nLoc,
            IntPtr nvCbData);

        public delegate int typMIPCallback
            (int nModel,
            IntPtr nvCbData,
            double dObj,
            IntPtr adPrimal);

        public delegate int typFuncalc
            (
            int nModel,
            IntPtr nvCbData,
            int nRow,
            IntPtr adX,
            int nJDiff,
            double dXJBase,
            ref  double dFuncVal,
            IntPtr pReserved);

        public delegate int typUsercalc
            (
            int nModel,
            int nArgs,
            IntPtr pdValues,
            IntPtr nvCbData,
            ref  double dFuncVal);

        public delegate int typGradcalc
            (int nModel,
            IntPtr nvCbData,
            int nRow,
            IntPtr adX,
            IntPtr adLB,
            IntPtr adUB,
            int nNewPnt,
            int nNPar,
            IntPtr aiPartial,
            IntPtr adPartial);

        // General Callback Declaration 

        [DllImport("lindo4_1.dll",
             EntryPoint = "LSsetCallback")]
        public static extern int LSsetCallback
            (int nModel,
            typCallback nfCallback,
            IntPtr nvCbData);

        [DllImport("lindo4_1.dll",
             EntryPoint = "LSsetCallback")]
        public static extern int LSsetCallback
            (int nModel,
            typCallback nfCallback,
            [MarshalAs(UnmanagedType.AsAny)]   object nvCbData);




        // MIP Callback Declaration 

        [DllImport("lindo4_1.dll",
             EntryPoint = "LSsetMIPCallback")]
        public static extern int LSsetMIPCallback
            (int nModel,
            typMIPCallback nfMIPCallback,
            IntPtr nvCbData);

        [DllImport("lindo4_1.dll",
             EntryPoint = "LSsetMIPCallback")]
        public static extern int LSsetMIPCallback
            (int nModel,
            typMIPCallback nfMIPCallback,
            [MarshalAs(UnmanagedType.AsAny)]   object nvCbData);



        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetCallbackInfo")]
        public static extern int LSgetCallbackInfo
            (int nModel,
            int nLocation,
            int nQuery,
            ref int nvValue);

        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetCallbackInfo")]
        public static extern int LSgetCallbackInfo
            (int nModel,
            int nLocation,
            int nQuery,
            ref double nvValue);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetMIPCallbackInfo")]
        public static extern int LSgetMIPCallbackInfo
            (int nModel,
            int nQuery,
            ref int nvValue);

        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetMIPCallbackInfo")]
        public static extern int LSgetMIPCallbackInfo
            (int nModel,
            int nQuery,
            ref double nvValue);


        /* function evaluation routines for NLP solvers */

        [DllImport("lindo4_1.dll",
             EntryPoint = "LSsetUsercalc")]
        public static extern int LSsetUsercalc
            (int nModel,
            typUsercalc nfFunc,
            IntPtr nvCbData);

        [DllImport("lindo4_1.dll",
             EntryPoint = "LSsetUsercalc")]
        public static extern int LSsetUsercalc
            (int nModel,
            typUsercalc nfFunc,
            [MarshalAs(UnmanagedType.AsAny)]   object nvCbData);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSsetFuncalc")]
        public static extern int LSsetFuncalc
            (int nModel,
            typFuncalc nfFunc,
            IntPtr nvCbData);

        [DllImport("lindo4_1.dll",
             EntryPoint = "LSsetFuncalc")]
        public static extern int LSsetFuncalc
            (int nModel,
            typFuncalc nfFunc,
            [MarshalAs(UnmanagedType.AsAny)]   object nvCbData);



        [DllImport("lindo4_1.dll",
             EntryPoint = "LSsetGradcalc")]
        public static extern int LSsetGradcalc
            (int nModel,
            typGradcalc nfGrad_func,
            IntPtr nvCbData,
            int nLenUseGrad,
            ref int nUseGrad);

        [DllImport("lindo4_1.dll",
             EntryPoint = "LSsetGradcalc")]
        public static extern int LSsetGradcalc
            (int nModel,
            typGradcalc nfGrad_func,
            [MarshalAs(UnmanagedType.AsAny)]   object nvCbData,
            int nLenUseGrad,
            ref int nUseGrad);

        /**********************************************************************
         *  Memory Related Routines (7)                                       *
         **********************************************************************/

        [DllImport("lindo4_1.dll",
             EntryPoint = "LSfreeSolverMemory")]
        public static extern int LSfreeSolverMemory
            (int nModel);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSfreeHashMemory")]
        public static extern int LSfreeHashMemory
            (int nModel);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSfreeSolutionMemory")]
        public static extern int LSfreeSolutionMemory
            (int nModel);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSfreeMIPSolutionMemory")]
        public static extern int LSfreeMIPSolutionMemory
            (int nModel);


        [DllImport("lindo4_1.dll",
             EntryPoint = "LSfreeGOPSolutionMemory")]
        public static extern int LSfreeGOPSolutionMemory
            (int nModel);


        /**********************************************************************
         * Deprecated functions from version 1.x                              *
         **********************************************************************/

        /* Deprecated,  use LSgetInfo() */
        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetLicenseInfo")]
        public static extern int LSgetLicenseInfo
            (int nModel,
            ref int nMaxcons,
            ref int nMaxvars,
            ref int nMaxintvars,
            ref int nReserved1,
            ref int nDaystoexp,
            ref int nDaystotrialexp,
            ref int nNlpAllowed,
            ref int nUsers,
            ref int nBarAllowed,
            ref int nRuntime,
            ref int nEdulicense,
            StringBuilder achText);


        /* Deprecated,  use LSgetInfo() */
        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetDimensions")]
        public static extern int LSgetDimensions
            (int nModel,
            ref int nVars,
            ref int nCons,
            ref int nAnnz,
            ref int nQCnnz,
            ref int nNLPnnz,
            ref int nNLPobjnnz,
            ref int nVarNamelen,
            ref int nConNamelen);


        /* Deprecated, use LSsolveMIP() */
        [DllImport("lindo4_1.dll",
             EntryPoint = "LSbnbSolve")]
        public static extern int LSbnbSolve
            (int nModel,
            string szFname);


        /* Deprecated,  use LSgetInfo() */
        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetDualMIPsolution")]
        public static extern int LSgetDualMIPsolution
            (int nModel,
            [MarshalAs(UnmanagedType.LPArray)] double[] adPrimal,
            [MarshalAs(UnmanagedType.LPArray)] double[] adDual,
            [MarshalAs(UnmanagedType.LPArray)] double[] adRedcosts,
            [MarshalAs(UnmanagedType.LPArray)] int[] anCstatus,
            [MarshalAs(UnmanagedType.LPArray)] int[] anRstatus);


        /* Deprecated,  use LSgetInfo() */
        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetMIPSolutionStatus")]
        public static extern int LSgetMIPSolutionStatus
            (int nModel,
            ref int nStatus);


        /* Deprecated,  use LSgetInfo() */
        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetSolutionStatus")]
        public static extern int LSgetSolutionStatus
            (int nModel,
            int nStatus);


        /* Deprecated,  use LSgetInfo() */
        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetObjective")]
        public static extern int LSgetObjective
            (int nModel,
            ref double dObjval);


        /* Deprecated,  use LSgetInfo() */
        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetSolutionInfo")]
        public static extern int LSgetSolutionInfo
            (int nModel,
            ref int nMethod,
            ref int nElapsed,
            ref int nSpxiter,
            ref int nBariter,
            ref int nNlpiter,
            ref int nPrimStatus,
            ref int nDualStatus,
            ref int nBasStatus,
            ref double dPobjval,
            ref double dDobjval,
            ref double dPinfeas,
            ref double dDinfeas);


        /* Deprecated,  use LSgetInfo() */
        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetMIPSolution")]
        public static extern int LSgetMIPSolution
            (int nModel,
            ref double dPobjval,
            [MarshalAs(UnmanagedType.LPArray)] double[] adPrimal);


        /* Deprecated,  use LSgetInfo() */
        [DllImport("lindo4_1.dll",
             EntryPoint = "LSgetCurrentMIPSolutionInfo")]
        public static extern int LSgetCurrentMIPSolutionInfo
            (int nModel,
            ref int nMIPstatus,
            ref double dMIPobjval,
            ref double dBestbound,
            ref int nSpxiter,
            ref int nBariter,
            ref int nNlpiter,
            ref int nLPcnt,
            ref int nBranchcnt,
            ref int nActivecnt,
            ref int nCons_prep,
            ref int nVars_prep,
            ref int nAnnz_prep,
            ref int nInt_prep,
            ref int nCut_contra,
            ref int nCut_obj,
            ref int nCut_gub,
            ref int nCut_lift,
            ref int nCut_flow,
            ref int nCut_gomory,
            ref int nCut_gcd,
            ref int nCut_clique,
            ref int nCut_disagg,
            ref int nCut_planloc,
            ref int nCut_latice,
            ref int nCut_coef);

    }

} /* End of declaration */

