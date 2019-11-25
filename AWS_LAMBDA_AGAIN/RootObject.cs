using Newtonsoft.Json;
using System.Collections.Generic;

namespace AWS_LAMBDA_AGAIN
{
    public class RootObject
    {
        public string Value1 { get; set; }
        public string Value2 { get; set; }

        [JsonIgnore]
        public string TwoValues => Value1 + Value2;
        public List<Student> StudentData { get; set; }
    }
}