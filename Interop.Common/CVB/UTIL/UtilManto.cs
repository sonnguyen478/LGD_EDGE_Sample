using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interop.Common.CVB.Util
{
    public class UtilManto
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

        // MCSearch(Manto.MCF mcf, Image.IMG img, int left, int top, int right, int bottom, bool vote, bool dotSensitivity, bool createResultImage, out double xPos, out double yPos, out double quality, out string classID, out Image.IMG resultImage);

        ////public static void RunProcess(string _mcfPath
        ////                                    , Cvb.Image.IMG _orgImage
        ////                                     , Interop.Common.CVB.cStruct.stCvbArea _td
        ////                                    , ref double _dPx
        ////                                    , ref double _dPy
        ////                                    , ref double _dPQuality
        ////                                    , out string _classID
        ////    //, out Cvb.Image.IMG _resultImage
        ////                                    , out string errMessage
        ////                                )
        ////{
        ////    //Cvb.Manto.Search.MCSearch

        ////    bool searchManto = false;
        ////    Cvb.SharedMCF MCF = new Cvb.SharedMCF();
        ////    //Cvb.Manto.MCF MCF = new Cvb.Manto.MCF(0);
        ////    // Cvb.Image.IMG sResultImage ;
        ////    Cvb.SharedImg sResultImage = new Cvb.SharedImg(); ;

        ////    errMessage = string.Empty;
        ////    _classID = string.Empty;
        ////    // _resultImage =null;

        ////    try
        ////    {

        ////        Cvb.Manto.Search.LoadMC(_mcfPath, out MCF);
        ////        if (MCF == 0)
        ////        {
        ////            errMessage = string.Format("UtilManto.RunProcess: MCF File Not Found : {0}", _mcfPath);
        ////        }
        ////        else
        ////        {
        ////            searchManto = Cvb.Manto.Search.MCSearch(MCF, _orgImage
        ////                                                     , (int)_td.Left, (int)_td.Top, (int)_td.Right, (int)_td.Bottom
        ////                                                     , false, false, false
        ////                                                     , out _dPx, out _dPy, out _dPQuality
        ////                                                     , out _classID, out sResultImage
        ////                                                   );
        ////            if (searchManto == false)
        ////            {
        ////                errMessage = string.Format("UtilManto.RunProcess: Not Search : MCF:{0}", _mcfPath);
        ////            }

        ////            _classID = _classID.ToUpper();

        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        searchManto = false;
        ////        errMessage = "UtilManto.RunProcess: " + ex.ToString();
        ////    }
        ////    finally
        ////    {
        ////       if( MCF != null )  MCF.Dispose();
        ////        if( sResultImage != null )       sResultImage.Dispose();
        ////    }

        ////    //if (MCF != 0) Cvb.Manto.Search.ReleaseMC(MCF);
        ////    //return searchManto;
        ////}



        public static bool ExcuteManto(string _mcfPath
                                                  , Cvb.Image.IMG _img
                                                 , Interop.Common.CVB.cStruct.stCvbArea _td
                                                 , double _Quality
                                                 , ref bool _Result
                                                 , ref double _dPx
                                                 , ref double _dPy
                                                 , ref double _dPq
                                                 , ref string _ClassID
                                                 , ref string _message

)
        {
            _message = "";

            //RunProcess(_mcfPath, _img, _td, ref _dPx, ref _dPy, ref _dPq, out _ClassID, out _message);


            _dPq = System.Math.Truncate(_dPq * 100) *0.01; // 소수점 아래 두자리까지만

            // 판정
            _Result = (_dPq >= _Quality);

            return String.IsNullOrEmpty(_message);

        }

    }



}
