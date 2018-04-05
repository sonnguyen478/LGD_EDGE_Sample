////#define VER2011
#define BGR

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace Interop.Common.CVB.Util
{
    public class UtilImage
    {
        /// <summary>
        /// Image.IMG 를 BitMap 으로 변환
        /// </summary>
        /// <param name="img">Cvb.Image.IMG</param>
        /// <param name="RecquiresCopy">out 변환여부 ( true : 변환 ) </param>
        /// <returns>bitMap</returns>
        public static unsafe Bitmap CvbImageToBitmap(Cvb.Image.IMG img, out bool RecquiresCopy)
        {
            Bitmap bm = null;

            // bitmap Data 복사 여부
            RecquiresCopy = true;

            // check image
            if (!Cvb.Image.IsImage(img))
            {
                return null;
            }

            // image 정보
            int nWidth = Cvb.Image.ImageWidth(img);
            int nHeight = Cvb.Image.ImageHeight(img);
            int nDimension = Cvb.Image.ImageDimension(img); // 1: mono , 3 : Color 

            // check the datatype ==> Dimension 은 각 8bit 로 구성 되어 있다.
            // mono  : 1 디멘져, 8bit
            // Color  : 3 디멘져 ,  8*3=24bit
            for (int i = 0; i < nDimension; i++)
            {
                if (Cvb.Image.ImageDatatype(img, i) != 8)
                    return null;
            }

            switch (nDimension)
            {
                case 1:
                    {
                        //  bitmap 생성

                        IntPtr pBase = IntPtr.Zero;
                        int xInc = -1;
                        int yInc = -1;

                        // try to get linear access to plane 0
                        //
                        //  Cvb.Utilities.GetLinearAccess
                        // 이미지가 데이터에 직접 선형 액세스를 지원 할 수 있는지 확인하기 위해 AnalysexVPAT 기능을 사용합니다.
                        // 함수가 TRUE를 반환하면 포인터를 통해 이미지 데이터에 액세스 할 수 있습니다. 
                        // 기존의 알고리즘은 신속하고 효율적으로 CVB 이미지를 포팅 할 수 있습니다.
                        // 매개 변수:
                        //   img:
                        //     Image object handle.
                        //
                        //   plane:
                        //    ImageDimension - 1 ( mono : 0 , Color :2 )
                        //
                        //   pBaseAddress:
                        //     이미지 첫번째 poxel pointer 
                        //
                        //   xInc:
                        //     Contains the increment in the X direction to the next pixel, e.g. (NextPixel
                        //     = *(lpBaseAddress += lXInc). Note, the increment can be positive OR negative.
                        //
                        //   yInc:
                        //     Contains the increment in the Y direction to the next line, e.g. (NextLine
                        //     = *(lpBaseAddress += lYInc).  Note, the increment can be positive OR negative.
                        //
                        // 반환 값:
                        //     False on error.
                        Cvb.Utilities.GetLinearAccess(img, 0, out pBase, out xInc, out yInc);

                        // Data 순서가 맞으면 이미지를 복사하지않는다.
                        if ((yInc == nWidth) && (xInc == 1))
                        {
                            // the y-pitch - 시작 번지
                            int stride = nWidth * xInc;

                            // bitMap 생성
                            bm = new Bitmap(nWidth, nHeight, stride, PixelFormat.Format8bppIndexed, pBase);

                            // 이미지 카피 하지 않았음.
                            RecquiresCopy = false;
                        }
                        else
                        {
                            // bitMap 생성
                            bm = new Bitmap(nWidth, nHeight, PixelFormat.Format8bppIndexed);
                            // copy the data
                            CopyIMGbitsToBitmap(img, ref bm);
                        }

                        // the palette
                        ColorPalette pal = bm.Palette;

                        // rewrite the palette
                        for (int i = 0; i < bm.Palette.Entries.Length; i++)
                        {
                            pal.Entries[i] = Color.FromArgb(i, i, i);
                        }
                        // apply it
                        bm.Palette = pal;

                        //return bm;
                        break;
                    }

                case 3:
                    {
                        //생성
                        bm = new Bitmap(nWidth, nHeight, PixelFormat.Format24bppRgb);

                        // copy the data
                        CopyIMGbitsToBitmap(img, ref bm);

                        //return bm;
                        break;
                    }

                default:
                    {
                        bm = null;
                        break;
                    }
            }
            return bm;
        }

        /// <summary>
        /// Cvb.Image.IMG 를 BitMap 로 변경
        /// </summary>
        /// <param name="img">원본 Cvb.Image.IMG </param>
        /// <param name="bm">ref, 결과 BitMap </param>
        /// <returns>변환 성공여부</returns>
        public static unsafe bool CopyIMGbitsToBitmap(Cvb.Image.IMG img, ref Bitmap bm)
        {
            // check image and bitmap
            if ((!Cvb.Image.IsImage(img)) || (bm == null))
            {
                return false;
            }

            int nWidth = Cvb.Image.ImageWidth(img);
            int nHeight = Cvb.Image.ImageHeight(img);
            int nDimension = Cvb.Image.ImageDimension(img); // Mono : 1 , Color : 3  ,  plane(=Dimension) 마다 8 bit.

            // bitmap 과 Image 가 동일한 size 인지 확인
            if ((bm.Width != nWidth) || (bm.Height != nHeight))
                return false;

            // 선형이미지 접근 (prepare linear image access)
            IntPtr[] pBase = new IntPtr[3];
            int[] xInc = new int[3];
            int[] yInc = new int[3];

            // prepare non-linear image access (through the VPAT)
            IntPtr[] addrVPAT = new IntPtr[3];
            //Cvb.Image.VPAEntry*[] pVPAT = new Cvb.Image.VPAEntry*[3];

            //#if VER2011
            //            Cvb.Image.VPAEntry*[] pVPAT = new Cvb.Image.VPAEntry*[ 3 ];

            //#else
            Interop.Common.CVB.cStruct.VPAEntry*[] pVPAT = new Interop.Common.CVB.cStruct.VPAEntry*[3]; // 12 버젼에 신규 생선된 것임.
            //#endif
            // flag to indicate wether the image data is linear or not
            bool bIsLinear = true;
            for (int i = 0; i < nDimension; i++)
            {
                // datatype 확인 ( 각 plane 이 8Bit 인지 확인 )
                if (Cvb.Image.ImageDatatype(img, i) != 8)
                {
                    return false;
                }

                // try to get linear access to the image data
                if (!Cvb.Utilities.GetLinearAccess(img, i, out pBase[i], out xInc[i], out yInc[i]))
                {
                    bIsLinear = false;
                }
            }

            // lock the bitmap
            Rectangle rc = new Rectangle(0, 0, nWidth, nHeight);
            BitmapData bmData = null;
            switch (nDimension)
            {
                case 1:
                    {
                        bmData = bm.LockBits(rc, ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);

                        // the pointer to the bitmap bits
                        byte* pDst = (byte*)bmData.Scan0;

                        // the linear case
                        if (bIsLinear)
                        {
                            for (int y = 0; y < nHeight; y++)
                            {
                                // the pointer to the start of line y of the IMG
                                byte* pSrc = (byte*)pBase[0] + y * yInc[0];
                                for (int x = 0; x < nWidth; x++)
                                {
                                    // inc. bitmap pointer
                                    *(pDst++) = *pSrc;
                                    // inc. IMG pointer
                                    pSrc += xInc[0];
                                }
                                // jump to the stride
                                for (int k = 0; k < bmData.Stride - bmData.Width; k++)
                                    pDst++;
                            }
                        }
                        // the confused VPAT case (e.g. unsorted multitap linescan)
                        else
                        {
                            // get VPAT access to the image data
                            Cvb.Image.GetImageVPA(img, 0, out pBase[0], out addrVPAT[0]);

                            //#if VER2011
                            //                            // a pointer to the VPAT
                            //                            pVPAT[ 0 ] = (Cvb.Image.VPAEntry*)addrVPAT[ 0 ].ToPointer();
                            //#else
                            // 이전버전
                            pVPAT[0] = (Interop.Common.CVB.cStruct.VPAEntry*)addrVPAT[0].ToPointer();
                            //#endif

                            for (int y = 0; y < nHeight; y++)
                            {
                                // a pointer to the start of the line
                                byte* pImageLine = (byte*)pBase[0] + pVPAT[0][y].YEntry.ToInt64();
                                for (int x = 0; x < nWidth; x++)
                                {
                                    // copy the pixel
                                    *(pDst++) = *(pImageLine + pVPAT[0][x].XEntry.ToInt64());
                                }
                                // jump to the stride
                                for (int k = 0; k < bmData.Stride - bmData.Width; k++)
                                    pDst++;
                            }
                        }
                        // unlock the bitmap bits
                        bm.UnlockBits(bmData);
                        return true;
                    }
                case 3:
                    {
                        // lock the bitmap
                        //Rectangle rc = new Rectangle(0, 0, nWidth, nHeight);
                        //BitmapData bmData = bm.LockBits(rc, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                        bmData = bm.LockBits(rc, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

                        // the pointer to the bitmap bits
                        byte* pDst = (byte*)bmData.Scan0;

                        // the linear case
                        if (bIsLinear)
                        {
                            // the pointers to the start of the lines of the IMG
                            byte*[] pSrc = new byte*[3]; // rgb
                            for (int y = 0; y < nHeight; y++)
                            {
                                // init the pointers to the start of line y of the IMG
                                for (int k = 0; k < 3; k++)
                                {
                                    pSrc[k] = (byte*)pBase[k] + y * yInc[k];
                                }

                                for (int x = 0; x < nWidth; x++)
                                {
                                    // copy the rgb (bgr) pixel
#if BGR
                                    //BGR
                                    *(pDst++) = *pSrc[2];
                                    *(pDst++) = *pSrc[1];
                                    *(pDst++) = *pSrc[0];
#else
                                      // RGB
                                      *(pDst++) = *pSrc[0];
                                      *(pDst++) = *pSrc[1];
                                      *(pDst++) = *pSrc[2];
#endif
                                    // inc. IMG pointers
                                    for (int k = 0; k < 3; k++)
                                        pSrc[k] += xInc[k];
                                }

                                // jump to the stride
                                for (int k = 0; k < bmData.Stride - bmData.Width * 3; k++)
                                    pDst++;
                            }
                        }

                        // the confused VPAT case (e.g. unsorted multitap linescan)
                        else
                        {
                            // get VPAT access to the image data
                            for (int k = 0; k < 3; k++)
                            {
                                Cvb.Image.GetImageVPA(img, k, out pBase[k], out addrVPAT[k]);

                                //#if VER2011
                                //                                // a pointer to the VPAT
                                //                                pVPAT[ k ] = (Cvb.Image.VPAEntry*)addrVPAT[ k ].ToPointer();
                                //#else
                                // 이전버전
                                pVPAT[k] = (Interop.Common.CVB.cStruct.VPAEntry*)addrVPAT[k].ToPointer();
                                //#endif
                            }

                            // the pointers to the start of the lines of the IMG
                            byte*[] pSrc = new byte*[3];
                            for (int y = 0; y < nHeight; y++)
                            {
                                // init the pointer to the start of line y of the IMG
                                for (int k = 0; k < 3; k++)
                                    pSrc[k] = (byte*)pBase[k] + pVPAT[k][y].YEntry.ToInt64();
                                for (int x = 0; x < nWidth; x++)
                                {
                                    // copy the rgb (bgr) pixel
#if BGR
                                    *(pDst++) = *(pSrc[2] + pVPAT[2][x].XEntry.ToInt64());
                                    *(pDst++) = *(pSrc[1] + pVPAT[1][x].XEntry.ToInt64());
                                    *(pDst++) = *(pSrc[0] + pVPAT[0][x].XEntry.ToInt64());


#else
                                      *(pDst++) = *(pSrc[0] + pVPAT[0][x].XEntry);
                                      *(pDst++) = *(pSrc[1] + pVPAT[1][x].XEntry);
                                      *(pDst++) = *(pSrc[2] + pVPAT[2][x].XEntry);
#endif
                                }
                                // jump to the stride
                                for (int k = 0; k < bmData.Stride - bmData.Width * 3; k++)
                                    pDst++;
                            }
                        }

                        // unlock the bitmap bits
                        bm.UnlockBits(bmData);
                        return true;
                    }
                default:
                    {
                        return false;
                    }
            }
        }

        /// <summary>
        /// 흑백으로 변경
        /// </summary>
        /// <param name="_image">원본 System.Draw.Image </param>
        /// <returns>BitMap</returns>
        public static System.Drawing.Bitmap Get_Image_GRAYSCALE(Image _image)
        {
            Bitmap bitMap = new Bitmap(_image, _image.Width, _image.Height);
            return Get_Image_GRAYSCALE(bitMap);
        }

        /// <summary>
        /// 흑백으로 변경
        /// </summary>
        /// <param name="_bitMap">BitMap</param>
        /// <returns>BitMap</returns>
        public static System.Drawing.Bitmap Get_Image_GRAYSCALE(Bitmap _bitMap)
        {
            System.Drawing.Bitmap m_nImage; // 1 Bitmap 객체
            System.Drawing.Imaging.BitmapData m_pImageData; // 2) Bitmap 정보를 얻어 올 BitmapData 객체 전 시간에 이야기한 Imageing 네임 스페이스 안에 묶여 잇는 객체

            m_nImage = (Bitmap)_bitMap;

            m_pImageData = m_nImage.LockBits(new Rectangle(0, 0, m_nImage.Width, m_nImage.Height),// 실제 데이터를 가져올 영역
                                         System.Drawing.Imaging.ImageLockMode.ReadWrite,    // ReadWrite 가능한 모드로 
                                         System.Drawing.Imaging.PixelFormat.Format24bppRgb /*픽셀 포멧을 24BIT RGB형태로 얻어오라*/); // 4 Bitmap  객체인 m_nImage로 부터 BitmapData 를 가져 온다.

            System.IntPtr pScan = m_pImageData.Scan0; // 5 m_pImageData.Scan0 는 BitmapData의 실제 각 픽셀 의 buffer 포인터 입니다. BYTE(unsigned char*)라고 생각 하시면 됩니다
            int nStride = m_pImageData.Stride;// 6 m_pImageData.Stride;는 한줄의 간격 입니다. 현재 한줄의 간격이라고 생각하면 24비트니까 버퍼는 RGBRGBRGBRGB 이렇게 이미지의 넓이 만큼 구성되니 width*3이 Stride이 되어야 합니다

            // 7
            // 이제 이 부분에 실제  pBuffer 부분을 하나씩 돌면서 픽셀 값에 일정 값을 모두 더해서 밝기 값을 변화 시킴니다. 각 R,G,B 값 
            // 각각 마다 일정 값이 더해 지는 거죠 단 각 자리가 8비트 라서 255가 한계 값이니 255가 넘는 경우에 대한 예외 처리 필수
            // 밝기 값은 (R+G+B) / 3이 밝기 값이라고 생각 하시면 됩니다. 단 nWidth는 실제 이미지 길이가 아니라 버퍼의 길이 입니다.
            // 24 비트에 8 비트가 RGB 중 하나씩 표현이고 현자 자료 현은 8비트인 BYTE 이니 실제 버퍼의 길이는 한줄이 이미지 넓이  * 3(R,G,B) 입니다.
            unsafe
            {
                byte* pBuffer = (byte*)(void*)pScan;

                int noffset = nStride - m_nImage.Width * 3;
                int nWidth = m_nImage.Width * 3;      //실제 이미지 길이가 아니라 버퍼의 길이
                int nTemp;
                nWidth = m_nImage.Width;

                for (int nY = 0; nY < m_nImage.Height; nY++)
                {
                    for (int nX = 0; nX < nWidth; nX++)
                    {

                        //pBuffer[0] = Blue
                        //pBuffer[1] = Green
                        //pBuffer[2] = Red
                        nTemp = (int)(0.299 * pBuffer[2] + 0.587 * pBuffer[1] + 0.114 * pBuffer[0]);

                        //nTemp = (byte)(0.299 * pBuffer[2] + 0.587 * pBuffer[1] + 0.114 * pBuffer[0]);

                        pBuffer[0] = pBuffer[1] = pBuffer[2] = (byte)nTemp;
                        pBuffer += 3;

                    }
                    pBuffer += noffset;
                }
            }

            m_nImage.UnlockBits(m_pImageData); // 8 UnlockBits 하여 Bitmap 객체에 변화된 버퍼를 반영 합니다. 

            return m_nImage;
        }

        /// <summary>
        /// 특정 색상으로 변경
        /// </summary>
        /// <param name="_bitMap">원본 Bitmap</param>
        /// <param name="_rgb"> R:0 , G:1 ,  B:2</param>
        /// <returns>bitMap</returns>
        public static System.Drawing.Bitmap Get_Image_RGB(Bitmap _bitMap, int _rgb)
        {
            System.Drawing.Bitmap m_nImage; // 1 Bitmap 객체
            System.Drawing.Imaging.BitmapData m_pImageData; // 2) Bitmap 정보를 얻어 올 BitmapData 객체 전 시간에 이야기한 Imageing 네임 스페이스 안에 묶여 잇는 객체

            m_nImage = (Bitmap)_bitMap;

            m_pImageData = m_nImage.LockBits(new Rectangle(0, 0, m_nImage.Width, m_nImage.Height),// 실제 데이터를 가져올 영역
                                         System.Drawing.Imaging.ImageLockMode.ReadWrite,    // ReadWrite 가능한 모드로 
                                         System.Drawing.Imaging.PixelFormat.Format24bppRgb /*픽셀 포멧을 24BIT RGB형태로 얻어오라*/); // 4 Bitmap  객체인 m_nImage로 부터 BitmapData 를 가져 온다.

            System.IntPtr pScan = m_pImageData.Scan0; // 5 m_pImageData.Scan0 는 BitmapData의 실제 각 픽셀 의 buffer 포인터 입니다. BYTE(unsigned char*)라고 생각 하시면 됩니다
            int nStride = m_pImageData.Stride;// 6 m_pImageData.Stride;는 한줄의 간격 입니다. 현재 한줄의 간격이라고 생각하면 24비트니까 버퍼는 RGBRGBRGBRGB 이렇게 이미지의 넓이 만큼 구성되니 width*3이 Stride이 되어야 합니다

            // 7
            // 이제 이 부분에 실제  pBuffer 부분을 하나씩 돌면서 픽셀 값에 일정 값을 모두 더해서 밝기 값을 변화 시킴니다. 각 R,G,B 값 
            // 각각 마다 일정 값이 더해 지는 거죠 단 각 자리가 8비트 라서 255가 한계 값이니 255가 넘는 경우에 대한 예외 처리 필수
            // 밝기 값은 (R+G+B) / 3이 밝기 값이라고 생각 하시면 됩니다. 단 nWidth는 실제 이미지 길이가 아니라 버퍼의 길이 입니다.
            // 24 비트에 8 비트가 RGB 중 하나씩 표현이고 현자 자료 현은 8비트인 BYTE 이니 실제 버퍼의 길이는 한줄이 이미지 넓이  * 3(R,G,B) 입니다.
            unsafe
            {
                byte* pBuffer = (byte*)(void*)pScan;

                int noffset = nStride - m_nImage.Width * 3;
                int nWidth = m_nImage.Width * 3;      //실제 이미지 길이가 아니라 버퍼의 길이

                nWidth = m_nImage.Width;

                for (int nY = 0; nY < m_nImage.Height; nY++)
                {
                    for (int nX = 0; nX < nWidth; nX++)
                    {


#if BGR
                        switch (_rgb)
                        {
                            case 0:
                                pBuffer[0] = 0;
                                pBuffer[1] = 0;
                                //pBuffer[2] = 0;
                                break;

                            case 1:
                                pBuffer[0] = 0;
                                //pBuffer[1] = 0;
                                pBuffer[2] = 0;
                                break;

                            case 2:
                                //pBuffer[0] = 0;
                                pBuffer[1] = 0;
                                pBuffer[2] = 0;
                                break;

                            default:
                                break;
                        }
#else 
                        
                        switch (_rgb)
                        {
                            case 0:
                                //pBuffer[0] = 0;
                                pBuffer[1] = 0;
                                pBuffer[2] = 0;
                                break;

                            case 1:
                                pBuffer[0] = 0;
                                //pBuffer[1] = 0;
                                pBuffer[2] = 0;
                                break;

                            case 2:
                                pBuffer[0] = 0;
                                pBuffer[1] = 0;
                                //pBuffer[2] = 0;
                                break;

                            default:
                                break;
                        }
#endif

                        pBuffer += 3;

                    }
                    pBuffer += noffset;
                }
            }

            m_nImage.UnlockBits(m_pImageData); // 8 UnlockBits 하여 Bitmap 객체에 변화된 버퍼를 반영 합니다. 

            return m_nImage;
        }

        /// <summary>
        /// BitMamp 을 Cvb.Image.IMG로 변환
        /// </summary>
        /// <param name="img">ref  변환 후 Cvb.Image.IMG</param>
        /// <param name="_pb">원본 PictureBox</param>
        /// <returns>bool</returns>
        public static bool CopyBitmapToIMGbits(ref Cvb.Image.IMG img, System.Windows.Forms.PictureBox _pb)
        {
            return CopyBitmapToIMGbits(ref img, (System.Drawing.Bitmap)_pb.Image);
        }

        /// <summary>
        /// 24Bit - BitMap 을 받아서 Image.IMG  로 변환
        /// </summary>
        /// <param name="img">ref 변환 후 Cvb.Image.IMG </param>
        /// <param name="bm">원본 bitmap</param>
        /// <returns>bool</returns>
        public static bool CopyBitmapToIMGbits(ref Cvb.Image.IMG img, Bitmap bm)
        {
            return CopyBitmapToIMGbits(ref   img, bm, 0);
        }

        public static unsafe bool CopyBitmapToIMGbits(ref Cvb.Image.IMG img, Bitmap bm, int _dimension)
        {
            // check image and bitmap
            if ((!Cvb.Image.IsImage(img)) || (bm == null))
            {
                return false;
            }

            int nWidth = Cvb.Image.ImageWidth(img);
            int nHeight = Cvb.Image.ImageHeight(img);
            int nDimension;

            if (_dimension == 0)
            {
                nDimension = Cvb.Image.ImageDimension(img); // Mono : 1 , Color : 3  ,  plane(=Dimension) 마다 8 bit.
            }
            else
            {
                nDimension = _dimension;
            }

            // bitmap 과 Image 가 동일한 size 인지 확인
            if ((bm.Width != nWidth) || (bm.Height != nHeight))
                return false;

            // 선형이미지 접근 (prepare linear image access)
            IntPtr[] pBase = new IntPtr[3];
            int[] xInc = new int[3];
            int[] yInc = new int[3];

            // prepare non-linear image access (through the VPAT)
            IntPtr[] addrVPAT = new IntPtr[3];


            //#if VER2011
            //            // a pointer to the VPAT
            //            Cvb.Image.VPAEntry*[] pVPAT = new Cvb.Image.VPAEntry*[ 3 ];
            //#else
            // 이전버전
            Interop.Common.CVB.cStruct.VPAEntry*[] pVPAT = new Interop.Common.CVB.cStruct.VPAEntry*[3]; // 12 버젼에 신규 생선된 것임.
            //#endif

            // flag to indicate wether the image data is linear or not
            bool bIsLinear = true;
            for (int i = 0; i < nDimension; i++)
            {
                if (_dimension == 0)
                {
                    // datatype 확인 ( 각 plane 이 8Bit 인지 확인 )
                    if (Cvb.Image.ImageDatatype(img, i) != 8)
                    {
                        return false;
                    }
                }

                // try to get linear access to the image data
                if (!Cvb.Utilities.GetLinearAccess(img, i, out pBase[i], out xInc[i], out yInc[i]))
                {
                    bIsLinear = false;
                }
            }

            // lock the bitmap
            Rectangle rc = new Rectangle(0, 0, nWidth, nHeight);
            BitmapData bmData = null;
            switch (nDimension)
            {
                case 1:
                    {
                        bmData = bm.LockBits(rc, ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);

                        // the pointer to the bitmap bits
                        byte* pDst = (byte*)bmData.Scan0;

                        // the linear case
                        if (bIsLinear)
                        {
                            for (int y = 0; y < nHeight; y++)
                            {
                                // the pointer to the start of line y of the IMG
                                byte* pSrc = (byte*)pBase[0] + y * yInc[0];
                                for (int x = 0; x < nWidth; x++)
                                {
                                    // inc. bitmap pointer
                                    *(pDst++) = *pSrc;
                                    // inc. IMG pointer
                                    pSrc += xInc[0];
                                }
                                // jump to the stride
                                for (int k = 0; k < bmData.Stride - bmData.Width; k++)
                                    pDst++;
                            }
                        }
                        // the confused VPAT case (e.g. unsorted multitap linescan)
                        else
                        {
                            // get VPAT access to the image data
                            Cvb.Image.GetImageVPA(img, 0, out pBase[0], out addrVPAT[0]);

                            //#if VER2011
                            //                            // a pointer to the VPAT
                            //                            pVPAT[ 0 ] = (Cvb.Image.VPAEntry*)addrVPAT[ 0 ].ToPointer();
                            //#else
                            // 이전버전
                            pVPAT[0] = (Interop.Common.CVB.cStruct.VPAEntry*)addrVPAT[0].ToPointer();
                            //#endif

                            for (int y = 0; y < nHeight; y++)
                            {
                                // a pointer to the start of the line
                                byte* pImageLine = (byte*)pBase[0] + pVPAT[0][y].YEntry.ToInt64();
                                for (int x = 0; x < nWidth; x++)
                                {
                                    // copy the pixel
                                    *(pDst++) = *(pImageLine + pVPAT[0][x].XEntry.ToInt64());
                                }
                                // jump to the stride
                                for (int k = 0; k < bmData.Stride - bmData.Width; k++)
                                    pDst++;
                            }
                        }
                        // unlock the bitmap bits
                        bm.UnlockBits(bmData);
                        return true;
                    }
                case 3:
                    {
                        // lock the bitmap
                        //Rectangle rc = new Rectangle(0, 0, nWidth, nHeight);
                        //BitmapData bmData = bm.LockBits(rc, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

                        bmData = bm.LockBits(rc, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

                        // the pointer to the bitmap bits
                        byte* pDst = (byte*)bmData.Scan0;

                        // the linear case
                        if (bIsLinear)
                        {
                            // the pointers to the start of the lines of the IMG
                            byte*[] pSrc = new byte*[3]; // rgb
                            for (int y = 0; y < nHeight; y++)
                            {
                                // init the pointers to the start of line y of the IMG
                                for (int k = 0; k < 3; k++)
                                {
                                    pSrc[k] = (byte*)pBase[k] + y * yInc[k];
                                }

                                for (int x = 0; x < nWidth; x++)
                                {
                                    // copy the rgb (bgr) pixel
#if BGR
                                    //BGR
                                    //*(pDst++) = *pSrc[2];
                                    //*(pDst++) = *pSrc[1];
                                    //*(pDst++) = *pSrc[0];
                                    *pSrc[2] = *(pDst++);
                                    *pSrc[1] = *(pDst++);
                                    *pSrc[0] = *(pDst++);

#else
                                      // RGB
                                      *(pDst++) = *pSrc[0];
                                      *(pDst++) = *pSrc[1];
                                      *(pDst++) = *pSrc[2];
#endif
                                    // inc. IMG pointers
                                    for (int k = 0; k < 3; k++)
                                        pSrc[k] += xInc[k];
                                }

                                // jump to the stride
                                for (int k = 0; k < bmData.Stride - bmData.Width * 3; k++)
                                    pDst++;
                            }
                        }

                        // the confused VPAT case (e.g. unsorted multitap linescan)
                        else
                        {
                            // get VPAT access to the image data
                            for (int k = 0; k < 3; k++)
                            {
                                Cvb.Image.GetImageVPA(img, k, out pBase[k], out addrVPAT[k]);

                                //#if VER2011
                                //                                // a pointer to the VPAT
                                //                                pVPAT[ k ] = (Cvb.Image.VPAEntry*)addrVPAT[ k ].ToPointer();
                                //#else
                                // 이전버전
                                pVPAT[k] = (Interop.Common.CVB.cStruct.VPAEntry*)addrVPAT[k].ToPointer();

                                //#endif
                            }

                            // the pointers to the start of the lines of the IMG
                            byte*[] pSrc = new byte*[3];
                            for (int y = 0; y < nHeight; y++)
                            {
                                // init the pointer to the start of line y of the IMG
                                for (int k = 0; k < 3; k++)
                                    pSrc[k] = (byte*)pBase[k] + pVPAT[k][y].YEntry.ToInt64();
                                for (int x = 0; x < nWidth; x++)
                                {
                                    // copy the rgb (bgr) pixel
#if BGR
                                    //*(pDst++) = *(pSrc[2] + pVPAT[2][x].XEntry.ToInt64());
                                    //*(pDst++) = *(pSrc[1] + pVPAT[1][x].XEntry.ToInt64());
                                    //*(pDst++) = *(pSrc[0] + pVPAT[0][x].XEntry.ToInt64());

                                    *(pSrc[2] + pVPAT[2][x].XEntry.ToInt64()) = *(pDst++);
                                    *(pSrc[1] + pVPAT[1][x].XEntry.ToInt64()) = *(pDst++);
                                    *(pSrc[0] + pVPAT[0][x].XEntry.ToInt64()) = *(pDst++);
#else
                                      *(pDst++) = *(pSrc[0] + pVPAT[0][x].XEntry);
                                      *(pDst++) = *(pSrc[1] + pVPAT[1][x].XEntry);
                                      *(pDst++) = *(pSrc[2] + pVPAT[2][x].XEntry);
#endif
                                }
                                // jump to the stride
                                for (int k = 0; k < bmData.Stride - bmData.Width * 3; k++)
                                    pDst++;
                            }
                        }

                        // unlock the bitmap bits
                        bm.UnlockBits(bmData);
                        return true;
                    }
                default:
                    {
                        return false;
                    }
            }
        }

        //private  Cvb.Image.IMG GetImageIMGCreate(ref  Cvb.Image.IMG Bitmap _pic, int _dimesion)
        //{
        //    Cvb.Image.IMG temoDemesion = 0;

        //    if (  _dimesion == (int)eDIMESION .MONO || _dimesion ==(int)eDIMESION.COLOR)
        //    {
        //            Cvb.Image.CreateGenericImage(  (int)_dimesion, _pic.Width, _pic.Height, false, out temoDemesion );
        //            //Cvb.Image.TArea t = new Cvb.Image.TArea();
        //            //t.X0 = 0;
        //            //t.X1 = 0;
        //            //t.X2 = _pic.Width - 1;
        //            //t.Y0 = 0;
        //            //t.Y1 = _pic.Height - 1;
        //            //t.Y2 = 0;              
        //    }

        //    return temoDemesion;            
        //}


        public static unsafe Cvb.SharedImg CopyBitmapToIMGbits(Cvb.Image.IMG img, Bitmap bm, int _dimension)
        {
            Cvb.SharedImg outImage = new Cvb.SharedImg();

            // check image and bitmap
            if ((!Cvb.Image.IsImage(img)) || (bm == null))
            {
                return outImage;
            }

            int nWidth = Cvb.Image.ImageWidth(img);
            int nHeight = Cvb.Image.ImageHeight(img);
            int nDimension;

            if (_dimension == 0)
            {
                nDimension = Cvb.Image.ImageDimension(img); // Mono : 1 , Color : 3  ,  plane(=Dimension) 마다 8 bit.
            }
            else
            {
                nDimension = _dimension;
            }

            // bitmap 과 Image 가 동일한 size 인지 확인
            if ((bm.Width != nWidth) || (bm.Height != nHeight))
            {
                return outImage;
            }

            // 선형이미지 접근 (prepare linear image access)
            IntPtr[] pBase = new IntPtr[3];
            int[] xInc = new int[3];
            int[] yInc = new int[3];

            // prepare non-linear image access (through the VPAT)
            IntPtr[] addrVPAT = new IntPtr[3];

            Interop.Common.CVB.cStruct.VPAEntry*[] pVPAT = new Interop.Common.CVB.cStruct.VPAEntry*[3]; // 12 버젼에 신규 생선된 것임.


            // flag to indicate wether the image data is linear or not
            bool bIsLinear = true;
            for (int i = 0; i < nDimension; i++)
            {
                if (_dimension == 0)
                {
                    // datatype 확인 ( 각 plane 이 8Bit 인지 확인 )
                    if (Cvb.Image.ImageDatatype(img, i) != 8)
                    {
                        return outImage;
                    }
                }

                // try to get linear access to the image data
                if (!Cvb.Utilities.GetLinearAccess(img, i, out pBase[i], out xInc[i], out yInc[i]))
                {
                    bIsLinear = false;
                }
            }

            // lock the bitmap
            Rectangle rc = new Rectangle(0, 0, nWidth, nHeight);
            BitmapData bmData = null;
            switch (nDimension)
            {
                case 1:
                    {
                        bmData = bm.LockBits(rc, ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);

                        // the pointer to the bitmap bits
                        byte* pDst = (byte*)bmData.Scan0;

                        // the linear case
                        if (bIsLinear)
                        {
                            for (int y = 0; y < nHeight; y++)
                            {
                                // the pointer to the start of line y of the IMG
                                byte* pSrc = (byte*)pBase[0] + y * yInc[0];
                                for (int x = 0; x < nWidth; x++)
                                {
                                    // inc. bitmap pointer
                                    *(pDst++) = *pSrc;
                                    // inc. IMG pointer
                                    pSrc += xInc[0];
                                }
                                // jump to the stride
                                for (int k = 0; k < bmData.Stride - bmData.Width; k++)
                                    pDst++;
                            }
                        } 
                        else
                        {
                            // get VPAT access to the image data
                            Cvb.Image.GetImageVPA(img, 0, out pBase[0], out addrVPAT[0]);

                            pVPAT[0] = (Interop.Common.CVB.cStruct.VPAEntry*)addrVPAT[0].ToPointer();


                            for (int y = 0; y < nHeight; y++)
                            {
                                // a pointer to the start of the line
                                byte* pImageLine = (byte*)pBase[0] + pVPAT[0][y].YEntry.ToInt64();
                                for (int x = 0; x < nWidth; x++)
                                {
                                    // copy the pixel
                                    *(pDst++) = *(pImageLine + pVPAT[0][x].XEntry.ToInt64());
                                }
                                // jump to the stride
                                for (int k = 0; k < bmData.Stride - bmData.Width; k++)
                                    pDst++;
                            }
                        }
                        // unlock the bitmap bits
                        bm.UnlockBits(bmData);
                        return outImage;
                    }
                case 3:
                    {
                        bmData = bm.LockBits(rc, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

                        byte* pDst = (byte*)bmData.Scan0;

                        // the linear case
                        if (bIsLinear)
                        {
                            // the pointers to the start of the lines of the IMG
                            byte*[] pSrc = new byte*[3]; // rgb
                            for (int y = 0; y < nHeight; y++)
                            {
                                // init the pointers to the start of line y of the IMG
                                for (int k = 0; k < 3; k++)
                                {
                                    pSrc[k] = (byte*)pBase[k] + y * yInc[k];
                                }

                                for (int x = 0; x < nWidth; x++)
                                {

                                    *pSrc[2] = *(pDst++);
                                    *pSrc[1] = *(pDst++);
                                    *pSrc[0] = *(pDst++);

                                    // inc. IMG pointers
                                    for (int k = 0; k < 3; k++)
                                        pSrc[k] += xInc[k];
                                }

                                // jump to the stride
                                for (int k = 0; k < bmData.Stride - bmData.Width * 3; k++)
                                    pDst++;
                            }
                        }

                        // the confused VPAT case (e.g. unsorted multitap linescan)
                        else
                        {
                            // get VPAT access to the image data
                            for (int k = 0; k < 3; k++)
                            {
                                Cvb.Image.GetImageVPA(img, k, out pBase[k], out addrVPAT[k]);
                                pVPAT[k] = (Interop.Common.CVB.cStruct.VPAEntry*)addrVPAT[k].ToPointer();
                            }

                            // the pointers to the start of the lines of the IMG
                            byte*[] pSrc = new byte*[3];
                            for (int y = 0; y < nHeight; y++)
                            {
                                // init the pointer to the start of line y of the IMG
                                for (int k = 0; k < 3; k++)
                                    pSrc[k] = (byte*)pBase[k] + pVPAT[k][y].YEntry.ToInt64();
                                for (int x = 0; x < nWidth; x++)
                                {
                                    // copy the rgb (bgr) pixel
                                    *(pSrc[2] + pVPAT[2][x].XEntry.ToInt64()) = *(pDst++);
                                    *(pSrc[1] + pVPAT[1][x].XEntry.ToInt64()) = *(pDst++);
                                    *(pSrc[0] + pVPAT[0][x].XEntry.ToInt64()) = *(pDst++);
                                }
                                // jump to the stride
                                for (int k = 0; k < bmData.Stride - bmData.Width * 3; k++)
                                    pDst++;
                            }
                        }

                        // unlock the bitmap bits
                        bm.UnlockBits(bmData);
                        return outImage;
                    }
                default:
                    {
                        return outImage;
                    }
            }
        }

        /// <summary>
        /// 디멘져 1로 바꾸기 ( 결과 흑백으로 변환됨)
        /// </summary>
        /// <param name="_tmp">ref AxCVIMAGELib.AxCVimage</param>
        /// <returns>boot</returns>
        public static bool SetConvertImageDimensionOne(ref AxCVIMAGELib.AxCVimage _tmp)
        {
            bool rtn = false;
            //Cvb.Image.IMG temoDemesion = 0;//; new Cvb.Image.IMG();
            Cvb.SharedImg temoDemesion = new Cvb.SharedImg();

            try
            {
                if (Cvb.Image.ImageDimension(_tmp.Image) > 0)
                {
                    //빈 이미지 생성
                    Cvb.Image.CreateGenericImage(1, _tmp.ImageWidth, _tmp.ImageHeight, false, out temoDemesion);
                    Cvb.Image.TArea t = new Cvb.Image.TArea();
                    t.X0 = 0;
                    t.X1 = 0;
                    t.X2 = _tmp.ImageWidth - 1;
                    t.Y0 = 0;
                    t.Y1 = _tmp.ImageHeight - 1;
                    t.Y2 = 0;

                    if (Cvb.Image.CopyImageArea(_tmp.Image, temoDemesion, 0, 0, t, 0, 0))
                    {
                        rtn = true;

                        _tmp.Image = temoDemesion;

                        // Cvb.Image.CreateDuplicateImage(temoDemesion, out _tmp);

                    }
                    //CreateGenericImage 1, CVimage1.ImageWidth, CVimage1.ImageHeight, 0, lImgTemp
                    //'원본 이미지를 새로운 이미지에 카피
                    //CopyImageArea CVimage1.Image, lImgTemp, 0, 0, 0, 0, 0, CVimage1.ImageHeight - 1, CVimage1.ImageWidth - 1, 0, 0, 0

                }
                else
                {
                    //    //ImageFolder = temoDemesion;
                    //    Cvb.Image.CreateDuplicateImage(_tmp.Image, out _tmp.Image);
                    rtn = false;
                }
            }
            catch { }
            finally
            {
                //if ( Cvb.Image.IsImage( temoDemesion ) )
                //{
                //    Cvb.Image.ReleaseImage( temoDemesion );
                //    temoDemesion = 0;
                //}
            }

            return rtn;
        }

        #region 이미지 회전

        /// <summary>
        ///  이미지 회전
        /// </summary>
        /// <param name="_imageObject">Cvb.Image.IMG</param>
        /// <param name="_roation">회전방향</param>
        /// <param name="_outImage">결과: out Cvb.Image.IMG </param>
        /// <returns>bool</returns>

        public static Cvb.SharedImg ImageRotation180(Cvb.Image.IMG _imageObject)
        {
            //Cvb.SharedImg tmpCopyImage1 = new Cvb.SharedImg(); 
            Cvb.Image.TArea CopyArea = new Cvb.Image.TArea();

            int w = Cvb.Image.ImageWidth((Cvb.Image.IMG)_imageObject);
            int h = Cvb.Image.ImageHeight((Cvb.Image.IMG)_imageObject);
            CopyArea.X0 = w;// Cvb.Image.ImageWidth( tmpImage );
            CopyArea.X1 = w;//Cvb.Image.ImageWidth( tmpImage );
            CopyArea.X2 = 0;
            CopyArea.Y0 = h;//Cvb.Image.ImageHeight( tmpImage );
            CopyArea.Y1 = 0;
            CopyArea.Y2 = h;//Cvb.Image.ImageHeight( tmpImage );

            return CreateSubAreaImage(_imageObject, CopyArea);
        }


        public static bool ImageRotation(Cvb.Image.IMG _imageObject, Interop.Common.CVB.cEnum.eRoation _roation, out Cvb.Image.IMG _outImage)
        {
            // Cvb.Image.IMG ExchangeImage;
            Cvb.Image.TArea CopyArea = new Cvb.Image.TArea();

            bool isrtn = false;

            int w = Cvb.Image.ImageWidth((Cvb.Image.IMG)_imageObject);
            int h = Cvb.Image.ImageHeight((Cvb.Image.IMG)_imageObject);

            _outImage = 0;

            //  Introp.CVB.Util.ConvertObjectToImage( _imageObject );

            switch (_roation)
            {
                case Interop.Common.CVB.cEnum.eRoation.LEFT: //시계 반대 방향으로 90 도 회전
                    CopyArea.X0 = w;// Cvb.Image.ImageWidth( tmpImage ); ;
                    CopyArea.X1 = 0;
                    CopyArea.X2 = w;//Cvb.Image.ImageWidth( tmpImage );
                    CopyArea.Y0 = 0;
                    CopyArea.Y1 = 0;
                    CopyArea.Y2 = h;// Cvb.Image.ImageHeight( tmpImage););                  
                    break;

                case Interop.Common.CVB.cEnum.eRoation.RIGHT: // 90도 //시계 방향으로 90 도 회전                
                    CopyArea.X0 = 0;
                    CopyArea.X1 = w;//Cvb.Image.ImageWidth( tmpImage ); 
                    CopyArea.X2 = 0;
                    CopyArea.Y0 = h;//Cvb.Image.ImageHeight( tmpImage );
                    CopyArea.Y1 = h;//Cvb.Image.ImageHeight( tmpImage );
                    CopyArea.Y2 = 0;
                    break;

                case Interop.Common.CVB.cEnum.eRoation.OneEighty: // 180도
                    CopyArea.X0 = w;// Cvb.Image.ImageWidth( tmpImage );
                    CopyArea.X1 = w;//Cvb.Image.ImageWidth( tmpImage );
                    CopyArea.X2 = 0;
                    CopyArea.Y0 = h;//Cvb.Image.ImageHeight( tmpImage );
                    CopyArea.Y1 = 0;
                    CopyArea.Y2 = h;//Cvb.Image.ImageHeight( tmpImage );
                    break;

                case Interop.Common.CVB.cEnum.eRoation.FlipX180:
                case Interop.Common.CVB.cEnum.eRoation.FlipY180:
                    Cvb.Image.CreateGenericImage((int)Interop.Common.CVB.cEnum.eDIMESION.COLOR, w, h, false, out _outImage);
                    break;

                default:
                    break;
            }

            switch (_roation)
            {
                case Interop.Common.CVB.cEnum.eRoation.LEFT:
                case Interop.Common.CVB.cEnum.eRoation.RIGHT:
                case Interop.Common.CVB.cEnum.eRoation.OneEighty:
                    _outImage = CreateSubAreaImage((Cvb.Image.IMG)_imageObject, CopyArea);
                    isrtn = Cvb.Image.IsImage(_outImage);

                    break;

                case Interop.Common.CVB.cEnum.eRoation.FlipY180:
                    {
                        System.Drawing.Image img = CvbImageToBitmap(_imageObject, out isrtn);
                        if (isrtn)
                        {
                            //img.RotateFlip( RotateFlipType.Rotate180FlipY ); // 180 도 회전
                            img.RotateFlip(RotateFlipType.RotateNoneFlipY);///좌우 반전
                            isrtn = CopyBitmapToIMGbits(ref _outImage, (Bitmap)img);
                        }
                    }
                    break;

                case Interop.Common.CVB.cEnum.eRoation.FlipX180:
                    {
                        //     bool ist=false;
                        Image img = CvbImageToBitmap(_imageObject, out isrtn);
                        if (isrtn)
                        {
                            img.RotateFlip(RotateFlipType.RotateNoneFlipX); // 상하 반전
                            isrtn = CopyBitmapToIMGbits(ref _outImage, (Bitmap)img);
                        }
                    }
                    break;


            }
            // Cvb.Image.ReleaseObject( ref tmpImage );
            //Cvb.Image.ReleaseObject( ref ExchangeImage );

            return isrtn;

        }

        public static Cvb.SharedImg ImageRotation(Cvb.Image.IMG _imageObject, Interop.Common.CVB.cEnum.eRoation _roation)
        {
            // Cvb.Image.IMG ExchangeImage;
            Cvb.Image.TArea CopyArea = new Cvb.Image.TArea();
            Cvb.SharedImg outImage = new Cvb.SharedImg();

            int w = Cvb.Image.ImageWidth((Cvb.Image.IMG)_imageObject);
            int h = Cvb.Image.ImageHeight((Cvb.Image.IMG)_imageObject);

            switch (_roation)
            {
                case Interop.Common.CVB.cEnum.eRoation.LEFT: //시계 반대 방향으로 90 도 회전
                    CopyArea.X0 = w;// Cvb.Image.ImageWidth( tmpImage ); ;
                    CopyArea.X1 = 0;
                    CopyArea.X2 = w;//Cvb.Image.ImageWidth( tmpImage );
                    CopyArea.Y0 = 0;
                    CopyArea.Y1 = 0;
                    CopyArea.Y2 = h;// Cvb.Image.ImageHeight( tmpImage););                  
                    break;

                case Interop.Common.CVB.cEnum.eRoation.RIGHT: // 90도 //시계 방향으로 90 도 회전                
                    CopyArea.X0 = 0;
                    CopyArea.X1 = w;//Cvb.Image.ImageWidth( tmpImage ); 
                    CopyArea.X2 = 0;
                    CopyArea.Y0 = h;//Cvb.Image.ImageHeight( tmpImage );
                    CopyArea.Y1 = h;//Cvb.Image.ImageHeight( tmpImage );
                    CopyArea.Y2 = 0;
                    break;

                case Interop.Common.CVB.cEnum.eRoation.OneEighty: // 180도
                    CopyArea.X0 = w;// Cvb.Image.ImageWidth( tmpImage );
                    CopyArea.X1 = w;//Cvb.Image.ImageWidth( tmpImage );
                    CopyArea.X2 = 0;
                    CopyArea.Y0 = h;//Cvb.Image.ImageHeight( tmpImage );
                    CopyArea.Y1 = 0;
                    CopyArea.Y2 = h;//Cvb.Image.ImageHeight( tmpImage );
                    break;

                case Interop.Common.CVB.cEnum.eRoation.FlipX180:
                case Interop.Common.CVB.cEnum.eRoation.FlipY180:
                    Cvb.Image.CreateGenericImage((int)Interop.Common.CVB.cEnum.eDIMESION.COLOR, w, h, false, out outImage);
                    break;

                default:
                    break;
            }

            bool isrtn = false;

            switch (_roation)
            {
                case Interop.Common.CVB.cEnum.eRoation.LEFT:
                case Interop.Common.CVB.cEnum.eRoation.RIGHT:
                case Interop.Common.CVB.cEnum.eRoation.OneEighty:
                    outImage = CreateSubAreaImage((Cvb.Image.IMG)_imageObject, CopyArea);
                    break;

                case Interop.Common.CVB.cEnum.eRoation.FlipY180:
                    {                  
                        System.Drawing.Image img = CvbImageToBitmap(_imageObject, out isrtn);
                        if (isrtn)
                        {
                            //img.RotateFlip( RotateFlipType.Rotate180FlipY ); // 180 도 회전
                            img.RotateFlip(RotateFlipType.RotateNoneFlipY);///좌우 반전
                            outImage = CopyBitmapToIMGbits(_imageObject, (Bitmap)img, 0);
                        }
                    }
                    break;

                case Interop.Common.CVB.cEnum.eRoation.FlipX180:
                    {
                        //     bool ist=false;
                        Image img = CvbImageToBitmap(_imageObject, out isrtn);
                        if (isrtn)
                        {
                            img.RotateFlip(RotateFlipType.RotateNoneFlipX); // 상하 반전
                            outImage = CopyBitmapToIMGbits(_imageObject, (Bitmap)img, 0);
                        }
                    }
                    break;
            }

            return outImage;

        }







        /// <summary>
        /// 회전영역 생성
        /// </summary>
        /// <param name="_inImg"></param>
        /// <param name="_inArea"></param>
        /// <param name="_outImg"></param>
        /// <returns></returns>
        private static Cvb.SharedImg CreateSubAreaImage(Cvb.Image.IMG _inImg, Cvb.Image.TArea _inArea)
        {
            Cvb.Image.TArea AreaNull;
            Cvb.Image.TCoordinateMap csTemp;
            Cvb.Image.TCoordinateMap csNull;
            Cvb.Image.TMatrix Matrix;

            Cvb.SharedImg tmpOutImage;

            if (!Cvb.Image.IsImage(_inImg))
            {
                return new Cvb.SharedImg();
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
            Cvb.Image.CreateSubImage(_inImg, AreaNull, out tmpOutImage);
            // ' Restore cs
            Cvb.Image.SetImageCoordinates(_inImg, csTemp);
            // ' Set cs of the new image
            Cvb.Image.InitCoordinateMap(out csNull);
            Cvb.Image.SetImageCoordinates(tmpOutImage, csNull);

            return tmpOutImage;
        }

        #endregion

        // Cvb.Image.IMG 복사
        public static Cvb.SharedImg DuplicateImage(Cvb.Image.IMG _image)
        {
            Cvb.SharedImg tmpCopyImage1 = new Cvb.SharedImg();
            Cvb.Image.CreateDuplicateImage(_image, out tmpCopyImage1);

            return tmpCopyImage1;
        }

        /// <summary>
        ///  컬러를 흑백으로 변경 - 화면 출력 용
        /// </summary>
        /// <param name="_img"></param>
        /// <returns></returns>
        public static Cvb.SharedImg ConvertTo8BitMonoLMI(AxCVIMAGELib.AxCVimage _img)
        {
            Cvb.SharedImg sResultImage = null;
            Cvb.SharedImg sImageTemp = null; //new Cvb.SharedImg(new IntPtr(_img.Image));
            // 16비트 칼라 일때만
            if (Cvb.Image.ImageDatatype(_img.Image, 0) == 16 && Cvb.Image.ImageDimension(_img.Image) == 3)
            {
                ////1..상하 반전
                ////MirrorImage ImgIN, 1, 0, lImgRot    'CVB 함수 버그, x를 1로 주어야 y 반전 됨
                //Cvb.Foundation.MirrorImage(_img.Image, true, false, out sImageTemp);

                //if (sImageTemp == null || sImageTemp == 0)
                //{
                //}

                ////2 16bit 를 8bit 변환 ( 칼라 )
                //Cvb.Image.MapTo8Bit(sImageTemp, false, out sImageTemp);

                //////3. 칼라를 흑백으로 변환
                //Cvb.Foundation.ConvertRGBToGrayScaleStandard(sImageTemp, out sResultImage);


                //if (sResultImage == null || sResultImage == 0)
                //{
                //}



                ////1..상하 반전 -  가끔씩 결과값을 리턴 하지 못하는 현상 발생
                //Cvb.Foundation.MirrorImage(_img.Image, true, false, out sImageTemp);

                //2 16bit 를 8bit 변환 ( 칼라 )
                Cvb.Image.MapTo8Bit(_img.Image, false, out sImageTemp);
                ////3. 칼라를 흑백으로 변환
                Cvb.Foundation.ConvertRGBToGrayScaleStandard(sImageTemp, out sResultImage);

                // Cvb.Image.IMG kkk = DuplicateImage(_img.Image);
                // Bitmap tmit = new Bitmap(_img.ImageWidth, _img.ImageHeight, PixelFormat.Format48bppRgb);
                //// bool bt;
                // // tmit = CvbImageToBitmap(_img.Image, out bt);
                // CopyIMGbitsToBitmap(_img.Image, ref tmit);

                // CopyBitmapToIMGbits(ref  kkk, tmit);

                // sResultImage = DuplicateImage(kkk);

                // Cvb.Image.ReleaseObject(ref kkk);


            }

            return sResultImage;
        }

        /// <summary>
        ///  검사용 이미지 - 16 비트 칼라를 16 비트 흑백으로 생성( Blue 로 )
        /// </summary>
        /// <param name="_img"></param>
        /// <returns></returns>
        public static Cvb.SharedImg ConvertTo16BitMonoLMI(AxCVIMAGELib.AxCVimage _img)
        {
           // Cvb.SharedImg sImageRoationTemp = null;// new Cvb.SharedImg();
            Cvb.SharedImg sImage16BitMono = null; ;// new Cvb.SharedImg(); //16비트 모노
            
            // 16비트 칼라 일때만
            if (Cvb.Image.ImageDatatype(_img.Image, 0) == 16 && Cvb.Image.ImageDimension(_img.Image) == 3)
            {                
                Cvb.Image.TArea t = new Cvb.Image.TArea();
                t.X0 = 0;
                t.X1 = 0;
                t.X2 = _img.ImageWidth - 1;
                t.Y0 = 0;
                t.Y1 = _img.ImageHeight - 1;
                t.Y2 = 0;
 
                ////1..상하 반전 - CVB 에서 에러 발생.
                ////MirrorImage ImgIN, 1, 0, lImgRot    'CVB 함수 버그, x를 1로 주어야 y 반전 됨
                // Cvb.Foundation.MirrorImage(_img.Image, true, false, out sImageRoationTemp);

                //if (sImageRoationTemp == null || sImageRoationTemp == 0)
                //{
                //}
                
                //Cvb.Image.CreateGenericImageDT(1, Cvb.Image.ImageWidth(_img.Image), Cvb.Image.ImageHeight(_img.Image), 16, out sImage16BitMono);  //'검사용
                //Cvb.Image.CopyImageArea(sImageRoationTemp, sImage16BitMono, 2, 0, t, 0, 0);  // RGB 중 Blue 에  높이정보가 포함되어 있다.
 
                Cvb.Image.CreateGenericImageDT(1, Cvb.Image.ImageWidth(_img.Image), Cvb.Image.ImageHeight(_img.Image), 16, out sImage16BitMono);  //'검사용 빈 이미지 생성
                Cvb.Image.CopyImageArea(_img.Image, sImage16BitMono, 2, 0, t, 0, 0);  // RGB 중 Blue 에  높이정보가 포함되어 있다. 

            }

            return sImage16BitMono;
        }
 
        /// <summary>
        ///  검사용- 8 bit Color - 미사용
        /// </summary>
        /// <param name="_img"></param>
        /// <returns></returns>
        public static Cvb.SharedImg ConvertTo8BitColoLMI(AxCVIMAGELib.AxCVimage _img)
        {
            Cvb.SharedImg sResultImage = new Cvb.SharedImg();
            // Cvb.SharedImg sImageTemp = new Cvb.SharedImg();

            Cvb.SharedImg sImageRoationTemp = new Cvb.SharedImg();
            Cvb.SharedImg sImage16BitColorTemp = new Cvb.SharedImg();
            Cvb.SharedImg sImage8BitColorTemp = new Cvb.SharedImg();

            // LookupTable
            int[] valuesR = null; ;
            int[] valuesG = null; ;
            int[] valuesB = null; ;
            Cvb.SharedImg sImageTemp1 = new Cvb.SharedImg();
            Cvb.SharedImg sImageTemp2 = new Cvb.SharedImg();
            Cvb.SharedImg sImageTemp3 = new Cvb.SharedImg();


            Cvb.Image.TArea t = new Cvb.Image.TArea();
            t.X0 = 0;
            t.X1 = 0;
            t.X2 = _img.ImageWidth - 1;
            t.Y0 = 0;
            t.Y1 = _img.ImageHeight - 1;
            t.Y2 = 0;

            // 16비트 칼라 일때만
            if (Cvb.Image.ImageDatatype(_img.Image, 0) == 16 && Cvb.Image.ImageDimension(_img.Image) == 3)
            {
                ////1..상하 반전 - C# 에서 사용 하면 에러 발생
                ////MirrorImage ImgIN, 1, 0, lImgRot    'CVB 함수 버그, x를 1로 주어야 y 반전 됨
                //Cvb.Foundation.MirrorImage(_img.Image, true, false, out sImageRoationTemp);
                Cvb.Image.CopyImageArea(_img.Image, sImageRoationTemp, 1, 0, t, 0, 0); 


                ////2.16비트  이미지 생성 (원본 이미지와 동일 크기의 빈 이미지)
                //Cvb.Image.CreateGenericImageDT(1, Cvb.Image.ImageWidth(_img.Image), Cvb.Image.ImageHeight(_img.Image), 16, out sImage16BitColorTemp);  //'검사용  

                ////3. 회전한 이미를 임시 저정
                //Cvb.Image.CopyImageArea(sImageRoationTemp, sImage16BitColorTemp, 1, 0, t, 0, 0);

                ////4. 16bit 를 8bit 변환 ( 칼라 )
                //Cvb.Image.MapTo8Bit(sImage16BitColorTemp, false, out sImage8BitColorTemp);

                

                //2.16비트  이미지 생성 (원본 이미지와 동일 크기의 빈 이미지)
                Cvb.Image.CreateGenericImageDT(1, Cvb.Image.ImageWidth(_img.Image), Cvb.Image.ImageHeight(_img.Image), 16, out sImage16BitColorTemp);  //'검사용  

                //3. 회전한 이미를 임시 저정
                Cvb.Image.CopyImageArea(sImageRoationTemp, sImage16BitColorTemp, 1, 0, t, 0, 0);

                //4. 16bit 를 8bit 변환 ( 칼라 )
                Cvb.Image.MapTo8Bit(sImage16BitColorTemp, false, out sImage8BitColorTemp);


                // LookUp Table 적용
                mGetLookup(ref valuesR, ref valuesG, ref valuesB);
                Cvb.Foundation.ApplyLUT8Bit(sImage8BitColorTemp, 0, valuesR, out sImageTemp1);
                Cvb.Foundation.ApplyLUT8Bit(sImage8BitColorTemp, 0, valuesG, out sImageTemp2);
                Cvb.Foundation.ApplyLUT8Bit(sImage8BitColorTemp, 0, valuesB, out sImageTemp3);

                //결과  이미지생성  ( 칼라 )
                Cvb.Image.CreateGenericImage(3, Cvb.Image.ImageWidth(sImage8BitColorTemp), Cvb.Image.ImageHeight(sImage8BitColorTemp), false, out sResultImage);

                //lookup 적용된 이미지를 바탕 이미지에 각 plane별로 카피
                Cvb.Image.CopyImageArea(sImageTemp1, sResultImage, 0, 0, t, 0, 0);
                Cvb.Image.CopyImageArea(sImageTemp2, sResultImage, 0, 1, t, 0, 0);
                Cvb.Image.CopyImageArea(sImageTemp3, sResultImage, 0, 2, t, 0, 0);
            }

            return sResultImage;
        }

        /// <summary>
        /// 칼라를 흑백으로 변환하기위한 LookUp Table
        /// </summary>
        /// <param name="_R"></param>
        /// <param name="_G"></param>
        /// <param name="_B"></param>
        private static void mGetLookup(ref int[] _R, ref int[] _G, ref int[] _B)
        {

            _R = new int[] { 0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,
                                    0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,
                                    0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,
                                    0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,
                                    0	,0	,0	,5	,10	,15	,20	,26	,31	,36	,41	,46	,51	,56	,61	,66	,71	,77	,82	,87	,92	,97	,102	,107	,112	,117	,122	,128	,133	,138	,143	,148	,153	,158	,163	,168	,173	,
                                    179	,184	,189	,194	,199	,204	,209	,214	,219	,224	,230	,235	,240	,245	,250	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,
                                    255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255
                                  };

            _G = new int[] {  0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,
                                    0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,5	,10	,15	,20	,26	,31	,36	,41	,46	,51	,56	,61	,66	,71	,77	,82	,87	,92	,97	,102	,107	,112	,117	,
                                    122	,128	,133	,138	,143	,148	,153	,158	,163	,168	,173	,179	,184	,189	,194	,199	,204	,209	,214	,219	,224	,230	,235	,240	,245	,250	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,
                                    255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,
                                    255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,
                                    255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,250	,245	,240	,235	,230	,224	,219	,214	,209	,204	,199	,194	,189	,184	,179	,173	,168	,163	,158	,153	,148	,
                                    143	,138	,133	,128	,122	,117	,112	,107	,102	,97	,92	,87	,82	,77	,71	,66	,61	,56	,51	,46	,41	,36	,31	,26	,20	,15	,10	,5	,0	,0	,0	,0	,0	,0	
                                 };

            _B = new int[] {  10	,11	,12	,15	,20	,26	,31	,36	,41	,46	,51	,56	,61	,66	,71	,77	,82	,87	,92	,97	,102	,107	,112	,117	,122	,128	,133	,138	,143	,148	,153	,158	,163	,168	,173	,179	,184	,
                                    189	,194	,199	,204	,209	,214	,219	,224	,230	,235	,240	,245	,250	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,
                                    255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,255	,250	,245	,240	,235	,230	,224	,219	,214	,209	,204	,
                                    199	,194	,189	,184	,179	,173	,168	,163	,158	,153	,148	,143	,138	,133	,128	,122	,117	,112	,107	,102	,97	,92	,87	,82	,77	,71	,66	,61	,56	,51	,46	,41	,36	,31	,26	,20	,15	,
                                    10	,5	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,
                                    0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,
                                    0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,0	,1	,2	,3	,4	,5	
                                  };
        }


        /// <summary>
        ///  이미지 Pixel 정보에서 LMI 정보를 가져 옴
        /// </summary>
        /// <param name="_img"></param>
        /// <param name="_info">ref </param>
        public static void mGetLMISenserInfo(Cvb.Image.IMG _img, ref   cStruct.sLMI_StampInfo _info)
        {
            //int val0 = 0;
            //int val1 = 0;
            //int val2 = 0;
            //int val3 = 0;
            //long valResult = 0; 
            _info.Init();

            {
                // 16Bit
                IntPtr pBase;
                int nXInc, nYInc;
                int x = 0;
                int y = 0;
                if (Cvb.Utilities.GetLinearAccess(_img, 0, out pBase, out nXInc, out nYInc))
                {
                    unsafe
                    {
                        System.UInt16* pPixel;

                        x = (int)cEnum.eLmiStampInfo.VERSION4;
                        _info.Version = *((System.UInt16*)((int)pBase + x * nXInc + y * nYInc));

                        x = (int)cEnum.eLmiStampInfo.FRAME_COUNT4;
                        _info.FrameCount = *((System.UInt16*)((int)pBase + x * nXInc + y * nYInc));

                        //x = (int)cEnum.eLmiStampInfo.TIEMSTAMP4 ;
                        //_info.Timestamp = *((System.UInt16*)((int)pBase + x * nXInc + y * nYInc));           //(us)

                        x = (int)cEnum.eLmiStampInfo.ENCODER_VALUE4;
                        _info.EncoderValue = *((System.UInt16*)((int)pBase + x * nXInc + y * nYInc));       //ticks

                        x = (int)cEnum.eLmiStampInfo.ENCODER_INDEX4;
                        _info.EncoderIndex = *((System.UInt16*)((int)pBase + x * nXInc + y * nYInc));       //ticks

                        x = (int)cEnum.eLmiStampInfo.DIGITAL_INPUT_STATES4;
                        _info.DigitalInputStates = *((System.UInt16*)((int)pBase + x * nXInc + y * nYInc));

                        x = (int)cEnum.eLmiStampInfo.X_OFFSET4;
                        _info.XOffset = *((System.UInt16*)((int)pBase + x * nXInc + y * nYInc));

                        x = (int)cEnum.eLmiStampInfo.X_RESOLUTION4;
                        _info.XResolution = *((System.UInt16*)((int)pBase + x * nXInc + y * nYInc)) * 0.000001d;         //mm (기본 값은 um)

                        x = (int)cEnum.eLmiStampInfo.Y_OFFSET4;
                        _info.YOffset = *((System.UInt16*)((int)pBase + x * nXInc + y * nYInc));

                        x = (int)cEnum.eLmiStampInfo.Y_RESOLUTION4;
                        _info.YResolution = *((System.UInt16*)((int)pBase + x * nXInc + y * nYInc)) * 0.000001d;           //mm (기본 값은 um)

                        x = (int)cEnum.eLmiStampInfo.Z_OFFSET4;
                        _info.ZOffset = *((System.UInt16*)((int)pBase + x * nXInc + y * nYInc)) * 0.000001d;

                        x = (int)cEnum.eLmiStampInfo.Z_RESOLUTION4;
                        _info.ZResolution = *((System.UInt16*)((int)pBase + x * nXInc + y * nYInc)) * 0.000001d;         //mm (기본 값은 um)

                        x = (int)cEnum.eLmiStampInfo.HEIGHT_MAP_WIDTH4;
                        _info.HeightMasterpWidth = *((System.UInt16*)((int)pBase + x * nXInc + y * nYInc));   //      'pixels

                        x = (int)cEnum.eLmiStampInfo.HEIGHT_MAP_HEIGHT4;
                        _info.HeightMasterpHeight = *((System.UInt16*)((int)pBase + x * nXInc + y * nYInc));  //    'pixels

                        //pPixel = (System.UInt16*)((int)pBase + x * nXInc + y * nYInc);
                        //System.Diagnostics.Debug.WriteLine(*((System.UInt16*)pPixel)); 
                    }
                }
            }
        }
        public static void mGetLMISenserInfo_New(Cvb.Image.IMG _img, ref   cStruct.sLMI_StampInfo _info)
        {
            //int val0 = 0;
            //int val1 = 0;
            //int val2 = 0;
            //int val3 = 0;
            //long valResult = 0; 
            _info.Init();

            {
                // 16Bit
                IntPtr pBase;
                int nXInc, nYInc;
                int x = 0;
                int y = 0;
                int tmp_01 = 0;
                int tmp_02 = 0;
                int tmp_03 = 0;
                int tmp_04 = 0;
                int val_01 = 0;
                int val_02 = 0;
                int val_03 = 0;
                int val_04 = 0;
                ///////////////////////////////////////////////////////////////////////////////////
                if ( Cvb.Utilities.GetLinearAccess( _img, 2, out pBase, out nXInc, out nYInc ) )
                {
                    unsafe
                    {
                        System.UInt16* pPixel;

                        tmp_01 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.VERSION1 * nXInc + y * nYInc ) );
                        tmp_02 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.VERSION2 * nXInc + y * nYInc ) );
                        tmp_03 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.VERSION3 * nXInc + y * nYInc ) );
                        tmp_04 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.VERSION4 * nXInc + y * nYInc ) );
                        val_01 = tmp_01 << 48;
                        val_02 = tmp_02 << 32;
                        val_03 = tmp_03 << 16;
                        val_04 = tmp_04 << 0;

                        _info.Version = ( val_01 + val_02 + val_03 + val_04 );

                        tmp_01 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.FRAME_COUNT1 * nXInc + y * nYInc ) );
                        tmp_02 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.FRAME_COUNT2 * nXInc + y * nYInc ) );
                        tmp_03 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.FRAME_COUNT3 * nXInc + y * nYInc ) );
                        tmp_04 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.FRAME_COUNT4 * nXInc + y * nYInc ) );
                        val_01 = tmp_01 << 48;
                        val_02 = tmp_02 << 32;
                        val_03 = tmp_03 << 16;
                        val_04 = tmp_04 << 0;

                        _info.FrameCount = ( val_01 + val_02 + val_03 + val_04 );

                        tmp_01 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.ENCODER_VALUE1 * nXInc + y * nYInc ) );
                        tmp_02 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.ENCODER_VALUE2 * nXInc + y * nYInc ) );
                        tmp_03 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.ENCODER_VALUE3 * nXInc + y * nYInc ) );
                        tmp_04 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.ENCODER_VALUE4 * nXInc + y * nYInc ) );
                        val_01 = tmp_01 << 48;
                        val_02 = tmp_02 << 32;
                        val_03 = tmp_03 << 16;
                        val_04 = tmp_04 << 0;

                        _info.EncoderValue = ( val_01 + val_02 + val_03 + val_04 );

                        tmp_01 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.ENCODER_INDEX1 * nXInc + y * nYInc ) );
                        tmp_02 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.ENCODER_INDEX2 * nXInc + y * nYInc ) );
                        tmp_03 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.ENCODER_INDEX3 * nXInc + y * nYInc ) );
                        tmp_04 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.ENCODER_INDEX4 * nXInc + y * nYInc ) );
                        val_01 = tmp_01 << 48;
                        val_02 = tmp_02 << 32;
                        val_03 = tmp_03 << 16;
                        val_04 = tmp_04 << 0;

                        _info.EncoderIndex = ( val_01 + val_02 + val_03 + val_04 );

                        tmp_01 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.DIGITAL_INPUT_STATES1 * nXInc + y * nYInc ) );
                        tmp_02 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.DIGITAL_INPUT_STATES2 * nXInc + y * nYInc ) );
                        tmp_03 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.DIGITAL_INPUT_STATES3 * nXInc + y * nYInc ) );
                        tmp_04 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.DIGITAL_INPUT_STATES4 * nXInc + y * nYInc ) );
                        val_01 = tmp_01 << 48;
                        val_02 = tmp_02 << 32;
                        val_03 = tmp_03 << 16;
                        val_04 = tmp_04 << 0;

                        _info.DigitalInputStates = ( val_01 + val_02 + val_03 + val_04 );

                        tmp_01 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.X_OFFSET1 * nXInc + y * nYInc ) );
                        tmp_02 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.X_OFFSET2 * nXInc + y * nYInc ) );
                        tmp_03 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.X_OFFSET3 * nXInc + y * nYInc ) );
                        tmp_04 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.X_OFFSET4 * nXInc + y * nYInc ) );
                        val_01 = tmp_01 << 48;
                        val_02 = tmp_02 << 32;
                        val_03 = tmp_03 << 16;
                        val_04 = tmp_04 << 0;

                        _info.XOffset = ( val_01 + val_02 + val_03 + val_04 ) * 0.000001d;

                        tmp_01 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.X_RESOLUTION1 * nXInc + y * nYInc ) );
                        tmp_02 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.X_RESOLUTION2 * nXInc + y * nYInc ) );
                        tmp_03 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.X_RESOLUTION3 * nXInc + y * nYInc ) );
                        tmp_04 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.X_RESOLUTION4 * nXInc + y * nYInc ) );
                        val_01 = tmp_01 << 48;
                        val_02 = tmp_02 << 32;
                        val_03 = tmp_03 << 16;
                        val_04 = tmp_04 << 0;

                        _info.XResolution = ( val_01 + val_02 + val_03 + val_04 ) * 0.000001d;

                        tmp_01 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.Y_OFFSET1 * nXInc + y * nYInc ) );
                        tmp_02 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.Y_OFFSET2 * nXInc + y * nYInc ) );
                        tmp_03 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.Y_OFFSET3 * nXInc + y * nYInc ) );
                        tmp_04 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.Y_OFFSET4 * nXInc + y * nYInc ) );
                        val_01 = tmp_01 << 48;
                        val_02 = tmp_02 << 32;
                        val_03 = tmp_03 << 16;
                        val_04 = tmp_04 << 0;

                        _info.YOffset = ( val_01 + val_02 + val_03 + val_04 ) * 0.000001d;

                        tmp_01 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.Y_RESOLUTION1 * nXInc + y * nYInc ) );
                        tmp_02 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.Y_RESOLUTION2 * nXInc + y * nYInc ) );
                        tmp_03 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.Y_RESOLUTION3 * nXInc + y * nYInc ) );
                        tmp_04 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.Y_RESOLUTION4 * nXInc + y * nYInc ) );
                        val_01 = tmp_01 << 48;
                        val_02 = tmp_02 << 32;
                        val_03 = tmp_03 << 16;
                        val_04 = tmp_04 << 0;

                        _info.YResolution = ( val_01 + val_02 + val_03 + val_04 ) * 0.000001d;

                        tmp_01 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.Z_OFFSET1 * nXInc + y * nYInc ) );
                        tmp_02 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.Z_OFFSET2 * nXInc + y * nYInc ) );
                        tmp_03 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.Z_OFFSET3 * nXInc + y * nYInc ) );
                        tmp_04 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.Z_OFFSET4 * nXInc + y * nYInc ) );
                        val_01 = tmp_01 << 48;
                        val_02 = tmp_02 << 32;
                        val_03 = tmp_03 << 16;
                        val_04 = tmp_04 << 0;

                        _info.ZOffset = ( val_01 + val_02 + val_03 + val_04 ) * 0.000001d;

                        tmp_01 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.Z_RESOLUTION1 * nXInc + y * nYInc ) );
                        tmp_02 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.Z_RESOLUTION2 * nXInc + y * nYInc ) );
                        tmp_03 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.Z_RESOLUTION3 * nXInc + y * nYInc ) );
                        tmp_04 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.Z_RESOLUTION4 * nXInc + y * nYInc ) );
                        val_01 = tmp_01 << 48;
                        val_02 = tmp_02 << 32;
                        val_03 = tmp_03 << 16;
                        val_04 = tmp_04 << 0;

                        _info.ZResolution = ( val_01 + val_02 + val_03 + val_04 ) * 0.000001d;

                        tmp_01 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.HEIGHT_MAP_WIDTH1 * nXInc + y * nYInc ) );
                        tmp_02 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.HEIGHT_MAP_WIDTH2 * nXInc + y * nYInc ) );
                        tmp_03 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.HEIGHT_MAP_WIDTH3 * nXInc + y * nYInc ) );
                        tmp_04 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.HEIGHT_MAP_WIDTH4 * nXInc + y * nYInc ) );
                        val_01 = tmp_01 << 48;
                        val_02 = tmp_02 << 32;
                        val_03 = tmp_03 << 16;
                        val_04 = tmp_04 << 0;

                        _info.HeightMasterpWidth = ( val_01 + val_02 + val_03 + val_04 );

                        tmp_01 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.HEIGHT_MAP_HEIGHT1 * nXInc + y * nYInc ) );
                        tmp_02 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.HEIGHT_MAP_HEIGHT2 * nXInc + y * nYInc ) );
                        tmp_03 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.HEIGHT_MAP_HEIGHT3 * nXInc + y * nYInc ) );
                        tmp_04 = *( (System.UInt16*)( (int)pBase + (int)cEnum.eLmiStampInfo.HEIGHT_MAP_HEIGHT4 * nXInc + y * nYInc ) );
                        val_01 = tmp_01 << 48;
                        val_02 = tmp_02 << 32;
                        val_03 = tmp_03 << 16;
                        val_04 = tmp_04 << 0;

                        _info.HeightMasterpHeight = ( val_01 + val_02 + val_03 + val_04 );
                    }
                }
            }
        }

        public static void mSetImageMantoInfo(ref Cvb.Image.IMG _img, int _mantoQty)
        {
            int tInx = 0;
            tInx = 1;
            Cvb.Image.SetPixel(_img, 0, 0, 0, ref tInx); // Manto

            tInx = _mantoQty;//
            Cvb.Image.SetPixel(_img, 0, 0, 1, ref tInx); // Manto
        }

        public static int mGetImageMantoInfo(Cvb.Image.IMG _img)
        {
            int tInx = 0;

            Cvb.Image.GetPixel(_img, 0, 0, 0,out tInx);
            if (tInx == 1)
            {
                Cvb.Image.GetPixel(_img, 0, 0, 1, out tInx);
            }

            return tInx;
        }


        /// <summary>
        ///  이미지 Pixel 정보에서 LMI 정보를 가져 옴
        /// </summary>
        /// <param name="_img"></param>
        /// <param name="_info"></param>
        public static void mGetLMISenserInfoGetPixel(Cvb.Image.IMG _img, ref cStruct.sLMI_StampInfo _info)
        {

            int val0 = 0;
            int val1 = 0;
            int val2 = 0;
            int val3 = 0;
            long valResult = 0;


            _info.Init();

            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.VERSION1, 0, out val0);
            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.VERSION2, 0, out val1);
            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.VERSION3, 0, out val2);
            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.VERSION4, 0, out val3);

            valResult = val3;
            _info.Version = valResult;

            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.FRAME_COUNT1, 0, out val0);
            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.FRAME_COUNT2, 0, out val1);
            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.FRAME_COUNT3, 0, out val2);
            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.FRAME_COUNT4, 0, out val3);

            valResult = val3;
            _info.FrameCount = valResult;


            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.TIEMSTAMP1, 0, out val0);
            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.TIEMSTAMP2, 0, out val1);
            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.TIEMSTAMP3, 0, out val2);
            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.TIEMSTAMP4, 0, out val3);

            valResult = val3;
            _info.Timestamp = valResult;

            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.ENCODER_VALUE1, 0, out val0);
            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.ENCODER_VALUE2, 0, out val1);
            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.ENCODER_VALUE3, 0, out val2);
            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.ENCODER_VALUE4, 0, out val3);

            valResult = val3;
            _info.EncoderValue = valResult;


            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.ENCODER_INDEX1, 0, out val0);
            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.ENCODER_INDEX2, 0, out val1);
            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.ENCODER_INDEX3, 0, out val2);
            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.ENCODER_INDEX4, 0, out val3);

            valResult = val3;
            _info.EncoderIndex = valResult;


            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.DIGITAL_INPUT_STATES1, 0, out val0);
            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.DIGITAL_INPUT_STATES2, 0, out val1);
            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.DIGITAL_INPUT_STATES3, 0, out val2);
            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.DIGITAL_INPUT_STATES4, 0, out val3);

            valResult = val3;
            _info.DigitalInputStates = valResult;

            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.X_OFFSET1, 0, out val0);
            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.X_OFFSET2, 0, out val1);
            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.X_OFFSET3, 0, out val2);
            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.X_OFFSET4, 0, out val3);

            valResult = val3;
            _info.XOffset = valResult;


            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.X_RESOLUTION1, 0, out val0);
            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.X_RESOLUTION2, 0, out val1);
            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.X_RESOLUTION3, 0, out val2);
            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.X_RESOLUTION4, 0, out val3);

            valResult = val3;
            _info.XResolution = valResult * 0.000001d;

            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.Y_OFFSET1, 0, out val0);
            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.Y_OFFSET2, 0, out val1);
            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.Y_OFFSET3, 0, out val2);
            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.Y_OFFSET4, 0, out val3);

            valResult = val3;
            _info.YOffset = valResult;


            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.Y_RESOLUTION1, 0, out val0);
            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.Y_RESOLUTION2, 0, out val1);
            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.Y_RESOLUTION3, 0, out val2);
            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.Y_RESOLUTION4, 0, out val3);

            valResult = val3;
            _info.YResolution = valResult * 0.000001d;


            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.Z_OFFSET1, 0, out val0);
            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.Z_OFFSET2, 0, out val1);
            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.Z_OFFSET3, 0, out val2);
            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.Z_OFFSET4, 0, out val3);

            valResult = val3;
            _info.ZOffset = valResult;


            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.Z_RESOLUTION1, 0, out val0);
            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.Z_RESOLUTION2, 0, out val1);
            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.Z_RESOLUTION3, 0, out val2);
            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.Z_RESOLUTION4, 0, out val3);

            valResult = val3;
            _info.ZResolution = valResult * 0.000001d;


            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.HEIGHT_MAP_WIDTH1, 0, out val0);
            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.HEIGHT_MAP_WIDTH2, 0, out val1);
            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.HEIGHT_MAP_WIDTH3, 0, out val2);
            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.HEIGHT_MAP_WIDTH4, 0, out val3);

            valResult = val3;
            _info.HeightMasterpWidth = valResult;

            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.HEIGHT_MAP_HEIGHT1, 0, out val0);
            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.HEIGHT_MAP_HEIGHT2, 0, out val1);
            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.HEIGHT_MAP_HEIGHT3, 0, out val2);
            Cvb.Image.GetPixel(_img, 0, (int)cEnum.eLmiStampInfo.HEIGHT_MAP_HEIGHT4, 0, out val3);

            valResult = val3;
            _info.HeightMasterpHeight = valResult;

        }


        /// <summary>
        ///  LMI 원복 이미지에서 특정 영역의 높이값을 가져 온다.
        ///  실제 값을 사용하기 위해서는  ZResolution을 곱해 준다.
        /// </summary>
        /// <param name="_img">LMI 원본 이미지</param>
        /// <param name="_left">검사영역</param>
        /// <param name="_top">검사영역</param>
        /// <param name="_right">검사영역</param>
        /// <param name="_bottom">검사영역</param>
        /// <returns></returns>
        public static int mGetLMISenserHValue(AxCVIMAGELib.AxCVimage _img, int _left, int _top, int _right, int _bottom)
        {
                 int pixCount = 0;
                 int hValue = 0;
                 float hValueSum = 0;
                // int kkk;
            // get vpat access to the image
            //int nWidth = this.axCVimage3.ImageWidth;
            //int nHeight = axCVimage3.ImageHeight;

                 try
                 {

                     for (int i = 0; i < _img.ImageDimension; i++)
                     {
                         if (_img.DataType(0) == 16)
                         {
                             // 16Bit
                             IntPtr pBase;
                             int nXInc, nYInc;
                             if (Cvb.Utilities.GetLinearAccess(_img.Image, i, out pBase, out nXInc, out nYInc))
                             {
                                 unsafe
                                 {
                                     System.UInt16* pPixel;
                                     for (int y = _top; y <= _bottom; y++)
                                         for (int x = _left; x <= _right; x++)
                                         {
                                             pPixel = (System.UInt16*)((int)pBase + x * nXInc + y * nYInc);

                                             // 32768 보다 픽셀 크면 음수 이므로 유효하지 않은 값이다.
                                             if (*((System.UInt16*)pPixel) > 0)
                                             {
                                                 // this.stLmiStmpInfo.ZOffset + 36175 *  this.stLmiStmpInfo.ZResolution 
                                                 hValueSum += *((System.UInt16*)pPixel);
                                                 pixCount++;
                                             }
                                             //else
                                             //{
                                             //    *((System.UInt16*)pPixel) = (System.UInt16)(65534 - *((System.UInt16*)pPixel)); // 반전 시키는것                                  
                                             //} 

                                             //Cvb.Image.GetPixel(_img.Image, 0, x, y, out kkk);  // VB 에서만 사용. 속도가 마니 느림.
                                             //if (kkk > 0)
                                             //{
                                             //    System.Diagnostics.Debug.WriteLine(kkk);
                                             //}

                                         }
                                 }
                             }



                             // 10Bit
                             //IntPtr pBase;
                             //int nXInc, nYInc;
                             //if (Cvb.Utilities.GetLinearAccess(m_cvImage.Image, i, out pBase, out nXInc, out nYInc))
                             //{
                             //    unsafe
                             //    {
                             //        UInt16* pPixel;
                             //        for (int y = nYStart; y <= nYStop; y++)
                             //            for (int x = nXStart; x <= nXStop; x++)
                             //            {
                             //                pPixel = (UInt16*)((int)pBase + x * nXInc + y * nYInc);
                             //                *((UInt16*)pPixel) = (UInt16)(1023 - *((UInt16*)pPixel));
                             //            }
                             //    }
                             //}  

                             //8bit
                             //IntPtr pBase;
                             //int nXInc, nYInc;
                             //if (Cvb.Utilities.GetLinearAccess(m_cvImage.Image, i, out pBase, out nXInc, out nYInc))
                             //{
                             //    unsafe
                             //    {
                             //        byte* pPixel;
                             //        for (int y = nYStart; y <= nYStop; y++)
                             //            for (int x = nXStart; x <= nXStop; x++)
                             //            {
                             //                pPixel = (byte*)((int)pBase + x * nXInc + y * nYInc);
                             //                *((byte*)pPixel) = (byte)(255 - *((byte*)pPixel));
                             //            }
                             //    }
                             //}


                             // 8bit
                             //IntPtr pBase;
                             //IntPtr pVPAT;
                             //Cvb.Image.GetImageVPA(axCVimage3.Image, i, out pBase, out pVPAT);
                             //unsafe
                             //{
                             //8bit
                             //byte* pPixel;
                             //for (int y = nYStart; y <= nYStop; y++)
                             //    for (int x = nXStart; x <= nXStop; x++)
                             //    {
                             //        pPixel = (byte*)((int)pBase + (int)((Cvb.Image.VPAEntry*)pVPAT)[x].XEntry + (int)((Cvb.Image.VPAEntry*)pVPAT)[y].YEntry);
                             //           * ((byte*)pPixel) = (byte)(255 - *((byte*)pPixel)); // 반전 시키는거
                             //    }
                             // }

                        if (pixCount == 0) hValue = 0;
                        else hValue = (int)(hValueSum / pixCount);
                    }
                } // end for
            }
            catch
            {
                hValue = 0;
            }
            return hValue;
        }


        private static void mSetLMISenserInfoCopy(ref Cvb.Image.IMG _inImg, int _startY)
        {
            // Resolution 값만 필요 함으로 필요한 값만 복원 한다.

            Interop.Common.CVB.cStruct.sLMI_StampInfo sLmiInfo = new Interop.Common.CVB.cStruct.sLMI_StampInfo();

            int tmpValue = 0;

            if (Cvb.Image.ImageDimension(_inImg) == 3)
            {
                Interop.Common.CVB.Util.UtilImage.mGetLMISenserInfo(_inImg, ref sLmiInfo);

                //tmpValue = (int)sLmiInfo.Version;
                //Cvb.Image.SetPixel(_inImg, 0, (int)cEnum.eLmiStampInfo.VERSION4, _startY, ref tmpValue);

                //tmpValue = (int)sLmiInfo.FrameCount;
                //Cvb.Image.SetPixel(_inImg, 0, (int)cEnum.eLmiStampInfo.FRAME_COUNT4, _startY, ref tmpValue);

                //tmpValue = (int)sLmiInfo.Timestamp;
                //Cvb.Image.SetPixel(_inImg, 0, (int)cEnum.eLmiStampInfo.TIEMSTAMP4, _startY, ref tmpValue);

                //tmpValue = (int)sLmiInfo.EncoderIndex;
                //Cvb.Image.SetPixel(_inImg, 0, (int)cEnum.eLmiStampInfo.ENCODER_INDEX4, _startY, ref tmpValue);

                //tmpValue = (int)sLmiInfo.DigitalInputStates;
                //Cvb.Image.SetPixel(_inImg, 0, (int)cEnum.eLmiStampInfo.DIGITAL_INPUT_STATES4, _startY, ref tmpValue);

                //tmpValue = (int)sLmiInfo.XOffset;
                //Cvb.Image.SetPixel(_inImg, 0, (int)cEnum.eLmiStampInfo.X_OFFSET4, _startY, ref tmpValue);

                tmpValue = (int)(sLmiInfo.XResolution * 1000000d);
                Cvb.Image.SetPixel(_inImg, 0, (int)cEnum.eLmiStampInfo.X_RESOLUTION4, _startY, ref tmpValue);

                //tmpValue = (int)sLmiInfo.YOffset;
                //Cvb.Image.SetPixel(_inImg, 0, (int)cEnum.eLmiStampInfo.Y_OFFSET4, _startY, ref tmpValue);

                tmpValue = (int)(sLmiInfo.YResolution * 1000000d);
                Cvb.Image.SetPixel(_inImg, 0, (int)cEnum.eLmiStampInfo.Y_RESOLUTION4, _startY, ref tmpValue);

                //tmpValue = (int)sLmiInfo.ZOffset;
                //Cvb.Image.SetPixel(_inImg, 0, (int)cEnum.eLmiStampInfo.Z_OFFSET4, _startY, ref tmpValue);

                tmpValue = (int)(sLmiInfo.ZResolution * 1000000d);
                Cvb.Image.SetPixel(_inImg, 0, (int)cEnum.eLmiStampInfo.Z_RESOLUTION4, _startY, ref tmpValue);

                //tmpValue = (int)sLmiInfo.HeightMasterpWidth;
                //Cvb.Image.SetPixel(_inImg, 0, (int)cEnum.eLmiStampInfo.HEIGHT_MAP_WIDTH4, _startY, ref tmpValue);

                //tmpValue = (int)sLmiInfo.HeightMasterpHeight;
                //Cvb.Image.SetPixel(_inImg, 0, (int)cEnum.eLmiStampInfo.HEIGHT_MAP_HEIGHT4, _startY, ref tmpValue);
            }


        }


        /// <summary>
        /// 이미지를 일정 비율만큼 Cut 한다.
        /// </summary>
        /// <param name="_inImgLIB">원본 AxCVimage</param>
        /// <param name="_inImg">원본 Image</param>
        /// <param name="_endPointRate"></param>
        /// <returns></returns>
        public static Cvb.SharedImg ConvertImageCut(AxCVIMAGELib.AxCVimage _inImgLIB, Cvb.Image.IMG _inImg, float _endPointRate)
        {
            return ConvertImageCut(_inImgLIB, _inImg, _endPointRate, 0);
        }

        /// <summary>
        /// 이미지를 일정 비율만큼 Cut 한다.
        /// </summary>
        /// <param name="_inImgLIB">원본 AxCVimage</param>
        /// <param name="_inImg">원본 Image</param>
        /// <param name="_endPointRate">End Rate</param>
        /// <param name="_startPointRate">start Rate</param>
        /// <returns></returns>
        public static Cvb.SharedImg ConvertImageCut(AxCVIMAGELib.AxCVimage _inImgLIB, Cvb.Image.IMG _inImg, float _endPointRate, float _startPointRate)
        {
            Cvb.SharedImg tmpOutImage = new Cvb.SharedImg();
            Cvb.Image.TArea AreaNull;
            Cvb.Image.TCoordinateMap csTemp;
            Cvb.Image.TCoordinateMap csNull;
            Cvb.Image.TMatrix Matrix;

            Cvb.Image.TArea _inArea = new Cvb.Image.TArea();
            _inArea.X0 = 0;
            _inArea.X1 = 0;
            _inArea.X2 = _inImgLIB.ImageWidth - 1;
            _inArea.Y0 = (int)(_inImgLIB.ImageHeight * _startPointRate);
            _inArea.Y1 = (int)(_inImgLIB.ImageHeight * _endPointRate) - 1;
            _inArea.Y2 = _inArea.Y0;


            if (!Cvb.Image.IsImage(_inImg))
            {
                return new Cvb.SharedImg();
            }

            mSetLMISenserInfoCopy(ref _inImg, (int)_inArea.Y0);

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
            Cvb.Image.CreateSubImage(_inImg, AreaNull, out tmpOutImage);
            // ' Restore cs
            Cvb.Image.SetImageCoordinates(_inImg, csTemp);
            // ' Set cs of the new image
            Cvb.Image.InitCoordinateMap(out csNull);
            Cvb.Image.SetImageCoordinates(tmpOutImage, csNull);

            return tmpOutImage;
        }
    }
}
