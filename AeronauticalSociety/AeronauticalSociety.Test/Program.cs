using AeronauticalSociety.BusinessLayer.Providers;
using AeronauticalSociety.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AeronauticalSociety.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string filename = "/test.xlsx";
            string dd = Path.GetFileName(filename);
            GetCityList();
        }

        private static void GetProvinceList()
        {
            NativePlaceProvider _Provider = new NativePlaceProvider();
            var reult = _Provider.GetProvinceList();
        }

        private static void GetCityList()
        {
            NativePlaceProvider _Provider = new NativePlaceProvider();
            var reult = _Provider.GetCityList("1000");
        }

        private static void AddWorkPlan()
        {
            WorkPlanProider _Provider = new WorkPlanProider();
            WorkPlan _WorkPlan = new WorkPlan();
            _WorkPlan.address = "sd";
            _WorkPlan.addressCity = "sdsd";
            _WorkPlan.addressCityID = 1;
            _WorkPlan.addressProvince = "sddd";
            _WorkPlan.addressProvinceID = 1;
            _WorkPlan.content = "sddd";
            _WorkPlan.scale = 10;
            _WorkPlan.startTime = DateTime.Now;
            _WorkPlan.endTime = DateTime.Now;
            _WorkPlan.workPlanTypeID = 1;
            _WorkPlan.year = "2017";
            _WorkPlan.name = "工作计划";
            WorkPlanContacts _WorkPlanContacts = new WorkPlanContacts();
            _WorkPlanContacts.phone = "13811961223";
            _WorkPlanContacts.statue = "add";
            _WorkPlanContacts.contactsName = "test1";
            _WorkPlan.Contacts.Add(_WorkPlanContacts);
            _Provider.AddWorkPlan(_WorkPlan);
        }

        private static void updateWorkPlan()
        {
            WorkPlanProider _Provider = new WorkPlanProider();
            WorkPlan _WorkPlan = new WorkPlan();
            _WorkPlan.id = 5;
            _WorkPlan.address = "sd";
            _WorkPlan.addressCity = "sdsd";
            _WorkPlan.addressCityID = 1;
            _WorkPlan.addressProvince = "sddd";
            _WorkPlan.addressProvinceID = 1;
            _WorkPlan.content = "sddd";
            _WorkPlan.scale = 10;
            _WorkPlan.startTime = DateTime.Now;
            _WorkPlan.endTime = DateTime.Now;
            _WorkPlan.workPlanTypeID = 1;
            _WorkPlan.year = "2017";
            _WorkPlan.name = "工作计划3";
            WorkPlanContacts _WorkPlanContacts = new WorkPlanContacts();
            _WorkPlanContacts.id = 2;
            _WorkPlanContacts.phone = "13811961223";
            _WorkPlanContacts.statue = "edit";
            _WorkPlanContacts.contactsName = "test2ee";
            _WorkPlan.Contacts.Add(_WorkPlanContacts);
            _Provider.UpdateWorkPlan(_WorkPlan);
        }

        private static void DelWorkPlan()
        {
            WorkPlanProider _Provider = new WorkPlanProider();
          
            _Provider.DelWorkPlan(5);
        }
        private static void Login()
        {
            AccountProvider _Provider = new AccountProvider();
            byte[] password = Encoding.UTF8.GetBytes("123456");
            LoginInParam _Param = new LoginInParam();
            _Param.Param1 = "test1";
            _Param.Param2 = Convert.ToBase64String(password);// "123456"
            _Provider.CheckIn(_Param);
        }


        private static void AddAdvertisement()
        {
            AdvertisementProvider _Provider = new AdvertisementProvider();
            Advertisement _Advertisement = new Advertisement();
            _Advertisement.count = 0;
            _Advertisement.imageBase64 = "121212swewewewe";
            _Advertisement.imageUrl = "ssdsddddsds";
            _Advertisement.index = 1;
            _Advertisement.isVail = true;
            _Advertisement.targetUrl = "sfsdeeewwdddd";
            _Advertisement.title = "asdfasdfasfasdfasfasdf";
            _Provider.InsterAdvertisement(_Advertisement);
        }

        private static void GetAbout()
        {
            NewsProvider _NewsProvider = new NewsProvider();
            //string result = _NewsProvider.GetSocietyIntroduction();
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        private static void RegUser()
        {
            AccountProvider _AccountProvider = new AccountProvider();
            User _User = new User();
            _User.loginName = "test2";
            _User.password = GetBase64String("123456");
            _AccountProvider.RegUser(_User);
        }

        #region 获取学会工作
        /// <summary>
        /// 获取学会工作
        /// </summary>
        public static void GetAssociation()
        {
            NewsProvider _NewsProvider = new NewsProvider();
            _NewsProvider.GetAssociation(0, 10);
        }

        #endregion

        #region 获取普通新闻
        /// <summary>
        /// 获取普通新闻
        /// </summary>
        public static void GetTextNews()
        {
            NewsProvider _NewsProvider = new NewsProvider();
            _NewsProvider.GetTextNews(0, 10);
        }
        #endregion

        #region 获取通知通告
        /// <summary>
        /// 获取通知通告
        /// </summary>
        public static void GetNotices()
        {
            NewsProvider _NewsProvider = new NewsProvider();
            _NewsProvider.GetNotices(0, 10);
        }
        #endregion

        #region 登录认证
        private static void LoginIn()
        {
            LoginInParam _LoginInParam = new LoginInParam();
            _LoginInParam.Param1 = "test2";
            _LoginInParam.Param2 = GetBase64String("123456");
            AccountProvider _AccountProvider = new AccountProvider();
            var result = _AccountProvider.CheckIn(_LoginInParam);
        }
        #endregion

        #region 绑定会员
        private static void BindMember()
        {
            BindMemberParam _BindMemberParam = new BindMemberParam();
            _BindMemberParam.MemberCode = "E281600750M";
            _BindMemberParam.UserName = "测试账号1";
            _BindMemberParam.UserKey = "3487f43f-b514-11e6-a9fa-00ffc891ca91";
            AccountProvider _AccountProvider = new AccountProvider();
            _AccountProvider.BindMember(_BindMemberParam);
        }
        #endregion

        private static string GetBase64String(string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            string result = Convert.ToBase64String(bytes);
            return result;
        }
    }
}
