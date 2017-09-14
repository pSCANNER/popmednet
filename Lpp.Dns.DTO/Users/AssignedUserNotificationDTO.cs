using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Lpp.Dns.DTO {

    /// <summary>
    /// </summary>
    [DataContract]
    public class AssignedUserNotificationDTO {

        /// <summary>
        ///     Th project, organization, datamart, or datamart within project details.
        /// </summary>
        /// <value>The description.</value>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        ///     The name of the event/notification
        /// </summary>
        /// <value>The event.</value>
        [DataMember]
        public string Event { get; set; }

        /// <summary>
        ///     The event ID
        /// </summary>
        /// <value>The event identifier.</value>
        [DataMember]
        public Guid EventID { get; set; }

        /// <summary>
        ///     The level at which this notification is set.
        /// </summary>
        /// <value>The level.</value>
        [DataMember]
        public string Level { get; set; }
    }
}