using System;
using System.Threading;

namespace LD36Quill18
{
    public enum Faction { Enemy, Player }

    /// <summary>
    /// Player OR AI Character.
    /// </summary>
    public class Character
    {
        public Character()
        {
            // Used for instantiating prototypical characters, like monsters.
        }

        public Character(Character other)
        {
            this.Chixel = other.Chixel;
            this.MaxHealth = other.MaxHealth;
            this.Health = other.Health;
            this.MeleeDamage = other.MeleeDamage;
            this.RangedDamage = other.RangedDamage;
            this.OnMeleeAttack = other.OnMeleeAttack;
            this.OnRangedAttack = other.OnRangedAttack;
        }

        public Character(Tile tile, Floor floor, Chixel chixel)
        {
            this.Floor = floor;
            this.X = tile.X;
            this.Y = tile.Y;
            this.Tile = tile;

            this.Chixel = chixel;

            this.Floor.AddCharacter(this);
        }

        public int Cooldown { get; set; }

        public int X { get; protected set; }
        public int Y { get; protected set; }
        public Floor Floor
        {
            get
            {
                return _Floor;
            }
            set
            {
                if (value != _Floor)
                {
                    if (_Floor != null)
                    {
                        _Floor.RemoveCharacter(this);
                    }

                    _Floor = value;
                    _Floor.AddCharacter(this);
                }
            }
        }
        public Chixel Chixel { get; set; }
        public Tile Tile { get
            {
                return _Tile;
            }
            set
            {
                if (_Tile != null && _Tile.Character == this)
                    _Tile.Character = null;
                
                _Tile = value;

                if (_Tile != null)
                {
                    this.X = _Tile.X;
                    this.Y = _Tile.Y;
                    this.Floor = _Tile.Floor;
                    _Tile.Character = this;
                }
            }
        }
        private Tile _Tile;

        public Faction Faction { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        #region stat_block
        public float MaxHealth { get; set; }
        public float Health 
        { 
            get { return _Health; }
            set
            {
                _Health = value;
                if (_Health > MaxHealth)
                {
                    _Health = MaxHealth;
                }
            }
        }

        public int DodgeBonus { get; set; }
        public int ToHitBonus { get; set; }
        public int MeleeDamage { get; set; }    // Actual damage is random from 50% - 100%
        public int RangedDamage { get; set; }    // Actual damage is random from 50% - 100%
        public event Action<Character, Character> OnMeleeAttack;
        public event Action<Character> OnRangedAttack;
        public int DamageReduction { get; set; }

        private float _Health;
        #endregion

        private Floor _Floor;

        /// <summary>
        /// Called every tick.
        /// </summary>
        virtual public void Update()
        {
            if (Cooldown > 0)
                Cooldown--;
        }

        public int TakeDamage(float dmg)
        {
            dmg -= DamageReduction;
            if (dmg < 1)
                dmg = 1;

            Health -= dmg;

            if (Health <= 0)
            {
                Die();
            }

            return (int)dmg;
        }

        virtual public void Die()
        {
            this.Tile.Character = null;
            this.Tile = null;
            this.Floor.RemoveCharacter(this);
        }

        public bool MoveBy(int dX, int dY)
        {
            return MoveTo(X + dX, Y + dY);
        }

        virtual public bool MoveTo(int newX, int newY)
        {
            // Check bounds
            if (newX < 0 || newX >= this.Floor.Width || newY < 0 || newY >= this.Floor.Height)
            {
                // Don't allow this move.
                return false;
            }

            if (newX == X && newY == Y)
            {
                // No move requested.
                return true;
            }

            Tile newTile = this.Floor.GetTile(newX, newY);

            // Is there an enemy standing in our target tile?
            if (newTile.Character != null)
            {
                MeleeAttack(newTile.Character);
                if (newTile.Character != null)
                {
                    // Character is still there. Abort move,
                    // but tell the character to stop trying.
                    return true;
                }
            }
            // If so, we should try to melee attack it.
            // If that kills it, then we can move in.

            if (newTile.IsWalkable() == false)
            {
                // Not walkable.
                if (this == PlayerCharacter.Instance)
                {
                    if (newTile.TileType == TileType.DOOR_LOCKED)
                    {
                        Game.Instance.Message("This door is locked.");
                    }
                    else
                    {
                        Game.Instance.Message("**thud**");
                    }
                }
                return false;
            }

            if (newTile.TileType == TileType.DOOR_CLOSED)
            {
                if (OpenDoor(newTile) == false)
                {
                    // failed to open door
                    return false;
                }

                if (this == Game.Instance.PlayerCharacter)
                {
                    Game.Instance.Message("You open a door.");
                }
                else
                {
                    Game.Instance.Message("A door opens.");
                }
            }

            X = newX;
            Y = newY;
            Tile = Floor.GetTile(X, Y);
            return true;
        }

        void DrawRangedAttack( Tile[] tiles )
        {
            Floor.RedrawRequestedArea();
            Utility.DrawPath(tiles, true, ConsoleColor.Red);
            Floor.RedrawCharacters();
            FrameBuffer.Instance.DrawFrame();
            //Thread.Sleep(1000);
            Game.Instance.SleepFor = 500;
        }

        virtual public void FireTowards(int x, int y)
        {
            if (OnRangedAttack != null)
            {
                OnRangedAttack(this);
            }

            Tile[] tiles = Utility.GeneratePath(X, Y, x, y);

            // Hit the first thing we find.
            DrawRangedAttack(tiles);

            foreach (Tile t in tiles)
            {
                if (t.IsLookable() == false)
                {
                    // Whack.
                    if (this == PlayerCharacter.Instance)
                    {
                        Game.Instance.Message("Your shot hits a wall.");
                    }
                    return;
                }

                if (t.Character != null && t.Character.Faction == this.Faction)
                {
                    // No Damage, but still stop processing.
                    return;
                }

                if (t.Character != null && t.Character.Faction != this.Faction)
                {
                    int dmg = RollDamage(RangedDamage);
                    Character victim = t.Character;
                    dmg = victim.TakeDamage(dmg);

                    if (this == Game.Instance.PlayerCharacter)
                    {
                        Game.Instance.Message(string.Format("You shoot {0} for {1} damage!", victim.Name, dmg));
                    }
                    else {
                        Game.Instance.Message(string.Format("{0} shoots you for {1} damage!", this.Name, dmg));
                    }


                    return;
                }
            }

        }

        protected int RollDamage(int max)
        {
            return Game.Instance.Random.Next(max / 2, max + 1);
        }

        protected virtual void MeleeAttack(Character target)
        {
            if (this.Faction == target.Faction)
            {
                // We don't do friendly fire.
                return;
            }

            if (OnMeleeAttack != null)
            {
                OnMeleeAttack(this, target);
            }

            int attackRoll = Game.Instance.Random.Next(1, 21);

            if (attackRoll!=20 && (attackRoll + ToHitBonus) < target.DodgeBonus + 10)
            {
                if (this == Game.Instance.PlayerCharacter)
                {
                    Game.Instance.Message(string.Format("You missed {0}.", target.Name));
                }
                else {
                    Game.Instance.Message(string.Format("{0} tries to attack you, but misses.", this.Name));
                }
                return;
            }

            int dmg = RollDamage(MeleeDamage);
            dmg = target.TakeDamage(dmg);

            if (this == Game.Instance.PlayerCharacter)
            {
                Game.Instance.Message(string.Format("You hit {0} for {1} damage!", target.Name, dmg));
            }
            else {
                Game.Instance.Message(string.Format("{0} hits you for {1} damage!", this.Name, dmg));
            }

        }

        bool OpenDoor(Tile tile)
        {
            if (tile.TileType != TileType.DOOR_CLOSED)
            {
                throw new Exception();
            }

            tile.TileType = TileType.DOOR_OPENED;
            return true;
        }

    }
}

