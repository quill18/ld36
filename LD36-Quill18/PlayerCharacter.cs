using System;
namespace LD36Quill18
{
    public enum EquipSlot { 
        Head,               // Accuracy
        Chest,              // Max health / energy
        Arm,                // Primarily melee damage
        ShoulderLauncher,   // Ranged -- very resource intensive.
        Legs                // Dodging
    }
    // All slots also have a certain armor contribution

    public class PlayerCharacter : Character
    {
        public PlayerCharacter(Tile tile, Floor floor, Chixel chixel) : base(tile, floor, chixel)
        {
            _instance = this;

            Faction = Faction.Player;

            Tile = tile;

            Health = MaxHealth = 100;

            Items = new Item[26];

            Name = "BE-02";
            Description = "Covered in 4,000 years of dust, rust, and obsolescence.";

            Energy = 130;
            EnergyPerMove = DefaultEnergyPerMove;
            EnergyPerMelee = DefaultEnergyPerMelee;
            EnergyPerRanged = DefaultEnergyPerRanged;
            VisionRadius = DefaultVisionRadius;

            MeleeDamage = DefaultMeleeAttackDamage;
            RangedDamage = DefaultRangedAttackDamage;
            OnRangedAttack += DefaultOnRangedAttack;
            OnMeleeAttack += DefaultOnMeleeAttack;

            EquippedItems = new Item[ Enum.GetValues(typeof(EquipSlot)).Length ];

        }

        public const int DefaultEnergyPerMelee = 2;
        public const int DefaultEnergyPerRanged = 10;
        public const int DefaultEnergyPerMove = 1;
        public const int DefaultVisionRadius = 2;
        public const int DefaultRangedAttackDamage = 6;
        public const int DefaultMeleeAttackDamage = 6;


        static public PlayerCharacter Instance
        {
            get
            {
                return _instance;
            }
        }

        public Item[] Items { get; set; }
        public int Money { get; set; }

        public int Energy { 
            get
            {
                return _Energy;
            }
            set
            {
                _Energy = value;
                if (_Energy < 0)
                    _Energy = 0;
            }
        }

        public int EnergyPerMove { get; set; }
        public int EnergyPerMelee { get; set; }
        public int EnergyPerRanged { get; set; }

        public int VisionRadius { get; set; }

        public Item[] EquippedItems { get; set; }

        static private PlayerCharacter _instance;
        private int target_dX;
        private int target_dY;
        private bool queuedFireAt;
        private int _Energy;

        public override void Die()
        {
            base.Die();
            Game.Instance.PlayerCharacter = null;
            Game.Instance.BSOD();
        }

        public void QueueMoveBy(int dX, int dY)
        {
            target_dX = dX;
            target_dY = dY;
        }

        public void QueueFireAt(int dX, int dY)
        {
            queuedFireAt = true;
            target_dX = dX;
            target_dY = dY;
        }


        public override void Update()
        {
            base.Update();

            if (Energy <= 0)
            {
                Game.Instance.Message("POWER CELL DEPLETED -- Operating on Reserves.");
            }

            if (queuedFireAt)
            {
                queuedFireAt = false;
                FireTowards(target_dX, target_dY);
            }
            else {
                MoveBy(target_dX, target_dY);
            }

            target_dX = target_dY = 0;
        }

        public override bool MoveTo(int newX, int newY)
        {
            if (base.MoveTo(newX, newY) == false)
            {
                // Didn't actually move.
                return false;
            }

            Energy -= EnergyPerMove;

            // Are we on an item?
            Item item = Tile.Item;
            if (item != null && item.Static==false)
            {
                AddItem(item);
            }

            UpdateVision();

            return true;
        }

        public void UpdateVision()
        {
            // Set all tiles within our vision radius to WasSeen=true
            for (int x = -(VisionRadius); x <= VisionRadius; x++)
            {
                for (int y = -VisionRadius; y <= VisionRadius; y++)
                {
                    // Manhattan distance
                    //int d = Math.Abs(x) + Math.Abs(y);
                    int d = Utility.CircleDistance(0, 0, x, y);
                    if (d <= VisionRadius)
                    {
                        Tile t = Floor.GetTile(X + x, Y + y);
                        if (t != null)
                        {
                            t.WasSeen = true;
                            Floor.RedrawTileAt(X + x, Y + y);
                        }
                    }
                }
            }

        }

        public void AddItem(Item item)
        {
            // Pick it up!
            Tile.Item = null;

            Console.Beep();

            if (item.IsMoney)
            {
                // random amount of monies
                int m = Game.Instance.Random.Next(50, 100);
                this.Money += m;
                Game.Instance.Message(string.Format("Picked up {0} units of metal scraps.", m));
                return;
            }

            // Find first empty inventory slot
            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i] == null)
                {
                    Items[i] = item;
                    Game.Instance.Message(string.Format("Picked up: [{0}] {1}", (char)((int)'a' + i), item.Name));
                    return;
                }
            }

            Game.Instance.Message(string.Format("Can't pick up {0}. Inventory is full!", item.Name));
            Tile.Item = item;
        }

        public void RemoveItem(Item item)
        {
            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i] == item)
                {
                    Items[i] = null;
                    return;
                }
            }
        }

        public void UseItem(int i)
        {
            if (Items[i] != null)
            {
                Items[i].Use();
            }
        }

        public void Equip(Item item)
        {
            EquipSlot slot = item.EquipSlot;
            Item oldEquip = EquippedItems[(int)slot];

            if (oldEquip != null)
            {
                Unequip((int)slot);
            }

            EquippedItems[(int)slot] = item;
            RemoveItem(item);

            // Apply new effects
            item.Equip(this);
            Game.Instance.Message(string.Format("Equipped {0}", item.Name));
        }

        public void Unequip(int slot)
        {
            Item oldEquip = EquippedItems[(int)slot];
            if (oldEquip == null)
                return;

            EquippedItems[(int)slot] = null;
            oldEquip.UnEquip(this);
            AddItem(oldEquip);
            Game.Instance.Message(string.Format("Unequipped {0}", oldEquip.Name));
        }

        public void GoUp()
        {
            if (this.Tile.TileType != TileType.UPSTAIR)
            {
                // Not on up stair, ignore.
                return;
            }

            if (Floor.FloorIndex == 0)
            {
                // This would mean exiting the bunker. 
                throw new Exception("Exiting bunker not yet implemented.");
            }

            ChangeFloor(this.Floor.FloorIndex - 1);
        }

        public void GoDown()
        {
            if (this.Tile == null)
            {
                Game.Instance.DebugMessage("Null tile when descending stairs. Are you dead?");
                return;
            }

            if (this.Tile.TileType != TileType.DOWNSTAIR)
            {
                // Not on up stair, ignore.
                return;
            }

            if (Floor.FloorIndex == Game.Instance.Map.NumFloors - 1)
            {
                // Already at the bottom! There should be a stair here!
                throw new Exception("Already at the bottom! There shouldn't be a stair here!");
            }

            ChangeFloor(this.Floor.FloorIndex + 1);
        }

        void ChangeFloor(int floorNum)
        {
            bool goingDown = floorNum > Game.Instance.Map.CurrentFloorIndex;

            this.Floor.RemoveCharacter(this);
            this.Floor = Game.Instance.Map.GetFloor(floorNum);
            this.Floor.AddCharacter(this);
            Game.Instance.Map.CurrentFloor = this.Floor;

            if (goingDown)
                this.Tile = this.Floor.Upstair;  //.GetTile(Tile.X, Tile.Y);
            else
                this.Tile = this.Floor.Downstair;

            this.Floor.Recenter(true);
            this.UpdateVision();
        }


        public static void DefaultOnRangedAttack(Character src)
        {
            // This can only be called by the player character
            ((PlayerCharacter)src).Energy -= ((PlayerCharacter)src).EnergyPerRanged;
        }

        public static void DefaultOnMeleeAttack(Character src, Character target)
        {
            // This can only be called by the player character
            ((PlayerCharacter)src).Energy -= ((PlayerCharacter)src).EnergyPerMelee;
        }


        public override void FireTowards(int x, int y)
        {
            if (Energy <= EnergyPerRanged)
            {
                Game.Instance.Message("Not enough energy to fire!");
                return;
            }
            
            base.FireTowards(x, y);
        }

        protected override void MeleeAttack(Character target)
        {
            if (Energy <= EnergyPerMelee)
            {
                Game.Instance.Message("Not enough energy to attack!");
                return;
            }

            base.MeleeAttack(target);
        }
    }
}

