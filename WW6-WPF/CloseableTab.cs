using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WinWam6
{
    class CloseableTab : TabItem
    {
        //Constructor
        public CloseableTab()
        {
            var myHeader = new CloseableTabHeader();
            this.Header = myHeader;
            //this.MaxWidth = 150;

            //Events to control the coloring on MouseOver, etc
            myHeader.button_close.MouseEnter += new MouseEventHandler(button_close_MouseEnter);
            myHeader.button_close.MouseLeave += new MouseEventHandler(button_close_MouseLeave);
            myHeader.button_close.Click += new RoutedEventHandler(button_close_Click);
        }

        public string Title
        {
            set
            {
                ((CloseableTabHeader)this.Header).label_TabTitle.Content = value;
            }
        }

        public BitmapImage Picture
        {
            set
            {
                ((CloseableTabHeader)this.Header).tabImage.Source = value;
            }

        }

        // Button MouseEnter - When the mouse is over the button - change color to Red
        void button_close_MouseEnter(object sender, MouseEventArgs e)
        {
            ((CloseableTabHeader)this.Header).button_close.Foreground = Brushes.Red;
        }
        // Button MouseLeave - When mouse is no longer over button - change color back to black
        void button_close_MouseLeave(object sender, MouseEventArgs e)
        {
            ((CloseableTabHeader)this.Header).button_close.Foreground = Brushes.Black;
        }
        // Button Close Click - Remove the Tab - (or raise
        // an event indicating a "CloseTab" event has occurred)
        void button_close_Click(object sender, RoutedEventArgs e)
        {
            ((TabControl)this.Parent).Items.Remove(this);
        }

        public void Animate()
        {
            DoubleAnimation fadeInAnimation = new DoubleAnimation();
            fadeInAnimation.To = 1;
            fadeInAnimation.From = 0;
            fadeInAnimation.Duration = TimeSpan.FromSeconds(0.5);
            this.BeginAnimation(CloseableTab.OpacityProperty, fadeInAnimation);
        }
       
        
    }
}
