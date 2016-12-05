using System;
using System.Collections.Generic;
using System.Linq;
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
        Image shoot = new Image();
        string currentKey = "";
        DispatcherTimer foward;
        DispatcherTimer update;
        DispatcherTimer explosionTimer;
        bool spaceBut = false;
        private int shootPause = 38;
        private List<Rectangle> bulletList;
        private List<Rectangle> ennemyList;



        Vector ennemyPath;
        static Vector positionWave;
        int testIndex;
        Rectangle ennemy;
        int timerInt = 20;

        Rectangle circleEnemy;
        double angle =0;
        double angle2 = 0;
        double X = 100;
        double Y = 100;
        Rectangle tryPattern;
        int tp = 50;
        bool collide = false;
        int invulnerable = 0;
        bool pause = false;

        int score = 0;

        List<EnnemyShoot> ennShoot = new List<EnnemyShoot>();

        List<Ennemy> testC = new List<Ennemy>();
        List<RedAlien> redAl = new List<RedAlien>();
        List<GreenAlien> greenAl = new List<GreenAlien>();
        List<BlueAlien> blueAl = new List<BlueAlien>();

        List<int> track = new List<int>();
        List<Rectangle> explosions = new List<Rectangle>();
        List<Point> exploLoc;
        Rectangle pauseScreen = new Rectangle();

        LevelOne lvl1;
        MediaPlayer shootSound;

        public MainWindow()
        {
            //LevelThree lvl3 = new LevelThree();

            InitializeComponent();

            

             //THIS IS GETTING THE REAL SIZE OF THE SCREEN ~~
            double testW = SystemParameters.VirtualScreenWidth/2.5;
            double testH = SystemParameters.VirtualScreenHeight -50;
            
            this.Height = testH;
            myCanvas.Height = this.Height;
            this.Width = testW;
            myCanvas.Width = this.Width;

            this.Top = testH/250;
            this.Left = testW/2;

            Canvas.SetTop(ship, myCanvas.Height/1.3);

            Canvas.SetZIndex(bgImg, -1);
            bgImg.Height = this.Height;
            bgImg.Width = this.Width;

            //PLACE FOR TEST LIST CLASSES

            //testC.Add(redAl);

            lvl1 = new LevelOne(myCanvas);

            

            exploLoc = new List<Point>();
            //testC = lvl1.run();
            test();

            if(ennShoot.Count != testC.Count)
            for (int i = 0; i < testC.Count; i++)
                ennShoot.Add(new EnnemyShoot(getLocation(testC.ElementAt(i).getEnemy()), myCanvas));
            //INSTANCEOF ~ like java
            //if (testC.ElementAt(0) is BlueAlien)



            //ennemyList = normalGame(ennemyList);
            //ennemyList = makeItRain(ennemyList);
            circleEnemy = new Rectangle();
            tryPattern = createMovement(circleEnemy);
            circleEnemy = createMovement(circleEnemy);
            ennemy = new Rectangle();
            testIndex = 0;
            //Original position (Middle)
            ennemyPath = new Vector(Canvas.GetLeft(ship), (this.Height/2));

            System.Media.SoundPlayer music = new System.Media.SoundPlayer(soundPath("GalagaRemix.wav"));
            //music.Play();
            MediaPlayer shootSound;
            //shootSound.Open(soundPath(new Uri(soundPath("Galaga_Firing_Sound.wav"))));

            //!!!
            /*double oriL = Canvas.GetLeft(ennemyList.ElementAt(5));
            double oriT = Canvas.GetTop(ennemyList.ElementAt(5));*/

            //bullet = shipTest.Properties.Resources.GalagaRocket;
            //bullet.MakeTransparent(Color.White);

            newLocX = this.Height / 2;

            update = new DispatcherTimer();
            update.Tick += Update_Tick;
            update.Interval = TimeSpan.FromSeconds(0.5);

            update.Start();

            explosionTimer = new DispatcherTimer();
            explosionTimer.Tick += ExplosionTimer_Tick;
            explosionTimer.Interval = TimeSpan.FromSeconds(0.1);

            foward = new DispatcherTimer();
            foward.Tick += Foward_Tick;
            foward.Interval = TimeSpan.FromMilliseconds(1);

            foward.Start();

            bulletList = new List<Rectangle>();
            
            KeyDown += shipMove;
            

        }

        private void ExplosionTimer_Tick(object sender, EventArgs e)
        {
             if(exploLoc.Count != 0)
            {
                for(int i =0; i < exploLoc.Count; i++)
                {

                    if (track.Count != 0)
                        track[i]++;

                    

                    if (track.ElementAt(i) == 1) {
                        explosions.ElementAt(i).Height = 40;
                        explosions.ElementAt(i).Width = 40;
                        explosions.ElementAt(i).Fill = new ImageBrush { ImageSource = 
                                new BitmapImage(new Uri(monsterPath("explosion2.png"), 
                                                                        UriKind.Absolute)) };
                        
                        }

                    if (track.ElementAt(i) == 2) {
                        explosions.ElementAt(i).Height = 30;
                        explosions.ElementAt(i).Width = 30;
                        explosions.ElementAt(i).Fill = new ImageBrush { ImageSource = 
                                new BitmapImage(new Uri(monsterPath("explosion3.png"), 
                                                                        UriKind.Absolute)) };
                        }

                    if (track.ElementAt(i) == 3) {
                        explosions.ElementAt(i).Height = 20;
                        explosions.ElementAt(i).Width = 20;
                        explosions.ElementAt(i).Fill = new ImageBrush { ImageSource = 
                                new BitmapImage(new Uri(monsterPath("explosion4.png"), 
                                                                        UriKind.Absolute)) };
                        }

                    if (track.ElementAt(i) == 4) {
                        explosions.ElementAt(i).Height = 10;
                        explosions.ElementAt(i).Width = 10;
                        explosions.ElementAt(i).Fill = new ImageBrush { ImageSource = 
                                new BitmapImage(new Uri(monsterPath("explosion5.png"), 
                                                                        UriKind.Absolute)) };
                        
                    }

                    if(track.ElementAt(i) > 4)
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
            
            for(int i =0; i < testC.Count; i++)
            {
                if (testC.ElementAt(i) is BlueAlien)
                    testC.ElementAt(i).updateSprite();

                if (testC.ElementAt(i) is RedAlien)
                    testC.ElementAt(i).updateSprite();

                if (testC.ElementAt(i) is GreenAlien)
                    testC.ElementAt(i).updateSprite();

            }                  
        }

        private Rectangle createExplosion(Point loc)
        {
            Rectangle rec = new Rectangle();

            rec.Height = 50;
            rec.Width = 50;
            rec.Fill = new ImageBrush { ImageSource = 
                                new BitmapImage(new Uri(monsterPath("explosion1.png"), 
                                                                        UriKind.Absolute)) };         

            Canvas.SetTop(rec, loc.Y);
            Canvas.SetLeft(rec, loc.X);
            myCanvas.Children.Add(rec);

            
            return rec;
        }


        public string updateExplosion(string explosion)
        {
            string path;

            path = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);

            path = path.Substring(0, path.LastIndexOf("\\"));
            path = path.Substring(0, path.LastIndexOf("\\"));
            path = path.Substring(path.IndexOf(":") + 2);
            path = System.IO.Path.Combine(path, ("Ressources\\" + explosion));
            return path;
        }

        private void Foward_Tick(object sender, EventArgs e)
        {
            if (this.Width != myCanvas.Width || this.Height != myCanvas.Height)
            {
                myCanvas.Width = this.Width;
                myCanvas.Height = this.Height;
            }

            shootPause++;
            Console.WriteLine(testC.Count);
            if (testC.Count == 0)
            {
                test();
                //testC = lvl1.run();
            }

           
            

            switch (currentKey)
            {
                case "Left":
                    //currentKey = "";
                    //newLocX -= 10;
                    newLocX -= 3.2;
                    if (newLocX < -6) newLocX = -6;
                    break;

                case "Right":
                    //currentKey = "";
                    //newLocX += 10;
                    newLocX += 3.2;               
                    if (newLocX > myCanvas.Width-80) newLocX = myCanvas.Width - 80;
                    break;

            }

            if (spaceBut)
            {
                spaceBut = false;

                if (shootPause > 20)
                {
                    shootPause = 0;
                    Rectangle bullet = new Rectangle();
                    bullet.Width = 50;
                    bullet.Height = 50;

                    var outPutDirectory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);

                    outPutDirectory = outPutDirectory.Substring(0, outPutDirectory.LastIndexOf("\\"));
                    outPutDirectory = outPutDirectory.Substring(0, outPutDirectory.LastIndexOf("\\"));
                    outPutDirectory = outPutDirectory.Substring(outPutDirectory.IndexOf(":") + 2);
                    //Console.WriteLine("PATH OUTPUTDIR: " + outPutDirectory);

                    var iconPath = System.IO.Path.Combine(outPutDirectory, "Ressources\\GalagaRocket.png");
                    //Console.WriteLine("PATH: " + iconPath);
                    bullet.Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(iconPath, UriKind.Absolute)) };

                    myCanvas.Children.Add(bullet);
                    setLocation(bullet, Canvas.GetTop(ship) - 15, Canvas.GetBottom(ship),
                                            Canvas.GetLeft(ship) + 5, Canvas.GetRight(ship));
                    bulletList.Add(bullet);

                    //shootSound.Play();
                }
            }


            //Console.WriteLine();

            //Remove bullet which are out of the screen
            for (int i = 0; i < bulletList.Count; i++)
            {
                
                Canvas.SetTop(bulletList.ElementAt(i), Canvas.GetTop(bulletList.ElementAt(i)) - 5);

                //Console.WriteLine(ennemyList.ElementAt(0));
                //collision(bulletList.ElementAt(i),ennemyList);

                if (Canvas.GetTop(bulletList.ElementAt(i)) < -myCanvas.Height / 4.5)
                {
                    
                    myCanvas.Children.Remove(bulletList.ElementAt(i));
                    bulletList.RemoveAt(i);                   
                }

                if (bulletList.Count != 0)
                {
                    int points;
                    points = collision(bulletList.ElementAt(i), testC);

                    if (points > 0)
                    {
                        scoreLb.Content = "Score: " + (score += points);
                        myCanvas.Children.Remove(bulletList.ElementAt(i));
                        bulletList.RemoveAt(i);
                    }
                    if(points == 0)
                    {
                        myCanvas.Children.Remove(bulletList.ElementAt(i));
                        bulletList.RemoveAt(i);
                    }
                }
               
            }

            if (exploLoc.Count != explosions.Count)
            {
                track.Add(0);
                explosions.Add(createExplosion(exploLoc.ElementAt(exploLoc.Count - 1)));

                System.Media.SoundPlayer music = new System.Media.SoundPlayer(soundPath("Grenade-Sound.wav"));
                music.Play();
                //music.PlayLooping();
                
                
                                    
                if (explosionTimer.IsEnabled == false)
                    explosionTimer.Start();

            }

            //CONSOLE SECOND
            //Console.WriteLine(DateTime.Now.ToString("ss") );

            if (timerInt > 20)
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
            timerInt++;
        

            //DIAGONAL ENNEMY WORKING ~ 70%
            ennemyPath.Normalize();
            Vector perp = new Vector(-ennemyPath.Y, ennemyPath.X);
            float waveAmp = 15.00f;
            float waveAngle = 5 * 3.14f * 2;
            Vector wave = perp * Math.Sin(waveAngle) * waveAmp;
            Vector vel = ennemyPath * 0.5;//speed
            positionWave += vel * 5 + wave;

            //Console.WriteLine("Vec posWave Y: " + positionWave.Y + "\tVec posWave X: " + positionWave.X);
            if(positionWave.Y > this.Height)
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

            if(collide)
                invulnerable++;

            if (collide && invulnerable > 100)
                collide = false;


            if (!collide)
                if (shipCollision(ship, testC))
                {
                    collide = true;
                    invulnerable = 0;
                    MessageBox.Show("Only Dumb can die HAHAHA", "NOOB", MessageBoxButton.OK);
                    dead(ship, testC);
                    //foward.Stop();
                }

            // testC = updateRain(testC);
            //testC = lvl1.updateGame(testC);
            testC = updateNextStep(testC);

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
                //Console.WriteLine("I: " + i);          

                //Console.WriteLine((int)enemyLoc.X + " " + (int)bulletLoc.X + " | " + (int)enemyLoc.Y + " " + (int)bulletLoc.Y);
                //Console.WriteLine(Canvas.GetTop(ennemy.ElementAt(i)) + "\tEnnemyLEft: " + Canvas.GetLeft(ennemy.ElementAt(i)) + "\tLeftBullet: " + Canvas.GetLeft(bullet) + "\tTopBullet: " + Canvas.GetTop(bullet));       


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
                        pause = true;
                        //pauseWindow(pause);
                    }
                    else
                    {
                        update.Start();
                        foward.Start();
                        pause = false;
                        //deletePause();
                    }
                    break;
            }
        }

        private void pauseWindow(bool display)
        {

            
            pauseScreen = new Rectangle();
            myCanvas.Children.Add(pauseScreen);
            pauseScreen.Width = myCanvas.Width -100;
            pauseScreen.Height = myCanvas.Height -100;
            pauseScreen.Fill = new SolidColorBrush(Color.FromRgb(26, 26, 26));
            
            Canvas.SetZIndex(pauseScreen, 99);

            Label points = new Label();
            points.Height = 50;
            points.Width = 75;
            points.Content = "Hi There";

            Canvas.SetTop(points, 100);
            Canvas.SetRight(points, 100);
            myCanvas.Children.Add(points);
            Canvas.SetZIndex(pauseScreen, 101);
            points.Background = new SolidColorBrush(Color.FromRgb(51, 0, 0));

            /*if (display)
                deletePause(points);*/

        }

        private void deletePause()
        {
            myCanvas.Children.Remove(pauseScreen);
            pauseScreen = null;
            Console.WriteLine(myCanvas.Children.Count);
            //myCanvas.Children.IndexOf(points);
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

        private string monsterPath(string creature)
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
            ennemy.Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(monsterPath("enemyD1.png"), UriKind.Absolute)) };
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
            ennemy.Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(monsterPath("enemyD2.png"), UriKind.Absolute)) };

            //myCanvas.Children.Add(rec);
            return ennemy;
        }

        private int collision(Rectangle bullet, List<Ennemy> ennemy)
        {


           
            bool touch = false;
            int points = -1;

            for (int i = 0; i < ennemy.Count && !touch; i++)
            {
               

                //Console.WriteLine((int)enemyLoc.X + " " + (int)bulletLoc.X + " | " + (int)enemyLoc.Y + " " + (int)bulletLoc.Y);
                //Console.WriteLine(Canvas.GetTop(ennemy.ElementAt(i)) + "\tEnnemyLEft: " + Canvas.GetLeft(ennemy.ElementAt(i)) + "\tLeftBullet: " + Canvas.GetLeft(bullet) + "\tTopBullet: " + Canvas.GetTop(bullet));       


                if (getLocation(ennemy.ElementAt(i).getEnemy()).X < (getLocation(bullet).X + bullet.ActualWidth) &&
                   (getLocation(ennemy.ElementAt(i).getEnemy()).X + ennemy.ElementAt(i).getEnemy().ActualWidth) > getLocation(bullet).X &&
                   getLocation(ennemy.ElementAt(i).getEnemy()).Y < (getLocation(bullet).Y + bullet.ActualHeight) &&
                   (ennemy.ElementAt(i).getEnemy().ActualHeight + getLocation(ennemy.ElementAt(i).getEnemy()).Y) > getLocation(bullet).Y)
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
                        
                        return points =0;
                    }                                                                              
                }
            }
            return points;
        }

        private List<Rectangle> normalGame(List<Rectangle> enemyList)
        {
            enemyList = new List<Rectangle>(40);
            myCanvas.Width = this.Width;
            myCanvas.Height = this.Height;
            int numEnnemy = 0;
            double spaceBetween = this.Width / 2 -250;
            double height = myCanvas.Height / 2.5;

            for (int i = 0; i < 40; i++)
            {
                
                if (i < 20)
                { //TWO LINES OF BLUE
                    enemyList.Add(new Rectangle());
                    enemyList.ElementAt(i).Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(monsterPath("enemyD1.png"), UriKind.Absolute)) };

                    myCanvas.Children.Add(enemyList.ElementAt(i));
                    enemyList.ElementAt(i).Width = 30;
                    enemyList.ElementAt(i).Height = 30;

                    if (numEnnemy < 10)
                    {
                        Canvas.SetTop(enemyList.ElementAt(i), height);
                        Canvas.SetLeft(enemyList.ElementAt(i), spaceBetween += 25 + 15);
                        numEnnemy++;
                    }
                    else //SECOND ROW
                    {

                        if (numEnnemy ==10)
                            spaceBetween = this.Width / 2 - 250;


                        Canvas.SetTop(enemyList.ElementAt(i), height - 40);
                        Canvas.SetLeft(enemyList.ElementAt(i), spaceBetween += 25 + 15);
                        numEnnemy++;
                    }

                }

                //Reset the number of ennemy for the new row (16)
                if (numEnnemy == 20 && i ==20)
                {
                    numEnnemy = 0;
                    spaceBetween = this.Width / 2 - 210;
                    height -= 85;
                }
                 


                if (i>=20 && i < 36)
                { //TWO LINES OF RED
                    
                    enemyList.Add(new Rectangle());
                    enemyList.ElementAt(i).Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(monsterPath("enemyA1.png"), UriKind.Absolute)) };
                    myCanvas.Children.Add(enemyList.ElementAt(i));
                    enemyList.ElementAt(i).Width = 30;
                    enemyList.ElementAt(i).Height = 30;

                    if (numEnnemy < 8)
                    {
                        Canvas.SetTop(enemyList.ElementAt(i), height);
                        Canvas.SetLeft(enemyList.ElementAt(i), spaceBetween += 25 + 15);
                        numEnnemy++;
                    }
                    else //SECOND ROW
                    {

                        if (numEnnemy == 8)
                            spaceBetween = this.Width / 2 - 210;


                        Canvas.SetTop(enemyList.ElementAt(i), height - 40);
                        Canvas.SetLeft(enemyList.ElementAt(i), spaceBetween += 25 + 15);
                        numEnnemy++;
                    }
                }

                //Reset the number of ennemy for the new row (16)
                
                if (numEnnemy == 16 && i ==36)
                {
                   
                    numEnnemy = 0;
                    spaceBetween = this.Width / 2 -130;
                    height -= 85;
                }

                if(i >= 36)
                { //4 GREEN
                    //Console.WriteLine(i);
                    enemyList.Add(new Rectangle());
                    enemyList.ElementAt(i).Fill = new ImageBrush { ImageSource = new BitmapImage(new Uri(monsterPath("enemyB1.png"), UriKind.Absolute)) };
                    myCanvas.Children.Add(enemyList.ElementAt(i));
                    enemyList.ElementAt(i).Width = 30;
                    enemyList.ElementAt(i).Height = 30;

                    if (numEnnemy < 4)
                    {
                        Canvas.SetTop(enemyList.ElementAt(i), height);
                        Canvas.SetLeft(enemyList.ElementAt(i), spaceBetween += 25 + 15);
                        numEnnemy++;
                    }
                }               
            }
            return enemyList;
        }


        private List<Ennemy> normalGame(List<Ennemy> enemyList)
        {
            enemyList = new List<Ennemy>(40);
            myCanvas.Width = this.Width;
            myCanvas.Height = this.Height;
            int numEnnemy = 0;
            double spaceBetween = this.Width / 2 - 250;
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
                            spaceBetween = this.Width / 2 - 250;


                        Canvas.SetTop(enemyList.ElementAt(i).getEnemy(), height - 40);
                        Canvas.SetLeft(enemyList.ElementAt(i).getEnemy(), spaceBetween += 25 + 15);
                        numEnnemy++;
                    }

                }

                //Reset the number of ennemy for the new row (16)
                if (numEnnemy == 20 && i == 20)
                {
                    numEnnemy = 0;
                    spaceBetween = this.Width / 2 - 210;
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
                            spaceBetween = this.Width / 2 - 210;


                        Canvas.SetTop(enemyList.ElementAt(i).getEnemy(), height - 40);
                        Canvas.SetLeft(enemyList.ElementAt(i).getEnemy(), spaceBetween += 25 + 15);
                        numEnnemy++;
                    }
                }

                //Reset the number of ennemy for the new row (16)

                if (numEnnemy == 16 && i == 36)
                {

                    numEnnemy = 0;
                    spaceBetween = this.Width / 2 - 130;
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

        private List<Rectangle> makeItRain(List<Rectangle> list)
        {
            list = new List<Rectangle>();
            int left = -100;
            for(int i =0; i < 14; i++)
            {              
                list.Add(createMovement(new Rectangle()));
                Canvas.SetTop(list.ElementAt(i), 0);
                Canvas.SetLeft(list.ElementAt(i), left += 50);

            }


            return list;
        }

        private List<Rectangle> updateRain(List<Rectangle> list)
        {
            foreach (Rectangle rec in list)
                if (Canvas.GetTop(rec) > 400)
                    Canvas.SetTop(rec, 0);
            else
                Canvas.SetTop(rec, Canvas.GetTop(rec) + 1.8);

            return list;
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

    }
}
