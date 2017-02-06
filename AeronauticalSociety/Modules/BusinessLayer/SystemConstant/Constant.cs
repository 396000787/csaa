using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace AeronauticalSociety.BusinessLayer.SystemConstant
{
    /// <summary>
    /// 系统常量
    /// </summary>
    public class Constant
    {
        #region JsonData文件地址

        /// <summary>
        /// 主菜单json文件名称
        /// </summary>
        public const string MainMenuJsonFile = "MainMenuData.json";

        /// <summary>
        /// 新闻菜单json文件名称
        /// </summary>
        public const string NewsMenuJsonFile = "NewsMenuData.json";

        /// <summary>
        /// 学会动态菜单json文件名称
        /// </summary>
        public const string AssociationMenuJsonFile = "AssociationMenuData.json";
        /// <summary>
        /// 关于学会菜单json文件名称
        /// </summary>
        public const string AboutJsonFile = "AboutData.json";

        /// <summary>
        /// 管理条例菜单json文件名称
        /// </summary>
        public const string ManagementRegulationsJsonFile = "ManagementRegulationData.json";

        /// <summary>
        ///  组织分类json文件名称
        /// </summary>
        public const string OrganizationalTypeJsonFile = "OrganizationalTypeData.json";

        /// <summary>
        ///  学术资源json文件名称
        /// </summary>
        public const string AcademicResourceJsonFile = "AcademicResourceData.json";

        /// <summary>
        ///  学术资源json文件名称
        /// </summary>
        public const string PublicationTypeJsonFile = "PublicationTypeData.json";

        /// <summary>
        ///  联系我们文件名称
        /// </summary>
        public const string ContentDataJsonFile = "ContentData.json";

        #endregion

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public readonly static string ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
        /// <summary>
        /// 官网地址
        /// </summary>
        public readonly static string csaaAddress = ConfigurationManager.AppSettings["csaaAddress"];
        /// <summary>
        /// 默认图片地址
        /// </summary>
        public readonly static string DefaultImage = ConfigurationManager.AppSettings["ErrorImg"];

        #region 新闻
        /// <summary>
        /// 航空新闻类型ID
        /// </summary>
        public const int TextNewsID = 1;
        /// <summary>
        /// 航空图片类型ID
        /// </summary>
        public const int ImageNewsID = 2;
        /// <summary>
        /// 航空视频类型ID
        /// </summary>
        public const int VideoNewsID = 3;
        /// <summary>
        /// 专栏类型ID
        /// </summary>
        public const int ColumnsID = 4;
        #endregion

        #region 学会动态
        /// <summary>
        /// 学会工作
        /// </summary>
        public const int AssociationID = 1;
        /// <summary>
        /// 通知公告
        /// </summary>
        public const int NoticeID = 3;
        /// <summary>
        /// 学会活动
        /// </summary>
        public const int ActionID = 2;
        #endregion

        #region  关于学会
        /// <summary>
        /// 学会简介
        /// </summary>
        public const int SocietyIntroductionID = 1;
        /// <summary>
        /// 学会章程
        /// </summary>
        public const int ConstitutionID = 2;
        /// <summary>
        /// 学会荣誉
        /// </summary>
        public const int HonorID = 3;
        /// <summary>
        /// 组织机构
        /// </summary>
        public const int OrganizationalID = 4;
        /// <summary>
        /// 管理条例
        /// </summary>
        public const int ManageRulesID = 5;
        #endregion

        #region 管理条例
        /// <summary>
        /// 表彰奖励管理条例
        /// </summary>
        public const int RecognitionAwardID = 1;
        /// <summary>
        /// 学术工作管理条例
        /// </summary>
        public const int AcademicWorkID = 2;
        /// <summary>
        /// 组织条例
        /// </summary>
        public const int OrgaincRulesID = 3;
        /// <summary>
        /// 管理条例
        /// </summary>
        //public const int ManageRulesID = 4;
        #endregion

        #region 组织机构
        /// <summary>
        /// 组织结构
        /// </summary>
        public const int OrganizationStructureID = 1;
        /// <summary>
        /// 理事会
        /// </summary>
        public const int CouncilID = 2;
        /// <summary>
        /// 常务理事会
        /// </summary>
        public const int AffairsCouncilID = 3;
        /// <summary>
        /// 工作委员会
        /// </summary>
        public const int WorkingCommitteeID = 4;
        /// <summary>
        /// 专业分会
        /// </summary>
        public const int ProfessionalBranchID = 5;
        /// <summary>
        /// 地方学会
        /// </summary>
        public const int LocalAssociationID = 6;
        /// <summary>
        /// 单位会员
        /// </summary>
        public const int UnitMemberID = 7;
        #endregion
    }
}
