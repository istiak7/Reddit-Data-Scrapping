using Quartz;
using Reddit_Management_System.Service.Jobs;

namespace Reddit_Management_System.DependencyExtensions
{
    public static class RegisterBackgroundJobs
    {
        public static void AddBackgroundJobs(this WebApplicationBuilder builder)
        {
            builder.Services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();
                var jobKey = new JobKey("RedditEmailJob");
                q.AddJob<RedditEmailJob>(opts => opts.WithIdentity(jobKey));
                
                q.AddTrigger(opts => opts
                    .ForJob(jobKey)
                    .WithIdentity("RedditEmailJob-StartNow-trigger")
                    .StartNow()); 

                
                q.AddTrigger(opts => opts
                    .ForJob(jobKey)
                    .WithIdentity("RedditEmailJob-Daily-trigger")
                    .WithCronSchedule("0 0 9 * * ?")); 
            });
            builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
        }
    }
}
