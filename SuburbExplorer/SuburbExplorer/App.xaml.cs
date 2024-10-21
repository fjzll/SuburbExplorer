using SuburbExplorer.Services;

namespace SuburbExplorer
{
    public partial class App : Application
    {
        public static SQLService sqlService = default!;

        public static SQLService SqlService
        {
            get
            {
                if (sqlService == null)
                {
                    string databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "FavoriteSuburb.db");
                    sqlService = new SQLService(databasePath);
                }
                return sqlService;
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
    }
}
