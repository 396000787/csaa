using AeronauticalSociety.Log;
using AeronauticalSociety.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AeronauticalSociety.BusinessLayer.Providers
{
    /// <summary>
    /// 收藏文章
    /// </summary>
    public class CollectionProvider
    {
        #region 获取收藏列表
        /// <summary>
        /// 获取收藏列表
        /// </summary>
        /// <param name="Start"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public List<Archives> GetCollectionList(int StartRow, int PageSize)
        {
            List<Archives> result = new List<Archives>();
            try
            {
                //获取当前用户
                AccountProvider _AccountProvider = new AccountProvider();
                var user = _AccountProvider.GetCurrentUser();
                if (user == null || user.id == 0)
                {
                    return result;
                }
                NewsProvider _NewsProvider = new NewsProvider();
                result = _NewsProvider.GetCollectionList(StartRow, PageSize, user.id);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取收藏列表", "GetCollectionList", ex);
            }
            return result;
        }
        #endregion

        #region 添加收藏
        /// <summary>
        /// 添加收藏    
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public bool AddColletion(Collection Data)
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
                //判断是否已经收藏
                if (IsCollection(Data.aid, user.id))
                {
                    return true;
                }
                Data.createID = user.id;
                Data.createTime = DateTime.Now;
                string sql = "insert into de2_collection(aid,createTime,createID) values(@aid,@createTime,@createID)";
                MySqlClient _MySqlClient = new MySqlClient();
                _MySqlClient.ExecuteNonQuery(sql, new MySqlParameter("@aid", Data.aid),
                    new MySqlParameter("@createTime", Data.createTime),
                    new MySqlParameter("@createID", Data.createID));
                return true;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "添加收藏", "AddColletion", ex);
                return false;
            }
        }
        #endregion

        #region 验证文章是否被收藏
        /// <summary>
        /// 验证文章是否被收藏
        /// </summary>
        /// <param name="aid">文章ID</param>
        /// <param name="userid">当前用户</param>
        /// <returns></returns>
        public bool IsCollection(int aid, int userid)
        {

            try
            {
                string sql = "select count(id) from de2_collection where aid=@aid and createID=@createID";
                MySqlClient _Client = new MySqlClient();
                var count = Convert.ToInt32(_Client.ExecuteScalar(sql, new MySqlParameter("@aid", aid), new MySqlParameter("@createID", userid)));
                if (count > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "验证文章是否被收藏", "IsCollection", ex);
            }
            return false;
        }
        #endregion

        #region 删除收藏
        /// <summary>
        /// 删除收藏
        /// </summary>
        /// <param name="cid">主键</param>
        /// <returns></returns>
        public bool DelColletion(int cid)
        {
            try
            {
                string sql = "delete from de2_collection where id=@id";
                MySqlClient _MySqlClient = new MySqlClient();
                _MySqlClient.ExecuteNonQuery(sql, new MySqlParameter("@id", cid));
                return true;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "删除收藏", "DelColletion", ex);
                return false;
            }
        }
        #endregion

        #region 取消收藏
        /// <summary>
        /// 取消收藏
        /// </summary>
        /// <param name="aid">文章编号</param>
        /// <returns></returns>
        public bool CancelColletion(int aid)
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
                string sql = "delete from de2_collection where aid=@aid  and createID=@createID";
                MySqlClient _Client = new MySqlClient();
                _Client.ExecuteNonQuery(sql, new MySqlParameter("@aid", aid), new MySqlParameter("@createID", user.id));
                return true;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "删除收藏", "DelColletion", ex);
                return false;
            }
        }
        #endregion
    }
}
