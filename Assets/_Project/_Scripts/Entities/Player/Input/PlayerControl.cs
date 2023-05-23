using StarterAssets;
using UnityEngine;

namespace ProjectPetrmon
{
    public class PlayerControl : MonoBehaviour
    {
        private PlayerInput _playerInput;
        private FirstPersonController _firstPersonController;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
            _firstPersonController = GetComponent<FirstPersonController>(); 
        }

        private void OnEnable()
        {
            DialogueManager.Instance.OnDialogueStart += RestrictMovement;
            DialogueManager.Instance.OnDialogueEnd += AllowMovement;
            BattleManager.Instance.OnBattleStart += RestrictMovement;
            BattleManager.Instance.OnBattleEnd += AllowMovement;
            PauseManager.OnPauseEnable += RestrictMovement;
            PauseManager.OnPauseDisable += AllowMovement;
        }

        private void OnDisable()
        {
            DialogueManager.Instance.OnDialogueStart -= RestrictMovement;
            DialogueManager.Instance.OnDialogueEnd -= AllowMovement;
            BattleManager.Instance.OnBattleStart -= RestrictMovement;
            BattleManager.Instance.OnBattleEnd -= AllowMovement;
            PauseManager.OnPauseEnable -= RestrictMovement;
            PauseManager.OnPauseDisable -= AllowMovement;
        }

        public void RestrictMovement()
        {
            _firstPersonController.enabled = false;
            _playerInput.cursorLocked = false;
            _playerInput.cursorInputForLook = false;
            Cursor.lockState = CursorLockMode.None;
        }

        public void AllowMovement()
        {
            _firstPersonController.enabled = true;
            _playerInput.cursorLocked = true;
            _playerInput.cursorInputForLook = true;
            Cursor.lockState = CursorLockMode.Locked;
        }

    }
}
