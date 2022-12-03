using Newtonsoft.Json;
using System.Reflection;

namespace AssetManagementTeam6.API.Test
{
    public class TestBase
    {
        public static TResult ReadJsonFromFile<TResult>(string fileName)
        {
            var dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filePath = Path.Combine(dir!, "TestData", fileName);

            if (!File.Exists(filePath))
            {
                return default!;
            }

            var jsonString = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<TResult>(jsonString);
        }
    }
}
