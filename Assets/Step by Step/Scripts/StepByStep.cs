using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class StepByStep : MonoBehaviour {

	[Header("Order, es si va en orden XD")]
	[SerializeField]
	bool Order = true;


	[Header("Objeto o Audio para Intro")]
	[SerializeField]
	AudioClip introAudio;

	[SerializeField]
	GameObject objectIntro;

	[Header("Objeto o Audio para Final")]
	[SerializeField]
	AudioClip finalTutoAudio;

	[SerializeField]
	GameObject finalTutoObject;

	[Header("Informacion")]
	[SerializeField]
	private int currentStep;
	
	public int CurrentStep { get { return currentStep; } set { currentStep = value; } }

	[SerializeField]
	private int InitStep;

	[SerializeField]
	List<Step> steps;

	AudioSource audio;

	bool empezar = false;

	[Serializable]
	public class Step
	{

		public string nombreDelPaso;

		public AudioClip audioIntro;

		public AudioClip audioFinal;

		public bool activo = false;

		public bool cumplido = false;

	}

	void Start () {
		currentStep = InitStep;
		StartCoroutine(empezarTuto());
		audio = GetComponent<AudioSource>();
		if (introAudio != null) {
			StartCoroutine(audioIntro(introAudio, true));
		}
		else {
			if (objectIntro != null)
			{
				StartCoroutine(objetoIntro(objectIntro, true));
			}
			else {
				empezar = true;
			}
		}

	}

	IEnumerator audioIntro(AudioClip audioClip, bool begin) {
		audio.clip = audioClip;
		audio.Play();
		yield return new WaitUntil(() => audio.isPlaying);
		empezar = begin;
	}

	IEnumerator objetoIntro(GameObject obj, bool begin) {
		obj.SetActive(true);
		yield return new WaitUntil(() => (!obj.activeSelf || obj == null));
		empezar = begin;
	}

	IEnumerator empezarTuto() {
		yield return new WaitUntil(() => !empezar);
		SetCurrentStep(steps[currentStep]);
		yield return new WaitWhile(() => empezar);
	}

	public void NextStep() {
		steps[currentStep].activo = false;
		currentStep++;
		if (currentStep < steps.Count)
		{
			SetCurrentStep(steps[currentStep]);
		}
		else {
			//Final Tutorial
			if (finalTutoAudio != null)
			{
				StartCoroutine(audioIntro(finalTutoAudio,false));
			}
			else
			{
				if (finalTutoObject != null)
				{
					StartCoroutine(objetoIntro(finalTutoObject,false));
				}

			}
		}
	}

	public void SetStepByIndex(int index) {
		SetCurrentStep(steps[index]);
	}

	void SetCurrentStep(Step step) {
		step.activo = true;
	}

	public string GetStepName(int index) {
		return steps[index].nombreDelPaso;
	}

	public bool GetStepState(int index)
	{
		return steps[index].activo;
	}
	public AudioClip GetStepIntroVideo(int index) {
		return steps[index].audioIntro;
	}

	public void SetStepCumplido(int index) {
		steps[index].cumplido = true;
	}
	public AudioClip GetStepFinalVideo(int index)
	{
		return steps[index].audioFinal;
	}
}
