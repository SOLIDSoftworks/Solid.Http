using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SolidHttp.Extensions.Testing.MSTest
{
    public class MSTestAsserter : IAsserter
    {
        public void AreEqual(object expected, object actual, string message)
        {
            Assert.AreEqual(expected, actual, message);
        }

        public void AreEqual<T>(T expected, T actual, string message)
        {
            Assert.AreEqual<T>(expected, actual, message);
        }

        public void AreNotEqual<T>(T notExpected, T actual, string message)
        {
            Assert.AreNotEqual(notExpected, actual, message);                
        }

        public void AreNotEqual(object notExpected, object actual, string message)
        {
            Assert.AreNotEqual(notExpected, actual, message);
        }

        public void AreNotSame(object notExpected, object actual, string message)
        {
            Assert.AreNotSame(notExpected, actual, message);
        }

        public void AreSame(object expected, object actual, string message)
        {
            Assert.AreSame(expected, actual, message);
        }

        public void Fail(string message)
        {
            Assert.Fail(message);
        }

        public void IsFalse(bool value, string message)
        {
            Assert.IsFalse(value, message);
        }

        public void IsInstanceOfType<TExpected>(object actual, string message)
        {
            Assert.IsInstanceOfType(actual, typeof(TExpected), message);
        }

        public void IsInstanceOfType(Type expected, object actual, string message)
        {
            Assert.IsInstanceOfType(actual, expected, message);
        }

        public void IsNotInstanceOfType<TNotExpected>(object actual, string message)
        {
            Assert.IsNotInstanceOfType(actual, typeof(TNotExpected), message);
        }

        public void IsNotInstanceOfType(Type notExpected, object actual, string message)
        {
            Assert.IsNotInstanceOfType(actual, notExpected, message);
        }

        public void IsNotNull(object value, string message)
        {
            Assert.IsNotNull(value, message);
        }

        public void IsNull(object value, string message)
        {
            Assert.IsNull(value, message);
        }

        public void IsTrue(bool value, string message)
        {
            Assert.IsTrue(value, message);
        }
    }
}
