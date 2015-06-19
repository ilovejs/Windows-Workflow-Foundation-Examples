// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WorkflowServiceTraceElement.cs" company="Microsoft">
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
// <summary>
//   The workflow service trace element.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SecurityDoor.Web
{
    using System;
    using System.ServiceModel.Configuration;

    /// <summary>
    /// The workflow service trace element.
    /// </summary>
    public class WorkflowServiceTraceElement : BehaviorExtensionElement
    {
        #region Properties

        /// <summary>
        /// Gets BehaviorType.
        /// </summary>
        public override Type BehaviorType
        {
            get
            {
                return typeof(WorkflowServiceTraceBehavior);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// The create behavior.
        /// </summary>
        /// <returns>
        /// The behavior
        /// </returns>
        protected override object CreateBehavior()
        {
            return new WorkflowServiceTraceBehavior();
        }

        #endregion
    }
}