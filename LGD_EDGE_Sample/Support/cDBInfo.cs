
using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

namespace LGD_EDGE_Sample
{
    public static class cDBInfo
    {
        public struct InspSaveMain
        {
            public string dateTime;
            public string model;
            public string finalResult;
            public string result01;
            public string result02;
            public string result03;
            public string result04;
            public string result05;
            public string result06;
            public string result07;
            public string result08;
            public int edgeDist01;
            public int edgeDist02;
            public int edgeDist03;
            public int edgeDist04;
            public int edgeDist05;
            public int edgeDist06;
            public int edgeDist07;
            public int edgeDist08;
            public float edgeDist01_MM;
            public float edgeDist02_MM;
            public float edgeDist03_MM;
            public float edgeDist04_MM;
            public float edgeDist05_MM;
            public float edgeDist06_MM;
            public float edgeDist07_MM;
            public float edgeDist08_MM;
            public string savePath01;
            public string savePath02;
            public string savePath03;
            public string savePath04;
            public string savePath05;
            public string savePath06;
            public string savePath07;
            public string savePath08;

            public void Clear()
            {
                dateTime = string.Empty;
                model = string.Empty;
                finalResult = string.Empty;
                result01 = string.Empty;
                result02 = string.Empty;
                result03 = string.Empty;
                result04 = string.Empty;
                result05 = string.Empty;
                result06 = string.Empty;
                result07 = string.Empty;
                result08 = string.Empty;
                savePath01 = string.Empty;
                savePath02 = string.Empty;
                savePath03 = string.Empty;
                savePath04 = string.Empty;
                savePath05 = string.Empty;
                savePath06 = string.Empty;
                savePath07 = string.Empty;
                savePath08 = string.Empty;
                edgeDist01 = 0;
                edgeDist02 = 0;
                edgeDist03 = 0;
                edgeDist04 = 0;
                edgeDist05 = 0;
                edgeDist06 = 0;
                edgeDist07 = 0;
                edgeDist08 = 0;
                edgeDist01_MM = 0.0f;
                edgeDist02_MM = 0.0f;
                edgeDist03_MM = 0.0f;
                edgeDist04_MM = 0.0f;
                edgeDist05_MM = 0.0f;
                edgeDist06_MM = 0.0f;
                edgeDist07_MM = 0.0f;
                edgeDist08_MM = 0.0f;
            }
        }
        public struct InspSaveSub
        {
            public string dateTime;
            public string model;
            public int camNo;
            public int inspSide;
            public int edgePoint_X1;
            public int edgePoint_Y1;
            public int edgePoint_X2;
            public int edgePoint_Y2;
            public string result;

            public void Clear()
            {
                dateTime = string.Empty;
                model = string.Empty;
                camNo = 0;
                inspSide = 0;
                edgePoint_X1 = 0;
                edgePoint_Y1 = 0;
                edgePoint_X2 = 0;
                edgePoint_Y2 = 0;
                result = string.Empty;
            }
        }

        public struct InspSettingEdge
        {
            public string model;
            public int camNo;
            public edgeFinderSetting inner;
            public edgeFinderSetting outer;
            //public int innerThreshold;
            //public int outerThreshold;
            //public string innerEdgePolarity;
            //public string outerEdgePolarity;
            //public int inspAreaTopLeft_X;
            //public int inspAreaTopLeft_Y;
            //public int inspAreaBotRight_X;
            //public int inspAreaBotRight_Y;

            public void Clear()
            {
                model = string.Empty;
                camNo = 0;
                inner.Clear();
                outer.Clear();
            }
        }

        public struct edgeFinderSetting
        {
            public int threshold;
            public string edgePolarity;
            public int inspAreaTopLeft_X;
            public int inspAreaTopLeft_Y;
            public int inspAreaBotRight_X;
            public int inspAreaBotRight_Y;

            public void Clear()
            {
                threshold = 0;
                edgePolarity = string.Empty;
                inspAreaTopLeft_X = 0;
                inspAreaTopLeft_Y = 0;
                inspAreaBotRight_X = 0;
                inspAreaBotRight_Y = 0;
            }
        }

        // Summary:
        //     Get a list of model in the database
        //
        // Parameters:
        //   
        public static string fnGetModelList()
        {
            string strSQL = string.Empty;
            try
            {
                strSQL = "SELECT MODEL FROM TB_MODEL_SETTINGS GROUP BY MODEL";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
                strSQL = string.Empty;
            }
            return strSQL;
        }

        // Summary:
        //     Search data through model and camera setting number
        //
        // Parameters:
        //   _model     Inspection model
        //   _cameraNo  Camera number
        public static string fnGetModelSettingData(string _model, int _cameraNo)
        {
            string strSQL = string.Empty;
            try
            {
                strSQL = string.Format("SELECT * FROM   TB_MODEL_SETTINGS WHERE  MODEL = '{0}' AND CAMERA_NO = {1}\n", _model, _cameraNo);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
                strSQL = string.Empty;
            }
            return strSQL;
        }

        // Summary:
        //     Generate the SQL command for updating an existed Model Setting in the database
        //
        // Parameters:
        //   _target    one row of the database
        public static string fnUpdateModelSetting(InspSettingEdge _target)
        {
            string strSQL = string.Empty;
            try
            {
                strSQL += "UPDATE TB_MODEL_SETTINGS ";
                strSQL += string.Format("SET    MODEL = '{0}'", _target.model);
                strSQL += string.Format("      ,CAMERA_NO = {0}", _target.camNo);
                strSQL += string.Format("      ,INNER_THRESHOLD = {0}", _target.inner.threshold);
                strSQL += string.Format("      ,INNER_EDGE_POLARITY = '{0}'", _target.inner.edgePolarity);
                strSQL += string.Format("      ,INNER_ROI_TOPLEFT_X = {0}", _target.inner.inspAreaTopLeft_X);
                strSQL += string.Format("      ,INNER_ROI_TOPLEFT_Y = {0}", _target.inner.inspAreaTopLeft_Y);
                strSQL += string.Format("      ,INNER_ROI_BOTRIGHT_X = {0}", _target.inner.inspAreaBotRight_X);
                strSQL += string.Format("      ,INNER_ROI_BOTRIGHT_Y = {0}", _target.inner.inspAreaBotRight_Y);
                strSQL += string.Format("      ,OUTER_THRESHOLD = {0}", _target.outer.threshold);
                strSQL += string.Format("      ,OUTER_EDGE_POLARITY = '{0}'", _target.outer.edgePolarity);
                strSQL += string.Format("      ,OUTER_ROI_TOPLEFT_X = {0}", _target.outer.inspAreaTopLeft_X);
                strSQL += string.Format("      ,OUTER_ROI_TOPLEFT_Y = {0}", _target.outer.inspAreaTopLeft_Y);
                strSQL += string.Format("      ,OUTER_ROI_BOTRIGHT_X = {0}", _target.outer.inspAreaBotRight_X);
                strSQL += string.Format("      ,OUTER_ROI_BOTRIGHT_Y = {0}", _target.outer.inspAreaBotRight_Y);
                strSQL += string.Format("WHERE  MODEL = '{0}'", _target.model);
                strSQL += string.Format("AND  CAMERA_NO = {0}", _target.camNo);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
                strSQL = string.Empty;
            }
            return strSQL;
        }

        // Summary:
        //     Generate the SQL command for writing Model Settings database
        //
        // Parameters:
        //   _target    one row of the database
        public static string fnInsertModelSetting(InspSettingEdge _target)
        {
            string strSQL = string.Empty;
            try
            {
                strSQL += "INSERT TB_MODEL_SETTINGS";
                strSQL += "           ([MODEL],[CAMERA_NO],[INNER_THRESHOLD],[INNER_EDGE_POLARITY],";
                strSQL += "            [INNER_ROI_TOPLEFT_X],[INNER_ROI_TOPLEFT_Y],[INNER_ROI_BOTRIGHT_X],[INNER_ROI_BOTRIGHT_Y],";
                strSQL += "            [OUTER_THRESHOLD],[OUTER_EDGE_POLARITY],";
                strSQL += "            [OUTER_ROI_TOPLEFT_X],[OUTER_ROI_TOPLEFT_Y],[OUTER_ROI_BOTRIGHT_X],[OUTER_ROI_BOTRIGHT_Y])";
                strSQL += string.Format("VALUES('{0}'", _target.model);
                strSQL += string.Format("      ,{0}", _target.camNo);
                strSQL += string.Format("      ,{0}", _target.inner.threshold);
                strSQL += string.Format("      ,'{0}'", _target.inner.edgePolarity);
                strSQL += string.Format("      ,{0}", _target.inner.inspAreaTopLeft_X);
                strSQL += string.Format("      ,{0}", _target.inner.inspAreaTopLeft_Y);
                strSQL += string.Format("      ,{0}", _target.inner.inspAreaBotRight_X);
                strSQL += string.Format("      ,{0}", _target.inner.inspAreaBotRight_Y);
                strSQL += string.Format("      ,{0}", _target.outer.threshold);
                strSQL += string.Format("      ,'{0}'", _target.outer.edgePolarity);
                strSQL += string.Format("      ,{0}", _target.outer.inspAreaTopLeft_X);
                strSQL += string.Format("      ,{0}", _target.outer.inspAreaTopLeft_Y);
                strSQL += string.Format("      ,{0}", _target.outer.inspAreaBotRight_X);
                strSQL += string.Format("      ,{0})", _target.outer.inspAreaBotRight_Y);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
                strSQL = string.Empty;
            }
            return strSQL;
        }

        // Summary:
        //     Generate the SQL command for writing Main History database
        //
        // Parameters:
        //   _target    one row of the database
        public static string saveHistoryMain(InspSaveMain _target)
        {
            string strSQL = string.Empty;
            try
            {
                strSQL = string.Empty;
                strSQL += "INSERT TB_HISTORY_MAIN";
                strSQL += "           ([DATETIME],[MODEL],[FINAL_RESULT]";
                strSQL += "           ,[RESULT_01],[RESULT_02],[RESULT_03],[RESULT_04],[RESULT_05],[RESULT_06],[RESULT_07],[RESULT_08]";
                strSQL += "           ,[EDGE_DIST_01],[EDGE_DIST_02],[EDGE_DIST_03],[EDGE_DIST_04],[EDGE_DIST_05],[EDGE_DIST_06],[EDGE_DIST_07],[EDGE_DIST_08]";
                strSQL += "           ,[EDGE_DIST_MM_01],[EDGE_DIST_MM_02],[EDGE_DIST_MM_03],[EDGE_DIST_MM_04],[EDGE_DIST_MM_05],[EDGE_DIST_MM_06],[EDGE_DIST_MM_07],[EDGE_DIST_MM_08]";
                strSQL += "           ,[SAVE_PATH_01],[SAVE_PATH_02],[SAVE_PATH_03],[SAVE_PATH_04],[SAVE_PATH_05],[SAVE_PATH_06],[SAVE_PATH_07],[SAVE_PATH_08])";
                strSQL += string.Format("VALUES('{0}'", _target.dateTime);
                strSQL += string.Format("      ,'{0}'", _target.model);
                strSQL += string.Format("      ,'{0}'", _target.finalResult);
                strSQL += string.Format("      ,'{0}'", _target.result01);
                strSQL += string.Format("      ,'{0}'", _target.result02);
                strSQL += string.Format("      ,'{0}'", _target.result03);
                strSQL += string.Format("      ,'{0}'", _target.result04);
                strSQL += string.Format("      ,'{0}'", _target.result05);
                strSQL += string.Format("      ,'{0}'", _target.result06);
                strSQL += string.Format("      ,'{0}'", _target.result07);
                strSQL += string.Format("      ,'{0}'", _target.result08);
                strSQL += string.Format("      ,{0}", _target.edgeDist01);
                strSQL += string.Format("      ,{0}", _target.edgeDist02);
                strSQL += string.Format("      ,{0}", _target.edgeDist03);
                strSQL += string.Format("      ,{0}", _target.edgeDist04);
                strSQL += string.Format("      ,{0}", _target.edgeDist05);
                strSQL += string.Format("      ,{0}", _target.edgeDist06);
                strSQL += string.Format("      ,{0}", _target.edgeDist07);
                strSQL += string.Format("      ,{0}", _target.edgeDist08);
                strSQL += string.Format("      ,{0}", _target.edgeDist01_MM);
                strSQL += string.Format("      ,{0}", _target.edgeDist02_MM);
                strSQL += string.Format("      ,{0}", _target.edgeDist03_MM);
                strSQL += string.Format("      ,{0}", _target.edgeDist04_MM);
                strSQL += string.Format("      ,{0}", _target.edgeDist05_MM);
                strSQL += string.Format("      ,{0}", _target.edgeDist06_MM);
                strSQL += string.Format("      ,{0}", _target.edgeDist07_MM);
                strSQL += string.Format("      ,{0}", _target.edgeDist08_MM);
                strSQL += string.Format("      ,'{0}'", _target.savePath01);
                strSQL += string.Format("      ,'{0}'", _target.savePath02);
                strSQL += string.Format("      ,'{0}'", _target.savePath03);
                strSQL += string.Format("      ,'{0}'", _target.savePath04);
                strSQL += string.Format("      ,'{0}'", _target.savePath05);
                strSQL += string.Format("      ,'{0}'", _target.savePath06);
                strSQL += string.Format("      ,'{0}'", _target.savePath07);
                strSQL += string.Format("      ,'{0}'", _target.savePath08);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
                strSQL = string.Empty;
            }
            return strSQL;
        }

        // Summary:
        //     Generate the SQL command for writing Sub History database
        //
        // Parameters:
        //   _target    one row of the database
        public static string saveHistorySub(InspSaveSub _target)
        {
            string strSQL = string.Empty;
            try
            {
                strSQL = string.Empty;

                strSQL += "INSERT TB_HISTORY_SUB";
                strSQL += "           ([DATETIME],[MODEL],[CAMERA_NO]";
                strSQL += "           ,[EDGE_POINT_X1],[EDGE_POINT_Y1],[EDGE_POINT_X2],[EDGE_POINT_Y2],[RESULT])";
                strSQL += string.Format("VALUES('{0}'", _target.dateTime);
                strSQL += string.Format("      ,'{0}'", _target.model);
                strSQL += string.Format("      ,{0}", _target.camNo);
                strSQL += string.Format("      ,{0}", _target.edgePoint_X1);
                strSQL += string.Format("      ,{0}", _target.edgePoint_Y1);
                strSQL += string.Format("      ,{0}", _target.edgePoint_X2);
                strSQL += string.Format("      ,{0}", _target.edgePoint_Y2);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
                strSQL = string.Empty;
            }
            return strSQL;
        }
    }





}
