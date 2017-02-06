using AeronauticalSociety.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyStack.Modules.iUtility.iTools;
using AeronauticalSociety.Log;
using AeronauticalSociety.BusinessLayer.SystemConstant;

namespace AeronauticalSociety.BusinessLayer.Providers
{
    public class MenuProvider
    {
        #region 获取主菜单数据
        /// <summary>
        /// 获取主菜单数据
        /// </summary>
        /// <returns></returns>
        public List<MenuModel> GetMainMenu()
        {
            List<MenuModel> result = new List<MenuModel>();
            try
            {
                //JsonDataHelp _JsonDataHelp = new JsonDataHelp();
                //result = _JsonDataHelp.GetJsonData<List<MenuModel>>(Constant.MainMenuJsonFile);
                DataCacheProvider _Provider = new DataCacheProvider();
                result = _Provider.GetCache(Constant.MainMenuJsonFile) as List<MenuModel>;
                if (result != null)
                {
                    result = result.OrderBy(a => a.Index).ToList();
                }
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取主菜单", "GetMainMenu", ex);
            }
            return result;
        }
        #endregion

        #region 获取新闻二级菜单
        /// <summary>
        /// 获取新闻二级菜单
        /// </summary>
        /// <returns></returns>
        public List<MenuModel> GetNewMenu()
        {
            List<MenuModel> result = new List<MenuModel>();
            try
            {
                //JsonDataHelp _JsonDataHelp = new JsonDataHelp();
                //result = _JsonDataHelp.GetJsonData<List<MenuModel>>(Constant.NewsMenuJsonFile);
                DataCacheProvider _Provider = new DataCacheProvider();
                result = _Provider.GetCache(Constant.NewsMenuJsonFile) as List<MenuModel>;
                if (result != null)
                {
                    result = result.OrderBy(a => a.Index).ToList();
                }
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取新闻二级菜单", "GetNewMenu", ex);
            }
            return result;
        }
        #endregion

        #region 获取学会动态菜单
        /// <summary>
        /// 获取学会动态菜单
        /// </summary>
        /// <returns></returns>
        public List<MenuModel> GetAssociationMenu()
        {
            List<MenuModel> result = new List<MenuModel>();
            try
            {
                //JsonDataHelp _JsonDataHelp = new JsonDataHelp();
                //result = _JsonDataHelp.GetJsonData<List<MenuModel>>(Constant.AssociationMenuJsonFile);
                DataCacheProvider _Provider = new DataCacheProvider();
                result = _Provider.GetCache(Constant.AssociationMenuJsonFile) as List<MenuModel>;
                if (result != null)
                {
                    result = result.OrderBy(a => a.Index).ToList();
                }
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取学会动态菜单", "GetAssociationMenu", ex);
            }
            return result;
        }
        #endregion

        #region 获取关于学会菜单
        /// <summary>
        /// 获取关于学会菜单
        /// </summary>
        /// <returns></returns>
        public List<MenuModel> GetAboutMenu()
        {
            List<MenuModel> result = new List<MenuModel>();
            try
            {
                //JsonDataHelp _JsonDataHelp = new JsonDataHelp();
                //result = _JsonDataHelp.GetJsonData<List<MenuModel>>(Constant.AboutJsonFile);
                DataCacheProvider _Provider = new DataCacheProvider();
                result = _Provider.GetCache(Constant.AboutJsonFile) as List<MenuModel>;
                if (result != null)
                {
                    result = result.OrderBy(a => a.Index).ToList();
                }
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取关于学会菜单", "GetAboutMenu", ex);
            }
            return result;
        }
        #endregion

        #region 获取管理条例二级目录
        /// <summary>
        /// 获取管理条例二级目录
        /// </summary>
        /// <returns></returns>
        public List<MenuModel> GetManagementRegulations()
        {
            List<MenuModel> result = new List<MenuModel>();
            try
            {
                //JsonDataHelp _JsonDataHelp = new JsonDataHelp();
                //result = _JsonDataHelp.GetJsonData<List<MenuModel>>(Constant.ManagementRegulationsJsonFile);
                DataCacheProvider _Provider = new DataCacheProvider();
                result = _Provider.GetCache(Constant.ManagementRegulationsJsonFile) as List<MenuModel>;
                if (result != null)
                {
                    result = result.OrderBy(a => a.Index).ToList();
                }
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取管理条例二级目录", "GetManagementRegulations", ex);
            }
            return result;
        }
        #endregion

        #region 获取新闻分类
        /// <summary>
        /// 获取新闻分类
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string GetNewTypeID(int ID)
        {
            var NewItems = GetNewMenu();
            string result = NewItems.Where(a => a.ID.Equals(ID)).Select(a => a.TypeID).FirstOrDefault();
            return result;
        }
        #endregion

        #region 获取学会动态分类
        /// <summary>
        /// 获取学会动态分类
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string GetAssociationTypeID(int ID)
        {
            var AssociationItems = GetAssociationMenu();
            string result = AssociationItems.Where(a => a.ID.Equals(ID)).Select(a => a.TypeID).FirstOrDefault();
            return result;
        }
        #endregion

        #region 获取关于学会分类
        /// <summary>
        /// 获取关于学会分类
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string GetAboutTypeID(int ID)
        {
            var AboutItems = GetAboutMenu();
            string result = AboutItems.Where(a => a.ID.Equals(ID)).Select(a => a.TypeID).FirstOrDefault();
            return result;
        }
        #endregion

        #region 获取学术资源菜单
        /// <summary>
        /// 获取学术资源菜单
        /// </summary>
        /// <returns></returns>
        public List<MenuModel> GetAcademicResourceMenu()
        {
            List<MenuModel> result = new List<MenuModel>();
            try
            {
                //JsonDataHelp _JsonDataHelp = new JsonDataHelp();
                //result = _JsonDataHelp.GetJsonData<List<MenuModel>>(Constant.MainMenuJsonFile);
                DataCacheProvider _Provider = new DataCacheProvider();
                result = _Provider.GetCache(Constant.AcademicResourceJsonFile) as List<MenuModel>;
                if (result != null)
                {
                    result = result.OrderBy(a => a.Index).ToList();
                }
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取学术资源菜单", "GetAcademicResourceMenu", ex);
            }
            return result;
        }
        #endregion

        #region 获取出版物分类
        /// <summary>
        /// 获取出版物分类
        /// </summary>
        /// <returns></returns>
        public List<MenuModel> GetPublicationTypeMenu()
        {
            List<MenuModel> result = new List<MenuModel>();
            try
            {
                //JsonDataHelp _JsonDataHelp = new JsonDataHelp();
                //result = _JsonDataHelp.GetJsonData<List<MenuModel>>(Constant.MainMenuJsonFile);
                DataCacheProvider _Provider = new DataCacheProvider();
                result = _Provider.GetCache(Constant.PublicationTypeJsonFile) as List<MenuModel>;
                if (result != null)
                {
                    result = result.OrderBy(a => a.Index).ToList();
                }
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取出版物分类", "GetPublicationTypeMenu", ex);
            }
            return result;
        }
        #endregion

        #region 获取组织机构分类列表
        /// <summary>
        /// 获取组织机构分类列表
        /// </summary>
        /// <returns></returns>
        public List<MenuModel> GetOrganizationalTypes()
        {
            List<MenuModel> result = new List<MenuModel>();
            try
            {
                DataCacheProvider _Provider = new DataCacheProvider();
                result = _Provider.GetCache(Constant.OrganizationalTypeJsonFile) as List<MenuModel>;
                if (result != null)
                {
                    result = result.OrderBy(a => a.Index).ToList();
                }
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取组织机构分类列表", "GetOrganizationalTypes", ex);
            }
            return result;
        }
        #endregion

        #region 获取组织机构分类ID
        /// <summary>
        /// 获取组织机构分类ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string GetOrganizationalTypeID(int ID)
        {
            var rganizationaltems = GetOrganizationalTypes();
            string result = rganizationaltems.Where(a => a.ID.Equals(ID)).Select(a => a.TypeID).FirstOrDefault();
            return result;
        }
        #endregion

        #region 获取管理条例列表
        /// <summary>
        /// 获取管理条例列表
        /// </summary>
        /// <param name="StartRow"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public List<MenuModel> GetManageRuleses()
        {
            List<MenuModel> Result = new List<MenuModel>();
            try
            {
                DataCacheProvider _Provider = new DataCacheProvider();
                Result = _Provider.GetCache(Constant.ManagementRegulationsJsonFile) as List<MenuModel>;
                if (Result != null)
                {
                    Result = Result.OrderBy(a => a.Index).ToList();
                }
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取管理条例列表", "GetManageRuleses", ex);
            }
            return Result;
        }
        #endregion

        #region 获取组织机构分类ID
        /// <summary>
        /// 获取组织机构分类ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string GetManageRulesID(int ID)
        {
            var ManageRulesesItems = GetManageRuleses();
            string result = ManageRulesesItems.Where(a => a.ID.Equals(ID)).Select(a => a.TypeID).FirstOrDefault();
            return result;
        }
        #endregion

        #region 获取学术资源菜单列表
        /// <summary>
        /// 获取学术资源菜单列表
        /// </summary>
        /// <param name="StartRow"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public List<MenuModel> GetAcademicResource()
        {
            List<MenuModel> Result = new List<MenuModel>();
            try
            {
                DataCacheProvider _Provider = new DataCacheProvider();
                Result = _Provider.GetCache(Constant.AcademicResourceJsonFile) as List<MenuModel>;
                if (Result != null)
                {
                    Result = Result.OrderBy(a => a.Index).ToList();
                }
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取学术资源菜单列表", "GetAcademicResource", ex);
            }
            return Result;
        }
        #endregion

    }
}
