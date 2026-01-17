using System.ComponentModel;
using System.Data;

namespace ZWL.BLL
{
    public interface IBaseModel
    {
        int ID { get; set; }
        void SetPropertyValue(DataSet ds);
    }
    public class EnumCollection
    {
    }
    public enum Opration
    {
        [Description("添加")]
        Added = 1,
        [Description("修改")]
        Modified = 2,
        [Description("删除")]
        Deleted = 3,
        [Description("审批")]
        Approved = 4,
        [Description("提交")]
        Submit = 5,
        [Description("催办")]
        Prompt = 6,
        [Description("转发")]
        Forward = 7,
    }
    public enum Action
    {
        [Description("通过")]
        Agree = 1,
        [Description("驳回")]
        Return = 2,
        [Description("不通过")]
        Reject = 3,
    }

    public enum AptitudeType
    {
        [Description("正本")]
        Original = 1,
        [Description("副本")]
        Carbon = 2,
        [Description("正本复印件")]
        OriginalCopy = 3,
        [Description("副本复印件")]
        CarbonCopy = 4,
    }
    public enum FileType
    {
        OriginalCopy = 1,
        PhotoCopy = 2,
    }
    public enum AptitudeState
    {
        Returned = 0,
        Using = 1,
    }
    public enum Active
    {
        Inactive = 0,
        Active = 1,
    }
    public enum FlowOperation
    {
        UnSet = 0,
        Add = 1,
        Edit = 2,
        Delete = 3,
    }

    public enum Expression
    {
        /// <summary>
        /// 大于
        /// </summary>
        [Description("大于")]
        GT = 1,
        /// <summary>
        /// 大于或等于
        /// </summary>
        [Description("大于或等于")]
        GE = 2,
        /// <summary>
        /// 小于
        /// </summary>
        [Description("小于")]
        LT = 3,
        /// <summary>
        /// 小于或等于
        /// </summary>
        [Description("小于或等于")]
        LE = 4,
        /// <summary>
        /// 大于或等于并且小于
        /// </summary>
        [Description("大于或等于并且小于")]
        GEL = 5,
        /// <summary>
        /// 大于并且小于或等于
        /// </summary>
        [Description("大于并且小于或等于")]
        GLE = 6,
    }
}
