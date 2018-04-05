using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LGD_EDGE_Sample
{
    public sealed class cBGWorkerEvents
    {
        public class EventArgList : EventArgs
        {
            public int data
            {
                get;
                set;
            }
        }

        public class EventHeartBit
        {
            public delegate void EventHandler(object sender, EventArgList e);
            public event EventHandler evHeartBitHandler;

            public void HeartbitEvent()
            {
                try
                {
                    if ( evHeartBitHandler != null )
                    {
                        evHeartBitHandler( null, null );
                    }

                }
                catch ( Exception ex )
                {
                    Console.WriteLine( "HeartbitEvent : " + ex.ToString() );
                }
            }
        }
        public class PLCEvents
        {
            public delegate void BGWorkerEventHandler(object sender, EventArgList e);
            public event BGWorkerEventHandler evHeartBit;
            public event BGWorkerEventHandler evBypass;
            public event BGWorkerEventHandler evInspStart;

            public void Fliker(int _data)
            {
                try
                {
                    if ( evHeartBit != null )
                    {
                        EventArgList arg = new EventArgList();
                        arg.data = _data;
                        evHeartBit( null, arg );
                    }

                }
                catch ( Exception ex )
                {
                    Console.WriteLine( "PLC Filker Error : " + ex.Message.ToString() );
                }
            }
            public void Bypass(int _data)
            {
                try
                {
                    if ( evBypass != null )
                    {
                        EventArgList arg = new EventArgList();
                        arg.data = _data;
                        evBypass( null, arg );
                    }

                }
                catch ( Exception ex )
                {
                    Console.WriteLine( "PLC Bypass : " + ex.Message.ToString() );
                }
            }
            public void InspStart(int _data)
            {
                try
                {
                    if ( evInspStart != null )
                    {
                        EventArgList arg = new EventArgList();
                        arg.data = _data;
                        evInspStart( null, arg );
                    }

                }
                catch ( Exception ex )
                {
                    Console.WriteLine( "PLC InspStart : " + ex.Message.ToString() );
                }
            }
           


        }
    }
}
