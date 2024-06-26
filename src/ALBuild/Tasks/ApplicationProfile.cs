using System.Text;

namespace ALBuild.Tasks
{
    public class ApplicationProfile
    {
        
        public string Publisher { get; set; }
        public string AppName { get; set; }
        public string VersionText { get; set; }
        public string AppID { get; set; }
        public string Tenant { get; set; }

        public string AsUrl(string baseUrl)
        {
            StringBuilder b = new StringBuilder();
            b.Append(baseUrl);

            if(!baseUrl.EndsWith("?"))
            {
                b.Append("?");
            }

            if (!String.IsNullOrEmpty(Publisher))
                b.Append($"publisher={Publisher}");

            if (!String.IsNullOrEmpty(AppName))
                b.Append($"&appName={AppName}");

            if (!String.IsNullOrEmpty(VersionText))
                b.Append($"&versionText={VersionText}");

            if (!String.IsNullOrEmpty(AppID))
                b.Append($"&appId={AppID}");

            if (!String.IsNullOrEmpty(Tenant))
                b.Append($"&tenant={Tenant}");

            return b.ToString();
        }
    }
}
