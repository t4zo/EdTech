namespace EdTech.Core.Constants
{
    public static class AuthorizationConstants
    {
        public const string AllowedOrigins = "AllowedOrigins";

        public const string Remember = "rmb";

        public static class Roles
        {
            public const string AllName = "All";

            public const string Superuser = "Superuser";

            public const string User = "User";
        }

        public static class CustomClaimTypes
        {
            public const string Permissions = "Permissions";
            public const string Permission = "Permission";
        }

        public static class Permissions
        {
            public static class Alunos
            {
                public const string View = "Permissions.Alunos.View";
                public const string Create = "Permissions.Alunos.Create";
                public const string Update = "Permissions.Alunos.Update";
                public const string Delete = "Permissions.Alunos.Delete";
            }
        }
    }
}