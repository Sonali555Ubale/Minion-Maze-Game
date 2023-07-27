using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitParticle : MonoBehaviour
{
    // Start is called before the first frame update
    public bool hit = false;
    public GameObject Hitobject;
    public Vector3 direction = new Vector3();
    public ParticleSystem dust;
    public ParticleSystem wood;
    public ParticleSystem metal;
    public Transform hitpoint;
    public float force = 2;

    public void ItemHit()
    {
        StressReceiver.Instance.InduceStress(0.1f);

        if (Hitobject != null && Hitobject.tag==MaterialTypes.Wood.ToString())
        {
            if (dust != null && hit)
            {
                ParticleSystem dustInstance = Instantiate(dust, hitpoint.position, Quaternion.Inverse(hitpoint.rotation));
                ParticleSystem WoodInstance = Instantiate(wood, hitpoint.position, Quaternion.Inverse(hitpoint.rotation));
                dustInstance.Play();
                WoodInstance.Play();
                Destroy(dustInstance.gameObject, dustInstance.main.duration);
                Destroy(WoodInstance.gameObject, WoodInstance.main.duration);
                Hitobject.GetComponent<MeshDestroy>().BreakMesh(direction,force);
                Hitobject = null;
            }
        }
        else if(Hitobject != null && Hitobject.tag == MaterialTypes.Metal.ToString())
        {
            ParticleSystem metalInstance = Instantiate(metal, hitpoint.position, Quaternion.Inverse(hitpoint.rotation));
            metalInstance.Play();
            Destroy(metalInstance.gameObject, metalInstance.main.duration);
            Hitobject = null;
        }
    }
}