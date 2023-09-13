using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TuYi.Practice.Interfaces;
using SqlSugar;
using TuYi.Practice.DbModels;
using TuYi.Practice.Framework.CustomEnum;
using TuYi.Practice.Framework.Models;
using TuYi.Practice.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TuYi.Practice.WebSite.Controllers
{
    public class UserInfoController : Controller
    {
        private readonly ILogger<UserInfoController> _logger;
        private readonly IUserInfoService _userInfoService;
        private readonly IMapper _mapper;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="userInfoService"></param>
        public UserInfoController(ILogger<UserInfoController> logger, IUserInfoService userInfoService, IMapper mapper)
        {
            _logger = logger;
            _userInfoService = userInfoService;
            _mapper = mapper;
        }

        /// <summary>
        /// 主页包括查询参数
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="userType"></param>
        /// <param name="userStatus"></param>
        /// <param name="userGender"></param>
        /// <param name="url"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index(string searchString, string userType, string userStatus, string userGender, string url, int pageIndex = 1, int pageSize = 10)
        {
            #region 拼接查询条件

            Expressionable<UserInfo> expressionable = new Expressionable<UserInfo>();
            expressionable = expressionable.AndIF(!string.IsNullOrWhiteSpace(searchString), s => s.Name.StartsWith(searchString));
            expressionable = expressionable.AndIF(!string.IsNullOrWhiteSpace(userType), s => s.UserType == int.Parse(userType));

            var isQueryStatus = !string.IsNullOrWhiteSpace(userStatus);

            if (isQueryStatus)
            {
                expressionable = expressionable.And(s => s.Status == int.Parse(userStatus));
            }
            else
            {
                expressionable = expressionable.And(s => s.Status != (int)StatusEnum.Delete);
            }

            #endregion

            var pageData = await _userInfoService.QueryPageAsync(expressionable.ToExpression(), pageSize, pageIndex, c => c.Id, false);
            var pageDataDTO = _mapper.Map<PagingData<UserInfo>, PagingData<UserInfoDTO>>(pageData);

            //获取查询参数下拉列表
            ViewData["userTypeList"] = CustomEnumExtend.ToSelectListByEnum(typeof(UserTypeEnum), userType);

            ViewData["userStatusList"] = CustomEnumExtend.ToSelectListByEnum(typeof(UserStatusEnum), userStatus);

            ViewData["userGenderList"] = CustomEnumExtend.ToSelectListByEnum(typeof(GenderEnum), userGender);

            ViewBag.Url = $"SearchString={searchString}&url={url}&userType={userType}&userStatu={userStatus}&userGender={userGender}";

            return View(pageDataDTO);
        }

        /// <summary>
        /// 新增用户界面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ViewData["userTypeList"] = CustomEnumExtend.ToSelectListByEnum(typeof(UserTypeEnum), ((int)UserTypeEnum.Administrators).ToString());
            ViewData["userGenderList"] = CustomEnumExtend.ToSelectListByEnum(typeof(GenderEnum));

            return await Task.FromResult(View());
        }

        /// <summary>
        /// 新增用户后台方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(UserInfoDTO addInfo)
        {
            if (ModelState.IsValid)
            {
                var dateNew = DateTime.Now;
                //新增数据
                var addModel = _mapper.Map<UserInfoDTO, UserInfo>(addInfo);

                addModel.CreateTime = dateNew;
                addModel.LastModifyTime = dateNew;
                addModel.Status = (int)UserStatusEnum.Normal;
                addModel.UserType = (int)UserTypeEnum.Administrators;
                await _userInfoService.InsertAsync(addModel);

                return RedirectToAction("Index");
            }
            else
            {
                ViewData["userTypeList"] = CustomEnumExtend.ToSelectListByEnum(typeof(UserTypeEnum), ((int)UserTypeEnum.Administrators).ToString());
                ViewData["userGenderList"] = CustomEnumExtend.ToSelectListByEnum(typeof(GenderEnum));

                return await Task.FromResult(View(addInfo));
            }
        }
    }
}
