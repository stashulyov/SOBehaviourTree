using Engine;
using Extensions;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class ReturnValueTests
    {
        [TestCase(Result.Success)]
        [TestCase(Result.Failure)]
        [TestCase(Result.Running)]
        public void OnUpdate_SetResult_ReturnsIt(Result resultToSet)
        {
            var returnValue = ScriptableObject.CreateInstance<ReturnValue>();
            returnValue.Result = resultToSet;

            var result = returnValue.OnUpdate(0f);

            Assert.IsTrue(result == resultToSet);
        }
    }
}