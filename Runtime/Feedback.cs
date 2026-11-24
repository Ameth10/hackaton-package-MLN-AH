using System.Collections;
using UnityEngine;

namespace MLNFeedbacks
{
    /// <summary>
    /// Classe de base de tous les feedbacks.
    /// Elle gère ce qui est commun à tous les effets : le délai, la durée, l'intensité,
    /// la courbe et le retour à l'état d'origine. Un effet (punch, shake...) hérite de
    /// cette classe et n'écrit que ce qui lui est propre dans <see cref="Animate"/> et
    /// <see cref="ResetState"/>.
    /// </summary>
    /// <example>
    /// Créer son propre feedback :
    /// <code>
    /// public class MonFeedback : Feedback
    /// {
    ///     protected override void Animate(float value) { /* appliquer l'effet */ }
    ///     protected override void ResetState() { /* remettre comme avant */ }
    /// }
    /// </code>
    /// </example>
    public abstract class Feedback : MonoBehaviour
    {
        [Tooltip("Attente avant le début de l'effet, en secondes.")]
        [Min(0f)] public float delay = 0f;

        [Tooltip("Durée de l'effet, en secondes.")]
        [Min(0.01f)] public float duration = 0.3f;

        [Tooltip("Force de l'effet (1 = normal, 2 = deux fois plus fort).")]
        [Min(0f)] public float intensity = 1f;

        [Tooltip("Forme de l'effet dans le temps. Horizontal : de 0 (début) à 1 (fin). Vertical : force de l'effet.")]
        public AnimationCurve curve = DefaultCurve();

        Coroutine _routine;
        bool _started;   // vrai une fois OnStart() appelé : l'état de départ est mémorisé

        /// <summary>Vrai tant que l'effet est en cours (délai compris).</summary>
        public bool IsPlaying => _routine != null;

        /// <summary>
        /// Lance l'effet. S'il est déjà en cours, il est remis à zéro puis relancé.
        /// </summary>
        public void Play()
        {
            if (!isActiveAndEnabled)
                return;

            Stop();
            _routine = StartCoroutine(PlayRoutine());
        }

        /// <summary>
        /// Arrête l'effet et remet immédiatement l'objet dans son état d'origine.
        /// </summary>
        public void Stop()
        {
            if (_routine == null)
                return;

            StopCoroutine(_routine);
            _routine = null;

            // Si on est encore dans le délai, rien n'a bougé : pas de remise en état.
            if (_started)
            {
                _started = false;
                ResetState();
            }
        }

        IEnumerator PlayRoutine()
        {
            if (delay > 0f)
                yield return new WaitForSeconds(delay);

            OnStart();
            _started = true;

            float t = 0f;
            while (t < 1f)
            {
                t = Mathf.Min(1f, t + Time.deltaTime / duration);
                Animate(curve.Evaluate(t) * intensity);
                yield return null;
            }

            ResetState();
            _started = false;
            _routine = null;
        }

        /// <summary>
        /// Appelée une fois au début de l'effet, après le délai.
        /// Sert à mémoriser l'état de départ ou à lancer quelque chose (particules, son...).
        /// </summary>
        protected virtual void OnStart() { }

        /// <summary>
        /// Appelée à chaque frame pendant l'effet.
        /// </summary>
        /// <param name="value">Valeur de la courbe multipliée par l'intensité (0 = pas d'effet).</param>
        protected abstract void Animate(float value);

        /// <summary>
        /// Remet l'objet exactement dans son état d'origine.
        /// </summary>
        protected abstract void ResetState();

        /// <summary>
        /// Si l'objet est désactivé pendant l'effet, on le remet dans son état d'origine.
        /// </summary>
        protected virtual void OnDisable() => Stop();

        /// <summary>
        /// Courbe par défaut : l'effet monte vite puis redescend doucement.
        /// </summary>
        protected static AnimationCurve DefaultCurve() =>
            new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(0.2f, 1f), new Keyframe(1f, 0f));
    }
}
