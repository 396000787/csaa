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
    /// 年度工作计划
    /// </summary>
    public class WorkPlanProider
    {
        #region 获取年度工作计划
        /// <summary>
        /// 获取年度工作计划
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <param name="StartRow"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public BaseResopne<List<WorkPlan>> GetWorkPlanList(string Year, string Month, string StartRow, string PageSize, string Title, string StartTime, string EndTime)
        {
            BaseResopne<List<WorkPlan>> result = new BaseResopne<List<WorkPlan>>();
            result.IsSuccess = true;
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sqlCount = new StringBuilder();
                //获取工作计划
                sql.AppendLine(@"select p.id,`year`,startTime,endTime,p.`name`,content,scale,address,addressCity,addressCityID,
                                addressProvince,addressProvinceID,workPlanTypeID ,t.`Name` workPlanTypeName
                                from de2_workplan as p
                                left join de2_workplantype as t on p.workPlanTypeID=t.id where 1=1");
                sqlCount.AppendLine(@"select count( p.id) from de2_workplan as p where 1=1");

                //拼接where条件
                if (!string.IsNullOrEmpty(Year))
                {
                    sql.AppendLine("and `year`=@`year`");
                    sqlCount.AppendLine("and `year`=@`year`");
                }
                //月份
                if (!string.IsNullOrEmpty(Month))
                {
                    sql.AppendLine("and MONTH(startTime)<=@Month and MONTH(endTime)>=@Month");
                    sqlCount.AppendLine("and MONTH(startTime)<=@Month and MONTH(endTime)>=@Month");
                }
                //标题
                if (!string.IsNullOrEmpty(Title))
                {
                    sql.AppendLine("and  p.`name` like @title");
                    sqlCount.AppendLine("and  p.`name` like @title");
                }
                //开始时间
                if (!string.IsNullOrEmpty(StartTime))
                {
                    sql.AppendLine("and  startTime >=@StartTime");
                    sqlCount.AppendLine("and  startTime >=@StartTime");
                }

                //结束时间
                if (!string.IsNullOrEmpty(EndTime))
                {
                    sql.AppendLine("and  endTime <@EndTime");
                    sqlCount.AppendLine("and  endTime <@EndTime");
                    EndTime = Convert.ToDateTime(EndTime).AddDays(1).ToString();
                }

                int Start = 0;
                int Size = int.MaxValue;

                if (!string.IsNullOrEmpty(StartRow))
                {
                    Start = Convert.ToInt32(StartRow);
                }
                if (!string.IsNullOrEmpty(PageSize))
                {
                    Size = Convert.ToInt32(PageSize);
                }
                sql.AppendLine("limit @start,@count");
                MySqlClient _client = new MySqlClient();
                //获取总数
                result.Total = Convert.ToInt32(_client.ExecuteScalar(sqlCount.ToString(),
                    new MySqlParameter("@start", Start),
                    new MySqlParameter("@count", Size),
                    new MySqlParameter("@Month", Month),
                    new MySqlParameter("@title", "%" + Title + "%"),
                    new MySqlParameter("@StartTime", StartTime),
                    new MySqlParameter("@EndTime", EndTime),
                    new MySqlParameter("@`year`", Year)));
                //获取数据
                result.Data = _client.ExecuteQuery<WorkPlan>(sql.ToString(),
                    new MySqlParameter("@start", Start),
                    new MySqlParameter("@count", Size),
                    new MySqlParameter("@Month", Month),
                    new MySqlParameter("@title", "%" + Title + "%"),
                    new MySqlParameter("@StartTime", StartTime),
                    new MySqlParameter("@EndTime", EndTime),
                    new MySqlParameter("@`year`", Year));

                //获取联系人
                if (result.Data.Count > 0)
                {
                    foreach (var item in result.Data)
                    {
                        item.Contacts = GetWorkPlanContacts(item.id);
                    }
                }
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取年度工作计划", "GetSocietyIntroduction", ex);
                result.IsSuccess = false;
                result.Error = ex.Message;
            }
            return result;
        }
        #endregion

        #region 获取年度计划详情
        /// <summary>
        /// 获取年度计划详情
        /// </summary>
        /// <param name="wid"></param>
        /// <returns></returns>
        public WorkPlan GetWorkPlanDetial(int wid)
        {
            WorkPlan _result = new WorkPlan();
            try
            {
                //获取年度计划详情
                string sql = @"  select p.id,`year`,startTime,endTime,p.`name`,content,scale,address,addressCity,addressCityID,
                                addressProvince,addressProvinceID,workPlanTypeID ,t.`Name` workPlanTypeName
                                from de2_workplan as p
                                left join de2_workplantype as t on p.workPlanTypeID=t.id where p.id=@id";
                MySqlClient _Client = new MySqlClient();
                _result = _Client.ExecuteQuery<WorkPlan>(sql, new MySqlParameter("@id", wid)).FirstOrDefault();
                //获取联系人
                if (_result == null)
                {
                    return _result;
                }
                _result.Contacts = GetWorkPlanContacts(wid);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取年度计划详情", "GetWorkPlanDetial", ex);
            }
            return _result;
        }
        #endregion

        #region 获取联系人
        /// <summary>
        /// 获取联系人
        /// </summary>
        /// <param name="wpid"></param>
        /// <returns></returns>
        public List<WorkPlanContacts> GetWorkPlanContacts(int wpid)
        {
            List<WorkPlanContacts> result = new List<WorkPlanContacts>();
            try
            {
                //
                string sql = "select id ,phone,contactsName ,wid,'edit' as statue from de2_workplancontacts where wid=@wid";
                MySqlClient _Client = new MySqlClient();
                result = _Client.ExecuteQuery<WorkPlanContacts>(sql, new MySqlParameter("@wid", wpid));
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取联系人", "GetWorkPlanContacts", ex);
            }
            return result;
        }
        #endregion

        #region 新建工作计划
        /// <summary>
        /// 新建工作计划
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public bool AddWorkPlan(WorkPlan Data)
        {
            try
            {
                AccountProvider _AccountProvider = new AccountProvider();
                var user = _AccountProvider.GetCurrentUser();
                string sql = @"insert into de2_workplan  (`year`,startTime,endTime,name,content,scale,addressProvinceID,addressProvince,addressCityID,addressCity,address,createTime,lastModifyTime,createUserID,workPlanTypeID) 
                              values (@`year`,@startTime,@endTime,@name,@content,@scale,@addressProvinceID,@addressProvince,@addressCityID,@addressCity,@address,@createTime,@lastModifyTime,@createUserID,@workPlanTypeID)";
                Data.createTime = DateTime.Now;
                Data.lastModifyTime = DateTime.Now;
                if (user == null)
                {
                    Data.createUserID = user.id;
                }
                MySqlClient _Client = new MySqlClient();
                Data.id = (int)_Client.Inster(sql,
                        new MySqlParameter("@`year`", Data.year)
                       , new MySqlParameter("@startTime", Data.startTime)
                       , new MySqlParameter("@endTime", Data.endTime)
                       , new MySqlParameter("@name", Data.name)
                       , new MySqlParameter("@content", Data.content)
                       , new MySqlParameter("@scale", Data.scale)
                       , new MySqlParameter("@addressProvinceID", Data.addressProvinceID)
                       , new MySqlParameter("@addressProvince", Data.addressProvince)
                       , new MySqlParameter("@addressCityID", Data.addressCityID)
                       , new MySqlParameter("@addressCity", Data.addressCity)
                       , new MySqlParameter("@address", Data.address)
                       , new MySqlParameter("@createTime", Data.createTime)
                       , new MySqlParameter("@lastModifyTime", Data.lastModifyTime)
                       , new MySqlParameter("@createUserID", Data.createUserID)
                       , new MySqlParameter("@workPlanTypeID", Data.workPlanTypeID));

                SaveWorkPlanContacts(Data);
                return true;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "新建工作计划", "AddWorkPlan", ex);
                return false;
            }
        }
        #endregion

        #region 保存联系人
        /// <summary>
        /// 保存联系人
        /// </summary>
        /// <param name="Data">数据</param>
        /// <returns></returns>
        public bool SaveWorkPlanContacts(WorkPlan Data)
        {
            try
            {
                if (Data.Contacts == null || Data.Contacts.Count == 0)
                {
                    return true;
                }
                var result = false;
                foreach (var item in Data.Contacts)
                {
                    switch (item.statue.ToLower())
                    {
                        case "add":
                            result = AddWorkPlanContacts(item, Data.id);
                            break;
                        case "edit":
                            result = UpdateWorkPlanContacts(item);
                            break;
                        case "del":
                            result = DelWorkPlanContacts(item.id);
                            break;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "保存联系人", "SaveWorkPlanContacts", ex);
                return false;
            }
        }
        #endregion

        #region 新建工作计划联系人
        /// <summary>
        /// 新建工作计划联系人
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        private bool AddWorkPlanContacts(WorkPlanContacts Data, int wid)
        {
            try
            {
                string sql = "insert into de2_workplancontacts(contactsID,contactsName,phone,wid) values (@contactsID,@contactsName,@phone,@wid)";
                MySqlClient _Client = new MySqlClient();
                _Client.ExecuteNonQuery(sql,
                    new MySqlParameter("@contactsID", Data.contactsID)
                    , new MySqlParameter("@contactsName", Data.contactsName)
                    , new MySqlParameter("@phone", Data.phone)
                    , new MySqlParameter("@wid", wid));
                return true;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "新建工作计划联系人", "AddWorkPlanContacts", ex);
                return false;
            }
        }
        #endregion

        #region 修改工作计划联系人
        /// <summary>
        /// 修改工作计划联系人
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        private bool UpdateWorkPlanContacts(WorkPlanContacts Data)
        {
            try
            {
                string sql = "update de2_workplancontacts set contactsID=@contactsID,contactsName=@contactsName,phone=@phone where id=@id";
                MySqlClient _Client = new MySqlClient();
                _Client.ExecuteNonQuery(sql,
                    new MySqlParameter("@contactsID", Data.contactsID)
                    , new MySqlParameter("@contactsName", Data.contactsName)
                    , new MySqlParameter("@phone", Data.phone)
                     , new MySqlParameter("@id", Data.id));
                return true;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "修改工作计划联系人", "UpdateWorkPlanContacts", ex);
                return false;
            }
        }
        #endregion

        #region 删除联系人
        /// <summary>
        /// 删除联系人
        /// </summary>
        /// <param name="cid">联系人ID</param>
        /// <returns></returns>
        public bool DelWorkPlanContacts(int cid)
        {
            try
            {
                string sql = "delete from de2_workplancontacts where id=@id";
                MySqlClient _Client = new MySqlClient();
                _Client.ExecuteNonQuery(sql, new MySqlParameter("@id", cid));
                return true;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "删除联系人", "DelWorkPlanContacts", ex);
                return false;
            }
        }
        #endregion

        #region 修改工作计划
        /// <summary>
        /// 修改工作计划
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public bool UpdateWorkPlan(WorkPlan Data)
        {
            try
            {
                AccountProvider _AccountProvider = new AccountProvider();
                var user = _AccountProvider.GetCurrentUser();
                string sql = "update de2_workplan set `year`=@`year`,startTime=@startTime,endTime=@endTime,name=@name,content=@content,scale=@scale,addressProvinceID=@addressProvinceID,addressProvince=@addressProvince,addressCityID=@addressCityID,addressCity=@addressCity,address=@address,lastModifyTime=@lastModifyTime,workPlanTypeID=@workPlanTypeID where id=@id"; Data.lastModifyTime = DateTime.Now;
                if (user == null)
                {
                    Data.createUserID = user.id;
                }
                MySqlClient _Client = new MySqlClient();
                _Client.ExecuteNonQuery(sql,
                    new MySqlParameter("@`year`", Data.year)
                    , new MySqlParameter("@startTime", Data.startTime)
                    , new MySqlParameter("@endTime", Data.endTime)
                    , new MySqlParameter("@name", Data.name)
                    , new MySqlParameter("@content", Data.content)
                    , new MySqlParameter("@scale", Data.scale)
                    , new MySqlParameter("@addressProvinceID", Data.addressProvinceID)
                    , new MySqlParameter("@addressProvince", Data.addressProvince)
                    , new MySqlParameter("@addressCityID", Data.addressCityID)
                    , new MySqlParameter("@addressCity", Data.addressCity)
                    , new MySqlParameter("@address", Data.address)
                    , new MySqlParameter("@lastModifyTime", Data.lastModifyTime)
                    , new MySqlParameter("@workPlanTypeID", Data.workPlanTypeID)
                    , new MySqlParameter("@id", Data.id));
                SaveWorkPlanContacts(Data);
                return true;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "修改工作计划", "UpdateWorkPlan", ex);
                return false;
            }
        }
        #endregion

        #region 删除工作计划
        /// <summary>
        /// 删除工作计划
        /// </summary>
        /// <param name="wid">工作计划id</param>
        /// <returns></returns>
        public bool DelWorkPlan(int wid)
        {
            try
            {
                string sql = @"delete from de2_workplan where id=@id;
                delete from de2_workplancontacts where wid=@id";
                MySqlClient _Client = new MySqlClient();
                _Client.ExecuteNonQuery(sql, new MySqlParameter("@id", wid));

                return true;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "删除工作计划", "DelWorkPlan", ex);
                return false;
            }
        }
        #endregion
    }
}
