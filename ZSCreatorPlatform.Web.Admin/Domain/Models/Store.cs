using System.Collections.Generic;

namespace ZSCreatorPlatform.Web.Admin.Domain.Models
{
    public class Store
    {
        #region Constructor

        public Store()
        {
            
        }

        public Store(string name,string address)
        {
            this.Name = name;
            this.Address = address;
        }

        #endregion

        #region Properties

        public int Id { get;private set; }

        /// <summary>
        /// 分店名称
        /// </summary>
        public string Name { get;private set; }

        /// <summary>
        /// 分店地址
        /// </summary>
        public string Address { get;private set;}

        /// <summary>
        /// 分店用户
        /// </summary>
        public IList<User> Users { get; } = new List<User>();

        #endregion

        #region Methods

        /// <summary>
        /// 更新门店信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="address"></param>
        public void UpdateStore(string name,string address)
        {
            this.Name = name;
            this.Address = address;
        }

        #endregion
    }
}