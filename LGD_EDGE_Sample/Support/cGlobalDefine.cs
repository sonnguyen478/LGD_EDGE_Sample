using System.Drawing;

namespace LGD_EDGE_Sample
{
    public sealed class cGlobalDefine
    {
        public static Color ColorOK = Color.PaleGreen;
        public static Color ColorNG = Color.Red;
        public static Color ColorDefault = Color.White;
        public static Color ColorYellow = Color.Yellow;

        public static Color CENTER_LINE_COLOR = Color.Green;
        public static Color INSP_LINE_COLOR = Color.Red;
        public static Color CROSS_LINE_COLOR = Color.PaleGreen;
        public static Color AREA_COLOR = Color.Green;
        public static Color NOR_COLOR = Color.White;

        public static int CENTER_LINE = 800;
        public static int INSP_LINE = 900;
        public static int CROSS_LINE = 1000;
        public static int CROSS_LINE_LENGTH = 200;
        public static int EDGE_INSP_STEP = 10; // edge 간적

        public static int InspCamCnt = 2;

        public const int cLabel = 100;
        public const int cCross = 1000;
        public const int cTarget = 10000;

        public enum eJudge
        {
            OK = 1,
            NG = 2,
        }
        public enum ePLCSignal
        {
            HEARTBIT = 0,
            INSP_START = 1,
            INSP_MODEL = 2,
        }

        public enum ePCSignal
        {
            HEARTBIT = 0,
            INSP_START = 1,
            INSP_COMP = 2,
            INSP_RESULT = 3,
        }

        public struct stProdCount
        {
            public uint nTotal;
            public uint nOK;
            public uint nNG;
            public float nOKPer;
            public float nNGPer;
        }






    }
}
