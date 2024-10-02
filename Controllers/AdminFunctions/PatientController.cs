using DieticianApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace DieticianApp.Controllers.AdminFunctions
{
    public class PatientController : AdminFunctionBaseController
    {
        public PatientController(AppDbContext context) : base(context)
        {
        }
    }
}
