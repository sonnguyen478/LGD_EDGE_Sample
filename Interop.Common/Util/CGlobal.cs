using System.Drawing;

namespace Interop.Common.Util
{
    public sealed class CGlobal
    {

        //Run 위치
        public static readonly string sPathRun = @"C:\MAINBUCK-DOOR-SERVER";

        //Ini 위치
        public static readonly string sPathSetting = @"C:\MAINBUCK-DOOR-SERVER\SETTING";

        // Log 위치
        public static readonly string sPathLog = @"C:\MAINBUCK-DOOR-SERVER-LOG";


        public static readonly string sPathImageData = "";

        ////Image 저장 위치
        //public static readonly string sPathImageNg = @"C:\VisionInspectionData\ImageNg";
        //public static readonly string sPathImageData = @"C:\VisionInspectionData\ImageData";
        //public static readonly string sPathImageDataSeq = @"C:\VisionInspectionData\ImageDataSEQ"; // 2013.07.25
        //public static readonly string sPathImageTemp = @"C:\VisionInspectionData\ImageTemp";

        public static readonly string FAMESFilePath = @"C:\CAR_INFO\";
        public static readonly string FAMESDataFile = "VIN.DAT";
        public static readonly string FAMESFlagFile = "VIN.FLG";

        // 판정 색
        public static Color Judge_OK_Color = System.Drawing.Color.FromArgb(192, 255, 192);//System.Drawing.Color.FromArgb(128, 255, 128); //Color.Lime;
        public static Color Judge_NG_Color = Color.DarkRed;

        //사양 
        public static string SELECTALL = "ALL";
        public static string CARSPEC1 = "일반";
        public static string CARSPEC2 = "파노";

        // Camera pixel resolution
        public const double PIXEL_TO_MM = 0.00766f;

        #region [ enum ]
        // 차종 = 모델
        public enum eModel
        {
            ALL = 0,
            LGD_E61_CG_OCA1 = 1,
            LGD_E61_CG_OCA1_TOUCH = 2,
            LGD_E61_CG_OCA1_TOUCH_OCA2 = 3,
            LGD_E61_CG_OCA1_TOUCH_OCA2_BA = 4,
            LGD_E61_CG_OCA1_TOUCH_OCA2_BA_METAL = 5,
            LGD_E61_CG_OCA1_TOUCH_OCA2_BA_METAL_X758 = 6,
        }

        // Inspection side
        public enum eInspSide
        {
            INSIDE = 0,
            OUTSIDE = 1,
        }

        // Edge polarity for edge detector
        public enum eEdgePolarity
        {
            POSITIVE = 0,
            NEGATIVE = 1,
        }

        // 판정
        public enum eJudge
        {
            OK = 1,
            NG = 2,
        }

        // 알람
        public enum eAlarm
        {
            OK = 0,
            SENSOR, // 센서이상
            INSPECTION,// 검사 이상
        }

        public enum eInspWeldingGubun
        {
            BEFORE = 0, // 용접 전
            AFTER = 1,
        }

        public enum eInspWeldingSide
        {
            LH = 0, //  
            RH = 1,
            NONE = -1,
        }

        // PLC
        public enum ePLC_Name
        {
            HEART_BIT = 0,
            CAR_INFO_CHECK, // 차량정보 Read 요청
            INSP_MODEL,// 차종, 1:YF , 2:LF, 4: AG, 8:HG
            INSP_SEQ,
            PLC_BY_PASS,
            INSP_SPEC, // 사양 - 1:일반,  2:PANO

            // 대차
            FRT_FLR = 10,
            RR_FLR_01,
            RR_FLR_02,
            SIDE_LH,
            SIDE_RH,
            CRP,
            BB,
            //ROOT,

            READ_WELDING_BEFORE_LH = 20, //용접전  Data Read
            READ_WELDING_AFTER_LH, //용접후  Data Read
            READ_WELDING_BEFORE_RH, //용접전  Data Read
            READ_WELDING_AFTER_RH, //용접후  Data Read

            // 센서 측정치
            MEASURE_VAL01_LH = 30,
            MEASURE_VAL02_LH,
            MEASURE_VAL03_LH,
            MEASURE_VAL04_LH,
            MEASURE_VAL05_LH,
            MEASURE_VAL06_LH,
            MEASURE_VAL07_LH,
            MEASURE_VAL08_LH,
            MEASURE_VAL09_LH,
            MEASURE_VAL10_LH,
            MEASURE_VAL11_LH,

            //// 센서 측정치 - LH/RH  값을 PLC 에서 동시 Write 할 경우 사용하기 위해 
            MEASURE_VAL01_RH = 50,
            MEASURE_VAL02_RH,
            MEASURE_VAL03_RH,
            MEASURE_VAL04_RH,
            MEASURE_VAL05_RH,
            MEASURE_VAL06_RH,
            MEASURE_VAL07_RH,
            MEASURE_VAL08_RH,
            MEASURE_VAL09_RH,
            MEASURE_VAL10_RH,
            MEASURE_VAL11_RH,
        }

        //PC
        public enum ePC_Name
        {
            HEART_BIT = 0,
            CAR_INFO_CHECK, // 검사진행 여부 
            READ_WELDING_BEFORE_CONFIRM_LH, // 용접전  Data Read 확인     
            READ_WELDING_AFTER_CONFIRM_LH, //  용접후   Data Read 확인
            READ_WELDING_BEFORE_CONFIRM_RH, // 용접전  Data Read 확인
            READ_WELDING_AFTER_CONFIRM_RH, //  용접후   Data Read 확인

            JUDGE_WELDING_BEFORE_CONFIRM_LH, // 용접전 LH 판정
            JUDGE_WELDING_AFTER_CONFIRM_LH,
            JUDGE_WELDING_BEFORE_CONFIRM_RH,
            JUDGE_WELDING_AFTER_CONFIRM_RH,

            //JUDGE_TOTAL, // 판정, 1:OK , 2:NG
            //  INSP_COMPLET, // 검사 완료 

            PC_ALRAM, // 0:정상 , 1:SENSOR 이상, 2:검사이상
        }
        public enum eSFMTG_InspStandard
        {
            DATE_TIME = 0,
            CAR_TYPE,
            L1_CENTER_X,
            L1_CENTER_Y,
            L1_RADIUS,
            L1_ANGLE,
            L2_CENTER_X,
            L2_CENTER_Y,
            L2_RADIUS,
            L2_ANGLE,
            LH_HEIGHT,
            R1_CENTER_X,
            R1_CENTER_Y,
            R1_RADIUS,
            R1_ANGLE,
            R2_CENTER_X,
            R2_CENTER_Y,
            R2_RADIUS,
            R2_ANGLE,
            RH_HEIGHT,
            APPDATE,
            APPYN,
        }
        public enum eSFMTG_InspRevison
        {
            INSPECTION_REVISON_NO = 0,
            MEASURE_POINT_DOWN,
            MEASURE_POINT_UP,
            RELATION_DOWN,
            RELATION_UP,
            STRAIGHT_DOWN,
            STRAIGHT_UP,
            TOLERANCE,//관리공차
            LAMP_USE_YN,

            DISTANCE_YF,
            DISTANCE_HG,
            DISTANCE_LF,
            DISTANCE_AG,

            DISTANCE_IG,
            DISTANCE_L2,
        }
        public enum eChart
        {
            VERTICAL,
            HORIZONTAL,
        }
        #endregion [ enum ]

        #region [ struct ]
        public struct stProdCount
        {
            public uint nTotal;
            public uint nOK;
            public uint nNG;
            public float nOKPer;
            public float nNGPer;
        }

        public struct structAlramCnt
        {
            public uint iCarNoParsing;// 차대 없는 경우= MES 에서 정보를 못가져 오는 경우
            public uint iInspect;// 검사 이상
            public uint iACSParsing;// 서버 DB 에 대차 번호가 없는 경우
        }

        //public struct structSignalLamp
        //{
        //    public string IP;
        //    public int Port;
        //    public bool Use;
        //    public bool beforUse;
        //    public Interop.Common.QLightLamp.LampName byPassBeforSignal; // bypass 전 경광등 색
        //}


        #endregion [ struct ]

    }
}
