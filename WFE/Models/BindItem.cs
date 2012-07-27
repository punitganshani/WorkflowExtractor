namespace WFE.Models
{
    public class BindItem
    {
        public string Name { get; set; }
        public object Value { get; set; }

        public BindItem(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
