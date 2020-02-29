// Kevin Ma
// June 4th 2017
// Culminating Assignment
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Culminating_Assignment
{
    public partial class Form1 : Form
    {
        // Determine the game state of the game
        int gameState;

        const int GAME_MENU = 0;
        const int GAME_START = 1;
        const int GAME_INSTRUCTIONS = 2;

        // Determine level
        const int LEVEL_ONE = 1;
        
        



        // Amount of zombies that can be summoned at once
        int zombieCap;
        int level;
        int score;
        

      
        // Rise and run of the projectile variables 
        int rise;
        int run;

        // Background graphics Data
        int backgroundX;
        int backgroundY;
        PointF backgroundLocation;
        SizeF backgroundSize;
        RectangleF backgroundBoundary;

        // Zombies graphics data
        PointF[] zombieLocation = new PointF[5];
        SizeF[] zombieSize = new SizeF[5];
        RectangleF[] zombieBoundary = new RectangleF[5];

        // Amount of zombies that is spawned at level one
        const int LEVEL_ONE_ZOMBIE = 5;

        // Determines if zombie is alive
        bool[] zombieAlive;
        // Keeps track of the zombies that die
        int zombieCounter;
      
        // hero graphics information
        int heroX;
        int heroY;
        PointF heroLocation;
        SizeF heroSize;
        RectangleF heroBoundary;

        // Score graphics information
        int scoreX;
        int scoreY;
        PointF scoreLocation;
        SizeF scoreSize;
        RectangleF scoreBoundary;

        int timeX;
        int timeY;
        PointF timeLocation;
        SizeF timeSize;
        RectangleF timeBoundary;

        
        // Amount of health for the player
        int heroHealth = 1000;
        int heroMaxHealth = 1000;

        bool [] zombieLeft;
        bool [] zombieRight;
        bool [] zombieUp ;
        bool [] zombieDown;
        
      
        // hero health bar graphics 
        int healthBarHeroX;
        int healthBarHeroY;
        PointF healthBarLocationHero;
        SizeF healthBarSizeHero;
        RectangleF healthBarBoundaryHero;


        // text graphics data for instructions
        Font textFont = new Font("Arial", 26.0f);
        PointF menuTextLocation;
        PointF instructionTextLocation;
        Random numberGeneratorX = new Random();
        Random numberGeneratorY = new Random();


        // cooldown when zombie hits player
        int[] zombieHitCoolDown;
        // cooldown limit after zombie hits player
        const int ZOMBIE_HIT_COOLDOWN_LIMIT = 10;
        // title graphics data
        int titleX;
        int titleY;
        PointF titleLocation;
        SizeF titleSize;
        RectangleF titleBoundary;

        

        // Determines cooldown in between projectiles
        int projectileCounter = 0;

        // number of projectiles possible on the screen
        const int NUMBER_OF_PROJECTILES = 20;

        // Projectile graphics data
        float projectileX;
        float projectileY;
        PointF [] projectileLocation = new PointF [NUMBER_OF_PROJECTILES];
        SizeF [] projectileSize = new SizeF [NUMBER_OF_PROJECTILES];
        RectangleF [] projectileBoundary = new RectangleF [NUMBER_OF_PROJECTILES];

        
        // Normal speed of the hero
        const int HERO_SPEED = 10;

        // Maximum amount of health packs spawned at a time
        const int MAX_HEALTH_PACK = 3;

        // Determines whether the health pack will be spawned
        bool healthPackSpawn = false;
        bool[] healthPackUsed = new bool[MAX_HEALTH_PACK]; 

        // health pack graphics data
        PointF [] healthPackLocation = new PointF [MAX_HEALTH_PACK];
        SizeF[] healthPackSize = new SizeF[MAX_HEALTH_PACK];
        RectangleF [] healthPackBoundary = new RectangleF [MAX_HEALTH_PACK];

        // Speed boost graphics data
        float speedBoostX;
        float speedBoostY;
        PointF speedBoostLocation;
        SizeF speedBoostSize;
        RectangleF speedBoostBoundary;

        // Instructions graphics data
        float instructionX;
        float instructionY;
        PointF instructionLocation;
        SizeF instructionSize;
        RectangleF instructionBoundary;

        // determines whether speed boost is spawning on map
        bool enableSpeedBoost = false;
        // determines whether speed boost is activated by player
        bool speedBoostActivated = false;
        // determines how long it takes for speed boost to spawn
        int speedBoostSpawn;
        // determines how long speed boost lasts on player
        int speedBoostDuration;
        // player speed when speed boost is activated
        const int SPEED_BOOST_SPEED = 20;
        // Speed of the projectile and coordinates when fired
        float [] projectileXSpeed;
        float[] projectileYSpeed;
        const int PROJECTILE_SPEED = 25;

        // cooldown intervals in between firing projectiles
        bool enableProjectileCoolDown = false;
        // max cooldown interval before projectile can fire again
        const int MAX_PROJECTILE_COOLDOWN = 10;
        // keeps track of the time in between projectiles
        int projectileCoolDown = 0;

        // determines when projectile is activated
        bool [] activateProjectile;
        // determines if projectile is in motion
        bool [] projectileMotion;
        // damage of proejctile
        const int PROJECTILE_DAMAGE = 1;
        // determines if a new level is occuring
        bool newLevel = true;

        // cooldown in between levels
        bool levelCoolDownPeriod = false;
        // keeps track of cooldown periods before new level starts
        int coolDownTime = 0;
        // Amount of time before new level starts
        const int COOL_DOWN_LIMIT = 5;
        
        // determines the health of the zombie
        int[] zombieHealth;
        const int ZOMBIE_SPEED = 3;
      
        // Determines when the player moves
        bool moveLeft;
        bool moveUp;
        bool moveDown;
        bool moveRight;

        // Determines the orientation of the player
        bool heroLeft;
        bool heroRight;
        bool heroUp;
        bool heroDown;



        // Frame rate loop variables 
        int previousTime;
        int previousSecond;
        const int FRAME_RATE = 30;
        const int FRAME_TIME = 1000 / FRAME_RATE;
        const int SECOND = 1000; 
        bool timerLoop = true;

        public Form1()
        {
            InitializeComponent();
            SetupBackground();
            SetupScore();
            SetupTitle();
            
        }
        // Set up coordinates, size and the boundary of the projectile
        private void SetupProjectile()
        {
            
            projectileX = heroLocation.X;
            projectileY = heroLocation.Y;
            projectileLocation [projectileCounter] = new PointF(projectileX, projectileY);
            projectileSize [projectileCounter] = new SizeF(30, 50);
            projectileBoundary [projectileCounter] = new RectangleF(projectileLocation[projectileCounter], projectileSize[projectileCounter]);

           
           
        }
        // Set up health of the zombie
        private void SetupZombieHealth()
        {
            for (int i = 0; i < zombieCap; i++)
            {
                zombieHealth[i] = 3; 
            }
        }
        // Set up spawn time for the speed boost along with coordinates, size and boundary
        // Subprogram also determines how long the speed boost buff will last on the player
        private void SetupSpeedBoost()
        {
            if (speedBoostSpawn == 10)
            {
                speedBoostX = numberGeneratorX.Next(0, 1080);
                speedBoostY = numberGeneratorY.Next(0, 720);
                speedBoostLocation = new PointF(speedBoostX, speedBoostY);
                speedBoostSize = new SizeF(50, 50);
                speedBoostBoundary = new RectangleF(speedBoostLocation, speedBoostSize);
                speedBoostSpawn = 0;
                enableSpeedBoost = true; 
            }
            if (speedBoostActivated == false)
            {
                speedBoostSpawn++;
            }
            
            // When speed boost is activated, the duration starts
            if (speedBoostActivated == true)
            {
                speedBoostDuration++;
            }
            // When the speed boost duration hits 5 seconds, the speed boost buff stops
            if (speedBoostDuration == 5)
            {
                speedBoostActivated = false;
                speedBoostDuration = 0;
            }
            
        }
        // Determine where projectile is fired to
        private void CreateProjectile()
        {
            if (heroLeft == true)
            {
                rise = 0;
                run = -1;
            }
            else if (heroRight == true)
            {
                rise = 0;
                run = 1;
            }
            else if (heroUp == true)
            {
                rise = -1;
                run = 0;
            }
            else if (heroDown == true)
            {
                rise = 1;
                run = 0;
            }
            float hypoteneuse = (float)Math.Sqrt(rise * rise + run * run);
            projectileXSpeed[projectileCounter] = run / hypoteneuse * PROJECTILE_SPEED;
            projectileYSpeed[projectileCounter] = rise / hypoteneuse * PROJECTILE_SPEED; 
            
        }
        // Move projectile when it is fired
        private void MoveProjectile()
        {
            for (int i =0; i < NUMBER_OF_PROJECTILES; i ++)
            {
                if (projectileMotion[i] == true)
                {
                    projectileLocation[i].X += projectileXSpeed[i];
                    projectileLocation[i].Y += projectileYSpeed[i];
                    projectileBoundary[i].Location = projectileLocation[i];
                }
                
            }
            
            
            
        }
        // Make sure player faces one direction only at a time
        private void OneDirectionOnly()
        {
            moveLeft = false;
            moveUp = false;
            moveDown = false;
            moveRight = false;
        }
        // Set up game when the game starts
        private void SetupGame()
        {
            if (gameState == GAME_START)
            {
                // show score 
                SetupScore();
                // show background
                SetupBackground();
                // set up the cool down time
                SetupTime();
                // Make the spawn of zombies become five
                zombieCap = LEVEL_ONE_ZOMBIE;
                projectileXSpeed = new float[NUMBER_OF_PROJECTILES];
                projectileYSpeed = new float[NUMBER_OF_PROJECTILES];
                // Make the health for all the zombies in the level
                zombieHealth = new int[LEVEL_ONE_ZOMBIE];
                // make all zombies alive 
                zombieAlive = new bool[LEVEL_ONE_ZOMBIE];
                // make player face up when the game starts
                heroUp = true;
                activateProjectile = new bool[NUMBER_OF_PROJECTILES];
                projectileMotion = new bool[NUMBER_OF_PROJECTILES];
                zombieHitCoolDown = new int[LEVEL_ONE_ZOMBIE];
                // Determine level
                level = 1;
                for (int i =0; i < NUMBER_OF_PROJECTILES; i ++)
                {
                    activateProjectile[i] = false;
                    projectileMotion[i] = false;

                }
            }
        }
        // Set up new level
        private void SetupLevel()
        {
            // when the amount of zombies dead equals to the amount of zombies in the level, start the cooldown interval between levels,
            // make health packs avaliable til the interval ends to recover health, and reset the zombie counter that follows the amount
            // of zombies dead to 0
            if (zombieCounter == zombieCap)
            {
                levelCoolDownPeriod = true;
                healthPackSpawn = true;
                level++;
                zombieCounter = 0;
            }
            
            
            // resize the array of the zombies based on the level
            
            int numberOfZombies = CalculateNumberOfZombies();
            Array.Resize(ref zombieAlive, numberOfZombies);
            Array.Resize(ref zombieLocation, numberOfZombies);
            Array.Resize(ref zombieSize, numberOfZombies);
            Array.Resize(ref zombieBoundary, numberOfZombies);
            Array.Resize(ref zombieHitCoolDown, numberOfZombies);
            Array.Resize(ref zombieHealth, numberOfZombies);
            Array.Resize(ref zombieLeft, numberOfZombies);
            Array.Resize(ref zombieDown, numberOfZombies);
            Array.Resize(ref zombieRight, numberOfZombies);
            Array.Resize(ref zombieUp, numberOfZombies);
            zombieCap = numberOfZombies;
            
        }
        // Calculate amount of zombies spawned depending on level
        int CalculateNumberOfZombies ()
        {
            return level * 5;
        }
        
        // Set up health pack graphics 
        private void SetupHealthPack()
        {
            // Make health pack spawn if its true
            if (healthPackSpawn == true)
            {
                for (int i = 0; i < MAX_HEALTH_PACK; i++)
                {
                    float healthPackX = numberGeneratorX.Next(0, 1080);
                    float healthPackY = numberGeneratorY.Next(0, 720);
                    healthPackLocation[i] = new PointF(healthPackX, healthPackY);
                    healthPackSize[i] = new SizeF(40, 40);
                    healthPackBoundary[i] = new RectangleF(healthPackLocation[i], healthPackSize[i]);
                    healthPackUsed[i] = true;
                }
             
            }
            healthPackSpawn = false;
               
        }
        // When zombie hits player, player takes damage in which there is a cooldown before it can hit again
        private void ZombieHit()
        {
            for (int i = 0; i < zombieCap; i++)
            {
                if (zombieBoundary[i].IntersectsWith(heroBoundary) && zombieAlive[i] == true && zombieHitCoolDown[i] == 0)
                {
                    heroHealth = heroHealth - 5;
                    
                    
                    zombieHitCoolDown[i] = 1;
                    
                }
                
                
            }
            // When zombie hits, cooldown occurs until it reaches the limit in which the zombie can hit again
            for (int i = 0; i < zombieCap; i++)
            {
                if (zombieHitCoolDown[i] > 0 && zombieHitCoolDown[i] != ZOMBIE_HIT_COOLDOWN_LIMIT)
                {
                    zombieHitCoolDown[i]++;
                }
                else if (zombieHitCoolDown[i] == ZOMBIE_HIT_COOLDOWN_LIMIT )
                {
                    zombieHitCoolDown[i] = 0;
                }

            }
            
            
        }
        
        // Set up zombie spawn points, size and boundaries
        private void SetupZombie()
        {
            for (int i = 0; i < zombieCap; i++)
            {

                float zombieX = numberGeneratorX.Next(0, 1080);
                float zombieY = numberGeneratorY.Next(0, 720);

                zombieLocation[i] = new PointF(zombieX, zombieY);
                zombieSize[i] = new SizeF(40, 60);
                zombieBoundary[i] = new RectangleF(zombieLocation[i], zombieSize[i]);
            }



       }
        
        // Set up graphics for the cooldown time
        private void SetupTime()
        {
            timeX = 700;
            timeY = 30;
            timeLocation = new PointF(timeX, timeY);
            timeSize = new SizeF(500, 40);
            timeBoundary = new RectangleF(timeLocation, timeSize);
        }
        // Set up zombie coordinates, health and make them alive 
        private void ZombieExist()
        {
            
            
            // When the new level starts, set up zombie coordinates, health and make them alive
            if (newLevel == true)
            {
                SetupZombie();
                SetupZombieHealth();
                for (int i = 0; i < zombieCap; i++)
                {
                    zombieAlive[i] = true;
                    
                }
            }

        }
        // Break interval between levels
        private void LevelCoolDown()
        {
            // cooldown between levels starts going up and health packs are spawned 
            if (levelCoolDownPeriod == true)
            {
                coolDownTime++;
                SetupHealthPack();
                // when the cooldown between levels is over, new level starts and health packs disappear
                if (coolDownTime == COOL_DOWN_LIMIT)
                {
                    levelCoolDownPeriod = false;
                    coolDownTime = 0;
                    for (int i = 0; i < MAX_HEALTH_PACK; i++)
                    {
                        healthPackUsed[i] = false;
                    }
                    newLevel = true;
                }
            }
        }
        // Check when something intersects with something
                
        private void CheckCollisions()
        {
            // Make sure the hero does not go past the boundaries
            if (heroLocation.Y  + heroSize.Height > ClientSize.Height)
            {
                heroLocation.Y = ClientSize.Height - heroSize.Height;
            }
            else if (heroLocation.Y < 0)
            {
                heroLocation.Y = 0;
            }
            else if (heroLocation.X  + heroSize.Width> ClientSize.Width)
            {
                heroLocation.X = ClientSize.Width - heroSize.Width;
            }
            else if (heroLocation.X < 0)
            {
                heroLocation.X = 0;
            }
            // Make sure the zombie does not go past the boundaries
            for (int i = 0; i < zombieCap; i ++)
            {
                if (zombieLocation[i].Y + zombieSize[i].Height> ClientSize.Height)
                {
                    zombieLocation[i].Y = ClientSize.Height - zombieSize[i].Height; 
                }
                else if (zombieLocation[i].Y < 0)
                {
                    zombieLocation[i].Y = 0; 
                }
                else if (zombieLocation[i].X + zombieSize[i].Width > ClientSize.Width)
                {
                    zombieLocation[i].X = ClientSize.Width - zombieSize[i].Width;
                }
                else if (zombieLocation[i].X < 0)
                {
                    zombieLocation[i].X = 0;
                }
                zombieBoundary[i].Location = zombieLocation[i];
            }
            // Projectile disappears when it goes past the boundary
            for (int i = 0; i < NUMBER_OF_PROJECTILES; i ++)
            {
                if (projectileLocation[i].Y > ClientSize.Height)
                {
                    projectileMotion[i] = false;
                    activateProjectile[i] = false;
                }
                else if (projectileLocation[i].Y < 0)
                {
                    projectileMotion[i] = false;
                    activateProjectile[i] = false;
                }
                else if (projectileLocation[i].X > ClientSize.Width)
                {
                    projectileMotion[i] = false;
                    activateProjectile[i] = false;
                }
                else if (projectileLocation[i].X < 0)
                {
                    projectileMotion[i] = false;
                    activateProjectile[i] = false;
                }
            }
            // Checks to see if the projectile intersected with a zombie, if it did, zombie loses health
            for (int i = 0; i < zombieCap; i ++)
            {
                for (int k = 0; k < NUMBER_OF_PROJECTILES; k ++)
                {
                    // zombie loses health and projectile disappears
                    if (projectileBoundary[k].IntersectsWith(zombieBoundary[i]) && zombieAlive[i] == true && projectileMotion [k] == true)
                    {
                        zombieHealth[i] = zombieHealth[i] - PROJECTILE_DAMAGE;
                        projectileMotion[k] = false;
                        activateProjectile[k] = false;
                        // when the zombie health hits 0, hero scores increases by 10 and zombie dies
                        if (zombieHealth[i] == 0)
                        {
                            zombieAlive[i] = false;

                            zombieCounter++;
                            score = score + 10;
                        }
                        
                    }
                }
                
            }
            // If player touches the health pack, they will regain health and the health pack will disappear
            for (int i = 0; i < MAX_HEALTH_PACK; i ++)
            {
                if (heroBoundary.IntersectsWith(healthPackBoundary[i]) && healthPackUsed[i] == true)
                {
                    heroHealth = heroHealth + 100;
                    if (heroHealth > 1000)
                    {
                        heroHealth = 1000;
                    }
                    healthPackUsed[i] = false;
                }
            }
            // ONce level is beyond 10, you win the game!
            if (level > 10)
            {
                MessageBox.Show("You Win!");
                Application.Exit();
                timerLoop = false;
            }
            // If the hero's health reaches 0 you lose the game
            if (heroHealth <= 0)
            {
                heroHealth = 0;
                Refresh();
                MessageBox.Show("You Lose!");
                Application.Exit();
                timerLoop = false;
            }
            // If the hero touches the speed buff, hero will increase its speed for a few seconds
            if (heroBoundary.IntersectsWith(speedBoostBoundary) && enableSpeedBoost == true)
            {
                speedBoostActivated = true;
                enableSpeedBoost = false;
            }
            
            
        }
        // Determine where the hero will move
        private void MoveHero()
        {
            // Hero will move left at its normal speed
            if (moveLeft == true && speedBoostActivated == false)
            {
                heroLocation.X = heroLocation.X - HERO_SPEED;
                
            }
            // Hero will move right at its normal speed
            else if (moveRight == true && speedBoostActivated == false)
            {
                heroLocation.X = heroLocation.X + HERO_SPEED;
            }
            // Hero will move down at its normal speed
            else if (moveDown == true && speedBoostActivated ==false)
            {
                heroLocation.Y = heroLocation.Y + HERO_SPEED;
            }
            // Hero will move right at its normal speed
            else if (moveUp == true && speedBoostActivated ==false)
            {
                heroLocation.Y = heroLocation.Y - HERO_SPEED;
            }
            // Hero will move left at speed boost speed when speed boost is activated
            else if (moveLeft == true && speedBoostActivated == true)
            {
                heroLocation.X = heroLocation.X - SPEED_BOOST_SPEED;

            }
            // Hero will move right at speed boost speed when speed boost is activated
            else if (moveRight == true && speedBoostActivated == true)
            {
                heroLocation.X = heroLocation.X + SPEED_BOOST_SPEED;
            }
            // Hero will move down at speed boost speed when speed boost is activated
            else if (moveDown == true && speedBoostActivated == true)
            {
                heroLocation.Y = heroLocation.Y + SPEED_BOOST_SPEED;
            }
            // Hero will move up at speed boost speed when speed boost is activated
            else if (moveUp == true && speedBoostActivated == true)
            {
                heroLocation.Y = heroLocation.Y - SPEED_BOOST_SPEED;
            }
            // Updates location of health bar
            healthBarLocationHero.X = heroLocation.X;
            healthBarLocationHero.Y = heroLocation.Y;
            healthBarBoundaryHero.Location = healthBarLocationHero;
            O// updates location of player
            heroBoundary.Location = heroLocation;

        }
        // Set up hero location, size and boundary
        private void SetupHero()
        {
            heroX = 300;
            heroY = 580;
            heroLocation = new PointF(heroX, heroY);
            heroSize = new SizeF(40, 80);
            heroBoundary = new RectangleF(heroLocation, heroSize);
        }
        // Set up background location, size and boundary
        private void SetupBackground()
        {
            backgroundX = 0;
            backgroundY = 0;
            backgroundLocation = new PointF (backgroundX, backgroundY);
            backgroundSize = new SizeF(1080, 720);
            backgroundBoundary = new RectangleF(backgroundLocation, backgroundSize);
        }
        // Set up score location, size and boundary
        private void SetupScore()
        {
            scoreX = 20;
            scoreY = 15;
            scoreLocation = new PointF(scoreX, scoreY);
            scoreSize = new SizeF(200, 150);
            scoreBoundary = new RectangleF(scoreLocation, scoreSize);
        }
        // Setup title location, size and boundary
        private void SetupTitle()
        {
            titleX = 200;
            titleY = 20;
            titleLocation = new PointF(titleX, titleY);
            titleSize = new SizeF(600, 100);
            titleBoundary = new RectangleF(titleLocation, titleSize);
            menuTextLocation = new PointF(150, 500);
            instructionTextLocation = new PointF(150, 600);
        }
        
        // Runs the loop at the given FRAME_RATE
        void Loop()
        {
            previousTime = Environment.TickCount;
            previousSecond = Environment.TickCount;
            while (timerLoop == true)
            {
                // Calculate the time since the last frame
                int timePassed = Environment.TickCount - previousTime;
                // Calculate the time since the last second
                int secondsPassed = Environment.TickCount - previousSecond;
                // Do something every frame update
                if (timePassed >= FRAME_TIME)
                {
                    previousTime = Environment.TickCount;

                    SetupLevel();
                    ZombieExist();
                    newLevel = false;
                    SetupHealthBar();
                    SetupHealthPack();
                    MoveHero();
                    ZombieHit();
                    ZombieMovement();
                    CheckCollisions();
                    MoveProjectile();
                    ProjectileCoolDown();
                    Refresh();
                }
                // make program do something every second
                if (secondsPassed >= SECOND)
                {
                    previousSecond = Environment.TickCount;
                    SetupSpeedBoost();
                    LevelCoolDown();
                }
                
                Application.DoEvents();
            }

        }
        // Set up health bar graphics 
        private void SetupHealthBar()
        {
            healthBarHeroX = heroX;
            healthBarHeroY = heroY - 30;
            healthBarLocationHero = new PointF(healthBarHeroX, healthBarHeroY);
            healthBarSizeHero = new SizeF(heroHealth / 20, 10);
            healthBarBoundaryHero = new RectangleF(healthBarLocationHero, healthBarSizeHero);

            

            healthBarBoundaryHero.Size = healthBarSizeHero;


        }
        // Set up zombie movement patterns
        private void ZombieMovement()
        {
            
            // Determines orientation of all the zombies and the speed it has to chase the player
            for (int i = 0; i < zombieCap; i++)
            {

                zombieLeft [i] = false;
                zombieRight [i] = false;
                zombieDown[i] = false;
                zombieUp [i] = false;
                if (zombieLocation[i].X < heroLocation.X)
                {
                    zombieLocation[i].X = zombieLocation[i].X + ZOMBIE_SPEED;
                    zombieRight[i] = true;
                }
                else if (zombieLocation[i].X > heroLocation.X + 4)
                {
                    zombieLocation[i].X = zombieLocation[i].X - ZOMBIE_SPEED;
                    zombieLeft[i] = true;
                }
                else if (zombieLocation[i].Y < heroLocation.Y )
                {
                    zombieLocation[i].Y = zombieLocation[i].Y + ZOMBIE_SPEED;
                    zombieUp[i] = true;
                }
                else if (zombieLocation[i].Y > heroLocation.Y )
                {
                    zombieLocation[i].Y = zombieLocation[i].Y - ZOMBIE_SPEED;
                    zombieDown[i] = true;
                }
                zombieBoundary[i].Location = zombieLocation[i];
            }


        }
       
        
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // Draws the background of the game
            e.Graphics.DrawImage(Properties.Resources.BeigeBackground, backgroundBoundary);
            // Draws the title in the menu
            if (gameState == GAME_MENU)
            {
                e.Graphics.DrawImage(Properties.Resources.LivingDeadTitle, titleBoundary);
                e.Graphics.DrawString("Press space to start or press I for instructions", textFont, Brushes.Black, menuTextLocation);
            }          
            // Draws the instructions when the game state is at GAME_INSTRUCTIONS
            else if (gameState == GAME_INSTRUCTIONS)
            {
                e.Graphics.DrawImage(Properties.Resources.Instructions, instructionBoundary);
                e.Graphics.DrawString("Press space to start", textFont, Brushes.Black, instructionTextLocation);
            }
            // Display the following when the game starts
            else if (gameState == GAME_START) 
            {
                // When player finishes a level, there is an interval of time between levels
                if (levelCoolDownPeriod == true)
                {
                   
                    e.Graphics.DrawString("Time: " + coolDownTime, textFont, Brushes.Black, timeBoundary);
                }
                // Health packs spawn between levels
                for (int i = 0; i < MAX_HEALTH_PACK; i++)
                {
                    if (healthPackUsed[i] == true)
                    {
                        e.Graphics.DrawImage(Properties.Resources.HealthPack, healthPackBoundary[i]);
                    }
                }
                // Display speed boost when enabled
                if (enableSpeedBoost == true)
                {
                    e.Graphics.DrawImage(Properties.Resources.SpeedBoost, speedBoostBoundary);
                }
                
                // Display the score
                e.Graphics.DrawString("Score: " + score, textFont, Brushes.Black, scoreBoundary);
                
                // Draw Zombie graphics
                for (int i = 0; i < zombieCap; i ++)
                {
                   
                    if (zombieAlive[i] == true)
                    {
                        // make zombie face down if its moving down
                        if (zombieDown [i] == true)
                        {
                            e.Graphics.DrawImage(Properties.Resources.BoxHeadDown, zombieBoundary[i]);
                        }
                        // make zombie face left if its moving left
                        else if (zombieLeft[i] == true)
                        {
                            e.Graphics.DrawImage(Properties.Resources.BoxHeadLeft, zombieBoundary[i]);
                        }
                        // make zombie face right if its moving right
                        else if (zombieRight[i] == true)
                        {
                            e.Graphics.DrawImage(Properties.Resources.BoxHeadRight, zombieBoundary[i]);
                        }
                        // make zombie face up if its moving up
                        else if (zombieUp[i] == true)
                        {
                            e.Graphics.DrawImage(Properties.Resources.BoxHeadUp, zombieBoundary[i]);
                        }
                        
                    }
                    
                    
                }

                // Display the health of the hero and remaining health
                e.Graphics.DrawString("Hero Health: " + heroHealth + "/" + heroMaxHealth , textFont, Brushes.Black, 50, 600);
                // Make hero face left
                if (heroLeft == true)
                {
                    e.Graphics.DrawImage(Properties.Resources.HeroLeft, heroBoundary);
                }
                // Make hero face right
                else if (heroRight == true)
                {
                    e.Graphics.DrawImage(Properties.Resources.HeroRight, heroBoundary);
                }
                // make hero face down
                else if (heroDown == true)
                {
                    e.Graphics.DrawImage(Properties.Resources.HeroDown, heroBoundary);
                }
                // make hero move up
                else if (heroUp == true)
                {
                    e.Graphics.DrawImage(Properties.Resources.HeroUp, heroBoundary);
                }
                for (int i =0; i < NUMBER_OF_PROJECTILES; i ++)
                {
                    if (activateProjectile[i] == true)
                    {
                        e.Graphics.DrawImage(Properties.Resources.Fireball, projectileBoundary[i]);
                    }
                }
                // Draw player health bar
                e.Graphics.FillRectangle(Brushes.Green, healthBarBoundaryHero);
                
                // displays the level
                e.Graphics.DrawString("Level " + level, textFont, Brushes.Black, 800, 50);
                
            }
            
            
            
        }
        // Makes sure the hero orientation are all false before it determines its actual orientation
        private void HeroDirection()
        {
            heroLeft = false;
            heroRight = false;
            heroUp = false;
            heroDown = false;
        }
        // Set up instructions graphics data
        private void SetupInstructions()
        {
            instructionX = 40;
            instructionY = 30;
            instructionLocation = new PointF(instructionX, instructionY);
            instructionSize = new SizeF(900, 600);
            instructionBoundary = new RectangleF(instructionLocation, instructionSize);
        }
        

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // the following actions are displayed while the game state is in the menu
            if (gameState == GAME_MENU || gameState == GAME_INSTRUCTIONS)
            {
                // When the player presses space, the game starts and everything is set up
                if (e.KeyCode == Keys.Space)
                {
                    gameState = GAME_START;
                    SetupGame();
                    SetupHero();
                    SetupZombie();
                    ZombieExist();
                    Loop();


                }
                // Displays instructions
                else if (e.KeyCode == Keys.I)
                {
                    gameState = GAME_INSTRUCTIONS;
                    SetupInstructions();
                    Refresh();
                }
            }
            // following actions occur while the game state is at GAME_StARt
            if (gameState == GAME_START)
            {
                // If player wants to skip the cooldown interval in between level, the player can press space
                if (levelCoolDownPeriod == true)
                {
                    if (e.KeyCode == Keys.Space)
                    {
                        coolDownTime = COOL_DOWN_LIMIT - 1;
                    }
                    
                }
                // player moves left
                if (e.KeyCode == Keys.Left)
                {
                    HeroDirection();
                    OneDirectionOnly();
                    moveLeft = true;
                    heroLeft = true;
             
                    
                    
                }
                // player moves right
                else if (e.KeyCode == Keys.Right)
                {
                    HeroDirection();
                    OneDirectionOnly();
                    moveRight = true;
                    heroRight = true;
                }
                // player moves up
                else if (e.KeyCode == Keys.Up)
                {
                    HeroDirection();
                    OneDirectionOnly();
                    moveUp = true;
                    heroUp = true;
                   
                    
                }
                // player moves down
                else if (e.KeyCode == Keys.Down)
                {
                    HeroDirection();
                    OneDirectionOnly();
                    moveDown = true;
                    heroDown = true;
                 
                    
                }
                // When player hits f, projectile will fire and every interval of time, you can fire again and again
                if (e.KeyCode == Keys.F)
                {
                    if (projectileMotion[projectileCounter] == false && enableProjectileCoolDown == false)
                    {
                        SetupProjectile();
                        CreateProjectile();
                        activateProjectile[projectileCounter] = true;
                        projectileMotion[projectileCounter] = true;
                        projectileCounter++;
                        enableProjectileCoolDown = true;
                    }


                    // Determines amount of projectiles that it can display at once
                    if (projectileCounter == NUMBER_OF_PROJECTILES)
                    {
                        projectileCounter = 0;
                    }

                }
            }
            
        }
        // When projectile is fired, there is a cooldown in between firing
        private void ProjectileCoolDown()
        {
            // when the projectile fires, there is a cooldown
            if (enableProjectileCoolDown == true)
            {
                projectileCoolDown++;
            }
            // When projectile cooldown reaches the max, projectile is ready to be fired again
            if (projectileCoolDown == MAX_PROJECTILE_COOLDOWN )
            {
                enableProjectileCoolDown = false;
                projectileCoolDown = 0; 
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            // the following actions can occur while the game state is at GAME_START
            if (gameState == GAME_START)
            {
                // When the left key is let go, player stops moving left
                if (e.KeyCode == Keys.Left)
                {
                    moveLeft = false;
                    
                }
                // when the right key is let go, player stops moving right
                else if (e.KeyCode == Keys.Right)
                {
                    moveRight = false;
                    

                }
                // when the up key is let go, player stops moving up
                else if (e.KeyCode == Keys.Up)
                {
                    moveUp = false;
                    
                }
                // when the down key is let go, player stops moving down
                else if (e.KeyCode == Keys.Down)
                {
                    moveDown = false;
                }
            }
        }
    }
}
