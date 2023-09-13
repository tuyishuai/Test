using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using AutoMapper;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace TuYi.Practice.WebSite
{
    /// <summary>
    /// 中间件注册扩展
    /// </summary>
    public static class HostBuilderExtend
    {
        public static void Register(this WebApplicationBuilder applicationBuilder)
        {
            var configuration = applicationBuilder.Configuration;
            string connectionString = configuration["ConnectionString"];

            applicationBuilder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.Converters.Add(new DataTimeJsonConverter("yyyy-MM-dd HH:mm:ss"));
            });

            applicationBuilder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureContainer<ContainerBuilder>(configurationBinder =>
                {
                    #region 注册Autofac支持

                    #region 通过接口和实现类所在程序集注册 

                    Assembly interfaceAssembly = Assembly.Load("TuYi.Practice.Interfaces");
                    Assembly serviceAssembly = Assembly.Load("TuYi.Practice.Services");
                    configurationBinder.RegisterAssemblyTypes(interfaceAssembly, serviceAssembly)
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();

                    #endregion

                    #region 注册每个控制器和抽象之间的关系

                    var controllerBaseType = typeof(ControllerBase);
                    configurationBinder.RegisterAssemblyTypes(typeof(Program).Assembly)
                        .Where(t => controllerBaseType.IsAssignableFrom(t) && t != controllerBaseType);

                    #endregion

                    #endregion

                    #region 注册Sqlsugar

                    configurationBinder.Register<ISqlSugarClient>(context =>
                    {
                        SqlSugarClient client = new SqlSugarClient(new ConnectionConfig()
                        {
                            ConnectionString = connectionString,
                            DbType = DbType.SqlServer,
                            InitKeyType = InitKeyType.Attribute,
                            SlaveConnectionConfigs = new List<SlaveConnectionConfig> {
                                 new SlaveConnectionConfig(){
                                     ConnectionString = connectionString,
                                     HitRate = 10
                                 }
                            }
                        });

                        //支持sql语句的输出，方便排除错误
                        client.Aop.OnLogExecuting = (sql, par) =>
                        {
                            Console.WriteLine("\r\n");
                            Console.WriteLine($"Sql语句:{sql}");
                            Console.WriteLine($"=========================================================================================================================================================================================================");
                        };

                        return client;
                    });

                    #endregion
                });

        }

    }

    public class DataTimeJsonConverter : JsonConverter<DateTime>
    {
        private readonly string Format;

        public DataTimeJsonConverter(string format)
        {
            this.Format = format;
        }

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.ParseExact(reader.GetString() ?? string.Empty, Format, null);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(Format));
        }
    }
}
