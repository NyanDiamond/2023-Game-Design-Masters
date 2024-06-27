//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.3
//     from Assets/Input Systems/MainControls.inputactions
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

public partial class @MainControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @MainControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MainControls"",
    ""maps"": [
        {
            ""name"": ""Main"",
            ""id"": ""0166b2d9-1e62-4453-be64-7870dc218b0c"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""675bba32-b576-490e-b5fd-a54f3ecb5dd1"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""f9d3863c-3f6c-4ec8-93e7-497639b1dc2c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Primary"",
                    ""type"": ""Button"",
                    ""id"": ""cac010b8-01cd-4776-a555-d5fe3c63a937"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Secondary"",
                    ""type"": ""Button"",
                    ""id"": ""f4e164b9-235d-46b6-b628-01aedf0eab49"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MouseX"",
                    ""type"": ""PassThrough"",
                    ""id"": ""479a1733-4b14-44e8-9312-fd992b2c1280"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MouseY"",
                    ""type"": ""PassThrough"",
                    ""id"": ""fe23a565-49a4-4893-9107-77f5971fa954"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Release"",
                    ""type"": ""Button"",
                    ""id"": ""b55c0dc9-35ff-4ae5-a6af-2348351bc94b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""4711bac7-eb98-49c0-956e-960b3412f8f5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Value"",
                    ""id"": ""8c2e981b-46ce-430e-8e2b-0f79670647c8"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""4e71ffa4-fd4b-4e2c-a40a-e6d93f40f059"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""K&M"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""18a55a44-d8b3-40a3-a267-b053336025c8"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""K&M"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8345d9e1-5854-4dfd-9737-28be5d056c87"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""68a3017b-5e13-48e5-8b18-4471d0907537"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f5b57deb-1a0a-4bae-84ac-6f0bb57af8ea"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""K&M"",
                    ""action"": ""Primary"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4babcdfd-633c-44c7-a311-2535a658e751"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Primary"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6707595e-9b24-4408-893a-64c993957feb"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""K&M"",
                    ""action"": ""Secondary"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""697093c4-fa6e-4ad6-8797-8652a9e0650f"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Secondary"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""627d5525-c7dd-45d4-a7cc-d0991979a141"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""dbbe8111-470f-4902-921b-db1f5e813cff"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""K&M"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""2812b956-b18c-49d4-918f-7f876c73d11a"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""K&M"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""99a61956-5f46-4e9e-829f-01224770152f"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""K&M"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""3e45fd7d-d2d6-4e8e-b4b3-1c65f7272fd5"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""K&M"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""b9522812-7001-446c-9d54-d0626cea0210"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone(min=0.2)"",
                    ""groups"": ""Controller"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""022bffd0-bf74-4bea-a290-884d246161d0"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""K&M"",
                    ""action"": ""MouseX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ffc8b4a3-78ab-41c3-acec-757dccf45de3"",
                    ""path"": ""<Mouse>/delta/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""K&M"",
                    ""action"": ""MouseY"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""774832c5-d719-452a-bd2e-acad2142cf34"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""K&M"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8b6b954a-57b4-4d62-ba6d-f86fb228aa63"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c46c1822-0f26-4e23-b7e4-2fc34b2e9b24"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone(min=0.2)"",
                    ""groups"": ""Controller"",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5c4e0290-87c0-49e9-9e5b-1ba3b71b6d4b"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""K&M"",
                    ""action"": ""Release"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""612913d8-6473-4f3c-b356-36bdd2f4257d"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Release"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""K&M"",
            ""bindingGroup"": ""K&M"",
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
        },
        {
            ""name"": ""Controller"",
            ""bindingGroup"": ""Controller"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Main
        m_Main = asset.FindActionMap("Main", throwIfNotFound: true);
        m_Main_Move = m_Main.FindAction("Move", throwIfNotFound: true);
        m_Main_Interact = m_Main.FindAction("Interact", throwIfNotFound: true);
        m_Main_Primary = m_Main.FindAction("Primary", throwIfNotFound: true);
        m_Main_Secondary = m_Main.FindAction("Secondary", throwIfNotFound: true);
        m_Main_MouseX = m_Main.FindAction("MouseX", throwIfNotFound: true);
        m_Main_MouseY = m_Main.FindAction("MouseY", throwIfNotFound: true);
        m_Main_Release = m_Main.FindAction("Release", throwIfNotFound: true);
        m_Main_Dash = m_Main.FindAction("Dash", throwIfNotFound: true);
        m_Main_Aim = m_Main.FindAction("Aim", throwIfNotFound: true);
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
    private readonly InputAction m_Main_Move;
    private readonly InputAction m_Main_Interact;
    private readonly InputAction m_Main_Primary;
    private readonly InputAction m_Main_Secondary;
    private readonly InputAction m_Main_MouseX;
    private readonly InputAction m_Main_MouseY;
    private readonly InputAction m_Main_Release;
    private readonly InputAction m_Main_Dash;
    private readonly InputAction m_Main_Aim;
    public struct MainActions
    {
        private @MainControls m_Wrapper;
        public MainActions(@MainControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Main_Move;
        public InputAction @Interact => m_Wrapper.m_Main_Interact;
        public InputAction @Primary => m_Wrapper.m_Main_Primary;
        public InputAction @Secondary => m_Wrapper.m_Main_Secondary;
        public InputAction @MouseX => m_Wrapper.m_Main_MouseX;
        public InputAction @MouseY => m_Wrapper.m_Main_MouseY;
        public InputAction @Release => m_Wrapper.m_Main_Release;
        public InputAction @Dash => m_Wrapper.m_Main_Dash;
        public InputAction @Aim => m_Wrapper.m_Main_Aim;
        public InputActionMap Get() { return m_Wrapper.m_Main; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MainActions set) { return set.Get(); }
        public void AddCallbacks(IMainActions instance)
        {
            if (instance == null || m_Wrapper.m_MainActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_MainActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Interact.started += instance.OnInteract;
            @Interact.performed += instance.OnInteract;
            @Interact.canceled += instance.OnInteract;
            @Primary.started += instance.OnPrimary;
            @Primary.performed += instance.OnPrimary;
            @Primary.canceled += instance.OnPrimary;
            @Secondary.started += instance.OnSecondary;
            @Secondary.performed += instance.OnSecondary;
            @Secondary.canceled += instance.OnSecondary;
            @MouseX.started += instance.OnMouseX;
            @MouseX.performed += instance.OnMouseX;
            @MouseX.canceled += instance.OnMouseX;
            @MouseY.started += instance.OnMouseY;
            @MouseY.performed += instance.OnMouseY;
            @MouseY.canceled += instance.OnMouseY;
            @Release.started += instance.OnRelease;
            @Release.performed += instance.OnRelease;
            @Release.canceled += instance.OnRelease;
            @Dash.started += instance.OnDash;
            @Dash.performed += instance.OnDash;
            @Dash.canceled += instance.OnDash;
            @Aim.started += instance.OnAim;
            @Aim.performed += instance.OnAim;
            @Aim.canceled += instance.OnAim;
        }

        private void UnregisterCallbacks(IMainActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Interact.started -= instance.OnInteract;
            @Interact.performed -= instance.OnInteract;
            @Interact.canceled -= instance.OnInteract;
            @Primary.started -= instance.OnPrimary;
            @Primary.performed -= instance.OnPrimary;
            @Primary.canceled -= instance.OnPrimary;
            @Secondary.started -= instance.OnSecondary;
            @Secondary.performed -= instance.OnSecondary;
            @Secondary.canceled -= instance.OnSecondary;
            @MouseX.started -= instance.OnMouseX;
            @MouseX.performed -= instance.OnMouseX;
            @MouseX.canceled -= instance.OnMouseX;
            @MouseY.started -= instance.OnMouseY;
            @MouseY.performed -= instance.OnMouseY;
            @MouseY.canceled -= instance.OnMouseY;
            @Release.started -= instance.OnRelease;
            @Release.performed -= instance.OnRelease;
            @Release.canceled -= instance.OnRelease;
            @Dash.started -= instance.OnDash;
            @Dash.performed -= instance.OnDash;
            @Dash.canceled -= instance.OnDash;
            @Aim.started -= instance.OnAim;
            @Aim.performed -= instance.OnAim;
            @Aim.canceled -= instance.OnAim;
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
    private int m_KMSchemeIndex = -1;
    public InputControlScheme KMScheme
    {
        get
        {
            if (m_KMSchemeIndex == -1) m_KMSchemeIndex = asset.FindControlSchemeIndex("K&M");
            return asset.controlSchemes[m_KMSchemeIndex];
        }
    }
    private int m_ControllerSchemeIndex = -1;
    public InputControlScheme ControllerScheme
    {
        get
        {
            if (m_ControllerSchemeIndex == -1) m_ControllerSchemeIndex = asset.FindControlSchemeIndex("Controller");
            return asset.controlSchemes[m_ControllerSchemeIndex];
        }
    }
    public interface IMainActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnPrimary(InputAction.CallbackContext context);
        void OnSecondary(InputAction.CallbackContext context);
        void OnMouseX(InputAction.CallbackContext context);
        void OnMouseY(InputAction.CallbackContext context);
        void OnRelease(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
    }
}
