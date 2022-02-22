// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Movement/Input Master.inputactions'

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Input;


namespace xbox
{
    [Serializable]
    public class GamePad : InputActionAssetReference
    {
        public GamePad()
        {
        }
        public GamePad(InputActionAsset asset)
            : base(asset)
        {
        }
        private bool m_Initialized;
        private void Initialize()
        {
            // Player
            m_Player = asset.GetActionMap("Player");
            m_Player_Attack = m_Player.GetAction("Attack");
            if (m_PlayerAttackActionStarted != null)
                m_Player_Attack.started += m_PlayerAttackActionStarted.Invoke;
            if (m_PlayerAttackActionPerformed != null)
                m_Player_Attack.performed += m_PlayerAttackActionPerformed.Invoke;
            if (m_PlayerAttackActionCancelled != null)
                m_Player_Attack.cancelled += m_PlayerAttackActionCancelled.Invoke;
            m_Player_Move = m_Player.GetAction("Move");
            if (m_PlayerMoveActionStarted != null)
                m_Player_Move.started += m_PlayerMoveActionStarted.Invoke;
            if (m_PlayerMoveActionPerformed != null)
                m_Player_Move.performed += m_PlayerMoveActionPerformed.Invoke;
            if (m_PlayerMoveActionCancelled != null)
                m_Player_Move.cancelled += m_PlayerMoveActionCancelled.Invoke;
            m_Initialized = true;
        }
        private void Uninitialize()
        {
            if (m_PlayerActionsCallbackInterface != null)
            {
                Player.SetCallbacks(null);
            }
            m_Player = null;
            m_Player_Attack = null;
            if (m_PlayerAttackActionStarted != null)
                m_Player_Attack.started -= m_PlayerAttackActionStarted.Invoke;
            if (m_PlayerAttackActionPerformed != null)
                m_Player_Attack.performed -= m_PlayerAttackActionPerformed.Invoke;
            if (m_PlayerAttackActionCancelled != null)
                m_Player_Attack.cancelled -= m_PlayerAttackActionCancelled.Invoke;
            m_Player_Move = null;
            if (m_PlayerMoveActionStarted != null)
                m_Player_Move.started -= m_PlayerMoveActionStarted.Invoke;
            if (m_PlayerMoveActionPerformed != null)
                m_Player_Move.performed -= m_PlayerMoveActionPerformed.Invoke;
            if (m_PlayerMoveActionCancelled != null)
                m_Player_Move.cancelled -= m_PlayerMoveActionCancelled.Invoke;
            m_Initialized = false;
        }
        public void SetAsset(InputActionAsset newAsset)
        {
            if (newAsset == asset) return;
            var PlayerCallbacks = m_PlayerActionsCallbackInterface;
            if (m_Initialized) Uninitialize();
            asset = newAsset;
            Player.SetCallbacks(PlayerCallbacks);
        }
        public override void MakePrivateCopyOfActions()
        {
            SetAsset(ScriptableObject.Instantiate(asset));
        }
        // Player
        private InputActionMap m_Player;
        private IPlayerActions m_PlayerActionsCallbackInterface;
        private InputAction m_Player_Attack;
        [SerializeField] private ActionEvent m_PlayerAttackActionStarted;
        [SerializeField] private ActionEvent m_PlayerAttackActionPerformed;
        [SerializeField] private ActionEvent m_PlayerAttackActionCancelled;
        private InputAction m_Player_Move;
        [SerializeField] private ActionEvent m_PlayerMoveActionStarted;
        [SerializeField] private ActionEvent m_PlayerMoveActionPerformed;
        [SerializeField] private ActionEvent m_PlayerMoveActionCancelled;
        public struct PlayerActions
        {
            private GamePad m_Wrapper;
            public PlayerActions(GamePad wrapper) { m_Wrapper = wrapper; }
            public InputAction @Attack { get { return m_Wrapper.m_Player_Attack; } }
            public ActionEvent AttackStarted { get { return m_Wrapper.m_PlayerAttackActionStarted; } }
            public ActionEvent AttackPerformed { get { return m_Wrapper.m_PlayerAttackActionPerformed; } }
            public ActionEvent AttackCancelled { get { return m_Wrapper.m_PlayerAttackActionCancelled; } }
            public InputAction @Move { get { return m_Wrapper.m_Player_Move; } }
            public ActionEvent MoveStarted { get { return m_Wrapper.m_PlayerMoveActionStarted; } }
            public ActionEvent MovePerformed { get { return m_Wrapper.m_PlayerMoveActionPerformed; } }
            public ActionEvent MoveCancelled { get { return m_Wrapper.m_PlayerMoveActionCancelled; } }
            public InputActionMap Get() { return m_Wrapper.m_Player; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled { get { return Get().enabled; } }
            public InputActionMap Clone() { return Get().Clone(); }
            public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
            public void SetCallbacks(IPlayerActions instance)
            {
                if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
                {
                    Attack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                    Attack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                    Attack.cancelled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                    Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                    Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                    Move.cancelled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                }
                m_Wrapper.m_PlayerActionsCallbackInterface = instance;
                if (instance != null)
                {
                    Attack.started += instance.OnAttack;
                    Attack.performed += instance.OnAttack;
                    Attack.cancelled += instance.OnAttack;
                    Move.started += instance.OnMove;
                    Move.performed += instance.OnMove;
                    Move.cancelled += instance.OnMove;
                }
            }
        }
        public PlayerActions @Player
        {
            get
            {
                if (!m_Initialized) Initialize();
                return new PlayerActions(this);
            }
        }
        private int m_XboxControlSchemeSchemeIndex = -1;
        public InputControlScheme XboxControlSchemeScheme
        {
            get

            {
                if (m_XboxControlSchemeSchemeIndex == -1) m_XboxControlSchemeSchemeIndex = asset.GetControlSchemeIndex("Xbox Control Scheme");
                return asset.controlSchemes[m_XboxControlSchemeSchemeIndex];
            }
        }
        [Serializable]
        public class ActionEvent : UnityEvent<InputAction.CallbackContext>
        {
        }
    }
    public interface IPlayerActions
    {
        void OnAttack(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
    }
}
