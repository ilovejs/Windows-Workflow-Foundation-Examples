namespace MathActivityLibrary.Tests
{
    using Microsoft.Activities.UnitTesting;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MathActivityTest
    {
        #region Properties

        ///<summary>
        ///  Gets or sets the test context which provides
        ///  information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        #endregion

        #region Public Methods

        [TestMethod]
        public void SumAddsTwoNumbers()
        {
            // Arrange
            var activity = new Sum();

            var host = WorkflowInvokerTest.Create(activity);

            // InArguments is a dynamic object of type Microsoft.Activities.WorkflowArguments
            host.InArguments.Num1 = 1;
            host.InArguments.Num2 = 2;

            try
            {
                // Act
                host.TestActivity();

                // Assert
                // Note: The host automatically captures the out arguments
                host.AssertOutArgument.AreEqual("Result", 3);

                Assert.AreEqual(3, host.OutArguments.Result);
            }
            finally
            {
                // Note: The host automatically captures tracking
                host.Tracking.Trace();
            }
        }

        #endregion
    }
}