using System.Collections.Generic;

namespace Project.Common.Common
{
    public class Selection : Select<int, string>
    {}

    public class Option : Option<int, string>
    {
        public Option()
        {

        }
        public Option(int key, string value) : base(key, value)
        {
        }
    }
    public class Select<TK,TV>
    {
        public TK Selected { get; set; }
        public IEnumerable<Option<TK, TV>> Options { get; set; }
    }
    public class Option<TK, TV>
    {
        public Option()
        {
            
        }
        public Option(TK key,TV value)
        {
            Key = key;
            Value = value;
        }
        public TK Key { get; set; }
        public TV Value  { get; set; }
    }
}
