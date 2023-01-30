using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private GameObject cureChange;
    [SerializeField] private float scrollSpeed = 0.5f;
    [SerializeField] private List<GameObject> mapFactor = new List<GameObject>();

    private int index = 1;

    private void FixedUpdate()
    {
        Movement();
        ScrollToMapFactor();
    }

    private void Movement()
    {
        // Scroll Move.... Down.
        transform.Translate(scrollSpeed * Time.deltaTime * Vector3.down);
    }

    private void ScrollToMapFactor()
    {
        // scroll standard position
        if (mapFactor[index].transform.position.y > -19.0f) return;

        // scroll 0, 19, 0
        mapFactor[index].transform.position = new Vector3(0.0f, 19.0f, 0.0f);

        // change Index
        index = index == 1 ? 0 : 1;
    }

    public IEnumerator ChangeMapToInfection()
    {
        // hard Code : Instantiate CureChange, Destroy CureChange, Change Animator Trigger
        GameObject cureChange = Instantiate(this.cureChange, Vector3.zero, Quaternion.identity);
        Destroy(cureChange, cureChange.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);

        yield return new WaitForSeconds(cureChange.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);

        for (int i = 0; i < mapFactor.Count; i++)
        {
            mapFactor[i].GetComponent<Animator>().ResetTrigger("Clear1");
            mapFactor[i].GetComponent<Animator>().ResetTrigger("Clear2");
            mapFactor[i].GetComponent<Animator>().SetTrigger("Infection");
        }
    }

    public IEnumerator ChangeMapToCure(int stage)
    {
        // hard Code : Instantiate CureChange, Destroy CureChange, Change Animator Trigger
        GameObject cureChange = Instantiate(this.cureChange, Vector3.zero, Quaternion.identity);
        Destroy(cureChange, cureChange.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);

        yield return new WaitForSeconds(cureChange.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);

        for (int i = 0; i < mapFactor.Count; i++)
        {
            mapFactor[i].GetComponent<Animator>().ResetTrigger("Clear" + stage.ToString());
            mapFactor[i].GetComponent<Animator>().SetTrigger("Infection");
        }
    }
}
