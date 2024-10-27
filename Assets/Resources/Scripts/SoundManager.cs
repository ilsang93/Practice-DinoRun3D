using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] private AudioClip[] sfxs;
    private AudioSource audioSource;

    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySfxOneshot(SfxEnum sfx)
    {
        audioSource.PlayOneShot(sfxs[(int)sfx]);
    }
}

public enum SfxEnum
{
    Click,
    DinoDie,
    DinoHit,
    DoorHit,
    GameOver
}
