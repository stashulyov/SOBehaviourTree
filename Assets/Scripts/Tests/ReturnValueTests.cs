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
        public void OnUpdate_SetResult_ReturnsIt(Result expectedResult)
        {
            var task = ScriptableObject.CreateInstance<ReturnValue>();
            task.Result = expectedResult;

            var result = task.OnUpdate(0f);

            Assert.AreEqual(expectedResult, result);
        }
    }
}