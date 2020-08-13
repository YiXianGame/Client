using Native.Sdk.Cqp.EventArgs;
using Native.Sdk.Cqp.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Pack
{
    public class Menu_OpenWindow : IMenuCall
    {
        ManualResetEvent manualResetEvent;
        private Thread thread;
        bool open = false;
        public void MenuCall(object sender, CQMenuCallEventArgs e)
        {
            if(thread == null)
            {
                thread = new Thread(new ThreadStart(() =>
                {
                    while (true)
                    {
                        open = true;
                        new MainWindow().ShowDialog();
                        GC.Collect();
                        open = false;
                        manualResetEvent = new ManualResetEvent(false);
                        Console.WriteLine("窗体Event返回:" + manualResetEvent.WaitOne());
                    }
                }));
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
            }
            else
            {
                if (open) Console.WriteLine("窗体已经打开");
                else manualResetEvent.Set();
            }
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 对变量置 null, 因为被关闭的窗口无法重复显示
            
        }
    }
}
