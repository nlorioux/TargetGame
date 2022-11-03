using System.Collections;
using UnityEngine;

public class goldTarget : MonoBehaviour
{
    private Rigidbody rb;
    public AudioClip clip;
    public AudioClip clipDestruction;
    private float creationTime;
    private bool currentlyDestroying;
    private float waitingTime = .05f;
    private int lifeExpectancy = 10;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Vector3 strength = new Vector3(0f, Random.Range(-30f, 30f), Random.Range(-30f, 30f));
        rb.useGravity = false;
        rb.AddForce(strength, ForceMode.Impulse);
        creationTime = Time.timeSinceLevelLoad;
        currentlyDestroying = false;
    }

    private IEnumerator FlashingObject()
    {
        for (int i = 0; i < 30; i++)
        {
            GetComponent<Renderer>().enabled = false;
            yield return new WaitForSeconds(waitingTime);
            GetComponent<Renderer>().enabled = true;
            yield return new WaitForSeconds(waitingTime);
        }
        AudioSource.PlayClipAtPoint(clipDestruction, new Vector3(0, 0, 0));
        GameManager.Instance.addScore(- 1);
        GameManager.Instance.targetCount -= 1;
        GameManager.Instance.loadLevelTarget();
        Destroy(gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        float timeSinceCreation = Time.timeSinceLevelLoad - creationTime;
        if (!currentlyDestroying && timeSinceCreation > lifeExpectancy)
        {
            currentlyDestroying = true;
            StartCoroutine(FlashingObject());
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(clip, new Vector3(0, 0, 0));
            GameManager.Instance.targetCount -= 1;
            GameManager.Instance.addScore(3);
            GameManager.Instance.loadLevelTarget();
            Destroy(gameObject);
        }

    }
}
