using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace LGD_EDGE_Sample
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault( false );

            bool checkOther = false;
            Mutex m_OneProcess = null;

            try
            {
                m_OneProcess = new System.Threading.Mutex( true, "Edge Sample TEST", out checkOther );

                if ( checkOther == false )
                {
                    MessageBox.Show( "이미 실행 중 입니다." );
                    return;
                }
                //Application.Run( new frmSnapTest() );
                Application.Run( new frmSample() );
            }
            finally
            {

            }
        }
    }
}
