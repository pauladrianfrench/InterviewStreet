namespace PointsInAPlaneV2
{
    using System.Collections.Generic;
    using System;

    public class ItemSet
    {
        uint indices;

        public ItemSet()
        {
            indices = 0;
            this.Count = 0;
        }

        public static long PowerOfTwo(int index)
        {
            switch (index)
            {
                case 0: return 1;
                 case 1: return 2;
                 case 2: return 4;
                case 3: return  8;
                 case 4: return 16;
                 case 5: return 32;
                 case 6: return 64;
                 case 7: return 128;
                 case 8: return 256;
                 case 9: return 512;
                 case 10: return 1024;
                 case 11: return 2048;
                 case 12: return 4096;
                 case 13: return 8192;
                 case 14: return 16384;
                 case 15: return 32768;
                 case 16: return 65536;
                case 17: return 131072;
                case 18: return 262144;
                case 19: return 524288;
                case 20: return 1048576;
               case 21: return  2097152;
                case 22: return 4194304;
                case 23: return 8388608;
                case 24: return 16777216;
               case 25: return  33554432;
               case 26: return  67108864;
               case 27: return  134217728;
                case 28: return 268435456;
               case 29: return  536870912;
               case 30: return  1073741824;
               case 31: return 2147483648;
             }
            return -1;
        }

        public static int LogBase2(uint mask)
        {
            switch (mask)
            {
                case 1: return 0;
                case 2: return 1;
                case 4: return 2;
                case 8: return 3;
                case 16: return 4;
                case 32: return 5;
                case 64: return 6;
                case 128: return 7;
                case 256: return 8;
                case 512: return 9;
                case 1024: return 10;
                case 2048: return 11;
                case 4096: return 12;
                case 8192: return 13;
                case 16384: return 14;
                case 32768: return 15;
                case 65536: return 16;
                case 131072: return 17;
                case 262144: return 18;
                case 524288: return 19;
                case 1048576: return 20;
                case 2097152: return 21;
                case 4194304: return 22;
                case 8388608: return 23;
                case 16777216: return 24;
                case 33554432: return 25;
                case 67108864: return 26;
                case 134217728: return 27;
                case 268435456: return 28;
                case 536870912: return 29;
                case 1073741824: return 30;
                case 2147483648: return 31;
            }
            return -1;
        }
        
        public bool HasItems(uint itms)
        {
            return (indices & itms) == itms;
        }

        public void Reset()
        {
            indices = 0;
            this.Count = 0;

        }

        public void SetUp(int count)
        {
            for (int i = 0; i < count; ++i)
            {
                Add(i);
            }
            Count = count;
        }

        public void Add(int i)
        {
            indices |= (uint)PowerOfTwo(i);
            this.Count++;

        }

        public void RemoveAt(int i)
        {
            indices = indices & (uint)~PowerOfTwo(i);
            this.Count--;
        }

        public uint GetIndices()
        {
            return indices;
        }

        public void SetIndices(uint ind)
        {
            indices = ind;
            Count = CountBits();
        }

        public ItemSet Clone()
        {
            ItemSet ret = new ItemSet();
            ret.indices = this.indices;
            ret.Count = this.Count;
            return ret;
        }

        public bool Equals(ItemSet set)
        {
            return this.indices == set.indices;
        }

        public int Count { get; set; }

        public int CountBits()
        {
            uint n = indices;
            int c; // c accumulates the total bits set in v
            for (c = 0; n > 0; c++)
            {
                n &= n - 1; // clear the least significant bit set
            }
            return c;
        }

        public T GetItem<T>(MyList<T> set, int index)
        {
            uint n = indices;

            int c; // c accumulates the total bits set in v
            uint alreadyRemoved = 0;
            for (c = 0; n > 0; c++)
            {
                n &= n - 1; // clear the least significant bit set
                uint removeThisCycle = indices - (indices & (n | alreadyRemoved));
                if (c == index)
                {
                    int idx = LogBase2(removeThisCycle);
                    return set[idx];
                }
                alreadyRemoved |= removeThisCycle;
            }

            throw new Exception("Item out of array bounds");
        }

        public List<T> GetList<T>(List<T> masterList)
        {
            List<T> ret = new List<T>();
            uint mask = 0x01;

            int nList = masterList.Count;
            int count = 0;
            for (int i = 0; i < nList; ++i)
            {
                if ((mask & indices) != 0)
                {
                    ret.Add(masterList[i]);
                    count++;
                }
                mask = mask << 1;
            }

            return ret;
        }

        public int GetItemIndex(int index)
        {
            uint n = indices;

            int c; // c accumulates the total bits set in v
            uint alreadyRemoved = 0;
            for (c = 0; n > 0; c++)
            {
                n &= n - 1; // clear the least significant bit set
                uint removeThisCycle = indices - (indices & (n | alreadyRemoved));
                if (c == index)
                {
                    int idx = LogBase2(removeThisCycle);
                    return idx;
                }
                alreadyRemoved |= removeThisCycle;
            }

            throw new Exception("Item out of array bounds");
        }
    }
}
