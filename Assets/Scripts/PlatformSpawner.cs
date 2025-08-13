using System.Collections;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject normalPlatform;
    public GameObject jumpPlatform;
    public GameObject movePlatform;
    public GameObject itemPlatform;
    public GameObject endPlatform;

    public GameObject[] Item;

    public float gap = 3f;
    public float afterMoveGap = 5f;
    public float jumpUp = 2f;
    public Vector3 startPos = Vector3.zero;

    private Vector3 nowPos;
    private float nowHeight = 0f;
    private GameObject lastPlatform = null;
    private Vector3 lastPlatformPos;

    public AudioClip bgmClip;
    private AudioSource audioSource;

    void Start()
    {
        nowPos = startPos;
        StartCoroutine(MakePlatforms());

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = bgmClip;
        audioSource.loop = true;
        audioSource.playOnAwake = true;
        audioSource.Play();
    }

    IEnumerator MakePlatforms()
    {
        for (int i = 0; i < 34; i++)
        {
            GameObject makePlatform;

            if (i < 31)
            {
                makePlatform = GetPlatform();
            }
            else if (i == 31)
            {
                makePlatform = jumpPlatform;
            }
            else if (i == 32)
            {
                makePlatform = normalPlatform;
            }
            else
            {
                makePlatform = endPlatform;
            }

            if (lastPlatform == movePlatform)
            {
                nowPos += Vector3.forward * afterMoveGap;
            }
            else if (lastPlatform == jumpPlatform)
            {
                nowPos += Vector3.forward * 8f;
            }
            else
            {
                nowPos += Vector3.forward * Random.Range(gap - 2f, gap + 3f);
            }

            Vector3 makePos = nowPos;
            makePos.y += nowHeight;

            GameObject newPlatform = Instantiate(makePlatform, makePos, Quaternion.identity);

            if (i == 33)
            {
                GameObject newitem = Instantiate(Item[Random.Range(0,Item.Length)], makePos, Quaternion.identity);
            }

            if (makePlatform == jumpPlatform)
            {
                nowHeight += jumpUp;
            }

            lastPlatform = makePlatform;
            lastPlatformPos = nowPos;

            yield return new WaitForSeconds(0.1f);
        }
    }

    GameObject GetPlatform()
    {
        int random = Random.Range(1, 101);

        if (random <= 70)
            return normalPlatform;

        if (random <= 80 && jumpPlatform != null)
            return jumpPlatform;

        if (random <= 90 && movePlatform != null)
            return movePlatform;

        if (itemPlatform != null)
            return itemPlatform;

        return normalPlatform;
    }

    public void SpawnNext()
    {

        StartCoroutine(MakePlatforms());
    }

}
