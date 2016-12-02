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
    /**
     * An Ennemy can shoot move life death points
     */ 

    abstract class Ennemy
    {
        private List<Rectangle> bullet = new List<Rectangle>();
        private int explosionTime = 0;
        abstract public void updateSprite();

        abstract public int collide();

        abstract public int score();

        abstract public Rectangle getEnemy();



        public void shoot(Canvas myCanvas, Ennemy obj)
        {

            bullet.Add(new Rectangle());

            bullet.ElementAt(bullet.Count - 1).Fill = new ImageBrush{ImageSource = new BitmapImage(new Uri(monsterPicPath("enemyBullet.png"),UriKind.Absolute))};

            bullet.ElementAt(bullet.Count - 1).Height = 25;
            bullet.ElementAt(bullet.Count - 1).Width = 22;

            Canvas.SetTop(bullet.ElementAt(bullet.Count - 1), Canvas.GetTop(obj.getEnemy()));
            Canvas.SetLeft(bullet.ElementAt(bullet.Count - 1), Canvas.GetLeft(obj.getEnemy()));
            myCanvas.Children.Add(bullet.ElementAt(bullet.Count - 1));
        }     

        public void updateShoot(Canvas myCanvas, Image ship)
        {
            for (int i = 0; i < bullet.Count; i++)
            {
                Canvas.SetTop(bullet.ElementAt(i), Canvas.GetTop(bullet.ElementAt(i)) + 5);

                if (Canvas.GetTop(bullet.ElementAt(i)) > myCanvas.Height)
                {
                    myCanvas.Children.RemoveAt(i);
                    bullet.RemoveAt(i);
                }

                if (bullet.Count != 0)
                    if (collision(myCanvas, ship, bullet))
                    {
                        
                    }
            }
        }

        private static Point getLocation(Rectangle obj)
        {
            return new Point(Canvas.GetLeft(obj), Canvas.GetTop(obj));
        }

        private static Point getLocation(Image obj)
        {
            return new Point(Canvas.GetLeft(obj), Canvas.GetTop(obj));
        }


        /**
         * Return the path to the picture
         * The picture has to be specified
         */
        public string monsterPicPath(string creature)
        {
            string path;

            path = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);

            path = path.Substring(0, path.LastIndexOf("\\"));
            path = path.Substring(0, path.LastIndexOf("\\"));
            path = path.Substring(path.IndexOf(":") + 2);
            path = System.IO.Path.Combine(path, ("Ressources\\" + creature));
            return path;
        }

        public int explosion(Canvas myCanvas, Rectangle rec)
        {
            Rectangle explose = new Rectangle();
            explose.Height = 30;
            explose.Width = 30;

            myCanvas.Children.Add(explose);
            Canvas.SetTop(explose, Canvas.GetTop(rec));
            Canvas.SetLeft(explose, Canvas.GetLeft(rec));
            Canvas.SetZIndex(explose, 2);
            if(explosionTime == 0)
            { 
                explose.Fill = new ImageBrush { ImageSource = 
                                new BitmapImage(new Uri(monsterPicPath("explosion1.bmp"), 
                                                                        UriKind.Absolute)) };
                explosionTime++;
            }      

            if(explosionTime == 1)
            { 
                explose.Fill = new ImageBrush { ImageSource = 
                                new BitmapImage(new Uri(monsterPicPath("explosion2.bmp"), 
                                                                        UriKind.Absolute)) };
                explosionTime++;
           }

            if(explosionTime == 2) 
            { 
                explose.Fill = new ImageBrush { ImageSource = 
                                new BitmapImage(new Uri(monsterPicPath("explosion3.bmp"), 
                                                                        UriKind.Absolute)) };
                explosionTime++;
            }

            if(explosionTime == 3)
            { 
                explose.Fill = new ImageBrush { ImageSource = 
                                new BitmapImage(new Uri(monsterPicPath("explosion4.bmp"), 
                                                                        UriKind.Absolute)) };
                explosionTime++;
            }


            if(explosionTime == 4)
            { 
                explose.Fill = new ImageBrush { ImageSource = 
                                new BitmapImage(new Uri(monsterPicPath("explosion5.bmp"), 
                                                                        UriKind.Absolute)) };
                explosionTime++;
                explosionTime = 0;

                return 4;
            }
           /* myCanvas.Children.Remove(explose);
            explose = null;*/

            return explosionTime;
        }

        private bool collision(Canvas myCanvas, Image ship, List<Rectangle> list)
        {

            bool touch = false;

            for (int i = 0; i < list.Count && !touch; i++)
            {

                if (getLocation(list.ElementAt(i)).X < (getLocation(ship).X + ship.ActualWidth) &&
                   (getLocation(list.ElementAt(i)).X + list.ElementAt(i).ActualWidth) > getLocation(ship).X &&
                   getLocation(list.ElementAt(i)).Y < (getLocation(ship).Y + ship.ActualHeight) &&
                   (list.ElementAt(i).ActualHeight + getLocation(list.ElementAt(i)).Y) > getLocation(ship).Y)
                {
                    myCanvas.Children.RemoveAt(i);
                    list.RemoveAt(i);
                    touch = true;
                    //myCanvas.Children.Remove(ship);
                    Console.WriteLine("SHIP TOUCHED");
                }
            }
            return touch;
        }

    }
}
