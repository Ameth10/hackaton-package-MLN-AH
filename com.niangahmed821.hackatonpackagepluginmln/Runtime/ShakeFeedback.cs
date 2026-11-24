using UnityEngine;

namespace MLNFeedbacks
{
    /// <summary>
    /// Highlight de transform : l'objet tremble autour de sa position d'origine.
    /// </summary>
    [AddComponentMenu("MLN Feedbacks/Shake")]
    public class ShakeFeedback : Feedback
    {
        [Tooltip("Distance maximale du tremblement, en unités Unity.")]
        public float strength = 0.1f;

        Vector3 _startPosition;

        protected override void OnStart() => _startPosition = transform.localPosition;

        protected override void Animate(float value) =>
            transform.localPosition = _startPosition + Random.insideUnitSphere * (strength * value);

        protected override void ResetState() => transform.localPosition = _startPosition;

        void Reset()
        {
            duration = 0.25f;
            // Fort tout de suite, puis s'atténue.
            curve = AnimationCurve.Linear(0f, 1f, 1f, 0f);
        }
    }
}
