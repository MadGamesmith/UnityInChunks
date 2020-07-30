//Script by Andy Noworol /twitter => @andynoworol
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MadConsole : MonoBehaviour {
    public static MadConsole instance;

    [Header("Settings and References")]
    public KeyCode activationKey;
    public GameObject consoleGameObject;
    public InputField consoleInput;

    [Header("Hacks")]
    public ConsoleHack[] hacks;

    private bool consoleOn;

    private void Awake() {
        DontDestroyOnLoad(gameObject);
        if (instance) Destroy(gameObject);
        else instance = this;
    }

    private void Update() {
        if (Debug.isDebugBuild) {
            InputCheck();
        }
    }

    private void InputCheck() {
        if (Input.GetKeyDown(activationKey)) {
            consoleOn = !consoleOn;
            consoleGameObject.SetActive(consoleOn);
            if (consoleOn) {
                StartCoroutine(ClearAndSelectInputField(""));
            }
        }
    }

    private IEnumerator ClearAndSelectInputField(string message) {
        yield return new WaitForEndOfFrame();
        consoleInput.text = message;
        consoleInput.Select();
        consoleInput.ActivateInputField();
    }

    public void CommitConsoleEntry(string entry) {
        for (int i = 0; i < hacks.Length; i++) {
            if (hacks[i].callKey == entry) {
                hacks[i].hack.Invoke();
                StartCoroutine(ClearAndSelectInputField(hacks[i].feedbackMessage));
                return;
            }
        }
        StartCoroutine(ClearAndSelectInputField(""));
    }
    public bool ConsoleOn() {
        return consoleOn;
    }
}


