using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomepageHandler : MonoBehaviour
{
    [SerializeField] private List<GameObject> interactableObjects;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // FixedUpdate is called at a specified fixed rate
    void FixedUpdate() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f)) {
            Debug.Log($"Hit Interactable:{hit.transform.tag}");
        }
    }
}

public enum PageState {
    HOME,
    ABOUT_ME,
    PORTFOLIO
}
