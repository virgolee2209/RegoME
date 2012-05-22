using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegoME.Apns
{
    public interface INotificationService
    {
        /// <summary>
        /// Send a chat message to an user
        /// </summary>
        /// <param name="deviceToken">The notification will be sent to the device with this Id</param>
        /// <param name="message">Chat Mesage to be sent to the device</param>
        void SendMessage(string deviceToken, Message message);

        /// <summary>
        /// Send a add friend request notification 
        /// </summary>
        /// <param name="deviceToken">The notification will be sent to the device with this Id</param>
        /// <param name="requestedBy">The id of the user that made the add friend request</param>
        /// <param name="receivedBy">The id of the user to whom the request has be made</param>
        void AddFriendRequest(string deviceToken, int requestedBy, int receivedBy);

        /// <summary>
        /// Send an friend request acceptance notification to the user that made the request
        /// </summary>
        /// <param name="deviceToken">The notification will be sent to the device with this Id</param>
        /// <param name="requestedBy">The id of the user that made the add friend request</param>
        /// <param name="acceptedBy">The id of the user who accepted the request </param>
        void AcceptFriendRequest(string deviceToken, int requestedBy, int acceptedBy);
    }
}
