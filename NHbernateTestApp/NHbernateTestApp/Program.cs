using BPMS.Data;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NHbernateTestApp
{
    public static class ExpressionExtensions
    {
        public static Expression AndAlso(this Expression left, Expression right)
        {
            return Expression.AndAlso(left, right);
        }
        public static Expression Call(this Expression instance, string methodName, params Expression[] arguments)
        {
            return Expression.Call(instance, instance.Type.GetMethod(methodName), arguments);
        }
        public static Expression Property(this Expression expression, string propertyName)
        {
            return Expression.Property(expression, propertyName);
        }
        public static Expression GreaterThan(this Expression left, Expression right)
        {
            return Expression.GreaterThan(left, right);
        }
        public static Expression<TDelegate> ToLambda<TDelegate>(this Expression body, params  ParameterExpression[] parameters)
        {
            return Expression.Lambda<TDelegate>(body, parameters);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();

            //Task selectTask = new Task(p.Select);
            //selectTask.Start();

            //string cmd = Console.ReadLine();
            //if(cmd == "R")
            //{
            //    Thread getTop10Task = new Thread(p.GetTop20);
            //    getTop10Task.Start();
            //}
            //else if (cmd == "W")
            //{
            //    Thread updateTask = new Thread(p.Update);
            //    updateTask.Start();

            //    //Thread updateTask1 = new Thread(p.Update);
            //    //updateTask1.Start();
            //}

            //Task SelectBySqlTask = new Task(p.SelectBySql);
            //SelectBySqlTask.Start();

            //AnzhuangdanDataProvider anzhuangdanDataProvider = new AnzhuangdanDataProvider();
            //AnzhuangdanDataModel model = anzhuangdanDataProvider.GetByNHQ();

            //anzhuangdanDataProvider.Refresh(model);

            //Console.WriteLine(model.CreatedBy);

            //ConcurrentBagTest concurrentBagTest = new ConcurrentBagTest();

            //Task writeTask = new Task(concurrentBagTest.Wirte);
            //Task readTask = new Task(concurrentBagTest.Read);
            //Task removeTask = new Task(concurrentBagTest.Remove);

            //writeTask.Start();
            //readTask.Start();
            //removeTask.Start();

            //while (true)
            //{

            //    string cmd = Console.ReadLine();

            //    AnzhuangdanDataProviderTests anzhuangdanDataProviderTests = new AnzhuangdanDataProviderTests();
            //    //anzhuangdanDataProviderTests.Select();
            //    anzhuangdanDataProviderTests.GetTop();
            //    anzhuangdanDataProviderTests.SelectBySqlDataReader();
            //}

            AnzhuangdanDataProvider anzhuangdanDataProvider = new AnzhuangdanDataProvider();
            AnzhuangdanDataModel model = anzhuangdanDataProvider.SelectFirst(m => m.ID == 0);
            if (model != null)
            {
                Console.WriteLine(model.ID);
            }
            else
            {
                Console.WriteLine("null");
            }

            Console.ReadLine();
        }

        private void ParallelTest()
        {
            object addLock = new object();
            List<int> idList = new List<int>();
            for (int i = 0; i < 500000; i++)
            {
                idList.Add(i);
            }

            List<AnzhuangdanDataModel> caches = new List<AnzhuangdanDataModel>(idList.Count);

            Parallel.ForEach<int>(idList, new ParallelOptions { MaxDegreeOfParallelism = 8 }, (int id) =>
            {
                AnzhuangdanDataModel cache = this.Map(id);

                lock (addLock)
                {
                    caches.Add(cache);
                    if (caches.Count % 100000 == 0)
                    {
                        Console.WriteLine(caches.Count + " " + DateTime.Now);
                    }
                }
            });

            Console.WriteLine(idList.Count + ":" + caches.Count);
        }

        private AnzhuangdanDataModel Map(int id)
        {
            RandomDataFiller randomDataFiller = new RandomDataFiller();

            AnzhuangdanDataModel model = randomDataFiller.GetValue<AnzhuangdanDataModel>();
            model.ID = id;

            return model;
        }

        public void Select()
        {
            while(true)
            {
                AnzhuangdanDataProvider anzhuangdanDataProvider = new AnzhuangdanDataProvider();
                AnzhuangdanDataModel model = anzhuangdanDataProvider.Get(2397);
            }
        }

        public void SelectBySql()
        {
            while (true)
            {
                AnzhuangdanDataProvider anzhuangdanDataProvider = new AnzhuangdanDataProvider();
                anzhuangdanDataProvider.GetBySql(2397);
            }
        }

        public void GetTop20()
        {
            AnzhuangdanDataProvider anzhuangdanDataProvider = new AnzhuangdanDataProvider();
            while (true)
            {
                IList<AnzhuangdanDataModel> models = anzhuangdanDataProvider.SelectBySqlDataReader(null, "where Status< 10");

                List<int> idList = models.Select(m => m.ID).Distinct().ToList();
                Console.WriteLine("models.Count:" + models.Count + ",idList.Count:" + idList.Count);
                
                if(models.Count != idList.Count)
                {
                    Console.WriteLine("重复数据:" + (models.Count - idList.Count));

                    List<IGrouping<int, AnzhuangdanDataModel>> groupList = models.GroupBy(m => m.ID).Where(g => g.Count() > 1).ToList();
                    foreach(IGrouping<int, AnzhuangdanDataModel> group in groupList)
                    {
                        List<AnzhuangdanDataModel> repeartModels = group.ToList();
                        Console.WriteLine(string.Format("对比数据一致性,repeartModels.Count:{0} IndexOf:{1} {2}, ID:{3}",
                            repeartModels.Count, models.IndexOf(repeartModels[0]), models.IndexOf(repeartModels[1]), group.Key));
                        ObjectComparer comparer = new ObjectComparer();
                        try
                        {
                            comparer.Compare(repeartModels[0], repeartModels[1]);
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
        }

        public void Update()
        {
            int i = 0;
            AnzhuangdanDataProvider anzhuangdanDataProvider = new AnzhuangdanDataProvider();
            IList<AnzhuangdanDataModel> models = null;
            using (ISession session = NHibernateHelper.OpenSession())
            {
                models = session.QueryOver<AnzhuangdanDataModel>()
                    .Where(m => m.Status < 4)
                    //.OrderBy(m => m.ID)
                    //.Asc
                    .Take(1000)
                    .List();
            }
            Console.WriteLine("loaded");
            while (true)
            {
                foreach (AnzhuangdanDataModel model in models)
                {
                    using (ISession session = NHibernateHelper.OpenSession())
                    {
                        //ITransaction tran = session.BeginTransaction();
                        try
                        {
                            i++;
                            model.OriginParam02 = i.ToString();
                            session.Update(model);
                            session.Flush();
                            //tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            //tran.Rollback();
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
                Console.WriteLine("updated");
            }
        }
    }
}
