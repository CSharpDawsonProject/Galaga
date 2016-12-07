using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace shipTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    /**
     * TO DO LIST: 
     * PAUSE MENU
     * SCORE BOARD
     * SAVE SCOREBOARD
     * MAIN MENU
     * PATTERN
     * ENNEMY / BOSS/ SPECIAL PATTERN
     * SOUND EFFECT
     * DEATH ANIMATION
     * LIFE + SCORE WHILE PLAYING
     * DEFINE HOW MUCH POINTS FOR EACH MONSTER / LEVEL
     * DEFINE A LEVEL
     *  
     *  
     * 
     */


    public partial class MainWindow : Window
    {
        static double newLocX;
        //static int newLocY;
        //Image shoot = new Image();
        string currentKey = "";
        DispatcherTimer foward;
        DispatcherTimer update;
        DispatcherTimer explosionTimer;
        bool spaceBut = false;
        private int shootPause = 38;
        private List<Rectangle> bulletList;



        Vector ennemyPath;
        static Vector positionWave;
        int testIndex;
        Rectangle ennemy;
        //int timerInt = 20;

        Rectangle circleEnemy;
        /*double angle =0;
        double angle2 = 0;
        double X = 100;
        double Y = 100;*/
        Rectangle tryPattern;
        //int tp = 50;
        bool collide = false;
        int invulnerable = 0;
        bool pause = false;

        int score = 0;



        List<Ennemy> enemyList = new List<Ennemy>();


        List<int> track = new List<int>();
        List<Rectangle> explosions = new List<Rectangle>();
        List<Point> exploLoc;
        Rectangle pauseScreen = new Rectangle();

        LevelOne lvl1;
        SoundPlayer shootSound;
        SoundPlayer explosionSound;
        //MediaElement musicBG;

        public MainWindow()
        {
            //LevelThree lvl3 = new LevelThree();

            InitializeComponent();


            gameStart();

            

        }

        private void gameStart()
        {
            screenSizeGame();

            lvl1 = new LevelOne(myCanvas);



            exploLoc = new List<Point>();
            //testC = lvl1.run();
            makeItRainLvl();

            circleEnemy = new Rectangle();
            tryPattern = createMovement(circleEnemy);
            circleEnemy = createMovement(circleEnemy);
            ennemy = new Rectangle();
            testIndex = 0;
            //Original position (Middle)
            ennemyPath = new Vector(Canvas.GetLeft(ship), (this.Height / 2));

            initializeMusic();

            initializeVariable();
        }

        /**
         * The method will set the size of the screen
         * based on the actual physical screen and set
         * the background.
         */
        private void screenSizeGame()
        {
            this.Height = SystemParameters.VirtualScreenHeight - 5;
            myCanvas.Height = this.Height;
            this.Width = SystemParameters.VirtualScreenWidth / 2.5;
            myCanvas.Width = this.Width;

            this.Top = this.Height / 250;
            this.Left = this.Width / 2;

            Canvas.SetTop(ship, myCanvas.Height / 1.3);

            Canvas.SetZIndex(bgImg, -1);
            bgImg.Height = this.Height;
            bgImg.Width = this.Width;
        }


        /**
         * The method will initialize variables and
         * all the timers to make the look constructor cleaner
         */
        private void initializeVariable()
        {
            //Start in the middle of the screen
            newLocX = this.Width / 2;

            //Update timer will update the "sprite" of enemies
            update = new DispatcherTimer();
            update.Tick += Update_Tick;
            update.Interval = TimeSpan.FromSeconds(0.5);

            update.Start();

         
            //ExplosionTimer will be initialized, but will start
            //only when a collision happen
            explosionTimer = new DispatcherTimer();
            explosionTimer.Tick += ExplosionTimer_Tick;
            explosionTimer.Interval = TimeSpan.FromSeconds(0.1);

            //Foward is the MAIN timer of that game
            //Foward will run all the methods useful for the game
            //and update his state
            foward = new DispatcherTimer();
            foward.Tick += Foward_Tick;
            foward.Interval = TimeSpan.FromMilliseconds(3.3);

            foward.Start();

            //bulletList is made for the ship only
            bulletList = new List<Rectangle>();

            //If a key is pressed the handler shipMove will run
            KeyDown += shipMove;
        }

        private void initializeMusic()
        {
            explosionSound = new SoundPlayer(soundPath("Grenade-Sound.wav"));

            shootSound = new SoundPlayer(soundPath("Galaga_Firing_Sound.wav"));
        }

        private void ExplosionTimer_Tick(object sender, EventArgs e)
        {
            if (exploLoc.Count != 0)
            {
                for (int i = 0; i < exploLoc.Count; i++)
                {

                    if (track.Count != 0)
                        track[i]++;



                    if (track.ElementAt(i) == 1)
                    {
                        explosions.ElementAt(i).Height = 40;
                        explosions.ElementAt(i).Width = 40;
                        explosions.ElementAt(i).Fill = new ImageBrush
                        {
                            ImageSource =
                                new BitmapImage(new Uri(resourcePath("explosion5.png"),
                                                                        UriKind.Absolute))
                        };

                    }

                    if (track.ElementAt(i) == 2)
                    {
                        explosions.ElementAt(i).Height = 30;
                        explosions.ElementAt(i).Width = 30;
                        explosions.ElementAt(i).Fill = new ImageBrush
                        {
                            ImageSource =
                                new BitmapImage(new Uri(resourcePath("explosion4.png"),
                                                                        UriKind.Absolute))
                        };
                    }

                    if (track.ElementAt(i) == 3)
                    {
                        explosions.ElementAt(i).Height = 20;
                        explosions.ElementAt(i).Width = 20;
                        explosions.ElementAt(i).Fill = new ImageBrush
                        {
                            ImageSource =
                                new BitmapImage(new Uri(resourcePath("explosion3.png"),
                                                                        UriKind.Absolute))
                        };
                    }

                    if (track.ElementAt(i) == 4)
                    {
                        explosions.ElementAt(i).Height = 10;
                        explosions.ElementAt(i).Width = 10;
                        explosions.ElementAt(i).Fill = new ImageBrush
                        {
                            ImageSource =
                                new BitmapImage(new Uri(resourcePath("explosion2.png"),
                                                                        UriKind.Absolute))
                        };

                    }

                    if (track.ElementAt(i) > 4)
                    {
                        myCanvas.Children.Remove(explosions.ElementAt(i));
                        track.RemoveAt(i);
                        exploLoc.RemoveAt(i);
                        explosions.RemoveAt(i);
                        i--;
                    }
                }
            }
            if (track.Count == 0)
                explosionTimer.Stop();
        }

        private void Update_Tick(object sender, EventArgs e)
        {

            for (int i = 0; i < enemyList.Count; i++)
            {
                if (enemyList.ElementAt(i) is BlueAlien)
                    enemyList.ElementAt(i).updateSprite();

                if (enemyList.ElementAt(i) is RedAlien)
                    enemyList.ElementAt(i).updateSprite();

                if (enemyList.ElementAt(i) is GreenAlien)
                    enemyList.ElementAt(i).updateSprite();

            }
        }

        private Rectangle createExplosion(Point loc)
        {
            Rectangle rec = new Rectangle();

            rec.Height = 50;
            rec.Width = 50;
            rec.Fill = new ImageBrush
            {
                ImageSource =
                                new BitmapImage(new Uri(resourcePath("explosion1.png"),
                                                                        UriKind.Absolute))
            };

            Canvas.SetTop(rec, loc.Y);
            Canvas.SetLeft(rec, loc.X);
            myCanvas.Children.Add(rec);


            return rec;
        }

        /**
         * The method will allow the ship to move only
         * left and right side. Also, if the user press
         * space the ship will be able to shoot.
         */
        private void moveAndShoot()
        {
            /*
             * Switch statement will ensure that the ship
             * move but only in X axes
             */
            switch (currentKey)
            {
                case "Left":
                    newLocX -= 3.2;
                    if (newLocX < -6) newLocX = -6;
                    break;

                case "Right":
                    newLocX += 3.2;
                    if (newLocX > myCanvas.Width - 80) newLocX = myCanvas.Width - 80;
                    break;

            }

            //If the spaceBut is set to true you that mean someone click it
            if (spaceBut)
            {
                spaceBut = false;

                //shootPause will ensure that the user do not spam 
                if (shootPause > 20)
                {
                    shootPause = 0;
                    Rectangle bullet = new Rectangle();
                    bullet.Width = 50;
                    bullet.Height = 50;

                    bullet.Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(resourcePath("GalagaRocket.png"), UriKind.Absolute)) };

                    myCanvas.Children.Add(bullet);
                    setLocation(bullet, Canvas.GetTop(ship) - 15, Canvas.GetBottom(ship),
                                            Canvas.GetLeft(ship) + 5, Canvas.GetRight(ship));
                    bulletList.Add(bullet);

                    shootSound.Play();
                }
            }
        }

        /**
         * The method will look if the bullet shooted by
         * mister ship hit an enemy or is outside of our canvas/screen.
         */
        private void updateShooting()
        {

            for (int i = 0; i < bulletList.Count; i++)
            {

                // -5 is the speed of bullets
                Canvas.SetTop(bulletList.ElementAt(i), Canvas.GetTop(bulletList.ElementAt(i)) - 5);

                //Remove bullet which are out of the screen
                if (Canvas.GetTop(bulletList.ElementAt(i)) < -myCanvas.Height / 4.5)
                {

                    myCanvas.Children.Remove(bulletList.ElementAt(i));
                    bulletList.RemoveAt(i);
                }

                if (bulletList.Count != 0)
                {
                    int points;
                    /*
                     * collision method will return -1 for not hit
                     * 0 for hitted, but the enemy has still health
                     * higher then 0 is the enemy is dead and it the score of 
                     * the killed enemy.
                     */
                    points = collision(bulletList.ElementAt(i), enemyList);

                    //collision returned a score so someone is dead
                    if (points > 0)
                    {
                        //change the score label
                        scoreLb.Content = "Score: " + (score += points);

                        myCanvas.Children.Remove(bulletList.ElementAt(i));
                        bulletList.RemoveAt(i);
                    }

                    //0 we hit something, but it not totally dead!
                    if (points == 0)
                    {

                        myCanvas.Children.Remove(bulletList.ElementAt(i));
                        bulletList.RemoveAt(i);
                    }
                }

            }
        }

        private void Foward_Tick(object sender, EventArgs e)
        {

            shootPause++;
            Console.WriteLine(enemyList.Count);
            if (enemyList.Count == 0)
            {
                makeItRainLvl();
                //testC = lvl1.run();
            }

            moveAndShoot();

            updateShooting();



            if (exploLoc.Count != explosions.Count)
            {
                track.Add(0);
                explosions.Add(createExplosion(exploLoc.ElementAt(exploLoc.Count - 1)));

                explosionSound.Play();

                if (explosionTimer.IsEnabled == false)
                    explosionTimer.Start();

            }

            /* if (timerInt > 20)
             {
                 if (testIndex == 0)
                 {
                     ennemy = createMovement(ennemy);
                     testIndex = 1;

                 }
                 else
                 {

                     ennemy = updateSprite(ennemy);
                     testIndex = 0;
                 }
                 timerInt = 0;
             }
             timerInt++;*/


            //DIAGONAL ENNEMY WORKING ~ 70%
            ennemyPath.Normalize();
            Vector perp = new Vector(-ennemyPath.Y, ennemyPath.X);
            float waveAmp = 15.00f;
            float waveAngle = 5 * 3.14f * 2;
            Vector wave = perp * Math.Sin(waveAngle) * waveAmp;
            Vector vel = ennemyPath * 0.5;//speed
            positionWave += vel * 5 + wave;

            //Console.WriteLine("Vec posWave Y: " + positionWave.Y + "\tVec posWave X: " + positionWave.X);
            if (positionWave.Y > this.Height)
                positionWave = vel * 5 + wave;

            if (ennemy != null)
                setLocation(ennemy, positionWave);

            //Canvas.SetLeft(tryPattern, tp += 2);
            //Canvas.SetTop(tryPattern, ((Y + 50) + (Math.Sin(angle += 0.06) * 100)));

            //NEW PATTERN 50% WORKING outOfBound Sir ...
            /*Canvas.SetLeft(ennemyList.ElementAt(5), Canvas.GetLeft(ennemyList.ElementAt(5)) -0.5);
            Canvas.SetTop(ennemyList.ElementAt(5), (Canvas.GetTop(ennemyList.ElementAt(5)) - (Math.Sin(angle2 -= 0.03) * 3)));
            */

            //Console.WriteLine(angle);
            /*if (angle > 13)
            {
                tp = 25;
                angle = 0;
            }*/

            //circleEnemy = createMovement(circleEnemy);

            //CIRCLE PATTERN ~ 80% (Combine wih Diagonal?)
            //Canvas.SetRight(circleEnemy, ( (X +=0.05) + (Math.Cos(angle+=0.03) * 100)));
            //Canvas.SetTop(circleEnemy, ( (Y+50) +(Math.Sin(angle += 0.03) * 100)));

            //Console.WriteLine(circleEnemy.ActualHeight);


            //CIRCLE PATTERN DO NOT TOUCH
            //origin X and y is the center of your circle
            /*X= originX + cos(angle) * radius;
            Y= originY + sin(angle) * radius;*/

            //THEORY ~ half working !!!!!!PLEASE DO NOT ERASE!!!!!!!!
            /*Vector dir = target - position; // direction
            dir.Normalize();
            Vector perp(-dir.y, dir.x ); // perpendicular

            float waveAmp = 0.05f; // adjust if needed
            float waveAngle = elapsedTime * 3.14f * 2; // adjust if needed
            Vector wave = perp * sin(waveAngle) * waveAmp;

            Vector vel = dir * speed;
            position += vel * elaspedTime + wave;*/


            //Console.WriteLine(newLocX);
            setLocation(ship, new Point(newLocX, 0));
            //ennemyList = updateRain(ennemyList);

            if (collide)
                invulnerable++;

            if (collide && invulnerable > 100)
                collide = false;


            if (!collide)
                if (shipCollision(ship, enemyList))
                {
                    collide = true;
                    invulnerable = 0;
                    MessageBox.Show("Only Dumb can die HAHAHA", "NOOB", MessageBoxButton.OK);
                    dead(ship, enemyList);
                    //foward.Stop();
                }

            // testC = updateRain(testC);
            //testC = lvl1.updateGame(testC);
            enemyList = updateNextStep(enemyList);

        }

        //Another meaning for after?
        private void dead(Image ship, List<Ennemy> list)
        {
            myCanvas.Children.Add(ship);

        }

        private bool shipCollision(Image ship, List<Ennemy> list)
        {

            bool touch = false;

            for (int i = 0; i < list.Count && !touch; i++)
            {

                if (getLocation(list.ElementAt(i).getEnemy()).X < (getLocation(ship).X + ship.ActualWidth) &&
                   (getLocation(list.ElementAt(i).getEnemy()).X + list.ElementAt(i).getEnemy().ActualWidth) > getLocation(ship).X &&
                   getLocation(list.ElementAt(i).getEnemy()).Y < (getLocation(ship).Y + ship.ActualHeight) &&
                   (list.ElementAt(i).getEnemy().ActualHeight + getLocation(list.ElementAt(i).getEnemy()).Y) > getLocation(ship).Y)
                {
                    myCanvas.Children.Remove(list.ElementAt(i).getEnemy());
                    Console.WriteLine("\nI HIT YOU");
                    list.Remove(list.ElementAt(i));
                    touch = true;
                    myCanvas.Children.Remove(ship);
                }
            }
            return touch;
        }
        private void shipMove(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                case Key.A:
                    currentKey = "Left";
                    break;

                case Key.Right:
                case Key.D:
                    currentKey = "Right";

                    break;

                case Key.Space:
                    spaceBut = true;
                    break;

                case Key.Down:
                case Key.S:
                    currentKey = "";
                    break;

                case Key.P:
                    if (!pause)
                    {
                        update.Stop();
                        foward.Stop();
                        explosionTimer.Stop();
                        pause = true;
                        pauseWindow(pause);
                    }
                    else
                    {
                        update.Start();
                        foward.Start();
                        explosionTimer.Start();
                        pause = false;
                        unPause();
                    }
                    break;
            }
        }

        private void pauseWindow(bool display)
        {        
            Application.Current.MainWindow.Opacity = .20;
            /*Canvas.SetTop(pauseImg, myCanvas.Height);
            Canvas.SetLeft(pauseImg, myCanvas.Width);   */

        }

        private void unPause()
        {
            Application.Current.MainWindow.Opacity = 1;            


        }
        private void setLocation(Image obj, Point objLocation)
        {
            Canvas.SetLeft(obj, objLocation.X);
            Canvas.SetRight(obj, objLocation.Y);
        }

        private void setLocation(Rectangle obj, double top, double bottom, double left, double right)
        {
            Canvas.SetLeft(obj, left);
            Canvas.SetRight(obj, right);
            Canvas.SetBottom(obj, bottom);
            Canvas.SetTop(obj, top);
        }

        private static Point getLocation(Rectangle obj)
        {
            return new Point(Canvas.GetLeft(obj), Canvas.GetTop(obj));
        }
        private static Point getLocation(Image obj)
        {
            return new Point(Canvas.GetLeft(obj), Canvas.GetTop(obj));
        }

        private static void setLocation(Rectangle rec, Vector vec)
        {
            Canvas.SetLeft(rec, vec.X);
            Canvas.SetTop(rec, vec.Y);
        }

        private string resourcePath(string creature)
        {
            string path;

            path = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);

            path = path.Substring(0, path.LastIndexOf("\\"));
            path = path.Substring(0, path.LastIndexOf("\\"));
            path = path.Substring(path.IndexOf(":") + 2);
            path = System.IO.Path.Combine(path, ("Ressources\\" + creature));
            return path;
        }

        private string soundPath(string music)
        {
            string path;

            path = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);

            path = path.Substring(0, path.LastIndexOf("\\"));
            path = path.Substring(0, path.LastIndexOf("\\"));
            path = path.Substring(path.IndexOf(":") + 2);
            path = System.IO.Path.Combine(path, ("Sound\\" + music));
            return path;
        }

        private Rectangle createMovement(Rectangle ennemy)
        {


            //Console.WriteLine("Creation of an ennemy (single)");

            myCanvas.Children.Remove(ennemy);
            ennemy.Width = 30;
            ennemy.Height = 30;
            ennemy.Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(resourcePath("enemyD1.png"), UriKind.Absolute)) };
            myCanvas.Children.Add(ennemy);


            return ennemy;
        }

        private Rectangle updateSprite(Rectangle ennemy)
        {

            /*Rectangle rec = new Rectangle();
            myCanvas.Children.Remove(ennemy);
            //Console.WriteLine("UPDATE SPRITE!");

            rec.Width = 30;
            rec.Height = 30;*/
            ennemy.Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(resourcePath("enemyD2.png"), UriKind.Absolute)) };

            //myCanvas.Children.Add(rec);
            return ennemy;
        }

        private int collision(Rectangle bullet, List<Ennemy> ennemy)
        {
            bool touch = false;
            int points = -1;

            for (int i = 0; i < ennemy.Count && !touch; i++)
            {
                Point enemyLoc = getLocation(ennemy.ElementAt(i).getEnemy());
                Point bulletLoc = getLocation(bullet);

                if (enemyLoc.X < (bulletLoc.X + bullet.ActualWidth) &&
                   (enemyLoc.X + ennemy.ElementAt(i).getEnemy().ActualWidth) > bulletLoc.X &&
                   enemyLoc.Y < (bulletLoc.Y + bullet.ActualHeight) &&
                   (ennemy.ElementAt(i).getEnemy().ActualHeight + enemyLoc.Y) > bulletLoc.Y)
                {
                    if (ennemy.ElementAt(i).collide() == 0)
                    {
                        exploLoc.Add(getLocation(ennemy.ElementAt(i).getEnemy()));
                        myCanvas.Children.Remove(ennemy.ElementAt(i).getEnemy());
                        points = ennemy.ElementAt(i).score();
                        ennemy.Remove(ennemy.ElementAt(i));

                        return points;
                    }
                    else
                    {
                        return points = 0;
                    }
                }
            }
            return points;
        }
    }       
}
