using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class BulletAudio : MonoBehaviour
{
    [Header("音效设置")]
    public AudioClip fireSound;
    [Range(0, 1)] public float volume = 0.8f;
    public float spatialBlend = 0.3f;

    [Header("高级设置")]
    public bool allowMultiple = false; // 是否允许多音效叠加
    public float maxSoundDistance = 20f;

    private static readonly HashSet<BulletAudio> ActiveInstances = new();
    private AudioSource _audioSource;

    void Awake()
    {
        InitializeAudioSource();
        ManageAudioPlayback();
    }

    void OnDestroy()
    {
        ActiveInstances.Remove(this);
    }

    void InitializeAudioSource()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _audioSource.spatialBlend = spatialBlend;
        _audioSource.volume = volume;
        _audioSource.maxDistance = maxSoundDistance;
        _audioSource.rolloffMode = AudioRolloffMode.Linear;
    }

    void ManageAudioPlayback()
    {
        // 非叠加模式时停止其他实例
        if (!allowMultiple)
        {
            foreach (var instance in ActiveInstances)
            {
                if (instance != this && instance._audioSource.isPlaying)
                {
                    instance._audioSource.Stop();
                }
            }
        }

        ActiveInstances.Add(this);
        PlaySound();
    }

    void PlaySound()
    {
        if (fireSound == null)
        {
            Debug.LogWarning("缺少子弹音效文件", this);
            return;
        }

        // 使用3D音效设置
        _audioSource.PlayOneShot(fireSound);

        // 自动销毁组件（可选）
        if (!allowMultiple)
        {
            Destroy(this, fireSound.length + 0.1f);
        }
    }
}