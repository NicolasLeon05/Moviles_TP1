using UnityEngine;
using UnityEngine.InputSystem;

public class PalletMover : ManejoPallets
{
    private InputSystem_Actions actions;

    [SerializeField] private int playerId = 0; // 0 = P1, 1 = P2

    public ManejoPallets Desde, Hasta;
    private bool segundoCompleto = false;


    private void TryInit()
    {
        if (actions == null && GameManager.Instancia != null)
            actions = GameManager.Instancia.actions;
    }

    private void Start()
    {
        TryInit();
    }

    private void OnEnable()
    {
        TryInit();

        if (playerId == 0)
        {
            actions.Unloading.FirstP1.performed += OnFirst;
            actions.Unloading.SecondP1.performed += OnSecond;
            actions.Unloading.ThirdP1.performed += OnThird;
        }
        else
        {
            actions.Unloading.FirstP2.performed += OnFirst;
            actions.Unloading.SecondP2.performed += OnSecond;
            actions.Unloading.ThirdP2.performed += OnThird;
        }
    }

    private void OnDisable()
    {
        if (playerId == 0)
        {
            actions.Unloading.FirstP1.performed -= OnFirst;
            actions.Unloading.SecondP1.performed -= OnSecond;
            actions.Unloading.ThirdP1.performed -= OnThird;
        }
        else
        {
            actions.Unloading.FirstP2.performed -= OnFirst;
            actions.Unloading.SecondP2.performed -= OnSecond;
            actions.Unloading.ThirdP2.performed -= OnThird;
        }
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
