using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LGD_EDGE_Sample
{
    public static class cDBInfo
    {
        public struct InspSaveMain
        {
            public string dateTime;
            public string model;
            public string totalResult;
            public string result01;
            public string result02;
            public string result03;
            public string result04;
            public string result05;
            public string result06;

            public void Clear()
            {
                dateTime = string.Empty;
                model = string.Empty;
                totalResult = string.Empty;
                result01 = string.Empty;
                result02 = string.Empty;
                result03 = string.Empty;
                result04 = string.Empty;
                result05 = string.Empty;
                result06 = string.Empty;
            }
        }
        public struct InspSaveSub
        {
            public string dateTime;
            public string model;
            public int resNo;
            public string posJudge;
            public float posX;
            public float posY;
            public float posZ;
            public float posAngle;
            public float pixelMM;
            public string savePath;

            public void Clear()
            {
                dateTime = string.Empty;
                model = string.Empty;
                resNo = 0;
                posJudge = string.Empty;
                posX = 0.0f;
                posY = 0.0f;
                posZ = 0.0f;
                posAngle = 0.0f;
                pixelMM = 0.0f;
                savePath = string.Empty;
            }
        }
    }





}
