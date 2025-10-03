using UnityEngine;
using UnityEngine.InputSystem;

public class PalletMover : ManejoPallets
{
    [SerializeField] private InputActionReference firstAction; // A/Left/StickLeft/SwipeLeft
    [SerializeField] private InputActionReference secondAction; // S/Down/StickDown/SwipeDown
    [SerializeField] private InputActionReference thirdAction; // D/Right/StickRight/SwipeRight
    public ManejoPallets Desde, Hasta; bool segundoCompleto = false; private void OnEnable()
    {
        firstAction.action.performed += OnFirst;
        secondAction.action.performed += OnSecond;
        thirdAction.action.performed += OnThird;
    }

    private void OnDisable()
    {
        firstAction.action.performed -= OnFirst;
        secondAction.action.performed -= OnSecond;
        thirdAction.action.performed -= OnThird;
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
        Pallets[0].transform.position = transform.position;
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
            pallet.Portador = gameObject;
            base.Recibir(pallet);
            return true;
        }
        return false;
    }
}