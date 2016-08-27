using System;

namespace LD36Quill18
{
    public class Map
    {
        public Map()
        {
            CurrentFloorIndex = 0;


            floors = new Floor[FloorMaps.floor.Length];

            for (int i = 0; i < floors.Length; i++)
            {
                floors[i] = new Floor(FloorMaps.floor[i], i);
            }
        }

        public int CurrentFloorIndex { get; set; }

        public Floor CurrentFloor { 
            get
            {
                return floors[CurrentFloorIndex];
            }
            set
            {
                for (int i = 0; i < floors.Length; i++)
                {
                    if (floors[i] == value)
                    {
                        CurrentFloorIndex = i;
                        return;
                    }
                }

                throw new Exception("Didn't find floor.");
            }
        }

        public int NumFloors
        {
            get
            {
                return floors.Length;
            }
        }

        public Floor GetFloor(int index)
        {
            if (index < 0 || index >= floors.Length)
            {
                throw new Exception();
            }

            return floors[index];
        }

        private Floor[] floors;
    }
}

