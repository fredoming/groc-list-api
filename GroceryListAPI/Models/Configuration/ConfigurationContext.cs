using Microsoft.Extensions.Configuration;

namespace GroceryListAPI.Models.Configuration
{
  public class ConfigurationContext
  {
    public static GoogleAuth GoogleAuth { get; set; }
    public static AWSSecrets AWSSecrets { get; set;}
    public static string Environment { get; private set; }

    public static void BindSettings(IConfiguration configuration)
    {
      GoogleAuth = new GoogleAuth();
      configuration.Bind("GoogleAuth", GoogleAuth);

      AWSSecrets = new AWSSecrets();
      configuration.Bind("AWSSecrets", AWSSecrets);
    }

    public static void SetEnvironment(string env)
    {
      Environment = env; 
    }
  }
}
