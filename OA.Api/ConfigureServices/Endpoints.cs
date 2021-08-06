using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OA.Api.Hubs;
using OA.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OA.Api.ConfigureServices
{
    public static class Endpoints
    {
        internal static void UseEndPoints(IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
                endpoints.MapHub<MessageHub>("/MessageHub");
                endpoints.MapHealthChecks("/Health", new HealthCheckOptions
                {
                    ResponseWriter = async (context, report) =>
                    {
                        context.Response.ContentType = "application/json";
                        var responce = new HealthCheckResponseModel
                        {
                            Status = report.Status.ToString(),
                            Checks = report.Entries.Select(x => new HealthCheckModel
                            {
                                Component = x.Key,
                                Status = x.Value.Status.ToString(),
                                Description = x.Value.Description
                            }),
                            Duration = report.TotalDuration
                        };
                        await context.Response.WriteAsync(text: JsonConvert.SerializeObject(responce));
                    },
                }).RequireAuthorization();
            });
        }
    }
}
