using UnityEngine;
using System.Collections;
[System.Serializable]
public class Damager : MonoBehaviour {
	private float damage;
	private bool destroyOnHit;

	public void setDamage(float dmg)
	{
		//print ("setting damage to" + dmg);
		damage = dmg;
	}
	public void setDoH(bool destroyy)
	{
		//print ("setting doh to" + destroyy);
		destroyOnHit = destroyy;
	}
	public float getDamage()
	{
		return damage;
	}
	public bool getDoH()
	{
		return destroyOnHit;
	}
}
