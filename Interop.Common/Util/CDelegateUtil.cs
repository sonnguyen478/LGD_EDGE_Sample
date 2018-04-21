using System;

namespace Interop.Common.Util
{
    /// <summary>
    ///  크로스 컴파일 에러 발생을 방지 하기위해 별도의 method 로 만듬.
    /// </summary>
    public static class CDelegate
    {
        //http://www.devpia.co.kr/MAEUL/Contents/Detail.aspx?BoardID=18&MAEULNO=8&no=1723&page=11


        // 사용법
        //textBox1.InvokeIfNeeded(() =>
        //                    {
        //                        textBox1.Text = ss;
        //                    });

        public static void InvokeIfNeeded(this System.Windows.Forms.Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                try
                {
                    control.Invoke(action);
                }
                catch { }
            }
            else
            {
                action();
            }
        }

        //글짜
        public static void SetText(System.Windows.Forms.Control _control, string _string)
        {
            _control.InvokeIfNeeded(() => { _control.Text = _string; });
        }

        //배경색
        public static void SetBackColor(System.Windows.Forms.Control _control, System.Drawing.Color _color)
        {
            _control.InvokeIfNeeded(() => { _control.BackColor = _color; });
        }

        // 글자색 변경
        public static void SetForeColor(System.Windows.Forms.Control _control, System.Drawing.Color _color)
        {
            _control.InvokeIfNeeded(() => { _control.ForeColor = _color; });
        }
        public static string GetText(System.Windows.Forms.Control _control)
        {
            string _text = string.Empty;

            _control.InvokeIfNeeded(() =>
            {
                _text = _control.Text;
            });

            return _text;
        }

        //this.Invoke(new MethodInvoker(delegate()
        //{ 
        //}));
    }
}
