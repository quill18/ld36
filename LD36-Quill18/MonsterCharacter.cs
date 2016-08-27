using System;
namespace LD36Quill18
{
    public class MonsterCharacter : Character
    {
        public MonsterCharacter()
        {
            // Used for instantiating prototypical monsters.
            Faction = Faction.Enemy;
        }

        public MonsterCharacter(MonsterCharacter other, Tile tile, Floor floor) : base(other)
        {
            this.Name = other.Name;
            this.Floor = floor;
            this.X = tile.X;
            this.Y = tile.Y;
            this.Tile = tile;
            tile.Character = this;
        }

        public MonsterCharacter(Tile tile, Floor floor, Chixel chixel) : base(tile, floor, chixel)
        {
            throw new Exception();
        }


        public override void Update()
        {
            base.Update();

            // Do AI
            PlayerCharacter pc = Game.Instance.PlayerCharacter;
            if (pc == null)
            {
                return;
            }

            int dX = 0;
            int dY = 0;

            if (pc.X < this.X)
            {
                dX = -1;
            }
            if (pc.Y < this.Y)
            {
                dY = -1;
            }
            if (pc.X > this.X)
            {
                dX = 1;
            }
            if (pc.Y > this.Y)
            {
                dY = 1;
            }

            MoveBy(dX, dY);
        }
    }
}

