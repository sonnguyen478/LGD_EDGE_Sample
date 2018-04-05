using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interop.Common.CVB.Util
{
   public class UtilComm
   {
       #region [ TArea 로 변환 ]
       public static Cvb.Image.TArea ConvertToTArea(double _left, double _right, double _top, double _bottom)
       {
           Cvb.Image.TArea area = new Cvb.Image.TArea();

           area.X0 = _left;
           area.X1 = _left;
           area.X2 = _right;
           area.Y0 = _top;
           area.Y1 = _bottom;
           area.Y2 = _top;

           return area;
       }

       public static Cvb.Image.TArea ConvertToTArea(Interop.Common.CVB.cStruct.stCvbArea _area)
       {
           Cvb.Image.TArea area = new Cvb.Image.TArea();

           area.X0 = _area.Left;
           area.X1 = _area.Left;
           area.X2 = _area.Right;
           area.Y0 = _area.Top;
           area.Y1 = _area.Bottom;
           area.Y2 = _area.Top;

           return area;
       }

       public static Cvb.Image.TArea ConvertToTArea(Cvb.Image.TDRect _rect)
       {
           Cvb.Image.TArea area = new Cvb.Image.TArea();

           area.X0 = _rect.Left;
           area.X1 = _rect.Left;
           area.X2 = _rect.Right;
           area.Y0 = _rect.Top;
           area.Y1 = _rect.Bottom ;
           area.Y2 = _rect.Top;

           return area;
       } 

       
       #endregion [ TArea 로 변환 ]

       #region [ TDRect 로 변환 ]

       public static Cvb.Image.TDRect ConvertToRect(double _left, double _right, double _top, double _bottom)
       {
           Cvb.Image.TDRect rect = new Cvb.Image.TDRect();

           rect.Left = _left;
           rect.Top = _right;
           rect.Right = _top;
           rect.Bottom = _bottom;

           return rect;
       }

       public static Cvb.Image.TDRect ConvertToRect(Cvb.Image.TArea _area)
       {
           Cvb.Image.TDRect rect = new Cvb.Image.TDRect();

           rect.Left = _area.X0;
           rect.Top = _area.Y0;
           rect.Right = _area.X2;
           rect.Bottom = _area.Y1;

           return rect;
       }

       public static Cvb.Image.TDRect ConvertToRect(Interop.Common.CVB.cStruct.stCvbArea _area)
       {
           Cvb.Image.TDRect rect = new Cvb.Image.TDRect();

           rect.Left = _area.Left;
           rect.Top = _area.Top;
           rect.Right = _area.Right;
           rect.Bottom = _area.Bottom;

           return rect;
       }


       #endregion [ TDRect 로 변환 ]

       #region [ stCvbArea 로 변환 - 공통 형식으로 ]
       
       public static Interop.Common.CVB.cStruct.stCvbArea ConvertToCommonArea(Cvb.Image.TArea _area)
       {
           Interop.Common.CVB.cStruct.stCvbArea rarea = new Interop.Common.CVB.cStruct.stCvbArea();

           rarea.Left = _area.X0;
           rarea.Top = _area.Y0;
           rarea.Right = _area.X2;
           rarea.Bottom = _area.Y1;

           return rarea;

       }

       public static Interop.Common.CVB.cStruct.stCvbArea ConvertToCommonArea(Cvb.Image.TDRect _area)
       {
           Interop.Common.CVB.cStruct.stCvbArea rarea = new Interop.Common.CVB.cStruct.stCvbArea();
           rarea.Left = _area.Left;
           rarea.Top = _area.Top;
           rarea.Right = _area.Right;
           rarea.Bottom = _area.Bottom;
           return rarea;
       }
     
       #endregion [ stCvbArea 로 변환 - 공통 형식으로  ]

       #region 이미지 저장

       // 원본 결과 이미지 하나만 저장
       public static bool SaveImage(Cvb.Image.IMG img, string _filePath, string _filename)
       {
           //string sDtNow = DateTime.Now.ToString("yyyyMMdd-HHmmss.fff");
           //string sImageFile = sDtNow + "_CAM" + iCamNo.ToString("00") + (isOK ? "_OK" : "_NG") + ".jpg";
           //string sModelPath = "M" + _modelNo.ToString("00") + "\\" + (isOK ? "OK" : "NG");
           //string sPathFileName = mDirCreate(CGlobal.sPathImageData, sModelPath, true, true) + "\\" + sImageFile;
 
           string sPathFileName= string.Empty;

           if (DirCreate(_filePath) == true)
           {
               sPathFileName = string.Format("{0}\\{1}", _filePath, _filename);
               return Cvb.Image.WriteImageFile(img, sPathFileName);
           }
           else
           {
               return false;
           }
       }

       public static bool DirCreate(string sPathTemp)
       {
           //string sPath = sPathTemp;

           //if (bMonth)
           //{
           //    sPath += "\\" + DateTime.Now.ToString("MM");
           //}

           //if (bDay)
           //{
           //    sPath += "\\" + DateTime.Now.ToString("dd");
           //}

           //// 날짜+ 모델
           //sPath += "\\" + _modelName;


           //if (!System.IO.Directory.Exists(sPath))
           //{
           //    try
           //    {
           //        System.IO.Directory.CreateDirectory(sPath);
           //    }
           //    catch
           //    {
           //    }
           //}

           //return sPath;

           bool bresult=false;

           if (!System.IO.Directory.Exists(sPathTemp.Trim()))
           {
               try
               {
                   System.IO.Directory.CreateDirectory(sPathTemp.Trim());                   
               }
               catch
               {
                   bresult = false;
               }
           }

           bresult = System.IO.Directory.Exists(sPathTemp.Trim());


           return bresult;
       }



       #endregion




   
   
   }
}
