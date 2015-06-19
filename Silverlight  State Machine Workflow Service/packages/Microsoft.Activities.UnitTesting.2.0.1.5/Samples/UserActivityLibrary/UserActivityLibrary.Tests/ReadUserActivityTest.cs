namespace UserActivityLibrary.Tests
{
    using System;

    using Microsoft.Activities.Extensions;
    using Microsoft.Activities.UnitTesting;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///   Verifies the behavior of the ReadUser.xaml activity
    /// </summary>
    [TestClass]
    public class ReadUserActivityTest
    {
        #region Constants and Fields

        public readonly TimeSpan TestTimeout = TimeSpan.FromMilliseconds(100);

        #endregion

        #region Properties

        ///<summary>
        ///  Gets or sets the test context which provides
        ///  information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        #endregion

        #region Public Methods

        ///<summary>
        ///  Verifies the behavior of the ReadUser activity
        ///</summary>
        ///<remarks>
        ///  The behavior of the activity is as follows
        ///  Given 
        ///  * An activity named ReadUser
        ///  * Which prompts the user for a first name and then sets a bookmark
        ///  * and when resumed with value "First" prompts the user for a last name and then sets a bookmark
        ///  When
        ///  * The activity is resumed with the value "Last" after the second bookmark
        ///  Then
        ///  * It writes a greeting to the console saying "Hello First Last"
        ///</remarks>
        [TestMethod]
        public void ReadUserShouldDisplayAGreeting()
        {
            // Arrange
            const string firstBookmark = "FirsName"; // Intentional error  - bookmark name misspelled - change it to "FirstName" to see the text work
            const string lastBookmark = "LastName";
            const string expectedFirst = "First";
            const string expectedLast = "Last";
            const string expectedGreeting = "Hello " + expectedFirst + " " + expectedLast;
            var host = WorkflowApplicationTest.Create(new ReadUser());
            try
            {
                // Act
                // This code uses Workflow Episode support provided by extension methods from Microsoft.Activities

                // Run the workflow to the first idle point where a bookmark named "FirstName" exists
                host.TestWorkflowApplication.RunEpisode(firstBookmark, this.TestTimeout);

                // Resume the workflow with the first name
                host.TestWorkflowApplication.ResumeEpisodeBookmark(
                    firstBookmark,
                    // The name of the bookmark to resume
                    expectedFirst,
                    // The value to resume the bookmark with
                    lastBookmark,
                    // The name of the next bookmark to wait for
                    this.TestTimeout);

                // Resume the workflow with the last name and run until complete, abort or timeout
                host.TestWorkflowApplication.ResumeEpisodeBookmark(lastBookmark, expectedLast, this.TestTimeout);

                // Assert
                // The text lines property captures text written with the WriteLine activity into an array of strings
                // There are three WriteLines plus an extra empty element at the end
                Assert.AreEqual(4, host.TextLines.Length);
                Assert.AreEqual(expectedGreeting, host.TextLines[2], "The greeting was not correct");
            }
            finally
            {
                host.Tracking.Trace();
            }
        }

        #endregion
    }
}