# Changelog
All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [1.0.0] - 2026-11-24

### Ajouté
- Classe de base `Feedback` : délai, durée, intensité, courbe et retour automatique à l'état d'origine.
- `FeedbackPlayer` : lance tous les feedbacks d'un objet en une ligne, événement `onPlay`, interrupteur général `FeedbackPlayer.GlobalEnabled`.
- Feedbacks de transforms : `PunchScaleFeedback` et `ShakeFeedback`.
- Feedback de matériau : `ColorFlashFeedback` (sans copier le matériau, compatible URP et pipeline classique).
- Feedback VFX et son : `VfxSoundFeedback`.
- Inspector du `FeedbackPlayer` : liste des feedbacks et boutons « Tester » / « Arrêter ».
- Sample « Demo (stand de tir) » : trois cibles, score, touche F pour activer ou désactiver les feedbacks.
- Tests en mode Play pour chaque feedback.
- Documentation : démarrage rapide et référence des feedbacks.

## [0.3.2] - 2026-11-24

### Ajouté
- Installation par la branche `upm` du dépôt, avec une adresse plus courte : `https://github.com/Ameth10/hackaton-package-mln.git#upm`.
- Tags `upm/vX.Y.Z` pour installer une version précise par cette branche.

### Modifié
- Documentation et README : adresse d'installation `#upm`.

## [0.3.1] - 2026-11-24

### Ajouté
- Documentation utilisateur dans `Documentation~/hackatonpackagepluginmln.md`, conformément à la structure officielle des packages Unity.

### Modifié
- Le README du package devient un résumé avec un lien vers la documentation.
- `Third Party Notices.md` indique qu'aucun composant tiers n'est utilisé.

### Supprimé
- Fichier `.sample.json` en double (le sample est déclaré dans `package.json`).

## [0.3.0] - 2026-11-24

### Ajouté
- Licence open source MIT (`LICENSE.md`).

## [0.2.0] - 2026-11-24

### Modifié
- Nom affiché du package : **MLN Feedbacks**.
- Compatible à partir de Unity 6000.0 (au lieu de 6000.6 uniquement).
- Description et liens (documentation, changelog) mis à jour.
- La documentation est maintenant dans le README du package.

### Supprimé
- Tests d'exemple et dépendance au Test Framework.
- Modèle de documentation `Documentation~`.

## [0.1.0] - 2026-11-24

### Ajouté
- Première version : structure du package UPM (Runtime, Editor, sample).
