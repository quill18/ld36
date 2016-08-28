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

        public MonsterCharacter(MonsterCharacter other) : base(other)
        {
            this.Name = other.Name;
        }

        public MonsterCharacter(Tile tile, Floor floor, Chixel chixel) : base(tile, floor, chixel)
        {
            throw new Exception();
        }

        bool spottedPlayer = false;
        int targetX;
        int targetY;

        bool CanSeePlayer()
        {
            PlayerCharacter pc = Game.Instance.PlayerCharacter;
            Tile[] los = Map.GeneratePath(X, Y, pc.X, pc.Y);
            foreach (Tile t in los)
            {
                if (t.IsLookable() == false)
                {
                    // We can't see the player.
                    return false;
                }
            }
            return true;
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


            if (CanSeePlayer())
            {
                spottedPlayer = true;
                targetX = pc.X;
                targetY = pc.Y;
            }
            else if (spottedPlayer == false)
            {
                return;
            }

            int dX = 0;
            int dY = 0;

            if (targetX < this.X)
            {
                dX = -1;
            }
            if (targetY < this.Y)
            {
                dY = -1;
            }
            if (targetX > this.X)
            {
                dX = 1;
            }
            if (targetY > this.Y)
            {
                dY = 1;
            }

            MoveBy(dX, dY);
        }
    }
}

