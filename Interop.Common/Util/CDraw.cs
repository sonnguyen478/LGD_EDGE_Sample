using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interop.Common.Util
{
    public sealed class CDraw
    {

        /// <summary>
        ///  Tow View 이미지 좌표로 변환
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int ConvertDrawPoint(double target)
        {
            return ConvertDrawPointLocal( target, false, 5 );
        }

        /// <summary>
        ///  Tow View 이미지 좌표로 변환
        /// </summary>
        /// <param name="target">변환 값</param>
        /// <param name="convert">true:상하좌우 뒤집어서 좌표 계산</param>
        /// <returns>int</returns>
        public static int ConvertDrawPoint(double target, bool convert)
        {
            return ConvertDrawPointLocal( target, convert, 5 );
        }

        /// <summary>
        ///  Tow View 이미지 좌표로 변환
        /// </summary>
        /// <param name="target">변환 값</param>
        /// <param name="convert">false:</param>
        /// <param name="_magnification">배율 , 기본: 5 </param>
        /// <returns></returns>
        public static int ConvertDrawPoint(double target, bool convert, double _magnification)
        {
            return ConvertDrawPointLocal( target, convert, _magnification );
        }

        private static int ConvertDrawPointLocal(double target, bool convert, double _magnification)
        {
            int resultPT = 0;
            double restD = 0;

            try
            {
                if (convert == true)
                {
                    restD = Math.Abs(target - 28);
                }
                else
                {
                    restD = (target);
                }

                resultPT = (int)( restD * _magnification );

                restD = ( 16 - target ) * 140;

                resultPT = (int)( restD * 0.25 );
            }
            catch ( Exception ex )
            {
                cLog.FileWrite_Str( "좌표 설정 에러 : " + ex.Message.ToString(), cLog.eLogType.EXCEPTION );
                cLog.FileWrite_Str( "좌표 설정 에러 : " + ex.Message.ToString(), cLog.eLogType.LOG );
                resultPT = 0;
            }
            return resultPT;
        }


        public static int ConvertDrawPoint(double target, double _center)
        {
            return ConvertDrawPointLocal( target, _center, false );
        }

        private static int ConvertDrawPointLocal(double target, double _center, bool _convert)
        {
            int resultPT = 0;

            try
            {
                // 이미지의 Size  가 (140,140)
                // 특정 중심좌표로 중심으로 얼마큼 이동 했는지를 구한다.
                // 0.25 = 28/140 +0.05  이다.==> 센스 받는 값이 28 까지 이면 이미지 픽셀이 140 으로 나눈고 마진을 주었다.
                // 0.167 = 기준 원 (0.75mm) 의 반지름이 22픽셀 이므로 X축 좌표는 70-22=48 가된다.
                //          따라서 중심에서 -0.75 한 값이 48가 되도록 조정 하여였다.
                //
                 
                resultPT = 70 - (int)( ( target - _center ) * 140 *0.167) * ( _convert ? 1 : -1 );
                //resultPT = 105 - (int)( ( target - _center ) * 140 * 0.25 ) * ( _convert ? 1 : -1 );
            }
            catch ( Exception ex )
            {
                cLog.FileWrite_Str( "좌표 설정 에러 : " + ex.Message.ToString(), cLog.eLogType.EXCEPTION );
                cLog.FileWrite_Str( "좌표 설정 에러 : " + ex.Message.ToString(), cLog.eLogType.LOG );
                resultPT = 0;
            }

            return resultPT;
        }

        /// <summary>
        ///  두점 직선 그리기
        /// </summary>
        /// <param name="grp"></param>
        /// <param name="pt01"></param>
        /// <param name="pt02"></param>
        public static void DrawTowPointLine(ref System.Drawing.Graphics grp, System.Drawing.Point pt01, System.Drawing.Point pt02)
        {
            DrawTowPointLineLocal( ref grp, pt01, pt02 );
        }
        private static void DrawTowPointLineLocal(ref System.Drawing.Graphics grp, System.Drawing.Point pt01, System.Drawing.Point pt02)
        {
            try
            {
                using ( System.Drawing.Pen pen = new System.Drawing.Pen( System.Drawing.Color.Blue, 5 ) )
                {
                    //pen.StartCap = System.Drawing.Drawing2D.LineCap.AnchorMask;
                    pen.StartCap = System.Drawing.Drawing2D.LineCap.RoundAnchor;
                    pen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;

                    grp.DrawLine( pen, pt01, pt02 );
                }
            }
            catch ( Exception ex )
            {
                cLog.FileWrite_Str( "방향 라인 설정 에러 : " + ex.Message.ToString(), cLog.eLogType.EXCEPTION );
                cLog.FileWrite_Str( "방향 라인 설정 에러 : " + ex.Message.ToString(), cLog.eLogType.LOG );
            }
        }

        /// <summary>
        /// Graphics  이미지에 글자 표기
        /// </summary>
        /// <param name="grp"></param>
        /// <param name="markColor">색상</param>
        /// <param name="markPT">Point</param>
        /// <param name="markData">string</param>
        public static void DrawMarkPoint(ref System.Drawing.Graphics grp, System.Drawing.Color markColor, System.Drawing.Point markPT, string markData)
        {
            DrawMarkPointLocal( ref grp, markColor, markPT, markData );
        }
        private static void DrawMarkPointLocal(ref System.Drawing.Graphics grp, System.Drawing.Color markColor, System.Drawing.Point markPT, string markData)
        {
            try
            {
                using ( System.Drawing.Pen pen = new System.Drawing.Pen( markColor, 5 ) )
                {
                    grp.DrawEllipse( pen, markPT.X - 5, markPT.Y - 5, 10, 10 );
                }

                System.Drawing.Font drawFont = new System.Drawing.Font( "현대하모니", 15, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel );
                grp.DrawString( markData, drawFont, System.Drawing.Brushes.Black, new System.Drawing.PointF( (float)( markPT.X + 10 ), (float)( markPT.Y - 15 ) ) );
            }
            catch ( Exception ex )
            {
                cLog.FileWrite_Str( "좌표 위치 설정 에러 : " + ex.Message.ToString(), cLog.eLogType.EXCEPTION );
                cLog.FileWrite_Str( "좌표 위치 설정 에러 : " + ex.Message.ToString(), cLog.eLogType.LOG );
            }
        }

        /// <summary>
        /// TowViwe 기본 이미지 그리기
        /// </summary>
        /// <param name="grp"></param>
        /// <param name="outerPT">외곽 원 중심</param>
        /// <param name="centerPT">내각 원 중심</param>
        /// <param name="radius">외곽 원 반지름</param>
        public static void DrawDefaultImage(ref System.Drawing.Graphics grp, System.Drawing.Point outerPT, System.Drawing.Point centerPT, int radius)
        {
            DrawDefaultImageLocal( ref grp, outerPT, centerPT, radius );
        }
        private static void DrawDefaultImageLocal(ref System.Drawing.Graphics grp, System.Drawing.Point outerPT, System.Drawing.Point centerPT, int _radius)
        {
            try
            {
                using ( System.Drawing.Pen pen = new System.Drawing.Pen( System.Drawing.Color.Black, 2 ) )
                {
                    int radius = _radius * 2 - 2;

                    //grp.DrawEllipse(pen, outerPT.X-1, outerPT.Y, radius, radius);
                    grp.DrawEllipse( pen, centerPT.X - _radius, centerPT.Y - _radius, radius, radius ); 

                }

                using ( System.Drawing.Pen pen = new System.Drawing.Pen( System.Drawing.Color.Red, 3 ) )
                {
                    grp.DrawEllipse( pen, centerPT.X - 1, centerPT.Y - 1, 1, 1 );
                }


                //using (System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Blue, 2))
                //{
                //    int radius = _radius * 2 - 2;
                     
                //    radius = (int)(140 * 0.234 * 0.9) * 2 ;
                //    grp.DrawEllipse(pen, centerPT.X - radius/2, centerPT.Y - radius/2, radius-2, radius-2);

                     
                //}

                //using (System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Pink, 2))
                //{
                //    int radius = _radius * 2 - 2;
 
                //    radius = (int)(140 * 0.234 * 1) * 2 - 2;
                //    grp.DrawEllipse(pen, centerPT.X - radius / 2, centerPT.Y - radius / 2, radius - 2, radius - 2);

                //} 

            }
            catch ( Exception ex )
            {
                cLog.FileWrite_Str( "기본 이미지 설정 에러 : " + ex.Message.ToString(), cLog.eLogType.EXCEPTION );
                cLog.FileWrite_Str( "기본 이미지 설정 에러 : " + ex.Message.ToString(), cLog.eLogType.LOG );
            }
        }

        /// <summary>
        /// Graphics 객체에 원을 그림
        /// </summary>
        /// <param name="grp">Graphics</param>
        /// <param name="_w">폭</param>
        /// <param name="_h">높이</param>
        public static void DrawDefaultImage(ref System.Drawing.Graphics grp, int _w, int _h)
        {
            System.Drawing.Point outerPT = new System.Drawing.Point( 0, 0 );
            System.Drawing.Point centerPT = new System.Drawing.Point( (int)( ( _w * 0.5 ) ), (int)( ( _h * 0.5 ) ) );
            int radius = 0;

            if ( _w > _h ) radius = (int)( _h * 0.5 );
            else
            {
                //0.23 =   가로길이(140피셀) / 좌우 전체 길이
                //     = 140/6  ( 6 을 실제 전체를 6mm 로 본다 )
                // 따라서 원은 0.75mm 이므로 곱한다.
                radius = (int)(_w * 0.234 * 0.75); 
            }

            DrawDefaultImageLocal( ref grp, outerPT, centerPT, radius );
        }












    }
}
