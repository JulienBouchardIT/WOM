using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WOM.W32;

namespace WOM
{
    public enum Direction{
        up,
        down,
        left,
        right
    }

    public enum Size
    {
        PlusWidth,
        PlusHeight,
        MinusWidth,
        MinusHeight
    }

    public class WinInterface
    {
        public WinInterface(System.Diagnostics.Process process)
        {
            this.process = process;
            this.id = process.Id;
            this.name = process.ProcessName;
            this.title = process.MainWindowTitle;
            this.fix = false;
            this.handler = process.MainWindowHandle;
            this.isIcon = true;
        }

        public WinInterface()
        {
            this.id = 0;
            this.name = "DESKTOP ICONS";
            this.title = "DESKTOP ICONS";
            this.fix = true;
            this.isIcon = false;
        }

        public void Move(Direction dir, int nPixel)
        {
            Rect rect;
            W32.GetWindowRect(handler, out rect);
            int w = rect.right - rect.left;
            int h = rect.bottom - rect.top;
            switch (dir)
            {
                case Direction.up:
                    rect.bottom += nPixel;
                    rect.top -= nPixel;
                    break;
                case Direction.down:
                    rect.bottom -= nPixel;
                    rect.top += nPixel;
                    break;
                case Direction.left:
                    rect.left -= nPixel;
                    rect.right += nPixel;
                    break;
                case Direction.right:
                    rect.left += nPixel;
                    rect.right -= nPixel;
                    break;
                default:
                    break;
            }

            W32.MoveWindow(handler, rect.left, rect.top, w, h, true);
        }

        public void Resize(Size size, int nPixel)
        {
            Rect rect;
            W32.GetWindowRect(handler, out rect);
            int w = rect.right - rect.left;
            int h = rect.bottom - rect.top;
            switch (size)
            {
                case Size.PlusHeight:
                    h += nPixel;
                    break;
                case Size.PlusWidth:
                    w += nPixel;
                    break;
                case Size.MinusHeight:
                    h -= nPixel;
                    break;
                case Size.MinusWidth:
                    w -= nPixel;
                    break;
                default:
                    break;
            }
            
            W32.MoveWindow(handler, rect.left, rect.top, w, h, true);
        }

        public void addAsChild(IntPtr child)
        {
            W32.SetParent(child, this.handler);
        }

        public void setForeground()
        {
            Console.WriteLine("setForeground "+this.name);
            W32.SetForegroundWindow(this.handler);
        }

        public void Kill()
        {
            this.process.Kill();
        }

        public bool isIcon { get; }
        public int id { get; }
        public string name { get; }
        public string title { get; }
        public bool fix { get; set; }
        public IntPtr handler { get; }

        private System.Diagnostics.Process process { get; set; }
    }
}
