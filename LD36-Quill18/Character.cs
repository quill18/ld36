using System;

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
            this.MeleeDamageFunc = other.MeleeDamageFunc;
            this.RangedDamageFunc = other.MeleeDamageFunc;
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
        public Tile Tile { get; protected set; }

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
        public Func<int> MeleeDamageFunc { get; set; }
        public Func<int> RangedDamageFunc { get; set; }
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
            // Base character doesn't have anything to do 
            // unless we have poison mechanics or something.
        }

        public void TakeDamage(float dmg)
        {
            Health -= dmg;

            if (Health <= 0)
            {
                Die();
            }
        }

        virtual public void Die()
        {
            this.Tile.Character = null;
            this.Tile = null;
            this.Floor.RemoveCharacter(this);
        }

        public void MoveBy(int dX, int dY)
        {
            MoveTo(X + dX, Y + dY);
        }

        virtual public bool MoveTo(int newX, int newY)
        {
            // Check bounds
            if (newX < 0 || newX >= this.Floor.Width || newY < 0 || newY >= this.Floor.Height)
            {
                // Don't allow this move.
                return false;
            }

            Tile newTile = this.Floor.GetTile(newX, newY);

            // Is there an enemy standing in our target tile?
            if (newTile.Character != null)
            {
                MeleeAttack(newTile.Character);
                if (newTile.Character != null)
                {
                    // Character is still there. Abort move.
                    return false;
                }
            }
            // If so, we should try to melee attack it.
            // If that kills it, then we can move in.

            if (newTile.IsWalkable() == false)
            {
                // Not walkable.
                return false;
            }

            if (newTile.TileType == TileType.DOOR_CLOSED)
            {
                if (OpenDoor(newTile) == false)
                {
                    // failed to open door
                    return false;
                }
            }

            X = newX;
            Y = newY;
            UpdateTile();
            return true;
        }

        virtual public void FireTowards(int x, int y)
        {
            if (OnRangedAttack != null)
            {
                OnRangedAttack(this);
            }

            Tile[] tiles = AimingOverlay.GeneratePath(X, Y, x, y);

            // Hit the first thing we find.

            foreach (Tile t in tiles)
            {
                if (t.TileType == TileType.WALL || t.TileType == TileType.DOOR_CLOSED)
                {
                    // Whack.
                    return;
                }

                if (t.Character != null)
                {
                    int dmg = RangedDamageFunc();
                    if (this == Game.Instance.PlayerCharacter)
                    {
                        Game.Instance.Message(string.Format("You hit {0} for {1} damage!", t.Character.Name, dmg));
                    }
                    else {
                        Game.Instance.Message(string.Format("{0} hits you for {1} damage!", this.Name, dmg));
                    }

                    t.Character.TakeDamage(dmg);

                    return;
                }
            }

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

            int dmg = MeleeDamageFunc();
            target.TakeDamage( dmg );

            if (this == Game.Instance.PlayerCharacter)
            {
                Game.Instance.Message(string.Format("You hit {0} for {1} damage!", target.Name, dmg));
            }
            else {
                Game.Instance.Message(string.Format("{0} hits you for {1} damage!", this.Name, dmg));
            }

        }

        protected void UpdateTile()
        {
            if (this.Tile != null)
            {
                this.Tile.Character = null;
            }

            this.Tile = this.Floor.GetTile(X, Y);
            this.Tile.Character = this;
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

