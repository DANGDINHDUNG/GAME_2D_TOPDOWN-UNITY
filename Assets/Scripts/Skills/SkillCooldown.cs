using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class SkillCooldown : MonoBehaviour
{
    private Camera mainCam;

    [SerializeField] public float currentCastTimer = 0f;
    [SerializeField] public bool castingSkill = false;
    public SkillSO skillToCast;
    public Vector3 mousePos, direction, rotation;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        //myRigidbody.isKinematic = true;

        Destroy(this.gameObject, skillToCast.LifeTime);
    }

    protected virtual void Start()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - transform.position;
        rotation = transform.position - mousePos;
    }
}
