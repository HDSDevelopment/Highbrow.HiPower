using System;
using System.IO;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Highbrow.HiPower.Services;
using Highbrow.HiPower.Models;

namespace Highbrow.HiPower.Controllers
{
    public class CompanyProfileController : Controller
    {        
        ICompanyProfileService _companyProfileService;

        public CompanyProfileController(ICompanyProfileService companyProfileService)
        {
            _companyProfileService = companyProfileService;
        }

        public IActionResult Index()
        {
            return View("Details");
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            //int idValue = id.Value;
            int idValue = 1;
            CompanyProfile companyProfile = await _companyProfileService.Details(idValue);
            
            if(companyProfile != null)
            {
                return View(companyProfile);
                }
                    else
                        {
                //Response.StatusCode = 404;
                //return View("~/Views/CompanyProfile/NotFound.cshtml", idValue);
                return RedirectToAction("Add", "CompanyProfile");
            }
        }

        [HttpGet, ActionName("Add")]
        public IActionResult AddGet()
        {            
            return View();
        }

		[ValidateAntiForgeryToken]
		[HttpPost, ActionName("Add")]
        public async Task<IActionResult> AddPost(CompanyProfile companyProfile, IFormFile companyLogo)
        {
            ServiceResult result = ServiceResult.Failure;

            if(ModelState.IsValid)
            {
                try
                {
                    result = await _companyProfileService.Add(companyProfile);

                    if(result == ServiceResult.Success)                    
                    result = await AddLogo(companyProfile, companyLogo);
                    
                    if(result == ServiceResult.Success)
                        //return RedirectToAction("Index", "Home");
                    return RedirectToAction("Details", "CompanyProfile", new {id = companyProfile.Id });
                }
                catch (Exception ex)
                {
                    //Perform logging here
                    return View("~/Views/Home/Error/500.cshtml");
                }
            }
            return View(companyProfile);
        }        

        [HttpGet, ActionName("Update")]
        public async Task<IActionResult> UpdateGet(int id)
        {
            CompanyProfile companyProfile = await _companyProfileService.Details(id);
            
            if(companyProfile != null)
                return View("Update", companyProfile);

                    return View("Error");            
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Update")]
        public async Task<IActionResult> UpdatePost(int id, CompanyProfile companyProfile, IFormFile companyLogo)
        {
            ServiceResult result = ServiceResult.Failure;

            if (ModelState.IsValid)
            {
                try
                {
                    result = await _companyProfileService.Update(id, companyProfile);

                    if (result == ServiceResult.Success)
                        result = await UpdateLogo(companyProfile, companyLogo);
                        
                        if (result == ServiceResult.Success)
                        //return RedirectToAction("Index", "Home");
                        return RedirectToAction("Details", "CompanyProfile", new { id = companyProfile.Id });
                }
                catch (Exception ex)
                {
                    //Perform logging here
                    return View("~/Views/Home/Error/500.cshtml");
                }
            }
            return View("Update", companyProfile);
        }

        [HttpGet, ActionName("Delete")]
        public async Task<IActionResult> DeleteGet(int? id)
        {
            int idValue = id.Value;
            CompanyProfile companyProfile = await _companyProfileService.Details(idValue);
            
            if(companyProfile != null)
                return View("Delete", companyProfile);
                else
                {
                    Response.StatusCode = 404;
                            return View("~/Views/CompanyProfile/NotFound.cshtml", idValue);
                }
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int id)
        {
            try
            {
            ServiceResult result = await _companyProfileService.Delete(id);
            
            if(result == ServiceResult.Success)
                return RedirectToAction("Index", "Home");
            }
            catch(Exception ex)
            {
                //Perform logging here
                return View("~/Views/Home/Error/500.cshtml");
            }
                return View("Error");
        }

        async Task<ServiceResult> AddLogo(CompanyProfile companyProfile, IFormFile companyLogo)
        {            
                    if(companyLogo.Length > 0)
                    {
                    string fileExtension = Path.GetExtension(companyLogo.FileName);                    
                    var newFileName = companyProfile.Id + "_CompanyLogo" + fileExtension;

                    string directoryPath = @"wwwroot\Uploads\CompanyLogo\";

                    CreateDirectory(directoryPath);

                    var filePath = Path.Combine(directoryPath, newFileName);                    
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    companyLogo.CopyTo(fileStream);
                }                    
                    companyProfile.LogoFileName = newFileName;
                    companyProfile.LogoFileSize = companyLogo.Length;
                    companyProfile.LogoContentType = companyLogo.ContentType;
                    companyProfile.LogoCreatedAt = DateTime.Now;
                    companyProfile.LogoUpdatedAt = companyProfile.LogoCreatedAt;
                    
                    return await _companyProfileService.Update(companyProfile.Id, companyProfile);
                    }
                    return ServiceResult.Failure;
        }

        async Task<ServiceResult> UpdateLogo(CompanyProfile companyProfile, IFormFile companyLogo)
        {
            if (companyLogo.Length > 0)
            {
                string directoryPath = @"wwwroot\Uploads\CompanyLogo\";
                if (companyProfile.LogoFileName != null)
                {
                    var deleteFileName = companyProfile.LogoFileName;
                    DeleteFile(directoryPath, deleteFileName);
                }

                string newFileExtension = Path.GetExtension(companyLogo.FileName);
                string newFileName = companyProfile.Id + "_CompanyLogo" + newFileExtension;

                CreateDirectory(directoryPath);
                var filePath = Path.Combine(directoryPath, newFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    companyLogo.CopyTo(fileStream);
                }
                companyProfile.LogoFileName = newFileName;
                companyProfile.LogoFileSize = companyLogo.Length;
                companyProfile.LogoContentType = companyLogo.ContentType;
                companyProfile.LogoUpdatedAt = companyProfile.UpdatedAt;

                return await _companyProfileService.Update(companyProfile.Id, companyProfile);
            }
            return ServiceResult.Failure;
        }

            void DeleteFile(string directory, string fileWithExtension)
            {
                string deleteFileWithPath = Path.Combine(directory, fileWithExtension);

                if (System.IO.File.Exists(deleteFileWithPath))
                    System.IO.File.Delete(deleteFileWithPath);
            }

            void CreateDirectory(string directory)
            {
                if(!Directory.Exists(directory))                                        
                        Directory.CreateDirectory(directory);
            }
    }
}