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
            if (_instance != null)
            {
                throw new Exception("More than one player?!?");
            }
            _instance = this;

            Faction = Faction.Player;

            Health = MaxHealth = 100;

            Items = new Item[26];

            Energy = 1000;
            EnergyPerMove = DefaultEnergyPerMove;
            EnergyPerMelee = DefaultEnergyPerMelee;
            EnergyPerRanged = DefaultEnergyPerRanged;

            MeleeDamageFunc = DefaultMeleeAttackDamage;
            RangedDamageFunc = DefaultRangedAttackDamage;
            OnRangedAttack += DefaultOnRangedAttack;
            OnMeleeAttack += DefaultOnMeleeAttack;

            EquippedItems = new Item[ Enum.GetValues(typeof(EquipSlot)).Length ];
        }

        public const int DefaultEnergyPerMelee = 2;
        public const int DefaultEnergyPerRanged = 10;
        public const int DefaultEnergyPerMove = 1;

        static public PlayerCharacter Instance
        {
            get
            {
                return _instance;
            }
        }

        public Item[] Items { get; set; }
        public int Money { get; set; }
        public int Energy { get; set; }
        public int EnergyPerMove { get; set; }
        public int EnergyPerMelee { get; set; }
        public int EnergyPerRanged { get; set; }

        public Item[] EquippedItems { get; set; }

        static private PlayerCharacter _instance;
        private int target_dX;
        private int target_dY;

        public override void Die()
        {
            base.Die();
            Game.Instance.PlayerCharacter = null;
        }

        public void QueueMoveBy(int dX, int dY)
        {
            target_dX = dX;
            target_dY = dY;
        }

        public override void Update()
        {
            base.Update();

            MoveBy(target_dX, target_dY);
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
            if (item != null)
            {
                AddItem(item);
            }


            return true;
        }

        public void AddItem(Item item)
        {
            // Pick it up!
            Tile.Item = null;

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
            this.Floor.RemoveCharacter(this);
            this.Floor = Game.Instance.Map.GetFloor(floorNum);
            this.Floor.AddCharacter(this);
            Game.Instance.Map.CurrentFloor = this.Floor;
            UpdateTile();
            RedrawRequests.FullRedraw();
        }

        public static int DefaultRangedAttackDamage()
        {
            return Game.Instance.Random.Next(1, 6);
        }

        public static int DefaultMeleeAttackDamage()
        {
            return Game.Instance.Random.Next(1, 6);
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
                Game.Instance.Message("Not enough energy to fire!");
                return;
            }

            base.MeleeAttack(target);
        }
    }
}

