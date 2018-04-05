using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interop.Common.CVB.Util
{
    public class UtilShapeFinder
    {
        //input : 영상  
        //        mcf 파일 경로
        //        검사영역
        //        검사종료 ( search ,SrarchALL , read Token

        //output : Quality
        //         className
        //         찾은 위치


        // --> UI 에서
        //판단
        //파일 저장
        public static void RunProcess(string _sf2Path
                                                , Cvb.Image.IMG _orgImage
                                                , Interop.Common.CVB.cStruct.stCvbArea _td
                                                , int _MaxNumSolutions
                                                , int _MinimalThreshold
                                                , int _Precision
                                                , int _RelativeThreshold
                                                , int _LocXY
                                                , double _Quality
                                                , double _ScaleMM // 픽셀당 mm 
                                                , bool _isFeatures
                                                , ref AxCVDISPLAYLib.AxCVdisplay _dsp
                                                , out  Interop.Common.CVB.cStruct.stShapeFinderOutputtData[] _outData
                                                , out string errMessage
                                        )
        {
            RunProcess(_sf2Path
                           , _orgImage, _td
                           , _MaxNumSolutions
                            , _MinimalThreshold
                            , _Precision
                            , _RelativeThreshold
                            , _LocXY
                            , _Quality
                            , _ScaleMM // 픽셀당 mm 
                            , _isFeatures
                            , ref  _dsp
                            , out    _outData
                            , out   errMessage, true);
        }

        public static void RunProcess(string _sf2Path
                                                , Cvb.Image.IMG _orgImage
                                                , Interop.Common.CVB.cStruct.stCvbArea _td
                                                , int _MaxNumSolutions
                                                , int _MinimalThreshold
                                                , int _Precision
                                                , int _RelativeThreshold
                                                , int _LocXY
                                                , double _Quality
                                                , double _ScaleMM // 픽셀당 mm 
                                                , bool _isFeatures
                                                , ref AxCVDISPLAYLib.AxCVdisplay _dsp
                                                , out  Interop.Common.CVB.cStruct.stShapeFinderOutputtData[] _outData
                                                , out string errMessage
                                                , bool _isDisplayClear
                                         )
        {

            Cvb.SharedSF m_Sf = new Cvb.SharedSF();
            Cvb.SharedPixelList m_SharedPixelList = new Cvb.SharedPixelList();
            Cvb.SharedImg sResultImage = new Cvb.SharedImg(); ;
            int overlayIndex = 0;
            errMessage = string.Empty;
            _outData = new cStruct.stShapeFinderOutputtData[_MaxNumSolutions];

            try
            {

                Cvb.ShapeFinder.SF2.TSearchAllParams searchParams = new Cvb.ShapeFinder.SF2.TSearchAllParams();
                searchParams = Cvb.ShapeFinder.SF2.TSearchAllParams.Default;
                searchParams.MaxNumSolutions = _MaxNumSolutions;  //찾을 개체 수
                searchParams.MinimalThreshold = _MinimalThreshold;  //Threshold (0 ~ 255)
                searchParams.Precision = _Precision; //검색 정밀도 0 : fast, poor accuracy, 1= medium, medium accuracy, 2=high accuracy
                searchParams.RelativeThreshold = _RelativeThreshold; //베스트 모델 대비 상대적 Threshold (0 ~ 100)
                searchParams.LocXY = _LocXY;   //결과 개체간의 픽셀
                //searchParams.LocA = 0x7FFFFFFF; //default값(예비)
                //searchParams.LocR = 0x7FFFFFFF; //default값(예비)


                for (int inx = 0; inx < _outData.Length; inx++)
                {
                    _outData[inx].CenterX = 0;
                    _outData[inx].CenterY = 0;
                    _outData[inx].Qty = 0;
                    _outData[inx].RadiusMM = 0;
                    _outData[inx].Result = false;
                }

                if (Cvb.ShapeFinder.LoadSF(out m_Sf, _sf2Path))
                {
                    bool bResult = Cvb.ShapeFinder.SF2.SetSF2SearchAllPars(m_Sf, searchParams);

                    if (bResult)
                    {
                        Cvb.ShapeFinder.SF2.TSFSolution Solution = new Cvb.ShapeFinder.SF2.TSFSolution();
                        double[] temp = Solution.ToDoubleArray();

                        // search
                        Cvb.SharedPixelList sf2Results;
                        bResult = Cvb.ShapeFinder.SF2.SF2Search(m_Sf, _orgImage, 0, (int)_td.Left, (int)_td.Top, (int)_td.Right, (int)_td.Bottom, out sf2Results);

                        if (bResult)
                        {
                            // get number of objects found
                            int resultCount = Cvb.Image.PixelListCount(sf2Results);

                            //System.Diagnostics.Debug.WriteLine("찾은 수 : " + resultCount);


                            if (resultCount > 0)
                            {
                                if (_isFeatures && _dsp != null && _isDisplayClear)
                                {
                                    _dsp.RemoveAllOverlays();
                                    _dsp.RemoveAllLabels();
                                    _dsp.RemoveAllOverlayObjects();
                                }


                                for (int i = 0; i < resultCount; i++)
                                {
                                    // Cvb.Image.ListPixelEx(sf2Results, 0, out temp);
                                    // Solution = Cvb.ShapeFinder.SF2.TSFSolution.FromDoubleArray(temp);

                                    // _outData[i].CenterX = Solution.X; ;
                                    // _outData[i].CenterY = Solution.Y; ;
                                    // _outData[i].Qty  = Solution.Z; ;
                                    //_outData[i].Result  =   (_Quality <_outData[i].Qty) ? true:false;



                                    double[] data; //X,Y,Z, A, R
                                    Cvb.Image.ListPixelEx(sf2Results, i, out data);
                                    // calculate the scaledValue in mm from the scale of the circle and 
                                    // the dScaleFactorPerMM which was acquired with the SearchResults and 
                                    // the real world values


                                    _outData[i].CenterX = ConvertA(data[0]);
                                    _outData[i].CenterY = ConvertA(data[1]);
                                    _outData[i].Qty = ConvertA(data[2]);
                                    _outData[i].Result = (_Quality < _outData[i].Qty) ? true : false;
                                    _outData[i].Radius = ConvertA(data[4]);
                                    _outData[i].RadiusMM = ConvertA(data[4] / _ScaleMM);  // 반지름... 

                                    if (_isFeatures && _dsp != null)
                                    {
                                        Cvb.SharedPixelList coarseFeatures;
                                        int coarseScale;
                                        Cvb.ShapeFinder.SF2.GetSF2Features(m_Sf, out m_SharedPixelList, out coarseFeatures, out coarseScale);

                                        ShowFeatures(data[0], data[1], 0, data[4], overlayIndex++, m_SharedPixelList, ref _dsp);

                                    }
                                }
                            }
                            else
                            {
                                errMessage = string.Format("UtilShapeFinder.RunProcess: Object1 Not Found ");
                            }
                        }

                        if (_isFeatures && _dsp != null)
                        {
                            _dsp.Refresh();
                        }
                    }
                    else
                    {
                        errMessage = string.Format("UtilShapeFinder.RunProcess: Object2 Not Found ");
                    }
                }
                else
                {
                    errMessage = string.Format("UtilShapeFinder.RunProcess: SF2 Not Found: SF2:{0}", _sf2Path);
                }
            }
            catch (Exception ex)
            {
                errMessage = "UtilShapeFinder.RunProcess: " + ex.ToString();
            }
            finally
            {
                if (m_Sf != null) m_Sf.Dispose();
                if (sResultImage != null) sResultImage.Dispose();
                if (m_SharedPixelList != null) m_SharedPixelList.Dispose();

            }
        }

        private static double ConvertA(double _v)
        {
            return System.Math.Truncate(_v * 100) * 0.01; // 소수점 아래 두자리까지만
        }

        private static void ShowFeatures(double posX, double posY, double Rotation, double Scale, int index, Cvb.SharedPixelList m_FineFeatures, ref AxCVDISPLAYLib.AxCVdisplay _dsp)
        {
            // create a transformation matrix based on scale and rotation
            Cvb.Image.TMatrix matrix;
            // normally you have to subtract the initial angle used at training, the according line would be
            //matrix.A11 = Math.Cos((Rotation - Result.InitialAngle) * Math.PI / 180.0) * Scale;
            // for in this sample the initial angle is 0.
            matrix.A11 = Math.Cos(Rotation * Math.PI / 180.0) * Scale;
            matrix.A12 = -Math.Sin(Rotation * Math.PI / 180.0) * Scale;
            matrix.A21 = Math.Sin(Rotation * Math.PI / 180.0) * Scale;
            matrix.A22 = Math.Cos(Rotation * Math.PI / 180.0) * Scale;
            // create a copy of the feature pixel list
            Cvb.SharedPixelList transformedFeatures;
            Cvb.Image.CopyPixelList(m_FineFeatures, out transformedFeatures);
            // transform it based on the matrix
            Cvb.Image.TransformPixelListMatrix(transformedFeatures, matrix, posX, posY);
            // show it
            using (Cvb.Plugin.TPixelListPlugInData PlugInData = new Cvb.Plugin.TPixelListPlugInData(transformedFeatures, Cvb.Plugin.TPenStyle.SOLID, 1))
            {
                // add overlay object for fine features
                _dsp.AddOverlayObjectNET("PixelList", "Fine Feature", false, false, Cvb.Plugin.ColorToInt32(System.Drawing.Color.Red), Cvb.Plugin.ColorToInt32(System.Drawing.Color.Red), false, index, PlugInData.PixelList.ToInt32(), PlugInData.ToIntPtr());
            }
        }



        //public static bool ExcuteShapeFinder(string _sf2Path
        //                                                    , Cvb.Image.IMG _orgImage
        //                                                    , Interop.Common.CVB.cStruct.stCvbArea _td
        //                                                    , int _MaxNumSolutions
        //                                                    , int _MinimalThreshold
        //                                                   , int _Precision
        //                                                   , int _RelativeThreshold
        //                                                   , int _LocXY
        //                                                   , double _Quality
        //                                                   , double _ScaleMM // 픽셀당 mm 
        //                                                    , bool _isFeatures
        //                                                    , ref AxCVDISPLAYLib.AxCVdisplay _dsp
        //                                                    , out  Interop.Common.CVB.cStruct.stShapeFinderOutputtData[] _outData
        //                                                    , out string _message
        //                                    )
        //{
        //    _message = "";

        //    RunProcess(_sf2Path, _orgImage, _td, _MaxNumSolutions, _MinimalThreshold, _Precision, _RelativeThreshold, _LocXY, _Quality, _ScaleMM, _isFeatures, ref _dsp, out  _outData, out _message);

        //    //_dPq = System.Math.Truncate(_dPq * 100) *0.01; // 소수점 아래 두자리까지만
        //    //// 판정
        //    //_Result = (_dPq >= _Quality);

        //    return String.IsNullOrEmpty(_message);
        //}
    }
}
