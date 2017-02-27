using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {

        static ObjectPropertySetter userPropertySetter;
        static int i = 0;

        static void Main(string[] args)
        {
            Get();

            Console.ReadLine();
        }

        static void Get()
        {
            Type type = typeof(User);
            userPropertySetter = new ObjectPropertySetter(type);

            int count = 5000000;

            List<User> users = new List<User>();
            for (int i = 0; i < count; i++)
            {
                users.Add(CreateUser2());
            }

            Stopwatch watch = Stopwatch.StartNew();

            for (int i = 0; i < count; i++)
            {
                GetUser1(users[i]);
            }
            watch.Stop();
            Console.WriteLine(watch.Elapsed.TotalSeconds);

            watch = Stopwatch.StartNew();

            for (int i = 0; i < count; i++)
            {
                GetUser2(users[i]);
            }
            watch.Stop();
            Console.WriteLine(watch.Elapsed.TotalSeconds);

        }

        static User GetUser1(User user)
        {
            Type type = typeof(User);

            type.GetProperty("Guid").GetValue(user);
            type.GetProperty("Name").GetValue(user);
            type.GetProperty("Age").GetValue(user);
            type.GetProperty("Age").GetValue(user);
            type.GetProperty("Age").GetValue(user);
            type.GetProperty("Age").GetValue(user);
            type.GetProperty("Age").GetValue(user);
            type.GetProperty("Age").GetValue(user);
            type.GetProperty("Age").GetValue(user);
            type.GetProperty("Age").GetValue(user);
            type.GetProperty("Age").GetValue(user);
            type.GetProperty("Age").GetValue(user);
            type.GetProperty("Age").GetValue(user);
            type.GetProperty("Age").GetValue(user);
            type.GetProperty("Age").GetValue(user);
            type.GetProperty("Age").GetValue(user);
            type.GetProperty("Age").GetValue(user);
            type.GetProperty("Age").GetValue(user);
            type.GetProperty("Age").GetValue(user);
            type.GetProperty("Age").GetValue(user);
            type.GetProperty("Age").GetValue(user);
            //type.GetProperty("Gongzi").SetValue(user, (double)new Random().Next(0, 1000));

            return (User)user;
        }

        static User GetUser2(User user)
        {
            Type type = typeof(User);

            userPropertySetter.GetObjectValue(user, "Guid");
            userPropertySetter.GetObjectValue(user, "Name");
            userPropertySetter.GetIntValue(user, "Age");
            userPropertySetter.GetIntValue(user, "Age");
            userPropertySetter.GetIntValue(user, "Age");
            userPropertySetter.GetIntValue(user, "Age");
            userPropertySetter.GetIntValue(user, "Age");
            userPropertySetter.GetIntValue(user, "Age");
            userPropertySetter.GetIntValue(user, "Age");
            userPropertySetter.GetIntValue(user, "Age");
            userPropertySetter.GetIntValue(user, "Age");
            userPropertySetter.GetIntValue(user, "Age");
            userPropertySetter.GetIntValue(user, "Age");
            userPropertySetter.GetIntValue(user, "Age");
            userPropertySetter.GetIntValue(user, "Age");
            userPropertySetter.GetIntValue(user, "Age");
            userPropertySetter.GetIntValue(user, "Age");
            userPropertySetter.GetIntValue(user, "Age");
            userPropertySetter.GetIntValue(user, "Age");
            userPropertySetter.GetIntValue(user, "Age");
            userPropertySetter.GetIntValue(user, "Age");

            return (User)user;
        }

        static void Create()
        {
            Type type = typeof(User);
            userPropertySetter = new ObjectPropertySetter(type);

            Stopwatch watch = Stopwatch.StartNew();
            int count = 1000000;
            //for (int i = 0; i < count; i++)
            //{
            //    CreateUser1();
            //}

            //watch.Stop();
            //Console.WriteLine(watch.Elapsed.TotalSeconds);


            watch = Stopwatch.StartNew();
            for (int i = 0; i < count; i++)
            {
                CreateUser2();
            }

            watch.Stop();
            Console.WriteLine(watch.Elapsed.TotalSeconds);
        }

        static User CreateUser1()
        {
            Type type = typeof(User);
            object user = Activator.CreateInstance(type);
            
            type.GetProperty("Guid").SetValue(user, Guid.NewGuid().ToString());
            type.GetProperty("Name").SetValue(user, Guid.NewGuid().ToString());
            type.GetProperty("Age").SetValue(user, i++);
            type.GetProperty("Age").SetValue(user, i++);
            type.GetProperty("Age").SetValue(user, i++);
            type.GetProperty("Age").SetValue(user, i++);
            type.GetProperty("Age").SetValue(user, i++);
            type.GetProperty("Age").SetValue(user, i++);
            type.GetProperty("Age").SetValue(user, i++);
            type.GetProperty("Age").SetValue(user, i++);
            type.GetProperty("Age").SetValue(user, i++);
            type.GetProperty("Age").SetValue(user, i++);
            type.GetProperty("Age").SetValue(user, i++);
            type.GetProperty("Age").SetValue(user, i++);
            type.GetProperty("Age").SetValue(user, i++);
            type.GetProperty("Age").SetValue(user, i++);
            type.GetProperty("Age").SetValue(user, i++);
            type.GetProperty("Age").SetValue(user, i++);
            type.GetProperty("Age").SetValue(user, i++);
            type.GetProperty("Age").SetValue(user, i++);
            type.GetProperty("Age").SetValue(user, i++);
            //type.GetProperty("Gongzi").SetValue(user, (double)new Random().Next(0, 1000));

            return (User)user;
        }
        static User CreateUser2()
        {
            Type type = typeof(User);
            object user = Activator.CreateInstance(type);

            userPropertySetter.Set(user, "Guid", Guid.NewGuid().ToString());
            userPropertySetter.Set(user, "Name", Guid.NewGuid().ToString());
            userPropertySetter.Set(user, "Age", i++);
            userPropertySetter.Set(user, "Age", i++);
            userPropertySetter.Set(user, "Age", i++);
            userPropertySetter.Set(user, "Age", i++);
            userPropertySetter.Set(user, "Age", i++);
            userPropertySetter.Set(user, "Age", i++);
            userPropertySetter.Set(user, "Age", i++);
            userPropertySetter.Set(user, "Age", i++);
            userPropertySetter.Set(user, "Age", i++);
            userPropertySetter.Set(user, "Age", i++);
            userPropertySetter.Set(user, "Age", i++);
            userPropertySetter.Set(user, "Age", i++);
            userPropertySetter.Set(user, "Age", i++);
            userPropertySetter.Set(user, "Age", i++);
            userPropertySetter.Set(user, "Age", i++);
            userPropertySetter.Set(user, "Age", i++);
            userPropertySetter.Set(user, "Age", i++);
            userPropertySetter.Set(user, "Age", i++);
            userPropertySetter.Set(user, "Age", i++);
            //userPropertySetter.Set(user, "Gongzi", (double)new Random().Next(0, 1000));

            return (User)user;
        }
        
    }
}
