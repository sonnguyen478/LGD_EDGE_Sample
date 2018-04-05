using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interop.Common.Util
{
    public sealed class CChart
    {
        #region [ Chart 에서 Line 그리기 ]

        public static void ChartDisplayStripLines(System.Windows.Forms.DataVisualization.Charting.Chart targetChart, double _min, double _max, double _zero)
        {
            //하단 라인
            targetChart.ChartAreas[0].AxisY.StripLines.Add(new System.Windows.Forms.DataVisualization.Charting.StripLine());
            targetChart.ChartAreas[0].AxisY.StripLines[0].BackColor = System.Drawing.Color.Red;
            targetChart.ChartAreas[0].AxisY.StripLines[0].StripWidth = 0.001;
            targetChart.ChartAreas[0].AxisY.StripLines[0].Interval = 0;
            targetChart.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = _min;

            //상단 라인
            targetChart.ChartAreas[0].AxisY.StripLines.Add(new System.Windows.Forms.DataVisualization.Charting.StripLine());
            targetChart.ChartAreas[0].AxisY.StripLines[1].BackColor = System.Drawing.Color.Red;
            targetChart.ChartAreas[0].AxisY.StripLines[1].StripWidth = 0.001;
            targetChart.ChartAreas[0].AxisY.StripLines[1].Interval = 0;
            targetChart.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = _max;

            // 영점
            targetChart.ChartAreas[0].AxisY.StripLines.Add(new System.Windows.Forms.DataVisualization.Charting.StripLine());
            targetChart.ChartAreas[0].AxisY.StripLines[2].BackColor = System.Drawing.Color.Black;
            targetChart.ChartAreas[0].AxisY.StripLines[2].StripWidth = 0.001;
            targetChart.ChartAreas[0].AxisY.StripLines[2].Interval = 0;
            targetChart.ChartAreas[0].AxisY.StripLines[2].IntervalOffset = _zero;
        }

        public static void ChartDisplayStripLinesHorizontal(System.Windows.Forms.DataVisualization.Charting.Chart targetChart, int lineIndex, double _value, bool _dispFlag)
        {
            if (_dispFlag == true)
            {
                if (targetChart.ChartAreas[0].AxisY.StripLines.Count <= 0)
                {
                    targetChart.ChartAreas[0].AxisY.StripLines.Add(new System.Windows.Forms.DataVisualization.Charting.StripLine());
                }
                targetChart.ChartAreas[0].AxisY.StripLines[lineIndex].BackColor = System.Drawing.Color.Red;
                targetChart.ChartAreas[0].AxisY.StripLines[lineIndex].StripWidth = 0.01;
                targetChart.ChartAreas[0].AxisY.StripLines[lineIndex].Interval = 0;
                targetChart.ChartAreas[0].AxisY.StripLines[lineIndex].IntervalOffset = _value;
            }
            else
            {
                targetChart.ChartAreas[0].AxisY.StripLines.Remove(targetChart.ChartAreas[0].AxisY.StripLines[lineIndex]);
            }
        }

        public static void ChartDisplayStripLinesVertical(System.Windows.Forms.DataVisualization.Charting.Chart targetChart, int lineIndex, double _value, bool _dispFlag)
        {
            try
            {
                if (_dispFlag == true)
                {
                    if (targetChart.ChartAreas[0].AxisX.StripLines.Count <= 0)
                    {
                        targetChart.ChartAreas[0].AxisX.StripLines.Add(new System.Windows.Forms.DataVisualization.Charting.StripLine());
                    }
                    targetChart.ChartAreas[0].AxisX.StripLines[lineIndex].BackColor = System.Drawing.Color.Red;
                    targetChart.ChartAreas[0].AxisX.StripLines[lineIndex].StripWidth = 0.01;
                    targetChart.ChartAreas[0].AxisX.StripLines[lineIndex].Interval = 0;
                    targetChart.ChartAreas[0].AxisX.StripLines[lineIndex].IntervalOffset = _value;
                }
                else
                {
                    if (targetChart.ChartAreas[0].AxisX.StripLines.Count > 0)
                    {
                        targetChart.ChartAreas[0].AxisX.StripLines.Remove(targetChart.ChartAreas[0].AxisX.StripLines[lineIndex]);
                    }
                }
            }
            catch { }
        }

        public static void ChartDisplayStripLines(System.Windows.Forms.DataVisualization.Charting.Chart targetChart, double _min, double _max)
        {
            ChartDisplayStripLines(targetChart, _min, _max, 0);
        }

        public static void ChartDisplayStripLines(System.Windows.Forms.DataVisualization.Charting.Chart targetChart)
        {
            ChartDisplayStripLines(targetChart, 1000, 1000, 1000);
        }

        #endregion [ Chart 에서 Line 그리기 ]

        #region [ Chart 초기화 ]
        public static void ChartDisplayFormDefault(System.Windows.Forms.DataVisualization.Charting.Chart targetChart, string seriesName, System.Windows.Forms.DataVisualization.Charting.SeriesChartType _chartType)
        {
            try
            {

                targetChart.Series.Clear();
                targetChart.Series.Add(seriesName);
                targetChart.Series[seriesName].ChartType = _chartType;
                targetChart.Series[seriesName].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;

                targetChart.Series[seriesName].IsValueShownAsLabel = false;
                targetChart.Series[seriesName].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle; // Mark 모양
                targetChart.Series[seriesName].MarkerSize = 3;
                targetChart.Series[seriesName].MarkerColor = System.Drawing.Color.DodgerBlue; //Mark 색
                targetChart.Series[seriesName].Color = System.Drawing.Color.DodgerBlue; // Line 색

                //Chart1.ChartAreas["Default"].BorderDashStyle 
                targetChart.ChartAreas[0].BorderColor = System.Drawing.Color.White;
                targetChart.ChartAreas[0].BorderWidth = 1;
                targetChart.ChartAreas[0].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;

                targetChart.ChartAreas[0].Position.Auto = false;
                targetChart.ChartAreas[0].Position.X = 10;
                targetChart.ChartAreas[0].Position.Y = 10;
                targetChart.ChartAreas[0].Position.Width = 100;
                targetChart.ChartAreas[0].Position.Height = 88;

                targetChart.ChartAreas[0].InnerPlotPosition.Auto = false;
                targetChart.ChartAreas[0].InnerPlotPosition.X = 7;
                targetChart.ChartAreas[0].InnerPlotPosition.Y = 0;
                targetChart.ChartAreas[0].InnerPlotPosition.Width = 93;
                targetChart.ChartAreas[0].InnerPlotPosition.Height = 88;

                targetChart.ChartAreas[0].AxisX.IsMarginVisible = true;
                targetChart.ChartAreas[0].AxisX.MajorGrid.Interval = 1;
                targetChart.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet; // 좌표 표시하는 그리드 선 안봐이게 함.
                targetChart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                targetChart.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
                targetChart.ChartAreas[0].AxisX.LabelStyle.Enabled = false;


                //targetChart.ChartAreas[ 0 ].AxisY.IsLogarithmic = false;
                targetChart.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet; // 좌표 표시하는 그리드 선 안봐이게 함.
                targetChart.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
                targetChart.ChartAreas[0].AxisY.MinorGrid.Enabled = false;
                targetChart.ChartAreas[0].AxisY.LabelStyle.Enabled = true;
                //targetChart.ChartAreas[ 0 ].AxisY.LabelAutoFitStyle = System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.None;
                targetChart.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("현대하모니", 9, System.Drawing.FontStyle.Regular);
                 targetChart.ChartAreas[ 0 ].AxisY.Interval = 0.5;
                //targetChart.ChartAreas[ 0 ].AxisY.IntervalOffset = 0.5;
                //targetChart.ChartAreas[ 0 ].AxisY.IsLabelAutoFit = false;      
                targetChart.ChartAreas[0].AxisY.LabelStyle.Format = "{F1}"; // Y축 Lable 자리수를 맞추기 위해서 포멧을 지정함.
                //targetChart.ChartAreas[ 0 ].AxisY.LabelStyle.Format = "{0:0.00}"; 

                targetChart.ChartAreas[0].CursorX.IsUserEnabled = true; // 마우스 선택 한곳에 선을 Y 축으로 보이게함.
                targetChart.ChartAreas[0].CursorX.LineColor = System.Drawing.Color.Transparent;// 선택을 선을 안보이게

                targetChart.ChartAreas[0].AlignmentOrientation = System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Vertical;
                targetChart.ChartAreas[0].AlignmentStyle = System.Windows.Forms.DataVisualization.Charting.AreaAlignmentStyles.All;
            }
            catch (Exception ex)
            {
                cLog.FileWrite_Str( "Chart 초기화 실패 : " + ex.Message.ToString(), cLog.eLogType.EXCEPTION );
            }
        }
        public static void ChartDisplayFormDefault(System.Windows.Forms.DataVisualization.Charting.Chart targetChart, string seriesName, System.Windows.Forms.DataVisualization.Charting.SeriesChartType _chartType, int gridLineCount)
        {
            try
            {

                targetChart.Series.Clear();
                targetChart.Series.Add(seriesName);
                targetChart.Series[seriesName].ChartType = _chartType;
                targetChart.Series[seriesName].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;

                targetChart.Series[seriesName].IsValueShownAsLabel = false;
                targetChart.Series[seriesName].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle; // Mark 모양
                targetChart.Series[seriesName].MarkerSize = 3;
                targetChart.Series[seriesName].MarkerColor = System.Drawing.Color.DodgerBlue; //Mark 색
                targetChart.Series[seriesName].Color = System.Drawing.Color.DodgerBlue; // Line 색

                //Chart1.ChartAreas["Default"].BorderDashStyle 
                targetChart.ChartAreas[0].BorderColor = System.Drawing.Color.Black;
                targetChart.ChartAreas[0].BorderWidth = 1;
                targetChart.ChartAreas[0].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;

                targetChart.ChartAreas[0].Position.Auto = false;
                targetChart.ChartAreas[0].Position.X = 10;
                targetChart.ChartAreas[0].Position.Y = 10;
                targetChart.ChartAreas[0].Position.Width = 100;
                targetChart.ChartAreas[0].Position.Height = 88;

                targetChart.ChartAreas[0].InnerPlotPosition.Auto = false;
                targetChart.ChartAreas[0].InnerPlotPosition.X = 7;
                targetChart.ChartAreas[0].InnerPlotPosition.Y = 0;
                targetChart.ChartAreas[0].InnerPlotPosition.Width = 93;
                targetChart.ChartAreas[0].InnerPlotPosition.Height = 88;

                 targetChart.ChartAreas[0].AxisX.IsMarginVisible = true;
                targetChart.ChartAreas[0].AxisX.MajorGrid.Interval = gridLineCount;
                targetChart.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid; // 좌표 표시하는 그리드 선 안봐이게 함.
                targetChart.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
                targetChart.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
                targetChart.ChartAreas[0].AxisX.LabelStyle.Enabled = false;


                //targetChart.ChartAreas[ 0 ].AxisY.IsLogarithmic = false;
                targetChart.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet; // 좌표 표시하는 그리드 선 안봐이게 함.
                targetChart.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
                targetChart.ChartAreas[0].AxisY.MinorGrid.Enabled = false;
                targetChart.ChartAreas[0].AxisY.LabelStyle.Enabled = true;
                //targetChart.ChartAreas[ 0 ].AxisY.LabelAutoFitStyle = System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.None;
                targetChart.ChartAreas[0].AxisY.LabelStyle.Font = new System.Drawing.Font("현대하모니", 9, System.Drawing.FontStyle.Regular);
                targetChart.ChartAreas[0].AxisY.Interval = 0.5;
                //targetChart.ChartAreas[ 0 ].AxisY.IntervalOffset = 0;
                //targetChart.ChartAreas[ 0 ].AxisY.IsLabelAutoFit = true;
                targetChart.ChartAreas[0].AxisY.LabelStyle.Format = "{F1}"; // Y축 Lable 자리수를 맞추기 위해서 포멧을 지정함.
                //targetChart.ChartAreas[ 0 ].AxisY.LabelStyle.Format = "{0:0.00}";

                targetChart.ChartAreas[0].CursorX.IsUserEnabled = true; // 마우스 선택 한곳에 선을 Y 축으로 보이게함.
                targetChart.ChartAreas[0].CursorX.LineColor = System.Drawing.Color.Transparent;// 선택을 선을 안보이게

                targetChart.ChartAreas[0].AlignmentOrientation = System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Vertical;
                targetChart.ChartAreas[0].AlignmentStyle = System.Windows.Forms.DataVisualization.Charting.AreaAlignmentStyles.All;


                //targetChart.ChartAreas[0].AxisX.Minimum = Double.NaN;
                //targetChart.ChartAreas[0].AxisX.Maximum = Double.NaN;

            }
            catch (Exception ex)
            {
                cLog.FileWrite_Str( "Chart 초기화 실패 : " + ex.Message.ToString(), cLog.eLogType.EXCEPTION );
            }
        }
        #endregion [ Chart 초기화 ]

        #region [ Chart 값 표시 ]

        public static void ChartDisplayFormDataTableIndex(System.Data.DataTable target,
                                                          System.Windows.Forms.DataVisualization.Charting.Chart targetChart,
                                                          string seriesName, int index, ref string[] xValue)
        {
            double minValue = double.MaxValue;
            double maxValue = double.MinValue;

            try
            {
                if (target.Rows.Count <= 0) return;

                // Chart 초기화
                ChartDisplayFormDefault(targetChart, seriesName, System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line);

                xValue = new string[target.Rows.Count];

                for (int idx = 0; idx < target.Rows.Count; idx++)
                {
                    targetChart.Series[seriesName].Points.AddXY(idx, Convert.ToDouble(target.Rows[idx][index]));
                    xValue[idx] = target.Rows[idx][0].ToString();

                    if (Convert.ToDouble(target.Rows[idx][index]) < minValue) minValue = Convert.ToDouble(target.Rows[idx][index]);
                    if (Convert.ToDouble(target.Rows[idx][index]) > maxValue) maxValue = Convert.ToDouble(target.Rows[idx][index]);
                }

                if ((seriesName == "ST_LH") || (seriesName == "ST_RH"))
                {
                    targetChart.ChartAreas[0].AxisY.Maximum = 2;
                    targetChart.ChartAreas[0].AxisY.Minimum = -0.1;
                }
                else
                {
                    targetChart.ChartAreas[0].AxisY.Maximum = (System.Math.Round(maxValue) < 2) ? 2 : System.Math.Round(maxValue) + 1;
                    targetChart.ChartAreas[0].AxisY.Minimum = (System.Math.Round(minValue) > -2) ? -2 : System.Math.Round(minValue) - 1;
                }

                targetChart.Invalidate();
            }
            catch (Exception ex)
            {
                cLog.FileWrite_Str( "Chart 정보 Display 실패 : " + ex.Message.ToString(), cLog.eLogType.EXCEPTION );
            }
        }

        public static void ChartDisplayFormDataTableIndex2(System.Data.DataTable target,
                                                          System.Windows.Forms.DataVisualization.Charting.Chart targetChart,
                                                          string seriesName, double minValue, double maxValue, int index, ref string[] xValue, bool minmaxFlag, int gridLineCnt)
        {
            double minSearchValue = double.MaxValue;
            double maxSearchValue = double.MinValue;

            try
            {
                if (target.Rows.Count <= 0) return;

                // Chart 초기화
                ChartDisplayFormDefault(targetChart, seriesName, System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line, gridLineCnt);

                xValue = new string[target.Rows.Count];
 
                for (int idx = 0; idx < target.Rows.Count; idx++)
                {
                    targetChart.Series[seriesName].Points.AddXY(idx, Convert.ToDouble(target.Rows[idx][index]));
                    xValue[idx] = target.Rows[idx][0].ToString();

                    if (Convert.ToDouble(target.Rows[idx][index]) < minSearchValue) minSearchValue = Convert.ToDouble(target.Rows[idx][index]);
                    if (Convert.ToDouble(target.Rows[idx][index]) > maxSearchValue) maxSearchValue = Convert.ToDouble(target.Rows[idx][index]);
                }

                if (minmaxFlag == true)
                {
                    targetChart.ChartAreas[0].AxisY.Maximum = maxValue;
                    targetChart.ChartAreas[0].AxisY.Minimum = minValue;
                    targetChart.ChartAreas[0].AxisY.Interval = 0.5;
                    targetChart.ChartAreas[0].AxisY.Interval = Math.Round((targetChart.ChartAreas[0].AxisY.Maximum - targetChart.ChartAreas[0].AxisY.Minimum) / 4d, 3);
                }
                else
                {
                    targetChart.ChartAreas[0].AxisY.Maximum = maxSearchValue + 0.2;
                    targetChart.ChartAreas[0].AxisY.Minimum = minSearchValue - 0.2;
                    targetChart.ChartAreas[0].AxisY.Interval = Math.Round( (targetChart.ChartAreas[0].AxisY.Maximum - targetChart.ChartAreas[0].AxisY.Minimum) / 4d , 3);
                    //targetChart.ChartAreas[0].AxisY.Interval = 0;

                }


                //targetChart.ChartAreas[0].AxisY.Maximum = 1; // 챠트 표시 Max
                //targetChart.ChartAreas[0].AxisY.Minimum = -1;
                //targetChart.ChartAreas[0].AxisY.Interval = 0.5; // 표시 할 값 간격


              //  targetChart.ChartAreas[0].AxisX.Maximum = target.Rows.Count + target.Rows.Count * 0.01;



                targetChart.Invalidate();
            }
            catch (Exception ex)
            {
                cLog.FileWrite_Str( "Chart 정보 Display 실패 : " + ex.Message.ToString(), cLog.eLogType.EXCEPTION );
            }
        }
        
        public static void ChartDisplayFormDataTable(System.Data.DataTable target,
                                                                       System.Windows.Forms.DataVisualization.Charting.Chart targetChart,
                                                                       string seriesName, ref string[] xValue)
        {
            double minValue = double.MaxValue;
            double maxValue = double.MinValue;

            try
            {
                if (target.Rows.Count <= 0) return;

                // Chart 초기화
                ChartDisplayFormDefault(targetChart, seriesName, System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line);

                xValue = new string[target.Rows.Count];
                for (int idx = 0; idx < target.Rows.Count; idx++)
                {
                    targetChart.Series[seriesName].Points.AddXY(idx, Convert.ToDouble(target.Rows[idx][1]));
                    xValue[idx] = target.Rows[idx][0].ToString();

                    if (Convert.ToDouble(target.Rows[idx][1]) < minValue) minValue = Convert.ToDouble(target.Rows[idx][1]);
                    if (Convert.ToDouble(target.Rows[idx][1]) > maxValue) maxValue = Convert.ToDouble(target.Rows[idx][1]);
                }

                targetChart.ChartAreas[0].AxisY.IsLogarithmic = false;
                targetChart.ChartAreas[0].AxisY.Maximum = ((minValue + maxValue) / 2) + 0.5;
                targetChart.ChartAreas[0].AxisY.Minimum = ((minValue + maxValue) / 2) - 0.5;
                targetChart.Series[seriesName].MarkerSize = 10;

                targetChart.ChartAreas[0].AxisY.StripLines.Add(new System.Windows.Forms.DataVisualization.Charting.StripLine());
                targetChart.ChartAreas[0].AxisY.StripLines.Add(new System.Windows.Forms.DataVisualization.Charting.StripLine());
                //상단 라인
                targetChart.ChartAreas[0].AxisY.StripLines[0].BackColor = System.Drawing.Color.Red;
                targetChart.ChartAreas[0].AxisY.StripLines[0].StripWidth = 0.001;
                targetChart.ChartAreas[0].AxisY.StripLines[0].Interval = 0;
                targetChart.ChartAreas[0].AxisY.StripLines[0].IntervalOffset = maxValue;
                //하단 라인
                targetChart.ChartAreas[0].AxisY.StripLines[1].BackColor = System.Drawing.Color.Red;
                targetChart.ChartAreas[0].AxisY.StripLines[1].StripWidth = 0.001;
                targetChart.ChartAreas[0].AxisY.StripLines[1].Interval = 0;
                targetChart.ChartAreas[0].AxisY.StripLines[1].IntervalOffset = minValue;

                targetChart.Invalidate();
            }
            catch (Exception ex)
            {
                cLog.FileWrite_Str( "Chart 정보 Display 실패 : " + ex.Message.ToString(), cLog.eLogType.EXCEPTION );
                cLog.FileWrite_Str( "Chart 정보 Display 실패 : " + ex.Message.ToString(), cLog.eLogType.LOG );
            }
        }

        #endregion [ Chart 값 표시 ]

        public static void ChartDisplayFormDataGridView(System.Windows.Forms.DataGridView target,
                                                                       System.Windows.Forms.DataVisualization.Charting.Chart targetChart,
                                                                       string seriesName, double stMax, double stMin, bool reverseFlag, bool minmaxFlag)
        {
            double minValue = double.MaxValue;
            double maxValue = double.MinValue;

            System.Windows.Forms.DataVisualization.Charting.Axis AxisX;

            try
            {
                //target[ 0, 1 ].ToString();
                if (target.Rows.Count <= 0) return;

                targetChart.Series.Clear();
                targetChart.Series.Add(seriesName);
                targetChart.Series[seriesName].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

                //xValue = new string[ target.Columns.Count ];
                for (int idx = 0; idx < target.Columns.Count; idx++)
                {
                    targetChart.Series[seriesName].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
                    targetChart.Series[seriesName].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
                    targetChart.Series[seriesName].Points.AddXY(Convert.ToDouble(target[idx, 1].Value), idx);
                    //xValue[ idx ] = target[ idx, 0 ].Value.ToString();
                    if (Convert.ToDouble(target[idx, 1].Value) < minValue) minValue = Convert.ToDouble(target[idx, 1].Value);
                    if (Convert.ToDouble(target[idx, 1].Value) > maxValue) maxValue = Convert.ToDouble(target[idx, 1].Value);
                }
                AxisX = targetChart.ChartAreas[0].AxisX;
                AxisX.IsReversed = reverseFlag;
                targetChart.Series[seriesName].IsValueShownAsLabel = false;
                targetChart.Series[seriesName].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
                targetChart.Series[seriesName].MarkerSize = 10;

                targetChart.ChartAreas[0].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
                targetChart.ChartAreas[0].BorderWidth = 1;

                targetChart.ChartAreas[0].Position.Auto = false;
                targetChart.ChartAreas[0].Position.X = 10;
                targetChart.ChartAreas[0].Position.Y = 10;
                targetChart.ChartAreas[0].Position.Width = 100;
                targetChart.ChartAreas[0].Position.Height = 100;

                targetChart.ChartAreas[0].InnerPlotPosition.Auto = false;
                targetChart.ChartAreas[0].InnerPlotPosition.X = 1;
                targetChart.ChartAreas[0].InnerPlotPosition.Y = 1;
                targetChart.ChartAreas[0].InnerPlotPosition.Width = 100;
                targetChart.ChartAreas[0].InnerPlotPosition.Height = 100;

                targetChart.ChartAreas[0].AxisX.IsMarginVisible = true;
                targetChart.ChartAreas[0].AxisX.MajorGrid.Interval = 1;
                targetChart.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet; // 좌표 표시하는 그리드 선 안봐이게 함.
                targetChart.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
                targetChart.ChartAreas[0].AxisX.LabelStyle.Enabled = true;
                targetChart.ChartAreas[0].AxisY.LabelStyle.Enabled = false;
                targetChart.ChartAreas[0].AxisX.LabelStyle.Font = new System.Drawing.Font("현대하모니 M", 8F);
                targetChart.ChartAreas[0].AxisX.LabelStyle.Angle = 90;


                targetChart.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
                targetChart.ChartAreas[0].AxisY.IsLogarithmic = false;

                if (minmaxFlag == true)
                {
                    targetChart.ChartAreas[0].AxisX.Maximum = stMax;
                    targetChart.ChartAreas[0].AxisX.Minimum = stMin;
                    targetChart.ChartAreas[0].AxisY.Interval = 0.5;
                }
                else
                {
                    targetChart.ChartAreas[0].AxisX.Maximum = maxValue + 0.1;
                    targetChart.ChartAreas[0].AxisX.Minimum = minValue - 0.1;
                    targetChart.ChartAreas[0].AxisY.Interval = 0.5;
                }

                targetChart.ChartAreas[0].AxisY.Maximum = 10.5;
                targetChart.ChartAreas[0].AxisY.Minimum = -0.5;

                targetChart.ChartAreas[0].AxisX.StripLines.Add(new System.Windows.Forms.DataVisualization.Charting.StripLine());
                targetChart.ChartAreas[0].AxisX.StripLines.Add(new System.Windows.Forms.DataVisualization.Charting.StripLine());
                targetChart.ChartAreas[0].AxisX.StripLines.Add(new System.Windows.Forms.DataVisualization.Charting.StripLine());
                //상단 라인
                targetChart.ChartAreas[0].AxisX.StripLines[0].BackColor = System.Drawing.Color.Red;
                targetChart.ChartAreas[0].AxisX.StripLines[0].StripWidth = 0.001;
                targetChart.ChartAreas[0].AxisX.StripLines[0].Interval = 0;
                targetChart.ChartAreas[0].AxisX.StripLines[0].IntervalOffset = stMax;
                //하단 라인
                targetChart.ChartAreas[0].AxisX.StripLines[1].BackColor = System.Drawing.Color.Red;
                targetChart.ChartAreas[0].AxisX.StripLines[1].StripWidth = 0.001;
                targetChart.ChartAreas[0].AxisX.StripLines[1].Interval = 0;
                targetChart.ChartAreas[0].AxisX.StripLines[1].IntervalOffset = stMin;
                // 영점
                targetChart.ChartAreas[0].AxisX.StripLines[2].BackColor = System.Drawing.Color.Black;
                targetChart.ChartAreas[0].AxisX.StripLines[2].StripWidth = 0.001;
                targetChart.ChartAreas[0].AxisX.StripLines[2].Interval = 0;
                targetChart.ChartAreas[0].AxisX.StripLines[2].IntervalOffset = 0;

                targetChart.ChartAreas[0].CursorX.IsUserEnabled = true;
                targetChart.ChartAreas[0].CursorX.LineColor = System.Drawing.Color.Transparent;
                targetChart.ChartAreas[0].AlignmentOrientation = System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Vertical;
                targetChart.ChartAreas[0].AlignmentStyle = System.Windows.Forms.DataVisualization.Charting.AreaAlignmentStyles.All;

                targetChart.ChartAreas[0].AxisX.LabelStyle.Format = "F1";
                targetChart.ChartAreas[0].AxisY.LabelStyle.Format = "F1";


                targetChart.Invalidate();
            }
            catch (Exception ex)
            {
                cLog.FileWrite_Str( "Chart 정보 Display 실패 : " + ex.Message.ToString(), cLog.eLogType.EXCEPTION );
                cLog.FileWrite_Str( "Chart 정보 Display 실패 : " + ex.Message.ToString(), cLog.eLogType.LOG );
            }
        }
        #region [ Chart 정보 Display ]
        public static void ChartDisplayFormDataGridView(System.Windows.Forms.DataGridView target,
                                                                       System.Windows.Forms.DataVisualization.Charting.Chart targetChart,
                                                                       string seriesName, ref string[] xValue, double stMax, double stMin, bool reverseFlag)
        {
            double minValue = double.MaxValue;
            double maxValue = double.MinValue;

            System.Windows.Forms.DataVisualization.Charting.Axis AxisX;

            try
            {
                //target[ 0, 1 ].ToString();
                if ( target.Rows.Count <= 0 ) return;

                targetChart.Series.Clear();
                targetChart.Series.Add( seriesName );
                targetChart.Series[ seriesName ].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

                xValue = new string[ target.Columns.Count ];
                for ( int idx = 0 ; idx < target.Columns.Count ; idx++ )
                {
                    targetChart.Series[ seriesName ].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
                    targetChart.Series[ seriesName ].YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
                    targetChart.Series[ seriesName ].Points.AddXY( Convert.ToDouble( target[ idx, 1 ].Value ), idx );
                    xValue[ idx ] = target[ idx, 0 ].Value.ToString();


                    if ( Convert.ToDouble( target[ idx, 1 ].Value ) < minValue ) minValue = Convert.ToDouble( target[ idx, 1 ].Value );
                    if ( Convert.ToDouble( target[ idx, 1 ].Value ) > maxValue ) maxValue = Convert.ToDouble( target[ idx, 1 ].Value );
                }
                AxisX = targetChart.ChartAreas[ 0 ].AxisX;
                AxisX.IsReversed = reverseFlag;
                targetChart.Series[ seriesName ].IsValueShownAsLabel = false;
                targetChart.Series[ seriesName ].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
                targetChart.Series[ seriesName ].MarkerSize = 10;

                targetChart.ChartAreas[ 0 ].InnerPlotPosition.Auto = true;
                targetChart.ChartAreas[ 0 ].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
                targetChart.ChartAreas[ 0 ].BorderWidth = 1;

                targetChart.ChartAreas[ 0 ].AxisX.IsMarginVisible = true;
                targetChart.ChartAreas[ 0 ].AxisX.MajorGrid.Interval = 1;
                targetChart.ChartAreas[ 0 ].AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet; // 좌표 표시하는 그리드 선 안봐이게 함.
                targetChart.ChartAreas[ 0 ].AxisX.MinorGrid.Enabled = false;
                targetChart.ChartAreas[ 0 ].AxisX.LabelStyle.Enabled = true;
                targetChart.ChartAreas[ 0 ].AxisY.LabelStyle.Enabled = false;
                targetChart.ChartAreas[ 0 ].AxisX.LabelStyle.Font = new System.Drawing.Font( "현대하모니 M", 8F );
                targetChart.ChartAreas[ 0 ].AxisX.LabelStyle.Angle = 90;


                targetChart.ChartAreas[ 0 ].AxisY.MajorGrid.Enabled = false;
                targetChart.ChartAreas[ 0 ].AxisY.IsLogarithmic = false;
                targetChart.ChartAreas[ 0 ].AxisX.Maximum = maxValue + 0.2;
                targetChart.ChartAreas[ 0 ].AxisX.Minimum = minValue - 0.2;

                targetChart.ChartAreas[ 0 ].AxisX.StripLines.Add( new System.Windows.Forms.DataVisualization.Charting.StripLine() );
                targetChart.ChartAreas[ 0 ].AxisX.StripLines.Add( new System.Windows.Forms.DataVisualization.Charting.StripLine() );
                //상단 라인
                targetChart.ChartAreas[ 0 ].AxisX.StripLines[ 0 ].BackColor = System.Drawing.Color.Red;
                targetChart.ChartAreas[ 0 ].AxisX.StripLines[ 0 ].StripWidth = 0.001;
                targetChart.ChartAreas[ 0 ].AxisX.StripLines[ 0 ].Interval = 0;
                targetChart.ChartAreas[ 0 ].AxisX.StripLines[ 0 ].IntervalOffset = stMax;
                //하단 라인
                targetChart.ChartAreas[ 0 ].AxisX.StripLines[ 1 ].BackColor = System.Drawing.Color.Red;
                targetChart.ChartAreas[ 0 ].AxisX.StripLines[ 1 ].StripWidth = 0.001;
                targetChart.ChartAreas[ 0 ].AxisX.StripLines[ 1 ].Interval = 0;
                targetChart.ChartAreas[ 0 ].AxisX.StripLines[ 1 ].IntervalOffset = stMin;

                targetChart.ChartAreas[ 0 ].CursorX.IsUserEnabled = true;
                targetChart.ChartAreas[ 0 ].CursorX.LineColor = System.Drawing.Color.Transparent;
                targetChart.ChartAreas[ 0 ].AlignmentOrientation = System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Vertical;
                targetChart.ChartAreas[ 0 ].AlignmentStyle = System.Windows.Forms.DataVisualization.Charting.AreaAlignmentStyles.All;

                targetChart.ChartAreas[ 0 ].AxisX.LabelStyle.Format = "F1";
                targetChart.ChartAreas[ 0 ].AxisY.LabelStyle.Format = "F1"; // Y축 Lable 자리수를 맞추기 위해서 포멧을 지정함.


                targetChart.Invalidate();
            }
            catch ( Exception ex )
            {
                cLog.FileWrite_Str( "Chart 정보 Display 실패 : " + ex.Message.ToString(), cLog.eLogType.EXCEPTION );
                cLog.FileWrite_Str( "Chart 정보 Display 실패 : " + ex.Message.ToString(), cLog.eLogType.LOG );
            }
        }
        public static void ChartDisplayTowDataFromDataTable(System.Data.DataTable target,
                                                                                  System.Windows.Forms.DataVisualization.Charting.Chart targetChart,
                                                                                  string[] seriesName)
        {
            try
            {
                if (target.Rows.Count <= 0) return;

                targetChart.Series.Clear();
                targetChart.Series.Add(seriesName[0]);
                targetChart.Series.Add(seriesName[1]);
                targetChart.Series[seriesName[0]].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                targetChart.Series[seriesName[1]].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;

                for (int idx = 0; idx < target.Rows.Count; idx++)
                {
                    targetChart.Series[seriesName[0]].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                    targetChart.Series[seriesName[0]].Points.AddXY(idx, Convert.ToDouble(target.Rows[idx][0]));

                    targetChart.Series[seriesName[1]].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                    targetChart.Series[seriesName[1]].Points.AddXY(idx, Convert.ToDouble(target.Rows[idx][1]));
                }

                targetChart.Series[seriesName[0]].IsValueShownAsLabel = false;
                targetChart.Series[seriesName[0]].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.None;
                targetChart.Series[seriesName[1]].IsValueShownAsLabel = false;
                targetChart.Series[seriesName[1]].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.None;

                targetChart.ChartAreas[0].InnerPlotPosition.Auto = true;
                targetChart.ChartAreas[0].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
                targetChart.ChartAreas[0].BorderWidth = 1;

                targetChart.ChartAreas[0].AxisX.IsMarginVisible = true;
                targetChart.ChartAreas[0].AxisX.MajorGrid.Interval = 1;
                targetChart.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
                targetChart.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
                targetChart.ChartAreas[0].AxisX.LabelStyle.Enabled = false;

                targetChart.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
                targetChart.ChartAreas[0].AxisY.IsLogarithmic = false;

                targetChart.Invalidate();
            }
            catch (Exception ex)
            {
                cLog.FileWrite_Str( "Chart 정보 Display 실패 : " + ex.Message.ToString(), cLog.eLogType.EXCEPTION );
                cLog.FileWrite_Str( "Chart 정보 Display 실패 : " + ex.Message.ToString(), cLog.eLogType.LOG );
            }
        }
        public static void ChartDisplayFormDataTable(System.Data.DataTable target,
                                                                       System.Windows.Forms.DataVisualization.Charting.Chart targetChart,
                                                                       string seriesName, ref string[] xValue, double stMax, double stMin)//, out double valueMax, out double valueMin)
        {
            double minValue = double.MaxValue;
            double maxValue = double.MinValue;

            try
            {
                if ( target.Rows.Count <= 0 )
                {
                    //valueMax = double.MinValue;
                    //valueMin = double.MaxValue;
                    return;
                }
                targetChart.Series.Clear();
                targetChart.Series.Add( seriesName );
                targetChart.Series[ seriesName ].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

                xValue = new string[ target.Rows.Count ];
                for ( int idx = 0 ; idx < target.Rows.Count ; idx++ )
                {
                    targetChart.Series[ seriesName ].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                    targetChart.Series[ seriesName ].Points.AddXY( idx, Convert.ToDouble( target.Rows[ idx ][ 1 ] ) );
                    xValue[ idx ] = target.Rows[ idx ][ 0 ].ToString();

                    if ( Convert.ToDouble( target.Rows[ idx ][ 1 ] ) < minValue ) minValue = Convert.ToDouble( target.Rows[ idx ][ 1 ] );
                    if ( Convert.ToDouble( target.Rows[ idx ][ 1 ] ) > maxValue ) maxValue = Convert.ToDouble( target.Rows[ idx ][ 1 ] );
                }

                targetChart.Series[ seriesName ].IsValueShownAsLabel = false;
                targetChart.Series[ seriesName ].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
                targetChart.Series[ seriesName ].MarkerSize = 10;

                targetChart.ChartAreas[ 0 ].InnerPlotPosition.Auto = true;
                targetChart.ChartAreas[ 0 ].BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
                targetChart.ChartAreas[ 0 ].BorderWidth = 1;

                targetChart.ChartAreas[ 0 ].AxisX.IsMarginVisible = true;
                targetChart.ChartAreas[ 0 ].AxisX.MajorGrid.Interval = 1;
                targetChart.ChartAreas[ 0 ].AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet; // 좌표 표시하는 그리드 선 안봐이게 함.
                targetChart.ChartAreas[ 0 ].AxisX.MinorGrid.Enabled = false;
                targetChart.ChartAreas[ 0 ].AxisX.LabelStyle.Enabled = false;

                targetChart.ChartAreas[ 0 ].AxisY.MajorGrid.Enabled = false;
                targetChart.ChartAreas[ 0 ].AxisY.IsLogarithmic = false;
                targetChart.ChartAreas[ 0 ].AxisY.Maximum = maxValue + 0.2; // ( ( minValue + maxValue ) / 2 ) + 0.5;
                targetChart.ChartAreas[ 0 ].AxisY.Minimum = minValue - 0.2; //( ( minValue + maxValue ) / 2 ) - 0.5;

                targetChart.ChartAreas[ 0 ].AxisY.StripLines.Add( new System.Windows.Forms.DataVisualization.Charting.StripLine() );
                targetChart.ChartAreas[ 0 ].AxisY.StripLines.Add( new System.Windows.Forms.DataVisualization.Charting.StripLine() );
                //상단 라인
                targetChart.ChartAreas[ 0 ].AxisY.StripLines[ 0 ].BackColor = System.Drawing.Color.Red;
                targetChart.ChartAreas[ 0 ].AxisY.StripLines[ 0 ].StripWidth = 0.001;
                targetChart.ChartAreas[ 0 ].AxisY.StripLines[ 0 ].Interval = 0;
                targetChart.ChartAreas[ 0 ].AxisY.StripLines[ 0 ].IntervalOffset = stMax;
                //하단 라인
                targetChart.ChartAreas[ 0 ].AxisY.StripLines[ 1 ].BackColor = System.Drawing.Color.Red;
                targetChart.ChartAreas[ 0 ].AxisY.StripLines[ 1 ].StripWidth = 0.001;
                targetChart.ChartAreas[ 0 ].AxisY.StripLines[ 1 ].Interval = 0;
                targetChart.ChartAreas[ 0 ].AxisY.StripLines[ 1 ].IntervalOffset = stMin;

                targetChart.ChartAreas[ 0 ].CursorX.IsUserEnabled = true;
                targetChart.ChartAreas[ 0 ].CursorX.LineColor = System.Drawing.Color.Transparent;
                targetChart.ChartAreas[ 0 ].AlignmentOrientation = System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Vertical;
                targetChart.ChartAreas[ 0 ].AlignmentStyle = System.Windows.Forms.DataVisualization.Charting.AreaAlignmentStyles.All;


                targetChart.ChartAreas[ 0 ].AxisX.LabelStyle.Format = "F1";
                targetChart.ChartAreas[ 0 ].AxisY.LabelStyle.Format = "F1"; // Y축 Lable 자리수를 맞추기 위해서 포멧을 지정함.

                targetChart.Invalidate();
            }
            catch ( Exception ex )
            {
                cLog.FileWrite_Str( "Chart 정보 Display 실패 : " + ex.Message.ToString(), cLog.eLogType.EXCEPTION );
                cLog.FileWrite_Str( "Chart 정보 Display 실패 : " + ex.Message.ToString(), cLog.eLogType.LOG );
            }
        }
        #endregion [ Chart 정보 Display ]

        #region [ Chart Location 확인 ]
        public static int GetChartPositionLoc(System.Windows.Forms.DataVisualization.Charting.Chart target, int pos)
        {
            int retValue = -1;

            try
            {
                retValue = Convert.ToInt32(target.Series[0].Points[pos].XValue);
            }
            catch (Exception ex)
            {
                cLog.FileWrite_Str( "Chart Location 확인 실패 : " + ex.Message.ToString(), cLog.eLogType.EXCEPTION );
                // CLog.FileWrite_Str( "Chart Location 확인 실패 : " + ex.Message.ToString(), CLog.eLogType.LOG );
                retValue = -1;
            }
            return retValue;
        }
        public static int GetChartPositionLocVertical(System.Windows.Forms.DataVisualization.Charting.Chart target, int pos)
        {
            int retValue = -1;

            try
            {
                retValue = Convert.ToInt32( target.Series[ 0 ].Points[ pos ].YValues[ 0 ] );
            }
            catch ( Exception ex )
            {
                cLog.FileWrite_Str( "Chart Location 확인 실패 : " + ex.Message.ToString(), cLog.eLogType.EXCEPTION );
                cLog.FileWrite_Str( "Chart Location 확인 실패 : " + ex.Message.ToString(), cLog.eLogType.LOG );
                retValue = -1;
            }
            return retValue;
        }
        #endregion [ Chart Location 확인 ]

        #region [ 마우스 클릭 시 좌표로 Chart Point Index ]
        public static int GetChartPosition(System.Windows.Forms.DataVisualization.Charting.Chart target, System.Windows.Forms.MouseEventArgs e)
        {
            int retValue = -1;
            try
            {
                int count = target.Series[0].Points.Count;
                double cX = target.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X);
                double cY = target.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y);
                double maxY = target.ChartAreas[0].AxisY.Maximum;
                double minY = target.ChartAreas[0].AxisY.Minimum;
                System.Windows.Forms.DataVisualization.Charting.HitTestResult re = target.HitTest(e.X, e.Y);

                //System.Data.DataTable getData = new System.Data.DataTable();

                if ((re.PointIndex == -1) || (cX < 0) || (cX > count + 1) || (cY < minY) || (cY > maxY))
                {
                    retValue = -1;
                }
                else
                {
                    retValue = re.PointIndex;
                }
            }
            catch (Exception ex)
            {
                cLog.FileWrite_Str( "Chart 포인트 확인 실패 : " + ex.Message.ToString(), cLog.eLogType.EXCEPTION );
                cLog.FileWrite_Str( "Chart 포인트 확인 실패 : " + ex.Message.ToString(), cLog.eLogType.LOG );
                retValue = -1;
            }
            return retValue;
        }
        public static int GetChartPosition(System.Windows.Forms.DataVisualization.Charting.Chart target, System.Windows.Forms.MouseEventArgs e, Interop.Common.Util.CGlobal.eChart _isVertical)
        {
            int retValue = -1;
            try
            {
                int count = target.Series[ 0 ].Points.Count;
                double cX = 0;
                double cY = 0;
                double maxLocVal = 0;
                double minLocVal = 0;

                // 수직 챠트
                if ( _isVertical == Interop.Common.Util.CGlobal.eChart.VERTICAL )
                {
                    cY = target.ChartAreas[ 0 ].AxisX.PixelPositionToValue( e.Location.X );
                    cX = target.ChartAreas[ 0 ].AxisY.PixelPositionToValue( e.Location.Y );
                    maxLocVal = target.ChartAreas[ 0 ].AxisX.Maximum;
                    minLocVal = target.ChartAreas[ 0 ].AxisX.Minimum;
                }
                else
                { // 수평
                    cX = target.ChartAreas[ 0 ].AxisX.PixelPositionToValue( e.Location.X ); // X 값
                    cY = target.ChartAreas[ 0 ].AxisY.PixelPositionToValue( e.Location.Y ); // Y 값
                    maxLocVal = target.ChartAreas[ 0 ].AxisY.Maximum;
                    minLocVal = target.ChartAreas[ 0 ].AxisY.Minimum;
                }

                System.Windows.Forms.DataVisualization.Charting.HitTestResult re = target.HitTest( e.X, e.Y );
                if ( ( re.PointIndex == -1 ) || ( cX < 0 ) || ( cX > count + 1 ) || ( cY < minLocVal ) || ( cY > maxLocVal ) )
                {
                    retValue = -1;
                }
                else
                {
                    retValue = re.PointIndex;
                }
            }
            catch ( Exception ex )
            {
                cLog.FileWrite_Str( "Chart 포인트 확인 실패 : " + ex.Message.ToString(), cLog.eLogType.EXCEPTION );
                cLog.FileWrite_Str( "Chart 포인트 확인 실패 : " + ex.Message.ToString(), cLog.eLogType.LOG );
                retValue = -1;
            }
            return retValue;
        }
        #endregion  [ 마우스 클릭 시 좌표로 Chart Point Index ]

        #region [ 선택한 Chart Point Index 의 Mark 강조 ]
        public static void DisplayChartSelectIndex(System.Windows.Forms.DataVisualization.Charting.Chart _tragetChart, int _OldPosition, int _NewPosition)
        {
            try
            {
                if (_NewPosition > -1)
                {
                    _tragetChart.Series[0].MarkerImage = "";
                    _tragetChart.Series[0].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;// (MarkerStyle)MarkerStyle.Parse(typeof(MarkerStyle), "Circle");

                    if (_OldPosition > -1)
                    {
                        _tragetChart.Series[0].Points[_OldPosition].DeleteCustomProperty("MarkerImage");
                        _tragetChart.Series[0].Points[_OldPosition].DeleteCustomProperty("MarkerStyle");

                        _tragetChart.Series[0].Points[_OldPosition].DeleteCustomProperty("MarkerSize");
                        _tragetChart.Series[0].Points[_OldPosition].DeleteCustomProperty("MarkerColor");
                        _tragetChart.Series[0].Points[_OldPosition].DeleteCustomProperty("MarkerBorderColor");
                        _tragetChart.Series[0].Points[_OldPosition].DeleteCustomProperty("MarkerBorderWidth");
                    }

                    _tragetChart.Series[0].MarkerSize = _tragetChart.Series[0].MarkerSize;
                    _tragetChart.Series[0].MarkerColor = _tragetChart.Series[0].MarkerColor;
                    _tragetChart.Series[0].MarkerBorderColor = _tragetChart.Series[0].MarkerBorderColor;
                    _tragetChart.Series[0].MarkerBorderWidth = _tragetChart.Series[0].MarkerBorderWidth;

                    _tragetChart.Series[0].Points[_NewPosition].MarkerSize = 8;
                    _tragetChart.Series[0].Points[_NewPosition].MarkerColor = System.Drawing.Color.Red;
                }
            }
            catch { }
        }

        public static void DisplayPointIndex(System.Windows.Forms.DataVisualization.Charting.Chart _tragetChart, int _valPos, bool visibleFalg)
        {
            try
            {
                if (_valPos > -1)
                {
                    if (true == visibleFalg)
                    {
                        _tragetChart.Series[0].MarkerImage = "";
                        _tragetChart.Series[0].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;// (MarkerStyle)MarkerStyle.Parse(typeof(MarkerStyle), "Circle");

                        _tragetChart.Series[0].MarkerSize = _tragetChart.Series[0].MarkerSize;
                        _tragetChart.Series[0].MarkerColor = _tragetChart.Series[0].MarkerColor;
                        _tragetChart.Series[0].MarkerBorderColor = _tragetChart.Series[0].MarkerBorderColor;
                        _tragetChart.Series[0].MarkerBorderWidth = _tragetChart.Series[0].MarkerBorderWidth;

                        _tragetChart.Series[0].Points[_valPos].MarkerSize = 8;
                        _tragetChart.Series[0].Points[_valPos].MarkerColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        _tragetChart.Series[0].Points[_valPos].DeleteCustomProperty("MarkerImage");
                        _tragetChart.Series[0].Points[_valPos].DeleteCustomProperty("MarkerStyle");

                        _tragetChart.Series[0].Points[_valPos].DeleteCustomProperty("MarkerSize");
                        _tragetChart.Series[0].Points[_valPos].DeleteCustomProperty("MarkerColor");
                        _tragetChart.Series[0].Points[_valPos].DeleteCustomProperty("MarkerBorderColor");
                        _tragetChart.Series[0].Points[_valPos].DeleteCustomProperty("MarkerBorderWidth");

                        _tragetChart.Series[0].Points[_valPos].MarkerSize = _tragetChart.Series[0].MarkerSize;
                        _tragetChart.Series[0].Points[_valPos].MarkerColor = _tragetChart.Series[0].MarkerColor;
                    }
                }
            }
            catch { }
        }
        public static void DisplayChartSelectIndexMarkerClear(System.Windows.Forms.DataVisualization.Charting.Chart _tragetChart)
        {
            try
            {
                for ( int inx = 0 ; inx < _tragetChart.Series[ 0 ].Points.Count ; inx++ )
                {
                    _tragetChart.Series[ 0 ].Points[ inx ].DeleteCustomProperty( "MarkerImage" );
                    _tragetChart.Series[ 0 ].Points[ inx ].DeleteCustomProperty( "MarkerStyle" );

                    _tragetChart.Series[ 0 ].Points[ inx ].DeleteCustomProperty( "MarkerSize" );
                    _tragetChart.Series[ 0 ].Points[ inx ].DeleteCustomProperty( "MarkerColor" );
                    _tragetChart.Series[ 0 ].Points[ inx ].DeleteCustomProperty( "MarkerBorderColor" );
                    _tragetChart.Series[ 0 ].Points[ inx ].DeleteCustomProperty( "MarkerBorderWidth" );
                }
            }
            catch { }
        }
        #endregion [  선택한 Chart Point Index 의 Mark 강조]

        public static void ChartDisplaySetMinMaxValue(System.Windows.Forms.DataVisualization.Charting.Chart targetChart, double _max, double _min)
        {
            try
            {
                if ( _max > _min )
                {
                    targetChart.ChartAreas[ 0 ].AxisY.Maximum = _max + 0.1;
                    targetChart.ChartAreas[ 0 ].AxisY.Minimum = _min - 0.1;
                }
            }
            catch ( Exception ex )
            {
                cLog.FileWrite_Str( "Chart Min/Max 설정 실패 : " + ex.Message.ToString(), cLog.eLogType.EXCEPTION );
                cLog.FileWrite_Str( "Chart Min/Max 설정 실패 : " + ex.Message.ToString(), cLog.eLogType.LOG );
            }
        }
        public static void ChartDisplaySetMinMaxValueVertical(System.Windows.Forms.DataVisualization.Charting.Chart targetChart, double _max, double _min)
        {
            try
            {
                if ( _max > _min )
                {
                    targetChart.ChartAreas[ 0 ].AxisX.Maximum = _max + 0.1;
                    targetChart.ChartAreas[ 0 ].AxisX.Minimum = _min - 0.1;
                }
            }
            catch ( Exception ex )
            {
                cLog.FileWrite_Str( "Chart Min/Max 설정 실패 : " + ex.Message.ToString(), cLog.eLogType.EXCEPTION );
                cLog.FileWrite_Str( "Chart Min/Max 설정 실패 : " + ex.Message.ToString(), cLog.eLogType.LOG );
            }
        }






        //신규 Display function 2017.10.13=================================================================▼▼▼▼▼▼▼▼
        public static void ChartDisplayFormDataTable(System.Data.DataTable target, System.Windows.Forms.DataVisualization.Charting.Chart targetChart)
        {
            try
            {
                if ( target.Rows.Count <= 0 ) return;

                // Chart 초기화
                ChartDisplayFormDefault( targetChart, "MAIN", System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line );

                targetChart.Series.Clear();

                targetChart.Series.Add( "OK" );
                targetChart.Series.Add( "NG" );

                for ( int idx = 0 ; idx < target.Rows.Count ; idx++ )
                {
                    targetChart.Series[ "OK" ].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                    targetChart.Series[ "OK" ].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                    targetChart.Series[ "OK" ].Color = System.Drawing.Color.Blue;
                    targetChart.Series[ "NG" ].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                    targetChart.Series[ "NG" ].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.String;
                    targetChart.Series[ "NG" ].Color = System.Drawing.Color.Red;
                    //targetChart.Series[ "OK" ].Points.AddXY( idx, Convert.ToDouble( target.Rows[ idx ][ 1 ] ) );
                    //targetChart.Series[ "NG" ].Points.AddXY( idx, Convert.ToDouble( target.Rows[ idx ][ 2 ] ) );
                    targetChart.Series[ "OK" ].Points.AddXY( target.Rows[ idx ][ 0 ], Convert.ToDouble( target.Rows[ idx ][ 1 ] ) );
                    targetChart.Series[ "NG" ].Points.AddXY( target.Rows[ idx ][ 0 ], Convert.ToDouble( target.Rows[ idx ][ 2 ] ) );
                }
                targetChart.Series[ "OK" ].IsValueShownAsLabel = true;
                targetChart.Series[ "NG" ].IsValueShownAsLabel = true;

                //targetChart.ChartAreas[ 0 ].AxisY.Maximum = maxSearchValue + 2;
                //targetChart.ChartAreas[ 0 ].AxisY.Minimum = 0;
                targetChart.ChartAreas[ 0 ].AxisY.Interval = Math.Round( ( targetChart.ChartAreas[ 0 ].AxisY.Maximum - targetChart.ChartAreas[ 0 ].AxisY.Minimum ) / 4d, 3 );
                targetChart.ChartAreas[ 0 ].AxisX.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
                targetChart.ChartAreas[ 0 ].AxisX.LabelStyle.IntervalOffset = 0;
                targetChart.ChartAreas[ 0 ].AxisX.LabelStyle.Interval = 1;
                targetChart.ChartAreas[ 0 ].AxisX.LabelStyle.Enabled = true;
                targetChart.ChartAreas[ 0 ].AxisX.LabelAutoFitMaxFontSize = 6;
                targetChart.ChartAreas[ 0 ].AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);

                targetChart.Invalidate();
            }
            catch ( Exception ex )
            {
                cLog.FileWrite_Str( "Chart 정보 Display 실패 : " + ex.Message.ToString(), cLog.eLogType.EXCEPTION );
            }
        }



    }
}