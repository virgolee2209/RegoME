using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using JdSoft.Apple.Apns.Notifications;
using JdSoftNotifications = JdSoft.Apple.Apns.Notifications;

namespace RegoME.Apns
{
    public class NotificationService :INotificationService
    {
        #region Properties

        #region Configurable
        /// <summary>
        /// File path to the .p12 or .pfx file that identify the apps
        /// </summary>
        public string P12FilePath { get; set; }

        /// <summary>
        /// Password to the .p12 file
        /// </summary>
        public string P12FilePassword { get; set; }

        /// <summary>
        /// Indicated whether the product is still under development
        /// </summary>
        public bool IsUnderDevelopment { get; set; } 
        #endregion

        //Notification service 
        private JdSoftNotifications.NotificationService notificationService;
        
        //nubmer of connections
        private int noConnections;

        private static readonly ILog log = LogManager.GetLogger(
                System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        /// <summary>
        /// Default constructor. The p12 file are required to be set up manually if this is used.
        /// </summary>
        public NotificationService() {
            Init();
        }

        /// <summary>
        /// Initialize the notification logic service object 
        /// </summary>
        /// <param name="p12FilePath">File path to the .p12 or .pfx file that identify the apps</param>
        /// <param name="p12FilePassword">Password to the .p12 file</param>
        public NotificationService(string p12FilePath, string p12FilePassword){
            P12FilePath = p12FilePath;
            P12FilePassword = p12FilePassword;
            Init();
        }

        //Initialize
        private void Init() {
            noConnections = 1;//defaut to 1 for testing purposes
            notificationService = new JdSoftNotifications.NotificationService(IsUnderDevelopment, P12FilePath, P12FilePassword, noConnections);

            notificationService.SendRetries = 5; //5 retries before generating notificationfailed event
            notificationService.ReconnectDelay = 5000; //5 seconds

            notificationService.Error += new JdSoftNotifications.NotificationService.OnError(service_Error);
            notificationService.NotificationTooLong += new JdSoftNotifications.NotificationService.OnNotificationTooLong(service_NotificationTooLong);
            notificationService.BadDeviceToken += new JdSoftNotifications.NotificationService.OnBadDeviceToken(service_BadDeviceToken);
            notificationService.NotificationFailed += new JdSoftNotifications.NotificationService.OnNotificationFailed(service_NotificationFailed);
            notificationService.NotificationSuccess += new JdSoftNotifications.NotificationService.OnNotificationSuccess(service_NotificationSuccess);
            notificationService.Connecting += new JdSoftNotifications.NotificationService.OnConnecting(service_Connecting);
            notificationService.Connected += new JdSoftNotifications.NotificationService.OnConnected(service_Connected);
            notificationService.Disconnected += new JdSoftNotifications.NotificationService.OnDisconnected(service_Disconnected);
        }

        #region Implement INotificationLogic
        public void SendMessage(string deviceToken, Message message)
        {
            //create the notification object
            var notification = new Notification(deviceToken);

            notification.Payload.Alert.Body = message.Content;
            notification.Payload.Sound = "default";
            notification.Payload.Badge = 1;

            notification.Payload.AddCustom("Type", NotificationType.Message.ToString());
            notification.Payload.AddCustom("Id", message.Id);
            notification.Payload.AddCustom("Sender", message.SenderId);
            notification.Payload.AddCustom("Receiver", message.ReceiverId);
            notification.Payload.AddCustom("Content", message.Content);
            notification.Payload.AddCustom("CreateDate", message.CreateDate);

            //queue the notification for notification
            notificationService.QueueNotification(notification);
        }

        public void AddFriendRequest(string deviceToken, int requestedBy, int receivedBy)
        {
            //create the notification object
            var notification = new Notification(deviceToken);

            notification.Payload.Sound = "default";
            notification.Payload.Badge = 1;

            notification.Payload.AddCustom("Type", NotificationType.AddFriend.ToString());
            notification.Payload.AddCustom("RequestedBy", requestedBy);
            notification.Payload.AddCustom("ReceivedBy", receivedBy);

            //queue the notification for notification
            notificationService.QueueNotification(notification);
        }

        public void AcceptFriendRequest(string deviceToken, int requestedBy, int acceptedBy)
        {
            //create the notification object
            var notification = new Notification(deviceToken);

            notification.Payload.Sound = "default";
            notification.Payload.Badge = 1;

            notification.Payload.AddCustom("Type", NotificationType.AcceptFriend.ToString());
            notification.Payload.AddCustom("RequestedBy", requestedBy);
            notification.Payload.AddCustom("ReceivedBy", acceptedBy);

            //queue the notification for notification
            notificationService.QueueNotification(notification);
        } 
        #endregion

        //TODO: implement the event handler properly
        static void service_BadDeviceToken(object sender, BadDeviceTokenException ex)
        {
            log.Debug(string.Format("Bad Device Token: {0}", ex.Message));
        }

        static void service_Disconnected(object sender)
        {
            log.Debug("Disconnected...");
        }

        static void service_Connected(object sender)
        {
            log.Debug("Connected...");
        }

        static void service_Connecting(object sender)
        {
            log.Debug("Connecting...");
        }

        static void service_NotificationTooLong(object sender, NotificationLengthException ex)
        {
            log.Debug(string.Format("Notification Too Long: {0}", ex.Notification.ToString()));
        }

        static void service_NotificationSuccess(object sender, Notification notification)
        {
            log.Debug(string.Format("Notification Success: {0}", notification.ToString()));
        }

        static void service_NotificationFailed(object sender, Notification notification)
        {
            log.Debug(string.Format("Notification Failed: {0}", notification.ToString()));
        }

        static void service_Error(object sender, Exception ex)
        {
            log.Debug(string.Format("Error: {0}", ex.Message));
        }
    }
}
