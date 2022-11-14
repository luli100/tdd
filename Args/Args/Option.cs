namespace Args
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.All)]
    public class OptionAttribute : Attribute
    {
        public String Value { get; set; }
        public OptionAttribute(string v)
        {
            this.Value = v;
        }
    }
}