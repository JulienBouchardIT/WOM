using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Diagnostics;
using System.Globalization;

namespace WOM
{

    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string WIKI_URL = "https://github.com/JulienBouchardIT/WOM/wiki";

        public MainWindow()
        {
            this.wom = WindowOrderManager.getWOM();
            InitializeComponent();
            Init_ListBox();
            Init_Tasktray();
            this.Topmost = true;
        }

        private void Window_Drag(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        #region Tasktray
        private System.Windows.Forms.NotifyIcon tasktrayProcess;
        private void Init_Tasktray()
        {
            tasktrayProcess = new System.Windows.Forms.NotifyIcon();
            tasktrayProcess.BalloonTipTitle = "WOM";
            tasktrayProcess.Text = "Open WOM config";
            tasktrayProcess.Icon = new System.Drawing.Icon("layer.ico", new System.Drawing.Size(32,32));
            tasktrayProcess.Click += new EventHandler(showConfig);
        }

        void Window_Closing(object sender, CancelEventArgs args)
        {
            args.Cancel = true; // Cancel program closing
            this.Visibility = Visibility.Hidden;
            tasktrayProcess.Visible = true;
        }

        void showConfig(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Visible;
            tasktrayProcess.Visible = false;
        }
        #endregion

        #region Listbox

        private WinInterface GetSelectedItf()
        {
            return (listNotAssing.SelectedItem as WinInterface);
        }

        private Point _dragStartPoint;

            private T FindVisualParent<T>(DependencyObject child)
                where T : DependencyObject
            {
                var parentObject = VisualTreeHelper.GetParent(child);
                if (parentObject == null)
                    return null;
                T parent = parentObject as T;
                if (parent != null)
                    return parent;
                return FindVisualParent<T>(parentObject);
            }

            private void Init_ListBox()
            {

                listNotAssing.ItemsSource = wom.listWinItf;

                listNotAssing.PreviewMouseMove += ListBox_PreviewMouseMove;

                var style = new Style(typeof(ListBoxItem));
                style.Setters.Add(new Setter(ListBoxItem.AllowDropProperty, true));
                style.Setters.Add(
                    new EventSetter(
                        ListBoxItem.PreviewMouseLeftButtonDownEvent,
                        new MouseButtonEventHandler(ListBoxItem_PreviewMouseLeftButtonDown)));
                style.Setters.Add(
                        new EventSetter(
                            ListBoxItem.DropEvent,
                            new DragEventHandler(ListBoxItem_Drop)));
                listNotAssing.ItemContainerStyle = style;
                listNotAssing.Items.Refresh();
            }


            private void ListBox_PreviewMouseMove(object sender, MouseEventArgs e)
            {
                Point point = e.GetPosition(null);
                Vector diff = _dragStartPoint - point;
                if (e.LeftButton == MouseButtonState.Pressed &&
                    (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                        Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
                {
                    var lb = sender as ListBox;
                    var lbi = FindVisualParent<ListBoxItem>(((DependencyObject)e.OriginalSource));
                    if (lbi != null)
                    {
                        DragDrop.DoDragDrop(lbi, lbi.DataContext, DragDropEffects.Move);
                    }
                }
            }
            private void ListBoxItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {
                _dragStartPoint = e.GetPosition(null);
            }

            private void ListBoxItem_Drop(object sender, DragEventArgs e)
            {
                if (sender is ListBoxItem)
                {
                    var source = e.Data.GetData(typeof(WinInterface)) as WinInterface;
                    var target = ((ListBoxItem)(sender)).DataContext as WinInterface;

                    int sourceIndex = listNotAssing.Items.IndexOf(source);
                    int targetIndex = listNotAssing.Items.IndexOf(target);

                    Move(source, sourceIndex, targetIndex);
                }
            }

            private void Move(WinInterface source, int sourceIndex, int targetIndex)
            {
                if (sourceIndex < targetIndex)
                {
                    wom.listWinItf.Insert(targetIndex + 1, source);
                    wom.listWinItf.RemoveAt(sourceIndex);
                }
                else
                {
                    int removeIndex = sourceIndex + 1;
                    if (wom.listWinItf.Count + 1 > removeIndex)
                    {
                        wom.listWinItf.Insert(targetIndex, source);
                        wom.listWinItf.RemoveAt(removeIndex);
                    }
                }
            }
            #endregion  

        public void CloseButton(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            tasktrayProcess.Visible = true;
        }

        public void OpenWiki(object sender, RoutedEventArgs e)
        {
            Process.Start(WIKI_URL);
        }

        public void Actualize(object sender, RoutedEventArgs e)
        {
            Init_ListBox();
        }

        public void Apply(object sender, RoutedEventArgs e)
        {
            wom.Apply();

            this.Visibility = Visibility.Hidden;
            tasktrayProcess.Visible = true;
        }

        public void Reset(object sender, RoutedEventArgs e)
        {
            //todo: Ask for confirmation (this will suppress every config and kill any app between desktop and icon)
            
        }

        public void Top(object sender, RoutedEventArgs e)
        {
            //todo:make temporary. Hold down button
            if (GetSelectedItf() != null)
                GetSelectedItf().setForeground();
        }

        public void Scale(object sender, RoutedEventArgs e)
        {
            if (GetSelectedItf() != null)
            {
                this.Visibility = Visibility.Hidden;
                wom.Scale(GetSelectedItf());
                this.Visibility = Visibility.Visible;
            }
        }

        public void Move(object sender, RoutedEventArgs e)
        {
            if (GetSelectedItf() != null)
            {
                this.Visibility = Visibility.Hidden;
                wom.Move(GetSelectedItf());
                this.Visibility = Visibility.Visible;
            }
        }

        public void Kill(object sender, RoutedEventArgs e)
        {
            if (GetSelectedItf() != null)
            {
                GetSelectedItf().Kill();
                Init_ListBox(); // Actualize
            }
        }

        public void SwitchDarkMode(object sender, RoutedEventArgs e)
        {

        }

        public void SwitchLightMode(object sender, RoutedEventArgs e)
        {

        }

        private WindowOrderManager wom { get; set; }

    }
}