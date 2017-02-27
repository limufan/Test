using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CollectionTestConsole
{
    class ReaderWriterLockedListTests
    {
        public ReaderWriterLockedListTests(User[] users)
        {
            this._users = new ReaderWriterLockedList<User>(users);
            this._user = users[users.Length / 2];
            this._whereTotalMillisecondsList = new List<double>();
            this._removeTotalMillisecondsList = new List<double>();
            this._addTotalMillisecondsList = new List<double>();
        }

        ReaderWriterLockedList<User> _users;
        User _user;

        List<double> _whereTotalMillisecondsList;
        List<double> _removeTotalMillisecondsList;
        List<double> _addTotalMillisecondsList;

        int _whereCount;
        int _removeCount;
        int _addCount;

        double _whereMax;
        double _removeMax;
        double _addMax;

        double _whereMin;
        double _removeMin;
        double _addMin;

        public double Where()
        {
            Stopwatch watch = Stopwatch.StartNew();

            //this._users.Where(user => user.Code == "李永强" && user.Guid.IndexOf("李永强") > -1);
            this._users.Where(user => user.Code == "李永强");

            watch.Stop();
            this._whereMax = Math.Max(this._whereMax, watch.Elapsed.TotalMilliseconds);
            this._whereMin = Math.Min(this._whereMin, watch.Elapsed.TotalMilliseconds);
            Interlocked.Increment(ref this._whereCount);

            return watch.Elapsed.TotalMilliseconds;
        }

        public double Remove()
        {
            Stopwatch watch = Stopwatch.StartNew();

            this._users.Remove(this._user);

            watch.Stop();

            this._removeMax = Math.Max(this._removeMax, watch.Elapsed.TotalMilliseconds);
            this._removeMin = Math.Min(this._removeMin, watch.Elapsed.TotalMilliseconds);
            Interlocked.Increment(ref this._removeCount); 

            return watch.Elapsed.TotalMilliseconds;
        }

        public double Add()
        {
            Stopwatch watch = Stopwatch.StartNew();

            this._users.Add(this._user);

            watch.Stop();

            this._addMax = Math.Max(this._addMax, watch.Elapsed.TotalMilliseconds);
            this._addMin = Math.Min(this._addMin, watch.Elapsed.TotalMilliseconds);
            Interlocked.Increment(ref this._addCount);

            return watch.Elapsed.TotalMilliseconds;
        }

        public void Where(int count)
        {
            Stopwatch watch = Stopwatch.StartNew();

            for (int i = 0; i < count; i++)
            {
                Task.Run(() => this.Where());
                Thread.Sleep(10);
            }
            watch.Stop();


            Console.WriteLine(this.GetType().Name + " Where: " + watch.Elapsed.TotalMilliseconds);
        }

        public void Remove(int count)
        {
            Stopwatch watch = Stopwatch.StartNew();

            for (int i = 0; i < count; i++)
            {
                Task.Run(() => this.Remove());
                Thread.Sleep(100);
            }
            watch.Stop();


            Console.WriteLine(this.GetType().Name + " Remove: " + watch.Elapsed.TotalMilliseconds);
        }

        public void Add(int count)
        {
            Stopwatch watch = Stopwatch.StartNew();

            for (int i = 0; i < count; i++)
            {
                Task.Run(() => this.Add());
                Thread.Sleep(100);
            }
            watch.Stop();


            Console.WriteLine(this.GetType().Name + " Add: " + watch.Elapsed.TotalMilliseconds);
        }

        public void Hunhe(int count)
        {
            Stopwatch watch = Stopwatch.StartNew();

            for (int i = 0; i < count; i++)
            {
                Task.Run(() => this.Add());
                Task.Run(() => this.Where());
                Task.Run(() => this.Where());
                Task.Run(() => this.Where());
                Task.Run(() => this.Where());
                Task.Run(() => this.Where());
                Task.Run(() => this.Where());
                Task.Run(() => this.Where());
                Task.Run(() => this.Where());
                Task.Run(() => this.Remove());
                Task.Run(() => this.Where());
                Task.Run(() => this.Where());
                Task.Run(() => this.Where());
                Task.Run(() => this.Where());
                Task.Run(() => this.Where());
                Task.Run(() => this.Where());
                Task.Run(() => this.Where());
                Task.Run(() => this.Where());
            }
            watch.Stop();


            Console.WriteLine(this.GetType().Name + " Add: " + watch.Elapsed.TotalMilliseconds);
        }

        public void Test()
        {
            Task.Run(() => this.Remove(100));
            Task.Run(() => this.Add(1000));
            Task.Run(() => this.Where(1000000));
            //Task.Run(() => this.Hunhe(1000000));
            while(true)
            {
                Console.WriteLine(string.Format("add max:{0}, remove max: {1}, where max: {2}",
                        this._addMax, this._removeMax, this._whereMax));
                
                Console.WriteLine(string.Format("add min:{0}, remove min: {1}, where min: {2}",
                        this._addMin, this._removeMin, this._whereMin));

                Console.WriteLine(string.Format("add count:{0}, remove count: {1}, where count: {2}",
                    this._addCount, this._removeCount, this._whereCount));

                this._addMax = this._removeMax = this._whereMax = 0;
                this._addMin = this._removeMin = this._whereMin = 1000;

                Thread.Sleep(5000);
            }
        }
    }
}
