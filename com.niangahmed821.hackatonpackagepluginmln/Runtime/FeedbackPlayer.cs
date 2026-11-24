using UnityEngine;
using UnityEngine.Events;

namespace MLNFeedbacks
{
    /// <summary>
    /// Lance tous les feedbacks présents sur cet objet, en une ligne :
    /// <c>GetComponent&lt;FeedbackPlayer&gt;().Play();</c>
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("MLN Feedbacks/Feedback Player")]
    public class FeedbackPlayer : MonoBehaviour
    {
        /// <summary>
        /// Interrupteur général : à faux, plus aucun FeedbackPlayer ne joue.
        /// Pratique pour une option « effets visuels » dans un menu.
        /// </summary>
        public static bool GlobalEnabled = true;

        [Tooltip("Lancer aussi les feedbacks posés sur les objets enfants.")]
        public bool includeChildren = false;

        [Tooltip("Appelé à chaque fois que les feedbacks sont lancés.")]
        public UnityEvent onPlay = new UnityEvent();

        /// <summary>Les feedbacks que ce player lance.</summary>
        public Feedback[] Feedbacks =>
            includeChildren ? GetComponentsInChildren<Feedback>() : GetComponents<Feedback>();

        /// <summary>
        /// Lance tous les feedbacks de l'objet en même temps.
        /// </summary>
        public void Play()
        {
            if (!GlobalEnabled || !isActiveAndEnabled)
                return;

            foreach (Feedback feedback in Feedbacks)
                feedback.Play();

            onPlay.Invoke();
        }

        /// <summary>
        /// Arrête tous les feedbacks et remet l'objet dans son état d'origine.
        /// </summary>
        public void Stop()
        {
            foreach (Feedback feedback in Feedbacks)
                feedback.Stop();
        }
    }
}
