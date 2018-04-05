using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interop.Common.CVB
{
    public class cStruct
    {
        #region  VPAEntry : cvb V11 에 신규로 생긴 Struct = Cvb.Image.VPAEntry
        public struct VPAEntry
        {
            // 요약:
            //     X value of this entry.
            public IntPtr XEntry;
            //
            // 요약:
            //     Y value of this entry.
            public IntPtr YEntry;
        }
        #endregion

        #region [ CVB 에서 사각형 영역 - left/top , right/bottom  ]

        //[System.ComponentModel.TypeConverter(typeof(System.ComponentModel.ExpandableObjectConverter))]
        public struct stCvbArea
        {
            //double x0;
            //double x1;
            //double x2;
            //double y0;
            //double y1;
            //double y2;
            public double Left;    // =x0=x1
            public double Right;   // =x2
            public double Top;     // =y0=y2
            public double Bottom;  // =y1

            //public double Left
            //{
            //    get { return left; }
            //    set { this.left = value; }
            //}
            //public double Right
            //{
            //    get { return right; }
            //    set { this.right = value; }
            //}

            //public double Top
            //{
            //    get { return top; }
            //    set { this.top = value; }
            //}

            //public double Bottom
            //{
            //    get { return bottom; }
            //    set { this.bottom = value; }
            //}
        }
        #endregion

        public struct stPosion
        {
            public int X;
            public int Y;

            public int X2;
            public int Y2;
        }
 

        #region [ CVB 검사 결과 - 공통 항목 ]
        public struct stResultList
        {
            public Interop.Common.CVB.cEnum.eRessult Result; //Quality 에 의한 판정 값
            public double PosionX; // 찾은위치
            public double PosionY;
            public double Quality;
            public string ClassID;
        }
        #endregion [ CVB 검사 결과 - 공통 항목 ]

        #region [ Manto Input]
        public struct stMantoInputData
        {
            public Cvb.Image.IMG OrgImage;
            public string MCF;
            public double Quality;
            public stCvbArea Area;
        }
        #endregion [ Manto Input]

        #region [ Manto output]
        public struct stMantoOutPutData
        {
            public Cvb.Image.IMG OutImage;
            public cEnum.eRessult OutResult;
            public string OutClassID;
            public double OutQuality;
            public double PosionX;
            public double PosionY;
            public string Message;
        }
        #endregion [ Manto output]

        #region [ Minos Input]
        public struct stMinosInputData
        {
            /// <summary>
            ///  검사 이미지
            /// </summary>
            public Cvb.Image.IMG OrgImage;

            /// <summary>
            /// 패턴파일경로 ( CLF 경로 )
            /// </summary>
            public string classfier; //CLF 파일           

            /// <summary>
            /// 검사 영역
            /// </summary>
            public stCvbArea Area;

            /// <summary>
            /// Quality ( default = 0.5 )
            /// </summary>
            public double Quality;

            /// <summary>
            /// 검사 Mode
            /// </summary>
            public cEnum.eMaintoSearchSelect SearchOption;


            /// <summary>
            ///  밀도 default = 500 
            /// </summary>
            public int Density;  // default = 500 , 1000을

            /// <summary>
            /// ALL 모드일때 검색 최대 수
            /// </summary>
            public int ALL_MaxSearhCount; //Search ALL 일때만 사용

            /// <summary>
            /// Search ALL 일때 반지름
            /// </summary>
            public int ALL_Radus;//Search ALL 일때만 사용

            /// <summary>
            ///  OCR 글자 간격
            /// </summary>
            public int OCR_DistanceLimit;

        }
        #endregion [ Manto Input]

        #region [ Minos output]
        public struct stMinosOutPutData
        {
            public Cvb.Image.IMG OutImage;
            public cEnum.eRessult OutResult;// 판정
            public string OutClassID;
            public double OutQuality;
            public double PosionX;
            public double PosionY;
            public string Message;
            //public Cvb.Minos.RESULTS OutArrayResultsList; //Search ALL 일때만 사용

            public stResultList[] arrResult;

        }
        #endregion [ Minos output]

        #region [ Minos Filter Input]
        public struct stMinosFilterInputData
        {
            public Cvb.Image.IMG OrgImage;
            //public stCvbArea Area;
            public Interop.Common.CVB.cEnum.eFilterType FilterType;
            public Interop.Common.CVB.cEnum.eFilterSubType FilterSubType;
            //public Cvb.Minos.TFilterDef userKernel;

            public bool butterWorthHighPass;
            public double butterWorthGain;
            public int butterWorthOffset;
            public int butterWorthOrder;
            public double butterWorthRange;
        }
        #endregion [ Minos  Filter Input]

        #region [  Minos Filter output]
        public struct stMinosFilterOutputData
        {
            public Cvb.Image.IMG OutImage;
            //public string OutClassID;
            //public double OutQuality;
            //public double PosionX;
            //public double PosionY;
            public string Message;
        }
        #endregion [  Minos Filter output]

        //#region [ Filter Input]
        //public struct stFilterInputData
        //{
        //    public Cvb.Image.IMG OrgImage;
        //    //public stCvbArea Area;
        //    //public Interop.Common.CVB.cEnum.eFilterFiilterType filterType;
        //    //public Cvb.Filter.TFilterDef userKernel;

        //    public bool butterWorthHighPass;
        //    public double butterWorthGain;
        //    public int butterWorthOffset;
        //    public int butterWorthOrder;
        //    public double butterWorthRange;

        //}
        //#endregion [ Minos  Filter Input]

        //#region [ Filter output]
        //public struct stFilterOutputData
        //{
        //    public Cvb.Image.IMG OutImage;
        //    //public string OutClassID;
        //    //public double OutQuality;
        //    //public double PosionX;
        //    //public double PosionY;
        //    public string Message;
        //}
        //#endregion [  Filter output]

        #region [ Blob Input]

        public struct stBlobSetting
        {
            public int ThresdHoldMin;
            public int ThresdHoldMax;

            public int SizeMin;
            public int SizeMax;

            public int WidthMin;
            public int WidthMax;

            public int HeightMin;
            public int HeightMax;

            public Cvb.Blob.TSortMode SortMode;//= CVBBlob.TSortMode.POSY;
            public Cvb.Blob.TSortOrder SortOrder;//= CVBBlob.TSortOrder.FALLING;
            public Cvb.Blob.TExtractionMode ExtractionMode; // ALL

            public Cvb.Blob.TBorderMask TouchBorder;// =TBorderMask.NONE

            public System.Drawing.Point SortCoordinate;
            public int NumProjections;
        }

        public struct stBlobResult
        {
            public int CenterX;
            public int CenterY;
            public int Size;

            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            public int Width;
            public int Height;

            public int transX, transY, transXY;
            public double Perimeter;
            public double Ratio;
            // 기타 등등 많이 있음... 

            // int ExecTime;
        }


        public struct stBlobInputData
        {
            public Cvb.Image.IMG OrgImage;

            public stCvbArea LeftTDRect;
            public stCvbArea RightTDRect;

            public double PixelToMM; // 픽셀당 mm
            public int LimitPixel; // 

            public stBlobSetting BlobSetting; // Blob 설정값

            // 정렬 순서
            //최소 길이

        }
        #endregion [ Minos  Filter Input]

        #region [ Blob output]
        public struct stBlobOutputData
        {
            public Cvb.Image.IMG OutImage;

            public bool InspResult; // 검추여부
            public int BlobCount; // 검출수
            public System.Drawing.Point[] CenterPoint; // blob 검출 center
            public int[] BlobSize; // 검출 Size
            public double EexecTime; // 전체 검출시간
            public double ResultPointMargin;// 직진도
            public double ResultMargin;
            public double LineAngle;

            public string Message;
        }
        #endregion [  Filter output]


        public struct stCircleInputData
        {
        }


        public struct stCircleOutputtData
        {
            public double CenterX;
            public double CenterY;
            public double Radius;
            public double RadiusMM;
            public double Qty;
            public bool Result;
        }


        public struct stShapeFinderOutputtData
        {
            public double CenterX;
            public double CenterY;
            public double Radius;
            public double RadiusMM;
            public double Qty;
            public bool Result;
        }



        //'3D 카메라 (LMI sensor) 이미지 정보 (stamp information)
        public struct sLMI_StampInfo
        {
            public long Version;
            public long FrameCount;
            public long Timestamp;           //(us)
            public long EncoderValue;       //ticks
            public long EncoderIndex;       //ticks
            public long DigitalInputStates;
            public double XOffset;              //mm (기본 값은 um)
            public double XResolution;         //mm (기본 값은 um)
            public double YOffset;             //mm (기본 값은 um)
            public double YResolution;         //mm (기본 값은 um)
            public double ZOffset;             //mm (기본 값은 um)
            public double ZResolution;         //mm (기본 값은 um)
            public long HeightMasterpWidth;//      'pixels
            public long HeightMasterpHeight; //    'pixels

            public void Init()
            {
                Version = 0;
                FrameCount = 0;
                Timestamp = 0;           //(us)
                EncoderValue = 0;       //ticks
                EncoderIndex = 0;       //ticks
                DigitalInputStates = 0;
                XOffset = 0;              //mm (기본 값은 um)
                XResolution = 0;         //mm (기본 값은 um)
                YOffset = 0;             //mm (기본 값은 um)
                YResolution = 0;         //mm (기본 값은 um)
                ZOffset = 0;             //mm (기본 값은 um)
                ZResolution = 0;         //mm (기본 값은 um)
                HeightMasterpWidth = 0;//      'pixels
                HeightMasterpHeight = 0; //    'pixels

            }
        }

    }
}
