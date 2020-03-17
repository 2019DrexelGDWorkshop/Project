using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public GameObject DEGO;


    public float moveSpeed = 6.0f;
    public float rotationSpeed = 10.0f;
    public float jumpSpeed = 8.0f;
    public float jumpUpTime = 0.5f;
    public float gravity = 10.0f;
    public float jetpackForce = 1;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool wasGrounded = false;

    //public bool isGrounded = false;
    public float groundMargin = 1.2f;
    public float groundDetectRadius = 0.5f;

    public Vector3 externalForces = Vector3.zero;

    [SerializeField]
    private LayerMask platformLayer;

    [HideInInspector]
    public Vector2 motion;
    public bool isjumping = false;
    private bool isFlying = false;
    private float delayJetpack = 0.0f;
    private float timer = 0.0f;
    private bool canUseJetpack = false;


    CharacterController cc;

    Vector3 targetDirection = new Vector3(0, 0, 0);
    private float currentJetpackForce = 0.0f;

    public delegate void OnGroundedHandler(bool isGrounded);
    public event OnGroundedHandler OnGrounded;

    private Rewired.Player rewiredPlayer;

    public void Init()
    {
        rewiredPlayer = Rewired.ReInput.players.GetPlayer(0);
        delayJetpack = jetpackForce;
        cc = GetComponent<CharacterController>();
    }

    /*public void changeGroundState(bool newState)
    {
        isGrounded = newState;
    }*/

    private void Update()
    {
        if (rewiredPlayer.GetButton(RewiredConsts.Action.Jump))
        {
            timer += Time.deltaTime;
        }
        else if (rewiredPlayer.GetButtonUp(RewiredConsts.Action.Jump))
        {
            timer = 0;
        }
        if (isjumping)
        {
            if (timer > 0.3f)
            {
                ActivateFlying();
            }
        }
        if(canUseJetpack)
            JetPack();
    }

    private void ActivateFlying()
    {
        isFlying = true;
    }

    bool IsGrounded()
    {
        Vector3 pointBottom = transform.position - transform.up * groundMargin + transform.up * groundDetectRadius;
        Vector3 pointTop = transform.position + transform.up * groundDetectRadius;
        LayerMask ignoreMask = ~(1 << 9);

        //Debug.DrawLine(pointBottom, pointTop, Color.green);
        //Debug.Log(Physics.OverlapCapsule(pointBottom, pointTop, groundDetectRadius, ignoreMask, QueryTriggerInteraction.Ignore)[0]);
        if (Physics.OverlapCapsule(pointBottom, pointTop, groundDetectRadius, ignoreMask, QueryTriggerInteraction.Ignore).Length != 0)
        {
            //Debug.Log(Physics.OverlapCapsule(pointBottom, pointTop, groundDetectRadius, ignoreMask, QueryTriggerInteraction.Ignore)[0]);
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (!wasGrounded && isGrounded)
        {

            wasGrounded = true;
            OnGrounded?.Invoke(true);

        }
        else if (wasGrounded && !isGrounded)
        {
            wasGrounded = false;
            OnGrounded?.Invoke(false);
        }

        return isGrounded;

        //return isGrounded;
        //isGrounded |= Physics.Raycast(transform.position, -Vector3.up, groundMargin, 1, QueryTriggerInteraction.Ignore);
        //isGrounded |= Physics.Raycast(transform.position + transform.forward, -Vector3.up, groundMargin, 1, QueryTriggerInteraction.Ignore);
        //return Physics.Raycast(transform.position, -Vector3.up, groundMargin, 1, QueryTriggerInteraction.Ignore);   // Watch out the layer!!!
    }

    bool IsGroundedJump()
    {
        Vector3 pointBottom = transform.position - transform.up * groundMargin + transform.up * groundDetectRadius;
        Vector3 pointTop = transform.position + transform.up * groundDetectRadius;
        LayerMask ignoreMask = ~(1 << 9);

        if (Physics.OverlapCapsule(pointBottom, pointTop, groundDetectRadius + 0.1f, ignoreMask, QueryTriggerInteraction.Ignore).Length != 0)
        {
            return true;
        }
        else
            return false;
    }

    public Vector3 moveVec = new Vector3(0.0f, 0.0f, 0.0f);
    public void updateMontion()
    {
        //Debug.Log(IsGrounded());

        updateRotation();

        float tmpSpeed = Mathf.Abs(motion.x) + Mathf.Abs(motion.y);
        tmpSpeed = Mathf.Clamp(tmpSpeed, 0, 1f);

        float tmpy = moveVec.y;

        if (CameraManager.Instance.cameraState == CameraState.SIDE_SCROLLER)
            moveVec = targetDirection * tmpSpeed * moveSpeed;
        else
            moveVec = transform.forward * tmpSpeed * moveSpeed;

        if (isjumping)
        {
            jumpCountDown -= Time.deltaTime;
            if (jumpCountDown <= 0.0f)
            {
                isjumping = false;
                moveVec.y = 0;
            }
            moveVec.y = Mathf.Lerp(0, jumpSpeed, jumpCountDown / jumpUpTime);
        }
        else if (!isjumping && !IsGrounded())
            moveVec.y = tmpy - gravity * Time.deltaTime;
        else
            moveVec.y = 0;
        cc.Move((moveVec + externalForces) * Time.deltaTime);
        externalForces = Vector3.zero;
    }

    void updateRotation()
    {
        //Debug.Log(targetDirection);
        if (targetDirection.magnitude <= 0)
            return;
        Vector3 lookDirection = targetDirection.normalized;
        Quaternion rotationAngle = Quaternion.LookRotation(lookDirection, transform.up);

        float diffRotation = rotationAngle.eulerAngles.y - transform.eulerAngles.y;
        float eulerY = Mathf.Abs(diffRotation) > 0 ? rotationAngle.eulerAngles.y : transform.eulerAngles.y;
        Vector3 euler = new Vector3(transform.eulerAngles.x, eulerY, transform.eulerAngles.z);

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(euler), rotationSpeed * Time.deltaTime);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PowerUp")
        {
            canUseJetpack = true;// jetpackForce = 1;
            Destroy(other.gameObject);
        }
    }

    private void JetPack()
    {
        if (isFlying)
        {
            if (rewiredPlayer.GetButton(RewiredConsts.Action.Jump) && jetpackForce > 0)
            {
                GetComponentInChildren<Animator>().enabled = false;
                jetpackForce -= Time.deltaTime;
                if (currentJetpackForce < 1)
                {
                    currentJetpackForce += Time.deltaTime * 10.0f;
                }
                else
                {
                    currentJetpackForce = 1.0f;
                }
            }
            if (jetpackForce < 0 && currentJetpackForce > 0)
            {
                currentJetpackForce -= Time.deltaTime;
            }
            if (!rewiredPlayer.GetButton(RewiredConsts.Action.Jump))
            {
                if (currentJetpackForce > 0)
                {
                    currentJetpackForce -= Time.deltaTime;
                }
                else
                {
                    currentJetpackForce = 0;
                }
                if (jetpackForce < delayJetpack)
                {
                    jetpackForce = delayJetpack;
                }
                else
                {
                    jetpackForce = delayJetpack;
                }
                isFlying = false;
            }
            if (currentJetpackForce > 0)
            {
                moveVec = Vector3.up;
                if (CameraManager.Instance.cameraState == CameraState.SIDE_SCROLLER)
                {
                    if (rewiredPlayer.GetAxis(RewiredConsts.Action.MoveRight) > 0)
                    {
                        transform.Translate(new Vector3(0, 0, 1) * moveSpeed * Time.deltaTime);
                    }
                    else if (rewiredPlayer.GetAxis(RewiredConsts.Action.MoveRight) < 0)
                        transform.Translate(new Vector3(0, 0, 1) * moveSpeed * Time.deltaTime);
                }
                if (CameraManager.Instance.cameraState == CameraState.THIRD_PERSON)
                {
                    moveVec += transform.right * rewiredPlayer.GetAxis(RewiredConsts.Action.MoveRight);
                    moveVec += transform.forward * rewiredPlayer.GetAxis(RewiredConsts.Action.MoveForward);
                }

                cc.Move((moveVec * moveSpeed * Time.deltaTime - cc.velocity * Time.deltaTime) * currentJetpackForce);
            }
        }
        else
        {
            GetComponentInChildren<Animator>().enabled = true;
        }
    }

    public void updateAnimation()
    {

    }

    private float jumpCountDown = 0.0f;
    public void Jump()
    {
        if (!IsGroundedJump())
            return;
        isjumping = true;
        jumpCountDown = jumpUpTime;
    }

    public void updateTargetDirection(Transform referenceTrans)
    {
        Vector3 forward = referenceTrans.TransformDirection(Vector3.forward);
        forward.y = 0;

        Vector3 right = referenceTrans.TransformDirection(Vector3.right);

        targetDirection = motion.x * right + motion.y * forward;
    }

    public void Kill()
    {
        DEGO.GetComponent<DeathEffectManager>().DeathEffect();
        transform.position = LevelManager.Instance.lastCheckPoint.position;
    }


    public void PlacePlayerOn3DPlatform()
    {
        StartCoroutine(PlacePlayerOn3DPlatformHelper());
    }

    private IEnumerator PlacePlayerOn3DPlatformHelper()
    {
        RaycastHit hit;
        if (Physics.SphereCast(this.transform.position, .5f, Vector3.down, out hit, 5f, platformLayer))
        {
            Debug.Log("Hit " + hit.transform.gameObject.name);
            Vector3 objPos = hit.point;
            bool isRight = true;


            Vector3 destination = objPos;
            destination.z = transform.position.z;

            Vector3 downCheck = destination + Vector3.up;
            Vector3 sideCheck = destination - (Vector3.up*.05f);

            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame(); // So 2d collider turns off

            Debug.DrawRay(downCheck, Vector3.down * 5f, Color.red, 100000f);
            if (Physics.SphereCast(downCheck, .25f, Vector3.down, out hit, 5f, platformLayer))
            {
                Debug.DrawRay(downCheck, Vector3.down * 5f, Color.red, 100000f);
                Debug.Log("Down " + hit.transform.gameObject.name);
            }
            else if (Physics.SphereCast(sideCheck, .25f, Vector3.right, out hit, 500f, platformLayer))
            {
                Debug.DrawRay(sideCheck, Vector3.right * 200f, Color.red, 100000f);
                Debug.Log("Right " + hit.transform.gameObject.name);
            }
            else if (Physics.SphereCast(sideCheck, .25f, Vector3.left, out hit, 500f, platformLayer))
            {
                Debug.DrawRay(sideCheck, Vector3.left * 200f, Color.red, 100000f);
                Debug.Log("Left " + hit.transform.gameObject.name);
            }

            destination.y = transform.position.y;
            destination.x = hit.point.x;

            this.transform.position = destination;
        }
    }

    private GameObject throwObj = null;

    public void pickUpCheck()
    {
        if (throwObj != null)
        {
            throwObj.GetComponent<ThrowableObjectLogic>().throwOut();
            throwObj = null;
        }
        else
        {
            Collider[] hitColliders = Physics.OverlapBox(transform.position, transform.localScale, Quaternion.identity, 1 << 13);
            int i = 0;
            //Check when there is a new collider coming into contact with the box
            while (i < hitColliders.Length)
            {
                //Output all of the collider names
                if (hitColliders[i].tag == "Throwable")
                {
                    Debug.Log("Hit : " + hitColliders[i].name + i);
                    throwObj = hitColliders[i].gameObject;
                    throwObj.GetComponent<ThrowableObjectLogic>().pickUp(this.gameObject);
                    break;
                }
                //Increase the number of Colliders in the array
                i++;
            }
        }
    }

}