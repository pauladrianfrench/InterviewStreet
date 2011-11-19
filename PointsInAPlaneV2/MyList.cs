namespace PointsInAPlaneV2
{
    public class MyList<T>
    {
        T [] items;
        int count;
        int increment;
        int size;

        public delegate bool MyDelegate(T param);

        public MyList()
        {
            size = 17;
            this.items = new T[size];
            count = 0;
            increment = 5;
        }

        public MyList(int initialSize, int increment = 10)
        {
            this.size = initialSize;
            this.items = new T[size];
            this.count = 0;
            this.increment = increment;
        }

        public int Count { get { return count; } }

        public T this[int i]
        {
            get
            {
                return items[i];
            }
            set
            {
                items[i] = value;
            }
        }

        public void Add(T p)
        {
            if (count+1 == size)
            {
                size += increment;
                T[] np = new T[size];
                for (int i = 0; i < count; i++)
                {
                    np[i] = this.items[i];
                }
                this.items = np;
            }
            items[count++] = p;
        }

        public void RemoveAt(int pos)
        {
            for (int i = pos; i < count; i++)
            {
                this.items[i] = this.items[i + 1];
            }
            count--;
        }

        public MyList<T> Clone()
        {
            MyList<T> ret = new MyList<T>(this.size, this.increment);
            ret.count = this.count;
            
            for (int i = 0; i < this.count; i++)
            {
                ret[i] = this[i];
            }
            return ret;
        }

        public bool Exists(MyDelegate searchFunc)
        {
            for (int i = 0; i < Count; i++)
            {
                if (searchFunc(items[i]))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
