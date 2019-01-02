using UnityEngine;
using System.Collections;

public class Possition : MonoBehaviour
{
	void OnDrawGizmos()
    {
		Gizmos.DrawWireSphere (transform.position, 1);
	}
}
