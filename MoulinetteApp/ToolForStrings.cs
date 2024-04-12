namespace NMoulinetteApp
{
    class ToolForStrings
    {
        public static string RemoveGuillemets(string txt)
        {
            return txt.Substring(1, txt.Length - 2);
        }
    }
}
