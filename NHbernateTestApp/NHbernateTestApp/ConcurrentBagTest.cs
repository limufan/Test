using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace NHbernateTestApp
{
    class ConcurrentBagItem
    {
        public string Name { set; get; }
    }

    class ConcurrentBagTest
    {
        ConcurrentBag<ConcurrentBagItem> _bag = new ConcurrentBag<ConcurrentBagItem>();
        Random _random = new Random();

        public void Wirte()
        {
            while(true)
            {
                if(this._bag.Count > 1000000)
                {
                    Thread.Sleep(10000);
                }
                ConcurrentBagItem item = new ConcurrentBagItem { Name = Guid.NewGuid().ToString() };
                this._bag.Add(item);

            }
        }
        public void Remove()
        {
            while (true)
            {
                if(this._bag.Count > 0)
                {
                    ConcurrentBagItem item = this.GetRandom();
                    this.Remove(item);
                }

            }
        }

        public ConcurrentBagItem GetRandom()
        {
            int index = this._random.Next(0, this._bag.Count - 1);
            int i = 0;
            foreach (ConcurrentBagItem item in this._bag)
            {
                if (i == index)
                {
                    return item;
                }
                i++;
            }
            return null;
        }

        public void Remove(ConcurrentBagItem item)
        {
            this._bag = new ConcurrentBag<ConcurrentBagItem>(this._bag.Where(x => x != item));
        }

        public void Read()
        {
            while (true)
            {
                foreach (ConcurrentBagItem item in this._bag)
                {
                    Console.WriteLine(item.Name + ", count:" + this._bag.Count);
                }
                Console.WriteLine("===================================end========================================");
                Thread.Sleep(1000);
            }
        }
    }
}
