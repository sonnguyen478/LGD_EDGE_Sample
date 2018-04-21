
namespace Interop.Common.CVB
{

    public class cEnum
    {
        /// <summary>
        /// 디멘져 
        /// </summary>
        public enum eDIMESION
        {
            MONO = 1,
            COLOR = 3
        }

        /// <summary>
        /// 이미지 회전 방향
        /// </summary>
        public enum eRoation
        {
            LEFT = 0, // 반시계방향
            RIGHT = 1, // 시계방향
            OneEighty = 2,//180도

            FlipY180 = 3, // 좌우 반전
            FlipX180 = 4,//상하반전 
        }

        /// <summary>
        ///  판정
        /// </summary>
        public enum eRessult
        {
            NONE = -1,
            OK = 0,
            NG = 1,
        }


        /// <summary>
        /// Minos 검색 옵션
        /// </summary>
        public enum eMaintoSearchSelect
        {
            FIRST = 0,
            OPTIMUM = 1,
            ALL = 2,
            ReadCharacterList,
        }

        /// <summary>
        /// minos Filter  종류
        /// </summary>
        public enum eFilterType
        {
            None = -1,
            ButterWorth = 0,
            Dilate = 1,
            Edge = 2,
            Erode = 3,
            Laplace = 4,
            Low = 5,
            Pyramid = 6,
            Sharpen = 7,
            User = 8,
        }

        /// <summary>
        // // Minis Filter 종료
        /// </summary>
        public enum eFilterSubType
        {
            None = -1,
            Sub2x2 = 0,
            Sub3x3 = 1,
            Sub4x4 = 2,
            Sub5x5 = 3,
        }

        ///// <summary>
        /////  Filter DLL 의 Filter 종류
        ///// </summary>
        //public enum eFilterFiilterType
        //{
        //    NormalizeMinMax,
        //    NormalizeMeanVariance,
        //    ButterWorth,
        //    Laplace,
        //    Sharpen,
        //    Low2x2,
        //    Low3x3,
        //    Low5x5,
        //    Dilate,
        //    Erode,
        //    Edge2x2,
        //    Edge3x3,
        //    User2x2,
        //    User3x3,
        //    User5x5,
        //    Pyramid3x3,
        //    Pyramid4x4,
        //    Pyramid5x5,
        //}

        /// <summary>
        ///  왼쪽 마우스 버튼 모드
        /// </summary>
        public enum LeftButtonMode
        {
            LB_NONE = 0,
            LB_RUBBER = 1,
            LB_FRAME = 2,
            LB_AREAMODE = 3,
            LB_SETORIGIN = 4,
            LB_DRAWPOINT = 5,
            LB_DRAWFILL = 6,
            LB_DRAWLINE = 7,
            LB_DRAWRECT = 8,
        }

        /// <summary>
        /// 휠 마우스 버튼 모드
        /// </summary>
        public enum MouseWheelMode
        {
            MW_NONE = 0,
            MW_ZOOM = 1,
        }

        /// <summary>
        ///  오른쪽 마우스 버튼 모드
        /// </summary>
        public enum RightButtonMode
        {
            RB_NONE = 0,
            RB_ZOOM = 1,
            RB_MENU = 2,
        }

        /// <summary>
        ///  
        /// </summary>
        public enum eMorphologicalType
        {
            Erosion, //침식
            Dilation,//팽장
            Opening,  //열림
            Closing, // 닫힘
            Vertical,
            Horizontal,
            FilterLiner, // User Filter 와 비슷한 기능 함.
        }
        //ErodeImage(Image.IMG imgIn, Foundation.MorphologyMask maskType, int maskWidth, int maskHeight, int maskOffsetX, int maskOffsetY, Image.IMG customMask, out SharedImg imgOut);

        /// <summary>
        ///  Foundtion - 회전
        /// </summary>
        public enum eTransformations
        {
            Roation,
            ReSize,
            Matix,
            SubImage,
            Mirror,
            Shear,
            Perspective,
        }

        /// <summary>
        ///  Foundtion - Mirror 종류
        /// </summary>
        public enum eTransformationsMirror
        {
            Vertical, // 좌우 대칭
            Horizontal, // 상하 대칭
            Both, // 좌우 상하 대칭
        }

        public enum eLmiStampInfo
        {

            //            /*3D 카메라 (LMI sensor) 이미지 정보 (stamp information) 위치
            //'  (LMI 메뉴얼로부터.. 고정 상수 값)
            VERSION1 = 0,
            VERSION2 = 1,
            VERSION3 = 2,
            VERSION4 = 3,
            FRAME_COUNT1 = 4,
            FRAME_COUNT2 = 5,
            FRAME_COUNT3 = 6,
            FRAME_COUNT4 = 7,
            TIEMSTAMP1 = 8,
            TIEMSTAMP2 = 9,
            TIEMSTAMP3 = 10,
            TIEMSTAMP4 = 11,
            ENCODER_VALUE1 = 12,
            ENCODER_VALUE2 = 13,
            ENCODER_VALUE3 = 14,
            ENCODER_VALUE4 = 15,
            ENCODER_INDEX1 = 16,
            ENCODER_INDEX2 = 17,
            ENCODER_INDEX3 = 18,
            ENCODER_INDEX4 = 19,
            DIGITAL_INPUT_STATES1 = 20,
            DIGITAL_INPUT_STATES2 = 21,
            DIGITAL_INPUT_STATES3 = 22,
            DIGITAL_INPUT_STATES4 = 23,
            X_OFFSET1 = 24,
            X_OFFSET2 = 25,
            X_OFFSET3 = 26,
            X_OFFSET4 = 27,
            X_RESOLUTION1 = 28,
            X_RESOLUTION2 = 29,
            X_RESOLUTION3 = 30,
            X_RESOLUTION4 = 31,
            Y_OFFSET1 = 32,
            Y_OFFSET2 = 33,
            Y_OFFSET3 = 34,
            Y_OFFSET4 = 35,
            Y_RESOLUTION1 = 36,
            Y_RESOLUTION2 = 37,
            Y_RESOLUTION3 = 38,
            Y_RESOLUTION4 = 39,
            Z_OFFSET1 = 40,
            Z_OFFSET2 = 41,
            Z_OFFSET3 = 42,
            Z_OFFSET4 = 43,
            Z_RESOLUTION1 = 44,
            Z_RESOLUTION2 = 45,
            Z_RESOLUTION3 = 46,
            Z_RESOLUTION4 = 47,
            HEIGHT_MAP_WIDTH1 = 48,
            HEIGHT_MAP_WIDTH2 = 49,
            HEIGHT_MAP_WIDTH3 = 50,
            HEIGHT_MAP_WIDTH4 = 51,
            HEIGHT_MAP_HEIGHT1 = 52,
            HEIGHT_MAP_HEIGHT2 = 53,
            HEIGHT_MAP_HEIGHT3 = 54,
            HEIGHT_MAP_HEIGHT4 = 55
        }


        // edge Positive Or Negative
        public enum eEdgePositive
        {
            Positive,
            Negative,
        }


        // Edge Type
        public enum eEdgeType
        {
            ALL,
            FIRST,
            EDGEPAIR,
            //ThresHoldResult,

        }

        // Inspection side
        public enum eEdgeInspSide
        {
            Inner = 0,
            Outer,
        }
    }
}

