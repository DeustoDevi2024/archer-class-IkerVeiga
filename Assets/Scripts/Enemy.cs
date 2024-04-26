using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Archer
{

    public class Enemy : MonoBehaviour, IScoreProvider
    {

        // Cuántas vidas tiene el enemigo
        [SerializeField]
        private int hitPoints;

        private int hits = 0;

        private Animator animator;

        public event IScoreProvider.ScoreAddedHandler OnScoreAdded;

        Coroutine coroutine;

        private AudioSource myAudioSource;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            myAudioSource = GetComponent<AudioSource>();
        }

        // Método que se llamará cuando el enemigo reciba un impacto
        public void Hit()
        {
            myAudioSource.Play();
            hits++;
            if (hits >= hitPoints)
            {
                Die();
            }
            Debug.Log(hits);
            Debug.Log("HIT");
        }

        IEnumerator DieCoroutine()
        {
            GameObject.Find("Directional Light").GetComponent<Light>().intensity = 2f;
            yield return new WaitForSeconds(3);
            GameObject.Find("Directional Light").GetComponent<Light>().intensity = 0;
            Destroy(this.gameObject);
        }

        private void Die()
        {
            animator.SetTrigger("Die");
            coroutine = StartCoroutine(DieCoroutine());

        }

        
    }

}