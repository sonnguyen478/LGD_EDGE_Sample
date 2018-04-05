/*
 * 1.높이 2개인진 한개인지 확인 : pos_LH_Z_L1 , pos_LH_Z_L2 ==> pos_LH_Z_HEIGHT 로 변경 여부 ??
 * 2. 직진도표시 ;각도인지 백분율인지 확인 및 
 * 
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Interop.Common.Util
{
    public class calcCoordinate
    {
        private Interop.Common.DB.cSQLServer standardInfo = null;

        #region [ 측정 항목 ]
        private double pos_LH_X_L1; // 좌측 상단 X Point
        private double pos_LH_Y_L1; // 좌측 상단 Y Point

        private double pos_LH_X_L2; // 좌측 하단 X Point
        private double pos_LH_Y_L2; // 좌측 하단 Y Point

        private double pos_RH_X_L1; // 우측 상단 X Point
        private double pos_RH_Y_L1; // 우측 상단 Y Point

        private double pos_RH_X_L2; // 우측 하단 X Point
        private double pos_RH_Y_L2; // 우측 하단 Y Point
        #endregion [ 측정 항목 ]

        #region [ 허용공차  변수]

        private double d_mPoint_Down; //  
        private double d_mPoint_Up; // 

        private double d_Relation_Down; //  
        private double d_Relation_Up; // 

        private double d_Straight_Down; //  
        private double d_Straight_Up; //  

        private double d_Tolerance;// 관리공차

        private bool d_LampUse = true;

        private double d_Distance_YF;
        private double d_Distance_LF;
        private double d_Distance_HG;
        private double d_Distance_AG;

        private double d_Distance_IG;
        private double d_Distance_L2;

        #endregion [ 허용공차 ]

        #region [ 설정항목 높이 ]
        //private double pos_LH_Z_L1; // 좌측 상단 Z Point
        //private double pos_LH_Z_L2; // 좌측 상단 Z Point
        //private double pos_RH_Z_L1; // 우측 상단 Z Point
        //private double pos_RH_Z_L2; // 우측 상단 Z Point

        private double pos_LH_HEIGHT;// 좌측 높이
        private double pos_RH_HEIGHT;// 우측 높이

        #endregion [ 설정항목 높이 ]

        #region [ 설정항목 반지름 각도 등.. ]
        private double pos_Cen_LH_X_L1;
        private double pos_Cen_LH_Y_L1;
        private double pos_Cen_LH_X_L2;
        private double pos_Cen_LH_Y_L2;

        private double pos_Cen_RH_X_L1;
        private double pos_Cen_RH_Y_L1;
        private double pos_Cen_RH_X_L2;
        private double pos_Cen_RH_Y_L2;

        private double pos_Radius_LH_L1;
        private double pos_Radius_LH_L2;

        private double pos_Radius_RH_L1;
        private double pos_Radius_RH_L2;

        private double pos_Angle_LH_L1;
        private double pos_Angle_LH_L2;

        private double pos_Angle_RH_L1;
        private double pos_Angle_RH_L2;
        #endregion [ 설정항목 반지름 각도 등.. ]

        #region [ 측정 항목 Property ]
        public double Pos_LH_X_L1
        {
            get { return this.pos_LH_X_L1; }
            set { this.pos_LH_X_L1 = value; }
        }
        public double Pos_LH_Y_L1
        {
            get { return this.pos_LH_Y_L1; }
            set { this.pos_LH_Y_L1 = value; }
        }
        public double Pos_LH_X_L2
        {
            get { return this.pos_LH_X_L2; }
            set { this.pos_LH_X_L2 = value; }
        }
        public double Pos_LH_Y_L2
        {
            get { return this.pos_LH_Y_L2; }
            set { this.pos_LH_Y_L2 = value; }
        }
        public double Pos_RH_X_L1
        {
            get { return this.pos_RH_X_L1; }
            set { this.pos_RH_X_L1 = value; }
        }
        public double Pos_RH_Y_L1
        {
            get { return this.pos_RH_Y_L1; }
            set { this.pos_RH_Y_L1 = value; }
        }
        public double Pos_RH_X_L2
        {
            get { return this.pos_RH_X_L2; }
            set { this.pos_RH_X_L2 = value; }
        }
        public double Pos_RH_Y_L2
        {
            get { return this.pos_RH_Y_L2; }
            set { this.pos_RH_Y_L2 = value; }
        }

        public double Pos_LH_HEIGHT
        {
            get { return pos_LH_HEIGHT; }
            set { pos_LH_HEIGHT = value; }
        }

        public double Pos_RH_HEIGHT
        {
            get { return pos_RH_HEIGHT; }
            set { pos_RH_HEIGHT = value; }
        }
        #endregion [ 측정 항목 Property ]

        #region [ 설정 항목 높이 Property ]
        //public double Pos_LH_Z_L1
        //{
        //    get { return this.pos_LH_Z_L1; }
        //    set { this.pos_LH_Z_L1 = value; }
        //}
        //public double Pos_LH_Z_L2
        //{
        //    get { return this.pos_LH_Z_L2; }
        //    set { this.pos_LH_Z_L2 = value; }
        //}
        //public double Pos_RH_Z_L1
        //{
        //    get { return this.pos_RH_Z_L1; }
        //    set { this.pos_RH_Z_L1 = value; }
        //}
        //public double Pos_RH_Z_L2
        //{
        //    get { return this.pos_RH_Z_L2; }
        //    set { this.pos_RH_Z_L2 = value; }
        //}
        #endregion [ 설정 항목 높이 Property ]

        #region [ 설정항목 반지름 각도 등.. Property ]
        public double Pos_Cen_LH_X_L1
        {
            get { return this.pos_Cen_LH_X_L1; }
            set { this.pos_Cen_LH_X_L1 = value; }
        }
        public double Pos_Cen_LH_Y_L1
        {
            get { return this.pos_Cen_LH_Y_L1; }
            set { this.pos_Cen_LH_Y_L1 = value; }
        }
        public double Pos_Cen_LH_X_L2
        {
            get { return this.pos_Cen_LH_X_L2; }
            set { this.pos_Cen_LH_X_L2 = value; }
        }
        public double Pos_Cen_LH_Y_L2
        {
            get { return this.pos_Cen_LH_Y_L2; }
            set { this.pos_Cen_LH_Y_L2 = value; }
        }
        public double Pos_Cen_RH_X_L1
        {
            get { return this.pos_Cen_RH_X_L1; }
            set { this.pos_Cen_RH_X_L1 = value; }
        }
        public double Pos_Cen_RH_Y_L1
        {
            get { return this.pos_Cen_RH_Y_L1; }
            set { this.pos_Cen_RH_Y_L1 = value; }
        }
        public double Pos_Cen_RH_X_L2
        {
            get { return this.pos_Cen_RH_X_L2; }
            set { this.pos_Cen_RH_X_L2 = value; }
        }
        public double Pos_Cen_RH_Y_L2
        {
            get { return this.pos_Cen_RH_Y_L2; }
            set { this.pos_Cen_RH_Y_L2 = value; }
        }
        public double Pos_Radius_LH_L1
        {
            get { return this.pos_Radius_LH_L1; }
            set { this.pos_Radius_LH_L1 = value; }
        }
        public double Pos_Radius_LH_L2
        {
            get { return this.pos_Radius_LH_L2; }
            set { this.pos_Radius_LH_L2 = value; }
        }
        public double Pos_Radius_RH_L1
        {
            get { return this.pos_Radius_RH_L1; }
            set { this.pos_Radius_RH_L1 = value; }
        }
        public double Pos_Radius_RH_L2
        {
            get { return this.pos_Radius_RH_L2; }
            set { this.pos_Radius_RH_L2 = value; }
        }
        public double Pos_Angle_LH_L1
        {
            get { return this.pos_Angle_LH_L1; }
            set { this.pos_Angle_LH_L1 = value; }
        }
        public double Pos_Angle_LH_L2
        {
            get { return this.pos_Angle_LH_L2; }
            set { this.pos_Angle_LH_L2 = value; }
        }
        public double Pos_Angle_RH_L1
        {
            get { return this.pos_Angle_RH_L1; }
            set { this.pos_Angle_RH_L1 = value; }
        }
        public double Pos_Angle_RH_L2
        {
            get { return this.pos_Angle_RH_L2; }
            set { this.pos_Angle_RH_L2 = value; }
        }
        #endregion [ 설정항목 반지름 각도 등.. Property ]

        #region [ 허용공차 property ]

        public double D_mPoint_Down
        {
            get { return d_mPoint_Down; }
            set { d_mPoint_Down = value; }
        }

        public double D_mPoint_Up
        {
            get { return d_mPoint_Up; }
            set { d_mPoint_Up = value; }
        }


        public double D_Relation_Down
        {
            get { return d_Relation_Down; }
            set { d_Relation_Down = value; }
        }

        public double D_Relation_Up
        {
            get { return d_Relation_Up; }
            set { d_Relation_Up = value; }
        }

        public double D_Straight_Down
        {
            get { return d_Straight_Down; }
            set { d_Straight_Down = value; }
        }

        public double D_Straight_Up
        {
            get { return d_Straight_Up; }
            set { d_Straight_Up = value; }
        }

        public double D_Tolerance
        {
            get { return d_Tolerance; }
            set { d_Tolerance = value; }
        }


        public bool D_LampUse
        {
            get { return d_LampUse; }
            set { d_LampUse = value; }
        }

        public double D_Distance_YF
        {
            get { return d_Distance_YF; }
            set { d_Distance_YF = value; }
        }

        public double D_Distance_LF
        {
            get { return d_Distance_LF; }
            set { d_Distance_LF = value; }
        }


        public double D_Distance_AG
        {
            get { return d_Distance_AG; }
            set { d_Distance_AG = value; }
        }


        public double D_Distance_HG
        {
            get { return d_Distance_HG; }
            set { d_Distance_HG = value; }
        }



        public double D_Distance_IG
        {
            get { return d_Distance_IG; }
            set { d_Distance_IG = value; }
        }


        public double D_Distance_L2
        {
            get { return d_Distance_L2; }
            set { d_Distance_L2 = value; }
        }





        #endregion [ 허용공차 property ]

        #region [ 초기 생성 ]
        public calcCoordinate()
        {
        }

        public calcCoordinate(Interop.Common.DB.cSQLServer parentDB)
        {
            if ( null != parentDB )
            {
                standardInfo = parentDB;
            }
        }
        #endregion [ 초기 생성 ]

        #region [ 기준정보 ]
        // 기준정보
        public bool RefreshStandardInformation(string carType)
        {
            bool result = false;
            DataTable makeData = null;
            try
            {
                if ( true == standardInfo.IsConnect() )
                {
                    makeData = standardInfo.getSFMTGStandardValue_SFMount( carType );

                    if ( makeData.Rows.Count > 0 )
                    {
                        pos_Cen_LH_X_L1 = Convert.ToDouble( makeData.Rows[ 0 ][ (int)CGlobal.eSFMTG_InspStandard.L1_CENTER_X ] );
                        pos_Cen_LH_Y_L1 = Convert.ToDouble( makeData.Rows[ 0 ][ (int)CGlobal.eSFMTG_InspStandard.L1_CENTER_Y ] );
                        pos_Radius_LH_L1 = Convert.ToDouble( makeData.Rows[ 0 ][ (int)CGlobal.eSFMTG_InspStandard.L1_RADIUS ] );
                        pos_Angle_LH_L1 = Convert.ToDouble( makeData.Rows[ 0 ][ (int)CGlobal.eSFMTG_InspStandard.L1_ANGLE ] );

                        pos_Cen_LH_X_L2 = Convert.ToDouble( makeData.Rows[ 0 ][ (int)CGlobal.eSFMTG_InspStandard.L2_CENTER_X ] );
                        pos_Cen_LH_Y_L2 = Convert.ToDouble( makeData.Rows[ 0 ][ (int)CGlobal.eSFMTG_InspStandard.L2_CENTER_Y ] );
                        pos_Radius_LH_L2 = Convert.ToDouble( makeData.Rows[ 0 ][ (int)CGlobal.eSFMTG_InspStandard.L2_RADIUS ] );
                        pos_Angle_LH_L2 = Convert.ToDouble( makeData.Rows[ 0 ][ (int)CGlobal.eSFMTG_InspStandard.L2_ANGLE ] );

                        this.pos_LH_HEIGHT = Convert.ToDouble( makeData.Rows[ 0 ][ (int)CGlobal.eSFMTG_InspStandard.LH_HEIGHT ] );

                        pos_Cen_RH_X_L1 = Convert.ToDouble( makeData.Rows[ 0 ][ (int)CGlobal.eSFMTG_InspStandard.R1_CENTER_X ] );
                        pos_Cen_RH_Y_L1 = Convert.ToDouble( makeData.Rows[ 0 ][ (int)CGlobal.eSFMTG_InspStandard.R1_CENTER_Y ] );
                        pos_Radius_RH_L1 = Convert.ToDouble( makeData.Rows[ 0 ][ (int)CGlobal.eSFMTG_InspStandard.R1_RADIUS ] );
                        pos_Angle_RH_L1 = Convert.ToDouble( makeData.Rows[ 0 ][ (int)CGlobal.eSFMTG_InspStandard.R1_ANGLE ] );

                        pos_Cen_RH_X_L2 = Convert.ToDouble( makeData.Rows[ 0 ][ (int)CGlobal.eSFMTG_InspStandard.R2_CENTER_X ] );
                        pos_Cen_RH_Y_L2 = Convert.ToDouble( makeData.Rows[ 0 ][ (int)CGlobal.eSFMTG_InspStandard.R2_CENTER_Y ] );
                        pos_Radius_RH_L2 = Convert.ToDouble( makeData.Rows[ 0 ][ (int)CGlobal.eSFMTG_InspStandard.R2_RADIUS ] );
                        pos_Angle_RH_L2 = Convert.ToDouble( makeData.Rows[ 0 ][ (int)CGlobal.eSFMTG_InspStandard.R2_ANGLE ] );

                        this.pos_RH_HEIGHT = Convert.ToDouble( makeData.Rows[ 0 ][ (int)CGlobal.eSFMTG_InspStandard.RH_HEIGHT ] );

                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
            }
            catch ( Exception ex )
            {
                cLog.FileWrite_Str( "좌표계 기준정보 Load 실패 : " + ex.Message.ToString(), cLog.eLogType.EXCEPTION );
                cLog.FileWrite_Str( "좌표계 기준정보 Load 실패 : " + ex.Message.ToString(), cLog.eLogType.LOG );
                result = false;
            }
            return result;
        }
        #endregion [ 기준정보 ]

        #region [ 허용공차 ]
        // 허용공차
        public bool RefreshRevisionInformation(int _location)
        {
            bool result = false;
            DataTable makeData = null;
            try
            {
                if ( true == standardInfo.IsConnect() )
                {
                    makeData = standardInfo.getSFMTGRevision_SFMount( _location );

                    if ( makeData.Rows.Count > 0 )
                    {
                        this.d_mPoint_Down = Convert.ToDouble( makeData.Rows[ 0 ][ (int)CGlobal.eSFMTG_InspRevison.MEASURE_POINT_DOWN ] );
                        this.d_mPoint_Up = Convert.ToDouble( makeData.Rows[ 0 ][ (int)CGlobal.eSFMTG_InspRevison.MEASURE_POINT_UP ] );
                        this.d_Relation_Down = Convert.ToDouble( makeData.Rows[ 0 ][ (int)CGlobal.eSFMTG_InspRevison.RELATION_DOWN ] );
                        this.d_Relation_Up = Convert.ToDouble( makeData.Rows[ 0 ][ (int)CGlobal.eSFMTG_InspRevison.RELATION_UP ] );

                        this.d_Straight_Down = Convert.ToDouble( makeData.Rows[ 0 ][ (int)CGlobal.eSFMTG_InspRevison.STRAIGHT_DOWN ] );
                        this.d_Straight_Up = Convert.ToDouble( makeData.Rows[ 0 ][ (int)CGlobal.eSFMTG_InspRevison.STRAIGHT_UP ] );

                        this.d_Tolerance = Convert.ToDouble( makeData.Rows[ 0 ][ (int)CGlobal.eSFMTG_InspRevison.TOLERANCE ] );

                        this.d_LampUse = Convert.ToBoolean( makeData.Rows[ 0 ][ (int)CGlobal.eSFMTG_InspRevison.LAMP_USE_YN ] );

                        this.d_Distance_HG = Convert.ToDouble( makeData.Rows[ 0 ][ (int)CGlobal.eSFMTG_InspRevison.DISTANCE_HG ] );
                        this.d_Distance_YF = Convert.ToDouble( makeData.Rows[ 0 ][ (int)CGlobal.eSFMTG_InspRevison.DISTANCE_YF ] );//미사용
                        this.d_Distance_LF = Convert.ToDouble( makeData.Rows[ 0 ][ (int)CGlobal.eSFMTG_InspRevison.DISTANCE_LF ] );
                        this.d_Distance_AG = Convert.ToDouble( makeData.Rows[ 0 ][ (int)CGlobal.eSFMTG_InspRevison.DISTANCE_AG ] );

                        this.d_Distance_IG = Convert.ToDouble( makeData.Rows[ 0 ][ (int)CGlobal.eSFMTG_InspRevison.DISTANCE_AG + 1 ] ); // IG
                        this.d_Distance_L2 = Convert.ToDouble( makeData.Rows[ 0 ][ (int)CGlobal.eSFMTG_InspRevison.DISTANCE_AG + 2 ] );// L2

                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
            }
            catch ( Exception ex )
            {
                cLog.FileWrite_Str( "CalcCoordinate 허용공차 Load 실패 : " + ex.Message.ToString(), cLog.eLogType.EXCEPTION );
                cLog.FileWrite_Str( "CalcCoordinate 허용공차 Load 실패 : " + ex.Message.ToString(), cLog.eLogType.LOG );
                result = false;
            }
            return result;
        }
        #endregion [ 허용공차 ]

        #region [ 직진도 판정 ]
        /// <summary>
        ///  직진도 판정
        /// </summary>
        /// <param name="_value">진진도</param>
        /// <returns>OK:true , NG:fakse</returns>
        public bool GetStraightJudge(double _value)
        {
            bool result = true;

            if ( _value >= this.d_Straight_Down && _value <= this.d_Straight_Up )
            {
                result = true;
            }
            else
            {
                result = false;
            }

            return result;
        }
        #endregion [ 직진도 판정 ]

        #region [ 관리공차 판정 ]
        /// <summary>
        /// 관리공차 판정
        /// </summary>
        /// <param name="_value">진진도</param>
        /// <returns></returns>
        public bool GetToleranceJudge(double _value)
        {
            bool result = true;

            if ( _value >= this.d_Straight_Down && _value <= this.d_Tolerance )
            {
                result = true;
            }
            else
            {
                result = false;
            }

            return result;
        }
        #endregion [ 직진도 판정 ]

        #region [ 세점의 길이 ]
        // 세점의 길이
        private double CalculationDistance()
        {
            //double retValue;
            //try
            //{
            //    retValue = Math.Sqrt( Math.Pow( ( this.pos_LH_X_L1 - this.pos_LH_X_L2 ), 2 )
            //                               + Math.Pow( ( this.pos_LH_Y_L1 - this.pos_LH_Y_L2 ), 2 )
            //                               + Math.Pow( ( this.pos_LH_Z_L1 - this.pos_LH_Z_L2 ), 2 ) );
            //}
            //catch ( Exception ex )
            //{
            //    CLog.FileWrite_Str( "좌표 거리계산 실패 : " + ex.Message.ToString(), CLog.eLogType.EXCEPTION );
            //    CLog.FileWrite_Str( "좌표 거리계산 실패 : " + ex.Message.ToString(), CLog.eLogType.LOG );
            //    retValue = 0;
            //}
            //return retValue;

            return CalculationDistance( this.pos_LH_X_L1, this.pos_LH_Y_L1, this.pos_LH_HEIGHT, this.pos_LH_X_L2, this.pos_LH_Y_L2, 0 );
        }
        #endregion [ 세점의 길이 ]

        #region [ 두점 길이 ]
        /// <summary>
        ///  두점 길이
        /// </summary>
        /// <param name="X1"></param>
        /// <param name="Y1"></param>
        /// <param name="Z1"></param>
        /// <param name="X2"></param>
        /// <param name="Y2"></param>
        /// <param name="Z2"></param>
        /// <returns></returns>
        public double CalculationDistance(double X1, double Y1, double Z1, double X2, double Y2, double Z2)
        {
            double retValue;

            try
            {
                retValue = Math.Sqrt( Math.Pow( Math.Abs( X1 - X2 ), 2 ) + Math.Pow( Math.Abs( Y1 - Y2 ), 2 ) + Math.Pow( Math.Abs( Z1 - Z2 ), 2 ) );

                if ( retValue <= 0 ) retValue = 0;
            }
            catch ( Exception ex )
            {
                CLog.FileWrite_Str( "입력 좌표 거리계산 실패 : " + ex.Message.ToString(), CLog.eLogType.EXCEPTION );
                CLog.FileWrite_Str( "입력 좌표 거리계산 실패 : " + ex.Message.ToString(), CLog.eLogType.LOG );
                retValue = 0;
            }
            return retValue;
        }
        #endregion [ 두점 길이 ]

        #region [ 두점 길이 ]
        /// <summary>
        /// 특정 각도회전값
        /// </summary>
        /// <param name="_targetValue">값</param>
        /// <param name="_CoordnatAngle">회전각</param>
        /// <returns></returns>
        public double CalculationChangeCoordinate(double _targetValue, double _CoordnatAngle)
        {
            double retValue;

            try
            {
                if ( _targetValue != 0 )
                {
                    retValue = Math.Cos( _CoordnatAngle * ( Math.PI / 180 ) ) * _targetValue;
                }
                else
                {
                    retValue = 0;
                }
            }
            catch ( Exception ex )
            {
                CLog.FileWrite_Str( "좌표 이동계산 실패 : " + ex.Message.ToString(), CLog.eLogType.EXCEPTION );
                CLog.FileWrite_Str( "좌표 이동계산 실패 : " + ex.Message.ToString(), CLog.eLogType.LOG );
                retValue = 0;
            }
            return retValue;
        }
        #endregion [ 특정 각도회전값 ]

        #region [ 특정 각도 만큼이동 ]
        //특정 각도 만큼이동
        public double CalculationChangeCoordinate_LH_L_X(double _targetValue)
        {
            return this.CalculationChangeCoordinate( _targetValue, this.pos_Angle_LH_L1 );
        }
        public double CalculationChangeCoordinate_LH_T_Y(double _targetValue)
        {
            return this.CalculationChangeCoordinate( _targetValue, this.pos_Angle_LH_L2 );
        }

        public double CalculationChangeCoordinate_RH_L_X(double _targetValue)
        {
            return this.CalculationChangeCoordinate( _targetValue, this.pos_Angle_RH_L1 );
        }
        public double CalculationChangeCoordinate_RH_T_Y(double _targetValue)
        {
            return this.CalculationChangeCoordinate( _targetValue, this.pos_Angle_RH_L2 );
        }
        #endregion [ 특정 각도 만큼이동 ]

        #region [ 직진도를 구하기 위한 최소/최대 길이 ]

        /// <summary>
        /// 왼쪽 최소길이 = 왼쪽 높이
        /// </summary>
        /// <returns></returns>
        public double CalculationMinDistance_L()
        {
            // return CalculationDistance(this.pos_Cen_LH_X_L1,this.pos_Cen_LH_Y_L1, this.pos_LH_HEIGHT ,this.pos_Cen_LH_X_L2,this.pos_Cen_LH_Y_L2, 0 );
            return this.pos_LH_HEIGHT;
        }
        /// <summary>
        /// 오른쪽 최소 길이
        /// </summary>
        /// <returns></returns>
        public double CalculationMinDistance_R()
        {
            // return CalculationDistance(this.pos_Cen_RH_X_L1,this.pos_Cen_RH_Y_L1, this.pos_RH_HEIGHT ,this.pos_Cen_RH_X_L2,this.pos_Cen_RH_Y_L2,0 );
            return this.pos_RH_HEIGHT;
        }

        /// <summary>
        ///  왼쪽(LH) 최대 길이 - SQRT(2R*2R + H*H)
        /// </summary>
        /// <returns></returns>
        public double CalculationMaxDistance_L()
        {
            ////return Math.Sqrt(this.pos_LH_HEIGHT * this.pos_LH_HEIGHT + this.pos_Radius_LH_L1 * this.pos_Radius_LH_L1);
            //return CalculationDistance(this.pos_Cen_LH_X_L1 + this.pos_Radius_LH_L1, this.pos_Cen_LH_Y_L1, this.pos_LH_HEIGHT, this.pos_Cen_LH_X_L2 - this.pos_Radius_LH_L2, this.pos_Cen_LH_Y_L2, 0);
            return Math.Round( Math.Sqrt( Math.Pow( Math.Abs( this.pos_Radius_LH_L1 ) * 2, 2 ) + Math.Pow( Math.Abs( this.pos_LH_HEIGHT ), 2 ) ), 3 );
        }

        /// <summary>
        /// 오른쪽(RH) 최대 길이
        /// </summary>
        /// <returns></returns>
        public double CalculationMaxDistance_R()
        {
            ////return Math.Sqrt(this.pos_RH_HEIGHT * this.pos_RH_HEIGHT + this.pos_Radius_H_L1 * this.pos_Radius_LH_L1);
            // return CalculationDistance(this.pos_Cen_RH_X_L1 + this.pos_Radius_RH_L1, this.pos_Cen_RH_Y_L1, this.pos_RH_HEIGHT, this.pos_Cen_RH_X_L2 - this.pos_Radius_RH_L2, this.pos_Cen_RH_Y_L2, 0);

            return Math.Round( Math.Sqrt( Math.Pow( Math.Abs( this.pos_Radius_RH_L1 ) * 2, 2 ) + Math.Pow( Math.Abs( this.pos_RH_HEIGHT ), 2 ) ), 3 );
        }

        #endregion

        #region [ 직진도 ]

        /// <summary>
        /// 직진도를 구함
        /// </summary>
        /// <param name="_isLeft">true:왼쪽 false:오른쪽</param>
        /// <param name="_currentValue">두점의 길이</param>
        /// <returns></returns>
        public double GetStraightness(bool _isLeft, double _currentValue)
        {
            // 직진도 =  (현재길이-최소길이)/(최대길이-최소길이)
            // 즉 최대 길이에 대한 현재 길이의 비율이다.

            double rtnvalue = 0;
            try
            {
                if ( _isLeft == true )
                {
                    rtnvalue = ( _currentValue - this.CalculationMinDistance_L() ) / ( this.CalculationMaxDistance_L() - this.CalculationMinDistance_L() );
                }
                else
                {
                    rtnvalue = ( _currentValue - this.CalculationMinDistance_R() ) / ( this.CalculationMaxDistance_R() - this.CalculationMinDistance_R() );
                }

                rtnvalue = CUtil.NumberToNumber( rtnvalue, 2 );//소수점아래 한자리                
            }
            catch ( Exception ex )
            {
                CLog.FileWrite_Str( "직진도를 구함 : " + ex.Message.ToString(), CLog.eLogType.EXCEPTION );

                //double.Parse("Infinity", System.Globalization.CultureInfo.InvariantCulture)
                if ( double.IsInfinity( rtnvalue ) )
                {
                    rtnvalue = 0; // Infilty 에러 발생시 0으로 처리
                    if ( _isLeft == true )
                    {
                        CLog.FileWrite_Str( "직진도를 구함 L: _currentValue:" + _currentValue + "  this.CalculationMinDistance_L():" + this.CalculationMinDistance_L()
                            + " , this.CalculationMaxDistance_L():" + this.CalculationMaxDistance_L() + ", CalculationMinDistance_L():" + CalculationMinDistance_L(), CLog.eLogType.EXCEPTION );
                    }
                    else
                    {
                        CLog.FileWrite_Str( "직진도를 구함 R : _currentValue:" + _currentValue + "  this.CalculationMinDistance_R():" + this.CalculationMinDistance_R()
                               + " , this.CalculationMaxDistance_R():" + this.CalculationMaxDistance_R() + ", CalculationMinDistance_R():" + CalculationMinDistance_R(), CLog.eLogType.EXCEPTION );

                    }
                }
            }

            return rtnvalue;
        }

        /// <summary>
        /// LH 직진도를 구함
        /// </summary>
        /// <param name="_currentvalue">현재 두점의 길이</param>
        /// <returns></returns>
        public double GetLeftStraightness(double _currentvalue)
        {
            return GetStraightness( true, _currentvalue );
        }

        /// <summary>
        /// RH 직진도를 구함
        /// </summary>
        /// <param name="_currentvalue">현재 두점의 길이</param>
        /// <returns></returns>
        public double GetRightStraightness(double _currentvalue)
        {
            return GetStraightness( false, _currentvalue );
        }


        #endregion [ 직진도 ]

        #region [  특정 회전 각 - L 산포 ]
        // 특정 회전 각 - L 산포
        public double CalculationChangeCoordinate_LH_L1_X()
        {
            return CalculationChangeCoordinate( this.pos_LH_X_L1, this.pos_Angle_LH_L1 );
        }

        // public double  CalculationChangeCoordinate_LH_L1_Y()
        //{
        //    return CalculationChangeCoordinate(this.pos_LH_Y_L1, this.pos_Angle_LH_L1);
        //}

        public double CalculationChangeCoordinate_LH_L2_X()
        {
            return CalculationChangeCoordinate( this.pos_LH_X_L2, this.pos_Angle_LH_L2 );
        }

        // public double  CalculationChangeCoordinate_LH_L2_Y()
        //{
        //    return CalculationChangeCoordinate(this.pos_LH_Y_L2, this.pos_Angle_LH_L2);
        //}

        public double CalculationChangeCoordinate_RH_L1_X()
        {
            return CalculationChangeCoordinate( this.pos_RH_X_L1, this.pos_Angle_RH_L1 );
        }

        // public double  CalculationChangeCoordinate_RH_L1_Y()
        //{
        //    return CalculationChangeCoordinate(this.pos_RH_Y_L1, this.pos_Angle_RH_L1);
        //}

        public double CalculationChangeCoordinate_RH_L2_X()
        {
            return CalculationChangeCoordinate( this.pos_RH_X_L2, this.pos_Angle_RH_L2 );
        }

        #endregion [  특정 회전 각 - L 산포 ]

        #region [ 세점 길이 ]

        // public double  CalculationChangeCoordinate_RH_L2_Y()
        //{
        //    return CalculationChangeCoordinate(this.pos_RH_Y_L2, this.pos_Angle_RH_L2);
        //}

        // 기준정보에서 값을 받아서 min 값 계산 - 기준정보에서 설정한 중심을 기준 구함
        public double CalculationMinDistance(double _x1, double _y1, double _z1, double _x2, double _y2, double _z2, double _h)
        {
            return CalculationDistance( _x1, _y1, _z1 + _h, _x2, _y2, _z2 );
        }

        #endregion [ 세점 길이 ]

        // 기준정보에서 값을 받아서 Max 값 계산 - 기준정보에서 설정한 중심에서 반지름 만큼 좌우로 이동시킨다.
        public double CalculationMaxDistance(double _x1, double _y1, double _z1, double _r1, double _x2, double _y2, double _z2, double _r2, double _h)
        {
            return CalculationDistance( _x1 + _r1, _y1, _z1 + _h, _x2 - _r2, _y2, _z2 );
        }

        // 기준정보에서 값을 받아서 min 값 계산 - 기준정보에서 설정한 중심을 지준 구함
        public double CalculationMinTowPointDistance(double _x1, double _y1, double _x2, double _y2, double _h)
        {
            return this.CalculationTowPointDistance( _x1, _y1 + _h, _x2, _y2 );
        }

        // 기준정보에서 값을 받아서 Max 값 계산 - 기준정보에서 설정한 중심에서 반지름 만큼 좌우로 이동시킨다.
        public double CalculationMaxTowPointDistance(double _x1, double _y1, double _z1, double _r1, double _x2, double _y2, double _z2, double _r2, double _h)
        {
            return CalculationTowPointDistance( _x1 + _r1, _y1 + _h, _x2 - _r2, _y2 );
        }

        // 두점 사이의 각도
        public double CalculationTowPointAngle(double _x1, double _y1, double _x2, double _y2)
        {
            double dx = _x2 - _x1;
            double dy = _y2 - _y1;

            double rad = Math.Atan2( dx, dy );
            double degree = ( rad * 180 ) / Math.PI;

            return degree;
        }

        // 두점의 길이
        public double CalculationTowPointDistance(double _x1, double _y1, double _x2, double _y2)
        {
            double diffX, diffY;
            double r;

            diffX = _x2 - _x1;
            diffY = _y2 - _y1;

            r = Math.Sqrt( diffX * diffX + diffY * diffY );
            return r;
        }

        //   평면 기울기
        public double CalculationMaxValeAngle(double _x1, double _y1, double _r1, double _x2, double _y2, double _r2, double _h)
        {
            return CalculationTowPointAngle( _x1 + _r1, _y1 + _h, _x2 - _r2, _y2 );
        }


        // // 세점 기울기
        //        public double  CalculationMaxValeAngle(double _x1, double _y1, double _z1, double _r1, double _x2, double _y2, double _z2, double _r2, double _h)
        //        {
        //           // return CalcualtionetTowPointAngle(_x1 + _r1, _z1 + _h, _x2 - _r2, _z2);
        //           // return CalculationDistance(_x1+_r1,_y1,_z1+_h ,_x2-_r2,_y2,_z2);
        //            return CalcualtionetAngle( _x1 + _r1,_y1, _z1 + _h, _x2 - _r2, _y2,_z2);
        //        }

        //        public double CalcualtionetAngle( double _x1, double _y1, double _z1,  double _x2, double _y2, double _z2 )
        //        {
        //            return 0;
        //        }

    }
}
