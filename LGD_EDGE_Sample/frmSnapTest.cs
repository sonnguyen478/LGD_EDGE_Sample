using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LGD_EDGE_Sample
{
    public partial class frmSnapTest : Form
    {
        private AxCVDISPLAYLib.AxCVdisplay[] axDisplayList; // 화면
        private AxCVIMAGELib.AxCVimage[] axImageList;// Image




        public frmSnapTest()
        {
            InitializeComponent();
        }
        private void frmSnapTest_Load(object sender, EventArgs e)
        {
            try
            {
                fnCAM_Initialize();
                fnCamEventPlus();
                mCVBInit();

                Interop.Common.Util.CDelegate.SetText( lblStartTime, "" );
                Interop.Common.Util.CDelegate.SetText( lblEndTime, "" );
                Interop.Common.Util.CDelegate.SetText( lblElapsedTime, "" );
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex.Message.ToString() );
            }
        }
        private void frmSnapTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                fnCamEventMinus();
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex.Message.ToString() );
            }
        }
        private void btnSnap_Click(object sender, EventArgs e)
        {
            DateTime startTime;
            DateTime endTime;
            try
            {
                startTime = System.DateTime.Now;
                Interop.Common.Util.CDelegate.SetText( lblStartTime, startTime.ToString( "yyyy/MM/dd HH:mm:ss:fff" ) );

                fnCVBDisplayClear();
                mSnap();

                endTime = System.DateTime.Now;
                Interop.Common.Util.CDelegate.SetText( lblEndTime, endTime.ToString( "yyyy/MM/dd HH:mm:ss:fff" ) );

                Interop.Common.Util.CDelegate.SetText( lblElapsedTime, ( endTime - startTime ).ToString() );
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex.Message.ToString() );
            }
        }





        private void fnCAM_Initialize()
        {
            bool bIsOpen = false;
            string driverString = string.Empty;
            try
            {
                driverString = Environment.ExpandEnvironmentVariables( "%CVB%" ) + @"Drivers\GenICam.vin";

                if ( axCVimage1.LoadImage( driverString ) )
                { bIsOpen = true; }
                else
                { MessageBox.Show( "#1 open error" ); }
                if ( bIsOpen == true ) { if ( axCVimage2.LoadImage( driverString ) ) { } else { bIsOpen = false; MessageBox.Show( "#2 open error" ); } }
                if ( bIsOpen == true ) { if ( axCVimage3.LoadImage( driverString ) ) { } else { bIsOpen = false; MessageBox.Show( "#3 open error" ); } }
                if ( bIsOpen == true ) { if ( axCVimage4.LoadImage( driverString ) ) { } else { bIsOpen = false; MessageBox.Show( "#4 open error" ); } }
                if ( bIsOpen == true ) { if ( axCVimage5.LoadImage( driverString ) ) { } else { bIsOpen = false; MessageBox.Show( "#5 open error" ); } }
                if ( bIsOpen == true ) { if ( axCVimage6.LoadImage( driverString ) ) { } else { bIsOpen = false; MessageBox.Show( "#6 open error" ); } }
                if ( bIsOpen == true ) { if ( axCVimage7.LoadImage( driverString ) ) { } else { bIsOpen = false; MessageBox.Show( "#7 open error" ); } }
                if ( bIsOpen == true ) { if ( axCVimage8.LoadImage( driverString ) ) { } else { bIsOpen = false; MessageBox.Show( "#8 open error" ); } }

                if ( bIsOpen == true )
                {
                    axCVgrabber1.CamPort = 0; axCVgrabber1.Image = axCVimage1.Image; axCVimage1.Image = axCVgrabber1.Image;
                    axCVgrabber2.CamPort = 1; axCVgrabber2.Image = axCVimage2.Image; axCVimage2.Image = axCVgrabber2.Image;
                    axCVgrabber3.CamPort = 2; axCVgrabber3.Image = axCVimage3.Image; axCVimage3.Image = axCVgrabber3.Image;
                    axCVgrabber4.CamPort = 3; axCVgrabber4.Image = axCVimage4.Image; axCVimage4.Image = axCVgrabber4.Image;
                    axCVgrabber5.CamPort = 0; axCVgrabber5.Image = axCVimage5.Image; axCVimage5.Image = axCVgrabber5.Image;
                    axCVgrabber6.CamPort = 1; axCVgrabber6.Image = axCVimage6.Image; axCVimage6.Image = axCVgrabber6.Image;
                    axCVgrabber7.CamPort = 2; axCVgrabber7.Image = axCVimage7.Image; axCVimage7.Image = axCVgrabber7.Image;
                    axCVgrabber8.CamPort = 3; axCVgrabber8.Image = axCVimage8.Image; axCVimage8.Image = axCVgrabber8.Image;
                }
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex.Message.ToString() );
            }
        }
        private void axCVgrabber_Pos01_ImageUpdated(object sender, System.EventArgs e) { axCVimage1.Image = axCVgrabber1.Image; }
        private void axCVgrabber_Pos02_ImageUpdated(object sender, System.EventArgs e) { axCVimage2.Image = axCVgrabber2.Image; }
        private void axCVgrabber_Pos03_ImageUpdated(object sender, System.EventArgs e) { axCVimage3.Image = axCVgrabber3.Image; }
        private void axCVgrabber_Pos04_ImageUpdated(object sender, System.EventArgs e) { axCVimage4.Image = axCVgrabber4.Image; }
        private void axCVgrabber_Pos05_ImageUpdated(object sender, System.EventArgs e) { axCVimage5.Image = axCVgrabber5.Image; }
        private void axCVgrabber_Pos06_ImageUpdated(object sender, System.EventArgs e) { axCVimage6.Image = axCVgrabber6.Image; }
        private void axCVgrabber_Pos07_ImageUpdated(object sender, System.EventArgs e) { axCVimage7.Image = axCVgrabber7.Image; }
        private void axCVgrabber_Pos08_ImageUpdated(object sender, System.EventArgs e) { axCVimage8.Image = axCVgrabber8.Image; }
        private void fnCamEventPlus()
        {
            try
            {
                axCVgrabber1.ImageUpdated += new System.EventHandler( axCVgrabber_Pos01_ImageUpdated );
                axCVgrabber2.ImageUpdated += new System.EventHandler( axCVgrabber_Pos02_ImageUpdated );
                axCVgrabber3.ImageUpdated += new System.EventHandler( axCVgrabber_Pos03_ImageUpdated );
                axCVgrabber4.ImageUpdated += new System.EventHandler( axCVgrabber_Pos04_ImageUpdated );
                axCVgrabber5.ImageUpdated += new System.EventHandler( axCVgrabber_Pos05_ImageUpdated );
                axCVgrabber6.ImageUpdated += new System.EventHandler( axCVgrabber_Pos06_ImageUpdated );
                axCVgrabber7.ImageUpdated += new System.EventHandler( axCVgrabber_Pos07_ImageUpdated );
                axCVgrabber8.ImageUpdated += new System.EventHandler( axCVgrabber_Pos08_ImageUpdated );
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex.Message.ToString() );
            }
        }
        private void fnCamEventMinus()
        {
            try
            {
                axCVgrabber1.ImageUpdated -= new System.EventHandler( axCVgrabber_Pos01_ImageUpdated );
                axCVgrabber2.ImageUpdated -= new System.EventHandler( axCVgrabber_Pos02_ImageUpdated );
                axCVgrabber3.ImageUpdated -= new System.EventHandler( axCVgrabber_Pos03_ImageUpdated );
                axCVgrabber4.ImageUpdated -= new System.EventHandler( axCVgrabber_Pos04_ImageUpdated );
                axCVgrabber5.ImageUpdated -= new System.EventHandler( axCVgrabber_Pos05_ImageUpdated );
                axCVgrabber6.ImageUpdated -= new System.EventHandler( axCVgrabber_Pos06_ImageUpdated );
                axCVgrabber7.ImageUpdated -= new System.EventHandler( axCVgrabber_Pos07_ImageUpdated );
                axCVgrabber8.ImageUpdated -= new System.EventHandler( axCVgrabber_Pos08_ImageUpdated );
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex.Message.ToString() );
            }
        }
        private void mCVBInit()
        {
            axDisplayList = new AxCVDISPLAYLib.AxCVdisplay[ 8 ];
            axImageList = new AxCVIMAGELib.AxCVimage[ 8 ];

            axDisplayList[ 0 ] = axCVdisplay1;
            axDisplayList[ 1 ] = axCVdisplay2;
            axDisplayList[ 2 ] = axCVdisplay3;
            axDisplayList[ 3 ] = axCVdisplay4;
            axDisplayList[ 4 ] = axCVdisplay5;
            axDisplayList[ 5 ] = axCVdisplay6;
            axDisplayList[ 6 ] = axCVdisplay7;
            axDisplayList[ 7 ] = axCVdisplay8;

            axImageList[ 0 ] = axCVimage1;
            axImageList[ 1 ] = axCVimage2;
            axImageList[ 2 ] = axCVimage3;
            axImageList[ 3 ] = axCVimage4;
            axImageList[ 4 ] = axCVimage5;
            axImageList[ 5 ] = axCVimage6;
            axImageList[ 6 ] = axCVimage7;
            axImageList[ 7 ] = axCVimage8;
        }
        private void fnCVBDisplayClear()
        {
            for ( int idx = 0 ; idx < axDisplayList.Length ; idx++ )
            {
                axDisplayList[ idx ].RemoveAllOverlayObjects();
                axDisplayList[ idx ].Refresh();
            }
        }
        private void mSnap()
        {
            string snapDate = string.Empty;
            try
            {
                snapDate = System.DateTime.Now.ToString( "yyyyMMddHHmmss");
                for ( int idx = 0 ; idx < axImageList.Length ; idx++ )
                {
                    if ( true == axImageList[ idx ].Snap() )
                    {
                        axDisplayList[ idx ].Image = axImageList[ idx ].Image;
                        axDisplayList[ idx ].Refresh();

                        if ( chkSnapFileSave.Checked == true )
                        {
                            if ( System.IO.Directory.Exists( @"D:\SNAPTES" ) == false ) System.IO.Directory.CreateDirectory( @"D:\SNAPTEST" );
                            snapDate = string.Format( "CAM_{0}_{1}", ( idx + 1 ).ToString( "00" ), snapDate );
                            axImageList[ idx ].SaveImage( string.Format( "{0}{1}.jpg", @"D:\SNAPTEST\", snapDate ) );
                        }
                    }
                }
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex.Message.ToString() );
            }
        }



    }
}
