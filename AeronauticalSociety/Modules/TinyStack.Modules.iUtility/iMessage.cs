using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyStack.Modules.iUtility
{
    public class iMessage
    {
        #region 消息内容
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Message
        {
            get;
            set;
        }
        #endregion

        #region 是否发送给所有在线人
        /// <summary>
        /// 是否发送给所有在线人
        /// </summary>
        public bool isAllClient
        {
            get;
            set;
        }
        #endregion

        #region 接收人列表
        /// <summary>
        /// 接收人列表
        /// </summary>
        public List<Guid> ReceiverList
        {
            get;
            set;
        }
        #endregion
    }
}
