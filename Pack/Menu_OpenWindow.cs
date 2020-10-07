using Make.MODEL;
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
        private Thread thread;
        ManualResetEvent manualResetEvent = new ManualResetEvent(false);
        public void MenuCall(object sender, CQMenuCallEventArgs e)
        {
            try
            {
                if (GeneralControl.CQApi != null)
                {
                    if (GeneralControl.socket != null && GeneralControl.socket.client_socket.Connected)
                    {
                        if (GeneralControl.MainMenu == null)
                        {
                            thread = new Thread(new ThreadStart(() =>
                            {
                                while (true)
                                {
                                    GeneralControl.MainMenu = new MainWindow();
                                    GeneralControl.MainMenu.ShowDialog();
                                    manualResetEvent.Reset();
                                    manualResetEvent.WaitOne();
                                }
                            }));
                            thread.SetApartmentState(ApartmentState.STA);
                            thread.Start();
                        }
                        else
                        {
                            manualResetEvent.Set();
                        }
                    }
                    Console.WriteLine("还未连接服务端，请稍后再试");
                }
                else Console.WriteLine("未登陆QQ，请登陆后尝试");
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 对变量置 null, 因为被关闭的窗口无法重复显示
            
        }
    }
}
