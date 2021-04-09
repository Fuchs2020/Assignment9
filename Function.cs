using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Amazon.Lambda.APIGatewayEvents;

using Amazon.Lambda.Core;
using System.Dynamic;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Assignment9
{
    public class Function
    {
        public static readonly HttpClient client = new HttpClient(); 

        public async Task<ExpandoObject> FunctionHandler(APIGatewayProxyRequest input, ILambdaContext context)
        {
            //create the expando object
            dynamic myData = new ExpandoObject();
            //create dictionary 
            Dictionary<string, string> myDict = (Dictionary<string, string>)input.QueryStringParameters;
            //create string to hold url and the api keys
            string myURL = await client.GetStringAsync("https://api.nytimes.com/svc/books/v3/lists/current/" 
                + myDict.First().Value + ".json?api-key=QQnAKXim2n4Xj8aYi0qEvVwMGybRqg8V");
            //store deserialized object
            dynamic myObjects = JsonConvert.DeserializeObject<ExpandoObject>(myURL);
            return myObjects;
        }
    }
}
