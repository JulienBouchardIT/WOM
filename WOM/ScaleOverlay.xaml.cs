using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WOM
{
    /// <summary>
    /// Logique d'interaction pour ScaleOverlay.xaml
    /// </summary>
    public partial class ScaleOverlay : Window
    {
        public ScaleOverlay(WindowOrderManager wom, WinInterface itf)
        {
            this.itf = itf;
            InitializeComponent();
            this.Topmost = true;
            this.KeyDown += new KeyEventHandler(MainWindow_KeyDown);
            this.Width = SystemParameters.PrimaryScreenWidth;
            this.Height = SystemParameters.PrimaryScreenHeight;
            this.Top = SystemParameters.VirtualScreenTop;
            this.Left = SystemParameters.VirtualScreenLeft;
            this.Activate();
        }

        void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Escape)
            {
                this.Close();
            }
            var key = e.Key;

            switch (key)
            {
                case Key.Up:
                    itf.Resize(Size.PlusHeight, 1);
                    break;
                case Key.Down:
                    itf.Resize(Size.MinusHeight, 2);
                    break;
                case Key.Left:
                    itf.Resize(Size.MinusWidth, 2);
                    break;
                case Key.Right:
                    itf.Resize(Size.PlusWidth, 1);
                    break;
                default:
                    break;
            }
            //W32.SetForegroundWindow(itf.handler);
        }

        private WinInterface itf { get; set; }
        private WindowOrderManager wom { get; set; }
    }
}
