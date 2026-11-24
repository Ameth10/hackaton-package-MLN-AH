using UnityEngine;

namespace MLNFeedbacks
{
    /// <summary>
    /// Combinaison VFX et son : fait apparaître des particules et joue un son en même temps.
    /// </summary>
    [AddComponentMenu("MLN Feedbacks/VFX + Sound")]
    public class VfxSoundFeedback : Feedback
    {
        [Header("VFX")]
        [Tooltip("Prefab de particules à faire apparaître sur l'objet (optionnel).")]
        public ParticleSystem vfxPrefab;

        [Tooltip("Décalage de l'apparition des particules par rapport à l'objet.")]
        public Vector3 vfxOffset = Vector3.zero;

        [Header("Son")]
        [Tooltip("Son à jouer (optionnel).")]
        public AudioClip sound;

        [Range(0f, 1f)] public float volume = 1f;

        [Tooltip("Variation aléatoire de la hauteur du son, pour qu'il ne soit pas toujours identique.")]
        [Range(0f, 0.5f)] public float pitchVariation = 0.1f;

        AudioSource _audioSource;

        protected override void OnStart()
        {
            if (vfxPrefab != null)
            {
                ParticleSystem vfx = Instantiate(vfxPrefab, transform.position + vfxOffset, Quaternion.identity);
                vfx.Play();
                ParticleSystem.MainModule main = vfx.main;
                Destroy(vfx.gameObject, main.duration + main.startLifetime.constantMax);
            }

            if (sound != null)
            {
                if (_audioSource == null)
                {
                    _audioSource = GetComponent<AudioSource>();
                    if (_audioSource == null)
                    {
                        _audioSource = gameObject.AddComponent<AudioSource>();
                        _audioSource.playOnAwake = false;
                    }
                }

                _audioSource.pitch = 1f + Random.Range(-pitchVariation, pitchVariation);
                _audioSource.PlayOneShot(sound, volume);
            }
        }

        // Les particules et le son se jouent tout seuls : rien à animer ni à remettre en état.
        protected override void Animate(float value) { }

        protected override void ResetState() { }
    }
}
