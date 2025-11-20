using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace AvailableOnTeams
{
    public partial class AvailableOnTeamsTray : Form
    {
        private NotifyIcon _icon;

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
        // Mouse actions
        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;
        // Cancellation Token
        public CancellationTokenSource _cts;
        public Task _workerTask;

        #region Toggle windows visualization
        private bool _windowsMinimized = false;

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        private const int SW_MINIMIZE = 6;

        // Helper to minimize all windows
        private void MinimizeAllWindows()
        {
            foreach (Process p in Process.GetProcesses())
            {
                if (p.MainWindowHandle != IntPtr.Zero && IsWindowVisible(p.MainWindowHandle))
                {
                    ShowWindow(p.MainWindowHandle, SW_MINIMIZE);
                }
            }
        }


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
        #endregion

        #region Global Hotkeys
            // Add these constants and imports
        private const int WM_HOTKEY = 0x0312;
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        // Modifier keys codes
        private const uint MOD_ALT = 0x1;
        private const uint MOD_CONTROL = 0x2;

        // Hotkey IDs
        private const int HOTKEY_ID_START = 1;
        private const int HOTKEY_ID_STOP = 2;

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            // Register Ctrl+Alt+A for Start
            RegisterHotKey(this.Handle, HOTKEY_ID_START, MOD_CONTROL | MOD_ALT, (uint)Keys.A);
            // Register Ctrl+Alt+S for Stop
            RegisterHotKey(this.Handle, HOTKEY_ID_STOP, MOD_CONTROL | MOD_ALT, (uint)Keys.S);
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            // Unregister hotkeys
            UnregisterHotKey(this.Handle, HOTKEY_ID_START);
            UnregisterHotKey(this.Handle, HOTKEY_ID_STOP);
            base.OnHandleDestroyed(e);
        }


        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_HOTKEY)
            {
                int id = m.WParam.ToInt32();
                if (id == HOTKEY_ID_START)
                {
                    Start();
                }
                else if (id == HOTKEY_ID_STOP)
                {
                    Stop();
                }
            }
            base.WndProc(ref m);
        }
        #endregion

        public AvailableOnTeamsTray()
        {
            InitializeComponent();
            // After InitializeComponent();
            label6.Text = $"Version: {Assembly.GetExecutingAssembly().GetName().Version}";

            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            this.Visible = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            var assembly = Assembly.GetExecutingAssembly();
            var names = assembly.GetManifestResourceNames();
            Debug.WriteLine(string.Join("\n", names));

            using var stream = assembly.GetManifestResourceStream("Atlas.AvailableOnTeams.Assets.AvailableOnTeams.ico") ?? 
                throw new InvalidOperationException("Embedded resource 'Atlas.AvailableOnTeams.Assets.AvailableOnTeams.ico' not found.");
            _icon = new NotifyIcon()
            {
                Icon = new Icon(stream),
                Visible = true,
                Text = "Make me available on Teams"
            };

            // Menu della tray
            var menu = new ContextMenuStrip();
            menu.Items.Add("Start", null, (_, _) => Start());
            menu.Items.Add("Stop", null, (_, _) => Stop());
            menu.Items.Add("About", null, (_, _) => ShowAbout());
            menu.Items.Add("Exit", null, (_, _) => ExitApp());

            _icon.ContextMenuStrip = menu;
        }


        #region Mouse Movement and Click Simulation
        private void Start()
        {
            if (!_windowsMinimized)
            {
                var shell = (IShellDispatch)new Shell32Class();
                shell.ToggleDesktop();
                _windowsMinimized = true;
            }

            _cts?.Cancel();
            _cts = new CancellationTokenSource();
            var token = _cts.Token;

            _workerTask = Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    Cursor.Position = new Point(1280, 540);

                    DoMouseClick();
                    for (int i = 0; i < 100 && !token.IsCancellationRequested; i++)
                    {
                        Cursor.Position = new Point(Cursor.Position.X + 1, Cursor.Position.Y);
                        Thread.Sleep(10);
                    }

                    for (int i = 0; i < 100 && !token.IsCancellationRequested; i++)
                    {
                        Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y - 1);
                        Thread.Sleep(10);
                    }

                    for (int i = 0; i < 100 && !token.IsCancellationRequested; i++)
                    {
                        Cursor.Position = new Point(Cursor.Position.X - 1, Cursor.Position.Y);
                        Thread.Sleep(10);
                    }

                    for (int i = 0; i < 100 && !token.IsCancellationRequested; i++)
                    {
                        Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y + 1);
                        Thread.Sleep(10);
                    }

                    for (int i = 0; i < 500 && !token.IsCancellationRequested; i++)
                    {
                        Thread.Sleep(10);
                    }
                }
            }, token);
        }

        private void Stop()
        {
            if (_windowsMinimized)
            {
                var shell = (IShellDispatch)new Shell32Class();
                shell.ToggleDesktop();
                _windowsMinimized = false;
            }

            _cts?.Cancel();
            _workerTask?.Wait(1000);
            _cts = null;
            _workerTask = null;
        }

        private void DoMouseClick()
        {
            //Call the imported function with the cursor's current position
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
        }

        #endregion

        private void ShowAbout()
        {
            this.Visible = true;
            this.ShowInTaskbar = true;
            this.WindowState = FormWindowState.Normal;
        }

        private void ExitApp()
        {
            _icon.Visible = false;
            Application.Exit();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
                this.ShowInTaskbar = false;
            }
            else
            {
                base.OnFormClosing(e);
            }
        }

        public void BuyMeACoffeeOnClick(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://ko-fi.com/atlasapp",
                UseShellExecute = true
            });
        }

    }
}
