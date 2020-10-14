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
    /// Logique d'interaction pour MoveOverlay.xaml
    /// </summary>
    public partial class MoveOverlay : Window
    {
        public MoveOverlay(WindowOrderManager wom, WinInterface itf)
        {
            this.wom = wom;
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
                wom.CloseOverlays();
                this.Close();
            }
            var key = e.Key;

            switch (key)
            {
                case Key.Up:
                    itf.Move(Direction.up, 2);
                    break;
                case Key.Down:
                    itf.Move(Direction.down, 1);
                    break;
                case Key.Left:
                    itf.Move(Direction.left, 2);
                    break;
                case Key.Right:
                    itf.Move(Direction.right, 1);
                    break;
                default:
                    break;
            }
            //W32.SetForegroundWindow(itf.handler);
        }

        private WindowOrderManager wom { get; set; }
        private WinInterface itf { get; set; }
    }
}
