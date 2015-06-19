namespace MathServiceLibrary.Tests
{
    using System.ServiceModel;

    using Microsoft.Activities.UnitTesting;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class IncrementServiceTest
    {
        #region Constants and Fields

        private static readonly EndpointAddress serviceAddress = new EndpointAddress("net.pipe://localhost/IncrementService");

        private static NetNamedPipeBinding binding = new NetNamedPipeBinding();

        #endregion

        #region Properties

        ///<summary>
        ///  Gets or sets the test context which provides
        ///  information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        #endregion

        #region Public Methods

[TestMethod]
// You must deploy the XAMLX file to the test directory
[DeploymentItem(@"MathServiceLibrary\IncrementService.xamlx")]
public void IncrementServiceShouldIncrementData()
{
    // Arrange
    const int initialData = 1;
    const int expectedData = 2;

    WorkflowServiceTestHost host = null;
    try
    {
        using (host = WorkflowServiceTestHost.Open("IncrementService.xamlx", serviceAddress))
        {
            var proxy = new IncrementService.ServiceClient(binding, serviceAddress);
            int? value = initialData;
            proxy.Increment(ref value);
            Assert.AreEqual(expectedData, value, "Increment did not correctly increment the value");
        }
    }
    finally
    {
        if (host != null)
        {
            host.Tracking.Trace();
        }
    }
}

        #endregion
    }
}