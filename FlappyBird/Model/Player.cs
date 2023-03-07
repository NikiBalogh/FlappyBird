using System;
using System.Collections.Generic;
using System.Text;

namespace FlappyBird.Model
{
    public class Player
    {
        //Fields
        string name;
        int playCount;
        int phoneNumber;
        string email;
        Score score = new Score();

        //Constructor
        public Player(string name, int playCount, int phoneNumber, string email, int points)
        {
            Name = name;
            PlayCount = playCount;
            PhoneNumber = phoneNumber;
            Email = email;
            Score.Points = points;
        }
        public Player()
        {

        }

        public Player(string name, string email, int phoneNumber)
        {
            Name = name;
            PlayCount = 0;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        public Player(string name, string email, int phoneNumber, int playCount)
        {
            Name = name;
            PlayCount = playCount;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        //Properties
        public string Name { get => name; set => name = value; }
        public int PlayCount { get => playCount; set => playCount = value; }
        public int PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string Email { get => email; set => email = value; }
        internal Score Score { get => score; set => score = value; }

        //Methods
        public bool CheckIfPlayerNameExists(string Name, List<Player> players)
        {
            bool IfPlayerNameExist = false;
            foreach (Player player in players)
            {
                if (Name.ToLower().Trim() == player.Name.ToLower().Trim())
                {
                    IfPlayerNameExist = true;
                }
            }
            return IfPlayerNameExist;
        }
    }
}
