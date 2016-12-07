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
        private static double newLocX;
        private string currentKey = "";
        private DispatcherTimer foward;
        private DispatcherTimer update;
        private DispatcherTimer enemyExplo;
        private DispatcherTimer allyExplo;
        private bool spaceBut = false;
        private int shootPause = 38;
        private List<Rectangle> bulletList;

        private bool collide = false;
        private int invulnerable = 0;
        private bool pause = false;

        private int occurence = 0;
        private int life = 2;
        private Rectangle[] shipLife = new Rectangle[2];

        int score = 0;



        private List<Ennemy> enemyList = new List<Ennemy>();


        private List<int> track = new List<int>();
        private List<Rectangle> explosions = new List<Rectangle>();
        private List<Point> exploLoc;
        private Rectangle pauseScreen = new Rectangle();

        private SoundPlayer shootSound;
        private SoundPlayer explosionSound;
        //MediaElement musicBG;

        public MainWindow()
        {

            InitializeComponent();

            gameStart();

        }

        private void gameStart()
        {
            screenSizeGame();

            exploLoc = new List<Point>();

            makeItRainLvl();

            initializeMusic();

            initializeVariable();

            initializeLife();
        }

        private void initializeLife()
        {
            int space = 0;

            for (int i = 0; i < shipLife.Length; i++)
            {
                Rectangle rect = new Rectangle();

                if (i == 0)
                {
                    rect.Fill = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri(resourcePath("shipB.png"),
                                                                            UriKind.Absolute))
                    };

                }
                else
                    rect.Fill = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri(resourcePath("shipPB.png"),
                                                                        UriKind.Absolute))
                    };

                Canvas.SetTop(rect, Canvas.GetTop(lifeLb));
                Canvas.SetLeft(rect, Canvas.GetLeft(lifeLb) + (space += 50));

                rect.Height = 40;
                rect.Width = 40;
                myCanvas.Children.Add(rect);

                shipLife[i] = rect;
            }
        }

        /**
         * The method will set the size of the screen
         * based on the actual physical screen and set
         * the background.
         */
        private void screenSizeGame()
        {
            this.Height = SystemParameters.VirtualScreenHeight - 50;
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
            Canvas.SetTop(lifeLb, myCanvas.Height / 1.1);

            //Start in the middle of the screen
            newLocX = this.Width / 2;

            //Update timer will update the "sprite" of enemies
            update = new DispatcherTimer();
            update.Tick += Update_Tick;
            update.Interval = TimeSpan.FromSeconds(0.5);

            update.Start();

            //allyExplo will be initialized, but will start
            //only when a collision happen
            allyExplo = new DispatcherTimer();
            allyExplo.Tick += AllyExplo_Tick;
            allyExplo.Interval = TimeSpan.FromSeconds(0.7);

            //enemyExplo will be initialized, but will start
            //only when a collision happen
            enemyExplo = new DispatcherTimer();
            enemyExplo.Tick += ExplosionTimer_Tick;
            enemyExplo.Interval = TimeSpan.FromSeconds(0.1);

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
                enemyExplo.Stop();
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
            }

            moveAndShoot();

            updateShooting();



            if (exploLoc.Count != explosions.Count)
            {
                track.Add(0);
                explosions.Add(createExplosion(exploLoc.ElementAt(exploLoc.Count - 1)));

                explosionSound.Play();

                if (enemyExplo.IsEnabled == false)
                    enemyExplo.Start();

            }


            if (collide)
                invulnerable++;

            if (collide && invulnerable > 100)
                collide = false;


            if (!collide)
                if (shipCollision(ship, enemyList))
                {
                    collide = true;
                    invulnerable = 0;
                    dead(ship);
                }

            enemyList = updateNextStep(enemyList);

        }

        private void AllyExplo_Tick(object sender, EventArgs e)
        {
            if (occurence == 0)
            {
                BitmapImage bi3 = new BitmapImage();
                bi3.BeginInit();
                bi3.UriSource = new Uri(resourcePath("shipExplose1.png"), UriKind.Absolute);
                bi3.EndInit();
                ship.Stretch = Stretch.Fill;
                ship.Source = bi3;

            }

            if (occurence == 1)
            {
                BitmapImage bi3 = new BitmapImage();
                bi3.BeginInit();
                bi3.UriSource = new Uri(resourcePath("shipExplose2.png"), UriKind.Absolute);
                bi3.EndInit();
                ship.Stretch = Stretch.Fill;
                ship.Source = bi3;

            }

            if (occurence == 3)
            {
                BitmapImage bi3 = new BitmapImage();
                bi3.BeginInit();
                bi3.UriSource = new Uri(resourcePath("shipExplose3.png"), UriKind.Absolute);
                bi3.EndInit();
                ship.Stretch = Stretch.Fill;
                ship.Source = bi3;
            }

            if (occurence == 4)
            {
                BitmapImage bi3 = new BitmapImage();
                bi3.BeginInit();
                bi3.UriSource = new Uri(resourcePath("shipExplose4.png"), UriKind.Absolute);
                bi3.EndInit();
                ship.Stretch = Stretch.Fill;
                ship.Source = bi3;
            }

            occurence++;

            if (occurence > 4)
            {
                occurence = 0;

                if (life > 0)
                {

                    life -= 1;
                }

                if (life < 0)
                    gameOver();



                if (life == 1)
                {
                    BitmapImage bi3 = new BitmapImage();
                    bi3.BeginInit();
                    bi3.UriSource = new Uri(resourcePath("shipPB.png"), UriKind.Absolute);
                    bi3.EndInit();
                    ship.Stretch = Stretch.Fill;
                    ship.Source = bi3;
                }
                else
                {
                    BitmapImage bi3 = new BitmapImage();
                    bi3.BeginInit();
                    bi3.UriSource = new Uri(resourcePath("shipB.png"), UriKind.Absolute);
                    bi3.EndInit();
                    ship.Stretch = Stretch.Fill;
                    ship.Source = bi3;
                }

                foward.Start();
                allyExplo.Stop();
            }

        }

        private void gameOver()
        {
            //KEYLENISTHEBEST
        }

        //Another meaning for after?
        private void dead(Image ship)
        {
            //myCanvas.Children.Add(ship);
            foward.Stop();

            allyExplo.Start();

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
                    list.Remove(list.ElementAt(i));
                    touch = true;
                    //myCanvas.Children.Remove(ship);
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
                        enemyExplo.Stop();
                        pause = true;
                        pauseWindow(pause);
                    }
                    else
                    {
                        update.Start();
                        foward.Start();
                        enemyExplo.Start();
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
