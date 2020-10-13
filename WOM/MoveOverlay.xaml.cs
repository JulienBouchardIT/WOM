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
        public MoveOverlay()
        {
            InitializeComponent();
            this.Topmost = true;
            this.KeyDown += new KeyEventHandler(MainWindow_KeyDown);
            this.Width = SystemParameters.PrimaryScreenWidth;
            this.Height = SystemParameters.PrimaryScreenHeight;
            this.Top = SystemParameters.VirtualScreenTop;
            this.Left = SystemParameters.VirtualScreenLeft;
        }

        void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Escape)
            {
                this.Close();
            }
        }
    }
}
