using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace shipTest
{
    partial class MainWindow
    {
        int speed = 2;
        int part = 0;

        private void test()
        {
            part++;

            if (part == 1)
            {
                enemyList = partA(enemyList);
                speed = 2;
            }

            if(part == 2)
            {
                enemyList = partB(enemyList);
            }

        }

        private List<Ennemy> partB(List<Ennemy> list)
        {
            list = new List<Ennemy>();
            int left = -50;
            int enemyPossible = (int)myCanvas.Width / 50;
            speed = 3;

            for (int i =0; i < (enemyPossible-2); i++)
            {
                if(i < 5)
                {
                    list.Add((new GreenAlien()));
                    Canvas.SetTop(list.ElementAt(i).getEnemy(), 0);
                    Canvas.SetLeft(list.ElementAt(i).getEnemy(), left += 50);
                    myCanvas.Children.Add(list.ElementAt(i).getEnemy());
                }

                if (i == 5) left += (50*2);

                if(i >= 5)
                {
                    list.Add((new GreenAlien()));
                    Canvas.SetTop(list.ElementAt(i).getEnemy(), 0);
                    Canvas.SetLeft(list.ElementAt(i).getEnemy(), left += 50);
                    myCanvas.Children.Add(list.ElementAt(i).getEnemy());
                }
            }

            return list;
        }

        private List<Ennemy> partB2(List<Ennemy> list)
        {

            list = new List<Ennemy>();
            int left = -50;
            int enemyPossible = (int)myCanvas.Width / 50;
            speed = 3;

            for(int i =0; i <enemyPossible; i++)
            {

            }

            return list;
        }

        private List<Ennemy> partA(List<Ennemy> enemyList)
        {
            enemyList = new List<Ennemy>();
            int enemyPossible = (int)myCanvas.Width / 50;
            Console.WriteLine(enemyPossible + "\tx3: " + enemyPossible*3);
            int test = enemyPossible * 3;
            int left = -50;
            int line = 1;

            for (int i = 0; i < test; i++)
            {
                if (i == enemyPossible)
                {
                    line++;
                    left = -50;
                }

                if (line == 1)
                {
                    enemyList.Add((new BlueAlien()));
                    Canvas.SetTop(enemyList.ElementAt(i).getEnemy(), 0);
                    Canvas.SetLeft(enemyList.ElementAt(i).getEnemy(), left += 50);
                    myCanvas.Children.Add(enemyList.ElementAt(i).getEnemy());
                }

                if (i == enemyPossible*2)
                {
                    line++;
                    left = -50;
                }

                if (line == 2)
                {                    
                    enemyList.Add((new BlueAlien()));
                    Canvas.SetTop(enemyList.ElementAt(i).getEnemy(), 40);
                    Canvas.SetLeft(enemyList.ElementAt(i).getEnemy(), left += 50);
                    myCanvas.Children.Add(enemyList.ElementAt(i).getEnemy());
                    
                }

                if (line == 3)
                {
                    enemyList.Add((new BlueAlien()));
                    Canvas.SetTop(enemyList.ElementAt(i).getEnemy(), 80);
                    Canvas.SetLeft(enemyList.ElementAt(i).getEnemy(), left += 50);
                    myCanvas.Children.Add(enemyList.ElementAt(i).getEnemy());

                }

            }

            

            return enemyList;
        }

        private List<Ennemy> updateNextStep(List<Ennemy> list)
        {
            bool once = false;       

            foreach (Ennemy rec in list)
                if (Canvas.GetTop(rec.getEnemy()) > 1000)
                {
                    Canvas.SetTop(rec.getEnemy(), 0);

                    
                    if (!once) {

                        enemyList = partB2(enemyList);

                        if(speed < 10 && part == 1) speed++;
                        
                        once = true;
                    }

                    
                }
                else
                    Canvas.SetTop(rec.getEnemy(), Canvas.GetTop(rec.getEnemy()) + speed);

            
            return list;
        }

    }
    }
