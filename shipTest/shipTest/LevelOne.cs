using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace shipTest
{
    class LevelOne : ILevel
    {
        List<Ennemy> enemy;
        public LevelOne()
        {


        }

        public double Height { get; private set; }
        public double Width { get; private set; }

        public List<Ennemy> run(Canvas myCanvas)
        {

            List<Ennemy> enemy = new List<Ennemy>();
            enemy = normalGame(enemy, myCanvas);

            return enemy;
        }

        public List<Ennemy> updateGame(List<Ennemy> enemy)
        {
            enemy = updateRain(enemy);

            return enemy;
        }

        private List<Ennemy> updateRain(List<Ennemy> list)
        {
            foreach (Ennemy rec in list)
                if (Canvas.GetTop(rec.getEnemy()) > 1000)
                    Canvas.SetTop(rec.getEnemy(), 0);
                else
                    Canvas.SetTop(rec.getEnemy(), Canvas.GetTop(rec.getEnemy()) + 2);

            return list;
        }

        private List<Ennemy> normalGame(List<Ennemy> enemyList, Canvas myCanvas)
        {
            enemyList = new List<Ennemy>(40);
            myCanvas.Width = myCanvas.Width;
            myCanvas.Height = myCanvas.Height;
            int numEnnemy = 0;
            double spaceBetween = myCanvas.Width / 2 - 250;
            double height = myCanvas.Height / 2.5;

            for (int i = 0; i < 40; i++)
            {

                if (i < 20)
                { //TWO LINES OF BLUE
                    enemyList.Add(new BlueAlien());


                    myCanvas.Children.Add(enemyList.ElementAt(i).getEnemy());


                    if (numEnnemy < 10)
                    {
                        Canvas.SetTop(enemyList.ElementAt(i).getEnemy(), height);
                        Canvas.SetLeft(enemyList.ElementAt(i).getEnemy(), spaceBetween += 25 + 15);
                        numEnnemy++;
                    }
                    else //SECOND ROW
                    {

                        if (numEnnemy == 10)
                            spaceBetween = myCanvas.Width / 2 - 250;


                        Canvas.SetTop(enemyList.ElementAt(i).getEnemy(), height - 40);
                        Canvas.SetLeft(enemyList.ElementAt(i).getEnemy(), spaceBetween += 25 + 15);
                        numEnnemy++;
                    }

                }

                //Reset the number of ennemy for the new row (16)
                if (numEnnemy == 20 && i == 20)
                {
                    numEnnemy = 0;
                    spaceBetween = myCanvas.Width / 2 - 210;
                    height -= 85;
                }



                if (i >= 20 && i < 36)
                { //TWO LINES OF RED

                    enemyList.Add(new RedAlien());
                    myCanvas.Children.Add(enemyList.ElementAt(i).getEnemy());


                    if (numEnnemy < 8)
                    {
                        Canvas.SetTop(enemyList.ElementAt(i).getEnemy(), height);
                        Canvas.SetLeft(enemyList.ElementAt(i).getEnemy(), spaceBetween += 25 + 15);
                        numEnnemy++;
                    }
                    else //SECOND ROW
                    {

                        if (numEnnemy == 8)
                            spaceBetween = myCanvas.Width / 2 - 210;


                        Canvas.SetTop(enemyList.ElementAt(i).getEnemy(), height - 40);
                        Canvas.SetLeft(enemyList.ElementAt(i).getEnemy(), spaceBetween += 25 + 15);
                        numEnnemy++;
                    }
                }

                //Reset the number of ennemy for the new row (16)

                if (numEnnemy == 16 && i == 36)
                {

                    numEnnemy = 0;
                    spaceBetween = myCanvas.Width / 2 - 130;
                    height -= 85;
                }

                if (i >= 36)
                { //4 GREEN
                    //Console.WriteLine(i);
                    enemyList.Add(new GreenAlien());
                    myCanvas.Children.Add(enemyList.ElementAt(i).getEnemy());

                    if (numEnnemy < 4)
                    {
                        Canvas.SetTop(enemyList.ElementAt(i).getEnemy(), height);
                        Canvas.SetLeft(enemyList.ElementAt(i).getEnemy(), spaceBetween += 25 + 15);
                        numEnnemy++;
                    }
                }
            }
            return enemyList;
        }
    }
}
