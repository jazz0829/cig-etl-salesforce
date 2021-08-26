using System.Globalization;
using System.Text;

namespace Eol.Cig.Etl.Salesforce.Client
{
	public partial class SforceService
	{
		public string Pod
		{
			get
			{
				var podPart = new StringBuilder();
				var i = 0;
				var urlParts = this.Url.Split('.');
				while (!urlParts[i].Contains("salesforce"))
				{
					podPart.Append(urlParts[i]).Append(".");
					i++;
				}
				var podPartValue = podPart.ToString().Substring(0, podPart.Length - 1).ToLower(CultureInfo.InvariantCulture);
				var pod = podPartValue.Replace("https://", string.Empty);

				return pod;
			}
		}
	}
}
