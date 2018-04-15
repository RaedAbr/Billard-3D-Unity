using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public enum BallsType { solids, stripes }
    public enum PlayerName { Player1, Player2 };

    public class Player
    {
        public PlayerName Name { get; set; }
        public BallsType BallsType { get; set; }

        public Player(PlayerName name)
        {
            this.Name = name;
        }
    }
}
