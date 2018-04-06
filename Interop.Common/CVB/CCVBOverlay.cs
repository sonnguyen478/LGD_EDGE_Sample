using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Interop.Common.CVB
{
    public sealed class CCVBOverlay
    {
        public enum eOjbectType
        {
            SMARTRECTANGLE = 0,
            RECTANGLE,
            TEXTOUT
        }

        //bool isSmartRectangle = true;

        private static int iRectWidth = 50;
        private static int iRectHeight = 50;

        //public static int IRectWidth
        //{
        //    get;
        //    set;
        //}

        public static int IRectWidth
        {
            get { return iRectWidth; }
            set { iRectWidth = value; }
        }

        public static int IRectHeight
        {
            get { return iRectHeight; }
            set { iRectHeight = value; }
        }


        private static Color colorFG = Color.Yellow;//65535
        private static Color colorHL = Color.Blue;//16711680

        public static Color ColorFG
        {
            get { return colorFG; }
            set { colorFG = value; }
        }

        public static Color ColorHL
        {
            get { return colorHL; }
            set { colorHL = value; }
        }

        #region CVBUtil로 넘길거임
        private static Cvb.Image.TArea mAreaSelGet(AxCVDISPLAYLib.AxCVdisplay _dp)
        {
            Cvb.Image.TArea TArea = new Cvb.Image.TArea();

            _dp.GetSelectedArea(ref TArea.X0, ref TArea.Y0, ref TArea.X1, ref TArea.Y1, ref TArea.X2, ref TArea.Y2);

            return TArea;
        }

        private static void mAreaSelSet(AxCVDISPLAYLib.AxCVdisplay _dp, Cvb.Image.TArea _TArea)
        {
            _dp.SetSelectedArea(_TArea.X0, _TArea.Y0, _TArea.X1, _TArea.Y1, _TArea.X2, _TArea.Y2);
        }

        private static void mDisplayTArea(Cvb.Image.TArea _ta)
        {
            //System.Diagnostics.Debug.WriteLine(string.Format("X0:{0}, X1:{1}, X2:{2}, Y0:{3}, Y1:{4}, Y2:{5}", _ta.X0.ToString(), _ta.X1.ToString(), _ta.X2.ToString(), _ta.Y0.ToString(), _ta.Y1.ToString(), _ta.Y2.ToString()));
        }

        public static void mDisplayTDRect(Cvb.Image.TDRect _td)
        {
            //System.Diagnostics.Debug.WriteLine(string.Format("left:{0}, right:{1}, top:{2}, bottom:{3}", _td.Left, _td.Right, _td.Top, _td.Bottom));

            //label1.Text = _td.Left.ToString();
            //label2.Text = _td.Right.ToString();
            //label3.Text = _td.Top.ToString();
            //label4.Text = _td.Bottom.ToString();
        }

        public static Cvb.Image.TArea TAreaToInt(int _x0, int _y0, int _x1, int _y1, int _x2, int _y2)
        {
            Cvb.Image.TArea Area = new Cvb.Image.TArea();

            Area.X0 = _x0;
            Area.X1 = _x1;
            Area.X2 = _x2;

            Area.Y0 = _y0;
            Area.Y1 = _y1;
            Area.Y2 = _y2;

            return Area;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_x0">left</param>
        /// <param name="_y0">Top</param>
        /// <param name="_x2">Right</param>
        /// <param name="_y1">Bottom</param>
        /// <returns></returns>
        public static Cvb.Image.TDRect TDRectToInt(int _x0, int _y0, int _x2, int _y1)
        {
            Cvb.Image.TDRect Rect = new Cvb.Image.TDRect();

            Rect.Left = _x0;
            Rect.Top = _y0;
            Rect.Right = _x2;
            Rect.Bottom = _y1;

            return Rect;
        }

        public static Cvb.Image.TDRect TAreaToTDRect(Cvb.Image.TArea _Area)
        {
            Cvb.Image.TDRect Rect = new Cvb.Image.TDRect();

            Rect.Left = _Area.X0;
            Rect.Top = _Area.Y0;
            Rect.Right = _Area.X2;
            Rect.Bottom = _Area.Y1;

            return Rect;
        }

        public static Cvb.Image.TArea TDRectToTArea(Cvb.Image.TDRect _Rect)
        {
            Cvb.Image.TArea Area = new Cvb.Image.TArea();

            Area.X0 = _Rect.Left;
            Area.X1 = _Rect.Left;
            Area.X2 = _Rect.Right;

            Area.Y0 = _Rect.Top;
            Area.Y1 = _Rect.Bottom;
            Area.Y2 = _Rect.Top;

            return Area;
        }
        #endregion


        public static Cvb.Image.TDRect mAlignRowCol(int _inspSeq)
        {
            int iColCnt = 4;
            int iColCur = 0;
            int iSpace = 20;

            Cvb.Image.TDRect td = new Cvb.Image.TDRect();

            iColCur = _inspSeq % iColCnt;

            td.Left = (CCVBOverlay.iRectWidth * iColCur) + (iSpace * iColCur) + iSpace;
            td.Right = td.Left + CCVBOverlay.iRectWidth;

            td.Top = (CCVBOverlay.iRectHeight * (int)(_inspSeq / iColCnt)) + ((int)(_inspSeq / iColCnt) * iSpace) + iSpace;
            td.Bottom = td.Top + CCVBOverlay.iRectHeight;
            return td;
        }

        public static bool mSmartRectangleCheckPos(ref Cvb.Image.TDRect _td)
        {
            return mSmartRectangleCheckPos(ref _td, false);
        }

        public static bool mSmartRectangleCheckPos(ref Cvb.Image.TDRect _td, bool _isFixed)
        {
            bool isChg = false;
            double dTmp = 0d;

            //영역 이내 인지 확인
            if (_td.Left < 0)
            {
                _td.Left = 0;

                isChg = true;
            }

            if (_td.Top < 0)
            {
                _td.Top = 0;

                isChg = true;
            }
            //if (_td.Right < 0) _td.Right = 0;
            //if (_td.Bottom < 0) _td.Bottom = 0;

            //좌우가 맞는지 확인
            if (_td.Left > _td.Right)
            {
                dTmp = _td.Left;
                _td.Left = _td.Right;
                _td.Right = dTmp;

                isChg = true;
            }

            //위아래가 맞는지
            if (_td.Top > _td.Bottom)
            {
                dTmp = _td.Top;
                _td.Top = _td.Bottom;
                _td.Bottom = dTmp;

                isChg = true;
            }

            if (_isFixed == true)
            {
                if (_td.Left + iRectWidth != _td.Right)
                {
                    _td.Right = _td.Left + iRectWidth;

                    isChg = true;
                }

                if (_td.Top + iRectHeight != _td.Bottom)
                {
                    _td.Bottom = _td.Top + iRectHeight;

                    isChg = true;
                }
            }

            return isChg;
        }

        public static void SmartRectangleCheck(AxCVDISPLAYLib.AxCVdisplay _dp, int _id, int _idCnt, bool isHighLight)
        {
            //if (!_dp.HasOverlayObject(_id))
            //{
            //    Cvb.Image.TDRect Rect = new Cvb.Image.TDRect();
            //    Rect.Top = 0;
            //    Rect.Bottom = Rect.Top + iRectWidth;
            //    Rect.Left = 0;
            //    Rect.Right = Rect.Left + iRectHeight;

            //    SmartRectangleDraw(_dp, _id, TDRectToTArea(Rect), colorFG, colorHL, true);
            //}

            mDisplayTArea(SmartRectangleGetPosition(_dp, _id));

            if (isHighLight) OverlayObjectHighLight(_dp, _id, _idCnt);
        }

        //SmartRectangle 전용임.
        public static Cvb.Image.TArea SmartRectangleGetPosition(AxCVDISPLAYLib.AxCVdisplay _dp, int _id)
        {
            Cvb.Image.TArea Area = new Cvb.Image.TArea();

            if (_dp.HasOverlayObject(_id))
            {
                int x0 = 0, y0 = 0, x1 = 0, y1 = 0, x2 = 0, y2 = 0;

                _dp.GetOverlayObjectPosition(_id, 0, ref x0, ref y0);
                _dp.GetOverlayObjectPosition(_id, 2, ref x2, ref y2);
                _dp.GetOverlayObjectPosition(_id, 6, ref x1, ref y1);

                Area.X0 = x0; Area.X1 = x1; Area.X2 = x2;
                Area.Y0 = y0; Area.Y1 = y1; Area.Y2 = y2;

            }

            return Area;
        }

        public static void SmartRectangleDraw(AxCVDISPLAYLib.AxCVdisplay _dp, int _id, Cvb.Image.TArea _Area, Color _colorFG, Color _colorHL, bool _Drag)
        {
            try
            {
                if ( _dp.HasOverlayObject( _id ) ) return;

                Cvb.Image.TDRect Rect = new Cvb.Image.TDRect();

                Rect.Left = _Area.X0;
                Rect.Right = _Area.X2;
                //Rect.Right = Rect.Left + iRectWidth;
                Rect.Top = _Area.Y0;
                Rect.Bottom = _Area.Y1;
                //Rect.Bottom = Rect.Top + iRectHeight;

                //Rect.Top = _Area.Y0;
                //Rect.Bottom = _Area.Y1;

                Cvb.Image.PIXELLIST pixels = Cvb.Image.CreatePixelList( 3 );

                Cvb.Image.AddPixel( pixels, new double[] { Rect.Left, Rect.Top, 0 } );
                Cvb.Image.AddPixel( pixels, new double[] { 0, 0, 0 } );

                Cvb.Image.AddPixel( pixels, new double[] { Rect.Right, Rect.Top, 0 } );
                Cvb.Image.AddPixel( pixels, new double[] { 0, 0, 0 } );

                Cvb.Image.AddPixel( pixels, new double[] { Rect.Right, Rect.Bottom, 0 } );
                Cvb.Image.AddPixel( pixels, new double[] { 0, 0, 0 } );

                Cvb.Image.AddPixel( pixels, new double[] { Rect.Left, Rect.Bottom, 0 } );
                Cvb.Image.AddPixel( pixels, new double[] { 0, 0, 0 } );

                Cvb.Plugin.TPenStylePlugInData data = new Cvb.Plugin.TPenStylePlugInData( Cvb.Plugin.TPenStyle.SOLID, 1 );
                _dp.AddOverlayObjectNET( "SmartRectangle", string.Format( "SmartRectangle #{0:0} overlay", _id ), _Drag, false
                                                  , (int)System.Drawing.ColorTranslator.ToWin32( System.Drawing.Color.Yellow )
                                                  , System.Drawing.Color.Blue.ToArgb(), false, _id, pixels.ToInt32(), data.ToIntPtr() );

                Cvb.Image.ReleaseObj( pixels );
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex.Message.ToString() );
            }
        }

        public static void RectangleDraw(AxCVDISPLAYLib.AxCVdisplay _dp, int _id, Cvb.Image.TDRect _Rect, Color _colorFG, Color _colorHL, bool _Drag)
        {
            RectangleDraw(_dp, _id, _Rect, _colorFG, _colorHL, _Drag, false);
        }
        public static void RectangleDraw(AxCVDISPLAYLib.AxCVdisplay _dp, int _id, Cvb.Image.TDRect _Rect, Color _colorFG, Color _colorHL, bool _Drag, bool _isFill)
        {
            if (_dp.HasOverlayObject(_id)) return;

            Cvb.Image.PIXELLIST pixels = Cvb.Image.CreatePixelList(3);
            Cvb.Image.AddPixel(pixels, new double[] { _Rect.Left, _Rect.Top, 0 });
            Cvb.Image.AddPixel(pixels, new double[] { _Rect.Right, _Rect.Bottom, 0 });

            Cvb.Plugin.TPenStylePlugInData data = new Cvb.Plugin.TPenStylePlugInData(Cvb.Plugin.TPenStyle.SOLID, 1);

            _dp.AddOverlayObjectNET("Rectangle", String.Format("Rectangle #{0:0} overlay", _id), _Drag, false, Cvb.Plugin.ColorToInt32(_colorFG), Cvb.Plugin.ColorToInt32(_colorHL), _isFill, _id, pixels.ToInt32(), data.ToIntPtr());

            Cvb.Image.ReleaseObj(pixels);
        }


        public static void TargetDraw(AxCVDISPLAYLib.AxCVdisplay _dp, int _id, int _centerX, int _centerY, int _targetNum, int _targetWidth, int _targetHeight, Color _colorFG, Color _colorHL, bool _Drag)
        {
            if (_dp.HasOverlayObject(_id)) return;

            Cvb.Image.PIXELLIST pixels = Cvb.Image.CreatePixelList(3);
            Cvb.Plugin.TTargetPlugInData data = new Cvb.Plugin.TTargetPlugInData();
            Cvb.Image.AddPixel(pixels, new double[] { _centerX, _centerY, 0 });


            data.PenStyle = Cvb.Plugin.TPenStyle.SOLID;
            data.PenWidth = 2;
            data.NumTargets = _targetNum;
            data.TargetRadius = (int)(System.Math.Min(_targetWidth * 0.5f, _targetHeight * 0.5f) / _targetNum);
            data.CrossHairWidth = 10;
            data.TargetType = Cvb.Plugin.TTargetType.RECTANGLE;

            // Cvb.Plugin.TPenStylePlugInData data = new Cvb.Plugin.TPenStylePlugInData(Cvb.Plugin.TPenStyle.SOLID, 1);

            _dp.AddOverlayObjectNET("Target", String.Format("Target #{0:0} overlay", _id), _Drag, false, Cvb.Plugin.ColorToInt32(_colorFG), Cvb.Plugin.ColorToInt32(_colorHL), false, _id, pixels.ToInt32(), data.ToIntPtr());

            Cvb.Image.ReleaseObj(pixels);
        }

        //private void DrawTextOut(ByRef coords As Point())
        //다른 OverlayObject 와 ID가 같으면 이상해짐
        public static void TextOutDraw(AxCVDISPLAYLib.AxCVdisplay _dp, int _id, string _msg, int _x, int _y, int _fontSize, Color _colorFG, Color _colorHL, bool _Drag)
        {
            Cvb.Plugin.TTextOutPlugInData textData = new Cvb.Plugin.TTextOutPlugInData();

            Cvb.Image.PIXELLIST pixels = Cvb.Image.CreatePixelList(3);
            Cvb.Image.AddPixel(pixels, new double[] { _x, _y, 0 });

            // create additional data
            textData.FontSize = _fontSize;
            textData.FontWeight = Cvb.Plugin.TFontWeight.BOLD;
            //textData.Italic = false;
            //textData.Underline = false;
            //textData.Rotation = 0;
            textData.FontName = "Arial";
            textData.Text = _msg;
            // set the following flag to 1 to display a marker at the top left of the text
            textData.Flags = 0;

            // draw text
            _dp.AddOverlayObjectNET("TextOut", String.Format("TextOut #{0:0} overlay", _id), _Drag, false, Cvb.Plugin.ColorToInt32(_colorFG), Cvb.Plugin.ColorToInt32(_colorHL), false, _id, pixels.ToInt32(), textData.ToIntPtr());

            Cvb.Image.ReleaseObj(pixels); 
        }
 

        public static void CrosshairDraw(AxCVDISPLAYLib.AxCVdisplay _dp, int _id , int _centerX, int _centerY, int _width, int _height, Color _colorFG, Color _colorHL, bool _Drag)
        {
            Cvb.Image.PIXELLIST pixels = Cvb.Image.CreatePixelList(3);
            Cvb.Image.AddPixel(pixels, new double[] { _centerX, _centerY, 0 });
            Cvb.Image.AddPixel(pixels, new double[] { _width, _height, 0 });

            Cvb.Plugin.TPenStylePlugInData data = new Cvb.Plugin.TPenStylePlugInData(Cvb.Plugin.TPenStyle.SOLID, 1);
            _dp.AddOverlayObjectNET( "Crosshair" , String.Format( "Crosshair #{0:0} overlay" , _id ) , false , false , Cvb.Plugin.ColorToInt32( _colorFG ) , Cvb.Plugin.ColorToInt32( _colorHL ) , false , _id , pixels.ToInt32() , data.ToIntPtr() );

            Cvb.Image.ReleaseObj(pixels);

        }

        public static void LabelDraw(AxCVDISPLAYLib.AxCVdisplay _dp, string _msg, Color _c, int _id, double _dX, double _dY)
        {
            _dp.AddLabel(_msg, false, Cvb.Plugin.ColorToInt32(_c), _id, (int)_dX, (int)_dY);
        }

        public static void OverlayObjectHighLight(AxCVDISPLAYLib.AxCVdisplay _dp, int _id, int _maxCnt)
        {
            for (int i = 0; i < _maxCnt; i++)
            {
                _dp.HighLightOverlayObject(i, i == _id);
            }

            _dp.Refresh();
        }

        public static void mOverlayDisplayAllClear(ref AxCVDISPLAYLib.AxCVdisplay dsp)
        {
            dsp.RemoveAllLabels();
            dsp.RemoveAllOverlayObjects();
            dsp.RemoveAllOverlays();
            dsp.RemoveAllUserObjects();
            dsp.Refresh();
        }

        public static void OverlayObjectRemoveAll(AxCVDISPLAYLib.AxCVdisplay _dp)
        {
            _dp.RemoveAllOverlays();
            _dp.Refresh();
        }

        public static void OverlayObjectRemove(AxCVDISPLAYLib.AxCVdisplay _dp, int _id)
        {
            _dp.RemoveOverlayObject(_id);
            _dp.Refresh();
        }

        public static void OverlayLabelRemoveAll(AxCVDISPLAYLib.AxCVdisplay _dp)
        {
            _dp.RemoveAllLabels();
            _dp.Refresh();
        }

        public static void OverlayRemove(AxCVDISPLAYLib.AxCVdisplay _dp, int _id)
        {
            _dp.RemoveLabel(_id);
            _dp.Refresh();
        }

        public static void OverlayObjectMove(AxCVDISPLAYLib.AxCVdisplay _dp, int _id, int _x, int _y)
        {
            _dp.MoveOverlayObject(_id, _x, _y, true);
            _dp.Refresh();
        }




        //public static void OverlayRemoveAll(AxCVDISPLAYLib.AxCVdisplay _dp)
        //{
        //    _dp.RemoveAllLabels();
        //    _dp.RemoveAllOverlayObjects();
        //    _dp.RemoveAllOverlays();
        //    _dp.Refresh();
        //}

        //#region [ 아산/울산 거리 측정 용 ]

        //// 두개의 라이을 그리고 세로로 그린다. Return 값은 두직선의 평균 차이 Pixel 이다.
        ////  출력 형태:
        ////                -----------
        ////                     |
        ////                -----------
        //public static void mShowBlobDrawLine(AxCVDISPLAYLib.AxCVdisplay _display, Interop.Common.CVB.cStruct.stBlobResult[] _Upper, Interop.Common.CVB.cStruct.stBlobResult[] _Lower, ref  Interop.Common.CVB.cStruct.stPosion[] _posion)
        //{
        //    mShowBlobDrawLine(_display, _Upper, _Lower, ref _posion, false, false);
        //}

        //public static void mShowBlobDrawLine(AxCVDISPLAYLib.AxCVdisplay _display, Interop.Common.CVB.cStruct.stBlobResult[] _Upper, Interop.Common.CVB.cStruct.stBlobResult[] _Lower, ref  Interop.Common.CVB.cStruct.stPosion[] _posion, bool _isOffset)
        //{
        //    mShowBlobDrawLine(_display, _Upper, _Lower, ref _posion, _isOffset, false);
        //}

        //public static void mShowBlobDrawLine(AxCVDISPLAYLib.AxCVdisplay _display, Interop.Common.CVB.cStruct.stBlobResult[] _Upper, Interop.Common.CVB.cStruct.stBlobResult[] _Lower, ref  Interop.Common.CVB.cStruct.stPosion[] _posion, bool _isOffset, bool isLineShow)
        //{
        //    int centY_Lower, centY_Upper;
        //    int LineOffset = 25; // 좌우 연장

        //    centY_Lower = 0;
        //    centY_Upper = 0;

        //    _posion = new cStruct.stPosion[3];

        //    try
        //    {
        //        for (int inx = 0; inx < _posion.Length; inx++)
        //        {
        //            _posion[inx].X = 0;
        //            _posion[inx].Y = 0;
        //            _posion[inx].X2 = 0;
        //            _posion[inx].Y2 = 0;
        //        }

        //        if (_Upper != null)
        //        {
        //            _posion[0].X = _Upper[0].CenterX - LineOffset;
        //            _posion[0].Y = _Upper[0].CenterY + (int)(_Upper[0].Height / 2);
        //            _posion[0].X2 = _Upper[_Upper.Length - 1].CenterX + LineOffset;
        //            _posion[0].Y2 = _Upper[_Upper.Length - 1].CenterY + (int)(_Upper[_Upper.Length - 1].Height / 2);
        //            //mShowDrawLine(_display,  _Upper[0].CenterX , _Upper[0].CenterY + (int)(_Upper[0].Height / 2 ) ,_Upper[_Upper.Length - 1].CenterX, _Upper[_Upper.Length - 1].CenterY + (int)_Upper[_Upper.Length - 1].Height / 2    );
                   
        //            if (isLineShow == true) mShowDrawLine(_display, _posion[0].X, _posion[0].Y, _posion[0].X2, _posion[0].Y2);
        //        }

        //        if (_isOffset == true)
        //        {
        //            // 행거를 중심부터 표시 
        //            //mShowDrawLine(_display, _Lower[0].CenterX, _Lower[0].CenterY, _Lower[_Lower.Length - 1].CenterX, _Lower[_Lower.Length - 1].CenterY );

        //            _posion[1].X = _Lower[0].CenterX - LineOffset;
        //            _posion[1].Y = _Lower[0].CenterY;
        //            _posion[1].X2 = _Lower[_Lower.Length - 1].CenterX + LineOffset;
        //            _posion[1].Y2 = _Lower[_Lower.Length - 1].CenterY;

        //            if (isLineShow == true)  mShowDrawLine(_display, _posion[1].X, _posion[1].Y, _posion[1].X2, _posion[1].Y2);
        //        }
        //        else
        //        {
        //            //mShowDrawLine(_display, _Lower[0].CenterX, _Lower[0].CenterY - (int)_Lower[0].Height / 2, _Lower[_Lower.Length - 1].CenterX, _Lower[_Lower.Length - 1].CenterY - (int)_Lower[_Lower.Length - 1].Height / 2);
        //            _posion[1].X = _Lower[0].CenterX - LineOffset;
        //            _posion[1].Y = _Lower[0].CenterY - (int)(_Lower[0].Height / 2);
        //            _posion[1].X2 = _Lower[_Lower.Length - 1].CenterX + LineOffset;
        //            _posion[1].Y2 = _Lower[_Lower.Length - 1].CenterY - (int)(_Lower[_Lower.Length - 1].Height / 2);

        //            if (isLineShow == true)  mShowDrawLine(_display, _posion[1].X, _posion[1].Y, _posion[1].X2, _posion[1].Y2);
        //        }

        //        if (_Upper != null)
        //        {
        //            for (int inx = 0; inx < _Upper.Length; inx++)
        //            {
        //                centY_Upper += _Upper[inx].CenterY + (int)_Upper[inx].Height / 2;
        //            }
        //        }

        //        if (_Lower != null)
        //        {
        //            for (int inx = 0; inx < _Lower.Length; inx++)
        //            {
        //                centY_Lower += _Lower[inx].CenterY;

        //                if (_isOffset == false) centY_Lower -= (int)(_Lower[inx].Height / 2);
        //            }
        //        }

        //        try
        //        {
        //            if (_Lower != null && _Upper != null)
        //            {
        //                _posion[2].X = (int)(_Upper[0].CenterX + _Lower[_Lower.Length - 1].CenterX) / 2;
        //                _posion[2].Y = (int)(centY_Upper / _Upper.Length);
        //                _posion[2].X2 = (int)(_Upper[0].CenterX + _Lower[_Lower.Length - 1].CenterX) / 2;
        //                _posion[2].Y2 = (int)(centY_Lower / _Lower.Length);

        //                if (isLineShow == true)  Interop.Common.CVB.CCVBOverlay.mShowDrawLine(_display, _posion[2].X, _posion[2].Y, _posion[2].X2, _posion[2].Y2);
                     
        //            }
        //        }
        //        catch { }

        //        _display.Refresh();
        //    }
        //    catch (Exception ex)
        //    {
        //        //this.SetMessage(ex.ToString());
        //        System.Diagnostics.Debug.WriteLine(ex.ToString());
        //    }
        //}
 

        //// 상,하 결과로 상단 및 하단의 결과 위치 찾기
        //// 상단은 결과에서 아래쪽,
        //// 하단은  _isOffset= false 일때 결과에서 위쪽 , _isOffset=true  이면 결과에서 중심
        //public static void mGetCenter(Interop.Common.CVB.cStruct.stBlobResult[] _Upper, Interop.Common.CVB.cStruct.stBlobResult[] _Lower, out int centY_Upper, out int centY_Lower)
        //{
        //    mGetCenter(_Upper, _Lower, out centY_Upper, out centY_Lower, false);
        //}

        //public static void mGetCenter(Interop.Common.CVB.cStruct.stBlobResult[] _Upper, Interop.Common.CVB.cStruct.stBlobResult[] _Lower, out int centY_Upper, out int centY_Lower, bool _isOffset)
        //{
        //    centY_Lower = 0;
        //    centY_Upper = 0;

        //    try
        //    {
        //        if (_Upper != null)
        //        {
        //            for (int inx = 0; inx < _Upper.Length; inx++)
        //            {
        //                centY_Upper += _Upper[inx].CenterY + (int)_Upper[inx].Height / 2;
        //            }

        //            if (centY_Upper > 0) centY_Upper = centY_Upper / _Upper.Length;
        //        }

        //        if (_Lower != null)
        //        { 
        //            for (int inx = 0; inx < _Lower.Length; inx++)
        //            {
        //                //   centY_Lower += _Lower[inx].CenterY - ((_isOffset == true) ? 0 : (int)_Lower[inx].Height / 2);
        //                centY_Lower += _Lower[inx].CenterY;

        //                if (_isOffset == false) centY_Lower -= (int)(_Lower[inx].Height / 2);
        //            }

        //            if (centY_Lower > 0) centY_Lower = centY_Lower / _Lower.Length; 
        //        }
        //    }
        //    catch { }
        //}

        //// Blob 에서 찾은 결과를 라인으로 출ㄹ력
        //public static void mShowBlobDrawLine(AxCVDISPLAYLib.AxCVdisplay _display, int _x11, int _y11, int _x12, int _y12, int _x21, int _y21, int _x22, int _y22)
        //{
        //    Cvb.Image.PIXELLIST pixels = Cvb.Image.CreatePixelList(3);

        //    try
        //    {
        //        // 상단
        //        //pixels = Cvb.Image.CreatePixelList(3);
        //        Cvb.Image.AddPixel(pixels, new double[] { _x11, _y11 });
        //        Cvb.Image.AddPixel(pixels, new double[] { _x12, _y12 });
        //        _display.AddOverlayObjectNET("Line", "My Line", false, false, Cvb.Plugin.ColorToInt32(Color.Green), 255, false, 900, pixels.ToInt32(), IntPtr.Zero);

        //        // 하단
        //        pixels = Cvb.Image.CreatePixelList(3);
        //        Cvb.Image.AddPixel(pixels, new double[] { _x21, _y21 });
        //        Cvb.Image.AddPixel(pixels, new double[] { _x22, _y22 });

        //        _display.AddOverlayObjectNET("Line", "My Line", false, false, Cvb.Plugin.ColorToInt32(Color.Green), 255, false, 900, pixels.ToInt32(), IntPtr.Zero);

        //        // 상,하단을 수직 
        //        pixels = Cvb.Image.CreatePixelList(3);
        //        Cvb.Image.AddPixel(pixels, new double[] { (int)((_x11 + _x12) / 2), (int)((_y11 + _x12) / 2) });
        //        Cvb.Image.AddPixel(pixels, new double[] { (int)((_x11 + _x12) / 2), (int)((_y21 + _y22) / 2) });

        //        _display.AddOverlayObjectNET("Line", "My Line", false, false, Cvb.Plugin.ColorToInt32(Color.Green), 255, false, 900, pixels.ToInt32(), IntPtr.Zero);
        //        _display.Refresh();
        //    }
        //    catch (Exception ex)
        //    {
        //        //this.SetMessage(ex.ToString());
        //        System.Diagnostics.Debug.WriteLine(ex.ToString());
        //    }

        //    Cvb.Image.ReleaseObject(ref pixels);
        //}

        //public static void mShowBlobDrawLine(AxCVDISPLAYLib.AxCVdisplay _display, int _x1, int _y1, int _x2, int _y2)
        //{
        //    Cvb.Image.PIXELLIST pixels = Cvb.Image.CreatePixelList(3);

        //    try
        //    {
        //        Cvb.Image.AddPixel(pixels, new double[] { _x1, _y1 });
        //        Cvb.Image.AddPixel(pixels, new double[] { _x2, _y1 });

        //        _display.AddOverlayObjectNET("Line", "My Line", false, false, Cvb.Plugin.ColorToInt32(Color.Green), 255, false, 900, pixels.ToInt32(), IntPtr.Zero);

        //        pixels = Cvb.Image.CreatePixelList(3);
        //        Cvb.Image.AddPixel(pixels, new double[] { _x1, _y2 });
        //        Cvb.Image.AddPixel(pixels, new double[] { _x2, _y2 });

        //        _display.AddOverlayObjectNET("Line", "My Line", false, false, Cvb.Plugin.ColorToInt32(Color.Green), 255, false, 900, pixels.ToInt32(), IntPtr.Zero);


        //        pixels = Cvb.Image.CreatePixelList(3);
        //        Cvb.Image.AddPixel(pixels, new double[] { (int)((_x1 + _x2) / 2), _y1 });
        //        Cvb.Image.AddPixel(pixels, new double[] { (int)((_x1 + _x2) / 2), _y2 });

        //        _display.AddOverlayObjectNET("Line", "My Line", false, false, Cvb.Plugin.ColorToInt32(Color.Green), 255, false, 900, pixels.ToInt32(), IntPtr.Zero);
        //        _display.Refresh();
        //    }
        //    catch (Exception ex)
        //    {
        //        //this.SetMessage(ex.ToString());
        //    }

        //    Cvb.Image.ReleaseObject(ref pixels);

        //}

        //public static void mShowDrawLine(AxCVDISPLAYLib.AxCVdisplay _display, int _x1, int _y1, int _x2, int _y2)
        //{
        //    Cvb.Image.PIXELLIST pixels = Cvb.Image.CreatePixelList(3);

        //    try
        //    {
        //        Cvb.Image.AddPixel(pixels, new double[] { _x1, _y1 });
        //        Cvb.Image.AddPixel(pixels, new double[] { _x2, _y2 });

        //        _display.AddOverlayObjectNET("Line", "My Line", false, false, Cvb.Plugin.ColorToInt32(Color.Yellow), 255, false, 900, pixels.ToInt32(), IntPtr.Zero);


        //        _display.Refresh();
        //    }
        //    catch (Exception ex)
        //    {
        //        //this.SetMessage(ex.ToString());
        //    }

        //    Cvb.Image.ReleaseObject(ref pixels);

        //}

        //#endregion [ 아산/울산 거리 측정 용 ]

    }
}
