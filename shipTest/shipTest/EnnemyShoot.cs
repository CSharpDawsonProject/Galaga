using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace shipTest
{
    class EnnemyShoot
    {
        Point location;
        Canvas myCanvas;
        double speed;
        double height;
        double width;
        List<Rectangle> bulletList = new List<Rectangle>();


        public EnnemyShoot(Point p, Canvas c)
        {
            location = p;
            myCanvas = c;
            this.speed = 5;
            height = 25;
            width = 22;
        }
        public EnnemyShoot(Point p, Canvas c, double speed, double h, double w)
        {
            location = p;
            myCanvas = c;
            this.speed = speed;
            height = h;
            width = w;
        }


        public void shoot()
        {

            bulletList.Add(new Rectangle());

            bulletList.ElementAt(bulletList.Count - 1).Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(monsterPicPath("enemyBullet.bmp"), UriKind.Absolute)) };

            //HEIGHT/WIDTH
            bulletList.ElementAt(bulletList.Count - 1).Height = height;
            bulletList.ElementAt(bulletList.Count - 1).Width = width;

            Canvas.SetTop(bulletList.ElementAt(bulletList.Count - 1), location.Y);
            Canvas.SetLeft(bulletList.ElementAt(bulletList.Count - 1), location.X);
            myCanvas.Children.Add(bulletList.ElementAt(bulletList.Count - 1));
        }

        public void updateShoot(Canvas myCanvas, Image ship)
        {
            for (int i = 0; i < bulletList.Count; i++)
            {
                //SPEED
                Canvas.SetTop(bulletList.ElementAt(i), Canvas.GetTop(bulletList.ElementAt(i)) + speed);

                if (Canvas.GetTop(bulletList.ElementAt(i)) > myCanvas.Height)
                {
                    myCanvas.Children.RemoveAt(i);
                    bulletList.RemoveAt(i);
                }

                if (bulletList.Count != 0)
                    if (collision(ship))
                    {

                    }
            }
        }



        private string monsterPicPath(string creature)
        {
            string path;

            path = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);

            path = path.Substring(0, path.LastIndexOf("\\"));
            path = path.Substring(0, path.LastIndexOf("\\"));
            path = path.Substring(path.IndexOf(":") + 2);
            path = System.IO.Path.Combine(path, ("Ressources\\" + creature));
            return path;
        }

        private bool collision(Image ship)
        {

            bool touch = false;

            for (int i = 0; i < bulletList.Count && !touch; i++)
            {

                if (getLocation(bulletList.ElementAt(i)).X < (getLocation(ship).X + ship.ActualWidth) &&
                   (getLocation(bulletList.ElementAt(i)).X + bulletList.ElementAt(i).ActualWidth) > getLocation(ship).X &&
                   getLocation(bulletList.ElementAt(i)).Y < (getLocation(ship).Y + ship.ActualHeight) &&
                   (bulletList.ElementAt(i).ActualHeight + getLocation(bulletList.ElementAt(i)).Y) > getLocation(ship).Y)
                {
                    myCanvas.Children.RemoveAt(i);
                    bulletList.RemoveAt(i);
                    touch = true;
                    //myCanvas.Children.Remove(ship);
                    Console.WriteLine("SHIP TOUCHED");
                }
            }
            return touch;
        }

        private static Point getLocation(Rectangle obj)
        {
            return new Point(Canvas.GetLeft(obj), Canvas.GetTop(obj));
        }
        private static Point getLocation(Image obj)
        {
            return new Point(Canvas.GetLeft(obj), Canvas.GetTop(obj));
        }

    }
}
