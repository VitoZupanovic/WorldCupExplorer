namespace DataLayer.Helpers
{
    public static class TeamDisplayHelper
    {
        public static string ExtractFifaCode(string displayText)
        {
            if (string.IsNullOrWhiteSpace(displayText))
            {
                return string.Empty;
            }

            int openBracketIndex = displayText.LastIndexOf('(');
            int closeBracketIndex = displayText.LastIndexOf(')');

            if (openBracketIndex == -1 || closeBracketIndex == -1 || closeBracketIndex <= openBracketIndex)
            {
                return string.Empty;
            }

            return displayText
                .Substring(openBracketIndex + 1, closeBracketIndex - openBracketIndex - 1)
                .Trim();
        }
    }
}
