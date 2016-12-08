using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace shipTest
{
    class LevelOne
    {
        List<Ennemy> enemy;
        Canvas myCanvas;
        bool nextStep = true;
        double angle = 0;
        double pos = 50;
        bool finish = false;

        public LevelOne(Canvas c)
        {
            myCanvas = c;

        }

        public List<Ennemy> run()
        {

            List<Ennemy> enemy = new List<Ennemy>();
            enemy = normalGame(enemy, myCanvas);

            return enemy;
        }

        public List<Ennemy> updateGame(List<Ennemy> alien)
        {

            if (alien.Count != 0 && !nextStep)
                alien = updateRain(alien);
            else
                nextStep = true;

            if (nextStep)
            {
                if (alien.Count == 0 && !nextStep)
                    alien = normalGame(alien, myCanvas);

                alien = updateNextStep(alien);
            }


            return alien;
        }

        private List<Ennemy> updateNextStep(List<Ennemy> list)
        {
            double spaceBetween = 20;

            for (int i = 0; i < list.Count; i++)
            {

                if (list.ElementAt(i) is BlueAlien)
                {
                    /*Canvas.SetLeft(list.ElementAt(i).getEnemy(),
                        (Canvas.GetLeft(list.ElementAt(i).getEnemy())));
                    Canvas.SetTop(list.ElementAt(i).getEnemy(),
                        (Canvas.GetTop(list.ElementAt(i).getEnemy())) - (Math.Sin(angle -= 0.08) * 50));*/

                    Canvas.SetLeft(list.ElementAt(0).getEnemy(), pos += 2);
                    Canvas.SetTop(list.ElementAt(0).getEnemy(), ((200) + (Math.Sin(angle += 0.06) * 100)));

                    if (pos > myCanvas.Width / 0.5)
                    {
                        Console.WriteLine("Angle: " + angle + "\tpos: " + pos + "\tGetTop: " +
                            Canvas.GetTop(list.ElementAt(0).getEnemy()) + "\tGetLeft: " + Canvas.GetLeft(list.ElementAt(0).getEnemy()));
                        pos = 0;
                        angle = 0;
                    }
                }
            }

            return list;
        }

        private List<Ennemy> updateRain(List<Ennemy> list)
        {
            foreach (Ennemy rec in list)
                if (Canvas.GetTop(rec.getEnemy()) > 1000)
                    Canvas.SetTop(rec.getEnemy(), 0);
                else
                    Canvas.SetTop(rec.getEnemy(), Canvas.GetTop(rec.getEnemy()) + 2);

            if (list.Count == 0) finish = true;

            return list;
        }

        public bool finishGame()
        {
            return finish;
        }

        private List<Ennemy> normalGame(List<Ennemy> enemyList, Canvas myCanvas)
        {
            enemyList = new List<Ennemy>(40);
            myCanvas.Width = myCanvas.Width;
            myCanvas.Height = myCanvas.Height;
            int numEnnemy = 0;
            double spaceBetween = myCanvas.Width / 2 - 250;
            double height = myCanvas.Height / 5;

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
