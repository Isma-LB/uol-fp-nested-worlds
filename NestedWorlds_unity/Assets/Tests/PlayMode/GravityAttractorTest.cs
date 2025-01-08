using System.Collections;
using IsmaLB;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


public class GravityAttractorTest
{
    [Test]
    public void AlignFunction()
    {
        GameObject planet = new();
        GravityAttractor target = planet.AddComponent<GravityAttractor>();

        GameObject go = new();
        Rigidbody rb = go.AddComponent<Rigidbody>();
        rb.position = Vector3.left;
        Assert.AreEqual(target.Align(rb, 1), Quaternion.AngleAxis(90, Vector3.forward));
    }
    [UnityTest]
    public IEnumerator AttractFunction()
    {
        GameObject planet = new();
        GravityAttractor target = planet.AddComponent<GravityAttractor>();

        GameObject go = new();
        Rigidbody rb = go.AddComponent<Rigidbody>();
        rb.position = Vector3.left;
        float distance = Vector3.Distance(rb.position, planet.transform.position);
        target.Attract(rb);
        yield return null;
        Assert.Less(Vector3.Distance(rb.position, planet.transform.position), distance);
    }
}
