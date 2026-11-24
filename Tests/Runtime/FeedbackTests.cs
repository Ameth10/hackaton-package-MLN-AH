using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace MLNFeedbacks.Tests
{
    /// <summary>
    /// Tests en mode Play : chaque feedback agit pendant l'effet,
    /// puis remet l'objet exactement dans son état d'origine.
    /// </summary>
    public class FeedbackTests
    {
        GameObject _cube;

        [SetUp]
        public void SetUp()
        {
            FeedbackPlayer.GlobalEnabled = true;
            _cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        }

        [TearDown]
        public void TearDown()
        {
            FeedbackPlayer.GlobalEnabled = true;
            Object.Destroy(_cube);
        }

        [UnityTest]
        public IEnumerator PunchScale_GrossitPuisRevientALaTailleDOrigine()
        {
            var punch = _cube.AddComponent<PunchScaleFeedback>();
            punch.duration = 0.2f;

            punch.Play();
            yield return new WaitForSeconds(0.05f);
            Assert.Greater(_cube.transform.localScale.x, 1f, "Le cube doit grossir pendant l'effet.");

            yield return new WaitForSeconds(0.3f);
            Assert.AreEqual(Vector3.one, _cube.transform.localScale, "Le cube doit revenir à sa taille d'origine.");
        }

        [UnityTest]
        public IEnumerator Shake_BougePuisRevientALaPositionDOrigine()
        {
            var start = new Vector3(1f, 2f, 3f);
            _cube.transform.localPosition = start;
            var shake = _cube.AddComponent<ShakeFeedback>();
            shake.duration = 0.2f;
            shake.strength = 0.5f;

            shake.Play();
            yield return new WaitForSeconds(0.05f);
            Assert.AreNotEqual(start, _cube.transform.localPosition, "Le cube doit bouger pendant l'effet.");

            yield return new WaitForSeconds(0.3f);
            Assert.AreEqual(start, _cube.transform.localPosition, "Le cube doit revenir à sa position d'origine.");
        }

        [UnityTest]
        public IEnumerator ColorFlash_ChangeLaCouleurPuisRevient()
        {
            var renderer = _cube.GetComponent<Renderer>();
            string property = renderer.sharedMaterial.HasProperty("_BaseColor") ? "_BaseColor" : "_Color";
            // Couleur d'origine telle qu'un MaterialPropertyBlock la stocke
            // (il peut la convertir en espace linéaire, d'où ce passage).
            var reference = new MaterialPropertyBlock();
            reference.SetColor(property, renderer.sharedMaterial.GetColor(property));
            Color start = reference.GetColor(property);

            var flash = _cube.AddComponent<ColorFlashFeedback>();
            flash.duration = 0.2f;
            flash.flashColor = new Color(1f, 0f, 0f, 1f);
            var block = new MaterialPropertyBlock();

            flash.Play();
            yield return new WaitForSeconds(0.05f);
            renderer.GetPropertyBlock(block);
            Assert.AreNotEqual(start, block.GetColor(property), "La couleur doit changer pendant l'effet.");

            yield return new WaitForSeconds(0.3f);
            renderer.GetPropertyBlock(block);
            Assert.AreEqual(start, block.GetColor(property), "La couleur doit revenir à l'origine.");
        }

        [UnityTest]
        public IEnumerator FeedbackPlayer_LanceTousLesFeedbacks()
        {
            _cube.AddComponent<PunchScaleFeedback>().duration = 0.2f;
            var shake = _cube.AddComponent<ShakeFeedback>();
            shake.duration = 0.2f;
            shake.strength = 0.5f;
            var player = _cube.AddComponent<FeedbackPlayer>();

            player.Play();
            yield return new WaitForSeconds(0.05f);
            Assert.Greater(_cube.transform.localScale.x, 1f, "Le punch doit être lancé.");
            Assert.AreNotEqual(Vector3.zero, _cube.transform.localPosition, "Le shake doit être lancé.");
        }

        [UnityTest]
        public IEnumerator FeedbackPlayer_NeJouePasSiDesactiveGlobalement()
        {
            _cube.AddComponent<PunchScaleFeedback>().duration = 0.2f;
            var player = _cube.AddComponent<FeedbackPlayer>();
            FeedbackPlayer.GlobalEnabled = false;

            player.Play();
            yield return new WaitForSeconds(0.05f);
            Assert.AreEqual(Vector3.one, _cube.transform.localScale, "Aucun feedback ne doit jouer.");
        }

        [UnityTest]
        public IEnumerator Relancer_PendantLeDelai_NeCassePasLObjet()
        {
            var punch = _cube.AddComponent<PunchScaleFeedback>();
            punch.delay = 0.1f;
            punch.duration = 0.2f;

            punch.Play();
            yield return new WaitForSeconds(0.02f);
            punch.Play();
            yield return null;
            Assert.AreEqual(Vector3.one, _cube.transform.localScale, "Relancer pendant le délai ne doit pas modifier l'objet.");

            yield return new WaitForSeconds(0.5f);
            Assert.AreEqual(Vector3.one, _cube.transform.localScale);
        }
    }
}
