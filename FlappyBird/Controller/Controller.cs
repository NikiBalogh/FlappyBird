using System;
using System.Collections.Generic;
using System.Text;
using FlappyBird.Model;

namespace FlappyBird.Controller
{
    class Controller
    {
        bool ifGameActive;
        DatabaseHandler databaseHandler = new DatabaseHandler(); 
        bool gotPoint = false;
        Box currentBox;
        List<Score> scoreList = new List<Score>();
        List<Player> playerList = new List<Player>();
        Player currentPlayer = new Player();
        List<Obstacle> obstacleList = new List<Obstacle>();

        public bool IfGameActive { get => ifGameActive; set => ifGameActive = value; }
        internal Box CurrentBox { get => currentBox; set => currentBox = value; }
        internal List<Obstacle> ObstacleList { get => obstacleList; set => obstacleList = value; }
        internal Player CurrentPlayer { get => currentPlayer; set => currentPlayer = value; }
        internal List<Player> PlayerList { get => playerList; set => playerList = value; }
        public bool GotPoint { get => gotPoint; set => gotPoint = value; }
        internal DatabaseHandler DatabaseHandler { get => databaseHandler; set => databaseHandler = value; }
        internal List<Score> ScoreList { get => scoreList; set => scoreList = value; }

        public void GameStart()
        {
            IfGameActive = true;
            Box box = new Box();
            currentBox = box;
        }
        
        public void PlayerInput()
        {
            CurrentBox.MoveUp(70);
        }

        public Obstacle GenerateObstacle()
        {
            Obstacle obstacle = new Obstacle();
            ObstacleList.Add(obstacle);
            return obstacle;
        }

        public List<Player> CreateUser(string Name, string Email, int PhoneNumber, List<Player> players)
        {
            if (currentPlayer.CheckIfPlayerNameExists(Name, players))
            {

            }
            else
            {
                Player player = new Player(Name.Trim(), Email.Trim(), PhoneNumber);
                players.Add(player);
                player.Score.PlayerId = players.Count;
                databaseHandler.InsertPlayer(player);
            }
            return players;
        }

        public Player FindCurrentPlayer(string Name, List<Player> players)
        {
            Player currentPlayer = new Player();
            foreach (Player player in players)
            {
                if (Name.ToLower().Trim() == player.Name.ToLower().Trim())
                {
                    currentPlayer = player;
                }
            }
            return currentPlayer;
        }

    }

}
