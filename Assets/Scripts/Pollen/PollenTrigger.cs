using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollenTrigger : MonoBehaviour 
{

	public Bud Bud;

	private void OnTriggerEnter2D (Collider2D collider) 
	{
		PlayerPollen _player = collider.gameObject.GetComponent<PlayerPollen> ();
		PollenGrain _grain = collider.gameObject.GetComponent<PollenGrain> ();
		if (_player) {
			if (_player.PlayerPollenColor == Bud.PollenColor && PlayerPollen.GrainCount > 0) {
				Bud.EnablePlatforms ();
				_player.DestroyOneGrain ();
			}
		} else if (_grain) {
			if (_grain.GrainColor == Bud.PollenColor) {
				Destroy (_grain.gameObject);
				Bud.EnablePlatforms ();
			}
		}
	}
}
