using AeronauticalSociety.Log;
using AeronauticalSociety.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AeronauticalSociety.BusinessLayer.Providers
{

    public class GetAdvertisementesParam : SearchParamBase
    {

    }
    /// <summary>
    /// 广告管理
    /// </summary>
    public class AdvertisementProvider
    {
        #region 获取广告列表
        /// <summary>
        /// 获取广告列表
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<Advertisement> GetAdvertisementList(int count)
        {
            List<Advertisement> result = new List<Advertisement>();
            try
            {
                string sql = string.Format("select * from de2_advertisement where  isVail=true order by `index` desc limit @limit");
                MySqlClient _client = new MySqlClient();
                result = _client.ExecuteQuery<Advertisement>(sql, new MySqlParameter("@limit", count));
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取广告列表", "GetAdvertisementList", ex);
            }
            return result;
        }
        #endregion

        #region 获取广告列表
        /// <summary>
        /// 获取广告列表
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public BaseResopne<List<Advertisement>> GetAdvertisementes(GetAdvertisementesParam Param)
        {
            BaseResopne<List<Advertisement>> result = new BaseResopne<List<Advertisement>>();
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder countSql = new StringBuilder();
                sql.AppendLine("select * from de2_advertisement");
                countSql.AppendLine("select count(id) from de2_advertisement");

                sql.AppendLine("order by `index` desc limit @start,@count");
                // string sql = string.Format("select * from de2_advertisement  order by index desc limit @limit");
                MySqlClient _client = new MySqlClient();

                result.Total = Convert.ToInt32(_client.ExecuteScalar(countSql.ToString()));
                result.Data = _client.ExecuteQuery<Advertisement>(sql.ToString(), new MySqlParameter("@start", Param.StartRow), new MySqlParameter("@count", Param.PageSize));
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取广告列表", "GetAdvertisementList", ex);
                result.IsSuccess = false;
                result.Error = ex.Message;
            }
            return result;
        }
        #endregion

        #region 获取广告详细信息
        /// <summary>
        /// 获取广告详细信息
        /// </summary>
        /// <param name="key">主键</param>
        /// <returns></returns>
        public Advertisement GetAdvertisement(int key)
        {
            Advertisement result = null;
            try
            {
                string sql = string.Format("select * from de2_advertisement where  id=@key");
                MySqlClient _client = new MySqlClient();
                result = _client.ExecuteQuery<Advertisement>(sql, new MySqlParameter("@key", key)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取广告详细信息", "GetAdvertisementList", ex);
            }
            return result;
        }
        #endregion

        #region 修改广告信息
        /// <summary>
        /// 修改广告信息
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public bool UpdateAdvertisement(Advertisement Data)
        {
            try
            {
                string sql = @" update  de2_advertisement set title=@title, targetUrl=@targetUrl, imageUrl=@imageUrl, imageBase64=@imageBase64, `index`=@`index`, lastModifyTime=@lastModifyTime where id=@id";
                Data.imageBase64 = GetImageBase64(Data.imageUrl);
                MySqlClient _MySqlClient = new MySqlClient();
                _MySqlClient.ExecuteNonQuery(sql, new MySqlParameter("@title", Data.title),
                    new MySqlParameter("@targetUrl", Data.targetUrl),
                    new MySqlParameter("@imageUrl", Data.imageUrl),
                    new MySqlParameter("@imageBase64", Data.imageBase64),
                    new MySqlParameter("@`index`", Convert.ToInt32(Data.index)),
                    new MySqlParameter("@lastModifyTime", DateTime.Now),
                      new MySqlParameter("@id", Data.id)
                    );
                return true;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "修改广告信息", "InsterAdvertisement", ex);
                return false;
            }
        }
        #endregion

        #region 读取图片
        /// <summary>
        /// 读取图片
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public string GetImageBase64(string Path)
        {
            string result = "";
            try
            {
                ///
                string imagePath = AppDomain.CurrentDomain.BaseDirectory + Path;
                //判断文件是否存在
                if (!File.Exists(imagePath))
                {
                    return result;
                }
                //
                byte[] file = File.ReadAllBytes(imagePath);
                result = Convert.ToBase64String(file);
            }
            catch (Exception ex)
            {

                throw;
            }
            return result;
        }
        #endregion

        #region 新建广告信息
        /// <summary>
        /// 新建广告信息
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public bool InsterAdvertisement(Advertisement Data)
        {
            try
            {
                string sql = @" INSERT into de2_advertisement(title, targetUrl, imageUrl, imageBase64, `index`, isVail,count,lastModifyTime,regTime) VALUES
                                      (@title, @targetUrl, @imageUrl, @imageBase64,@`index`, @isVail,@count,@lastModifyTime,@regTime)";
                Data.imageBase64 = GetImageBase64(Data.imageUrl);
                MySqlClient _MySqlClient = new MySqlClient();
                _MySqlClient.ExecuteNonQuery(sql, new MySqlParameter("@title", Data.title),
                    new MySqlParameter("@targetUrl", Data.targetUrl),
                    new MySqlParameter("@imageUrl", Data.imageUrl),
                    new MySqlParameter("@imageBase64", Data.imageBase64),
                    new MySqlParameter("@`index`", Convert.ToInt32(Data.index)),
                    new MySqlParameter("@isVail", true),
                    new MySqlParameter("@count", Convert.ToInt32(0)),
                    new MySqlParameter("@lastModifyTime", DateTime.Now),
                    new MySqlParameter("@regTime", DateTime.Now)
                    );
                return true;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "新建广告信息", "InsterAdvertisement", ex);
                return false;
            }
        }
        #endregion

        #region 停用广告信息
        /// <summary>
        /// 停用广告信息
        /// </summary>
        /// <param name="key">主键</param>
        /// <returns></returns>
        public Advertisement StopAdvertisement(int key)
        {
            Advertisement result = null;
            try
            {
                string sql = string.Format("update de2_advertisement set isVail=false where  id=@key");
                MySqlClient _client = new MySqlClient();
                result = _client.ExecuteQuery<Advertisement>(sql, new MySqlParameter("@key", key)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "停用广告信息", "DelAdvertisement", ex);
            }
            return result;
        }
        #endregion

        #region 启用广告信息
        /// <summary>
        /// 启用广告信息
        /// </summary>
        /// <param name="key">主键</param>
        /// <returns></returns>
        public Advertisement StartAdvertisement(int key)
        {
            Advertisement result = null;
            try
            {
                string sql = string.Format("update de2_advertisement set isVail=true where  id=@key");
                MySqlClient _client = new MySqlClient();
                result = _client.ExecuteQuery<Advertisement>(sql, new MySqlParameter("@key", key)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "启用广告信息", "StartAdvertisement", ex);
            }
            return result;
        }
        #endregion

        #region 删除广告信息
        /// <summary>
        /// 删除广告信息
        /// </summary>
        /// <param name="key">主键</param>
        /// <returns></returns>
        public Advertisement DelAdvertisement(int key)
        {
            Advertisement result = null;
            try
            {
                string sql = string.Format("delete from de2_advertisement where  id=@key");
                MySqlClient _client = new MySqlClient();
                result = _client.ExecuteQuery<Advertisement>(sql, new MySqlParameter("@key", key)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "删除广告信息", "DelAdvertisement", ex);
            }
            return result;
        }
        #endregion

        #region 记录广告点击次数
        /// <summary>
        /// 记录广告点击次数
        /// </summary>
        /// <param name="key">主键</param>
        /// <returns></returns>
        public Advertisement AdvertisementClickCount(int key)
        {
            Advertisement result = null;
            try
            {
                string sql = string.Format("update   de2_advertisement set count=(count+1) where  id=@key");
                MySqlClient _client = new MySqlClient();
                result = _client.ExecuteQuery<Advertisement>(sql, new MySqlParameter("@key", key)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "记录广告点击次数", "AdvertisementClickCount", ex);
            }
            return result;
        }
        #endregion
    }
}
