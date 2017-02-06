using AeronauticalSociety.Log;
using AeronauticalSociety.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using TinyStack.Modules.iUtility.iTools;

namespace AeronauticalSociety.BusinessLayer.Providers
{
    #region  注册用户结果
    /// <summary>
    /// 注册用户结果
    /// </summary>
    public class RegUserResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool isSuccess { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string errMessage { get; set; }
    }
    #endregion

    #region 登录认证结果
    /// <summary>
    /// 登录认证结果
    /// </summary>
    public class LoginResult
    {
        /// <summary>
        /// 是否认证成功
        /// </summary>
        public bool isSuccess { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string errMessage { get; set; }

        /// <summary>
        /// 用户信息
        /// </summary>
        public User UserInfo { get; set; }
    }
    #endregion

    #region 登录认证参数
    /// <summary>
    /// 登录认证参数
    /// </summary>
    public class LoginInParam
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Param1 { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Param2 { get; set; }
    }
    #endregion

    #region  绑定会员证参数
    /// <summary>
    /// 绑定会员证参数
    /// </summary>
    public class BindMemberParam
    {
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 会员证号
        /// </summary>
        public string MemberCode { get; set; }
        /// <summary>
        /// 用户Key
        /// </summary>
        public string UserKey { get; set; }
    }
    #endregion

    #region  修改密码参数
    /// <summary>
    /// 修改密码参数
    /// </summary>
    public class UpdatePasswordParam
    {
        /// <summary>
        /// 旧密码
        /// </summary>
        public string Param1 { get; set; }
        /// <summary>
        /// 新密码
        /// </summary>
        public string Param2 { get; set; }
    }
    #endregion

    #region   修改密码结果
    /// <summary>
    /// 修改密码结果
    /// </summary>
    public class UpdatePasswordResult
    {
        /// <summary>
        /// 执行结果
        /// </summary>
        public bool isSuccess { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string errMessage { get; set; }
    }
    #endregion

    #region 会员绑定结果
    /// <summary>
    /// 会员绑定结果
    /// </summary>
    public class BindMemberResult
    {
        /// <summary>
        /// 绑定结果
        /// </summary>
        public bool isSuccess { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string errMessage { get; set; }
    }
    #endregion

    public class AccountProvider
    {
        #region 获取浏览权限
        /// <summary>
        /// 获取浏览权限
        /// </summary>
        /// <returns></returns>
        public int GetBrowsePermissions()
        {
            //获取当前用户
            User user = GetCurrentUser();
            if (user == null)
            {
                return 0;
            }

            return user.level;
        }
        #endregion

        #region 用户注册
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public RegUserResult RegUser(User Data)
        {
            RegUserResult _result = new RegUserResult();
            try
            {
                if (Data == null)
                {
                    _result.isSuccess = false;
                    _result.errMessage = "用户注册失败";
                    return _result;
                }
                //验证数据有效性
                _result = CheckUserData(Data);
                if (!_result.isSuccess)
                {
                    return _result;
                }

                Data.userKey = Guid.NewGuid();
                Data.regTime = DateTime.Now;
                Data.lastModifyTime = DateTime.Now;
                //加密密码
                Data.password = EncryptPassword(Data.password);
                if (RegUserCommit(Data))
                {
                    _result.isSuccess = true;
                }

                return _result;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "用户注册", "RegUserID", ex);
                _result.isSuccess = false;
                _result.errMessage = "用户注册失败";
            }
            return _result;
        }
        #endregion

        #region 提交用户注册数据
        /// <summary>
        /// 提交用户注册数据
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        private bool RegUserCommit(User Data)
        {
            string sql = @" INSERT into de2_user(userKey, loginName, password, userName, memberCode, sex,regTime,lastModifyTime,state) VALUES
                                      (@userKey, @loginName, @password, @userName,@memberCode, @sex,@regTime,@lastModifyTime,1)";
            MySqlClient _MySqlClient = new MySqlClient();
            _MySqlClient.ExecuteNonQuery(sql, new MySqlParameter("userKey", Data.userKey),
                new MySqlParameter("loginName", Data.loginName),
                new MySqlParameter("password", Data.password),
                new MySqlParameter("userName", Data.userName),
                new MySqlParameter("memberCode", Data.memberCode),
                new MySqlParameter("sex", Data.sex),
                new MySqlParameter("regTime", Data.regTime),
                new MySqlParameter("lastModifyTime", Data.lastModifyTime)
                );
            return true;
        }
        #endregion

        #region 验证用户
        /// <summary>
        /// 验证用户
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="type">0:新建；1:编辑</param>
        /// <returns></returns>
        private RegUserResult CheckUserData(User Data)
        {
            RegUserResult _RegUserResult = new RegUserResult();
            try
            {
                //验证用户名
                _RegUserResult = CheckUserName(Data);
                if (!_RegUserResult.isSuccess)
                {
                    return _RegUserResult;
                }
                //验证密码
                _RegUserResult = CheckPassword(Data.password);
                if (!_RegUserResult.isSuccess)
                {
                    return _RegUserResult;
                }
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "验证用户", "CheckUserData", ex);
                _RegUserResult.isSuccess = false;
                _RegUserResult.errMessage = "用户注册失败";
            }
            _RegUserResult.isSuccess = true;
            return _RegUserResult;
        }
        #endregion

        #region 验证用户名
        /// <summary>
        /// 验证用户名
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="type">0:新建；1:编辑</param>
        /// <returns></returns>
        private RegUserResult CheckUserName(User Data)
        {
            RegUserResult _RegUserResult = new RegUserResult();

            //判断是否为空
            string userName = Data.loginName.Trim();
            if (string.IsNullOrEmpty(userName))
            {
                _RegUserResult.isSuccess = false;
                _RegUserResult.errMessage = "用户登录名称不能为空";
                return _RegUserResult;
            }
            //验证用户名长度是否小于3位
            if (userName.Length < 3)
            {
                _RegUserResult.isSuccess = false;
                _RegUserResult.errMessage = "用户登录名称最小长度必须大于3位字符";
                return _RegUserResult;
            }
            //验证用户名是否存在
            if (LoginNameIsRepeat(Data.loginName, Data.userKey))
            {
                _RegUserResult.isSuccess = false;
                _RegUserResult.errMessage = "用户登录名称已经存在";
                return _RegUserResult;
            }
            _RegUserResult.isSuccess = true;
            return _RegUserResult;
        }
        #endregion

        #region 验证用户是否存在
        /// <summary>
        /// 验证用户是否存在
        /// </summary>
        /// <param name="LoginName"></param>
        /// <param name="UserKey">新建时为空</param>
        /// <returns></returns>
        public bool LoginNameIsRepeat(string LoginName, Guid UserKey)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("select count(id) from de2_user where loginName=@loginName ");
                if (UserKey != Guid.Empty)
                {
                    sql.AppendLine(" && userKey=@userKey");
                }
                MySqlClient _MySqlClient = new MySqlClient();
                int UserCount = 0;
                if (UserKey != Guid.Empty)
                {
                    UserCount = Convert.ToInt32(_MySqlClient.ExecuteScalar(sql.ToString(), new MySqlParameter("loginName", LoginName), new MySqlParameter("userKey", UserKey)));
                }
                else
                {
                    UserCount = Convert.ToInt32(_MySqlClient.ExecuteScalar(sql.ToString(), new MySqlParameter("loginName", LoginName)));
                }
                if (UserCount > 0)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "验证用户是否存在", "LoginNameIsRepeat", ex);
                return true;
            }
        }
        #endregion

        #region  验证密码
        /// <summary>
        /// 验证密码
        /// </summary>
        /// <param name="Password"></param>
        /// <returns></returns>
        private RegUserResult CheckPassword(string Password)
        {
            RegUserResult _RegUserResult = new RegUserResult();
            //验证密码是否为空
            if (string.IsNullOrEmpty(Password))
            {
                _RegUserResult.isSuccess = true;
                _RegUserResult.errMessage = "用户登录密码为空";
                return _RegUserResult;
            }
            //
            string _password = Password.Trim();
            byte[] outputb = Convert.FromBase64String(Password);
            _password = Encoding.UTF8.GetString(outputb);
            //验证密码长度
            if (_password.Length < 6)
            {
                _RegUserResult.isSuccess = true;
                _RegUserResult.errMessage = "用户登录密码最小长度必须大于等于6位字符";
                return _RegUserResult;
            }
            _RegUserResult.isSuccess = true;
            return _RegUserResult;
        }
        #endregion

        #region 加密密码
        /// <summary>
        /// 加密密码
        /// </summary>
        /// <param name="Password"></param>
        /// <returns></returns>
        public string EncryptPassword(string Password)
        {
            if (string.IsNullOrEmpty(Password))
            {
                return Password;
            }
            byte[] outputb = Convert.FromBase64String(Password);
            string orgStr = Encoding.UTF8.GetString(outputb);

            return EncryptDecryption.MD5Encrypt(orgStr);
        }
        #endregion

        #region 登录认证
        /// <summary>
        /// 登录认证
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public LoginResult CheckIn(LoginInParam Param)
        {
            LoginResult _LoginResult = new LoginResult();
            try
            {
                //加密密码
                Param.Param2 = EncryptPassword(Param.Param2);
                string sql = @"select u.id, userKey,loginName, userName,memberCode,m.level,rank.membername as levelName
 ,r.admintypeid
                                from de2_user as u
                                left join de2_user_member as m on u.id = m.userid 
                                left join de2_arcrank as  rank on rank.id = m.`level` 
                                left join de2_user_role as r on u.id=r.uid where u.loginName=@loginName and u.password=@password and u.state=1";
                MySqlClient _MySqlClient = new MySqlClient();
                User result = _MySqlClient.ExecuteQuery<User>(sql, new MySqlParameter("loginName", Param.Param1), new MySqlParameter("password", Param.Param2)).FirstOrDefault();
                if (result == null)
                {
                    _LoginResult.isSuccess = false;
                    _LoginResult.errMessage = "用户名或密码错误";
                }
                else
                {
                    _LoginResult.isSuccess = true;
                    _LoginResult.UserInfo = result;
                    SetSession(result);
                    WriteLoginLog(result.userKey, result.userName);
                }
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "登录认证", "LoginCheck", ex);
                _LoginResult.isSuccess = false;
                _LoginResult.errMessage = "登录失败";
            }
            return _LoginResult;
        }
        #endregion

        #region 管理员登陆认证
        /// <summary>
        /// 管理员登陆认证
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public LoginResult AdminCheckIn(LoginInParam Param)
        {
            LoginResult _LoginResult = new LoginResult();
            try
            {
                //加密密码
                Param.Param2 = EncryptPassword(Param.Param2);
                string sql = @"select u.id, userKey,loginName, userName,memberCode,m.level,rank.membername as levelName
 ,r.admintypeid as adminType
                                from de2_user as u
                                left join de2_user_member as m on u.id = m.userid 
                                left join de2_arcrank as  rank on rank.id = m.`level` 
                                left join de2_user_role as r on u.id=r.uid where u.loginName=@loginName and u.password=@password and u.state=1 ";
                MySqlClient _MySqlClient = new MySqlClient();
                User result = _MySqlClient.ExecuteQuery<User>(sql, new MySqlParameter("loginName", Param.Param1), new MySqlParameter("password", Param.Param2)).FirstOrDefault();
                if (result == null)
                {
                    _LoginResult.isSuccess = false;
                    _LoginResult.errMessage = "用户名或密码错误";
                    return _LoginResult;
                }
                if (result.adminType == 0 || result.adminType > 2)
                {
                    _LoginResult.isSuccess = false;
                    _LoginResult.errMessage = "用户名没有访问权限";
                    return _LoginResult;
                }
                _LoginResult.isSuccess = true;
                _LoginResult.UserInfo = result;
                SetSession(result);
                WriteLoginLog(result.userKey, result.userName);

            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "管理员登陆认证", "AdminCheckIn", ex);
                _LoginResult.isSuccess = false;
                _LoginResult.errMessage = "登录失败";
            }
            return _LoginResult;
        }
        #endregion

        #region 记录登录日志
        /// <summary>
        /// 记录登录日志
        /// </summary>
        /// <param name="UserKey"></param>
        /// <param name="UserName"></param>
        public void WriteLoginLog(Guid UserKey, string UserName)
        {

        }
        #endregion

        #region 设置Session
        /// <summary>
        /// 设置Session
        /// </summary>
        /// <param name="Data"></param>
        private void SetSession(User Data)
        {
            if (HttpContext.Current == null || HttpContext.Current.Session == null)
            {
                return;
            }
            if (HttpContext.Current.Session["CurrentUser"] == null)
            {
                HttpContext.Current.Session.Add("CurrentUser", Data);
            }
            else
            {
                HttpContext.Current.Session["CurrentUser"] = Data;
            }
            //设置cookie
            if (HttpContext.Current.Response.Cookies["user"] == null)
            {
                HttpCookie _Cookie = new HttpCookie("user", System.Web.HttpUtility.UrlEncode(Data.ToJsonString(), Encoding.UTF8));
                _Cookie.Expires = DateTime.Now.AddDays(7);
                _Cookie.Path = "/";
                HttpContext.Current.Response.Cookies.Add(_Cookie);
            }
            else
            {
                HttpContext.Current.Response.Cookies["user"].Value = HttpUtility.UrlEncode(Data.ToJsonString());
                HttpContext.Current.Response.Cookies["user"].Expires = DateTime.Now.AddDays(7);
                HttpContext.Current.Response.Cookies["user"].Path = "/";
            }
        }
        #endregion

        #region 清空Session
        /// <summary>
        /// 清空Session
        /// </summary>
        /// <param name="Data"></param>
        private void ClearSession()
        {
            if (HttpContext.Current == null || HttpContext.Current.Session == null)
            {
                return;
            }
            if (HttpContext.Current.Session["CurrentUser"] != null)
            {
                HttpContext.Current.Session["CurrentUser"] = null;
            }
            //清空cookie
            if (HttpContext.Current.Response.Cookies["user"] != null)
            {
                HttpContext.Current.Request.Cookies["user"].Value = null;
                HttpContext.Current.Request.Cookies.Remove("user");
                HttpContext.Current.Response.Cookies["user"].Value = null;
                HttpContext.Current.Response.Cookies["user"].Expires = DateTime.Now.AddDays(-1);
            }
        }
        #endregion

        #region 退出登录
        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public bool CheckOut()
        {
            try
            {
                //清空session
                ClearSession();
                return true;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "退出登录", "CheckOut", ex);
                return false;
            }
        }
        #endregion

        #region 注册用户绑定
        /// <summary>
        /// 注册用户绑定
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public BindMemberResult BindMember(BindMemberParam Param)
        {
            BindMemberResult _result = new BindMemberResult();
            try
            {
                //查询会员账号是否已被绑定

                //查询会员是否存在
                string sql = "select * from de2_user_member where memberID=@memberID and memberName=@memberName";
                MySqlClient _MySqlClient = new MySqlClient();
                Member _Member = _MySqlClient.ExecuteQuery<Member>(sql, new MySqlParameter("memberID", Param.MemberCode), new MySqlParameter("memberName", Param.UserName)).FirstOrDefault();
                if (_Member == null)
                {
                    _result.isSuccess = false;
                    _result.errMessage = "会员证号验证失败";
                    return _result;
                }
                if (_Member.userID != 0)
                {
                    _result.isSuccess = false;
                    _result.errMessage = "会员证号已被绑定请联系管理员";
                    return _result;
                }
                //获取用户ID
                sql = "select * from de2_user where userKey=@userKey";
                User UserInfo = _MySqlClient.ExecuteQuery<User>(sql, new MySqlParameter("userKey", Param.UserKey)).FirstOrDefault();
                if (UserInfo == null)
                {
                    _result.isSuccess = false;
                    _result.errMessage = "注册用户不存在";
                    return _result;
                }
                //关联会员信息
                StringBuilder sqlb = new StringBuilder();
                sqlb.AppendLine(" update de2_user_member set userid=@userid where memberID=@memberID ;");
                sqlb.AppendLine(" update de2_user set memberCode= @memberID ,userName=@userName where id=@userid ;");
                _MySqlClient.ExecuteNonQuery(sqlb.ToString(), new MySqlParameter("userid", UserInfo.id)
                    , new MySqlParameter("memberID", _Member.memberID), new MySqlParameter("userName", _Member.memberName));

                //设置session
                User SessionUserInfo = new User();
                SessionUserInfo.id = UserInfo.id;
                SessionUserInfo.userKey = UserInfo.userKey;
                SessionUserInfo.loginName = UserInfo.loginName;
                SessionUserInfo.userName = UserInfo.userName;
                SessionUserInfo.memberCode = _Member.memberID;
                SessionUserInfo.level = _Member.level;
                SessionUserInfo.levelName = _Member.levelName;
                SetSession(SessionUserInfo);

                //修改用户信息表
                _result.isSuccess = true;
                return _result;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "注册用户绑定", "BindMember", ex);
                _result.isSuccess = false;
                _result.errMessage = "用户绑定失败";
            }
            return _result;
        }
        #endregion

        #region 获取用户详细信息
        /// <summary>
        /// 获取用户详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Member GetMember(string ID)
        {
            if (string.IsNullOrEmpty(ID))
            {
                return null;
            }
            Member _Member = new Member();
            try
            {
                string sql = @"select u.id, u.sex, u.loginName , u.address,u.addressCity, u.addressProvince
,u.addressCityID, u.addressProvinceID,u.age,u.email,u.mobileNO,u.levelID as userLeveID,l.levelName as userLeveName
,a.areaName as areaName, b.branchName as branchName , p.postName as post 
, s.`name` as state,m.state as stateID ,m.areaID,m.branchID,m.postID,u.oid as occupationID
,o.oname as occupationName,u.pid as professionID ,pr.typeName as professionName,m.memberID,m.memberName
,m.`level`,rank.membername as levelName
from de2_user as u 
LEFT JOIN de2_user_level as l on l.id=u.levelID
left join de2_user_member m on m.userid=u.id
left join de2_arcrank rank on rank.id = m.`level`
left join de2_user_member_area a on a.id = m.areaID
left join de2_user_member_branch b on b.id = m.branchID
LEFT JOIN de2_user_member_post p on p.id = m.postID
LEFT JOIN de2_user_member_state s on s.id = u.state
left join de2_user_occupation o on o.id=u.oid
LEFT JOIN de2_user_profession pr on pr.id=u.pid  where u.id=@ID";
                MySqlClient _MySqlClient = new MySqlClient();
                _Member = _MySqlClient.ExecuteQuery<Member>(sql, new MySqlParameter("ID", Convert.ToInt32(ID))).FirstOrDefault();
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取用户详细信息", "GetMember", ex);
            }
            return _Member;
        }
        #endregion

        #region 修改登录密码
        /// <summary>
        /// 修改登录密码
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public UpdatePasswordResult UpdatePassword(UpdatePasswordParam Param)
        {
            UpdatePasswordResult _Result = new UpdatePasswordResult();
            try
            {
                string oldPassword = EncryptPassword(Param.Param1);
                string newPassword = EncryptPassword(Param.Param2);
                User _User = GetCurrentUser();
                //验证旧密码是否正确
                string sql = @"select count(id) from de2_user where id=@id and password=@password";
                MySqlClient _MySqlClient = new MySqlClient();
                int count = Convert.ToInt32(_MySqlClient.ExecuteScalar(sql, new MySqlParameter("id", _User.id), new MySqlParameter("password", oldPassword)));
                if (count == 0)
                {
                    _Result.isSuccess = false;
                    _Result.errMessage = "原密码不正确";
                    return _Result;
                }
                //验证新密码
                RegUserResult CheckResult = CheckPassword(Param.Param2);
                if (!CheckResult.isSuccess)
                {
                    _Result.isSuccess = false;
                    _Result.errMessage = CheckResult.errMessage;
                    return _Result;
                }
                //修改密码
                sql = @"update de2_user set password=@password  where id=@id  ";
                _MySqlClient.ExecuteNonQuery(sql, new MySqlParameter("id", _User.id), new MySqlParameter("@password", newPassword));
                _Result.isSuccess = true;
                return _Result;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "修改登录密码", "UpdatePassword", ex);
            }
            return _Result;
        }
        #endregion

        #region 修改用户信息
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public bool UpdateMember(Member Data)
        {
            try
            {
                string sql = @"update de2_user set address=@address,email=@email,sex=@sex,addressProvinceID=@addressProvinceID,addressProvince=@addressProvince,addressCityID=@addressCityID,addressCity=@addressCity ,age=@age,mobileNO=@mobileNO,pid=@professionID,oid=@occupationID where id=@id;
                  update de2_user_member set areaID=@areaID,branchID=@branchID where userid=@id";
                MySqlClient _MySqlClient = new MySqlClient();
                _MySqlClient.ExecuteNonQuery(sql,
                    new MySqlParameter("branchID", Data.branchID),
                    new MySqlParameter("areaID", Data.areaID),
                    new MySqlParameter("address", Data.address),
                    new MySqlParameter("email", Data.email),
                    new MySqlParameter("sex", Data.sex),
                    new MySqlParameter("addressProvinceID", Data.addressProvinceID),
                    new MySqlParameter("addressProvince", Data.addressProvince),
                    new MySqlParameter("addressCityID", Data.addressCityID),
                    new MySqlParameter("addressCity", Data.addressCity),
                    new MySqlParameter("age", Data.age),
                    new MySqlParameter("mobileNO", Data.mobileNO),
                    new MySqlParameter("occupationID", Data.occupationID),
                    new MySqlParameter("professionID", Data.professionID),
                    new MySqlParameter("id", Data.id)
                    );
                return true;
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "修改用户信息", "UpdateMember", ex);
                return false;
            }
        }
        #endregion

        #region 获取当前用户
        /// <summary>
        /// 获取当前用户
        /// </summary>
        /// <returns></returns>
        public User GetCurrentUser()
        {
            User _User = null;
            if (HttpContext.Current == null || HttpContext.Current.Session == null)
            {
                return _User;
            }
            if (HttpContext.Current.Session["CurrentUser"] != null)
            {
                _User = (User)HttpContext.Current.Session["CurrentUser"];
                return _User;
            }
            if (HttpContext.Current.Request.Cookies["user"] != null && !string.IsNullOrEmpty(HttpContext.Current.Request.Cookies["user"].Value))
            {
                _User = (System.Web.HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies["user"].Value, Encoding.UTF8)).FormJsonString<User>();
                //重新设置session
                SetSession(_User);
            }
            return _User;
        }
        #endregion

        #region 获取所在地区
        /// <summary>
        /// 获取所在地区
        /// </summary>
        /// <returns></returns>
        public List<Area> GetAreaList()
        {
            List<Area> result = new List<Area>();
            try
            {
                string sql = "select id, areaName  from de2_user_member_area order by `index`    ";
                MySqlClient _MySqlClient = new MySqlClient();
                result = _MySqlClient.ExecuteQuery<Area>(sql);
                result.Insert(0, new Area() { id = 0, areaName = "--", index = 0 });
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取所在地区", "GetAreaList", ex);
            }
            return result;
        }
        #endregion

        #region 获取专业分会
        /// <summary>
        /// 获取专业分会
        /// </summary>
        /// <returns></returns>
        public List<Branch> GetBranchList()
        {
            List<Branch> result = new List<Branch>();
            try
            {
                string sql = "select id,branchName  from de2_user_member_branch order by `index`   ";
                MySqlClient _MySqlClient = new MySqlClient();
                result = _MySqlClient.ExecuteQuery<Branch>(sql);
                result.Insert(0, new Branch() { id = 0, branchName = "--", index = 0 });
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取专业分会", "GetBranchList", ex);
            }
            return result;
        }
        #endregion

        #region 获取专业列表
        /// <summary>
        /// 获取专业列表
        /// </summary>
        /// <returns></returns>
        public List<Profession> GetProfessionList()
        {
            List<Profession> result = new List<Profession>();
            try
            {
                string sql = "select * from de2_user_profession order by typeIndex";
                MySqlClient _Client = new MySqlClient();
                result = _Client.ExecuteQuery<Profession>(sql);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取专业列表", "GetProfessionList", ex);
            }
            return result;
        }
        #endregion

        #region 获取职业列表
        /// <summary>
        /// 获取职业列表
        /// </summary>
        /// <returns></returns>
        public List<Occupation> GetOccupationList()
        {
            List<Occupation> result = new List<Occupation>();
            try
            {
                string sql = "select * from de2_user_occupation  order by oindex";
                MySqlClient _Client = new MySqlClient();
                result = _Client.ExecuteQuery<Occupation>(sql);
            }
            catch (Exception ex)
            {
                LogFactory _LogFactory = new LogFactory(this.GetType());
                _LogFactory.CreateLog(LogType.Error, "获取职业列表", "GetOccupationList", ex);
            }
            return result;
        }
        #endregion

    }
}
