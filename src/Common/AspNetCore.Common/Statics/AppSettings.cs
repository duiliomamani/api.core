using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.Common.Statics
{
    public sealed class AppSettings
    {
        public enum CoreTypeEnum
        {
            MongoDb,
            SqlServer,
            AzureStorage,
            Redis,
            RabbitMq,
            PostgreSql
        }
        public class Swagger
        {
            /// <summary>
            /// Swagger for title documentation
            /// </summary>
            public string Title { get; set; }
            /// <summary>
            /// Swagger for entry point api-docs
            /// </summary>
            public string RoutePrefix { get; set; }
            /// <summary>
            /// Swagger for Enabled documentation xml
            /// </summary>
            public bool DocumentationApi { get; set; }
            /// <summary>
            /// Swagger for Enabled api-docs endpoint
            /// </summary>
            public bool Enabled { get; set; }
        }
        public class ConnectionString
        {
            public CoreTypeEnum Core { get; set; }
            public string Connection { get; set; }
            public string Server { get; set; }
            public string Password { get; set; }
        }
        public class Auth
        {
            public string Domain { get; set; }
            public string Audience { get; set; }
        }
    }
}
