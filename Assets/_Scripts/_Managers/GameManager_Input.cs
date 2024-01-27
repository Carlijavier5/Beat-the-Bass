using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager {
    public class InputManager {
        private readonly PlayerInput inputMap;
        public event System.Action<Vector2> OnMove;
        public event System.Action OnInteraction;
        public event System.Action OnBeat;

        public InputManager(PlayerInput inputMap) {
            this.inputMap = inputMap;
            inputMap.MovementMap.Move.performed += (dir) => OnMove?.Invoke(dir.ReadValue<Vector2>());
            inputMap.InteractionMap.Interact.performed += (callback) => OnInteraction?.Invoke();
            inputMap.InteractionMap.Beat.performed += (callback) => OnBeat?.Invoke();
        }

        public void ToggleMovement(bool toggle) {
            if (toggle) inputMap.MovementMap.Enable();
            else inputMap.MovementMap.Disable();
        }

        public void ToggleInteraction(bool toggle) {
            if (toggle) inputMap.InteractionMap.Enable();
            else inputMap.InteractionMap.Disable();
        }
    } public InputManager Input { get; private set; }
}