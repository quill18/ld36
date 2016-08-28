using System;

namespace LD36Quill18
{
    public enum TileType { FLOOR, WALL, DOOR_CLOSED, DOOR_OPENED, UPSTAIR, DOWNSTAIR, DEBRIS }

    public class Tile
    {
        const string TILE_GLYPHS = @" #+-<>X";

        public Tile(int x, int y, Floor floor, char textChar)
        {
            this.X = x;
            this.Y = y;
            this.Floor = floor;

            switch(textChar)
            {
                case ' ':
                    TileType = TileType.FLOOR;
                    break;
                case '#':
                    TileType = TileType.WALL;
                    break;
                case 'X':
                    TileType = TileType.DEBRIS;
                    Chixel.ForegroundColor = ConsoleColor.Gray;
                    break;
                case '+':
                    TileType = TileType.DOOR_CLOSED;
                    break;
                case '-':
                    TileType = TileType.DOOR_OPENED;
                    break;
                case '<':
                    TileType = TileType.UPSTAIR;
                    Floor.Upstair = this;
                    break;
                case '>':
                    TileType = TileType.DOWNSTAIR;
                    Floor.Downstair = this;
                    break;
                case '@':
                    TileType = TileType.FLOOR;

                    // Spawn a character (only if one doesn't exist)
                    Game.Instance.Message("Character spawned!");

                    if (Game.Instance.PlayerCharacter != null)
                    {
                        throw new Exception("Already have a player character!");
                    }
                    Game.Instance.PlayerCharacter = new PlayerCharacter( this, this.Floor, new Chixel('@') );
                    break;
                default:
                    // Everything else is either a monster or item
                    // so do that??
                    TileType = TileType.FLOOR;

                    if (textChar >= '0' && textChar <= '9')
                    {
                        if (FloorMaps.ItemSpawner[Floor.FloorIndex, (int)textChar - '0'] == null)
                        {
                            throw new Exception(string.Format("No item spawner for FloorIndex={0} and ID={1}", floor.FloorIndex, textChar));
                        }
                        FloorMaps.ItemSpawner[floor.FloorIndex, (int)textChar - '0'](this);
                        return;
                    }
                    else if (MonsterList.Monsters.ContainsKey(textChar))
                    {
                        // Yup, it's a monster.
                        MonsterCharacter mc = new MonsterCharacter(MonsterList.Monsters[textChar]);
                        mc.Tile = this;
                        return;
                    }
                    else if (ItemList.Items.ContainsKey(textChar))
                    {
                        // It's an item
                        this.Item = new Item(ItemList.Items[textChar]);
                        return;
                    }

                    //throw new Exception("No character entry for: " + textChar);
                    Game.Instance.DebugMessage("No character entry for: " + textChar);

                    Item item = new Item();
					item.Name = "Furniture";
					item.Description = "There is a word from some dead, long forgotten language engraved on it.";
					item.Chixel = new Chixel(textChar, ConsoleColor.Gray);
                    item.Static = true;
                    this.Item = item;


                    break;
            }

        }

        public TileType TileType { 
            get
            {
                return _TileType; 
            }
            set
            {
                _TileType = value;
                Chixel = new Chixel( TILE_GLYPHS[(int)_TileType] );
            }
        }

        public int X { get; protected set; }
        public int Y { get; protected set; }
        public Floor Floor { get; protected set; }
        public Chixel Chixel { get; protected set; }
        public Character Character { get; set; }
        public Item Item { get; set; }
        public bool WasSeen { get; set; }

        private TileType _TileType;

        public bool IsWalkable()
        {
            return TileType != TileType.WALL && TileType != TileType.DEBRIS;
        }

        public bool IsLookable()
        {
            return TileType != TileType.WALL && TileType != TileType.DEBRIS && TileType != TileType.DOOR_CLOSED;
        }


        public void Draw(int viewOffsetX, int viewOffsetY)
        {
            if (WasSeen == false)
            {
                FrameBuffer.Instance.SetChixel(X + viewOffsetX, Y + viewOffsetY, '\u2591');
                return;
            }

            if (Item != null)
            {
                // Draw item
                Item.Draw(X+viewOffsetX, Y+viewOffsetY);
                return;
            }

            FrameBuffer.Instance.SetChixel(X+viewOffsetX, Y+viewOffsetY, Chixel);
        }
    }
}

