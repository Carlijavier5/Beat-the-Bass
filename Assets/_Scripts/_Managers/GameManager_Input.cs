using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameManager {
    public class InputManager {
        public PlayerInput InputMap { get; private set; }
        public Vector2 MoveVector => InputMap.MovementMap.Move.ReadValue<Vector2>();
        public event System.Action OnInteraction;
        public event System.Action OnBeat;

        public InputManager(PlayerInput inputMap) {
            InputMap = inputMap;
            InputMap.MenuInput.Enable();
            ToggleMovement(true);
            ToggleInteraction(true);
            inputMap.InteractionMap.Interact.performed += (callback) => OnInteraction?.Invoke();
            inputMap.InteractionMap.Beat.performed += (callback) => OnBeat?.Invoke();
        }

        public void ToggleMovement(bool toggle) {
            if (toggle) InputMap.MovementMap.Enable();
            else InputMap.MovementMap.Disable();
        }

        public void ToggleInteraction(bool toggle) {
            if (toggle) InputMap.InteractionMap.Enable();
            else InputMap.InteractionMap.Disable();
        }
    } public InputManager Input { get; private set; }
}