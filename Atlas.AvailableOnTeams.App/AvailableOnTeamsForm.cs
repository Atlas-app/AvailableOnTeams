using System.Runtime.InteropServices;

namespace AvailableOnTeams
{
    public partial class AvailableOnTeamsForm : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
        //Mouse actions
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        //private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        //private const int MOUSEEVENTF_RIGHTUP = 0x10;

        [ComImport]
        [Guid("13709620-C279-11CE-A49E-444553540000")]
        [ClassInterface(ClassInterfaceType.None)]
        private class Shell32Class { }

        [ComImport]
        [Guid("D8F015C0-C278-11CE-A49E-444553540000")]
        [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
        private interface IShellDispatch
        {
            void ToggleDesktop();
        }

        public AvailableOnTeamsForm()
        {
            InitializeComponent();

            var shell = (IShellDispatch)new Shell32Class();
            shell.ToggleDesktop();

            Task.Run(() =>
            {
                while (true)
                {
                    Cursor.Position = new Point(1280, 540);

                    DoMouseClick();
                    for (int i = 0; i < 100; i++)
                    {
                        Cursor.Position = new Point(Cursor.Position.X + 1, Cursor.Position.Y);
                        Thread.Sleep(10);
                    }

                    for (int i = 0; i < 100; i++)
                    {
                        Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y - 1);
                        Thread.Sleep(10);
                    }

                    for (int i = 0; i < 100; i++)
                    {
                        Cursor.Position = new Point(Cursor.Position.X - 1, Cursor.Position.Y);
                        Thread.Sleep(10);
                    }

                    for (int i = 0; i < 100; i++)
                    {
                        Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y + 1);
                        Thread.Sleep(10);
                    }

                    Thread.Sleep(5000);
                }
            });
        }

        private void DoMouseClick()
        {
            //Call the imported function with the cursor's current position
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
