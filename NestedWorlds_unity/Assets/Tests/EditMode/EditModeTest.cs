using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EditModeTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void EditModeTestSimplePasses()
    {
        int test = 0;
        test++;
        Assert.AreEqual(test, 1);
        // Use the Assert class to test conditions
    }
}
