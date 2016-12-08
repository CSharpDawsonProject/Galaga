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
  

    abstract class Ennemy
    {
        private List<Rectangle> bullet = new List<Rectangle>();
        private int explosionTime = 0;
        abstract public void updateSprite();

        abstract public int collide();

        abstract public int score();

        abstract public Rectangle getEnemy();

        abstract public int getHealth();

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
    }
}
