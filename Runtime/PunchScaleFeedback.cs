using UnityEngine;

namespace MLNFeedbacks
{
    /// <summary>
    /// Highlight de transform : l'objet grossit puis revient à sa taille d'origine.
    /// </summary>
    [AddComponentMenu("MLN Feedbacks/Punch Scale")]
    public class PunchScaleFeedback : Feedback
    {
        [Tooltip("Agrandissement maximal (0.3 = +30 %).")]
        public float strength = 0.3f;

        Vector3 _startScale;

        protected override void OnStart() => _startScale = transform.localScale;

        protected override void Animate(float value) =>
            transform.localScale = _startScale * (1f + value * strength);

        protected override void ResetState() => transform.localScale = _startScale;

        void Reset()
        {
            duration = 0.4f;
            // Grossit, rebondit un peu en dessous de la taille normale, puis se stabilise.
            curve = new AnimationCurve(
                new Keyframe(0f, 0f), new Keyframe(0.25f, 1f), new Keyframe(0.55f, -0.3f),
                new Keyframe(0.8f, 0.1f), new Keyframe(1f, 0f));
        }
    }
}
