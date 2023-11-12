using UnityEngine;
using System.Collections;

public class OpenTrunk : MonoBehaviour {

	// on définit une variable privée qui va contenir le composant Animator de l'objet sur lequel est appliqué le script
	private Animator Opening;
	public Transform trunk;
	// Fonction Start se lance 1 fois au lancement du jeu
	void Start () {
		Opening = trunk.GetComponent<Animator>();
	}

	private void OnTriggerEnter (Collider collision) {
		if (collision.gameObject.name == "key") {
			if (Opening.GetInteger ("EtatAnim") == 1) {
				Opening.SetInteger("EtatAnim",2);
				GameManager.instance.opened = true;
			} else {
				Opening.SetInteger("EtatAnim",1);
			};
		}
	}
}
