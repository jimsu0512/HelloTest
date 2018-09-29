using eUI.BLL;
using eUI.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace easyUITest.Controllers
{
    //上传用户头像
    public class UploadController : ApiController
    {
        UserInfoBLL userInfoBLL = new UserInfoBLL();

        [System.Web.Http.HttpPost]
        [ValidateInput(false)]
        public UploadPicModel UploadUserPic()
        {
            UploadPicModel uploadPicModel = new UploadPicModel();
            string info = string.Empty;
            try
            {
                //获取客户端上传的文件集合
                HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
                //判断是否存在文件
                if (files.Count > 0)
                {
                    int userID = Convert.ToInt32(System.Web.HttpContext.Current.Request.Form["Id"]);
                    //获取文件集合中的第一个文件(每次只上传一个文件)
                    HttpPostedFile file = files[0];
                    //定义文件存放的目标路径
                    string targetDir = System.Web.HttpContext.Current.Server.MapPath("/Img/UploadPic");
                    //创建目标路径
                    //ZFiles.CreateDirectory(targetDir);
                    //组合成文件的完整路径
                    string path = System.IO.Path.Combine(targetDir, System.IO.Path.GetFileName(file.FileName));
                    //保存上传的文件到指定路径中
                    file.SaveAs(path);

                    //上传之后之后更新数据库信息
                    bool result = userInfoBLL.UpdatePic(new UserInfo() { Id = userID, PicUrl = "/Img/UploadPic/" + file.FileName });

                    if (result)
                    {
                        uploadPicModel.IsSucceed = true;
                    }
                    else
                    {
                        uploadPicModel.IsSucceed = false;
                        uploadPicModel.ErrorMsg = "更新数据库失败";
                    }

                }
                else
                {
                    uploadPicModel.IsSucceed = false;
                    uploadPicModel.ErrorMsg = "未获取到文件";
                }
            }
            catch (Exception ex)
            {
                uploadPicModel.IsSucceed = false;
                uploadPicModel.ErrorMsg = "上传异常:" + ex.Message;
            }

            return uploadPicModel;

        }

        public UploadPicModel UploadBannerImg()
        {
            UploadPicModel uploadPicModel = new UploadPicModel();
            string info = string.Empty;
            try
            {
                //获取客户端上传的文件集合
                HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
                //判断是否存在文件
                if (files.Count > 0)
                {
                    int userID = Convert.ToInt32(System.Web.HttpContext.Current.Request.Form["Id"]);
                    //获取文件集合中的第一个文件(每次只上传一个文件)
                    HttpPostedFile file = files[0];
                    //定义文件存放的目标路径
                    string targetDir = System.Web.HttpContext.Current.Server.MapPath("/Img/UploadPic/BannerImg");
                    //创建目标路径
                    //ZFiles.CreateDirectory(targetDir);
                    //组合成文件的完整路径
                    string path = System.IO.Path.Combine(targetDir, System.IO.Path.GetFileName(file.FileName));
                    //保存上传的文件到指定路径中
                    file.SaveAs(path);

                    //上传之后之后更新数据库信息
                    bool result = Config_BannerInfoBLL.UpdateBannerImg(new Config_BannerInfo() { CBannerId = userID, CBannerUrl = "/Img/UploadPic/BannerImg/" + file.FileName });

                    if (result)
                    {
                        uploadPicModel.IsSucceed = true;
                    }
                    else
                    {
                        uploadPicModel.IsSucceed = false;
                        uploadPicModel.ErrorMsg = "更新数据库失败";
                    }

                }
                else
                {
                    uploadPicModel.IsSucceed = false;
                    uploadPicModel.ErrorMsg = "未获取到文件";
                }
            }
            catch (Exception ex)
            {
                uploadPicModel.IsSucceed = false;
                uploadPicModel.ErrorMsg = "上传异常:" + ex.Message;
            }

            return uploadPicModel;
        }
    }
}
