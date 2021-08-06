using System;
using ServiceStack;
using SerilogTesting.ServiceModel;

namespace SerilogTesting.ServiceInterface
{
    public class MyServices : Service
    {
        public object Any(Hello request)
        {
            return new HelloResponse { Result = $"Hello, {request.Name}!" };
        }
    }
}
