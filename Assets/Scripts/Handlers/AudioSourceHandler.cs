using DG.Tweening;
using UnityEngine;

public class AudioSourceHandler : MonoBehaviour
{
    private float volumeScale;
    private AudioSource audioSource;
    private Transform player;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        player = Managers.Game.Player.transform;
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        audioSource.volume = Mathf.Clamp01(1 - (distance / 20.0f)) * volumeScale;
        audioSource.panStereo = Mathf.Clamp((player.position.x - transform.position.x) * 0.1f, -1f, 1f);
    }

    public void PlayOneShot(AudioClip clip, float volumeScale)
    {
        this.volumeScale = volumeScale;
        audioSource.clip = clip;
        audioSource.volume = volumeScale;
        audioSource.Play();

        DOVirtual.DelayedCall(clip.length, () => Managers.Resource.Destroy(gameObject));
    }
}