using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using FMODUnity;


public class DetectionGround : MonoBehaviour
{
    [SerializeField]
    private LayerMask _floorLayer;
    [SerializeField]
    private TextureSound[] _textureSounds;
    [SerializeField]
    private bool _blendTerrainSounds;


    private void Start()
    {
        StartCoroutine(CheckGround());
    }


    //Check du sol avec Collider a niveau des pieds
    //Si il touche Terrain il commence un Coroutine
    private IEnumerator CheckGround()
    {
        while (true)
        {
            Vector3 origin = transform.position;
            //Debug.Log(origin);
            Debug.Log(PlayerBrain.Instance.playerRigidbody.linearVelocity);
            if (PlayerBrain.Instance.isGrounded && PlayerBrain.Instance.playerRigidbody.linearVelocity != Vector3.zero &&
                Physics.Raycast (origin, 
                    Vector3.down,
                    out RaycastHit hit,
                    2f,
                    _floorLayer)
                )
            {
                if (hit.collider.TryGetComponent<Terrain>(out Terrain terrain))
                {
                    Debug.Log("ca marche");
                    yield return StartCoroutine(PlayFootstepSoundFromTerrain(terrain, hit.point));
                    
                }
                else if (hit.collider.TryGetComponent<Renderer>(out Renderer renderer))
                {
                    yield return StartCoroutine(PlayFootstepSoundFromRenderer(renderer));
                }
            }

            yield return null;
        }
    }


    //Jouer un type de son par rapport au Terrain
    //Il détecte quel type de matériaux est en dessous du joueurs et renvoie une information
    private IEnumerator PlayFootstepSoundFromTerrain(Terrain Terrain, Vector3 HitPoint)
    {
        Vector3 terrainPosition = HitPoint - Terrain.transform.position;
        Vector3 splatMapPosition = new Vector3(
            terrainPosition.x / Terrain.terrainData.size.x,
            0,
            terrainPosition.z / Terrain.terrainData.size.z
        );

        int x = Mathf.FloorToInt(splatMapPosition.x * Terrain.terrainData.alphamapWidth);
        int z = Mathf.FloorToInt(splatMapPosition.z * Terrain.terrainData.alphamapHeight);

        float[,,] alphaMap = Terrain.terrainData.GetAlphamaps(x, z, 1, 1);

        if (!_blendTerrainSounds)
        {
            int primaryIndex = 0;
            for (int i = 1; i < alphaMap.Length; i++)
            {
                if (alphaMap[0, 0, i] > alphaMap[0, 0, primaryIndex])
                {
                    primaryIndex = i;
                }
            }

            foreach (TextureSound textureSound in _textureSounds)
            {
                if (textureSound.BaseColor == Terrain.terrainData.terrainLayers[primaryIndex].diffuseTexture)
                {
                    EventReference clip = GetClipFromTextureSound(textureSound);
                    RuntimeManager.PlayOneShot(clip);
                    Debug.Log("C'est good");
                    yield return null;
                }
            }
        }

    }


    private IEnumerator PlayFootstepSoundFromRenderer(Renderer Renderer)
    {
        foreach (TextureSound textureSound in _textureSounds)
        {
            if (textureSound.BaseColor == Renderer.material.GetTexture("_MainTex"))
            {
                EventReference clip = GetClipFromTextureSound(textureSound);

                RuntimeManager.PlayOneShot(clip);
                Debug.Log("Ca marche");
                yield return null;
            }
        }
    }

    //Récupère l'information envoyer dans le script et revoie l'information du son de la texture associer.
    private EventReference GetClipFromTextureSound(TextureSound TextureSound)
    {

        return TextureSound.Clips;
    }

    [System.Serializable]
    private class TextureSound
    {
        public Texture BaseColor;
        public EventReference Clips;
    }
}