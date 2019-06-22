using Universal.System.WebApi.Extensions;
using Universal.System.WebApi.Filter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Universal.System.WebApi
{

    /// <summary>
    /// 参考 https://dnczeus.codedefault.com/rbac/user
    /// </summary>
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;

        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // 替换系统默认Controller创建器  ps：必须放到 AddMvc 前面注册
            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());

            services.AddDbConnectionMaster("Data Source=;Initial Catalog=US_Local;User ID=it;Password=;");

            #region 配置跨域处理
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin", builder =>
                {
                    builder.WithOrigins(Configuration["AllowCors:AllowOrigin"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();//指定处理cookie
                });
            });
            #endregion

            services.AddMvc(options => options.Filters.Add<ValidateTokenAttribute>())
                .AddJsonOptions(options => options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss")
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            return services.AutofacInitialize(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowOrigin");

            //提供对静态资源的访问
            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
