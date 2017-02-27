using BPMS.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHbernateTestApp
{
    class AnzhuangdanDataProviderTests
    {
        public void GetTop()
        {
            Stopwatch watch = Stopwatch.StartNew();

            AnzhuangdanDataProvider anzhuangdanDataProvider = new AnzhuangdanDataProvider();
            List<AnzhuangdanDataModel> models = anzhuangdanDataProvider.GetTop(800000);

            watch.Stop();
            Console.WriteLine(this.GetType().Name + ".GetTop:" + watch.Elapsed.TotalSeconds);
        }
        public void Select()
        {
            Stopwatch watch = Stopwatch.StartNew();

            AnzhuangdanDataProvider anzhuangdanDataProvider = new AnzhuangdanDataProvider();
            IList<AnzhuangdanDataModel> models = anzhuangdanDataProvider.Select();

            watch.Stop();
            Console.WriteLine(this.GetType().Name + ".GetTop:" + watch.Elapsed.TotalSeconds);
        }

        public void SelectBySqlDataReader()
        {
            Stopwatch watch = Stopwatch.StartNew();

            AnzhuangdanDataProvider anzhuangdanDataProvider = new AnzhuangdanDataProvider();
            IList<AnzhuangdanDataModel> models = anzhuangdanDataProvider.SelectBySqlDataReader(800000, "");

            watch.Stop();
            Console.WriteLine(this.GetType().Name + ".SelectBySqlDataReader:" + watch.Elapsed.TotalSeconds);
        }
    }
}
