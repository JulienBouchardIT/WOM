using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace WOM
{
    class WindowOrderManager
    {
        private static WindowOrderManager wom = new WindowOrderManager();
        public static WindowOrderManager getWOM() { return wom; }

        public WindowOrderManager()
        {
            this.config = Config.getConfig();
            initListWinItf();
        }

        private void initListWinItf()
        {
            if (listWinItf == null)
                listWinItf = new ObservableCollection<WinInterface>();

            listWinItf.Clear();
            listWinItf.Add(new WinInterface());

            foreach (WinInterface window in DesktopHandler.GetAllWindows())
            {
                listWinItf.Add(window);
            }

        }

        private void saveConfig()
        {
            //todo: Modif config obj according to 

            config.saveConfig();
        }

        public void apply()
        {
            IntPtr wallpaperHandler = DesktopHandler.GetDesktopHandler();

            foreach (WinInterface itf in listWinItf)
            {
                //todo: Complet
                if (itf.id == 0)
                {
                    break;
                }
                W32.Rect rct;

                W32.GetWindowRect(itf.handler, out rct);

                W32.SetParent(itf.handler, wallpaperHandler);

                W32.MoveWindow(itf.handler, rct.left, rct.top, rct.right - rct.left, rct.bottom - rct.top, true);
            }
            saveConfig();
        }


        public IList<WinInterface> listWinItf { get; set; }
        public Config config { get; private set; }
        
    }
}
