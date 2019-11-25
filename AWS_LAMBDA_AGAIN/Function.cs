using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AWS_LAMBDA_AGAIN
{
    public class Function
    {

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        //public string FunctionHandler(List<RootObject> input, ILambdaContext context)
        //{
        //    return $"val1: {input[0].Value1}, val2: {input[0].Value2}, twovalues: {input[0].TwoValues}, studentData: {input[0].StudentData[0].Name}";
        //}
        public async Task<int> FunctionHandler(string input, ILambdaContext context)
        {
            
            var res = opAsync();
            var res1 = opAsync1();
            var res2 = opAsync2();
            Task.WaitAll(new Task[] { res, res1, res2 });
            return await Task.FromResult(1);
        }

        public async Task<int> opAsync()
        {
            await Task.Delay(500);
            return 1;
        }
        public async Task<int> opAsync1()
        {
            await Task.Delay(500);
            return 1;
        }
        public async Task<int> opAsync2()
        {
            await Task.Delay(500);
            return 1;
        }
    }
}
