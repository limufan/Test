using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            User[] users = new User[300000];
            for (int i = 0; i < users.Length; i++)
            {
                users[i] = new User { Guid = Guid.NewGuid().ToString(), Code = Guid.NewGuid().ToString() };
            }

            HashSetTests hashSetTests = new HashSetTests(users);
            ListTests listTests = new ListTests(users);
            List<HashSetTests> hashSetTestsList = new List<HashSetTests>();
            List<ListTests> listTestsList = new List<ListTests>();

            //hashSetTests.Contains(users[users.Length / 2], 1000);
            //listTests.Contains(users[users.Length / 2], 1000);

            //hashSetTests.Remove(users[users.Length / 2], 1000);
            //listTests.Remove(users[users.Length / 2], 1000);

            GC.Collect();

            Console.WriteLine("Loaded");
            while(true)
            {
                string cmd = Console.ReadLine();
                if (cmd == "CreateHashSetTests")
                {
                    hashSetTestsList.Add(new HashSetTests(users));
                }
                else if (cmd == "CreateListTests")
                {
                    listTestsList.Add(new ListTests(users));
                }
                else if (cmd == "ReaderWriterLockedListTests")
                {
                    
                }
                new ReaderWriterLockedListTests(users).Test();
                GC.Collect();
            }
        }
    }
}
