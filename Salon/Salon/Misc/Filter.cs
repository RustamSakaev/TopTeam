namespace Salon.Misc
{
    internal class Filter
    {
        public string InnerKey;
        public string Key;
        public string Expression;

        public Filter(string innerKey, string key, string expression)
        {
            InnerKey = innerKey;
            Key = key;
            Expression = expression;
        }
    }
}
