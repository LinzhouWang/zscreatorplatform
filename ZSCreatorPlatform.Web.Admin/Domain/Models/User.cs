using System;

namespace ZSCreatorPlatform.Web.Admin.Domain.Models
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class User
    {

        #region Constructor

        /// <summary>
        /// 初始化一个User实例
        /// </summary>
        public User()
        {
            this.CreateTime = DateTime.Now;
            this.UpdateTime = DateTime.Now;
        }

        /// <summary>
        /// 初始化一个User实例
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <param name="address"></param>
        /// <param name="storeId"></param>
        public User(string name,string password,string address,int storeId):this()
        {
            this.Name = name;
            this.Password = password;
            this.Address = address;
            this.StoreId = storeId;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 主键id
        /// </summary>
        public int Id { get; private set; }
        
        /// <summary>
        /// 用户名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; private set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; private set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; private set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime UpdateTime { get; private set; }

        /// <summary>
        /// 所属分店id
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// 分店信息
        /// </summary>
        public Store Store { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// 修改用户名称
        /// </summary>
        /// <param name="userName"></param>
        public void UpdateName(string userName)
        {
            this.Name = userName;
            this.UpdateTime = DateTime.Now;
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="password"></param>
        public void UpdatePassword(string password)
        {
            this.Password = password;
            this.UpdateTime = DateTime.Now;
        }

        /// <summary>
        /// 修改用户地址
        /// </summary>
        /// <param name="address"></param>
        public void UpdateAddress(string address)
        {
            this.Address = address;
            this.UpdateTime = DateTime.Now;
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="address"></param>
        /// <param name="password"></param>
        public void Update(string name,string address,string password)
        {
            this.Name = name;
            this.Address = address;
            this.Password = password;
            this.UpdateTime = DateTime.Now;
        }

        #endregion
    }
}