using System;
namespace LD36Quill18
{
    /// <summary>
    /// Can be equippable or consumable. Can be on the ground or in inventory.
    /// </summary>
    public class Item
    {
        public Item()
        {
        }

        public Item(Item other)
        {
            this.Name = other.Name;
            this.Chixel = other.Chixel;
            this.OnPickup = other.OnPickup;
            this.OnUse = other.OnUse;
            this.UsesLeft = other.UsesLeft;
            this.EquipSlot = other.EquipSlot;
            this.OnEquip = other.OnEquip;
            this.OnUnequip = other.OnUnequip;
            this.Description = other.Description;
            this.Static = other.Static;
            this.IsKey = other.IsKey;
            this.Value = other.Value;
            this.IsFabricator = other.IsFabricator;
        }

        public Chixel Chixel { get; set; }
        public string Name { get; set; }
        public int Value = 0;   // Replaces IsMoney
        public bool IsKey = false;
        public event Action OnPickup;
        public event Action<Item> OnUse;
        public int UsesLeft { get; set; }
        public EquipSlot EquipSlot { get; set; }
        public string Description { get; set; }
        public bool Static { get; set; }
        public bool IsFabricator { get; set; }

        public string FullDescription { 
            get
            {
                string s = Name + ": ";
                if (OnEquip != null)
                {
                    s += "- Equippable: " + EquipSlot.ToString() + "\n";
                }
                s += Utility.WordWrap(Description);
                return s;
            }
        }


        public event Action<Item, PlayerCharacter> OnEquip;
        public event Action<Item, PlayerCharacter> OnUnequip;

        public void Draw(int x, int y)
        {
            FrameBuffer.Instance.SetChixel(x, y, Chixel);
        }

        public void Use()
        {
            if (OnUse != null)
            {
                OnUse(this);

                if (UsesLeft > 0)
                {
                    UsesLeft--;
                    if (UsesLeft <= 0)
                    {
                        PlayerCharacter.Instance.RemoveItem(this);
                    }
                }
            }
        }

        public void Equip(PlayerCharacter pc)
        {
            if (OnEquip != null)
                OnEquip(this, pc);
        }

        public void UnEquip(PlayerCharacter pc)
        {
            if (OnUnequip != null)
                OnUnequip(this, pc);
        }


    }
}

