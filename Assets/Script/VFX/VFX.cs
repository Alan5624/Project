using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class VFX : MonoBehaviour
{
    private void OnEnable()
	{
		StartCoroutine("CheckIfAlive");
	}

    IEnumerator CheckIfAlive ()
	{
		ParticleSystem ps = GetComponent<ParticleSystem>();

		while(true && ps != null)
		{
			yield return new WaitForSeconds(0.5f);
			if(!ps.IsAlive(true))
			{
				Destroy(this.gameObject);
				break;
			}
		}
	}
}
