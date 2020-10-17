using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms.VisualStyles;

namespace WOM
{
    /// <summary>
    /// Singleton
    /// WindowOrderManager manage all operations regarding the application itself. 
    /// </summary>
    public class WindowOrderManager
    {
        private static WindowOrderManager wom = new WindowOrderManager();
        public static WindowOrderManager getWOM() { return wom; }

        private WindowOrderManager()
        {
            this.config = Config.getConfig();
            InitList();
            GetRunningItfs();
        }

        private void InitList()
        {
            ListNotAssing = new ObservableCollection<WinInterface>();
            ListBackground = new ObservableCollection<WinInterface>();
            ListBottom = new ObservableCollection<WinInterface>();
            ListForeground = new ObservableCollection<WinInterface>();
        }

        private void GetRunningItfs()
        {
            if (ListNotAssing == null)
                ListNotAssing = new ObservableCollection<WinInterface>();

            ListNotAssing.Clear();

            foreach (WinInterface window in DesktopHandler.GetAllWindows())
            {
                ListNotAssing.Add(window);
            }

        }

        private void SaveConfig()
        {
            //todo: Modif config obj according to 

            config.saveConfig();
        }

        public void Apply()
        {
            IntPtr wallpaperHandler = DesktopHandler.GetDesktopHandler();

            foreach (WinInterface itf in ListNotAssing)
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

                break;
            }
            SaveConfig();
        }

        public void CloseOverlays()
        {
            //todo:
        }

        public void Scale(WinInterface itf)
        {
            ScaleOverlay overlay = new ScaleOverlay(this, itf);
            W32.SetForegroundWindow(itf.handler);

            overlay.ShowDialog();

        }

        public void Move(WinInterface itf)
        {
            MoveOverlay overlay = new MoveOverlay(this, itf);
            W32.SetForegroundWindow(itf.handler);

            overlay.ShowDialog();

        }

        #region List manager

        private IList<WinInterface> GetListOfItf(WinInterface itf)
        {
            if (ListNotAssing.Contains(itf))
                return ListNotAssing;
            if (ListBackground.Contains(itf))
                return ListBackground;
            if (ListBottom.Contains(itf))
                return ListBottom;
            if (ListForeground.Contains(itf))
                return ListForeground;
            return null; //todo: trow error
        }

        public void MoveItfInList(WinInterface itf, IList<WinInterface> fromList, IList<WinInterface> toList)
        {
            fromList.Remove(itf);
            toList.Add(itf);
        }

        public void MoveItfBetweenItf(WinInterface sourceItf, WinInterface targetItf)
        {
            IList<WinInterface> targetList = GetListOfItf(targetItf);
            RemoveItfInList(sourceItf);
            int i = targetList.IndexOf(targetItf);
            targetList.Insert(i, sourceItf);
        }

        public void RemoveItfInList(WinInterface itf)
        {
            ListNotAssing.Remove(itf);
            ListBackground.Remove(itf);
            ListBottom.Remove(itf);
            ListForeground.Remove(itf);
        }

        public void AddItfToList(WinInterface itf, IList<WinInterface> aList)
        {
            aList.Add(itf);
        }

        public IList<WinInterface> ListNotAssing { get; private set; }
        public IList<WinInterface> ListBackground { get; private set; }
        public IList<WinInterface> ListBottom { get; private set; }
        public IList<WinInterface> ListForeground { get; private set; }

        #endregion
        public IList<Window> Overlays { get; set; }

        public Config config { get; private set; }
        
    }
}
