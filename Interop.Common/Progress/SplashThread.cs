using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Interop.Common.Progress
{
    public class SplashThread
    {
        private delegate void CloseCallback();
        private delegate void UpdateProgressCallback(int value, string _text);

        private Thread thread;
        private SplashForm form;
        private EventWaitHandle loaded;

        private int iAllStepCnt = 10;
        public int AllStepCnt
        {
            get { return iAllStepCnt; }
            set { iAllStepCnt = value; }
        }

        public SplashThread()
        {
            thread = new Thread(new ThreadStart(RunSplash));
            loaded = new EventWaitHandle(false, EventResetMode.ManualReset);
        }

        public void Open()
        {
            thread.Start();
        }

        public void Close()
        {
            loaded.WaitOne();
            form.Invoke(new CloseCallback(form.Close));
        }

        public void Join()
        {
            thread.Join();
        }

        public void UpdateProgress(int value, string _text)
        {
            loaded.WaitOne();

            int _value = 0;
            try
            {
                _value = (int)(value*100 / AllStepCnt);
            }
            catch {
                _value = 0;
            }

            form.Invoke(new UpdateProgressCallback(form.UpdateProgress), _value, _text);
        }

        private void RunSplash()
        {
            form = new SplashForm();
            form.Load += new EventHandler(OnLoad);
            form.ShowDialog();
        }
          
        void OnLoad(object sender, EventArgs e)
        {
            loaded.Set();
        }  
    }
}
