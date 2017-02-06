using AeronauticalSociety.Log;
using AeronauticalSociety.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AeronauticalSociety.BusinessLayer.Providers
{
    public class ConcernsProvider
    {
        #region 获取关注作者列表
        /// <summary>
        /// 获取关注作者列表
        /// </summary>
        /// <param name="Start"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public List<Concerns> GetConcernsList(int StartRow, int PageSize)
        {
            List<Concerns> result = new List<Concerns>();
            try
            {
                //获取当前用户
                AccountProvider _AccountProvider = new AccountProvider();
                var user = _AccountProvider.GetCurrentUser();
                if (user == null || user.id == 0)
                {
                    return result;
                }
                string sql = "select * from de2_concerns where createID=@userID order by createTime DESC";
                MySqlClient _client = new MySqlClient();
                result = _client.ExecuteQuery<Concerns>(sql.ToString(),
                   new MySqlParameter("@userID", user.id));
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取关注作者列表", "GetCollectionList", ex);
            }
            return result;
        }
        #endregion

        #region 添加关注
        /// <summary>
        /// 添加关注    
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public bool AddConcerns(Concerns Data)
        {
            try
            {
                //获取当前用户
                AccountProvider _AccountProvider = new AccountProvider();
                var user = _AccountProvider.GetCurrentUser();
                if (user == null || user.id == 0)
                {
                    return false;
                }
                //判断是否已经关注
                if (IsFocues(Data.authorName, user.id))
                {
                    return true;
                }

                Data.authorID = 0;
                Data.createID = user.id;
                Data.createTime = DateTime.Now;
                string sql = "insert into de2_concerns(authorID,authorName,createTime,createID) values(@authorID,@authorName,@createTime,@createID)";
                MySqlClient _MySqlClient = new MySqlClient();
                _MySqlClient.ExecuteNonQuery(sql, new MySqlParameter("@authorID", Data.authorID),
                      new MySqlParameter("@authorName", Data.authorName),
                    new MySqlParameter("@createTime", Data.createTime),
                    new MySqlParameter("@createID", Data.createID));
                return true;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "添加关注", "AddConcerns", ex);
                return false;
            }
        }
        #endregion

        #region 验证作者是否被关注
        /// <summary>
        /// 验证作者是否被关注
        /// </summary>
        /// <param name="author">作者</param>
        /// <param name="userid">当前用户</param>
        /// <returns></returns>
        public bool IsFocues(string author, int userid)
        {
            if (string.IsNullOrEmpty(author))
            {
                return false;
            }
            try
            {
                string sql = "select count(id) from de2_concerns where authorName=@authorName and createID=@createID";
                MySqlClient _Client = new MySqlClient();
                var count = Convert.ToInt32(_Client.ExecuteScalar(sql, new MySqlParameter("@authorName", author), new MySqlParameter("@createID", userid)));
                if (count > 0)
                {
                    return true;
                }

            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "验证作者是否被关注", "IsFocues", ex);
            }
            return false;
        }
        #endregion

        #region 删除关注
        /// <summary>
        /// 删除关注
        /// </summary>
        /// <param name="cid">主键</param>
        /// <returns></returns>
        public bool DelConcerns(int cid)
        {
            try
            {
                string sql = "delete from de2_concerns where id=@id";
                MySqlClient _MySqlClient = new MySqlClient();
                _MySqlClient.ExecuteNonQuery(sql, new MySqlParameter("@id", cid));
                return true;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "删除关注", "DelConcerns", ex);
                return false;
            }
        }
        #endregion

        #region 取消关注
        /// <summary>
        /// 取消关注
        /// </summary>
        /// <param name="author">作者</param>
        /// <returns></returns>
        public bool CancelConcerns(string author)
        {
            try
            {
                //获取当前登录人
                AccountProvider _AccountProvider = new AccountProvider();
                var user = _AccountProvider.GetCurrentUser();
                if (user == null)
                {
                    return false;
                }
                string sql = "delete from de2_concerns where authorName=@authorName  and createID=@createID";
                MySqlClient _Client = new MySqlClient();
                _Client.ExecuteNonQuery(sql, new MySqlParameter("@authorName", author), new MySqlParameter("@createID", user.id));
                return true;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "取消关注", "CancelConcerns", ex);
                return false;
            }
        }
        #endregion
    }
}
