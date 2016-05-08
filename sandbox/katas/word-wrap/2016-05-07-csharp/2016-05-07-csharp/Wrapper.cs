namespace Word_wrap_2016_05_07_csharp
{
    public static class Wrapper
    {
        public static string Wrap(string s, int len)
        {
            if (s.Length <= len)
                return s;

            var prefix = s.Substring(0, len);
            var suffix = s.Substring(len);

            if (prefix.Contains(" "))
                return WrapOnLastSpace(s, prefix, len);

            return prefix + "\n" + Wrap(suffix, len);
        }

        public static string WrapOnLastSpace(string s, string pfx, int len)
        {
            var lastSpace = pfx.LastIndexOf(" ");
            var prefix = s.Substring(0, lastSpace).TrimEnd();
            var suffix = s.Substring(lastSpace).TrimStart();

            return prefix + "\n" + Wrap(suffix, len);
        }
    }
}