using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Interop.Common.Util;

namespace Interop.Common.CVB
{
    public sealed class CCVBUtil
    {
        #region Cam 설정 읽기
        //public static void mReadCameraInfo()
        //{
        //    mReadCameraInfo(0);
        //}


        //카메라 설정은 별도 Class로 분리하기
        public static void mReadCameraInfo(int _modelNo, int _iTotCamNum)
        {
            if (_iTotCamNum == 0)
            {
                System.Windows.Forms.MessageBox.Show("Camera 설정이 없습니다.", "Error");
                return;
            }

            //if (formMain.iTotModelNum == 0)
            //{
            //    System.Windows.Forms.MessageBox.Show("모델이 등록 되지 않았습니다.", "Error");
            //    return;
            //}

            //for (int i = 0; i < formMain.iTotCamNum; i++)
            //{
                //formMain.stCam[i].sSN = CIni.ReadCamSN(i);
                //formMain.stCam[i].sTITLE = CIni.ReadCamTitle(i);
            //    formMain.stCam[i].tArea = CIni.ReadArea(_modelNo, i);
            //    formMain.stCam[i].stResultManto.bResult = false;
            //    formMain.stCam[i].stResultManto.dPx = 0;
            //    formMain.stCam[i].stResultManto.dPy = 0;
            //    formMain.stCam[i].stResultManto.dPq = 0;
            //    formMain.stCam[i].stResultManto.sPClassID = "";
            //}
        }
        #endregion
 
        #region [ ExposureTime ]

        // ExposureTime(노출시간) 항목이 있는 경우가 있고 Manta 는 ExposureTimeAbs 항목만 존재
        // ExposureTime 을 수정 못하는 카메라도 있으므로
        // GiniCam 에서 ExposureTime 수정 가능 한지 확인 필요

        public static void SetExposureTime(AxCVIMAGELib.AxCVimage _image, string _time)
        {

            Interop.Common.CVB.NodeMap nodeMap;
            string kk;

            nodeMap = new Interop.Common.CVB.NodeMap( _image.Image );

            Cvb.GenApi.NGetAsString(nodeMap["ExposureTime"], out kk);//32000

            if (string.IsNullOrEmpty(kk))
            {
                Cvb.GenApi.NSetAsString(nodeMap["ExposureTimeAbs"], _time);
                Cvb.GenApi.NGetAsString(nodeMap["ExposureTimeAbs"], out kk);//32000
            }
            else
            {
                Cvb.GenApi.NSetAsString(nodeMap["ExposureTime"], _time);
                Cvb.GenApi.NGetAsString(nodeMap["ExposureTime"], out kk);//32000
            }
        }


        public string GetCamExposreTime(AxCVIMAGELib.AxCVimage _image)
        {

            Interop.Common.CVB.NodeMap nodeMap;
            string kk;

            nodeMap = new Interop.Common.CVB.NodeMap(_image.Image);

            Cvb.GenApi.NGetAsString(nodeMap["ExposureTime"], out kk);//32000

            if (string.IsNullOrEmpty(kk))
            {
                Cvb.GenApi.NGetAsString(nodeMap["ExposureTimeAbs"], out kk);//32000
            }

            return kk;
        }
        #endregion [ ExposureTime ]

        private string GetCamProperty(AxCVIMAGELib.AxCVimage _image , string _name)
        {

            Interop.Common.CVB.NodeMap nodeMap;
            string kk;

            nodeMap = new Interop.Common.CVB.NodeMap(_image.Image);

            Cvb.GenApi.NGetAsString(nodeMap[_name], out kk);

            if (string.IsNullOrEmpty(kk))
            {
                Cvb.GenApi.NGetAsString(nodeMap[_name], out kk);//32000
            }

            return kk; 
        }

        private  static bool  SetCamProperty(AxCVIMAGELib.AxCVimage _image, string _name, string _value)
        {

            Interop.Common.CVB.NodeMap nodeMap;
            string kk;
            bool _result = false;
            nodeMap = new Interop.Common.CVB.NodeMap(_image.Image);

            Cvb.GenApi.NGetAsString(nodeMap[_name], out kk);//32000


            //AcquisitionFrameRateEnable  =  true
            //AcquisitionFrameRate = 30
            //UserSetSelector= UserSet1
            //UserSetLoad  = on=1 / off=0
            //ExposureTime
            //GevCurrentIPAddress
 
            if (!string.IsNullOrEmpty(kk))
            {
                Cvb.GenApi.NSetAsString(nodeMap[_name], _value);
                _result = true;
            }

            return _result;
        }
 
        #region 이미지 저장

        // 원본 결과 이미지 하나만 저장
        public static string mSaveImage(int _modelNo, int iCamNo, Cvb.Image.IMG img, bool isOK)
        {
            string sDtNow = DateTime.Now.ToString("yyyyMMdd-HHmmss.fff");
            string sImageFile = sDtNow + "_CAM" + iCamNo.ToString("00") + (isOK ? "_OK" : "_NG") + ".jpg";
            string sModelPath = "M" + _modelNo.ToString("00") + "\\" + (isOK ? "OK" : "NG");
            string sPathFileName = mDirCreate(CGlobal.sPathImageData, sModelPath, true, true) + "\\" + sImageFile;
            Cvb.Image.WriteImageFile(img, sPathFileName);
            return sPathFileName;
        }

        public static string mSaveImage(int _modelNo, int iCamNo, string _sSeq, Cvb.Image.IMG img, bool isOK)
        {
            string sDtNow = DateTime.Now.ToString("yyyyMMdd-HHmmss.fff");
            string sImageFile = sDtNow + "_CAM" + iCamNo.ToString("00") + (isOK ? "_OK" : "_NG") + "(" + _sSeq + ")" + ".jpg";
            string sModelPath = "M" + _modelNo.ToString("00") + "\\" + (isOK ? "OK" : "NG");
            string sPathFileName = mDirCreate(CGlobal.sPathImageData, sModelPath, true, true) + "\\" + sImageFile;
            //string sPathFileName = mDirCreate( CGlobal.sPathImageData, sModelPath, true, true ) + "\\" + sImageFile;
            Cvb.Image.WriteImageFile(img, sPathFileName);
            return sPathFileName;
        }

        public static string mSaveImage(int _modelNo, string _sLotID, int iCamNo, Cvb.Image.IMG img, bool isOK)
        {
            string sDtNow = DateTime.Now.ToString("yyyyMMdd-HHmmss.fff");
            string sImageFile = sDtNow + "_" + _sLotID + "_CAM" + iCamNo.ToString("00") + (isOK ? "_OK" : "_NG") + ".jpg";
            string sModelPath = "M" + _modelNo.ToString("00") + "\\" + (isOK ? "OK" : "NG");
            string sPathFileName = mDirCreate(CGlobal.sPathImageData, sModelPath, true, true) + "\\" + sImageFile;
            Cvb.Image.WriteImageFile(img, sPathFileName);
            return sPathFileName;
        }

        public static string mSaveImage(int _modelNo, string _sLotID, int iCamNo, string _sSeq, Cvb.Image.IMG img, bool isOK)
        {
            string sDtNow = DateTime.Now.ToString("yyyyMMdd-HHmmss.fff");
            string sImageFile = sDtNow + "_" + _sLotID + "_CAM" + iCamNo.ToString("00") + (isOK ? "_OK" : "_NG") + "(" + _sSeq + ")" + ".jpg";
            string sModelPath = "M" + _modelNo.ToString("00") + "\\" + (isOK ? "OK" : "NG");
            string sPathFileName = mDirCreate(CGlobal.sPathImageData, sModelPath, true, true) + "\\" + sImageFile;
            //string sPathFileName = mDirCreate( CGlobal.sPathImageDataSeq, sModelPath, true, true ) + "\\" + sImageFile;
            Cvb.Image.WriteImageFile(img, sPathFileName);
            return sPathFileName;
        }


        public static string mSaveImage2(int _modelNo, string _sLotID, int iCamNo, Cvb.Image.IMG img, bool isOK)
        {
            string sDtNow = DateTime.Now.ToString("yyyyMMdd-HHmmss.fff");
            string sImageFile = sDtNow+"_"+_sLotID + "_CAM" + iCamNo.ToString("00") + (isOK ? "_OK" : "_NG") + ".jpg";
            string sModelPath = "M" + _modelNo.ToString("00") + "\\" + (isOK ? "OK" : "NG");
            string sPathFileName = mDirCreate(CGlobal.sPathImageData, sModelPath, true, true) + "\\" + sImageFile;
            Cvb.Image.WriteImageFile(img, sPathFileName);
            return sPathFileName;
        }

        public static string mSaveImage2(int _modelNo,  string _sLotID, int iCamNo, string _sSeq, Cvb.Image.IMG img, bool isOK)
        {
            string sDtNow = DateTime.Now.ToString("yyyyMMdd-HHmmss.fff");
            string sImageFile = sDtNow+"_"+_sLotID + "_CAM" + iCamNo.ToString("00") + (isOK ? "_OK" : "_NG") + "(" + _sSeq + ")" + ".jpg";
            string sModelPath = "M" + _modelNo.ToString("00") + "\\" + (isOK ? "OK" : "NG");
            string sPathFileName = mDirCreate(CGlobal.sPathImageData, sModelPath, true, true) + "\\" + sImageFile;
            //string sPathFileName = mDirCreate( CGlobal.sPathImageDataSeq, sModelPath, true, true ) + "\\" + sImageFile;
            Cvb.Image.WriteImageFile(img, sPathFileName);
            return sPathFileName;
        }

        //public static bool mSaveImage(Cvb.SharedImg img, string _fullpath)
        //{
        //    bool result = true; ;

        //    try
        //    {

        //        if (!Directory.Exists(CUtil.GetFolderPath(_fullpath)))
        //        {
        //            try
        //            {
        //                Directory.CreateDirectory(CUtil.GetFolderPath(_fullpath));
        //            }
        //            catch
        //            {
        //            }
        //        }
        //        Cvb.Image.WriteImageFile(img, _fullpath);
        //        // return sPathFileName;
        //    }
        //    catch
        //    {
        //        result = false;
        //    }

        //    return result;
        //}

        //public static bool mSaveImage(Cvb.Image.IMG img, string _fullpath)
        //{
        //    bool result = true; ;
         
        //    try
        //    {

        //        if (!Directory.Exists(CUtil.GetFolderPath(_fullpath)))
        //        {
        //            try
        //            {
        //                Directory.CreateDirectory(CUtil.GetFolderPath(_fullpath));
        //            }
        //            catch
        //            {
        //            }
        //        }
        //        Cvb.Image.WriteImageFile(img, _fullpath);
        //        // return sPathFileName;
        //    }
        //    catch
        //    {
        //        result = false;
        //    }

        //    return result;
        //}


        private static string mSaveImage( Cvb.Image.IMG img , string _path, string _filePerName)
        {
           // string sDtNow = DateTime.Now.ToString("yyyyMMdd-HHmmss.fff");
            string sImageFile = string.Empty;
 
             sImageFile = _filePerName +".jpg";


            string sPathFileName = mDirCreate(CGlobal.sPathImageData, _path, true, true) + "\\" + sImageFile;
            Cvb.Image.WriteImageFile(img, sPathFileName);
            return sPathFileName;
        }


        #endregion
          
        public static string mDirCreate(string sPathTemp, string _modelName, bool bMonth, bool bDay)
        {
            string sPath = sPathTemp;

            if (bMonth)
            {
                sPath += "\\" + DateTime.Now.ToString("MM");
            }

            if (bDay)
            {
                sPath += "\\" + DateTime.Now.ToString("dd");
            }

            // 날짜+ 모델
            sPath += "\\" + _modelName;


            if (!Directory.Exists(sPath))
            {
                try
                {
                    Directory.CreateDirectory(sPath);
                }
                catch
                {
                }
            }

            return sPath;
        }

        public static string mTranslateFileName()
        {
            string sFileName;
            Cvb.Utilities.TranslateFileName("%CVB%", out sFileName, 4098);

            return sFileName;
        }
 
        //private static double dQuality = 0.7;

        //public static double DQuality
        //{
        //    get { return dQuality; }
        //    set { dQuality = value; }
        //}


        #region  [ 이미지 회전 - Foundtion 회전을 사용 해도 됨.]

        // 이미지 회전 방향
        public enum eRoation
        {
            LEFT = 0, // 반시계방향
            RIGHT = 1, // 시계방향
            OneEighty = 2,//180도

            //FlipY180 = 3, // 좌우 반전
            //FlipX180 = 4,//상하반전
            None = 99,
        }

        public enum eDIMESION
        {
            MONO = 1,
            COLOR = 3
        }


       

        public static bool mImageRotation(Cvb.Image.IMG _imageObject, eRoation _roation, out Cvb.Image.IMG _outImage)
        {
            // Cvb.Image.IMG ExchangeImage;
            Cvb.Image.TArea CopyArea = new Cvb.Image.TArea();

            bool isrtn = false;

            int w = Cvb.Image.ImageWidth((Cvb.Image.IMG)_imageObject);
            int h = Cvb.Image.ImageHeight((Cvb.Image.IMG)_imageObject);

            _outImage = 0;

            switch (_roation)
            {
                case eRoation.LEFT: //시계 반대 방향으로 90 도 회전
                    CopyArea.X0 = w;// Cvb.Image.ImageWidth( tmpImage ); ;
                    CopyArea.X1 = 0;
                    CopyArea.X2 = w;//Cvb.Image.ImageWidth( tmpImage );
                    CopyArea.Y0 = 0;
                    CopyArea.Y1 = 0;
                    CopyArea.Y2 = h;// Cvb.Image.ImageHeight( tmpImage););                  
                    break;

                case eRoation.RIGHT: // 90도 //시계 방향으로 90 도 회전                
                    CopyArea.X0 = 0;
                    CopyArea.X1 = w;//Cvb.Image.ImageWidth( tmpImage ); 
                    CopyArea.X2 = 0;
                    CopyArea.Y0 = h;//Cvb.Image.ImageHeight( tmpImage );
                    CopyArea.Y1 = h;//Cvb.Image.ImageHeight( tmpImage );
                    CopyArea.Y2 = 0;
                    break;

                case eRoation.OneEighty: // 180도
                    CopyArea.X0 = w;// Cvb.Image.ImageWidth( tmpImage );
                    CopyArea.X1 = w;//Cvb.Image.ImageWidth( tmpImage );
                    CopyArea.X2 = 0;
                    CopyArea.Y0 = h;//Cvb.Image.ImageHeight( tmpImage );
                    CopyArea.Y1 = 0;
                    CopyArea.Y2 = h;//Cvb.Image.ImageHeight( tmpImage );
                    break;

                //case eRoation.FlipX180:
                //case eRoation.FlipY180:
                //    Cvb.Image.CreateGenericImage((int)eDIMESION.COLOR, w, h, false, out _outImage);//Dimesion 변경
                //    break; 

                case eRoation.None:
                    CopyArea.X0 = 0;
                    CopyArea.X1 = 0;
                    CopyArea.X2 = w;
                    CopyArea.Y0 = 0;
                    CopyArea.Y1 = h;
                    CopyArea.Y2 = 0;
                    break;

                default:
                    break;
            }

            switch (_roation)
            {
                case eRoation.LEFT:
                case eRoation.RIGHT:
                case eRoation.OneEighty:
                case eRoation.None:
                    isrtn = CreateSubAreaImage((Cvb.Image.IMG)_imageObject, CopyArea, out _outImage);
                    break;
                //case eRoation.FlipY180:
                //    {
                //        System.Drawing.Image img = CvbImageToBitmap(_imageObject, out isrtn);
                //        if (isrtn)
                //        {
                //            //img.RotateFlip( RotateFlipType.Rotate180FlipY ); // 180 도 회전
                //            img.RotateFlip(System.Drawing.RotateFlipType.RotateNoneFlipY);///좌우 반전
                //            isrtn = CopyBitmapToIMGbits(ref _outImage, (System.Drawing.Bitmap)img);
                //        }
                //    }
                //    break;
                //case eRoation.FlipX180:
                //    {
                //        //     bool ist=false;
                //        System.Drawing.Image img = CvbImageToBitmap(_imageObject, out isrtn);
                //        if (isrtn)
                //        {
                //            img.RotateFlip(System.Drawing.RotateFlipType.RotateNoneFlipX); // 상하 반전
                //            isrtn = CopyBitmapToIMGbits(ref _outImage, (System.Drawing.Bitmap)img);
                //        }
                //    }
                //    break;
            }

            return isrtn;
        }


        private static bool CreateSubAreaImage(Cvb.Image.IMG _inImg, Cvb.Image.TArea _inArea, out Cvb.Image.IMG _outImg)
        {
            Cvb.Image.TArea AreaNull;
            Cvb.Image.TCoordinateMap csTemp;
            Cvb.Image.TCoordinateMap csNull;
            Cvb.Image.TMatrix Matrix;

            _outImg = 0;

            if (!Cvb.Image.IsImage(_inImg))
            {
                return false;
            }

            // Get current cs
            Cvb.Image.GetImageCoordinates(_inImg, out csTemp);

            // Transform area to 0cs
            Cvb.Image.InitCoordinateMap(out csNull);
            Cvb.Image.CoordinateMapTransformArea(_inArea, csNull, out AreaNull);

            // Rotate cs to area
            Cvb.Image.RotationMatrix(Cvb.Image.Argument(AreaNull.X2 - AreaNull.X0, AreaNull.Y2 - AreaNull.Y0), out Matrix);

            csNull.Matrix.A11 = Matrix.A11;
            csNull.Matrix.A12 = Matrix.A12;
            csNull.Matrix.A21 = Matrix.A21;
            csNull.Matrix.A22 = Matrix.A22;

            // '// Set image cs
            Cvb.Image.SetImageCoordinates(_inImg, csNull);

            // ' Transform(area)
            Cvb.Image.PixelAreaToImage(_inImg, AreaNull, out AreaNull);

            // ' Sub image
            Cvb.Image.CreateSubImage(_inImg, AreaNull, out _outImg);

            // ' Restore cs
            Cvb.Image.SetImageCoordinates(_inImg, csTemp);

            // ' Set cs of the new image
            Cvb.Image.InitCoordinateMap(out csNull);
            Cvb.Image.SetImageCoordinates(_outImg, csNull);

            return true;
        }


        #endregion  [ 이미지 회전 ]
    }
}
