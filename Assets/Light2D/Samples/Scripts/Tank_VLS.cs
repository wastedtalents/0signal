using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tank_VLS : MonoBehaviour 
{
    public bool isPlayerControlled = true;

    public float linearSpeed = 5;
    public float rotationSpeed = 5;
    public int maxBulletCount = 10;

    public GameObject basePiece;
    public GameObject turretPiece;
    public GameObject bulletPiece;
    public Transform bulletEmissionPoint;

    private GameObject[] bulletPool;

    void Start()
    {
        bulletPool = new GameObject[maxBulletCount];
        for (int i = 0; i < maxBulletCount; i++)
        {
            bulletPool[i] = (GameObject)Instantiate(bulletPiece, Vector3.zero, Quaternion.identity);
            bulletPool[i].SetActive(false);
        }
    }

    void Update()
    {
        if (isPlayerControlled)
            UpdatePlayerMovements();
        else
            UpdateAIMovements();
    }

    void UpdatePlayerMovements()
    {
        basePiece.transform.Translate(0, -Input.GetAxis("Vertical") * linearSpeed * Time.deltaTime, 0, Space.Self);
        turretPiece.transform.Rotate(0, 0, -Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime , Space.Self);

        if (Input.GetButton("Fire1"))
        {
            bulletPool[0].SetActive(true);
            bulletPool[0].transform.position = bulletEmissionPoint.position;
            bulletPool[0].transform.rotation = bulletEmissionPoint.rotation;
            bulletPool[0].rigidbody2D.velocity = new Vector2(0, 50);
        }

        if (Input.GetAxis("Vertical") != 0)
        {
            basePiece.transform.rotation = Quaternion.Lerp(basePiece.transform.rotation, turretPiece.transform.rotation, Time.deltaTime * 2);
            turretPiece.transform.rotation = Quaternion.Lerp(turretPiece.transform.rotation, basePiece.transform.rotation, Time.deltaTime * 2);
        }
    }

    void UpdateAIMovements()
    {

    }
}
