using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlaqueManager;

public class PlaqueManager : MonoBehaviour
{
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- Initialisation de la liste a modifier  -------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


    public enum PositionID { Haut, Bas, Gauche, Droite }

    [System.Serializable]
    public class PlaqueInfo
    {
        public PositionID positionID;
        public Transform plaqueTransform;
        public PositionID goodposition;

        [HideInInspector] public Vector3 originalForward;
    }

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- Variable  ------------------------------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    [SerializeField] private List<PlaqueInfo> plaques = new List<PlaqueInfo>();
    [SerializeField] private float _swapDuration = 1f;
    [SerializeField] private float _turnDuration = 0.7f;
    [SerializeField] private float _decalDistance = 0.2f;

    [SerializeField] private bool _enigmaisEnd = false;
    [SerializeField] public bool _allIsFacing = false;

    [SerializeField] private Transform _gumGum;





    private bool isSwapping = false;

    void Start()
    {
        foreach (var plaque in plaques)
            plaque.originalForward = plaque.plaqueTransform.forward;

        _allIsFacing = false;
        _enigmaisEnd = false;

    }

    void Update()
    {
        if (AllPlaquesAreCorrect() && _allIsFacing && !_enigmaisEnd)
        {
            Ending();
        }
    }

    private bool AllPlaquesAreCorrect()
    {
        foreach (var p in plaques)
        {
            if (p.positionID != p.goodposition)
                return false;
        }
        return true;
    }
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- Fonction appelé par les bouton  --------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public void Swap(PositionID pos1, PositionID pos2)
    {
        Debug.Log("SWAP");
        if (!isSwapping && !_enigmaisEnd )
        {
            Transform a = GetPlaqueByPosition(pos1);
            Transform b = GetPlaqueByPosition(pos2);

            if (a == null || b == null)
            {
                Debug.LogWarning("Position non trouvée.");
                return;
            }

            StartCoroutine(SwapRoutine(a, b));
        }  
    }

    public void Turnup()
    {
        Debug.Log("TOURNEEEEE");

        if (!isSwapping && !_enigmaisEnd)
        {
            Transform r = GetPlaqueByPosition(PositionID.Gauche);
            StartCoroutine(TurnRoutine(r));

        }

    }



    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- Fonction pour trouvé la plaque avec ID  ------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    private Transform GetPlaqueByPosition(PositionID id)
    {
        foreach (var p in plaques)
            if (p.positionID == id)
                return p.plaqueTransform;
        return null;
    }

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- Coroutine de swap  ---------------------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    private IEnumerator SwapRoutine(Transform _plaque1, Transform _plaque2)
    {
        isSwapping = true;

        Vector3 startA = _plaque1.position;
        Vector3 startB = _plaque2.position;

        Vector3 forwardA = plaques.Find(p => p.plaqueTransform == _plaque1).originalForward;
        Vector3 forwardB = plaques.Find(p => p.plaqueTransform == _plaque2).originalForward;

        Vector3 preMoveA = startA - forwardA * _decalDistance;
        Vector3 preMoveB = startB + forwardB * _decalDistance;

        float elapsed = 0f;

        //Phase 1
        while (elapsed < _swapDuration * 0.3f)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / (_swapDuration * 0.3f));

            _plaque1.position = Vector3.Lerp(startA, preMoveA, t);
            _plaque2.position = Vector3.Lerp(startB, preMoveB, t);

            yield return null;
        }

        //Phase 2

        Vector3 crossA = startB - forwardA * _decalDistance;
        Vector3 crossB = startA + forwardB * _decalDistance;

        elapsed = 0f;
        while (elapsed < _swapDuration * 0.4f)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / (_swapDuration * 0.4f));

            _plaque1.position = Vector3.Lerp(preMoveA, crossA, t);
            _plaque2.position = Vector3.Lerp(preMoveB, crossB, t);

            yield return null;
        }


        //Phase 3
        Vector3 settleA = startB;
        Vector3 settleB = startA;

        elapsed = 0f;

        while (elapsed < _swapDuration * 0.3f)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / (_swapDuration * 0.3f));

            _plaque1.position = Vector3.Lerp(crossA, settleA, t);
            _plaque2.position = Vector3.Lerp(crossB, settleB, t);

            yield return null;
        }

        _plaque1.position = settleA;
        _plaque2.position = settleB;

        PlaqueInfo infoA = plaques.Find(p => p.plaqueTransform == _plaque1);
        PlaqueInfo infoB = plaques.Find(p => p.plaqueTransform == _plaque2);
        if (infoA != null && infoB != null)
        {
            var _temp = infoA.positionID;
            infoA.positionID = infoB.positionID;
            infoB.positionID = _temp;
        }

        isSwapping = false;
    }


    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- Coroutine de rotation  -----------------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    private IEnumerator TurnRoutine(Transform r)
    {
        isSwapping = true;

        Quaternion startRotation = r.rotation;
        Quaternion endRotation = Quaternion.AngleAxis(180f, Vector3.up) * startRotation;

        float elapsed = 0f;

        while (elapsed < _turnDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / _turnDuration);

            r.rotation = Quaternion.Slerp(startRotation, endRotation, t);

            yield return null;
        }

        r.rotation = endRotation;
        isSwapping = false;
    }

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- Fin de l'énigme  -----------------------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    public void Ending()
    {
        _enigmaisEnd = true;
        StartCoroutine(GumGumUp(3f));
        Debug.Log("c'est fintio");
    }

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    // -- Coroutine de GumGum  -------------------------------------
    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    IEnumerator GumGumUp(float height)
    {
        float duration = 1f;
        float elapsed = 0f;

        Vector3 initialPosition = _gumGum.transform.position;
        Vector3 targetPosition = initialPosition + Vector3.up * height;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            _gumGum.transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
            yield return null;
        }

        _gumGum.transform.position = targetPosition;
    }

    [ContextMenu("fin de l'énigme final")]
    public void End() => Ending();
}
