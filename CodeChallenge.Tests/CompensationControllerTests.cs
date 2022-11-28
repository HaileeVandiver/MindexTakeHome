using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CodeChallenge.Models;

using CodeCodeChallenge.Tests.Integration.Extensions;
using CodeCodeChallenge.Tests.Integration.Helpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeChallenge.Tests.Integration
{
        [TestClass]
        public class CompensationControllerTests
        {
        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        // Attribute ClassInitialize requires this signature
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer();
            _httpClient = _testServer.NewClient();
        }
        [TestCleanup]
            public void CleanUpTest()
            {
                _httpClient.Dispose();
                _testServer.Dispose();
            }

            [TestMethod]
            public void CreateCompensation_Returns_Compensation()
            {
            // Arrange
            var compensation = new Compensation()
            {
                Employee = "b7839309-3348-463b-a7e3-5de1c168beb3", // Paul Mccartney
                Salary = 90000,
                EffectiveDate ="09/10/2020"
            };

                var requestContent = new JsonSerialization().ToJson(compensation);
      
                // Execute
                Task<HttpResponseMessage> postRequestTask = _httpClient.PostAsync("api/compensation",
                new StringContent(requestContent, Encoding.UTF8, "application/json"));
                postRequestTask.Wait();
                var response = postRequestTask.Result;

                // Assert
                Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

                var newCompensation = response.DeserializeContent<Compensation>();
                Assert.AreEqual(compensation.Employee, newCompensation.Employee);
                Assert.AreEqual(compensation.Salary, newCompensation.Salary);
                Assert.AreEqual(compensation.EffectiveDate, newCompensation.EffectiveDate);
            }

            [TestMethod]
            public void GetCompensation_Returns_NotFound()
            {
                // Execute
                var id = "BadId";
                var getTask = _httpClient.GetAsync($"api/compensation/{id}");
                var response = getTask.Result;

                // Assert
                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            }
        }
}
