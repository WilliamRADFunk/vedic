using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GifAnimator : MonoBehaviour
{
    int index = 0;
    [SerializeField]
    private string animatedFolder;
    [SerializeField]
    private GameObject frame;
    private Sprite[] faceAnimations;
    [SerializeField]
    private Sprite backupFrame;
    [SerializeField]
    private int framesPerSecond = 10;
    void Start()
    {
        faceAnimations = Resources.LoadAll<Sprite>(animatedFolder);

        frame.GetComponent<SpriteRenderer>().sprite = backupFrame;
    }

    // Update is called once per frame
    void Update ()
    {
        // Calculate index
        int index = (int)Mathf.Ceil(Time.time * framesPerSecond);
        // repeat when exhausting all frames
        index = (faceAnimations.Length > 0) ? index % faceAnimations.Length : 0;

        if (faceAnimations.Length > 0)
        {
            frame.GetComponent<SpriteRenderer>().sprite = faceAnimations[index];
        }
    }
}