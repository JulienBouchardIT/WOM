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
    public class ListBoxFactory
    {

        public static void InitAllListBox(ListBox listBox0, ListBox listBox1, ListBox listBox2, ListBox listBox3,
            MouseEventHandler ListBox_PreviewMouseMove, MouseButtonEventHandler ListBoxItem_PreviewMouseLeftButtonDown, DragEventHandler ListBoxItem_Drop,
            MouseButtonEventHandler ListBox_PreviewMouseLeftButtonDown, DragEventHandler ListBox_Drop)
        {
            InitListBox(listBox0, ListBox_PreviewMouseMove, ListBoxItem_PreviewMouseLeftButtonDown, ListBoxItem_Drop, ListBox_PreviewMouseLeftButtonDown, ListBox_Drop);
            InitListBox(listBox1, ListBox_PreviewMouseMove, ListBoxItem_PreviewMouseLeftButtonDown, ListBoxItem_Drop, ListBox_PreviewMouseLeftButtonDown, ListBox_Drop);
            InitListBox(listBox2, ListBox_PreviewMouseMove, ListBoxItem_PreviewMouseLeftButtonDown, ListBoxItem_Drop, ListBox_PreviewMouseLeftButtonDown, ListBox_Drop);
            InitListBox(listBox3, ListBox_PreviewMouseMove, ListBoxItem_PreviewMouseLeftButtonDown, ListBoxItem_Drop, ListBox_PreviewMouseLeftButtonDown, ListBox_Drop);

        }

        private static void InitListBox(ListBox listBox,
            MouseEventHandler ListBox_PreviewMouseMove, MouseButtonEventHandler ListBoxItem_PreviewMouseLeftButtonDown, DragEventHandler ListBoxItem_Drop,
            MouseButtonEventHandler ListBox_PreviewMouseLeftButtonDown, DragEventHandler ListBox_Drop)
        {

            listBox.PreviewMouseMove += ListBox_PreviewMouseMove;

            var styleItem = new Style(typeof(ListBoxItem));
            styleItem.Setters.Add(new Setter(ListBoxItem.AllowDropProperty, true));
            styleItem.Setters.Add(
                new EventSetter(
                    ListBoxItem.PreviewMouseLeftButtonDownEvent, ListBoxItem_PreviewMouseLeftButtonDown));
            styleItem.Setters.Add(
                    new EventSetter(
                        ListBoxItem.DropEvent,
                        new DragEventHandler(ListBoxItem_Drop)));
            listBox.ItemContainerStyle = styleItem;

            var styleBox = new Style(typeof(ListBox));
            styleBox.Setters.Add(new Setter(ListBox.AllowDropProperty, true));
            styleBox.Setters.Add(
                new EventSetter(
                    ListBox.PreviewMouseLeftButtonDownEvent, ListBox_PreviewMouseLeftButtonDown));
            styleBox.Setters.Add(
                    new EventSetter(
                        ListBox.DropEvent,
                        new DragEventHandler(ListBox_Drop)));
            listBox.Style = styleBox;

            listBox.Items.Refresh();
        }

    }
}
