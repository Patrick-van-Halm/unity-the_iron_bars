// GENERATED AUTOMATICALLY FROM 'Assets/Inputs/PlayerInputs.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputs : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputs"",
    ""maps"": [
        {
            ""name"": ""CharacterControls"",
            ""id"": ""50fcb546-ab96-4e63-a0fa-3199fc13e6ce"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""7149c837-072d-4fd0-ad80-a0001f90cc15"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""217c6829-c4c1-4d1c-b501-0ae39c1e0f1e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Value"",
                    ""id"": ""4b61b840-23bc-4e6e-95ea-e4c0ffd54d64"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Value"",
                    ""id"": ""2fe0bdfa-c182-456c-8381-be5548f89130"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Value"",
                    ""id"": ""ced0bc23-4fbf-4ed2-b104-451edf5427f4"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Value"",
                    ""id"": ""cb1f0bd7-21d3-4cd8-a607-ca806e521d92"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Drop"",
                    ""type"": ""Value"",
                    ""id"": ""2ec43270-2e17-483c-8e85-9a7033ba4005"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Flashlight"",
                    ""type"": ""Value"",
                    ""id"": ""c4ebbcc6-8469-41de-b30f-7dc78509eb67"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Settings"",
                    ""type"": ""Value"",
                    ""id"": ""98949eda-2f82-4f86-afad-0a6fe1d6880a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""04a6dc7e-72d1-45ab-80d1-fd9e82922d7c"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f0defcac-864f-48e0-b97d-9a600c7934a7"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""a8340eeb-6920-400f-a58b-300e4271472d"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""d780e92b-f12e-4e2f-9eee-bdd7f02fbb07"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""c6f030c5-8b54-42ac-9c5f-1060fbdd819b"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""a2480794-634b-412d-916f-b3ee2d373b54"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4e9cdcbd-eb5b-43f3-a427-abb02f212366"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eddbfb97-11b9-4f85-9878-6e831e47ea8f"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dfff26f3-3564-4687-abc2-588e272630c9"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dcaabce4-5224-43ff-980f-96d1652caa5e"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8100a9eb-0856-42c1-a85f-37196db92b08"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""33a68319-e992-4c90-bae6-80fd72e98cc3"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""73eb8085-f21e-43b7-88af-d89277668c49"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ccfd811c-ef3d-459d-9c06-bf307023eb54"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d7c7c0b8-e43f-4c7c-9b5f-1da0de50c764"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d78bdbf0-29d1-4b9f-9e43-93865cd1def2"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Drop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c52ee38c-1380-4b8b-8708-078c022deb18"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Drop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1dee9378-e784-45a0-9a1f-ec3b478764bc"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Flashlight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1b5678ba-b20d-4332-819c-70eb865222d1"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Flashlight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8e7bc1f8-53bf-4349-86f9-d8f12a1d86f9"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Settings"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""74b8c932-9923-408b-a856-766f76203b2e"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Settings"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard and Mouse"",
            ""bindingGroup"": ""Keyboard and Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // CharacterControls
        m_CharacterControls = asset.FindActionMap("CharacterControls", throwIfNotFound: true);
        m_CharacterControls_Movement = m_CharacterControls.FindAction("Movement", throwIfNotFound: true);
        m_CharacterControls_Look = m_CharacterControls.FindAction("Look", throwIfNotFound: true);
        m_CharacterControls_Jump = m_CharacterControls.FindAction("Jump", throwIfNotFound: true);
        m_CharacterControls_Sprint = m_CharacterControls.FindAction("Sprint", throwIfNotFound: true);
        m_CharacterControls_Crouch = m_CharacterControls.FindAction("Crouch", throwIfNotFound: true);
        m_CharacterControls_Interact = m_CharacterControls.FindAction("Interact", throwIfNotFound: true);
        m_CharacterControls_Drop = m_CharacterControls.FindAction("Drop", throwIfNotFound: true);
        m_CharacterControls_Flashlight = m_CharacterControls.FindAction("Flashlight", throwIfNotFound: true);
        m_CharacterControls_Settings = m_CharacterControls.FindAction("Settings", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // CharacterControls
    private readonly InputActionMap m_CharacterControls;
    private ICharacterControlsActions m_CharacterControlsActionsCallbackInterface;
    private readonly InputAction m_CharacterControls_Movement;
    private readonly InputAction m_CharacterControls_Look;
    private readonly InputAction m_CharacterControls_Jump;
    private readonly InputAction m_CharacterControls_Sprint;
    private readonly InputAction m_CharacterControls_Crouch;
    private readonly InputAction m_CharacterControls_Interact;
    private readonly InputAction m_CharacterControls_Drop;
    private readonly InputAction m_CharacterControls_Flashlight;
    private readonly InputAction m_CharacterControls_Settings;
    public struct CharacterControlsActions
    {
        private @PlayerInputs m_Wrapper;
        public CharacterControlsActions(@PlayerInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_CharacterControls_Movement;
        public InputAction @Look => m_Wrapper.m_CharacterControls_Look;
        public InputAction @Jump => m_Wrapper.m_CharacterControls_Jump;
        public InputAction @Sprint => m_Wrapper.m_CharacterControls_Sprint;
        public InputAction @Crouch => m_Wrapper.m_CharacterControls_Crouch;
        public InputAction @Interact => m_Wrapper.m_CharacterControls_Interact;
        public InputAction @Drop => m_Wrapper.m_CharacterControls_Drop;
        public InputAction @Flashlight => m_Wrapper.m_CharacterControls_Flashlight;
        public InputAction @Settings => m_Wrapper.m_CharacterControls_Settings;
        public InputActionMap Get() { return m_Wrapper.m_CharacterControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CharacterControlsActions set) { return set.Get(); }
        public void SetCallbacks(ICharacterControlsActions instance)
        {
            if (m_Wrapper.m_CharacterControlsActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnMovement;
                @Look.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnLook;
                @Jump.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnJump;
                @Sprint.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnSprint;
                @Crouch.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnCrouch;
                @Crouch.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnCrouch;
                @Crouch.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnCrouch;
                @Interact.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnInteract;
                @Drop.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnDrop;
                @Drop.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnDrop;
                @Drop.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnDrop;
                @Flashlight.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnFlashlight;
                @Flashlight.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnFlashlight;
                @Flashlight.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnFlashlight;
                @Settings.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnSettings;
                @Settings.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnSettings;
                @Settings.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnSettings;
            }
            m_Wrapper.m_CharacterControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @Crouch.started += instance.OnCrouch;
                @Crouch.performed += instance.OnCrouch;
                @Crouch.canceled += instance.OnCrouch;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Drop.started += instance.OnDrop;
                @Drop.performed += instance.OnDrop;
                @Drop.canceled += instance.OnDrop;
                @Flashlight.started += instance.OnFlashlight;
                @Flashlight.performed += instance.OnFlashlight;
                @Flashlight.canceled += instance.OnFlashlight;
                @Settings.started += instance.OnSettings;
                @Settings.performed += instance.OnSettings;
                @Settings.canceled += instance.OnSettings;
            }
        }
    }
    public CharacterControlsActions @CharacterControls => new CharacterControlsActions(this);
    private int m_KeyboardandMouseSchemeIndex = -1;
    public InputControlScheme KeyboardandMouseScheme
    {
        get
        {
            if (m_KeyboardandMouseSchemeIndex == -1) m_KeyboardandMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard and Mouse");
            return asset.controlSchemes[m_KeyboardandMouseSchemeIndex];
        }
    }
    public interface ICharacterControlsActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnDrop(InputAction.CallbackContext context);
        void OnFlashlight(InputAction.CallbackContext context);
        void OnSettings(InputAction.CallbackContext context);
    }
}
