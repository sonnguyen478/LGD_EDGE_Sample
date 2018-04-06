using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interop.Common.CVB.Util
{
    public class UtilEdge
    {
        //public static void RunProcess(
        //                               Cvb.Image.IMG _orgImage
        //                            , Interop.Common.CVB.cStruct.stCvbArea _td
        //                            , Interop.Common.CVB.cEnum.eEdgeType _edgeType
        //                            , Interop.Common.CVB.cEnum.eEdgePositive _edgePositive
        //                            , double _threshold
        //                            , int _planIndex
        //                            , int _density
        //                            , int _maxEdge

        //                            , double _pair_Threshold1 // Edge Pair
        //                            , double _pair_Threshold2
        //                            , Interop.Common.CVB.cEnum.eEdgePositive _pair_PositiveEdge1
        //                            , Interop.Common.CVB.cEnum.eEdgePositive _pair_PositiveEdge2

        //                            , Cvb.Edge.EDGERESULTS _thresholdResult_In // thresHoldResilt
        //                            , double _thresholdResult_Min
        //                            , double _thresholdResult_Max

        //                            , ref AxCVDISPLAYLib.AxCVdisplay _dsp
        //                            , out  Interop.Common.CVB.cStruct.stEdgeOutputData _outData
        //                            , out string _errMessage
        //                                )
        //{
        //    RunProcess( 
        //                      _orgImage
        //                    , _td
        //                    , _edgeType
        //                    , _edgePositive
        //                    , _threshold
        //                    , _planIndex
        //                    , _density
        //                    , _maxEdge
        //                    , _pair_Threshold1
        //                    , _pair_Threshold2
        //                    , _pair_PositiveEdge1
        //                    , _pair_PositiveEdge2
        //                    , _thresholdResult_In
        //                    , _thresholdResult_Min
        //                    , _thresholdResult_Max
        //                    , ref  _dsp
        //                    , out    _outData
        //                    , out   _errMessage
        //                    , true);
        //}



        public static void RunProcess(Cvb.Image.IMG _orgImage
                                       , Interop.Common.CVB.cStruct.stCvbArea _td
            //, Interop.Common.CVB.cEnum.eEdgeType _edgeType
                                       , Interop.Common.CVB.cEnum.eEdgePositive _edgePositive
                                       , double _threshold
            // , int _planIndex
            // , int _density

                                       //, ref AxCVDISPLAYLib.AxCVdisplay _dsp
                                       , out  Interop.Common.CVB.cStruct.stEdgeOutputData _outData
                                       , out string _errMessage
                                )
        {
            RunProcess(
                              _orgImage
                            , _td
                            , Interop.Common.CVB.cEnum.eEdgeType.FIRST
                            , _edgePositive
                            , _threshold
                            , 0
                            , 500
                            , 0
                            , 0
                            , 0
                            , 0
                            , 0
                            , ( new Cvb.Edge.EDGERESULTS( (IntPtr)0 ) )
                            , 0
                            , 0
                            , out    _outData
                            , out   _errMessage
                            );

        }

        private static void RunProcess(Cvb.Image.IMG _orgImage
                                        , Interop.Common.CVB.cStruct.stCvbArea _td
                                        , Interop.Common.CVB.cEnum.eEdgeType _edgeType
                                        , Interop.Common.CVB.cEnum.eEdgePositive _edgePositive
                                        , double _threshold
                                        , int _planIndex
                                        , int _density
                                        , int _maxEdge

                                        , double _pair_Threshold1 // Edge Pair
                                        , double _pair_Threshold2
                                        , Interop.Common.CVB.cEnum.eEdgePositive _pair_PositiveEdge1
                                        , Interop.Common.CVB.cEnum.eEdgePositive _pair_PositiveEdge2

                                        , Cvb.Edge.EDGERESULTS _thresholdResult_In // thresHoldResilt
                                        , double _thresholdResult_Min
                                        , double _thresholdResult_Max

                                        //, ref AxCVDISPLAYLib.AxCVdisplay _dsp
                                        , out  Interop.Common.CVB.cStruct.stEdgeOutputData _outData
                                        , out string _errMessage
                                 )
        {
            _errMessage = string.Empty;
            _outData = new cStruct.stEdgeOutputData();

            bool edgePositive = ( _edgePositive == cEnum.eEdgePositive.Positive );
            Cvb.Image.TArea tarea = new Cvb.Image.TArea();

            try
            {
                tarea.X0 = _td.Left;
                tarea.Y0 = _td.Top;
                tarea.X1 = _td.Left;
                tarea.Y1 = _td.Bottom;
                tarea.X2 = _td.Right;
                tarea.Y2 = _td.Top;

                switch ( _edgeType )
                {
                    case cEnum.eEdgeType.ALL:
                        _outData.InspResult = Cvb.Edge.TFindAllEdges( _orgImage, _planIndex, _density, tarea, _threshold, edgePositive, -_maxEdge, out  _outData.ALL_EdgeResult );
                        break;

                    case cEnum.eEdgeType.FIRST:
                        _outData.InspResult = Cvb.Edge.TFindFirstEdge( _orgImage, _planIndex, _density, tarea, _threshold, edgePositive, out _outData.EdgeResult );
                        break;

                    case cEnum.eEdgeType.EDGEPAIR:
                        bool edgePositive1 = ( _pair_PositiveEdge1 == cEnum.eEdgePositive.Positive );
                        bool edgePositive2 = ( _pair_PositiveEdge2 == cEnum.eEdgePositive.Positive );

                        _outData.InspResult = Cvb.Edge.TFindEdgePair( _orgImage, _planIndex, _density, tarea, _pair_Threshold1, edgePositive1, out _outData.Pair_EdgeResult1, _pair_Threshold2, edgePositive1, out _outData.Pair_EdgeResult2 );
                        break;

                    //case cEnum.eEdgeType.ThresHoldResult:
                    //    break;

                    default:
                        _outData.InspResult = false;
                        _errMessage = "UtilEdge.RunProcess:  Not Type : " + _edgeType;
                        break;

                }


                if ( _outData.InspResult == false )
                {
                    _errMessage = string.Format( "UtilEdge.RunProcess: Object Not Found " );
                }

            }
            catch ( Exception ex )
            {
                _errMessage = "UtilEdge.RunProcess: " + ex.ToString();
                _outData.InspResult = false;
            }
            finally
            {


            }
        }





    }
}
