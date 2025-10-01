using UnityEngine;
using UnityEngine.InputSystem;

public class PalletMover : ManejoPallets
{
    private PlayerInput playerInput;

    private InputAction firstAction;
    private InputAction secondAction;
    private InputAction thirdAction;

    public ManejoPallets Desde, Hasta;
    bool segundoCompleto = false;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        var actions = playerInput.actions;

        firstAction = actions["First"];
        secondAction = actions["Second"];
        thirdAction = actions["Third"];
    }

    private void OnEnable()
    {
        firstAction.performed += OnFirst;
        secondAction.performed += OnSecond;
        thirdAction.performed += OnThird;

        firstAction.Enable();
        secondAction.Enable();
        thirdAction.Enable();
    }

    private void OnDisable()
    {
        firstAction.performed -= OnFirst;
        secondAction.performed -= OnSecond;
        thirdAction.performed -= OnThird;

        firstAction.Disable();
        secondAction.Disable();
        thirdAction.Disable();
    }

    private void OnFirst(InputAction.CallbackContext ctx)
    {
        if (!Tenencia() && Desde.Tenencia())
            PrimerPaso();
    }

    private void OnSecond(InputAction.CallbackContext ctx)
    {
        if (Tenencia())
            SegundoPaso();
    }

    private void OnThird(InputAction.CallbackContext ctx)
    {
        if (segundoCompleto && Tenencia())
            TercerPaso();
    }

    void PrimerPaso()
    {
        Desde.Dar(this);
        segundoCompleto = false;
    }

    void SegundoPaso()
    {
        base.Pallets[0].transform.position = transform.position;
        segundoCompleto = true;
    }

    void TercerPaso()
    {
        Dar(Hasta);
        segundoCompleto = false;
    }

    public override void Dar(ManejoPallets receptor)
    {
        if (Tenencia())
        {
            if (receptor.Recibir(Pallets[0]))
                Pallets.RemoveAt(0);
        }
    }

    public override bool Recibir(Pallet pallet)
    {
        if (!Tenencia())
        {
            pallet.Portador = this.gameObject;
            base.Recibir(pallet);
            return true;
        }
        return false;
    }
}
