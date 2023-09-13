// See https://aka.ms/new-console-template for more information

using SqlSugar;

Console.WriteLine("----sqlsugar生成数据库实体----");

try
{
    //从属配置列表
    var connetctionList = new List<SlaveConnectionConfig>() {
        //第一个从库
        new SlaveConnectionConfig() {
            HitRate = 10,
            ConnectionString = "Data Source=DESKTOP-D0PKR3I;Initial Catalog=LiveBackgroundManagement;User ID=sa;Password=123"
        }
    };

    ConnectionConfig connectionConfig = new ConnectionConfig()
    {
        DbType = DbType.SqlServer,
        ConnectionString = "Data Source=DESKTOP-D0PKR3I;Initial Catalog=LiveBackgroundManagement;User ID=sa;Password=123",
        InitKeyType = InitKeyType.Attribute,
        //IsAutoCloseConnection = true,
        //SlaveConnectionConfigs = connetctionList
    };

    //基于数据库实体生成C#实体对象
    using (ISqlSugarClient client = new SqlSugarClient(connectionConfig))
    {
        //DBFirst
        client.DbFirst.CreateClassFile(@"D:\Vs2022工作项目\TuYi.Practice.WebSite\TuYi.Practice.SqlSugarMigration\DBModels");
    }

    Console.WriteLine("实体对象生成成功");
}
catch (Exception ex)
{
    Console.WriteLine(ex.StackTrace?.ToString());
    throw;
}