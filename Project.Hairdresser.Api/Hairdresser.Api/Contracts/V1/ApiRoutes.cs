namespace Hairdresser.Api.Contracts.V1
{

    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class Post
        {
            public const string GetAll = Base + "/posts";
            public const string Create = Base + "/posts";
            public const string Get = Base + "/posts/{postId}";
            public const string Update = Base + "/posts/{postId}";
            public const string Delete = Base + "/posts/{postId}";
        }

        public static class Identity
        {
            public const string Login = Base + "/identity/login";
            public const string Register = Base + "/identity/register";
            public const string RegisterAdmin = Base + "/identity/registeradmin";
        }

        public static class Tags
        {
            public const string GetAll = Base + "/tags";
            public const string Create = Base + "/tags";
            public const string Get = Base + "/tags/{tagName}";
            public const string Delete = Base + "/tags/{tagName}";
        }

    }
}
