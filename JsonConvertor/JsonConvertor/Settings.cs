namespace JsonConvertor
{
    public static class Settings
    {
        public static char KeyDelimiter { get; set; } = '\"';

        public static char KeySeparator { get; set; } = '.';

        public static char ValueSeparator { get; set; } = '\t';

        public static char IndentChar { get; set; } = ' ';

        public static int Indentation { get; set; } = 4;
    }
}
