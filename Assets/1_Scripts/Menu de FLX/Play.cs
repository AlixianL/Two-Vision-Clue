using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play : MonoBehaviour,IActivatable
{
    [SerializeField] private List<GameObject> MainMenu = new List<GameObject>();
    [SerializeField] private GameObject _recuperationPanel;



    [SerializeField] private Transform _playerPosition;
    [SerializeField] private GameObject _cameraPanel;

    [SerializeField] private float _effectDuration;
    [SerializeField] private float _playerDistance;


    public void Start()
    {
        GameManager.Instance.ToggleMovementFreezePlayer();
        _cameraPanel.SetActive(false);
    }
    public void Activate()
    {
        foreach (GameObject Panel in MainMenu)
        {
            SetLayerRecursively(Panel, LayerMask.NameToLayer("OnlyVisibleToTheCamera"));

            BoxCollider collider = Panel.GetComponent<BoxCollider>();
            if (collider != null)
            {
                collider.enabled = false;
            }
        }

        _recuperationPanel.SetActive(true);

        _cameraPanel.SetActive(true);
        StartCoroutine(PlayIntroEffect());


        GameManager.Instance.ToggleMovementFreezePlayer();
    }

    private void SetLayerRecursively(GameObject obj, int newLayer)
    {
        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }
    private IEnumerator PlayIntroEffect()
    {


        RectTransform camRect = _cameraPanel.GetComponent<RectTransform>();

        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // -- Reference ------------------------------------------------
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        // --- Reference du joueur ---
        Vector3 startPos = _playerPosition.position;
        Vector3 endPos = startPos - _playerPosition.forward * _playerDistance;

        // --- Reference des taille ---
        Vector2 startSize = camRect.sizeDelta;
        Vector2 endSize = new Vector2(600f, 400f);

        // --- Reference des position ---
        Vector2 startPosUI = Vector2.zero;
        Vector2 endPosUI = new Vector2(-620f, 300f);


        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // -- logique des déplacement  ---------------------------------
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        for (float t = 0; t < 1f; t += Time.deltaTime / _effectDuration)
        {
            float progress = t;

            _playerPosition.position = Vector3.Lerp(startPos, endPos, progress);
            camRect.sizeDelta = Vector2.Lerp(startSize, endSize, progress);
            camRect.anchoredPosition = Vector2.Lerp(startPosUI, endPosUI, progress);

            yield return null;
        }

        _playerPosition.position = endPos;
        camRect.sizeDelta = endSize;
        camRect.anchoredPosition = endPosUI;
    }

}
