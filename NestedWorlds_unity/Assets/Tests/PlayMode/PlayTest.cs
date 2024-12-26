using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void PlayTestSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator PlayTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        GameObject game = new GameObject();
        game.AddComponent<Rigidbody>();
        yield return null;
        Assert.IsTrue(game.TryGetComponent<Rigidbody>(out var component));
    }
}
