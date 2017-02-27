using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BPMS.Data
{
    public class AnzhuangdanDataProvider : DataProvider<AnzhuangdanDataModel>
    {
        public override AnzhuangdanDataModel Get(object id)
        {
            AnzhuangdanDataModel model = null;
            using (ISession session = NHibernateHelper.OpenSession())
            {
                model = session.Get<AnzhuangdanDataModel>(id, LockMode.None);
            }

            return model;
        }

        public void GetBySql(object id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ISQLQuery query = session.CreateSQLQuery("select top 1 *from o_I_Service  ");
                query.List();
            }

        }

        public AnzhuangdanDataModel GetByNHQ()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                IQuery query = session.CreateQuery("from AnzhuangdanDataModel where docGuid = '5EA5D009-DF23-4DED-8875-A48B00242E46'");
                return query.UniqueResult<AnzhuangdanDataModel>();
            }

        }

        public int GetCountByNHQ()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                IQuery query = session.CreateQuery("select count(id) from AnzhuangdanDataModel  where DocGuidDocGuid = '5EA5D009-DF23-4DED-8875-A48B00242E46'");
                query.UniqueResult();
                return query.UniqueResult<int>();
            }

        }

        public void Refresh(object model)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                session.Refresh(model);
            }

        }

        //public override List<AnzhuangdanDataModel> GetTop(int count)
        //{
        //    List<AnzhuangdanDataModel> models = null;
        //    using (ISession session = NHibernateHelper.OpenSession())
        //    {
        //        models = session.QueryOver<AnzhuangdanDataModel>()
        //            .OrderBy(m => m.ID)
        //            .Desc
        //            .Take(count).List().ToList();
        //    }

        //    return models;
        //}

        public IList<AnzhuangdanDataModel> Select()
        {
            IList<AnzhuangdanDataModel> models = null;
            using (ISession session = NHibernateHelper.OpenSession())
            {
                models = session.QueryOver<AnzhuangdanDataModel>()
                    .List();
            }

            return models;
        }
    }
}
