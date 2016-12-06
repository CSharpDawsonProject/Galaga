using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace shipTest
{
    partial class MainWindow
    {
        bool play = false;

        private bool mainMenu()
        {
            scoreLb.Visibility = Visibility.Hidden;
            ship.Visibility = Visibility.Hidden;
            bgImg.Visibility = Visibility.Hidden;

            resize();
            setBackGround();

            return play;
        }

        private void resize()
        {
            this.Height = SystemParameters.VirtualScreenHeight /2;
            myCanvas.Height = this.Height;
            this.Width = SystemParameters.VirtualScreenWidth / 2.5;
            myCanvas.Width = this.Width;
        }

        private void setBackGround()
        {
            Canvas.SetZIndex(menuBg, -1);
            Canvas.SetTop(menuBg,0);
            Canvas.SetLeft(menuBg, -6);
            menuBg.Height = this.Height;
            menuBg.Width = this.Width;
            
        }


    }
}
