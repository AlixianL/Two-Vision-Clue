using System;
using UnityEngine;
using UnityEngine.UI;

public class TabeltteScrollBar : MonoBehaviour
{
    [Header("References"), Space(5)]
    [SerializeField] private Scrollbar _scrollbar;
    [SerializeField] private ActivateTrackPlayer _activateTrackPlayer;
    [SerializeField] private TabletteToggle _tabletteToggle;
    [SerializeField] private RectTransform _tabletteRectTransform;
    [SerializeField] private Vector2 _tabletteScrollPositionMax;

    [Header("Variables"), Space(5)]
    [SerializeField] private float _mouseWeel;
    [SerializeField, Range(1, 500)] private float _amplificator;
    private void Update()
    {
        if (_activateTrackPlayer._playerInRange && _tabletteToggle._infoIsOn)
        {
            _mouseWeel = PlayerBrain.Instance.player.GetAxis("ScrollBarMovement");
            if (_mouseWeel != 0)
            {
                if (_tabletteRectTransform.position.y > 0 && _tabletteRectTransform.position.y < _tabletteScrollPositionMax.y)
                {
                    _tabletteRectTransform.transform.localPosition = new Vector2(_tabletteRectTransform.transform.localPosition.x, _tabletteRectTransform.transform.localPosition.y + -_mouseWeel*_amplificator);
                }
            }
        }
    }
}
