using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayParticleOnCharacterDeath : MonoBehaviour {

    public Robo_CharacterController targetCharacter;
    ParticleSystem particle;

    // This allows us to re-use the same particle over and over.
	void Start()
    {
        particle = GetComponent<ParticleSystem>();

        targetCharacter.Died += Character_Died;
    }

    private void Character_Died(Robo_CharacterController character)
    {
        particle.transform.position = character.transform.position;
        particle.Clear();
        particle.Play();
    }
}
