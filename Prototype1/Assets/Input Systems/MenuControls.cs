//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.3
//     from Assets/Input Systems/MenuControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @MenuControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @MenuControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MenuControls"",
    ""maps"": [
        {
            ""name"": ""Main"",
            ""id"": ""a38b116b-a4d9-4fd6-b5a7-c97a4ad4678b"",
            ""actions"": [
                {
                    ""name"": ""Menu"",
                    ""type"": ""Button"",
                    ""id"": ""7c0ed259-3a3e-47d0-b38e-3fd666603451"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Restart"",
                    ""type"": ""Button"",
                    ""id"": ""bf321da9-869e-46b4-91f6-dcf073883897"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ToggleLasso"",
                    ""type"": ""Button"",
                    ""id"": ""dafbaa4f-dc93-4805-9b5d-9a231f15b40b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""NextMission"",
                    ""type"": ""Button"",
                    ""id"": ""39bbd840-743a-4d49-9124-6fa22d5f39a0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PreviousMission"",
                    ""type"": ""Button"",
                    ""id"": ""f20ab1df-5358-47db-b3b4-6e8caa76124f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Console"",
                    ""type"": ""Button"",
                    ""id"": ""ad41e45c-aa44-45d7-b14b-ce95cec8a8cc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Journal"",
                    ""type"": ""Button"",
                    ""id"": ""8e9520bc-d4f4-4acd-88c1-fd8454c6cbc3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1add38c9-e26e-4259-a15b-d1373a913a93"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""74196267-f22e-47fb-9f83-6f9adafb4e95"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0f08ac83-e77c-4bb0-a0b9-765c2b2a2c29"",
                    ""path"": ""<Keyboard>/equals"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleLasso"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7255c41e-2bec-4174-aa29-181a0d68f83b"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleLasso"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""72b94747-a395-4dc2-a1e0-f3179e7a36f8"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NextMission"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0ee41c5d-bcf4-4ce7-89b9-efaf9595c781"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""NextMission"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a6c24137-6860-401e-ae81-368df3e35ef2"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PreviousMission"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d5fe5c73-d754-46e1-8687-ef06aa2a56c6"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PreviousMission"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9882855a-49fa-4f0b-9194-f5c23e7d6500"",
                    ""path"": ""<Keyboard>/backquote"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Console"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""048facfa-6da9-4826-a7a5-dae82cacf7bd"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Journal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Main
        m_Main = asset.FindActionMap("Main", throwIfNotFound: true);
        m_Main_Menu = m_Main.FindAction("Menu", throwIfNotFound: true);
        m_Main_Restart = m_Main.FindAction("Restart", throwIfNotFound: true);
        m_Main_ToggleLasso = m_Main.FindAction("ToggleLasso", throwIfNotFound: true);
        m_Main_NextMission = m_Main.FindAction("NextMission", throwIfNotFound: true);
        m_Main_PreviousMission = m_Main.FindAction("PreviousMission", throwIfNotFound: true);
        m_Main_Console = m_Main.FindAction("Console", throwIfNotFound: true);
        m_Main_Journal = m_Main.FindAction("Journal", throwIfNotFound: true);
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

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Main
    private readonly InputActionMap m_Main;
    private List<IMainActions> m_MainActionsCallbackInterfaces = new List<IMainActions>();
    private readonly InputAction m_Main_Menu;
    private readonly InputAction m_Main_Restart;
    private readonly InputAction m_Main_ToggleLasso;
    private readonly InputAction m_Main_NextMission;
    private readonly InputAction m_Main_PreviousMission;
    private readonly InputAction m_Main_Console;
    private readonly InputAction m_Main_Journal;
    public struct MainActions
    {
        private @MenuControls m_Wrapper;
        public MainActions(@MenuControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Menu => m_Wrapper.m_Main_Menu;
        public InputAction @Restart => m_Wrapper.m_Main_Restart;
        public InputAction @ToggleLasso => m_Wrapper.m_Main_ToggleLasso;
        public InputAction @NextMission => m_Wrapper.m_Main_NextMission;
        public InputAction @PreviousMission => m_Wrapper.m_Main_PreviousMission;
        public InputAction @Console => m_Wrapper.m_Main_Console;
        public InputAction @Journal => m_Wrapper.m_Main_Journal;
        public InputActionMap Get() { return m_Wrapper.m_Main; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MainActions set) { return set.Get(); }
        public void AddCallbacks(IMainActions instance)
        {
            if (instance == null || m_Wrapper.m_MainActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_MainActionsCallbackInterfaces.Add(instance);
            @Menu.started += instance.OnMenu;
            @Menu.performed += instance.OnMenu;
            @Menu.canceled += instance.OnMenu;
            @Restart.started += instance.OnRestart;
            @Restart.performed += instance.OnRestart;
            @Restart.canceled += instance.OnRestart;
            @ToggleLasso.started += instance.OnToggleLasso;
            @ToggleLasso.performed += instance.OnToggleLasso;
            @ToggleLasso.canceled += instance.OnToggleLasso;
            @NextMission.started += instance.OnNextMission;
            @NextMission.performed += instance.OnNextMission;
            @NextMission.canceled += instance.OnNextMission;
            @PreviousMission.started += instance.OnPreviousMission;
            @PreviousMission.performed += instance.OnPreviousMission;
            @PreviousMission.canceled += instance.OnPreviousMission;
            @Console.started += instance.OnConsole;
            @Console.performed += instance.OnConsole;
            @Console.canceled += instance.OnConsole;
            @Journal.started += instance.OnJournal;
            @Journal.performed += instance.OnJournal;
            @Journal.canceled += instance.OnJournal;
        }

        private void UnregisterCallbacks(IMainActions instance)
        {
            @Menu.started -= instance.OnMenu;
            @Menu.performed -= instance.OnMenu;
            @Menu.canceled -= instance.OnMenu;
            @Restart.started -= instance.OnRestart;
            @Restart.performed -= instance.OnRestart;
            @Restart.canceled -= instance.OnRestart;
            @ToggleLasso.started -= instance.OnToggleLasso;
            @ToggleLasso.performed -= instance.OnToggleLasso;
            @ToggleLasso.canceled -= instance.OnToggleLasso;
            @NextMission.started -= instance.OnNextMission;
            @NextMission.performed -= instance.OnNextMission;
            @NextMission.canceled -= instance.OnNextMission;
            @PreviousMission.started -= instance.OnPreviousMission;
            @PreviousMission.performed -= instance.OnPreviousMission;
            @PreviousMission.canceled -= instance.OnPreviousMission;
            @Console.started -= instance.OnConsole;
            @Console.performed -= instance.OnConsole;
            @Console.canceled -= instance.OnConsole;
            @Journal.started -= instance.OnJournal;
            @Journal.performed -= instance.OnJournal;
            @Journal.canceled -= instance.OnJournal;
        }

        public void RemoveCallbacks(IMainActions instance)
        {
            if (m_Wrapper.m_MainActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IMainActions instance)
        {
            foreach (var item in m_Wrapper.m_MainActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_MainActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public MainActions @Main => new MainActions(this);
    public interface IMainActions
    {
        void OnMenu(InputAction.CallbackContext context);
        void OnRestart(InputAction.CallbackContext context);
        void OnToggleLasso(InputAction.CallbackContext context);
        void OnNextMission(InputAction.CallbackContext context);
        void OnPreviousMission(InputAction.CallbackContext context);
        void OnConsole(InputAction.CallbackContext context);
        void OnJournal(InputAction.CallbackContext context);
    }
}
