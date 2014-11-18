using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Tanks
{
    delegate void Del();

    // Модель. В этом классе инкапсулирована бизнес-логика приложения.
    class Model
    {
        public event Del StatusChangeEvent;

        #region Объявление переменных, свойств и ссылок

        int sizeField;
        int amountTanks;
        int amountApples;
        int speedGame;
        public int SpeedGame 
        { 
          get {return speedGame;}
          set { speedGame = value;} 
        }

        int collectedHeartsY = -45;
        int heartsWereCollected = 0;
        int numberOfHeartsToWin = 13;
        
        public GameStatus gameStatus;

        PackMan packMan;
        public PackMan PackMan { get { return packMan; } }

        Projectile projectile;
        public Projectile Projectile
        {
            get { return projectile; }
        }

        List<Tank> tanks;
        public List<Tank> Tanks
        {
            get { return tanks; }
        }


        public Wall wall;

        List<Heart> hearts;
        public List<Heart> Hearts
        {
            get { return hearts; }
        }

        Random random = new Random();

        #endregion


        public Model(int sizeField, int amountTanks, int amountApples, int speedGame)       // Constructor
        {
            this.sizeField = sizeField;
            this.amountTanks = amountTanks;
            this.amountApples = amountApples;
            this.speedGame = speedGame;
            NewGame();
        }

        private void CreateHearts()
        {
            CreateHearts(0);
        }
        
        private void CreateHearts(int heartsWereCollected)
        {
            int x, y;
            while (hearts.Count < amountApples + heartsWereCollected)
            {
                x = random.Next(6) * 90; 
                y = random.Next(6) * 90;
                bool fl = true;

                foreach (Heart item in hearts)
                    if (item.X == x && item.Y == y)
                    {
                        fl = false;
                        break;
                    }

                if (fl)
                    hearts.Add(new Heart(x, y));
            }
        }

        private void CreateTanks()
        {
            
            for (int i = 0; i < amountTanks; i++)
            {
                if (i == 0)
               
                    tanks.Add(new Hunter(sizeField, random.Next(6) * 90, random.Next(6) * 90));
              

                int x, y;   // стартовые координаты для танка
                do
                {
                    x = random.Next(sizeField - 44);
                    y = random.Next(sizeField - 44);
                }
                while(!areAppropriateCoordsForTanks(x,y));

                tanks.Add(new Tank(sizeField, x, y));
            }
        }

        // Метод определяет являются ли данные координаты подходящими для создания нового танка на уникальном месте и на перекрестке
        private bool areAppropriateCoordsForTanks(int x, int y)
        {
            bool isCrossroad;
            bool isUnique = true;

            if (Math.IEEERemainder(x, 90) == 0 && Math.IEEERemainder(y, 90) == 0)
                isCrossroad = true;
            else
                isCrossroad = false;

            foreach (Tank item in tanks)
                if (x == item.X && y == item.Y)
                {
                    isUnique = false;
                    break;
                }
               
            if (isCrossroad && isUnique)
                return true;
            else
                return false;
        }

        public void Play()
        {
            while (gameStatus == GameStatus.play)
            {
                Thread.Sleep(speedGame);

                packMan.Run();
                projectile.Run();

                ((Hunter)tanks[0]).Run(packMan.X, packMan.Y);

                for (int i = 1; i < tanks.Count; i++)
                    tanks[i].Run();

                KeepTrackOfMonsterCollisions();

                ProvideHeartsCollection();

                KeepTrackOfPackmanCollisions();

                RemoveDeadTanks();

                if (heartsWereCollected >= numberOfHeartsToWin)
                    gameStatus = GameStatus.winner;
                if (StatusChangeEvent != null)
                    StatusChangeEvent();
                
            }
        }

        private void RemoveDeadTanks()
        {
            for (int i = 0; i < tanks.Count; i++)
            {
                if (Math.Abs(projectile.X - tanks[i].X) < 30 && Math.Abs(projectile.Y - tanks[i].Y) < 30)
                    tanks[i].Die();
            }
            
        }

        private void KeepTrackOfPackmanCollisions() // Отслеживание столкновений Пакмена с танком.
        {
            for (int i = 0; i < tanks.Count; i++)
            {
                if (    // ситуация лобового столкновения
                        (Math.Abs(tanks[i].X - packMan.X) <= 40) && (tanks[i].Y == packMan.Y)
                        ||
                        (Math.Abs(tanks[i].Y - packMan.Y) <= 40) && (tanks[i].X == packMan.X)

                        ||
                    // ситуация углового столкновения
                        (Math.Abs(tanks[i].X - packMan.X) <= 30) && (Math.Abs(tanks[i].Y - packMan.Y) <= 30)
                    )
                {
                    gameStatus = GameStatus.loser;
                    if (StatusChangeEvent != null)
                        StatusChangeEvent();
                   
                }
            }
        }

        private void ProvideHeartsCollection()  // Подбор Пакменом сердечек.
        {
            for (int i = 0; i < Hearts.Count; i++)
            {
                if (packMan.X == hearts[i].X && packMan.Y == hearts[i].Y)
                {
                    heartsWereCollected++;
                    collectedHeartsY += 45;
                    hearts[i] = new Heart(595, collectedHeartsY);
                    CreateHearts(heartsWereCollected);
                }
                
            }
        }

        private void KeepTrackOfMonsterCollisions()    // Отслеживание столкновений вражеских сущностей
        {
            for (int i = 0; i < tanks.Count - 1; i++)
                for (int j = i + 1; j < tanks.Count; j++)
                    if (        // лобовое столкновение
                        (Math.Abs(tanks[i].X - tanks[j].X) <= 45) && (tanks[i].Y == tanks[j].Y)
                        ||
                        (Math.Abs(tanks[i].Y - tanks[j].Y) <= 45) && (tanks[i].X == tanks[j].X)

                                // или угловое столкновение
                        ||
                        (Math.Abs(tanks[i].X - tanks[j].X) <= 45) && (Math.Abs(tanks[i].Y - tanks[j].Y) <= 45)
                       )
                    {
                        if (tanks[i] is Hunter)
                            ((Hunter)tanks[i]).UTurn();
                        else
                            tanks[i].UTurn();
                        if (tanks[j] is Hunter)
                            ((Hunter)tanks[j]).UTurn();
                        else
                            tanks[j].UTurn();
                    }
        }


        internal void NewGame()
        {
            collectedHeartsY = -45;
            heartsWereCollected = 0;

            packMan = new PackMan(sizeField);
            projectile = new Projectile();

            tanks = new List<Tank>();
            CreateTanks();

            wall = new Wall();

            hearts = new List<Heart>();
            CreateHearts();

            gameStatus = GameStatus.stop;
            if (StatusChangeEvent != null)
                StatusChangeEvent();
        }
    }
}
