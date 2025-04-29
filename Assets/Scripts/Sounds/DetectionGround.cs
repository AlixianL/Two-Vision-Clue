using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using FMODUnity;


public class DetectionGround : MonoBehaviour
{
    [SerializeField]
    private LayerMask FloorLayer;
    [SerializeField]
    private TextureSound[] TextureSounds;
    [SerializeField]
    private bool BlendTerrainSounds;

    private CharacterController Controller;
    

    private void Awake()
    {
        Controller = GetComponent<CharacterController>();
        
    }

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
            if (Controller.isGrounded && Controller.velocity != Vector3.zero &&
                Physics.Raycast(transform.position - new Vector3(0, 0.5f * Controller.height + 0.5f * Controller.radius, 0),
                    Vector3.down,
                    out RaycastHit hit,
                    1f,
                    FloorLayer)
                )
            {
                if (hit.collider.TryGetComponent<Terrain>(out Terrain terrain))
                {
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

        if (!BlendTerrainSounds)
        {
            int primaryIndex = 0;
            for (int i = 1; i < alphaMap.Length; i++)
            {
                if (alphaMap[0, 0, i] > alphaMap[0, 0, primaryIndex])
                {
                    primaryIndex = i;
                }
            }

            foreach (TextureSound textureSound in TextureSounds)
            {
                if (textureSound.BaseColor == Terrain.terrainData.terrainLayers[primaryIndex].diffuseTexture)
                {
                    EventReference clip = GetClipFromTextureSound(textureSound);
                    RuntimeManager.PlayOneShot(clip);
                    yield return null; 
                }
            }
        }
       
    }

    
    private IEnumerator PlayFootstepSoundFromRenderer(Renderer Renderer)
    {
        foreach (TextureSound textureSound in TextureSounds)
        {
            if (textureSound.BaseColor == Renderer.material.GetTexture("_MainTex"))
            {
                EventReference clip = GetClipFromTextureSound(textureSound);

                RuntimeManager.PlayOneShot(clip);
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