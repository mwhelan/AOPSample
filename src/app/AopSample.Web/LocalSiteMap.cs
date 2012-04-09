namespace AopSample.Web
{
    public static class LocalSiteMap
    {
        public static class PageLinks
        {
            //public static readonly string Home = "home";
            //public static readonly string About = "about";

            public static class Home
            {
                public static readonly string CreateShow = "Create Show";
                public static readonly string ShowList = "Show List";
            }

            public static class Shows
            {
                public static readonly string Index = "Show List";
                public static readonly string Create = "Create Show";
                public static readonly string Edit = "Edit";
                public static readonly string Details = "View";
            }
        }

        public static class PageTitles
        {
            public static class Home
            {
                public static readonly string HomePage = "Episode Guide - Home Page";
            }

            public static class Shows
            {
                public static readonly string Index = "Show List";
                public static readonly string Create = "Create Show";
                public static readonly string Edit = "Edit Show";
            }
        }

        public static class Screen
        {
            public static class Shows
            {
                public static readonly string Index = "listShow";
                public static readonly string Create = "createShow";
                public static readonly string Edit = "editShow";
            }
        }

        public static class Buttons
        {
            public static readonly string Save = "Save";
        }

        public static class Urls
        {
            public static string HomePage { get; set; }

            public static class Shows
            {
                public static string Create
                {
                    get
                    {
                        return string.Format(@"{0}/Shows/Create", HomePage);
                    }
                }

                public static string List
                {
                    get
                    {
                        return string.Format(@"{0}/Shows/Index", HomePage);
                    }
                }
            }
        }

    }
}