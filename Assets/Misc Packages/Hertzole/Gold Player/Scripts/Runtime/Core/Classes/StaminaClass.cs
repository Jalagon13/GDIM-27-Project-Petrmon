﻿using UnityEngine;
using UnityEngine.Serialization;

namespace Hertzole.GoldPlayer
{
    /// <summary>
    /// Used to calculate stamina/limited running.
    /// </summary>
    [System.Serializable]
    public class StaminaClass : PlayerModule
    {
        [SerializeField]
        [Tooltip("Determines if stamina should be enabled.")]
        [FormerlySerializedAs("m_EnableStamina")]
        private bool enableStamina = false;
        [SerializeField]
        [Tooltip("Sets when the stamina should be drained.")]
        [FormerlySerializedAs("m_DrainStaminaWhen")]
        private RunAction drainStaminaWhen = RunAction.IsRunningAndPressingRun;
        [SerializeField]
        [Tooltip("The maximum amount of stamina.")]
        [FormerlySerializedAs("m_MaxStamina")]
        private float maxStamina = 10f;
        [SerializeField]
        [Tooltip("How much stamina will be drained per second.")]
        [FormerlySerializedAs("m_DrainRate")]
        private float drainRate = 1f;
        [SerializeField]
        [Tooltip("The threshold where it counts if the player is standing still.")]
        private float stillThreshold = 0.2f;
        [SerializeField]
        [Tooltip("How much stamina will regenerate per second when standing still.")]
        [FormerlySerializedAs("m_RegenRate")]
        [FormerlySerializedAs("regenRate")]
        private float regenRateStill = 0.8f;
        [SerializeField]
        [Tooltip("How much stamina will regenerate per second when moving.")]
        private float regenRateMoving = 0.5f;
        [SerializeField]
        [Tooltip("How long it will wait before starting to regenerate stamina.")]
        [FormerlySerializedAs("m_RegenWait")]
        private float regenWait = 1f;

        // The amount current stamina.
        private float currentStamina;
        // The current regen wait time.
        private float currentRegenWait;

        /// <summary> Determines if stamina should be enabled. </summary>
        public bool EnableStamina { get { return enableStamina; } set { enableStamina = value; } }
        /// <summary> Sets when the stamina should be drained. </summary>
        public RunAction DrainStaminaWhen { get { return drainStaminaWhen; } set { drainStaminaWhen = value; } }
        /// <summary> The maximum amount of stamina. </summary>
        public float MaxStamina { get { return maxStamina; } set { maxStamina = value; } }
        /// <summary> The threshold where it counts if the player is standing still. </summary>
        public float StillThreshold { get { return stillThreshold; } set { stillThreshold = value; } }
        /// <summary> How much stamina will be drained per second. </summary>
        public float DrainRate { get { return drainRate; } set { drainRate = value; } }
        /// <summary> "How much stamina will regenerate per second when standing still. </summary>
        public float RegenRateStill { get { return regenRateStill; } set { regenRateStill = value; } }
        /// <summary> "How much stamina will regenerate per second when moving. </summary>
        public float RegenRateMoving { get { return regenRateMoving; } set { regenRateMoving = value; } }
        /// <summary>How long it will wait before starting to regenerate stamina. </summary>
        public float RegenWait { get { return regenWait; } set { regenWait = value; } }

        /// <summary> The current amount of stamina. </summary>
        public float CurrentStamina { get { return currentStamina; } set { currentStamina = value; } }
        /// <summary> The current regen wait time. </summary>
        public float CurrentRegenWait { get { return currentRegenWait; } set { currentRegenWait = value; } }

        #region Obsolete
#if UNITY_EDITOR
        [System.Obsolete("Use RegenRateStill or RegenRateMoving instead. This will be removed on build.", true)]
        public float RegenRate { get { return regenRateStill; } set { regenRateStill = value; } }
#endif
        #endregion

        protected override void OnInitialize()
        {
            // Set the current stamina to the max stamina. This way we always start with a full stamina bar.
            currentStamina = maxStamina;
            // Set the current regen wait to the regen wait. This way we will always start at a full regen time.
            currentRegenWait = regenWait;
        }

        public override void OnUpdate(float deltaTime)
        {
            // Do the stamina logic.
            HandleStamina(deltaTime);
        }

        /// <summary>
        /// Handles all the stamina logic.
        /// </summary>
        protected virtual void HandleStamina(float deltaTime)
        {
            // There's no point in doing stamina logic if we can't run. Stop here if running is disabled.
            if (!PlayerController.Movement.CanRun)
            {
                return;
            }

            // Stop here if stamina is disabled.
            if (!enableStamina)
            {
                return;
            }

            // If we should drain stamina when move speed is above walk speed, drain stamina when 'isRunning' is true.
            // Else drain it when 'isRunning' is true and the run button is being held down.
            if (drainStaminaWhen == RunAction.IsRunning)
            {
                // If 'isRunning' is true, drain the stamina.
                // Else if the run button is not being held down, regen the stamina.
                if (PlayerController.Movement.IsRunning)
                {
                    DrainStamina(deltaTime);
                }
                else if (!GetButton(PlayerController.Movement.RunInput))
                {
                    RegenStamina(deltaTime);
                }
            }
            else if (drainStaminaWhen == RunAction.IsRunningAndPressingRun)
            {
                // If 'isRunning' is true and the run button is being held down, drain the stamina.
                // Else if the run button is not being held down, regen the stamina.
                if (PlayerController.Movement.IsRunning && GetButton(PlayerController.Movement.RunInput))
                {
                    DrainStamina(deltaTime);
                }
                else if (!GetButton(PlayerController.Movement.RunInput))
                {
                    RegenStamina(deltaTime);
                }
            }

            // Clamps the values so they stay within range.
            ClampValues();
        }

        /// <summary>
        /// Drains the stamina.
        /// </summary>
        protected virtual void DrainStamina(float deltaTime)
        {
            // Only drain the stamina is the current stamina is above 0.
            if (currentStamina > 0)
            {
                currentStamina -= drainRate * deltaTime;
            }

            // Set the current regen wait to 0.
            currentRegenWait = 0;
        }

        /// <summary>
        /// Does the stamina regeneration logic.
        /// </summary>
        protected virtual void RegenStamina(float deltaTime)
        {
            // If the current regen wait is less than the regen wait, increase the current regen wait.
            if (currentRegenWait < regenWait)
            {
                currentRegenWait += deltaTime;
            }

            // If the current regen wait is the same as regen wait and current stamina is less than max stamina,
            // increase the current stamina with regen rate.
            if (currentRegenWait >= regenWait && currentStamina < maxStamina)
            {
                // Check if the controller velocity is below the still threshold. If it is, regen as if the player is standing still.
                // Else regen as if the player is moving.
                currentStamina += (CharacterController.velocity.magnitude <= stillThreshold ? regenRateStill : regenRateMoving) * deltaTime;
            }
        }

        /// <summary>
        /// Clamps current stamina and current regen wait.
        /// </summary>
        protected virtual void ClampValues()
        {
            // Make sure current stamina doesn't go below 0.
            if (currentStamina < 0)
            {
                currentStamina = 0;
            }

            // Make sure current stamina doesn't go above max stamina.
            if (currentStamina > maxStamina)
            {
                currentStamina = maxStamina;
            }

            // Make sure current regen wait doesn't go above regen wait.
            if (currentRegenWait > regenWait)
            {
                currentRegenWait = regenWait;
            }

            // Make sure current regen wait doesn't go below 0.
            if (currentRegenWait < 0)
            {
                currentRegenWait = 0;
            }
        }
    }
}
