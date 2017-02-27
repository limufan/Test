using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionTestConsole
{
    class HashSetTests
    {
        public HashSetTests(User[] users)
        {
            this._users = new HashSet<User>(users);
            this._users.TrimExcess();
        }

        HashSet<User> _users;

        public void Contains(User user, int count)
        {
            Stopwatch watch = Stopwatch.StartNew();

            for (int i = 0; i < count; i++)
            {
                this._users.Contains(user);
            }
            watch.Stop();

            Console.WriteLine(this.GetType().Name + " Contains: " + watch.Elapsed.TotalMilliseconds);
        }

        public void Remove(User user, int count)
        {
            Stopwatch watch = Stopwatch.StartNew();

            for (int i = 0; i < count; i++)
            {
                this._users.Remove(user);
            }
            watch.Stop();

            Console.WriteLine(this.GetType().Name + " Remove: " + watch.Elapsed.TotalMilliseconds);
        }
    }
}
