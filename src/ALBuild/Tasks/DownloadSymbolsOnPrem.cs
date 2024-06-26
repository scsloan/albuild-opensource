using Newtonsoft.Json.Linq;

namespace ALBuild.Tasks
{
    public class DownloadSymbolsOnPrem
    { 
        public DownloadSymbolsOnPrem()
        {

        }

        public async Task<Result> RunAsync(JObject Settings)
        {
            try
            {
                string DevBaseUrl = Settings["DevBaseUrl"].ToString();
                List<ApplicationProfile> apps = this.BuildApplicationProfiles();

                foreach(var app in apps)
                {
                    Console.WriteLine($"- Downloading {app.AppName} by {app.Publisher}");
                    await DownloadApplicationSymbols(DevBaseUrl, Settings["ServerInstance"].ToString(), app, Settings["AppPath"].ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Download Failed: {0}", ex.Message);
                return new Result(false, ex.Message);
            }

            return new Result(true, string.Empty);

        }

        private List<ApplicationProfile> BuildApplicationProfiles()
        {
            //Todo. I think we can get all this from the metadata endpoint, saving us having to maintain this
            //      endpoint: http://bc.selectcustomsolutions.com:6049/{Instance}/dev/metadata?tenant=default


            List<ApplicationProfile> profiles = new List<ApplicationProfile>
            {
                new ApplicationProfile() { Publisher = "Microsoft", AppName = "Application", VersionText = "23.0.0.0", AppID = "00000000-0000-0000-0000-000000000000", Tenant = "default" },
                new ApplicationProfile() { Publisher = "Microsoft", AppName = "System", VersionText = "1.0.0.0", AppID = "8874ed3a-0643-4247-9ced-7a7002f7135d", Tenant = "default" },
                new ApplicationProfile() { Publisher = "Microsoft", AppName = "System Application", VersionText = "23.4.0.0", AppID = "63ca2fa4-4f03-4f2b-a480-172fef340d3f", Tenant = "default" },
                new ApplicationProfile() { Publisher = "Microsoft", AppName = "Base Application", VersionText = "23.4.0.0", AppID = "437dbf0e-84ff-417a-965d-ed2bb9650972", Tenant = "default" }
            };

            return profiles;
        }

        private async Task DownloadApplicationSymbols(string baseURL, string ServerInstance, ApplicationProfile app, string SavePath)
        {
            if(!baseURL.EndsWith("/"))
            {
                ServerInstance = $"/{ServerInstance}/dev/packages?";
            }

            baseURL += ServerInstance;

            HttpClient httpClient = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true });

            var result = await httpClient.GetByteArrayAsync(app.AsUrl(baseURL));
            File.WriteAllBytes($"{SavePath}\\.alpackages\\{app.Publisher}_{app.AppName}_{app.VersionText}.app", result);
        }
    }
}
