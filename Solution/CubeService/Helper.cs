using Microsoft.AspNetCore.Components.Forms;

namespace CubeService
{
    public static class Helper
    {
        public static string FirstCharToUpper(string input)
        {
            input = input.ToLower();
            return input.First().ToString().ToUpper() + input.Substring(1);
        }
    }
}
