﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Moq;
using static System.Net.WebRequestMethods;
using System.Web.Routing;
using SportsStore.WebUI;
using System.Reflection;

namespace SportsStore.UnitTests
{
    [TestClass]
    class RouteTests
    {
         private HttpContextBase CreateHttpContext(string targeturl =null,
                                                string httpmethod ="Get")
        {
            Mock<HttpRequestBase> mockRequest = new Mock<HttpRequestBase>();
            mockRequest.Setup(m => m.CurrentExecutionFilePath).
                            Returns(targeturl);
            mockRequest.Setup(m => m.HttpMethod).Returns(httpmethod);
            //create mock response
            Mock<HttpResponseBase> mockResponse = new Mock<HttpResponseBase>();
            mockResponse.Setup(m => m.ApplyAppPathModifier(It.IsAny<string>())).
                Returns<string>(s => s);
            // create the mock context, using the request and response
            Mock<HttpContextBase> mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(m => m.Request).Returns(mockRequest.Object);
            mockContext.Setup(m => m.Response).Returns(mockResponse.Object);

            //return the mocked context
            return mockContext.Object;


        }
        private void TestRouteMatch(string url, string controller, string action,
                object routeProperties = null, string httpMethod = "GET")
        {
            // Arrange
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);
            // Act - process the route
            RouteData result
            = routes.GetRouteData(CreateHttpContext(url, httpMethod));
            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(TestIncomingRouteResult(result, controller,
            action, routeProperties));
        }

        private bool TestIncomingRouteResult(RouteData routeResult,
                        string controller, string action, object propertySet = null)
        {
            Func<object, object, bool> valCompare = (v1, v2) => {
                return StringComparer.InvariantCultureIgnoreCase
                .Compare(v1, v2) == 0;
            };
            bool result = valCompare(routeResult.Values["controller"], controller)
            && valCompare(routeResult.Values["action"], action);
            if (propertySet != null)
            {
                PropertyInfo[] propInfo = propertySet.GetType().GetProperties();
                foreach (PropertyInfo pi in propInfo)
                {
                    if (!(routeResult.Values.ContainsKey(pi.Name)
                    && valCompare(routeResult.Values[pi.Name],
                    pi.GetValue(propertySet, null))))
                    {
                        result = false;
                        break;
                    }
                }
            }
            return result;
        }

        private void TestRouteFail(string url)
        {
            // Arrange
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);
            // Act - process the route
            RouteData result = routes.GetRouteData(CreateHttpContext(url));
            // Assert
            Assert.IsTrue(result == null || result.Route == null);
        }
        [TestMethod]
        public void TestIncomingRoutes()
        {
            // check for the URL that is hoped for
            TestRouteMatch("~/Product","Product","List");
            // check that the values are being obtained from the segments
            TestRouteMatch("~/Product/List","Product","List");
            // ensure that too many or too few segments fails to match
            TestRouteFail("~/Admin/Index/Segment");
            TestRouteFail("~/Admin");
        }
    }
}
