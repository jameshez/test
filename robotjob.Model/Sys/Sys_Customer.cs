using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace robotjob.Model.Sys
{
    [Serializable]
    public class Sys_Customer
    {
        /// <summary>
        /// id
        /// </summary>
        private string _CustomerId;
        public string CustomerId
        {
            set { _CustomerId = value; }
            get { return _CustomerId; }
        }

        /// <summary>
        /// 用户昵称
        /// </summary>
        private string _NickName;
        public string NickName
        {
            set { _NickName = value; }
            get { return _NickName; }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        private string _CustomerName;
        public string CustomerName
        {
            set { _CustomerName = value; }
            get { return _CustomerName; }
        }


        /// <summary>
        /// 用户密码
        /// </summary>
        private string _CustomerPass;
        public string CustomerPass
        {
            get { return _CustomerPass; }
            set { _CustomerPass = value; }
        }

        /// <summary>
        /// 手机号
        /// </summary>
        private string _Phone;
        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }

        /// <summary>
        /// 邮箱
        /// </summary>
        private string _Email;
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        /// <summary>
        /// 手机是否激活
        /// </summary>
        private short? _IsActivPhone;
        public short? IsActivPhone
        {
            get { return _IsActivPhone; }
            set { _IsActivPhone = value; }
        }

        /// <summary>
        /// 邮箱是否激活
        /// </summary>
        private short? _IsActivEmail;
        public short? IsActivEmail
        {
            get { return _IsActivEmail; }
            set { _IsActivEmail = value; }
        }

        /// <summary>
        /// 用户类型，1为企业，2为用户
        /// </summary>
        private short? _CustomerType;
        public short? CustomerType
        {
            get { return _CustomerType; }
            set { _CustomerType = value; }
        }

        private DateTime? _adddate;
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime? AddDate
        {
            set { _adddate = value; }
            get { return _adddate; }
        }

    }
}
