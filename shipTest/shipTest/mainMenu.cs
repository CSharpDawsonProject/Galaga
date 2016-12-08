using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            lifeImg.Visibility = Visibility.Hidden;
            gameOverImg.Visibility = Visibility.Hidden;
            pauseImg.Visibility = Visibility.Hidden;

            if (!start.IsVisible)
            {
                start.Visibility = Visibility.Visible;
                scores.Visibility = Visibility.Visible;
                credit.Visibility = Visibility.Visible;
                exit.Visibility = Visibility.Visible;
            }

            KeyDown += keyMove;




            resize();
            setBackGround();




            return play;
        }

        private void keyMove(object sender, KeyEventArgs e)
        {
            switch (e.Key.ToString())
            {
                case "down":
                    start.Focus();
                    break;


            }
        }

        private void resize()
        {
            this.Height = SystemParameters.VirtualScreenHeight / 2;
            myCanvas.Height = this.Height;
            this.Width = SystemParameters.VirtualScreenWidth / 2.5;
            myCanvas.Width = this.Width;

            Canvas.SetLeft(start, myCanvas.Width / 6);
            Canvas.SetLeft(scores, myCanvas.Width / 6);
            Canvas.SetLeft(credit, myCanvas.Width / 6);
            Canvas.SetLeft(exit, myCanvas.Width / 6);
        }

        private void setBackGround()
        {
            menuBg.Visibility = Visibility.Visible;
            Canvas.SetZIndex(menuBg, -1);
            Canvas.SetTop(menuBg, 0);
            Canvas.SetLeft(menuBg, -6);
            menuBg.Height = this.Height;
            menuBg.Width = this.Width;



        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            start.Visibility = Visibility.Hidden;       
            scores.Visibility = Visibility.Hidden;
            credit.Visibility = Visibility.Hidden;
            exit.Visibility = Visibility.Hidden;

            scoreLb.Visibility = Visibility.Visible;
            menuBg.Visibility = Visibility.Hidden;
            bgImg.Visibility = Visibility.Visible;
            ship.Visibility = Visibility.Visible;
            lifeImg.Visibility = Visibility.Visible;
            gameStart();
        }

        private void menuSelection()
        {

        }




    }
}
