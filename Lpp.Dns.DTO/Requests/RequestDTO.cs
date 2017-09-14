using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using Lpp.Objects;
using Lpp.Dns.DTO.Enums;
using Lpp.Objects.ValidationAttributes;

namespace Lpp.Dns.DTO {

    /// <summary>
    ///     Request
    /// </summary>
    /// <seealso cref="Lpp.Objects.EntityDtoWithID"/>
    [DataContract]
    public class RequestDTO : EntityDtoWithID {

        /// <summary>
        ///     Activity dscription
        /// </summary>
        /// <value>The activity description.</value>
        [DataMember]
        [MaxLength(255)]
        public string ActivityDescription { get; set; }

        /// <summary>
        ///     Gets or sets the ID of Activity
        /// </summary>
        /// <value>The activity identifier.</value>
        [DataMember]
        public Guid? ActivityID { get; set; }

        /// <summary>
        ///     Adapter Package version
        /// </summary>
        /// <value>The adapter package version.</value>
        [DataMember]
        public string AdapterPackageVersion { get; set; }

        /// <summary>
        ///     Additional instructions
        /// </summary>
        /// <value>The additional instructions.</value>
        [DataMember]
        public string AdditionalInstructions { get; set; }

        /// <summary>
        ///     The date the request was canceled on
        /// </summary>
        /// <value>The cancelled on.</value>
        [DataMember]
        public DateTimeOffset? CancelledOn { get; set; }

        /// <summary>
        ///     Th date the request was completed on
        /// </summary>
        /// <value>The completed on.</value>
        [DataMember]
        public DateTimeOffset? CompletedOn { get; set; }

        /// <summary>
        ///     Created By
        /// </summary>
        /// <value>The created by.</value>
        [DataMember]
        public string CreatedBy { get; set; }

        /// <summary>
        ///     Gets or sets the ID of createdby
        /// </summary>
        /// <value>The created by identifier.</value>
        [DataMember]
        public Guid CreatedByID { get; set; }

        /// <summary>
        ///     The date the request was created on
        /// </summary>
        /// <value>The created on.</value>
        [DataMember]
        public DateTimeOffset CreatedOn { get; set; }

        /// <summary>
        ///     Current workflow activity
        /// </summary>
        /// <value>The current work flow activity.</value>
        [DataMember, ReadOnly(true)]
        public string CurrentWorkFlowActivity { get; set; }

        /// <summary>
        ///     Gets or sets the ID of current workflow activity
        /// </summary>
        /// <value>The current work flow activity identifier.</value>
        [DataMember]
        public Guid? CurrentWorkFlowActivityID { get; set; }

        /// <summary>
        ///     Gets or sets the indicator to specify if request is deleted
        /// </summary>
        /// <value><c>true</c> if deleted; otherwise, <c>false</c>.</value>
        [DataMember]
        public bool Deleted { get; set; }

        /// <summary>
        ///     Description
        /// </summary>
        /// <value>The description.</value>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        ///     Due date
        /// </summary>
        /// <value>The due date.</value>
        [DataMember]
        public DateTimeOffset? DueDate { get; set; }

        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [DataMember, ReadOnly(true)]
        public long Identifier { get; set; }

        /// <summary>
        ///     IRB Approval No
        /// </summary>
        /// <value>The irb approval no.</value>
        [DataMember]
        [MaxLength(100)]
        public string IRBApprovalNo { get; set; }

        /// <summary>
        ///     Major EventBy
        /// </summary>
        /// <value>The major event by.</value>
        [DataMember, ReadOnly(true)]
        public string MajorEventBy { get; set; }

        /// <summary>
        ///     ID of major event
        /// </summary>
        /// <value>The major event by identifier.</value>
        [DataMember, ReadOnly(true)]
        public Guid MajorEventByID { get; set; }

        /// <summary>
        ///     The Date the major event date was submitted
        /// </summary>
        /// <value>The major event date.</value>
        [DataMember, ReadOnly(true)]
        public DateTimeOffset MajorEventDate { get; set; }

        /// <summary>
        ///     Gets or sets the indicator to specify if source/budget box is Checked
        /// </summary>
        /// <value><c>true</c> if [mirror budget fields]; otherwise, <c>false</c>.</value>
        [DataMember]
        public bool MirrorBudgetFields { get; set; }

        /// <summary>
        ///     Returns identifier
        /// </summary>
        /// <value>The identifier.</value>
        [DataMember]
        public string MSRequestID { get; set; }

        /// <summary>
        ///     Name
        /// </summary>
        /// <value>The name.</value>
        [DataMember]
        [MaxLength(255)]
        public string Name { get; set; }

        /// <summary>
        ///     Organization
        /// </summary>
        /// <value>The organization.</value>
        [DataMember]
        public string Organization { get; set; }

        /// <summary>
        ///     Gets or sets the ID of rganization
        /// </summary>
        /// <value>The organization identifier.</value>
        [DataMember]
        public Guid OrganizationID { get; set; }

        /// <summary>
        ///     Gets or sets the ID of parent request
        /// </summary>
        /// <value>The parent request identifier.</value>
        [DataMember]
        public Guid? ParentRequestID { get; set; }

        /// <summary>
        ///     PHI Disclosure level
        /// </summary>
        /// <value>The phi disclosure level.</value>
        [DataMember]
        public string PhiDisclosureLevel { get; set; }

        /// <summary>
        ///     Gets or sets the priorities
        /// </summary>
        /// <value>The priority.</value>
        [DataMember]
        public Priorities Priority { get; set; }

        /// <summary>
        ///     Project
        /// </summary>
        /// <value>The project.</value>
        [DataMember]
        public string Project { get; set; }

        /// <summary>
        ///     Gets or sets the ID of projct
        /// </summary>
        /// <value>The project identifier.</value>
        [DataMember]
        public Guid ProjectID { get; set; }

        /// <summary>
        ///     Purpose of use
        /// </summary>
        /// <value>The purpose of use.</value>
        [DataMember]
        [MaxLength(100)]
        public string PurposeOfUse { get; set; }

        /// <summary>
        ///     Query
        /// </summary>
        /// <value>The query.</value>
        [DataMember]
        public string Query { get; set; }

        /// <summary>
        ///     Report Aggregation Level
        /// </summary>
        /// <value>The report aggregation level.</value>
        [DataMember]
        public string ReportAggregationLevel { get; set; }

        /// <summary>
        ///     Gets or sets the ID of Report Aggregation Level
        /// </summary>
        /// <value>The report aggregation level identifier.</value>
        [DataMember]
        public Guid? ReportAggregationLevelID { get; set; }

        /// <summary>
        ///     Requester Center
        /// </summary>
        /// <value>The requester center.</value>
        [DataMember]
        public string RequesterCenter { get; set; }

        /// <summary>
        ///     Gets or sets the ID of Requester Center
        /// </summary>
        /// <value>The requester center identifier.</value>
        [DataMember]
        public Guid? RequesterCenterID { get; set; }

        /// <summary>
        ///     Request type
        /// </summary>
        /// <value>The type of the request.</value>
        [DataMember]
        public string RequestType { get; set; }

        /// <summary>
        ///     Gets or sets the ID of Request Type
        /// </summary>
        /// <value>The request type identifier.</value>
        [DataMember]
        public Guid RequestTypeID { get; set; }

        /// <summary>
        ///     Schedule
        /// </summary>
        /// <value>The schedule.</value>
        [DataMember]
        public string Schedule { get; set; }

        /// <summary>
        ///     Gets or sets the schedule count
        /// </summary>
        /// <value>The schedule count.</value>
        [DataMember]
        public int ScheduleCount { get; set; }

        /// <summary>
        ///     Gets or sets the indicator to specify if request is scheduled
        /// </summary>
        /// <value><c>true</c> if scheduled; otherwise, <c>false</c>.</value>
        [DataMember]
        public bool Scheduled { get; set; }

        /// <summary>
        ///     Gets or sets the ID of Source Activity
        /// </summary>
        /// <value>The source activity identifier.</value>
        [DataMember]
        public Guid? SourceActivityID { get; set; }

        /// <summary>
        ///     Gets or sets the ID of Source Activity Project
        /// </summary>
        /// <value>The source activity project identifier.</value>
        [DataMember]
        public Guid? SourceActivityProjectID { get; set; }

        /// <summary>
        ///     Gets or sets the ID of Source Task Order
        /// </summary>
        /// <value>The source task order identifier.</value>
        [DataMember]
        public Guid? SourceTaskOrderID { get; set; }

        /// <summary>
        ///     Gets or set the request status
        /// </summary>
        /// <value>The status.</value>
        [DataMember, ReadOnly(true)]
        public RequestStatuses Status { get; set; }

        /// <summary>
        ///     Status Text
        /// </summary>
        /// <value>The status text.</value>
        [DataMember, ReadOnly(true)]
        public string StatusText { get; set; }

        /// <summary>
        ///     Submitted By
        /// </summary>
        /// <value>The submitted by.</value>
        [DataMember]
        public string SubmittedBy { get; set; }

        /// <summary>
        ///     The ID of the user who submitted the request
        /// </summary>
        /// <value>The submitted by identifier.</value>
        [DataMember]
        public Guid? SubmittedByID { get; set; }

        /// <summary>
        ///     Submitted By Name
        /// </summary>
        /// <value>The name of the submitted by.</value>
        [DataMember]
        public string SubmittedByName { get; set; }

        /// <summary>
        ///     Th date the request was submitted on
        /// </summary>
        /// <value>The submitted on.</value>
        [DataMember]
        public DateTimeOffset? SubmittedOn { get; set; }

        /// <summary>
        ///     Gets or sets the indicator to specify if template is selected
        /// </summary>
        /// <value><c>true</c> if template; otherwise, <c>false</c>.</value>
        [DataMember]
        public bool Template { get; set; }

        /// <summary>
        ///     Updated By
        /// </summary>
        /// <value>The updated by.</value>
        [DataMember]
        public string UpdatedBy { get; set; }

        /// <summary>
        ///     Gets or sets the ID of user who updated the request
        /// </summary>
        /// <value>The updated by identifier.</value>
        [DataMember]
        public Guid UpdatedByID { get; set; }

        /// <summary>
        ///     The date the request was updated on
        /// </summary>
        /// <value>The updated on.</value>
        [DataMember]
        public DateTimeOffset UpdatedOn { get; set; }

        /// <summary>
        ///     User identifier
        /// </summary>
        /// <value>The user identifier.</value>
        [DataMember, MaxLength(100)]
        public string UserIdentifier { get; set; }

        /// <summary>
        ///     Workflow
        /// </summary>
        /// <value>The workflow.</value>
        [DataMember, ReadOnly(true)]
        public string Workflow { get; set; }

        /// <summary>
        ///     Gets or sets the ID of workflow
        /// </summary>
        /// <value>The workflow identifier.</value>
        [DataMember]
        public Guid? WorkflowID { get; set; }

        /// <summary>
        ///     Workplan Type
        /// </summary>
        /// <value>The type of the workplan.</value>
        [DataMember]
        public string WorkplanType { get; set; }

        /// <summary>
        ///     Gets or sets the ID of workplan type
        /// </summary>
        /// <value>The workplan type identifier.</value>
        [DataMember]
        public Guid? WorkplanTypeID { get; set; }

        /// <summary>
        ///     Request DTO
        /// </summary>
        public RequestDTO() { }
    }
}