using DieticianApp.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DieticianApp.MiddleWares
{
    public class ProfileCompleteMiddleware
    {
        private readonly RequestDelegate _next;

        public ProfileCompleteMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, AppDbContext dbContext)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var userId = int.Parse(context.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var patient = await dbContext.Patients
                    .Include(u => u.User)
                    .FirstOrDefaultAsync(p => p.User_Id == userId);

                if (patient != null)
                {
                    //// Ellenőrizd, hogy a kérés a "PendingPatient" vagy "CompleteProfile" oldalakra vonatkozik-e
                    //var path = context.Request.Path.Value.ToLower();
                    //if (path == "PendingPatient" || path == "/account/completeprofile")
                    //{
                    //    await _next(context);
                    //    return; // Ne irányítsuk át, ha már ezen az oldalon van
                    //}

                    if (patient.Dietician_Id == null)
                    {
                        context.Response.Redirect("PendingPatient");
                        return;
                    }
                    //else if (patient.User.Is_profile_completed == false)
                    //{
                    //    context.Response.Redirect("/Account/CompleteProfile");
                    //    return;
                    //}
                }
            }

            await _next(context);
        }

    }
}
