using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPMS.Data
{

    public enum AcceptStatus
    {
        /// <summary>
        /// 未受理 0
        /// </summary>
        Unaccepted = 10,

        /// <summary>
        /// 已受理 1
        /// </summary>
        Accepted = 11
    }

    public class AnzhuangdanDataModel
    {
        public virtual int ID { set; get; }

        public virtual Guid DocGuid { set; get; }

        public virtual string DocNo { set; get; }

        public virtual int? Accept { set; get; }

        public virtual string ProvinceCode { set; get; }

        public virtual string CityCode { set; get; }

        public virtual string AreaCode { set; get; }

        public virtual string TownCode { set; get; }

        public virtual Guid DesignatedUnitGuid { set; get; }

        public virtual Guid AcceptGuid { set; get; }

        public virtual Guid FirstUnitGuid { set; get; }

        public virtual string DesignatedUser { set; get; }

        public virtual DateTime? DesignatedDate { set; get; }

        public virtual string Dispaching { set; get; }

        public virtual string AcceptUser { set; get; }

        public virtual DateTime? AcceptDate { set; get; }

        public virtual string CreatedBy { set; get; }

        public virtual DateTime? CreatedOn { set; get; }

        public virtual string DocType { set; get; }

        public virtual int Status { set; get; }

        public virtual int? delType { set; get; }

        public virtual int? reminder { set; get; }

        public virtual Guid ConsumerGuid { set; get; }

        public virtual string ConsumerName { set; get; }

        public virtual string Address { set; get; }

        public virtual string ConsumerMobile { set; get; }

        public virtual string ConsumerPhone { set; get; }

        public virtual string OperatorLog { set; get; }

        public virtual int? designatedMark { set; get; }

        public virtual int? operation { set; get; }

        public virtual int? StatusViste1 { set; get; }

        public virtual int? StatusViste2 { set; get; }

        public virtual DateTime? FinishDate { set; get; }

        public virtual Guid CreatedOrg { set; get; }

        public virtual string OriginCompany { set; get; }

        public virtual string OriginDocNo { set; get; }

        public virtual int OriginDocID { set; get; }

        public virtual string OriginMainGuid { set; get; }

        public virtual string OriginType { set; get; }

        public virtual string OriginParam01 { set; get; }

        public virtual string OriginParam02 { set; get; }

        public virtual int? IsStatements { set; get; }

        public virtual Guid ProductGuid { set; get; }

        public virtual string Product { set; get; }

        public virtual string ProductType { set; get; }

        public virtual Guid BrandGuid { set; get; }

        public virtual string Shop { set; get; }

        public virtual DateTime? BuyDate { set; get; }

        public virtual int SettlementType { set; get; }

        public virtual string Remark1 { set; get; }

        /// <summary>
        /// 协议类型
        /// </summary>
        public virtual string Product_I_Mold { set; get; }

        public virtual DateTime? ClosedDate { set; get; }

        /// <summary>
        /// 生产日期
        /// </summary>
        public virtual DateTime? ProductionDate { set; get; }
    }
}
