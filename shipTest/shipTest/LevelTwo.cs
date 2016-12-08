using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace shipTest
{
    class LevelTwo
    {
        bool finish = false;
        Canvas myCanvas;
        List<Ennemy> enemy;
        public LevelTwo(Canvas c)
        {
            myCanvas = c;

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
            {
                if (Canvas.GetTop(rec.getEnemy()) > 1000)
                {
                    Canvas.SetTop(rec.getEnemy(), 0);


                    //pepe was here

                    if (rec is GreenAlien)
                    {

                        Canvas.SetLeft(rec.getEnemy(), 50);
                        Canvas.SetTop(rec.getEnemy(), ((20) + (Math.Sin(0.06) * 100)));

                    }//pepe out
                }
                else
                    Canvas.SetTop(rec.getEnemy(), Canvas.GetTop(rec.getEnemy()) + 2);
            }
            return list;
        }
        public bool finishGame()
        {
            return finish;
        }

        private List<Ennemy> normalGame(List<Ennemy> enemyList, Canvas myCanvas)
        {
            enemyList = new List<Ennemy>(30);
            myCanvas.Width = myCanvas.Width;
            myCanvas.Height = myCanvas.Height;

            int numEnnemy = 0;
            double spaceBetween = myCanvas.Width / 2 - 250;
            bool once = true;
            double height = myCanvas.Height / 3.0;

            for (int i = 0; i < 30; i++)
            {

                if (i < 11)
                { //This will be 2 lines of red
                    enemyList.Add(new RedAlien());


                    myCanvas.Children.Add(enemyList.ElementAt(i).getEnemy());


                    if (numEnnemy < 5)
                    {
                        Canvas.SetTop(enemyList.ElementAt(i).getEnemy(), height);
                        Canvas.SetLeft(enemyList.ElementAt(i).getEnemy(), spaceBetween += 25 + 55);
                        numEnnemy++;
                    }
                    else //second row of 6 reds
                    {

                        if (numEnnemy == 5)
                            spaceBetween = myCanvas.Width / 2 - 250;


                        Canvas.SetTop(enemyList.ElementAt(i).getEnemy(), height - 40);
                        Canvas.SetLeft(enemyList.ElementAt(i).getEnemy(), spaceBetween += 25 + 45);
                        numEnnemy++;
                    }

                }


                if (numEnnemy == 11 && i == 11)
                {
                    Console.WriteLine(i);
                    numEnnemy = 0;
                    spaceBetween = this.Width / 2 - 50;
                    height -= 85;
                }



                if (i >= 11 && i < 16)
                { //1 line of greens

                    enemyList.Add(new GreenAlien());

                    myCanvas.Children.Add(enemyList.ElementAt(i).getEnemy());


                    if (numEnnemy < 8)
                    {
                        Canvas.SetTop(enemyList.ElementAt(i).getEnemy(), height);
                        Canvas.SetLeft(enemyList.ElementAt(i).getEnemy(), spaceBetween += 25 + 70);
                        numEnnemy++;
                        Console.WriteLine("NumEn: " + numEnnemy + "\tI: " + i);
                    } //Commenting it out cause there will only be 1 line
                      /* else //SECOND ROW
                       {

                           if (numEnnemy == 8)
                               spaceBetween = this.Width / 2 - 210;


                           Canvas.SetTop(enemyList.ElementAt(i), height - 40);
                           Canvas.SetLeft(enemyList.ElementAt(i), spaceBetween += 25 + 15);
                           numEnnemy++;
                       }*/
                }



                if (numEnnemy == 5 && i == 15)
                {
                    Console.WriteLine(i);
                    numEnnemy = 0;
                    spaceBetween = this.Width / 2 - 30;
                    height -= 40;
                }

                if (i >= 16 && i < 25)
                { //9 yellows in this line

                    enemyList.Add(new BlueAlien());

                    myCanvas.Children.Add(enemyList.ElementAt(i).getEnemy());


                    if (numEnnemy < 9)
                    {
                        Canvas.SetTop(enemyList.ElementAt(i).getEnemy(), height);
                        Canvas.SetLeft(enemyList.ElementAt(i).getEnemy(), spaceBetween += 25 + 30);
                        numEnnemy++;
                    }

                    if (numEnnemy == 9 && i == 24)
                    {
                        Console.WriteLine(i);
                        numEnnemy = 0;
                        spaceBetween = this.Width / 2 - 30;
                        height -= 40;
                        Console.WriteLine("NumEn: " + numEnnemy + "\tI: " + i);
                    }
                }

                if (i >= 25 && i < 30)
                { //1 line of greens

                    enemyList.Add(new GreenAlien());

                    myCanvas.Children.Add(enemyList.ElementAt(i).getEnemy());


                    if (numEnnemy < 6)
                    {
                        Canvas.SetTop(enemyList.ElementAt(i).getEnemy(), height);
                        Canvas.SetLeft(enemyList.ElementAt(i).getEnemy(), spaceBetween += 25 + 65);
                        numEnnemy++;
                        Console.WriteLine("NumEn: " + numEnnemy + "\tI: " + i);
                    }
                    else //SECOND ROW
                    {

                        if (numEnnemy == 1)
                            spaceBetween = this.Width / 2 - 210;


                        Canvas.SetTop(enemyList.ElementAt(i).getEnemy(), height - 40);
                        Canvas.SetLeft(enemyList.ElementAt(i).getEnemy(), spaceBetween += 25 + 15);
                        numEnnemy++;

                    }
                }
            }
            return enemyList;
        }
    }
}


