using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kaizen.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Taste.Utilities;

namespace UploadandDowloadService.Pages.Admin.Users

{
    [Authorize(Roles = "Manager")]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
