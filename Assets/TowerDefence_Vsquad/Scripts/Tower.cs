using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {


    public bool Catcher = false;
	public Transform shootElement;
    public GameObject Towerbug;
    public Transform LookAtObj;    
    public GameObject bullet;
    public GameObject DestroyParticle;
    public Vector3 impactNormal_2;
    public Transform target;
    public int dmg = 10;
    public float shootDelay;
    bool isShoot;
    private float homeY;

    public LayerMask enemyLayer;
    public float sightRange;

    // for Catcher tower 

    void Start()
    {
        homeY = LookAtObj.transform.localRotation.eulerAngles.y;
    }
           

    
    // for Catcher tower attack animation

    void GetDamage()

    {
        if (target)
        {
            target.GetComponent<EnemyBehavior>().TakeDamage(dmg, out var _, 0);
        }
    }




    void Update () {

        var sightColiders = Physics.OverlapSphere(transform.position, sightRange, enemyLayer);
        // Tower`s rotate

        if (sightColiders.Length != 0)
        {
            target = sightColiders[0].gameObject.transform;
            if (sightColiders[0].gameObject.transform)
            {

                Vector3 dir = target.transform.position - LookAtObj.transform.position;
                dir.y = 0;
                Quaternion rot = Quaternion.LookRotation(dir);
                LookAtObj.transform.rotation = Quaternion.Slerp(LookAtObj.transform.rotation, rot, 5 * Time.deltaTime);

            }
            else
            {

                Quaternion home = new Quaternion(0, homeY, 0, 1);

                LookAtObj.transform.rotation = Quaternion.Slerp(LookAtObj.transform.rotation, home, Time.deltaTime);
            }
        }




        // Shooting
               

            if (!isShoot)
            {
                StartCoroutine(shoot());

            }

        
        if (Catcher == true)
        {
            if (!target || target.CompareTag("Dead"))
            {

                StopCatcherAttack();
            }

        }



    }

	IEnumerator shoot()
	{
		isShoot = true;
		yield return new WaitForSeconds(shootDelay);


        if (target && Catcher == false)
        {
            GameObject b = GameObject.Instantiate(bullet, shootElement.position, Quaternion.identity) as GameObject;
            b.GetComponent<TowerBullet>().target = target;
            b.GetComponent<TowerBullet>().twr = this;
          
        }



        isShoot = false;
	}



        void StopCatcherAttack()

        {                
            target = null;     
        } 
          

}



