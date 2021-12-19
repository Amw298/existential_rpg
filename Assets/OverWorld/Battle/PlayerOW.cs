// GENERATED AUTOMATICALLY FROM 'Assets/OverWorld/Battle/PlayerOW.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerOW : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerOW()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerOW"",
    ""maps"": [
        {
            ""name"": ""OW"",
            ""id"": ""b240d89f-cd35-4cf4-acb1-5911381f20d5"",
            ""actions"": [
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""fc334dce-7ebb-48a3-a038-7fe2590b5df2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""82f5f917-7fee-4544-a0eb-4343205745d1"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6712fe3d-3f55-4ab3-92fb-b26b4e3e0649"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dc90536a-0749-489b-9b38-c41a9480dcd0"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""4a2f255a-72c1-45a3-8943-4b4c11217b7e"",
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
                    ""id"": ""ae2879d1-375d-40b3-8290-b56c0bbcaf23"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""42e9148f-5e71-414f-aadd-77d5ee2dd830"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""70b1507e-f53a-4fe9-b1ce-5eb7910f1d72"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""e25b6cea-f624-441d-9fa3-5bace174f10a"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // OW
        m_OW = asset.FindActionMap("OW", throwIfNotFound: true);
        m_OW_Interact = m_OW.FindAction("Interact", throwIfNotFound: true);
        m_OW_Move = m_OW.FindAction("Move", throwIfNotFound: true);
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

    // OW
    private readonly InputActionMap m_OW;
    private IOWActions m_OWActionsCallbackInterface;
    private readonly InputAction m_OW_Interact;
    private readonly InputAction m_OW_Move;
    public struct OWActions
    {
        private @PlayerOW m_Wrapper;
        public OWActions(@PlayerOW wrapper) { m_Wrapper = wrapper; }
        public InputAction @Interact => m_Wrapper.m_OW_Interact;
        public InputAction @Move => m_Wrapper.m_OW_Move;
        public InputActionMap Get() { return m_Wrapper.m_OW; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(OWActions set) { return set.Get(); }
        public void SetCallbacks(IOWActions instance)
        {
            if (m_Wrapper.m_OWActionsCallbackInterface != null)
            {
                @Interact.started -= m_Wrapper.m_OWActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_OWActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_OWActionsCallbackInterface.OnInteract;
                @Move.started -= m_Wrapper.m_OWActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_OWActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_OWActionsCallbackInterface.OnMove;
            }
            m_Wrapper.m_OWActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
            }
        }
    }
    public OWActions @OW => new OWActions(this);
    public interface IOWActions
    {
        void OnInteract(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
    }
}
