using AeronauticalSociety.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AeronauticalSociety.Log;
using AeronauticalSociety.Model;
using MySql.Data.MySqlClient;

namespace AeronauticalSociety.BusinessLayer.Providers
{
    /// <summary>
    /// 省市，地区
    /// </summary>
    public class NativePlaceProvider
    {
        #region 获取省列表
        /// <summary>
        /// 获取省列表
        /// </summary>
        /// <returns></returns>
        public List<NativePlace> GetProvinceList()
        {
            List<NativePlace> result = new List<NativePlace>();
            try
            {
                string sql = "select ename as Name,evalue as id from de2_sys_enum where egroup='nativeplace' and issign=0 order by disorder";
                MySqlClient _Client = new MySqlClient();
                result = _Client.ExecuteQuery<NativePlace>(sql);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取省列表", "GetProvinceList", ex);
            }
            return result;
        }
        #endregion

        #region 根据省ID 获取市
        /// <summary>
        /// 根据省ID 获取市
        /// </summary>
        /// <param name="ParentID">省ID</param>
        /// <returns></returns>
        public List<NativePlace> GetCityList(string ParentID)
        {
            List<NativePlace> result = new List<NativePlace>();
            try
            {
                if (string.IsNullOrEmpty(ParentID))
                {
                    return result;
                }
                ParentID = ParentID.Substring(0, (ParentID.Length - 2));
                string sql = @"select  ename as Name,evalue as id    from de2_sys_enum 
                 where egroup='nativeplace' and issign=1  and substring(evalue ,1,(LENGTH(evalue)-2))=@ParentID order by disorder ";
                MySqlClient _Client = new MySqlClient();
                result = _Client.ExecuteQuery<NativePlace>(sql, new MySqlParameter("@ParentID", ParentID));
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "根据省ID 获取市", "GetCityList", ex);
            }
            return result;
        }
        #endregion
    }
}
