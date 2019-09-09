using UnityEngine;

public class Torch : MonoBehaviour
{
    public GameObject fireParticle;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void lightOnFire()
    {
        fireParticle.SetActive(true);
    }
}