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
            this.RangedAmmo = other.RangedAmmo;
            this.MaximumRange = other.MaximumRange;
            this.TriesToKeepMinimumDistance = other.TriesToKeepMinimumDistance;
        }

        public MonsterCharacter(Tile tile, Floor floor, Chixel chixel) : base(tile, floor, chixel)
        {
            throw new Exception();
        }

        public int RangedAmmo { get; set; }
        public int MaximumRange { get; set; }
        public int TriesToKeepMinimumDistance { get; set; }

        bool spottedPlayer = false;
        int targetX;
        int targetY;

        bool CanSeePlayer()
        {
            PlayerCharacter pc = Game.Instance.PlayerCharacter;
            Tile[] los = Utility.GeneratePath(X, Y, pc.X, pc.Y);
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

        public override void Die()
        {
            if (Tile.Item == null)
            {
                // Spawn a bit of scrap metal
                Item item = new Item();
                item.Name = "Metal Scraps";
                item.Description = "Useless by itself, but can be used to provide raw matter to an Augmentation Station.";
                item.Value = 50;
                item.Chixel = new Chixel('$', ConsoleColor.Yellow);
                Tile.Item = item;
            }

            base.Die();
        }

        public override void Update()
        {
            base.Update();

            if (Cooldown > 0)
                return;

            // Do AI
            PlayerCharacter pc = Game.Instance.PlayerCharacter;
            if (pc == null)
            {
                // No PC exists
                return;
            }

            bool haveLineOfSight = CanSeePlayer();
            if (haveLineOfSight)
            {
                spottedPlayer = true;
                targetX = pc.X;
                targetY = pc.Y;
            }
            else if (spottedPlayer == false)
            {
                return;
            }

            int playerDistance = Utility.CircleDistance(X, Y, targetX, targetY);

            // Do we need to run from the player?
            if (haveLineOfSight && TriesToKeepMinimumDistance >= playerDistance)
            {
                if (Update_MoveTowardsPlayer(false))
                {
                    // Successfully moved away.
                    return;
                }

                // If we failed, we'll try to shoot or melee instead
            }

            // Do we have ranged attacks & are in range?
            if (haveLineOfSight && playerDistance > 1 && RangedAmmo > 0 && MaximumRange >= playerDistance)
            {
                Update_ShootPlayer();
                return;
            }

            Update_MoveTowardsPlayer();
        }

        void Update_ShootPlayer()
        {
            FireTowards(targetX, targetY);

            Cooldown = 3; // Ranged attacks only happen every 3 ticks.
        }

        bool Update_MoveTowardsPlayer(bool runTowards = true)
        {
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

            if (!runTowards)
            {
                dX = -dX;
                dY = -dY;
            }

            if (MoveBy(dX, dY) == false)
            {
                // Try sidestepping instead?
                if (dX != 0 && MoveBy(dX, 0) == false)
                {
                    if (dY != 0 && MoveBy(0, dY) == false)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

    }
}

