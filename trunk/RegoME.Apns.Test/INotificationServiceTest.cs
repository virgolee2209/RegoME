using RegoME.Apns;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace RegoME.Apns.Test
{
    
    
    /// <summary>
    ///This is a test class for INotificationServiceTest and is intended
    ///to contain all INotificationServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class INotificationServiceTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        private const string P21Path = "D:\\@projects\\RegoME\\trunk\\RegoME.Apns.Test\\aps_developer_identity.p12";
        private const string TestDevideToken = "ced45ae0af9706b5ef69fc475d4d9f826f3d27003d49f36bf1106351ec77b4c8";

        internal virtual INotificationService CreateINotificationService()
        {
            // TODO: Instantiate an appropriate concrete class.
            INotificationService target = new NotificationService(P21Path, "regome") {
                IsUnderDevelopment = true
            };
            return target;
        }

        /// <summary>
        ///A test for SendMessage
        ///</summary>
        [TestMethod()]
        public void SendMessageTest()
        {
            INotificationService target = CreateINotificationService(); 
            Message message = new Message { 
                Id = Guid.NewGuid().ToString(),
                SenderId = 1,
                ReceiverId = 2,
                SenderName = "Hieu",
                Content = "This is a test message",
                CreateDate = DateTime.Now
            };
            target.SendMessage(TestDevideToken, message);
            //Assert.AreEqual(true, true);
            
        }

        /// <summary>
        ///A test for AcceptFriendRequest
        ///</summary>
        [TestMethod()]
        public void AcceptFriendRequestTest()
        {
            INotificationService target = CreateINotificationService(); 
            string deviceToken = TestDevideToken;
            int requestedBy = 1;
            int acceptedBy = 2;
            target.AcceptFriendRequest(deviceToken, requestedBy, acceptedBy);
            //Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddFriendRequest
        ///</summary>
        [TestMethod()]
        public void AddFriendRequestTest()
        {
            INotificationService target = CreateINotificationService(); // TODO: Initialize to an appropriate value
            string deviceToken = TestDevideToken;
            int requestedBy = 2; 
            int receivedBy = 1; 
            target.AddFriendRequest(deviceToken, requestedBy, receivedBy);
            //Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }
    }
}
