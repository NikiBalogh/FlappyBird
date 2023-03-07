using System;
using System.Collections.Generic;
using System.Text;

namespace FlappyBird.Model
{
    class Score
    {
        int playerId;
        int points;

        public int PlayerId { get => playerId; set => playerId = value; }
        public int Points { get => points; set => points = value; }
    }
}
