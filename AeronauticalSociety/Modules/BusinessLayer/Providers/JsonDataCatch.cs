using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AeronauticalSociety.Log;
using System.IO;
using AeronauticalSociety.Model;
using AeronauticalSociety.BusinessLayer.SystemConstant;

namespace AeronauticalSociety.BusinessLayer.Providers
{
    /// <summary>
    /// 缓存Json数据
    /// </summary>
    public class JsonDataCatch
    {
        #region 缓存jsonData
        /// <summary>
        /// 缓存jsonData
        /// </summary>
        /// <returns></returns>
        public bool StartJsonDataCatch()
        {
            try
            {
                DataCacheProvider _DataCacheProvider = new DataCacheProvider();
                //清空缓存
                _DataCacheProvider.ClearCache();
                //
                SetCache<MenuModel>(_DataCacheProvider, Constant.MainMenuJsonFile);
                SetCache<MenuModel>(_DataCacheProvider, Constant.ManagementRegulationsJsonFile);
                SetCache<MenuModel>(_DataCacheProvider, Constant.NewsMenuJsonFile);
                SetCache<MenuModel>(_DataCacheProvider, Constant.OrganizationalTypeJsonFile);
                SetCache<MenuModel>(_DataCacheProvider, Constant.AssociationMenuJsonFile);
                SetCache<MenuModel>(_DataCacheProvider, Constant.AboutJsonFile);
                SetCache<MenuModel>(_DataCacheProvider, Constant.PublicationTypeJsonFile);
                SetCache<MenuModel>(_DataCacheProvider, Constant.AcademicResourceJsonFile);
                SetCache<Content>(_DataCacheProvider, Constant.ContentDataJsonFile);
                //缓存角色数据
                CacheRole(_DataCacheProvider);
                return true;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "缓存jsonData", "StartJsonDataCatch", ex);
                return false;
            }
        }
        #endregion

        #region 设置缓存
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Provider"></param>
        /// <param name="JSONPath"></param>
        /// <returns></returns>
        private bool SetCache<T>(DataCacheProvider Provider, string JSONPath)
        {
            try
            {
                List<T> result = new List<T>();
                JsonDataHelp _JsonDataHelp = new JsonDataHelp();
                result = _JsonDataHelp.GetJsonData<List<T>>(JSONPath);
                return Provider.SetCache(JSONPath, result);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "设置缓存", "SetCache", ex);
                return false;
            }
        }
        #endregion

        #region 缓存角色信息
        /// <summary>
        /// 缓存角色信息
        /// </summary>
        /// <returns></returns>
        private bool CacheRole(DataCacheProvider Provider)
        {
            try
            {
                string sql = "select * from de2_user_roletype";
                MySqlClient _Client = new MySqlClient();
              var result=  _Client.ExecuteQuery<RoleType>(sql);
              return Provider.SetCache("roetype", result); 
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "缓存角色信息", "CacheRole", ex);
                return false;
            }
            
        }
        #endregion
    }
}
