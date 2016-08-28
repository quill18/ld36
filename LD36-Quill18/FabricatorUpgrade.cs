using System;
namespace LD36Quill18
{
    public class FabricatorUpgrade
    {
        public FabricatorUpgrade(string name, Action<PlayerCharacter> onPurchase, int nextUpdateCost=100)
        {
            this.Name = name;
            this.OnPurchase = onPurchase;
            this.NextUpgradeCost = nextUpdateCost;
        }

        public string Name;
        public Action<PlayerCharacter> OnPurchase;
        public int NextUpgradeCost;
    }
}

