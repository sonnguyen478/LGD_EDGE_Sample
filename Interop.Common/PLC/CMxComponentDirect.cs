using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACTMULTILib; // C:\MELSEC\Act\Control\actmult.dll 참조

using Interop.Common.Util;



//14.04.16 짬나는 때 작업할 것들
//1. 시뮬레이션 기능 (상호 내부 메모리만 가지고 처리되도록 해보자)
//2. wordbit를 별도의 변수로 두지 않고 bit변환 명령만 써서 사용하자(wordbit는 word의 연속 번지가 대부분이다. 
//(고민할 부분은 wordbit부분만 Read, Write되어야 시간 loss가 없기는 하다. word를 읽으면서 wordbit영역을 갱신하는건 무리가 없지만 wordbit를 읽기 위해서 word영역을 같이 갱신한다. 효율성은 좀...
//필요한 갯수만큼만 업데이트하는걸 만들까??? 시작번지에서 필요한 개수만큼만...

namespace Interop.Common.PLC.Melsec 
{
    public class CMxComponentDirect
    {

        //public delegate void LogDelegate(string sMsg, CLog.eLogType eLogType);
        //public event LogDelegate evLog ;


        public void evLogStr(string _message, Interop.Common.Util.cLog.eLogType _log)
        {
            //if (evLog != null)
            //{
            //    // Event 가 등록되어 있는 경우
            //    evLog(_message, _log);
            //}
        }


        private ActEasyIFClass com_ReferencesEasyIF = new ActEasyIFClass();
        private Encoding objAsciiCodePageEncoding = Encoding.Default;

        //BitSize = 1는 2Byte를 의미함.
        private const short BitSize = 1;
        private const short WordSize = 1;
        private const short Word2Size = 2;
        private const short Word4Size = 4;

        private const short stationNum = 0;

        private int iRet = 0;

        //Melsec Open 상태
        public bool isOpen = false;

        //delegate 처리 유무
        //public bool isDelegate = false;

        public string sBitHeartBit = "0";
        //현재 Bit를 가짐
        public string sBitTrigger = "0";

        //public string strStateBit = "1111111111";
        public string sBitState = "1";

        //ReadDeviceRandom		: 1Word용으로는 사용 가능함. 읽기 변수가 Int
        //ReadDeviceRandom2		: 1Word용으로 사용 가능함. 읽기 변수가 Short
        //ReadDeviceBlock		: Bit, Word용으로 사용 가능. 읽기 변수가 Int
        //ReadDeviceBlock2		: Bit, Word용으로 사용 가능. 읽기 변수가 Short
        //WriteDeviceRandom		: 1BIt 단위로 처리 가능. 쓰기 변수가 Int
        //WriteDeviceRandom2	: 1BIt 단위로 처리 가능. 쓰기 변수가 Short
        //WriteDeviceBlock		: 임의의 단위로 처리 가능. 쓰기 변수가 Int
        //WriteDeviceBlock2		: 임의의 단위로 처리 가능. 쓰기 변수가 Short

        //~CMxComponent_MC() {
        //    Channel_Close();
        //}

        //private string sBitPrefix = "M";    //통상적으로 'B' 또는 'M'임
        //private string sWordPrefix = "D";   //통상적으로 'W' 또는 'D'임

        //12.12.2
        //1. 상대방의 Addr 배치가 Dec단위 인지 Hex단위인지 맞추어서 사용해야 한다.
        //2. Bit 와 Word를 분리하여 사용하는 경우 
        //shWordPLC의 크기와 iWordPLCAddrStartCnt는 동일한 크기를 가진다
        //하지만 1Word를 나누어 bit로 쓰는 경우 성능 향상을 위해 최소의
        //영역만 읽기 위해서는 shWordPLC개수와 iWordBitPLCAddrStartCnt/iWordPLCAddrStartCnt가
        //서로 다르게 설정 될 수 있따.

        //확인완료
        public void Channel_Open()
        {
            try
            {
                com_ReferencesEasyIF.ActLogicalStationNumber = stationNum;

                if (!isOpen)
                {
                    iRet = com_ReferencesEasyIF.Open();

                    if (iRet == 0)
                    {
                        isOpen = true;
                    }
                    else
                    {
                        evLogStr( "Error CMxComponentDirect_" + "Channel_Open " + iRet, Interop.Common.Util.cLog.eLogType.EXCEPTION );
                    }
                }
            }
            catch (Exception ex)
            {
                evLogStr( "Error CMxComponentDirect_" + "Channel_Open " + ex.ToString(), Interop.Common.Util.cLog.eLogType.EXCEPTION );
            }
        }

        //확인완료
        public void Channel_Open(int _stationNumber)
        {
            try
            {
                com_ReferencesEasyIF.ActLogicalStationNumber = _stationNumber;

                if (!isOpen)
                {
                    iRet = com_ReferencesEasyIF.Open();

                    if (iRet == 0)
                    {
                        isOpen = true;
                    }
                    else
                    {
                        evLogStr( "Error CMxComponentDirect_" + "Channel_Open " + iRet, Interop.Common.Util.cLog.eLogType.EXCEPTION );
                    }
                }
            }
            catch (Exception ex)
            {
                evLogStr( "Error CMxComponentDirect_" + "Channel_Open " + ex.ToString(), Interop.Common.Util.cLog.eLogType.EXCEPTION );
            }
        }

        //확인완료
        public void Channel_Close()
        {
            try
            {
                iRet = 99;

                if (isOpen)
                {
                    iRet = com_ReferencesEasyIF.Close();

                    if (iRet == 0)
                    {
                        isOpen = false;
                    }
                    else
                    {
                        evLogStr( "Error CMxComponentDirect_" + "Channel_Close " + iRet, Interop.Common.Util.cLog.eLogType.EXCEPTION );
                    }
                }
            }
            catch (Exception ex)
            {
                evLogStr( "Error CMxComponentDirect_" + "Channel_Close " + ex.ToString(), Interop.Common.Util.cLog.eLogType.EXCEPTION );
            }
        }

        short DevValue = 0;
        //string DevName = string.Empty;

        //M, B는 Ethernet 이상만 사용하자
        //확인완료
        //Bit On
        public void Bit_On(string _Addr)
        {
            //if (!isOpen)
            //    return;

            //DevName = string.Format("B{0:X4}", Addr);
            //DevValue = 1;
            //iRet = com_ReferencesEasyIF.WriteDeviceRandom2(DevName, 1, ref DevValue);

            //if (iRet != 0) {
            //    CLOG.FileWrite_Str("Error CMxComponentDirect_Bit_On " + iRet, CLog.eLogType.EXCEPTION);
            //    MessageBox.Show("Error CMxComponentDirect_Bit_On " + iRet);
            //}
            Bit_OnOff(_Addr, true);
        }

        //확인완료
        //Bit Off
        public void Bit_Off(string _Addr)
        {
            //if (!isOpen)
            //    return;

            //DevName = string.Format("B{0:X4}", Addr);
            //DevValue = 0;
            //iRet = com_ReferencesEasyIF.WriteDeviceRandom2(DevName, 1, ref DevValue);

            //if (iRet != 0) {
            //    CLOG.FileWrite_Str("Error CMxComponentDirect_Bit_Off " + iRet, CLog.eLogType.EXCEPTION);
            //    MessageBox.Show("Error CMxComponentDirect_Bit_Off " + iRet);
            //}
            Bit_OnOff(_Addr, false);
        }

        //확인완료
        //Bit를 On/Off할 때
        public void Bit_OnOff(string _Addr, bool _Trigger)
        {
            if (!isOpen) return;

            try
            {
                DevValue = (short)(_Trigger ? 1 : 0);
                iRet = com_ReferencesEasyIF.WriteDeviceRandom2(_Addr, 1, ref DevValue);

                //if (_Trigger)
                //{
                //    //On
                //    //WriteDeviceRandom2는 Bit일 경우 1bit, Word일 경우 1Word임
                //    DevValue = 1;
                //    iRet = com_ReferencesEasyIF.WriteDeviceRandom2(_Addr, 1, ref DevValue);
                //}
                //else
                //{
                //    //Off
                //    DevValue = 0;
                //    iRet = com_ReferencesEasyIF.WriteDeviceRandom2(_Addr, 1, ref DevValue);
                //}

                if (iRet != 0)
                {
                    evLogStr( "Error CMxComponentDirect_" + "Bit_OnOff " + _Trigger + " " + iRet, Interop.Common.Util.cLog.eLogType.EXCEPTION );
                }
            }
            catch (Exception ex)
            {
                evLogStr( "Error CMxComponentDirect_" + "Bit_OnOff " + ex.ToString(), Interop.Common.Util.cLog.eLogType.EXCEPTION );
            }

        }

        //확인완료
        //Trigger Bit를 읽기
        public string Bit_Read(string _Addr)
        {
            if (!isOpen) return "".PadLeft(16, '0');

            short buf = 0;

            try
            {
                //ReadDeviceBlock2는 Block단위로 처리하며 1 Block는 16bit=2byte=1word임
                //short[] buf = new short[16];
                //iRet = com_ReferencesEasyIF.ReadDeviceBlock2(DevName, 16, out buf[0x00]);

                //BitSize = 1는 2Byte를 의미함.
                iRet = com_ReferencesEasyIF.ReadDeviceBlock2(_Addr, BitSize, out buf);

                if (iRet != 0)
                {
                    evLogStr( "Error CMxComponentDirect_" + "Read_Bit " + iRet, Interop.Common.Util.cLog.eLogType.EXCEPTION );
                    //MessageBox.Show("Error CMxComponentDirect_Read_Bit " + iRet);
                    return "".PadLeft(16, '0');
                }

                //2진수 -> 10진수
                //int nVal = System.Convert.ToInt32("100", 2);
                //10진수 -> 2진수
                //string strVla = System.Convert.ToString(nVal, 2);
            }
            catch (Exception ex)
            {
                //CLOG.FileWrite_Str("Error CMxComponentDirect_Write_2Word " + ex, CLog.eLogType.EXCEPTION);
                evLogStr( "Error CMxComponentDirect_" + "Read_Bit " + ex.ToString(), Interop.Common.Util.cLog.eLogType.EXCEPTION );
            }

            return mDec2Bit(buf);
        }

        #region Word에서 1Bit On/Off 하기
        ////Word로 처리시 임의 한 Bit만 변경처리할 때 사용 가능
        //char[] c0x201 = {'0','0','0','0',
        //                 '0','0','0','0',
        //                 '0','0','0','0',
        //                 '0','0','0','0'};

        //public void Write_Word_Bit_OnOff(int _inx, Boolean isOnOff)
        //{
        //    //char[] chars = strData.ToCharArray(); //String일때 Char로 변환하기
        //    c0x201[_inx] = (isOnOff) ? '1' : '0';    //원본값을 변경 시킴
        //    char[] chars = (char[])c0x201.Clone();   //Clone() 안하면 주소복사가 됨.
        //    Array.Reverse(chars);               //기계어 순서에 맞게 뒤집기
        //    string _sTmp = new string(chars);
        //    int nVal = System.Convert.ToInt16(_sTmp, 2);
        //    //System.Diagnostics.Debug.WriteLine(_sTmp);
        //    //System.Diagnostics.Debug.WriteLine(nVal);
        //    Write_Word(0x201, nVal);

        //    //Write_Word_Bit_PC_Sync();
        //}

        //Write Bit는 자기 영역에만 사용하기 때문에 Write PLC는 만들지 않음
        public void WordBit_OnOff(string _Addr, int _inx, Boolean _isOnOff)
        {
            if (!isOpen) return;

            try
            {
                int buf = Word_Read(_Addr);
                string sVal = mDec2Bit((short)buf);
                sVal = WordBit_1BitOnOff(sVal, _inx, _isOnOff);

                //char[] chars = strData.ToCharArray(); //String일때 Char로 변환하기
                //c0x201[_inx] = (isOnOff) ? '1' : '0';    //원본값을 변경 시킴
                //char[] chars = (char[])c0x201.Clone();   //Clone() 안하면 주소복사가 됨.
                //Array.Reverse(chars);               //기계어 순서에 맞게 뒤집기
                //string _sTmp = new string(chars);
                //int nVal = System.Convert.ToInt16(_sTmp, 2);
                //System.Diagnostics.Debug.WriteLine(_sTmp);
                //System.Diagnostics.Debug.WriteLine(nVal);
                int iVal = mBit2Dec(sVal);
                Word_Write(_Addr, iVal);	//Sync는 WordBit_Write_PC에서 처리됨.
            }
            catch (Exception ex)
            {
                //CLOG.FileWrite_Str("Error CMxComponentDirect_Write_2Word " + ex, CLog.eLogType.EXCEPTION);
                evLogStr( "Error CMxComponentDirect_" + "WordBit_OnOff " + ex.ToString(), Interop.Common.Util.cLog.eLogType.EXCEPTION );
            }
        }

        public string WordBit_1BitOnOff(string _sBuf, int _inx, Boolean _isOnOff)
        {
            char[] cBit;
            string _sTmp = string.Empty;

            try
            {
                //string sBitTemp = string.Empty;
                cBit = _sBuf.ToCharArray();
                //Array.Reverse(cBit);
                //Addr -= iWordBitPLCAddrStart;

                cBit[_inx] = (_isOnOff) ? '1' : '0';    //원본값을 변경 시킴
                //char[] chars = (char[])cBit.Clone();   //Clone() 안하면 주소복사가 됨.
                //Array.Reverse(chars);               //기계어 순서에 맞게 뒤집기
                //string _sTmp = new string(chars);
                
                _sTmp = new string(cBit);
            }
            catch (Exception ex)
            {
                //CLOG.FileWrite_Str("Error CMxComponentDirect_Write_2Word " + ex, CLog.eLogType.EXCEPTION);
                evLogStr( "Error CMxComponentDirect_" + "WordBit_1BitOnOff " + ex.ToString(), Interop.Common.Util.cLog.eLogType.EXCEPTION );
            }

            //int iVal = System.Convert.ToInt16(_sTmp, 2);
            //int iVal = mBit2Dec(_sTmp);
            //return iVal;
            return _sTmp;
        }

        //public void WordBit_Write(string _Addr, Int32 _WordData)
        //{		//Int로 만든 이유 : 정수로 +-를 넣을 경우는 문제 없으나 Int16에서는 0xFFFF가 입력범위 초과로 됨.
        //    if (!isOpen) return;

        //    try
        //    {
        //        iRet = com_ReferencesEasyIF.WriteDeviceBlock(_Addr, 1, ref _WordData);
        //        //iRet = com_ReferencesEasyIF.WriteDeviceRandom(_Addr, 1, ref _WordData);

        //        //if (iRet != 0) {
        //        //    //CLOG.FileWrite_Str("Error CMxComponentDirect_Write_Word " + iRet, CLog.eLogType.EXCEPTION);
        //        //    MessageBox.Show("Error CMxComponentDirect_Write_Word " + iRet);
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        //CLOG.FileWrite_Str("Error CMxComponentDirect_Write_2Word " + ex, CLog.eLogType.EXCEPTION);
        //        evLog("Error CMxComponentDirect_" + "WordBit_Write " + ex.ToString(), CLog.eLogType.EXCEPTION);
        //    }
        //}
        #endregion

        //Word 값을 읽어올 때
        public int Word_Read(string _Addr)
        {
            if (!isOpen) return 0;

            short buf = 0;

            try
            {
                //멀티 16 Block 처리 시
                //short[] buf = new short[16];
                //iRet = com_ReferencesEasyIF.ReadDeviceBlock2(DevName, 16, out buf[0x00]);

                iRet = com_ReferencesEasyIF.ReadDeviceBlock2(_Addr, WordSize, out buf);         //두개 다 사용 가능함
                //iRet = com_ReferencesEasyIF.ReadDeviceRandom2(_Addr, WordSize, out buf);		//두개 다 사용 가능함

                //if (iRet != 0) {
                //    //CLOG.FileWrite_Str("Error CMxComponentDirect_Read_Word " + iRet, CLog.eLogType.EXCEPTION);
                //    MessageBox.Show("Error CMxComponentDirect_Read_Word " + iRet);
                //}
            }
            catch (Exception ex)
            {
                //CLOG.FileWrite_Str("Error CMxComponentDirect_Write_2Word " + ex, CLog.eLogType.EXCEPTION);
                evLogStr( "Error CMxComponentDirect_" + "Word_Read " + ex.ToString(), Interop.Common.Util.cLog.eLogType.EXCEPTION );
            }

            return buf;
        }

        //Word 값을 읽어올 때
        public short[] Word_Read(string _Addr, int _iCnt)
        {
            short[] buf = new short[_iCnt];
            if (!isOpen) return buf;

            try
            {
                iRet = com_ReferencesEasyIF.ReadDeviceBlock2(_Addr, _iCnt, out buf[0x0]);
            }
            catch (Exception ex)
            {
                //CLOG.FileWrite_Str("Error CMxComponentDirect_Write_2Word " + ex, CLog.eLogType.EXCEPTION);
                evLogStr( "Error CMxComponentDirect_" + "Word_Read " + ex.ToString(), Interop.Common.Util.cLog.eLogType.EXCEPTION );
            }

            return buf;
        }

        public void Word_Write(string _Addr, int _WordData)
        {		//Int로 만든 이유 : 정수로 +-를 넣을 경우는 문제 없으나 Int16에서는 0xFFFF가 입력범위 초과로 됨.
            if (!isOpen) return;

            try
            {
                //iRet = com_ReferencesEasyIF.WriteDeviceRandom(_Addr, WordSize, ref _WordData);
                iRet = com_ReferencesEasyIF.WriteDeviceBlock(_Addr, WordSize, ref _WordData);

                //if (iRet != 0) {
                //    //CLOG.FileWrite_Str("Error CMxComponentDirect_Write_Word " + iRet, CLog.eLogType.EXCEPTION);
                //    MessageBox.Show("Error CMxComponentDirect_Write_Word " + iRet);
                //}
            }
            catch (Exception ex)
            {
                //CLOG.FileWrite_Str("Error CMxComponentDirect_Write_2Word " + ex, CLog.eLogType.EXCEPTION);
                evLogStr( "Error CMxComponentDirect_" + "Word_Write " + ex.ToString(), Interop.Common.Util.cLog.eLogType.EXCEPTION );
            }
        }

        public void Word_Write(string _Addr, int[] _WordData)
        {		//Int로 만든 이유 : 정수로 +-를 넣을 경우는 문제 없으나 Int16에서는 0xFFFF가 입력범위 초과로 됨.
            if (!isOpen) return;

            try
            {
                //iRet = com_ReferencesEasyIF.WriteDeviceRandom(_Addr, WordSize, ref _WordData);
                iRet = com_ReferencesEasyIF.WriteDeviceBlock(_Addr, _WordData.Length, ref _WordData[0]);

                //if (iRet != 0) {
                //    //CLOG.FileWrite_Str("Error CMxComponentDirect_Write_Word " + iRet, CLog.eLogType.EXCEPTION);
                //    MessageBox.Show("Error CMxComponentDirect_Write_Word " + iRet);
                //}
            }
            catch (Exception ex)
            {
                //CLOG.FileWrite_Str("Error CMxComponentDirect_Write_2Word " + ex, CLog.eLogType.EXCEPTION);
                evLogStr( "Error CMxComponentDirect_" + "Word_Write " + ex.ToString(), Interop.Common.Util.cLog.eLogType.EXCEPTION );
            }
        }

        //Word값을 개수만큼 읽어 String으로 리턴함.
        public string Word_Read_Str(string _Addr)
        {
            return Word_Read_Str(_Addr, 1);

            //string readValue = string.Empty;

            //try
            //{
            //    readValue += Hex2Str(Word_Read(_Addr));
            //}
            //catch (Exception ex)
            //{
            //    //CLOG.FileWrite_Str("Error CMxComponentDirect_Write_2Word " + ex, CLog.eLogType.EXCEPTION);
            //    evLog("Error CMxComponentDirect_" + "WordStr_Read " + ex.ToString(), CLog.eLogType.EXCEPTION);
            //}

            //return readValue;
        }

        //Word값을 개수만큼 읽어 String으로 리턴함.
        public string Word_Read_Str(string _Addr, int _iCnt)
        {
            string readValue = string.Empty;
            short[] buf = new short[_iCnt];

            try
            {
                iRet = com_ReferencesEasyIF.ReadDeviceBlock2(_Addr, _iCnt, out buf[0x00]);

                readValue = mHex2Str(buf, _iCnt);
            }
            catch (Exception ex)
            {
                //CLOG.FileWrite_Str("Error CMxComponentDirect_Write_2Word " + ex, CLog.eLogType.EXCEPTION);
                evLogStr( "Error CMxComponentDirect_" + "WordStr_Read " + ex.ToString(), Interop.Common.Util.cLog.eLogType.EXCEPTION );
            }

            return readValue;
        }

        //확인완료
        //문자를 변환하여 Addr에 쓴다
        //개수를 지정할 수 있다.
        public void Word_Write_Str(string _Addr, string _str)
        {
            ////실제 사용 시에는 홀수 일 경우 excetion이 발생한다.
            ////짝수로 맞추어서 사용하던가 
            ////If vstrcnt > 0 Then buf = Hex(Asc(Mid(vuserid, 1, 1)))
            ////If vstrcnt > 1 Then buf = Hex(Asc(Mid(vuserid, 2, 1))) & buf
            ////If vstrcnt > 0 Then Call Write_Word(wordadd, "&H" & buf)

            //try
            //{
            //    int cnt = _str.Length;

            //    if (cnt % 2 == 1) _str += " ";

            //    Word_Write(_Addr, 0);
            //    Word_Write(_Addr, Str2Hex(_str.Substring(0, 2)));
            //}
            //catch (Exception ex)
            //{
            //    //CLOG.FileWrite_Str("Error CMxComponentDirect_Write_2Word " + ex, CLog.eLogType.EXCEPTION);
            //    evLog("Error CMxComponentDirect_" + "WordStr_Write " + ex.ToString(), CLog.eLogType.EXCEPTION);
            //}
            Word_Write_Str(_Addr, _str, 1);
        }

        public void Word_Write_Str(string _Addr, string _str, int _iWordCnt)
        {
            short[] buf = new short[_iWordCnt];
            int cnt = _str.Length;

            try
            {
                //크기만큼 기본값으로 초기화 시키기
                iRet = com_ReferencesEasyIF.WriteDeviceBlock2(_Addr, _iWordCnt, ref buf[0X00]);

                if (cnt % 2 == 1) _str += " ";
                for (int i = 0 ; i < _iWordCnt ; i++)
                {
                    if (cnt > i * 2)
                    {
                        buf[i] =  (short)mStr2Hex(_str.Substring(i * 2, 2));
                    }
                }

                iRet = com_ReferencesEasyIF.WriteDeviceBlock2(_Addr, _iWordCnt, ref buf[0X00]);

                //_Cnt 크기만큼만 기록해야 되는데 문자의 크기만큼 적네... (담에 보면 수정하자)
                //if (cnt > 0) Write_Word(_Addr + 0, Str2Hex(_str.Substring(0, 2)), _isSyncWrite);
                //if (cnt > 2) Write_Word(_Addr + 1, Str2Hex(_str.Substring(2, 2)), _isSyncWrite);
                //if (cnt > 4) Write_Word(_Addr + 2, Str2Hex(_str.Substring(4, 2)), _isSyncWrite);
                //if (cnt > 6) Write_Word(_Addr + 3, Str2Hex(_str.Substring(6, 2)), _isSyncWrite);
                //if (cnt > 8) Write_Word(_Addr + 4, Str2Hex(_str.Substring(8, 2)), _isSyncWrite);
                //if (cnt > 10) Write_Word(_Addr + 5, Str2Hex(_str.Substring(10, 2)), _isSyncWrite);
                //if (cnt > 12) Write_Word(_Addr + 6, Str2Hex(_str.Substring(12, 2)), _isSyncWrite);
                //if (cnt > 14) Write_Word(Addr + 7, Str2Hex(_str.Substring(14, 2)), _isSyncWrite);
            }
            catch (Exception ex)
            {
                //CLOG.FileWrite_Str("Error CMxComponent_MC_Write_2Word " + ex, CLog.eLogType.EXCEPTION);
                evLogStr( "Error CMxComponent_MC_" + "WordStr_Write " + ex.ToString(), Interop.Common.Util.cLog.eLogType.EXCEPTION );
            }
        }

        //확인 완료
        //2Word씩 Dec값을 읽어올 때
        public int Word2_Read(string _Addr)
        {
            if (!isOpen) return 0;

            try
            {
                //int readValue = 0;
                short[] buf = new short[Word2Size];
                byte[] byarrBuffer = new byte[Word2Size * 2];
                //byte[] byarrTemp = null;

                //DevName = string.Format(sWordFirstStr + "{0:X4}", Addr);

                //short buf = 0;

                iRet = com_ReferencesEasyIF.ReadDeviceBlock2(_Addr, Word2Size, out buf[0x00]);

                //if (iRet != 0) {
                //    //CLOG.FileWrite_Str("Error CMxComponentDirect_Read_2Word " + iRet, CLog.eLogType.EXCEPTION);
                //    MessageBox.Show("Error CMxComponentDirect_Read_2Word " + iRet);
                //    return 0;
                //}

                //for (int iNum = 0 ; iNum < Word2Size ; ++iNum)
                //{
                //    //buf[iNum] = buf[iNum];
                //    byarrTemp = BitConverter.GetBytes(buf[iNum]);
                //    byarrBuffer[iNum * 2] = byarrTemp[0];
                //    byarrBuffer[iNum * 2 + 1] = byarrTemp[1];
                //}

                ////System.Diagnostics.Debug.WriteLine(BitConverter.ToInt32(byarrBuffer,0));
                //readValue = BitConverter.ToInt32(byarrBuffer, 0);

                return mWord2ToInt(buf[0], buf[1]);
            }
            catch (Exception ex)
            {
                //CLOG.FileWrite_Str("Error CMxComponentDirect_Write_2Word " + ex, CLOG.eLogType.EXCEPTION);
                evLogStr( "Error CMxComponentDirect_" + "Word2_Read " + ex.ToString(), Interop.Common.Util.cLog.eLogType.EXCEPTION );
                return 0;
            }
        }

        //2Word씩 Dec값을 쓸 대
        public void Word2_Write(string _Addr, Int64 _WordData)
        {
            //Int64로 만든 이유 : 정수로 +-를 넣을 경우는 문제 없으나 Int32에서는 0xFFFFFFFF가 입력범위 초과로 됨.
            if (!isOpen) return;

            try
            {
                //DevName = string.Format(sWordFirstStr + "{0:X4}", Addr);
                short[] DevValue = mIntToWord2(_WordData);

                iRet = com_ReferencesEasyIF.WriteDeviceBlock2(_Addr, Word2Size, ref DevValue[0x00]);

                //if (iRet != 0) {
                //CLOG.FileWrite_Str("Error CMxComponentDirect_Write_2Word " + iRet, CLOG.eLogType.EXCEPTION);
                //	MessageBox.Show("Error CMxComponentDirect_Write_2Word " + iRet);
                //}
            }
            catch (Exception ex)
            {
                //CLOG.FileWrite_Str("Error CMxComponentDirect_Write_2Word " + ex, CLOG.eLogType.EXCEPTION);
                evLogStr( "Error CMxComponentDirect_" + "Word2_Write " + ex.ToString(), Interop.Common.Util.cLog.eLogType.EXCEPTION );
            }
        }

        //2Word씩 소수점이 있는 Dec값을 읽어올 때
        public Single Word2_Read_Single_Precision(string _Addr)
        {
            if (!isOpen) return 0;

            try
            {
                //Single readValue = 0;
                short[] buf = new short[Word2Size];
                //byte[] byarrBuffer = new byte[Word2Size * 2];
                //byte[] byarrTemp = null;

                //DevName = string.Format(sWordFirstStr + "{0:X4}", Addr);

                //short buf = 0;

                iRet = com_ReferencesEasyIF.ReadDeviceBlock2(_Addr, Word2Size, out buf[0x00]);

                //if (iRet != 0) {
                //    //CLOG.FileWrite_Str("Error CMxComponentDirect_Read_2Word " + iRet, CLOG.eLogType.EXCEPTION);
                //    MessageBox.Show("Error CMxComponentDirect_Read_2Word " + iRet);
                //    return 0;
                //}

                //for (int iNum = 0 ; iNum < Word2Size ; ++iNum)
                //{
                //    //buf[iNum] = buf[iNum];
                //    byarrTemp = BitConverter.GetBytes(buf[iNum]);
                //    byarrBuffer[iNum * 2] = byarrTemp[0];
                //    byarrBuffer[iNum * 2 + 1] = byarrTemp[1];
                //}
                ////4바이트에서 변환된 단정밀도 부동 소수점 숫자 
                //readValue = BitConverter.ToSingle(byarrBuffer, 0);

                //8바이트에서 변환된 배정밀도 부동 소수점 숫자
                //BitConverter.ToDouble
                return mWord2ToSingle(buf[0], buf[1]);
            }
            catch (Exception ex)
            {
                //CLOG.FileWrite_Str("Error CMxComponentDirect_Write_2Word " + ex, CLOG.eLogType.EXCEPTION);
                evLogStr( "Error CMxComponentDirect_" + "Word2_Read_Single_Precision " + ex.ToString(), Interop.Common.Util.cLog.eLogType.EXCEPTION );
                return 0;
            }
        }

        //2Word씩 소수점이 있는 Dec값을 쓸 대
        //소수점 구분 없이 7개만 사용가능
        //더 큰 숫자를 사용하려면 Double Precession을 사용하기 바람-이거 구현 할때 아래 내용 정도는 이해하기 바람
        //LittleEndian은 통신에서 주로 쓰이며 바이트배열을 거꾸로 쓰는것을 의미한다
        //즉 0x100000을 int형 바이트 배열로 표시한때
        //00 00 10 00 으로 쓰면 LittleEndian
        //00 10 00 00 으로 쓰면 BigEndian
        public void Word2_Write_Single_Precision(string _Addr, Single _WordData)
        {
            //정수 7자리 또는 소수 7자리까지 가능함. (Ex : 9999999 or 0.1111111)
            //Int64로 만든 이유 : 정수로 +-를 넣을 경우는 문제 없으나 Int32에서는 0xFFFFFFFF가 입력범위 초과로 됨.
            if (!isOpen) return;

            try
            {
                short[] DevValue = mSingleToWord2(_WordData);

                iRet = com_ReferencesEasyIF.WriteDeviceBlock2(_Addr, Word2Size, ref DevValue[0x00]);

                //if (iRet != 0) {
                //CLOG.FileWrite_Str("Error CMxComponentDirect_Write_2Word " + iRet, CLOG.eLogType.EXCEPTION);
                //	MessageBox.Show("Error CMxComponentDirect_Write_2Word " + iRet);
                //}
            }
            catch (Exception ex)
            {
                //CLOG.FileWrite_Str("Error CMxComponentDirect_Write_2Word " + ex, CLOG.eLogType.EXCEPTION);
                evLogStr( "Error CMxComponentDirect_" + "Word2_Write_Single_Precision " + ex.ToString(), Interop.Common.Util.cLog.eLogType.EXCEPTION );
            }
        }

        //데이터 복사
        public void Word_Copy(string _AddrSrc, string _AddrDest)
        {
            Word_Copy(_AddrSrc, _AddrDest, 1);

            //try
            //{
            //    //Word_Write(_AddrDest, Word_Read(_AddrSrc));	//short변수끼리 연산하니 int가 되어 버린다. 맘 편히 int로 가자
            //}
            //catch (Exception ex)
            //{
            //    //CLOG.FileWrite_Str("Error CMxComponent_MC_Write_2Word " + ex, CLOG.eLogType.EXCEPTION);
            //    evLog("Error CMxComponentDirect_" + "Word_Copy " + ex.ToString(), CLog.eLogType.EXCEPTION);
            //}
        }

        //데이터 복사
        public void Word_Copy(string _AddrSrc, string _AddrDest, int _iWordCnt)
        {
            short[] buf = new short[_iWordCnt];

            try
            {
                iRet = com_ReferencesEasyIF.ReadDeviceBlock2(_AddrSrc, _iWordCnt, out buf[0x00]);
                iRet = com_ReferencesEasyIF.WriteDeviceBlock2(_AddrDest, _iWordCnt, ref buf[0x00]);

                //Word_Write(_AddrDest, Word_Read(_AddrSrc));	//short변수끼리 연산하니 int가 되어 버린다. 맘 편히 int로 가자
            }
            catch (Exception ex)
            {
                //CLOG.FileWrite_Str("Error CMxComponent_MC_Write_2Word " + ex, CLOG.eLogType.EXCEPTION);
                evLogStr( "Error CMxComponentDirect_" + "Word_Copy " + ex.ToString(), Interop.Common.Util.cLog.eLogType.EXCEPTION );
            }
        }

        public int mWord2ToInt(short _Data1, short _Data2)
        {
            byte[] byarrBuffer = new byte[Word2Size * 2];
            byte[] byarrTemp = BitConverter.GetBytes(_Data1);
            byarrTemp.CopyTo(byarrBuffer, 0);
            //byarrBuffer[0] = byarrTemp[0];
            //byarrBuffer[1] = byarrTemp[1];
            byarrTemp = BitConverter.GetBytes(_Data2);
            byarrTemp.CopyTo(byarrBuffer, 2);
            //byarrBuffer[2] = byarrTemp[0];
            //byarrBuffer[3] = byarrTemp[1];

            return BitConverter.ToInt32(byarrBuffer, 0);
        }

        public short[] mIntToWord2(Int64 _Data)
        {
            byte[] byarrBuffer = BitConverter.GetBytes(_Data);
            short[] DevValue = new short[2];
            DevValue[0] = BitConverter.ToInt16(byarrBuffer, 0);
            DevValue[1] = BitConverter.ToInt16(byarrBuffer, 2);
            return DevValue;
        }

        public Single mWord2ToSingle(short _Data1, short _Data2)
        {
            byte[] byarrBuffer = new byte[Word2Size * 2];
            byte[] byarrTemp = BitConverter.GetBytes(_Data1);

            byarrTemp.CopyTo(byarrBuffer, 0);
            //byarrBuffer[0] = byarrTemp[0];
            //byarrBuffer[1] = byarrTemp[1];
            byarrTemp = BitConverter.GetBytes(_Data2);
            byarrTemp.CopyTo(byarrBuffer, 2);
            //byarrBuffer[2] = byarrTemp[0];
            //byarrBuffer[3] = byarrTemp[1];

            return BitConverter.ToSingle(byarrBuffer, 0);
        }

        public short[] mSingleToWord2(Single _Data)
        {
            byte[] byarrBuffer = BitConverter.GetBytes(_Data);
            short[] DevValue = new short[2];
            DevValue[0] = BitConverter.ToInt16(byarrBuffer, 0);
            DevValue[1] = BitConverter.ToInt16(byarrBuffer, 2);
            return DevValue;
        }

        //공통화 처리 가능
        public int mBit2Dec(string _sBit)
        {
            _sBit = mStrReverse(_sBit);

            if (_sBit.Length > 16) _sBit = _sBit.Substring(0, 16); //right trim
            //_sBit = _sBit.Substring(_sBit.Length - 16, 16);  //left trim

            //16 bit 로 처리할 때 (bit16 all on일때 -1)
            return System.Convert.ToInt16(_sBit, 2);

            //32 bit 로 처리할 때 (bit16 all on일때 655535)
            //return System.Convert.ToInt32(_sBit, 2);
        }

        //public void testBitAndDec()
        //{
        //    Console.WriteLine(mDec2Bit(32767));
        //    Console.WriteLine(mDec2Bit(-1));
        //    Console.WriteLine(mBit2Dec("1111111111111111110"));

        //    Console.WriteLine(mBit2Dec(mDec2Bit(-1)));
        //    Console.WriteLine(mBit2Dec(mDec2Bit(65535)));

        //    int aa = 65535;
        //    Console.WriteLine((short)aa);
        //}

        public string mDec2Bit(int _Value)
        {
            //16bit로 처리할 때
            return mStrReverse(Convert.ToString((short)_Value, 2).PadLeft(16, '0'));

            //32bit로 처리할 때
            //return mStrReverse(Convert.ToString(_Value, 2).PadLeft(32, '0'));
        }

        //확인 완료
        //Bit 뒤집기-기본은 제일 뒤에가 0에 자리인데 제일 앞에를 0에 자리로 만든다.
        public string mStrReverse(string _sData)
        {
            try
            {
                char[] chars = _sData.ToCharArray();
                Array.Reverse(chars);

                return new string(chars);
                //return new string(chars).PadRight(16,'0');
            }
            catch (Exception ex)
            {
                //CLOG.FileWrite_Str("Error CMxComponentDirect_Write_2Word " + ex, CLOG.eLogType.EXCEPTION);
                evLogStr( "Error CMxComponentDirect_" + "strReverse " + ex.ToString(), Interop.Common.Util.cLog.eLogType.EXCEPTION );
                return "";
            }
        }

        //확인 완료
        //숫자를 문자로 변환한다.
        public string mHex2Str(int _buf)
        {
            byte[] byarrBuffer = new byte[2];
            byte[] byarrTemp = null;
            string strtmp = string.Empty;

            try
            {
                for (int iNum = 0 ; iNum < WordSize ; ++iNum)
                {
                    byarrTemp = BitConverter.GetBytes(_buf);
                    byarrBuffer[iNum] = byarrTemp[0];
                    byarrBuffer[iNum + 1] = byarrTemp[1];
                }

                strtmp = objAsciiCodePageEncoding.GetString(byarrBuffer);

                //문자 뒤집기
                //strtmp = strReverse(objAsciiCodePageEncoding.GetString(byarrBuffer));

                strtmp = strtmp.Replace("\0", "");
            }
            catch (Exception ex)
            {
                //CLOG.FileWrite_Str("Error CMxComponentDirect_Write_2Word " + ex, CLOG.eLogType.EXCEPTION);
                evLogStr( "Error CMxComponentDirect_" + "Hex2Str " + ex.ToString(), Interop.Common.Util.cLog.eLogType.EXCEPTION );
            }

            return strtmp;

            //string sret = string.Empty;
            //string sLeft = string.Empty;
            //string sRight = string.Empty;

            //try {
            //    sLeft = Convert.ToChar(buf & 0xFF).ToString();
            //    //sRight = Convert.ToChar((buf & 0xFF00) >> 8).ToString();
            //    sRight = Convert.ToChar(buf >> 8).ToString();

            //    sret = sRight + sLeft;
            //    sret = sret.Replace("\0", "");
            //} catch (Exception ex) {
            //    CLOG.FileWrite_Str("Error CMelsecNet_Hex2Str " + ex, CLOG.eLogType.EXCEPTION);
            //    MessageBox.Show("Error CMelsecNet_Hex2Str " + ex);
            //}

            //return sret;
        }

        public string mHex2Str(short[] _Data, int _iCnt)
        {
            string readValue = string.Empty;

            for (int iNum = 0 ; iNum < _iCnt ; ++iNum)
            {
                readValue += mHex2Str(_Data[iNum]);
            }

            return readValue;
        }

        //확인 완료
        //문자를 숫자로 변환한다.
        public int mStr2Hex(string _stmp)
        {
            byte[] byarrTemp = null;

            try
            {
                byarrTemp = objAsciiCodePageEncoding.GetBytes(_stmp);
            }
            catch (Exception ex)
            {
                //CLOG.FileWrite_Str("Error CMxComponentDirect_Write_2Word " + ex, CLOG.eLogType.EXCEPTION);
                evLogStr( "Error CMxComponentDirect_" + "Str2Hex " + ex.ToString(), Interop.Common.Util.cLog.eLogType.EXCEPTION );
            }

            return BitConverter.ToInt16(byarrTemp, 0);

            //int iRes = 0;
            //byte bych = 0;

            //if (stmp.Length > 2)
            //    return 0;

            //try {
            //    foreach (byte ch in stmp) {
            //        //bych = Convert.ToByte(ch);

            //        iRes = (iRes << 8) | ch;
            //    }
            //} catch (Exception ex) {
            //    CLOG.FileWrite_Str("Error CMelsecNet_Str2Hex " + ex, CLOG.eLogType.EXCEPTION);
            //    MessageBox.Show("Error CMelsecNet_Str2Hex " + ex);
            //}

            //return iRes;
        }

        //        If iReturnCode <> 0 Then
        //            DisplayErrorMessage(iReturnCode)
        //            Exit Sub
        //        End If

        //        #Region " [[[Processing of displaying error message]]] "

        //    Private Sub DisplayErrorMessage(ByVal iActReturnCode As Integer)

        //        Dim szActErrorMessage As String     'Message as the return code of ActEasyIF
        //        Dim iSupportReturnCode As Integer   'Return code of ActSupport

        //        szActErrorMessage = String.Empty

        //        'The GetErrorMessage method is executed
        //        iSupportReturnCode = AxActSupport1.GetErrorMessage(iActReturnCode, szActErrorMessage)

        //        'When ActSupport returns error code, display error code of ActEasyIF.
        //        If iSupportReturnCode <> 0 Then
        //            MsgBox("Cannot get the string data of error message." & vbLf & _
        //                   "  Error code = 0x" & Hex(iActReturnCode), _
        //                   MsgBoxStyle.Critical)
        //        Else
        //            MsgBox(szActErrorMessage, MsgBoxStyle.Critical)
        //        End If

        //    End Sub

        //#End Region


        //iReturnCode = AxActEasyIF1.FreeDeviceStatus()
        //public string Read_LED() {
        //    short buf = 0;
        //    string strState = string.Empty;

        //    iRet = mdBdLedRead(Path, ref buf);
        //    strState = buf.ToString() + " " + System.Convert.ToString(buf, 2);

        //    return strState;
        //}

        //public void Reset() {
        //    iRet = mdBdRst(Path);

        //    if (iRet != 0) {
        //        CLOG.FileWrite_Str("Error CMelsecNet_Reset " + iRet, CLOG.eLogType.EXCEPTION);
        //        MessageBox.Show("Error CMelsecNet_Reset " + iRet);
        //    }
        //}

        ////Bit On
        //public void Bit_On(int Addr) {
        //    iRet = mdDevSet(Path, StNo, DevB, Addr);

        //    if (iRet != 0) {
        //        CLOG.FileWrite_Str("Error CMelsecNet_Bit_On " + iRet, CLOG.eLogType.EXCEPTION);
        //        MessageBox.Show("Error CMelsecNet_Bit_On " + iRet);
        //    }
        //}

        ////Bit Off
        //public void Bit_Off(int Addr) {
        //    iRet = mdDevRst(Path, StNo, DevB, Addr);

        //    if (iRet != 0) {
        //        CLOG.FileWrite_Str("Error CMelsecNet_Bit_Off " + iRet, CLOG.eLogType.EXCEPTION);
        //        MessageBox.Show("Error CMelsecNet_Bit_Off " + iRet);
        //    }
        //}

        ////Bit를 On/Off할 때
        //public void Bit_OnOff(int Addr, bool Trigger) {
        //    if (Trigger) {	//On
        //        iRet = mdDevSet(Path, StNo, DevB, Addr);
        //    } else {		//Off
        //        iRet = mdDevRst(Path, StNo, DevB, Addr);
        //    }

        //    if (iRet != 0) {
        //        CLOG.FileWrite_Str("Error CMelsecNet_Bit_OnOff " + iRet, CLOG.eLogType.EXCEPTION);
        //        MessageBox.Show("Error CMelsecNet_Bit_OnOff " + iRet);
        //    }
        //}

        ////Trigger Bit를 읽기
        //public string Read_Bit(int Addr) {
        //    int buf = 0;
        //    iRet = mdReceive(Path, StNo, DevB, Addr, ref Size, ref buf);

        //    if (iRet != 0) {
        //        CLOG.FileWrite_Str("Error CMelsecNet_Read_Bit " + iRet, CLOG.eLogType.EXCEPTION);
        //        MessageBox.Show("Error CMelsecNet_Read_Bit " + iRet);
        //        return "";
        //    }

        //    //2진수 -> 10진수
        //    //int nVal = System.Convert.ToInt32("100", 2);
        //    //10진수 -> 2진수
        //    //string strVla = System.Convert.ToString(nVal, 2);

        //    string strVal = System.Convert.ToString(buf, 2);
        //    return strReverse(strVal);
        //}

        ////Word 값을 읽어올 때
        //public int Read_Word(int Addr) {
        //    short buf = 0;

        //    iRet = mdReceive(Path, StNo, DevW, Addr, ref Size, ref buf);

        //    if (iRet != 0) {
        //        CLOG.FileWrite_Str("Error CMelsecNet_Read_Word " + iRet, CLOG.eLogType.EXCEPTION);
        //        MessageBox.Show("Error CMelsecNet_Read_Word " + iRet);
        //    }

        //    return buf;

        //    #region MyRegion
        //    //if (iret == 0) {
        //    //    byte bLeft = 0;
        //    //    byte bRight = 0;

        //    //    if (readWord != 0) {
        //    //        long decs = Int64.Parse(readWord.ToString());

        //    //        string strBuffer = Convert.ToString(decs, 16);

        //    //        if (strBuffer.ToString().Length < 3)
        //    //            bLeft = Convert.ToByte(Convert.ToInt64(strBuffer.ToString().Substring(0, 2), 16));
        //    //        else {
        //    //            if (strBuffer.Length == 3) {
        //    //                bLeft = Convert.ToByte(Convert.ToInt64(strBuffer.ToString().Substring(0, 1), 16));
        //    //                bRight = Convert.ToByte(Convert.ToInt64(strBuffer.ToString().Substring(1, 2), 16));
        //    //            } else {
        //    //                bLeft = Convert.ToByte(Convert.ToInt64(strBuffer.ToString().Substring(0, 2), 16));
        //    //                bRight = Convert.ToByte(Convert.ToInt64(strBuffer.ToString().Substring(2, 2), 16));
        //    //            }
        //    //        }
        //    //    }

        //    //    return System.Text.Encoding.ASCII.GetString(new byte[] { bRight, bLeft });
        //    //} else {
        //    //    CLOG.FileWrite_Str("Error CMelsecNet_Read_Word " + iret, CLOG.eLogType.EXCEPTION);
        //    //    MessageBox.Show("Error CMelsecNet_Read_Word " + iret);
        //    //    return "";
        //    //} 
        //    #endregion
        //}

        ////2Word씩 Dec값을 읽어올 때
        //public int Read_2Word(int Addr) {
        //    try {
        //        short ret = 99;
        //        int readWord = 0;
        //        short sSize = 4;

        //        ret = mdReceive(Path, StNo, DevW, Addr, ref sSize, ref readWord);

        //        if (ret != 0)
        //            return 0;

        //        return readWord;

        //        #region MyRegion
        //        //byte bLeft = 0;
        //        //byte bRight = 0;
        //        //string AAA = string.Empty;
        //        //int BBB = 0;
        //        //string CCC = string.Empty;
        //        //string DDD = string.Empty;

        //        //CCC = "0x" + Convert.ToString(readWord, 16);

        //        //DDD = string.Format("{0:x}", readWord);

        //        //AAA = Convert.ToString(Convert.ToInt32(CCC, 16), 2);

        //        ////AAA = Convert.ToString(CCC, 2);
        //        //BBB = Convert.ToInt32(CCC, 2);
        //        //AAA.Substring(AAA.Length - 8, 8); 
        //        #endregion
        //    } catch (Exception ex) {
        //        CLOG.FileWrite_Str("Read_2Word " + ex, CLOG.eLogType.EXCEPTION);
        //        MessageBox.Show("Read_2Word " + ex);
        //        return 0;
        //    }
        //}

        ////Word에 값을 쓸대
        //public void Write_Word(Int32 Addr, int WordData) {
        //    iRet = mdSend(Path, StNo, DevW, Addr, ref Size, ref WordData);

        //    if (iRet != 0) {
        //        CLOG.FileWrite_Str("Error CMelsecNet_Write_Word ", CLOG.eLogType.EXCEPTION);
        //        MessageBox.Show("Error CMelsecNet_Write_Word ");
        //    }
        //}

        ////2Word씩 Dec값을 쓸 대
        //public bool Write_2Word(Int32 Addr, Int64 WordData) {
        //    Int16 isize = 4;

        //    short ret = 99;
        //    ret = mdSend(Path, StNo, DevW, Addr, ref isize, ref WordData);

        //    if (ret != 0)
        //        return false;

        //    return true;
        //}

        //private string strReverse(string strData) {
        //    //strData = strData.PadLeft(16, '0');
        //    //char[] chars = strData.ToCharArray();
        //    //Array.Reverse(chars);
        //    //return new string(chars);

        //    try {
        //        char[] chars = strData.ToCharArray();
        //        Array.Reverse(chars);

        //        return new string(chars).PadRight(16, '0');
        //    } catch (Exception ex) {
        //        CLOG.FileWrite_Str("Error CMelsecNet_strReverse " + ex, CLOG.eLogType.EXCEPTION);
        //        MessageBox.Show("Error CMelsecNet_strReverse " + ex);
        //        return "";
        //    }

        //    //string retData = string.Empty;
        //    //try {
        //    //    if (strData.Length < 16) {
        //    //        int nLen = 16 - strData.Length;
        //    //        string strPlus = string.Empty;

        //    //        for (int i = 0; i < nLen; i++) {
        //    //            strPlus = strPlus + "0";
        //    //        }

        //    //        strData = strPlus + strData;
        //    //    }

        //    //    for (int i = 1; i <= strData.Length; i++) {
        //    //        retData = retData + strData.Substring(strData.Length - i, 1);
        //    //    }

        //    //    return retData;
        //    //} catch (Exception ex) {
        //    //    CLOG.FileWrite_Str("Error CMelsecNet_strReverse " + ex, CLOG.eLogType.EXCEPTION);
        //    //    MessageBox.Show("Error CMelsecNet_strReverse " + ex);
        //    //    return "";
        //    //}
        //}

        //public string Hex2Str(int buf) {
        //    string sret = string.Empty;
        //    string sLeft = string.Empty;
        //    string sRight = string.Empty;

        //    try {
        //        //BitConverter.GetBytes(buf);

        //        sLeft = Convert.ToChar(buf & 0xFF).ToString();
        //        //sRight = Convert.ToChar((buf & 0xFF00) >> 8).ToString();
        //        sRight = Convert.ToChar(buf >> 8).ToString();

        //        sret = sRight + sLeft;
        //        sret = sret.Replace("\0", "");
        //    } catch (Exception ex) {
        //        CLOG.FileWrite_Str("Error CMelsecNet_Hex2Str " + ex, CLOG.eLogType.EXCEPTION);
        //        MessageBox.Show("Error CMelsecNet_Hex2Str " + ex);
        //    }

        //    return sret;
        //}

        //public int Str2Hex(string stmp) {
        //    int iRes = 0;
        //    byte bych = 0;

        //    if (stmp.Length > 2)
        //        return 0;

        //    try {
        //        foreach (byte ch in stmp) {
        //            //bych = Convert.ToByte(ch);

        //            iRes = (iRes << 8) | ch;
        //        }
        //    } catch (Exception ex) {
        //        CLOG.FileWrite_Str("Error CMelsecNet_Str2Hex " + ex, CLOG.eLogType.EXCEPTION);
        //        MessageBox.Show("Error CMelsecNet_Str2Hex " + ex);
        //    }

        //    return iRes;
        //}

        //public void Word_Copy(int nSrcAddr, int nDestAddr, int nCnt) {
        //    for (int nIdx = 0; nIdx < nCnt; ++nIdx) {
        //        Write_Word((nDestAddr + nIdx), (short)Read_Word(nSrcAddr + nIdx));	//short변수끼리 연산하니 int가 되어 버린다. 맘 편히 int로 가자
        //    }
        //}



        //Real Number처리
        //        #Region " [[[Processing of Write button for Real Number]]] "

        //    Private Sub btn_WriteRealNumber_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_WriteRealNumber.Click

        //        Dim iReturnCode As Integer                                          'Return code
        //        Dim byarrBufferByte() As Byte                                       'Array for using BitConverter class
        //        Dim sharrBufferForDeviceValue(ELEMENT_SIZE_REALNUMBER - 1) As Short 'Array for writing to the PLC

        //        'Error Handler
        //        On Error GoTo CatchError

        //        'Convert the TextBox data into the 'byarrBufferByte' as real number.(Array size:4 bytes)
        //        byarrBufferByte = BitConverter.GetBytes(CSng(txt_WriteRealNumber.Text))

        //        'Convert the 'byarrBufferByte' to the array for writing to the PLC.
        //        sharrBufferForDeviceValue(0) = BitConverter.ToInt16(byarrBufferByte, 0)
        //        sharrBufferForDeviceValue(1) = BitConverter.ToInt16(byarrBufferByte, 2)

        //        'The WriteDeviceBlock2 method is executed.(to D12-D13)
        //        iReturnCode = AxActEasyIF1.WriteDeviceBlock2("D12", _
        //                                                     ELEMENT_SIZE_REALNUMBER, _
        //                                                     sharrBufferForDeviceValue(0))

        //        'When ActEasyIF returns error code, display error message.
        //        If iReturnCode <> 0 Then
        //            DisplayErrorMessage(iReturnCode)
        //            Exit Sub
        //        End If

        //        Exit Sub

        //CatchError:  'Exception processing

        //        MsgBox(Err.Description(), MsgBoxStyle.Critical)
        //        End

        //    End Sub

        //#End Region



        //#Region " [[[Processing of Read button for Real Number]]] "

        //    Private Sub btn_ReadRealNumber_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_ReadRealNumber.Click

        //        Dim iReturnCode As Integer                                              'Return code
        //        Dim sharrBufferForDeviceValue(ELEMENT_SIZE_REALNUMBER - 1) As Short     'Array for using BitConverter class
        //        Dim byarrBufferByte(ELEMENT_SIZE_REALNUMBER * 2 - 1) As Byte            'Array for reading to the PLC
        //        Dim byarrTemp() As Byte                                                 'Temporary array for copying data
        //        Dim iNumber As Integer                                                  'Loop counter

        //        'Error Handler
        //        On Error GoTo CatchError

        //        'The ReadDeviceBlock2 method is executed.(from D12-D13)
        //        iReturnCode = AxActEasyIF1.ReadDeviceBlock2("D12", _
        //                                                    ELEMENT_SIZE_REALNUMBER, _
        //                                                    sharrBufferForDeviceValue(0))

        //        'When ActEasyIF returns error code, display error message.
        //        If iReturnCode <> 0 Then
        //            DisplayErrorMessage(iReturnCode)
        //            Exit Sub
        //        End If

        //        'Convert the 'sharrBufferForDeviceValue' to the array for using BitConverter class.
        //        For iNumber = 0 To ELEMENT_SIZE_REALNUMBER - 1
        //            byarrTemp = BitConverter.GetBytes(sharrBufferForDeviceValue(iNumber))
        //            byarrBufferByte(iNumber * 2) = byarrTemp(0)
        //            byarrBufferByte(iNumber * 2 + 1) = byarrTemp(1)
        //        Next iNumber

        //        'Convert the 'byarrBufferByte' to real number, and set the data to the TextBox as string.
        //        txt_ReadRealNumber.Text = CStr(BitConverter.ToSingle(byarrBufferByte, 0))

        //        Exit Sub

        //CatchError:  'Exception processing

        //        MsgBox(Err.Description(), MsgBoxStyle.Critical)
        //        End

        //    End Sub

        //#End Region




        // Bit 값들을 Word 에 저장
        //public void Word_Write_BitToDec(string _Addr, string _str)
        //{
        //    if (!isOpen) return;

        //    try
        //    {
        //        int _count = 16;
        //        int loopIndex = 0;
        //        string tmpBin = string.Empty;
        //        string tmpStr = string.Empty;

        //        loopIndex = _str.Length / _count;
        //        tmpStr = _str.PadRight(loopIndex * _count, '0');

        //        for (int inx = 0; inx < loopIndex; inx++)
        //        {
        //            tmpBin = "";
        //            tmpBin = tmpStr.Substring(inx * _count, _count);
        //            Word_Write(_Addr, mBit2Dec(mStrReverse(tmpBin)));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        evLog("Error CMxComponentMemory_" + "Word_Write_BitToDec " + ex.ToString(), CLog.eLogType.EXCEPTION);
        //    }
        //}
    }
}

