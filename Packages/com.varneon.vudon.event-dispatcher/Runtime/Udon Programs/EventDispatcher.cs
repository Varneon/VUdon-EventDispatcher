using JetBrains.Annotations;
using UdonSharp;
using UnityEngine;
using Varneon.VUdon.ArrayExtensions;
using Varneon.VUdon.Common.VRCEnums;
using VRC.SDKBase;
using VRC.Udon.Common.Interfaces;

namespace Varneon.VUdon.EventDispatcher
{
    /// <summary>
    /// Dispatcher for following update events: Fixed Update, Update, LateUpdate, PostLateUpdate
    /// </summary>
    /// <remarks>
    /// Default execution order of the dispatcher is 0
    /// </remarks>
    [ExcludeFromPreset]
    [DisallowMultipleComponent]
    [HelpURL("https://github.com/Varneon/VUdon-EventDispatcher")]
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class EventDispatcher : UdonSharpBehaviour
    {
        #region Private Variables
        /// <summary>
        /// Handler behaviours for all of the events
        /// </summary>
        private IUdonEventReceiver[]
            fixedUpdateHandlers = new IUdonEventReceiver[0],
            updateHandlers = new IUdonEventReceiver[0],
            lateUpdateHandlers = new IUdonEventReceiver[0],
            postLateUpdateHandlers = new IUdonEventReceiver[0];

        /// <summary>
        /// Quick state for checking if certain event has any handlers to bypass array lenght check
        /// </summary>
        private bool
            hasFixedUpdateHandlers,
            hasUpdateHandlers,
            hasLateUpdateHandlers,
            hasPostLateUpdateHandlers;

        /// <summary>
        /// Quick handler count for bypassing array length check
        /// </summary>
        private int
            fixedUpdateHandlerCount,
            updateHandlerCount,
            lateUpdateHandlerCount,
            postLateUpdateHandlerCount;
        #endregion

        #region Constants
        /// <summary>
        /// Handler method names for each event
        /// </summary>
        private const string
            FixedUpdateHandlerName = "_FixedUpdateHandler",
            UpdateHandlerName = "_UpdateHandler",
            LateUpdateHandlerName = "_LateUpdateHandler",
            PostLateUpdateHandlerName = "_PostLateUpdateHandler";
        #endregion

        #region Public API Methods
        /// <summary>
        /// Adds a handler
        /// </summary>
        /// <param name="eventType">Event type which the handler gets added for</param>
        /// <param name="handler">Handler behaviour with the handler method which will be called</param>
        [PublicAPI]
        public void AddHandler(VRCUpdateEventType eventType, IUdonEventReceiver handler)
        {
            switch (eventType)
            {
                case VRCUpdateEventType.FixedUpdate:
                    AddFixedUpdateHandler(handler);
                    break;
                case VRCUpdateEventType.Update:
                    AddUpdateHandler(handler);
                    break;
                case VRCUpdateEventType.LateUpdate:
                    AddLateUpdateHandler(handler);
                    break;
                case VRCUpdateEventType.PostLateUpdate:
                    AddPostLateUpdateHandler(handler);
                    break;
            }
        }

        /// <summary>
        /// Removes a handler
        /// </summary>
        /// <param name="eventType">Event type which the handler gets removed for</param>
        /// <param name="handler">Handler behaviour</param>
        [PublicAPI]
        public void RemoveHandler(VRCUpdateEventType eventType, IUdonEventReceiver handler)
        {
            switch (eventType)
            {
                case VRCUpdateEventType.FixedUpdate:
                    RemoveFixedUpdateHandler(handler);
                    break;
                case VRCUpdateEventType.Update:
                    RemoveUpdateHandler(handler);
                    break;
                case VRCUpdateEventType.LateUpdate:
                    RemoveLateUpdateHandler(handler);
                    break;
                case VRCUpdateEventType.PostLateUpdate:
                    RemovePostLateUpdateHandler(handler);
                    break;
            }
        }

        /// <summary>
        /// Sets event handler active state
        /// </summary>
        /// <param name="eventType">Event type</param>
        /// <param name="handler">Handler behaviour</param>
        /// <param name="active">Should the handler be active or not</param>
        [PublicAPI]
        public void SetHandlerActive(VRCUpdateEventType eventType, IUdonEventReceiver handler, bool active)
        {
            if (active)
            {
                AddHandler(eventType, handler);
            }
            else
            {
                RemoveHandler(eventType, handler);
            }
        }

        /// <summary>
        /// Add handler for FixedUpdate()
        /// </summary>
        /// <param name="handler">Handler behaviour with _FixedUpdateHandler() which will be called</param>
        [PublicAPI]
        public void AddFixedUpdateHandler(IUdonEventReceiver handler)
        {
            hasFixedUpdateHandlers = (fixedUpdateHandlerCount = (fixedUpdateHandlers = fixedUpdateHandlers.AddUnique(handler)).Length) > 0;
        }

        /// <summary>
        /// Remove handler for FixedUpdate()
        /// </summary>
        /// <param name="handler">Handler behaviour</param>
        [PublicAPI]
        public void RemoveFixedUpdateHandler(IUdonEventReceiver handler)
        {
            if (fixedUpdateHandlerCount > 0) { hasFixedUpdateHandlers = (fixedUpdateHandlerCount = (fixedUpdateHandlers = fixedUpdateHandlers.Remove(handler)).Length) > 0; }
        }

        /// <summary>
        /// Add handler for Update()
        /// </summary>
        /// <param name="handler">Handler behaviour with _UpdateHandler() which will be called</param>
        [PublicAPI]
        public void AddUpdateHandler(IUdonEventReceiver handler)
        {
            hasUpdateHandlers = (updateHandlerCount = (updateHandlers = updateHandlers.AddUnique(handler)).Length) > 0;
        }

        /// <summary>
        /// Remove handler for Update()
        /// </summary>
        /// <param name="handler">Handler behaviour</param>
        [PublicAPI]
        public void RemoveUpdateHandler(IUdonEventReceiver handler)
        {
            if (updateHandlerCount > 0) { hasUpdateHandlers = (updateHandlerCount = (updateHandlers = updateHandlers.Remove(handler)).Length) > 0; }
        }

        /// <summary>
        /// Add handler for LateUpdate()
        /// </summary>
        /// <param name="handler">Handler behaviour with _LateUpdateHandler() which will be called</param>
        [PublicAPI]
        public void AddLateUpdateHandler(IUdonEventReceiver handler)
        {
            hasLateUpdateHandlers = (lateUpdateHandlerCount = (lateUpdateHandlers = lateUpdateHandlers.AddUnique(handler)).Length) > 0;
        }

        /// <summary>
        /// Remove handler for LateUpdate()
        /// </summary>
        /// <param name="handler">Handler behaviour</param>
        [PublicAPI]
        public void RemoveLateUpdateHandler(IUdonEventReceiver handler)
        {
            if (lateUpdateHandlerCount > 0) { hasLateUpdateHandlers = (lateUpdateHandlerCount = (lateUpdateHandlers = lateUpdateHandlers.Remove(handler)).Length) > 0; }
        }

        /// <summary>
        /// Add handler for PostLateUpdate()
        /// </summary>
        /// <param name="handler">Handler behaviour with _PostLateUpdateHandler() which will be called</param>
        [PublicAPI]
        public void AddPostLateUpdateHandler(IUdonEventReceiver handler)
        {
            hasPostLateUpdateHandlers = (postLateUpdateHandlerCount = (postLateUpdateHandlers = postLateUpdateHandlers.AddUnique(handler)).Length) > 0;
        }

        /// <summary>
        /// Remove handler for PostLateUpdate()
        /// </summary>
        /// <param name="handler">Handler behaviour</param>
        [PublicAPI]
        public void RemovePostLateUpdateHandler(IUdonEventReceiver handler)
        {
            if (postLateUpdateHandlerCount > 0) { hasPostLateUpdateHandlers = (postLateUpdateHandlerCount = (postLateUpdateHandlers = postLateUpdateHandlers.Remove(handler)).Length) > 0; }
        }
        #endregion

        #region Update Methods
        private void FixedUpdate()
        {
            if (hasFixedUpdateHandlers)
            {
                for (int i = 0; i < fixedUpdateHandlerCount; i++)
                {
                    fixedUpdateHandlers[i].SendCustomEvent(FixedUpdateHandlerName);
                }
            }
        }

        private void Update()
        {
            if (hasUpdateHandlers)
            {
                for (int i = 0; i < updateHandlerCount; i++)
                {
                    updateHandlers[i].SendCustomEvent(UpdateHandlerName);
                }
            }
        }

        private void LateUpdate()
        {
            if (hasLateUpdateHandlers)
            {
                for (int i = 0; i < lateUpdateHandlerCount; i++)
                {
                    lateUpdateHandlers[i].SendCustomEvent(LateUpdateHandlerName);
                }
            }
        }

        public override void PostLateUpdate()
        {
            if (hasPostLateUpdateHandlers)
            {
                for (int i = 0; i < postLateUpdateHandlerCount; i++)
                {
                    postLateUpdateHandlers[i].SendCustomEvent(PostLateUpdateHandlerName);
                }
            }
        }
        #endregion

        #region Player Events
        public override void OnPlayerLeft(VRCPlayerApi player)
        {
            // If the player leaving is the local player, disable all handlers to prevent errors
            if (!Utilities.IsValid(player))
            {
                hasFixedUpdateHandlers = false;
                hasUpdateHandlers = false;
                hasLateUpdateHandlers = false;
                hasPostLateUpdateHandlers = false;
            }
        }
        #endregion
    }
}
