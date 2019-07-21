using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TrueSingleton<T> : Singleton<T> where T : MonoBehaviour {
    protected override void Awake() {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);
    }
}
public class PersistentAudio : TrueSingleton<PersistentAudio> {

}
