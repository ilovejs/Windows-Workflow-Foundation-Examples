// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WorkflowServiceTraceBehavior.cs" company="Microsoft">
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// <summary>
//   The workflow service trace behavior.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SecurityDoor.Web
{
    using System;
    using System.Activities.Tracking;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.ServiceModel;
    using System.ServiceModel.Activities;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Description;

    using Microsoft.Activities.Diagnostics;
    using Microsoft.Activities.Tracking;
    using Microsoft.Activities.UnitTesting.Tracking;

    /// <summary>
    /// The workflow service trace behavior.
    /// </summary>
    public class WorkflowServiceTraceBehavior : TrackingParticipant, IServiceBehavior
    {
        #region Implemented Interfaces

        #region IServiceBehavior

        /// <summary>
        /// The add binding parameters.
        /// </summary>
        /// <param name="serviceDescription">
        /// The service description.
        /// </param>
        /// <param name="serviceHostBase">
        /// The service host base.
        /// </param>
        /// <param name="endpoints">
        /// The endpoints.
        /// </param>
        /// <param name="bindingParameters">
        /// The binding parameters.
        /// </param>
        public void AddBindingParameters(
            ServiceDescription serviceDescription, 
            ServiceHostBase serviceHostBase, 
            Collection<ServiceEndpoint> endpoints, 
            BindingParameterCollection bindingParameters)
        {
        }

        /// <summary>
        /// The apply dispatch behavior.
        /// </summary>
        /// <param name="serviceDescription">
        /// The service description.
        /// </param>
        /// <param name="serviceHostBase">
        /// The service host base.
        /// </param>
        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            if (serviceHostBase is WorkflowServiceHost)
            {
                var host = serviceHostBase as WorkflowServiceHost;
                host.WorkflowExtensions.Add(this);
            }
        }

        /// <summary>
        /// The validate.
        /// </summary>
        /// <param name="serviceDescription">
        /// The service description.
        /// </param>
        /// <param name="serviceHostBase">
        /// The service host base.
        /// </param>
        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// The track.
        /// </summary>
        /// <param name="record">
        /// The record.
        /// </param>
        /// <param name="timeout">
        /// The timeout.
        /// </param>
        protected override void Track(TrackingRecord record, TimeSpan timeout)
        {
            record.Trace();
        }

        #endregion
    }
}